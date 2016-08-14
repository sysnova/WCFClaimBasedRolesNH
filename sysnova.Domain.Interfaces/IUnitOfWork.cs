using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        //object Add(object obj);//all other supported CRUD operations we want to expose
        void Commit();
        void Rollback();
        Guid GetSessionId();
    }
}
