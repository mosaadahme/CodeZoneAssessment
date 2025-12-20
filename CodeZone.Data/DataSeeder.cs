using Bogus;
using CodeZone.Core.Entities;

namespace CodeZone.Data
{
    public static class DataSeeder
    {
        public static async Task SeedAsync ( AppDbContext context )
        {
            if ( !context.Courses.Any ( ) )
            {
                var courseFaker = new Faker<Course> ( )
                    .RuleFor ( c => c.Title, f => f.PickRandom ( new [] { "C# Advanced", "Entity Framework Core", "SQL Server Optimization", "React Basics", "Angular for .NET Devs", "Docker & Kubernetes", "Microservices Architecture", "Clean Architecture", "Design Patterns", "Cloud Computing with Azure" } ) )
                    .RuleFor ( c => c.Description, f => f.Lorem.Sentence ( 5 ) )
                    .RuleFor ( c => c.MaxCapacity, f => f.Random.Int ( 10, 50 ) );

                var courses = courseFaker.Generate ( 10 );

                await context.Courses.AddRangeAsync ( courses );
                await context.SaveChangesAsync ( );
            }

            if ( !context.Students.Any ( ) )
            {
                 var studentFaker = new Faker<Student> ( "ar" )
                    .RuleFor ( s => s.FullName, f => f.Name.FullName ( ) ) 
                    .RuleFor ( s => s.Email, ( f, s ) => f.Internet.Email ( s.FullName ) ) 
                    .RuleFor ( s => s.NationalId, f => f.Random.Replace ( "##############" ) )
                    .RuleFor ( s => s.BirthDate, f => f.Date.Past ( 25, DateTime.Now.AddYears ( -18 ) ) )
                    .RuleFor ( s => s.PhoneNumber, f => f.Phone.PhoneNumber ( "01#########" ) );

                var students = studentFaker.Generate ( 50 );

                await context.Students.AddRangeAsync ( students );
                await context.SaveChangesAsync ( );
            }
        }
    }
}