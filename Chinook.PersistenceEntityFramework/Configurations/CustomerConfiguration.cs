using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            #region Class

            this.ToTable("Customer");

            this.HasKey(x => x.CustomerId);

            #endregion Class

            #region Properties

            this.Property(x => x.CustomerId)
                .HasColumnName("CustomerId")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            this.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .HasColumnType("varchar")
                .HasMaxLength(40)
                .IsRequired();

            this.Property(x => x.LastName)
                .HasColumnName("LastName")
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();

            this.Property(x => x.Company)
                .HasColumnName("Company")
                .HasColumnType("varchar")
                .HasMaxLength(80);

            this.Property(x => x.Address)
                .HasColumnName("Address")
                .HasColumnType("varchar")
                .HasMaxLength(70);

            this.Property(x => x.City)
                .HasColumnName("City")
                .HasColumnType("varchar")
                .HasMaxLength(40);

            this.Property(x => x.State)
                .HasColumnName("State")
                .HasColumnType("varchar")
                .HasMaxLength(40);

            this.Property(x => x.Country)
                .HasColumnName("Country")
                .HasColumnType("varchar")
                .HasMaxLength(40);

            this.Property(x => x.PostalCode)
                .HasColumnName("PostalCode")
                .HasColumnType("varchar")
                .HasMaxLength(10);

            this.Property(x => x.Phone)
                .HasColumnName("Phone")
                .HasColumnType("varchar")
                .HasMaxLength(24);

            this.Property(x => x.Fax)
                .HasColumnName("Fax")
                .HasColumnType("varchar")
                .HasMaxLength(24);

            this.Property(x => x.Email)
                .HasColumnName("Email")
                .HasColumnType("varchar")
                .HasMaxLength(60)
                .IsRequired();

            this.Property(x => x.SupportRepId)
                .HasColumnName("SupportRepId")
                .HasColumnType("int");

            #endregion Properties

            #region Associations (FK)

            this.HasOptional(x => x.Employee)
                .WithMany(x => x.Customers)
                .HasForeignKey(x => x.SupportRepId);

            #endregion Associations (FK)
        }
    }
}