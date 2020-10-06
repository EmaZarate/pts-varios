using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.CorrectiveActionEvidence
{
    public class CorrectiveActionEvidence
    {
        public int CorrectiveActionEvidenceID { get; set; }
        public int CorrectiveActionID { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
