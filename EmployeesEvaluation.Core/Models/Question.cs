using System;
using System.Collections.Generic;

namespace EmployeesEvaluation.Core.Models
{
    public class Question : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public QuestionType QuestionType { get; set; }
        public int QuestionTypeId { get; set; }
        public ICollection<EvaluationQuestion> EvaluationQuestions { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}