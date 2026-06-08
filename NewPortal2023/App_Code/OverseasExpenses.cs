using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public class OverseasExpenses
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;



        internal DataSet InsertTravelOverseasClaim(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@REQSDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, RequisitionDate.ToString().Trim());
            objDBUtility.AddParameters("@VISITPURP", DBUtilDBType.Varchar, DBUtilDirection.In, 50, VisitPurpose.ToString().Trim());
            objDBUtility.AddParameters("@VISITPLACE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, VisitPlace.ToString().Trim());
            objDBUtility.AddParameters("@DEPTDTIND", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DepartdateInd.ToString().Trim());
            objDBUtility.AddParameters("@ARVLDTIND", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ArvlDateInd.ToString().Trim());
            objDBUtility.AddParameters("@HODRCMD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, HODRcmd.ToString().Trim());
            objDBUtility.AddParameters("@FILNGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FilingStatus.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "InsertTravelOverseasClaim");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet GetTravelOverseasClaimList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
           
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GetTravelOverseasClaimList");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet getTrOvrClaimById(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AppNo.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "getTrOvrClaimById");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetSAARReimb(string compValue, string category)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, category.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSAARREIMB");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetNonEasternConReimb(string compValue, string category)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, category.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEASTERNCOUNTRYREIMB");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetOtherConReimb(string compValue, string category)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, category.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETOTHERCOUNTRYREIMB");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet GetEntReimb(string compValue, string category)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, category.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETENTREIMB");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet GetWardrReimb(string compValue, string category)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, category.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETWARDRRIEMB");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetHodName(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETHODNAME");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }


        string _EntryAid;
        string _RequisitionDate;
        string _VisitPurpose;
        string _VisitPlace;
        string _DepartdateInd;
        string _ArvlDateInd;
        string _HODRcmd;
        string _FilingStatus;
        string _Status;
        string _AppNo;

        public string EntryAid
        {
            get { return _EntryAid; }
            set { _EntryAid = value; }
        }

        public string AppNo
        {
            get { return _AppNo; }
            set { _AppNo = value; }
        }

        public string RequisitionDate
        {
            get { return _RequisitionDate; }
            set {_RequisitionDate = value; }
        }

        public string VisitPurpose
        {
            get { return _VisitPurpose; }
            set { _VisitPurpose = value; }
        }

        public string VisitPlace
        {
            get { return _VisitPlace; }
            set { _VisitPlace = value; }
        }

        
        public string DepartdateInd
        {
            get { return _DepartdateInd; }
            set { _DepartdateInd = value; }
        }

        public string ArvlDateInd
        {
            get { return _ArvlDateInd; }
            set { _ArvlDateInd = value; }
        }

        public string HODRcmd
        {
            get { return _HODRcmd; }
            set { _HODRcmd = value; }
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

        
    }
}