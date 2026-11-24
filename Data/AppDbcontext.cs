using Microsoft.EntityFrameworkCore;
using task1_web_api.Models;

namespace task1_web_api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);

            // Seed Patients
            modelBuilder.Entity<Patient>().HasData(
                new Patient
                {
                    Id = 1,
                    Name = "Ahmed Mohamed",
                    DateOfBirth = new DateTime(2000, 5, 10)
                },
                new Patient
                {
                    Id = 2,
                    Name = "Sara Ali",
                    DateOfBirth = new DateTime(1998, 8, 15)
                }
            );

            // Seed Doctors
            modelBuilder.Entity<Doctor>().HasData(
                new Doctor
                {
                    Id = 1,
                    Name = "Dr. Mahmoud",
                    Specialization = "Cardiology"
                },
                new Doctor
                {
                    Id = 2,
                    Name = "Dr. Mona",
                    Specialization = "Dentistry"
                }
            );

            // Seed Appointments
            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    PatientId = 1,
                    DoctorId = 1,
                    AppointmentDate = new DateTime(2026, 12, 1)
                },
                new Appointment
                {
                    PatientId = 2,
                    DoctorId = 2,
                    AppointmentDate = new DateTime(2026, 12, 2)
                }
            );
        }
    }
}