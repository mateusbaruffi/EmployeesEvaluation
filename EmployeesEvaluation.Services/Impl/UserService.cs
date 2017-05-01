using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesEvaluation.Repository.Repositories;
using EmployeesEvaluation.Core.Models;

namespace EmployeesEvaluation.Services.Impl
{
    public class UserService : IUserService
    {

        private IUserRepository _userRepository;
        private IUserRelationRepository _userRelationRepository;

        public UserService(IUserRepository userRepository, IUserRelationRepository userRelationRepository)
        {
            this._userRepository = userRepository;
            this._userRelationRepository = userRelationRepository;
        }

        public IEnumerable<ApplicationUser> All()
        {
            return _userRepository.GetAll();
        }

        public void Add(UserRelation ur)
        {
            _userRelationRepository.Add(ur);
        }
    }
}
