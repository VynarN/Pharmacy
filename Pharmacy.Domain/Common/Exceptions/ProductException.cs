using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Pharmacy.Domain.Common.Exceptions
{
    [Serializable]
    public class ProductException: ApplicationException
    {
        public ProductException()
        {
        }

        public ProductException(string message)
            : base(message)
        {
        }

        public ProductException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ProductException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            base.GetObjectData(info, context);
        }
    }
}
