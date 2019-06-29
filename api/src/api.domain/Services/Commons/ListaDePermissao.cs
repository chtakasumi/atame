using api.domain.Entity;
using System;
using System.Collections.Generic;

namespace api.domain.Services.Commons
{
    public class ListaDePermissao
    {
        public static ICollection<PerfilGrupo> Listar
        {
            get
            {
                return new PerfilGrupo[]
                {
                    #region Configuracao
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao),
                            Funcionalidade ="usuario",
                            Descricao ="Tela de Usuário",
                            Order =1
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao),
                            Funcionalidade ="grupo",
                            Descricao ="Tela de Grupo",
                            Order =2
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao),
                            Funcionalidade ="uf",
                            Descricao ="Tela de UFs",
                            Order =3
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao),
                            Funcionalidade ="municipio",
                            Descricao ="Tela de Municipio",
                            Order =4
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao),
                            Funcionalidade ="banco",
                            Descricao ="Tela de Banco",
                            Order =5
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas) ,
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao),
                            Funcionalidade ="empresa",
                            Descricao ="Tela de Empresa",
                            Order =6
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao),
                            Funcionalidade ="desconto",
                            Descricao ="Tela de Desconto",
                            Order =7
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao),
                            Funcionalidade ="parametro",
                            Descricao ="Tela de Parâmentro",
                            Order =8
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao),
                            Funcionalidade ="geradorRelatorio",
                            Descricao ="Tela de geração de relatório",
                            Order =9
                        }
                    },
                    
                    #endregion

                    #region Cadastro
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu),EnumMenu.Cadastro),
                            Funcionalidade ="tipoCurso",
                            Descricao ="Tela de Tipo de Curso",
                            Order =10
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro),
                            Funcionalidade ="docente",
                            Descricao ="Tela de Docente",
                            Order =11
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro),
                            Funcionalidade ="conteudoProgramatico",
                            Descricao ="Tela de Conteúdo Programático",
                            Order =12
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro),
                            Funcionalidade ="curso",
                            Descricao ="Tela de Curso",
                            Order =13
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro),
                            Funcionalidade ="turma",
                            Descricao ="Tela de Turma",
                            Order =14
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro),
                            Funcionalidade ="vendedor",
                            Descricao ="Tela de vendedor",
                            Order =15
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro),
                            Funcionalidade ="cliente",
                            Descricao ="Tela de Cliente",
                            Order =16
                        }
                    },
                    #endregion

                    #region Operacao
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Operacional),
                            Funcionalidade ="orcamento",
                            Descricao ="Tela de Orçamento",
                            Order =17
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Operacional),
                            Funcionalidade ="venda",
                            Descricao ="Tela de Venda",
                            Order =18
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Operacional),
                            Funcionalidade ="faturamento",
                            Descricao ="Tela de Faturamento",
                            Order =19
                        }
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil
                        {
                            Modulo =Enum.GetName(typeof(EnumModulo), EnumModulo.Vendas),
                            Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Operacional),
                            Funcionalidade ="comissao",
                            Descricao ="Tela de Comissão",
                            Order =20
                        }
                    }
                    #endregion
                };
            }
        }
    }
}
