using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Pharmacy.Application.Common.Exceptions
{
    [Serializable]
    public class ConfirmationException : ObjectException
    {
        public ConfirmationException()
        {
        }

        public ConfirmationException(string message)
            : base(message)
        {
        }

        public ConfirmationException(string message, string objectIdentifier)
            : base(message, objectIdentifier)
        {
        }

        public ConfirmationException(string message, string objectIdentifier, Exception inner)
            : base(message, objectIdentifier, inner)
        {
        }

        public ConfirmationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ConfirmationException(SerializationInfo info, StreamingContext context)
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
