using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Repository.Repositories;
using Microsoft.Extensions.Logging;

namespace EmployeesEvaluation.Services.Impl
{
    public class EvaluationService : IEvaluationService
    {
        private IEvaluationRepository _evaluationRepository;
        private IQuestionRepository _questionRepository;
        private IEvaluationAssignedRepository _evaluationAssignedRepository;
        private IEvaluationQuestionRepository _evaluationQuestionRepository;
        private IEvaluationResponseRepository _evaluationResponseRepository;

        private readonly ILogger _logger;

        public EvaluationService(ILogger<EvaluationService> logger, IEvaluationResponseRepository evaluationResponseRepository, IEvaluationAssignedRepository evaluationAssignedRepository, IEvaluationRepository evaluationRepository, IQuestionRepository questionRepository, IEvaluationQuestionRepository evaluationQuestionRepository) 
        {
            this._logger = logger;
            this._evaluationRepository = evaluationRepository; 
            this._questionRepository = questionRepository;
            this._evaluationQuestionRepository = evaluationQuestionRepository;
            this._evaluationAssignedRepository = evaluationAssignedRepository;
            this._evaluationResponseRepository = evaluationResponseRepository;
        } 

        public IEnumerable<Evaluation> LoadAll()
        {
            return _evaluationRepository.LoadAll(); 
        }

        public IEnumerable<EvaluationResponse> GetEvaluationResponses()
        {
            return _evaluationResponseRepository.AllIncluding(er => er.Employee, e => e.Evaluation);
        }

        public IEnumerable<Evaluation> AllIncluding(params Expression<Func<Evaluation, object>>[] includeProperties)
        {
            IEnumerable<Evaluation> _evaluations = _evaluationRepository
                .AllIncluding(includeProperties)
                .ToList();

            return _evaluations;
        }

        public Evaluation GetSingleIncludingAll(Expression<Func<Evaluation, bool>> predicate)
        {
            return _evaluationRepository.GetSingleIncludingAll(predicate);
        }

        public IEnumerable<Evaluation> All() 
        { 
            return _evaluationRepository.GetAll(); 
        } 
 
        public Evaluation Get(int id) 
        { 
            return _evaluationRepository.GetSingle(id); 
        }

        public Evaluation GetSingleIncluding(Expression<Func<Evaluation, bool>> predicate, params Expression<Func<Evaluation, object>>[] includeProperties)
        {
            return _evaluationRepository.GetSingleIncluding(predicate, includeProperties);
        }

        public void Create(Evaluation evaluation)
        {

            _evaluationRepository.Add(evaluation); 
            
            foreach (var question in evaluation.Questions) 
            {
                _questionRepository.Add(question);

                EvaluationQuestion eq = new EvaluationQuestion()
                {
                    EvaluationId = evaluation.Id,
                    QuestionId = question.Id
                };

                _evaluationQuestionRepository.Add(eq);
            }

            _evaluationRepository.Commit();
        } 

        public void CreateWithExistingQuestions(Evaluation evaluation, List<int> questionIds)
        {
            // add evaluation
            _evaluationRepository.Add(evaluation);

            // add questions to evaluationquestion context
            BuildEvaluationQuestion(evaluation.Id, questionIds);

            _evaluationRepository.Commit();
        }
        
        public void UpdateWithExistingQuestions(Evaluation evaluation, List<int> questionIds)
        {
            // update the evaluation
            _evaluationRepository.Update(evaluation);

            // add questions to evaluationquestion context
            BuildEvaluationQuestion(evaluation.Id, questionIds);

            // remove the older questions in EvaluationQuestion
            _evaluationQuestionRepository.DeleteWhere(eq => eq.EvaluationId == evaluation.Id && !questionIds.Contains(eq.QuestionId));

            _evaluationRepository.Commit();
        }

        public void Update(Evaluation evaluation) 
        { 
            _evaluationRepository.Update(evaluation); 
            _evaluationRepository.Commit();
        } 
 
        public void Delete(int id) 
        {             
            Evaluation evaluation = Get(id); 
            _evaluationRepository.Delete(evaluation); 
            _evaluationRepository.Commit(); 
        }

        public void CreateEvaluationResponse(EvaluationResponse evaluationResponse)
        {
            _evaluationResponseRepository.Add(evaluationResponse);
            _evaluationResponseRepository.Commit();
        }

        public void AssignEvaluationEmployee(EvaluationAssigned evaluationAssigned)
        {
            _evaluationAssignedRepository.Add(evaluationAssigned);
            _evaluationAssignedRepository.Commit();
        }

        public Evaluation GetEvaluationAssigned(int evaluationId, string employeeId)
        {
            EvaluationAssigned evaluationAssigned = _evaluationAssignedRepository.FindBy(ea => ea.EvaluationId == evaluationId && ea.EmployeeId == employeeId).FirstOrDefault();

            Evaluation evaluation = null;
            
            // if there is an evaluation assigned, return the evaluation
            if (evaluationAssigned != null)
            {
                evaluation = _evaluationRepository.GetSingleIncludingAll(e => e.Id == evaluationId);
            }

            return evaluation;
        }

        private void BuildEvaluationQuestion(int evaluationId, List<int> questionIds)
        {
            // foreach existing question, add a new evaluationQuestion
            foreach (int questionId in questionIds)
            {
                // verify existing relations in EvaluationQuestion
                EvaluationQuestion existQuestion =_evaluationQuestionRepository.FindBy(ex => ex.EvaluationId == evaluationId && ex.QuestionId == questionId).FirstOrDefault();

                // Add EvaluationQuestion relation if there is no previous relation between this evaluation and the questions 
                if (existQuestion == null)
                {
                    EvaluationQuestion eq = new EvaluationQuestion()
                    {
                        EvaluationId = evaluationId,
                        QuestionId = questionId
                    };

                    _evaluationQuestionRepository.Add(eq);
                }
            }
        }
    }
}