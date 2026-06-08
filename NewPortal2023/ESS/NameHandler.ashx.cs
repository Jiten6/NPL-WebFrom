using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Script.Serialization;

namespace NewPortal2023.ESS
{
    /// <summary>
    /// Summary description for NameHandler
    /// </summary>
    public class NameHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string term = context.Request["term"] ?? "";
            List<string> listStudentNames = new List<string>();

            string cs = ConfigurationManager.ConnectionStrings["sqloledb"].ConnectionString;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand("SP_EXPENSESNAME", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter()
                {
                    ParameterName = "@term",
                    Value = term
                });
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    listStudentNames.Add(rdr["emp_fname"].ToString());
                }
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            context.Response.Write(js.Serialize(listStudentNames));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}