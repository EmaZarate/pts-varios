using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.Finding
{
    public class FindingComment
    {
        public int FindingCommentID { get; set; }
        public DateTime Date { get; set; }
        public string Comment { get; set; }
        public string CreatedByUserID { get; set; }
    }
}
