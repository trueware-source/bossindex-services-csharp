
using System;

namespace HappyPortal.Lib.Data
{
    public interface IReasonData
    {
        Guid QuestionId { get; set; }
        string QuestionText { get; set; }
        int Count { get; set; }
    }
}