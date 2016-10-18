using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.EventBroker.Domain
{
    public class Child
    {
        public bool EventReceived { get; private set; }

        [EventSubscription("SomeEventTopic", typeof(OnBackground))]
        public void HandleSomeEvent(object sender, EventArgs e)
        {
            this.EventReceived = true;
            System.Diagnostics.Debug.WriteLine("<----- Raise Event Broker ----->");
            System.Threading.Thread.Sleep(5000);
            System.Diagnostics.Debug.WriteLine("<----- Post Delay Event Broker ----->");

        }
    }
}
