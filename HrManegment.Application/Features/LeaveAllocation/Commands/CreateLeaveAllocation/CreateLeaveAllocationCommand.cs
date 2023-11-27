using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;

public class CreateLeaveAllocationCommand : IRequest<Unit>
{
    public Guid LeaveTypeId { get; set; }
}
