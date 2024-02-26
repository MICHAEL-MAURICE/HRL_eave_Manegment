using HR.LeaveManagement.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HrManegment.Application.Specifications
{
    public abstract class Specification<TEntity> where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>> Criteria { get; }
        public List<Expression<Func<TEntity, Object>>> Includes { get; } = new List<Expression<Func<TEntity, Object>>>();
        public Expression<Func<TEntity, Object>> OrderBy { get; private set; }

        public Specification() { }
        public Specification(Expression<Func<TEntity,bool>>criteria) {
            Criteria = criteria;
        }

        protected void AddInclude(Expression<Func<TEntity, Object>> IncludeExpression)
        {
            Includes?.Add(IncludeExpression);  
        }

        protected void AddOrderBy(Expression<Func<TEntity, Object>> OrderByExpression)
        {
            OrderBy = OrderByExpression;
        }

    }
}
