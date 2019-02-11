using api.domain.Interfaces;
using api.domain.Services;
using api.infra.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace api.web
{
    public class InjectCDI
    {
        /// <summary>
        /// - Transient : Criado a cada vez que são solicitados.
        /// - Scoped: Criado uma vez por solicitação.
        /// - Singleton: Criado na primeira vez que são solicitados.Cada solicitação subseqüente usa a instância que foi criada na primeira vez.
        /// </summary>
        /// <param name="services"></param>
        public InjectCDI(IServiceCollection services)
        {
            services.AddScoped<UsuarioService>();
            services.AddScoped<CursoService>();
            services.AddScoped<TipoCursoService>();
            services.AddScoped<DocenteService>();
            services.AddScoped<ConteudoProgramaticoService>();
            services.AddScoped<VendedorService>();

            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<ICursoRepository, CursoRepository>();
            services.AddTransient<ITipoCursoRepository, TipoCursoRepository>();
            services.AddTransient<IDocenteRepository, DocenteRepository>();
            services.AddTransient<IConteudoProgramaticoRepository, ConteudoProgramaticoRepository>();
            services.AddTransient<IVendedorRepository, VendedorRepository>();

        }
    }
}
