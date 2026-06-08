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
using DocumentFormat.OpenXml.Spreadsheet;
using System.Web.Services.Description;
using DocumentFormat.OpenXml.Office.Word;

namespace NewPortal2023.ESS
{
    public partial class PMSReport : System.Web.UI.Page
    {
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();

        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    FillFinYear();
                }
            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void FillFinYear()
        {
            DataSet ds = new DataSet();

            ds = objAppraisal.GetYears((string)Session["sCompID"]);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PYDESC"].ToString() != "")
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(row["PYDESC"].ToString()))
                        {
                            drpFinancialYear.Items.Add(new ListItem(row["PYDESC"].ToString(), row["PYDESC"].ToString()));
                        }
                    }
                }
            }

            string currentFinancialYear = ds.Tables[1].Rows[0]["CYDESC"].ToString();
            string nextFinancialYear = ds.Tables[2].Rows[0]["NYDESC"].ToString();

            // Add values to the dropdown
            drpFinancialYear.Items.Add(new ListItem(currentFinancialYear, currentFinancialYear));
            drpFinancialYear.Items.Add(new ListItem(nextFinancialYear, nextFinancialYear));
        }

        protected void drpFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            if (drpFinancialYear.SelectedIndex == 0)
            {
                divKRAFin.Visible = false;
            }
            else
            {
                divKRAFin.Visible = true;
            }

            ds = objAppraisal.GetYears((string)Session["sCompID"]);

            string currentFinancialYear = ds.Tables[1].Rows[0]["CYDESC"].ToString();
            string nextFinancialYear = ds.Tables[2].Rows[0]["NYDESC"].ToString();

            if (drpFinancialYear.SelectedValue == currentFinancialYear)
            {
                lblCycle.Text = "Appraisal Cycle";
                ViewState["CYCLE"] = "Appraisal Cycle";
                ViewState["CYCLE_AID"] = "C1";
            }
            else if (drpFinancialYear.SelectedValue == nextFinancialYear)
            {
                lblCycle.Text = "KRA Cycle";
                ViewState["CYCLE"] = "KRA Cycle";
                ViewState["CYCLE_AID"] = "C0";
            }
            else
            {
                lblCycle.Text = "";
                ViewState["CYCLE"] = "";
                ViewState["CYCLE_AID"] = "";
            }

            ViewState["Year"] = drpFinancialYear.SelectedValue;

            fillDepartment();
            divEmp.Visible = false;
            divPMS.Visible = false;
            divPMSAll.Visible = false;

        }

        private void fillDepartment()
        {
            ds = objAppraisal.FillDepartment();
            drpDptList.Items.Clear();
            drpDptList.DataTextField = "NAME";
            drpDptList.DataValueField = "CODE";
            drpDptList.DataSource = ds.Tables[0];
            drpDptList.DataBind();
            drpDptList.Items.Insert(0, new ListItem("[Select One]", "0"));

        }

        protected void drpDptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEmpCode.Visible = true;

            if (drpDptList.SelectedValue == "0")
            {
                txtEmpCode.Visible = true;
                drpEmpCode.Visible = false;

            }
            else
            {
                txtEmpCode.Visible = false;
                drpEmpCode.Visible = true;
                fillemployee();
            }

        }

        private void fillemployee()
        {
            objAppraisal.DeptID = drpDptList.SelectedValue;
            ds = objAppraisal.Fillemployee();
            drpEmpCode.Items.Clear();
            drpEmpCode.DataTextField = "EMPCODE";
            drpEmpCode.DataValueField = "CODE";
            drpEmpCode.DataSource = ds.Tables[0];
            drpEmpCode.DataBind();
            drpEmpCode.Items.Insert(0, new ListItem("[Select One]", "0"));
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpType.SelectedValue == "")
            {
                divEmp.Visible = false;
                divPMSAll.Visible = true;
                divPMS.Visible = false;
            }
            else
            {
                divEmp.Visible = true;
                divPMS.Visible = true;
                divPMSAll.Visible = false;
            }
        }

        protected void drpEmpCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void drpReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            drpType.SelectedValue = "";

            if (drpReportType.SelectedValue != "")
            {
                ViewState["REPORTTYPE"] = drpReportType.SelectedValue;
                divEmpType.Visible = true;
                divEmp.Visible = false;
                divPMS.Visible = false;
                divPMSAll.Visible = true;
            }
            else
            {
                drpType.SelectedValue = "";
                divEmpType.Visible = false;
                divEmp.Visible = false;
                divPMSAll.Visible = false;
                divPMS.Visible = false;
            }
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string reporttype = ViewState["REPORTTYPE"].ToString();
            string Emp_Code;

            if (txtEmpCode.Text == "")
            {
                Emp_Code = drpEmpCode.SelectedValue;
            }
            else
            {
                Emp_Code = txtEmpCode.Text;
            }

            objAppraisal.Year = ViewState["Year"].ToString();

            ds = objAppraisal.GetPMSReport((string)Session["sCompID"], Emp_Code);

            ReportDataSource datasource1 = new ReportDataSource("dsIndividual", ds.Tables[0]);
            ReportDataSource datasource2 = new ReportDataSource("dsKeyAccomplishment", ds.Tables[1]);
            ReportDataSource datasource3 = new ReportDataSource("dsTrainAndDev", ds.Tables[2]);
            ReportDataSource datasource4 = new ReportDataSource("dsPLPPayout", ds.Tables[3]);
            ReportDataSource datasource5 = new ReportDataSource("dsOverallRating", ds.Tables[4]);

            if (reporttype == "KRA")
            {
                rptPrint.Visible = true;
                rptPrint.LocalReport.DataSources.Clear();
                rptPrint.LocalReport.ReportPath = @"Reports/IndividualKRA.rdlc";
                rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
                rptPrint.LocalReport.DataSources.Add(datasource1);
                rptPrint.LocalReport.Refresh();
            }
            else if (reporttype == "KeyAccomp")
            {
                rptPrint.Visible = true;
                rptPrint.LocalReport.DataSources.Clear();
                rptPrint.LocalReport.ReportPath = @"Reports/KeyAccomplishment.rdlc";
                rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
                rptPrint.LocalReport.DataSources.Add(datasource2);
                rptPrint.LocalReport.Refresh();
            }
            else if (reporttype == "TrainAndDevlp")
            {
                rptPrint.Visible = true;
                rptPrint.LocalReport.DataSources.Clear();
                rptPrint.LocalReport.ReportPath = @"Reports/TrainingAndDevelopment.rdlc";
                rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
                rptPrint.LocalReport.DataSources.Add(datasource3);
                rptPrint.LocalReport.Refresh();
            }
            else if (reporttype == "PLPPayout")
            {
                rptPrint.Visible = true;
                rptPrint.LocalReport.DataSources.Clear();
                rptPrint.LocalReport.ReportPath = @"Reports/PLPReport.rdlc";
                rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
                rptPrint.LocalReport.DataSources.Add(datasource4);
                rptPrint.LocalReport.Refresh();
            }
            else if (reporttype == "OverallRating")
            {
                rptPrint.Visible = true;
                rptPrint.LocalReport.DataSources.Clear();
                rptPrint.LocalReport.ReportPath = @"Reports/OverallRating.rdlc";
                rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
                rptPrint.LocalReport.DataSources.Add(datasource5);
                rptPrint.LocalReport.Refresh();
            }

            //rptPrint.LocalReport.ReportPath = @"Reports/PMSReport.rdlc";
            //rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
            //rptPrint.LocalReport.DataSources.Add(datasource1);
            //rptPrint.LocalReport.DataSources.Add(datasource2);
            //rptPrint.LocalReport.DataSources.Add(datasource3);
            //rptPrint.LocalReport.DataSources.Add(datasource4);
            //rptPrint.LocalReport.DataSources.Add(datasource5);

            rptPrint.LocalReport.Refresh();
            //gvLeave.DataSource = ds;
            //gvLeave.DataBind();
        }

        protected void btnGenerateReportAllEmp_Click(object sender, EventArgs e)
        {
            objAppraisal.Year = ViewState["Year"].ToString();
            ds = objAppraisal.GetEmpCode((string)Session["sCompID"]);
            DataTable dt = ds.Tables[0];

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count == 0)
            {
                objcommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                string message = "No Employee Found";
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = "No Employee Found";
            }
            else
            {
                string folderPath = Server.MapPath("~/EmployeeReports/");
                Directory.CreateDirectory(folderPath);

                foreach (string file in Directory.GetFiles(folderPath))
                {
                    File.Delete(file);
                }

                foreach (DataRow row in dt.Rows)
                {
                    string reporttype = ViewState["REPORTTYPE"].ToString();
                    string Emp_Code = row["EMP_MID"].ToString();
                    objAppraisal.Year = ViewState["Year"].ToString();

                    DataSet empDS = objAppraisal.GetPMSReport((string)Session["sCompID"], Emp_Code);

                    ReportDataSource datasource1 = new ReportDataSource("dsIndividual", empDS.Tables[0]);
                    ReportDataSource datasource2 = new ReportDataSource("dsKeyAccomplishment", empDS.Tables[1]);
                    ReportDataSource datasource3 = new ReportDataSource("dsTrainAndDev", empDS.Tables[2]);
                    ReportDataSource datasource4 = new ReportDataSource("dsPLPPayout", empDS.Tables[3]);
                    ReportDataSource datasource5 = new ReportDataSource("dsOverallRating", empDS.Tables[4]);

                    if (reporttype == "KRA")
                    {
                        rptPrint.Visible = true;
                        rptPrint.LocalReport.DataSources.Clear();
                        rptPrint.LocalReport.ReportPath = @"Reports/IndividualKRA.rdlc";
                        rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
                        rptPrint.LocalReport.DataSources.Add(datasource1);
                        rptPrint.LocalReport.Refresh();
                    }
                    else if (reporttype == "KeyAccomp")
                    {
                        rptPrint.Visible = true;
                        rptPrint.LocalReport.DataSources.Clear();
                        rptPrint.LocalReport.ReportPath = @"Reports/KeyAccomplishment.rdlc";
                        rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
                        rptPrint.LocalReport.DataSources.Add(datasource2);
                        rptPrint.LocalReport.Refresh();
                    }
                    else if (reporttype == "TrainAndDevlp")
                    {
                        rptPrint.Visible = true;
                        rptPrint.LocalReport.DataSources.Clear();
                        rptPrint.LocalReport.ReportPath = @"Reports/TrainingAndDevelopment.rdlc";
                        rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
                        rptPrint.LocalReport.DataSources.Add(datasource3);
                        rptPrint.LocalReport.Refresh();
                    }
                    else if (reporttype == "PLPPayout")
                    {
                        rptPrint.Visible = true;
                        rptPrint.LocalReport.DataSources.Clear();
                        rptPrint.LocalReport.ReportPath = @"Reports/PLPReport.rdlc";
                        rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
                        rptPrint.LocalReport.DataSources.Add(datasource4);
                        rptPrint.LocalReport.Refresh();
                    }
                    else if (reporttype == "OverallRating")
                    {
                        rptPrint.Visible = true;
                        rptPrint.LocalReport.DataSources.Clear();
                        rptPrint.LocalReport.ReportPath = @"Reports/OverallRating.rdlc";
                        rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
                        rptPrint.LocalReport.DataSources.Add(datasource5);
                        rptPrint.LocalReport.Refresh();
                    }

                    //rptPrint.Visible = true;
                    //rptPrint.LocalReport.DataSources.Clear();
                    //rptPrint.LocalReport.ReportPath = @"Reports/PMSReport.rdlc";
                    //rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
                    //rptPrint.LocalReport.DataSources.Add(datasource1);
                    //rptPrint.LocalReport.DataSources.Add(datasource2);
                    //rptPrint.LocalReport.DataSources.Add(datasource3);
                    //rptPrint.LocalReport.DataSources.Add(datasource4);
                    //rptPrint.LocalReport.DataSources.Add(datasource5);
                    //rptPrint.LocalReport.Refresh();

                    byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out string contentType, out string encoding, out string extension, out string[] streamIds, out Warning[] warnings);

                    //string folderPath = Server.MapPath("~/EmployeeReports/");
                    //Directory.CreateDirectory(folderPath);

                    string fileName = "PMS_REPORT_" + Emp_Code + ".xls";
                    string filePath = Path.Combine(folderPath, fileName);
                    File.WriteAllBytes(filePath, bytes);
                }

                string reportsFolder = Server.MapPath("~/EmployeeReports/");
                string zipFilePath = Server.MapPath("~/DownloadPMS/EmployeeReports.zip");

                using (FileStream zipFile = new FileStream(zipFilePath, FileMode.Create))
                {
                    using (ZipArchive archive = new ZipArchive(zipFile, ZipArchiveMode.Create))
                    {
                        string[] reportFiles = Directory.GetFiles(reportsFolder, "*.xls");

                        foreach (string file in reportFiles)
                        {
                            string fileName = Path.GetFileName(file);
                            ZipArchiveEntry entry = archive.CreateEntry(fileName);

                            using (FileStream sourceStream = new FileStream(file, FileMode.Open))
                            using (Stream targetStream = entry.Open())
                            {
                                sourceStream.CopyTo(targetStream);
                            }
                        }
                    }
                }

                objcommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                string message = "Report Generated Successfully";
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlert, "success");
                lblMessage.Text = "Report Generated Successfully";

                Response.Clear();
                Response.ContentType = "application/zip";
                Response.AppendHeader("content-disposition", "attachment; filename=AllEmployeePMSReports.zip");
                Response.WriteFile(zipFilePath);
                Response.End();

                File.Delete(zipFilePath);

            }
        }


        //protected void btnGenerateReportAllEmp_Click(object sender, EventArgs e)
        //{
        //    ds = objAppraisal.GetEmpCode();
        //    DataTable dt = ds.Tables[0];

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        string Emp_Code = row["EMP_MID"].ToString();
        //        DataSet empDS = objAppraisal.GetPMSReport((string)Session["sCompID"], Emp_Code);

        //        ReportDataSource datasource1 = new ReportDataSource("dsIndividual", empDS.Tables[0]);
        //        ReportDataSource datasource2 = new ReportDataSource("dsKeyAccomplishment", empDS.Tables[1]);
        //        ReportDataSource datasource3 = new ReportDataSource("dsTrainAndDev", empDS.Tables[2]);
        //        ReportDataSource datasource4 = new ReportDataSource("dsPLPPayout", empDS.Tables[3]);
        //        ReportDataSource datasource5 = new ReportDataSource("dsOverallRating", empDS.Tables[4]);

        //        rptPrint.Visible = true;
        //        rptPrint.LocalReport.DataSources.Clear();
        //        rptPrint.LocalReport.ReportPath = @"Reports/PMSReport.rdlc";
        //        rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
        //        rptPrint.LocalReport.DataSources.Add(datasource1);
        //        rptPrint.LocalReport.DataSources.Add(datasource2);
        //        rptPrint.LocalReport.DataSources.Add(datasource3);
        //        rptPrint.LocalReport.DataSources.Add(datasource4);
        //        rptPrint.LocalReport.DataSources.Add(datasource5);
        //        rptPrint.LocalReport.Refresh();

        //        byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out string contentType, out string encoding, out string extension, out string[] streamIds, out Warning[] warnings);

        //        string folderPath = Server.MapPath("~/EmployeeReports/");
        //        Directory.CreateDirectory(folderPath);

        //        string fileName = "PMS_REPORT_" + Emp_Code + ".xls";
        //        string filePath = Path.Combine(folderPath, fileName);
        //        File.WriteAllBytes(filePath, bytes);
        //    }

        //    string reportsFolder = Server.MapPath("~/EmployeeReports/");
        //    string zipFilePath = Server.MapPath("~/DownloadPMS/EmployeeReports.zip");

        //    using (FileStream zipFile = new FileStream(zipFilePath, FileMode.Create))
        //    {
        //        using (ZipArchive archive = new ZipArchive(zipFile, ZipArchiveMode.Create))
        //        {
        //            string[] reportFiles = Directory.GetFiles(reportsFolder, "*.xls");

        //            foreach (string file in reportFiles)
        //            {
        //                string fileName = Path.GetFileName(file);
        //                ZipArchiveEntry entry = archive.CreateEntry(fileName);

        //                using (FileStream sourceStream = new FileStream(file, FileMode.Open))
        //                using (Stream targetStream = entry.Open())
        //                {
        //                    sourceStream.CopyTo(targetStream);
        //                }
        //            }
        //        }
        //    }

        //    Response.Clear();
        //    Response.ContentType = "application/zip";
        //    Response.AppendHeader("content-disposition", "attachment; filename=AllEmployeePMSReports.zip");
        //    Response.WriteFile(zipFilePath);
        //    Response.End();

        //    File.Delete(zipFilePath);






        //    //string reportsFolder = Server.MapPath("~/EmployeeReports/");
        //    //Directory.CreateDirectory(reportsFolder);

        //    //ds = objAppraisal.GetEmpCode();
        //    //DataTable dt = ds.Tables[0];

        //    //foreach (DataRow row in dt.Rows)
        //    //{
        //    //    string Emp_Code = row["EMP_MID"].ToString();

        //    //    DataSet empDS = objAppraisal.GetPMSReport((string)Session["sCompID"], Emp_Code);

        //    //    ReportDataSource datasource1 = new ReportDataSource("dsIndividual", empDS.Tables[0]);
        //    //    ReportDataSource datasource2 = new ReportDataSource("dsKeyAccomplishment", empDS.Tables[1]);
        //    //    ReportDataSource datasource3 = new ReportDataSource("dsTrainAndDev", empDS.Tables[2]);
        //    //    ReportDataSource datasource4 = new ReportDataSource("dsPLPPayout", empDS.Tables[3]);
        //    //    ReportDataSource datasource5 = new ReportDataSource("dsOverallRating", empDS.Tables[4]);

        //    //    rptPrint.Visible = true;
        //    //    rptPrint.LocalReport.DataSources.Clear();
        //    //    rptPrint.LocalReport.ReportPath = @"Reports/PMSReport.rdlc";
        //    //    rptPrint.LocalReport.DisplayName = "PMS REPORT" + " (" + Emp_Code + ")";
        //    //    rptPrint.LocalReport.DataSources.Add(datasource1);
        //    //    rptPrint.LocalReport.DataSources.Add(datasource2);
        //    //    rptPrint.LocalReport.DataSources.Add(datasource3);
        //    //    rptPrint.LocalReport.DataSources.Add(datasource4);
        //    //    rptPrint.LocalReport.DataSources.Add(datasource5);
        //    //    rptPrint.LocalReport.Refresh();

        //    //    byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out string contentType, out string encoding, out string extension, out string[] streamIds, out Warning[] warnings);

        //    //    string folderPath = Server.MapPath("~/EmployeeReports/");
        //    //    Directory.CreateDirectory(folderPath);

        //    //    string fileName = "PMS_REPORT_" + Emp_Code + ".xls";
        //    //    string filePath = Path.Combine(folderPath, fileName);
        //    //    File.WriteAllBytes(filePath, bytes);

        //    //}

        //    //// Create a zip archive containing all files in the reports folder
        //    //string zipFilePath = Server.MapPath("~/TempReports.zip");
        //    //ZipFile.CreateFromDirectory(reportsFolder, zipFilePath);

        //    //// Clear any content from the response
        //    //Response.Clear();

        //    //// Set the content type to zip
        //    //Response.ContentType = "application/zip";

        //    //// Set the content-disposition header to force download
        //    //Response.AppendHeader("content-disposition", "attachment; filename=EmployeeReports.zip");

        //    //// Write the zip file to the response
        //    //Response.WriteFile(zipFilePath);

        //    //// End the response
        //    //Response.End();

        //    //// Delete the temporary zip file
        //    //File.Delete(zipFilePath);


        //}
    }
}