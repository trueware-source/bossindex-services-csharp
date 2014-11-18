using System;
using System.Collections.Generic;

namespace HappyPortal.Lib.Data
{
    public interface IPollingStationData
    {
        Guid Id { get; set; }
        IList<Guid> QuestionIds { get; set; }
        string Name { get; set; }
    }
}