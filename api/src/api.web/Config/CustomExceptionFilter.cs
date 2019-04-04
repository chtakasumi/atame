using api.domain.Services.Commons;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace api.web.Config
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var status = 0;
            var mensagem = "";
            // HttpStatusCode status = HttpStatusCode.InternalServerError;
            if (context.Exception.GetType() == typeof(MensagemException))
            {
                MensagemException exception = (MensagemException)context.Exception;

                status = (int)exception.StatusCode;

                mensagem = exception.Mensagem;
            }
            else
            { 
                status = 500;
                mensagem = context.Exception.Message;
                if (context.Exception.InnerException != null) {
                    mensagem += " - " +context.Exception.InnerException.Message;
                }
            }

            HttpResponse response = context.HttpContext.Response;

            response.StatusCode = status;
            response.ContentType = "application/json";
            context.Result = new JsonResult(mensagem);
        }
       
    }
}
