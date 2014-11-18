using System;
using System.Collections.Generic;

namespace HappyPortal.Lib.Data
{
    public interface IPollingStationRepository
    {
        IList<IPollingStationData> GetAll();
        IPollingStationData Add(string name);
        IPollingStationData Get(Guid id);
        IPollingStationData Update(IPollingStationData data);

    }
}