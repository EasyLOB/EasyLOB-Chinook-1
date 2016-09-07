using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class InvoiceConfiguration : EntityTypeConfiguration<Invoice>
    {
        public InvoiceConfiguration()
        {
            #region Class

            this.ToTable("Invoice");

            this.HasKey(x => x.InvoiceId);

            #endregion Class

            #region Properties

            this.Property(x => x.InvoiceId)
                .HasColumnName("InvoiceId")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            this.Property(x => x.CustomerId)
                .HasColumnName("CustomerId")
                .HasColumnType("int")
                .IsRequired();

            this.Property(x => x.InvoiceDate)
                .HasColumnName("InvoiceDate")
                .HasColumnType("datetime")
                .IsRequired();

            this.Property(x => x.BillingAddress)
                .HasColumnName("BillingAddress")
                .HasColumnType("varchar")
                .HasMaxLength(70);

            this.Property(x => x.BillingCity)
                .HasColumnName("BillingCity")
                .HasColumnType("varchar")
                .HasMaxLength(40);

            this.Property(x => x.BillingState)
                .HasColumnName("BillingState")
                .HasColumnType("varchar")
                .HasMaxLength(40);

            this.Property(x => x.BillingCountry)
                .HasColumnName("BillingCountry")
                .HasColumnType("varchar")
                .HasMaxLength(40);

            this.Property(x => x.BillingPostalCode)
                .HasColumnName("BillingPostalCode")
                .HasColumnType("varchar")
                .HasMaxLength(10);

            this.Property(x => x.Total)
                .HasColumnName("Total")
                .HasColumnType("decimal")
                .IsRequired();

            #endregion Properties

            #region Associations (FK)

            this.HasRequired(x => x.Customer)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.CustomerId);

            #endregion Associations (FK)
        }
    }
}