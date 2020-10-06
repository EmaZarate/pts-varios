using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Context;
using Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Entities.Quality;
using Hoshin.Quality.Application.Repositories;
using Hoshin.Quality.Domain.CorrectiveAction;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hoshin.Core.Data.SQL.EntityFrameworkCoreDataAccess.SQLServer.Repository.Quality
{
    public class CorrectiveActionEvidenceRepository: ICorrectiveActionEvidenceRepository, CrossCutting.WorkflowCore.Repositories.ICorrectiveActionEvidenceRepository
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICorrectiveActionRepository _correctiveActionRepository;
        private readonly ICorrectiveActionStateRepository _correctiveActionStateRepository;
        public CorrectiveActionEvidenceRepository(
            IServiceProvider serviceProvider
            //ICorrectiveActionRepository correctiveActionRepository,
            //ICorrectiveActionStateRepository correctiveActionStateRepository
            )
        {
            _serviceProvider = serviceProvider;
            //_correctiveActionRepository = correctiveActionRepository;
            //_correctiveActionStateRepository = correctiveActionStateRepository;
        }

        public bool Update(int correctiveActionId, List<string> addUrls, List<string> deleteUrls)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
                List<CorrectiveActionEvidences> correctiveActionEvidences = new List<CorrectiveActionEvidences>();
                if(addUrls != null)
                {
                    foreach (var url in addUrls)
                    {
                        CorrectiveActionEvidences fe = new CorrectiveActionEvidences()
                        {
                            CorrectiveActionID = correctiveActionId,
                            Name = FormatFileName(url),
                            Url = url
                        };
                        correctiveActionEvidences.Add(fe);
                    }
                    _ctx.CorrectiveActionEvidences.AddRange(correctiveActionEvidences);
                }
                if(deleteUrls != null)
                {
                    foreach (var url in deleteUrls)
                    {
                        var fe = _ctx.CorrectiveActionEvidences.Where(x => x.Url == url && x.CorrectiveActionID == correctiveActionId).FirstOrDefault();
                        if (fe != null)
                        {
                            _ctx.CorrectiveActionEvidences.Remove(fe);
                        }
                    }
                }
                          
                _ctx.SaveChanges();

                return true;
            }
        }

        //public bool EffectiveEvaluate(CorrectiveAction correctiveAction, List<string> addUrls)
        //{
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
        //        List<CorrectiveActionEvidences> correctiveActionEvidences = new List<CorrectiveActionEvidences>();
        //        if (addUrls != null)
        //        {
        //            foreach (var url in addUrls)
        //            {
        //                CorrectiveActionEvidences fe = new CorrectiveActionEvidences()
        //                {
        //                    CorrectiveActionID = correctiveAction.CorrectiveActionID,
        //                    Name = FormatFileName(url),
        //                    Url = url
        //                };
        //                correctiveActionEvidences.Add(fe);
        //            }
        //            _ctx.CorrectiveActionEvidences.AddRange(correctiveActionEvidences);
        //        }

        //        var entityToUpdate = _correctiveActionRepository.GetOne(correctiveAction.CorrectiveActionID);
        //        entityToUpdate.EvaluationCommentary = correctiveAction.EvaluationCommentary;
        //        entityToUpdate.EffectiveDateImplementation = DateTime.Now;
        //        entityToUpdate.CorrectiveActionStateID = _correctiveActionStateRepository.GetAll().Where(x => x.Code == "CER" && x.Name == "Cerrada").FirstOrDefault().CorrectiveActionStateID;
        //        entityToUpdate.isEffective = true;
        //        entityToUpdate.SectorPlantLocation = null;
        //        entityToUpdate.SectorPlantTreatment = null;
        //        entityToUpdate.Evidences = null;

        //        _correctiveActionRepository.Update(entityToUpdate);

        //        _ctx.SaveChanges();

        //        return true;
        //    }
        //}

        //public bool NoEffectiveEvaluate(CorrectiveAction correctiveAction, List<string> addUrls)
        //{
        //    using (var scope = _serviceProvider.CreateScope())
        //    {
        //        var _ctx = scope.ServiceProvider.GetService(typeof(SQLHoshinCoreContext)) as SQLHoshinCoreContext;
        //        List<CorrectiveActionEvidences> correctiveActionEvidences = new List<CorrectiveActionEvidences>();
        //        if (addUrls != null)
        //        {
        //            foreach (var url in addUrls)
        //            {
        //                CorrectiveActionEvidences fe = new CorrectiveActionEvidences()
        //                {
        //                    CorrectiveActionID = correctiveAction.CorrectiveActionID,
        //                    Name = FormatFileName(url),
        //                    Url = url
        //                };
        //                correctiveActionEvidences.Add(fe);
        //            }
        //            _ctx.CorrectiveActionEvidences.AddRange(correctiveActionEvidences);
        //        }

        //        var entityToUpdate = _correctiveActionRepository.GetOne(correctiveAction.CorrectiveActionID);
        //        entityToUpdate.EvaluationCommentary = correctiveAction.EvaluationCommentary;
        //        entityToUpdate.EffectiveDateImplementation = DateTime.Now;
        //        entityToUpdate.CorrectiveActionStateID = _correctiveActionStateRepository.GetAll().Where(x => x.Code == "CER" && x.Name == "Cerrada").FirstOrDefault().CorrectiveActionStateID;
        //        entityToUpdate.isEffective = false;
        //        entityToUpdate.SectorPlantLocation = null;
        //        entityToUpdate.SectorPlantTreatment = null;
        //        entityToUpdate.Evidences = null;

        //        _correctiveActionRepository.Update(entityToUpdate);

        //        _ctx.SaveChanges();

        //        return true;
        //    }
        //}

        private string FormatFileName(string url)
        {
            string name = url.Split("correctiveaction/")[1];
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
