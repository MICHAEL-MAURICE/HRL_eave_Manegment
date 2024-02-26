using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.Specifications
{
    public class VacationSpecification:Specification<LeaveType>
    {
        public VacationSpecification():base(leave=>leave.Name == "Vacation") 
        {
                
        }
    }
}
