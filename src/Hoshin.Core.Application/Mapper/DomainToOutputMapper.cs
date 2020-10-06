using AutoMapper;
using Hoshin.Core.Application.UseCases.Role;
using Hoshin.Core.Application.UseCases.User.GetAllUser;
using Hoshin.Core.Domain.Claim;
using Hoshin.Core.Domain.Role;
using Hoshin.Core.Application.UseCases.CRUDJob;
using Hoshin.Core.Application.UseCases.CRUDSector;
using Hoshin.Core.Domain.Job;
using Hoshin.Core.Domain.Sector;
using Hoshin.Core.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hoshin.Core.Application.Mapper
{
    public class DomainToOutputMapper : Profile
    {
        public DomainToOutputMapper()
        {
            CreateMap<User, UserOutput>()
                .ForMember(e => e.Name, opt => opt.MapFrom(e => e.FirstName))
                .ForMember(e => e.Surname, opt => opt.MapFrom(e => e.LastName))
                .ForMember(e => e.Job, opt => opt.MapFrom(e => e.JobSectorPlant.Job.Name))
                .ForMember(e => e.Plant, opt => opt.MapFrom(e => e.JobSectorPlant.SectorPlant.Plant.Name))
                .ForMember(e => e.Sector, opt => opt.MapFrom(e => e.JobSectorPlant.SectorPlant.Sector.Name));
            CreateMap<Role, RoleOutput>();
            CreateMap<Claim, ClaimOutput>();
            CreateMap<Sector, SectorOutput>()
                .ForMember(e => e.SectorId, opt => opt.MapFrom(e => e.Id));
            CreateMap<Job, JobOutput>()
                .ForMember(e => e.JobId, opt => opt.MapFrom(e => e.Id));
        }
    }
}
