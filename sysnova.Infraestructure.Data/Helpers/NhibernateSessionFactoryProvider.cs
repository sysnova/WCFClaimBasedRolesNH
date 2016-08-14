using NHibernate;
using Ninject.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infraestructure.Data
{
    public class NhibernateSessionFactoryProvider : Provider<ISessionFactory>
    {
        protected override ISessionFactory CreateInstance(IContext context)
        {
            var sessionFactory = new NhibernateSessionFactory();
            return sessionFactory.GetSessionFactory();
        }
    }
}
