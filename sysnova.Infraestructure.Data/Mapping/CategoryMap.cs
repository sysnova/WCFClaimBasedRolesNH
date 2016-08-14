using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysnova.Domain.Entities;
using FluentNHibernate.Conventions.Helpers;

namespace sysnova.Infraestructure.Data
{
    public class CategoryMap : ClassMap<Category>
    {
        public CategoryMap()
        {
            Cache.ReadOnly();
            Id(x => x.CategoryId);
            Map(x => x.CategoryName);
            HasMany(x => x.Products)
                .LazyLoad();
            Table("Categories");
        }
    }
}
