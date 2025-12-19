using CodeZone.Core.Entities;

namespace CodeZone.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync ( AppDbContext context )
        {
            if ( !context.Courses.Any ( ) )
            {
                var courses = new List<Course>
                {
                    new Course { Title = "C# Fundamentals", Description = "Learn the basics of C#", MaxCapacity = 5 },
                    new Course { Title = ".NET Core", Description = "Deep dive into .NET", MaxCapacity = 20 },
                    new Course { Title = "SQL Server", Description = "Database design and optimization", MaxCapacity = 2 } 
                };

                await context.Courses.AddRangeAsync ( courses );
                await context.SaveChangesAsync ( );
            }

            if ( !context.Students.Any ( ) )
            {
                var students = new List<Student>
                {
                    new Student { FullName = "Ahmed Ali", Email = "ahmed@codezone.com", NationalId = "29901011234567", BirthDate = new DateTime(1999, 1, 1), PhoneNumber = "01000000001" },
                    new Student { FullName = "Sara Hassan", Email = "sara@codezone.com", NationalId = "29805051234567", BirthDate = new DateTime(1998, 5, 5), PhoneNumber = "01100000002" }
                };

                await context.Students.AddRangeAsync ( students );
                await context.SaveChangesAsync ( );
            }
        }
    }
}