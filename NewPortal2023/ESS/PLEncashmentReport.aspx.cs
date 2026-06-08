using Microsoft.Reporting.WebForms;
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
    public partial class PLEncashmentReport : System.Web.UI.Page
    {
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        NewPortal2023.ESS.LeaveCards objInv = new NewPortal2023.ESS.LeaveCards();
        NewPortal2023.ESS.LeaveStructure objLs = new NewPortal2023.ESS.LeaveStructure();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsInv = new DataSet();
        string OldYear, NewYear;
        NewPortal2023.ESS.FlexiPay objFlexi = new NewPortal2023.ESS.FlexiPay();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
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
            }

        }
        private void FillLeaveCardsCurrentDate()
        {
            try
            {
                dsInv = objInv.GetLeaveCurrentDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
                //dsInv = objInv.GetAttendance();
                gvLeaveCards.DataSource = dsInv;
                gvLeaveCards.DataBind();
                OtherDetails.Visible = true;
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
                        OtherDetails.Visible = true;
                        trHr.Visible = false;
                        //FillLeave();
                    }
                    else
                    {

                        OtherDetails.Visible = false;
                        trHr.Visible = true;
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
                    //lnkLeave.Text = "PL Encashment Report";
                    //lnkLeave.Text = "Leave Card for the year " + years[0];
                    // InsertNewRecord();




                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error occurred in application'" + ex + "'.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
                }
            }
        }

        //private void SENDEMAIL()
        //{
        //    emailSend = new ESS.Email();
        //    DataSet dsMail = emailSend.GetEmpDetails((string)Session["sCompID"], (string)Session["sEmpID"], "AD001004");
        //    if (dsMail.Tables[0].Rows[0]["CLIENTEMAIL"].ToString() != "")
        //    {

        //        if (dsMail.Tables[2].Rows[0]["CHECKERMAIL"].ToString() != "")
        //        {
        //            string clientbodys = "Dear " + dsMail.Tables[0].Rows[0]["EMP_FNAME"].ToString() +"\n\nYour Business Travel Claim Send Successful.\n"
        //           + "Please wait for Approvel. We will Notify On Mail or Can You Check Hrrl Portal Site.\nThankYou.\n\nWith Best Regards,\nPayrollservices";
        //            string emails = dsMail.Tables[0].Rows[0]["CLIENTEMAIL"].ToString();
        //            string subjects = "For Business Travel Claim";
        //            string checkerbodys = "Dear Sir/Madam,\n\nPlease Check than approve my Business Travel Claim.\n"
        //            + "My Business Claim  Application ID is-:" + dsMail.Tables[1].Rows[0]["TRAVEL_APP_NO"].ToString() + "\n"
        //                +"\nThankYou.\n\nWith Best Regards,\n" + dsMail.Tables[0].Rows[0]["EMP_FNAME"].ToString() + "";

        //            //emailSend.SendEmailBT(emails, subjects, clientbodys);
        //            for (int i = 0; Convert.ToBoolean(dsMail.Tables[2].Rows.Count); i++)
        //            {
        //                if (dsMail.Tables[2].Rows.Count != i)
        //                {
        //                    // emailSend.SendEmailBT(emails, subjects, checkerbodys);

        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }
        //            //emailSend.SendEmail(string[]  
        //        }
        //    }

        //    //else
        //    //{
        //    //    lblMessage.Text = "Please Provide Us Your Mail Id To Notify The Company";
        //    //}

        //}

        protected void btnLeaveDetails_Click(object sender, EventArgs e)
        {
            try
            {
                FillDetails();
                Session["EMPAID"] = Session["sEmpID"];
                Session["sEmpID"] = txtEmpCode.Text;

                FillLeave();
                Session["sEmpID"] = Session["EMPAID"];
            }
            catch (Exception ex)
            {
                lblMessage.Text = "";
            }
        }

        private void InsertNewRecord()
        {
            try
            {
                dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, OldYear);
                if (dsInv.Tables[1].Rows.Count >= 0)
                {

                    DataSet dsInsert = new DataSet();
                    for (int i = 0; i < dsInv.Tables[1].Rows.Count; i++)
                    {
                        string EMP_AID = dsInv.Tables[1].Rows[i]["EMP_AID"].ToString();
                        string datetime = dsInv.Tables[1].Rows[i]["JOIN_DATE"].ToString();
                        string[] dateString = datetime.Split(' ');
                        string JoinDT = dateString[0];
                        var temp = DateTime.ParseExact(JoinDT, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        var JoinDate = temp.ToString("yyyy-MM-dd");
                        //string Leave_CId = "";
                        string Leave_CId = "1";
                        //string Leave_CId = dsInv.Tables[1].Rows[i]["LEAVE_CID"].ToString();
                        // dsInsert = objInv.InsertLeaveDetails((string)Session["sCompID"], EMP_AID, NewYear, JoinDate, Leave_CId);
                        dsInsert = objInv.InsertLeave((string)Session["sCompID"], EMP_AID, NewYear, JoinDate, Leave_CId);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "";
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
                gvLeaveCards.DataSource = dsInv;
                gvLeaveCards.DataBind();
                OtherDetails.Visible = true;
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

        //protected void gvLeaveCards_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    int cnt = 0;
        //    if (e.Row.RowType == DataControlRowType.Header)
        //    {
        //        foreach (TableCell cell in e.Row.Cells)
        //        {
        //            cell.CssClass = "GridViewHeader";
        //            cell.HorizontalAlign = HorizontalAlign.Center;

        //        }
        //    }
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        foreach (TableCell cell in e.Row.Cells)
        //        {
        //            if (cnt == 0 || cnt == 1)
        //            {
        //                cell.CssClass = "GridViewItem";
        //                cell.HorizontalAlign = HorizontalAlign.Left;
        //            }
        //            else
        //            {
        //                cell.CssClass = "GridViewItem";
        //                cell.HorizontalAlign = HorizontalAlign.Center;
        //            }

        //            if (cnt + 1 == e.Row.Cells.Count && cell.Text == "No")
        //            {
        //                cell.BackColor = System.Drawing.Color.Wheat;
        //            }
        //            cnt = cnt + 1;
        //        }
        //    }

        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                NewYear = drpSelectFinancialYear.SelectedValue;
                string NewMonth = drpSelectMonth.SelectedValue;
                dsInv = objInv.GetPLEncashReport((string)Session["sCompID"], NewYear, NewMonth);
                //dsInv = objInv.GetAttendance();
                gvLeaveCards.DataSource = dsInv;
                gvLeaveCards.DataBind();
                OtherDetails.Visible = true;
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
            if (gvLeaveCards != null)
            {
                if (gvLeaveCards.Rows.Count > 0)
                {
                    ExportGridView(gvLeaveCards);
                }
                else
                {
                    lblMessage.Text = "Generate report first.";
                }
            }
        }

        private void ExportGridView(GridView gvFiles)
        {
            string guid;
            string path;

            this.gvLeaveCards.AllowPaging = false;
            this.gvLeaveCards.AllowSorting = false;
            this.gvLeaveCards.EditIndex = -1;

            Response.Clear();
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition",
                    "attachment;filename=PL_Encashment.xls");
            Response.Charset = "";
            StringWriter swriter = new StringWriter();
            HtmlTextWriter hwriter = new HtmlTextWriter(swriter);
            gvLeaveCards.RenderControl(hwriter);
            Response.Write(swriter.ToString());
            Response.End();


            //System.IO.StringWriter sw = new System.IO.StringWriter();
            //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

            //guid = Convert.ToString(Guid.NewGuid());
            //guid = guid + "Attendance" + DateTime.Now.ToString() + ".xlsx"    ;
            //path = Request.PhysicalApplicationPath.ToString() + "Time Sheet\\";
            //path=path + guid;

            //// Render grid view control.

            //gvFiles.RenderControl(htw);

            //// Write the rendered content to a file.
            //string renderedGridView = sw.ToString();
            //File.WriteAllText(@path , renderedGridView);

        }
    }
}