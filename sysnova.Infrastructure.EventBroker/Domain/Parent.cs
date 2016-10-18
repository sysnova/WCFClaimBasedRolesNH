using Appccelerate.EventBroker;
using Ninject;
using sysnova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.EventBroker.Domain
{
    public class Parent : IDomainParentEvent, IDisposable
    {
        public Guid Id { get; private set; }

        public Parent(
            [Named("FirstChild")] Child firstChild )//,
            //[Named("SecondChild")]Child secondChild)
        {
            this.FirstChild = firstChild;
            //this.SecondChild = secondChild;
            this.Id = Guid.NewGuid();
        }

        [EventPublication("SomeEventTopic")]
        public event EventHandler SomeEvent;

        public Child FirstChild { get; private set; }

        //public Child SecondChild { get; private set; }

        public void FireSomeEvent()
        {
            if (this.SomeEvent != null)
            {
                System.Diagnostics.Debug.WriteLine("<----- ID Event Broker --"+this.Id+"--->");
                this.SomeEvent(this, EventArgs.Empty);
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
                System.Diagnostics.Debug.WriteLine("<----- GARBAGEEEEE --->");

                // free managed resources
            }
            // free native resources if there are any.
        }
    }
}
