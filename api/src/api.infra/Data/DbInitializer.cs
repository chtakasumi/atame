using api.domain.Entity;
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
                        #region configuracao
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="configuracao_usuario",}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="configuracao_grupos"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="configuracao_uf"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="configuracao_municipio"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="configuracao_banco"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="configuracao_empresa"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="configuracao_desconto"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="configuracao_parametro"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="configuracao_parametro"}
                            },
                        #endregion

                        #region cadastro
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="cadastro_tipoCurso"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="cadastro_docente"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="cadastro_conteudoProgramatico"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="cadastro_curso"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="cadastro_turma"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="cadastro_vendedor"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="cadastro_cliente"}
                            },
                         #endregion

                        #region operacao
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="operacao_orcamento"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="operacao_venda"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="operacao_fatura"}
                            },
                            new PerfilGrupo
                            {
                                Perfil = new Perfil{Descricao="operacao_comissao"}
                            }
                            #endregion
                    };
        }
    }
}
