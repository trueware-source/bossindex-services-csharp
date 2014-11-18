
using System;

namespace HappyPortal.Lib.Data.MongoDb
{
    public class UnhappyData : IReasonData
    {
        public Guid QuestionId { get; set; }
        public int Count { get; set; }
        public string QuestionText { get; set; }
    }
}