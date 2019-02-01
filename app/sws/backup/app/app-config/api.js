"use strict";
var api = angular.module('api', [ 'toaster', 'ui.bootstrap' ]);

api.service('helper',
['$http',
'toaster',
'$location',
'$timeout','$rootScope',
function($http, toaster, $location, $timeout,$rootScope) {

	var SERVER_URL = 'http://admfrotas.com.br/sws/';
	var ext = ''; //->para qual tipo de aplicacao está sendo consumida os dados.

	return {
		get : function(url, id, sucess) {	
			//$rootScope.$broadcast('abrirLoad');
			url = SERVER_URL + url + ext;
			url = (id === null) ? url : url + "/"+ id;
			return $http({
				method : 'GET',				
				url : url
			}).success(sucess);
		},
		post : function(url, data, sucess) {	
			$rootScope.$broadcast('abrirLoad');
			url = SERVER_URL + url + ext;
			return $http({
				method : 'POST',
				url : url,
				data : data
			}).success(sucess);
		},
		msgAdvertencia:function(msg){
			toaster.publicarAdvertencia(msg);
		},
		mensagem : function(msg) {
			/*
			 * Tipo: Sucesso = 1, Aviso = 2, Advertência =
			 * 3, Erro = 4
			 */	
			if(msg===null) return;
			var tipo = parseInt(msg[0].Tipo);
			
			if ( tipo === 1) {
				toaster.publicarSucesso(msg[0].Mensagem);

			} else if (tipo === 2) {
				angular.forEach(msg,function(value, key) {
					toaster.publicarInformacao(value.Mensagem);
				});
			}else if (tipo === 3) {
				angular.forEach(msg,function(value, key) {
					toaster.publicarAdvertencia(value.Mensagem);
				});
			}else if (tipo === 4) {
				angular.forEach(msg,function(value, key) {
					toaster.publicarErro(value.Mensagem);
				});
			}
		},
		redirecionar : function(url, time) {
			if (time === '' || time === null
					|| time === undefined) {
				$location.path(url);
			} else {
				$timeout(function() {
					$location.path(url);
				}, (time * 1000));// tempo em segundos
			}
		},
		
		toList:function(objeto){				
			if (objeto !== 'null' && objeto !== '' && objeto!==undefined && objeto!==null) {	
				if (objeto.length === undefined) {
					return [objeto];
				}			
				return objeto;
			} else {
				return [];			
			}
		},
		path:function(){
			return $location.absUrl().substr(0,$location.absUrl().indexOf("#"));
		},
		loadingOpen:function(){
			$rootScope.$broadcast('abrirLoad');
		},
		loadingClose:function(){
			$rootScope.$broadcast('fecharLoad');
		},
	}
}]);

api.directive("uiModal", [ '$modal', function($modal) {

	return {
		restrict : 'A',
		templateUrl : '',
		link : function(scope, element, attr) {
			element.click(function() {
				
				var url = element.attr("rota");				
				var controller = element.attr("controller");
				var param = element.attr("param");
				var size = element.attr("size");
								
				$modal.open({
					templateUrl : url,
					controller : controller,
					size : size,
					keyboard : false,
					backdrop : 'static',
					resolve : {
						parametro : function() {
							return param;
						}
					}
				});
			});
		}
	}
} ]);

api.directive("loading", [ 'helper', function(helper) {
	return {
		replace : true,
		restrict : 'EA',
		scope : true,
		require : '?ngModel',
		templateUrl : helper.path() + "app-config/template/loading.html",
		link : function(scope, element, attr) {

			var imgLoad = element.find("img");
			imgLoad.attr("src", helper.path() + "img/loader.gif");

			scope.$on("abrirLoad", function(){							
				var modal =element.modal({
					backdrop : 'static'
				});
				var elementos = $(angular.element('.modal-backdrop'));	
				var $el = $(elementos.get(1));				
				$el.attr('id','loadin-modal-backdrop').css('z-index','9998');				
													
			});
			scope.$on("fecharLoad", function() {								
				var modal =element.modal('hide');
				var $el = $(angular.element('#loadin-modal-backdrop'));				
				if($el[0]!==undefined){
					$el.remove();		
				}				
			});
		}
	};
}]);

