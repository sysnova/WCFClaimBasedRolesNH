using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
using Ninject;
using sysnova.Domain.Entities;
using sysnova.Domain.Interfaces;
using sysnova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.EventBroker.Domain
{
    public class Child : IDomainChildEvent//, IDisposable
    {
        private IRepository<Category> _catRepo;
        //private IUnitOfWork _uow;

        private MyJobProcessor _processor;

        private Guid _id;
        private Boolean executed = false;
        //public string _CategoryName = "Autogenerate";
        //public string _Description = "FromEventSource";
        //public Category _cat = new Category();

        public Child(IRepository<Category> catRepo, MyJobProcessor processor) //por Cache de Objetos valido si ya se ejecuto para evitar excepcion. //IUnitOfWork uow
        {
            _processor = processor;
            _catRepo = catRepo;
            //_uow = uow; //No hace falta, ya que el commit se maneja a nivel servicio global.
            _id = Guid.NewGuid();
        }
        public bool EventReceived { get; private set; }

        [EventSubscription("SomeEventTopic", typeof(OnPublisher))] //OnBackground
        public void HandleSomeEvent(object sender, CustomEventArgs e)
        {
            try
            {
                if (!executed)
                {
                    this.EventReceived = true;

                    //System.Threading.Thread.Sleep(5000);

                    //TO-DO Agregar el Processor.Execute. La logica la pase a una clase
                    //para ver si Ninject vuelve a inyectar dependencia de repositorio en el
                    //nuevo Thread

                    //MyJobProcessor _processor = 

                    try
                    {
                        _processor.Execute(e.Id.ToString());
                    }
                    finally
                    {
                        _processor.Dispose();
                    }

                    //
                    executed = true;
                    Dispose();
                }
            }
            catch
            {
                this.EventReceived = false;
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
                System.Diagnostics.Debug.WriteLine("<----- GARBAGEEEEE CHILD--->");
                // free managed resources
            }
            // free native resources if there are any.
        }
    }
}
