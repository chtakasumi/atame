var app = angular.module('main');

//comportamento da pagina login
app.controller('loginCtrl', ['$scope', 'autenticacaoService', '$location', function ($scope, autenticacaoService, $location) {
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
        modelService.modelService.carregarLov({
            scope: $scope.lovPesquisa = {},
            servico: parm.tipoCursoService(),
            aoSelecionar: function (item) {
                $scope.filtros.tipoCursoId = item.id;
            }
        });

        //CarregarLovCadastro     
        modelService.modelService.carregarLov({
            scope: $scope.lovCadastro = {},
            servico: parm.tipoCursoService(),
            aoSelecionar: function (item) {
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
                $scope.filtros.cursoId = item.id;
            }
        });

        //CarregarLovCadastro     
        modelService.carregarLov({
            scope: $scope.lovCadastro = {},
            servico: parm.cursoService(),
            aoSelecionar: function (item) {
                $scope.model.cursoId = item.id;
                //tra a grid preenchidas
                if ($scope.titulo.indexOf("Cadastrar") > -1) {
                    if ($scope.model.conteudosProgramaticos.length == 0) {
                        $scope.model.conteudosProgramaticos = item.conteudosProgramaticos;
                    }
                    if ($scope.model.docentes.length == 0) {
                        $scope.model.docentes = item.docentes;
                    }
                }
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
    modelService.extendsAbstractController($scope, alertService, parm);
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
                $scope.filtros.ufId = item.id;
            }
        });

        //CarregarLovCadastro     
        modelService.carregarLov({
            scope: $scope.lovCadastro = {},
            servico: parm.ufService(),
            aoSelecionar: function (item) {
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

//comportamento da pagina venda
app.controller('vendaCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm, function () {

        //CarregarLovTurma
        modelService.carregarLov({
            scope: $scope.lovPesquisaTurma = {},
            servico: parm.turmaService(),
            aoSelecionar: function (item) {
                $scope.filtros.turmaId = item.id;
            }
        });

        modelService.carregarLov({
            scope: $scope.lovCadastroTurma = {},
            servico: parm.turmaService(),
            aoSelecionar: function (turma) {
                $scope.model.turmaId = turma.id;
                $scope.model.curso = turma.curso;
                if ($scope.titulo.indexOf("Cadastrar") > -1) {
                    $scope.model.valorCurso = turma.preco; //estou pegando um valor do curso e não da turma tirar duvidas com o clovis...
                }
                $scope.calculaValorVenda();
            }
        });

        //CarregarLovVendedor
        modelService.carregarLov({
            scope: $scope.lovPesquisaVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.filtros.vendedorId = item.id;
            }
        });

        modelService.carregarLov({
            scope: $scope.lovCadastroVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.model.vendedorId = item.id;
            }
        });

        //CarregarLovCliente
        modelService.carregarLov({
            scope: $scope.lovPesquisaCliente = {},
            servico: parm.clienteService(),
            aoSelecionar: function (item) {
                $scope.filtros.clienteFinanceiroId = item.id;
            }
        });

        modelService.carregarLov({
            scope: $scope.lovCadastroCliente = {},
            servico: parm.clienteService(),
            aoSelecionar: function (item) {
                $scope.model.clienteFinanceiroId = item.id;
            }
        });

        //regras de calculo do desconto
        $scope.calculaValorVenda = function () {
            var valorTotal = ($scope.model.quantidade > 0) ? $scope.model.valorCurso * $scope.model.quantidade : $scope.model.valorCurso;
            var desconto = (($scope.model.desconto * valorTotal) / 100);
            $scope.model.valorVenda = valorTotal - desconto;
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

        //***ANTES DE VOLTAR VALIDA REGISTROS NÃO SALVOS***//
        $scope.antesDeVoltar = function (form) {       
            modelService.existeDadosNaoGravados($scope, $scope.model.clientesAcademicos, 'ACADEMICO', function () {
                $scope.voltar(form);
            });
        }
    });
}]);

app.controller('prospeccaoCtrl', ['$scope', 'alertService', 'parm', 'modelService', function ($scope, alertService, parm, modelService) {
    modelService.extendsAbstractController($scope, alertService, parm, function () {
        
        //CarregarLovVendedor
        modelService.carregarLov({
            scope: $scope.lovPesquisaVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.filtros.vendedorId = item.id;
            }
        });

        modelService.carregarLov({
            scope: $scope.lovCadastroVendedor = {},
            servico: parm.vendedorService(),
            aoSelecionar: function (item) {
                $scope.model.vendedorId = item.id;
            }
        });

        //CarregarLovCliente
        modelService.carregarLov({
            scope: $scope.lovPesquisaCliente = {},
            servico: parm.clienteService(),
            aoSelecionar: function (item) {
                $scope.filtros.clienteId = item.id;
            }
        });

        modelService.carregarLov({
            scope: $scope.lovCadastroCliente = {},
            servico: parm.clienteService(),
            aoSelecionar: function (item) {
                $scope.model.clienteId = item.id;
            }
        });
               
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

