using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class InvoiceLineConfiguration : EntityTypeConfiguration<InvoiceLine>
    {
        public InvoiceLineConfiguration()
        {
            #region Class

            this.ToTable("InvoiceLine");

            this.HasKey(x => x.InvoiceLineId);

            #endregion Class

            #region Properties

            this.Property(x => x.InvoiceLineId)
                .HasColumnName("InvoiceLineId")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            this.Property(x => x.InvoiceId)
                .HasColumnName("InvoiceId")
                .HasColumnType("int")
                .IsRequired();

            this.Property(x => x.TrackId)
                .HasColumnName("TrackId")
                .HasColumnType("int")
                .IsRequired();

            this.Property(x => x.UnitPrice)
                .HasColumnName("UnitPrice")
                .HasColumnType("decimal")
                .IsRequired();

            this.Property(x => x.Quantity)
                .HasColumnName("Quantity")
                .HasColumnType("int")
                .IsRequired();

            #endregion Properties

            #region Associations (FK)

            this.HasRequired(x => x.Invoice)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.InvoiceId);

            this.HasRequired(x => x.Track)
                .WithMany(x => x.InvoiceLines)
                .HasForeignKey(x => x.TrackId);

            #endregion Associations (FK)
        }
    }
}