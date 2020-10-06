using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.User.GetAllUser
{
    public interface IGetAllUserUseCase
    {
        Task<List<UserOutput>> Execute(int id_sector, int id_plant);
        List<UserOutput> Execute();
    }
}
