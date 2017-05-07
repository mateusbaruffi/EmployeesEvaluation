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
using EmployeesEvaluation.WEB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace EmployeesEvaluation.WEB.Controllers
{
    [Authorize]
    public class EvaluationsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEvaluationService _evaluationService;
        private readonly IUserService _userService;
        private readonly ISeasonService _seasonService;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _environment;

        public EvaluationsController(IHostingEnvironment hostingEnviroment, IEmailSender emailSender, ILogger<EvaluationsController> logger, ISeasonService seasonService, IEvaluationService evaluationService, IUserService userService)
        {
            this._logger = logger;
            this._evaluationService = evaluationService;
            this._userService = userService;
            this._seasonService = seasonService;
            this._emailSender = emailSender;
            this._environment = hostingEnviroment;
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

        public IActionResult Assign()
        {
            return View();
        }

        public async Task<IActionResult> AssignSave(EvaluationAssignedDto evaluationAssignedDto)
        {
            var evaluationAssigned = Mapper.Map<EvaluationAssignedDto, EvaluationAssigned>(evaluationAssignedDto);

            try
            {
                _evaluationService.AssignEvaluationEmployee(evaluationAssigned);
                await SendEmail(evaluationAssignedDto);
            }
            catch (Exception e)
            {
                _logger.LogError("-------------- Something went wrong -----------", e);
            }
            

            return RedirectToAction("Index", "Evaluations");
        }

        public IActionResult Responses()
        {
            // emp only see their data 
            // dm see their data and their emps
            // hr all
            return View();
        }


        public IActionResult Reply(int id, string employeeId)
        {
            var evaluation = _evaluationService.GetEvaluationAssigned(id, employeeId);
            var evaluationDto = Mapper.Map<Evaluation, EvaluationDto>(evaluation);

            var evaluationReplyDto = new EvaluationResponseDto();
            evaluationReplyDto.EvaluationDto = evaluationDto;
            evaluationReplyDto.EvaluationId = id;
            evaluationReplyDto.EmployeeId = employeeId;

            if (evaluation == null)
                _logger.LogInformation("--------------- This Employee Has No Evaluation To Answer ");
            else
            {
                // verify if this evaluation has already been answered
            }

            return View(evaluationReplyDto);
        }

        public async Task<IActionResult> SaveReply(EvaluationResponseDto evaluationResponseDto, IFormFile File)
        {
            _logger.LogInformation("---------------- " + evaluationResponseDto );

            await UploadAnswersFile(evaluationResponseDto.QuestionAnswers);

            var evaluationResponse = Mapper.Map<EvaluationResponseDto, EvaluationResponse>(evaluationResponseDto);
            _evaluationService.CreateEvaluationResponse(evaluationResponse);

            return RedirectToAction("Index", "Evaluations");
        }

        private async Task UploadAnswersFile(ICollection<QuestionAnswerDto> questionAnswers)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");

            foreach (var answer in questionAnswers)
            {
                if ((answer.File != null) && (answer.File.Length > 0))
                {
                    _logger.LogInformation("--------------------- Starting File Upload");

                    using (var fileStream = new FileStream(Path.Combine(uploads, answer.File.FileName), FileMode.Create))
                    {
                        await answer.File.CopyToAsync(fileStream);
                    }
                }
            }
        }

        private async Task SendEmail(EvaluationAssignedDto evaluationAssignedDto)
        {
            // get the employee's email
            var employee = _userService.FindBy(u => u.Id == evaluationAssignedDto.EmployeeId).FirstOrDefault();
            var evaluationId = evaluationAssignedDto.EvaluationId;

            var link = $"http://localhost:63585/Evaluations/Reply/{evaluationId}?employeeId={employee.Id}";
           
            await _emailSender.SendEmailAsync(employee.Email, "Employees Evaluation",
                $"Hi {employee.Email}, <br /> we would like you to take a time to fill up our evaluation.<br /><br /> <a href='{link}' target='_blank'>Fill Up the Evaluation</a>");
            
        }


    }
}