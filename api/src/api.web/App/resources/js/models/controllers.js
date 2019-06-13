var app = angular.module('main');

//comportamento da pagina login
app.controller('loginCtrl', ['$scope', 'autenticacaoService', '$location', '$rootScope', function ($scope, autenticacaoService, $location, $rootScope) {
    $scope.autenticar = function () {
        autenticacaoService.autenticar($scope.login, $scope.senha, function (chave) {
            if (chave) {               
                $location.path("/home");
            }
        });
    }
}]);

app.controller('cursoCtrl', ['$scope', 'alertService', 'parm', 'modelService', 'modelService', function ($scope, alertService, parm, modelService) {

    modelService.extendsAbstractController($scope, alertService, parm, function () {

        //CarregarLovPesquisa
        modelService.carregarLov({
            scope: $scope.lovPesquisa = {},
            servico: parm.tipoCursoService(),
            aoSelecionar: function (item) {
                $scope.filtros.tipoCursoId = null;
                if (!item) return;
                $scope.filtros.tipoCursoId = item.id;
            }
        });

        //CarregarLovCadastro     
        modelService.carregarLov({
            scope: $scope.lovCadastro = {},
            servico: parm.tipoCursoService(),
            aoSelecionar: function (item) {
                $scope.model.tipoCursoId = null;
                if (!item) return;
                $scope.model.tipoCursoId = item.id;                
            }
        });

        //***VINCULAR CONTEUDO PROGRAMATICO***//       
        var param1 = {
            scopePartial: $scope.cursoConteudoProgramatico = {},
            servicoVinculo: parm.cursoConteudoProgramaticoService(),
            servicoLov: parm.conteudoProgramaticoService(),
            nomeAtributoFK01: "cursoId",
            nomeAtributoFK02: "conteudoProgramaticoId",
            nomeTabelaLov: 'conteudoProgramatico',
            model: $scope.model,
            alertService: alertService,
            funcGrid: function () {
                return $scope.model.conteudosProgramaticos;
            }
        };
        modelService.vincular(param1);

        //***VINCULAR DOCENTE***//        
        var param2 = {
            scopePartial: $scope.cursoDocente = {},
            servicoVinculo: parm.cursoDocenteService(),
            servicoLov: parm.docenteService(),
            nomeAtributoFK01: "cursoId",
            nomeAtributoFK02: "docenteId",
            nomeTabelaLov: 'docente',
            model: $scope.model,
            alertService: alertService,
            funcGrid: function () {
                return $scope.model.docentes;
            }
        };
        modelService.vincular(param2);
        
        $scope.calculaValorParcela = function () {
            $scope.model.valorParcela = ($scope.model.preco / $scope.model.parcela);
        }

        //***ANTES DE VOLTAR VALIDA REGISTROS NÃO SALVOS***//
        $scope.antesDeVoltar = function (form) {
            modelService.existeDadosNaoGravados($scope, $scope.model.docentes, 'DOCENTE', function () {
                modelService.existeDadosNaoGravados($scope, $scope.model.conteudosProgramaticos, 'CONTEUDOS PROGRAMATICOS', function () {
                    $scope.voltar(form);
                });
            });
        }

    });

}]);

