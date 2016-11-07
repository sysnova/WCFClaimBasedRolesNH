using System;
using Ninject.Extensions.ContextPreservation;
using Ninject.Extensions.NamedScope;
using Ninject.Modules;
using Ninject.Extensions.Wcf;
using sysnova.Infrastructure.EventBroker.Domain;
using sysnova.Infrastructure.Interfaces;
using sysnova.Domain.Entities;

namespace sysnova.Infrastructure.EventBroker
{
    public class EventBrokerModule : WcfModule
    {
        /// <summary>
        /// The name of the default global event broker
        /// </summary>
        public const string DefaultGlobalEventBrokerName = "GlobalEventBroker";

        /// <summary>
        /// Loads the module into the kernel.
        /// </summary>
        public static class ProcessingScope
        {
            public static EventBrokerModule Current { get; set; }
        }
        public override void Load()
        {
            this.Kernel.AddGlobalEventBroker(DefaultGlobalEventBrokerName);
            
            //this.Kernel.Bind<Parent>().ToSelf()
            
            //this.Kernel.Bind<IDomainParentEvent>().To<Parent>()
            //    .RegisterOnEventBroker(DefaultGlobalEventBrokerName);
            
            //this.Kernel.Bind(typeof (IDomainParentEvent<Category>)).To<Parent>()
            //    .InThreadScope()
            //    .RegisterOnGlobalEventBroker();
            
            //this.Kernel.Bind<IDomainChildEvent>().To<Child>().InThreadScope().RegisterOnGlobalEventBroker();
            
            //this.Kernel.Bind<Parent>().ToSelf().RegisterOnEventBroker(DefaultGlobalEventBrokerName);
            
            //this.Kernel.Bind<Child>().ToSelf()
            //    .Named("FirstChild")
                //.WhenInjectedInto<Parent>()
                //.InDependencyCreatorScope()
            //    .RegisterOnGlobalEventBroker();
            
            //this.Kernel.Bind<Child>().ToSelf().Named("SecondChild").RegisterOnEventBroker(DefaultGlobalEventBrokerName);
            
            this.Kernel.Bind(typeof (IDomainParentEvent)).To<Parent>()
                //.InScope(o => { })
                .RegisterOnGlobalEventBroker();

            this.Kernel.Bind<IDomainChildEvent>().To<Child>()
                .Named("FirstChild")
                .RegisterOnGlobalEventBroker();

/*
            this.Kernel.Bind<Child>().ToSelf()
            //.InThreadScope()
            .Named("FirstChild")
            .RegisterOnGlobalEventBroker();
 */
        }

        /// <summary>
        /// Called after loading the modules. A module can verify here if all other required modules are loaded.
        /// </summary>
        public override void VerifyRequiredModulesAreLoaded()
        {
            if (!this.Kernel.HasModule(typeof(NamedScopeModule).FullName))
            {
                throw new InvalidOperationException("The EventBrokerModule requires NamedScopeModule!");
            }

            if (!this.Kernel.HasModule(typeof(ContextPreservationModule).FullName))
            {
                throw new InvalidOperationException("The EventBrokerModule requires ContextPreservationModule!");
            }
        }
    }
}
