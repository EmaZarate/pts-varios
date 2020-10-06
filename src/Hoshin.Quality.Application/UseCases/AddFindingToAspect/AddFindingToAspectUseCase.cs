using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.Finding;
using Microsoft.AspNetCore.Http;

namespace Hoshin.Quality.Application.UseCases.AddFindingToAspect
{
    public class AddFindingToAspectUseCase : IAddFindingToAspectUseCase
    {
        private readonly IAuditStandardAspectRepository _auditStandardAspectRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AddFindingToAspectUseCase(IAuditStandardAspectRepository auditStandardAspectRepository, IHttpContextAccessor httpContextAccessor)
        {
            _auditStandardAspectRepository = auditStandardAspectRepository;
            _httpContextAccessor = httpContextAccessor;
        }
        public bool Execute(Domain.Finding.Finding finding)
        {
            finding.EmitterUserID = _httpContextAccessor.HttpContext.User.FindFirst("id").Value;
            return _auditStandardAspectRepository.AddFinding(finding);
        }
    }
}
