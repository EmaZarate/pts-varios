using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.Standard;

namespace Hoshin.Quality.Application.UseCases.Standard.CreateStandard
{
    public class CreateStandardUseCase : ICreateStandardUseCase
    {
        private readonly IStandardRepository _standardRepository;
        private readonly IMapper _mapper;


        public CreateStandardUseCase(IStandardRepository standardRepository, IMapper mapper)
        {
            _standardRepository = standardRepository;
            _mapper = mapper;
        }


        public string Execute(Domain.Standard.Standard standard)
        {

            string response = _standardRepository.Add(standard);

            switch (response)
            {
                case "NameExist":
                    throw new DuplicateEntityException(standard.Name, "Ya existe una norma con este nombre o código", 436);
                case "OneActive":
                    throw new DuplicateEntityException(standard.Name, "Debe existir al menos un aspecto activo.", 436);
                default:
                    return "Ok";
            }
        }
    }
}
