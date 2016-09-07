using EasyLOB.Data;
using EasyLOB.Library.Resources;
using System;
using System.Web.Mvc;

/*
<Report>
  <DataSets>
    <DataSet Name = "Query">
      <Fields>
        <Field Name="ArtistId">
          <DataField>ArtistId</DataField>
          <rd:TypeName>System.Int32</rd:TypeName>
        </Field>
        <Field Name = "Name" >
          <DataField> Name </DataField>
          <rd:TypeName>System.String</rd:TypeName>
        </Field>
      </Fields>
      <Query>
        <DataSourceName>Chinook</DataSourceName>
        <CommandType>Text</CommandType>
        <CommandText>SELECT
    ArtistId
    , Name
FROM
    Artist
ORDER BY
    Artist.Name</CommandText>
      </Query>
    </DataSet>
  </DataSets>
</Report>
 */

// Syncfusion Report Viewer
// http://help.syncfusion.com/aspnetmvc/reportviewer/getting-started
// http://help.syncfusion.com/aspnetmvc/reportviewer/report-controller

namespace EasyLOB.Library.Mvc
{
    public partial class ReportsController : BaseControllerReport
    {
        [HttpGet]
        public ActionResult RDLC(string reportDirectory, string reportName)
        {
            OperationResultModel operationResultModel = new OperationResultModel();

            if (String.IsNullOrEmpty(reportDirectory) || String.IsNullOrEmpty(reportName))
            {
                operationResultModel.OperationResult.ErrorMessage = ErrorResources.RDL_Parameters;

                return View("OperationResult", operationResultModel);
            }
            else
            {
                if (!IsAuthorized(reportDirectory, reportName, operationResultModel.OperationResult))
                {
                    return View("OperationResult", operationResultModel);
                }
                else
                {
                    ReportModelRDLC reportModel = new ReportModelRDLC();

                    reportModel.ReportDirectory = reportDirectory;
                    reportModel.ReportName = reportName;
                    /*
                    // Query

                    XmlDocument xml = new XmlDocument();
                    string rdlcFileName = Path.Combine(Server.MapPath(LibraryHelper.AppSettings<string>("Report.RDLCDirectory")),
                        reportDirectory, reportName + ".rdlc");
                    xml.Load(rdlcFileName);
                    xml.InnerXml = xml.InnerXml.Replace("xmlns=", "xmlns:ns=");

                    string connectionName =
                        xml.DocumentElement.SelectSingleNode("/Report/DataSets/DataSet[@Name='Query']/Query/DataSourceName").InnerText;
                    if (!String.IsNullOrEmpty(connectionName))
                    {
                        string sql =
                            xml.DocumentElement.SelectSingleNode("/Report/DataSets/DataSet[@Name='Query']/Query/CommandText").InnerText;
                        if (!String.IsNullOrEmpty(sql))
                        {
                            DbConnection connection = null;

                            try
                            {
                                DbProviderFactory provider;

                                provider = AdoNetHelper.GetProvider(connectionName);
                                connection = provider.CreateConnection();
                                connection.ConnectionString = AdoNetHelper.GetConnectionString(connectionName);
                                connection.Open();

                                DbCommand command;
                                DbDataReader reader;

                                command = provider.CreateCommand();
                                command.Connection = connection;
                                command.CommandTimeout = 600;
                                command.CommandType = System.Data.CommandType.Text;
                                command.CommandText = sql;

                                reader = command.ExecuteReader();

                                DataTable table = new DataTable();
                                table.Load(reader);

                                // System.Data.EnumerableRowCollection<System.Data.DataRow>
                                // https://msdn.microsoft.com/en-us/library/system.data.enumerablerowcollection%28v=vs.110%29.aspx
                                // System.Data.DataRow
                                // https://msdn.microsoft.com/en-us/library/system.data.datarow%28v=vs.110%29.aspx
                                //IEnumerable data = table.AsEnumerable();

                                reportModel.Data = table.AsEnumerable();
                            }
                            catch (Exception exception)
                            {
                                reportModel.OperationResult.AddOperationError(exception);
                            }
                            finally
                            {
                                if (connection != null)
                                {
                                    connection.Close();
                                }
                            }
                        }
                    }
                     */
                    return View("RDLC", reportModel);
                }
            }
        }
    }
}