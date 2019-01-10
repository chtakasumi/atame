using api.domain.Entity;

namespace api.domain.Interfaces
{
    public interface IGrupoRepository: IRepository<Grupo>
    {
        void Testagrupo();
    }
}
