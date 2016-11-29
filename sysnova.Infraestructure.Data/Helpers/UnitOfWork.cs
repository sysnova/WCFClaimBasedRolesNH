using sysnova.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace sysnova.Infraestructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly ISession _session;
        private readonly ITransaction _transaction;

        public ISession Session { get {return _session;} }
        
        public UnitOfWork(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
            _session = _sessionFactory.OpenSession();
            _transaction = _session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                // commit transaction if there is one active
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Commit();
            }
            catch
            {
                // rollback if there was an exception
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();

                throw;
            }
            finally
            {
                _session.Dispose();
            }
        }

        public void Rollback()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();
            }
            finally
            {
                _session.Dispose();
            }
        }
        public Guid GetSessionId()
        {
            return _session.GetSessionImplementation().SessionId;
        }
    }
}
