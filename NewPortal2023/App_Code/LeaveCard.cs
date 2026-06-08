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
/// Summary description for LeaveCard
/// </summary>
namespace NewPortal2023.ESS
{
    public class LeaveCards
    {

        private NewPortal2023.ESS.DBUtility objDBUtility;
        public LeaveCards()
        {
            //
            // TODO: Add constructor logic here
            //
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
        
        public DataSet GetLeaveDetails(string compValue, string empValue, string NewYear, string NewMonth)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLeave");
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
            objDBUtility.AddParameters("@MONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewMonth);

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVECARD");

                objDBUtility.ClearTransactionalParams();

            return dsInv;
        }
        public DataSet GetLeaveCurrentDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEAVECURRENTDATE");
        

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVECARD");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }
        public DataSet GetLeaveCardReport(string compValue, string empValue, string NewYear, string NewMonth)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLeaveCardReport");
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
            objDBUtility.AddParameters("@MONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewMonth);

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVECARD");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
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
            drpSelectFinancialYear.Items.Insert(0, new ListItem("[Select All]", ""));
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

            string[] correctMonthOrder = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

            // Create a new DataTable for reordered rows
            DataTable reorderedDt = dt.Clone();

            // Iterate through the correct order of month names and copy corresponding rows
            foreach (string monthName in correctMonthOrder)
            {
                // Copy rows with matching month name in either "DES" or "AID" column
                DataRow[] matchingRows = dt.Select("DES = '" + monthName + "' OR AID = '" + monthName + "'");
                foreach (DataRow row in matchingRows)
                {
                    reorderedDt.ImportRow(row);
                }
            }

            drpSelectMonth.Items.Clear();
            drpSelectMonth.DataTextField = "DES";
            drpSelectMonth.DataValueField = "AID";
            drpSelectMonth.DataSource = reorderedDt;
            drpSelectMonth.DataBind();
            drpSelectMonth.Items.Insert(0, new ListItem("[Select All]", ""));
            //drpSelectFinancialYear.Items.Add(new ListItem("Other", "9999"));

            objDBUtility.ClearTransactionalParams();

            return dt;
        }

        public DataSet GetLeaveDetailsAlt(string compValue, string empValue, string NewYear, string OldYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEAVEFROMCREDIT");
            objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
            objDBUtility.AddParameters("@OLDYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OldYear);

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVECARD");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

       


       // -------------------FOR NEW EMPLOYEE ADD IN LEAVE ALLOTMENT TABLE----------------------------------------

        public DataSet InsertLeave(string compValue, string empValue, string NewYear, string JoinDate, string Leave_CId)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTRECORD");
            objDBUtility.AddParameters("@FINYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
            objDBUtility.AddParameters("@JOINDT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, JoinDate);
            objDBUtility.AddParameters("@LEAVE_CID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Leave_CId);
            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVECARD");


            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        //-------------------FOR NEW DETAILS LEAVE ADD IN LEAVE ALLOTMENT TABLE----------------------------------------

        public DataSet InsertLeaveDetails(string compValue, string empValue, string NewYear, string JoinDate, string Leave_CId)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
           // objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTRECORD");
            objDBUtility.AddParameters("@FINYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
            objDBUtility.AddParameters("@JOINDT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, JoinDate);
            objDBUtility.AddParameters("@LEAVE_CID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Leave_CId);
            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVECOUNT");


            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetEmpLeaveDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet dsInv = new DataSet();
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPLEAVEDETL");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVECARD");
           objDBUtility.ClearTransactionalParams();
            return dsInv;
        }
        

        public DataSet GetAttendanceDetails(string compValue, string empValue)

        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ATTENDIN", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ATTENDIN.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTEND");
            

            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }
        
        

        public DataSet InsertAttendDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ATTENDIN", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ATTENDIN.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTATTENDREC");


            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVECARD");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet UpdateAttendDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ATTENDOUT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ATTENDOUT.ToString().Trim());
            //objDBUtility.AddParameters("@ENTRY_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ENTRY_AID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEATTENDREC");


            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVECARD");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet InsertAttendDetailsNpl(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@GEOLOC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, GEOLOC.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ATTENDIN", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ATTENDIN.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTATTENDREC");


            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet UpdateAttendDetailsNpl(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ATTENDOUT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ATTENDOUT.ToString().Trim());
            //objDBUtility.AddParameters("@ENTRY_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ENTRY_AID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEATTENDREC");


            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetAttendance()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDENCE");


            dsInv = objDBUtility.Execute_StoreProc_DataSet("[SP_NPL]");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetPLEncashReport(string compValue, string NewYear, string NewMonth)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "PLENCASHREPORT");
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
            objDBUtility.AddParameters("@MONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewMonth);

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVECARD");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        string _ATTENDIN;
        string _ATTENDOUT;
        string _ENTRY_AID;
        string _GEOLOC;

        public string GEOLOC
        {
            get { return _GEOLOC; }
            set { _GEOLOC = value; }
        }

        public string ATTENDIN
        {
            get { return _ATTENDIN; }
            set { _ATTENDIN = value; }
        }
        public string ATTENDOUT
        {
            get { return _ATTENDOUT; }
            set { _ATTENDOUT = value; }
        }
        public string ENTRY_AID
        {
            get { return _ENTRY_AID; }
            set { _ENTRY_AID = value; }
        }
    }
}