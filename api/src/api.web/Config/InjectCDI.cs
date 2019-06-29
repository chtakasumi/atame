using api.domain.Entity;
using api.domain.Interfaces;
using api.domain.Services;
using api.infra.Repository;
using DinkToPdf;
using DinkToPdf.Contracts;
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
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            

            services.AddScoped<UsuarioService>();
            services.AddScoped<CursoService>();
            services.AddScoped<TipoCursoService>();
            services.AddScoped<DocenteService>();
            services.AddScoped<ConteudoProgramaticoService>();
            services.AddScoped<VendedorService>();
            services.AddScoped<TurmaService>();
            services.AddScoped<MunicipioService>();
            services.AddScoped<UFService>();
            services.AddScoped<ClienteService>();
            services.AddScoped<VendaService>();
            services.AddScoped<ProspeccaoService>();
            services.AddScoped<ParametroService>();          
            services.AddScoped<FaturamentoParcelaService>();
            services.AddScoped<ComissaoService>();
            services.AddScoped<BancoService>();
            services.AddScoped<DescontoService>();
            services.AddScoped<EmpresaService>();
            services.AddScoped<GrupoService>();
            services.AddScoped<GeradorRelatorioService>();
            
            services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            services.AddTransient<ICursoRepository, CursoRepository>();
            services.AddTransient<ITipoCursoRepository, TipoCursoRepository>();
            services.AddTransient<IDocenteRepository, DocenteRepository>();
            services.AddTransient<IConteudoProgramaticoRepository, ConteudoProgramaticoRepository>();
            services.AddTransient<IVendedorRepository, VendedorRepository>();
            services.AddTransient<ITurmaRepository, TurmaRepository>();
            services.AddTransient<IMunicipioRepository, MunicipioRepository>();
            services.AddTransient<IUFRepository, UFRepository>();
            services.AddTransient<IClienteRepository, ClienteRepository>();
            services.AddTransient<IVendaRepository, VendaRepository>();
            services.AddTransient<IProspeccaoRepository, ProspeccaoRepository>();
            services.AddTransient<IParametroRepository, ParametroRepository>();
            services.AddTransient<IParcelaRepository, ParcelaRepository>();
            services.AddTransient<IFaturamentoRepository, FaturamentoRepository>();
            services.AddTransient<IComissaoRepository, ComissaoRepository>();
            services.AddTransient<IBancoRepository, BancoRepository>();
            services.AddTransient<IDescontoRepository, DescontoRepository>();
            services.AddTransient<IEmpresaRepository, EmpresaRepository>();
            services.AddTransient<IGrupoRepository, GrupoRepository>();
            services.AddTransient<IGeradorRelatorioRepository, GeradorRelatorioRepository>();
            services.AddTransient<IPerfilRepository, PerfilRepository>();

            
        }
    }
}
