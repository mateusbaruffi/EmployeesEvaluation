using System;

namespace EmployeesEvaluation.Core.Models
{
    public class EvaluationQuestion : IEntityBase
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public Evaluation Evaluation { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

                  
    }
}