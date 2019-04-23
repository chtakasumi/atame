using api.infra;
using api.infra.Data;
using api.web.Config;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Net;

namespace api.web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           // services.AddCors();
            
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "App";              
                
            });

            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(CustomExceptionFilter));
                config.Filters.Add(typeof(TransactionActionFilter));
            })

            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)

            //discução=> https://stackoverflow.com/questions/7397207/json-net-error-self-referencing-loop-detected-for-type
            .AddJsonOptions(                  
                    o =>
                    {
                        //o.SerializerSettings.ContractResolver = new DefaultContractResolver();
                        o.SerializerSettings.Converters.Add(new StringEnumConverter());
                        o.SerializerSettings.Formatting = Formatting.Indented;
                        o.SerializerSettings.NullValueHandling = NullValueHandling.Include;                      
                        o.SerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
                        o.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    }               
             );

            //Dizendo qual o contexto da aplicação e qual a string de conexoa para o sqlserver
            services.AddDbContext<DBContext>(options =>            
            options.UseSqlServer(Configuration.GetConnectionString("gestao_comercial")));

            //Aplicando as injeções de dependencia ao projeto
            new InjectCDI(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // app.UseCors(
            //    options => options.WithOrigins("http://localhost:52565").AllowAnyMethod()
            //);            


            InitializeMyDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();


            InicializarPaginasHtml(app, env);

        }
        
        /// <summary>
        /// Metodo adicionado para Gerar migrações dinamicas e ainda, gerar dados iniciais
        /// </summary>
        /// <param name="app">https://docs.microsoft.com/pt-br/ef/core/managing-schemas/migrations/</param>
        private void InitializeMyDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<DBContext>();

                //facilita mas nem tanto
                context.Database.Migrate();  //so cria o banco, leva em consideração a migração já feitas, como se esse comando fosse apenas o: "update-database"
                
                //modo primitivo**** localhost isso seria legal
                //context.Database.EnsureCreated(); //Cria o banco e gerar as migrações, porem não gera o historico de versão

                DbInitializer.Initialize(context); //Popula algumas tabelas
            }
        }

        private void InicializarPaginasHtml(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "App";                         
            });
            
        }
    }
}
