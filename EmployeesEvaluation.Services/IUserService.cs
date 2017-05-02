using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesEvaluation.Core.Models;
using System.Linq.Expressions;

namespace EmployeesEvaluation.Services
{
    public interface IUserService
    {

        IEnumerable<ApplicationUser> All();
        void Add(UserRelation ur);
        IEnumerable<ApplicationUser> FindBy(Expression<Func<ApplicationUser, bool>> predicate);
        

    }
}
