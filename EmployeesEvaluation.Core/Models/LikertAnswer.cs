using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesEvaluation.Core.Models
{
    public class LikertAnswer : IEntityBase
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
