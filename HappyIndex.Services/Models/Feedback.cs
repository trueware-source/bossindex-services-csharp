using System;
using System.Collections.Generic;

namespace HappyPortal.Models
{
    public class Feedback
    {
        public Guid Id { get; set; }
        public int Indicator { get; set; }
        public Guid QuestionId { get; set; }
        public IList<Guid> QuestionsShownIds { get; set; }
        public string PollingStation { get; set; }
        public DateTimeOffset CreateDate { get; set; }
    }
}