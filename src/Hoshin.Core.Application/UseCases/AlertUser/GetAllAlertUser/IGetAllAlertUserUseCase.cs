using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.AlertUser.GetAllAlertUser
{
    public interface IGetAllAlertUserUseCase
    {
        Task<List<AlertUserOutput>> Execute(string userId);
    }
}
