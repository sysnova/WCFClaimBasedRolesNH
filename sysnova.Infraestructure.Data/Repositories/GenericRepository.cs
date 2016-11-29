using NHibernate;
using NHibernate.Criterion;
using sysnova.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infraestructure.Data
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private UnitOfWork _unitOfWork;
        public GenericRepository (IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }
        protected ISession Session { get { return _unitOfWork.Session; } }

        public int Add(TEntity item)
        {
            return (int)Session.Save(item);
        }

        public void Update(TEntity item)
        {
            Session.SaveOrUpdate(item);
        }

        public IEnumerable<TEntity> GetById(int Id)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(TEntity));
            //criteria.SetCacheable(true);
            if (typeof(TEntity).Name.Equals("Category"))
                criteria.Add(Restrictions.Eq("CategoryId", Id));
            else if (typeof(TEntity).Name.Equals("Cloud") || typeof(TEntity).Name.Equals("HelpDesk"))
                criteria.Add(Restrictions.Eq("IssueId", Id));
            else
                criteria.Add(Restrictions.Eq("ProductId", Id));


            IEnumerable<TEntity> authorities = criteria.List<TEntity>();
            return authorities;
        }

        public IEnumerable<TEntity> GetAll()
        {
            IList<TEntity> authorities = Session.QueryOver<TEntity>().List();
            return authorities;
        }

    }
}
