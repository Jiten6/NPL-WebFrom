using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
/// <summary>
/// Summary description for Common
/// </summary>
namespace NewPortal2023.ESS
{

    public class Common
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        public Common()
        {

        }

        public void Display(string key, string script)
        {
            RegisterScript((Page)HttpContext.Current.Handler, key, script);
        }




        public static void RegisterScript(Control pageIns, string key, string script)
        {
            ScriptManager ScriptManagerIns = ScriptManager.GetCurrent((Page)pageIns);
            if (ScriptManagerIns == null)
            {
                pageIns.Page.ClientScript.RegisterStartupScript(pageIns.GetType(), key, script, true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(pageIns, pageIns.GetType(), key, script, true);
            }
        }



        //public void Display(string key, string script, bool reloadPage = false)
        //{
        //    RegisterScript((Page)HttpContext.Current.Handler, key, script, reloadPage);
        //}

        //public static void RegisterScript(Control pageIns, string key, string script, bool reloadPage = false)
        //{
        //    ScriptManager ScriptManagerIns = ScriptManager.GetCurrent((Page)pageIns);

        //    // Check if the page should be reloaded
        //    if (reloadPage)
        //    {
        //        // Add a script to reload the page after showing the error message
        //        script += "location.reload();";
        //    }

        //    if (ScriptManagerIns == null)
        //    {
        //        pageIns.Page.ClientScript.RegisterStartupScript(pageIns.GetType(), key, script, true);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(pageIns, pageIns.GetType(), key, script, true);
        //    }
        //}



        //private void RegisterScript(Page page, string key, string script)
        //{
        //    if (page != null && !page.ClientScript.IsClientScriptBlockRegistered(key))
        //    {
        //        string styledScript = $@"
        //    <style>
        //        .red-popup {{
        //            color: red; /* Set the text color to red */
        //            /* Add additional styling as needed */
        //        }}
        //    </style>
        //    <script type='text/javascript'>
        //         Your original script here
        //        {script}
        //    </script>";

        //        page.ClientScript.RegisterClientScriptBlock(page.GetType(), key, styledScript, true);
        //    }
        //}

        public string SendSMS(string gateway)
        {
            string SendTo = string.Empty;
            string title = string.Empty;
            string val = string.Empty;
            string response = string.Empty;
            string statusret = "";
            ArrayList strresp = new ArrayList();


            try
            {
                strresp.AddRange(response.Split(new char[] { ';' }));

                //default.

                if (gateway.ToString().Trim() != "")
                {
                    title = HttpUtility.UrlEncode(title);
                    //string strResult = gateway.ToString().Replace("<<mobileno>>", SendTo).Replace("<<message>>", title);

                    //Uri uri = new Uri(HttpUtility.UrlPathEncode(strResult),true);
                    //title = Uri.EscapeDataString(title);

                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(gateway);
                    httpRequest.Method = "GET";
                    WebResponse httpResponse = httpRequest.GetResponse();

                    Stream str = httpResponse.GetResponseStream();

                    StreamReader strr = new StreamReader(str, System.Text.Encoding.Default);

                    string GetWebPage = strr.ReadToEnd();

                    statusret = "1";

                    for (int intc = 0; intc <= strresp.Count - 1; intc++)
                    {
                        if (GetWebPage.Contains(strresp[intc].ToString()) == true)
                        {
                            statusret = GetWebPage.ToString();
                            break;
                        }
                    }
                    //statusret = (GetWebPage.Contains("invalid login") ? "0" : "1");

                }

                return statusret;
            }
            catch (Exception ex)
            {
                statusret = "0";
                return statusret;
            }


        }

        public string GetFinalcialYear()
        {
            string CurrFin = string.Empty;
            string PreFin;

            if (DateTime.Now.Month >= 1 && DateTime.Now.Month <= 3)
            {
                CurrFin = Convert.ToString(DateTime.Now.Year - 1) + Convert.ToString(DateTime.Now.Year);
                PreFin = Convert.ToString(DateTime.Now.Year - 2) + Convert.ToString(DateTime.Now.Year - 1);
            }
            else
            {
                CurrFin = Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Year + 1);
                PreFin = Convert.ToString(DateTime.Now.Year - 1) + Convert.ToString(DateTime.Now.Year);
            }
            return CurrFin + '.' + PreFin;
        }

