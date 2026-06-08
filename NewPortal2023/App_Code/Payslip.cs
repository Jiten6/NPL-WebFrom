using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Xml;
using System.IO;
using System.Data.OleDb;
using System.Text;
/// <summary>
/// Summary description for Payslip
/// </summary>
namespace NewPortal2023.ESS
{

    public class Payslip
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        public Payslip()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet FillMonths(string  strDir)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@XMLDIR", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, strDir);
               // objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMONTH");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_PAYSLIP");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet FillYear(string strDir)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@XMLDIR", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, strDir);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETYEAR");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_PAYSLIP");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetFNFStatus(string compValue, string userValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETFNFSTATUS");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_PAYSLIP");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public string UploadFNFStatusXL(string compValue, string strPath, string empValue)
        {
            string conn = string.Empty;
            string query = string.Empty;
            string status = string.Empty;
            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();

            conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            query = "SELECT * FROM [Sheet1$]";

            using (var connection = new OleDbConnection(conn))
            {
                using (var da = new OleDbDataAdapter(query, connection))
                {
                    connection.Open();
                    da.Fill(ds);
                }
            }

            status = "";

            StringBuilder sbTaxDetails = new StringBuilder();
            string xmlInv = string.Empty;

            sbTaxDetails.Append("<ROOT>");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                sbTaxDetails.Append("<Inv EMP='" + dr[0].ToString().Trim() + "'");
                sbTaxDetails.Append(" FNF='" + ReplaceSpecialCharacters(dr[1].ToString().Trim()) + "'/>");
            }

            sbTaxDetails.Append("</ROOT>");

            xmlInv = sbTaxDetails.ToString();

            status = UpdateFNFStatus(xmlInv, compValue, empValue);

            return status;
        }

        public string UpdateFNFStatus(string xmlValue, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;

            objDBUtility.AddParameters("@InvXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEFNFSTATUS");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_PAYSLIP");


            if (dsInv.Tables.Count > 0)
            {
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                    {
                        strresult = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["status"].ToString().Trim());
                    }
                }
            }

            objDBUtility.ClearTransactionalParams();

            return strresult;
        }

        private string ReplaceSpecialCharacters(string inputString)
        {
            inputString = inputString.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&#39;").
                    Replace("\"", "&quot;");

            return inputString;
        }

        public DataSet FillEmpList(string CompAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPLIST");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_PAYSLIP");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet FillEmpList(string EmpAid,string CompAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid);
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpAid);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPLISTSINGLE");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_PAYSLIP");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet GetEmpMid(string CompAid, string EmpAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid);
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpAid);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPMID");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_PAYSLIP");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        
    }
}