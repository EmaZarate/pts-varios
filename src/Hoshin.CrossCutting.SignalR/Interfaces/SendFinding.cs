using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.CrossCutting.SignalR.Interfaces
{
    public interface SendFinding
    {
        Task SendFinding(string data);
    }
}
