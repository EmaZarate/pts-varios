using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.WorkflowCore.Finding.Data
{
    class FindingsState
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public bool Active { get; set; }
    }
}
