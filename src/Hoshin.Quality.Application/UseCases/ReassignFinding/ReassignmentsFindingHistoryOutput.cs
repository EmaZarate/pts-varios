using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Domain.Finding;

namespace Hoshin.Quality.Application.UseCases.ReassignFinding
{
    public class ReassignmentsFindingHistoryOutput
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string ReassignedUserID { get; set; }
        public int FindingID { get; set; }
        public string CreatedByUserID { get; set; }
        public string State { get; set; }
        public string CauseOfReject { get; set; }

    }
}
