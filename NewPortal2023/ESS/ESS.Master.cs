using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

namespace NewPortal2023.ESS
{
    public partial class ESS : System.Web.UI.MasterPage
    {
        NewPortal2023.ESS.Common objCommon;
        ArrayList ArrListName;
        ArrayList ArrListValue;

        public string MenuScript = String.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Cache.SetExpires(DateTime.UtcNow.AddDays(1));
                Response.Cache.SetValidUntilExpires(false);
                Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();

                if (Session["sCompID"] == null)
                {
                    //string message = "Your session has expired. You will be redirected to the login page.";
                    //string redirectUrl = "../ESS/Login.aspx";

                    //string script = $@"
                    //<script type='text/javascript'>
                    //    alert('{message}');
                    //    window.location.href = '{redirectUrl}';
                    //</script>";

                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "alertRedirect", script);

                    HttpCookie loginCookie = Request.Cookies["Login"];
                    if (loginCookie != null)
                    {

                        Session["sEmpID"] = loginCookie.Values["sEmpID"];
                        Session["sCompID"] = loginCookie.Values["sCompID"];
                        Session["sEmpCode"] = loginCookie.Values["sEmpCode"];
                        Session["sEmpName"] = loginCookie.Values["sEmpName"];

                        Session["sJoinDate"] = loginCookie.Values["sJoinDate"];
                        Session["sPAN"] = loginCookie.Values["sPAN"];

                        Session["sLastLogin"] = loginCookie.Values["sLastLogin"];
                        Session["IsLogin"] = true;
                        Session["sCompAID"] = loginCookie.Values["sCompAID"];
                    }

                }

                DataTable dt = new DataTable();

                //if (Session["sEmpID"] == "CO000114")
                //{
                //    Panel1.Visible = true;
                //}

                //if (Session["sEmpID"] != null)
                //{
                lblUser.Text = (string)Session["sEmpName"];
                //lblEmail.Text = (string)Session["EmpEmail"];

                //lblPAN.Text = (string)(Session["sPAN"]);
                //lblDateOfJoin.Text = (string)(Session["sJoinDate"]);
                //lblLoginDateTime.Text = (string)(Session["sLastLogin"]);

                //lblEmployeeCodeHRRL.Text = (string)(Session["sEmpCode"]);
                //lblEmployeeNameHRRL.Text = (string)(Session["sEmpName"]);

                if (!Page.IsPostBack)
                {

                }

                Fill_Menu();
                if (Session["SubLink"] != null)
                {
                    Session["SubLink"] = null;
                }
                else if (this.Page.Page.ToString().Replace("ASP.", "").Replace("_", ".") != "default.aspx")
                {
                    dt = (DataTable)Session["Menu"];
                    DataRow[] dr;
                    if (dt != null)
                    {
                        dr = dt.Select("menu_url='" + this.Page.Page.ToString().Replace("ASP.", "").Replace("_", ".") + "'");

                    }
                }

