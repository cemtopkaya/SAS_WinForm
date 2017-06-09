using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace websrvcSAS
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class VeriAl : WebService
    {
        SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["dbSAS"].ConnectionString);
        public AuthHeader M_Authentication;

        [WebMethod]
        [SoapHeader("M_Authentication", Required = true)]
        public bool f_VeriAl(DataSet _ds)
        {
            try
            {
                if (cnn.State != ConnectionState.Open)
                {
                    cnn.Open();
                }

                foreach (DataTable dataTable in _ds.Tables)
                {
                    using (SqlBulkCopy copy = new SqlBulkCopy(cnn))
                    {
                        foreach (DataColumn dataColumn in dataTable.Columns)
                        {
                            copy.ColumnMappings.Add(dataColumn.ColumnName, dataColumn.ColumnName);
                        }
                        copy.DestinationTableName = dataTable.TableName;

                        copy.WriteToServer(dataTable);
                    }
                }
            }
            catch (Exception ex)
            {
                //--IS
                return false;
            }

            return true;
        }

    }
}
