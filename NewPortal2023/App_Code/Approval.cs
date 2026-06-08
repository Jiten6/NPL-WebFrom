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


    public class Approval
    {
        private DBUtility objDBUtility;
        NewPortal2023.ESS.Common objCommon;

        public Approval()
        {

        }
        
        public void Fill_currency(DropDownList drpList)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GLCREATIONCURRENCY");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");


            drpList.Items.Clear();
            drpList.DataTextField = "AID";
            drpList.DataValueField = "DESC";
            drpList.DataSource = ds;
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem("[Select Currency]", ""));

            objDBUtility.ClearTransactionalParams();
        }

        public void Fill_CurrencyEdit(DropDownList drpList)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GLCREATIONCURRENCYEDIT");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");


            drpList.Items.Clear();
            drpList.DataTextField = "DESCC";
            drpList.DataValueField = "AID";
            drpList.DataSource = ds;
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem("[Select Currency]", ""));

            objDBUtility.ClearTransactionalParams();
        }

        public void Fill_AcountType(DropDownList drpList)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GLCREATIONACCOUNTTYPE");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");


            drpList.Items.Clear();
            drpList.DataTextField = "AID";
            drpList.DataValueField = "DESC";
            drpList.DataSource = ds;
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem("[Select One]", ""));

            objDBUtility.ClearTransactionalParams();
        }

        public void Fill_AcountTypeEdit(DropDownList drpList)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GLCREATIONACCOUNTTYPEEDIT");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");


            drpList.Items.Clear();
            drpList.DataTextField = "AID";
            drpList.DataValueField = "DESC";
            drpList.DataSource = ds;
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem("[Select One]", ""));

            objDBUtility.ClearTransactionalParams();
        }

        public void fill_SolID(DropDownList drpList)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLBRANCHNAME");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");


            drpList.Items.Clear();
            drpList.DataTextField = "BRANCH_AID";
            drpList.DataValueField = "BRANCH_DESC";
            drpList.DataSource = ds;
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem("[Select One]", ""));

            objDBUtility.ClearTransactionalParams();
        }

        public void fill_SolIDEDIT(DropDownList drpList)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLBRANCHNAMEEDIT");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");


            drpList.Items.Clear();
            drpList.DataTextField = "BRANCH_DESC";
            drpList.DataValueField = "BRANCH_AID";
            drpList.DataSource = ds;
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem("[Select One]", ""));

            objDBUtility.ClearTransactionalParams();
        }
        public void fill_Partion(DropDownList drpList)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLPARTITION");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");


            drpList.Items.Clear();
            drpList.DataTextField = "PARTITION_DESC";
            drpList.DataValueField = "PARTITION_AID";
            drpList.DataSource = ds;
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem("[Select One]", ""));

            objDBUtility.ClearTransactionalParams();
        }
        public void Fill_GenralLegerType(DropDownList drpList)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLGLTYPE");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");


            drpList.Items.Clear();
            drpList.DataTextField = "GLTYPE_DESC";
            drpList.DataValueField = "GLTYPE_AID";
            drpList.DataSource = ds;
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem("[Select One]", ""));

            objDBUtility.ClearTransactionalParams();
        }
        public void Fill_GenralLegerTypeEdit(DropDownList drpList)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLGLTYPEEDIT");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");


            drpList.Items.Clear();
            drpList.DataTextField = "GLTYPE_DESC";
            drpList.DataValueField = "GLTYPE_AID";
            drpList.DataSource = ds;
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem("[Select One]", ""));

            objDBUtility.ClearTransactionalParams();
        }
        public DataSet Fill_SolDescription(string solid)
        {
            objDBUtility = new DBUtility();

            DataSet ds;
            objDBUtility.AddParameters("@SOLID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, solid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLSOLDESCRIPTION");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet CreateInsertGL()
        {

            objDBUtility = new DBUtility();
            DataSet dsIns = null;

            objDBUtility.AddParameters("@GLNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, GLNO.ToString().Trim());
            objDBUtility.AddParameters("@SolId", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SolId.ToString().Trim());
            objDBUtility.AddParameters("@SolDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SolDescription.ToString().Trim());
            objDBUtility.AddParameters("@Currency", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Currency.ToString().Trim());
            objDBUtility.AddParameters("@GLType", DBUtilDBType.Varchar, DBUtilDirection.In, 50, GLType.ToString().Trim());
            objDBUtility.AddParameters("@AccountType", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AccountType.ToString().Trim());
            objDBUtility.AddParameters("@AccountDesc", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AccountDesc.ToString().Trim());
            objDBUtility.AddParameters("@BusinessRationale", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BusinessRationale.ToString().Trim());
            objDBUtility.AddParameters("@NatureOfAc", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NatureOfAc.ToString().Trim());
            objDBUtility.AddParameters("@CashTrEnb", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CashTrEnb.ToString().Trim());
            objDBUtility.AddParameters("@CashPlaceholder", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CashPlaceholder.ToString().Trim());
            objDBUtility.AddParameters("@ManualTrAllw", DBUtilDBType.Varchar, DBUtilDirection.In, 100, ManualTrAllw.ToString().Trim());
            objDBUtility.AddParameters("@AnyTrAllw", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AnyTrAllw.ToString().Trim());
            objDBUtility.AddParameters("@Partition", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Partition.ToString().Trim());
            objDBUtility.AddParameters("@AsynBalanceUp", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AsynBalanceUp.ToString().Trim());
            objDBUtility.AddParameters("@DebitCreditFreezeReq", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DebitCreditFreezeReq.ToString().Trim());
            objDBUtility.AddParameters("@FreezeReason", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FreezeReason.ToString().Trim());
            objDBUtility.AddParameters("@CashLimitDr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CashlimitDr.ToString().Trim());
            objDBUtility.AddParameters("@CashLimitCr", DBUtilDBType.Varchar, DBUtilDirection.In, 100, CashLimitCr.ToString().Trim());
            objDBUtility.AddParameters("@CashClrLimitDr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CashClrlimitDr.ToString().Trim());
            objDBUtility.AddParameters("@CashClrLimitCr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CashClrLimitCr.ToString().Trim());
            objDBUtility.AddParameters("@TransferLimitDr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TransferlimitDr.ToString().Trim());
            objDBUtility.AddParameters("@TransferLimitCr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TransferlimitCr.ToString().Trim());
            objDBUtility.AddParameters("@BalLimitDr", DBUtilDBType.Varchar, DBUtilDirection.In, 100, BalLimitDr.ToString().Trim());
            objDBUtility.AddParameters("@UsersAccessprovided", DBUtilDBType.Varchar, DBUtilDirection.In, 50, UsersAccessprovided.ToString().Trim());
            objDBUtility.AddParameters("@PrimaryOwnerEmail", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PrimaryOwnerEmail.ToString().Trim());
            objDBUtility.AddParameters("@OwnerDepartment", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OwnerDepartment.ToString().Trim());
            objDBUtility.AddParameters("@RecPeriodOwnership", DBUtilDBType.Varchar, DBUtilDirection.In, 100, RecPeriodOwnership.ToString().Trim());
            objDBUtility.AddParameters("@Product", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Product.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CREATINSERTGL");

            dsIns = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            objDBUtility.ClearTransactionalParams();


            return dsIns;
        }

        public void Fill_viewlist(GridView vwList, string pageIndex, string pageSize, string status, string userLevel, string level)
        //public void Fill_viewlist(GridView vwList, string pageIndex, string pageSize, string UserLevel)
        {
            objDBUtility = new DBUtility();

            DataSet ds;
            //objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDate.ToString().Trim());
            //objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@USERTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, UserType.ToString().Trim());
            objDBUtility.AddParameters("@LEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, level.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@PAGEINDEX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageIndex.ToString().Trim());
            objDBUtility.AddParameters("@PAGESIZE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageSize.ToString().Trim());
            objDBUtility.AddParameters("@USERLEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userLevel.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "VIEWGLINFO");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");
            vwList.DataSource = ds;
            vwList.DataBind();

            if (vwList.HeaderRow != null)
            {
                vwList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            objDBUtility.ClearTransactionalParams();
        }
        public void Fill_Ownerviewlist(GridView vwList, string pageIndex, string pageSize, string status, string userLevel, string level)
        //public void Fill_viewlist(GridView vwList, string pageIndex, string pageSize, string UserLevel)
        {
            objDBUtility = new DBUtility();

            DataSet ds;
            objDBUtility.AddParameters("@USERTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, UserType.ToString().Trim());
            objDBUtility.AddParameters("@LEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, level.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status.ToString().Trim());
            objDBUtility.AddParameters("@PAGEINDEX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageIndex.ToString().Trim());
            objDBUtility.AddParameters("@PAGESIZE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageSize.ToString().Trim());
            objDBUtility.AddParameters("@USERLEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userLevel.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "VIEWOWNERGLINFO");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");
            vwList.DataSource = ds;
            vwList.DataBind();

            if (vwList.HeaderRow != null)
            {
                vwList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            objDBUtility.ClearTransactionalParams();
        }
        public void DeleteParamDept(string DateCr, string PrimaryName)
        {
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@PrimaryOwnerEmail", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PrimaryName.ToString().Trim());
            objDBUtility.AddParameters("@DateCr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DateCr.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "DELETEDATECR");

            objDBUtility.Execute_StoreProc("SP_APPROVAL_GL");

            objDBUtility.ClearTransactionalParams();
        }
        public DataSet Fill_TransactionRemark()
        {
            objDBUtility = new DBUtility();
            DataSet dsRe = null;

            objDBUtility.AddParameters("@PrimaryOwnerEmail", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PrimaryOwnerEmail.ToString().Trim());
            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLTRANSACTIONREMARK");

            dsRe = objDBUtility.Execute_StoreProc_DataSet("SP_GL");

            objDBUtility.ClearTransactionalParams();
            return dsRe;

        }


        public void FilliEtherRemark()
        {
            objDBUtility = new DBUtility();
            DataSet dsRe = null;


            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLEITHERREMARK");

            dsRe = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            objDBUtility.ClearTransactionalParams();
           

        }

        public void Fill_Updatelist(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            //objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDate.ToString().Trim());
            //objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@Level", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Level.ToString().Trim());
            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@PAGEINDEX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageIndex.ToString().Trim());
            objDBUtility.AddParameters("@USERLEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, UserLevel.ToString().Trim());
            objDBUtility.AddParameters("@PAGESIZE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageSize.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLUPDATELIST");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            vwList.DataSource = ds;
            vwList.DataBind();

            if (vwList.HeaderRow != null)
            {
                vwList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            if (ds.Tables[2].Rows[0]["COUNTS"].ToString()!=null)
            {
                COUNT = ds.Tables[2].Rows[0]["COUNTS"].ToString();
            }
            if (ds.Tables[3].Rows[0]["EMPCOUNT"].ToString() != null)
            {
                EMPCOUNT = ds.Tables[3].Rows[0]["EMPCOUNT"].ToString();
            }
            if (ds.Tables[4].Rows[0]["EMPETHERCOUNT"].ToString() != null)
            {
                EMPETHERCOUNT = ds.Tables[4].Rows[0]["EMPETHERCOUNT"].ToString();
            }
            objDBUtility.ClearTransactionalParams();
        }

        public void Fill_HistoryDashBoard(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds;
            
           
            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLDASHBORADLIST");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            vwList.DataSource = ds;
            vwList.DataBind();

           
            objDBUtility.ClearTransactionalParams();
        }


        public DataSet Fill_Checkbox()
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLUPDATELIST");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");
            objDBUtility.ClearTransactionalParams();
            return ds;
        }
        public void UpdateDetails(string GLCode, string SolID)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@GLCode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, GLCode.ToString().Trim());
            objDBUtility.AddParameters("@SOLID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SolID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETUPDATEDETAIL");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");
            objDBUtility.ClearTransactionalParams();

            if (ds.Tables[0].Rows.Count > 0)
            {
                GLNO = ds.Tables[0].Rows[0]["GL_CODE"].ToString();
                SolId = ds.Tables[0].Rows[0]["SOL_ID"].ToString();
                SolDescription = ds.Tables[0].Rows[0]["SOL_DESCRIPTION"].ToString();
                Currency = ds.Tables[0].Rows[0]["CURRENCY"].ToString();
                GLType = ds.Tables[0].Rows[0]["GENERAL_LEDGER_TYPE"].ToString();
                AccountType = ds.Tables[0].Rows[0]["ACCOUNT_TYPE"].ToString();
                AccountDesc = ds.Tables[0].Rows[0]["ACCOUNT_DESCRIPTION"].ToString();
                BusinessRationale = ds.Tables[0].Rows[0]["BUSINESS_RATIONALE"].ToString();
                NatureOfAc = ds.Tables[0].Rows[0]["NATURE_OF_ACCOUNT"].ToString();
                CashTrEnb = ds.Tables[0].Rows[0]["CASH_TRANSACTION_ENB_RATIONALE"].ToString();
                CashPlaceholder = ds.Tables[0].Rows[0]["CASH_PLACEHOLDER"].ToString();
                ManualTrAllw = ds.Tables[0].Rows[0]["MANUAL_TRANSAC_ALLWED"].ToString();
                AnyTrAllw = ds.Tables[0].Rows[0]["ANYWHERE_TRANSAC_ALLWED"].ToString();
                Partition = ds.Tables[0].Rows[0]["PARTITION"].ToString();
                AsynBalanceUp = ds.Tables[0].Rows[0]["ACCT_BALANCE_UP"].ToString();
                DebitCreditFreezeReq = ds.Tables[0].Rows[0]["DEBIT_CREDIT_FREEZE"].ToString();
                FreezeReason = ds.Tables[0].Rows[0]["FREEZE_REASON"].ToString();
                CashlimitDr = ds.Tables[0].Rows[0]["CASH_LIMIT_DR"].ToString();
                CashLimitCr = ds.Tables[0].Rows[0]["CASH_LIMIT_CR"].ToString();
                CashClrlimitDr = ds.Tables[0].Rows[0]["CLG_LIMIT_DR"].ToString();
                CashClrLimitCr = ds.Tables[0].Rows[0]["CLG_LIMIT_CR"].ToString();
                TransferlimitDr = ds.Tables[0].Rows[0]["TRANSFER_LIMIT_DR"].ToString();
                TransferlimitCr = ds.Tables[0].Rows[0]["TRANSFER_LIMIT_CR"].ToString();
                BalLimitDr = ds.Tables[0].Rows[0]["BALANCE_LIMIT_DR"].ToString();
                UsersAccessprovided = ds.Tables[0].Rows[0]["USER_ACCESS_PROVIDED"].ToString();
                PrimaryOwnerEmail = ds.Tables[0].Rows[0]["PRIMARY_OWNER_EMAIL"].ToString();
                OwnerDepartment = ds.Tables[0].Rows[0]["OWNER_DEPARTMENT"].ToString();
                RecPeriodOwnership = ds.Tables[0].Rows[0]["RECONCILIATION_PERIOD_OWNERSHIP"].ToString();
                Product = ds.Tables[0].Rows[0]["PRODUCT"].ToString();
                CreatorRemark = ds.Tables[0].Rows[0]["CREATOR_REMARK"].ToString();
            }


        }
        public DataSet FILL_FiananceFields(string GLCode, string SolID)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@GLCode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, GLCode.ToString().Trim());
            objDBUtility.AddParameters("@SOLID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SolID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "VIEWFIANANCELIST");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_FIANANCE_GL");
            objDBUtility.ClearTransactionalParams();
            return ds;
            
        }

        public DataSet FILL_FiananceFieldsExcel()
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "VIEWFIANANCELISTEXCEL");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_FIANANCE_GL");
            objDBUtility.ClearTransactionalParams();
            return ds;

        }
        public void updateGLDetail()
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@GLNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, GLNO.ToString().Trim());
            objDBUtility.AddParameters("@SolId", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SolId.ToString().Trim());
            objDBUtility.AddParameters("@SolDescription", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SolDescription.ToString().Trim());
            objDBUtility.AddParameters("@GLType", DBUtilDBType.Varchar, DBUtilDirection.In, 50, GLType.ToString().Trim());
            objDBUtility.AddParameters("@AccountType", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AccountType.ToString().Trim());
            objDBUtility.AddParameters("@Currency", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Currency.ToString().Trim());
            objDBUtility.AddParameters("@AccountDesc", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AccountDesc.ToString().Trim());
            objDBUtility.AddParameters("@BusinessRationale", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BusinessRationale.ToString().Trim());
            objDBUtility.AddParameters("@NatureOfAc", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NatureOfAc.ToString().Trim());
            objDBUtility.AddParameters("@CashTrEnb", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CashTrEnb.ToString().Trim());
            objDBUtility.AddParameters("@CashPlaceholder", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CashPlaceholder.ToString().Trim());
            objDBUtility.AddParameters("@ManualTrAllw", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ManualTrAllw.ToString().Trim());
            objDBUtility.AddParameters("@AnyTrAllw", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AnyTrAllw.ToString().Trim());
            objDBUtility.AddParameters("@Partition", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Partition.ToString().Trim());
            objDBUtility.AddParameters("@AsynBalanceUp", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AsynBalanceUp.ToString().Trim());
            objDBUtility.AddParameters("@DebitCreditFreezeReq", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DebitCreditFreezeReq.ToString().Trim());
            objDBUtility.AddParameters("@FreezeReason", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FreezeReason.ToString().Trim());
            objDBUtility.AddParameters("@CashLimitDr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CashClrLimitCr.ToString().Trim());
            objDBUtility.AddParameters("@CashLimitCr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CashLimitCr.ToString().Trim());
            objDBUtility.AddParameters("@CashClrLimitDr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CashClrlimitDr.ToString().Trim());
            objDBUtility.AddParameters("@CashClrLimitCr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CashClrLimitCr.ToString().Trim());
            objDBUtility.AddParameters("@TransferLimitDr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TransferlimitDr.ToString().Trim());
            objDBUtility.AddParameters("@TransferLimitCr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TransferlimitCr.ToString().Trim());
            objDBUtility.AddParameters("@BalLimitDr", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BalLimitDr.ToString().Trim());
            objDBUtility.AddParameters("@UsersAccessprovided", DBUtilDBType.Varchar, DBUtilDirection.In, 50, UsersAccessprovided.ToString().Trim());
            objDBUtility.AddParameters("@PrimaryOwnerEmail", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PrimaryOwnerEmail.ToString().Trim());
            objDBUtility.AddParameters("@OwnerDepartment", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OwnerDepartment.ToString().Trim());
            objDBUtility.AddParameters("@RecPeriodOwnership", DBUtilDBType.Varchar, DBUtilDirection.In, 50, RecPeriodOwnership.ToString().Trim());
            objDBUtility.AddParameters("@Product", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Product.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEGLDETAIL");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");
            objDBUtility.ClearTransactionalParams();

        }

        public void Fill_DetailsSearch(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@SolId", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SolId.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ActionType.ToString().Trim());

            //objDBUtility.AddParameters("@CHALLANDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ChallanDate.ToString().Trim());
            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@PAGEINDEX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageIndex.ToString().Trim());
            objDBUtility.AddParameters("@PAGESIZE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageSize.ToString().Trim());
           
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDETAILSSEARCH");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            //vwList.VirtualItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());

            vwList.DataSource = ds;
            vwList.DataBind();

            if (vwList.HeaderRow != null)
            {
                vwList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            objDBUtility.ClearTransactionalParams();
        }

        public void UpdateApproveCheckData()
        {
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@REMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Remarks.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ActionType.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@Level", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Level.ToString().Trim());
            objDBUtility.AddParameters("@GLNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, GLNO.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEAPPROVECHECKDATA");

            objDBUtility.Execute_StoreProc("SP_APPROVAL_GL");
            objDBUtility.ClearTransactionalParams();
        }

        public void DeleteGLCode(string GLCode)
        {
            objDBUtility = new DBUtility();


            objDBUtility.AddParameters("@GLCode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, GLCode.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "DELETEGLCODE");

            objDBUtility.Execute_StoreProc("SP_APPROVAL_GL");

            objDBUtility.ClearTransactionalParams();
        }
        
         public DataSet UpdateAllGlSubmit()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ActionType.ToString().Trim());
            objDBUtility.AddParameters("@REMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Remark.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@LEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Level.ToString().Trim());
            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEALLGLSUBMIT");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet UpdateChkGlSubmit()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@Level", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Level.ToString().Trim());
            objDBUtility.AddParameters("@USERLEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, UserLevel.ToString().Trim());
            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEGLSUBMIT");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public Boolean UpdateUnChkGlSubmit(string xmlValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@Level", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Level.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEUNCHKGLAPPROVE");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            objDBUtility.ClearTransactionalParams();

            return true;
        }

        public void UpdateGLTransactionId()
        {
            objDBUtility = new DBUtility();


            objDBUtility.AddParameters("@GLCode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, GLNO.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATETRANACTIONIDBYGLCODE");

            objDBUtility.Execute_StoreProc("SP_APPROVAL_GL");

            objDBUtility.ClearTransactionalParams();
        }

        public void ApproveRoleStatus()
        {
            objDBUtility = new DBUtility();

            DataSet ds;
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "APRROVEROLESTATUS");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            if (ds.Tables[0].Rows.Count > 0)
            {
                Status = ds.Tables[0].Rows[0]["STATUS"].ToString();
                UserLevel = ds.Tables[0].Rows[0]["USER_LEVEL"].ToString();
                UserType = ds.Tables[0].Rows[0]["USER_TYPE"].ToString();
                Level = ds.Tables[0].Rows[0]["LEVEL"].ToString();
               
            }
            objDBUtility.ClearTransactionalParams();
        }


        public void RevertGLCode(string GLCode,string xmlValue)
        {
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@PrimaryOwnerEmail", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PrimaryOwnerEmail.ToString().Trim());
            objDBUtility.AddParameters("@LEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Level.ToString().Trim());
            objDBUtility.AddParameters("@USERLEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, UserLevel.ToString().Trim());
            objDBUtility.AddParameters("@GLCode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, GLCode.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEREVOKEGLCODEMASTER");

            objDBUtility.Execute_StoreProc("SP_APPROVAL_GL");

            objDBUtility.ClearTransactionalParams();
        }

        public void RejectedByTransactionId()
        {
            objDBUtility = new DBUtility();

            
            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@PrimaryOwnerEmail", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PrimaryOwnerEmail.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "REJECTEDBYTRANSACTIONID");

            objDBUtility.Execute_StoreProc("SP_APPROVAL_GL");

            objDBUtility.ClearTransactionalParams();
        }



        public void FillTransactionRemark()
        {
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@REMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Remark.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ActionType.ToString().Trim());
            objDBUtility.AddParameters("@LEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Level.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@USERLEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, UserLevel.ToString().Trim());
            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@PrimaryOwnerEmail", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PrimaryOwnerEmail.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "TRANSACTIONREMARK");

            objDBUtility.Execute_StoreProc("SP_APPROVAL_GL");
            objDBUtility.ClearTransactionalParams();
        }
        public Boolean AllGlSubmit(string xmlValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@REMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Remarks.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ActionType.ToString().Trim());
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            objDBUtility.AddParameters("@Level", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Level.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "ALLGLAPPROVEBYXML");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            objDBUtility.ClearTransactionalParams();

            return true;
        }

        public string UploadMultiApprove(string strPath, string strRoot)
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
                query = "SELECT * FROM [FinanceCheck$A3:BN];";

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
                    string level = Level.ToString();
                    int rows = Convert.ToInt16(level) - 1;
                    string action = "ACTIONTYPE" + rows;
                    string Remark = "REMARKS" + rows;
                    int A = 0;
                    int R = 0;
                    if (action == "ACTIONTYPE1")
                    {
                        A = 7;
                        R = 8;
                    }
                    if (action == "ACTIONTYPE2")
                    {
                        A = 10;
                        R = 11;
                    }
                    if (action == "ACTIONTYPE3")
                    {
                        A = 13;
                        R = 14;
                    }
                    if (action == "ACTIONTYPE5")
                    {
                        A = 29;
                        R = 30;
                    }
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Columns[0].ColumnName != "Sr_No")
                        {
                            status = "Incorrect file format";
                            return status;
                        }

                        if (dt.Columns[A].ColumnName != action)
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[R].ColumnName != Remark)
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                        for (int cnt = 0; cnt <= dt.Rows.Count - 1; cnt++)
                        {
                            int Rowno = cnt + 1;
                            string Columns = string.Empty;


                            string ActionApp = (Convert.ToString(dt.Rows[cnt][action]));
                            
                                string Sr_No = (Convert.ToString(dt.Rows[cnt]["Sr_No"]));
                                if (Sr_No == "")
                                {
                                    break;
                                }
                                else
                                {
                                     //StatusTEst = "";

                                    if (ActionApp == "Approve")
                                    {
                                    string StatusTEst = "";
                                    }
                                    else if (ActionApp == "Reject")
                                    {

                                    string StatusTEst = "";
                                    }
                                    else if (ActionApp == "Revert")
                                    {

                                    string StatusTEst = "";
                                    }
                                    else
                                    {
                                        Columns = "SR. No." + " " + Sr_No + ". " + "You have typed " + " " + ActionApp + ". " + "Kindly correct the spelling (Approve/Reject/Revert) in the Template and re-upload" ;
                                        status = status + Columns + " , ";
                                    }
                                   
                                
                                }

                        }

                        if (status == "")
                        {
                            sbDetails.Append("<ROOT>");
                            for (int cnt = 0; cnt <= dt.Rows.Count - 1; cnt++)
                            {
                                string Sr_No = (Convert.ToString(dt.Rows[cnt]["Sr_No"]));
                                if (Sr_No == "")
                                {
                                    break;
                                }
                                else
                                {
                                    sbDetails.Append("<UP Sr_No='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Sr_No"])) + "'");
                                    sbDetails.Append(" ACTIONTYPE='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt][action])) + "'");
                                    sbDetails.Append(" REMARKS='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt][Remark])) + "'/>");
                                }
                            }
                            sbDetails.Append("</ROOT>");


                            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
                            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());

                            objDBUtility.AddParameters("@LEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Level.ToString().Trim());
                            objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
                            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 10000000, sbDetails.ToString());
                            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPLOADMULTIAPPROVE");

                            ds = new DataSet();
                            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

                            string countTable = ds.Tables.Count.ToString();

                            objDBUtility.ClearTransactionalParams();
                        }
                        if (status == "")
                        {
                            status = "The File is Uploaded Successfully,Please click on submit button to proceed.";

                        }
                        else
                        {
                            return status;
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
        public DataSet DownloadSample()
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLUPDATELIST");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");
            objDBUtility.ClearTransactionalParams();
            return ds;
        }
        public void Fill_DetailsAVSearch(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            //objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@CREATOR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Creator.ToString().Trim());
            //objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Date.ToString().Trim());
            objDBUtility.AddParameters("@LEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Level.ToString().Trim());
            objDBUtility.AddParameters("@PAGEINDEX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageIndex.ToString().Trim());
            objDBUtility.AddParameters("@PAGESIZE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageSize.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDETAILSAVSEARCH");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            //vwList.VirtualItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());

            vwList.DataSource = ds;
            vwList.DataBind();

            if (vwList.HeaderRow != null)
            {
                vwList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            objDBUtility.ClearTransactionalParams();
        }



        public DataSet DownloadSampleEXCEL()
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLUPDATELISTEXCEL");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_FIANANCE_CHECK_GL");
            objDBUtility.ClearTransactionalParams();
            return ds;
        }
        public DataSet FillRemark(string GLCode, string SolID)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@GLCode", DBUtilDBType.Varchar, DBUtilDirection.In, 50, GLCode.ToString().Trim());
            objDBUtility.AddParameters("@SOLID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SolID.ToString().Trim());
            objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLREMARK");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_FIANANCE_CHECK_GL");
            objDBUtility.ClearTransactionalParams();
            return ds;
        }
        public void ApproveList(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            //objDBUtility.AddParameters("@TRANSACTIONID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TRANSACTIONID.ToString().Trim());
            objDBUtility.AddParameters("@CREATOR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Creator.ToString().Trim());
            //objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Date.ToString().Trim());
            objDBUtility.AddParameters("@LEVEL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Level.ToString().Trim());
            objDBUtility.AddParameters("@PAGEINDEX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageIndex.ToString().Trim());
            objDBUtility.AddParameters("@PAGESIZE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pageSize.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDETAILSAVSEARCH");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPROVAL_GL");

            //vwList.VirtualItemCount = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());

            vwList.DataSource = ds;
            vwList.DataBind();

            if (vwList.HeaderRow != null)
            {
                vwList.HeaderRow.TableSection = TableRowSection.TableHeader;
            }

            objDBUtility.ClearTransactionalParams();
        }

        string _GLNO;
        string _SolId;
        string _SolDescription;
        string _Currency;
        string _GLType;
        string _AccountType;
        string _AccountDesc;
        string _BusinessRationale;
        string _NatureOfAc;
        string _CashTrEnb;
        string _CashPlaceholder;
        string _ManualTrAllw;
        string _AnyTrAllw;
        string _Partition;
        string _AsynBalanceUp;
        string _DebitCreditFreezeReq;
        string _FreezeReason;
        string _CashlimitDr;
        string _CashLimitCr;
        string _CashClrlimitDr;
        string _CashClrLimitCr;
        string _TransferlimitDr;
        string _TransferlimitCr;
        string _BalLimitDr;
        string _UsersAccessprovided;
        string _PrimaryOwnerName;
        string _OwnerDepartment;
        string _RecPeriodOwnership;
        string _Product;
        string _TRANSACTIONID;
        string _EmpCode;
        string _Status;
        string _UserType;
        string _UserLevel;
        string _Level;
        string _Remark;
        string _Glsubheadcode;
        string _GLSHDesc;
        string _Bacid;
        string _BacidDesc;
        string _ForacidCreation;
        string _AccDiscription;
        string _CONSBalFlg;
        string _Emp_Name;
        string _Remarks;
        string _ActionType;
        string _Date;
        string _drpAction;
        string _Reapprove;
        string _COUNT;
        string _FromDate;
        string _ToDate;
        string _EMPCOUNT;
        string _EMPETHERCOUNT;
        string _CreatorRemark;
        string _Creator;
        public string Creator
        {
            get { return _Creator; }
            set { _Creator = value; }
        }

        public string CreatorRemark
        {
            get { return _CreatorRemark; }
            set { _CreatorRemark = value; }
        }
        public string EMPETHERCOUNT
        {
            get { return _EMPETHERCOUNT; }
            set { _EMPETHERCOUNT = value; }
        }
        public string EMPCOUNT
        {
            get { return _EMPCOUNT; }
            set { _EMPCOUNT = value; }
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
        public string COUNT
        {
            get { return _COUNT; }
            set { _COUNT = value; }
        }
        public string Reapprove
        {
            get { return _Reapprove; }
            set { _Reapprove = value; }
        }
        public string drpAction
        {
            get { return _drpAction; }
            set { _drpAction = value; }
        }
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        public string ActionType
        {
            get { return _ActionType; }
            set { _ActionType = value; }
        }

        public string EmpName
        {
            get { return _Emp_Name; }
            set { _Emp_Name = value; }
        }


        public string CONSBalFlg
        {
            get { return _CONSBalFlg; }
            set { _CONSBalFlg = value; }
        }
        public string ForacidCreation
        {
            get { return _ForacidCreation; }
            set { _ForacidCreation = value; }
        }
        public string AccDiscription
        {
            get { return _AccDiscription; }
            set { _AccDiscription = value; }
        }
        public string Bacid
        {
            get { return _Bacid; }
            set { _Bacid = value; }
        }
        public string BacidDesc
        {
            get { return _BacidDesc; }
            set { _BacidDesc = value; }
        }
        public string Glsubheadcode
        {
            get { return _Glsubheadcode; }
            set { _Glsubheadcode = value; }
        }
        public string GLSHDesc
        {
            get { return _GLSHDesc; }
            set { _GLSHDesc = value; }
        }

        public string Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
        public string Level
        {
            get { return _Level; }
            set { _Level = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public string UserLevel
        {
            get { return _UserLevel; }
            set { _UserLevel = value; }
        }
        public string UserType
        {
            get { return _UserType; }
            set { _UserType = value; }
        }
        public string EmpCode
        {
            get { return _EmpCode; }
            set { _EmpCode = value; }
        }
       
        public string TRANSACTIONID
        {
            get { return _TRANSACTIONID; }
            set { _TRANSACTIONID = value; }
        }
        public string GLNO
        {
            get { return _GLNO; }
            set { _GLNO = value; }
        }
        public string SolId
        {
            get { return _SolId; }
            set { _SolId = value; }
        }
        public string SolDescription
        {
            get { return _SolDescription; }
            set { _SolDescription = value; }
        }
        public string Currency
        {
            get { return _Currency; }
            set { _Currency = value; }
        }
        public string GLType
        {
            get { return _GLType; }
            set { _GLType = value; }
        }
        public string AccountType
        {
            get { return _AccountType; }
            set { _AccountType = value; }
        }
        public string AccountDesc
        {
            get { return _AccountDesc; }
            set { _AccountDesc = value; }
        }
        public string BusinessRationale
        {
            get { return _BusinessRationale; }
            set { _BusinessRationale = value; }
        }
        public string NatureOfAc
        {
            get { return _NatureOfAc; }
            set { _NatureOfAc = value; }
        }
        public string CashTrEnb
        {
            get { return _CashTrEnb; }
            set { _CashTrEnb = value; }
        }
        public string CashPlaceholder
        {
            get { return _CashPlaceholder; }
            set { _CashPlaceholder = value; }
        }
        public string ManualTrAllw
        {
            get { return _ManualTrAllw; }
            set { _ManualTrAllw = value; }
        }
        public string AnyTrAllw
        {
            get { return _AnyTrAllw; }
            set { _AnyTrAllw = value; }
        }
        public string Partition
        {
            get { return _Partition; }
            set { _Partition = value; }
        }
        public string AsynBalanceUp
        {
            get { return _AsynBalanceUp; }
            set { _AsynBalanceUp = value; }
        }
        public string DebitCreditFreezeReq
        {
            get { return _DebitCreditFreezeReq; }
            set { _DebitCreditFreezeReq = value; }
        }
        
        public string FreezeReason
        {
            get { return _FreezeReason; }
            set { _FreezeReason = value; }
        }
        public string CashlimitDr
        {
            get { return _CashlimitDr; }
            set { _CashlimitDr = value; }
        }
        public string CashLimitCr
        {
            get { return _CashLimitCr; }
            set { _CashLimitCr = value; }
        }
        public string CashClrlimitDr
        {
            get { return _CashClrlimitDr; }
            set { _CashClrlimitDr = value; }
        }
        public string CashClrLimitCr
        {
            get { return _CashClrLimitCr; }
            set { _CashClrLimitCr = value; }
        }
        public string TransferlimitDr
        {
            get { return _TransferlimitDr; }
            set { _TransferlimitDr = value; }
        }
        public string TransferlimitCr
        {
            get { return _TransferlimitCr; }
            set { _TransferlimitCr = value; }
        }
        public string BalLimitDr
        {
            get { return _BalLimitDr; }
            set { _BalLimitDr = value; }
        }
        public string UsersAccessprovided
        {
            get { return _UsersAccessprovided; }
            set { _UsersAccessprovided = value; }
        }
        public string PrimaryOwnerEmail
        {
            get { return _PrimaryOwnerName; }
            set { _PrimaryOwnerName = value; }
        }
        public string OwnerDepartment
        {
            get { return _OwnerDepartment; }
            set { _OwnerDepartment = value; }
        }
        public string RecPeriodOwnership
        {
            get { return _RecPeriodOwnership; }
            set { _RecPeriodOwnership = value; }
        }
        public string Product
        {
            get { return _Product; }
            set { _Product = value; }
        }
        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

    }
}

