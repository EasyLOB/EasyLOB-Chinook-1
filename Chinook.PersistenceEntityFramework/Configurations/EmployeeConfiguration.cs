using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class EmployeeConfiguration : EntityTypeConfiguration<Employee>
    {
        public EmployeeConfiguration()
        {
            #region Class

            this.ToTable("Employee");

            this.HasKey(x => x.EmployeeId);

            #endregion Class

            #region Properties

            this.Property(x => x.EmployeeId)
                .HasColumnName("EmployeeId")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            this.Property(x => x.LastName)
                .HasColumnName("LastName")
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();

            this.Property(x => x.FirstName)
                .HasColumnName("FirstName")
                .HasColumnType("varchar")
                .HasMaxLength(20)
                .IsRequired();

            this.Property(x => x.Title)
                .HasColumnName("Title")
                .HasColumnType("varchar")
                .HasMaxLength(30);

            this.Property(x => x.ReportsTo)
                .HasColumnName("ReportsTo")
                .HasColumnType("int");

            this.Property(x => x.BirthDate)
                .HasColumnName("BirthDate")
                .HasColumnType("datetime");

            this.Property(x => x.HireDate)
                .HasColumnName("HireDate")
                .HasColumnType("datetime");

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
                .HasMaxLength(60);

            #endregion Properties

            #region Associations (FK)

            this.HasOptional(x => x.EmployeeReportsTo)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.ReportsTo);

            #endregion Associations (FK)
        }
    }
}