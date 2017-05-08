using EmployeesEvaluation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EmployeesEvaluation.Repository.Repositories.Impl
{
    public class EvaluationResponseRepository : GenericRepository<EvaluationResponse>, IEvaluationResponseRepository
    {
        private EmployeesEvaluationContext _context;
        public EvaluationResponseRepository(EmployeesEvaluationContext context) : base(context) {
            this._context = context;

        }

        public EvaluationResponse GetSingleIncludingAll(Expression<Func<EvaluationResponse, bool>> predicate)
        {
            IQueryable<EvaluationResponse> query = _context.Set<EvaluationResponse>();
            query = query.Include(er => er.Employee).Include(er => er.Evaluation).Include(er => er.QuestionAnswers).ThenInclude(qa => qa.Question).ThenInclude(q => q.LikertAnswers);
            return query.Where(predicate).FirstOrDefault();
        }
    }
}
