using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Services;
using Microsoft.Extensions.Logging;

namespace EmployeesEvaluation.WEB.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger _logger;

        public DepartmentsController(IDepartmentService departmentService, ILogger<DepartmentsController> logger)
        {
            this._departmentService = departmentService;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }

        public IActionResult Save(Department department)
        {
            _logger.LogInformation("@@@@@@@@@@@@@@@@@@ entrou no save");
            if (!ModelState.IsValid)
                return View();

            _departmentService.Create(department);

            return RedirectToAction("Index", "Departments");
        }

    }
}