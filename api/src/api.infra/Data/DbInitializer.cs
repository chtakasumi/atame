using api.domain.Entity;
using System.Linq;

namespace api.infra.Data
{
    public class DbInitializer
    {
        public static void Initialize(APIContext context)
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
                            Perfil = new Perfil{Descricao="tela_cliente_cadastro"}
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
                    Login = "Master",
                    Senha = "1",//Todo: Falta criptografar
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

            }
        }
    }
}
