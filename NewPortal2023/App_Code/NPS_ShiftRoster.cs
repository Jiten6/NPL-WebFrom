using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{

    public class NPS_ShiftRoster
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        NewPortal2023.ESS.Common objCommon;

        public NPS_ShiftRoster()
        {
            //
            // TODO: Add constructor logic here

            //
        }

        public string UploadShiftRosterData(string strPath, string strRoot)
        {
            string conn = string.Empty;
            string query = string.Empty;
            string status = string.Empty;

            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();
            objCommon = new Common();
            try
            {
                conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
                //query = "SELECT * FROM [Upload$A3:BY]";
                query = "SELECT * FROM [Upload$]";

                using (var connection = new OleDbConnection(conn))
                {
                    using (var da = new OleDbDataAdapter(query, connection))
                    {
                        connection.Open();
                        da.Fill(ds);
                    }
                }

                status = "";
            }


            catch (Exception ex)
            {
                status = ex.Message;
            }

            try
            {
                DataTable dt = new DataTable();
                StringBuilder sbDetails = new StringBuilder();

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Columns[1].ColumnName != "Emp_Name")
                        {
                            status = "Incorrect file format";
                            return status;
                        }

                        //if (dt.Columns[2].ColumnName != "Emp_Name")
                        //{
                        //    status = "Incorrect file format";
                        //    return status;
                        //}
                        //if (dt.Columns[3].ColumnName != "Date")
                        //{
                        //    status = "Incorrect file format";
                        //    return status;
                        //}
                        //if (dt.Columns[3].ColumnName != "Shift_Schedule")
                        //{
                        //    status = "Incorrect file format";
                        //    return status;
                        //}
                        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    }

                    if (status == "")
                    {
                        sbDetails.Append("<ROOT>");

                        for (int cnt = 0; cnt <= dt.Rows.Count - 1; cnt++)
                        {

                            string SR_No = Convert.ToString(dt.Rows[cnt]["Emp_Name"]);
                            if (SR_No == "")
                            {
                                break;
                            }
                            else
                            {
                                for (int cntCol = 0; cntCol <= dt.Columns.Count - 1; cntCol++)
                                {
                                    if (cntCol != 0 && cntCol != 1)
                                    {
                                        sbDetails.Append("<UP Emp_Code='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Emp_Code"])) + "'");
                                        sbDetails.Append(" Emp_Name='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Emp_Name"])) + "'");
                                        sbDetails.Append(" Date='" + (Convert.ToString(dt.Columns[cntCol])) + "'");
                                        sbDetails.Append(" Shift_Schedule='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt][cntCol])) + "'/>");
                                    }
                                }


                            }
                            // }

                        }

                        sbDetails.Append("</ROOT>");

                        objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 1000000000, sbDetails.ToString());
                        objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpCode.ToString());
                        objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, EmpName.ToString());

                        objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPLOADEXCELSHIFTROSTERDATA");

                        ds = new DataSet();
                        ds = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");


                        objDBUtility.ClearTransactionalParams();
                        if (Convert.ToString(ds.Tables[0].Rows[0]["Msg"]) == "Already Uploaded Shift Roster Data For This Month")
                        {
                            status = "Already Uploaded Shift Roster Data For This Month.";
                        }
                        else
                        {

                            if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) != "")
                            {
                                status = Convert.ToString(ds.Tables[1].Rows[0]["result"]);
                            }
                            else if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) == "")
                            {
                                status = "Successfuly uploaded Shift Roster Data.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }



        public string UploadAttendanceData(string strPath, string strRoot)
        {
            string conn = string.Empty;
            string query = string.Empty;
            string status = string.Empty;

            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();
            objCommon = new Common();
            try
            {
                conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
                //query = "SELECT * FROM [Upload$A3:BY]";
                query = "SELECT * FROM [EasyHR$]";

                using (var connection = new OleDbConnection(conn))
                {
                    using (var da = new OleDbDataAdapter(query, connection))
                    {
                        connection.Open();
                        da.Fill(ds);
                    }
                }

                status = "";
            }


            catch (Exception ex)
            {
                status = ex.Message;
            }

            try
            {
                DataTable dt = new DataTable();
                StringBuilder sbDetails = new StringBuilder();

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Columns[0].ColumnName != "Employee code")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                    }

                    if (status == "")
                    {
                        sbDetails.Append("<ROOT>");

                        for (int cnt = 0; cnt <= dt.Rows.Count - 1; cnt++)
                        {

                            string SR_No = Convert.ToString(dt.Rows[cnt]);
                            if (SR_No == "")
                            {
                                break;
                            }
                            else
                            {
                                sbDetails.Append("<UP Emp_Code='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Employee code"])) + "'");
                                sbDetails.Append(" Time_In='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Sign-in(dd-mm-yyyy HH:mm:ss)"])) + "'");
                                sbDetails.Append(" Time_Out='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Sign-out(dd-mm-yyyy HH:mm:ss)"])) + "'/>");
                            }

                        }

                        sbDetails.Append("</ROOT>");

                        objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 1000000000, sbDetails.ToString());
                        objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpCode.ToString());
                        objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 1000, EmpName.ToString());

                        objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPADTEEXCELATTENDANCETIMEINOUTDATA");

                        ds = new DataSet();
                        ds = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");


                        objDBUtility.ClearTransactionalParams();

                        if (Convert.ToString(ds.Tables[0].Rows[0]["result"]) != "")
                        {
                            status = Convert.ToString(ds.Tables[0].Rows[0]["result"]);
                        }
                        else if (Convert.ToString(ds.Tables[0].Rows[0]["result"]) == "")
                        {
                            status = "Successfuly uploaded Shift Roster Data.";
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                status = ex.Message;
            }
            return status;
        }
        public DataSet GetDatetime(string compValue, string empValue)

        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            //objDBUtility.AddParameters("@ATTENDIN", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ATTENDIN.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDNOTIFY");


            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }


        public DataSet GenerateAbsentAttendance(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETABSENTRECORD");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet getNewRoster(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETNEXTMONTHROSTERFORGENSHIFT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GenerateAllEmpAttendanceReport(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            //objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, flag.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETALLEMPATTENDANCEHOURS");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GetZinghrAttendanceNotifyLogIn(string compValue, string empValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDTIMEINNOTIFY");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }
        public DataSet GetZinghrAttendanceNotifyLogOut(string compValue, string empValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDTIMEOUTNOTIFY");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GetZinghrAttendanceLogIn(string compValue, string empValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETZINGHRATTENDANCELOGIN");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }
        public DataSet GetZinghrAttendanceLogOut(string compValue, string empValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETZINGHRATTENDANCELOGOUT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GetAttendanceHrs(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDENCE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }


        public DataSet GetAttendanceZingHr(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDANCEZINGHRDATA");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet InsertAttendanceNPL(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTATTENDANCEDATANPL");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }


        public DataSet GenerateAttendanceReportFlag(string compValue, String flag)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            //objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            //objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            //objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, flag.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDANCEHOURSBYTILDATE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }
        public DataSet GenerateEmpAttendanceReportFlag(string compValue, String flag)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            //objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            //objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, flag.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPATTENDANCEHOURSBYTILDATE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }
        public DataSet GenerateAttendanceReportRPTFlag(string compValue, String flag)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, flag.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDANCEHOURSBYTILDATE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GenerateAttendanceReportRPTFlagAll(string compValue, string empValue, string flag)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, flag.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDANCEHOURSBYTILDATERPTALL");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }
        public DataSet GenerateAttendanceReport(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDANCEHOURS");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GenerateAttendanceReportAll(string compValue, string Type)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Type.ToString().Trim());
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETALLREPORTDATA");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        
        public DataSet GenerateAttendanceSummeryReport(string compValue, string EmpCode)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@SELMONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, selMonth.ToString().Trim());
            objDBUtility.AddParameters("@SELYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, selYear.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GenerateAttendanceSummeryReport");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GenerateOTAndCOReportEmpWise(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVALOTANDCO");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }


        public DataSet GenerateCOAndOTReport(string compValue, string empValue, string type)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, type.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOANDOTREPORTBYRPT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet getName(string compValue, string empValue)
        {

            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "REPORTINGGENERATEOTCO");


            dsInv = objDBUtility.Execute_StoreProc_DataSet("[SP_NPL]");
            objDBUtility.ClearTransactionalParams();


            return dsInv;

        }

        public DataSet DeleteAttendance(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            //objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            //objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "DELETEATTENDANCEBYUSERWISE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }


        public DataSet GetRosterReport(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETROSTEREPORT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GetRosterReportWise(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETROSTEREPORTEEMP");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }
        public DataSet GetRosterReportEmpWise(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETROSTEREPORTEEMPWISE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }
        public DataSet GetRosterReportAllWise(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETROSTEREPORTEEMPALL");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }
        public DataSet GetRosterReportHRWiseTillDate(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //DBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETROSTEREPORTEEMPTILLDATEHR");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GetRosterReportWiseTillDate(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETROSTEREPORTEEMPTILLDATE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public Boolean UpdateChkSubmit(string xmlValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@STATUS ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status);
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECHKGLSUBMIT");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return true;
        }

        public DataSet UpdateChkLASubmit(string xmlValue, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@STATUS ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status);
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECHKLASUBMIT");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }





        public DataSet UpdateChkSubmit1(string xmlValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@STATUS ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status);
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECHKGLSUBMIT");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

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
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETHISTORY");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }


        public DataSet GetAppDetails(string compValue, string empValue, string NewYear, string OldYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVALLIST");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public void CancelLeave(string cid)
        {
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CANCELLEAVE");

            objDBUtility.Execute_StoreProc_DataSet("SP_NPL");



            objDBUtility.ClearTransactionalParams();
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

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetLeaveTypeDetails(string compValue, string empValue, string leaveType)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();




            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEAVETYPEDETAILS");
            objDBUtility.AddParameters("@LEAVE_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leaveType);

            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");



            objDBUtility.ClearTransactionalParams();
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

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public string UpdateLeaveStatus(string compValue, string Empname, string empValue, string status, string CID, string fromdate, string todate, string txtFormTime, string txtToTime, string txtTotalHrTime, string rem, string ODType, string NewYear, string leaveDays)
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
                objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empname.ToString().Trim());
                objDBUtility.AddParameters("@FROM_DT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromdate);
                objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, todate);
                objDBUtility.AddParameters("@FROM_TM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtFormTime);
                objDBUtility.AddParameters("@TO_TM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtToTime);
                objDBUtility.AddParameters("@TOTALHRS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtTotalHrTime);
                objDBUtility.AddParameters("@ODTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ODType);
                objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rem);
                objDBUtility.AddParameters("@STATUS ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status);
                objDBUtility.AddParameters("@FINYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                objDBUtility.AddParameters("@LEAVEDAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leaveDays);
                objDBUtility.AddParameters("@CID ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CID);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATESTATUS");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");


                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            strresult = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["status"].ToString().Trim());
                            //strurl = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["url"].ToString().Trim());
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

        public string UpdateLeave(string compValue, string Empname, string empValue, string fromdate, string todate, string txtFormTime, string txtToTime, string txtTotalHrTime, string rem, string status, string ODType, string NewYear, string leaveDays)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;
            string strurl = string.Empty;
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empname.ToString().Trim());
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                objDBUtility.AddParameters("@FROM_DT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromdate);
                objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, todate);
                objDBUtility.AddParameters("@FROM_TM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtFormTime);
                objDBUtility.AddParameters("@TO_TM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtToTime);
                objDBUtility.AddParameters("@TOTALHRS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtTotalHrTime);
                objDBUtility.AddParameters("@ODTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ODType);
                objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rem);
                objDBUtility.AddParameters("@STATUS ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status);
                objDBUtility.AddParameters("@FINYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                objDBUtility.AddParameters("@LEAVEDAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leaveDays);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEOUTSIDEDUTY");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");


                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            strresult = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["status"].ToString().Trim());
                            //strurl = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["url"].ToString().Trim());
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
        //public string UpdateLeave(string compValue, string Empname, string empValue, string fromdate, string todate, string rem, string status, string NewYear, string leaveDays)
        //{
        //    objDBUtility = new DBUtility();

        //    DataSet dsInv = new DataSet();
        //    string strresult = string.Empty;
        //    string strurl = string.Empty;
        //    try
        //    {
        //        objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
        //        objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
        //        objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empname.ToString().Trim());
        //        objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
        //        objDBUtility.AddParameters("@FROM_DT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromdate);
        //        objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, todate);
        //        objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rem);
        //        objDBUtility.AddParameters("@STATUS ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status);
        //        objDBUtility.AddParameters("@FINYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
        //        objDBUtility.AddParameters("@LEAVEDAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leaveDays);
        //        objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEOUTSIDEDUTY");

        //        dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");


        //        if (dsInv.Tables.Count > 0)
        //        {
        //            if (dsInv.Tables[0].Rows.Count > 0)
        //            {
        //                for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
        //                {
        //                    strresult = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["status"].ToString().Trim());
        //                    //strurl = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["url"].ToString().Trim());
        //                }
        //            }
        //        }

        //        objDBUtility.ClearTransactionalParams();


        //    }
        //    catch (Exception ex)
        //    {
        //        //CreateErrorLog("", ex.Message, "Common_Validate_Login");
        //    }

        //    return strresult;
        //}

        public DataSet GetDetailEmps(string compValue, string empValue, string CID)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDETAILEMPS");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GenerateOTandCOReport(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETOTANDCOREPORT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet UpdateAttend(string compValue, string empMID, string empValue, string EmpCode, string Name, string Date, string Remark)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;
            string strurl = string.Empty;
            try
            {

                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empMID.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.ToString().Trim());
                objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Name == null ? "" : Name.ToString().Trim());
                objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Date == null ? "" : Date.ToString().Trim());
                objDBUtility.AddParameters("@ENTRY_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.ToString().Trim());

                objDBUtility.AddParameters("@SHIFTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Shift == null ? "" : Shift.ToString().Trim());
                objDBUtility.AddParameters("@INTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDate == null ? "" : FromDate.ToString().Trim());
                objDBUtility.AddParameters("@INOUT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ToDate == null ? "" : ToDate.ToString().Trim());
                objDBUtility.AddParameters("@OTCNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OT == null ? "" : OT.ToString().Trim());
                objDBUtility.AddParameters("@COCNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CO == null ? "" : CO.ToString().Trim());


                objDBUtility.AddParameters("@OLDSHIFTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_Shift == null ? "" : Old_Shift.ToString().Trim());
                objDBUtility.AddParameters("@OLDINTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_InTime == null ? "" : Old_InTime.ToString().Trim());
                objDBUtility.AddParameters("@OLDOUTTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_OutTime == null ? "" : Old_OutTime.ToString().Trim());
                objDBUtility.AddParameters("@OLDOT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_OT == null ? "" : Old_OT.ToString().Trim());
                objDBUtility.AddParameters("@OLDCO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_CO == null ? "" : Old_CO.ToString().Trim());


                objDBUtility.AddParameters("@REMARKS ", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Remark == null ? "" : Remark.ToString().Trim());
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

        public DataSet GETAPPROVALOTANDCO(string CompId, string Empid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Date.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "APPROVALOTANDCO");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());


            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GenerateQueryString()
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            //objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            //objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            //objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, flag.ToString().Trim());

            objDBUtility.AddParameters("@LEAVE", DBUtilDBType.Varchar, DBUtilDirection.In, 10000, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETQUERYSTRINGPATHH");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }
        public DataTable GetFinancialYearList(string compValue, string empValue, DropDownList drpSelectFinancialYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETFINANCIALYEAR");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            dt = dsInv.Tables[0];

            drpSelectFinancialYear.Items.Clear();
            drpSelectFinancialYear.DataTextField = "DES";
            drpSelectFinancialYear.DataValueField = "AID";
            drpSelectFinancialYear.DataSource = dsInv.Tables[0];
            drpSelectFinancialYear.DataBind();
            drpSelectFinancialYear.Items.Insert(0, new ListItem("[Select Year]", ""));
            //drpSelectFinancialYear.Items.Add(new ListItem("Other", "9999"));

            objDBUtility.ClearTransactionalParams();

            return dt;
        }

        public DataSet GetApprovedDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVEDLEAVED");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetApprovedLTAID(string compValue, string empValue, string id)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVEDLTAID");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ENTRY_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, id.ToString().Trim());
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetAbscent(string compValue, string empValue, string fromDt, string toDt)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, fromDt.ToString().Trim());
                objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, toDt.ToString().Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "NPLABCENTRECORD");
                //objDBUtility.AddParameters("@CURRENTDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CurrentDate == null ? "" : CurrentDate.Trim());
                //objDBUtility.AddParameters("@FromDt", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromDt);
                //objDBUtility.AddParameters("@ToDate", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDt);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet GetCurrentdateAttendance(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CURRENTATTENDANCEDATE");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet GetCurrentdateAttendanceStatus(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EmpName.ToString().Trim());
                objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Status.ToString().Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CURRENTATTENDANCEDATE");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet GenerateAttendanceReportNPL(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "NPLATTENDANCERECORD");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GetCurrentdateAttendanceSummary(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CURRENTATTENDANCEDDATECHART");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
                objDBUtility.ClearTransactionalParams();

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet FillDepartment()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSUBTYPE");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet FillLocation()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSUBTYPE");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetInsertAbsentAttendanceSummary(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTCURRENTABSENTDATE");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
                objDBUtility.ClearTransactionalParams();

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet GetCurrentdateAbsentAttendanceSummary(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();

            try
            {
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CURRENTABSENTDATECHART");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
                objDBUtility.ClearTransactionalParams();

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet GetCurrentdateaAbsentAttendanceStatus(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EmpName.ToString().Trim());
                objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Status.ToString().Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CURRENTABSENTATTENDANCEDATE");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetOutSideDuty(string compValue, string EmpCode)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETOUTSIDEDUTY");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GetEmpAttendanceaPPROVE(string compValue, string empValue, string id)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, id.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPATTENDANCERECAPPROVEDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet GenerateOTAndCOReportRPTFlag(string compValue, String flag)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, flag.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "REPORTOTANDCOEMPWISE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GenerateOTAndCOReportRPTFlagAll(string compValue, string empValue, string flag)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, flag.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "REPORTOTANDCOALLEMPWISE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GetEmpDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMENULIST");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet GetEmpProfileDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPPROFILEDETAILS");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;
        }

        public DataSet UpdateLeaveBal(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                //objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATELEAVEBALANCE");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;
        }


        public DataSet LeaveUpdatePreviousMonth(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATE1MONTHPREVIOUSLEAVEBALANCE");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetRODetails(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREPORTOFFICERDETAILS");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

                objDBUtility.ClearTransactionalParams();

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetROName(string compValue, string Emp_Aid)
        {
            objDBUtility = new DBUtility();
            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Emp_Aid.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREPORTOFFICERDETAILS");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;
        }

        public DataSet GetROEmpDetails(string compValue, string Emp_Mid)
        {
            objDBUtility = new DBUtility();
            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Emp_Mid.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREPORTOFFICERDETAILSBYID");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;
        }

        public DataSet UpdateReportingofficersDetails(string compValue, string Emp_Mid, string Emp_Appr_Aid)
        {
            objDBUtility = new DBUtility();
            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Emp_Mid.ToString().Trim());
                objDBUtility.AddParameters("@EMP_APPRO_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Emp_Appr_Aid.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEREPORTOFFICERID");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;
        }

        public DataSet GetReportingofficersDetails(string compValue, string Emp_Mid)
        {
            objDBUtility = new DBUtility();
            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Emp_Mid.ToString().Trim());

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREPORTINGOFFICER");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;
        }

        public DataSet GetEmpAndROName(string compValue, string Emp_Mid)
        {
            objDBUtility = new DBUtility();
            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Emp_Mid.ToString().Trim());

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREPORTINGOFFICER");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;
        }

        public DataSet GetExitEmployeeDetails(string compValue)
        {
            objDBUtility = new DBUtility();
            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEXITEMPLOYEELIST");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;
        }

        public DataSet GenerateAttendanceAuto(string compValue)
        {


            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPATTENDANCEHOURSBYTILDATEAUTO");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet getDays(string compValue, string empValue, string fromdate, string todate)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());

            objDBUtility.AddParameters("@FROM_DT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromdate);
            objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, todate);

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDAYS");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            return dsInv;
        }

        public DataSet insertcootapprover(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOOTAPPROVER");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;
        }

        public DataSet GetEmpMid(string compValue, string EmpCode)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GetEmpId");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet getLeaveCount(string compValue, string EmpCode, string LeaveType)
        {

            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@LEAVETYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, LeaveType.ToString().Trim());


            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEAVECOUNT");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            return dsInv;

        }

        public DataSet checkLeaveBal(string compValue, string EmpCode, string leaveCode )//,string leave_type)
        {

            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@LEAVE_CODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leaveCode.ToString().Trim());
            //objDBUtility.AddParameters("@LEAVE_NUMBER", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leave_type.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CHECKLEAVEBAL");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("[COMMON_SP_LEAVECARD]");
            return dsInv;

        }

        public DataSet getShiftRoster(string compValue, string EmpCode, string frmDate, string toDate, string LeaveTypes)
        {

            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@FROM_DT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, frmDate.ToString().Trim());
            objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDate.ToString().Trim());
            objDBUtility.AddParameters("@LEAVE_COUNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, LeaveTypes.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSHIFTROSTER");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            return dsInv;

        }

        public DataSet GetZinghrAttendanceLogInSequel(string compValue, string empValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETZINGHRATTENDANCELOGINSEQUEL");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }
        public DataSet GetZinghrAttendanceLogOutSequel(string compValue, string empValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETZINGHRATTENDANCELOGOUTSEQUEL");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }
        public DataSet Reminder(string compValue)
        {
            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEAVEAPPREMINDER");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        public DataSet UpdateMissPunch(string xmlValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            //objDBUtility.AddParameters("@ACTION_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ActionType.ToString().Trim());
            //objDBUtility.AddParameters("@REMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Remark.ToString().Trim());
            //objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            //objDBUtility.AddParameters("@EMPLEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Emplevel.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEMISSPUNCH");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GenerateAttendanceDetailsReport(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());

            objDBUtility.AddParameters("@DEPT_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpDpt == "0" ? "" : drpDpt.Trim());
            objDBUtility.AddParameters("@DRP_EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpEmpCode == null ? "" : drpEmpCode.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtEmpCode == null ? "" : txtEmpCode.Trim());

            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDANCEDETAILSREPORT");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GenerateAttendanceDetailsReportAll(string compValue, string Type)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Type.ToString().Trim());
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());

            objDBUtility.AddParameters("@DEPT_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpDpt == "0" ? "" : drpDpt.Trim());
            objDBUtility.AddParameters("@DRP_EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpEmpCode == null ? "" : drpEmpCode.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtEmpCode == null ? "" : txtEmpCode.Trim());

            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDANCEDETAILSREPORT");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet Fillemployee()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@DEPT_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSUBEMP");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet GetOutSideDutyfromdate(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@FROM_DT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETOUTSIDEDUTYFROMDATE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet UpdateAttendancedetails(string xmlValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            //objDBUtility.AddParameters("@ACTION_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ActionType.ToString().Trim());
            //objDBUtility.AddParameters("@REMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Remark.ToString().Trim());
            //objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            //objDBUtility.AddParameters("@EMPLEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Emplevel.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEATTENDANCEDETAIL");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }


        public DataSet UpdateAttend(string compValue, string empMID, string empValue, string Name, string Date, string Remark)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;
            string strurl = string.Empty;
            try
            {

                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empMID.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Name == null ? "" : Name.ToString().Trim());
                objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Date == null ? "" : Date.ToString().Trim());
                objDBUtility.AddParameters("@ENTRY_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.ToString().Trim());

                objDBUtility.AddParameters("@SHIFTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Shift == null ? "" : Shift.ToString().Trim());
                objDBUtility.AddParameters("@INTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDate == null ? "" : FromDate.ToString().Trim());
                objDBUtility.AddParameters("@INOUT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ToDate == null ? "" : ToDate.ToString().Trim());
                objDBUtility.AddParameters("@OTCNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OT == null ? "" : OT.ToString().Trim());
                objDBUtility.AddParameters("@COCNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CO == null ? "" : CO.ToString().Trim());


                objDBUtility.AddParameters("@OLDSHIFTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_Shift == null ? "" : Old_Shift.ToString().Trim());
                objDBUtility.AddParameters("@OLDINTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_InTime == null ? "" : Old_InTime.ToString().Trim());
                objDBUtility.AddParameters("@OLDOUTTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_OutTime == null ? "" : Old_OutTime.ToString().Trim());
                objDBUtility.AddParameters("@OLDOT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_OT == null ? "" : Old_OT.ToString().Trim());
                objDBUtility.AddParameters("@OLDCO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_CO == null ? "" : Old_CO.ToString().Trim());


                objDBUtility.AddParameters("@REMARKS ", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Remark == null ? "" : Remark.ToString().Trim());
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


        //dnyaneshwar

        public DataSet UpdateShift(string compValue, string EmpCode, string dateValue, string ddlNewShift)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@DATE ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, dateValue.ToString().Trim());
            objDBUtility.AddParameters("@NEWSHIFTTYPE ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ddlNewShift.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UpdateShift");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet UpdateAttendMissingPunch(string compValue, string empMID, string empValue, string Name, string Date, string Remark)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;
            string strurl = string.Empty;
            try
            {

                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empMID.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Name == null ? "" : Name.ToString().Trim());
                objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Date == null ? "" : Date.ToString().Trim());
                objDBUtility.AddParameters("@ENTRY_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.ToString().Trim());

                objDBUtility.AddParameters("@SHIFTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Shift == null ? "" : Shift.ToString().Trim());
                objDBUtility.AddParameters("@INTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDate == null ? "" : FromDate.ToString().Trim());
                objDBUtility.AddParameters("@INOUT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ToDate == null ? "" : ToDate.ToString().Trim());
                objDBUtility.AddParameters("@OTCNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OT == null ? "" : OT.ToString().Trim());
                objDBUtility.AddParameters("@COCNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CO == null ? "" : CO.ToString().Trim());


                objDBUtility.AddParameters("@OLDSHIFTTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_Shift == null ? "" : Old_Shift.ToString().Trim());
                objDBUtility.AddParameters("@OLDINTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_InTime == null ? "" : Old_InTime.ToString().Trim());
                objDBUtility.AddParameters("@OLDOUTTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_OutTime == null ? "" : Old_OutTime.ToString().Trim());
                objDBUtility.AddParameters("@OLDOT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_OT == null ? "" : Old_OT.ToString().Trim());
                objDBUtility.AddParameters("@OLDCO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Old_CO == null ? "" : Old_CO.ToString().Trim());
                //objDBUtility.AddParameters("@SHIFTHRS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ShiftHrs == null ? "" : ShiftHrs.ToString().Trim());


                objDBUtility.AddParameters("@REMARKS ", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Remark == null ? "" : Remark.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 100, "INSERTATTENDANCEMISSINGPUNCH");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");




                objDBUtility.ClearTransactionalParams();


            }

            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;
        }

        public DataSet GenerateAttendanceMissingPunch(string compValue)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDANCEMISSINGPUNCH");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet GenerateAttendanceMissingPunchAll(string compValue, string Type)
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Type.ToString().Trim());
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDANCEMISSINGPUNCH");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet updateattendanceNs()
        {
            DataSet dsLogin = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATENS");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");
            objDBUtility.ClearTransactionalParams();
            return dsLogin;
        }

        public DataSet updateNplLeaveBalance(string compValue, string xmlValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "LEAVEUPDATENEXTMONTHS");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetLeaveHistoryData(string compAid, string fromDate, string toDate)
        {
            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compAid);
            objDBUtility.AddParameters("@FROM_DT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromDate);
            objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDate);
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GET_LEAVE_HISTORY");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        //dnyaneshwar



        //REPORTINGGENERATEANDUPLOAD

        string _EmpName;
        string _EmpCode;
        string _FromDate;
        string _ToDate;
        string _Status;
        string _OT;
        string _Old_OT;
        string _CO;
        string _Old_CO;
        string _Old_OutTime;
        string _Old_InTime;
        string _Old_Shift;
        string _Shift;
        string _EntryAid;
        string _Date;
        string _CurrentDate;
        string _LeaveType;
        string _selMonth;
        string _selYear;
        string _Empid;
        string _Empmid;
        string _drpDpt;
        string _drpEmpCode;
        string _txtEmpCode;



        public string drpDpt
        {
            get { return _drpDpt; }
            set { _drpDpt = value; }
        }
        public string drpEmpCode
        {
            get { return _drpEmpCode; }
            set { _drpEmpCode = value; }
        }
        public string txtEmpCode
        {
            get { return _txtEmpCode; }
            set { _txtEmpCode = value; }
        }

        public string CurrentDate
        {
            get { return _CurrentDate; }
            set { _CurrentDate = value; }
        }
        public string Empmid
        {
            get { return _Empmid; }
            set { _Empmid = value; }
        }
        public string Empid
        {
            get { return _Empid; }
            set { _Empid = value; }
        }
        public string LeaveType
        {
            get { return _LeaveType; }
            set { _LeaveType = value; }
        }
        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        public string EntryAid
        {
            get { return _EntryAid; }
            set { _EntryAid = value; }
        }
        public string Shift
        {
            get { return _Shift; }
            set { _Shift = value; }
        }
        public string Old_Shift
        {
            get { return _Old_Shift; }
            set { _Old_Shift = value; }
        }
        public string Old_InTime
        {
            get { return _Old_InTime; }
            set { _Old_InTime = value; }
        }
        public string Old_OutTime
        {
            get { return _Old_OutTime; }
            set { _Old_OutTime = value; }
        }
        public string Old_CO
        {
            get { return _Old_CO; }
            set { _Old_CO = value; }
        }
        public string CO
        {
            get { return _CO; }
            set { _CO = value; }
        }
        public string Old_OT
        {
            get { return _Old_OT; }
            set { _Old_OT = value; }
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

        public string EmpName
        {
            get { return _EmpName; }
            set { _EmpName = value; }
        }
        public string EmpCode
        {
            get { return _EmpCode; }
            set { _EmpCode = value; }
        }


        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        public string selMonth
        {
            get { return _selMonth; }
            set { _selMonth = value; }
        }

        public string selYear
        {
            get { return _selYear; }
            set { _selYear = value; }
        }
    }


    public class Loc
    {
        //public string IPAddress { get; set; }
        //public string CountryName { get; set; }
        //public string CountryCode { get; set; }
        //public string CityName { get; set; }
        //public string RegionName { get; set; }
        //public string ZipCode { get; set; }
        //public string Latitude { get; set; }
        //public string Longitude { get; set; }
        //public string TimeZone { get; set; }

        //public string IPAddress { get; set; }
        //public string CountryName { get; set; }
        //public string country_code { get; set; }
        //public string city { get; set; }
        //public string region { get; set; }
        //public string postal_code { get; set; }
        //public string longitude { get; set; }
        //public string latitude { get; set; }
        //public string continent { get; set; }

        public string ip { get; set; }
        public string country_code { get; set; }
        public string country_name { get; set; }
        public string city_name { get; set; }
        public string region_name { get; set; }
        public string zip_code { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }
}