using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeesEvaluation.Core.Models;

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


    }
}