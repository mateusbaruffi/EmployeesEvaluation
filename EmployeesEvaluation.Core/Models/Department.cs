using System;

namespace EmployeesEvaluation.Core.Models
{
    public class Department : IEntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
           
    }
}