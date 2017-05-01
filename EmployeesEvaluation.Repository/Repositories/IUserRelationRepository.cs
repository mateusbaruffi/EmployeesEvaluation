using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using EmployeesEvaluation.Core.Models;

namespace EmployeesEvaluation.Repository.Repositories
{
    public interface IUserRelationRepository
    {

        IEnumerable<UserRelation> FindBy(Expression<Func<UserRelation, bool>> predicate);
        void Add(UserRelation ur);
        void DeleteWhere(Expression<Func<UserRelation, bool>> predicate);
        void Commit();
    }
}
