using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;


namespace NewPortal2023.ESS
{
    public class LocalExpenses
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        public DataSet GeLocalList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLOCALLIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }


        public DataSet GetApproverLocalList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVERLOCALLIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

       

        public DataSet GetLocCategoryType(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLOCCATEGORYTYPE");
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

        public DataSet GetLocalReimb(string compValue, string category)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, category.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLOCALREIMB");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
       
        public DataSet InsertLocClaim(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@REMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, UserRemarks.ToString().Trim());
            objDBUtility.AddParameters("@EXPENSESDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Expenses_Date.ToString().Trim());
            objDBUtility.AddParameters("@CHASVOUCHER", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cash_Voucher.ToString().Trim());
            objDBUtility.AddParameters("@TRAVEL_DESC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Travel_Description.ToString().Trim());
            objDBUtility.AddParameters("@MEAL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Meal.ToString().Trim());
            objDBUtility.AddParameters("@OTHER_EXPENSES", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Other_Expenses.ToString().Trim());
            objDBUtility.AddParameters("@NAME_BUSSI_ASS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Name_Bussi_Ass.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM1_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim1_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM2_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim2_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM3_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim3_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM4_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim4_Amount.ToString().Trim());
            objDBUtility.AddParameters("@ADVANCE_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, advance.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FilingStatus.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CategoryType.ToString().Trim());
            objDBUtility.AddParameters("@APPROVEDAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 500, TotalAmmount == null ? "" : TotalAmmount.Trim());
            objDBUtility.AddParameters("@DESGID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Desg_Aid.ToString().Trim());
            objDBUtility.AddParameters("@ADVID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, advid == null ? "" : advid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTLOCCLAIM");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet getLocalClaimById(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AppNo.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLOCALCLAIMBYID");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet getaPPROVELocalClaimById(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AppNo.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVELOCALCLAIMBYID");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet GetChkStatus(string compValue, string empValue, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLOCALSTATUSBYID");
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
            objDBUtility.AddParameters("@REVERT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, revert.ToString().Trim());
            objDBUtility.AddParameters("@CHECKREMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CheckerRemark.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Action.ToString().Trim());
            objDBUtility.AddParameters("@APPROVEDAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 500, TotalAmmount == null ? "" : TotalAmmount.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTLOCALSTATUSRMK");
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
            //objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, type.ToString().Trim());
            //objDBUtility.AddParameters("@REJECT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, REJECT.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            //objDBUtility.AddParameters("@REVOKE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, REVOKE.ToString().Trim());
            //objDBUtility.AddParameters("@RECALL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, RECALL.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "LOCALAPPROVAL");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return true;
        }


        internal DataSet InsertLocStaffClaim(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@REMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 5000, UserRemarks.ToString().Trim());
            objDBUtility.AddParameters("@EXPENSESDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Expenses_Date.ToString().Trim());
            objDBUtility.AddParameters("@CHASVOUCHER", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cash_Voucher.ToString().Trim());
            objDBUtility.AddParameters("@TRAVEL_DESC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Travel_Description.ToString().Trim());
            objDBUtility.AddParameters("@MEAL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Meal.ToString().Trim());
            objDBUtility.AddParameters("@OTHER_EXPENSES", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Other_Expenses.ToString().Trim());
            objDBUtility.AddParameters("@NAME_BUSSI_ASS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Name_Bussi_Ass.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM1_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim1_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM2_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim2_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM3_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim3_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM4_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim4_Amount.ToString().Trim());
            objDBUtility.AddParameters("@ADVANCE_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, advance.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FilingStatus.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CategoryType.ToString().Trim());
            objDBUtility.AddParameters("@APPROVEDAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 500, TotalAmmount == null ? "" : TotalAmmount.Trim());
            objDBUtility.AddParameters("@DESGID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Desg_Aid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTLOCSTAFFCLAIM");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet getLocalWELFAREClaimById(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AppNo.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLOCALWELFARECLAIMBYID");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GeLocalwelfareList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLOCALWELFARELIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet GetApproverLocalwelfareList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVERLOCALWELFARELIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet getaPPROVELocalwelfareClaimById(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AppNo.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVELOCALWELFARECLAIMBYID");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet GetEmpName(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTEMPNAME");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;

        }

        public DataSet InsertAdvanceClaim(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMP_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpNAME.ToString().Trim());
            objDBUtility.AddParameters("@EXPENSESDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Expenses_Date.ToString().Trim());
            objDBUtility.AddParameters("@TYPES", DBUtilDBType.Varchar, DBUtilDirection.In, 50, expensetype.ToString().Trim());
            objDBUtility.AddParameters("@TRAVEL_DESC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Travel_Description.ToString().Trim());
            objDBUtility.AddParameters("@CHASVOUCHER", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cash_Voucher.ToString().Trim());
            objDBUtility.AddParameters("@ADVANCE_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, advance == null ? "" : advance.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTADVANCECLAIM");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetAdvanceList(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
      
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETADVANCELIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet getAdvanceClaimById(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AppNo.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETADVANCECLAIMBYID");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }


        string _AppNo;
        string _Expenses_Date;
        string _Cash_Voucher;
        string _Travel_Description;
        string _Meal;
        string _Other_Expenses;
        string _Name_Bussi_Ass;
        string _Claim1_Amount;
        string _Claim2_Amount;
        string _Claim3_Amount;
        string _Claim4_Amount;
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
        string _Action;
        string _Desg_Aid;
        string _COMP_AID;
        string _CategoryType;
        string _UserRemarks;
        string _TotalAmmount;
        string _revert;
        string _advance;
        string _expensetype;
        string _advid;

        public string advid
        {
            get { return _advid; }
            set { _advid = value; }
        }
        public string expensetype
        {
            get { return _expensetype; }
            set { _expensetype = value; }
        }
        public string advance
        {
            get { return _advance; }
            set { _advance = value; }
        }
        public string revert
        {
            get { return _revert; }
            set { _revert = value; }
        }

        public string COMP_AID
        {
            get { return _COMP_AID; }
            set { _COMP_AID = value; }
        }
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }
        public string Desg_Aid
        {
            get { return _Desg_Aid; }
            set { _Desg_Aid = value; }
        }


        public string CategoryType
        {
            get { return _CategoryType; }
            set { _CategoryType = value; }
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
        public string Cash_Voucher
        {
            get { return _Cash_Voucher; }
            set { _Cash_Voucher = value; }
        }
        public string Expenses_Date
        {
            get { return _Expenses_Date; }
            set { _Expenses_Date = value; }
        }

        public string Travel_Description
        {
            get { return _Travel_Description; }
            set { _Travel_Description = value; }
        }
        public string Meal
        {
            get { return _Meal; }
            set { _Meal = value; }
        }
        public string TotalAmmount
        {
            get { return _TotalAmmount; }
            set { _TotalAmmount = value; }
        }

        public string Other_Expenses
        {
            get { return _Other_Expenses; }
            set { _Other_Expenses = value; }
        }

        public string Name_Bussi_Ass
        {
            get { return _Name_Bussi_Ass; }
            set { _Name_Bussi_Ass = value; }
        }

        public string Claim1_Amount
        {
            get { return _Claim1_Amount; }
            set { _Claim1_Amount = value; }
        }
        public string Claim2_Amount
        {
            get { return _Claim2_Amount; }
            set { _Claim2_Amount = value; }
        }
        public string Claim3_Amount
        {
            get { return _Claim3_Amount; }
            set { _Claim3_Amount = value; }
        }
        public string Claim4_Amount
        {
            get { return _Claim4_Amount; }
            set { _Claim4_Amount = value; }
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

        public string ReportingId
        {
            get { return _ReportingId; }
            set { _ReportingId = value; }
        }
        public string UserRemarks
        {
            get { return _UserRemarks; }
            set { _UserRemarks = value; }
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