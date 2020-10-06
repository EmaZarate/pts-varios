using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.Alert.GetAllAlert
{
    public class AlertOutput
    {
        public int AlertID { get; set; }
        public string Description { get; set; }
        public string AlertType { get; set; }
    }
}
