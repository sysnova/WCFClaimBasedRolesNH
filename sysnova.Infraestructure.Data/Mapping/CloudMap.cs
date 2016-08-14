using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysnova.Domain.Entities;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Mapping;

namespace sysnova.Infraestructure.Data
{
    public class CloudMap : SubclassMap<Cloud>
    {
         public CloudMap()
            {
                //Cache.ReadOnly();
                //Id(x => x.Id);
                //DiscriminatorValue((int)new Cloud().IssueType);
                KeyColumn("Cloud_Id");
                //Join("Cloud", join =>
                //{
                //    join.KeyColumn("Cloud_Id");
                //    join.Optional();
                Map(x => x.IssueType);
                Map(x => x.Caption);
                Map(x => x.Value);
                //});
                //References(x => x.Issue, "IssueId");
                LazyLoad();
                Table("Cloud");
            }
     }
}
