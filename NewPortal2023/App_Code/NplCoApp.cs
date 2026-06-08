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
/// Summary description for LeaveApplication
/// </summary>
namespace NewPortal2023.ESS
{

    public class NplCoApp
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;
        private NewPortal2023.ESS.Common objcommon;

        public string UpdateLeave(string compValue, string empValue, string fromdate, string reason, string status, string NewYear, string frDate, string toDate)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;
            string strurl = string.Empty;
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                
                objDBUtility.AddParameters("@FROM_DT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromdate);
                
                objDBUtility.AddParameters("@REASON_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, reason);
                
                
                objDBUtility.AddParameters("@STATUS ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status);
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);

                objDBUtility.AddParameters("@FRDT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, frDate);
                objDBUtility.AddParameters("@TODT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDate);

                

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTCO");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL_COAPP");

                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            strresult = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["status"].ToString().Trim());
                            
                        }
                    }
                }

                objDBUtility.ClearTransactionalParams();


            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return strresult;
        }

        public DataSet getDays(string compValue, string empValue, string fromdate, string todate)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());

            objDBUtility.AddParameters("@FROM_DT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromdate);
            objDBUtility.AddParameters("@TO_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, todate);

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDAYS");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL_COAPP");
            return dsInv;
        }

        public bool UpdateAttendanceStatus(string compValue, string empValue,  string remaks, string status, string cid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());

            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, remaks);

            objDBUtility.AddParameters("@ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cid);
            //objDBUtility.AddParameters("@ACTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, action);

            objDBUtility.AddParameters("@STATUS ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status);
            


            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEAPPROVERACTION");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL_COAPP");


            

            objDBUtility.ClearTransactionalParams();

            return true;
        }

        public DataSet GetCOList(string compValue, string empValue, string NewYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPLISTCOAPP");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL_COAPP");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetApproverCOList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVERLISTCOAPP");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("[SP_NPL_COAPP]");

            objDBUtility.ClearTransactionalParams();
            return dsInv;
        }

        public DataSet GetCOListAdmin(string compValue, string empValue, string NewYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPLISTCOAPPADMIN");
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL_COAPP");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetCOApplicationDetailsByCID(string cid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet(); 

            try
            {
                objDBUtility.AddParameters("@CIDID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cid.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPLISTCOAPPBYCID");
                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL_COAPP");
                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
               
            }

            return dsInv;
        }

        public string ExistingUpdateLeave(string compValue, string empValue, string fromdate, string reason, string status, string NewYear, string frDate, string toDate,string cid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;
            string strurl = string.Empty;
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());

                objDBUtility.AddParameters("@FROM_DT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, fromdate);

                objDBUtility.AddParameters("@REASON_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, reason);


                objDBUtility.AddParameters("@STATUS ", DBUtilDBType.Varchar, DBUtilDirection.In, 50, status);
                objDBUtility.AddParameters("@NEWYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, NewYear);

                objDBUtility.AddParameters("@FRDT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, frDate);
                objDBUtility.AddParameters("@TODT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, toDate);
                objDBUtility.AddParameters("@CIDID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cid);



                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECO");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("SP_NPL_COAPP");

                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            strresult = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["status"].ToString().Trim());

                        }
                    }
                }

                objDBUtility.ClearTransactionalParams();


            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return strresult;
        }

    }
}