using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Alert.GetAllAlert
{
    public interface IGetAllAlertUseCase
    {
        Task<Dictionary<string, List<AlertOutput>>> Execute();
    }
}
