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
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
        }
    }
}
