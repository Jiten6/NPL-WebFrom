using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public class PerformanceAppraisal
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;
        private NewPortal2023.ESS.Common objCommon;


        public DataSet SubmitEmployeePMS(string Column, string compValue, string empValue, string Flag)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "SUBMITEMPLOYEEPMS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetEmpApprStatus()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPAPPRSTATUS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        
        public DataSet GetCoreCompetencies(string compValue, string empValue, string currentYearString, string quarter)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, currentYearString == null ? "" : currentYearString.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, quarter == null ? "" : quarter.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCORCOMPDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        

        public DataSet GetManagCompetencies(string compValue, string empValue, string currentYearString, string quarter)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, currentYearString == null ? "" : currentYearString.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, quarter == null ? "" : quarter.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMANAGCOMPDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }


        public DataSet Fill_ApprisialOverallScore()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLAPPRISALOVERALLSCORE");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        

        public DataSet GetLeadCompetencies(string compValue, string empValue, string currentYearString, string quarter)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, currentYearString == null ? "" : currentYearString.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, quarter == null ? "" : quarter.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEADCOMPDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }
        
        public DataSet CreateUpdateParamApprisal(string Column, string compValue, string empValue, string Flag)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim()); 
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@REJECTSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, RejectStatus == null ? "" : RejectStatus.Trim());
            objDBUtility.AddParameters("@CYCLE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cycle == null ? "" : Cycle.Trim());
            objDBUtility.AddParameters("@CYCLE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CycleAid == null ? "" : CycleAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTAPPRAISAL");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet RecallKRAStatus(string Column, string compValue, string empValue, string Flag)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@CYCLE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cycle == null ? "" : Cycle.Trim());
            objDBUtility.AddParameters("@CYCLE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CycleAid == null ? "" : CycleAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "RECALLKRASTATUS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet InsertOverallRatingScore(string Column, string compValue, string empValue)
        {
            objDBUtility = new DBUtility(); 
             DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
           
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@KRA_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KraFlag == null ? "" : KraFlag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTOVERALLRATINGSCORERM");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet InsertOverallRatingScoreHOD(string Column, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@KRA_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KraFlag == null ? "" : KraFlag.Trim());
            objDBUtility.AddParameters("@ACTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Action == null ? "" : Action.Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Remarks == null ? "" : Remarks.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTOVERALLRATINGSCOREHOD");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet InsertOverallRatingScoreHR(string Column, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@KRA_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KraFlag == null ? "" : KraFlag.Trim());
            objDBUtility.AddParameters("@ACTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Action == null ? "" : Action.Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Remarks == null ? "" : Remarks.Trim());
            objDBUtility.AddParameters("@EMP_OLD_DESG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OldDesg == null ? "" : OldDesg.Trim());
            objDBUtility.AddParameters("@EMP_NEW_DESG_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewDesgAid == null ? "" : NewDesgAid.Trim());
            objDBUtility.AddParameters("@EMP_NEW_DESGTEXT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewDesgDesc == null ? "" : NewDesgDesc.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTOVERALLRATINGSCOREHR");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet INSERTACTION()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpAID == null ? "" : EmpAID.Trim());

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@RPT_RMK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Remarks == null ? "" : Remarks.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "OVERALLREPORTINGOFFICERRMK");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }
        public DataSet UpdateParamApprisalRM(string Column, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@EMP_RPT_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpRptMID == null ? "" : EmpRptMID.Trim());
            objDBUtility.AddParameters("@EMP_HR_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpHRMID == null ? "" : EmpHRMID.Trim());
            objDBUtility.AddParameters("@EMP_CFO_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCFOMID == null ? "" : EmpCFOMID.Trim());

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@KRA_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KraFlag == null ? "" : KraFlag.Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEAPPRAISALRM");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet UpdateParamApprisalHR(string Column, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@EMP_RPT_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpRptMID == null ? "" : EmpRptMID.Trim());
            objDBUtility.AddParameters("@EMP_HR_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpHRMID == null ? "" : EmpHRMID.Trim());
            objDBUtility.AddParameters("@EMP_CFO_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCFOMID == null ? "" : EmpCFOMID.Trim());

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@KRA_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KraFlag == null ? "" : KraFlag.Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEAPPRAISALHR");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet Fill_ApprisialEmp(string empValue, string CompAid, string Column)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
                objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column.ToString().Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid);
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
                objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLAPPRISIALEMP");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet Fill_Apprisial(string empValue, string CompAid, string Column)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
                objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column.ToString().Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid);
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCode == null ? "" : EmpCode.Trim());
                objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
                objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLAPPRISIAL");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet Fill_ApprisialLIstEMPLIST(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLAPPRISIALGVLISTEMPLOYEE");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        public DataSet Fill_PLPEmployeeList(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@FLAGTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLPLPEMPLOYEELIST");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        public DataSet Fill_KeyAccomploshmentsEMPLIST(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLKEYGVLISTEMPLOYEE");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");
            
            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        public DataSet Fill_KeyAccomploshmentsEMPLISTHOD(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLKEYGVLISTEMPLOYEE");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[1].Rows[0]["FLAG"].ToString() == "RM")
                {
                    vwList.DataSource = ds;
                    vwList.DataBind();
                }
                else if (ds.Tables[1].Rows[0]["FLAG"].ToString() == "HOD")
                {
                    vwList.DataSource = ds;
                    vwList.DataBind();
                }

                //vwList.DataSource = ds;
                //vwList.DataBind();

                if (vwList.HeaderRow != null)
                {
                    vwList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        internal DataSet GetEmpDesignation()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPDESIGNATION");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet Fill_ApprisialLIst()
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLAPPRISIALGVLIST");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet Fill_ApprisialLIstApproval(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@FLAGTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLAPPRISIALGVLISTAPPROVAL");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }



        public DataSet Fill_PLPListApproval(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLPLPLISTAPPROVAL");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }


        /// /////////////traning and devlopment //////////

        public DataSet CreateUpdateParamApprisalTraninAndDev(string Column, string compValue, string empValue, string Flag)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_APPR_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ApprovalAid == null ? "" : ApprovalAid.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@KRA_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KraFlag == null ? "" : KraFlag.Trim());
            objDBUtility.AddParameters("@CYCLE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cycle == null ? "" : Cycle.Trim());
            objDBUtility.AddParameters("@CYCLE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CycleAid == null ? "" : CycleAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTTRAININGANDDEV");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        

        public DataSet UpdateParamApprisalTraninAndDev(string Column, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@EMP_RPT_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpRptMID == null ? "" : EmpRptMID.Trim());
            objDBUtility.AddParameters("@EMP_HR_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpHRMID == null ? "" : EmpHRMID.Trim());
            objDBUtility.AddParameters("@EMP_CFO_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCFOMID == null ? "" : EmpCFOMID.Trim());

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@KRA_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KraFlag == null ? "" : KraFlag.Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATETRAININGANDDEVRM");


            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        
        public DataSet TraninAndDevLeartFromPast(string Column, string compValue, string empValue, string Flag)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTTRAININGANDDEV");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }


        public DataSet TraninAndDevLeartFromPastRPT(string Column, string compValue, string empValue, string Flag)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATETRAININGANDDEV");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet Fill_TrainingAndDev(string EmpAid, string CompAid, string Column)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
                objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column.ToString().Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid);
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
                objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLTRAINANDDEV");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }



        public DataSet Fill_TrainingAndDevEmp(string empValue, string CompAid, string Column)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
                objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column.ToString().Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid);
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
                objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLTRAINANDDEVEMP");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }


        public DataSet Fill_TrainingAndDevLession(string EmpAid, string CompAid, string Column)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
                objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column.ToString().Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid);
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpAid);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLTRAINANDDEVLESSION");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet Fill_TrainingAndDevList(string EmpCode, string CompAid)
        {

            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid == null ? "" : CompAid.Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLTRAINANDDEVLIST");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet Fill_TrainingAndDevListEmp(string EmpCode, string CompAid)
        {

            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid == null ? "" : CompAid.Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLTRAINANDDEVLISTEMP");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet Fill_ApprovalTrainingAndDevList(string EmpCode, string CompAid)
        {

            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid == null ? "" : CompAid.Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
                objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
                objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLAPPROVALTRAINANDDEVLISTONE");
                

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet Fill_RPTTrainingAndDevList(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpAID == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLRPTTRAINANDDEV");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");
            if (ds.Tables.Count > 0)
            {
                vwList.DataSource = ds;
                vwList.DataBind();

                if (vwList.HeaderRow != null)
                {
                    vwList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            objDBUtility.ClearTransactionalParams();
            return ds;
        }


        /// <summary>
        /// //////////////////KEY \\\\\\\\\\\\\\\\\
        /// </summary>

        public DataSet Fill_ACCOMPLISHMENTS(string EmpValue, string CompAid, string Column)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid == null ? "" : CompAid.Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpValue == null ? "" : EmpValue.Trim());
                objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
                objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Status == null ? "" : Status.Trim());
                objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLKEYACCOMPLISHMENTS");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet Fill_ACCOMPLISHMENTSEMP(string EmpValue, string CompAid, string Column)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid == null ? "" : CompAid.Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpValue == null ? "" : EmpValue.Trim());
                objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Status == null ? "" : Status.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLKEYACCOMPLISHMENTSEMP");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }
        public DataSet INSERT_KEYACCOMPLISHMENTS(string Column, string compValue, string empValue, string Flag)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_APPR_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ApprovalAid == null ? "" : ApprovalAid.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@KRA_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KraFlag == null ? "" : KraFlag.Trim());
            objDBUtility.AddParameters("@CYCLE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cycle == null ? "" : Cycle.Trim());
            objDBUtility.AddParameters("@CYCLE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CycleAid == null ? "" : CycleAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTKEYACCOMP");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }
        public DataSet INSERT_KEYlession(string Column, string compValue, string empValue, string Flag)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@LESSION", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Area.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTKeyLESSION");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet UPDATE_KEYACCOMPLISHMENTS(string Column, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@EMP_RPT_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpRptMID == null ? "" : EmpRptMID.Trim());
            objDBUtility.AddParameters("@EMP_HR_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpHRMID == null ? "" : EmpHRMID.Trim());
            objDBUtility.AddParameters("@EMP_CFO_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCFOMID == null ? "" : EmpCFOMID.Trim());

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@KRA_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KraFlag == null ? "" : KraFlag.Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEKEYACCOMPRM");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }
        public DataSet InsertCompentencies(string compValue, string empAId, string rptAId, string quarter, string rating, string descAId, string currentYearString)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rptAId == null ? "" : rptAId.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empAId == null ? "" : empAId.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, currentYearString == null ? "" : currentYearString.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, quarter == null ? "" : quarter.Trim());
            objDBUtility.AddParameters("@RATING_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rating == null ? "" : rating.Trim());
            objDBUtility.AddParameters("@DESC_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, descAId == null ? "" : descAId.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTCOCOMPDETAILS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet UpdateCompentencies(string compValue, string empAId, string rptAId, string quarter, string rating, string descAId, string currentYearString, string entryAID)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rptAId == null ? "" : rptAId.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empAId == null ? "" : empAId.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, currentYearString == null ? "" : currentYearString.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, quarter == null ? "" : quarter.Trim());
            objDBUtility.AddParameters("@RATING_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, rating == null ? "" : rating.Trim());
            objDBUtility.AddParameters("@DESC_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, descAId == null ? "" : descAId.Trim());
            objDBUtility.AddParameters("@ENTRY_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAID == null ? "" : entryAID.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECOCOMPDETAILS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }
        public DataSet InsertOverallScoreCompt(string compValue, string empAId, string quarter, string currentYearString, string type, string score)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empAId == null ? "" : empAId.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, currentYearString == null ? "" : currentYearString.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, quarter == null ? "" : quarter.Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, type == null ? "" : type.Trim());
            objDBUtility.AddParameters("@COMPENTENCIES_SCORE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, score == null ? "" : score.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTOVERALLSCORE");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }


        public DataSet InsertOverallScoreKey(string compValue, string empAId, string quarter, string currentYearString, string type, string score)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empAId == null ? "" : empAId.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, currentYearString == null ? "" : currentYearString.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, quarter == null ? "" : quarter.Trim());
            objDBUtility.AddParameters("@TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, type == null ? "" : type.Trim());
            objDBUtility.AddParameters("@KEY_SCORE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, score == null ? "" : score.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTOVERALLSCORE");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet Fill_ApprisialLIstCompFact(GridView vwList, string index, string size)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLAPPRISIALGVLISTCOMPFACT");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");
            if (ds.Tables.Count > 0)
            {
                vwList.DataSource = ds;
                vwList.DataBind();

                if (vwList.HeaderRow != null)
                {
                    vwList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        internal DataSet Fill_PLPList(GridView vwList, string index, string size)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPLPPAYOUT");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");
            if (ds.Tables.Count > 0)
            {
                vwList.DataSource = ds;
                vwList.DataBind();

                if (vwList.HeaderRow != null)
                {
                    vwList.HeaderRow.TableSection = TableRowSection.TableHeader;
                }
            }
            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        internal DataSet Fill_KeyAccList(string EmpCode, string CompAid, string Column)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid == null ? "" : CompAid.Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLKEYACCLIST");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        internal DataSet GetApproverAid(string compValue, string empAId)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empAId == null ? "" : empAId.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVALAID");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetEmployeeType(string compValue, string empcode)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empcode == null ? "" : empcode.Trim());
            objDBUtility.AddParameters("@FLAGTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPLOYEETYPE");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        //dnyaneshwar
        internal DataSet GetEmployeeWiseType(string compValue, string empAId)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empAId == null ? "" : empAId.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPMSHISTORICDATA");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }




        internal string NewInsertKRA(string strPath, string strRoot, string compValue, string empValue, string Column)
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
                query = "SELECT * FROM [KRA_UPLOAD$A1:BY];";

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
                string xmlCO = string.Empty;

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];

                        if (dt.Columns[0].ColumnName != "AREA")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[1].ColumnName != "METRIC")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[2].ColumnName != "TARGET")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[3].ColumnName != "WEIGHTAGE")
                        {
                            status = "Incorrect file format";
                            return status;
                        }

                        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                        int Rowno = 1;
                        int Qt = Convert.ToInt32(Quarter);


                        if (status == "")
                        {
                            try
                            {
                                string connectionString = "Server=162.215.230.14;Database=SEQUELPAY;uid=payrollservices;pwd=soPL@786;";  //Live Server String
                                
                                if (!dt.Columns.Contains("EMP_CODE"))
                                {
                                    DataColumn colEmp = new DataColumn("EMP_CODE", typeof(string));
                                    dt.Columns.Add(colEmp);
                                    colEmp.SetOrdinal(0); // First column
                                }

                                if (!dt.Columns.Contains("ACC_YEAR"))
                                {
                                    DataColumn accYear = new DataColumn("ACC_YEAR", typeof(string));
                                    dt.Columns.Add(accYear);
                                    accYear.SetOrdinal(1); // Second column
                                }

                                // -----------------------------------
                                // 2️⃣ Fill values
                                // -----------------------------------
                                foreach (DataRow row in dt.Rows)
                                {
                                    row["EMP_CODE"] = empValue;      // your variable
                                    row["ACC_YEAR"] = Year;    // your variable
                                }

                                // -----------------------------------
                                // 3️⃣ Bulk Copy
                                // -----------------------------------
                                using (SqlConnection con = new SqlConnection(connectionString))
                                {
                                    con.Open();

                                    using (SqlBulkCopy copy = new SqlBulkCopy(con))
                                    {
                                        copy.DestinationTableName = "dbo.tempIndividual";

                                        // Safe mapping by column name
                                        foreach (DataColumn column in dt.Columns)
                                        {
                                            copy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                                        }

                                        copy.WriteToServer(dt);
                                    }
                                }
                            }
                            catch (Exception e)
                            {
                                throw new System.Exception("Error: " + e.Message);
                            }

                            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
                            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
                            objDBUtility.AddParameters("@EMP_APPR_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ApprovalAid == null ? "" : ApprovalAid.Trim());
                            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
                            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
                            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
                            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
                            objDBUtility.AddParameters("@CYCLE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cycle == null ? "" : Cycle.Trim());
                            objDBUtility.AddParameters("@CYCLE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CycleAid == null ? "" : CycleAid.Trim());
                            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTKRAINDIVIDUAL");

                            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                            objDBUtility.ClearTransactionalParams();

                            string countTable = ds.Tables.Count.ToString();

                            objDBUtility.ClearTransactionalParams();
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
                            if (status == "")
                            {
                                status = "Successfuly uploaded";

                            }
                            else
                            {
                                return status;
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

        internal string NewUploadEMPKRARM(string strPath, string strRoot, string compValue, string empValue)
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
                query = "SELECT * FROM [KRA_UPLOAD$A1:BY];";

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
                string xmlCO = string.Empty;

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Columns[0].ColumnName != "EMP CODE")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[1].ColumnName != "ACC YEAR")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[2].ColumnName != "AREA")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[3].ColumnName != "GROUP")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[4].ColumnName != "METRIC")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[5].ColumnName != "TARGET")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[6].ColumnName != "WEIGHTAGE")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[7].ColumnName != "ACH/REMARKS")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[8].ColumnName != "ACH")
                        {
                            status = "Incorrect file format";
                            return status;
                        }

                        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                        int Rowno = 1;
                        int Qt = Convert.ToInt32(Quarter);


                        if (status == "")
                        {

                            //string connectionString = "Server=162.215.230.14;Database=SEQUELPAY;uid=payrollservices;pwd=soPL@786;";
                            //string connectionString = "Server=162.215.230.14;Database=NPLUAT;uid=NPLUATUSER1;pwd=npltest@1234;";
                            string connectionString = "Server=4.247.170.238;Database=NPLUAT;uid=sa;pwd=#pa$$w0rd123@321#;";

                            SqlBulkCopy copy = new SqlBulkCopy(connectionString);

                            try
                            {
                                //ArrayList columns = new ArrayList();
                                //columns.Add(ds.Tables[0].Rows[0].ItemArray.Select(x => x.ToString()).ToArray());

                                ArrayList columns = new ArrayList();

                                object[] items = ds.Tables[0].Rows[0].ItemArray;
                                string[] values = new string[items.Length];

                                for (int i = 0; i < items.Length; i++)
                                {
                                    values[i] = items[i] == null ? "" : items[i].ToString();
                                }

                                columns.Add(values);


                                copy.ColumnMappings.Add(0, 0);
                                copy.ColumnMappings.Add(1, 1);
                                copy.ColumnMappings.Add(2, 2);
                                copy.ColumnMappings.Add(3, 3);
                                copy.ColumnMappings.Add(4, 4);
                                copy.ColumnMappings.Add(5, 5);
                                copy.ColumnMappings.Add(6, 6);
                                copy.ColumnMappings.Add(7, 7);
                                copy.ColumnMappings.Add(8, 8);

                                string tableName = "dbo.tempIndividual";
                                copy.DestinationTableName = tableName;
                                copy.WriteToServer(ds.Tables[0]);
                            }
                            catch (Exception e)
                            {
                                throw new System.Exception("Error: " + e.Message);
                            }
                            finally
                            {
                                if (copy != null) { copy.Close(); }

                            }

                            objDBUtility.AddParameters("@EMP_RPT_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
                            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
                            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
                            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
                            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());

                            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTKRAINDIVIDUALRM");

                            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                            objDBUtility.ClearTransactionalParams();

                            string countTable = ds.Tables.Count.ToString();

                            objDBUtility.ClearTransactionalParams();
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    dt = ds.Tables[0];

                                    StringBuilder statusBuilder = new StringBuilder();

                                    for (int cnt = 0; cnt < dt.Rows.Count; cnt++)
                                    {
                                        string result = Convert.ToString(dt.Rows[cnt]["result"]);
                                        if (!string.IsNullOrEmpty(result))
                                        {
                                            if (statusBuilder.Length > 0)
                                            {
                                                statusBuilder.Append(", ");
                                            }
                                            statusBuilder.Append(result);
                                        }
                                    }

                                    status = statusBuilder.ToString();
                                }
                            }
                            if (status == "")
                            {
                                status = "Successfuly uploaded";

                            }
                            else
                            {
                                return status;
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


        internal string NewUploadEMPKRAHR(string strPath, string strRoot, string compValue)
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
                query = "SELECT * FROM [KRA_UPLOAD$A1:BY];";

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
                string xmlCO = string.Empty;

                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        dt = ds.Tables[0];
                        if (dt.Columns[0].ColumnName != "EMP CODE")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[1].ColumnName != "ACC YEAR")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[2].ColumnName != "AREA")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[3].ColumnName != "GROUP")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[4].ColumnName != "METRIC")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[5].ColumnName != "TARGET")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[6].ColumnName != "WEIGHTAGE")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[7].ColumnName != "ACH/REMARKS")
                        {
                            status = "Incorrect file format";
                            return status;
                        }
                        if (dt.Columns[8].ColumnName != "ACH")
                        {
                            status = "Incorrect file format";
                            return status;
                        }


                        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                        int Rowno = 1;
                        int Qt = Convert.ToInt32(Quarter);


                        if (status == "")
                        {
                           
                            string connectionString = "Server=162.215.230.14;Database=SEQUELPAY;uid=payrollservices;pwd=soPL@786;";
                            //string connectionString = "Server=162.215.230.14;Database=NPLUAT;uid=NPLUATUSER1;pwd=npltest@1234;";
                            //string connectionString = "Server=4.247.170.238;Database=NPLUAT;uid=sa;pwd=#pa$$w0rd123@321#;";


                            SqlBulkCopy copy = new SqlBulkCopy(connectionString);

                            try
                            {
                                //ArrayList columns = new ArrayList();
                                //columns.Add(ds.Tables[0].Rows[0].ItemArray.Select(x => x.ToString()).ToArray());

                                ArrayList columns = new ArrayList();

                                object[] items = ds.Tables[0].Rows[0].ItemArray;
                                string[] values = new string[items.Length];

                                for (int i = 0; i < items.Length; i++)
                                {
                                    values[i] = items[i] == null ? "" : items[i].ToString();
                                }

                                columns.Add(values);


                                copy.ColumnMappings.Add(0, 0);
                                copy.ColumnMappings.Add(1, 1);
                                copy.ColumnMappings.Add(2, 2);
                                copy.ColumnMappings.Add(3, 3);
                                copy.ColumnMappings.Add(4, 4);
                                copy.ColumnMappings.Add(5, 5);
                                copy.ColumnMappings.Add(6, 6);
                                copy.ColumnMappings.Add(7, 7);
                                copy.ColumnMappings.Add(8, 8);


                                string tableName = "dbo.tempIndividual";
                                copy.DestinationTableName = tableName;
                                copy.WriteToServer(ds.Tables[0]);
                            }
                            catch (Exception e)
                            {
                                throw new System.Exception("Error: " + e.Message);
                            }
                            finally
                            {
                                if (copy != null) { copy.Close(); }

                            }

                            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
                            objDBUtility.AddParameters("@EMP_APPR_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ApprovalAid == null ? "" : ApprovalAid.Trim());
                            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
                            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
                            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
                            objDBUtility.AddParameters("@CYCLE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cycle == null ? "" : Cycle.Trim());
                            objDBUtility.AddParameters("@CYCLE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CycleAid == null ? "" : CycleAid.Trim());

                            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTKRAINDIVIDUALHR");

                            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                            objDBUtility.ClearTransactionalParams();

                            string countTable = ds.Tables.Count.ToString();

                            objDBUtility.ClearTransactionalParams();
                            if (ds != null)
                            {
                                if (ds.Tables.Count > 0)
                                {
                                    dt = ds.Tables[0];

                                    StringBuilder statusBuilder = new StringBuilder();

                                    for (int cnt = 0; cnt < dt.Rows.Count; cnt++)
                                    {
                                        string result = Convert.ToString(dt.Rows[cnt]["result"]);
                                        if (!string.IsNullOrEmpty(result))
                                        {
                                            if (statusBuilder.Length > 0)
                                            {
                                                statusBuilder.Append(", "); 
                                            }
                                            statusBuilder.Append(result); 
                                        }
                                    }

                                    status = statusBuilder.ToString();
                                }
                            }
                            if (status == "")
                            {
                                status = "Successfuly uploaded";

                            }
                            else
                            {
                                return status;
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

        internal DataSet Fill_TrainingAndDevAll(string EmpAid, string CompAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid);
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpAid);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLTRAINANDDEVALL");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        internal DataSet GetAppraisalStatus(string EmpValue, string CompAid, string Column)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid == null ? "" : CompAid.Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpValue == null ? "" : EmpValue.Trim());
                objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Status == null ? "" : Status.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPRAISALSTATUS");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        internal DataSet Fill_TrainingAndDevApproval(string EmpAid, string CompAid, string Column)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
                objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column.ToString().Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid);
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
                objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
                objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLTRAINANDDEVAPPROVAL");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        internal DataSet Fill_TrainingAndDevAllApproval(string EmpAid, string CompAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, CompAid);
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
                objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
                objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
                
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLTRAINANDDEVALLAPPROVAL");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetIndvidualPercentage(string EmpAid, string CompAid)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETINDIVIDUALPERCENTAGE");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet InsertPLPStatus(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());

            objDBUtility.AddParameters("@EMP_APPR_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ApprovalAid == null ? "" : ApprovalAid.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@EMP_HR_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpHRMID == null ? "" : EmpHRMID.Trim());
            objDBUtility.AddParameters("@EMP_CFO_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCFOMID == null ? "" : EmpCFOMID.Trim());

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());

            objDBUtility.AddParameters("@TARINDIV", DBUtilDBType.Varchar, DBUtilDirection.In, 50, tarIndiv == null ? "" : tarIndiv.Trim());
            objDBUtility.AddParameters("@TARCOMPFACT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, tarCompFact == null ? "" : tarCompFact.Trim());
            objDBUtility.AddParameters("@TARCOMPPROM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, tarCompProm == null ? "" : tarCompProm.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTPLPSTATUS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet FillPLPDataAbv(string empType, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@EMP_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empType == null ? "" : empType.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@COMPFACT1", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtCompFactAbv == null ? "" : txtCompFactAbv.Trim());
            objDBUtility.AddParameters("@COMPFACT2", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtCompPromAbv == null ? "" : txtCompPromAbv.Trim());
            objDBUtility.AddParameters("@COMPPROM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtCompPromAbv20 == null ? "" : txtCompPromAbv20.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLPLPDATAABV");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet FillPLPDataBlw(string empType, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@EMP_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empType == null ? "" : empType.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@COMPFACT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtCompFactBlw == null ? "" : txtCompFactBlw.Trim());
            objDBUtility.AddParameters("@COMPPROM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtCompPromBlw == null ? "" : txtCompPromBlw.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLPLPDATABLW");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet Fill_DetailsOverAllRating(GridView gvList, string index, string size)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpAID == null ? "" : EmpAID.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLDETAILSOVERALLRATING");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet Fill_DetailsOverAllRatingHOD(GridView gvList, string index, string size)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLDETAILSOVERALLRATINGHOD");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet Fill_DetailsOverAllRatingHR(GridView gvList, string index, string size)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLDETAILSOVERALLRATINGHR");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet Fill_OverallRatEmpList(GridView gvList, string index, string size)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpAID == null ? "" : EmpAID.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@CYCLE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cycle == null ? "" : Cycle.Trim());
            objDBUtility.AddParameters("@CYCLE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CycleAid == null ? "" : CycleAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLOVRLRATEMPLOYEELIST");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        internal DataSet Fill_OverallRatEmpListHR(GridView gvList, string index, string size)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLOVRLRATEMPLOYEELISTHR");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();
            return ds;
        }


        internal DataSet GetOverAllRAtingScore(string drpLedBehav, string drpKRARating)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@DRPLEDBEHAV", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpLedBehav == null ? "" : drpLedBehav.Trim());
            objDBUtility.AddParameters("@DRPKRARATING", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpKRARating == null ? "" : drpKRARating.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETOVERALLRATINFSCORE");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetSummaryListRM(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@CYCLE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cycle == null ? "" : Cycle.Trim());
            objDBUtility.AddParameters("@CYCLE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CycleAid == null ? "" : CycleAid.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSUMMARYLISTRM");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetSummaryListHOD(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, empValue == null ? "" : empValue.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSUMMARYLISTHOD");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetSummaryListHR(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, empValue == null ? "" : empValue.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSUMMARYLISTHR");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetCompFactor(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMPFACTOR");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetPMSReport(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPMSREPORT");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetOverallratingReport(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETOVERALLRATINGREPORTDATA");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GettAllEmployeeAppraisalList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETALLEMPAPPRLLIST");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet FillDepartment()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDEPARTMENT");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet FillDeSignation()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDESIGNATION");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet Fillemployee()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@DEPT_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DeptID == null ? "" : DeptID.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPLIST");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                
            }

            return dsInv;
        }

        internal DataSet GetEmpCode(string compValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPCODE");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;

        }

        internal DataSet GetPMSReportAllEmp(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPMSREPORTALLEMP");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetHistoricEmployeeWiseType(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETHISTORICEMPWISE");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetLastPRomotionEmployeeWiseType(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLASTPROMOTIONEMPWISE");
            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }


        //dnyaneshwar

        public DataSet Fill_RdlcReport()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETALLEXPENSEDATA");
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDate == null ? "" : FromDate.Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, ToDate == null ? "" : ToDate.Trim());
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("[SP_APPRAISAL]");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_allApprovedRdlcReport()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETALLAPPROVEDDATA");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("[SP_APPRAISAL]");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_PaidRdlcReport()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPAIDREPORT");
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FromDate == null ? "" : FromDate.Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 500, ToDate == null ? "" : ToDate.Trim());
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("[SP_APPRAISAL]");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet Fill_PaidAllRdlcReport()
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETALLPAIDREPORT");


            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("[SP_APPRAISAL]");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public string UploadPaymentDetails(string strPath, string strRoot)
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
                query = "SELECT * FROM [ReportExpense$];";

                using (var connection = new OleDbConnection(conn))
                {
                    using (var da = new OleDbDataAdapter(query, connection))
                    {
                        DataTable dt = new DataTable();
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

                        //if (dt.Rows[0][9].ToString() == ("Travel + Entertainment"))
                        //{
                        //    status = "Incorrect file format";
                        //    return status;
                        //}
                        //else if (dt.Rows[0][9].ToString() == ("Local"))
                        //{
                        //    status = "Incorrect file format";
                        //    return status;
                        //}
                        //else if(dt.Rows[0][9].ToString() == ("Miscellaneous"))
                        //{
                        //    status = "Incorrect file format";
                        //    return status;
                        //}
                        //else if(dt.Rows[0][9].ToString() == ("Entertainment"))
                        //{
                        //    status = "Incorrect file format";
                        //    return status;
                        //}
                        //else if(dt.Rows[0][9].ToString() == ("TELE"))
                        //{
                        //    status = "Incorrect file format";
                        //    return status;
                        //}

                        //for (int i = 0; i < dt.Rows.Count; i++)
                        //{
                        //    //string columnValue = dt.Rows[i][10].ToString();
                        //    string columnValue = dt.Rows[i][10].ToString();

                        //    if (columnValue == "Travel" ||
                        //        columnValue == "Travel + Entertainment" ||
                        //        columnValue == "Local" ||
                        //        columnValue == "Miscellaneous" ||
                        //        columnValue == "Entertainment" ||
                        //        columnValue == "TELE")
                        //    {
                        //        status = "";
                        //    }
                        //    else
                        //    {
                        //        status = "Invalid";
                        //        return status;
                        //    }
                        //}

                        int initialRowCount = dt.Rows.Count; // Store the initial row count
                        int rowsRemoved = 0;

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (string.IsNullOrWhiteSpace(dt.Rows[i][8].ToString()) && string.IsNullOrWhiteSpace(dt.Rows[i][9].ToString()) && string.IsNullOrWhiteSpace(dt.Rows[i][10].ToString()))
                            {
                                dt.Rows.RemoveAt(i); // Remove the row directly
                                rowsRemoved++;
                            }
                        }



                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            string columnValue = dt.Rows[i][10].ToString();
                            int rowNumber = i + 1 + rowsRemoved;


                            if (!(columnValue == "Travel" ||
                                columnValue == "Travel + Entertainment" ||
                                columnValue == "Local" ||
                                columnValue == "Miscellaneous" ||
                                columnValue == "Entertainment" ||
                                columnValue == "HANDSET" ||
                                columnValue == "TELE"))
                            {

                                status = "Invalid Category in Column EXPENSE TYPE at Row " + rowNumber;
                                return status;
                            }


                            if (!ValidationDataType.Double.Equals(dt.Rows[i][8]) && string.IsNullOrEmpty(dt.Rows[i][8].ToString()))
                            {
                                status = "Invalid entry in Claim_Amount column at Row " + rowNumber;
                                return status;
                            }
                            if (!ValidationDataType.Double.Equals(dt.Rows[i][9]) && string.IsNullOrEmpty(dt.Rows[i][9].ToString()))
                            {
                                status = "Invalid entry in ClaimApproved_Amount column at Row " + rowNumber;
                                return status;
                            }


                            status = "";
                        }


                        TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                        DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
                        int Row_No = 1;
                        int Qt = Convert.ToInt32(Quarter);


                        for (int cnt = 0; cnt <= dt.Rows.Count - 1; cnt++)
                        {
                            Row_No = cnt + 1;
                            string Columns = string.Empty;
                            string Sr_No = (Convert.ToString(dt.Rows[cnt]["Claim no"]));
                            if (Sr_No == "")
                            {
                                break;
                            }
                            else
                            {

                            }
                        }

                        if (status == "")
                        {

                            sbDetails.Append("<ROOT>");
                            for (int cnt = 0; cnt <= dt.Rows.Count - 1; cnt++)
                            {
                                string Sr_No = (Convert.ToString(dt.Rows[cnt]["Claim no"]));
                                if (Sr_No == "")
                                {
                                    break;
                                }
                                else
                                {

                                    sbDetails.Append("<UP Payment_type ='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["PAYMENT TYPE"])) + "'");
                                    sbDetails.Append(" Payment_date='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["PAYMENT DATE"])) + "'");
                                    sbDetails.Append(" Emp_Name= '" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["BENEFECIERY NAME"])) + "'");
                                    sbDetails.Append(" Bank_account_no='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["BANK ACCOUNT NO"])) + "'");
                                    sbDetails.Append(" lfsc='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["IFSC"])) + "'");
                                    sbDetails.Append(" Emp_Mid='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["EMP MID"])) + "'");
                                    sbDetails.Append(" Claim_no='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim no"])) + "'");
                                    sbDetails.Append(" Claim_Date='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim Date"])) + "'");
                                    sbDetails.Append(" Claim_Amount='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim Amount"])) + "'");
                                    sbDetails.Append(" Claim_Approved_Amount='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim Approved Amount"])) + "'");
                                    //sbDetails.Append(" Claim_Amount='" + objCommon.ReplaceSpecialCharacters(Convert.ToDecimal(dt.Rows[cnt]["Claim Amount"]).ToString()) + "'");
                                    //sbDetails.Append(" Claim_Approved_Amount='" + objCommon.ReplaceSpecialCharacters(Convert.ToDecimal(dt.Rows[cnt]["Claim Approved Amount"]).ToString()) + "'");
                                    sbDetails.Append(" Flag= '" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["EXPENSE TYPE"])) + "'/>");


                                    //sbDetails.Append("<UP ='PAYMENT_TYPE" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Payment_type"])) + "'");
                                    //sbDetails.Append(" PAYMENT_DATE='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Payment_date"])) + "'");
                                    //sbDetails.Append(" BENEFECIERY_NAME= '" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Emp_Name"])) + "'");
                                    //sbDetails.Append(" BANK_ACCOUNT_NO='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Bank_account_no"])) + "'");
                                    //sbDetails.Append(" IFSC='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["lfsc"])) + "'");
                                    //sbDetails.Append(" EMP_MID='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Emp_Mid"])) + "'");
                                    //sbDetails.Append(" Claim_no='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim_no"])) + "'");
                                    //sbDetails.Append(" Claim_Date='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim_date"])) + "'");
                                    //sbDetails.Append(" Claim_Amount='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim_Amount"])) + "'");
                                    //sbDetails.Append(" Claim_Approved_Amount='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["ClaimApproved_amount"])) + "'");
                                    //sbDetails.Append(" EXPENSE_TYPE= '" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Flag"])) + "'/>");
                                }

                            }
                            sbDetails.Append("</ROOT>");




                            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 10000000, sbDetails.ToString());
                            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPLOADPAYMENTBILL");

                            ds = new DataSet();
                            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                            string countTable = ds.Tables.Count.ToString();

                            objDBUtility.ClearTransactionalParams();
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
                            if (status == "")
                            {
                                status = "The File is Uploaded Successfully.";

                            }
                            else
                            {
                                return status;
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

        //public string UploadPaymentDetails(string strPath, string strRoot)
        //{
        //    string conn = string.Empty;
        //    string query = string.Empty;
        //    string status = string.Empty;

        //    DataSet ds = new DataSet();
        //    objDBUtility = new DBUtility();
        //    objCommon = new Common();
        //    try
        //    {
        //        conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
        //        query = "SELECT * FROM [ReportExpense$];";

        //        using (var connection = new OleDbConnection(conn))
        //        {
        //            using (var da = new OleDbDataAdapter(query, connection))
        //            {
        //                connection.Open();
        //                da.Fill(ds);
        //            }
        //        }

        //        status = "";
        //    }
        //    catch (Exception ex)
        //    {
        //        status = ex.Message;
        //    }

        //    try
        //    {
        //        DataTable dt = new DataTable();
        //        StringBuilder sbDetails = new StringBuilder();

        //        if (ds != null)
        //        {

        //            if (ds.Tables.Count > 0)
        //            {
        //                dt = ds.Tables[0];


        //                TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        //                DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        //                int Row_No = 1;
        //                int Qt = Convert.ToInt32(Quarter);


        //                for (int cnt = 0; cnt <= dt.Rows.Count - 1; cnt++)
        //                {
        //                    Row_No = cnt + 1;
        //                    string Columns = string.Empty;
        //                    string Sr_No = (Convert.ToString(dt.Rows[cnt]["Claim no"]));
        //                    if (Sr_No == "")
        //                    {
        //                        break;
        //                    }
        //                    else
        //                    {




        //                    }
        //                }

        //                if (status == "")
        //                {

        //                    sbDetails.Append("<ROOT>");
        //                    for (int cnt = 0; cnt <= dt.Rows.Count - 1; cnt++)
        //                    {
        //                        string Sr_No = (Convert.ToString(dt.Rows[cnt]["Claim no"]));
        //                        if (Sr_No == "")
        //                        {
        //                            break;
        //                        }
        //                        else
        //                        {

        //                            sbDetails.Append("<UP Payment_type ='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["PAYMENT TYPE"])) + "'");
        //                            sbDetails.Append(" Payment_date='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["PAYMENT DATE"])) + "'");
        //                            sbDetails.Append(" Emp_Name= '" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["BENEFECIERY NAME"])) + "'");
        //                            sbDetails.Append(" Bank_account_no='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["BANK ACCOUNT NO"])) + "'");
        //                            sbDetails.Append(" lfsc='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["IFSC"])) + "'");
        //                            sbDetails.Append(" Emp_Mid='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["EMP MID"])) + "'");
        //                            sbDetails.Append(" Claim_no='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim no"])) + "'");
        //                            sbDetails.Append(" Claim_Date='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim Date"])) + "'");
        //                            sbDetails.Append(" Claim_Amount='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim Amount"])) + "'");
        //                            sbDetails.Append(" Claim_Approved_Amount='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim Approved Amount"])) + "'");
        //                            sbDetails.Append(" Flag= '" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["EXPENSE TYPE"])) + "'/>");


        //                            //sbDetails.Append("<UP ='PAYMENT_TYPE" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Payment_type"])) + "'");
        //                            //sbDetails.Append(" PAYMENT_DATE='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Payment_date"])) + "'");
        //                            //sbDetails.Append(" BENEFECIERY_NAME= '" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Emp_Name"])) + "'");
        //                            //sbDetails.Append(" BANK_ACCOUNT_NO='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Bank_account_no"])) + "'");
        //                            //sbDetails.Append(" IFSC='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["lfsc"])) + "'");
        //                            //sbDetails.Append(" EMP_MID='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Emp_Mid"])) + "'");
        //                            //sbDetails.Append(" Claim_no='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim_no"])) + "'");
        //                            //sbDetails.Append(" Claim_Date='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim_date"])) + "'");
        //                            //sbDetails.Append(" Claim_Amount='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Claim_Amount"])) + "'");
        //                            //sbDetails.Append(" Claim_Approved_Amount='" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["ClaimApproved_amount"])) + "'");
        //                            //sbDetails.Append(" EXPENSE_TYPE= '" + objCommon.ReplaceSpecialCharacters(Convert.ToString(dt.Rows[cnt]["Flag"])) + "'/>");
        //                        }

        //                    }
        //                    sbDetails.Append("</ROOT>");




        //                    objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 10000000, sbDetails.ToString());
        //                    objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPLOADPAYMENTBILL");

        //                    ds = new DataSet();
        //                    ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

        //                    string countTable = ds.Tables.Count.ToString();

        //                    objDBUtility.ClearTransactionalParams();
        //                    if (ds != null)
        //                    {
        //                        if (ds.Tables.Count > 0)
        //                        {
        //                            dt = ds.Tables[0];

        //                            for (Int32 cnt = 0; cnt <= dt.Rows.Count - 1; cnt++)
        //                            {
        //                                if (Convert.ToString(dt.Rows[cnt]["result"]) != "")
        //                                {
        //                                    status = Convert.ToString(dt.Rows[cnt]["result"]);
        //                                }
        //                            }
        //                        }
        //                    }
        //                    if (status == "")
        //                    {
        //                        status = "The File is Uploaded Successfully.";

        //                    }
        //                    else
        //                    {
        //                        return status;
        //                    }

        //                }

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        status = ex.Message;
        //    }
        //    return status;
        //}

        public DataSet UpdateParamApprisalTraninAndDevHODF(string Column, string compValue, string empValue, string Flag)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@EMP_RPT_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpRptMID == null ? "" : EmpRptMID.Trim());
            objDBUtility.AddParameters("@EMP_HR_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpHRMID == null ? "" : EmpHRMID.Trim());
            objDBUtility.AddParameters("@EMP_CFO_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCFOMID == null ? "" : EmpCFOMID.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
           
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
           
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Remarks == null ? "" : Remarks.Trim());
            objDBUtility.AddParameters("@ACTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Action == null ? "" : Action.Trim());
            objDBUtility.AddParameters("@CYCLE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cycle == null ? "" : Cycle.Trim());
            objDBUtility.AddParameters("@CYCLE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CycleAid == null ? "" : CycleAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATETRAININGANDDEVHODF");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet UPDATE_KEYACCOMPLISHMENTSHODF(string Column, string compValue, string empValue, string Flag)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@EMP_RPT_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpRptMID == null ? "" : EmpRptMID.Trim());
            objDBUtility.AddParameters("@EMP_HR_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpHRMID == null ? "" : EmpHRMID.Trim());
            objDBUtility.AddParameters("@EMP_CFO_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCFOMID == null ? "" : EmpCFOMID.Trim());

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Remarks == null ? "" : Remarks.Trim());
            objDBUtility.AddParameters("@ACTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Action == null ? "" : Action.Trim());
            objDBUtility.AddParameters("@CYCLE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cycle == null ? "" : Cycle.Trim());
            objDBUtility.AddParameters("@CYCLE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CycleAid == null ? "" : CycleAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEKEYACCOMPHODF");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }
        public DataSet Fill_ApprisialLIstEMPLISTHODF(GridView vwList, string pageIndex, string pageSize)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@CYCLE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CycleAid == null ? "" : CycleAid.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, Comp_Aid == null ? "" : Comp_Aid.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 8000, EmpCode == null ? "" : EmpCode.Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid == null ? "" : EntryAid.Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Flag == null ? "" : Flag.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLAPPRISIALGVLISTEMPLOYEE");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        public DataSet UpdateParamApprisalHODF(string Column, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;
            objDBUtility.AddParameters("@XML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, XML.Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
            objDBUtility.AddParameters("@EMP_RPT_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpRptMID == null ? "" : EmpRptMID.Trim());
            objDBUtility.AddParameters("@EMP_HOD_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpHODMID == null ? "" : EmpHODMID.Trim());
            objDBUtility.AddParameters("@EMP_HR_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpHRMID == null ? "" : EmpHRMID.Trim());
            objDBUtility.AddParameters("@EMP_CFO_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpCFOMID == null ? "" : EmpCFOMID.Trim());

            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@STATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status == null ? "" : Status.Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Remarks == null ? "" : Remarks.Trim());
            objDBUtility.AddParameters("@ACTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Action == null ? "" : Action.Trim());
            objDBUtility.AddParameters("@KRA_FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, KraFlag == null ? "" : KraFlag.Trim());
            objDBUtility.AddParameters("@CYCLE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Cycle == null ? "" : Cycle.Trim());
            objDBUtility.AddParameters("@CYCLE_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CycleAid == null ? "" : CycleAid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEAPPRAISALHODF");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet GetPMSStatusReport(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPMSSTATUSREPORT");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        internal DataSet UpdateNewDesignation()
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Comp_Aid == null ? "" : Comp_Aid.Trim());
                objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpMakerMID == null ? "" : EmpMakerMID.Trim());
                objDBUtility.AddParameters("@EMP_OLD_DESG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OldDesg == null ? "" : OldDesg.Trim());
                objDBUtility.AddParameters("@EMP_NEW_DESG_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewDesgAid == null ? "" : NewDesgAid.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATENEWDESIGNATION");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        internal DataSet GetYears(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPMSYEARS");

            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        internal DataSet GetMailidDetails(string compaid, string empaid)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compaid == null ? "" : compaid.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empaid == null ? "" : empaid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMAILIDDETAILS");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");
            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        internal DataSet GetMailidDetailsRM(string compaid, string empaid, string makermid)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compaid == null ? "" : compaid.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empaid == null ? "" : empaid.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, makermid == null ? "" : makermid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMAILIDDETAILSRM");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");
            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        internal DataSet GetMailidDetailsHOD(string compaid, string empaid, string makermid)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compaid == null ? "" : compaid.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empaid == null ? "" : empaid.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, makermid == null ? "" : makermid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMAILIDDETAILSHOD");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");
            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        internal DataSet GetMailidDetailsHR(string compaid, string empaid, string makermid)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compaid == null ? "" : compaid.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empaid == null ? "" : empaid.Trim());
            objDBUtility.AddParameters("@EMP_MAKER_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, makermid == null ? "" : makermid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMAILIDDETAILSHR");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");
            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        internal DataSet GetCyclePhaseDetails(string CompAid, string Year)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompAid == null ? "" : CompAid.Trim());
                objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCYCLEPHASEDETAILS");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        //dnyaneshwar

        string _Area;
        string _EntryAid;
        string _Metric;
        string _Target;
        string _wt;
        string _Weightage;
        string _Remarks;
        string _XML;
        string _SECTION;
        string _Quarter;
        string _Year;
        string _EmpCode;
        string _Comp_Aid;
        string _EmpAID;
        string _Status;
        string _ApprovalAid;
        string _EmpAid;
        String _EmpMID;
        string _EmpMakerMID;
        string _EmpRptMID;
        string _EmpHRMID;
        string _EmpCFOMID;
        string _tarIndiv;
        string _tarCompFact;
        string _tarCompProm;
        string _txtCompFactAbv;
        string _txtCompPromAbv;
        string _txtCompFactBlw;
        string _txtCompPromBlw;
        string _drpKRARating;
        string _drpLedBehav;
        string _DeptID;
        string _Flag;
        string _txtCompPromAbv20;
        //dnyaneshwar
        string _FromDate;
        string _ToDate;
        string _Action;
        string _KraFlag;
        string _ColumnType;
        string _EmpHODMID;
        string _NewDesgAid;
        string _OldDesg;
        string _Cycle;
        string _CycleAid;
        string _NewDesgDesc;
        string _RejectStatus;

        public string RejectStatus
        {
            get { return _RejectStatus; }
            set { _RejectStatus = value; }
        }
        public string NewDesgDesc
        {
            get { return _NewDesgDesc; }
            set { _NewDesgDesc = value; }
        }
        public string Cycle
        {
            get { return _Cycle; }
            set { _Cycle = value; }
        }

        public string CycleAid
        {
            get { return _CycleAid; }
            set { _CycleAid = value; }
        }

        public string OldDesg
        {
            get { return _OldDesg; }
            set { _OldDesg = value; }
        }
        public string NewDesgAid
        {
            get { return _NewDesgAid; }
            set { _NewDesgAid = value; }
        }
        public string KraFlag
        {
            get { return _KraFlag; }
            set { _KraFlag = value; }
        }

        public string ColumnType
        {
            get { return _ColumnType; }
            set { _ColumnType = value; }
        }

        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
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
        //dnyaneshwar
        public string txtCompPromAbv20
        {
            get { return _txtCompPromAbv20; }
            set { _txtCompPromAbv20 = value; }
        }
        public string Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }

        public string Comp_Aid
        {
            get { return _Comp_Aid; }
            set { _Comp_Aid = value; }
        }
        public string EmpCode
        {
            get { return _EmpCode; }
            set { _EmpCode = value; }
        }
        public string EmpAID
        {
            get { return _EmpAID; }
            set { _EmpAID = value; }
        }
        public string SECTION
        {
            get { return _SECTION; }
            set { _SECTION = value; }
        }
        public string Year
        {
            get { return _Year; }
            set { _Year = value; }
        }
        public string Quarter
        {
            get { return _Quarter; }
            set { _Quarter = value; }
        }
        public string XML
        {
            get { return _XML; }
            set { _XML = value; }
        }
        public string Area
        {
            get { return _Area; }
            set { _Area = value; }
        }

        public string Metric
        {
            get { return _Metric; }
            set { _Metric = value; }
        }

        public string EntryAid
        {
            get { return _EntryAid; }
            set { _EntryAid = value; }
        }
        public string Target
        {
            get { return _Target; }
            set { _Target = value; }
        }

        public string wt
        {
            get { return _wt; }
            set { _wt = value; }
        }

        public string Weightage
        {
            get { return _Weightage; }
            set { _Weightage = value; }
        }
        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }

        }
        public string ApprovalAid
        {
            get { return _ApprovalAid; }
            set { _ApprovalAid = value; }
        }
        public string EmpAid
        {
            get { return _EmpAid; }
            set { _EmpAid = value; }
        }
        public string EmpMID
        {
            get { return _EmpMID; }
            set { _EmpMID = value; }
        }
        public string EmpMakerMID
        {
            get { return _EmpMakerMID; }
            set { _EmpMakerMID = value; }
        }
        public string EmpRptMID
        {
            get { return _EmpRptMID; }
            set { _EmpRptMID = value; }
        }
        public string EmpHODMID
        {
            get { return _EmpHODMID; }
            set { _EmpHODMID = value; }
        }
        public string EmpHRMID
        {
            get { return _EmpHRMID; }
            set { _EmpHRMID = value; }
        }
        public string EmpCFOMID
        {
            get { return _EmpCFOMID; }
            set { _EmpCFOMID = value; }
        }
        public string tarIndiv
        {
            get { return _tarIndiv; }
            set { _tarIndiv = value; }
        }
        public string tarCompFact
        {
            get { return _tarCompFact; }
            set { _tarCompFact = value; }
        }
        public string tarCompProm
        {
            get { return _tarCompProm; }
            set { _tarCompProm = value; }
        }
        public string txtCompFactAbv
        {
            get { return _txtCompFactAbv; }
            set { _txtCompFactAbv = value; }
        }
        public string txtCompPromAbv
        {
            get { return _txtCompPromAbv; }
            set { _txtCompPromAbv = value; }
        }
       
        public string txtCompFactBlw
        {
            get { return _txtCompFactBlw; }
            set { _txtCompFactBlw = value; }
        }
        public string txtCompPromBlw
        {
            get { return _txtCompPromBlw; }
            set { _txtCompPromBlw = value; }
        }
        public string drpKRARating
        {
            get { return _drpKRARating; }
            set { _drpKRARating = value; }
        }
        public string drpLedBehav
        {
            get { return _drpLedBehav; }
            set { _drpLedBehav = value; }
        }
        public string DeptID
        {
            get { return _DeptID; }
            set { _DeptID = value; }
        }


    }
}