using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Domain.Interfaces
{
    public interface IRepositoryUoW<TEntity, TId> where TEntity : class 
    {
        TId Add(TEntity item);//add other missing CRUD operations
    }
}
