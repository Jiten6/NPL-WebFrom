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
    public partial class LeaveHistory : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveApplication objInv = new NewPortal2023.ESS.LeaveApplication();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
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
                           
                            drpleaveyear.SelectedValue = DateTime.Now.Year.ToString();



                            FillLeave();
                        FillType();
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

        //private void FillLeave()
        //{

        //    string finYear = objcommon.GetFinalcialYear();
        //    string[] years = finYear.Split('.');
        //    OldYear = years[1];
        //    NewYear = years[0];

        //    string selectedYear = DateTime.Now.Year.ToString();

        //    dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, OldYear);
        //    gvLeave.DataSource = dsInv;
        //    gvLeave.DataBind();
        //}

        private void FillLeave()
        {

            string NewYear = DateTime.Now.Year.ToString();


            string finYear = objcommon.GetFinalcialYear();

            string[] years = finYear.Split('.');

            if (drpleaveyear.SelectedValue == DateTime.Now.Year.ToString())
            {
                string selectedYear = drpleaveyear.SelectedValue;
                string leaveType = "0";

                dsInv = objInv.GetLeaveTypeDetails((string)Session["sCompID"], (string)Session["sEmpID"], selectedYear, leaveType);
                //dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], selectedYear, OldYear);
                gvLeave.DataSource = dsInv;
                gvLeave.DataBind();
            }
            else
            {
                dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, OldYear);
            }
            

            // Fetch leave details for the current year (NewYear)
            

            // Bind the fetched data to GridView
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
            LinkButton lnkstatuss = (LinkButton)lnkstatus.NamingContainer.FindControl("lnkstatus");
            if (lnkstatuss.Text!="Approved")
            {
                Response.Redirect("LeaveApplication.aspx?sender=me&id=" + lblid.Text);
            }
            

        }

        //protected void lnkcancel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        LinkButton lnkcancel = (LinkButton)sender;
        //        Label lblid = (Label)lnkcancel.NamingContainer.FindControl("lblId");
        //        objInv.CancelLeave(lblid.Text);
        //        FillLeave();
        //        lblMessage.Text = "Leave application cancelled.";
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //    }
        //}

        protected void lnkcancel_Click1(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkcancel = (LinkButton)sender;
                Label lblid = (Label)lnkcancel.NamingContainer.FindControl("lblId");
                objInv.CancelLeave(lblid.Text);
                FillLeave();
                lblMessage.Text = "Leave application cancelled.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        private void FillType()
        {
            dsInv = objInv.GetLeave((string)Session["sCompID"], (string)Session["sEmpID"]);
            drpLeaveType.Items.Clear();
            drpLeaveType.DataTextField = "LEAVE";
            drpLeaveType.DataValueField = "Cid";
            drpLeaveType.DataSource = dsInv.Tables[0];
            drpLeaveType.DataBind();
            drpLeaveType.Items.Insert(0, new ListItem("[Select One]", "0"));
        }

        protected void drpleaveyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string selectedYear = drpleaveyear.SelectedValue;
            string leaveType = drpLeaveType.SelectedValue;
            if (drpLeaveType.SelectedValue != "")
            {
                dsInv = objInv.GetLeaveTypeDetails((string)Session["sCompID"], (string)Session["sEmpID"], selectedYear, leaveType);
                //dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], selectedYear, OldYear);
                gvLeave.DataSource = dsInv;
                gvLeave.DataBind();
            }
            else
            {
                dsInv = objInv.GetLeaveTypeDetails((string)Session["sCompID"], (string)Session["sEmpID"], selectedYear, leaveType);
                gvLeave.DataSource = dsInv;
                gvLeave.DataBind();
            }
        }

        protected void drpLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedYear = drpleaveyear.SelectedValue;
            string leaveType = drpLeaveType.SelectedValue;
            if (drpLeaveType.SelectedValue != "")
            {
                dsInv = objInv.GetLeaveTypeDetails((string)Session["sCompID"], (string)Session["sEmpID"], selectedYear, leaveType);
                gvLeave.DataSource = dsInv;
                gvLeave.DataBind();
            }
            else
            {
                dsInv = objInv.GetLeaveTypeDetails((string)Session["sCompID"], (string)Session["sEmpID"], selectedYear, leaveType);
                gvLeave.DataSource = dsInv;
                gvLeave.DataBind();
            }

        }


    }
}