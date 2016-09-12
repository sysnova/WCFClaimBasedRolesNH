using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.Interfaces
{
    public interface IDomainHandler<T> where T : IDomainEvent
    {
        void Handle(T @event);
    }
}
