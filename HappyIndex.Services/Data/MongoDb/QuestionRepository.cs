using System;
using System.Collections.Generic;
using System.Linq;
using HappyPortal.Models;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace HappyPortal.Lib.Data.MongoDb
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly MongoDatabase _database;
        private readonly MongoCollection _questionCollection;

        public QuestionRepository(string connectionString)
        {
            _database = new MongoClient(connectionString).GetServer().GetDatabase("happyindex");
            _questionCollection = _database.GetCollection<QuestionData>("question");
        }

        public IQuestionData Add(string text, string category)
        {
            var question = new QuestionData()
            {
                Id = Guid.NewGuid(),
                Text = text,
                Category = category
            };
            _questionCollection.Insert(question);
            return question;
        }

        public IList<IQuestionData> GetByCategory(string category)
        {
            var result = new List<IQuestionData>();
            var query = Query<QuestionData>.EQ(q => q.Category, category);
            foreach (var questionData in _questionCollection.FindAs<QuestionData>(query))
            {
                result.Add(new QuestionData(){Category = questionData.Category, Id = questionData.Id, Text = questionData.Text});
            }
            return result;
        }

        public IQuestionData Get(Guid id)
        {
            var query = Query<QuestionData>.EQ(q => q.Id, id);
            return _questionCollection.FindOneAs<QuestionData>(query);
        }

        public IList<IQuestionData> GetAllQuestions()
        {
            var cursor = _questionCollection.FindAllAs<QuestionData>();
            var questions = new List<IQuestionData>();
            foreach (var question in cursor)
            {
                questions.Add(new QuestionData() { 
                    Category = question.Category,
                    Id = question.Id,
                    Text = question.Text
                });
            }
            return questions;
        }
    }
}