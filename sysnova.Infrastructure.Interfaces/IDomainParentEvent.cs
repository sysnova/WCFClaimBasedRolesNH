﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Infrastructure.Interfaces
{
    public interface IDomainParentEvent //<TEntity> where TEntity : class
    {
        void FireSomeEvent(); //(TEntity cat);
    }
}
