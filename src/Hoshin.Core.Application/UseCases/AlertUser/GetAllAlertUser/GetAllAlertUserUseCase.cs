using AutoMapper;
using Hoshin.Core.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.AlertUser.GetAllAlertUser
{
    public class GetAllAlertUserUseCase : IGetAllAlertUserUseCase
    {

        private readonly IAlertUserRepository _alertUserRepository;
        private readonly IMapper _mapper;

        public GetAllAlertUserUseCase(IAlertUserRepository alertUserRepository, IMapper mapper)
        {
            _alertUserRepository = alertUserRepository;
            _mapper = mapper;
        }

        public async Task<List<AlertUserOutput>> Execute(string userId)
        {
            return _mapper.Map<List<Domain.AlertUser.AlertUser>, List<AlertUserOutput>>(await _alertUserRepository.GetAllAlertByUser(userId));
        }

    }
}
