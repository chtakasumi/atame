﻿using api.domain.Entity;
using api.libs;
using System.Linq;

namespace api.infra.Data
{
    public class DbInitializer
    {
        public static void Initialize(DBContext context)
        {
            if (!context.Perfis.Any())
            {               
                #region Grupo

                var grupo = new Grupo
                {
                    Descricao = "Administrador Master",                   
                    PerfisGrupos = new PerfilGrupo[]
                    {
                        new PerfilGrupo
                        {
                            Perfil = new Perfil{Descricao="menu_cliente"}                          
                        },
                        new PerfilGrupo
                        {
                            Perfil = new Perfil{Descricao="tela_cliente_cadastrar"}
                        },
                        new PerfilGrupo
                        {
                            Perfil = new Perfil{Descricao="tela_cliente_editar"}
                        },
                        new PerfilGrupo
                        {
                            Perfil = new Perfil{Descricao="tela_cliente_atualizar"}
                        },
                        new PerfilGrupo
                        {
                            Perfil = new Perfil{Descricao="tela_cliente_excluir"}
                        }
                    }                   
                };

                context.Add(grupo);

                context.SaveChanges();

                #endregion

                #region Usuario

                var usuario = new Usuario
                {
                    Login = "MASTER",
                    Senha = Seguranca.GerarHash("1"),
                    Ativo = "S",
                    GruposUsuarios = new GrupoUsuario[]
                    {
                        new GrupoUsuario {
                            Grupo= grupo
                        }
                    }
                };

                context.Add(usuario);

                context.SaveChanges();

                #endregion

                #region Curso

                var curso1 = new Curso
                {
                    Nome = "Informatica",

                };

                var curso2 = new Curso
                {
                    Nome = "Culinaria",

                };

                var curso3 = new Curso
                {
                    Nome = "Medicina",
                };

                context.Add(curso1);
                context.Add(curso2);
                context.Add(curso3);
                context.SaveChanges();
                #endregion

            }



        }
    }
}
