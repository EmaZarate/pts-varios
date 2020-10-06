using AutoMapper;
using Hoshin.Core.Application.Repositories;
using Hoshin.Quality.Application.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hoshin.CrossCutting.Authorization.Claims.Quality;
using Hoshin.CrossCutting.Authorization.Claims;

namespace Hoshin.Quality.Application.UseCases.Audit.GetAllAudit
{
    public class GetAllAuditUseCase : IGetAllAuditUseCase
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GetAllAuditUseCase(
            IAuditRepository auditRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IUserRepository userRepository)
        {
            _auditRepository = auditRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }

        public List<AuditOutput> Execute()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;

            var userJobId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst("job").Value);

            List<Domain.Audit.Audit> audits = new List<Domain.Audit.Audit>();

            if (_httpContextAccessor.HttpContext.User.HasClaim(CustomClaimTypes.Permission, CrossCutting.Authorization.Claims.Quality.Audits.ApporveReport))
            {
               audits = _auditRepository.GetAll();
            }
            else if (_httpContextAccessor.HttpContext.User.HasClaim(CustomClaimTypes.Permission, CrossCutting.Authorization.Claims.Quality.Audits.Planning))
            {
                audits = _auditRepository.GetAllForAuditor(userId);
            }
            else
            {
                audits = _auditRepository.GetAllForColaboratorOrSectorBoss(userJobId);
            }

            var auditsOutput = _mapper.Map<List<Domain.Audit.Audit>, List<AuditOutput>>(audits);
            
            return auditsOutput;
        }
    }
}
