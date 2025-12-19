namespace CodeZone.Core.Entities
{
    public class Student : BaseEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string NationalId { get; set; }
        public string? PhoneNumber { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment> ( );
    }
}
