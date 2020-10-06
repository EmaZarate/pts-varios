using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities
{
    public class Users : IdentityUser
    {
        public int JobID { get; set; }
        public int SectorID { get; set; }
        public int PlantID { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string Status { get; set; }
        public string MicrosoftGraphId { get; set; }
        public byte[] ImageProfile { get; set; }
        public string base64Profile { get; set; }
        public string Address { get; set; }
        public bool Active { get; set; }
        public JobsSectorsPlants JobSectorPlant { get; set; }
        public ICollection<Findings> FindingResponsibleUser { get; set; }
        public ICollection<Findings> FindingEmitterUser { get; set; }
        public virtual ICollection<UserClaim> Claims { get; set; }
        public virtual ICollection<UserLogin> Logins { get; set; }
        public virtual ICollection<UserToken> Tokens { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<AlertUsers> AlertUsers { get; set; }
        public ICollection<UserCorrectiveAction> UserCorrectiveActions { get; set; }
    }
}
