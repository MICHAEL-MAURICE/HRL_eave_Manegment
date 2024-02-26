using HR.LeaveManagement.Domain;
using HrManegment.Application.Contracts.Persistence;
using HrManegment.Application.Specifications;
using HrManegment.Persistence.DatabaseContext;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HrDatabaseContext context) : base(context)
        {
        }

        public async Task<List<LeaveType>> GetVacationLeaveType(Specification<LeaveType> specification)
        {
            var query = await SpecificationQueryBuilder.GetQueryAsync(_context.LeaveTypes, specification);
            return await query.ToListAsync();
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            return await _context.LeaveTypes.AnyAsync(q => q.Name == name);
        }
    }
}
