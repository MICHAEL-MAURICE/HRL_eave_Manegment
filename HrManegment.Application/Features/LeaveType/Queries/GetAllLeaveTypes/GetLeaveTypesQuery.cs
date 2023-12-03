using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public record GetLeaveTypesQuery(int PageNumber = 1, int Count = 10) : IRequest<IEnumerable<LeaveTypeDto>>;
    
}
