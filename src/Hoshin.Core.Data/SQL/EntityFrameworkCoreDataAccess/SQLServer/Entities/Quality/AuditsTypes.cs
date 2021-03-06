﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality
{
    public class AuditsTypes
    {
        public int AuditTypeID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public ICollection<Audits> Audits { get; set; }
    }
}
