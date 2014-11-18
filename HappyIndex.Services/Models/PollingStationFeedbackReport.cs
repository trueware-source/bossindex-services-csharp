
using HappyPortal.Lib.Data;
using System.Collections.Generic;

namespace HappyPortal.Models
{
    public class PollingStationFeedbackReport : IPollingStationFeedbackReport
    {
        public int HappinessIndex { get; set; }
        public int HappinessIndexToday { get; set; }
        public int FeedbackCount { get; set; }
        public string PollingStation{ get; set; }
        public IList<IDailyIndexData> DailyIndexes { get; set; }
        public IList<IReasonData> UnhappyReasons { get; set; }
        public IList<IReasonData> HappyReasons { get; set; }
    }
}