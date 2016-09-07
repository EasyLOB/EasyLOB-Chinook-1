using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class TrackConfiguration : EntityTypeConfiguration<Track>
    {
        public TrackConfiguration()
        {
            #region Class

            this.ToTable("Track");

            this.HasKey(x => x.TrackId);

            #endregion Class

            #region Properties

            this.Property(x => x.TrackId)
                .HasColumnName("TrackId")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            this.Property(x => x.Name)
                .HasColumnName("Name")
                .HasColumnType("varchar")
                .HasMaxLength(200)
                .IsRequired();

            this.Property(x => x.AlbumId)
                .HasColumnName("AlbumId")
                .HasColumnType("int");

            this.Property(x => x.MediaTypeId)
                .HasColumnName("MediaTypeId")
                .HasColumnType("int")
                .IsRequired();

            this.Property(x => x.GenreId)
                .HasColumnName("GenreId")
                .HasColumnType("int");

            this.Property(x => x.Composer)
                .HasColumnName("Composer")
                .HasColumnType("varchar")
                .HasMaxLength(220);

            this.Property(x => x.Milliseconds)
                .HasColumnName("Milliseconds")
                .HasColumnType("int")
                .IsRequired();

            this.Property(x => x.Bytes)
                .HasColumnName("Bytes")
                .HasColumnType("int");

            this.Property(x => x.UnitPrice)
                .HasColumnName("UnitPrice")
                .HasColumnType("decimal")
                .IsRequired();

            #endregion Properties

            #region Associations (FK)

            this.HasOptional(x => x.Album)
                .WithMany(x => x.Tracks)
                .HasForeignKey(x => x.AlbumId);

            this.HasOptional(x => x.Genre)
                .WithMany(x => x.Tracks)
                .HasForeignKey(x => x.GenreId);

            this.HasRequired(x => x.MediaType)
                .WithMany(x => x.Tracks)
                .HasForeignKey(x => x.MediaTypeId);

            #endregion Associations (FK)
        }
    }
}