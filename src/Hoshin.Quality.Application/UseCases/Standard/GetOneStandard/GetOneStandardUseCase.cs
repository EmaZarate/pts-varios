using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Standard.GetOneStandard
{
    public class GetOneStandardUseCase : IGetOneStandardUseCase
    {
        private IStandardRepository _standardRepository;
        private readonly IMapper _mapper;

        public StandardOutput Execute(int id)
        {
            return _mapper.Map<Domain.Standard.Standard, StandardOutput>(_standardRepository.Get(id));
        }

        public GetOneStandardUseCase(IStandardRepository standardRepository,
        IMapper mapper)
        {
            _standardRepository = standardRepository;
            _mapper = mapper;
        }

    }
}
