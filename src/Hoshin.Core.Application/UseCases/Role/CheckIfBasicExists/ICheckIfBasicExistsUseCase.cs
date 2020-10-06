using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Role.CheckIfBasicExists
{
    public interface ICheckIfBasicExistsUseCase
    {
        Task<bool> Execute();
    }
}
