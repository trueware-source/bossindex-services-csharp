using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HappyPortal.Lib.Data;

namespace HappyPortal.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        //public bool Enabled { get; set; }
        public string Category { get; set; }
    }
}