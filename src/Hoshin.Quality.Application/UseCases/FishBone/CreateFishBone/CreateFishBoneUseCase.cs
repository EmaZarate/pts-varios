using Hoshin.Core.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Quality.Application.UseCases.FishBone.CreateFishBone
{
    public class CreateFishBoneUseCase : ICreateFishBoneUseCase
    {

        private IFishBoneRepository _fishBoneRepository;

        public CreateFishBoneUseCase(IFishBoneRepository fishBoneRepository)
        {
            _fishBoneRepository = fishBoneRepository;
        }

        public FishBoneOutput Execute(bool active, string color, string name)
        {
            if(_fishBoneRepository.GetCountActive() < 6 && active)
            {
                if (_fishBoneRepository.Get(name, color) == null)
                {
                    var newParam = new Domain.FishBone.FishBone(name, color, active);
                    newParam = _fishBoneRepository.Add(newParam);
                    return new FishBoneOutput(newParam.FishboneID, newParam.Name, newParam.Color, newParam.Active);
                }
                else
                {
                    throw new DuplicateEntityException(name, "Ya existe un estado de espina de pescado con este nombre");
                }
            }
            else
            {
                throw new DuplicateEntityException(name, "No se puede agregar mas de 6 categorías activas");
            } 
        }
    }
}
