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
        //private IDomainChildEvent _child;
        public CreateOrUpdateCategoryHandler(IDomainParentEvent Parent) //(IDomainParentEvent<Category> Parent) //IMappingEngine mapper, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            this._parent = Parent;
            //this._child = Child;
            //this.mapper = mapper;
            //this.categoryRepository = categoryRepository;
            //this.unitOfWork = unitOfWork;
        }
        public ICommandResult Execute(CreateOrUpdateCategoryCommand command)
        {
            /*
            var category = this.mapper.Map<Category>(command);

            if (category.CategoryId == 0)
                categoryRepository.Add(category);
            else
                categoryRepository.Update(category);

            unitOfWork.Commit();
            return new CommandResult(true);
            */

            //Mapear con Mapper // Event Log - Creamos objeto para loguear en el EventHandler
            var category = new Category();
                Random rnd= new Random();
                int rndnumber = rnd.Next();
                category.CategoryId = rndnumber;
                category.CategoryName = "Autogenerate";
            // 
            
            _parent.FireSomeEvent(); //trackea la creacion de una entidad Category

            return new CommandResult(true);
        }
    }
}
