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
    public class EvaluationsController : Controller
    {

        private readonly IEvaluationService _evaluationService;
        private readonly ILogger _logger;

        public EvaluationsController(IEvaluationService evaluationService, ILogger<EvaluationsController> logger)
        {
            this._evaluationService = evaluationService;
            this._logger = logger;
        }

        [HttpPost("List")]
        public JsonResult List([DataSourceRequest]DataSourceRequest request)
        {
            var result = _evaluationService.All().Select(Mapper.Map<Evaluation, EvaluationDto>);
            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }

        [HttpPost("Delete")]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, EvaluationDto evaluationDto)
        {
            //var evaluation = Mapper.Map<EvaluationDto, Evaluation>(evaluationDto);

            _evaluationService.Delete(evaluationDto.Id);

            return Json(new { Data = evaluationDto });
        }
    }
}