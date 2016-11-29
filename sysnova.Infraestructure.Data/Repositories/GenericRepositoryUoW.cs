using NHibernate;
using sysnova.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infraestructure.Data
{
    public class GenericRepositoryUoW<TEntity, TId> : IRepositoryUoW<TEntity, TId> where TEntity : class
    {
        private UnitOfWork _unitOfWork;
        public GenericRepositoryUoW (IUnitOfWork unitOfWork)
        {
            _unitOfWork = (UnitOfWork)unitOfWork;
        }
        protected ISession Session { get { return _unitOfWork.Session; } }

        public TId Add(TEntity item)
        {
            return (TId)Session.Save(item);
        }
    }
}
