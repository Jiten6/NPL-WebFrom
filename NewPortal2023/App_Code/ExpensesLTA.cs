using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public class ExpensesLTA
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        internal DataSet GetExempData(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());


            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEXMPDATA");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet GetEmpDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());


            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet GetLTAApproverList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLTAAPPROVERLIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet InsertLTAClaim(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@APPAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, App_AID == null ? "" : App_AID.Trim());
            //objDBUtility.AddParameters("@ENTRMDESC", DBUtilDBType.Varchar, DBUtilDirection.In, 500, EntermDesc == null ? "" : EntermDesc.Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@LTARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TxtltaRs.ToString().Trim());
            objDBUtility.AddParameters("@DRPYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpYear.ToString().Trim());
            objDBUtility.AddParameters("@NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtName.ToString().Trim());
            objDBUtility.AddParameters("@LOC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtLoc.ToString().Trim());
            objDBUtility.AddParameters("@DESIGN", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtDesig.ToString().Trim());
            objDBUtility.AddParameters("@EMPGRADE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtGrd.ToString().Trim());
            objDBUtility.AddParameters("@LTADAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtdays.ToString().Trim());
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtFrDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtToDate.ToString().Trim());
            objDBUtility.AddParameters("@DPOREMP", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtDepOrEmp.ToString().Trim());
            objDBUtility.AddParameters("@RESUMING", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtResuming.ToString().Trim());
            objDBUtility.AddParameters("@ADDVACTN", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtAddVactn.ToString().Trim());
            objDBUtility.AddParameters("@DESTLTA", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtDestLTA.ToString().Trim());
            //objDBUtility.AddParameters("@TOTALEXP", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtTotalexp.ToString().Trim());

            objDBUtility.AddParameters("@NOPER", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtTotPers.ToString().Trim());
            objDBUtility.AddParameters("@CHLDAGE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtAgeChild.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELMODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpModeOfTravel.ToString().Trim());
            objDBUtility.AddParameters("@OTHERTYP", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtOtherType.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELCLASS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtClassOfTravel.ToString().Trim());
            objDBUtility.AddParameters("@TAX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpTax.ToString().Trim());
            objDBUtility.AddParameters("@LTAMONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpMonthLTA.ToString().Trim());
            objDBUtility.AddParameters("@LTAYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpYearLTA.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FilingStatus.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTLTACLAIM");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet GetLTAList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLTALIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet getLTAClaimById(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, App_AID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLTACLAIMBYID");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet UpdateLTAStatus(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, App_AID.ToString().Trim());           
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());            
            objDBUtility.AddParameters("@REMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtRmk.ToString().Trim());
            objDBUtility.AddParameters("@CHECKREMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtRmk.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpActionType.ToString().Trim());
            objDBUtility.AddParameters("@APPROVEDAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 500, (txtApprAmt.ToString() ?? "0.00").Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATELTASTATUS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        String _App_AID;
        string _txtltaRs;
        string _drpYear;
        string _txtName;
        string _txtLoc;
        string _txtDesig;
        string _txtGrd;
        string _txtdays;
        string _txtFrDate;
        string _txtToDate;
        string _txtDepOrEmp;
        string _txtResuming;
        string _txtAddVactn;
        string _txtDestLTA;
        string _txtTotPers;
        string _txtAgeChild;
        string _drpModeOfTravel;
        string _txtOtherType;
        string _txtClassOfTravel;
        string _drpTax;
        string _drpMonthLTA;
        string _drpYearLTA;
        String _FilingStatus;
        String _Status;
        string _txtRmk;
        string _txtApprAmt;
        string _drpActionType;

        public string App_AID
        {
            get { return _App_AID; }
            set { _App_AID = value; }
        }
        public string TxtltaRs
        {
            get { return _txtltaRs; }
            set { _txtltaRs = value; }
        }
        public string drpYear
        {
            get { return _drpYear; }
            set { _drpYear = value; }
        }
        public string txtName
        {
            get { return _txtName; }
            set { _txtName = value; }
        }
        public string txtLoc
        {
            get { return _txtLoc; }
            set { _txtLoc = value; }
        }
        public string txtDesig
        {
            get { return _txtDesig; }
            set { _txtDesig = value; }
        }
        public string txtGrd
        {
            get { return _txtGrd; }
            set { _txtGrd = value; }
        }
        public string txtdays
        {
            get { return _txtdays; }
            set { _txtdays = value; }
        }
        public string txtFrDate
        {
            get { return _txtFrDate; }
            set { _txtFrDate = value; }
        }
        public string txtToDate
        {
            get { return _txtToDate; }
            set { _txtToDate = value; }
        }
        public string txtDepOrEmp
        {
            get { return _txtDepOrEmp; }
            set { _txtDepOrEmp = value; }
        }
        public string txtResuming
        {
            get { return _txtResuming; }
            set { _txtResuming = value; }
        }
        public string txtAddVactn
        {
            get { return _txtAddVactn; }
            set { _txtAddVactn = value; }
        }

        public string txtDestLTA
        {
            get { return _txtDestLTA; }
            set { _txtDestLTA = value; }
        }
        public string txtTotPers
        {
            get { return _txtTotPers; }
            set { _txtTotPers = value; }
        }
        public string txtAgeChild
        {
            get { return _txtAgeChild; }
            set { _txtAgeChild = value; }
        }
        public string drpModeOfTravel
        {
            get { return _drpModeOfTravel; }
            set { _drpModeOfTravel = value; }
        }
        public string txtOtherType
        {
            get { return _txtOtherType; }
            set { _txtOtherType = value; }
        }
        public string txtClassOfTravel
        {
            get { return _txtClassOfTravel; }
            set { _txtClassOfTravel = value; }
        }
        public string drpTax
        {
            get { return _drpTax; }
            set { _drpTax = value; }
        }
        public string drpMonthLTA
        {
            get { return _drpMonthLTA; }
            set { _drpMonthLTA = value; }
        }
        public string drpYearLTA
        {
            get { return _drpYearLTA; }
            set { _drpYearLTA = value; }
        }
        public string FilingStatus
        {
            get { return _FilingStatus; }
            set { _FilingStatus = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        public string txtRmk
        {
            get { return _txtRmk; }
            set { _txtRmk = value; }
        }
        public string txtApprAmt
        {
            get { return _txtApprAmt; }
            set { _txtApprAmt = value; }
        }
        public string drpActionType
        {
            get { return _drpActionType; }
            set { _drpActionType = value; }
        }


    }

    
}
