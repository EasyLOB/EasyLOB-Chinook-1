using Chinook.Data;
using Chinook.Persistence;
using EasyLOB.Data;
using EasyLOB.Persistence;
using ServiceStack.Redis.Generic;
using System;
using System.Data.Common;

namespace Chinook.Shell
{
    partial class Program
    {
        private static void SQLServer2Redis()
        {
            Console.WriteLine("\nChinook SQL Server => Redis");
            Console.Write("\nPress <Y> to execute... ");

            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar != 'y' && key.KeyChar != 'Y')
            {
                return;
            }
            Console.WriteLine();

            DbConnection connection = null;
            ChinookRedis chinook = new ChinookRedis();

            chinook.Client.FlushDb();

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

                // Chinook

                DbCommand2Redis<Album>(command, chinook);
                DbCommand2Redis<Artist>(command, chinook);
                DbCommand2Redis<Customer>(command, chinook);
                DbCommand2Redis<Employee>(command, chinook);
                DbCommand2Redis<Genre>(command, chinook);
                DbCommand2Redis<Invoice>(command, chinook);
                DbCommand2Redis<InvoiceLine>(command, chinook);
                DbCommand2Redis<MediaType>(command, chinook);
                DbCommand2Redis<Playlist>(command, chinook);
                DbCommand2Redis<PlaylistTrack>(command, chinook);
                DbCommand2Redis<Track>(command, chinook);

                //DbCommand2Redis<CustomerDocument>(command, chinook);

                // Sequence

                IUnitOfWork unitOfWork = new ChinookUnitOfWorkRedis();

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

                chinook.Client.Save();
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

        private static void DbCommand2Redis<T>(DbCommand command, ChinookRedis redis)
        {
            IRedisTypedClient<T> TypedClient = redis.Client.As<T>();

            string entityName = typeof(T).Name;

            command.CommandText = "SELECT TOP 10 * FROM " + typeof(T).Name + " ORDER BY 1"; // ??? TOP N
            DbDataReader reader = command.ExecuteReader();
            Console.WriteLine("\n" + entityName);
            while (reader.Read())
            {
                Console.WriteLine(entityName + " {0} - {1}", reader.GetValue(0), reader.GetValue(1));
                T entity = reader.ToEntity<T>();

                TypedClient.Store(entity);
            }
            reader.Close();
        }
    }
}