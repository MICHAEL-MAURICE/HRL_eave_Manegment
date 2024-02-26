using HR.LeaveManagement.Domain;
using HrManegment.Application.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.Contracts.Persistence
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
        Task<bool> IsLeaveTypeUnique(string name);

        Task<List<LeaveType>>GetVacationLeaveType(Specification<LeaveType> specification);

    }
}
