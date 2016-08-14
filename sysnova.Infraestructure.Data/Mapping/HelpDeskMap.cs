using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysnova.Domain.Entities;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;

namespace sysnova.Infraestructure.Data.Mapping
{
    public class HelpDeskMap : SubclassMap<HelpDesk>
    {
       public HelpDeskMap()
       {
           KeyColumn("HelpDesk_Id");
           Map(x => x.IssueType);
           Map(x => x.Caption);
           Map(x => x.Value);
           LazyLoad();
           Table("HelpDesk");
       }
    }
}
