using AutoMapper;
using Hoshin.Core.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.Alert.GetAllAlert
{
    public class GetAllAlertUseCase : IGetAllAlertUseCase
    {
        private readonly IAlertRepository _alertRepository;
        private readonly IMapper _mapper;

        public GetAllAlertUseCase(IAlertRepository alertRepository, IMapper mapper)
        {
            _alertRepository = alertRepository;
            _mapper = mapper;
        }

        public async Task<Dictionary<string, List<AlertOutput>>> Execute()
        {
            Dictionary<string, List<AlertOutput>> dicAlert = new Dictionary<string, List<AlertOutput>>();
            List<AlertOutput> alert = _mapper.Map<List<Domain.Alert.Alert>, List<AlertOutput>>(await _alertRepository.GetAll());

            List<string> listAlertType = alert.AsEnumerable().Select(x => x.AlertType).ToList();

            foreach (string alertType in listAlertType)
            {
                if (!dicAlert.ContainsKey(alertType))
                {
                    dicAlert.Add(alertType, alert.AsEnumerable().Where(x => x.AlertType == alertType).Select(x => x).ToList());
                }
            }

            return dicAlert;
        }
    }
}

