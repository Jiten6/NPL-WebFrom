using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class NewPortalDefaultPage : System.Web.UI.Page
    {
        DataSet ds = new DataSet();
        NewPortal2023.ESS.Common objCommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["NPLID"] != null)
                {
                    string data = Request.QueryString["NPLID"];

                    //   string EMPid = objCommon.Decrypt(data);
                    string[] splitStrings = data.Split('_');


                    string Emp_id = splitStrings[0];
                    string Comp_id = splitStrings[1];
                    string empid = Emp_id;
                    TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
                    DateTime indianTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);

                    string token = indianTime.ToString("yyyy-MM-dd HH:mm:ss");

                    objCommon.Validate_Login(Comp_id, empid, objUser);

                    if (objUser.EmpId != null)
                    {
                        if (objUser.EmpId.ToString() != "")
                        {
                            Session["sEmpID"] = objUser.EmpId.Trim();
                            Session["sCompID"] = objUser.CompId.Trim();
                            Session["sEmpCode"] = objUser.EmpCode.Trim();
                            Session["sEmpName"] = objUser.EmpName.Trim();
                            Session["sEmpSex"] = objUser.EmpSex.Trim();
                            Session["sDesignation"] = objUser.Designation.Trim();
                            Session["sLocation"] = objUser.Location.Trim();
                            Session["sJoinDate"] = objUser.JoinDate.Trim();
                            Session["sPAN"] = objUser.PAN.Trim();
                            Session["SMSURL"] = objUser.SMSURL.Trim();
                            Session["sGrade"] = objUser.Grade.Trim();
                            Session["sEmailId"] = objUser.EmailId.Trim();
                            Session["sLastLogin"] = objUser.LastLogin.Trim();
                            Session["IsLogin"] = true;

                            Session.Timeout = 30;

                            FormsAuthentication.SetAuthCookie(objUser.EmpName.Trim(), false);

                            objCommon.UpdateLoginDateTime((string)Session["sCompID"], (string)Session["sEmpCode"], DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), token);

                            ViewState["isExternalAccess"] = true;
                            Response.Redirect("Default.aspx", true);
                        }
                        else
                        {

                            Session["Error"] = "Employee does not exists (EMPBLANK).";
                            Response.Redirect("../ErrorPage.aspx", true);
                        }
                    }
                    else
                    {

                        Session["Error"] = "Employee does not exists (EMPNULL).";
                        Response.Redirect("../ErrorPage.aspx", true);
                    }
                }




            }
            catch (Exception ex)
            {

            }
        }
    }
}