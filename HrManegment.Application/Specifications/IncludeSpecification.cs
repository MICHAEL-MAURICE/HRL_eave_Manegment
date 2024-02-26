using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.Specifications
{
    public class IncludeSpecification:Specification<LeaveType>
    {
        public IncludeSpecification()
        {
               // AddInclude(le=>le)
        }
    }
}
