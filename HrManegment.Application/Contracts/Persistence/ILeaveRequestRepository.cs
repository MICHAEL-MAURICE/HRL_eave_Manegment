using HR.LeaveManagement.Domain;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
        Task<LeaveRequest> GetLeaveRequestWithDetails(Guid id);
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetails();
        Task<List<LeaveRequest>> GetLeaveRequestsWithDetails(string userId);
    }
}
