using HR.LeaveManagement.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.Contracts.Persistence
{
    public interface ILeaveAllocationRepository : IGenericRepository<LeaveAllocation>
    {
        Task<LeaveAllocation> GetLeaveAllocationWithDetails(Guid id);
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();
        Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string userId);
        Task<bool> AllocationExists(string userId, Guid leaveTypeId, int period);
        Task AddAllocations(List<LeaveAllocation> allocations);
        Task<LeaveAllocation> GetUserAllocations(string userId, Guid leaveTypeId);

    }
}
