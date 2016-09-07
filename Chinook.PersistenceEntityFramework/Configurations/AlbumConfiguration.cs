using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class AlbumConfiguration : EntityTypeConfiguration<Album>
    {
        public AlbumConfiguration()
        {
            #region Class

            this.ToTable("Album");

            this.HasKey(x => x.AlbumId);

            #endregion Class

            #region Properties

            this.Property(x => x.AlbumId)
                .HasColumnName("AlbumId")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            this.Property(x => x.Title)
                .HasColumnName("Title")
                .HasColumnType("varchar")
                .HasMaxLength(160)
                .IsRequired();

            this.Property(x => x.ArtistId)
                .HasColumnName("ArtistId")
                .HasColumnType("int")
                .IsRequired();

            #endregion Properties

            #region Associations (FK)

            this.HasRequired(x => x.Artist)
                .WithMany(x => x.Albums)
                .HasForeignKey(x => x.ArtistId);

            #endregion Associations (FK)
        }
    }
}