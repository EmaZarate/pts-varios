using Hoshin.Quality.Application.Repositories;
using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;

namespace Hoshin.Quality.Application.UseCases.Finding.GetOneFinding
{
    public class GetOneFindingUseCase : IGetOneFindingUseCase
    {
        private readonly IFindingRepository _findingRepository;
        private readonly IMapper _mapper;

        public GetOneFindingUseCase(IFindingRepository findingRepository, IMapper mapper)
        {
            _findingRepository = findingRepository;
            _mapper = mapper;
        }

        public FindingOutput Execute(int id)
        {
            var finding = _findingRepository.Get(id);
            
            var x = _mapper.Map<Domain.Finding.Finding, FindingOutput>(finding);
            if (finding != null) return x;
            throw new EntityNotFoundException(id, "No se encontró un hallazgo con ese ID");
        }
    }
}
