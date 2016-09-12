using Ninject;
using sysnova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.EventBus.Dispatcher
{
    public class DefaultEventBus : IEventDispatcher //Se invoca desde el RegisterService del Module
   {
        private readonly IKernel _kernel;

        public DefaultEventBus(IKernel kernel)
        {
            _kernel = kernel;
        }


        public void Dispatch<TEvent>(TEvent eventToDispatch) where TEvent : IDomainEvent
        {
            foreach (var handler in _kernel.GetAll<IDomainHandler<TEvent>>())
            {
                handler.Handle(eventToDispatch);
            }
        }
    }
}
