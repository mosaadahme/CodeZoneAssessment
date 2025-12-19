using CodeZone.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeZone.Data.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure ( EntityTypeBuilder<Student> builder )
        {
            builder.HasKey ( s => s.Id );

            builder.Property ( s => s.FullName )
                .IsRequired ( )
                .HasMaxLength ( 100 );

            builder.Property ( s => s.Email )
                .IsRequired ( );

            builder.HasIndex ( s => s.Email ).IsUnique ( );

            builder.Property ( s => s.NationalId )
                .IsRequired ( )
                .HasMaxLength ( 14 );

            builder.HasIndex ( s => s.NationalId ).IsUnique ( );

            builder.Property ( s => s.PhoneNumber )
                .HasMaxLength ( 11 );
        }
    }
}