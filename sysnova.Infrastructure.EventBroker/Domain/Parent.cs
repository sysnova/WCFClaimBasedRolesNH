using Appccelerate.EventBroker;
using Ninject;
using sysnova.Domain.Entities;
using sysnova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.EventBroker.Domain
{
    public class Parent : IDomainParentEvent//, IDisposable //<Category>, IDisposable
    {
        public String Id { get; private set; }

        public Parent(
            [Named("FirstChild")] IDomainChildEvent firstChild)
            //[Named("SecondChild")]Child secondChild)
        {
            this.FirstChild = firstChild;
            //this.SecondChild = secondChild;
            this.Id = DateTime.Now.ToString("yyyyMMddHHmmssffff");
        }

        [EventPublication("SomeEventTopic", HandlerRestriction.Synchronous)]
        public event EventHandler<CustomEventArgs> SomeEvent;

        public IDomainChildEvent FirstChild { get; private set; }

        //public Child SecondChild { get; private set; }

        public void FireSomeEvent()
        {
            if (this.SomeEvent != null)
            {
                System.Diagnostics.Debug.WriteLine("<----- ID Event Broker: --"+this.Id+"-->");
                this.SomeEvent(this, new CustomEventArgs());
                Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                System.Diagnostics.Debug.WriteLine("<----- GARBAGEEEEE PARENT --->");
                //this.FirstChild = null;
                // free managed resources
            }
            // free native resources if there are any.
        }
    }
}
