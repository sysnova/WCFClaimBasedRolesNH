using sysnova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.Interfaces
{
    public interface IProductService
    {
        //IMPLEMENTAR T > Generic tanto en GetCategories como en Add
        IEnumerable<Category> GetCategories(int Id);

        void UpdateCat(Category category);

        int AddCloud(Cloud item);

        int AddHelpDesk(HelpDesk item);

        IEnumerable<Cloud> GetFormCloud(int Id);

        IEnumerable<HelpDesk> GetFormHelpDesk(int Id);

        IEnumerable<Issues> GetAllIssues();

    }
}
