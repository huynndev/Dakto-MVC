﻿using Sora.Common.Extensions;
using Sora.Entites.ME;
using Sora.Services.ViewModels;

namespace Sora.Services.Infrastructure.Helpers
{
    public static class ConvertViewModelToEntityHelper
    {
        public static MESpecialist ToMESpecialist(this SpecialistViewModel dto)
        {
            MESpecialist entity = new MESpecialist();
            entity.CopyPropertiesFrom(dto);
            return entity;
        }

        public static MEDoctor ToMEDoctor(this DoctorViewModel dto)
        {
            MEDoctor entity = new MEDoctor();
            entity.CopyPropertiesFrom(dto);
            return entity;
        }

        public static MESpecialistType ToMESpecialistType(this SpecialistTypeViewModel dto)
        {
            MESpecialistType entity = new MESpecialistType();
            entity.CopyPropertiesFrom(dto);
            return entity;
        }

    }
}
