using api.infra;
using api.infra.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace api.web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<APIContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("gestao_comercial")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            InitializeMyDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }    

            app.UseMvc();
        }

        /// <summary>
        /// Metodo adicionado para Gerar migrações dinamicas e ainda, gerar dados iniciais
        /// </summary>
        /// <param name="app"></param>
        private void InitializeMyDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<APIContext>();

                //facilita mas nem tanto
                //context.Database.Migrate();  //so cria o banco, leva em consideração a migração já feitas, como se esse comando fosse apenas o: "update-database"
                
                //modo primitivo**** localhost isso seria legal
                context.Database.EnsureCreated(); //Cria o banco e gerar as migrações, porem não gera o historico de versão

                DbInitializer.Initialize(context); //Popula algumas tabelas
            }
        }
    }
}
