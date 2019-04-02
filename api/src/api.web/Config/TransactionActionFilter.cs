using api.infra;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore.Storage;

namespace api.web.Config
{
    public class TransactionActionFilter : IActionFilter
    {
        protected readonly DBContext _dbContexto;
        private IDbContextTransaction currentTransaction;

        public TransactionActionFilter(DBContext dbbcontexto) {

            _dbContexto = dbbcontexto;
            currentTransaction = null;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (currentTransaction == null) return;
            
            if (context.Exception != null)
                //Todo:  talvez tratar aqui os tipos de execel de sucesso e informação
                currentTransaction.Rollback();
            else
                currentTransaction.Commit();
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.Request.Method.ToUpper() == "GET" ||
                (context.HttpContext.Request.Method.ToUpper() == "POST" 
                && context.HttpContext.Request.Path.Value.ToLower().IndexOf("listar")>-1)) {
                return;
            }
            currentTransaction = _dbContexto.Database.BeginTransaction();
        }
    }
    
}
