namespace CodeZone.Core.DTOs.Students
{
    public class StudentResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
        public string BirthDateFormatted { get; set; }  
    }
}