using CodeZone.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeZone.Data.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure ( EntityTypeBuilder<Enrollment> builder )
        {
            builder.HasOne ( e => e.Student )
                .WithMany ( s => s.Enrollments )
                .HasForeignKey ( e => e.StudentId );

            builder.HasOne ( e => e.Course )
                .WithMany ( c => c.Enrollments )
                .HasForeignKey ( e => e.CourseId );
        }
    }
}