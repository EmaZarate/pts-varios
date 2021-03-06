﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.Aspect
{
    public class Aspect
    {
        public int AspectID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public int StandardID { get; set; }
        public bool Active { get; set; }
    }
}
