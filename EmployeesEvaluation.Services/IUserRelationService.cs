using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesEvaluation.Core.Models;
using System.Linq.Expressions;

namespace EmployeesEvaluation.Services
{
    public interface IUserRelationService
    {

        IEnumerable<UserRelation> FindBy(Expression<Func<UserRelation, bool>> predicate);
        void DeleteWhere(Expression<Func<UserRelation, bool>> predicate);
        void Create(UserRelation ur);

    }
}
