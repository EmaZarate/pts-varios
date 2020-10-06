using Hoshin.Core.Domain.Alert;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.Repositories
{
    public interface IAlertRepository
    {
        Task<List<Alert>> GetAll();
    }
}
