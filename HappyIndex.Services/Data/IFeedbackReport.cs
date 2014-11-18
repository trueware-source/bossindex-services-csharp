using System.Collections.Generic;
using HappyPortal.Lib.Data.MongoDb;

namespace HappyPortal.Lib.Data
{
    public interface IFeedbackReport
    {
        int HappinessIndex { get; set; }
        int FeedbackCount { get; set; }
        IList<IDailyIndexData> DailyIndexes { get; set; }
        int HappinessIndexToday { get; set; }
        IList<IReasonData> UnhappyReasons { get; set; }
        IList<IReasonData> HappyReasons { get; set; }
    }
}
