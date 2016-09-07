using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class MediaTypeConfiguration : EntityTypeConfiguration<MediaType>
    {
        public MediaTypeConfiguration()
        {
            #region Class

            this.ToTable("MediaType");

            this.HasKey(x => x.MediaTypeId);

            #endregion Class

            #region Properties

            this.Property(x => x.MediaTypeId)
                .HasColumnName("MediaTypeId")
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