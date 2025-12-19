using CodeZone.Core.DTOs.Courses;
using FluentValidation;

namespace CodeZone.Services.Courses
{
    public class CourseAddValidator : AbstractValidator<CourseAddRequest>
    {
        public CourseAddValidator ( )
        {
            RuleFor ( x => x.Title ).NotEmpty ( ).MaximumLength ( 100 );
            RuleFor ( x => x.MaxCapacity ).GreaterThan ( 0 );
        }
    }

    public class CourseUpdateValidator : AbstractValidator<CourseUpdateRequest>
    {
        public CourseUpdateValidator ( )
        {
            RuleFor ( x => x.Id ).GreaterThan ( 0 );
            RuleFor ( x => x.Title ).NotEmpty ( ).MaximumLength ( 100 );
            RuleFor ( x => x.MaxCapacity ).GreaterThan ( 0 );
        }
    }
}