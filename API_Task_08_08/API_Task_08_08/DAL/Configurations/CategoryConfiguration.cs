using API_Task_08_08.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API_Task_08_08.DAL.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.HasIndex(c => c.Name).IsUnique();
        }
    }
}
