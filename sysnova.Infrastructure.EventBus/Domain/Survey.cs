using sysnova.Infrastructure.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.EventBus.Domain
{
    public class Survey
    {
        public Guid Id { get; private set; }
        public DateTime EndTime { get; private set; }
        public string QualityChecker { get; set; }

        public Survey()
        {
            this.Id = Guid.NewGuid();
        }

        public void EndSurvey()
        {
            EndTime = DateTime.Now;
            DomainEvent.Raise(new EndOfSurvey() { Survey = this });
        }

    }
}
