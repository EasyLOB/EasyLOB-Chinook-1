using Chinook.Data;
using EasyLOB.Data;
using EasyLOB.Library;
using EasyLOB.Library.Syncfusion;
using EasyLOB.Persistence;
using Syncfusion.XlsIO;
using System;
using System.Data.Common;
using System.IO;

namespace Chinook.Application
{
    public partial class ChinookApplication : IChinookApplication
    {
        #region Methods

        public bool ExportAlbumByArtistXLSX(ZOperationResult operationResult, string templateDirectory, string fileDirectory,
            out string filePath)
        {
            string template = "AlbumByArtist";
            string templatePath = LibraryHelper.AddDirectorySeparator(templateDirectory) + template + ".xlsx";
            string file = template + "." + String.Format("{0:yyyyMMdd.HHmmssfff}", DateTime.Now) + ".xlsx";
            filePath = Path.Combine(fileDirectory, file);

            DbConnection connection = null;

            ExcelEngine excelEngine = null;

            try
            {
                DbProviderFactory provider;
                string connectionName = "Chinook";

                provider = AdoNetHelper.GetProvider(connectionName);
                connection = provider.CreateConnection();
                connection.ConnectionString = AdoNetHelper.GetConnectionString(connectionName);
                connection.Open();

                DbCommand command;
                DbDataReader reader;
                //DbParameter parameter;

                command = provider.CreateCommand();
                command.Connection = connection;
                command.CommandTimeout = 600;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"
SELECT
    Artist.ArtistId
    ,Artist.Name ArtistName
    ,Album.AlbumId
    ,Album.Title AlbumTitle
FROM
    Artist
    LEFT JOIN Album ON
        Album.ArtistId = Artist.ArtistId
ORDER BY
    Artist.Name
    ,Album.Title
";

                //parameter = command.CreateParameter();
                //parameter.DbType = DbType.DateTime;
                //parameter.ParameterName = "@Data";
                //parameter.Value = viewModel.XDateTime;
                //command.Parameters.Add(parameter);

                File.Copy(templatePath, filePath);

                excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                IWorkbook workbook = application.Workbooks.Open(filePath);
                //workbook.Version = ExcelVersion.Excel2013;
                IWorksheet worksheet;

                // Sintético

                worksheet = workbook.Worksheets[0];

                worksheet.Range[1, 4].Value2 = String.Format("{0:dd/MM/yyyy}", DateTime.Today);

                // Analítico

                worksheet = workbook.Worksheets[2];

                reader = command.ExecuteReader();

                int row = 2, column;
                while (reader.Read())
                {
                    column = 1;

                    worksheet.Range[row, column++].Value2 = reader.ToInt32("ArtistId");
                    worksheet.Range[row, column++].Value2 = reader.ToString("ArtistName");
                    worksheet.Range[row, column++].Value2 = reader.ToInt32("AlbumId");
                    worksheet.Range[row, column++].Value2 = reader.ToString("AlbumTitle");

                    row++;
                }

                reader.Close();

                worksheet.AutoAlign(1, 4);
                workbook.Save();
                workbook.Close();
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }
            finally
            {
                if (excelEngine != null)
                {
                    excelEngine.Dispose();
                }

                if (connection != null)
                {
                    connection.Close();
                }
            }

            return operationResult.Ok;
        }

        public bool ExportGenreXLSX(ZOperationResult operationResult, string fileDirectory,
            out string filePath)
        {
            string template = "Genre";
            string file = template + "." + String.Format("{0:yyyyMMdd.HHmmssfff}", DateTime.Now) + ".xlsx";
            filePath = Path.Combine(fileDirectory, file);

            DbConnection connection = null;

            ExcelEngine excelEngine = null;

            try
            {
                DbProviderFactory provider;
                string connectionName = "Chinook";

                provider = AdoNetHelper.GetProvider(connectionName);
                connection = provider.CreateConnection();
                connection.ConnectionString = AdoNetHelper.GetConnectionString(connectionName);
                connection.Open();

                DbCommand command;
                DbDataReader reader;
                //DbParameter parameter;

                command = provider.CreateCommand();
                command.Connection = connection;
                command.CommandTimeout = 600;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = @"
SELECT
    GenreId
    ,Name
FROM
    Genre
ORDER BY
    Genre.Name
";

                //parameter = command.CreateParameter();
                //parameter.DbType = DbType.DateTime;
                //parameter.ParameterName = "@Data";
                //parameter.Value = viewModel.XDateTime;
                //command.Parameters.Add(parameter);

                excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                IWorkbook workbook = application.Workbooks.Create(1);
                //workbook.Version = ExcelVersion.Excel2013;
                IWorksheet worksheet = workbook.Worksheets[0];

                worksheet.Range[1, 1].Text = "GenreId";
                worksheet.Range[1, 2].Text = "Name";

                reader = command.ExecuteReader();

                int row = 2;

                while (reader.Read())
                {
                    int column = 1;

                    worksheet.Range[row, column++].Value2 = reader.ToInt32("GenreId");
                    worksheet.Range[row, column++].Value2 = reader.ToString("Name");

                    row++;
                }

                reader.Close();

                worksheet.AutoAlign(1, 2);
                workbook.SaveAs(filePath);
                workbook.Close();
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }
            finally
            {
                if (excelEngine != null)
                {
                    excelEngine.Dispose();
                }

                if (connection != null)
                {
                    connection.Close();
                }
            }

            return operationResult.Ok;
        }

        public bool ImportGenreXLSX(ZOperationResult operationResult, string filePath, IChinookGenericApplication<Genre> genreApplication)
        {
            // Column A: GenreId
            // Column B: Name
            // Row 1: Column Names

            ExcelEngine excelEngine = null;

            try
            {
                excelEngine = new ExcelEngine();
                IApplication application = excelEngine.Excel;
                application.DefaultVersion = ExcelVersion.Excel2013;
                IWorkbook workbook = excelEngine.Excel.Workbooks.Open(filePath);
                IWorksheet worksheet = workbook.Worksheets[0];

                int row = 2;
                while (true)
                {
                    int column = 1;

                    if (String.IsNullOrEmpty(worksheet.Range[row, column].Value2 as string))
                    {
                        break;
                    }

                    Genre genre = new Genre();
                    genre.GenreId = (int)worksheet.Range[row, column++].Value2;
                    genre.Name = (string)worksheet.Range[row, column++].Value2;

                    Genre genreById = genreApplication.GetById(operationResult, genre.GenreId);

                    if (!operationResult.Ok)
                    {
                        break;
                    }

                    if (genreById == null)
                    {
                        genreApplication.Create(operationResult, genre);
                    }
                    else
                    {
                        genreApplication.Update(operationResult, genre);
                    }

                    row++;
                }

                workbook.Close();
            }
            catch (Exception exception)
            {
                operationResult.ParseException(exception);
            }
            finally
            {
                if (excelEngine != null)
                {
                    excelEngine.Dispose();
                }
            }

            return operationResult.Ok;
        }

        #endregion Methods
    }
}