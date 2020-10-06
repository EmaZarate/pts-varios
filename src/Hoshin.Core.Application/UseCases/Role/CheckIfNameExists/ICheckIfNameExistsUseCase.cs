using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Role.CheckIfNameExists
{
    public interface ICheckIfNameExistsUseCase
    {
        Task<bool> Execute(string name);
    }
}
