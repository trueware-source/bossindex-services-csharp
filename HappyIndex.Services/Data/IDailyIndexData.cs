using System;
using System.Collections.Generic;

namespace HappyPortal.Lib.Data
{
    public interface IDailyIndexData
    {
        int Index { get; set; }
        DateTime ReportDate { get; set; }
        int Count { get; set; }
    }
}