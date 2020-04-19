using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Pharmacy.Application.Common.Exceptions
{
    [Serializable]
    public class SendEmailException: Exception
    {
        public string EmailReceiver = "Receiver: ";

        public string EmailSender = "Sender: ";

        public SendEmailException()
        {
        }

        public SendEmailException(string message)
            : base(message)
        {
        }

        public SendEmailException(string message, string receiver, string sender)
            : base(message)
        {
            EmailReceiver += receiver;
            EmailSender += sender;
        }
        
        public SendEmailException(string message, string receiver, string sender, Exception inner)
            : base(message, inner)
        {
            EmailReceiver += receiver;
            EmailSender += sender;
        }

        public SendEmailException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected SendEmailException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
            EmailSender = info.GetString("EmailSender");
            EmailReceiver = info.GetString("EmailReceiver");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException("info");
            }

            info.AddValue("EmailSender", EmailSender);
            info.AddValue("EmailReceiver", EmailReceiver);

            base.GetObjectData(info, context);
        }
    }
}
