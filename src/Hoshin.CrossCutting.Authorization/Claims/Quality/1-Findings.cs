using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class Findings : IClaim
    {
        //Used to know what project belongs
        public string Quality { get; set; }

        public const string Add = "findings.add";
        public const string UpdateApproved = "findings.update.approved";
        public const string Approve = "findings.approve";
        public const string Reject = "findings.reject";
        public const string Reassign = "findings.reassign";
        public const string ReassginDirectly = "findings.reassign.direct";
        public const string RequestReassign = "findings.request.reassign";
        public const string Close = "findings.close";
        public const string Read = "findings.read";
        public const string ReadSector = "findings.read.sector";
        public const string ApproveReassignment = "findings.approve.reassignment";
        public const string RejectReassignment = "findings.reject.reassignment";
        public const string EditExpirationDate = "findings.edit.expirationdate";
    }
}
