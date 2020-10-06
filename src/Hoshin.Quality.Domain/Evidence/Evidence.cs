using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Domain.Evidence
{
    public class Evidence
    {
        public int EvidenceID { get; set; }
        public string Base64 { get; set; }
        public byte[] Bytes { get; set; }
        public string FileName { get; set; }
        public bool IsInsert { get; set; }
        public bool IsDelete { get; set; }
        public string Url { get; set; }
    }
}