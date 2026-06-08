using Microsoft.Reporting.WebForms;
//using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace NewPortal2023.ESS
{
    public partial class LeaveCard : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveStructure objLs = new NewPortal2023.ESS.LeaveStructure();
        NewPortal2023.ESS.LeaveCards objInv = new NewPortal2023.ESS.LeaveCards();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsInv = new DataSet();
        string OldYear, NewYear;
        NewPortal2023.ESS.FlexiPay objFlexi = new NewPortal2023.ESS.FlexiPay();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            
             if (Session["sCompID"]!=null)
            {
            divAlert.Visible = false;
            lblMessage.Text = "";

            if (!Page.IsPostBack)
            {
                if (Session["sCompID"] != null)
                {


                    FillDetails();
                    FillLeaveCardsCurrentDate();
                }
                else
                {
                    Response.Redirect("Logout.aspx");
                }
                mv.SetActiveView(vwList);
                SetRights();
               // Fill_Details("1", gvLeaveCards.PageSize.ToString());
            }
        }
             else
             {
                 Response.Redirect("Login.aspx");
             }

        }
        private void FillLeaveCardsCurrentDate()
        {
            try
            {
               // OtherDetails.Visible = true;
                dsInv = objInv.GetLeaveCurrentDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
                //dsInv = objInv.GetAttendance();
                gvLeaveCards.DataSource = dsInv;
                gvLeaveCards.DataBind();
                mv.SetActiveView(vwList);
                SetRights();

            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application'" + ex + "'.";
            }
        }

        private void FillDetails()
        {

            if (Session["sCompID"] != null)
            {
                try
                {
                    objInv.GetFinancialYearList((string)Session["sCompID"], (string)Session["sEmpID"], "", drpSelectFinancialYear);
                    objInv.GetMonthList((string)Session["sCompID"], (string)Session["sEmpID"], "", drpSelectMonth);
                    if (Session["NewYear"] != null)
                    {
                        drpSelectFinancialYear.SelectedValue = Session["NewYear"].ToString();
                    }
                    if (Session["NewMonth"] != null)
                    {
                        drpSelectMonth.SelectedValue = Session["NewMonth"].ToString();
                    }
                    ds = objLs.GetRoleDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
                    if (ds.Tables[0].Rows[0]["Counts"].ToString() == "0")
                    {
                        //OtherDetails.Visible = true;
                        //trHr.Visible = false;
                        FillLeave();
                    }
                    else
                    {

                        //OtherDetails.Visible = false;
                        //trHr.Visible = true;
                    }

                    //SENDEMAIL();
                    string strResult = objcommon.Validate_ControlInfo("INV");

                    if (strResult.Contains("This page is currently unavailable.") == true)
                    {
                        Response.Redirect("Unavailable.aspx?strName=Investment Details");
                        return;
                    }
                    //string finYear = objInv.GetFinalcialYear();
                    //string[] years = finYear.Split('.');
                    //OldYear = years[1];
                    //NewYear = years[0];
                    //lnkLeave.Text = "Leave Card";
                    //lnkLeave.Text = "Leave Card for the year " + years[0];
                    // InsertNewRecord();


                    mv.SetActiveView(vwList);
                    SetRights();

                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error occurred in application'" + ex + "'.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
                }
            }
        }

        private void FillLeave()
        {
            try
            {
                //if (Session["NewYear"].ToString() != "" || Session["NewYear"] != null && )
                //{
                NewYear = drpSelectFinancialYear.SelectedValue;
                string NewMonth = drpSelectMonth.SelectedValue;
                dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, NewMonth);
                this.gvLeaveCards.DataSource = dsInv;
                this.gvLeaveCards.DataBind();
                mv.SetActiveView(vwList);
                SetRights();
                //}
            }
            catch (Exception ex)
            {
                lblMessage.Text = "";
                gvLeaveCards.Visible = false;
            }
        }

        protected void drpSelectFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpSelectFinancialYear.SelectedItem.ToString() != "")
                {
                    //string id = "";
                    //DataSet ds = objFlexi.GetReimbDataAllTelClaim((string)Session["sCompID"], (string)Session["sEmpID"], "AD000207", id);
                    string drpFinYears = drpSelectFinancialYear.SelectedItem.ToString();
                    Session["NewYear"] = drpFinYears;
                    FillLeave();


                }
                else
                {
                    lblMessage.Text = "FinancialYear Data Not Found At.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(ex.Message) + "');");
            }
        }

        protected void drpSelectMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpSelectMonth.SelectedItem.ToString() != "")
                {
                    //string id = "";
                    //DataSet ds = objFlexi.GetReimbDataAllTelClaim((string)Session["sCompID"], (string)Session["sEmpID"], "AD000207", id);
                    string drpSelectMon = drpSelectMonth.SelectedItem.ToString();
                    Session["NewYear"] = drpSelectMon;
                    FillLeave();


                }
                else
                {
                    lblMessage.Text = "FinancialYear Data Not Found At.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(ex.Message) + "');");
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                NewYear = drpSelectFinancialYear.SelectedValue;
                string NewMonth = drpSelectMonth.SelectedValue;
                dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, NewMonth);
                //dsInv = objInv.GetAttendance();
                this.gvLeaveCards.DataSource = dsInv;
                this.gvLeaveCards.DataBind();
                //OtherDetails.Visible = true;
                mv.SetActiveView(vwList);
                SetRights();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the 
            //specified ASP.NET server control at run time. 
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBackControl(this.btnExport);
            if (gvLeaveCards != null)
            {
                if (gvLeaveCards.Rows.Count > 0)
                {
                    NewYear = drpSelectFinancialYear.SelectedValue;
                    string NewMonth = drpSelectMonth.SelectedValue;
                    dsInv = objInv.GetLeaveCardReport((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, NewMonth);
                    //string extension;
                    //string encoding;
                    //string contentType;
                    //string[] streamIds;
                    //Warning[] warnings;


                    ReportDataSource dataSource = new ReportDataSource("dsLeaveCard_Details", dsInv.Tables[0]);

                    rptPrint.LocalReport.DataSources.Clear();

                    rptPrint.LocalReport.ReportPath = @"ESS/LeaveCardRport.rdlc";
                    rptPrint.LocalReport.DataSources.Add(dataSource);

                    rptPrint.LocalReport.Refresh();

                   // byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                    //string FileName = "LeaveCard.xls";

                    //Response.Clear();
                    //Response.Buffer = true;
                    //Response.Charset = "";
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //Response.ContentType = ContentType;
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
                    ////Response.BinaryWrite(bytes);
                    //Response.Flush();
                    //Response.End();

                    mv.SetActiveView(vwList);
                    SetRights();
                }
                else
                {
                    lblMessage.Text = "Search first.";
                }
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
                    cr = dr[0][3].ToString();
                    vw = dr[0][4].ToString();
                    up = dr[0][5].ToString();
                    de = dr[0][6].ToString();

                    ViewState["cr"] = cr;
                    ViewState["vw"] = vw;
                    ViewState["up"] = up;
                    ViewState["de"] = de;
                }
            }
        }

        private void Fill_Details(string index, string size)
        {
            //SBM.Approval objApprove = new SBM.Approval();
            //DataSet ds = new DataSet();
            }

        }
}