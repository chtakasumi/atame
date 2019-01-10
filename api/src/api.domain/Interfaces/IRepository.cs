using System;
using System.Collections.Generic;

namespace api.domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        //metodos de busca
        IEnumerable<TEntity> PesquisarTodos();
        IEnumerable<TEntity> Pesquisar(Func<TEntity, bool> expressao);
        TEntity PesquisarPorId(int id);

        //metodos de Insercao
        TEntity Inserir(TEntity entity);
        IEnumerable<TEntity> Inserir(IEnumerable<TEntity> entities);

        //metodos de Atualizacao
        void Atualizar(TEntity entity);
        void Atualizar(IEnumerable<TEntity> entities);

        //metodos de exclusao
        void Excluir(TEntity entity);
        void Excluir(IEnumerable<TEntity> entities);
    }

}
