using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text;


namespace NewPortal2023.ESS
{
    public partial class OutSideDutyApprovalView : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveApproval objLA = new NewPortal2023.ESS.LeaveApproval();
        NewPortal2023.ESS.LeaveApplication objInv = new NewPortal2023.ESS.LeaveApplication();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsInv = new DataSet();
        string OldYear, NewYear;
       

        protected void Page_Load(object sender, EventArgs e)
        {
              if (Session["sCompID"]!=null)
            {
            if (!Page.IsPostBack)
            {
                if (Session["sCompID"] != null)
                {
                    try
                    {
                        string strResult = objcommon.Validate_ControlInfo("INV");

                        if (strResult.Contains("This page is currently unavailable.") == true)
                        {
                            Response.Redirect("Unavailable.aspx?strName=Investment Details");
                            return;
                        }
                        FillLeave();
                        FillStatus();
                        //FillType();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
                    }
                }
                else
                {
                    Response.Redirect("Logout.aspx");
                }
            }
        }
              else
              {
                  Response.Redirect("Login.aspx");
              }
        }

        private void FillStatus()
        {
            string Status = "1";
            dsInv = objInv.GetStatus(Status);
            drpStatus.Items.Clear();
            drpStatus.DataTextField = "status";
            drpStatus.DataValueField = "id";
            drpStatus.DataSource = dsInv;
            drpStatus.DataBind();
            drpStatus.Items.Insert(0, new ListItem("[Select One]", "-"));
        }

        private string MakeChkXml(GridView gv)
        {
            StringBuilder sbChkGlCode = new StringBuilder();
            StringBuilder sbUnChkGlCode = new StringBuilder();
            string xmlChkGlCode = string.Empty;
            string xmlUnChkGlCode = string.Empty;

            sbChkGlCode.Append("<ROOT>");

            foreach (GridViewRow gvr in gv.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    sbChkGlCode.Append("<CHKGLSUMBIT CODE='" + ((Label)gvr.FindControl("lblId")).Text.Trim() + "'/>");
                    string lblId = ((Label)gvr.FindControl("lblId")).Text;

                }

            }
            sbChkGlCode.Append("</ROOT>");

            xmlChkGlCode = sbChkGlCode.ToString();

