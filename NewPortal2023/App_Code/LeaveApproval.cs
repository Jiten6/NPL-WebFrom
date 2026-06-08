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
using System.Globalization; /// <summary>
/// <summary>
/// Summary description for LeaveApplication
/// </summary>
namespace NewPortal2023.ESS
{
    public class LeaveApproval
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        public LeaveApproval()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet GetType(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETType");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPP");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet GetLeaveDetails(string compValue, string empValue, string NewYear, string OldYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVAL");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public string UpdateLeave(string compValue, string empValue, string fromdate, string todate, string reason, string address, string rem, string status, string NewYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;
            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                objDBUtility.AddParameters("@FROM_DT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromdate);
                objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, todate);
                objDBUtility.AddParameters("@REASON_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, reason);
                objDBUtility.AddParameters("@ADDRESS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, address);
                objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rem);
                objDBUtility.AddParameters("@STATUS ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status);
                objDBUtility.AddParameters("@FINYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATE");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPP");


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
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return strresult;
        }

        public DataSet GetStatus(string status)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GetStatus");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetApprLeaveDetails(string compValue, string empValue, string NewYear, string OldYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVEDLIST");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetApprODDetails(string compValue, string empValue, string NewYear, string OldYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPRODLIST");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetApprAttendOTCO(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPRATTENDOTCO");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("[COMMON_SP_LEAVEAPPROVAL]");

            objDBUtility.ClearTransactionalParams();
            return dsInv;
        }

        public DataSet GetLeaveCardData(string year, string month)
        {
            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@YEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, year);
            objDBUtility.AddParameters("@MONTHS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, month);
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GET_LEAVE_CARD");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

    }
}
