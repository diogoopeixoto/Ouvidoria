using System;
using System.Collections.Generic;
using System.Text;

namespace OuvidoriaAPI.DTO
{
    public sealed class ResultadoAcao
    {
        public override string ToString()
        {
            return $"[{Status}]: {Message}";
        }

        public string Message { get; }
        public object Data { get; }
        public StatusRetorno Status { get; }

        internal ResultadoAcao(string message = null, StatusRetorno status = StatusRetorno.OK)
        {
            Status = status;
            Message = message;
        }

        internal ResultadoAcao(string message, object data, StatusRetorno status = StatusRetorno.OK)
        {
            Status = status;
            Message = message;
            Data = data;
        }

        internal ResultadoAcao(object data, StatusRetorno status = StatusRetorno.OK)
        {
            Status =status;
            Data = data;
        }

        internal ResultadoAcao(Exception ex, StatusRetorno status = StatusRetorno.BadRequest)
        {
            Status = status;
            Message = ex.Message;
            if (ex.InnerException != null)
                Message += $"\n{ex.InnerException.Message}";
        }
    }
}
