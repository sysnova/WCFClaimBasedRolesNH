using sysnova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.EventBroker.Domain
{
    public class CustomEventArgs : EventArgs
    {
       public Guid Id { get; private set; }
       //public Category Cat { get; private set; }

       public CustomEventArgs()// (Category cat)
       {
           this.Id = Guid.NewGuid();
           //this.Cat = cat;
       }
    }
}
