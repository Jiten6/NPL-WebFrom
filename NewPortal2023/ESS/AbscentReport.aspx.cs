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
    public partial class AbscentReport : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveUpload objInv = new NewPortal2023.ESS.LeaveUpload();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
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
                        //FillLeave();
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
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDate.Text == "")
                {
                    lblMessage.Text = "Select from date";
                    return;
                }
                if (txtDateTo.Text == "")
                {
                    lblMessage.Text = "Select to date";
                    return;
                }

                //if (Convert.ToDateTime(txtDate.Text) > Convert.ToDateTime(txtDateTo.Text))
                //{
                //    lblMessage.Text = "From date cannot be greater that to date";
                //    return;
                //}
                FillLeave();
                lblMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application";
            }
        }
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

        private void FillLeave()
        {
            //string NewYear;
            //string finYear = objcommon.GetFinalcialYear();
            //string[] years = finYear.Split('.');
            //NewYear = years[0];

            dsInv = objNps.GetAbscent((string)Session["sCompID"], (string)Session["sEmpID"], txtDate.Text, txtDateTo.Text);
            gvLeave.DataSource = dsInv;
            gvLeave.DataBind();
        }
        protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.BackColor = System.Drawing.Color.White;
            }
        }

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
            string guid;
            string path;

            this.gvLeave.AllowPaging = false;
            this.gvLeave.AllowSorting = false;
            this.gvLeave.EditIndex = -1;

            Response.Clear();
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition",
                    "attachment;filename=Absent.xls");
            Response.Charset = "";
            StringWriter swriter = new StringWriter();
            HtmlTextWriter hwriter = new HtmlTextWriter(swriter);
            gvLeave.RenderControl(hwriter);
            Response.Write(swriter.ToString());
            Response.End();

        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the 
            //specified ASP.NET server control at run time. 
        }
    }
}