app.controller('turmaCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {

    modelService.extendsAbstractController($scope, alertService, parm, function () {

        //CarregarLovPesquisa
        modelService.carregarLov({
            scope: $scope.lovPesquisa = {},
            servico: parm.cursoService(),
            aoSelecionar: function (item) {
                $scope.filtros.cursoId = null;
                if (!item) return;
                 $scope.filtros.cursoId = item.id;
            }
        });

        //CarregarLovCadastro     
        modelService.carregarLov({
            scope: $scope.lovCadastro = {},
            servico: parm.cursoService(),
            aoSelecionar: function (curso) {
                $scope.model.cursoId = null;
                if (!item) return;
                $scope.model.cursoId = curso.id;
                //traz a grid preenchidas
                if ($scope.titulo.indexOf("Cadastrar") > -1) {
                    if ($scope.model.conteudosProgramaticos.length == 0) {
                        $scope.model.conteudosProgramaticos = curso.conteudosProgramaticos;
                    }
                    if ($scope.model.docentes.length == 0) {
                        $scope.model.docentes = curso.docentes;
                    }
                }

                $scope.model.parcela = curso.parcela;
                $scope.model.preco = curso.preco;
                $scope.model.valorParcela = curso.valorParcela;
            }
        });


        ////***VINCULAR CONTEUDO PROGRAMATICO***//       
        var param1 = {
            scopePartial: $scope.turmaConteudoProgramatico = {},
            servicoVinculo: parm.turmaConteudoProgramaticoService(),
            servicoLov: parm.conteudoProgramaticoService(),
            nomeAtributoFK01: "turmaId",
            nomeAtributoFK02: "conteudoProgramaticoId",
            nomeTabelaLov: 'conteudoProgramatico',
            model: $scope.model,
            alertService: alertService,
            funcGrid: function () {
                return $scope.model.conteudosProgramaticos;
            }
        };
        modelService.vincular(param1);


        ////***VINCULAR DOCENTE***//    
        var param2 = {
            scopePartial: $scope.turmaDocente = {},
            servicoVinculo: parm.turmaDocenteService(),
            servicoLov: parm.docenteService(),
            nomeAtributoFK01: "turmaId",
            nomeAtributoFK02: "docenteId",
            nomeTabelaLov: 'docente',
            model: $scope.model,
            alertService: alertService,
            funcGrid: function () {
                return $scope.model.docentes;
            }
        };
        modelService.vincular(param2);


        $scope.calculaValorParcela = function () {
            $scope.model.valorParcela = ($scope.model.preco / $scope.model.parcela);
        }


        //***ANTES DE VOLTAR VALIDA REGISTROS NÃO SALVOS***//
        $scope.antesDeVoltar = function (form) {
            modelService.existeDadosNaoGravados($scope, $scope.model.docentes, 'DOCENTE', function () {
                modelService.existeDadosNaoGravados($scope, $scope.model.conteudosProgramaticos, 'CONTEUDOS PROGRAMATICOS', function () {
                    $scope.voltar(form);
                });
            });
        }

    });

}]);

app.controller('tipoCursoCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm);
}]);

//comportamento da pagina docente
app.controller('docenteCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
        modelService.extendsAbstractController($scope, alertService, parm, function () {  
            
            //Carregar UF lovUfExpeditor.dados
            modelService.carregarLov({
                scope: $scope.lovUfExpeditor = {},
                servico: parm.ufService(),
                aoSelecionar: function (item) {
                    $scope.model.ufExpedicaoId = null;
                    if (!item) return;
                    $scope.model.ufExpedicaoId = item.id;
                }
            });

            $scope.lovOrgaoExpedicao = {};
            parm.utilsService().orgaoExpeditor(function (data) {
                //  debugger;
                $scope.lovOrgaoExpedicao.dados = data;
                $scope.lovOrgaoExpedicao.selecionar = function (item) {
                    $scope.model.orgaoExpedicaoSiglaDescricao = null;
                    if (!item) return;
                    $scope.model.orgaoExpedicaoSiglaDescricao = item.siglaDescricao;
                }
            });

            $scope.lovTipoConta = {};
            parm.utilsService().tipoConta(function (data) {              
                $scope.lovTipoConta.dados = data;
                $scope.lovTipoConta.selecionar = function (item) {
                    $scope.model.tipoContaDescricao = null;
                    if (!item) return;
                    $scope.model.tipoContaDescricao = item.descricao;
                }
            });

            //Carregar lovBanco
            modelService.carregarLov({
                scope: $scope.lovBanco = {},
                servico: parm.bancoService(),
                aoSelecionar: function (item) {
                    $scope.model.bancoId = null;
                    if (!item) return;
                    $scope.model.bancoId = item.id;
                }
            });
            
    });
}]);

app.controller('conteudoProgramaticoCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm);
}]);

//comportamento da pagina vendedor
app.controller('vendedorCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm);
}]);

//comportamento da pagina Uf
app.controller('ufCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm);
}]);

//comportamento da pagina Uf
app.controller('municipioCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm, function () {


        //CarregarLovPesquisa
        modelService.carregarLov({
            scope: $scope.lovPesquisa = {},
            servico: parm.ufService(),
            aoSelecionar: function (item) {  
                $scope.filtros.ufId = null;
                if (!item) return;
                $scope.filtros.ufId = item.id;
            }
        });

        //CarregarLovCadastro     
        modelService.carregarLov({
            scope: $scope.lovCadastro = {},
            servico: parm.ufService(),
            aoSelecionar: function (item) {
                $scope.model.ufId = null;
                if (!item) return;
                $scope.model.ufId = item.id;
            }
        });

    });
}]);

