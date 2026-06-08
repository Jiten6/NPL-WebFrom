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
    public class PMSHistoric
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;
        private NewPortal2023.ESS.Common objCommon;


        public DataSet GetPMSHistoric()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            //objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            //objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            //objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            //objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            //objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            //objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPMSHISTORICDATA");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet GetPMSLastPromotion()
        {
            objDBUtility = new DBUtility();
            DataSet ds = null;

            //objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue == null ? "" : empValue.Trim());
            //objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue == null ? "" : compValue.Trim());
            //objDBUtility.AddParameters("@YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Year == null ? "" : Year.Trim());
            //objDBUtility.AddParameters("@Quarter", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Quarter == null ? "" : Quarter.Trim());
            //objDBUtility.AddParameters("@COLNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Column == null ? "" : Column.Trim());
            //objDBUtility.AddParameters("@SECTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SECTION == null ? "" : SECTION.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPMSLASTPROMOTIONDATA");

            ds = objDBUtility.Execute_StoreProc_DataSet("SP_APPRAISAL");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }
    }
}