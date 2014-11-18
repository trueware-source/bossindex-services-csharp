using System;
using System.Collections.Generic;

namespace HappyPortal.Models
{
    public class PollingStation
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<Guid> QuestionIds { get; set; }
    }
}