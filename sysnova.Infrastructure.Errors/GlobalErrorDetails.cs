using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace sysnova.Infrastructure.Errors
{
    public class GlobalErrorDetails
    {
        public static readonly GlobalErrorDetails Default = new GlobalErrorDetails();

        [DataMember]
        public String Message { get; private set; }

        public GlobalErrorDetails()
        {
            this.Message = "There is a problem in the spacetime continuum, Marty";
        }

        public GlobalErrorDetails(String message)
        {
            this.Message = message;
        }
    }
}
