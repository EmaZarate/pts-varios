using AutoMapper;
using Hoshin.Core.Application.Exceptions.Job;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Job;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.UseCases.CRUDJob.UpdateJobUseCase
{
    public class UpdateJobUseCase : IUpdateJobUseCase
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;

        public UpdateJobUseCase(IMapper mapper, IJobRepository jobRepository)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
        }

        public JobOutput Execute(Job job)
        {
            var j = _jobRepository.CheckDuplicated(job);
            if(j == null)
            {
                return _mapper.Map<Job, JobOutput>(_jobRepository.Update(job));
            }
            else
            {
                throw new JobWithThisNameAndOrCodeAlreadyExists(job.Name, job.Code, "Ya existe un puesto con este nombre y/o con este código");
            }
        }
    }
}
