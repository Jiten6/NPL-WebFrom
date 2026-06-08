using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Windows.Forms;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.IO.Compression;

namespace NewPortal2023.ESS
{
    public partial class PMSHistoricPage : System.Web.UI.Page
    {
        NewPortal2023.ESS.PMSHistoric objPMSHIS = new NewPortal2023.ESS.PMSHistoric();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet ds = new DataSet();
        DataSet ds1 = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Report Generated Successfully.";
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "HideLoader()", true);

            ds = objPMSHIS.GetPMSHistoric();

            string extension;
            string encoding;
            string contentType;
            string[] streamIds;
            Warning[] warnings;
            rptPrint.Visible = true;
            ReportDataSource datasource1 = new ReportDataSource("dsPerformanceRating", ds.Tables[0]);


            rptPrint.LocalReport.DataSources.Clear();
            rptPrint.ProcessingMode = ProcessingMode.Local;
            rptPrint.LocalReport.ReportPath = @"ESS\PMSHistoricDataReport.rdlc";

            //rptPrint.LocalReport.DisplayName = (string)Session["sEmpCode"] + "_Flexi";
            rptPrint.LocalReport.DataSources.Add(datasource1);
            rptPrint.LocalReport.Refresh();

            byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = ContentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=Employee_Missing_Punch_Report.xls");
            string filename = "Past_Performance_Ratingd_Report.xls";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.BinaryWrite(bytes);
            Response.Flush();

            Response.End();

            divAlert.Visible = true;
            string script = $@"<script type='text/javascript'>alert('Report Generated Successfully.');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            lblMessage.Text = "Report Generated Successfully.";
            divAlert.Visible = false;
        }

        protected void btnGenerateReportAllEmp_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "Report Generated Successfully.";
            //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "HideLoader()", true);

            ds1 = objPMSHIS.GetPMSLastPromotion();

            string extension;
            string encoding;
            string contentType;
            string[] streamIds;
            Warning[] warnings;
            rptPrint.Visible = true;
            ReportDataSource datasource1 = new ReportDataSource("dsLastPromotion", ds1.Tables[0]);


            rptPrint.LocalReport.DataSources.Clear();
            rptPrint.ProcessingMode = ProcessingMode.Local;
            rptPrint.LocalReport.ReportPath = @"ESS\PMSLastPromotionReport.rdlc";

            //rptPrint.LocalReport.DisplayName = (string)Session["sEmpCode"] + "_Flexi";
            rptPrint.LocalReport.DataSources.Add(datasource1);
            rptPrint.LocalReport.Refresh();

            byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = ContentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=Employee_Missing_Punch_Report.xls");
            string filename = "Last_Promotion_Report.xls";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.BinaryWrite(bytes);
            Response.Flush();

            Response.End();

            divAlert.Visible = true;
            string script = $@"<script type='text/javascript'>alert('Report Generated Successfully.');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            lblMessage.Text = "Report Generated Successfully.";
            divAlert.Visible = false;
        }
    }
}