using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysnova.Domain.Entities;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;

namespace sysnova.Infraestructure.Data
{
    public class IssuesMap : ClassMap<Issues>
    {
        public IssuesMap()
                {
                    Cache.ReadOnly();
                    Id(x => x.IssueId).GeneratedBy.Identity();

                    //DiscriminateSubClassesOnColumn("IssueType");

                    //Map(x => x.IssueType, "IssueType")
                    //    .Access.None()
                    //    .CustomType<FormUnitType>().ReadOnly();
                    
                    Map(x => x.IssueName);
                    Map(x => x.InitDate);
                    Map(x => x.FinishDate);
                    Map(x => x.AssignedTO);
                    Map(x => x.ResolvedBY);
                    Map(x => x.State);
                    //HasMany(x => x.Cloud).LazyLoad();
                    Table("Issues");
                }
            }
}
