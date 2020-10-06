using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Core.Domain.Job;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoshin.Core.Application.UseCases.CRUDJob.GetAllJobsUseCase
{
    public class GetAllJobsUseCase : IGetAllJobsUseCase
    {
        private readonly IMapper _mapper;
        private readonly IJobRepository _jobRepository;

        public GetAllJobsUseCase(IMapper mapper, IJobRepository jobRepository)
        {
            _mapper = mapper;
            _jobRepository = jobRepository;
        }

        public async Task<List<JobOutput>> Execute()
        {
            var list = await _jobRepository.GetAll();
            return _mapper.Map<List<Job>, List<JobOutput>>(list);
        }
    }
}
