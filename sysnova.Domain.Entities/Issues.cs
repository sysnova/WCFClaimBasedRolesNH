using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Domain.Entities
{
    public abstract class Issues
    {
        public virtual int IssueId { get; set; }
        public abstract FormUnitType IssueType { get; set; }
        public virtual string IssueName { get; set; }
        public virtual DateTime InitDate { get; set; }
        public virtual DateTime? FinishDate { get; set; }
        public virtual string AssignedTO { get; set; }
        public virtual string ResolvedBY { get; set; }
        public virtual string State { get; set; }
        
        //public virtual Issues ParentId { get; set; }

        //public virtual IEnumerable<Cloud> Cloud { get; set; }
    }
}
