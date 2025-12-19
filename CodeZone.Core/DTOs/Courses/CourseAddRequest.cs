namespace CodeZone.Core.DTOs.Courses
{
    public class CourseAddRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int MaxCapacity { get; set; }
    }
}