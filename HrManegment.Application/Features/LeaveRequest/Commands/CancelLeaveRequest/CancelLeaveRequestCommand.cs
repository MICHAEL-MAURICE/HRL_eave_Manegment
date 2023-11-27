using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommand : IRequest<Unit>
{
    public Guid Id { get; set; }
}
