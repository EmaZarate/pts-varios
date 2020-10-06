using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.ReassignmentsFindingHistory
{
    public class ReassignmentsFindingHistory
    {
        public int FindingReassignmentHistoryID { get; set; }
        public DateTime Date { get; set; }
        public string ReassignedUserID { get; set; }
        public int FindingID { get; set; }
        public string CreatedByUserID { get; set; }
        public Finding.Finding Finding { get; set; }
        public string State { get; set; }
        public string CauseOfReject { get; set; }

        public ReassignmentsFindingHistory(int findingID, string reassignedUserId, string createByUserId, string state)
        {
            this.FindingID = findingID;
            this.ReassignedUserID = reassignedUserId;
            this.CreatedByUserID = createByUserId;
            this.State = state;
        }
        public ReassignmentsFindingHistory()
        {

        }
    }
}
