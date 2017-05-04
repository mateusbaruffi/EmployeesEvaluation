using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EmployeesEvaluation.Core.Models;


namespace EmployeesEvaluation.Services
{

    public interface IEvaluationService : IGenericService<Evaluation>
    {
        IEnumerable<Evaluation> LoadAll();

        void CreateWithExistingQuestions(Evaluation evaluation, List<int> questionIds);

        void UpdateWithExistingQuestions(Evaluation evaluation, List<int> questionIds);

        Evaluation GetSingleIncludingAll(Expression<Func<Evaluation, bool>> predicate);
    }

}