using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace NewPortal2023.ESS
{
    public partial class Print12BB : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objcommon;
        DataSet DsTest = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["sCompID"] == "CO000114")
            {
                Menu navMenu = (Menu)Master.FindControl("NavigationMenu");
                navMenu.Visible = false;
            }

            try
            {
                string exportOption = "Excel";
                RenderingExtension extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
                if (extension != null)
                {
                    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    fieldInfo.SetValue(extension, false);
                }
                exportOption = "Word";
                extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
                if (extension != null)
                {
                    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    fieldInfo.SetValue(extension, false);
                }

                lblMessagecreate.Text = "";
                if (!IsPostBack)
                {
                    NewPortal2023.ESS.Investments objBl = new NewPortal2023.ESS.Investments();
                    DsTest = objBl.Fill_Report((string)(Session["sEmpID"]), (string)(Session["sCompID"]));

                    rptPrint.Visible = true;
                    if ((string)Session["sCompID"] == "CO000056")
                    {
                        ReportDataSource datasource = new ReportDataSource("dsEmp_dtEmpInd", DsTest.Tables[0]);
                        rptPrint.LocalReport.DataSources.Clear();
                        rptPrint.LocalReport.ReportPath = @"ESS\ReportIndiafirst.rdlc";
                        rptPrint.LocalReport.DisplayName = (string)Session["sEmpCode"] + "_Form 124";
                        rptPrint.LocalReport.DataSources.Add(datasource);
                        rptPrint.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                        rptPrint.LocalReport.Refresh();
                    }
                    else
                    {
                        ReportDataSource datasource = new ReportDataSource("dsEmp_dtEmp", DsTest.Tables[0]);
                        rptPrint.LocalReport.DataSources.Clear();
                        rptPrint.LocalReport.ReportPath = @"ESS\Report12BB.rdlc";
                        rptPrint.LocalReport.DisplayName = (string)Session["sEmpCode"] + "_Form 124";
                        rptPrint.LocalReport.DataSources.Add(datasource);
                        rptPrint.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                        rptPrint.LocalReport.Refresh();
                    }

                    //if (Directory.Exists(Server.MapPath(hdnFolder.Value)) == false)
                    //{
                    //    Directory.CreateDirectory(Server.MapPath(hdnFolder.Value));
                    //}
                }
            }
            catch (Exception ex)
            {
                objcommon = new NewPortal2023.ESS.Common();
                lblMessagecreate.Text = ex.Message;
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString("Error occurred in application.") + "');");
            }
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("dsEmp_dt80C", DsTest.Tables[1]));
            e.DataSources.Add(new ReportDataSource("dsEmp_dt80CCD", DsTest.Tables[2]));
            e.DataSources.Add(new ReportDataSource("dsEmp_dtOtherSec", DsTest.Tables[3]));
            e.DataSources.Add(new ReportDataSource("dsEmp_dtForceVisible", DsTest.Tables[4]));
        }
    }
}