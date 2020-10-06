using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class AuditType : IClaim
    {
        public string Quality { get; set; }

        public const string AddAuditType = "audittype.add";
        public const string EditAuditType = "audittype.edit";
        public const string ReadAuditType = "audittype.read";
        public const string ActivateAuditType = "audittype.activate";
        public const string DeactivateAuditType = "audittype.deactivate";
    }
}
