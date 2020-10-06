using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Job;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDJob.GetOneJobUseCase
{
    public class GetOneJobUseCase : IGetOneJobUseCase
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;

        public GetOneJobUseCase(IMapper mapper, IJobRepository jobRepository)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
        }

        public JobOutput Execute(int id)
        {
            return _mapper.Map<Job, JobOutput>(_jobRepository.GetOne(id));
        }
    }
}
