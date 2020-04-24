using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Pharmacy.Application.Common.Exceptions
{
    [Serializable]
    public class ModelValidationException : ObjectException
    {
        public string Value { get; } = "Value: ";

        public ModelValidationException()
        {
        }

        public ModelValidationException(string message)
            : base(message)
        {
        }

        public ModelValidationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ModelValidationException(string message, string objectIdentifier) : base(message, objectIdentifier)
        {
        }
        
        public ModelValidationException(string message, string objectIdentifier, string value) : base(message, objectIdentifier)
        {
            Value += value;
        }

        public ModelValidationException(string message, string objectIdentifier, Exception inner) : base(message, objectIdentifier, inner)
        {
        }
        
        public ModelValidationException(string message, string objectIdentifier, string value, Exception inner) : base(message, objectIdentifier, inner)
        {
            Value += value;
        }

        protected ModelValidationException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
            Value += info.GetString("Value");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            base.GetObjectData(info, context);

            info.AddValue("Value", Value);
        }

        public override string ToString()
        {
            return GetType().Name + ": " + Message + ObjectIdentifier + ". " + Value;
        }
    }
}
