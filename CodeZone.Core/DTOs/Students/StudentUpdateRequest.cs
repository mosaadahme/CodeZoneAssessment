namespace CodeZone.Core.DTOs.Students
{
    public class StudentUpdateRequest
    {
        public int Id { get; set; }  
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public string NationalId { get; set; }
        public string? PhoneNumber { get; set; }
    }
}