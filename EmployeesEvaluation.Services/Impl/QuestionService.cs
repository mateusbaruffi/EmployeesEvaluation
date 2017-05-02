using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Repository.Repositories;

namespace EmployeesEvaluation.Services.Impl
{
    public class QuestionService : IQuestionService
    {
        private IQuestionRepository _questionRepository;
 
        public QuestionService(IQuestionRepository questionRepository) 
        { 
            this._questionRepository = questionRepository; 
        } 

        public IEnumerable<Question> AllIncluding(params Expression<Func<Question, object>>[] includeProperties)
        {
            IEnumerable<Question> _questions = _questionRepository
                .AllIncluding(includeProperties)
                .ToList();

            return _questions;
        }

        public IEnumerable<Question> All() 
        { 
            return _questionRepository.GetAll(); 
        } 
 
        public Question Get(int id) 
        { 
            return _questionRepository.GetSingle(id); 
        } 

        public Question GetSingleIncluding(Expression<Func<Question, bool>> predicate, params Expression<Func<Question, object>>[] includeProperties)
        {
            return _questionRepository.GetSingleIncluding(predicate, includeProperties);
        }


        public void Create(Question question) 
        {
            _questionRepository.Add(question); 
            _questionRepository.Commit();
        } 
        
        public void Update(Question question) 
        { 
            _questionRepository.Update(question); 
            _questionRepository.Commit();
        } 
 
        public void Delete(int id) 
        {             
            Question question = Get(id); 
            _questionRepository.Delete(question); 
            _questionRepository.Commit(); 
        } 
    }
}