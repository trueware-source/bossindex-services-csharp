using HappyPortal.Lib.Data;
using System.Collections.Generic;

namespace HappyPortal.Models
{
    public class CompanyFeedbackReport : ICompanyFeedbackReport
    {
        public CompanyFeedbackReport()
        {
            DailyIndexes = new List<IDailyIndexData>();
            UnhappyReasons = new List<IReasonData>();
        }

        public int HappinessIndex { get; set; }
        public int HappinessIndexToday { get; set; }
        public int FeedbackCount { get; set; }
        public IList<IDailyIndexData> DailyIndexes { get; set; }
        public IList<IReasonData> UnhappyReasons { get; set; }
        public IList<IReasonData> HappyReasons { get; set; }
    }
}