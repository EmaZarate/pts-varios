using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.CorrectiveActionState
{
    public class CorrectiveActionState
    {
        public int CorrectiveActionStateID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public bool Active { get; set; }
    }
}
