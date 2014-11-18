using System;

namespace HappyPortal.Lib.Data
{
    public interface IQuestionData
    {
        string Text { get; set; }
        Guid Id { get; set; }
        string Category { get; set; }
    }
}