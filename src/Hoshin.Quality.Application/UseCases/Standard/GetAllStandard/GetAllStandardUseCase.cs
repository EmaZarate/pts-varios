using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Standard.GetAllStandard
{
    public class GetAllStandardUseCase : IGetAllStandardUseCase
    {
        private readonly IStandardRepository _standardRepository;
        private readonly IMapper _mapper;

        public GetAllStandardUseCase(IStandardRepository standardRepository,
            IMapper mapper)
        {
            _standardRepository = standardRepository;
            _mapper = mapper;
        }

        public List<StandardOutput> Execute()
        {
            return _mapper.Map<List<Domain.Standard.Standard>, List<StandardOutput>>(_standardRepository.GetAll());
        }

    }
}
