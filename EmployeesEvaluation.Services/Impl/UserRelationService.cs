using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Repository.Repositories;
using System.Linq.Expressions;

namespace EmployeesEvaluation.Services.Impl
{
    public class UserRelationService : IUserRelationService
    {
       
        private IUserRelationRepository _userRelationRepository;

        public UserRelationService(IUserRelationRepository userRelationRepository)
        {
           
            this._userRelationRepository = userRelationRepository;
        }

        public IEnumerable<UserRelation> FindBy(Expression<Func<UserRelation, bool>> predicate)
        {
            return _userRelationRepository.FindBy(predicate);
        }

        public void DeleteWhere(Expression<Func<UserRelation, bool>> predicate)
        {
            _userRelationRepository.DeleteWhere(predicate);
            _userRelationRepository.Commit();
        }

        public void Create(UserRelation ur)
        {
            _userRelationRepository.Add(ur);
            _userRelationRepository.Commit();
        }

    }
}
