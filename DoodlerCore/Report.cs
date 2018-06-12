using System;
using System.Collections.Generic;
using System.Text;

namespace DoodlerCore
{
    public class Report
    {
        public int Id { get; set; }

        public Poll ReportedPoll { get; set; }

        public string Description { get; set; }

        public DateTime CreatedAt { get; set; }

        public Report(Poll reportedPoll, string description)
        {
            ReportedPoll = reportedPoll;
            Description = description;
            CreatedAt = DateTime.Now;
        }
    }
}
