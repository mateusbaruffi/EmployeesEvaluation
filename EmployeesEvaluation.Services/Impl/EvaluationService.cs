using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Repository.Repositories;

namespace EmployeesEvaluation.Services.Impl
{
    public class EvaluationService : IEvaluationService
    {
        private IEvaluationRepository _evaluationRepository;
        private IQuestionRepository _questionRepository;

        private IEvaluationQuestionRepository _evaluationQuestionRepository;
 
        public EvaluationService(IEvaluationRepository evaluationRepository, IQuestionRepository questionRepository, IEvaluationQuestionRepository evaluationQuestionRepository) 
        { 
            this._evaluationRepository = evaluationRepository; 
            this._questionRepository = questionRepository;
            this._evaluationQuestionRepository = evaluationQuestionRepository; 
        } 

        public IEnumerable<Evaluation> LoadAll()
        {
            return _evaluationRepository.LoadAll(); 
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

            // foreach existing question, add a new evaluationQuestion
            foreach (int questionId in questionIds)
            {
                EvaluationQuestion eq = new EvaluationQuestion()
                {
                    EvaluationId = evaluation.Id,
                    QuestionId = questionId
                };

                _evaluationQuestionRepository.Add(eq);
            }

            _evaluationRepository.Commit();
        }
        
        public void UpdateWithExistingQuestions(Evaluation evaluation, List<int> questionIds)
        {
            // update the evaluation
            _evaluationRepository.Update(evaluation);

            // add questions to evaluationquestion context
            BuildEvaluationQuestion(evaluation.Id, questionIds);

            // remove the older questions in EvaluationQuestion
            _evaluationQuestionRepository.DeleteWhere(eq => eq.EvaluationId == evaluation.Id);

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

        private void BuildEvaluationQuestion(int evaluationId, List<int> questionIds)
        {
            // foreach existing question, add a new evaluationQuestion
            foreach (int questionId in questionIds)
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