namespace CodeZone.Core.DTOs.Courses
{
    public class CourseResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int MaxCapacity { get; set; }

        public int EnrollmentsCount { get; set; }
    }
}