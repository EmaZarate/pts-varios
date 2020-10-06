using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.AlertUser.UpdateAlertUserUseCase
{
    public interface IUpdateAlertUserUseCase
    {
        bool Execute(Dictionary<string, List<Domain.AlertUser.AlertUser>> dicAlertUser);
    }
}
