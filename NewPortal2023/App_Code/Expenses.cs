using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public class Expenses
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        public DataSet GetDomesticList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDOMESTICLIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

       

        public DataSet GetApproverDomesticList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVERDOMESTICLIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

 

        public DataSet GetSeqApproverDomesticList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSEQAPPROVERDOMESTICLIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetCategoryType(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCATEGORYTYPE");
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

        

        public DataSet GetMetroReimb(string compValue, string category)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, category.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMETROREIMB");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet GetNonMetroReimb(string compValue, string category)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, category.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETNONMETROREIMB");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet InsertDomesticClaim(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@ENTRMDESC", DBUtilDBType.Varchar, DBUtilDirection.In, 500, EntermDesc == null ? "" : EntermDesc.Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FINYEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FinYears.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELCLASS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TravelClass.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TravelType.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM1_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim1_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM2_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim2_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM3_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim3_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM4_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim4_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM5_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim5_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM6_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim6_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM7_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim7_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM8_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim8_Amount.ToString().Trim()); 
            objDBUtility.AddParameters("@TOTALEXP", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtTotalexp.ToString().Trim());
            objDBUtility.AddParameters("@ELIGIBILITY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtligibility.ToString().Trim());
            objDBUtility.AddParameters("@ADVANCE_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, advance.ToString().Trim());
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@FROM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, From.ToString().Trim());
            objDBUtility.AddParameters("@TO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, To.ToString().Trim());
            objDBUtility.AddParameters("@DESCRIPTION_EXP", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DESCRIPTION_EXP.ToString().Trim());
            objDBUtility.AddParameters("@METROTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, MetroOrNonMetro.ToString().Trim());
            objDBUtility.AddParameters("@NODAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, StayDays.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TravelAmount.ToString().Trim());
            objDBUtility.AddParameters("@ENTAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntAmount.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELRMK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, UserTravelRemarks.ToString().Trim());
            objDBUtility.AddParameters("@ENTRMK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, UserEligiRemark.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FilingStatus.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CategoryType.ToString().Trim());
            objDBUtility.AddParameters("@ADVID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, advid == null ? "" : advid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTDOMCLAIM");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet getDomClaimById(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AppNo.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDOMCLAIMBYID");
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
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDOMCLAIMSTATUSBYID");
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
            objDBUtility.AddParameters("@REVERT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Revert.ToString().Trim());
            objDBUtility.AddParameters("@CHECKREMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CheckerRemark.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Action.ToString().Trim());
            objDBUtility.AddParameters("@APPROVEDAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 500, TotalAmmount == null ? "" : TotalAmmount.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTSTATUSRMK");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public Boolean UpdateFinGlSubmit(string xmlValue, string drpActionAll, string rmkAll, string type)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            // objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@CHECKREMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rmkAll.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpActionAll.ToString().Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, type.ToString().Trim());
            //objDBUtility.AddParameters("@REJECT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, REJECT.ToString().Trim());
            //objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            //objDBUtility.AddParameters("@REVOKE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, REVOKE.ToString().Trim());
            //objDBUtility.AddParameters("@RECALL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, RECALL.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEFINGLSUBMIT");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return true;
        }
        public DataSet GetDOMLimit(string empp_aid, string Claim_no, string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empp_aid.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim_no.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDOMLIMIT");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public void fillEmployee(DropDownList drpList)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLEMPLOYEE");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");


            drpList.Items.Clear();
            drpList.DataTextField = "DESC";
            drpList.DataValueField = "AID";
            drpList.DataSource = ds;
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem("[Select One]", ""));

            objDBUtility.ClearTransactionalParams();
        }
        public DataSet Fill_Report()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDate == null ? "" : FromDate.Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, ToDate == null ? "" : ToDate.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type == null ? "" : Type.Trim());

            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCLAIMREPORT");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetAadhaarDetails()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            //objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@AADHAARNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AadhaarNo.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAADHAAR");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetPanDetails()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            //objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@PANNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PanNo.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPAN");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_AADHAARPAN");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        internal DataSet fillAdhharData(string nameLabel, string genderLabel, string dobLabel, string addressLabel)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, nameLabel == null ? "" : nameLabel.Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, genderLabel == null ? "" : genderLabel.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, dobLabel == null ? "" : dobLabel.Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, addressLabel == null ? "" : addressLabel.Trim());

            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLADHHARDATA");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        internal DataSet tokengenerate()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "TOKENGENERATE");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");
            return ds;
        }
        internal DataSet tokenINSERT(string token)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;
            objDBUtility.AddParameters("@TOKEN", DBUtilDBType.Varchar, DBUtilDirection.In, 50, token == null ? "" : token.Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "TOKENINSERT");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");
            return ds;
        }

        internal DataSet GetwelfareList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETWELFARELIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet InsertStaffDomesticClaim(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@ENTRMDESC", DBUtilDBType.Varchar, DBUtilDirection.In, 500, EntermDesc == null ? "" : EntermDesc.Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FINYEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FinYears.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELCLASS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TravelClass.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TravelType.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM1_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim1_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM2_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim2_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM3_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim3_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM4_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim4_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM5_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim5_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM6_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim6_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM7_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim7_Amount.ToString().Trim());
            objDBUtility.AddParameters("@CLAIM8_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Claim8_Amount.ToString().Trim());
            objDBUtility.AddParameters("@TOTALEXP", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtTotalexp.ToString().Trim());
            objDBUtility.AddParameters("@ELIGIBILITY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtligibility.ToString().Trim());
            objDBUtility.AddParameters("@ADVANCE_AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, advance.ToString().Trim());
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@FROM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, From.ToString().Trim());
            objDBUtility.AddParameters("@TO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, To.ToString().Trim());
            objDBUtility.AddParameters("@DESCRIPTION_EXP", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DESCRIPTION_EXP.ToString().Trim());
            objDBUtility.AddParameters("@METROTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, MetroOrNonMetro.ToString().Trim());
            objDBUtility.AddParameters("@NODAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, StayDays.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, TravelAmount.ToString().Trim());
            objDBUtility.AddParameters("@ENTAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntAmount.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELRMK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, UserTravelRemarks.ToString().Trim());
            objDBUtility.AddParameters("@ENTRMK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, UserEligiRemark.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FilingStatus.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@CATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CategoryType.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTSTAFFDOMCLAIM");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet GetSeqApproverDomesticwelfareList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSEQAPPROVERDOMESTICWELFARELIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetChkStatuswelfare(string compValue, string empValue, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDOMWELFARECLAIMSTATUSBYID");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet InsertwelfareStatus(string compValue, string empValue, string entryAid, string type, string radiochkent)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, type.ToString().Trim());
            objDBUtility.AddParameters("@CHECKED", DBUtilDBType.Varchar, DBUtilDirection.In, 50, radiochkent.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@REVERT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Revert.ToString().Trim());
            objDBUtility.AddParameters("@CHECKREMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CheckerRemark.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Action.ToString().Trim());
            objDBUtility.AddParameters("@APPROVEDAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 500, TotalAmmount == null ? "" : TotalAmmount.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTSTATUSRMKDOMWELFARE");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public Boolean UpdatewelfareFinGlSubmit(string xmlValue, string drpActionAll, string rmkAll, string type)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            // objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@CHECKREMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rmkAll.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpActionAll.ToString().Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, type.ToString().Trim());
            //objDBUtility.AddParameters("@REJECT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, REJECT.ToString().Trim());
            //objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode.ToString().Trim());
            //objDBUtility.AddParameters("@EMPNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpName.ToString().Trim());
            //objDBUtility.AddParameters("@REVOKE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, REVOKE.ToString().Trim());
            //objDBUtility.AddParameters("@RECALL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, RECALL.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEWELFAREFINGLSUBMIT");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return true;
        }
        public DataSet GetempadvanceList(string compValue, string empValue, string type)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@TYPES", DBUtilDBType.Varchar, DBUtilDirection.In, 50, type.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPADVANCELIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        public DataSet GetCount(string compValue, string empValue, string type)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@TYPES", DBUtilDBType.Varchar, DBUtilDirection.In, 50, type.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPADVANCECOUNT");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet INSERT_PETTYEXPENSEDETAILS(string Column, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());

            //objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());


            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTPETTYEXPENSEDETAILS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet Fill_KeyAccList(string EmpCode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLGVLIST");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }


        public DataSet INSERT_PETTYEXPENSEDETAILS(string Column, string compValue, string empValue, string Flag)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTPettyExpenseStatement");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet InsertPettyReimb(string empMid, string empAid)
        {
            objDBUtility = new DBUtility();
            DataSet dsLogin = null;
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTPETTYREIMB");
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, empMid.ToString().Trim());
            objDBUtility.AddParameters("@Status", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Status.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, empAid.ToString().Trim());
            objDBUtility.AddParameters("@ExpenseType", DBUtilDBType.Varchar, DBUtilDirection.In, 100, ExpenseType.ToString().Trim());
            objDBUtility.AddParameters("@Date", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Date.ToString().Trim());
            objDBUtility.AddParameters("@Onbehalfof", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Onbehalfof.ToString().Trim());
            objDBUtility.AddParameters("@Natureofexpense", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Natureofexpense.ToString().Trim());
            objDBUtility.AddParameters("@Billno", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Billno.ToString().Trim());
            objDBUtility.AddParameters("@Amount", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Amount.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 100, TravelType.ToString().Trim());

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet UpdatePettyReimb()
        {
            objDBUtility = new DBUtility();
            DataSet dsLogin = null;
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEPETTYREIMB");
            
            objDBUtility.AddParameters("@Status", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Status.ToString().Trim());
            
            objDBUtility.AddParameters("@ExpenseType", DBUtilDBType.Varchar, DBUtilDirection.In, 100, ExpenseType.ToString().Trim());
            objDBUtility.AddParameters("@Date", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Date.ToString().Trim());
            objDBUtility.AddParameters("@Onbehalfof", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Onbehalfof.ToString().Trim());
            objDBUtility.AddParameters("@Natureofexpense", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Natureofexpense.ToString().Trim());
            objDBUtility.AddParameters("@Billno", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Billno.ToString().Trim());
            objDBUtility.AddParameters("@Amount", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Amount.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 100, TravelType.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 100, AppNo.ToString().Trim());

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPettyReimb(string empMid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPETTYREIMB");
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, empMid.ToString().Trim());
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPettyReimbById(string empMid, string id)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETALLPETTYREIMBYENTRYID");
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, empMid.ToString().Trim());
            objDBUtility.AddParameters("@EXPENSES_NO", DBUtilDBType.Varchar, DBUtilDirection.In, 100, id.ToString().Trim());
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet FinalUpdatePettyRemb()
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FINALUPDATEPETTYREMB");
            objDBUtility.AddParameters("@REMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 100, ReportingRemarks.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 100, AppNo.ToString().Trim());
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet UpdateById(string empMid, string id)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETALLPETTYREIMBYID");
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, empMid.ToString().Trim());
            objDBUtility.AddParameters("@EXPENSES_NO", DBUtilDBType.Varchar, DBUtilDirection.In, 100, id.ToString().Trim());
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet DeleteByEntryId(string empMid, string id)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "DELETEBYENTRYID");
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, empMid.ToString().Trim());
            objDBUtility.AddParameters("@EXPENSES_NO", DBUtilDBType.Varchar, DBUtilDirection.In, 100, id.ToString().Trim());
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetStatus(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSTATUS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetApproverList(string empMid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "APPROVERBYLIST");
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, empMid.ToString().Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Type.ToString().Trim());
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet ApproverAction(string empCode)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;
            

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "APPROVERACTION");
            objDBUtility.AddParameters("@REMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 100, ReportingRemarks.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Action.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, empCode.ToString().Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Status.ToString().Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Type.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 100, AppNo.ToString().Trim());
            objDBUtility.AddParameters("@APPROVEDAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 500, TotalAmmount == null ? "" : TotalAmmount.Trim());
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        //dnyaneshwar
        public void fillExpensetype(DropDownList drpExpensetype)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLDRPEXPENSETYPE");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");


            drpExpensetype.Items.Clear();
            drpExpensetype.DataTextField = "GL_Desc";
            drpExpensetype.DataValueField = "GL_Code";
            drpExpensetype.DataSource = ds;
            drpExpensetype.DataBind();
            drpExpensetype.Items.Insert(0, new ListItem("[Select One]", ""));


            objDBUtility.ClearTransactionalParams();
        }
        //dnyaneshwar

        public DataSet GetEmpTypeCode()
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;


            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPTYPECODE");
          
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 100, AppNo.ToString().Trim());
            
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public Boolean UpdateAllPettyExpSubmit(string xmlValue, string drpActionAll, string rmkAll, string empCode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@Xml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, empCode.ToString().Trim());
            objDBUtility.AddParameters("@REMARK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rmkAll.ToString().Trim());
            objDBUtility.AddParameters("@ACTIONTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpActionAll.ToString().Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Type.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEALLPETTYEXPSUBMIT");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return true;
        }


        public DataSet GetSeqSupportingomesticList(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSUPPORTINGDOMLIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }


        public DataSet GetSupportingList(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSUPPORTINGMISCLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetSupportingTeleList(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSUPPORTINGTELELIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetSupportingLocalList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSUPPORTINGLOCALLIST");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_AgeReport()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
            //objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type == null ? "" : Type.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPAGEPROFILING");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_ReportTenure()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type == null ? "" : Type.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPTENURE");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_ReportOtandAmt()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
            objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            // objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type == null ? "" : Type.Trim());
            objDBUtility.AddParameters("@PAYMONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, MONTH == null ? "" : MONTH.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETOTANDAMT");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_ReportPaidctc()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
            //objDBUtility.AddParameters("@EMPCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            // objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type == null ? "" : Type.Trim());
            objDBUtility.AddParameters("@PAYMONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 50, MONTH == null ? "" : MONTH.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPAIDCTC");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_Reportatt()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDANCE");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_ReporNjAndAtt()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Type == null ? "" : Type.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEAVEDATA");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_Reportact(string month, string year, string joinPdate)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
            objDBUtility.AddParameters("@JoinDate", DBUtilDBType.Varchar, DBUtilDirection.In, 50, joinPdate.ToString().Trim());
            objDBUtility.AddParameters("@LeaveYear", DBUtilDBType.Varchar, DBUtilDirection.In, 50, year.ToString().Trim());
            objDBUtility.AddParameters("@LeaveMonth", DBUtilDBType.Varchar, DBUtilDirection.In, 50, month.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETACTIVEEMP");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_ReportPreviousMonthCost()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPREVIOUSMONTHPAIDCTCAVG");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_AttendanceReport()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDate == null ? "" : FromDate.Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, ToDate == null ? "" : ToDate.Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETATTENDANCEREPORT");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public void fillEmployeeMid(DropDownList drpList)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLEMPLOYEEMID");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");


            drpList.Items.Clear();
            drpList.DataTextField = "DESC";
            drpList.DataValueField = "AID";
            drpList.DataSource = ds;
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem("[Select One]", ""));

            objDBUtility.ClearTransactionalParams();
        }


        string _PanNo;
        string _AadhaarNo;
        string _advid;

        public string advid
        {
            get { return _advid; }
            set { _advid = value; }
        }

        public string AadhaarNo
        {
            get { return _AadhaarNo; }
            set { _AadhaarNo = value; }
        }
        public string PanNo
        {
            get { return _PanNo; }
            set { _PanNo = value; }
        }

        private string _ExpenseType;
        private string _Date;
        private string _Onbehalfof;
        private string _Natureofexpense;
        private string _Billno;
        private string _Amount;




        public string ExpenseType
        {
            get { return _ExpenseType; }
            set { _ExpenseType = value; }
        }

        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        public string Onbehalfof
        {
            get { return _Onbehalfof; }
            set { _Onbehalfof = value; }
        }

        public string Natureofexpense
        {
            get { return _Natureofexpense; }
            set { _Natureofexpense = value; }
        }

        public string Billno
        {
            get { return _Billno; }
            set { _Billno = value; }
        }

        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }
        public string MONTH
        {
            get { return _MONTH; }
            set { _MONTH = value; }
        }


        string _MONTH;

        string _AppNo;
        string _TravelType;
        string _TravelClass;
        string _FromDate;
        string _ToDate;
        string _From;
        string _To;
        string _MetroOrNonMetro;
        string _StayDays;
        string _TravelAmount;
        string _EntAmount;
        string _EnterDesc;
        string _UserEligiRemark;
        string _UserTravelRemarks;
        string _CheckerId;
        string _CheckerRemark;
        string _ReportingId;
        string _ReportingRemarks;
        string _CEOId;
        string _CEORemarks;
        string _FinanaceId;
        string _FinanaceRemarks;
        string _FilingStatus;
        string _Status;
        string _FinYears;
        string _EntryAid;
        string _EntryCode;
        string _TRAVELXML;
        string _EmpCode;
        string _EmpNAME;
        string _Revert;
        string _Remarks;
        string _TotalAmmount;
        string _Type;
        string _EntermDesc;
        string _CategoryType;
        string _DESCRIPTION_EXP;
        string _Action;
        string _Claim1_Amount;
        string _Claim2_Amount;
        string _Claim3_Amount;
        string _Claim4_Amount;
        string _Claim5_Amount;
        string _Claim6_Amount;
        string _Claim7_Amount;
        string _Claim8_Amount;
        string _txtTotalexp;
        string _txtligibility;
        string _advance;
        string _XML;




        public string XML
        {
            get { return _XML; }
            set { _XML = value; }
        }
        public string advance
        {
            get { return _advance; }
            set { _advance = value; }
        }
        public string txtligibility
        {
            get { return _txtligibility; }
            set { _txtligibility = value; }
        }
        public string txtTotalexp
        {
            get { return _txtTotalexp; }
            set { _txtTotalexp = value; }
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
        public string Claim5_Amount
        {
            get { return _Claim5_Amount; }
            set { _Claim5_Amount = value; }
        }
        public string Claim6_Amount
        {
            get { return _Claim6_Amount; }
            set { _Claim6_Amount = value; }
        }
        public string Claim7_Amount
        {
            get { return _Claim7_Amount; }
            set { _Claim7_Amount = value; }
        }
        public string Claim8_Amount
        {
            get { return _Claim8_Amount; }
            set { _Claim8_Amount = value; }
        }
        public string DESCRIPTION_EXP
        {
            get { return _DESCRIPTION_EXP; }
            set { _DESCRIPTION_EXP = value; }
        }
        public string TotalAmmount
        {
            get { return _TotalAmmount; }
            set { _TotalAmmount = value; }
        }
        public string EntermDesc
        {
            get { return _EntermDesc; }
            set { _EntermDesc = value; }
        }
        public string UserTravelRemarks
        {
            get { return _UserTravelRemarks; }
            set { _UserTravelRemarks = value; }
        }

        public string UserEligiRemark
        {
            get { return _UserEligiRemark; }
            set { _UserEligiRemark = value; }
        }

        public string EnterDesc
        {
            get { return _EnterDesc; }
            set { _EnterDesc = value; }
        }

        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
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

        public string MetroOrNonMetro
        {
            get { return _MetroOrNonMetro; }
            set { _MetroOrNonMetro = value; }
        }
        public string StayDays
        {
            get { return _StayDays; }
            set { _StayDays = value; }
        }

        public string TravelAmount
        {
            get { return _TravelAmount; }
            set { _TravelAmount = value; }
        }

        public string EntAmount
        {
            get { return _EntAmount; }
            set { _EntAmount = value; }
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

        public string FinanaceId
        {
            get { return _FinanaceId; }
            set { _FinanaceId = value; }
        }
        public string Revert
        {
            get { return _Revert; }
            set { _Revert = value; }
        }
        public string FinanaceRemarks
        {
            get { return _FinanaceRemarks; }
            set { _FinanaceRemarks = value; }
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

       

    }
}