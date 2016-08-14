using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace sysnova.Domain.Entities
{
    public class Cloud : Issues
    {
        public override FormUnitType IssueType { get { return FormUnitType.Cloud; } set { ;} }
        public virtual string Caption { get; set; }
        public virtual string Value { get; set; }
        
    }
}
