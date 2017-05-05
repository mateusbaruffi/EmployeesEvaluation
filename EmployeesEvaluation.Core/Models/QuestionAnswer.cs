using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesEvaluation.Core.Models
{
    public class QuestionAnswer : IEntityBase
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int LikertAnswerId { get; set; }
        public string OpenEndedAnswer { get; set; }
        public string FileName { get; set; }
        public EvaluationResponse EvaluationResponse { get; set; }
    }
}
