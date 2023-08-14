using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Project99.Model.Configurations
{
    public class IntegrantesConfig : IEntityTypeConfiguration<Integrantes>
    {
        public void Configure(EntityTypeBuilder<Integrantes> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(e => e.Cargo)
             .WithMany(e => e.Integrantes)
             .HasForeignKey(e => e.IdCargo)
             .IsRequired();
        }
    }
}
