
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Hoshin.Core.Domain;
using Hoshin.Core.Domain.Users;
using Hoshin.Quality.Domain.Interfaces;



namespace Hoshin.Quality.Domain.Task
{
    public class Task
    {
      
        public int TaskID { get; set; }
        public int EntityID { get; set; }
        public int EntityType { get; set; }
        public string Description { get; set; }
        public string ResponsibleUserID { get; set; }
        public User ResponsibleUser { get; set; }
        public DateTime ImplementationPlannedDate { get; set; }
        public DateTime? ImplementationEffectiveDate { get; set; }
        public string Observation { get; set; }
        public string Result { get; set; }
        public int TaskStateID { get; set; }
        public bool RequireEvidence { get; set; }
        public TaskState.TaskState TaskState { get; set; }
        public List<Evidence.Evidence> TaskEvidences { get; set; }
        public List<string> DeleteEvidencesUrls { get; set; }
        public List<string> NewEvidencesUrls { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int PlantID { get; set; }
        public int SectorID { get; set; }
        public DateTime? overdureTime { get; set; }
        public Task()
        {

        }
    }




}


