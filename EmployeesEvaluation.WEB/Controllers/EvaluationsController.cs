using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeesEvaluation.WEB.Dtos;
using Microsoft.Extensions.Logging;
using AutoMapper;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Services;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EmployeesEvaluation.WEB.Controllers
{
    public class EvaluationsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEvaluationService _evaluationService;
        private readonly IUserService _userService;
        private readonly ISeasonService _seasonService;

        public EvaluationsController(ILogger<EvaluationsController> logger, ISeasonService seasonService, IEvaluationService evaluationService, IUserService userService)
        {
            this._logger = logger;
            this._evaluationService = evaluationService;
            this._userService = userService;
            this._seasonService = seasonService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            EvaluationDto evaluationDto = new EvaluationDto();

            // get all Department Managers from users
            evaluationDto.DepartmentManagers = _userService.FindBy(u => u.UserType == UserType.DM).Select(u => new SelectListItem
            {
                Text = u.Email,
                Value = u.Id
            }).ToList();

            // get all seasons
            evaluationDto.Seasons = _seasonService.All().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            return View("Form", evaluationDto);
        }

        public IActionResult Edit(int id)
        {
            var evaluation = _evaluationService.GetSingleIncludingAll(e => e.Id == id);

            var evaluationDto = Mapper.Map<Evaluation, EvaluationDto>(evaluation);

            // get all Department Managers from users
            evaluationDto.DepartmentManagers = _userService.FindBy(u => u.UserType == UserType.DM).Select(u => new SelectListItem
            {
                Text = u.Email,
                Value = u.Id
            }).ToList();

            // get all seasons
            evaluationDto.Seasons = _seasonService.All().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            return View("Form", evaluationDto);
        }

        public IActionResult Save(EvaluationDto evaluationDto)
        {
            var evaluation = Mapper.Map<EvaluationDto, Evaluation>(evaluationDto);

            if (evaluation.Id == 0)
            {
                _logger.LogInformation("--------------- Creating evaluation -----------------");
                _evaluationService.CreateWithExistingQuestions(evaluation, evaluationDto.QuestionIds);
            }
            else
            {
                _logger.LogInformation("--------------- UPDATING evaluation -----------------");
                _evaluationService.UpdateWithExistingQuestions(evaluation, evaluationDto.QuestionIds);
            }

            return RedirectToAction("Index", "Evaluations");
        }
    }
}