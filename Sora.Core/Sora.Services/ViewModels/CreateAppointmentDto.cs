namespace Sora.Services.ViewModels
{
    public class CreateAppointmentDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public long DayOfBirth { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public long? FirstDay { get; set; }

        public string FirstTime { get; set; }

        public long? SecondDay { get; set; }

        public string SecondTime { get; set; }

        public int? FK_MESpecialistID { get; set; }

        public string Description { get; set; }
    }
}
