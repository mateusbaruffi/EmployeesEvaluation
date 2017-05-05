using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EmployeesEvaluation.WEB.Dtos;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace EmployeesEvaluation.WEB.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private readonly IUserService _userService;
        private readonly IUserRelationService _userRelationService;
        private readonly ILogger _logger;

        public UsersController(IUserRelationService userRelationService, IUserService userService, ILogger<UsersController> logger)
        {
            this._userRelationService = userRelationService;
            this._userService = userService;
            this._logger = logger;
        }

        [HttpGet("ListDepartmentManagers")]
        public IActionResult ListDepartmentManagers([DataSourceRequest]DataSourceRequest request, string text)
        {
            var result = _userService.FindBy(u => u.UserType == UserType.DM);
            //var result = _questionService.All().Select(Mapper.Map<Question, QuestionDto>);
            //var dsResult = result.ToDataSourceResult(request);
            return Json(result.ToList());
        }

        [HttpPost("GetEmployeesByDepartmentManagerId")]
        public JsonResult GetEmployeesByDepartmentManagerId(string departmentManagerId)
        {

            var result = _userRelationService.GetEmployeesByDepartmentManagerId(departmentManagerId).Select(Mapper.Map<ApplicationUser, UserDto>);
           
            return Json(result.ToList());
        }
    }
}