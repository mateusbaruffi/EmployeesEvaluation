using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeesEvaluation.Core.Models;
using System.Linq.Expressions;

namespace EmployeesEvaluation.Repository.Repositories
{
    public interface IUserRepository {

        IEnumerable<ApplicationUser> GetAll();
        IEnumerable<ApplicationUser> FindBy(Expression<Func<ApplicationUser, bool>> predicate);
        
    }
}