using System;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class LeavestatusReporrt : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveUpload objInv = new NewPortal2023.ESS.LeaveUpload();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    NewPortal2023.ESS.LeaveUpload objInv = new NewPortal2023.ESS.LeaveUpload();

                }
            }
            catch (Exception ex)
            {


            }
        }
        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                NewPortal2023.ESS.LeaveUpload objInv = new NewPortal2023.ESS.LeaveUpload();
                ds = objInv.Fill_leave_status((string)Session["sCompID"]);

                gvLeaveStatus.DataSource = ds;
                gvLeaveStatus.DataBind();

            }
            catch (Exception ex)
            {


                //lblMessageCreate.Text = ex.Message;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvLeaveStatus != null)
            {
                if (gvLeaveStatus.Rows.Count > 0)
                {
                    ExportGridView(gvLeaveStatus);
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

            this.gvLeaveStatus.AllowPaging = false;
            this.gvLeaveStatus.AllowSorting = false;
            this.gvLeaveStatus.EditIndex = -1;

            Response.Clear();
            Response.ContentType = "application/vnd.xls";
            Response.AddHeader("content-disposition",
                    "attachment;filename=Leave_Status_Report.xls");
            Response.Charset = "";
            StringWriter swriter = new StringWriter();
            HtmlTextWriter hwriter = new HtmlTextWriter(swriter);
            gvLeaveStatus.RenderControl(hwriter);
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

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the 
            //specified ASP.NET server control at run time. 
        }
    }
}