api.directive('select2', ['$timeout','helper', '$compile',function ($timeout,helper,$compile) {
	var typing = 0;
    var init = true;
    var lappedChange = false;
    return {
        restrict: 'A',
        scope:{
            ngModel: '=',
            ngInitValue: '=',
            ngAssociated: '&ngAssociated'
        },
        link: function (scope, element, attr, ngModel) {
            attr.placeholder = attr.placeholder ? attr.placeholder : 'Busque aqui';
            var fnChangeAssociated = function () {
                if (scope.ngAssociated && !lappedChange) {
                    scope.ngAssociated();
                    lappedChange = true;
                }
            };
            element.select2({            	
                placeholder: attr.placeholder,
                minimumInputLength: 3,
                allowClear: true,
                escapeMarkup: function (markup) {  
                   return markup;
                },               
                formatSelection: function (object, container) {                	
                    $timeout(function () {
                        scope.ngModel = object.id;
                        scope.ngInitValue = object.item;
                    }, 0);
                    return object.text;
                },
                initSelection: function (inputHidden, callback) {                
                    if (scope.ngInitValue) {
                        $timeout(function () {
                            callback({ id: scope.ngInitValue.id, text: scope.ngInitValue.texto ? scope.ngInitValue.texto : attr.placeholder, item: angular.copy(scope.ngInitValue) });
                            fnChangeAssociated();
                        }, 0);
                    }
                },
                query: function (options){    
                    typing++;
                    var wait = parseInt(attr.wait);
                    wait = isNaN(wait) ? 500 : wait;
                    if (attr.url) {
                        var fnGo = function () {
                            typing--;
                            if (typing > 0) return;
                            var data = { results: [] };                            
                            helper.get(attr.url,options.term, function (dados) {
                            	var lov = helper.toList(dados.Lov);                       	
	                            if(lov!==null){
	                                for (var i = 0; i < lov.length; i++) {
	                                    data.results.push({ id: lov[i].id, text: lov[i].texto, item: lov[i] });
	                                }                                
	                                options.callback(data);
	                            }                                
                            });
                        };                        
                        $timeout(fnGo, wait);
                    }
                }
            }).on("change", function (e) {            
                $timeout(function () {
                    fnChangeAssociated();
                }, 0);
            });
            
          
            attr.$observe('disabled', function (valor) { 
               $timeout(function(){
            	   var $el = angular.element("#s2id_"+attr.id);            	
               	   $el.trigger('select2:sincronizar');
               });            	         	
            });            
                     
            if (scope.ngInitValue) {
                element.select2('val', scope.ngInitValue.ID);               
            }

            scope.$watch('ngModel', function (newValue, oldValue) {
                lappedChange = false;
                element.select2('val', newValue);
            });

            $timeout(function () { init = false; }, 0);
        }
    };
}]);

api.directive('datatable', ['$timeout', function ($timeout) {

    return {
        restrict: 'A',
        scope: {
            ngSource: '='
        },
        link: function(scope, element, attr, ngModel) {

            element.fadeOut(0);

            var columnsNotOrder, lengthMenu, pageLength;
            columnsNotOrder = attr.columnsnotorder;
            lengthMenu = attr.lengthmenu || "10, 25, 50, 'Todos'";
            pageLength = attr.pagelength || 10;

            scope.fnGetColumnsNotOrder = function() {
                var alItensNotOrder = [];
                var columnsHead = element.find('thead tr th');

                if (columnsNotOrder) {

                    for (var i = 0; i < columnsHead.length; i++) {
                        if (columnsNotOrder.indexOf(i) > -1) {
                            alItensNotOrder.push({ "orderable": false });
                        } else {
                            alItensNotOrder.push(null);
                        }
                    }
                    return alItensNotOrder;
                }

                for (var i = 0; i < columnsHead.length - 1; i++) {
                    alItensNotOrder.push(null);
                }
                alItensNotOrder.push({ "orderable": false });

                return alItensNotOrder;
            };

            scope.fnGetLengthMenu = function() {
                var alLengthMenu = [];

                if (lengthMenu) {
                    var attrLengthMenu = eval("[" + lengthMenu + "]");
                    if (typeof(attrLengthMenu[attrLengthMenu.length - 1]) === 'string') {
                        for (var i = 0; i < attrLengthMenu.length - 1; i++) {
                            alLengthMenu.push(attrLengthMenu[i]);
                        }

                        alLengthMenu.push(-1);

                        return [alLengthMenu, attrLengthMenu];
                    }

                    for (var i = 0; i < attrLengthMenu.length; i++) {
                        alLengthMenu.push(attrLengthMenu[i]);
                    }

                    return [alLengthMenu, alLengthMenu];
                }
            };

            scope.fnGetPageLength = function() {
                if (pageLength) {
                    pageLength = parseInt(pageLength);
                    return isNaN(pageLength) ? scope.fnGetLengthMenu()[0][0] : pageLength;
                }

                return scope.fnGetLengthMenu()[0][0];
            };

            scope.fnCreateGrid = function() {
                $timeout(function() {
                    element.dataTable({
                        "filter": true,
                        "destroy": true,
                        'language': {
                            'lengthMenu': 'Exibir _MENU_ itens por página.',
                            'zeroRecords': 'Nenhum registro encontrado.',
                            'info': 'Item _START_ até _END_ de _TOTAL_ Itens',
                            'infoEmpty': '',//Sem resultados
                            'infoFiltered': '(filtrados de _MAX_ itens no total)',
                            'search': '<span title="Busca apenas dados já impresso nesta lista">Busca Rápida:</span>',
                            'searchTitle': 'Busca apenas dados já impresso nesta lista',
                            'paginate': {
                                'first': 'Primeira',
                                'previous': 'Anterior',
                                'next': 'Próxina',
                                'last': 'Última'
                            }
                        },
                        'columns': scope.fnGetColumnsNotOrder(),
                        'lengthMenu': scope.fnGetLengthMenu(),
                        'pageLength': scope.fnGetPageLength()
                    });
                    //$('select', element.closest('.dataTables_wrapper')).width(50).chosen();
                    element.removeAttr('style').fadeIn(0);
                }, 0);
            };

            scope.fnClearGrid = function() {
                element.fadeOut(0);
                element.fnDestroy();
            };

            if (scope.ngSource) {
                scope.ngSource.clearGrid = scope.fnClearGrid;
            }
            //$watchCollection
            scope.$watch('ngSource', function(newValue, oldValue) {
                if (newValue) {
                    $timeout(function() {
                        newValue.clearGrid = scope.fnClearGrid;
                        scope.fnCreateGrid();
                    }, 0);
                }
            });

            scope.fnCreateGrid();
        }
    };
}]);

