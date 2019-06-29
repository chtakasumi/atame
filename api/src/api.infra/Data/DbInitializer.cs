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
                //tenho que checar 1 a 1 se o perfil esta cadastrado no banco...
                //se não tiver cadastra
                if (gruposMaster.PerfisGrupos.Count > 0)
                {                 
                    //existe o Grupo master
                    Grupo grupo = ObterPerfilTelaNaoInseridoNoBanco(gruposMaster);

                    context.Grupos.UpdateRange(grupo);
                    context.SaveChanges();

                    Console.WriteLine(@"********************************************************************************");
                    Console.WriteLine(@"***************************Perfil Atualizado************************************");
                    Console.WriteLine(@"********************************************************************************");

                }
                else
                {
                    Console.WriteLine(@"********************************************************************************");
                    Console.WriteLine(@"********************************Cria perfil e Atualiza Grupo*********************");
                    Console.WriteLine(@"********************************************************************************");

                    CriaPerfilEhAtualuzaGrupo(context,  gruposMaster);                  
                }

            }
            else
            {
                CriarGrupoPerfilUsuario(context);
            }
        }

        private static void CriaPerfilEhAtualuzaGrupo(DBContext context, Grupo grupo)
        {
            grupo.PerfisGrupos= ListaDePermissao.Listar;
            context.Update(grupo);
            context.SaveChanges();
        }

        private static void CriarGrupoPerfilUsuario(DBContext context)
        {
            //se não existir o grupo master
            var grupo = new Grupo
            {
                Descricao = "Administrador Master",
                PerfisGrupos = ListaDePermissao.Listar
            };
            context.Add(grupo);
            context.SaveChanges();

            Console.WriteLine(@"********************************************************************************");
            Console.WriteLine(@"**************************Grupo e Perfil criados********************************");
            Console.WriteLine(@"********************************************************************************");


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

                Console.WriteLine(@"********************************************************************************");
                Console.WriteLine(@"************************************Usuário Criado******************************");
                Console.WriteLine(@"********************************************************************************");
            }
        }

        private static Grupo ObterPerfilTelaNaoInseridoNoBanco(Grupo gruposMaster)
        {
            List<Perfil> ListaPerfilQueNaoExisteNoBanco = new List<Perfil>();
            var listaPerfilTela = ListaDePermissao.Listar;

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
                if (perfilTela.Perfil.Funcionalidade == perfilDoBanco.Perfil.Funcionalidade)
                {
                    return true;
                }
            }
            return false;
        }

      
    }
}
