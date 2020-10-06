using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.TaskEvidence
{
    public class TaskEvidence
    {
        /*   public string Name { get; set; }
           public int TaskEvidenceID { get; set;}
           public int TaskID { get; set; }
         //  public string Base64 { get; set; }
         //  public byte[] Bytes { get; set; }
           public string FileName { get; set; }
           
           public string Url { get; set; }*/
        //public bool IsInsert { get; set; }
        //public bool IsDelete { get; set; }
        public int TaskEvidencesID { get; set; }
        public int TaskID { get; set; }
        //public Task.Task Task { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