api.directive('chosen', function () {

    var __indexOf = [].indexOf || function (item) { for (var i = 0, l = this.length; i < l; i++) { if (i in this && this[i] === item) return i; } return -1; };

    var CHOSEN_OPTION_WHITELIST, NG_OPTIONS_REGEXP, isEmpty, snakeCase;

    NG_OPTIONS_REGEXP = /^\s*(.*?)(?:\s+as\s+(.*?))?(?:\s+group\s+by\s+(.*))?\s+for\s+(?:([\$\w][\$\w]*)|(?:\(\s*([\$\w][\$\w]*)\s*,\s*([\$\w][\$\w]*)\s*\)))\s+in\s+(.*?)(?:\s+track\s+by\s+(.*?))?$/;
    CHOSEN_OPTION_WHITELIST = ['noResultsText', 'allowSingleDeselect', 'disableSearchThreshold', 'disableSearch', 'enableSplitWordSearch', 'inheritSelectClasses', 'maxSelectedOptions', 'placeholderTextMultiple', 'placeholderTextSingle', 'searchContains', 'singleBackstrokeDelete', 'displayDisabledOptions', 'displaySelectedOptions', 'width'];
    snakeCase = function (input) {
        return input.replace(/[A-Z]/g, function ($1) {
            return "_" + ($1.toLowerCase());
        });
    };
    isEmpty = function (value) {
        var key;

        if (angular.isArray(value)) {
            return value.length === 0;
        } else if (angular.isObject(value)) {
            for (key in value) {
                if (value.hasOwnProperty(key)) {
                    return false;
                }
            }
        }
        return true;
    };
    return {
        restrict: 'A',
        require: '?ngModel',
        terminal: true,
        link: function (scope, element, attr, ngModel) {
            var chosen, defaultText, disableWithMessage, empty, initOrUpdate, match, options, origRender, removeEmptyMessage, startLoading, stopLoading, valuesExpr, viewWatch;

            element.addClass('localytics-chosen');
            options = scope.$eval(attr.chosen) || {};
            angular.forEach(attr, function (value, key) {
                if (__indexOf.call(CHOSEN_OPTION_WHITELIST, key) >= 0) {
                    return options[snakeCase(key)] = scope.$eval(value);
                }
            });
            startLoading = function () {
                //return element.addClass('loading').attr('disabled', true).trigger('chosen:updated');
                return element.addClass('loading').trigger('chosen:updated');
            };
            stopLoading = function () {
                //return element.removeClass('loading').attr('disabled', false).trigger('chosen:updated');
                return element.removeClass('loading').trigger('chosen:updated');
            };
            chosen = null;
            defaultText = null;
            empty = false;
            initOrUpdate = function () {
                if (chosen) {
                    return element.trigger('chosen:updated');
                } else {
                    if (!options.no_results_text) {
                        options.no_results_text = 'Sem resultados para:';
                    }
                    chosen = element.chosen(options).data('chosen');
                    return defaultText = chosen.default_text;
                }
            };
            removeEmptyMessage = function () {
                empty = false;
                return element.attr('data-placeholder', defaultText);
            };
            disableWithMessage = function () {
                empty = true;
                //return element.attr('data-placeholder', chosen.results_none_found).attr('disabled', true).trigger('chosen:updated');
                return element.attr('data-placeholder', chosen.results_none_found).trigger('chosen:updated');
            };
            if (ngModel) {
                origRender = ngModel.$render;
                ngModel.$render = function () {
                    origRender();
                    return initOrUpdate();
                };
                if (attr.multiple) {
                    viewWatch = function () {
                        return ngModel.$viewValue;
                    };
                    scope.$watch(viewWatch, ngModel.$render, true);
                }
            } else {
                initOrUpdate();
            }
            attr.$observe('disabled', function () {
                return element.trigger('chosen:updated');
            });

            if (attr.ngOptions && ngModel) {
                match = attr.ngOptions.match(NG_OPTIONS_REGEXP);
                valuesExpr = match[7];
                return scope.$watchCollection(valuesExpr, function (newVal, oldVal) {
                    if (angular.isUndefined(newVal)) {
                        return startLoading();
                    } else {
                        if (empty) {
                            removeEmptyMessage();
                        }
                        stopLoading();
                        if (isEmpty(newVal)) {
                            return disableWithMessage();
                        }
                    }
                });
            }           
        }
    };
});