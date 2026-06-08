using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;

namespace NewPortal2023.ESS
{
    public partial class EmployeePassword : System.Web.UI.Page
    {
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        NewPortal2023.ESS.Common objCommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Support objInv = new NewPortal2023.ESS.Support();
        NewPortal2023.ESS.FlexiPay objFlexi = new NewPortal2023.ESS.FlexiPay();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        private string SourcePath = string.Empty;
        private string savePath = string.Empty;

        DataSet ds = new DataSet();
        string id;

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //    if (Session["sCompID"] != null)
        //    {
        //    if (!Page.IsPostBack)
        //    {
        //        if (Session["sCompID"] != null)
        //        {
        //            try
        //            {
        //                if (Session["id"] != "")
        //                {
        //                    DataSet dsAdmin = objFlexi.GetPaswords((string)Session["sCompID"]);
        //                    string exportOption = "PDF";
        //                    RenderingExtension extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
        //                    if (extension != null)
        //                    {
        //                        System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        //                        fieldInfo.SetValue(extension, false);
        //                    }
        //                    exportOption = "Word";
        //                    extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
        //                    if (extension != null)
        //                    {
        //                        System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        //                        fieldInfo.SetValue(extension, false);
        //                    }

        //                    rptPrint.Visible = true;

        //                    ReportDataSource datasourceEmployee = new ReportDataSource("dsEmployeePassword_dtEmp_Password", dsAdmin.Tables[0]);


        //                    rptPrint.LocalReport.DataSources.Clear();
        //                    rptPrint.ProcessingMode = ProcessingMode.Local;
        //                    rptPrint.LocalReport.ReportPath = @"ESS\GenrateEmployeePassword.rdlc";


        //                    rptPrint.LocalReport.DataSources.Add(datasourceEmployee);
        //                    rptPrint.LocalReport.Refresh();

        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                lblMessage.Text = ex.Message;
        //            }
        //        }

        //    }
        //    else
        //    {
        //        Response.Redirect("Login.aspx");
        //    }

        //    }


        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            //// Check if the session variable is null early
            //if (Session["sCompID"] == null)


            try
            {
                ds = objcommon.ProfIdOfEmp((string)Session["sCompID"], (string)Session["sEmpID"]);

                Session["PROF_ID"] = ds.Tables[0].Rows[0]["PROF_ID"].ToString();

                ds = objcommon.CheckMenuAccess((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["PROF_ID"]);

                // Get the current page's name (e.g., KISNAInvestmentDetailsAdmin.aspx)
                string currentPageName = Path.GetFileName(Request.Url.AbsolutePath);

                // Check if the MENU_URL matches the current page name
                bool accessGranted = false;

                // Iterate through all rows in the dataset
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["MENU_URL"].ToString() == currentPageName)
                    {
                        accessGranted = true; // If match found, access is granted
                        break; // Exit the loop once match is found
                    }
                }
                if (accessGranted)
                {

                    // This block ensures the report data loading happens only on first load
                    if (!Page.IsPostBack)
                    {
                        try
                        {
                            if (Session["id"] != "")
                            {
                                DataSet dsAdmin = objFlexi.GetPaswords((string)Session["sCompID"]);
                                string exportOption = "PDF";
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

                                rptPrint.Visible = true;

                                ReportDataSource datasourceEmployee = new ReportDataSource("dsEmployeePassword_dtEmp_Password", dsAdmin.Tables[0]);

                                rptPrint.LocalReport.DataSources.Clear();
                                rptPrint.ProcessingMode = ProcessingMode.Local;
                                rptPrint.LocalReport.ReportPath = @"ESS\GenrateEmployeePassword.rdlc";

                                rptPrint.LocalReport.DataSources.Add(datasourceEmployee);
                                rptPrint.LocalReport.Refresh();
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                        }
                    }
                }
                else
                {
                    // If access is denied, redirect to the previous page
                    string previousPage = Request.UrlReferrer?.ToString();

                    if (!string.IsNullOrEmpty(previousPage))
                    {
                        Response.Redirect(previousPage); // Redirect to the previous page
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                        return;
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                //Session["Error"] = ex.Message + "<br>Payslip.aspx";
                Session["Error"] = ex.Message + "<br>Payslip.aspx<br>" + Request.QueryString["key"].Replace(" ", "+");
                Response.Redirect("../ErrorPage.aspx", true);
            }

           
        }

    }
}
