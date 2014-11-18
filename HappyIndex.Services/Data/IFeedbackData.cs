using System;
using System.Collections.Generic;

namespace HappyPortal.Lib.Data
{
    public interface IFeedbackData
    {
        Guid Id { get; set; }
        int Indicator { get; set; }
        Guid QuestionId { get; set; }
        IList<Guid> QuestionsShownIds { get; set; }
        string PollingStation { get; set; }
        DateTime CreateDate { get; set; }
    }
}