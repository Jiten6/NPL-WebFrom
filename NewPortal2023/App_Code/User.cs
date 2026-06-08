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

namespace NewPortal2023.ESS
{
    public class User
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        //public UserRights()
        //{
        //    //
        //    // TODO: Add constructor logic here
        //    //
        //}

        public DataSet GetLeaveDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@PROF_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPROFILELIST");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_USERRIGHTS");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetEmpDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@PROF_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMENULIST");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_USERRIGHTS");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetEmpDetails(string compValue, string empValue, string empMid, string empName)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empMid.ToString().Trim());
                objDBUtility.AddParameters("@EMP_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empName.ToString().Trim());
                objDBUtility.AddParameters("@PROF_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMENULIST");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_USERRIGHTS");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetProfile(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPROFILE");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_USERRIGHTS");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public Boolean Update(string xmlValue, string CompId, string Empid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
                objDBUtility.AddParameters("@PROF_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
                objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEPROFILE");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_USERRIGHTS");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                return false;
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return true;
        }

        public Boolean UpdateEmp(string xmlValue, string CompId, string profID, string profDesc, string xmlAll = "")
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@XmlAll", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlAll.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@PROF_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, profID.ToString().Trim());
            objDBUtility.AddParameters("@PROF_DESC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, profDesc.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATERIGHTS");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_USERRIGHTS");

            objDBUtility.ClearTransactionalParams();

            return true;
        }
    }
}