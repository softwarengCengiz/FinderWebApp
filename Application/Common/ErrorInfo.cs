using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    [Serializable]
    public class ErrorInfo
    {
        public int Code { get; set; }

        public Guid CorrelationId { get; set; }

        public string Details { get; set; }

        public string Message { get; set; }

        //public IEnumerable<ValidationErrorInfo> ValidationErrors { get; set; }

        public ErrorInfo()
        {
        }

        public ErrorInfo(string message)
        {
            Message = message;
        }

        public ErrorInfo(string message, Guid correlationId)
        {
            Message = message;
            CorrelationId = correlationId;
        }

        public ErrorInfo(int code)
        {
            Code = code;
        }

        public ErrorInfo(int code, string message)
            : this(message)
        {
            Code = code;
        }

        public ErrorInfo(string message, string details)
            : this(message)
        {
            Details = details;
        }

        public ErrorInfo(int code, string message, string details)
            : this(message, details)
        {
            Code = code;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (this == obj)
            {
                return true;
            }

            return obj.GetType() == GetType() && Equals((ErrorInfo)obj);
        }

        //public override int GetHashCode()
        //{
        //    return HashCode.Combine(CorrelationId, Code, Message, Details, ValidationErrors);
        //}

        //protected bool Equals(ErrorInfo other)
        //{
        //    return CorrelationId.Equals(other.CorrelationId) && Code == other.Code && Message == other.Message && Details == other.Details && object.Equals(ValidationErrors, other.ValidationErrors);
        //}
    }
}
