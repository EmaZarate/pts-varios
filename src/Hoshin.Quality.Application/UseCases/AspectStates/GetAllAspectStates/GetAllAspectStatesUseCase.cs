using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.AspectStates;

namespace Hoshin.Quality.Application.UseCases.AspectStates.GetAllAspectStates
{
    public class GetAllAspectStatesUseCase : IGetAllAspectStatesUseCase
    {
        private readonly IAspectStatesRepository _aspectStatesRepository;
        private readonly IMapper _mapper;

        public GetAllAspectStatesUseCase(IAspectStatesRepository aspectStatesRepository, IMapper mapper)
        {
            _aspectStatesRepository = aspectStatesRepository;
            _mapper = mapper;
        }
        public List<AspectStatesOutput> Execute()
        {
            var listAspectStatesOutput = new List<AspectStatesOutput>();
            var listAspectStatesDomain = _aspectStatesRepository.GetAll();
            return _mapper.Map<List<Hoshin.Quality.Domain.AspectStates.AspectStates>, List<AspectStatesOutput>>(listAspectStatesDomain);
            //foreach(var aspectState in listAspectStatesDomain)
            //{
            //    var aspectStatesOutput = _mapper.Map<Domain.AspectStates.AspectStates, AspectStatesOutput>(aspectState);
            //    listAspectStatesOutput.Add(aspectStatesOutput);
            //}
            //return listAspectStatesOutput;
        }
    }
}
