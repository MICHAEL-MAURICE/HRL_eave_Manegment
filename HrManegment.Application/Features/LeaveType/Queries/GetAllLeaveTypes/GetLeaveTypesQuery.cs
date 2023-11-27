using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public record GetLeaveTypesQuery:IRequest<IEnumerable<LeaveTypeDto>>;
    
}
