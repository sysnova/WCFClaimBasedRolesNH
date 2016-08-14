using sysnova.Domain.Entities;
using sysnova.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Domain.Forms
{
    public class FormCloud : ITrack //<Issues>
    {
        public object GetInstance()
        {
            return new Cloud();
        }

    }
}
