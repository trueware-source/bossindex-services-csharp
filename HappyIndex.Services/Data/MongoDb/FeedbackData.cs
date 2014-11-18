using System;
using System.Collections.Generic;

namespace HappyPortal.Lib.Data.MongoDb
{
    public class FeedbackData : IFeedbackData
    {
        public Guid Id { get; set; }
        public int Indicator { get; set; }
        public Guid QuestionId { get; set; }
        public IList<Guid> QuestionsShownIds { get; set; }
        public DateTime CreateDate { get; set; }
        public string PollingStation { get; set; }
    }
}