//comportamento da pagina cliente
app.controller('clienteCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm, function () {
        //CarregarLovUF
        modelService.carregarLov({
            scope: $scope.lovCadastroUF = {},
            servico: parm.ufService(),
            aoSelecionar: function (item) {
                $scope.model.ufId = null;
                if (!item) return;
                $scope.model.ufId = item.id;
            }
        });
        
        //CarregarLovVendedor
        modelService.carregarLov({
            scope: $scope.lovPesquisaVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.filtros.vendedorId = null;
                if (!item) return;
                $scope.filtros.vendedorId = item.id;
            }
        });

        modelService.carregarLov({
            scope: $scope.lovCadastroVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.model.vendedorId = null;
                if (!item) return;
                $scope.model.vendedorId = item.id;
            }
        });

        $scope.lovCadastroMunicipio = {};
        $scope.lovCadastroMunicipio.pesquisar = function (fitro) {
            parm.municipioService().listar({ ufId: $scope.model.ufId, nome: fitro }, function (data) {
                $scope.lovCadastroMunicipio.dados = data;
            });
        }

        $scope.lovCadastroMunicipio.selecionar = function (item) {
            $scope.model.municipioId = item.id
        }

    });

}]);

//comportamento da pagina venda
app.controller('vendaCtrl', ['$scope', 'alertService', 'parm', 'modelService', 'utils', 'globalService', function ($scope, alertService, parm, modelService, utils, globalService) {
    modelService.extendsAbstractController($scope, alertService, parm, function () {

       //Isso para sobreescerver a referencia do botão novo e executar no final
        var novo = angular.copy($scope.novo);
        $scope.novo = function (){
            utils.getData(function (data) {
                novo();
                $scope.model.data = data; 
                //$scope.model.vencimentoPrimeiraParcela = utils.incrementaDias(data, 30);
            });
        }
        //override
        var editar = angular.copy($scope.editar);
        $scope.editar = function (model){   
            model.descontoObject = { percentual: model.desconto };
            editar(model);
        }

        //CarregarLovTurma
        modelService.carregarLov({
            scope: $scope.lovPesquisaTurma = {},
            servico: parm.turmaService(),
            aoSelecionar: function (item) {
                $scope.filtros.turmaId = null;
                if (!item) return;
                $scope.filtros.turmaId = item.id;
            }
        });

        modelService.carregarLov({
            scope: $scope.lovCadastroTurma = {},
            servico: parm.turmaService(),
            aoSelecionar: function (item) {

                $scope.model.turmaId = null;
                $scope.model.curso = null;
                $scope.model.valorCurso = null;
                if (!item) return;
                $scope.model.turmaId = item.id;
                $scope.model.curso = item.curso;
                if ($scope.titulo.indexOf("Cadastrar") > -1) {
                    $scope.model.valorCurso = item.preco; //estou pegando um valor do curso e não da turma tirar duvidas com o clovis...
                }
                $scope.calculaValorVendaEhParcela();
            }
        });

        //CarregarLovVendedor
        modelService.carregarLov({
            scope: $scope.lovPesquisaVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.filtros.vendedorId = null;
                if (!item) return;
                $scope.filtros.vendedorId = item.id;
            }
        });

        modelService.carregarLov({
            scope: $scope.lovCadastroVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.model.vendedorId = null;
                if (!item) return;
                $scope.model.vendedorId = item.id;
            }
        });

        //CarregarLovCliente
        modelService.carregarLov({
            scope: $scope.lovPesquisaCliente = {},
            servico: parm.clienteService(),
            aoSelecionar: function (item) {
                $scope.filtros.clienteFinanceiroId = null;
                if (!item) return;
                $scope.filtros.clienteFinanceiroId = item.id;
            }
        });

        modelService.carregarLov({
            scope: $scope.lovCadastroCliente = {},
            servico: parm.clienteService(),
            aoSelecionar: function (item) {
                $scope.model.clienteFinanceiroId = null;
                if (!item) return;
                $scope.model.clienteFinanceiroId = item.id;
            }
        });

        modelService.carregarLov({
            scope: $scope.lovCadastroDesconto = {},
            servico: parm.descontoService(),
            aoSelecionar: function (item) {              
                $scope.model.desconto = null;
                if (!item) return;
                $scope.model.descontoObject = item;
                $scope.model.desconto = item.percentual;                   
               
            }
        });

        //regras de calculo do desconto
        $scope.calculaValorVendaEhParcela = function () {
            var valorTotal = ($scope.model.quantidade > 0) ? $scope.model.valorCurso * $scope.model.quantidade : $scope.model.valorCurso;
            var desconto = (($scope.model.desconto * valorTotal) / 100);
            $scope.model.valorVenda = valorTotal - desconto;
            $scope.model.valorParcela = $scope.model.valorVenda / $scope.model.parcela;
        }

        //***VINCULAR CURSO AO DOCENTE***//        
        var vinculoAcademico = {
            scopePartial: $scope.clienteAcademico = {}, //ok
            servicoVinculo: parm.vendaClienteService(), //ok
            servicoLov: parm.clienteService(),
            nomeAtributoFK01: "vendaId",
            nomeAtributoFK02: "clienteAcademicoId",
            nomeTabelaLov: 'clienteAcademico',
            model: $scope.model,
            alertService: alertService,
            funcGrid: function () {
                return $scope.model.clientesAcademicos;
            }
        };
        modelService.vincular(vinculoAcademico);

        $scope.gerarParcelas = function () {

           $scope.model.parcelas = [];

           var qtdParcela =angular.copy($scope.model.parcela);

            var parcela = { id: 0, vendaId: 0, venda: null, previsaoPgto: null, numero: null, preco: $scope.model.valorParcela, status: 0};
            var vencimento;

            if (qtdParcela > 0) {
                for (var i = 1; i <= qtdParcela; i++) {   
                
                    parcela.numero = i;
                  
                    if (i === 1) {
                        vencimento = utils.incrementaDias($scope.model.vencimentoPrimeiraParcela, 0);
                        parcela.previsaoPgto = angular.copy(vencimento);
                    } else {
                        vencimento = utils.incrementaDias(vencimento, 30);                      
                        parcela.previsaoPgto = angular.copy(vencimento);
                    }                   
                    $scope.model.parcelas.push(angular.copy(parcela));
                }
            }         
        }

        //***ANTES DE VOLTAR VALIDA REGISTROS NÃO SALVOS***//
        $scope.antesDeVoltar = function (form) {       
            //modelService.existeDadosNaoGravados($scope, $scope.model.clientesAcademicos, 'ACADEMICO', function () {
                $scope.voltar(form);
           // });
        }

        $scope.getStatusDescription = function (status) {
            return globalService.getStatusDescription(status);
        }
    });
}]);

