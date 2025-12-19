using CodeZone.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeZone.Data.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure ( EntityTypeBuilder<Course> builder )
        {
            builder.Property ( c => c.Title )
                .IsRequired ( )
                .HasMaxLength ( 100 );

            builder.Property ( c => c.MaxCapacity )
                .IsRequired ( );
        }
    }
}