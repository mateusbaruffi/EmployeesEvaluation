using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeesEvaluation.Core.Models;
using System.Linq.Expressions;

namespace EmployeesEvaluation.Repository.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private EmployeesEvaluationContext _context;

        public UserRepository(EmployeesEvaluationContext context) {
            this._context = context;
        }

        public virtual IEnumerable<ApplicationUser> GetAll()
        {
            return _context.Set<ApplicationUser>().AsEnumerable();
        }

        public virtual IEnumerable<ApplicationUser> FindBy(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return _context.Set<ApplicationUser>().Where(predicate);
        }

    }
}