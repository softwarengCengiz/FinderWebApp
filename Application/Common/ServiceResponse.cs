using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    [Serializable]
    public sealed class ServiceResponse<TResult> : ServiceResponseInfo
    {
        public TResult Result { get; set; }

        public ServiceResponse(TResult result, bool isSuccessful = true)
            : base(isSuccessful)
        {
            Result = result;
        }

        public ServiceResponse(TResult result, ErrorInfo error, bool isSuccessful = false)
            : base(error, isSuccessful)
        {
            Result = result;
        }

        public override bool Equals(object obj)
        {
            return this == obj || (obj is ServiceResponse<TResult> other && Equals(other));
        }

        public override int GetHashCode()
        {
            return EqualityComparer<TResult>.Default.GetHashCode(Result);
        }

        private bool Equals(ServiceResponse<TResult> other)
        {
            return EqualityComparer<TResult>.Default.Equals(Result, other.Result);
        }
    }
}
