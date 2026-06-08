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
    public partial class LeaveHoliday1 : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveHoliday objInv = new NewPortal2023.ESS.LeaveHoliday();
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
                           
                            drpleaveyear.SelectedValue = "2026";

                            FillLeave();

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
             NewYear = drpleaveyear.SelectedValue;
            dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, OldYear);
            gvLeave.DataSource = dsInv;
            gvLeave.DataBind();
        }

        protected void drpleaveyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            string selectedYear = drpleaveyear.SelectedValue;
            dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], selectedYear, OldYear);

            if (dsInv != null && dsInv.Tables.Count > 0 && dsInv.Tables[0].Rows.Count > 0)
            {
                gvLeave.DataSource = dsInv.Tables[0];
                gvLeave.DataBind();
            }

        }

        protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //int cnt = 0;
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    foreach (TableCell cell in e.Row.Cells)
            //    {
            //        cell.CssClass = "GridViewHeader";
            //        cell.HorizontalAlign = HorizontalAlign.Center;

            //    }
            //}
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    foreach (TableCell cell in e.Row.Cells)
            //    {
            //        if (cnt == 0 || cnt == 1)
            //        {
            //            cell.CssClass = "GridViewItem";
            //            cell.HorizontalAlign = HorizontalAlign.Center;
            //        }
            //        else
            //        {
            //            cell.CssClass = "GridViewItem";
            //            cell.HorizontalAlign = HorizontalAlign.Center;
            //        }

            //        if (cnt + 1 == e.Row.Cells.Count && cell.Text == "No")
            //        {
            //            cell.BackColor = System.Drawing.Color.Wheat;
            //        }
            //        cnt = cnt + 1;
            //    }
            //}

        }
    }
}