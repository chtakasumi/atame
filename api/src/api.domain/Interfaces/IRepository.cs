using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace api.domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //metodos de busca       
        IEnumerable<TEntity> Pesquisar(Expression<Func<TEntity, bool>> expressao);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expressaoWhere, Expression<Func<TEntity, int?>> key);
        TEntity PesquisarPorId(int id);
        IEnumerable<TEntity> BuscarTodos();

        //metodos de Insercao
        TEntity Inserir(TEntity entity);
        IEnumerable<TEntity> Inserir(IEnumerable<TEntity> entities);

        //metodos de Atualizacao
        void Atualizar(TEntity entity);
        void Atualizar(IEnumerable<TEntity> entities);

        //metodos de exclusao
        void Excluir(TEntity entity);
        void Excluir(IEnumerable<TEntity> entities);

        //Estado da entidade desanexado        
        void EntityStateDetached(TEntity entity);



    }

}
