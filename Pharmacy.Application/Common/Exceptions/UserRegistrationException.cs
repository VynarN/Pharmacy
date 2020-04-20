using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Pharmacy.Application.Common.Exceptions
{
    [Serializable]
    public class UserRegistrationException : ObjectException
    {
        public UserRegistrationException()
        {
        }

        public UserRegistrationException(string message)
            : base(message)
        {
        }

        public UserRegistrationException(string message, string objectIdentifier)
            : base(message, objectIdentifier)
        {
        }

        public UserRegistrationException(string message, string objectIdentifier, Exception inner)
            : base(message, objectIdentifier, inner)
        {
        }

        public UserRegistrationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected UserRegistrationException(SerializationInfo info, StreamingContext context)
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
