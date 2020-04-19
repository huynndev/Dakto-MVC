﻿using System;

namespace Sora.Services.ViewModels
{
    public class AppointmentViewModel
    {
        public int? Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime DayOfBirth { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public DateTime? FirstDay { get; set; }

        public string FirstTime { get; set; }

        public DateTime? SecondDay { get; set; }

        public string SecondTime { get; set; }

        public int? FK_MESpecialistID { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public ShortSpecialistDto Specialist { get; set; }
    }
}
