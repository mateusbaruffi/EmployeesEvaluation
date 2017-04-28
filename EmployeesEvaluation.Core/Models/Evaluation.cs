using System;
using System.Collections.Generic;

namespace EmployeesEvaluation.Core.Models
{
    public class Evaluation : IEntityBase
    {
        public int Id { get; set; }
        public string Introduction { get; set; }           
        public string Disclosure { get; set; }
        public ICollection<EvaluationQuestion> EvaluationQuestions { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}