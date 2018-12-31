using sysnova.Domain.Entities;
using sysnova.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Security.Permissions;
using System.Threading;
using sysnova.Infrastructure.Errors;
using sysnova.Infrastructure.Interfaces;
using sysnova.Infrastructure.CommandBus.Dispatcher;
using sysnova.Infrastructure.CommandBus.Command;
using sysnova.Domain.Core.Common;
using sysnova.Infrastructure.EventBus.Domain;
using sysnova.Infrastructure.EventBus.Events;
using sysnova.Infrastructure.EventBus;
using System.Threading.Tasks;

namespace sysnova.Services.CRUDService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerSession)]
    [GlobalErrorHandlerBehaviour(typeof(GlobalErrorHandler))]
    public class Service1 : IService1
    {
        //BY REPOSITORY
        private IUnitOfWork _uow;
        private IRepository<Category> _catRepo;
        private IRepository<Product> _prodRepo;
        // BY SERVICE
        private IProductService _serviceCloud;
        //BY COMMANDBUS
        private readonly ICommandBus _commandBus;

        public Service1(ICommandBus commandBus, IUnitOfWork uow, IRepository<Product> prodRepo, IRepository<Category> catRepo, IProductService serviceCloud)
        {
            _commandBus = commandBus;
            _uow = uow;
            _prodRepo = prodRepo; 
            _catRepo = catRepo;
            _serviceCloud = serviceCloud;
        }
        public Service1()
        {

        }
        public string[] GetProducts(int value)
        {
            string id = _uow.GetSessionId().ToString();
            IEnumerable<Product> result = _prodRepo.GetById(value);
            _uow.Commit();
            string[] s = result.Select(p => string.Format("{0} - {1} - {2} - {3}", p.ProductId, p.ProductName, p.Category.CategoryId, p.Category.CategoryName))
                     .ToArray();
            return s;
        }
        public string DoAdd()
        {
            int idCat;
            try
            { 
                Product someProd = new Product();
                Category someCat = new Category();
                someCat.CategoryName = "UoW RollBack";
                    idCat = (int)_catRepo.Add(someCat);
                someProd.Category = (Category) _catRepo.GetById(idCat).FirstOrDefault();
                    someProd.ProductName = "UoW TestRollBack";
                    someProd.UnitPrice = 100;
                    _prodRepo.Add(someProd);
                _uow.Commit();
            }
            catch (Exception e)
            {
                _uow.Rollback();
                return e.Message;
            }
            return idCat.ToString();
        }


        public string DoAddCloud()
        {
            int idCloud;
            try
            {
                Cloud someCloud = new Cloud();

                someCloud.InitDate = DateTime.Today;
                someCloud.IssueName = "TEST INTEGRACION";
                someCloud.State = "INIT";
                someCloud.Value = "VALOR";
                someCloud.Caption = "CAPTION";
                
                idCloud = _serviceCloud.AddCloud(someCloud);

                IEnumerable<Cloud> cloudForms = _serviceCloud.GetFormCloud(17);
                
                IEnumerable<HelpDesk> helpDeskForms = _serviceCloud.GetFormHelpDesk(1016);

                //TEST QUERY ISSUES. TRAE DE TODO!! Del tipo Cloud & HelpDesk
                IEnumerable<Issues> issuesForms = _serviceCloud.GetAllIssues();

                _uow.Commit(); // el control del commit lo saco al servicio a traves de UoW.
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return idCloud.ToString();
        }

        public string DoAddHelpDesk()
        {
            int idHelp;
            try
            {
                HelpDesk someHelp = new HelpDesk();
                //someHelp.IssueType = FormUnitType.HelpDesk;
                someHelp.InitDate = DateTime.Today;
                someHelp.IssueName = "TEST ATENCION";
                someHelp.State = "INIT";
                someHelp.Value = "AT.TEL";
                someHelp.Caption = "MARCADOR";

                idHelp = _serviceCloud.AddHelpDesk(someHelp);

                _uow.Commit();
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return idHelp.ToString();
        }

        [PrincipalPermission(SecurityAction.Demand, Authenticated = true, Role = "Condiments")]
        public string[] GetCategories(int value)
        {
            //CommandBus
            var command = new CreateOrUpdateCategoryCommand()
            {
                CategoryId = 8,
                Name = "LEO",
                Description = "LEO"
            };
            
            IEnumerable<ValidationResult> errors = _commandBus.Validate(command);

            var resultBus = _commandBus.Submit(command);
            
            //EventBus
            //var survey = new Survey();
            //survey.EndSurvey(); // NOTIFY EVENT
            //

            var principal = Thread.CurrentPrincipal;
            if (!(principal.IsInRole("Condiments")))
                throw new System.ServiceModel.Security.SecurityAccessDeniedException("Insuficient privileges - Procedure");

            IEnumerable<Category> result = _catRepo.GetById(value);
            string[] s = result.Select(p => string.Format("{0} - {1}", p.CategoryId, p.CategoryName)).ToArray();
            //string[] s = {"TEST","THREAD","ASYNC"};
            _uow.Commit();
            return s;
        }

    }
}
