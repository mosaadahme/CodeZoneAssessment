using CodeZone.Core.DTOs.Enrollments;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeZone.Services.Enrollments
{
    public class EnrollmentValidator : AbstractValidator<EnrollmentRequest>
    {
        public EnrollmentValidator ( )
        {
            RuleFor ( x => x.StudentId ).GreaterThan ( 0 ).WithMessage ( "Please select a student." );
            RuleFor ( x => x.CourseId ).GreaterThan ( 0 ).WithMessage ( "Please select a course." );
        }
    }
}
