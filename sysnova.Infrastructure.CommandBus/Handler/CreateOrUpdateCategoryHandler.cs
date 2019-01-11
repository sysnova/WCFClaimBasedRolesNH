using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysnova.Infrastructure.Interfaces;
using sysnova.Infrastructure.CommandBus.Command;
using sysnova.Domain.Entities;

namespace sysnova.Infrastructure.CommandBus.Handler
{
    public class CreateOrUpdateCategoryHandler : ICommandHandler<CreateOrUpdateCategoryCommand>
    {
        //private IDomainParentEvent<Category> _parent;
        private IDomainParentEvent _parent;
        private IProductService _serviceCat;
        //private IDomainChildEvent _child;
        public CreateOrUpdateCategoryHandler(IDomainParentEvent Parent, IProductService Service) //(IDomainParentEvent<Category> Parent) //IMappingEngine mapper, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this._parent = Parent;
            _serviceCat = Service;
            //this._child = Child;
            //this.mapper = mapper;
            //this.categoryRepository = categoryRepository;
            //this.unitOfWork = unitOfWork;
        }
        public ICommandResult Execute(CreateOrUpdateCategoryCommand command)
        {

            //Mapear con Mapper -> Command to Category             
            var category = new Category();
                //Random rnd= new Random();
                //int rndnumber = rnd.Next();
                category.CategoryId = command.CategoryId;
                category.CategoryName = command.Name;
                category.Description = command.Description;

            _serviceCat.UpdateCat(category);

            //By SERVICE
            IEnumerable<Category> result = _serviceCat.GetCategories(category.CategoryId);
            string[] s = result.Select(p => string.Format("{0} - {1} - {2}", p.CategoryId, p.CategoryName, p.Description)).ToArray();

            _parent.FireSomeEvent(); //trackea la creacion de una entidad Category

            return new CommandResult(s);
        }
    }
}
