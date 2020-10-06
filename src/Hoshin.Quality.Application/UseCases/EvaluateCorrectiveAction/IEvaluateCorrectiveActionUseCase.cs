using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Hoshin.Quality.Application.UseCases.EvaluateCorrectiveAction
{
    public interface IEvaluateCorrectiveActionUseCase
    {
        Task Execute(Domain.CorrectiveAction.CorrectiveAction correctiveAction, IFormFile[] Evidences);
    }
}
