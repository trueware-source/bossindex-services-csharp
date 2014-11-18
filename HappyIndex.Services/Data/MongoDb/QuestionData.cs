using System;

namespace HappyPortal.Lib.Data.MongoDb
{
    public class QuestionData : IQuestionData
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Category { get; set; }
    }
}