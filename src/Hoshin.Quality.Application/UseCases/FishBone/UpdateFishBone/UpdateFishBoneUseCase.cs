using System;
using System.Collections.Generic;
using System.Text;
using Hoshin.Quality.Application.Exceptions.Common;
using Hoshin.Quality.Application.Repositories;


namespace Hoshin.Quality.Application.UseCases.FishBone.UpdateFishBone
{
    public class UpdateFishBoneUseCase : IUpdateFishBoneUseCase
    {
        private readonly IFishBoneRepository _fishBoneRepository;

        public UpdateFishBoneUseCase(IFishBoneRepository fishBoneRepository)
        {
            _fishBoneRepository = fishBoneRepository;
        }

        public FishBoneOutput Execute(int id, string name, string color, bool active)       
        {
            var fishBone = _fishBoneRepository.Get(id);
            if (fishBone != null)
            {
                if(!((fishBone.Active != active) && !active && _fishBoneRepository.GetCountActive() <=  1 ))
                {
                    if (!((fishBone.Active != active) && active && _fishBoneRepository.GetCountActive() >= 6))
                    {
                        if (_fishBoneRepository.CheckExistsFishBone(color, name, id) == null)
                        {
                            fishBone.Name = name;
                            fishBone.Active = active;
                            fishBone.Color = color;
                            var res = _fishBoneRepository.Update(fishBone);
                            return new FishBoneOutput(fishBone.FishboneID, fishBone.Name, fishBone.Color, fishBone.Active);
                        }
                        else
                        {
                            throw new DuplicateEntityException(name, "Ya existe una espina de pesacado con este nombre");
                        }
                    }
                    else
                    {
                        throw new CantDesactivateException(name, "No se puede tener mas de 6 categorías activas");
                    }
                    
                }
                else
                {
                    throw new CantDesactivateException(name, "Debe existir al menos una categoría activa");
                } 
            }
            else
            {
                throw new EntityNotFoundException(id, "No se encontró espina de pesacado con ese ID");
            } 
        }
    }
}
