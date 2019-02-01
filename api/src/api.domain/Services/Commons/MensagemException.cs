using System;

namespace api.domain.Services.Commons
{

    [Serializable]
    public class MensagemException : Exception
    {
        public EnumStatusCode StatusCode { get; private set; }
        public string Mensagem { get; private set; }

        public MensagemException()
        {
        }

        internal MensagemException(EnumStatusCode statusCode, string mensagem = null)
        {
            this.StatusCode = statusCode;
            this.Mensagem = mensagem;
        }

    }
        
}
