using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class LeaveTypeDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public int DefaultDays { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
