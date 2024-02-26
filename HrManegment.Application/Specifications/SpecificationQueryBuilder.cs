using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore; // Add this line
namespace HrManegment.Application.Specifications
{
    public static class SpecificationQueryBuilder
    {
        public static async Task<IQueryable<TEntity>> GetQueryAsync<TEntity>(
            IQueryable<TEntity> inputQuery, Specification<TEntity> specification) where TEntity : BaseEntity
        {
            var query = inputQuery;
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            if (specification.Includes != null)
            {

                query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            }

            if (specification.OrderBy != null)
            {

                query = query.OrderBy(specification.OrderBy);

            }

            return query;
        }
    }
}
