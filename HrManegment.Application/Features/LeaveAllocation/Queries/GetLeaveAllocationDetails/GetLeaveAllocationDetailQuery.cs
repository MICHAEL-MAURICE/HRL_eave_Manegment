using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailQuery : IRequest<LeaveAllocationDetailsDto>
{
    public Guid Id { get; set; }
}
