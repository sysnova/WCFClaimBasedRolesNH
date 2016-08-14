using FluentNHibernate.Mapping;
using sysnova.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infraestructure.Data
{
    public class ProductMap : ClassMap<Product>
    {
        public ProductMap()
        {
            Id(x => x.ProductId);
            Map(x => x.ProductName);
            Map(x => x.UnitPrice);
            Map(x => x.Discontinued);
            References(x => x.Category, "CategoryId");
            Table("Products");
        }
    }
}
