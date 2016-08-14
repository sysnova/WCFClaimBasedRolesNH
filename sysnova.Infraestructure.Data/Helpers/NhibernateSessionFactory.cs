using FluentNHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infraestructure.Data
{
    public class NhibernateSessionFactory
    {
        [ThreadStatic]
        private NHibernate.Cfg.Configuration _configuration;
        private ISessionFactory _sessionFactory;

        public ISessionFactory GetSessionFactory()
      {
          _configuration = new NHibernate.Cfg.Configuration();
          _configuration.Configure();
          _sessionFactory = Fluently.Configure(_configuration)
               .Mappings(m => m.FluentMappings.AddFromAssemblyOf<CategoryMap>())
               .BuildSessionFactory();

          return _sessionFactory;
      }

    }
}
