using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class GenreConfiguration : EntityTypeConfiguration<Genre>
    {
        public GenreConfiguration()
        {
            #region Class

            this.ToTable("Genre");

            this.HasKey(x => x.GenreId);

            #endregion Class

            #region Properties

            this.Property(x => x.GenreId)
                .HasColumnName("GenreId")
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