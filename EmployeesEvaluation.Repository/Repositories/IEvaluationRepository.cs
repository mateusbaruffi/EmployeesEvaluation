using EmployeesEvaluation.Core.Models;
using System.Collections.Generic;

namespace EmployeesEvaluation.Repository.Repositories
{
    public interface IEvaluationRepository : IGenericRepository<Evaluation> { 
        IEnumerable<Evaluation> LoadAll();

    }
}