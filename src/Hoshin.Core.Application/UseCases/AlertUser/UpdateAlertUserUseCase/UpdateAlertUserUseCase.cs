using AutoMapper;
using Hoshin.Core.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.AlertUser.UpdateAlertUserUseCase
{
    public class UpdateAlertUserUseCase : IUpdateAlertUserUseCase
    {
        private readonly IAlertUserRepository _alertUserRepository;
        private readonly IMapper _mapper;

        public UpdateAlertUserUseCase(IAlertUserRepository alertUserRepository)
        {
            _alertUserRepository = alertUserRepository;
        }

        public bool Execute(Dictionary<string, List<Domain.AlertUser.AlertUser>> dicAlertUser)
        {
            return _alertUserRepository.InsertOrUpdate(dicAlertUser);
        }
    }
}
;