//orçamento
app.controller('prospeccaoCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm, function () {
        
        //CarregarLovVendedor
        modelService.carregarLov({
            scope: $scope.lovPesquisaVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.filtros.vendedorId = null;
                if (!item) return;
                $scope.filtros.vendedorId = item.id;
            }
        });

        modelService.carregarLov({
            scope: $scope.lovCadastroVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.model.vendedorId = null;
                if (!item) return;
                $scope.model.vendedorId = item.id;
            }
        });


        var metodonovo = $scope.novo;

        $scope.novo = function ()
        {
           
            metodonovo();
            $scope.model.status = "EmAnalise";
           
        }                 
               
        //***VINCULAR CURSO DE INTERESSE***//        
        var vinculoAcademico = {
            scopePartial: $scope.interesses = {}, //ok
            servicoVinculo: parm.prospeccaoInteresseService(), //ok
            servicoLov: parm.cursoService(),
            nomeAtributoFK01: "prospeccaoId",
            nomeAtributoFK02: "cursoId",
            nomeTabelaLov: 'curso',
            model: $scope.model,
            alertService: alertService,
            funcGrid: function () {
                return $scope.model.interesses;
            }
        };
        modelService.vincular(vinculoAcademico);

        //***ANTES DE VOLTAR VALIDA REGISTROS NÃO SALVOS***//
        $scope.antesDeVoltar = function (form) {    
            modelService.existeDadosNaoGravados($scope, $scope.model.interesses, 'INTERESSES', function () {
                $scope.voltar(form);
            });
        }
    });
}]);

//comportamento da pagina parametro
app.controller('parametroCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm, function () {      
        //CarregarLovCadastro     
        parm.service().listarChave(function (dados) {           
            $scope.listachaves =  dados
        })      
    });
}]);

