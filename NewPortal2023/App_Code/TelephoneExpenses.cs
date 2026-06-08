using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace NewPortal2023.ESS
{
    public class TelephoneExpenses
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        public DataSet GetTeleList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());

            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELECLIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetTelLimit(string empp_aid, string Claim_no,string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empp_aid.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim_no.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELELIMIT");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetApproverTeleList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVERTELELIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetRole(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETROLE");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        //public DataSet GetTeleCategoryType(string compValue, string empValue)
        //{
        //    objDBUtility = new DBUtility();

        //    DataSet dsEmpData = null;

        //    objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
        //    objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
        //    objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELECATEGORYTYPE");
        //    dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

        //    objDBUtility.ClearTransactionalParams();

        //    return dsEmpData;
        //}
        public DataSet GetTeleCategoryType(string compValue, string empValue, string Flag)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELECATEGORYTYPE");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetFinYear(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETFINYEAR");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetHandSetReimb(string compValue, string category,string desgid)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, category.ToString().Trim());
            objDBUtility.AddParameters("@DESGID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, desgid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETHANDSETREIMB");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet GetTeleReimb(string compValue, string category, string desgid)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, category.ToString().Trim());
            objDBUtility.AddParameters("@DESGID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, desgid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELEREIMB");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet InsertTeleClaim(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;


            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            //objDBUtility.AddParameters("@FINYEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FinYears.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CategoryAid.ToString().Trim());
            objDBUtility.AddParameters("@DESGID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Desg_Aid.ToString().Trim());
            objDBUtility.AddParameters("@GROUP_TYPE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Group_Type_Aid.ToString().Trim());
            objDBUtility.AddParameters("@GROUP_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Group_Type.ToString().Trim());
            objDBUtility.AddParameters("@PHONENO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PhoneNumber.ToString().Trim());
            objDBUtility.AddParameters("@BILLNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BillNo.ToString().Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type.ToString().Trim());
            objDBUtility.AddParameters("@BILLDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BillDate.ToString().Trim());
            objDBUtility.AddParameters("@BILLMONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BillMonth.ToString().Trim());
            objDBUtility.AddParameters("@BILLAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BillAmt.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FilingStatus.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@Description_HT_Exp", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Description_HT_Exp.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTTELECLAIM");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet InsertHandsetClaim(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            //objDBUtility.AddParameters("@FINYEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FinYears.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CategoryAid.ToString().Trim());
            objDBUtility.AddParameters("@DESGID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Desg_Aid.ToString().Trim());
            objDBUtility.AddParameters("@GROUP_TYPE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Group_Type_Aid.ToString().Trim());
            objDBUtility.AddParameters("@GROUP_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Group_Type.ToString().Trim());
            
            objDBUtility.AddParameters("@BILLNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BillNo.ToString().Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type.ToString().Trim());
            objDBUtility.AddParameters("@BILLDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BillDate.ToString().Trim());
            
            objDBUtility.AddParameters("@BILLAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BillAmt.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FilingStatus.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@Description_HT_Exp", DBUtilDBType.Varchar, DBUtilDirection.In, 10000, Description_HT_Exp.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTHANDSETCLAIM");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet getTeleClaimById(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AppNo.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELCLAIMBYID");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public Boolean UpdateFinGlSubmit(string xmlValue, string drpActionAll, string rmkAll)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, COMP_AID.ToString().Trim());
            objDBUtility.AddParameters("@CHECKREMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rmkAll.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpActionAll.ToString().Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type.ToString().Trim());
            //objDBUtility.AddParameters("@REJECT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, REJECT.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            //objDBUtility.AddParameters("@REVOKE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, REVOKE.ToString().Trim());
            //objDBUtility.AddParameters("@RECALL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, RECALL.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTTELSTATUSRMK");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return true;
        }
        public DataSet GetChkStatus(string compValue, string empValue, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid == null ? "" : entryAid.Trim()); 
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELSTATUSBYID");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet InsertStatus(string compValue, string empValue, string entryAid, string type, string radiochkent)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, type.ToString().Trim());
            objDBUtility.AddParameters("@CHECKED", DBUtilDBType.Varchar, DBUtilDirection.In, 50, radiochkent.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CategoryType.ToString().Trim());
            objDBUtility.AddParameters("@DESG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Desg_Aid.ToString().Trim());
            //objDBUtility.AddParameters("@Description_HT_Exp", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Description_HT_Exp.ToString().Trim());
            objDBUtility.AddParameters("@CHECKREMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CheckerRemark.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Action.ToString().Trim());
            objDBUtility.AddParameters("@APPROVEDAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 500, TotalAmmount == null ? "" : TotalAmmount.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTTELEPHONESTATUSRMK");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet InsertTelStatus(string compValue, string empValue, string entryAid, string type, string radiochkent)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, type.ToString().Trim());
            objDBUtility.AddParameters("@CHECKED", DBUtilDBType.Varchar, DBUtilDirection.In, 50, radiochkent.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@CHECKREMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CheckerRemark.ToString().Trim());
            //objDBUtility.AddParameters("@Description_HT_Exp", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Description_HT_Exp.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CategoryType.ToString().Trim());
            objDBUtility.AddParameters("@DESG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Desg_Aid.ToString().Trim());

            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Action.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTTELSTATUSRMK");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet CheckBillMonth(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@GROUP_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Group_Type.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@BILLMONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BillMonth.ToString().Trim());

           
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CHECKBILLMONTH");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet CheckHandsetLimit(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());           
            objDBUtility.AddParameters("@GROUP_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Group_Type.ToString().Trim());
          
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CHECKHANDSETLIMIT");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        string _AppNo;
        string _TravelType;
        string _TravelClass;
        string _FromDate;
        string _ToDate;
        string _From;
        string _To;
        string _Type;
       
        string _CheckerId;
        string _CheckerRemark;
        string _ReportingId;
        string _ReportingRemarks;
        string _CEOId;
        string _CEORemarks;
        string _FilingStatus;
        string _Status;
        string _FinYears;
        string _EntryAid;
        string _EntryCode;
        string _TRAVELXML;
        string _EmpCode;
        string _EmpNAME;

        string _CategoryType;

        string _CategoryAid;
        //string _BrandOther;
        //string _IsFinal;



        string _PhoneNumber;
        string _PhoneType;
        string _ServiceProvider;
        string _BillNo;
        string _BillAmt;
        string _BillDate;
        string _BillMonth;
        string _Desg_Aid;
        string _Group_Type_Aid;
        string _Group_Type;
        string _COMP_AID;
        string _Action;
        string _Description_HT_Exp;
        string _TotalAmmount;

        public string CategoryType
        {
            get { return _CategoryType; }
            set { _CategoryType = value; }
        }
        public string Description_HT_Exp
        {
            get { return _Description_HT_Exp; }
            set { _Description_HT_Exp = value; }
        }
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        public string COMP_AID
        {
            get { return _COMP_AID; }
            set { _COMP_AID = value; }
        }

        public string BillMonth
        {
            get { return _BillMonth; }
            set { _BillMonth = value; }
        }
        public string Desg_Aid
        {
            get { return _Desg_Aid; }
            set { _Desg_Aid = value; }
        }
        public string Group_Type_Aid
        {
            get { return _Group_Type_Aid; }
            set { _Group_Type_Aid = value; }
        }
        public string Group_Type
        {
            get { return _Group_Type; }
            set { _Group_Type = value; }
        }
        public string CategoryAid
        {
            get { return _CategoryAid; }
            set { _CategoryAid = value; }
        }
        public string EmpNAME
        {
            get { return _EmpNAME; }
            set { _EmpNAME = value; }
        }
        public string EmpCode
        {
            get { return _EmpCode; }
            set { _EmpCode = value; }
        }


        public string AppNo
        {
            get { return _AppNo; }
            set { _AppNo = value; }
        }
        public string TravelType
        {
            get { return _TravelType; }
            set { _TravelType = value; }
        }
        public string TravelClass
        {
            get { return _TravelClass; }
            set { _TravelClass = value; }
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

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        public string PhoneType
        {
            get { return _PhoneType; }
            set { _PhoneType = value; }
        }

        public string ServiceProvider
        {
            get { return _ServiceProvider; }
            set { _ServiceProvider = value; }
        }

        public string BillNo
        {
            get { return _BillNo; }
            set { _BillNo = value; }
        }

        public string BillAmt
        {
            get { return _BillAmt; }
            set { _BillAmt = value; }
        }

        public string BillDate
        {
            get { return _BillDate; }
            set { _BillDate = value; }
        }
        public string CheckerId
        {
            get { return _CheckerId; }
            set { _CheckerId = value; }
        }

        public string CheckerRemark
        {
            get { return _CheckerRemark; }
            set { _CheckerRemark = value; }
        }

        public string TotalAmmount
        {
            get { return _TotalAmmount; }
            set { _TotalAmmount = value; }
        }

        public string ReportingId
        {
            get { return _ReportingId; }
            set { _ReportingId = value; }
        }


        public string ReportingRemarks
        {
            get { return _ReportingRemarks; }
            set { _ReportingRemarks = value; }
        }

        public string CEOId
        {
            get { return _CEOId; }
            set { _CEOId = value; }
        }




        public string CEORemarks
        {
            get { return _CEORemarks; }
            set { _CEORemarks = value; }
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


        public string FinYears
        {
            get { return _FinYears; }
            set { _FinYears = value; }
        }

        public string EntryAid
        {
            get { return _EntryAid; }
            set { _EntryAid = value; }
        }

        public string EntryCode
        {
            get { return _EntryCode; }
            set { _EntryCode = value; }
        }

        public string TRAVELXML
        {
            get { return _TRAVELXML; }
            set { _TRAVELXML = value; }
        }

        



        //public string REIMAmt
        //{
        //    get { return _REIMAmt; }
        //    set { _REIMAmt = value; }
        //}

        //public string REIMMapLink
        //{
        //    get { return _REIMMapLink; }
        //    set { _REIMMapLink = value; }
        //}

        //public string REIMAcualAmt
        //{
        //    get { return _REIMAcualAmt; }
        //    set { _REIMAcualAmt = value; }
        //}

        //public string REIMTollAmt
        //{
        //    get { return _REIMTollAmt; }
        //    set { _REIMTollAmt = value; }
        //}

        //public string REIMVechNo
        //{
        //    get { return _REIMVechNo; }
        //    set { _REIMVechNo = value; }
        //}








        //public string TypeOfTAX
        //{
        //    get { return _TypeOfTAX; }
        //    set { _TypeOfTAX = value; }
        //}

        //public string TAXBillDate
        //{
        //    get { return _TAXBillDate; }
        //    set { _TAXBillDate = value; }
        //}

        //public string TAXAmount
        //{
        //    get { return _TAXAmount; }
        //    set { _TAXAmount = value; }
        //}
        //public string CarTAX
        //{
        //    get { return _CarTAX; }
        //    set { _CarTAX = value; }
        //}

        //public string EntryTAX
        //{
        //    get { return _EntryTAX; }
        //    set { _EntryTAX = value; }
        //}

        //public string RoadTAX
        //{
        //    get { return _RoadTAX; }
        //    set { _RoadTAX = value; }
        //}

        //public string TypeOfLUP
        //{
        //    get { return _TypeOfLUP; }
        //    set { _TypeOfLUP = value; }
        //}

        //public string LUPBillDate
        //{
        //    get { return _LUPBillDate; }
        //    set { _LUPBillDate = value; }
        //}
        //public string Loading
        //{
        //    get { return _Loading; }
        //    set { _Loading = value; }
        //}

        //public string UnLoading
        //{
        //    get { return _UnLoading; }
        //    set { _UnLoading = value; }
        //}
        //public string PackingCharge
        //{
        //    get { return _Packing_Charge; }
        //    set { _Packing_Charge = value; }
        //}

        //public string LUPAmount
        //{
        //    get { return _LUPAmount; }
        //    set { _LUPAmount = value; }
        //}

        //public string TypeOfFARE
        //{
        //    get { return _TypeOfFARE; }
        //    set { _TypeOfFARE = value; }
        //}

        //public string FAREBillDate
        //{
        //    get { return _FAREBillDate; }
        //    set { _FAREBillDate = value; }
        //}
        //public string FARENoPerson
        //{
        //    get { return _FARENoPerson; }
        //    set { _FARENoPerson = value; }
        //}
        //public string FAREAmount
        //{
        //    get { return _FAREAmount; }
        //    set { _FAREAmount = value; }
        //}

        //public string EmpLocn
        //{
        //    get { return _EmpLocn; }
        //    set { _EmpLocn = value; }
        //}

        //public string EmpAddr
        //{
        //    get { return _EmpAddr; }
        //    set { _EmpAddr = value; }
        //}

        //public string Amount
        //{
        //    get { return _Amount; }
        //    set { _Amount = value; }
        //}

        //public string Vendor
        //{
        //    get { return _Vendor; }
        //    set { _Vendor = value; }
        //}

        //public string InvoiceNo
        //{
        //    get { return _InvoiceNo; }
        //    set { _InvoiceNo = value; }
        //}

        //public string InvoiceDate
        //{
        //    get { return _InvoiceDate; }
        //    set { _InvoiceDate = value; }
        //}

        //public string Brand
        //{
        //    get { return _Brand; }
        //    set { _Brand = value; }
        //}

        //public string BrandOther
        //{
        //    get { return _BrandOther; }
        //    set { _BrandOther = value; }
        //}

        //public string IsFinal
        //{
        //    get { return _IsFinal; }
        //    set { _IsFinal = value; }
        //}

        //public string FilingStatus
        //{
        //    get { return _FilingStatus; }
        //    set { _FilingStatus = value; }
        //}

        //public string Status
        //{
        //    get { return _Status; }
        //    set { _Status = value; }
        //}

        //public string Remarks
        //{
        //    get { return _Remarks; }
        //    set { _Remarks = value; }
        //}

        //public string PhoneNumber
        //{
        //    get { return _PhoneNumber; }
        //    set { _PhoneNumber = value; }
        //}

        //public string PhoneType
        //{
        //    get { return _PhoneType; }
        //    set { _PhoneType = value; }
        //}

        //public string ServiceProvider
        //{
        //    get { return _ServiceProvider; }
        //    set { _ServiceProvider = value; }
        //}

        //public string BillNo
        //{
        //    get { return _BillNo; }
        //    set { _BillNo = value; }
        //}

        //public string BillAmt
        //{
        //    get { return _BillAmt; }
        //    set { _BillAmt = value; }
        //}

        //public string BillDate
        //{
        //    get { return _BillDate; }
        //    set { _BillDate = value; }
        //}

        //public string FromDate
        //{
        //    get { return _FromDate; }
        //    set { _FromDate = value; }
        //}

        //public string ToDate
        //{
        //    get { return _ToDate; }
        //    set { _ToDate = value; }
        //}

        //public string ActiveDate
        //{
        //    get { return _ActiveDate; }
        //    set { _ActiveDate = value; }
        //}

        //public string Active
        //{
        //    get { return _Active; }
        //    set { _Active = value; }
        //}

        //public string EntryAid
        //{
        //    get { return _EntryAid; }
        //    set { _EntryAid = value; }
        //}

        //public string EntryCode
        //{
        //    get { return _EntryCode; }
        //    set { _EntryCode = value; }
        //}

        //public string TravelMode1
        //{
        //    get { return _TravelMode1; }
        //    set { _TravelMode1 = value; }
        //}

        //public string TravelMode2
        //{
        //    get { return _TravelMode2; }
        //    set { _TravelMode2 = value; }
        //}

        //public string TravelModeOther
        //{
        //    get { return _TravelModeOther; }
        //    set { _TravelModeOther = value; }
        //}

        //public string From
        //{
        //    get { return _From; }
        //    set { _From = value; }
        //}

        //public string FromText
        //{
        //    get { return _FromText; }
        //    set { _FromText = value; }
        //}

        //public string To
        //{
        //    get { return _To; }
        //    set { _To = value; }
        //}

        //public string ToText
        //{
        //    get { return _ToText; }
        //    set { _ToText = value; }
        //}

        //public string DepartureDate
        //{
        //    get { return _DepartureDate; }
        //    set { _DepartureDate = value; }
        //}

        //public string DepartureTime
        //{
        //    get { return _DepartureTime; }
        //    set { _DepartureTime = value; }
        //}

        //public string DepartureDateDest
        //{
        //    get { return _DepartureDateDest; }
        //    set { _DepartureDateDest = value; }
        //}

        //public string DepartureTimeDest
        //{
        //    get { return _DepartureTimeDest; }
        //    set { _DepartureTimeDest = value; }
        //}

        //public string ReachDate
        //{
        //    get { return _ReachDate; }
        //    set { _ReachDate = value; }
        //}

        //public string ReachTime
        //{
        //    get { return _ReachTime; }
        //    set { _ReachTime = value; }
        //}

        //public string ReachDateDest
        //{
        //    get { return _ReachDateDest; }
        //    set { _ReachDateDest = value; }
        //}

        //public string ReachTimeDest
        //{
        //    get { return _ReachTimeDest; }
        //    set { _ReachTimeDest = value; }
        //}

        //public string DailyAllowanceDays
        //{
        //    get { return _DailyAllowanceDays; }
        //    set { _DailyAllowanceDays = value; }
        //}

        //public string StayDays
        //{
        //    get { return _StayDays; }
        //    set { _StayDays = value; }
        //}

        //public string VisitReason
        //{
        //    get { return _VisitReason; }
        //    set { _VisitReason = value; }
        //}

        //public string TravelExpense
        //{
        //    get { return _TravelExpense; }
        //    set { _TravelExpense = value; }
        //}

        //public string HotelExpense
        //{
        //    get { return _HotelExpense; }
        //    set { _HotelExpense = value; }
        //}

        //public string DailyAllowance
        //{
        //    get { return _DailyAllowance; }
        //    set { _DailyAllowance = value; }
        //}

        //public string OtherExpense
        //{
        //    get { return _OtherExpense; }
        //    set { _OtherExpense = value; }
        //}

        //public string TRAVELXML
        //{
        //    get { return _TRAVELXML; }
        //    set { _TRAVELXML = value; }
        //}


        //public string FinYears
        //{
        //    get { return _FinYears; }
        //    set { _FinYears = value; }
        //}

        //public string DayName
        //{
        //    get { return _DayName; }
        //    set { _DayName = value; }
        //}
        //public string Date
        //{
        //    get { return _Date; }
        //    set { _Date = value; }
        //}
        //public string DayCategory
        //{
        //    get { return _DayCategory; }
        //    set { _DayCategory = value; }
        //}
        //public string Fromhrs
        //{
        //    get { return _Fromhrs; }
        //    set { _Fromhrs = value; }
        //}
        //public string Tohrs
        //{
        //    get { return _Tohrs; }
        //    set { _Tohrs = value; }
        //}
        //public string Total
        //{
        //    get { return _Total; }
        //    set { _Total = value; }
        //}
        //public string Particulars
        //{
        //    get { return _Particulars; }
        //    set { _Particulars = value; }
        //}
        //public string RembDis
        //{
        //    get { return _RembDis; }
        //    set { _RembDis = value; }
        //}

        //public string EmpNAME
        //{
        //    get { return _EmpNAME; }
        //    set { _EmpNAME = value; }
        //}
        //public string EmpCode
        //{
        //    get { return _EmpCode; }
        //    set { _EmpCode = value; }
        //}
    }
}