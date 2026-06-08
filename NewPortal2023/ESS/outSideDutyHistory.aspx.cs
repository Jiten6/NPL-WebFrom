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
    public partial class outSideDutyHistory : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveApplication objInv = new NewPortal2023.ESS.LeaveApplication();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
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


        private void FillLeave()
        {
            string finYear = objcommon.GetFinalcialYear();
            string[] years = finYear.Split('.');
            OldYear = years[1];
            NewYear = years[0];

            dsInv = objNps.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, OldYear);
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

                if (hyper.Text == "Approved" || hyper.Text == "Draft" || hyper.Text == "Rejected")
                {
                    hyper.Enabled = true;
                }

                if (hyper.Text == "Submitted")
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
            Response.Redirect("NPL_OutSideDuty.aspx?sender=me&id=" + lblid.Text);

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
    }
}