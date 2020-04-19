using System;

namespace Sora.Services.ViewModels
{
    public class ShortAppointmentDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public DateTime DayOfBirth { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }

        public DateTime? FirstDay { get; set; }

        public DateTime? SecondDay { get; set; }
    }
}
