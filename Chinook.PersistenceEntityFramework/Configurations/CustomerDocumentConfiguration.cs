using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Chinook.Data;

namespace Chinook.Persistence
{
    public class CustomerDocumentConfiguration : EntityTypeConfiguration<CustomerDocument>
    {
        public CustomerDocumentConfiguration()
        {
            #region Class

            this.ToTable("CustomerDocument");

            this.HasKey(x => x.CustomerDocumentId);

            #endregion Class

            #region Properties

            this.Property(x => x.CustomerDocumentId)
                .HasColumnName("CustomerDocumentId")
                .HasColumnType("int")
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            this.Property(x => x.CustomerId)
                .HasColumnName("CustomerId")
                .HasColumnType("int")
                .IsRequired();

            this.Property(x => x.Description)
                .HasColumnName("Description")
                .HasColumnType("varchar")
                .HasMaxLength(100);

            this.Property(x => x.FileAcronym)
                .HasColumnName("FileAcronym")
                .HasColumnType("varchar")
                .HasMaxLength(10);

            #endregion Properties

            #region Associations (FK)

            // !!!
            //this.HasRequired(x => x.Customer)
            //    .WithMany(x => x.CustomerDocuments)
            //    .HasForeignKey(x => x.CustomerId);

            #endregion Associations (FK)
        }
    }
}