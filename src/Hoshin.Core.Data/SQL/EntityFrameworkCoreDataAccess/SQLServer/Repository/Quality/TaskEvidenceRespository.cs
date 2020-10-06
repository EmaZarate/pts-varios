using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Quality.Application.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class TaskEvidenceRespository : ITasksEvidenceRepository
    {
        private readonly IServiceProvider _serviceProvider;
        public TaskEvidenceRespository(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        public bool Update(int taskID, List<string> addUrls, List<string> deletUrls)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                List<TaskEvidences> taskEvidences = new List<TaskEvidences>();
                if (deletUrls != null)
                {
                    foreach (var url in deletUrls)
                    {
                        var fe = _ctx.TaskEvidences.Where(x => x.Url == url && x.TaskID == taskID).FirstOrDefault();
                        if (fe != null)
                        {
                            _ctx.TaskEvidences.Remove(fe);
                        }
                    }
                    _ctx.SaveChanges();
                }
                if (addUrls != null)
                {
                    foreach (var url in addUrls)
                    {
                        

                        TaskEvidences te = new TaskEvidences()
                        {
                            TaskID = taskID,
                            Name = FormatFileName(url),
                            Url = url


                        };
                        taskEvidences.Add(te);
                    }
                    _ctx.TaskEvidences.AddRange(taskEvidences);
                    _ctx.SaveChanges();
                }
                
                
                return true;
            }
        }


        private string FormatFileName(string url)
        {
            string name = url.Split("tasks/")[1];
            return DecodeUrlString(name);
        }

        private static string DecodeUrlString(string name)
        {
            string newName;
            while ((newName = Uri.UnescapeDataString(name)) != name)
                name = newName;
            return newName;
        }

    }

}