app.controller('faturamentoCtrl', ['$scope', 'alertService', 'parm', 'modelService', 'globalService', 'utils', function ($scope, alertService, parm, modelService, globalService, utils) {

    modelService.extendsAbstractController($scope, alertService, parm, function () {

        $scope.baixar = function (model) {   

            utils.getData(function (data) {
                var vdata = new Date(data);
                model.faturamento.competenciaAno = vdata.getFullYear();
                model.faturamento.competenciaMes = vdata.getMonth() + 1;
                model.faturamento.vendaId = model.vendaId;
                model.faturamento.parcelaId = model.id;
                model.faturamento.valorPago = model.preco;
                model.faturamento.dataPgto = data;
                $scope.editar(model);
                $scope.titulo = "Baixar parcela";
            })
        }
               
        //CarregarLovVendedor
        modelService.carregarLov({
            scope: $scope.lovPesquisaVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.filtros.vendedorId = null;
                if (!item) return;
                $scope.filtros.vendedorId = item.id;                    
               
            }
        });

        modelService.carregarLov({
            scope: $scope.lovPesquisaCliente = {},
            servico: parm.clienteService(),
            aoSelecionar: function (item) {               
                $scope.filtros.clienteFinanceiroId = null;
                if (!item) return;
                $scope.filtros.clienteFinanceiroId = item.id;                
            }
        });
        
    });

    $scope.getStatusDescription = function (status) {
        return globalService.getStatusDescription(status);
    }
}]);

app.controller('usuarioCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    
    modelService.extendsAbstractController($scope, alertService, parm, function () {        
        //CarregarLovVendedor
        modelService.carregarLov({
            scope: $scope.lovPesquisaVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.filtros.vendedorId = null;
                if (!item) return;
                $scope.filtros.vendedorId = item.id;               
            }
        });

        modelService.carregarLov({
            scope: $scope.lovCadastroVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.model.vendedorId = null;
                if (!item) return;
                $scope.model.vendedorId = item.id;
            }
        });  

        $scope.atribuirGrupo = function (element) {
            $scope.model.gruposCedidas.push(element);
            $scope.model.gruposNaoCedidas.splice($scope.model.gruposNaoCedidas.indexOf(element), 1);
        }

        $scope.removerGrupo = function (element) {
            $scope.model.gruposCedidas.splice($scope.model.gruposCedidas.indexOf(element), 1);
            $scope.model.gruposNaoCedidas.push(element);
        }

    });

}]);

app.controller('comissaoCtrl', ['$scope', 'alertService', 'parm', 'modelService', 'utils', function ($scope, alertService, parm, modelService, utils) {
    modelService.extendsAbstractController($scope, alertService, parm, function () {
        //Carregar status   
        parm.service().listaStatus(function (dados) {
            $scope.listaStatusComissao = dados
        });        
        
        $scope.baixar = function (model) {
            utils.getData(function (data) {
                var vdata = new Date(data);
                model.dataPagamento = vdata;
                model.valorPago = model.valorApagar;
                $scope.editar(model);
                $scope.titulo = "Efeturar pagamento";
            });
        }
        
        $scope.color = function (status) {
            switch (status) {
                case 'ParcialmentePago':
                    return 'blue';

                case 'Pago':
                    return 'green';
                    break;

                case 'EmAberto':
                    return 'red';
                    break;

            };
        }
    });
}]);

app.controller('bancoCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm);
}]);

app.controller('descontoCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm);
}]);

app.controller('empresaCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm, function () {
        //CarregarLovUF
        modelService.carregarLov({
            scope: $scope.lovCadastroUF = {},
            servico: parm.ufService(),
            aoSelecionar: function (item) {
                $scope.model.ufId = null;
                if (!item) return;
                $scope.model.ufId = item.id;
            }
        });
       
        $scope.lovCadastroMunicipio = {};
        $scope.lovCadastroMunicipio.pesquisar = function (fitro) {
            parm.municipioService().listar({ ufId: $scope.model.ufId, nome: fitro }, function (data) {
                $scope.lovCadastroMunicipio.dados = data;
            });
        }

        $scope.lovCadastroMunicipio.selecionar = function (item) {
            $scope.model.municipioId = item.id
        }

    });

}]);

//comportamento da pagina grupo
app.controller('grupoCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm, function () {
       
        $scope.atribuirPermissao = function (element) {          
            $scope.model.permissoesCedidas.push(element);
            $scope.model.permissoesNaoCedidas.splice($scope.model.permissoesNaoCedidas.indexOf(element), 1);
        }
        
        $scope.removerPermissao = function (element) {
            $scope.model.permissoesCedidas.splice($scope.model.permissoesCedidas.indexOf(element), 1);
            $scope.model.permissoesNaoCedidas.push(element);
            
        }
    });
}]);