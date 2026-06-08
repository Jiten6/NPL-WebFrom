using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using HBS.Encoder;

namespace NewPortal2023.ESS
{
    public partial class FlexipayDetails : System.Web.UI.Page
    {
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();
        NewPortal2023.ESS.FlexiPay objInv = new NewPortal2023.ESS.FlexiPay();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataTable dtInv = new DataTable();
        DataSet dsInv = new DataSet();
        DataTable dtInv1 = new DataTable();
        DataSet dsInv1 = new DataSet();
        double totAmt = 0;
        double basic = 0;
        public string MenuScript;

        protected void Page_Load(object sender, EventArgs e)
        {
            //try
            //{
            //Session["Error"] = "Action not allowed";
            //Response.Redirect("../ErrorPage.aspx", true);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["key"] != null)
                {
                    string key = Request.QueryString["key"].Replace(" ", "+");
                    string keyDecrypted = TokenManager.DecryptStringAES(key.Trim(), "#hsenid$");

                    string[] id = new string[4];
                    id = keyDecrypted.Split(';');

                    string empid = id[0].Replace("empid=", "").Trim();
                    string token = id[2].Replace("token=", "").Trim();

                    DateTime dtToken = DateTime.ParseExact(token.Replace("+", " "), "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    DateTime dtTokenNow = dtToken.AddMinutes(330);
                    DateTime dtTokenPlus = dtToken.AddMinutes(345);

                    if (DateTime.Now.AddMinutes(15) < dtTokenNow)
                    {
                        lblMessage.Text = "Token invalid.";
                        Session["Error"] = "Token invalid.";
                        Response.Redirect("../ErrorPage.aspx", true);
                    }
                    else if (DateTime.Now > dtTokenPlus)
                    {
                        lblMessage.Text = "Token invalid.";
                        Session["Error"] = "Token invalid.";
                        Response.Redirect("../ErrorPage.aspx", true);
                    }

                    objcommon.Validate_Login("ANGEL", empid, objUser);

                    if (objUser.EmpId != null)
                    {
                        if (objUser.EmpId.ToString() != "")
                        {
                            Session["sEmpID"] = objUser.EmpId.Trim();
                            Session["sCompID"] = objUser.CompId.Trim();
                            Session["sEmpCode"] = objUser.EmpCode.Trim();
                            Session["sEmpName"] = objUser.EmpName.Trim();
                            Session["sDesignation"] = objUser.Designation.Trim();
                            Session["sLocation"] = objUser.Location.Trim();
                            Session["sJoinDate"] = objUser.JoinDate.Trim();
                            Session["sPAN"] = objUser.PAN.Trim();
                            Session["SMSURL"] = objUser.SMSURL.Trim();
                            Session["sGrade"] = objUser.Grade.Trim();
                            Session["sEmailId"] = objUser.EmailId.Trim();
                            Session["sLastLogin"] = objUser.LastLogin.Trim();
                            Session["IsLogin"] = true;

                            if ((string)Session["sEmpCode"] != "A00001")
                            {
                                string[] allowed_grades = { "M1", "M2", "M3", "M4", "M5", "M6", "M7", "M8", "M9" };

                                if (!allowed_grades.Contains(objUser.Grade.Trim()))
                                {
                                    Session["Error"] = "Not allowed.";
                                    Response.Redirect("../ErrorPage.aspx", true);
                                }

                                ArrayList ArrListName = new ArrayList();
                                ArrayList ArrListValue = new ArrayList();

                                //objcommon.Get_Menu(ArrListName, ArrListValue, (string)(Session["sCompID"]), (string)(Session["sEmpID"]));
                                Session["Menu"] = objcommon.Get_Menu((string)(Session["sEmpID"]), (string)(Session["sCompID"]), ref MenuScript, this.Page.Page.ToString().Replace("ASP.", "").Replace("_", ".").ToLower());


                                if (!ArrListValue.Contains(objcommon.GetCurrentPageName(Request.Url.AbsolutePath)))
                                {
                                    //Session["Error"] = "Flexi option will be available only between 10th and 20th of every month.";
                                    //Response.Redirect("../ErrorPage.aspx", true);

                                    btnsubmit.Visible = false;
                                }
                                else
                                {
                                    btnsubmit.Visible = true;
                                }
                            }

                            FormsAuthentication.SetAuthCookie(objUser.EmpName.Trim(), false);

                            objcommon.UpdateLoginDateTime((string)Session["sCompID"], (string)Session["sEmpCode"], DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), HttpUtility.HtmlEncode(keyDecrypted));

                            ViewState["isExternalAccess"] = true;

                            if ((string)Session["sCompID"] == "CO000114")
                            {
                                Menu navMenu = (Menu)Master.FindControl("NavigationMenu");
                                navMenu.Visible = false;
                            }
                        }
                        else
                        {
                            lblMessage.Text = "Employee does not exists (EMPBLANK).";
                            Session["Error"] = "Employee does not exists (EMPBLANK).";
                            Response.Redirect("../ErrorPage.aspx", true);
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Employee does not exists (EMPNULL).";
                        Session["Error"] = "Employee does not exists (EMPNULL).";
                        Response.Redirect("../ErrorPage.aspx", true);
                    }
                }

                if (Session["sCompID"] != null && Session["sEmpCode"].ToString() == ("E01006") || Session["sEmpCode"].ToString() == ("E84647") || Session["sEmpCode"].ToString() == ("E83136"))
                {
                    try
                    {
                        string strResult = objcommon.Validate_ControlInfo("FLEX");

                        if (strResult.Contains("This page is currently unavailable.") == true)
                        {
                            Response.Redirect("Unavailable.aspx?strName=Flexi Pay Details");
                            return;
                        }
                        if (Session["sCompID"].ToString() == "CO000114")
                        {
                            tdtitle.InnerText = "Cost to Company";
                            trAgree.Visible = false;
                            btnsubmit.Visible = false;

                            FillCTCEMPDET();

                        }
                        else
                        {
                            FillCTC();
                        }
                    }

                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading flexi pay details.');");
                    }
                }

                // if (Session["sCompID"] != null && Session["sEmpCode"].ToString() == ("E83243") || Session["sEmpCode"].ToString() == ("E73267") || Session["sEmpCode"].ToString() == ("E50555") || Session["sEmpCode"].ToString() == ("E79431"))
                else if (Session["sCompID"] != null && Session["sEmpCode"].ToString() == ("E83243") || Session["sEmpCode"].ToString() == ("E82310"))
                {
                    try
                    {
                        string strResult = objcommon.Validate_ControlInfo("FLEX");

                        if (strResult.Contains("This page is currently unavailable.") == true)
                        {
                            Response.Redirect("Unavailable.aspx?strName=Flexi Pay Details");
                            return;
                        }

                        if (Session["sCompID"].ToString() == "CO000114")
                        {
                            tdtitle.InnerText = "Cost to Company";
                            trAgree.Visible = false;
                            btnsubmit.Visible = false;
                            FillCTCEMP();

                        }
                        else
                        {
                            FillCTC();
                        }
                    }


                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading flexi pay details.');");
                    }
                }
                else if (Session["sCompID"].ToString() != null && Session["sEmpCode"].ToString() != ("E83243") || Session["sEmpCode"].ToString() != ("E82310") || Session["sEmpCode"].ToString() != ("E01006") || Session["sEmpCode"].ToString() != ("E84647") || Session["sEmpCode"].ToString() != ("E83136"))
                {
                    try
                    {
                        tdtitle.InnerText = "Cost to Company";
                        trAgree.Visible = false;
                        btnsubmit.Visible = false;
                        FillCTCEMPDET();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = ex.Message;
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading flexi pay details.');");
                    }
                }

