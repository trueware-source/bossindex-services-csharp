using System;
using System.Collections.Generic;

namespace HappyPortal.Lib.Data
{
    public interface IFeedbackRepository
    {
        IFeedbackData Add(int indicator, Guid questionId, DateTime createDate, string pollingStation, IList<Guid> questionsShownIds);
        IFeedbackData GetById(Guid id);
        bool DeleteById(Guid id);
        IList<IFeedbackData> GetAll();
        IList<IFeedbackData> FindByDateRange(DateTime startDate, DateTime endDate);
        int GetIndexByDateRange(DateTime startDate, DateTime endDate);
        List<IReasonData> GetUnhappyReasons(DateTime startDate, DateTime endDate);
        List<IReasonData> GetUnhappyReasonsByPollingStation(string name, DateTime startDate, DateTime endDate);
        List<IReasonData> GetHappyReasons(DateTime startDate, DateTime endDate);
        List<IReasonData> GetHappyReasonsByPollingStation(string name, DateTime startDate, DateTime endDate);
        IPollingStationFeedbackReport GetPollingStationFeedbackReport(DateTime startDate, DateTime endDate, string pollingStation);
        ICompanyFeedbackReport GetCompanyFeedbackReport(DateTime startDate, DateTime endDate);
    }
}