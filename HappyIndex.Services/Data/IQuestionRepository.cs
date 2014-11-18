using System;
using System.Collections.Generic;

namespace HappyPortal.Lib.Data
{
    public interface IQuestionRepository
    {
        IQuestionData Add(string text, string category);
        IList<IQuestionData> GetByCategory(string category);
        IList<IQuestionData> GetAllQuestions();
        IQuestionData Get(Guid id);

    }
}