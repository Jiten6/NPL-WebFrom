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
    public partial class AttendanceEdit : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveUpload objInv = new NewPortal2023.ESS.LeaveUpload();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsInv = new DataSet();

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
                        if (Session["EmpCode"] != null)
                        {
                            FillLeave();
                        }
                        else if (Session["sEmpCode"] != null && Session["sCompID"].ToString() != "CO000141")
                        {
                            FillLeave();
                        }
                        else if (Session["sEmpCode"] != null && Session["sCompID"].ToString() == "CO000141")
                        {
                            FillNPLLeave();
                            //FillLeave();
                        }
                        //btnExport.Visible = false;

                        //DateTime now = DateTime.Now;
                        //DateTime startDate = new DateTime(now.Year, now.Month, 1);
                        //DateTime endDate = startDate.AddMonths(1).AddDays(-1);

                        //txtDate.Text = startDate.ToString("dd-MMM-yyyy");
                        //txtDateTo.Text = endDate.ToString("dd-MMM-yyyy");

                        //btnSave.Visible = false;
                        //btnExport.Visible = false;
                    }
                    catch (Exception ex)
                    {
                        //lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
                    }
                }
                else
                {
                    Response.Redirect("Logout.aspx");
                }
            }
        }
        //protected void btnGenerate_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (txtDate.Text == "")
        //        {
        //            lblMessage.Text = "Select from date";
        //            return;
        //        }
        //        if (txtDateTo.Text == "")
        //        {
        //            lblMessage.Text = "Select to date";
        //            return;
        //        }

        //        if (Convert.ToDateTime(txtDate.Text) > Convert.ToDateTime(txtDateTo.Text))
        //        {
        //            lblMessage.Text = "From date cannot be greater that to date";
        //            return;
        //        }

        //        //DateTime fromDt = DateTime.ParseExact(txtDate.Text.Trim(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //        //DateTime toDt = DateTime.ParseExact(txtDateTo.Text.Trim(), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

        //        //if (fromDt.Month == toDt.Month && fromDt.Year == toDt.Year)
        //        //{
        //            FillLeave();
        //            lblMessage.Text = "";

        //            //DateTime date2day = DateTime.ParseExact("02-" + DateTime.Now.ToString("MM-yyyy"), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //            //if (toDt < date2day)
        //            //{
        //            //    btnSave.Visible = false;
        //            //}
        //            //else
        //            //{
        //                btnSave.Visible = true;
        //            //    btnExport.Visible = true;
        //            //}
        //        //}
        //        //else
        //        //{
        //        //    gvLeave.DataSource = null;
        //        //    gvLeave.DataBind();
        //        //    lblMessage.Text = "You can only select one month at a time.";
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Error occurred in application";
        //    }
        //}
        //protected void btnFrom_Click(object sender, EventArgs e)
        //{
        //    calFrom.Visible = true;
        //}
        //protected void calFrom_SelectionChanged(object sender, EventArgs e)
        //{
        //    txtDate.Text = calFrom.SelectedDate.Date.ToString("dd-MMM-yyyy");
        //    calFrom.Visible = false;
        //}
        //protected void btnFromTo_Click(object sender, EventArgs e)
        //{
        //    calFromTo.Visible = true;
        //}
        //protected void calFromTo_SelectionChanged(object sender, EventArgs e)
        //{
        //    txtDateTo.Text = calFromTo.SelectedDate.Date.ToString("dd-MMM-yyyy");
        //    calFromTo.Visible = false;
        //}

        private void FillNPLLeave()
        {
            lblMessage.Text = "";
            string NewYear;
            string finYear = objcommon.GetFinalcialYear();
            string[] years = finYear.Split('.');
            NewYear = years[0];

            dsInv = objInv.GetAttendanceEditEmpAid((string)Session["sCompID"], (string)Session["sEmpCode"]);
            //dsInv = objInv.GetAttendanceEditDetails((string)Session["sCompID"], (string)Session["sEmpCode"]);
            gvLeave.DataSource = dsInv;
            gvLeave.DataBind();
        }

        private void FillLeave()
        {
            lblMessage.Text = "";
            string NewYear;
            string finYear = objcommon.GetFinalcialYear();
            string[] years = finYear.Split('.');
            NewYear = years[0];

            //dsInv = objInv.GetAttendanceEdit((string)Session["sCompID"], (string)Session["sEmpID"], txtDate.Text, txtDateTo.Text);


            if ((string)Session["sCompID"].ToString() == "CO000141")
            {
                dsInv = objInv.GetAttendanceEditDetails((string)Session["sCompID"], (string)Session["EmpCode"]);
                gvLeave.DataSource = dsInv;
                gvLeave.DataBind();
            }
            else
            {
                dsInv = objInv.GetAttendanceEditDetails((string)Session["sCompID"], (string)Session["sEmpCode"]);
                //gvLeaveOther.DataSource = dsInv;
               // gvLeaveOther.DataBind();
            }

            //DateTime dateNow = DateTime.ParseExact("02-" + DateTime.Now.ToString("MM-yyyy") , "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime dateFrom = DateTime.ParseExact(txtDate.Text.Trim(), "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
            //DateTime dateTo = DateTime.ParseExact(txtDateTo.Text.Trim(), "dd-MMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);


        }


        //protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        TextBox txtRem = (TextBox)e.Row.FindControl("txtRem");
        //        Label lblRem = (Label)e.Row.FindControl("lblRem");
        //        txtRem.Visible = false;
        //    }
        //}

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvLeave != null)
            {
                if (gvLeave.Rows.Count > 0)
                {
                    ExportGridView(gvLeave);
                }
                else
                {
                    lblMessage.Text = "Generate report first.";
                }
            }

        }
        private void ExportGridView(GridView gvFiles)
        {
            // dsInv = objInv.GetAttendanceEdit((string)Session["sCompID"], (string)Session["sEmpID"], txtDate.Text, txtDateTo.Text);

            string attachment = "attachment; filename=attendance_" + DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss") + ".xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/vnd.ms-excel";
            string tab = "";
            foreach (DataColumn dc in dsInv.Tables[0].Columns)
            {
                Response.Write(tab + dc.ColumnName);
                tab = "\t";
            }
            Response.Write("\n");
            int i;
            foreach (DataRow dr in dsInv.Tables[0].Rows)
            {
                tab = "";
                for (i = 0; i < dsInv.Tables[0].Columns.Count; i++)
                {
                    Response.Write(tab + dr[i].ToString());
                    tab = "\t";
                }
                Response.Write("\n");
            }
            Response.End();

            //this.gvLeave.AllowPaging = false;
            //this.gvLeave.AllowSorting = false;
            //this.gvLeave.EditIndex = -1;

            //Response.Clear();
            //Response.ContentType = "application/vnd.xls";
            //Response.AddHeader("content-disposition",
            //        "attachment;filename=Attendance.xls");
            //Response.Charset = "";
            //StringWriter swriter = new StringWriter();
            //HtmlTextWriter hwriter = new HtmlTextWriter(swriter);
            //gvLeave.RenderControl(hwriter);
            //Response.Write(swriter.ToString());
            //Response.End();


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

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the 
            //specified ASP.NET server control at run time. 
        }

        private string MakeInvXml(GridView GV)
        {

            StringBuilder sbTaxDetails = new StringBuilder();
            string xmlInv = string.Empty;

            //sbTaxDetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sbTaxDetails.Append("<ROOT>");

            foreach (GridViewRow gvr in GV.Rows)
            {
                sbTaxDetails.Append("<DIR AID='" + ReplaceSpecialCharacters(((Label)gvr.FindControl("lblCID")).Text.Trim()) + "'");
                sbTaxDetails.Append(" INTM='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtIn")).Text.Trim()) + "'");
                sbTaxDetails.Append(" OUTTM='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtOut")).Text.Trim()) + "'");
                sbTaxDetails.Append(" REMARK='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRem")).Text.Trim()) + "' />");
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objInv.UploadAttendance(MakeInvXml(gvLeave));
                lblMessage.Text = "Success.";
                FillLeave();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}