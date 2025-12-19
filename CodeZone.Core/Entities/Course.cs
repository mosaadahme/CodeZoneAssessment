namespace CodeZone.Core.Entities
{
    public class Course : BaseEntity
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int MaxCapacity { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment> ( );
    }
}