                else
                {
                    FillCTC();
                    //Response.Redirect("Logout.aspx");
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message;
            //}

        }

        private void FillCTCEMPDET()
        {
            try
            {
                dtInv = objInv.GetCTCDetailsAngel((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"], txtCTC, txtBal, txtAnnCTC, lblAmtMnt, lblAmtAnn);
                if (Session["sCompID"].ToString() == "CO000114")
                {
                    double ctc = Convert.ToDouble(txtCTC.Text);

                    double totAmts = totAmt * 12;
                    txtAnnCTC.Text = string.Format("{0:0.00}", totAmts);
                    //txtAnnCTC.Text = Convert.ToString(totAmt * 12);
                    double aunctc = ctc * 12;
                    txtAnctc.Text = string.Format("{0:0.00}", aunctc);
                    // txtAnctc.Text = Convert.ToString(ctc * 12);

                    txtAnctc.Visible = true;
                    gvCTC.DataSource = dtInv;
                    gvCTC.DataBind();
                    //dtInv1 = objInv.GetCTCDetailsAngel1((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"], txtCTC, txtBal, txtAnnCTC, lblAmtMnt, lblAmtAnn);
                    //gvCTC1.DataSource = dtInv1;
                    //gvCTC1.DataBind();
                    trcheckCtc.Visible = false;
                    trName.Visible = false;
                    dtInv1 = objInv.GetCTCDetailsAngel1((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"], txtCTC, txtBal, txtAnnCTC, lblAmtMnt1, lblAmtAnn1);
                    gvCTC1.DataSource = dtInv1;
                    gvCTC1.DataBind();
                    InvDetails1.Visible = true;
                    divctctotal.Visible = true;
                    lblBasePay.Text = "BASE PAY";

                    double monthctc = Convert.ToDouble(lblAmtMnt.Text);

                    Session["monthctc"] = monthctc;

                    double AmtAnn = monthctc * 12;
                    lblAmtAnn.Text = string.Format("{0:0.00}", AmtAnn);

                    //lblAmtAnn.Text = Convert.ToString(monthctc * 12);
                    lblBasePay.Font.Bold = true;
                    lblAmtMnt.Font.Bold = true;
                    lblAmtAnn.Font.Bold = true;
                    lblAmtMnt.ForeColor = System.Drawing.Color.Black;
                    lblAmtAnn.ForeColor = System.Drawing.Color.Black;
                    divctctotal1.Visible = true;

                    lblBasePay1.Text = "TOTAL BENEFITS";

                    string strAmtMnt1 = "";
                    if (lblAmtMnt1.Text == "")
                    {
                        strAmtMnt1 = "0";
                    }
                    else
                    {
                        strAmtMnt1 = lblAmtMnt1.Text;
                    }

                    double monthctc1 = Convert.ToDouble(strAmtMnt1);
                    Session["monthctc1"] = monthctc1;

                    double AmtAnn1 = monthctc1 * 12;
                    lblAmtAnn1.Text = string.Format("{0:0.00}", AmtAnn1);

                    // lblAmtAnn1.Text = Convert.ToString(monthctc1 * 12);
                    lblBasePay1.Font.Bold = true;
                    lblAmtMnt1.Font.Bold = true;
                    lblAmtAnn1.Font.Bold = true;
                    lblAmtMnt1.ForeColor = System.Drawing.Color.Black;
                    lblAmtAnn1.ForeColor = System.Drawing.Color.Black;
                    divAlltotal.Visible = true;


                    lblAllTotal.Text = "COST TO COMPANY (CTC)";

                    double AllAmtMnt = monthctc + monthctc1;
                    lblAllAmtMnt.Text = string.Format("{0:0.00}", AllAmtMnt);
                    //lblAllAmtMnt.Text = Convert.ToString(monthctc + monthctc1);
                    double anutotal = Convert.ToDouble(lblAmtAnn.Text) + Convert.ToDouble(lblAmtAnn1.Text);
                    lblAllAmtAnn.Text = string.Format("{0:0.00}", anutotal);

                    //lblAllAmtMnt.Text = Convert.ToString(monthctc + monthctc1);
                    //double anutotal = Convert.ToDouble(lblAmtAnn.Text) + Convert.ToDouble(lblAmtAnn1.Text);
                    //lblAllAmtAnn.Text = Convert.ToString(anutotal);
                    lblAllTotal.Font.Bold = true;
                    lblAllAmtMnt.Font.Bold = true;
                    lblAllAmtAnn.Font.Bold = true;
                    lblAllAmtMnt.ForeColor = System.Drawing.Color.Black;
                    lblAllAmtAnn.ForeColor = System.Drawing.Color.Black;
                    divNotes.Visible = true;

                }
                else
                {
                    txtAnnCTC.Text = Convert.ToString(totAmt * 12);
                }

                CalculateGross();
                ResetSpecialAllowance();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void FillCTCEMP()
        {
            try
            {
                dtInv = objInv.GetCTCDetailsAngel((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"], txtCTC, txtBal, txtAnnCTC, lblAmtMnt, lblAmtAnn);
                if (Session["sCompID"].ToString() == "CO000114")
                {
                    double ctc = Convert.ToDouble(txtCTC.Text);
                    txtAnnCTC.Text = Convert.ToString(totAmt * 12);
                    double aunctc = ctc * 12;
                    txtAnctc.Text = string.Format("{0:0.00}", aunctc);
                    txtAnctc.Visible = true;
                    gvCTC.DataSource = dtInv;
                    gvCTC.DataBind();
                    trcheckCtc.Visible = true;
                    trName.Visible = false;
                    dtInv1 = objInv.GetCTCDetailsAngel1((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"], txtCTC, txtBal, txtAnnCTC, lblAmtMnt1, lblAmtAnn1);
                    gvCTC1.DataSource = dtInv1;
                    gvCTC1.DataBind();
                    InvDetails1.Visible = true;
                    divctctotal.Visible = true;
                    lblBasePay.Text = "BASE PAY";

                    double monthctc = Convert.ToDouble(lblAmtMnt.Text);
                    Session["monthctc"] = monthctc;

                    double AmtAnn = monthctc * 12;
                    lblAmtAnn.Text = string.Format("{0:0.00}", AmtAnn);

                    // lblAmtAnn.Text = Convert.ToString(monthctc * 12);
                    lblBasePay.Font.Bold = true;
                    lblAmtMnt.Font.Bold = true;
                    lblAmtAnn.Font.Bold = true;
                    lblAmtMnt.ForeColor = System.Drawing.Color.Black;
                    lblAmtAnn.ForeColor = System.Drawing.Color.Black;
                    divctctotal1.Visible = true;

                    lblBasePay1.Text = "TOTAL BENEFITS";
                    double monthctc1 = Convert.ToDouble(lblAmtMnt1.Text);
                    Session["monthctc1"] = monthctc1;

                    double AmtAnn1 = monthctc1 * 12;
                    lblAmtAnn1.Text = string.Format("{0:0.00}", AmtAnn1);

                    //lblAmtAnn1.Text = Convert.ToString(monthctc1 * 12);
                    lblBasePay1.Font.Bold = true;
                    lblAmtMnt1.Font.Bold = true;
                    lblAmtAnn1.Font.Bold = true;
                    lblAmtMnt1.ForeColor = System.Drawing.Color.Black;
                    lblAmtAnn1.ForeColor = System.Drawing.Color.Black;
                    divAlltotal.Visible = true;

                    lblAllTotal.Text = "COST TO COMPANY (CTC)";


                    double AllAmtMnt = monthctc + monthctc1;
                    lblAllAmtMnt.Text = string.Format("{0:0.00}", AllAmtMnt);
                    //lblAllAmtMnt.Text = Convert.ToString(monthctc + monthctc1);
                    double anutotal = Convert.ToDouble(lblAmtAnn.Text) + Convert.ToDouble(lblAmtAnn1.Text);
                    lblAllAmtAnn.Text = string.Format("{0:0.00}", anutotal);
                    lblAllTotal.Font.Bold = true;
                    lblAllAmtMnt.Font.Bold = true;
                    lblAllAmtAnn.Font.Bold = true;
                    lblAllAmtMnt.ForeColor = System.Drawing.Color.Black;
                    lblAllAmtAnn.ForeColor = System.Drawing.Color.Black;
                    divNotes.Visible = true;


                }
                else
                {
                    txtAnnCTC.Text = Convert.ToString(totAmt * 12);
                }

                CalculateGross();
                ResetSpecialAllowance();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void FillCTC()
        {
            try
            {
                totAmt = 0;
                dtInv = objInv.GetCTCDetailsAngel((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"], txtCTC, txtBal, txtAnnCTC, lblAmtMnt, lblAmtAnn);
                if (Session["sAdmin"].ToString() == "1")
                {
                    trcheckCtc.Visible = true;
                }
                else
                {
                    trName.Visible = false;
                    trcheckCtc.Visible = false;
                }

                gvCTC.DataSource = dtInv;
                gvCTC.DataBind();
                if (Session["sCompID"].ToString() == "CO000114")
                {
                    double ctc = Convert.ToDouble(txtCTC.Text);

                    double aunctc = ctc * 12;
                    txtAnctc.Text = string.Format("{0:0.00}", aunctc);

                    //txtAnctc.Text = Convert.ToString(ctc * 12);
                    txtAnctc.Visible = true;
                    divctctotal.Visible = true;
                    dtInv1 = objInv.GetCTCDetailsAngel1((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"], txtCTC, txtBal, txtAnnCTC, lblAmtMnt1, lblAmtAnn1);
                    gvCTC1.DataSource = dtInv1;
                    gvCTC1.DataBind();

                    divctctotal.Visible = true;
                    InvDetails1.Visible = true;
                    divctctotal.Visible = true;
                    lblBasePay.Text = "BASE PAY";

                    double monthctc = Convert.ToDouble(lblAmtMnt.Text);
                    Session["monthctc"] = monthctc;

                    double AmtAnn = monthctc * 12;
                    lblAmtAnn.Text = string.Format("{0:0.00}", AmtAnn);

                    //lblAmtAnn.Text = Convert.ToString(monthctc * 12);
                    lblBasePay.Font.Bold = true;
                    lblAmtMnt.Font.Bold = true;
                    lblAmtAnn.Font.Bold = true;
                    lblAmtMnt.ForeColor = System.Drawing.Color.Black;
                    lblAmtAnn.ForeColor = System.Drawing.Color.Black;
                    divctctotal1.Visible = true;

                    lblBasePay1.Text = "TOTAL BENEFITS";
                    double monthctc1 = Convert.ToDouble(lblAmtMnt1.Text);
                    Session["monthctc1"] = monthctc1;

                    double AmtAnn1 = monthctc1 * 12;
                    lblAmtAnn1.Text = string.Format("{0:0.00}", AmtAnn1);

                    //lblAmtAnn1.Text = Convert.ToString(monthctc1 * 12);
                    lblBasePay1.Font.Bold = true;
                    lblAmtMnt1.Font.Bold = true;
                    lblAmtAnn1.Font.Bold = true;
                    lblAmtMnt1.ForeColor = System.Drawing.Color.Black;
                    lblAmtAnn1.ForeColor = System.Drawing.Color.Black;
                    divAlltotal.Visible = true;

                    lblAllTotal.Text = "COST TO COMPANY (CTC)";

                    double AllAmtMnt = monthctc + monthctc1;
                    lblAllAmtMnt.Text = string.Format("{0:0.00}", AllAmtMnt);
                    //lblAllAmtMnt.Text = Convert.ToString(monthctc + monthctc1);
                    double anutotal = Convert.ToDouble(lblAmtAnn.Text) + Convert.ToDouble(lblAmtAnn1.Text);
                    lblAllAmtAnn.Text = string.Format("{0:0.00}", anutotal);



                    //lblAllAmtMnt.Text = Convert.ToString(monthctc + monthctc1);
                    //double anutotal = Convert.ToDouble(lblAmtAnn.Text) + Convert.ToDouble(lblAmtAnn1.Text);
                    //lblAllAmtAnn.Text = Convert.ToString(anutotal);
                    lblAllTotal.Font.Bold = true;
                    lblAllAmtMnt.Font.Bold = true;
                    lblAllAmtAnn.Font.Bold = true;
                    lblAllAmtMnt.ForeColor = System.Drawing.Color.Black;
                    lblAllAmtAnn.ForeColor = System.Drawing.Color.Black;
                    divNotes.Visible = true;

                }
                else
                {
                    txtAnnCTC.Text = Convert.ToString(totAmt * 12);
                }
                txtAnnCTC.Text = Convert.ToString(totAmt * 12);
                CalculateGross();
                ResetSpecialAllowance();


            }
            catch (Exception EX)
            {

                lblMessage.Text = EX.Message;
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dtInv = objInv.GetEMPDetailsAngel((string)Session["sCompID"], txtEmpCode.Text);
                string empCode = dtInv.Rows[0]["EMP_AID"].ToString();
                if (dtInv.Rows[0]["EMP_FNAME"].ToString() != "")
                {
                    string EmpName = dtInv.Rows[0]["EMP_FNAME"].ToString();
                    trName.Visible = true;
                    txtName.Text = EmpName.ToString();
                }
                else
                {
                    trName.Visible = false;
                }

                totAmt = 0;
                dtInv = objInv.GetCTCDetailsAngel((string)Session["sCompID"], empCode, txtEmpCode.Text, txtCTC, txtBal, txtAnnCTC, lblAmtMnt, lblAmtAnn);
                gvCTC.DataSource = dtInv;
                gvCTC.DataBind();

                dtInv1 = objInv.GetCTCDetailsAngel1((string)Session["sCompID"], empCode, txtEmpCode.Text, txtCTC, txtBal, txtAnnCTC, lblAmtMnt1, lblAmtAnn1);
                gvCTC1.DataSource = dtInv1;
                gvCTC1.DataBind();

                if (Session["sCompID"].ToString() == "CO000114")
                {
                    double ctc = Convert.ToDouble(txtCTC.Text);

                    double aunctc = ctc * 12;
                    txtAnctc.Text = string.Format("{0:0.00}", aunctc);

                    // txtAnctc.Text = Convert.ToString(ctc * 12);
                    txtAnctc.Visible = true;

                    divctctotal.Visible = true;
                    InvDetails1.Visible = true;
                    divctctotal.Visible = true;
                    lblBasePay.Text = "BASE PAY";

                    double monthctc = Convert.ToDouble(lblAmtMnt.Text);

                    double AmtAnn = monthctc * 12;
                    lblAmtAnn.Text = string.Format("{0:0.00}", AmtAnn);

                    //lblAmtAnn.Text = Convert.ToString(monthctc * 12);
                    lblBasePay.Font.Bold = true;
                    lblAmtMnt.Font.Bold = true;
                    lblAmtAnn.Font.Bold = true;
                    lblAmtMnt.ForeColor = System.Drawing.Color.Black;
                    lblAmtAnn.ForeColor = System.Drawing.Color.Black;
                    divctctotal1.Visible = true;

                    lblBasePay1.Text = "TOTAL BENEFITS";
                    string strAmtMnt1 = "";
                    if (lblAmtMnt1.Text == "")
                    {
                        strAmtMnt1 = "0";
                    }
                    else
                    {
                        strAmtMnt1 = lblAmtMnt1.Text;
                    }
                    double monthctc1 = Convert.ToDouble(strAmtMnt1);

                    double AmtAnn1 = monthctc1 * 12;
                    lblAmtAnn1.Text = string.Format("{0:0.00}", AmtAnn1);

                    // lblAmtAnn1.Text = Convert.ToString(monthctc1 * 12);
                    lblBasePay1.Font.Bold = true;
                    lblAmtMnt1.Font.Bold = true;
                    lblAmtAnn1.Font.Bold = true;
                    lblAmtMnt1.ForeColor = System.Drawing.Color.Black;
                    lblAmtAnn1.ForeColor = System.Drawing.Color.Black;
                    divAlltotal.Visible = true;

                    lblAllTotal.Text = "COST TO COMPANY (CTC)";

                    double AllAmtMnt = monthctc + monthctc1;
                    lblAllAmtMnt.Text = string.Format("{0:0.00}", AllAmtMnt);
                    //lblAllAmtMnt.Text = Convert.ToString(monthctc + monthctc1);
                    double anutotal = Convert.ToDouble(lblAmtAnn.Text) + Convert.ToDouble(lblAmtAnn1.Text);
                    lblAllAmtAnn.Text = string.Format("{0:0.00}", anutotal);

                    //lblAllAmtMnt.Text = Convert.ToString(monthctc + monthctc1);
                    //double anutotal = Convert.ToDouble(lblAmtAnn.Text) + Convert.ToDouble(lblAmtAnn1.Text);
                    //lblAllAmtAnn.Text = Convert.ToString(anutotal);
                    lblAllTotal.Font.Bold = true;
                    lblAllAmtMnt.Font.Bold = true;
                    lblAllAmtAnn.Font.Bold = true;
                    lblAllAmtMnt.ForeColor = System.Drawing.Color.Black;
                    lblAllAmtAnn.ForeColor = System.Drawing.Color.Black;
                    divNotes.Visible = true;
                }
                else
                {
                    txtAnnCTC.Text = Convert.ToString(totAmt * 12);
                }
                txtAnnCTC.Text = Convert.ToString(totAmt * 12);
                CalculateGross();
                //ResetSpecialAllowance();


                //txtEmpCode.Text;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow gvr in gvCTC.Rows)
                {

                    if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true && ((Label)gvr.FindControl("lbldetId")).Text == "AD000212")
                    {
                        if (((TextBox)gvr.FindControl("txtPRAN")).Text.Trim() == "")
                        {
                            lblMessage.Text = "Enter PRAN.";
                            objcommon.Display("Validate", "DisplayErrorMessage('Enter PRAN.');");
                            return;
                        }
                    }
                }

                if (objInv.UpdateFlexipayAngel(MakeCTCXml(gvCTC, ""), (string)Session["sCompID"], (string)Session["sEmpID"]) == false)
                {
                    lblMessage.Text = "Error occurred in application.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating Flexi Pay details.');");
                    return;
                }
                FillCTC();
                lblMessage.Text = "Successfuly updated flexi pay details.";
                objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated flexi pay details.');");

                Session["AnnCTC"] = txtAnnCTC.Text;
                Session["MonCTC"] = txtMonCTC.Text;
                Session["Gross"] = txtGross.Text;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating Flexi Pay details.');");
            }

        }

        private string MakeCTCXml(GridView GV, string strType)
        {

            StringBuilder sbTaxDetails = new StringBuilder();
            string xmlInv = string.Empty;

            sbTaxDetails.Append("<ROOT>");

            foreach (GridViewRow gvr in GV.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true && ((CheckBox)gvr.FindControl("chkSelect")).Visible == true)
                {
                    sbTaxDetails.Append("<Flexi COMP_AID='" + (string)Session["sCompID"] + "'");
                    sbTaxDetails.Append(" EMP_AID='" + (string)Session["sEmpID"] + "'");
                    sbTaxDetails.Append(" ALLWDED_AID='" + ((Label)gvr.FindControl("lbldetId")).Text.Trim() + "'");
                    sbTaxDetails.Append(" Fix_Amount='" + ((Label)gvr.FindControl("lblLimit")).Text.Trim() + "'");
                    sbTaxDetails.Append(" PRAN='" + ((TextBox)gvr.FindControl("txtPRAN")).Text.Trim() + "'");
                    sbTaxDetails.Append(" Amount='" + ((TextBox)gvr.FindControl("txtAmount")).Text.Trim() + "'");
                    sbTaxDetails.Append(" PCNT='" + ((Label)gvr.FindControl("lblpct")).Text.Trim() + "'/>");
                }
            }

            sbTaxDetails.Append("</ROOT>");

            xmlInv = sbTaxDetails.ToString();

            return xmlInv;


        }

        protected string ReplaceSpecialCharacters(string inputString)
        {
            inputString = inputString.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&#39;").
                    Replace("\"", "&quot;");

            return inputString;
        }

        protected void gvCTC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //try
            //{
            if (Session["sCompID"].ToString() == "CO000114")
            {
                // tdtitle.InnerText = "Cost to Company";
                this.gvCTC.Columns[0].Visible = false;
                this.gvCTC.Columns[8].Visible = false;
                //trAgree.Visible = false;
                // btnsubmit.Visible = false;
                //////trName.Visible = true;

                //////trcheckCtc.Visible = true;
                this.gvCTC.Columns[1].ItemStyle.Width = 700;
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblDesc = (Label)e.Row.FindControl("lblDesc");

                    if (lblDesc.Text == "BASIC")
                    {
                        lblDesc.Font.Bold = true;
                    }
                    if (lblDesc.Text == "HRA")
                    {
                        lblDesc.Font.Bold = true;
                    }
                    if (lblDesc.Text == "BONUS")
                    {
                        lblDesc.Font.Bold = true;
                    }
                    if (lblDesc.Text == "SPECIAL ALLOWANCE")
                    {
                        lblDesc.Font.Bold = true;
                    }

                    TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                    if (txtAmount.Text == "0.00")
                    {
                        e.Row.Visible = false;
                    }
                    TextBox txtAmountYearly = (TextBox)e.Row.FindControl("txtAmountYearly");
                    if (txtAmountYearly.Text == "0.00")
                    {
                        e.Row.Visible = false;
                    }
                }

            }
            else
            {
                trAgree.Visible = true;
                btnsubmit.Visible = true;
                trName.Visible = false;

                trcheckCtc.Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double ctc = Convert.ToDouble(txtCTC.Text);
                Label lblDesc = (Label)e.Row.FindControl("lblDesc");
                Label lbldetId = (Label)e.Row.FindControl("lbldetId");
                Label lblGroup = (Label)e.Row.FindControl("lblGroup");
                Label lblSel = (Label)e.Row.FindControl("lblSel");
                Label lblLimit = (Label)e.Row.FindControl("lblLimit");
                Label lbledit = (Label)e.Row.FindControl("lbledit");

                Label lblshow = (Label)e.Row.FindControl("lblshow");
                Label lblman = (Label)e.Row.FindControl("lblman");

                TextBox txtPRAN = (TextBox)e.Row.FindControl("txtPRAN");
                Label lblPRAN = (Label)e.Row.FindControl("lblPRAN");
                TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                TextBox txtAmountYearly = (TextBox)e.Row.FindControl("txtAmountYearly");
                TextBox txtpcnt = (TextBox)e.Row.FindControl("txtpcnt");

                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");

                //if (lbldetId.Text == "AD000001")
                //{
                //    basicamt = Convert.ToDouble(txtAmount.Text);
                //}

                //if (lblGroup.Text.Trim() == "C")
                //{
                //    if (lbldetId.Text == "AD000001")
                //    {
                //        double pct = Convert.ToDouble(txtpcnt.Text);
                //        basic = (ctc * pct) / 100;
                //        txtAmount.Text = basic.ToString("0.00");

                //    }

                //    if (lbldetId.Text == "AD000002")
                //    {
                //        double pct = Convert.ToDouble(txtpcnt.Text);
                //        txtAmount.Text = ((basic * pct) / 100).ToString("0.00");
                //    }
                //}

                txtpcnt.Visible = false;
                chkSelect.Visible = (lbldetId.Text.Trim() == "00000000" ? false : true);
                txtAmount.Visible = (lbldetId.Text.Trim() == "00000000" ? false : true);

                //if (lblGroup.Text.Trim() != "C" )
                //{
                //    txtAmount.ReadOnly = false;
                //}
                //else
                //{
                txtAmount.ReadOnly = true;
                txtpcnt.ReadOnly = true;
                //}

                //lblDesc.Font.Bold = (lbldetId.Text.Trim() == "00000000" || lblGroup.Text.Trim() == "B" ? true : false);
                chkSelect.Enabled = (lblGroup.Text.Trim() == "C" || lblGroup.Text.Trim() == "B" || lblGroup.Text.Trim() == "OG" ? false : true);
                chkSelect.Checked = (lblGroup.Text.Trim() == "C" || lblGroup.Text.Trim() == "B" || lblGroup.Text.Trim() == "OG" ? true : false);

                //lblLimit.Text = (Convert.ToDouble(txtAmount.Text) == 0 ? "" : txtAmount.Text);   
                if (lblSel.Text.Trim() == "1")
                {
                    chkSelect.Checked = true;
                }

                if (chkSelect.Checked == true && lblGroup.Text.Trim() != "C" && lblGroup.Text.Trim() != "OG")
                {
                    txtAmount.ReadOnly = true;
                    txtpcnt.ReadOnly = false;
                }

                if (lblGroup.Text.Trim() != "C" && lblGroup.Text.Trim() != "B" && lblGroup.Text.Trim() != "OG")
                {
                    if (chkSelect.Checked == false)
                    {
                        txtAmount.Text = "0";
                        txtpcnt.Text = "0";
                    }

                    if (lbledit.Text.Trim() == "1")
                    {
                        txtpcnt.Visible = true;
                        txtpcnt.Text = (chkSelect.Checked == true ? txtpcnt.Text : "0");
                    }
                    else
                    {
                        txtpcnt.Visible = false;
                    }
                }

                //double amount = 0;
                //double totamount = 0;
                //for (int i = 0; i <= this.gvCTC.Rows.Count - 1; i++)
                //{
                //    GridViewRow gvr = gvCTC.Rows[i];

                //    if (((Label)gvr.FindControl("lblgross")).Text == "1")
                //    {
                //        amount = amount + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
                //    }
                //    totamount = totamount + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
                //}

                if (lbldetId.Text == "AD000025")
                {
                    //if (Math.Round(amount) < 21000)
                    //{
                    //    double esi = 0;
                    //    esi = Math.Round((4.75 * amount) / 100);
                    //    txtAmount.Text = esi.ToString("0.00");
                    //}
                    //else
                    //{
                    //    txtAmount.Text = "0.00";
                    //}

                    chkSelect.Enabled = false;
                    chkSelect.Checked = true;
                    txtAmount.ReadOnly = true;
                }

                if (lbldetId.Text == "AD000156" || lbldetId.Text == "AD000083")
                {
                    txtAmount.Text = lblLimit.Text;

                    chkSelect.Enabled = false;
                    chkSelect.Checked = true;
                    txtAmount.ReadOnly = true;
                }

                if (lbldetId.Text == "AD000219")
                {
                    chkSelect.Enabled = false;
                }

                if (lbldetId.Text == "AD000212")
                {
                    if (chkSelect.Checked == true)
                    {
                        txtPRAN.Visible = true;
                        txtPRAN.Text = lblPRAN.Text;
                    }
                    else
                    {
                        txtPRAN.Visible = false;
                    }
                }

                //if (lbldetId.Text == "AD000046")
                //{
                //    if (Math.Round(basicamt) < 21000)
                //    {
                //        txtAmount.Text = "750.00";
                //    }
                //    else
                //    {
                //        txtAmount.Text = "0.00";
                //    }

                //    chkSelect.Enabled = false;
                //    chkSelect.Checked = true;
                //    txtAmount.ReadOnly = true;
                //}
            }
            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message;
            //}
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkSelect = (CheckBox)sender;
                Label lblDesc = (Label)chkSelect.NamingContainer.FindControl("lblDesc");
                TextBox txtAmount = (TextBox)chkSelect.NamingContainer.FindControl("txtAmount");
                TextBox txtPRAN = (TextBox)chkSelect.NamingContainer.FindControl("txtPRAN");
                TextBox txtpcnt = (TextBox)chkSelect.NamingContainer.FindControl("txtpcnt");
                Label lbldetId = (Label)chkSelect.NamingContainer.FindControl("lbldetId");
                Label lblLimit = (Label)chkSelect.NamingContainer.FindControl("lblLimit");
                Label lblpct = (Label)chkSelect.NamingContainer.FindControl("lblpct");

                if (chkSelect.Checked == true)
                {
                    if (lbldetId.Text == "AD000219")
                    {
                        double amount = 0;
                        double basicamt = 0;
                        for (int i = 0; i <= this.gvCTC.Rows.Count - 1; i++)
                        {
                            GridViewRow gvr = gvCTC.Rows[i];

                            if (((Label)gvr.FindControl("lbldetId")).Text == "AD000001")
                            {
                                amount = (Convert.ToDouble(txtCTC.Text) * 38.17) / 100;
                                ((TextBox)gvr.FindControl("txtAmount")).Text = amount.ToString("0.00");
                                ((Label)gvr.FindControl("lblpct")).Text = "38.17";
                                basicamt = amount;
                            }

                            if (((Label)gvr.FindControl("lbldetId")).Text == "AD000002")
                            {
                                double pct = Convert.ToDouble(((Label)gvr.FindControl("lblpct")).Text);
                                ((TextBox)gvr.FindControl("txtAmount")).Text = ((amount * pct) / 100).ToString("0.00");
                            }
                        }

                        txtAmount.Text = ((basicamt * Convert.ToDouble(lblpct.Text)) / 100).ToString("0.00");
                    }
                    else if (lbldetId.Text == "AD000212")
                    {
                        double amount = 0;
                        double basicamt = 0;
                        for (int i = 0; i <= this.gvCTC.Rows.Count - 1; i++)
                        {
                            GridViewRow gvr = gvCTC.Rows[i];

                            if (((Label)gvr.FindControl("lbldetId")).Text == "AD000001")
                            {
                                basicamt = Convert.ToDouble(((TextBox)gvr.FindControl("txtAmount")).Text);
                            }
                        }

                        txtAmount.Text = ((basicamt * Convert.ToDouble(lblpct.Text)) / 100).ToString("0.00");
                        txtPRAN.Visible = true;
                    }
                    else if (lblLimit.Text == "" || lblLimit.Text == "0" || lblLimit.Text == "0.00")
                    {
                        chkSelect.Checked = false;
                        lblMessage.Text = objcommon.EncodeJsString("Not allowed for your grade.");
                        objcommon.Display("Validate", "DisplayErrorMessage('Not allowed for your grade.');");
                    }
                    else
                    {
                        string flexAmt = objInv.GetFlexiAmountFromGrade((string)Session["sCompID"], (string)Session["sEmpID"], lbldetId.Text).Replace(".0000", ".00");
                        txtAmount.Text = flexAmt;
                        lblLimit.Text = flexAmt;
                    }
                }
                else
                {
                    if (lbldetId.Text == "AD000219")
                    {
                        double amount = 0;
                        for (int i = 0; i <= this.gvCTC.Rows.Count - 1; i++)
                        {
                            GridViewRow gvr = gvCTC.Rows[i];

                            if (((Label)gvr.FindControl("lbldetId")).Text == "AD000001")
                            {
                                amount = (Convert.ToDouble(txtCTC.Text) * 40) / 100;
                                ((TextBox)gvr.FindControl("txtAmount")).Text = amount.ToString("0.00");
                                ((Label)gvr.FindControl("lblpct")).Text = "40";
                            }

                            if (((Label)gvr.FindControl("lbldetId")).Text == "AD000002")
                            {
                                double pct = Convert.ToDouble(((Label)gvr.FindControl("lblpct")).Text);
                                ((TextBox)gvr.FindControl("txtAmount")).Text = ((amount * pct) / 100).ToString("0.00");
                            }
                        }
                    }

                    txtPRAN.Visible = false;
                    txtPRAN.Text = "";
                    txtAmount.ReadOnly = true;
                    txtpcnt.ReadOnly = true;
                    txtAmount.Text = "0";
                    txtpcnt.Text = "0";

                    txtAmount.Text = "0.00";
                }

                if (ResetSpecialAllowance() == false)
                {
                    chkSelect.Checked = false;
                    txtAmount.ReadOnly = true;
                    txtpcnt.ReadOnly = true;
                    txtAmount.Text = "";
                }

                CalculateGross();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private bool ValidateAlternate()
        {
            string strMsg = "";
            int sel = 0;
            int man = 0;
            string DDLmsg = "";
            for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
            {
                GridViewRow gvr = gvCTC.Rows[i];

                if (((Label)gvr.FindControl("lblGroup")).Text != "OG" && ((Label)gvr.FindControl("lblGroup")).Text != "OA")
                {


                    if (((Label)gvr.FindControl("lblGroup")).Text == "A")
                    {
                        if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                        {
                            sel = sel + 1;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
            {
                GridViewRow gvr = gvCTC.Rows[i];
                if (((Label)gvr.FindControl("lbldetId")).Text == "AD000079")
                {
                    if (((CheckBox)gvr.FindControl("chkSelect")).Checked)
                    {
                        if (((DropDownList)gvr.FindControl("drpCardType")).SelectedItem.Text == "[Select]")
                        {
                            DDLmsg = "Select option for Food Card/Gift Card.";
                        }
                    }
                }

                if (((Label)gvr.FindControl("lbldetId")).Text == "AD000130")
                {
                    if (((CheckBox)gvr.FindControl("chkSelect")).Checked)
                    {
                        if (((DropDownList)gvr.FindControl("drpGiftCard")).SelectedItem.Text == "[Select]")
                        {
                            DDLmsg = "Select option for Food Card/Gift Card.";
                        }
                    }
                }
            }

            for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
            {
                GridViewRow gvr = gvCTC.Rows[i];

                if (((Label)gvr.FindControl("lblGroup")).Text != "OG" && ((Label)gvr.FindControl("lblGroup")).Text != "OA")
                {
                    if (((Label)gvr.FindControl("lblshow")).Text == "1" && ((Label)gvr.FindControl("lblman")).Text == "1")
                    {
                        if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                        {
                            if (((TextBox)gvr.FindControl("txtAddr")).Text.Trim() == "" || ((TextBox)gvr.FindControl("txtPin")).Text.Trim() == "")
                            {
                                man = 1;
                                break;
                            }
                        }
                    }
                }
            }
            //if (sel == 0)
            //{
            //    strMsg = "Please select at least one allowance in Annexure C.";
            //    lblMessage.Text =strMsg ;
            //    objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(strMsg) + "');");
            //    return false;
            //}
            if (man == 1)
            {
                strMsg = "Please enter both address and pin code.";
                lblMessage.Text = strMsg;
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(strMsg) + "');");
                return false;
            }
            else if (DDLmsg != "")
            {
                strMsg = DDLmsg;
                lblMessage.Text = strMsg;
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(strMsg) + "');");
                return false;
            }
            else
            {
                return true;
            }
        }

        private string Validate_Alternate(string allId)
        {
            string strMsg = string.Empty;
            Boolean isFound = false;
            try
            {
                dsInv = objInv.Validate_Alternate((string)Session["sCompID"], (string)Session["sEmpID"], allId);

                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            if (Convert.ToString(dsInv.Tables[0].Rows[IntRow]["ALLWDED_AID"].ToString().Trim()) == allId)
                            {
                                isFound = true;
                            }

                            //for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
                            //{
                            //    GridViewRow gvr = gvCTC.Rows[i];

                            //    if (((Label)gvr.FindControl("lblGroup")).Text == "A" && ((Label)gvr.FindControl("lblGroup")).Text != allId)
                            //    {
                            //        if (((Label)gvr.FindControl("lbldetId")).Text == Convert.ToString(dsInv.Tables[0].Rows[IntRow]["ALLWDED_AID"].ToString().Trim()) && ((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                            //        {
                            //            strMsg = "Can not select this allowance when " + ((Label)gvr.FindControl("lblDesc")).Text + " is selected.";
                            //            objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(strMsg) + "');");
                            //            return "Invalid";
                            //        }
                            //    }
                            //}
                        }
                    }

                    if (isFound == false)
                    {
                        strMsg = "Can not select this allowance for your grade.";
                        lblMessage.Text = strMsg;
                        objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(strMsg) + "');");
                        return "Invalid";
                    }

                    if (dsInv.Tables[1].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[1].Rows.Count - 1; IntRow++)
                        {
                            for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
                            {
                                GridViewRow gvr = gvCTC.Rows[i];

                                if (((Label)gvr.FindControl("lblGroup")).Text != "OG" && ((Label)gvr.FindControl("lblGroup")).Text != "OA")
                                {
                                    if (((Label)gvr.FindControl("lblGroup")).Text == "A" && ((Label)gvr.FindControl("lblGroup")).Text != allId)
                                    {
                                        if (((Label)gvr.FindControl("lbldetId")).Text == Convert.ToString(dsInv.Tables[1].Rows[IntRow]["NOT_ALLWDED_AID"].ToString().Trim()) && ((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                                        {
                                            strMsg = "Can not select this allowance when " + ((Label)gvr.FindControl("lblDesc")).Text + " is selected.";
                                            lblMessage.Text = strMsg;
                                            objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(strMsg) + "');");
                                            return "Invalid";
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
            }
            return strMsg;
        }

        private double CalculateAmount(string headAid, double pct)
        {
            double amount = 0;
            for (int i = 0; i <= this.gvCTC.Rows.Count - 1; i++)
            {
                GridViewRow gvr = gvCTC.Rows[i];

                if (headAid == ((Label)gvr.FindControl("lbldetId")).Text)
                {
                    amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));

                    amount = Math.Round((amount * pct) / 100, 0);

                    break;
                }
            }

            return amount;
        }

        private void CalculateGross()
        {
            double amount = 0;
            double totamount = 0;
            for (int i = 0; i <= this.gvCTC.Rows.Count - 1; i++)
            {
                GridViewRow gvr = gvCTC.Rows[i];

                if (((Label)gvr.FindControl("lblgross")).Text == "1")
                {
                    amount = amount + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
                }
                totamount = totamount + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
                ((TextBox)gvr.FindControl("txtAmountYearly")).Text = Math.Round((Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text)) * 12)).ToString("0.00");
                ((TextBox)gvr.FindControl("txtAmount")).Text = ((TextBox)gvr.FindControl("txtAmount")).Text == "0" ? "0.00" : ((TextBox)gvr.FindControl("txtAmount")).Text;
            }

            amount = Math.Round(amount * 12);
            txtGross.Text = Convert.ToString(amount);
            //txtAnnCTC.Text = Convert.ToString(totamount); 
        }

        private Boolean ResetSpecialAllowance()
        {
            if (Session["sCompID"].ToString() == "CO000114")
            {
                double ctc = Convert.ToDouble(txtCTC.Text);
                double sumCTC = 0;
                double spallowance = 0;
                TextBox txtSP = new TextBox();
                TextBox txtSPYearly = new TextBox();

                //for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
                //{
                //    GridViewRow gvr = gvCTC.Rows[i];

                //    if (((Label)gvr.FindControl("lblGroup")).Text != "B")
                //    {
                //        if ((((CheckBox)gvr.FindControl("chkSelect")).Checked == true))
                //        {
                //            sumCTC = sumCTC + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));

                //        }
                //    }
                //    else
                //    {
                //        txtSP = (TextBox)gvr.FindControl("txtAmount");
                //        txtSPYearly = (TextBox)gvr.FindControl("txtAmountYearly");
                //    }
                //}

                spallowance = ((ctc) - Convert.ToDouble(Session["monthctc1"]) - Convert.ToDouble(Session["monthctc1"]));

                if (spallowance < 0)
                {
                    lblMessage.Text = "Special Allowance can not be less than zero.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Special Allowance can not be less than zero.');");
                    return false;
                }


                txtSP.Text = spallowance.ToString("0.00");
                txtSPYearly.Text = (spallowance * 12).ToString("0.00");
            }
            else
            {
                double ctc = Convert.ToDouble(txtCTC.Text);
                double sumCTC = 0;
                double spallowance = 0;
                TextBox txtSP = new TextBox();
                TextBox txtSPYearly = new TextBox();

                for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
                {
                    GridViewRow gvr = gvCTC.Rows[i];

                    if (((Label)gvr.FindControl("lblGroup")).Text != "B")
                    {
                        if ((((CheckBox)gvr.FindControl("chkSelect")).Checked == true))
                        {
                            sumCTC = sumCTC + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));

                        }
                    }
                    else
                    {
                        txtSP = (TextBox)gvr.FindControl("txtAmount");
                        txtSPYearly = (TextBox)gvr.FindControl("txtAmountYearly");
                    }
                }

                spallowance = ctc - sumCTC;

                if (spallowance < 0)
                {
                    lblMessage.Text = "Special Allowance can not be less than zero.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Special Allowance can not be less than zero.');");
                    return false;
                }

                txtSP.Text = spallowance.ToString("0.00");
                txtSPYearly.Text = (spallowance * 12).ToString("0.00");
            }
            return true;
        }

        protected void txtpcnt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double Limit = 0;
                double Amount = 0;

                TextBox txtpcnt = (TextBox)sender;
                Label lblpct = (Label)txtpcnt.NamingContainer.FindControl("lblpct");
                Label lblhead = (Label)txtpcnt.NamingContainer.FindControl("lblhead");
                TextBox txtAmount = (TextBox)txtpcnt.NamingContainer.FindControl("txtAmount");

                Limit = Convert.ToDouble((lblpct.Text.Trim() == "" ? "0" : lblpct.Text.Trim()));
                Amount = Convert.ToDouble((txtpcnt.Text.Trim() == "" ? "0" : txtpcnt.Text.Trim()));

                if (Limit != 0 && Amount > Limit)
                {
                    lblMessage.Text = objcommon.EncodeJsString(((Label)txtpcnt.NamingContainer.FindControl("lblDesc")).Text) + " % can not be greater than limit.";
                    objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)txtpcnt.NamingContainer.FindControl("lblDesc")).Text) + " % can not be greater than limit.');");
                    txtpcnt.Text = "0";
                    txtAmount.Text = "0";
                    return;
                }

                txtAmount.Text = Convert.ToString(CalculateAmount(lblhead.Text.Trim(), Amount));

                ResetSpecialAllowance();
                CalculateGross();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double Limit = 0;
                double Amount = 0;

                TextBox txtAmount = (TextBox)sender;
                Label lblLimit = (Label)txtAmount.NamingContainer.FindControl("lblLimit");
                Label lblDesc = (Label)txtAmount.NamingContainer.FindControl("lblDesc");
                Label lbldetId = (Label)txtAmount.NamingContainer.FindControl("lbldetId");
                DropDownList drpIsCar = (DropDownList)txtAmount.NamingContainer.FindControl("drpIsCar");

                Limit = Convert.ToDouble((lblLimit.Text.Trim() == "" ? "0" : lblLimit.Text.Trim()));
                Amount = Convert.ToDouble((txtAmount.Text.Trim() == "" ? "0" : txtAmount.Text.Trim()));

                ResetSpecialAllowance();
                CalculateGross();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnFlexiPrint_Click(object sender, EventArgs e)
        {
            Session["AnnCTC"] = txtAnnCTC.Text;
            Session["MonCTC"] = txtMonCTC.Text;
            Session["Gross"] = txtGross.Text;

            Response.Redirect("FlexiPrint.aspx", false);
        }
    }
}