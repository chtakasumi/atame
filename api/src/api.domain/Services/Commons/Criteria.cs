using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace api.domain.Services.Commons
{
    public  class Criteria<T>
    {
        private readonly  List<Expression<Func<T, bool>>> _list = new List<Expression<Func<T, bool>>>();

        public  void Add(Expression<Func<T, bool>> criterios)
        {


           // Expression<Func<T, bool>> query = Expression.Lambda<Func<T, bool>>(
           //Expression.And(expressId, expressNome), Expression.Parameter(typeof(T), "objeto"));

           // _list.Add(Expression.And(criterios));
        }

        public List<Expression<Func<T, bool>>> Criterios
        {
            get { return _list; }
        }

     
    }

}
