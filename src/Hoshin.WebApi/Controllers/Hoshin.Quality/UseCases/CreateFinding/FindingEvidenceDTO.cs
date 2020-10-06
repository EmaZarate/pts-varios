using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hoshin.WebApi.Controllers.Hoshin.Quality.UseCases.CreateFinding
{
    public class FindingEvidenceDTO
    {
        public string Base64 { get; set; }
        public string FileName { get; set; }
        public bool IsInsert { get; set; }
        public bool IsDelete { get; set; }
        public string Url { get; set; }
    }
}
