using System;
using System.Collections.Generic;

namespace EmployeesEvaluation.Core.Models
{
    public class Season : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Evaluation> Evaluations { get; set; }           
    }
}