using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace sysnova.Infrastructure.Errors
{
    public class GlobalErrorHandler : IErrorHandler
    {
        public bool HandleError(Exception error) //this runs async
        {
            //add your logic to check the error and log to db or log files etc.
            return true;
        }

        public void ProvideFault(Exception error, System.ServiceModel.Channels.MessageVersion version, ref System.ServiceModel.Channels.Message fault)
        {
            //check the error here and return the 'fault' object with message that you want the user to see 
            if (!(error is FaultException || error is Exception))
                return;

            // Creates the exception we want to send back to the client
            var exception = new FaultException<GlobalErrorDetails>(
                GlobalErrorDetails.Default,
                new FaultReason(GlobalErrorDetails.Default.Message));

            // Creates a message fault
            var messageFault = exception.CreateMessageFault();

            // Creates the new message based on the message fault
            fault = Message.CreateMessage(version, messageFault, exception.Action);
        }
    }
}
