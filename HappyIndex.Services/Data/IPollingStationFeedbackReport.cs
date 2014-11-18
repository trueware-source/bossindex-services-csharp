using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappyPortal.Lib.Data
{
    public interface IPollingStationFeedbackReport : IFeedbackReport
    {
        string PollingStation { get; set; }
    }
}
