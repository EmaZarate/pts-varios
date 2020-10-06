using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class AuditStates: IClaim
    {
        public string Quality { get; set; }

        public const string AddAuditState = "auditstate.add";
        public const string EditAuditState = "auditstate.edit";
        public const string ReadAuditState = "auditstate.read";
        public const string ActivateAuditState = "auditstate.activate";
        public const string DeactivateAuditState = "auditstate.deactivate";
    }
}
