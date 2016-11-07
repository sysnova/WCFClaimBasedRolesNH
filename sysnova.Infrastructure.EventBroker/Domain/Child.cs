using Appccelerate.EventBroker;
using Appccelerate.EventBroker.Handlers;
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
        private IUnitOfWork _uow;
        private Guid _id;
        private Boolean executed = false;

        public Child(IRepository<Category> catRepo) //por Cache de Objetos valido si ya se ejecuto para evitar excepcion. //IUnitOfWork uow
        {
            _catRepo = catRepo;
            //_uow = uow; //No hace falta, ya que el commit lo maneja el servicio global.
            _id = Guid.NewGuid();
        }
        public bool EventReceived { get; private set; }

        [EventSubscription("SomeEventTopic", typeof(OnPublisher))] //OnPublisher
        public void HandleSomeEvent(object sender, CustomEventArgs e)
        {
            try
            {
                if (!executed)
                {
                    this.EventReceived = true;
                    //System.Threading.Thread.Sleep(5000);
                    _catRepo.Add(_catRepo.GetById(12000).FirstOrDefault()); //SESSION CLOSE!! REVISAR ERROR SOBRE TODO EN BACKGORUND. VER DE LEVANTAR LA SESION DESDE OTRO LUGAR, YA QUE CHILD Y PARENT SE LEVANTA UNA SOLA VEZ
                    //_uow.Commit();
                    System.Diagnostics.Debug.WriteLine("<----- Raise Event Category ----->");
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
                System.Diagnostics.Debug.WriteLine("<----- GARBAGEEEEE --->");
                // free managed resources
            }
            // free native resources if there are any.
        }
    }
}
