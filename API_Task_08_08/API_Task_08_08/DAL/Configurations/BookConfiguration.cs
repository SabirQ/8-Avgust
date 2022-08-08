using API_Task_08_08.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Task_08_08.DAL.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();
            builder.Property(c => c.Pages).IsRequired();
            builder.Property(c => c.Price).HasColumnType("decimal(6,2)").IsRequired();
            builder.Property(c => c.CategoryId).IsRequired();
            
        }
    }
}
