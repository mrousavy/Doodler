﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DoodlerCore
{
    public class Report
    {
        public int Id { get; set; }

        public Poll ReportedPoll { get; set; }

        public string Description { get; set; }
    }
}
