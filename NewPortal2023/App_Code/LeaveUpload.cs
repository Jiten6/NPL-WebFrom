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
using System.Data.OleDb;
using System.Text;
/// <summary>
/// Summary description for LeaveUpload
/// </summary>
namespace NewPortal2023.ESS
{

    public class LeaveUpload
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        public LeaveUpload()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public DataSet GetShift(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSHIFT");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet Fill_leave_status(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet ds = new DataSet();
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "LEAVE_STATUS");
            ds = objDBUtility.Execute_StoreProc_DataSet("[COMMON_SP_LEAVEAPPROVAL]");

            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        public DataSet GETATTENDAPPROVAL(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDAPPROVAL");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("[COMMON_SP_LEAVEAPPROVAL]");
            
            objDBUtility.ClearTransactionalParams();
            return dsInv;
        }

        public string GetFinalcialYear()
        {
            string CurrFin = string.Empty;
            string PreFin;

            if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 3)
            {
                CurrFin = Convert.ToString(DateTime.Now.Year - 1) + Convert.ToString(DateTime.Now.Year);
                PreFin = Convert.ToString(DateTime.Now.Year - 2) + Convert.ToString(DateTime.Now.Year - 1);
            }
            else
            {
                CurrFin = Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Year + 1);
                PreFin = Convert.ToString(DateTime.Now.Year - 1) + Convert.ToString(DateTime.Now.Year);
            }
            return CurrFin + '.' + PreFin;
        }

       
        //public Boolean UpdateAttendanceStatus(string xmlValue, string status, string CompId, string Empid)
        //{
        //    objDBUtility = new DBUtility();

        //    DataSet dsInv = new DataSet();

        //    objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
        //    objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status.ToString().Trim());
        //    objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEATTENDANCE");
        //    objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
        //    objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());

        //    dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

        //    objDBUtility.ClearTransactionalParams();

        //    return true;
        //}

        public DataSet GetLeaveDetails(string compValue, string empValue, string NewYear,string fromDt,string toDt)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLeave");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                objDBUtility.AddParameters("@FromDt", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromDt );
                objDBUtility.AddParameters("@ToDate", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDt);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetAttendanceDetails(string compValue, string empValue, string NewYear, string fromDt, string toDt)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDANCEDEATILS");
               // objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                //objDBUtility.AddParameters("@FromDt", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromDt);
                //objDBUtility.AddParameters("@ToDate", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDt);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVECARD");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetOTCOReport(string compValue, string empValue, string fromDt, string toDt)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

          
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "REPORTINGGENERATEOTCO");
            
                objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, fromDt.ToString().Trim());
                objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, toDt.ToString().Trim());

                dsInv = objDBUtility.Execute_StoreProc_DataSet("[SP_NPL]");
                objDBUtility.ClearTransactionalParams();
            

            return dsInv;
        }

        public DataSet GetReport(string compValue, string empValue, string fromDt, string toDt,string reportType)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@RPTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, reportType.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREPORTOTCO");

            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, fromDt.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, toDt.ToString().Trim());

            dsInv = objDBUtility.Execute_StoreProc_DataSet("[SP_NPL]");
            objDBUtility.ClearTransactionalParams();


            return dsInv;
        }

        public DataSet GetOTCOStatus(string compValue, string empValue, string fromDt, string toDt)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
           // objDBUtility.AddParameters("@RPTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, reportType.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETOTANDCOSTATUS");

            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, fromDt.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, toDt.ToString().Trim());

            dsInv = objDBUtility.Execute_StoreProc_DataSet("[SP_NPL]");
            objDBUtility.ClearTransactionalParams();


            return dsInv;
        }

        

        public DataSet GetOTAndCOReportEmpWise(string empCode, string fromDate, string toDate,string reportType)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            
           // objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, fromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, toDate.ToString().Trim());
            objDBUtility.AddParameters("@RPTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, reportType.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "REPORTOTANDCOEMPWISE");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("[SP_NPL]");
            objDBUtility.ClearTransactionalParams();


            return dsInv;
        }
      

        public DataSet UpDateOTAndCOReportEmpWise(string empCode, string fromDate, string toDate, string TimeIn, string empName, string date, string oT, string cO, string action, string remarks,string rptType)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            // objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, fromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, toDate.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 500, empName.ToString().Trim());
            objDBUtility.AddParameters("@DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, date.ToString().Trim());
            objDBUtility.AddParameters("@INTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 500, TimeIn.ToString().Trim());
            objDBUtility.AddParameters("@OTHRS", DBUtilDBType.Varchar, DBUtilDirection.In, 500, oT.ToString().Trim());
            objDBUtility.AddParameters("@COHRS", DBUtilDBType.Varchar, DBUtilDirection.In, 500, cO.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, action.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 500, remarks.ToString().Trim());
            objDBUtility.AddParameters("@RPTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rptType.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEREPORTOTANDCOEMPWISE");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("[SP_NPL]");
            objDBUtility.ClearTransactionalParams();


            return dsInv;
        }

        public DataSet GetOTCOReportByFilter(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "REPORTINGGENERATEOTCO");

            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, From.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, To.ToString().Trim());

            dsInv = objDBUtility.Execute_StoreProc_DataSet("[SP_NPL]");
            objDBUtility.ClearTransactionalParams();


            return dsInv;
        }

        public DataSet GetAttendance(string compValue, string empValue, string fromDt, string toDt)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
              
                objDBUtility.AddParameters("@FromDt", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromDt);
                objDBUtility.AddParameters("@ToDate", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDt);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "AttendanceRPT");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet GetTillAttendance(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "AttendanceTillRPT");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetAttendanceEditDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "AttendanceRPTEdit");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;

        }
        public DataSet GetAttendanceEditEmpAid(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "AttendanceRPTEditEmpAid");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;

        }

        public DataSet GetAttendanceEdit(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "AttendanceRPTEdit");
                //objDBUtility.AddParameters("@FromDt", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromDt);
                //objDBUtility.AddParameters("@ToDate", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDt);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetAttendanceEditApproval(string compValue, string empValue, string fromDt, string toDt)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "AttendanceRPTApproval");
                objDBUtility.AddParameters("@FromDt", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromDt);
                objDBUtility.AddParameters("@ToDate", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDt);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetAttendanceEditApprove(string compValue, string empValue, string cid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@ENTRY_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cid.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "AttendanceRPTApprove");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetAttendanceEditReject(string compValue, string empValue, string cid, string remarks)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@ENTRY_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cid.ToString().Trim());
                objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, remarks.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "AttendanceRPTReject");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetLeaveBalance(string compValue, string empValue, string month)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@PAY_MONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, month.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEAVEBALANCE");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");

            return dsInv;
        }

        public DataSet GetAbscent(string compValue, string empValue, string NewYear, string fromDt, string toDt)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "AbscentRPT");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                objDBUtility.AddParameters("@FromDt", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromDt);
                objDBUtility.AddParameters("@ToDate", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDt);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet GetLateReport(string compValue, string empValue, string NewYear, string fromDt, string toDt)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "LateRPT");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                objDBUtility.AddParameters("@FromDt", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromDt);
                objDBUtility.AddParameters("@ToDate", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDt);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetLessReport(string compValue, string empValue, string NewYear, string fromDt, string toDt)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "LessRPT");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                objDBUtility.AddParameters("@FromDt", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromDt);
                objDBUtility.AddParameters("@ToDate", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDt);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public void UploadAttendance(string xml)
        {
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xml.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEATT");

            objDBUtility.Execute_StoreProc("COMMON_SP_LEAVEUPLOAD");

            objDBUtility.ClearTransactionalParams();
        }

        public string  UploadLeave(string compValue, string NewYear, string dtUpload,string strPath,string shift)
        {
            string conn = string.Empty;
            string query = string.Empty;
            string status =string.Empty ; 
            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();
            try
            {
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

                status ="";
            }
            catch(Exception ex)
            {
                status = "Select valid excel file..";
            }

            try
            {
                DataTable dt=new DataTable ();
                StringBuilder sbDetails = new StringBuilder();

                if (ds!=null )
                {
                    if (ds.Tables.Count >0 ) 
                    {
                        dt=ds.Tables [0];

                        if (dt.Columns[0].ColumnName != "COMP_AID")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[1].ColumnName != "EMP_AID")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[2].ColumnName != "IN TIME")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[3].ColumnName != "OUT TIME")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        
                        for (Int32 cnt = 0; cnt <= dt.Rows.Count - 1; cnt++)
                        {
                            if (Convert.ToString(dt.Rows[cnt]["COMP_AID"]) != compValue)
                            {
                                status = "Company ID does not match";
                                return status;
                            }
                            if (Convert.ToDateTime(dt.Rows[cnt]["IN TIME"]).TimeOfDay > Convert.ToDateTime(dt.Rows[cnt]["OUT TIME"]).TimeOfDay)
                            {
                                status = "Out Time cannot be less than In Time";
                                return status;
                            }

                        }
                        sbDetails.Append("<ROOT>");
                        for(Int32 cnt=0;cnt<=dt.Rows.Count -1;cnt++)
                        {
                            sbDetails.Append("<up COMP_AID='" + ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["COMP_AID"])) + "'");
                            sbDetails.Append(" EMP_AID='" + ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["EMP_AID"])) + "'");
                            sbDetails.Append(" ATT_DATE='" + ReplaceSpecialCharacters(dtUpload)  + "'");
                            sbDetails.Append(" INTIME='" + ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["IN TIME"])) + "'");
                            sbDetails.Append(" OUTTIME='" + ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["OUT TIME"])) + "'");
                            sbDetails.Append(" FINYEAR='" + ReplaceSpecialCharacters(NewYear)  + "'/>");

                        }
                        sbDetails.Append("</ROOT>");

                        objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                        objDBUtility.AddParameters("@SHIFT_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, shift.ToString().Trim());
                        objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATE");
                        objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                        objDBUtility.AddParameters("@DTUPLOAD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, dtUpload);
                        objDBUtility.AddParameters("@xml", DBUtilDBType.Varchar, DBUtilDirection.In, 1000000,Convert.ToString (  sbDetails ));
                        ds = new DataSet();
                        ds = objDBUtility.Execute_StoreProc_DataSet ("COMMON_SP_LEAVEUPLOAD");

                        if (ds != null)
                        {
                            if (ds.Tables.Count > 0)
                            {
                                dt = ds.Tables[0];

                                for (Int32 cnt = 0; cnt <= dt.Rows.Count - 1; cnt++)
                                {
                                    if (Convert.ToString(dt.Rows[cnt]["result"]) != "")
                                    {
                                        status = Convert.ToString(dt.Rows[cnt]["result"]);
                                    }
                                }
                            }
                        }


                        objDBUtility.ClearTransactionalParams();
                        if (status == "")
                        {
                            status = "Successfuly uploaded time sheet.";
                        }
                        else
                        {
                            return status;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains("The string was not recognized as a valid DateTime.") == true)
                {
                    status = "Invalid Date/Time.";
                }
                else
                {
                    status = "Error in application.";
                }
            }
            return status; 
        }

        protected string ReplaceSpecialCharacters(string inputString)
        {
            inputString = inputString.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&#39;").
                    Replace("\"", "&quot;");

            return inputString;
        }

        public void FillMonth(DropDownList drpParam)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMONTHLEAVEBALANCE");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEUPLOAD");

            drpParam.Items.Clear();
            drpParam.DataTextField = "MONYEARNAME";
            drpParam.DataValueField = "MONYEARCODE";
            drpParam.DataSource = ds;
            drpParam.DataBind();
            drpParam.Items.Insert(0, new ListItem("[Select One]", "0"));

            objDBUtility.ClearTransactionalParams();
        }

        public Boolean UpdateAttendanceStatus(string EntryAId, string Remark, string status, string CompId, string Empid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EntryAId.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Remark == null ? "" : Remark.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEATTENDANCE");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@SHIFTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Shift == null ? "" : Shift.ToString().Trim());
            objDBUtility.AddParameters("@INTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, From == null ? "" : From.ToString().Trim());
            objDBUtility.AddParameters("@INOUT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, To == null ? "" : To.ToString().Trim());
            objDBUtility.AddParameters("@OTCNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OT == null ? "" : OT.ToString().Trim());
            objDBUtility.AddParameters("@COCNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CO == null ? "" : CO.ToString().Trim());

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return true;
        }
        public Boolean UpdateAttendanceWithoutEntryAid(string EntryAId, string Remark, string status, string CompId, string Empid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EntryAId.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Remark == null ? "" : Remark.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEATTENDANCE");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
          

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return true;
        }
        public DataSet UpdateAttendanceOTANDCOWithoutEntryAid(string EntryAId, string Remark, string status, string CompId, string Empid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EntryAId.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Remark == null ? "" : Remark.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEATTENDANCE");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());


            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }
        string _From;
        string _To;
        string _Type;
        string _Status;
        string _OT;
        string _CO;
        string _Shift;

        public string Shift
        {
            get { return _Shift; }
            set { _Shift = value; }
        }
        
        public string CO
        {
            get { return _CO; }
            set { _CO = value; }
        }
       
        public string OT
        {
            get { return _OT; }
            set { _OT = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string From
        {
            get { return _From; }
            set { _From = value; }
        }
        public string To
        {
            get { return _To; }
            set { _To = value; }
        }
    }
}