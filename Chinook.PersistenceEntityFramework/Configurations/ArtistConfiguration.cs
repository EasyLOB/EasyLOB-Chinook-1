using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class ArtistConfiguration : EntityTypeConfiguration<Artist>
    {
        public ArtistConfiguration()
        {
            #region Class

            this.ToTable("Artist");

            this.HasKey(x => x.ArtistId);

            #endregion Class

            #region Properties

            this.Property(x => x.ArtistId)
                .HasColumnName("ArtistId")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            this.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .HasMaxLength(120);

            #endregion Properties
        }
    }
}