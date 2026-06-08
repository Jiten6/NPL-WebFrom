using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class DataTransferOneServerToAnotherServer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        private void TransferDataAndBindGridView()
        {
            // Source and Destination connection strings
            string sourceConnectionString = "Data Source=183.82.0.227;Initial Catalog=etimetracklite1;User ID=Essl;Password=Micro@123;";
            string destinationConnectionString = "Data Source=162.215.230.14;Initial Catalog=NPLUAT;User ID=NPLUATUSER1;Password=npltest@1234;";

            // Query to select data from source table
            //string selectQuery = "SELECT EmployeeCode, LogDateTime, LogDate, LogTime, Direction FROM GrateHr where CONVERT(DATE,LOGDATE,105)  BETWEEN  CONVERT(DATE,GETDATE()-1,105) AND CONVERT(DATE,GETDATE()-1,105) ";
            string selectQuery = "SELECT EmployeeCode, LogDateTime, LogDate, LogTime, Direction FROM GrateHr where CONVERT(DATE,LOGDATETIME,105)  BETWEEN  CONVERT(DATE,'01-09-2024',105) AND CONVERT(DATE,GETDATE(),105)";

            // Query to insert data into destination table
            string insertQuery = "INSERT INTO GrateHrColonTable (EmployeeCode, LogDateTime, LogDate, LogTime, Direction) " +
                                 "VALUES (@EmployeeCode, @LogDateTime, @LogDate, @LogTime, @Direction)";

            // Transfer data from source to destination
            using (SqlConnection sourceConnection = new SqlConnection(sourceConnectionString))
            {
                SqlCommand selectCommand = new SqlCommand(selectQuery, sourceConnection);
                sourceConnection.Open();
                SqlDataReader reader = selectCommand.ExecuteReader();

                using (SqlConnection destinationConnection = new SqlConnection(destinationConnectionString))
                {
                    destinationConnection.Open();

                    while (reader.Read())
                    {
                        using (SqlCommand insertCommand = new SqlCommand(insertQuery, destinationConnection))
                        {
                            insertCommand.Parameters.AddWithValue("@EmployeeCode", reader["EmployeeCode"]);
                            insertCommand.Parameters.AddWithValue("@LogDateTime", reader["LogDateTime"]);
                            insertCommand.Parameters.AddWithValue("@LogDate", reader["LogDate"]);
                            insertCommand.Parameters.AddWithValue("@LogTime", reader["LogTime"]);
                            insertCommand.Parameters.AddWithValue("@Direction", reader["Direction"]);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }

            // Bind the destination table to the GridView
            BindGridView(destinationConnectionString);
        }

        private void BindGridView(string connectionString)
        {
            string selectDestinationQuery = "SELECT EmployeeCode, LogDateTime, LogDate, LogTime, Direction FROM GrateHrColonTable";

            using (SqlConnection destinationConnection = new SqlConnection(connectionString))
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter(selectDestinationQuery, destinationConnection);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);

                gvLocalClaimList.DataSource = dataTable;
                gvLocalClaimList.DataBind();
            }
        }

        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            TransferDataAndBindGridView();
        }
    }

}



    