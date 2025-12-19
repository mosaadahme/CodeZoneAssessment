using CodeZone.Core.DTOs.Students;
using FluentValidation;

namespace CodeZone.Services.Students
{
    public class StudentAddValidator : AbstractValidator<StudentAddRequest>
    {
        public StudentAddValidator ( )
        {
            RuleFor ( x => x.FullName ).NotEmpty ( ).Length ( 3, 100 );
            RuleFor ( x => x.Email ).NotEmpty ( ).EmailAddress ( );
            RuleFor ( x => x.NationalId ).Length ( 14 ).Matches ( "^[0-9]*$" );
            RuleFor ( x => x.BirthDate ).LessThan ( DateTime.Now );
        }
    }

    public class StudentUpdateValidator : AbstractValidator<StudentUpdateRequest>
    {
        public StudentUpdateValidator ( )
        {
            RuleFor ( x => x.Id ).GreaterThan ( 0 );
            RuleFor ( x => x.FullName ).NotEmpty ( ).Length ( 3, 100 );
            RuleFor ( x => x.Email ).NotEmpty ( ).EmailAddress ( );
            RuleFor ( x => x.NationalId ).Length ( 14 ).Matches ( "^[0-9]*$" );
        }
    }
}