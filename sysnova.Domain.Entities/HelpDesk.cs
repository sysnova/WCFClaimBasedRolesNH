using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysnova.Domain.Entities
{
    public class HelpDesk : Issues
    {
        public override FormUnitType IssueType { get { return FormUnitType.HelpDesk; } set { ;} }
        public virtual string Caption { get; set; }
        public virtual string Value { get; set; }
    }
}
