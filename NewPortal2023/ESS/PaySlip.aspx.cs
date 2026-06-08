using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace NewPortal2023.ESS
{
    public partial class PaySlip : System.Web.UI.Page
    {
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();
        NewPortal2023.ESS.Payslip objInv = new NewPortal2023.ESS.Payslip();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsMon;
        private string SourcePath = string.Empty;
        public string filePath = "";
        public string compAId = "";
        public string compPath = "";
        public string signedUrl = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sCompID"] != null)
            {
            try
            {
                if (!Page.IsPostBack)
                {
                    //    //string compid = (string)Session["sCompID"];
                    //    if (Request.QueryString["key"] != null)
                    //    {
                    //        string key = Request.QueryString["key"].Replace(" ", "+");
                    //        string keyDecrypted = TokenManager.DecryptStringAES(key.Trim(), "#hsenid$");

                    //        string[] id = new string[4];
                    //        id = keyDecrypted.Split(';');

                    //        string empid = id[0].Replace("empid=", "").Trim();
                    //        string token = id[2].Replace("token=", "").Trim();

                    //        DateTime dtToken = DateTime.ParseExact(token.Replace("+", " "), "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    //        DateTime dtTokenNow = dtToken.AddMinutes(330);
                    //        DateTime dtTokenPlus = dtToken.AddMinutes(345);

                    //        if (DateTime.Now.AddMinutes(15) < dtTokenNow)
                    //        {
                    //            lblMessage.Text = "Token invalid.";
                    //            Session["Error"] = "Token invalid.";
                    //            Response.Redirect("../ErrorPage.aspx", true);
                    //        }
                    //        else if (DateTime.Now > dtTokenPlus)
                    //        {
                    //            lblMessage.Text = "Token invalid.";
                    //            Session["Error"] = "Token invalid.";
                    //            Response.Redirect("../ErrorPage.aspx", true);
                    //        }

                    //        objcommon.Validate_Login("ANGEL", empid, objUser);

                    //        if (objUser.EmpId != null)
                    //        {
                    //            if (objUser.EmpId.ToString() != "")
                    //            {
                    //                Session["sEmpID"] = objUser.EmpId.Trim();
                    //                Session["sCompID"] = objUser.CompId.Trim();
                    //                Session["sEmpCode"] = objUser.EmpCode.Trim();
                    //                Session["sEmpName"] = objUser.EmpName.Trim();
                    //                Session["sDesignation"] = objUser.Designation.Trim();
                    //                Session["sLocation"] = objUser.Location.Trim();
                    //                Session["sJoinDate"] = objUser.JoinDate.Trim();
                    //                Session["sPAN"] = objUser.PAN.Trim();
                    //                Session["SMSURL"] = objUser.SMSURL.Trim();
                    //                Session["sGrade"] = objUser.Grade.Trim();
                    //                Session["sEmailId"] = objUser.EmailId.Trim();
                    //                Session["sLastLogin"] = objUser.LastLogin.Trim();
                    //                Session["IsLogin"] = true;
                    //                Session["sCompAID"] = objUser.CompsName.Trim();




                    //                if ((string)Session["sEmpCode"] != "A00001")
                    //                {
                    //                    ArrayList ArrListName = new ArrayList();
                    //                    ArrayList ArrListValue = new ArrayList();

                    //                    objcommon.Get_Menu(ArrListName, ArrListValue, (string)(Session["sCompID"]), (string)(Session["sEmpID"]));

                    //                    if (!ArrListValue.Contains(objcommon.GetCurrentPageName(Request.Url.AbsolutePath)))
                    //                    {
                    //                        Session["Error"] = "Action not allowed";
                    //                        Response.Redirect("../ErrorPage.aspx", true);
                    //                    }
                    //                }

                    //                FormsAuthentication.SetAuthCookie(objUser.EmpName.Trim(), false);

                    //                objcommon.UpdateLoginDateTime((string)Session["sCompID"], (string)Session["sEmpCode"], DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), HttpUtility.HtmlEncode(keyDecrypted));

                    //                ViewState["isExternalAccess"] = true;

                    //                if ((string)Session["sCompID"] == "CO000114")
                    //                {
                    //                    Menu navMenu = (Menu)Master.FindControl("NavigationMenu");
                    //                    navMenu.Visible = false;
                    //                }
                    //            }
                    //            else
                    //            {
                    //                lblMessage.Text = "Employee does not exists (EMPBLANK).";
                    //                Session["Error"] = "Employee does not exists (EMPBLANK).";
                    //                Response.Redirect("../ErrorPage.aspx", true);
                    //            }
                    //        }
                    //        else
                    //        {
                    //            lblMessage.Text = "Employee does not exists (EMPNULL).";
                    //            Session["Error"] = "Employee does not exists (EMPNULL).";
                    //            Response.Redirect("../ErrorPage.aspx", true);
                    //        }
                    //    }
                    lblMessage.Text = "";
                    mv.SetActiveView(vwList);
                    SetRights();
                    if ((string)Session["sCompID"] == "CO000114")
                    {
                        try
                        {
                            //Session["CompName"] = Session["sCompAID"].ToString();
                            //if (Session["CompName"].ToString() == "Alumni")
                            //{
                            ////    trPaySlipType.Visible = true;
                            ////    trMonth.Visible = false;
                            //}
                            //else
                            //{
                            // trPaySlipType.Visible = false;
                            FillMonthsAngel();
                            CreateDocumentsStructureAngel();
                            // }

                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                            //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading payslip details.');");
                        }
                    }
                    else if (Session["sCompID"] != null)
                    {
                        try
                        {
                            
                            // trPaySlipType.Visible = false;
                            FillMonths();
                            CreateDocumentsStructure();
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                            //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading payslip details.');");
                        }
                    }


                    else if (Session["sCompID"] != null)
                    {
                        try
                        {
                            if ((string)Session["sCompID"] == "CO000057")
                            {
                                Session["sCompAID"] = "ORRA";
                            }
                            // trPaySlipType.Visible = false;
                            FillMonths();
                            CreateDocumentsStructure();
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                            //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading payslip details.');");
                        }
                    }
                    else
                    {
                        //lblMessage.Text = "Employee does not exists (COMP).";
                        //Session["Error"] = "Employee does not exists (COMP).";
                        Session["Error"] = Request.QueryString["key"].Replace(" ", "+") + "<br>COMP";
                        Response.Redirect("../ErrorPage.aspx", true);
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                //Session["Error"] = ex.Message + "<br>Payslip.aspx";
                Session["Error"] = ex.Message + "<br>Payslip.aspx<br>" + Request.QueryString["key"].Replace(" ", "+");
                Response.Redirect("../ErrorPage.aspx", true);
            }
        }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }
        private void SetRights()
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] urlSlip = url.Split('/');
            string page = urlSlip[urlSlip.Length - 1];
            string cr = "";
            string vw = "";
            string up = "";
            string de = "";

            DataTable dt = (DataTable)Session["Menu"];
            DataRow[] dr;
            if (dt != null)
            {
                dr = dt.Select("menu_url='" + page + "'");
                if (dr.Length > 0)
                {
                    //cr = dr[0][3].ToString();
                    //vw = dr[0][4].ToString();
                    //up = dr[0][5].ToString();
                    //de = dr[0][6].ToString();

                    //ViewState["cr"] = cr;
                    //ViewState["vw"] = vw;
                    //ViewState["up"] = up;
                    //ViewState["de"] = de;
                }
            }
        }

        private void FillMonthsAngel()
        {
            try
            {
                StringBuilder sbDetails = new StringBuilder();
                dsMon = new DataSet();
                lblMessage.Text = "";
                // System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]));
                //if (Session["CompName"].ToString() == "Alumni")
                //{
                //    if (drpPaySlipType.SelectedValue != "")
                //    {
                //        if (drpPaySlipType.SelectedValue == "Current")
                //        {
                //            System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGELALUMNI");
                //            System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
                //            sbDetails.Append("<ROOT>");
                //            foreach (DirectoryInfo dir in fileNames)
                //            {
                //                if (dir.Name.ToString().ToUpper() != "FORM16" && dir.Name.ToString().ToUpper() != "INCRLETTERS" && dir.Name.ToString().ToUpper() != "FORM12BB" && dir.Name.ToString().ToUpper() != "REIMBURSEMENT" && dir.Name.ToString().ToUpper() != "DOCUMENTS" && dir.Name.ToString().ToUpper() != "TAXCOMPUTATION" && dir.Name.ToString().ToUpper() != "CUMULATIVE")
                //                {
                //                    sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
                //                }
                //            }
                //            sbDetails.Append("</ROOT>");

                //            dsMon = objInv.FillMonths(sbDetails.ToString());

                //            drpMonth.Items.Clear();
                //            drpMonth.DataTextField = "MONYEARCODE";
                //            drpMonth.DataValueField = "MONYEARNAME";
                //            drpMonth.DataSource = dsMon;
                //            drpMonth.DataBind();
                //            drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));
                //        }
                //        else if (drpPaySlipType.SelectedValue == "Previous")
                //        {
                //            System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL");
                //            System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
                //            sbDetails.Append("<ROOT>");
                //            foreach (DirectoryInfo dir in fileNames)
                //            {
                //                if (dir.Name.ToString().ToUpper() != "FORM16" && dir.Name.ToString().ToUpper() != "INCRLETTERS" && dir.Name.ToString().ToUpper() != "FORM12BB" && dir.Name.ToString().ToUpper() != "REIMBURSEMENT" && dir.Name.ToString().ToUpper() != "DOCUMENTS" && dir.Name.ToString().ToUpper() != "TAXCOMPUTATION" && dir.Name.ToString().ToUpper() != "CUMULATIVE")
                //                {
                //                    sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
                //                }
                //            }
                //            sbDetails.Append("</ROOT>");

                //            dsMon = objInv.FillMonths(sbDetails.ToString());

                //            drpMonth.Items.Clear();
                //            drpMonth.DataTextField = "MONYEARCODE";
                //            drpMonth.DataValueField = "MONYEARNAME";
                //            drpMonth.DataSource = dsMon;
                //            drpMonth.DataBind();
                //            drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));
                //        }

                //    }
                //    else
                //    {
                //        lblMessage.Text = "First Select PaySlip Type.";
                //    }

                //}
                //else
                //{
                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path"))  + "\\PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL");
                System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
                sbDetails.Append("<ROOT>");
                foreach (DirectoryInfo dir in fileNames)
                {
                    if (dir.Name.ToString().ToUpper() != "FORM16" && dir.Name.ToString().ToUpper() != "INCRLETTERS" && dir.Name.ToString().ToUpper() != "FORM12BB" && dir.Name.ToString().ToUpper() != "REIMBURSEMENT" && dir.Name.ToString().ToUpper() != "DOCUMENTS" && dir.Name.ToString().ToUpper() != "TAXCOMPUTATION" && dir.Name.ToString().ToUpper() != "CUMULATIVE")
                    {
                        sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
                    }
                }
                sbDetails.Append("</ROOT>");

                dsMon = objInv.FillMonths(sbDetails.ToString());

                drpMonth.Items.Clear();
                drpMonth.DataTextField = "MONYEARCODE";
                drpMonth.DataValueField = "MONYEARNAME";
                drpMonth.DataSource = dsMon;
                drpMonth.DataBind();
                drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));
                // }


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void FillMonths()
        {
            try
            {
                StringBuilder sbDetails = new StringBuilder();
                dsMon = new DataSet();

                // System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]));

                //System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]));
                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]));

                System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
                sbDetails.Append("<ROOT>");
                foreach (DirectoryInfo dir in fileNames)
                {
                    if (dir.Name.ToString().ToUpper() != "FORM16" && dir.Name.ToString().ToUpper() != "INCRLETTERS" && dir.Name.ToString().ToUpper() != "FORM12BB" && dir.Name.ToString().ToUpper() != "REIMBURSEMENT" 
                        && dir.Name.ToString().ToUpper() != "DOCUMENTS" && dir.Name.ToString().ToUpper() != "TAXCOMPUTATION" && dir.Name.ToString().ToUpper() != "CUMULATIVE" && dir.Name.ToString().ToUpper() != "MISREPORTS" 
                        && dir.Name.ToString().ToUpper() != "MONTHLYREPORTS" && dir.Name.ToString().ToUpper() != "SALARY ARREARS AND PLP 2022_2023" && dir.Name.ToString().ToUpper() != "PMS")

                    {
                        sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
                    }
                }
                sbDetails.Append("</ROOT>");

                dsMon = objInv.FillMonths(sbDetails.ToString());

                drpMonth.Items.Clear();
                drpMonth.DataTextField = "MONYEARCODE";
                drpMonth.DataValueField = "MONYEARNAME";
                drpMonth.DataSource = dsMon;
                drpMonth.DataBind();
                drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));
                drpMonth.Items.Insert(1, new ListItem("SALARY ARREARS AND PLP 2022_2023", "SALARY ARREARS AND PLP 2022_2023"));
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)Session["sCompID"] == "CO000114")
            {
                //DisplayDocumentsAngel();
                DisplayDocuments();
            }
            else
            {
                DisplayDocuments();
            }
            //Session["Month_Year"] = drpMonth.SelectedValue;

            if ((string)Session["serverpath"] != null)
            {
                divAlert.Visible = false;
                string pdfPath = Session["serverpath"].ToString();
                //string secretKey = "MySuperSecretKey123";

     
                string secretKey = NewPortal2023.ESS.KeyManager.Get("secretKey");
                DateTime istExpiry = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time")).AddMinutes(1);

                long expiryTicks = new DateTimeOffset(istExpiry).ToUnixTimeSeconds();


                string signature = GenerateSignature(pdfPath, expiryTicks, secretKey);
                signedUrl = $"/PayslipHandler.ashx?file={HttpUtility.UrlEncode(pdfPath)}&expiry={expiryTicks}&sig={HttpUtility.UrlEncode(signature)}";

                ScriptManager.RegisterStartupScript(this, GetType(), "LoadPdf", $"loadPdf('{ResolveUrl(signedUrl)}');", true);
                //hdnSignedUrl.Value = ResolveUrl(signedUrl);

                //ScriptManager.RegisterStartupScript(this, GetType(), "LoadPdf", "loadPdf();", true);

                //lblMessage.Text = "Raw:- " + signedUrl + "Paresh:-" + pdfPath;
                //lblMessage.Visible = true;
            }
        }

        private void DisplayDocumentsAngel()
        {
            try
            {
                CreateDocumentsStructureAngel();
                //if (drpPaySlipType.SelectedValue.ToString() == "Current")
                //{
                //    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGELALUMNI\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "\\PAYSLIP";
                //    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                //    System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                //    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                //    foreach (System.IO.FileInfo fi in fileNames)
                //    {
                //        string[] strEmpcode = fi.Name.Split('_');

                //        string Emp_ID = Session["sEmpCode"].ToString();
                //        string EmpCode = strEmpcode[0].ToString().Trim().ToUpper();
                //        if (EmpCode == Emp_ID)
                //        //if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                //        {
                //            DataRow drDocRow = dtDocInfo.NewRow();
                //            drDocRow["FILEPATH"] = fi.FullName;
                //            drDocRow["FILENAME"] = fi.Name;
                //            dtDocInfo.Rows.Add(drDocRow);
                //            filePath = "../PDF Reports/" + Convert.ToString(Session["sCompID"]) + "/ANGEL/" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "/PAYSLIP/" + fi.Name;
                //            //string filePath = string.Format("Payslip.aspx?FN={0}",  fi.Name);
                //        }
                //    }
                //    this.gvViewDocDetails.DataSource = dtDocInfo;
                //    this.gvViewDocDetails.DataBind();
                //}
                //else if (drpPaySlipType.SelectedValue.ToString() == "Previous")
                //{
                //    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "\\PAYSLIP";
                //    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                //    System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                //    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                //    foreach (System.IO.FileInfo fi in fileNames)
                //    {
                //        string[] strEmpcode = fi.Name.Split('_');

                //        string Emp_ID = Session["sEmpCode"].ToString();
                //        string EmpCode = strEmpcode[0].ToString().Trim().ToUpper();
                //        if (EmpCode == Emp_ID)
                //        //if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                //        {
                //            DataRow drDocRow = dtDocInfo.NewRow();
                //            drDocRow["FILEPATH"] = fi.FullName;
                //            drDocRow["FILENAME"] = fi.Name;
                //            dtDocInfo.Rows.Add(drDocRow);
                //            filePath = "../PDF Reports/" + Convert.ToString(Session["sCompID"]) + "/ANGEL/" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "/PAYSLIP/" + fi.Name;
                //            //string filePath = string.Format("Payslip.aspx?FN={0}",  fi.Name);
                //        }
                //    }
                //    this.gvViewDocDetails.DataSource = dtDocInfo;
                //    this.gvViewDocDetails.DataBind();
                //}
                //else
                //{
                //SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "\\PAYSLIP";
                SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\NPL\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "\\PAYSLIP";
                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                foreach (System.IO.FileInfo fi in fileNames)
                {
                    string[] strEmpcode = fi.Name.Split('_');

                    string Emp_ID = Session["sEmpCode"].ToString();
                    string EmpCode = strEmpcode[0].ToString().Trim().ToUpper();
                    if (EmpCode == Emp_ID)
                    //if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                        filePath = "../PDF Reports/" + Convert.ToString(Session["sCompID"]) + "/NPL/" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "/PAYSLIP/" + fi.Name;
                        //string filePath = string.Format("Payslip.aspx?FN={0}",  fi.Name);
                    }
                }

                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + "\\PDF Reports\\"  + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\CUMULATIVE";

                    dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                    fileNames = dirInfo.GetFiles("*.*");

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        string[] strEmpcode = fi.Name.Split('_');

                        if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                        {
                            DataRow drDocRow = dtDocInfo.NewRow();
                            drDocRow["FILEPATH"] = fi.FullName;
                            drDocRow["FILENAME"] = fi.Name;
                            dtDocInfo.Rows.Add(drDocRow);
                            //break;
                        }
                    }

                    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + "\\PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\INCRLETTERS";

                    dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                    fileNames = dirInfo.GetFiles("*.*");

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        string[] strEmpcode = fi.Name.Split('_');

                        if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                        {
                            DataRow drDocRow = dtDocInfo.NewRow();
                            drDocRow["FILEPATH"] = fi.FullName;
                            drDocRow["FILENAME"] = fi.Name;
                            dtDocInfo.Rows.Add(drDocRow);
                        }
                    }
                }

                this.gvViewDocDetails.DataSource = dtDocInfo;
                this.gvViewDocDetails.DataBind();
                // }
            }
            catch (Exception ex)
            {

            }
        }

        private void CreateDocumentsStructureAngel()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("PREVIEW", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvViewDocDetails.DataSource = dtDocuments;
                gvViewDocDetails.DataBind();
            }
        }

        private void DisplayDocuments()
        {
            try
            {
                CreateDocumentsStructure();
                if ((string)Session["sCompID"] != "CO000114")
                {
                    //SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "\\PAYSLIP";
                    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "\\PAYSLIP";
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);


                    if (!dirInfo.Exists)
                    {
                        Session["serverpath"] = null;
                        gvViewDocDetails.DataSource = null;
                        gvViewDocDetails.DataBind();
                        return;
                    }
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);
                    bool fileFound = false;
                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        string[] strEmpcode = fi.Name.Split('_');

                        string Emp_ID = Session["sEmpCode"].ToString();
                        string EmpCode = strEmpcode[0].ToString().Trim().ToUpper();
                        if (EmpCode == Emp_ID)
                        //if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                        {
                            DataRow drDocRow = dtDocInfo.NewRow();
                            drDocRow["FILEPATH"] = fi.FullName;
                            drDocRow["FILENAME"] = fi.Name;
                            dtDocInfo.Rows.Add(drDocRow);
                            //filePath = "../PDF Reports/" + Convert.ToString(Session["sCompID"]) + "/" + Convert.ToString(Session["sCompAID"]) + "/" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "/PAYSLIP/" + fi.Name;

                            filePath = "~/PDF Reports/" + Convert.ToString(Session["sCompID"]) + "/NPL/" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "/PAYSLIP/" + fi.Name;

                            //filePath = "~/PDF Reports/" + Convert.ToString(Session["sCompID"]) + "/" +  Convert.ToString(Session["sCompAID"]) + "/" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "/ PAYSLIP / " + fi.Name;


                            if (fi.Name.ToUpper().Contains("PAYSLIP"))
                            {
                                Session["serverpath"] = filePath;
                            }
                            

                            this.gvViewDocDetails.DataSource = dtDocInfo;
                            this.gvViewDocDetails.DataBind();

                            fileFound = true;

                        }
                    }

                    if (!fileFound)
                    {
                        Session["serverpath"] = null;
                        lblMessage.Text = "Payslip is not available";
                        divAlert.Visible = true;
                    }
                    //this.gvViewDocDetails.DataSource = dtDocInfo;
                    //this.gvViewDocDetails.DataBind();
                }

                //if ((string)Session["sCompID"] == "CO000114")
                //{
                //    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + "\\PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "/PAYSLIP/" + fi.Name;

                //    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                //    System.IO.FileInfo[]  fileNames = dirInfo.GetFiles("*.*");
                //    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);
                //    foreach (System.IO.FileInfo fi in fileNames)
                //    {
                //        string[] strEmpcode = fi.Name.Split('_');

                //        if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                //        {
                //            DataRow drDocRow = dtDocInfo.NewRow();
                //            drDocRow["FILEPATH"] = fi.FullName;
                //            drDocRow["FILENAME"] = fi.Name;
                //            dtDocInfo.Rows.Add(drDocRow);
                //            //break;
                //        }
                //    }

                //    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + "\\PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\INCRLETTERS";

                //    dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                //    fileNames = dirInfo.GetFiles("*.*");

                //    foreach (System.IO.FileInfo fi in fileNames)
                //    {
                //        string[] strEmpcode = fi.Name.Split('_');

                //        if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                //        {
                //            DataRow drDocRow = dtDocInfo.NewRow();
                //            drDocRow["FILEPATH"] = fi.FullName;
                //            drDocRow["FILENAME"] = fi.Name;
                //            dtDocInfo.Rows.Add(drDocRow);
                //        }
                //    }
                //    this.gvViewDocDetails.DataSource = dtDocInfo;
                //    this.gvViewDocDetails.DataBind();
                //}

               
            }
            catch (Exception ex)
            {
                Session["serverpath"] = null;
                gvViewDocDetails.DataSource = null;
                gvViewDocDetails.DataBind();
            }
        }

        protected void lnkBtnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblTSFileStorageName");

                string openFilePath = lblTSFileStorageName.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
                                                                //openFilePath = openFilePath + ViewState["FILE_PATH"].ToString();

                //string fileName = lblTSFileStorageName.Text;
                System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + lnkBtnOpenFiles.Text);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void CreateDocumentsStructure()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));
                //dtDocuments.Columns.Add(new DataColumn("PREVIEW", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvViewDocDetails.DataSource = dtDocuments;
                gvViewDocDetails.DataBind();
            }
        }

        protected void lnkBtnOpenPreviewFile_Click(object sender, EventArgs e)
        {

            try
            {
                try
                {
                    LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                    LinkButton lnkpdfpath = (LinkButton)lnkBtnOpenFiles.NamingContainer.FindControl("lnkBtnOpenFile");
                    Session["lnkBtnOpenFile"] = lnkpdfpath.Text;
                    Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblTSFileStorageName");
                    Session["openFilePath"] = lblTSFileStorageName.Text;


                    Response.Redirect("../ESS/PreviewPagePaySlip.aspx", true);
                }
                catch
                {
                    lblMessage.Text = "Error occurred in application.";
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }

        }

        protected void drpPaySlipType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            if (drpPaySlipType.SelectedValue.ToString() != "")
            {

                trMonth.Visible = true;
                FillMonthsAngel();
                CreateDocumentsStructureAngel();
            }
            else
            {
                trMonth.Visible = false;
            }
        }

        private static string GenerateSignature(string fileName, long expiryTicks, string key)
        {
            string normalizedFile = fileName.Replace("\\", "/").Trim();
            string data = normalizedFile + "|" + expiryTicks;
            /*   string data = fileName + "|" + expiryTicks;*/
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(hash);
            }
        }
    }
}