﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Maker
    {
        public static int nextMakerId = 1;
        public int MakerId { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }

    }
}