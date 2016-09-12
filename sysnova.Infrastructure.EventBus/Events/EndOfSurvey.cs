using sysnova.Infrastructure.EventBus.Domain;
using sysnova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.EventBus.Events
{
    public class EndOfSurvey : IDomainEvent
    {
        public Survey Survey { get; set; }
    }
}
