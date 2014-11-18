using System;
using System.Collections.Generic;

namespace HappyPortal.Lib.Data.MongoDb

{
    public class PollingStationData : IPollingStationData
    {
        public string Name { get; set; }
        public Guid Id { get; set; }

        IList<Guid> _questionIds = new List<Guid>();

        public IList<Guid> QuestionIds 
        {
            get { return _questionIds; }
            set { _questionIds = value; }
        }
    }
}