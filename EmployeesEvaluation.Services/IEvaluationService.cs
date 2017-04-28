using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EmployeesEvaluation.Core.Models;


namespace EmployeesEvaluation.Services
{

    public interface IEvaluationService : IGenericService<Evaluation>
    {
        IEnumerable<Evaluation> LoadAll();
    }

}