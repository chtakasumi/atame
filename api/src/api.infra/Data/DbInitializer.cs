using api.domain.Entity;
using api.domain.Services.Commons;
using api.libs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.infra.Data
{
    public class DbInitializer
    {
        public static void Initialize(DBContext context)
        {
            Grupo gruposMaster = context.Grupos.Include(x => x.PerfisGrupos).ThenInclude(x => x.Perfil).Where(x => x.Descricao == "Administrador Master").SingleOrDefault();

            if (gruposMaster != null)
            {
                if (gruposMaster.PerfisGrupos.Count > 0 && gruposMaster.PerfisGrupos.Count <= ListaPerfil().Count())
                {
                    //existe o Grupo master
                    Grupo grupo = ObterPerfilTelaNaoInseridoNoBanco(gruposMaster);
                    context.Grupos.UpdateRange(grupo);
                    context.SaveChanges();
                }
                else
                {
                    Console.WriteLine(@"********************************************************************************");
                    Console.WriteLine(@"**O Grupo 'Administrador Master' master deve estar associado a todos os perfis**");
                    Console.WriteLine(@"********************************************************************************");
                }
            }
            else
            {
                //se não existir o grupo master
                var grupo = new Grupo
                {
                    Descricao = "Administrador Master",
                    PerfisGrupos = ListaPerfil()
                };
                context.Add(grupo);
                context.SaveChanges();

                if (!context.Usuarios.Any())
                {
                    #region Usuario

                    var usuario = new Usuario
                    {
                        Login = "MASTER",
                        Senha = Seguranca.GerarHash("1"),
                        Ativo = "S",
                        GruposUsuarios = new GrupoUsuario[]
                        {
                            new GrupoUsuario
                            {
                                Grupo= grupo
                            }
                        }
                    };

                    context.Add(usuario);

                    context.SaveChanges();

                    #endregion
                }
            }
        }

        private static Grupo ObterPerfilTelaNaoInseridoNoBanco(Grupo gruposMaster)
        {
            List<Perfil> ListaPerfilQueNaoExisteNoBanco = new List<Perfil>();
            var listaPerfilTela = ListaPerfil();

            //se o perfil do banco existe na tela
            foreach (var perfilTela in listaPerfilTela)
            {
                if (!ExisteNoBanco(perfilTela, gruposMaster))
                {
                    gruposMaster.PerfisGrupos.Add(perfilTela);
                }
            }

            return gruposMaster;

        }

        private static bool ExisteNoBanco(PerfilGrupo perfilTela, Grupo gruposMaster)
        {
            foreach (PerfilGrupo perfilDoBanco in gruposMaster.PerfisGrupos)
            {
                if (perfilTela.Perfil.Descricao == perfilDoBanco.Perfil.Descricao)
                {
                    return true;
                }
            }
            return false;
        }

        private static ICollection<PerfilGrupo> ListaPerfil()
        {
            return new PerfilGrupo[]
            {
                #region Configuracao
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao), Funcionalidade="usuario", Descricao="Tela de Usuário", Order=1}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao), Funcionalidade="grupo",  Descricao="Tela de Grupo", Order=2}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao), Funcionalidade="uf", Descricao="Tela de UFs", Order=3}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao), Funcionalidade="municipio", Descricao="Tela de Municipio", Order=4}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao), Funcionalidade="banco", Descricao="Tela de Banco", Order=5}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao), Funcionalidade="empresa", Descricao="Tela de Empresa", Order=6}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao), Funcionalidade="desconto", Descricao="Tela de Desconto", Order=7}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Configuracao), Funcionalidade="parametro", Descricao="Tela de Parâmentro", Order=8}
                    },                    
                #endregion

                #region Cadastro
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro), Funcionalidade="tipoCurso", Descricao="Tela de Tipo de Curso", Order=9}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro), Funcionalidade="docente", Descricao="Tela de Docente", Order=10}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro), Funcionalidade="conteudoProgramatico", Descricao="Tela de Conteúdo Programático", Order=11}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro), Funcionalidade="curso", Descricao="Tela de Curso", Order=12}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro), Funcionalidade="turma", Descricao="Tela de Turma", Order=13}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro), Funcionalidade="vendedor", Descricao="Tela de vendedor", Order=14}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Cadastro), Funcionalidade="cliente",Descricao="Tela de Cliente", Order=15}
                    },
                #endregion

                #region Operacao
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Operacional), Funcionalidade="orcamento",Descricao="Tela de Orçamento", Order=16}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Operacional), Funcionalidade="venda", Descricao="Tela de Venda", Order=17}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Operacional), Funcionalidade="faturamento",Descricao="Tela de Faturamento", Order=18}
                    },
                    new PerfilGrupo
                    {
                        Perfil = new Perfil{Modulo=Enum.GetName(typeof(EnumModulo), EnumModulo.Financeiro) , Menu =Enum.GetName(typeof(EnumMenu), EnumMenu.Operacional), Funcionalidade="comissao", Descricao="Tela de Comissão", Order=19}
                    }
                    #endregion
            };
        }
    }
}