        public bool ValidateCalendarYear(DateTime dt)
        {
            int year = dt.Year;
            // Get the current calendar year
            int currentYear = DateTime.Now.Year;

            // Check if the date's year is the same as the current calendar year
            return year == currentYear;
        }

        public bool ValidateFinalcialYear(DateTime dt)
        {
            string year;

            if (dt.Month >= 1 && dt.Month <= 3)
            {
                year = Convert.ToString(dt.Year - 1) + Convert.ToString(dt.Year);
            }
            else
            {
                year = Convert.ToString(dt.Year) + Convert.ToString(dt.Year + 1);
            }
            string finYear = GetFinalcialYear();
            string[] years = finYear.Split('.');
            if (Convert.ToString(years[0]) != year)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void Validate_Login(string compValue, string userValue, LoginParams objUser)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            try
            {

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "REDIRECT");
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userValue.ToString().Trim());

                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                objUser.EmpId = "";

                if (dsLogin.Tables.Count > 0)
                {

                    if (dsLogin.Tables[0].Rows.Count > 0)
                    {
                        objUser.EmpId = dsLogin.Tables[0].Rows[0]["EMP_AID"].ToString().Trim();
                        objUser.EmpCode = dsLogin.Tables[0].Rows[0]["EMP_MID"].ToString().Trim();
                        objUser.EmpName = dsLogin.Tables[0].Rows[0]["EMP_NAME"].ToString().Trim();
                        objUser.EmpSex = dsLogin.Tables[0].Rows[0]["SEX"].ToString().Trim();
                        objUser.Designation = dsLogin.Tables[0].Rows[0]["GRADE_DESC"].ToString().Trim();
                        objUser.Location = dsLogin.Tables[0].Rows[0]["LOC_DESC"].ToString().Trim();
                        objUser.PAN = dsLogin.Tables[0].Rows[0]["PAN_NUMBER"].ToString().Trim();
                        objUser.JoinDate = dsLogin.Tables[0].Rows[0]["JOIN_DATE"].ToString().Trim();
                        objUser.CompId = dsLogin.Tables[0].Rows[0]["COMP_AID"].ToString().Trim();
                        objUser.SMSURL = dsLogin.Tables[0].Rows[0]["SMSURL"].ToString().Trim();
                        objUser.Grade = dsLogin.Tables[0].Rows[0]["GRADE_DESC"].ToString().Trim();
                        objUser.EmailId = dsLogin.Tables[0].Rows[0]["EMAIL_ID"].ToString().Trim();
                        objUser.LastLogin = dsLogin.Tables[0].Rows[0]["LOGIN_DATETIME"].ToString().Trim();
                        objUser.IsAdmin = dsLogin.Tables[0].Rows[0]["ISADMIN"].ToString().Trim();
                        objUser.CompsName = dsLogin.Tables[0].Rows[0]["COMP_SNAME"].ToString().Trim();
                    }

                }


                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
        }



        public void Validate_Login_Saml(string compValue, string userValue, LoginParams objUser)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            try
            {

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "REDIRECTSAML");
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userValue.ToString().Trim());

                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                objUser.EmpId = "";

                if (dsLogin.Tables.Count > 0)
                {

                    if (dsLogin.Tables[0].Rows.Count > 0)
                    {
                        objUser.EmpId = dsLogin.Tables[0].Rows[0]["EMP_AID"].ToString().Trim();
                        objUser.EmpCode = dsLogin.Tables[0].Rows[0]["EMP_MID"].ToString().Trim();
                        objUser.EmpName = dsLogin.Tables[0].Rows[0]["EMP_NAME"].ToString().Trim();
                        objUser.EmpSex = dsLogin.Tables[0].Rows[0]["SEX"].ToString().Trim();
                        objUser.Designation = dsLogin.Tables[0].Rows[0]["GRADE_DESC"].ToString().Trim();
                        objUser.Location = dsLogin.Tables[0].Rows[0]["LOC_DESC"].ToString().Trim();
                        objUser.PAN = dsLogin.Tables[0].Rows[0]["PAN_NUMBER"].ToString().Trim();
                        objUser.JoinDate = dsLogin.Tables[0].Rows[0]["JOIN_DATE"].ToString().Trim();
                        objUser.CompId = dsLogin.Tables[0].Rows[0]["COMP_AID"].ToString().Trim();
                        objUser.SMSURL = dsLogin.Tables[0].Rows[0]["SMSURL"].ToString().Trim();
                        objUser.Grade = dsLogin.Tables[0].Rows[0]["GRADE_DESC"].ToString().Trim();
                        objUser.EmailId = dsLogin.Tables[0].Rows[0]["EMAIL_ID"].ToString().Trim();
                        objUser.LastLogin = dsLogin.Tables[0].Rows[0]["LOGIN_DATETIME"].ToString().Trim();
                        objUser.IsAdmin = dsLogin.Tables[0].Rows[0]["ISADMIN"].ToString().Trim();
                        objUser.CompsName = dsLogin.Tables[0].Rows[0]["COMP_SNAME"].ToString().Trim();
                    }

                }


                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
        }

        public void Validate_Login(string compValue, string userValue, string passValue, LoginParams objUser)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;
            try
            {
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "LOGIN");
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userValue.ToString().Trim());
                objDBUtility.AddParameters("@PASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, passValue.ToString().Trim());

                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                objUser.EmpId = "";
                if (dsLogin.Tables.Count > 0)
                {
                    if (dsLogin.Tables[0].Rows.Count > 0)
                    {
                        objUser.EmpId = dsLogin.Tables[0].Rows[0]["EMP_AID"].ToString().Trim();
                        objUser.EmpCode = dsLogin.Tables[0].Rows[0]["EMP_MID"].ToString().Trim();
                        objUser.EmpName = dsLogin.Tables[0].Rows[0]["EMP_NAME"].ToString().Trim();
                        objUser.EmpSex = dsLogin.Tables[0].Rows[0]["SEX"].ToString().Trim();
                        objUser.Designation = dsLogin.Tables[0].Rows[0]["GRADE_DESC"].ToString().Trim();
                        objUser.Location = dsLogin.Tables[0].Rows[0]["LOC_DESC"].ToString().Trim();
                        objUser.PAN = dsLogin.Tables[0].Rows[0]["PAN_NUMBER"].ToString().Trim();
                        objUser.JoinDate = dsLogin.Tables[0].Rows[0]["JOIN_DATE"].ToString().Trim();
                        objUser.CompId = dsLogin.Tables[0].Rows[0]["COMP_AID"].ToString().Trim();
                        objUser.SMSURL = dsLogin.Tables[0].Rows[0]["SMSURL"].ToString().Trim();
                        objUser.Grade = dsLogin.Tables[0].Rows[0]["GRADE_DESC"].ToString().Trim();
                        objUser.EmailId = dsLogin.Tables[0].Rows[0]["EMAIL_ID"].ToString().Trim();
                        objUser.LastLogin = dsLogin.Tables[0].Rows[0]["LOGIN_DATETIME"].ToString().Trim();
                        objUser.IsAdmin = dsLogin.Tables[0].Rows[0]["ISADMIN"].ToString().Trim();
                        objUser.CarLeaseLimit = dsLogin.Tables[0].Rows[0]["CAR_LEASE_LIMIT"].ToString().Trim();
                        objUser.Role = dsLogin.Tables[0].Rows[0]["APPROVER_ROLE"].ToString().Trim();
                        objUser.CompsName = dsLogin.Tables[0].Rows[0]["COMP_SNAME"].ToString().Trim();
                        objUser.EmpType = dsLogin.Tables[0].Rows[0]["EMP_TYPE"].ToString().Trim();
                    }
                }

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
        }
        public void Validate_Login_Alumni(string compValue, string userValue, string passValue, LoginParams objUser)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userValue.ToString().Trim());
            objDBUtility.AddParameters("@PASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, passValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "LOGINALUMNI");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

            objUser.EmpId = "";

            if (dsLogin.Tables.Count > 0)
            {
                if (dsLogin.Tables[0].Rows.Count > 0)
                {
                    objUser.EmpId = dsLogin.Tables[0].Rows[0]["EMP_AID"].ToString().Trim();
                    objUser.CompId = dsLogin.Tables[0].Rows[0]["COMP_AID"].ToString().Trim();
                    objUser.EmpCode = dsLogin.Tables[0].Rows[0]["EMP_MID"].ToString().Trim();
                    objUser.EmpName = dsLogin.Tables[0].Rows[0]["EMP_NAME"].ToString().Trim();
                    objUser.EmailId = dsLogin.Tables[0].Rows[0]["EMAIL_ID"].ToString().Trim();
                    objUser.PAN = dsLogin.Tables[0].Rows[0]["PAN_NUMBER"].ToString().Trim();
                    objUser.JoinDate = dsLogin.Tables[0].Rows[0]["JOIN_DATE"].ToString().Trim();
                    objUser.LastLogin = dsLogin.Tables[0].Rows[0]["LOGIN_DATETIME"].ToString().Trim();
                    objUser.IsAdmin = dsLogin.Tables[0].Rows[0]["ISADMIN"].ToString().Trim();
                }
            }

            objDBUtility.ClearTransactionalParams();
        }
        public void UpdateLoginDateTime(string compValue, string userValue, string dateValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            try
            {

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "LOGINSUCCESS");
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userValue.ToString().Trim());
                objDBUtility.AddParameters("@LOGIN_DATETIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, dateValue.ToString().Trim());

                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
        }

        public void UpdateLeave(string compValue, string userValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            try
            {

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "LOGINLEAVEUPDATE");
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userValue.ToString().Trim());

                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVE");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
        }
        public void UpdateLoginDateTime(string compValue, string userValue, string dateValue, string queryString)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            try
            {

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "LOGINSUCCESSQUERY");
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userValue.ToString().Trim());
                objDBUtility.AddParameters("@LOGIN_DATETIME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, dateValue.ToString().Trim());
                objDBUtility.AddParameters("@QUERYSTRING", DBUtilDBType.Varchar, DBUtilDirection.In, 50, queryString.ToString().Trim());

                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
        }

        public void Verify_User(string compValue, string userValue, string panValue, string joinDate, out bool isPAN, out bool isQuest)
        {
            objDBUtility = new DBUtility();
            isPAN = false;
            isQuest = false;
            DataSet dsLogin;

            try
            {

                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userValue.ToString().Trim());
                objDBUtility.AddParameters("@PAN_NO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, panValue.ToString().Trim());
                objDBUtility.AddParameters("@JOIN_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, joinDate.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "VERIFYPAN");

                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                if (dsLogin.Tables.Count > 0)
                {
                    if (dsLogin.Tables[0].Rows.Count > 0)
                    {
                        isPAN = true;
                    }
                    else
                    {
                        isPAN = false;
                    }

                    if (dsLogin.Tables[1].Rows.Count > 0)
                    {
                        isQuest = true;
                    }
                    else
                    {
                        isQuest = false;
                    }
                }
                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
        }
        public DataSet FillProfile(string compValue, string empValue)
        {


            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();
            try
            {


                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FILLPROFILE");
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");


            }

            catch (Exception ex)
            {

            }

            objDBUtility.ClearTransactionalParams();
            return ds;
        }
        public void Verify_User_V2(string compValue, string userValue, string panValue, string joinDate, out bool isVerified)
        {
            objDBUtility = new DBUtility();
            isVerified = false;
            DataSet dsLogin = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userValue.ToString().Trim());
                objDBUtility.AddParameters("@PAN_NO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, panValue.ToString().Trim());
                objDBUtility.AddParameters("@JOIN_DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, joinDate.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "VERIFYPANV2");

                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                if (dsLogin.Tables.Count > 0)
                {
                    if (Convert.ToInt32(dsLogin.Tables[0].Rows[0][0]) > 0)
                    {
                        isVerified = true;
                    }
                    else
                    {
                        isVerified = false;
                    }
                }
                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
        }

        public Boolean Create_Question(string compValue, string empValue, string question, string answer)
        {
            objDBUtility = new DBUtility();
            DataSet ds = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@QUESTION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, question.ToString().Trim());
                objDBUtility.AddParameters("@ANSWER", DBUtilDBType.Varchar, DBUtilDirection.In, 50, answer.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CRQUESTION");
                ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                objDBUtility.ClearTransactionalParams();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Boolean Reset_Password(string compValue, string empValue, string passValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@PASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, passValue.ToString());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "RESETPASS");
                ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                objDBUtility.ClearTransactionalParams();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DataSet Fetch_Question(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "FETCHQUESTION");
                ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

        public DataSet GetPasswordList(string compValue)
        {
            objDBUtility = new DBUtility();
            DataSet ds = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPASSWORDLIST");
            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public void Get_CompanyHeader(Label lblCompName, string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "COMPANY");
                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                if (dsLogin.Tables.Count > 0)
                {

                    if (dsLogin.Tables[0].Rows.Count > 0)
                    {
                        lblCompName.Text = dsLogin.Tables[0].Rows[0]["COMP_NAME"].ToString().Trim();
                    }

                }


                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
        }

        public void GetAlumani_Menu(ArrayList ArrListName, ArrayList ArrListValue, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "MENUALUMNI");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

            if (dsLogin.Tables.Count > 0)
            {
                if (dsLogin.Tables[0].Rows.Count > 0)
                {
                    for (int cnt = 0; cnt <= dsLogin.Tables[0].Rows.Count - 1; cnt++)
                    {
                        ArrListName.Add(dsLogin.Tables[0].Rows[cnt]["MENU_NAME"].ToString().Trim());
                        ArrListValue.Add(dsLogin.Tables[0].Rows[cnt]["MENU_URL"].ToString().Trim());
                    }
                }
            }

            objDBUtility.ClearTransactionalParams();
        }

        public DataTable Get_Menu(string empValue, string compValue, ref string MenuScript, string menuName)
        //public void Get_Menu(ArrayList ArrListName, ArrayList ArrListValue, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet ds;
            DataTable dt = new DataTable();
            StringBuilder strMenu = new StringBuilder();


            //objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            // objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "MENUEXPENSE");

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "MENUEXPENSE");
            ds = objDBUtility.Execute_StoreProc_DataSet("[COMMON_SP_GET_EMPLOYEE]");

            strMenu.Append("<aside>");
            strMenu.Append("    <div id=\"sidebar\" class=\"nav-collapse\">");
            strMenu.Append("        <div class=\"leftside-navigation\">");
            strMenu.Append("            <ul class=\"sidebar-menu\" id=\"nav-accordion\">");
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int cnt = 0; cnt <= ds.Tables[1].Rows.Count - 1; cnt++)
                    {
                        if (ds.Tables[1].Rows[cnt]["IS_SELF"].ToString() == "1")
                        {
                            strMenu.Append("                <li>");
                            if (menuName == ds.Tables[1].Rows[cnt]["MAIN_MENU_URL"].ToString().ToLower())
                            {
                                strMenu.Append("                    <a class=\"active\" href=\"" + ds.Tables[1].Rows[cnt]["MAIN_MENU_URL"].ToString() + "\">");
                            }
                            else
                            {
                                strMenu.Append("                    <a href=\"" + ds.Tables[1].Rows[cnt]["MAIN_MENU_URL"].ToString() + "\">");
                            }
                            strMenu.Append("                        <i class=\"" + ds.Tables[1].Rows[cnt]["CSS_CLASS"].ToString() + "\"></i>");
                            strMenu.Append("                        <span>" + ds.Tables[1].Rows[cnt]["MAIN_MENU"].ToString() + "</span>");
                            strMenu.Append("                    </a>");
                            strMenu.Append("                </li>");
                        }
                        else
                        {
                            strMenu.Append("                <li class=\"sub-menu\">");
                            strMenu.Append("                    <a class=\"\" href=\"javascript:;\">");
                            strMenu.Append("                        <i class=\"" + ds.Tables[1].Rows[cnt]["CSS_CLASS"].ToString() + "\"></i>");
                            strMenu.Append("                        <span>" + ds.Tables[1].Rows[cnt]["MAIN_MENU"].ToString() + "</span>");
                            strMenu.Append("                    </a>");
                            strMenu.Append("                    <ul class=\"sub\">");

                            DataRow[] dtr = ds.Tables[0].Select("MAIN_MENU='" + ds.Tables[1].Rows[cnt]["MAIN_MENU"].ToString() + "'");
                            foreach (DataRow row in dtr)
                            {
                                //if (row["ALLOW_DISPLAY"].ToString() == "1")
                                //{
                                if (menuName == row["MENU_URL"].ToString().ToLower())
                                {
                                    strMenu.Append("<li class=\"active\"><a href='" + row["MENU_URL"].ToString() + "'>" + row["MENU_NAME"].ToString() + "</a></li>");
                                    strMenu.Replace("class=\"\"", "class=\"active\"");
                                }
                                else
                                {
                                    strMenu.Append("<li><a href='" + row["MENU_URL"].ToString() + "'>" + row["MENU_NAME"].ToString() + "</a></li>");
                                }
                                // }
                            }

                            strMenu.Replace("class=\"\"", "");
                            strMenu.Append("                    </ul>");
                            strMenu.Append("                </li>");
                        }
                    }
                }
            }
            strMenu.Append("            </ul>");
            strMenu.Append("        </div>");
            strMenu.Append("    </div>");
            strMenu.Append("</aside>");

            MenuScript = strMenu.ToString();
            objDBUtility.ClearTransactionalParams();



            return ds.Tables[0];
        }


        //public void Get_Menu(ArrayList ArrListName, ArrayList ArrListValue, string compValue, string empValue)
        //{
        //    objDBUtility = new DBUtility();

        //    DataSet dsLogin;

        //    try
        //    {
        //        objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
        //        objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
        //        objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "MENU");
        //        dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

        //        if (dsLogin.Tables.Count > 0)
        //        {

        //            if (dsLogin.Tables[0].Rows.Count > 0)
        //            {
        //                for (int cnt = 0; cnt <= dsLogin.Tables[0].Rows.Count - 1; cnt++)
        //                {
        //                    ArrListName.Add(dsLogin.Tables[0].Rows[cnt]["MENU_NAME"].ToString().Trim());
        //                    ArrListValue.Add(dsLogin.Tables[0].Rows[cnt]["MENU_URL"].ToString().Trim());
        //                }
        //            }

        //        }


        //        objDBUtility.ClearTransactionalParams();
        //    }
        //    catch (Exception ex)
        //    {
        //        //CreateErrorLog("", ex.Message, "Common_Validate_Login");
        //    }
        //}

        //Get Page Name
        public string GetCurrentPageName(string sPath)
        {
            System.IO.FileInfo oInfo = new System.IO.FileInfo(sPath);
            string sRet = oInfo.Name;
            return sRet;
        }

        public string Validate_Password(string oldPass, string newPass, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;
            string strResult = string.Empty;

            try
            {

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CHANGEPWD");
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@PASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, newPass.ToString().Trim());
                objDBUtility.AddParameters("@OLDPASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, oldPass.ToString().Trim());

                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                if (dsLogin.Tables.Count > 0)
                {

                    if (dsLogin.Tables[0].Rows.Count > 0)
                    {
                        strResult = dsLogin.Tables[0].Rows[0]["Result"].ToString().Trim();
                    }

                }


                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                strResult = ex.Message.ToString();
            }

            return strResult;
        }

        public string Validate_PasswordAlumni(string oldPass, string newPass, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;
            string strResult = string.Empty;

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@PASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, newPass.ToString().Trim());
                objDBUtility.AddParameters("@OLDPASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, oldPass.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CHANGEPWDALUMNI");

                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                if (dsLogin.Tables.Count > 0)
                {
                    if (dsLogin.Tables[0].Rows.Count > 0)
                    {
                        strResult = dsLogin.Tables[0].Rows[0]["Result"].ToString().Trim();
                    }
                }

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                strResult = ex.Message.ToString();
            }

            return strResult;
        }

        public string Validate_ControlInfo(string strValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;
            string strResult = string.Empty;

            try
            {

                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CONTROL");
                objDBUtility.AddParameters("@VALUE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INV");

                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_CONTROLINFO");

                if (dsLogin.Tables.Count > 0)
                {

                    if (dsLogin.Tables[0].Rows.Count > 0)
                    {
                        strResult = dsLogin.Tables[0].Rows[0]["Result"].ToString().Trim();
                    }

                }


                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                strResult = ex.Message.ToString();
            }

            return strResult;
        }

        public string EncodeJsString(string s)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("\"");
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\'':
                        sb.Append("\\'");
                        break;
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        int i = (int)c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            //sb.Append("\"");

            return sb.ToString();
        }

        public DataSet GetDocs(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = new DataSet();

            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETDOCS");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public string UpdateDocs(string CompAid, string Empid, string DOCxml)
        {
            objDBUtility = new DBUtility();

            try
            {

                objDBUtility.AddParameters("@DOCXML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DOCxml);
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEDOCS");
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompAid.ToString().Trim());

                objDBUtility.Execute_StoreProc("COMMON_SP_GET_EMPLOYEE");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Violation of UNIQUE KEY"))
                {
                    return "Duplicate description.";
                }
                else
                {
                    throw new Exception(ex.Message);
                }

                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return "";
        }

        public string XOREncryptDecrypt(string szPlainText, int szEncryptionKey)
        {
            StringBuilder szInputStringBuild = new StringBuilder(szPlainText);
            StringBuilder szOutStringBuild = new StringBuilder(szPlainText.Length);
            char Textch;
            for (int iCount = 0; iCount < szPlainText.Length; iCount++)
            {
                Textch = szInputStringBuild[iCount];
                Textch = (char)(Textch ^ szEncryptionKey);
                szOutStringBuild.Append(Textch);
            }
            return szOutStringBuild.ToString();
        }

        public string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public string DecodeUrlString(string url)
        {
            string newUrl;
            while ((newUrl = Uri.UnescapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }

        public string EncodeUrlString(string url)
        {
            string newUrl;
            while ((newUrl = Uri.EscapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }

        public string EncryptOrDecrypt(string text)
        {
            string key = "666666";

            var result = new System.Text.StringBuilder();

            for (int c = 0; c < text.Length; c++)
            {
                // take next character from string
                char character = text[c];

                // cast to a uint
                uint charCode = (uint)character;

                // figure out which character to take from the key
                int keyPosition = c % key.Length; // use modulo to "wrap round"

                // take the key character
                char keyChar = key[keyPosition];

                // cast it to a uint also
                uint keyCode = (uint)keyChar;

                // perform XOR on the two character codes
                uint combinedCode = charCode ^ keyCode;

                // cast back to a char
                char combinedChar = (char)combinedCode;

                // add to the result
                result.Append(combinedChar);
            }

            return result.ToString();
        }

        public int FinancialYear(DateTime dateTime)
        {
            return (dateTime.Month >= 4 ? dateTime.Year + 1 : dateTime.Year);
        }

        public string CreatePassword(int length)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

        public string ReplaceSpecialCharacters(string inputString)
        {
            inputString = inputString.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&#39;").Replace("\"", "&quot;");
            return inputString;
        }

        public void Get_Menu(ArrayList ArrListName, ArrayList ArrListValue, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "MENU");
                dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                if (dsLogin.Tables.Count > 0)
                {

                    if (dsLogin.Tables[0].Rows.Count > 0)
                    {
                        for (int cnt = 0; cnt <= dsLogin.Tables[0].Rows.Count - 1; cnt++)
                        {
                            ArrListName.Add(dsLogin.Tables[0].Rows[cnt]["MENU_NAME"].ToString().Trim());
                            ArrListValue.Add(dsLogin.Tables[0].Rows[cnt]["MENU_URL"].ToString().Trim());
                        }
                    }

                }

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
        }

        public void SetMessageColor(HtmlGenericControl control, string messageType)
        {
            if (messageType == "success")
            {
                control.Attributes["class"] = "alert alert-block alert-success fade in";
            }
            else if (messageType == "danger")
            {
                control.Attributes["class"] = "alert alert-block alert-danger fade in";
            }
        }

        internal DataSet GetEmpType(string CompId, string Empid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPTYPE");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        internal DataSet GetPendingData(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPENDINGDATA");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        internal DataSet GetApprovedData(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVEDDATA");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        internal DataSet GetPendingApprovalData(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPENDINGAPPROVALDATA");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }
        internal DataSet GetPendingDataall(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPENDINGDATAALL");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;

        }
        internal DataSet GetApprovedDataall(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVEDDATAALL");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }
        internal DataSet GetPendingDataattend(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPENDINGDATAATTEND");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }
        internal DataSet GetApprovedDataATTEND(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVEDDATAATTEND");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }


        internal DataSet GetPendingDatahandset(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPENDINGHANDSET");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        internal DataSet GetPendingtel(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPENDINGTEL");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        internal DataSet GetPendingLOCAL(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPENDINGLOCAL");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        internal DataSet GetPendingDOM(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPENDINGDOM");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        internal DataSet GetApprovedDOM(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVEDDOM");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        internal DataSet GetApprovedmob(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVEDMOB");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }


        internal DataSet GetApprovedtele(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVEDTELE");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        internal DataSet GetApprovedloc(string CompId, string Empid, string Empcode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empcode.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVEDLOC");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_LEAVEAPPROVAL");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetEmial(string empValue, string compValue)
        {


            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMAIL");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

            objDBUtility.ClearTransactionalParams();

            return ds;


        }

        public DataSet PasswordSaved(string empValue, string compValue, string password)
        {

            objDBUtility = new DBUtility();

            DataSet dsPassword = new DataSet();

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "SAVEPASSWORD");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@PASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, password.ToString().Trim());
            dsPassword = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

            objDBUtility.ClearTransactionalParams();

            return dsPassword;
        }

        public DataSet ProfIdOfEmp(string compaid, string empaid)
        {
            objDBUtility = new DBUtility();

            DataSet ds;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compaid == null ? "" : compaid.Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empaid == null ? "" : empaid.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "PROFIDOFEMP");
            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");
            objDBUtility.ClearTransactionalParams();
            return ds;
        }

        internal DataSet CheckMenuAccess(string compaid, string empaid, string profid)
        {
            objDBUtility = new DBUtility();
            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compaid == null ? "" : compaid.Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empaid == null ? "" : empaid.Trim());
                objDBUtility.AddParameters("@PROF_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, profid == null ? "" : profid.Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CHECKMENUACCESS");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {


            }

            return dsInv;
        }


    }

    public class LoginParams
    {
        string Emp_id;
        string Emp_name;
        string Emp_Sex;
        string Desgn;
        string JoinDt;
        string Panno;
        string SMS;
        string Loc;
        string _Grade;
        string Emp_Code;
        string Comp_id;
        string _EmailId;
        string _LastLogin;
        string _IsAdmin;
        string _CarLeaseLimit;
        string _Role;
        string _CompsName;
        string _EmpType;
        public string CompId
        {
            get { return Comp_id; }
            set { Comp_id = value; }
        }

        public string EmpId
        {
            get { return Emp_id; }
            set { Emp_id = value; }
        }

        public string EmpCode
        {
            get { return Emp_Code; }
            set { Emp_Code = value; }
        }

        public string EmpName
        {
            get { return Emp_name; }
            set { Emp_name = value; }
        }

        public string EmpSex
        {
            get { return Emp_Sex; }
            set { Emp_Sex = value; }
        }

        public string Designation
        {
            get { return Desgn; }
            set { Desgn = value; }
        }

        public string Location
        {
            get { return Loc; }
            set { Loc = value; }
        }
        public string PAN
        {
            get { return Panno; }
            set { Panno = value; }
        }
        public string SMSURL
        {
            get { return SMS; }
            set { SMS = value; }
        }
        public string JoinDate
        {
            get { return JoinDt; }
            set { JoinDt = value; }
        }
        public string Grade
        {
            get { return _Grade; }
            set { _Grade = value; }
        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        public string LastLogin
        {
            get { return _LastLogin; }
            set { _LastLogin = value; }
        }
        public string IsAdmin
        {
            get { return _IsAdmin; }
            set { _IsAdmin = value; }
        }
        public string CarLeaseLimit
        {
            get { return _CarLeaseLimit; }
            set { _CarLeaseLimit = value; }
        }
        public string Role
        {
            get { return _Role; }
            set { _Role = value; }
        }

        public string CompsName
        {
            get { return _CompsName; }
            set { _CompsName = value; }
        }

        public string EmpType
        {
            get { return _EmpType; }
            set { _EmpType = value; }
        }




    }

    public class EmployeeInfo
    {
        public string org { get; set; }
        public string employee_no { get; set; }
        public string timestamp { get; set; }
    }
}