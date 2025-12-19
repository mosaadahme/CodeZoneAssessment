namespace CodeZone.Core.DTOs.Courses
{
    public class CourseUpdateRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int MaxCapacity { get; set; }
    }
}