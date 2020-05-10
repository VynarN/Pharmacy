using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Pharmacy.Application.Common.Exceptions
{
    [Serializable]
    public class SendEmailException: Exception
    {
        public string EmailReceiver = "Receiver: ";

        public SendEmailException()
        {
        }

        public SendEmailException(string message)
            : base(message)
        {
        }

        public SendEmailException(string message, string receiver)
            : base(message)
        {
            EmailReceiver += receiver;
        }
        
        public SendEmailException(string message, string receiver, Exception inner)
            : base(message, inner)
        {
            EmailReceiver += receiver;
        }

        public SendEmailException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected SendEmailException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
            EmailReceiver = info.GetString("EmailReceiver");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("EmailReceiver", EmailReceiver);

            base.GetObjectData(info, context);
        }

        public override string ToString()
        {
            return GetType().Name + ": " + Message + EmailReceiver;
        }
    }
}
