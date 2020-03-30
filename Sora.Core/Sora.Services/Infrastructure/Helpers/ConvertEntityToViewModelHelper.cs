﻿using Sora.Common.Extensions;
using Sora.Entites.ME;
using Sora.Services.ViewModels;
using System.Linq;

namespace Sora.Services.Infrastructure.Helpers
{
    public static class ConvertEntityToViewModelHelper
    {
        public static SpecialistViewModel ToSpecialistViewModel(this MESpecialist entity)
        {
            if(entity == null)
            {
                return null;
            }
            SpecialistViewModel dto = new SpecialistViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.MESpecialistID = entity.MESpecialistID;
            dto.TotalDoctor = entity.MEDoctorSpecialists == null ? 0 : entity.MEDoctorSpecialists.Count(x => x.FK_MEDoctorID.HasValue);
            dto.ChiefDoctor = entity.MEDoctor.ToShortDoctorDto();
            return dto;
        }

        public static SpecialistViewModel ToSpecialistViewModel(this MEDoctorSpecialist entity)
        {
            if(entity.MESpecialist == null)
            {
                return null;
            }
            SpecialistViewModel dto = new SpecialistViewModel();
            dto.CopyPropertiesFrom(entity.MESpecialist);
            dto.MESpecialistID = entity.MESpecialist.MESpecialistID;
            return dto;
        }

        public static DoctorViewModel ToDoctorViewModel(this MEDoctor entity)
        {
            if (entity == null)
                return null;

            DoctorViewModel dto = new DoctorViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.MEDoctorID = entity.MEDoctorID;
            return dto;
        }

        public static DoctorViewModel ToDoctorViewModel(this MEDoctorSpecialist entity, SpecialistViewModel[] specialists)
        {
            if (entity.MEDoctor == null)
                return null;

            DoctorViewModel dto = new DoctorViewModel();
            dto.CopyPropertiesFrom(entity.MEDoctor);
            dto.MEDoctorID = entity.MEDoctor.MEDoctorID;
            dto.Specialists = specialists;
            return dto;
        }
        public static DoctorViewModel ToDoctorViewModel(this MEDoctor entity, SpecialistViewModel[] specialists)
        {
            if (entity == null)
                return null;

            DoctorViewModel dto = new DoctorViewModel();
            dto.CopyPropertiesFrom(entity);
            dto.MEDoctorID = entity.MEDoctorID;
            dto.Specialists = specialists;
            dto.MESpecialistIds = specialists.Select(x => x.MESpecialistID.ToString()).ToArray();
            return dto;
        }

        public static ShortDoctorDto ToShortDoctorDto(this MEDoctor entity)
        {
            if (entity == null)
                return null;
            ShortDoctorDto dto = new ShortDoctorDto();
            dto.CopyPropertiesFrom(entity);
            return dto;
        }
    }
}
