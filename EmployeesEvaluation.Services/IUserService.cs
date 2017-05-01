using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesEvaluation.Core.Models;

namespace EmployeesEvaluation.Services
{
    public interface IUserService
    {

        IEnumerable<ApplicationUser> All();
        void Add(UserRelation ur);

    }
}
