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

/// <summary>
/// Summary description for Support
/// </summary>
/// 
namespace NewPortal2023.ESS
{

    public class Support
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        public Support()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet FillYear()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETYEAR");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_SUPPORT");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet Upload_Support(string compValue, string empValue, string fileName, string invYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FILE_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, fileName.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, invYear.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "HISTORYSUPPORT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_SUPPORT");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet Upload_Support_Password(string compValue, string empValue, string fileName, string invYear, string password)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FILE_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, fileName.ToString().Trim());
            objDBUtility.AddParameters("@PASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 100, password.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, invYear.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "HISTORYSUPPORTPWD");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_SUPPORT");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet Upload_Support_Flexi(string compValue, string empValue, string fileName, string invYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FILE_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, fileName.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, invYear.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "HISTORYSUPPORTFLEXI");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_SUPPORT");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet Upload_Support_Delete(string compValue, string empValue, string fileName, string invYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FILE_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, fileName.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, invYear.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "HISTORYSUPPORTDELETE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_SUPPORT");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet Upload_Declaration(string compValue, string empValue, string fileName, string invYear, string password)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FILE_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, fileName.ToString().Trim());
            objDBUtility.AddParameters("@PASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 100, password.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, invYear.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "HISTORYDECLARATIONFILE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_SUPPORT");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
    }
}