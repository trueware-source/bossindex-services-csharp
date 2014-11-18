using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HappyPortal.Lib.Data.MongoDb
{
    public class DailyIndexData : IDailyIndexData
    {
        public int Index { get; set; }
        public DateTime ReportDate { get; set; }
        public int Count { get; set; }
    }
}