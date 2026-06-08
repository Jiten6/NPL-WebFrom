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

    public class LeaveApplication
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;
        private NewPortal2023.ESS.Common objcommon;
        //ESS.Common objcommon = new ESS.Common();

        public LeaveApplication()
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

        public DataSet GetLeave(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue);
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLeave");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPP");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
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

        public DataTable GetPLLeave(string compValue, string empValue, string status, DropDownList drptype)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            //objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status);
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPLLEAVE");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPP");



            objDBUtility.ClearTransactionalParams();

            dt = dsInv.Tables[0];

            drptype.Items.Clear();
            drptype.DataTextField = "Leave";
            drptype.DataValueField = "cid";
            drptype.DataSource = dsInv.Tables[0];
            drptype.DataBind();
            drptype.Items.Insert(0, new ListItem("", ""));
            drptype.Items.Insert(0, new ListItem("[Select One]", ""));

            objDBUtility.ClearTransactionalParams();

            return dt;

        }

        public DataTable GetFinancialYearList(string compValue, string empValue, string allId, DropDownList drpSelectFinancialYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETFINANCIALYEAR");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            dt = dsInv.Tables[1];

            drpSelectFinancialYear.Items.Clear();
            drpSelectFinancialYear.DataTextField = "DES";
            drpSelectFinancialYear.DataValueField = "AID";
            drpSelectFinancialYear.DataSource = dsInv.Tables[1];
            drpSelectFinancialYear.DataBind();
            drpSelectFinancialYear.Items.Insert(0, new ListItem("[Select One]", ""));
            //drpSelectFinancialYear.Items.Add(new ListItem("Other", "9999"));

            objDBUtility.ClearTransactionalParams();

            return dt;
        }

        public DataTable GetMonthList(string compValue, string empValue, string allId, DropDownList drpSelectMonth)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETFINANCIALYEAR");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            dt = dsInv.Tables[2];

            drpSelectMonth.Items.Clear();
            drpSelectMonth.DataTextField = "DES";
            drpSelectMonth.DataValueField = "AID";
            drpSelectMonth.DataSource = dsInv.Tables[2];
            drpSelectMonth.DataBind();
            drpSelectMonth.Items.Insert(0, new ListItem("[Select One]", ""));
            //drpSelectFinancialYear.Items.Add(new ListItem("Other", "9999"));

            objDBUtility.ClearTransactionalParams();

            return dt;
        }


        public DataSet GetLeaveDetails(string compValue, string empValue, string NewYear, string OldYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETHISTORY");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPP");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetDetails(string compValue, string empValue, string CID)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDETAILS");
                objDBUtility.AddParameters("@CID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CID);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetLeaveTypeDetails(string compValue, string empValue, string selectedYear, string leaveType)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();




            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEAVETYPEDETAILS");
            objDBUtility.AddParameters("@LEAVE_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leaveType);
            objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, selectedYear);

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPP");



            objDBUtility.ClearTransactionalParams();
            return dsInv;
        }

        public DataSet UpdateAttend(string compValue, string empValue, string SHR, string InTime, string OutTime, string EmpCode, string Name, string Date, string Remark, string oldShift, string oldTimeIn)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;
            string strurl = string.Empty;
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode);
                objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Name);
                objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Date);

                objDBUtility.AddParameters("@SHIFTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SHR);
                objDBUtility.AddParameters("@INTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, InTime);
                objDBUtility.AddParameters("@OLDSHIFTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, oldShift);
                objDBUtility.AddParameters("@OLDINTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, oldTimeIn);
                objDBUtility.AddParameters("@INOUT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OutTime);

                objDBUtility.AddParameters("@REMARKS ", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Remark);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 100, "INSERTATTENDANCE");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");




                objDBUtility.ClearTransactionalParams();


            }

            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;
        }

        //-------------------------------------------------------NEW SP FOR TESTDEMO-------------------------------------------------------//

        public DataSet GetDetailEmps(string compValue, string empValue, string CID)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDETAILEMPS");
                objDBUtility.AddParameters("@CID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CID);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("[COMMON_SP_GETDETAILEMPS]");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public string UpdateLeaveStatus(string compValue, string empValue, string status, string CID, string fromdate, string todate, string reason, string address, string rem, string NewYear, string Leave, string leaveDays)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;
            string strurl = string.Empty;
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
                objDBUtility.AddParameters("@LEAVEDAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leaveDays);
                objDBUtility.AddParameters("@CID ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CID);
                objDBUtility.AddParameters("@LEAVE ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Leave);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATESTATUS");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");


                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            strresult = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["status"].ToString().Trim());
                            strurl = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["url"].ToString().Trim());
                        }
                    }
                }

                objDBUtility.ClearTransactionalParams();

                if (strurl != "")
                {
                    objcommon.SendSMS(strurl);
                }

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return strresult;
        }
        public string UpdateLeave(string compValue, string empValue, string fromdate, string todate, string reason, string address, string rem, string status, string NewYear, string Leave, string leaveDays)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;
            string strurl = string.Empty;
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
                objDBUtility.AddParameters("@LEAVEDAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leaveDays);
                objDBUtility.AddParameters("@LEAVE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Leave);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATE");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPP");


                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            strresult = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["status"].ToString().Trim());
                            strurl = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["url"].ToString().Trim());
                        }
                    }
                }

                objDBUtility.ClearTransactionalParams();

                if (strurl != "")
                {
                    objcommon.SendSMS(strurl);
                }
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return strresult;
        }

        public void CancelLeave(string cid)
        {
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CANCELLEAVE");

            objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPP");



            objDBUtility.ClearTransactionalParams();
        }
        public DataSet GetPLOpeningBal(string compValue, string empValue, string NewYear, string NewMonth, string Status)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEAVETYPE");
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
            objDBUtility.AddParameters("@MONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewMonth);
            objDBUtility.AddParameters("@LEAVE_CODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status);

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPP");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet UpdateEnacashment(string compValue, string empValue, string NewYear, string NewMonth, string Status, string encash, string bal)
        {
            objDBUtility = new DBUtility();
            DataSet dsInv = new DataSet();
            
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                objDBUtility.AddParameters("@MONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewMonth);
                objDBUtility.AddParameters("@LEAVE_CODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
                //objDBUtility.AddParameters("@REASON_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, plopening.ToString().Trim());
                objDBUtility.AddParameters("@ENCASHED", DBUtilDBType.Varchar, DBUtilDirection.In, 50, encash.ToString().Trim());
                objDBUtility.AddParameters("@CLOSING_BAL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, bal.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEENCASH");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPP");
                objDBUtility.ClearTransactionalParams();
                return dsInv;

        }

        public DataSet GetId(string compValue, string empValue, string fromdate, string todate, string reason, string address, string rem, string status, string NewYear, string Leave, string leaveDays)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

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
                objDBUtility.AddParameters("@LEAVEDAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leaveDays);
                objDBUtility.AddParameters("@LEAVE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Leave);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETID");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPP");

                objDBUtility.ClearTransactionalParams();

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        internal DataSet Fillemployee()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPLIST");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {

            }

            return dsInv;
        }

        internal DataSet GetLeaveData(string empCode, string leaveCode, string year, string month)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
                objDBUtility.AddParameters("@LEAVEC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leaveCode);
                objDBUtility.AddParameters("@LEAVEYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, year);
                objDBUtility.AddParameters("@LEAVEMONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, month);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEAVEDATA");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {

            }

            return dsInv;
        }


        internal DataSet UpdateLeaveData(string empCode, string leaveCode, string year, string month,
            string openingBalance, string availed, string closingBalance)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
                objDBUtility.AddParameters("@LEAVEC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leaveCode);
                objDBUtility.AddParameters("@LEAVEYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, year);
                objDBUtility.AddParameters("@LEAVEMONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, month);
                objDBUtility.AddParameters("@OB", DBUtilDBType.Varchar, DBUtilDirection.In, 50, openingBalance);
                objDBUtility.AddParameters("@AV", DBUtilDBType.Varchar, DBUtilDirection.In, 50, availed);
                objDBUtility.AddParameters("@CB", DBUtilDBType.Varchar, DBUtilDirection.In, 50, closingBalance);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATELEAVEDATA");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {

            }

            return dsInv;
        }

        public DataSet GETLEAVESLIP(string compValue, string empValue, string fromDate, string toDate, string month)
        {
            objDBUtility = new DBUtility();
            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue);
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue);
            objDBUtility.AddParameters("@FROM_DT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromDate);
            objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDate);
            objDBUtility.AddParameters("@MONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, month);
            //objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, year);
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEAVESLIP");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet getEmpId(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPMID");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

    }
}