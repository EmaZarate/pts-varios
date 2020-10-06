using Hoshin.Quality.Application.Repositories;

namespace Hoshin.Quality.Application.UseCases.Finding.GetCountFinding
{
    public class GetCountFindingUseCase : IGetCountFindingUseCase
    {
        private readonly IFindingRepository _findingRepository;

        public GetCountFindingUseCase(IFindingRepository findingRepository)
        {
            _findingRepository = findingRepository;
        }

        public int Execute()
        {
            return _findingRepository.GetCount();
        }
    }
}
