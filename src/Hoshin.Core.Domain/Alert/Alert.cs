using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Domain.Alert
{
    public class Alert
    {
        public int AlertID { get; set; }
        public string Description { get; set; }
        public string AlertType { get; set; }
    }
}
