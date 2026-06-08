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
using System.Text;
using System.Globalization;

namespace NewPortal2023.ESS
{
    public partial class LeaveApproval1 : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveApproval objInv = new NewPortal2023.ESS.LeaveApproval();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsInv = new DataSet();
        string OldYear, NewYear;
       

        protected void Page_Load(object sender, EventArgs e)
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

        private void FillLeave()
        {
            string finYear = objcommon.GetFinalcialYear();
            string[] years = finYear.Split('.');
            OldYear = years[1];
            NewYear = years[0];

            dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, OldYear);
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

        protected void drpStatus_TextChanged(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
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
                dsInv = objNps.UpdateChkLASubmit(MakeChkXml(gvLeave), (string)Session["sCompID"], (string)Session["sEmpID"]);
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
                        if (drpStatus.SelectedItem.Text == "Approve")
                        {
                            dsInv = objNps.updateNplLeaveBalance((string)Session["sCompID"], (MakeChkXml(gvLeave)));
                        }
                        dsInv = objNps.LeaveUpdatePreviousMonth((string)Session["sCompID"]);
                        MakeChkXmlEmail(gvLeave);
                        lblMessage.Text = "Leave Application " + Status + " Successfully.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Leave Application '" + Status + "' Successfully.');");
                    }

                }
                FillLeave();
                FillStatus();


            }
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

        private void MakeChkXmlEmail(GridView gv)
        {
            StringBuilder sbChkGlCode = new StringBuilder();
            StringBuilder sbUnChkGlCode = new StringBuilder();
            string xmlChkGlCode = string.Empty;
            string xmlUnChkGlCode = string.Empty;



            foreach (GridViewRow gvr in gv.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    //sbChkGlCode.Append("<CHKGLSUMBIT CODE='" + ((Label)gvr.FindControl("lblId")).Text.Trim() + "'/>");
                    string lblId = ((Label)gvr.FindControl("lblId")).Text;
                    string lblFrom = ((Label)gvr.FindControl("lblFrom")).Text;
                    string lblTo = ((Label)gvr.FindControl("lblTo")).Text;
                    string lblCr_Date = ((Label)gvr.FindControl("lblCr_Date")).Text;
                    string lblLeave = ((Label)gvr.FindControl("lblLeave")).Text;
                    APPROVEMAIL(lblId, lblFrom, lblTo, lblCr_Date, lblLeave);
                    lblId = "";
                    lblFrom = "";
                    lblTo = "";
                    lblCr_Date = "";
                    lblLeave = "";
                }

            }
        }

        private void APPROVEMAIL(string lblId, string lblFrom, string lblTo, string lblCr_Date, string lblLeave)
        {
            emailSend = new  NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            string Status = string.Empty;
            dsmkkMail = emailSend.GetEmpAttendanceLeaveaPPROVE((string)Session["sCompID"], (string)Session["sEmpID"], lblId);
            DataSet ds = new DataSet();

            ds = emailSend.getName((string)Session["sCompID"], lblLeave);
            if (ds.Tables.Count > 0)
            {
                lblLeave = ds.Tables[0].Rows[0]["LEAVE_NAME"].ToString();
            }
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

                string clientbodys = "Dear " + EMPNAME + ",<br><br>" + UserName + " Leave Application is " + Status + " Successfully.<br>"
                    + "<br><br> Details"
                    + "<br>Leave Type :- " + lblLeave
                    + "<br>Applied Date :- " + lblCr_Date
                    + "<br> From Date :- " + lblFrom
                    + "<br> To Date :- " + lblTo
                    + "<br><br>ThankYou."
                    + "< br >Payroll Team< br > ";
                string emails = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
                string subjects = "Do Not Reply: Leave Application";
                //emailSend.SendEmailNPL(emails, subjects, clientbodys);


                string Newemails = "";
                string checkerbodys = "Dear " + UserName + ",<br><br>" + EMPNAME + " has Reviewed your Leave Application.Your Leave  Application  is " + Status + "."
               + "<br><br> Details"
               + "<br>Leave Type :- " + lblLeave
               + "<br>Applied Date :- " + lblCr_Date
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
                divLeaveAppr.Visible = false;
            }
            else
            {
                divLeaveAppr.Visible = true;
                FILLApprovedList();
            }
        }

        private void FILLApprovedList()
        {
            DataSet ds = new DataSet();
            string finYear = objcommon.GetFinalcialYear();
            string[] years = finYear.Split('.');
            OldYear = years[1];
            NewYear = years[0];

            ds = objInv.GetApprLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, OldYear);

            if (ds.Tables.Count > 0)
            {
                gvLeaveAppr.DataSource = ds;
                gvLeaveAppr.DataBind();
            }
            else
            {
                gvLeaveAppr.DataSource = null;
                gvLeaveAppr.DataBind();
            }

        }
    }
}