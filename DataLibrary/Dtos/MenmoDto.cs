﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Dtos
{
    public class MenmoDto:BaseDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
    }
}
