

using HrManegment.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocations
{
    public class LeaveAllocationDto
    {
        public Guid Id { get; set; }
        public int NumberOfDays { get; set; }
        public LeaveTypeDto LeaveType { get; set; }
        public Guid LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}