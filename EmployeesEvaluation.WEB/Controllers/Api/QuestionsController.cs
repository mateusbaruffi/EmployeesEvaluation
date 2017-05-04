using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class QuestionsController : Controller
    {

        private readonly IQuestionService _questionService;
        private readonly ILogger _logger;

        public QuestionsController(IQuestionService questionService, ILogger<QuestionsController> logger)
        {
            this._questionService = questionService;
            this._logger = logger;
        }

        [HttpPost("List")]
        public JsonResult List([DataSourceRequest]DataSourceRequest request)
        {
            var result = _questionService.All().Select(Mapper.Map<Question, QuestionDto>);
            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }

        [HttpGet("FindBy")]
        public IActionResult FindBy([DataSourceRequest]DataSourceRequest request, string text)
        {
           var result = _questionService.FindBy(q => q.Description.Contains(text)).Select(Mapper.Map<Question, QuestionDto>);
            //var result = _questionService.All().Select(Mapper.Map<Question, QuestionDto>);
            //var dsResult = result.ToDataSourceResult(request);
            return Json(result.ToList());
        }

        [HttpPost("Delete")]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, QuestionDto questionDto)
        {
            var question = Mapper.Map<QuestionDto, Question>(questionDto);

            _questionService.Delete(questionDto.Id);

            return Json(new { Data = questionDto });
        }
    }
}