                //else
                //{
                //Response.Redirect("Login.aspx", false);
                //}
            }
            catch (Exception ex)
            {
                objCommon.Display("Validate", "DisplayErrorMessage('" + objCommon.EncodeJsString(ex.Message) + "');");
            }
        }



        private void Fill_Menu()
        {
            objCommon = new NewPortal2023.ESS.Common();
            //Session["Menu"] = objCommon.Get_Menu((string)(Session["sEmpID"]), (string)(Session["sEmpName"]), Convert.ToString(Session["sCompCode"]), ref MenuScript, this.Page.Page.ToString().Replace("ASP.", "").Replace("_", ".").ToLower());
            //Session["Menu"] = objCommon.Get_Menu((string)(Session["sEmpCode"]),(string)(Session["ProfId"]), ref MenuScript, this.Page.Page.ToString().Replace("ASP.", "").Replace("_", ".").ToLower());
            Session["Menu"] = objCommon.Get_Menu((string)(Session["sEmpID"]), (string)(Session["sCompID"]), ref MenuScript, this.Page.Page.ToString().Replace("ASP.", "").Replace("_", ".").ToLower());
            string menu = Session["Menu"].ToString();
        }

        protected void NavigationMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            try
            {
                Response.Redirect(e.Item.Value.ToString().Trim(), true);

            }

            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
        }

        //private void Fill_Menu()
        //{
        //    objCommon = new NewPortal2023.ESS.Common();
        //    ////Session["Menu"] = objCommon.Get_Menu((string)(Session["sEmpID"]), (string)(Session["sEmpName"]), Convert.ToString(Session["sCompCode"]), ref MenuScript, this.Page.Page.ToString().Replace("ASP.", "").Replace("_", ".").ToLower());
        //    ////Session["Menu"] = objCommon.Get_Menu((string)(Session["sEmpCode"]),(string)(Session["ProfId"]), ref MenuScript, this.Page.Page.ToString().Replace("ASP.", "").Replace("_", ".").ToLower());
        //    //Session["Menu"] = objCommon.Get_Menu((string)(Session["sEmpCode"]), (string)(Session["ProfId"]),ref MenuScript, this.Page.Page.ToString().Replace("ASP.", "").Replace("_", ".").ToLower());
        //    //string menu = Session["Menu"].ToString();

        //    ArrListName = new ArrayList();
        //    ArrListValue = new ArrayList();

        //    objCommon.Get_Menu(ArrListName, ArrListValue, (string)Session["sCompID"], (string)Session["sEmpID"]);

        //    if (ArrListName.Count > 0)
        //    {
        //        ArrListName.Insert(0, "Home");
        //        ArrListValue.Insert(0, "Default.aspx");
        //        Session["MenuList"] = ArrListName;
        //        for (int cnt = 0; cnt <= ArrListName.Count - 1; cnt++)
        //        {
        //            MenuItem mnu = new MenuItem(Convert.ToString(ArrListName[cnt]), "", "", Convert.ToString(ArrListValue[cnt]));
        //            NavigationMenu.Items.Add(mnu);
        //        }
        //    }
        //    else
        //    {
        //        Session["LoginError"] = "NOMENU";
        //        if ((string)(Session["sCompID"]) != "CO000126")
        //        {
        //            Response.Redirect("Login.aspx", true);
        //        }
        //        else
        //        {
        //            Response.Redirect("LoginAlt.aspx", true);
        //        }
        //    }
        //}

        protected void lnkHome_Click(object sender, EventArgs e)
        {
            try
            {
                //string TEST = "https://npl.sequelgroup.co.in/ESS/NPLDefault.aspx?NPLID=" + objCommon.Encrypt((string)(Session["sEmpCode"]));
                // Response.Redirect("https://npl.sequelgroup.co.in/ESS/NPLDefault.aspx?NPLID=" + objCommon.Encrypt((string)(Session["sEmpCode"])), false);
                if (Session["sCompID"].ToString() == "CO000141")
                {
                    //Response.Redirect("https://npl.sequelgroup.co.in/ESS/Default.aspx?NPLID=" + (string)(Session["sEmpCode"]), false);
                    //Response.Redirect("https://npl.sequelgroup.co.in/ExpenseDefaultPage.aspx?NPLID=" + (string)(Session["sEmpCode"]) + "_" + (string)(Session["sCompID"]), false);
                    Response.Redirect("https://naperol.sequelgroup.co.in/ESS/NewPortalDefaultPage.aspx?NPLID=" + (string)(Session["sEmpCode"]) + "_" + (string)(Session["sCompID"]), false);
                }
                else
                {
                    Response.Redirect("https://sequelgroup.co.in/ESS/NPLDefault.aspx?NPLID=" + (string)(Session["sEmpCode"]), false);
                }
            }
            catch (Exception ex)
            {
                objCommon.Display("Validate", "DisplayErrorMessage('" + objCommon.EncodeJsString(ex.Message) + "');");
            }
        }

        protected void lnkManual_Click(object sender, EventArgs e)
        {
            try
            {
                if ((string)Session["sCompID"] == "CO000141")
                {
                    string title = "Attendance";

                    //dsInv = objNps.GetEmpDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
                    //if (dsInv.Tables.Count > 0)
                    //{
                        //if (dsInv.Tables[0].Rows[0]["PROF_ID"].ToString() == "101")
                        //{
                        System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/npl/NPL_ESS_GUIDELINES_Employee.pdf"));
                        if (fileObj.Exists)
                        {
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AppendHeader("content-disposition", @"attachment; filename=NPL_ESS_GUIDELINES_Employee.pdf");
                            Response.ContentType = "text/csv";
                            Response.WriteFile(fileObj.FullName);
                            Response.End();
                           
                        }
                        //}
                        //else if (dsInv.Tables[0].Rows[0]["PROF_ID"].ToString() == "84")
                        //{
                        //    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/npl/NPL_ESS_GUIDELINES_Employee.pdf"));
                        //    if (fileObj.Exists)
                        //    {
                        //        Response.Clear();
                        //        Response.Buffer = true;
                        //        Response.AppendHeader("content-disposition", @"attachment; filename=NPL_ESS_GUIDELINES_Employee.pdf");
                        //        Response.ContentType = "text/csv";
                        //        Response.WriteFile(fileObj.FullName);
                        //        Response.End();
                        //        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", false);
                        //    }
                        //}
                    

                }
            }
            catch (Exception ex)
            {
                
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
            }
        }
    }
}