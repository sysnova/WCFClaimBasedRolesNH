using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysnova.Infrastructure.CommandBus.Command;
using sysnova.Infrastructure.Interfaces;
using sysnova.Domain.Core.Common;

namespace sysnova.Infrastructure.CommandBus.ValidationHandler
{
    //SEGUIR!!! Vamos bien.. Este tiene que ser un HandlerValidation.. Crear el Handler Execute
    public class CreateOrUpdateCategoryValidationHandler : IValidationHandler<CreateOrUpdateCategoryCommand>
    {
        //private readonly ICategoryRepository categoryRepository;

        public CreateOrUpdateCategoryValidationHandler()//ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            //this.categoryRepository = categoryRepository;
        }

        public IEnumerable<ValidationResult> Validate(CreateOrUpdateCategoryCommand command)
        {

            yield return new ValidationResult("Name", "Existe");
        
        }
    }
}
