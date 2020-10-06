using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.CrossCutting.Authorization.Claims.Quality
{
    public class Audits: IClaim
    {
        public string Quality { get; set; }

        public const string ReedAudit = "audit.read";
        public const string ReedAuditCalendar = "audit.readcalendar";
        public const string Schedule = "audit.schedule";
        public const string Reschedule = "audit.reschedule";
        public const string Planning = "audit.planning";
        public const string ApporvePlanning = "audit.approve.planning";
        public const string RejectPlanning = "audit.reject.planning";
        public const string EmmitReport = "audit.emmit.report";
        public const string ApporveReport = "audit.approve.report";
        public const string RejectReport = "audit.reject.report";
        public const string EditRejectedReport = "audit.edit.rejectedreport";
        public const string Delete = "audit.delete";
        public const string Export = "audit.export";
        public const string AddFindings = "audit.addfindings";

    }
}
