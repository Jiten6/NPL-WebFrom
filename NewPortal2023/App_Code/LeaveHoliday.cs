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
/// Summary description for LeaveHoliday
/// </summary>
namespace NewPortal2023.ESS
{

    public class LeaveHoliday
    {

        private NewPortal2023.ESS.DBUtility objDBUtility;

        public LeaveHoliday()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataSet GetLeaveDetails(string compValue, string empValue, string NewYear, string OldYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLeave");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);
                objDBUtility.AddParameters("@OLDYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OldYear);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEHOLIDAY");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

    }
}   