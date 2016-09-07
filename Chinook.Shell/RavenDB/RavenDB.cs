using Chinook.Data;
using Chinook.Persistence;
using EasyLOB.Data;
using EasyLOB.Persistence;
using Raven.Client;
using System;
using System.Data.Common;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void SQLServer2RavenDB()
        {
            Console.WriteLine("\nChinook SQL Server => RavenDB");
            Console.Write("\nPress <Y> to execute... ");

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar != 'y' && key.KeyChar != 'Y')
            {
                return;
            }
            Console.WriteLine();

            DbConnection connection = null;
            ChinookRavenDB chinook = new ChinookRavenDB();

            try
            {
                DbProviderFactory provider;
                string connectionName = "Chinook";

                provider = AdoNetHelper.GetProvider(connectionName);
                connection = provider.CreateConnection();
                connection.ConnectionString = AdoNetHelper.GetConnectionString(connectionName);
                connection.Open();

                DbCommand command;

                command = provider.CreateCommand();
                command.Connection = connection;
                command.CommandTimeout = 600;
                command.CommandType = System.Data.CommandType.Text;

                DbCommand2RavenDB<Album>(command, chinook);
                DbCommand2RavenDB<Artist>(command, chinook);
                DbCommand2RavenDB<Customer>(command, chinook);
                DbCommand2RavenDB<Employee>(command, chinook);
                DbCommand2RavenDB<Genre>(command, chinook);
                DbCommand2RavenDB<Invoice>(command, chinook);
                DbCommand2RavenDB<InvoiceLine>(command, chinook);
                DbCommand2RavenDB<MediaType>(command, chinook);
                DbCommand2RavenDB<Playlist>(command, chinook);
                DbCommand2RavenDB<PlaylistTrack>(command, chinook);
                DbCommand2RavenDB<Track>(command, chinook);

                //DbCommand2RavenDB<CustomerDocument>(command, chinook);

                // Sequence

                IUnitOfWork unitOfWork = new ChinookUnitOfWorkRavenDB();

                command.CommandText = "SELECT MAX(AlbumId) FROM Album";
                unitOfWork.GetRepository<Album>().SetSequence((int)command.ExecuteScalar());

                command.CommandText = "SELECT MAX(ArtistId) FROM Artist";
                unitOfWork.GetRepository<Artist>().SetSequence((int)command.ExecuteScalar());

                command.CommandText = "SELECT MAX(CustomerId) FROM Customer";
                unitOfWork.GetRepository<Customer>().SetSequence((int)command.ExecuteScalar());

                command.CommandText = "SELECT MAX(EmployeeId) FROM Employee";
                unitOfWork.GetRepository<Employee>().SetSequence((int)command.ExecuteScalar());

                command.CommandText = "SELECT MAX(GenreId) FROM Genre";
                unitOfWork.GetRepository<Genre>().SetSequence((int)command.ExecuteScalar());

                command.CommandText = "SELECT MAX(InvoiceId) FROM Invoice";
                unitOfWork.GetRepository<Invoice>().SetSequence((int)command.ExecuteScalar());

                command.CommandText = "SELECT MAX(InvoiceLineId) FROM InvoiceLine";
                unitOfWork.GetRepository<InvoiceLine>().SetSequence((int)command.ExecuteScalar());

                command.CommandText = "SELECT MAX(MediaTypeId) FROM MediaType";
                unitOfWork.GetRepository<MediaType>().SetSequence((int)command.ExecuteScalar());

                command.CommandText = "SELECT MAX(PlaylistId) FROM Playlist";
                unitOfWork.GetRepository<Playlist>().SetSequence((int)command.ExecuteScalar());

                command.CommandText = "SELECT MAX(PlaylistId * 1000000 + TrackId) FROM PlaylistTrack"; // !!!
                unitOfWork.GetRepository<PlaylistTrack>().SetSequence((int)command.ExecuteScalar());

                command.CommandText = "SELECT MAX(TrackId) FROM Track";
                unitOfWork.GetRepository<Track>().SetSequence((int)command.ExecuteScalar());

                //command.CommandText = "SELECT MAX(CustomerDocumentId) FROM CustomerDocument";
                //unitOfWork.GetRepository<CustomerDocument>().SetSequence((int)command.ExecuteScalar());
                unitOfWork.GetRepository<CustomerDocument>().SetSequence(1);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
            }
        }

        private static void DbCommand2RavenDB<T>(DbCommand command, ChinookRavenDB ravenDB)
        {
            using (IDocumentSession session = ravenDB.DocumentStore.OpenSession())
            {
                string entityName = typeof(T).Name;

                command.CommandText = "SELECT TOP 10 * FROM " + typeof(T).Name + " ORDER BY 1"; // ??? TOP N
                DbDataReader reader = command.ExecuteReader();
                Console.WriteLine("\n" + entityName);
                while (reader.Read())
                {
                    Console.WriteLine(entityName + " {0} - {1}", reader.GetValue(0), reader.GetValue(1));
                    T entity = reader.ToEntity<T>();

                    session.Store(entity);
                }
                reader.Close();

                session.SaveChanges();
            }
        }
    }
}