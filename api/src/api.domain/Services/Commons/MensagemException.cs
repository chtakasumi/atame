using System;

namespace api.domain.Services.Commons
{

    [Serializable]
        internal class MensagemException : Exception
        {
            private EnumStatusCode requisicaoInvalida;
            private string mensagem;

            public MensagemException()
            {
            }
          
            public MensagemException(EnumStatusCode requisicaoInvalida, string mensagem = null)
            {
                this.requisicaoInvalida = requisicaoInvalida;
                this.mensagem = mensagem;
            }
            
        } 
        
}
