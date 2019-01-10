using api.domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace api.infra.Data
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly DBContext _dbContexto;

        public EfRepository(DBContext dbbcontexto)
        {
            _dbContexto = dbbcontexto;
        }

        #region atualização

        public virtual void Atualizar(TEntity entity)
        {            
            _dbContexto.Set<TEntity>().Update(entity);
            _dbContexto.SaveChanges();
        }

        public virtual void Atualizar(IEnumerable<TEntity> entities)
        {
            _dbContexto.Set<TEntity>().UpdateRange(entities);
            _dbContexto.SaveChanges();
        }

        #endregion

        #region exclusao

        public virtual void Excluir(TEntity entity)
        {
            _dbContexto.Set<TEntity>().Remove(entity);
            _dbContexto.SaveChanges();
        }

        public virtual void Excluir(IEnumerable<TEntity> entities)
        {
            _dbContexto.Set<TEntity>().RemoveRange(entities);
            _dbContexto.SaveChanges();
        }

        #endregion

        #region inserção

        public virtual TEntity Inserir(TEntity entity)
        {             
            _dbContexto.Set<TEntity>().Add(entity);         
            _dbContexto.SaveChanges();
            return entity;
        }

        public virtual IEnumerable<TEntity> Inserir(IEnumerable<TEntity> entities)
        {
            _dbContexto.AddRange(entities);
            _dbContexto.SaveChanges();
            return entities;
        }

        #endregion

        #region pesquisa

        public virtual IEnumerable<TEntity> Pesquisar(Func<TEntity, bool> expressao)
        {          
            return _dbContexto.Set<TEntity>().Where(expressao).AsEnumerable();
        }

        public TEntity PesquisarPorId(int id)
        {
            return _dbContexto.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> PesquisarTodos()
        {
            return _dbContexto.Set<TEntity>().AsEnumerable();
        }

        #endregion
    }
}
