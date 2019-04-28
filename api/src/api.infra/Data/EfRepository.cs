using api.domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

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

        public virtual IEnumerable<TEntity> Pesquisar(Expression<Func<TEntity, bool>>  expressao)
        {          
            return _dbContexto.Set<TEntity>().Where(expressao).Take(500);
        }
              
        public TEntity PesquisarPorId(int id)
        {
            return _dbContexto.Set<TEntity>().Find(id);
        }

        public IEnumerable<TEntity> BuscarTodos()
        {
            return _dbContexto.Set<TEntity>().AsEnumerable(); 
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expressaoWhere, Expression<Func<TEntity, int?>> key)
        {
           return _dbContexto.Set<TEntity>().Where(expressaoWhere).OrderBy(key).Take(500);           
        }

        public void EntityStateDetached(TEntity entity)
        {
            if (entity != null)
            {
                _dbContexto.Entry(entity).State = EntityState.Detached;
            }
            
        }
      
        #endregion

    }
}
