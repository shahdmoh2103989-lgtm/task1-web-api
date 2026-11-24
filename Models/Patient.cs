namespace task1_web_api.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }

        public List<Appointment> Appointments { get; set; }
    }
}