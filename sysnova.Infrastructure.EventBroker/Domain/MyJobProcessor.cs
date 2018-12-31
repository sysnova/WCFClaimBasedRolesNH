using Ninject.Extensions.NamedScope; //DisposeNotifyingObject
using sysnova.Domain.Entities;
using sysnova.Domain.Interfaces;
using sysnova.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting; //IRegisteredObject

namespace sysnova.Infrastructure.EventBroker.Domain
{
    public class MyJobProcessor : DisposeNotifyingObject, IRegisteredObject
    {
        private IRepository<Category> _catRepo;

        public MyJobProcessor(IRepository<Category> catRepo)
        {
            _catRepo = catRepo;
        }

        public void Execute(string CategoryName) {

            Category _cat = (Category)_catRepo.GetById(6).FirstOrDefault();
            _cat.CategoryName = CategoryName.Substring(0, 15);
            _catRepo.Update(_cat); //SESSION CLOSE!! REVISAR ERROR SOBRE EXECUTE BACKGORUND. VER DE LEVANTAR LA SESION DESDE OTRO LUGAR, YA QUE CHILD Y PARENT SE LEVANTA UNA SOLA VEZ
                                   //_uow.Commit();
            System.Diagnostics.Debug.WriteLine("<----- Raise Event Category ----->");
        }
        public void Stop(bool immediate) {

        }
    }
   
}
