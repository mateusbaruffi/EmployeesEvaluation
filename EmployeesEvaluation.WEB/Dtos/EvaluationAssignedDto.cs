using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesEvaluation.WEB.Dtos
{
    public class EvaluationAssignedDto
    {
        public int Id { get; set; }

        public int EvaluationId { get; set; }
        public string EmployeeId { get; set; }
        public int DepartmentManagerId { get; set; }
       
    }
}
