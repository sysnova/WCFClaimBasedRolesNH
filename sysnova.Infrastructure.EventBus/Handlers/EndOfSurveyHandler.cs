using sysnova.Infrastructure.EventBus.Events;
using sysnova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.EventBus.Handlers
{
    public class EndOfSurveyHandler : IDomainHandler<EndOfSurvey>
    {
        public void Handle(EndOfSurvey args)
        {
            args.Survey.QualityChecker = "Ivan Amalo";
            //Send Email To Ivan to check the survey
        }
    }
}
