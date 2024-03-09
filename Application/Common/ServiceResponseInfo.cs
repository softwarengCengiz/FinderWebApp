using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    [Serializable]
    public class ServiceResponseInfo
    {
        public ErrorInfo Error { get; set; }

        public bool IsSuccessful { get; set; }

        public ServiceResponseInfo(bool isSuccessful = true)
        {
            IsSuccessful = isSuccessful;
        }

        public ServiceResponseInfo(ErrorInfo error, bool isSuccessfull = false)
        {
            Error = error;
            IsSuccessful = isSuccessfull;
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

            return obj.GetType() == GetType() && Equals((ServiceResponseInfo)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IsSuccessful, Error);
        }

        protected bool Equals(ServiceResponseInfo other)
        {
            return IsSuccessful == other.IsSuccessful && object.Equals(Error, other.Error);
        }
    }
}
