using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class PlaylistConfiguration : EntityTypeConfiguration<Playlist>
    {
        public PlaylistConfiguration()
        {
            #region Class

            this.ToTable("Playlist");

            this.HasKey(x => x.PlaylistId);

            #endregion Class

            #region Properties

            this.Property(x => x.PlaylistId)
                .HasColumnName("PlaylistId")
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