            return xmlChkGlCode;
        }

        private void MakeChkXmleMAIL(GridView gv)
        {


            foreach (GridViewRow gvr in gv.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {

                    string lblId = ((Label)gvr.FindControl("lblId")).Text;
                    string lblFrom = ((Label)gvr.FindControl("lblFrom")).Text;
                    string lblTo = ((Label)gvr.FindControl("lblTo")).Text;
                    string lblCr_Date = ((Label)gvr.FindControl("lblCr_Date")).Text;
                    string lblOdType = ((Label)gvr.FindControl("lblOdType")).Text;
                    APPROVEMAIL(lblId, lblFrom, lblTo, lblCr_Date, lblOdType);
                    lblId = "";
                    lblFrom = "";
                    lblTo = "";
                    lblCr_Date = "";
                    lblOdType = "";
                }

            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (drpStatus.SelectedValue.Trim() == "-")
            {
                lblMessage.Text = "Select Action.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select Action.');");

            }
            else
            {
                objNps.Status = drpStatus.SelectedValue;
                objNps.EmpCode = Session["sEmpID"].ToString();
                dsInv = objNps.UpdateChkSubmit1(MakeChkXml(gvLeave));
                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows[0]["result"].ToString() == "")
                    {
                        string Status = string.Empty;
                        if (drpStatus.SelectedItem.Text == "Approve")
                        {
                            Status = "Approved";

                        }
                        else if (drpStatus.SelectedItem.Text == "Reject")
                        {
                            Status = "Rejected";
                        }
                        MakeChkXmleMAIL(gvLeave);
                        lblMessage.Text = "Out Side Duty Claim " + Status + " Successfully.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Out Side Duty Claim '" + Status + "' Successfully.');");
                    }
                }
                FillLeave();
                FillStatus();

            }
        }
        private void APPROVEMAIL(string lblId, string lblFrom, string lblTo, string lblCr_Date, string lblOdType)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            string Status = string.Empty;
            dsmkkMail = emailSend.GetEmpAttendanceaPPROVE((string)Session["sCompID"], (string)Session["sEmpID"], lblId);

            //if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            {
                //if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
                //{

                if (drpStatus.SelectedItem.Text == "Approve")
                {
                    Status = "Approved";

                }
                else if (drpStatus.SelectedItem.Text == "Reject")
                {
                    Status = "Rejected";
                }
                string EMPNAME = dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString();
                string UserName = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();

                string clientbodys = "Dear " + EMPNAME + ",<br><br>" + UserName + " Out Door Duty Application is " + Status + " Successfully.<br>"
                     + "<br><br> Details"
                   + "<br>Out Door Duty Type :- " + lblOdType
                    + "<br> Applied Date :- " + lblCr_Date
                    + "<br> From Date :- " + lblFrom
                    + "<br> To Date :- " + lblTo
                    + "<br><br>ThankYou."
                   + "< br >Payroll Team< br > ";
                string emails = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
                string subjects = "Do Not Reply: Out Door Duty Application";
                //emailSend.SendEmailNPL(emails, subjects, clientbodys);

                string Newemails = "";
                string checkerbodys = "Dear " + dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString() + ",<br><br>" + EMPNAME +
                    " has Reviewed your Out Door Duty Application. Your Out Door Duty application is " + Status + "."
                    + "<br><br> Details"
                    + "<br>Out Door Duty Type :- " + lblOdType
                    + "<br> Applied Date :- " + lblCr_Date
                    + "<br> From Date :- " + lblFrom
                    + "<br> To Date :- " + lblTo
                   + "<br><br>ThankYou."
                   + "< br >Payroll Team< br > ";
                //\nWith Best Regards,\nSequel Outsourcing PVT.LTD<br> < br >
                Newemails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
                emailSend.SendEmailNPL(Newemails, subjects, checkerbodys);



                //}
            }

        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
            CheckBox chckheader = (CheckBox)gvLeave.HeaderRow.FindControl("chkSelectAll");
            foreach (GridViewRow row in gvLeave.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckheader.Checked == true)
                {
                    chckrw.Checked = true;

                }
                else
                {
                    chckrw.Checked = false;
                }


            }
        }
        protected void drpStatus_OnTextChanged(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;

        }
        private void FillLeave()
        {
            string finYear = objcommon.GetFinalcialYear();
            string[] years = finYear.Split('.');
            OldYear = years[1];
            NewYear = years[0];

            dsInv = objNps.GetAppDetails((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, OldYear);
            gvLeave.DataSource = dsInv;
            gvLeave.DataBind();
        }

        protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int cnt = 0;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.CssClass = "GridViewHeader";
                    cell.HorizontalAlign = HorizontalAlign.Center;

                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton hyper = (LinkButton)e.Row.FindControl("lnkstatus");
                LinkButton cancel = (LinkButton)e.Row.FindControl("lnkcancel");
                hyper.Enabled = false;

                if (hyper.Text == "Approved" || hyper.Text == "Draft")
                {
                    hyper.Enabled = true;
                }

                if (hyper.Text == "Submited" || hyper.Text == "Draft")
                {
                    cancel.Visible = true;
                }
                else
                {
                    cancel.Visible = false;
                }

                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cnt == 0 || cnt == 1)
                    {
                        cell.CssClass = "GridViewItem";
                        cell.HorizontalAlign = HorizontalAlign.Center;
                    }
                    else
                    {
                        cell.CssClass = "GridViewItem";
                        cell.HorizontalAlign = HorizontalAlign.Center;
                    }

                    if (cnt + 1 == e.Row.Cells.Count && cell.Text == "No")
                    {
                        cell.BackColor = System.Drawing.Color.Wheat;
                    }
                    cnt = cnt + 1;
                }
            }

        }
        protected void lnkstatus_Click(object sender, EventArgs e)
        {
            LinkButton lnkstatus = (LinkButton)sender;
            Label lblid = (Label)lnkstatus.NamingContainer.FindControl("lblId");
            Response.Redirect("OutSideDutyApproval.aspx?sender=me&id=" + lblid.Text);

        }


        protected void lnkcancel_Click1(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkcancel = (LinkButton)sender;
                Label lblid = (Label)lnkcancel.NamingContainer.FindControl("lblId");
                objNps.CancelLeave(lblid.Text);
                FillLeave();
                lblMessage.Text = "Out Side Duty application cancelled.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        private void FillType()
        {
            dsInv = objNps.GetLeave((string)Session["sCompID"], (string)Session["sEmpID"]);
            drpLeaveType.Items.Clear();
            drpLeaveType.DataTextField = "LEAVE";
            drpLeaveType.DataValueField = "Cid";
            drpLeaveType.DataSource = dsInv.Tables[1];
            drpLeaveType.DataBind();
            drpLeaveType.Items.Insert(0, new ListItem("[Select One]", "0"));
        }

        protected void drpLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string leaveType = drpLeaveType.SelectedValue;
            if (drpLeaveType.SelectedValue != "")
            {
                dsInv = objNps.GetLeaveTypeDetails((string)Session["sCompID"], (string)Session["sEmpID"], leaveType);
                gvLeave.DataSource = dsInv;
                gvLeave.DataBind();
            }
            else
            {
                dsInv = objNps.GetLeaveTypeDetails((string)Session["sCompID"], (string)Session["sEmpID"], leaveType);
                gvLeave.DataSource = dsInv;
                gvLeave.DataBind();
            }

        }

        /// <summary>
        /// //////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkApp_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (ViewState["countadd"] != null)
            {
                count += Convert.ToInt32(ViewState["countadd"].ToString()) + 1;
            }
            else
            {
                count = 1;
            }
            int countadd = count % 2;
            ViewState["countadd"] = count;
            if (countadd == 0)
            {
                divApprOD.Visible = false;
            }
            else
            {
                divApprOD.Visible = true;
                FILLApprovedList();
            }
        }

        protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
            
        }

        private void FILLApprovedList()
        {
            DataSet ds = new DataSet();
            string finYear = objcommon.GetFinalcialYear();
            string[] years = finYear.Split('.');
            OldYear = years[1];
            NewYear = years[0];

            ds = objLA.GetApprODDetails((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, OldYear);
            if (ds.Tables.Count > 0)
            {
                gvApprOD.DataSource = ds;
                gvApprOD.DataBind();
                btnExport.Visible = true;
            }
            else
            {
                gvApprOD.DataSource = null;
                gvApprOD.DataBind();
                btnExport.Visible = false;
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the 
            //specified ASP.NET server control at run time. 
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvApprOD != null)
            {
                if (gvApprOD.Rows.Count > 0)
                {
                    ExportGridView(gvApprOD);
                }
                else
                {
                    lblMessage.Text = "Generate report first.";
                }
            }
        }

        private void ExportGridView(GridView gvFiles)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "OutDoorDutyReport_" + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvFiles.GridLines = GridLines.Both;
            gvFiles.HeaderStyle.Font.Bold = true;
            gvFiles.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }
    }
}