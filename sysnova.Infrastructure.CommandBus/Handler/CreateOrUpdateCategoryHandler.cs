using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysnova.Infrastructure.Interfaces;
using sysnova.Infrastructure.CommandBus.Command;

namespace sysnova.Infrastructure.CommandBus.Handler
{
    public class CreateOrUpdateCategoryHandler : ICommandHandler<CreateOrUpdateCategoryCommand>
    {
        public CreateOrUpdateCategoryHandler() //IMappingEngine mapper, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
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
            return new CommandResult(true);
        }
    }
}
