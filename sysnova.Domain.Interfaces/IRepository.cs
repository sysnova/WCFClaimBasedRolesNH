using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class 
    {
        int Add(TEntity item);//add other missing CRUD operations

        IEnumerable<TEntity> GetById(int Id);

        IEnumerable<TEntity> GetAll();

    }
}
