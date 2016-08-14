using sysnova.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Domain.Forms
{
    public static class FormsFactory
    {
        public static ITrack Create<T>() where T : ITrack, new()
        {
            return new T();
        }
               
    }
}
