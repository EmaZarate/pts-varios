using Hoshin.Quality.Application.UseCases.Standard;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.Audit
{
    public class AuditStandardOutput
    {

        public int StandardID { get; set; }
        public StandardOutput Standard { get; set; }
        public List<AuditStandardAspectOutput> AuditStandardAspects { get; set; }
    }
}
