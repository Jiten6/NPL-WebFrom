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
    public partial class ExitEmployees : System.Web.UI.Page
    {
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.DBUtility obdbutility = new NewPortal2023.ESS.DBUtility();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["sCompID"] != null)
            {
            if (!Page.IsPostBack)
            {

                if (Session["sCompID"] != null)
                {
                    try
                    {
                        GetDetail();
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

        private void GetDetail()
        {
            try
            {
                ds = objNps.GetExitEmployeeDetails((string)Session["sCompID"]);
                if (ds.Tables.Count > 0)
                {
                    gvExitEmpList.DataSource = ds;
                    gvExitEmpList.DataBind();
                }
                else
                {
                    gvExitEmpList.DataSource = null;
                    gvExitEmpList.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the 
            //specified ASP.NET server control at run time. 
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvExitEmpList != null)
            {
                if (gvExitEmpList.Rows.Count > 0)
                {
                    ExportGridView(gvExitEmpList);
                }
                else
                {
                    lblMessage.Text = "Quit Data Not Founds.";
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
            string FileName = "Exit Employee" + DateTime.Now + ".xls";
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