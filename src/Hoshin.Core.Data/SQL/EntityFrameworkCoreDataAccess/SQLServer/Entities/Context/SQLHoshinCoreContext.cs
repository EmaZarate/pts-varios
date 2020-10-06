using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context
{
    public class SQLHoshinCoreContext : IdentityDbContext<Users, Roles, string,
                                                      UserClaim, UserRole, UserLogin,
                                                      RoleClaim, UserToken>
    {
        public SQLHoshinCoreContext(DbContextOptions<SQLHoshinCoreContext> options) : base(options)
        {

        }
        //For test purposes
        public SQLHoshinCoreContext()
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //HoshinCore
            modelBuilder.Entity<Companies>(entity =>
            {
                entity.HasKey(e => e.CompanyID);
            });

            modelBuilder.Entity<Alert>(entity =>
            {
                entity.HasKey(e => e.AlertID);
                entity.HasMany<AlertRol>()
                .WithOne(e => e.Alert)
                .HasForeignKey(e => e.AlertID);
            });

            modelBuilder.Entity<AlertRol>(b =>
            {
                b.HasKey(e => new { e.AlertID, e.RolID });
            });

            modelBuilder.Entity<AlertUsers>(entity =>
            {
                entity.HasKey(e => e.AlertUsersID);

                entity.HasOne(m => m.Alert)
                .WithMany(n => n.AlertUsers)
                .HasForeignKey(m => m.AlertID);

                entity.HasOne(m => m.Users)
                    .WithMany(n => n.AlertUsers)
                    .HasForeignKey(m => m.UsersID);
            });


            modelBuilder.Entity<Plants>(entity =>
            {
                entity.HasKey(e => e.PlantID);
            });

            modelBuilder.Entity<SectorsPlants>(entity =>
            {
                entity.HasKey(e => new { e.PlantID, e.SectorID });
                entity.HasOne(m => m.Plant)
                    .WithMany(n => n.SectorsPlants)
                    .HasForeignKey(m => m.PlantID)
                    .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(m => m.Sector)
                    .WithMany(n => n.SectorsPlants)
                    .HasForeignKey(m => m.SectorID);

                entity.HasMany(e => e.Audits)
                    .WithOne(e => e.SectorPlant)
                    .HasForeignKey(e => new { e.PlantID, e.SectorID });
            });

            modelBuilder.Entity<Sectors>(entity =>
            {
                entity.HasKey(e => e.SectorID);
            });

            modelBuilder.Entity<Jobs>(entity =>
            {
                entity.HasKey(e => e.JobID);
            });

            modelBuilder.Entity<JobsSectorsPlants>(entity =>
            {
                entity.HasKey(e => new { e.JobID, e.PlantID, e.SectorID });
                entity.HasOne(m => m.Job)
                    .WithMany(n => n.JobsSectorsPlants)
                    .HasForeignKey(m => m.JobID);

                entity.HasOne(m => m.SectorPlant)
                    .WithMany(n => n.JobsSectorsPlants)
                    .HasForeignKey(m => new { m.PlantID, m.SectorID });
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasOne(m => m.JobSectorPlant)
                    .WithMany(n => n.Users)
                    .HasForeignKey(m => new { m.JobID, m.PlantID, m.SectorID });
            });

            modelBuilder.Entity<Users>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne(e => e.User)
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();

                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne(e => e.User)
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();

                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne(e => e.User)
                    .HasForeignKey(ut => ut.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Roles>(b =>
            {
                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                // Each Role can have many associated RoleClaims
                b.HasMany(e => e.RoleClaims)
                    .WithOne(e => e.Role)
                    .HasForeignKey(rc => rc.RoleId)
                    .IsRequired();

                b.HasMany<AlertRol>()
                    .WithOne(e => e.Rol)
                    .HasForeignKey(e => e.RolID);

            });

            //HoshinQuality
            modelBuilder.Entity<ParametrizationCriterias>(b =>
            {
                b.HasKey(e => e.ParametrizationCriteriaID);
                b.HasMany(e => e.ParametrizationsFindingTypes)
                    .WithOne(e => e.ParametrizationCriteria)
                    .HasForeignKey(e => e.ParametrizationCriteriaID);
            });

            modelBuilder.Entity<FindingTypes>(b =>
            {
                b.HasKey(e => e.FindingTypeID);
                b.HasMany(e => e.ParametrizationsFindingTypes)
                    .WithOne(e => e.FindingType)
                    .HasForeignKey(e => e.FindingTypeID);

                b.HasMany(e => e.Findings)
                    .WithOne(e => e.FindingType)
                    .HasForeignKey(e => e.FindingTypeID);
            });

            modelBuilder.Entity<ParametrizationsFindingTypes>(b =>
            {
                b.HasKey(e => new { e.ParametrizationCriteriaID, e.FindingTypeID });
            });

            modelBuilder.Entity<Findings>(b =>
            {
                b.HasKey(e => e.FindingID);

                b.HasOne(e => e.CorrectiveAction)
                    .WithOne(e => e.Finding)
                    .HasForeignKey<CorrectiveActions>(e => e.FindingID)
                    .IsRequired(false);

                b.HasOne(e => e.SupplierEvaluation)
                    .WithOne(e => e.Finding)
                    .HasForeignKey<SupplierEvaluations>(e => e.FindingID);

                b.HasOne(e => e.AuditStandardAspect)
                    .WithMany(e => e.Findings)
                    .HasForeignKey(e => new { e.AspectID, e.AuditID, e.StandardID })
                    .IsRequired(false);

                b.HasMany(e => e.FindingsStatesHistory)
                    .WithOne(e => e.Finding)
                    .HasForeignKey(e => e.FindingID)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasMany(e => e.FindingComments)
                    .WithOne(e => e.Finding)
                    .HasForeignKey(e => e.FindingID);

                b.HasMany(e => e.FindingsEvidences)
                    .WithOne(e => e.Finding)
                    .HasForeignKey(e => e.FindingID);

                b.HasMany(e => e.FindingsReassignmentsHistory)
                    .WithOne(e => e.Finding)
                    .HasForeignKey(e => e.FindingID);
            });

            modelBuilder.Entity<FindingsStates>(b =>
            {
                b.HasKey(e => e.FindingStateID);
                b.HasMany(e => e.Findings)
                    .WithOne(e => e.FindingState)
                    .HasForeignKey(e => e.FindingStateID);
            });

            modelBuilder.Entity<Users>(b =>
            {
                b.HasMany(e => e.FindingEmitterUser)
                    .WithOne(e => e.EmitterUser)
                    .HasForeignKey(e => e.EmitterUserID);

                b.HasMany(e => e.FindingResponsibleUser)
                    .WithOne(e => e.ResponsibleUser)
                    .HasForeignKey(e => e.ResponsibleUserID)
                    .IsRequired(false);
            });

            modelBuilder.Entity<SectorsPlants>(b =>
            {
                b.HasMany(e => e.FindingLocation)
                    .WithOne(e => e.SectorPlantLocation)
                    .HasForeignKey(e => new { e.PlantLocationID, e.SectorLocationID })
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);

                b.HasMany(e => e.FindingTreatment)
                    .WithOne(e => e.SectorPlantTreatment)
                    .HasForeignKey(e => new { e.PlantTreatmentID, e.SectorTreatmentID })
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<FindingsStatesHistory>(b =>
            {
                b.HasKey(e => e.FindingStateHistoryID);
            });

            modelBuilder.Entity<FindingComments>(b =>
            {
                b.HasKey(e => new { e.FindingCommentID });
            });

            modelBuilder.Entity<FindingsEvidences>(b =>
            {
                b.HasKey(e => e.FindingEvidenceID);
            });

            modelBuilder.Entity<FindingsReassignmentsHistory>(b =>
            {
                b.HasKey(e => e.FindingReassignmentHistoryID);
            });

            modelBuilder.Entity<CorrectiveActions>(b =>
            {
                b.HasKey(e => e.CorrectiveActionID);
            });

            modelBuilder.Entity<SupplierEvaluations>(b =>
            {
                b.HasKey(e => e.SupplierEvaluationID);
            });


            modelBuilder.Entity<AuditStates>(b =>
            {
                b.HasKey(e => e.AuditStateID);
            });

            modelBuilder.Entity<AuditsTypes>(b =>
            {
                b.HasKey(e => e.AuditTypeID);
            });

            modelBuilder.Entity<AuditReschedulingHistory>(b =>
            {
                b.HasKey(e => e.AuditReschedulingHistoryID);
            });

            modelBuilder.Entity<AuditStandard>(b =>
            {
                b.HasKey(e => new { e.AuditID, e.StandardID });
            });

            modelBuilder.Entity<Standards>(b =>
            {
                b.HasKey(e => e.StandardID);

                b.HasMany(e => e.Aspects)
                    .WithOne(e => e.Standard)
                    .HasForeignKey(e => e.StandardID);

                b.HasMany<AuditStandard>()
                    .WithOne(e => e.Standard)
                    .HasForeignKey(e => e.StandardID);
            });

            modelBuilder.Entity<Aspects>(b =>
            {
                b.HasKey(e => e.AspectID);
            });

            modelBuilder.Entity<AspectStates>(b =>
            {
                b.HasKey(e => e.AspectStateID);
            });

            modelBuilder.Entity<AuditStandardAspect>(b =>
            {
                b.HasKey(e => new { e.AspectID, e.AuditID, e.StandardID });

                b.HasOne(e => e.Aspect)
                    .WithMany()
                    .HasForeignKey(e => e.AspectID).OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.AuditStandard)
                    .WithMany(e => e.AuditStandardAspects)
                    .HasForeignKey(e => new { e.AuditID, e.StandardID }).OnDelete(DeleteBehavior.Restrict);

                b.HasOne(e => e.AspectState)
                    .WithMany()
                    .HasForeignKey(e => e.AspectStateID).OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<Audits>(b =>
            {
                b.HasKey(e => e.AuditID);

                b.HasOne(e => e.AuditState)
                    .WithMany()
                    .HasForeignKey(e => e.AuditStateID);

                b.HasOne(e => e.AuditType)
                    .WithMany(e => e.Audits)
                    .HasForeignKey(e => e.AuditTypeID);

                b.HasMany(e => e.AuditReschedulingHistories)
                    .WithOne()
                    .HasForeignKey(e => e.AuditID);

                b.HasMany(e => e.AuditStandards)
                    .WithOne(e => e.Audit)
                    .HasForeignKey(e => e.AuditID);

                b.HasOne(e => e.Auditor)
                    .WithMany()
                    .HasForeignKey(e => e.AuditorID);
            });


            modelBuilder.Entity<TaskStates>(b =>
            {
                b.HasKey(k => k.TaskStateID);
            });

            modelBuilder.Entity<TaskEvidences>(b =>
            {
                b.HasKey(k => k.TaskEvidencesID);
            });

            modelBuilder.Entity<Tasks>(b =>
            {
                b.HasKey(k => k.TaskID);

                b.HasOne(e => e.TaskState)
                    .WithMany()
                    .HasForeignKey(k => k.TaskStateID);

                b.HasOne(e => e.ResponsibleUser)
                    .WithMany()
                    .HasForeignKey(k => k.ResponsibleUserID);

                b.HasMany(e => e.TaskEvidences)
                    .WithOne(e => e.Task)
                    .HasForeignKey(k => k.TaskID);
            });

            modelBuilder.Entity<CorrectiveActionStates>(b =>
            {
                b.HasKey(k => k.CorrectiveActionStateID);
            });

            modelBuilder.Entity<Fishbone>(b =>
            {
                b.HasKey(k => k.FishboneID);
            });

            modelBuilder.Entity<CorrectiveActionFishbone>(b =>
            {
                b.HasKey(k => new { k.CorrectiveActionID, k.FishboneID });

                b.HasOne(e => e.Fishbone)
                    .WithMany()
                    .HasForeignKey(k => k.FishboneID);

                b.HasOne(e => e.CorrectiveAction)
                    .WithMany(e => e.CorrectiveActionFishbones)
                    .HasForeignKey(k => k.CorrectiveActionID);

                b.HasMany(e => e.CorrectiveActionFishboneCauses)
                    .WithOne(e => e.CorrectiveActionFishbone)
                    .HasForeignKey(k => new { k.CorrectiveActionID, k.FishboneID })
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CorrectiveActionFishboneCauses>(b =>
            {
                b.HasKey(k => k.CorrectiveActionFishboneCauseID);

                b.HasMany(e => e.CorrectiveActionFishboneCauseWhys)
                    .WithOne(e => e.CorrectiveActionFishboneCause)
                    .HasForeignKey(k => k.CorrectiveActionFishboneCauseID)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CorrectiveActionFishboneCauseWhys>(b =>
            {
                b.HasKey(k => k.CorrectiveActionFishboneCauseWhyID);
            });

            modelBuilder.Entity<CorrectiveActionEvidences>(b =>
            {
                b.HasKey(k => k.CorrectiveActionEvidenceID);
            });

            modelBuilder.Entity<CorrectiveActions>(b =>
            {
                b.HasKey(k => k.CorrectiveActionID);

                b.HasOne(e => e.EmitterUser)
                    .WithMany()
                    .HasForeignKey(k => k.EmitterUserID);

                b.HasOne(e => e.SectorPlantLocation)
                    .WithMany()
                    .HasForeignKey(k => new { k.PlantLocationID, k.SectorLocationID })
                    .IsRequired(false);

                b.HasOne(e => e.SectorPlantTreatment)
                    .WithMany()
                    .HasForeignKey(k => new { k.PlantTreatmentID, k.SectorTreatmentID })
                    .IsRequired(false);

                b.HasMany(e => e.Evidences)
                    .WithOne(e => e.CorrectiveAction)
                    .HasForeignKey(k => k.CorrectiveActionID);

                b.HasOne(e => e.CorrectiveActionState)
                    .WithMany()
                    .HasForeignKey(k => k.CorrectiveActionStateID);

                b.HasOne(e => e.ResponisbleUser)
                    .WithMany()
                    .HasForeignKey(k => k.ResponsibleUserID);

                b.HasOne(e => e.ReviewerUser)
                    .WithMany()
                    .HasForeignKey(k => k.ReviewerUserID);

                b.HasMany(e => e.CorrectiveActionStatesHistory)
                    .WithOne(e => e.CorrectiveAction)
                    .HasForeignKey(e => e.CorrectiveActionID)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<CorrectiveActionStatesHistory>(b =>
            {
                b.HasKey(e => e.CorrectiveActionStatesHistoryID);
            });

            modelBuilder.Entity<UserCorrectiveAction>(b =>
            {
                b.HasKey(e => new { e.UserID, e.CorrectiveActionID });

                b.HasOne(e => e.CorrectiveActions)
                    .WithMany( e => e.UserCorrectiveActions)
                    .HasForeignKey(e => e.CorrectiveActionID);

                b.HasOne(e => e.Users)
                    .WithMany(e => e.UserCorrectiveActions)
                    .HasForeignKey(e => e.UserID);
            });

            modelBuilder.Entity<ParametrizationCorrectiveActions>(b =>
            {
                b.HasKey(e => e.ParametrizationCorrectiveActionID);
            });

            modelBuilder.Query<FindingsSP>();
        }

        //HoshinCore
        public virtual DbSet<Companies> Companies { get; set; }
        public virtual DbSet<Plants> Plants { get; set; }
        public virtual DbSet<SectorsPlants> SectorsPlants { get; set; }
        public virtual DbSet<Sectors> Sectors { get; set; }
        public virtual DbSet<JobsSectorsPlants> JobsSectorsPlants { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<AlertUsers> AlertUsers { get; set; }
        public virtual DbSet<Alert> Alert { get; set; }

        //HoshinQuality
        public virtual DbSet<ParametrizationCriterias> ParametrizationCriterias { get; set; }
        public virtual DbSet<ParametrizationCorrectiveActions> ParametrizationCorrectiveActions { get; set; }
        public virtual DbSet<FindingTypes> FindingTypes { get; set; }
        public virtual DbSet<ParametrizationsFindingTypes> ParametrizationsFindingTypes { get; set; }
        public virtual DbSet<Findings> Findings { get; set; }
        public virtual DbSet<SupplierEvaluations> SupplierEvaluations { get; set; }
        public virtual DbSet<FindingComments> FindingComments { get; set; }
        public virtual DbSet<FindingsStates> FindingsStates { get; set; }
        public virtual DbSet<FindingsEvidences> FindingsEvidences { get; set; }

        public virtual DbSet<FindingsReassignmentsHistory> FindingsReassignmentsHistories { get; set; }
        public virtual DbSet<FindingsStatesHistory> FindingsStatesHistories { get; set; }

        public virtual DbSet<AuditStates> AuditStates { get; set; }
        public virtual DbSet<AuditsTypes> AuditsTypes { get; set; }
        public virtual DbSet<Audits> Audits { get; set; }
        public virtual DbSet<AuditReschedulingHistory> AuditReschedulingHistory { get; set; }
        public virtual DbSet<AuditStandard> AuditStandard { get; set; }
        public virtual DbSet<Standards> Standards { get; set; }
        public virtual DbSet<AuditStandardAspect> AuditStandardAspects { get; set; }
        public virtual DbSet<Aspects> Aspects { get; set; }
        public virtual DbSet<AspectStates> AspectStates { get; set; }
        public virtual DbSet<AlertRol> AlertRol { get; set; }

        public virtual DbSet<CorrectiveActions> CorrectiveActions { get; set; }
        public virtual DbSet<CorrectiveActionStatesHistory> CorrectiveActionStatesHistory { get; set; }
        public virtual DbSet<Fishbone> Fishbone { get; set; }
        public virtual DbSet<CorrectiveActionFishbone> CorrectiveActionFishbone { get; set; }
        public virtual DbSet<CorrectiveActionFishboneCauses> CorrectiveActionFishboneCauses { get; set; }
        public virtual DbSet<CorrectiveActionFishboneCauseWhys> CorrectiveActionFishboneCauseWhys { get; set; }
        public virtual DbSet<CorrectiveActionStates> CorrectiveActionStates { get; set; }
        public virtual DbSet<TaskStates> TaskStates { get; set; }
        public virtual DbSet<CorrectiveActionEvidences> CorrectiveActionEvidences { get; set; }
        public virtual DbSet<TaskEvidences> TaskEvidences { get; set; }
        public virtual DbSet<Tasks> Tasks { get; set; }
        public virtual DbSet<UserCorrectiveAction> UserCorrectiveActions { get; set; }

    }
}