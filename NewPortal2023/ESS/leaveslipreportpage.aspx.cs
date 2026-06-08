using DocumentFormat.OpenXml.Packaging;
using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Web.UI;

namespace NewPortal2023.ESS
{
    public partial class leaveslipreportpage : System.Web.UI.Page
    {
        LeaveApplication objInv = new LeaveApplication();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYear();
            }
        }

        private void BindYear()
        {
            ddlYear.Items.Clear();
            ddlYear.Items.Add("--Select Year--");

            int currentYear = DateTime.Now.Year;

            for (int i = 2021; i <= currentYear; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                string reportType = ddlreportdrop.SelectedValue;
                string empId = txtReportInput.Text.Trim();
                string fromDate = txtFromDate1.Text.Trim();
                string toDate = txtToDate1.Text.Trim();
                string month = ddlMonth.SelectedValue;
                string compId = (string)Session["sCompID"];

                // Validation
                if (string.IsNullOrEmpty(reportType))
                {
                    ShowMessage("Please select Report Type");
                    return;
                }

                if (reportType == "Employeewise" && string.IsNullOrEmpty(empId))
                {
                    ShowMessage("Please enter Employee ID");
                    return;
                }

                if (string.IsNullOrEmpty(fromDate) || string.IsNullOrEmpty(toDate))
                {
                    ShowMessage("Please select From and To Date");
                    return;
                }

                if (string.IsNullOrEmpty(month))
                {
                    ShowMessage("Please select Month");
                    return;
                }

                //Extract Year from FromDate
                string year = DateTime.ParseExact(fromDate, "dd-MM-yyyy", null).Year.ToString();

                string exportPath = Server.MapPath("~/Export/");

                if (!Directory.Exists(exportPath))
                {
                    Directory.CreateDirectory(exportPath);
                }


                if (reportType == "Employeewise")
                {
                    foreach (FileInfo file in new DirectoryInfo(exportPath).GetFiles("*.pdf"))
                    {
                        file.Delete();
                    }

                    GeneratePDF(compId, empId, fromDate, toDate, month, year);

                    string fileName = empId + "_LeaveSlip_" + month + "_" + year + ".pdf";
                    string filePath = Path.Combine(exportPath, fileName);

                    if (File.Exists(filePath))
                    {
                        Response.Clear();
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                        Response.WriteFile(filePath);
                        Response.End();
                    }
                }


                else if (reportType == "All")
                {
                    DataSet dsEmp = objInv.getEmpId(compId);

                    if (dsEmp != null && dsEmp.Tables.Count > 0)
                    {
                        foreach (FileInfo file in new DirectoryInfo(exportPath).GetFiles("*.pdf"))
                        {
                            file.Delete();
                        }

                        for (int i = 0; i < dsEmp.Tables[0].Rows.Count; i++)
                        {
                            string emp = dsEmp.Tables[0].Rows[i]["EMP_MID"].ToString();

                            GeneratePDF(compId, emp, fromDate, toDate, month, year);
                        }

                        // ZIP creation
                        string zipPath = Server.MapPath("~/LeaveSlips.zip");

                        if (File.Exists(zipPath))
                        {
                            File.SetAttributes(zipPath, FileAttributes.Normal);
                            File.Delete(zipPath);
                        }

                        System.IO.Compression.ZipFile.CreateFromDirectory(exportPath, zipPath);

                        Response.Clear();
                        Response.ContentType = "application/zip";
                        Response.AddHeader("content-disposition", "attachment;filename=LeaveSlips.zip");
                        Response.WriteFile(zipPath);
                        Response.End();
                    }
                    else
                    {
                        ShowMessage("No employees found");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessage(ex.Message);
            }
        }


        private void GeneratePDF(string compId, string empId, string fromDate, string toDate, string month, string year)
        {
            try
            {
                //    DataSet ds = objInv.GETLEAVESLIP(compId, empId, fromDate, toDate, month);

                //    if (ds == null)
                //    {
                //        throw new Exception("DataSet is NULL");
                //    }

                //    if (ds.Tables.Count < 5)
                //    {
                //        throw new Exception("Expected 5 tables but found : " + ds.Tables.Count);
                //    }

                //    string reportPath = Server.MapPath("~/Reports/SalaryslipReports.rdlc");



                //    if (!File.Exists(reportPath))
                //    {
                //        throw new Exception("RDLC file not found : " + reportPath);
                //    }

                //    rptPrint.Visible = false;

                //    rptPrint.LocalReport.DataSources.Clear();
                //    rptPrint.ProcessingMode = ProcessingMode.Local;
                //    rptPrint.LocalReport.ReportPath = reportPath;

                ////    ReportParameter[] param = new ReportParameter[]
                ////{
                ////    new ReportParameter("FromDate", fromDate),
                ////    new ReportParameter("ToDate", toDate)
                ////};

                ////    rptPrint.LocalReport.SetParameters(param);

                //    rptPrint.LocalReport.DataSources.Add(
                //        new ReportDataSource("Dsleaveslip", ds.Tables[0]));

                //    rptPrint.LocalReport.DataSources.Add(
                //        new ReportDataSource("Dsleaveslipdata", ds.Tables[1]));

                //    rptPrint.LocalReport.DataSources.Add(
                //        new ReportDataSource("DsAttendanceAbsentData", ds.Tables[2]));

                //    rptPrint.LocalReport.DataSources.Add(
                //        new ReportDataSource("DsTotalAbsentDays", ds.Tables[3]));

                //    rptPrint.LocalReport.DataSources.Add(
                //        new ReportDataSource("DsClosingbal", ds.Tables[4]));

                //    rptPrint.LocalReport.Refresh();

                //    byte[] bytes = rptPrint.LocalReport.Render(
                //        "PDF",
                //        null,
                //        out string contentType,
                //        out string encoding,
                //        out string extension,
                //        out string[] streamIds,
                //        out Warning[] warnings);

                //    string folderPath = Server.MapPath("~/Export/");

                //    if (!Directory.Exists(folderPath))
                //    {
                //        Directory.CreateDirectory(folderPath);
                //    }

                //    string filePath = Path.Combine(
                //        folderPath,
                //        empId + "_LeaveSlip_" + month + "_" + year + ".pdf");

                //    File.WriteAllBytes(filePath, bytes);

                //    Response.Write("<br/>PDF Generated Successfully");
                DataSet ds = objInv.GETLEAVESLIP(compId, empId, fromDate, toDate, month);

                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {
                    return;
                }

                rptPrint.Visible = false;

                rptPrint.LocalReport.DataSources.Clear();
                rptPrint.ProcessingMode = ProcessingMode.Local;
                rptPrint.LocalReport.ReportPath = @"Reports/SalaryslipReports.rdlc";


                //ReportParameter[] param = new ReportParameter[]
                //{
                //new ReportParameter("FromDate", fromDate),
                //new ReportParameter("ToDate", toDate)
                //};

                //rptPrint.LocalReport.SetParameters(param);


                rptPrint.LocalReport.DataSources.Add(new ReportDataSource("Dsleaveslip", ds.Tables[0]));
                rptPrint.LocalReport.DataSources.Add(new ReportDataSource("Dsleaveslipdata", ds.Tables[1]));
                rptPrint.LocalReport.DataSources.Add(new ReportDataSource("DsAttendanceAbsentData", ds.Tables[2]));
                rptPrint.LocalReport.DataSources.Add(new ReportDataSource("DsTotalAbsentDays", ds.Tables[3]));
                rptPrint.LocalReport.DataSources.Add(new ReportDataSource("DsClosingbal", ds.Tables[4]));
                rptPrint.LocalReport.DataSources.Add(new ReportDataSource("DsDates", ds.Tables[5]));

                rptPrint.LocalReport.Refresh();


                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string ext;

                byte[] bytes = rptPrint.LocalReport.Render("PDF", null, out contentType, out encoding, out ext, out streamIds, out warnings);

                string folderPath = Server.MapPath("~/Export/");
                string filePath = folderPath + empId + "_LeaveSlip_" + month + "_" + year + ".pdf";

                File.WriteAllBytes(filePath, bytes);
            }
            catch (Exception ex)
            {
                Response.Write("<pre>");

                Response.Write("ERROR :\n\n");
                Response.Write(ex.ToString());

                Exception inner = ex.InnerException;

                while (inner != null)
                {
                    Response.Write("\n\n==================== INNER EXCEPTION ====================\n\n");
                    Response.Write(inner.ToString());

                    inner = inner.InnerException;
                }

                Response.Write("</pre>");
            }
        }

        //private void GeneratePDF(string compId, string empId, string fromDate, string toDate, string month, string year)
        //{
        //    try
        //    {
        //        DataSet ds = objInv.GETLEAVESLIP(compId, empId, fromDate, toDate, month);

        //        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        //        {
        //            return;
        //        }

        //        GenerateZipForAll();


        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("<pre>");

        //        Response.Write("ERROR :\n\n");
        //        Response.Write(ex.ToString());

        //        Exception inner = ex.InnerException;

        //        while (inner != null)
        //        {
        //            Response.Write("\n\n==================== INNER EXCEPTION ====================\n\n");
        //            Response.Write(inner.ToString());

        //            inner = inner.InnerException;
        //        }

        //        Response.Write("</pre>");
        //    }
        //}

        //private void GenerateZipForAll(DataSet dt)
        //{
        //    // 🔹 Final storage folder (like single)
        //    string mainFolder = Server.MapPath("~/GeneratedLetters/" + letterType + "/");

        //    if (!Directory.Exists(mainFolder))
        //        Directory.CreateDirectory(mainFolder);

        //    // 🔹 Temp folder only for ZIP collection
        //    string tempFolder = Server.MapPath("~/GeneratedLetters/TempLetters/");

        //    if (Directory.Exists(tempFolder))
        //        Directory.Delete(tempFolder, true);

        //    Directory.CreateDirectory(tempFolder);

        //    foreach (DataRow row in dt.Rows)
        //    {
        //        // 🔹 Generate Word in respective folder
        //        string docPath = GenerateWord(row, letterType);

        //        // 🔹 Convert to PDF (same folder as docx)
        //        string pdfPath = Path.ChangeExtension(docPath, ".pdf");
        //        ConvertToPdf(docPath, pdfPath);

        //        // 🔹 Delete DOCX (optional)
        //        File.Delete(docPath);

        //        // 🔹 Copy PDF to temp folder for ZIP
        //        string tempPdfPath = Path.Combine(tempFolder, Path.GetFileName(pdfPath));
        //        File.Copy(pdfPath, tempPdfPath, true);
        //    }

        //    // 🔥 Create ZIP outside temp folder
        //    string zipFileName = "Letters_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".zip";
        //    string zipPath = Server.MapPath("~/GeneratedLetters/" + zipFileName);

        //    ZipFile.CreateFromDirectory(tempFolder, zipPath);

        //    // 🔹 Download ZIP
        //    Response.Clear();
        //    Response.ContentType = "application/zip";
        //    Response.AddHeader("content-disposition", "attachment;filename=" + zipFileName);
        //    Response.WriteFile(zipPath);
        //    Response.Flush();
        //    Response.End();

        //    // 🔹 Cleanup (optional)
        //    System.Threading.Tasks.Task.Run(() =>
        //    {
        //        System.Threading.Thread.Sleep(3000);
        //        try
        //        {
        //            if (Directory.Exists(tempFolder))
        //                Directory.Delete(tempFolder, true);
        //        }
        //        catch { }
        //    });
        //}

        //private string GenerateWord(DataRow row, string letterType)
        //{
        //    string outputPath = "";
        //    string templatePath = "";
        //    string fileName = row["EMP_CODE"] + "_" + letterType + ".docx";

        //    // 🔹 Template selection
        //    switch (letterType)
        //    {
        //        case "Increment":
        //            templatePath = Server.MapPath("~/Templates/Increment.docx");
        //            break;

        //        case "NoIncrement":
        //            templatePath = Server.MapPath("~/Templates/NoIncrement.docx");
        //            break;

        //        case "Promotion":
        //            templatePath = Server.MapPath("~/Templates/Promotion.docx");
        //            break;

        //        case "Realignment":
        //            templatePath = Server.MapPath("~/Templates/Realignment.docx");
        //            break;
        //    }

        //    // 🔹 Folder path (dynamic)
        //    string folderPath = "~/GeneratedLetters/" + letterType + "/";
        //    outputPath = Server.MapPath(folderPath + fileName);



        //    File.Copy(templatePath, outputPath, true);

        //    using (WordprocessingDocument doc = WordprocessingDocument.Open(outputPath, true))
        //    {
        //        string docText = "";

        //        using (StreamReader sr = new StreamReader(doc.MainDocumentPart.GetStream()))
        //        {
        //            docText = sr.ReadToEnd();
        //        }


        //        if (letterType == "Increment")
        //        {
        //            docText = docText.Replace("«issued_date»", row["ISSUEDDATE"] != DBNull.Value ? Convert.ToDateTime(row["ISSUEDDATE"]).ToString("dd-MMM-yyyy") : "");
        //            docText = docText.Replace("«TITLE»", row["TITLE"]?.ToString() ?? "");
        //            docText = docText.Replace("«EMPLOYEE_NAME»", row["EMPLOYEE_NAME"]?.ToString() ?? "");
        //            docText = docText.Replace("«code»", row["EMP_CODE"]?.ToString() ?? "");


        //            //docText = docText.Replace("«rating»", row["PERFORMANCERATING"]?.ToString() ?? "");

        //            double rating = 0;

        //            double.TryParse(row["PERFORMANCERATING"]?.ToString(), out rating);

        //            string ratingText = "";

        //            if (rating >= 0.50 && rating <= 1.49)
        //            {
        //                ratingText = "Rating Scale 1 – Did Not Meet Expectation";
        //            }
        //            else if (rating >= 1.50 && rating <= 2.49)
        //            {
        //                ratingText = "Rating Scale 2 – Often Did Not Meet Expectation";
        //            }
        //            else if (rating >= 2.50 && rating <= 3.49)
        //            {
        //                ratingText = "Rating Scale 3 – Meets Expectation";
        //            }
        //            else if (rating >= 3.50 && rating <= 4.49)
        //            {
        //                ratingText = "Rating Scale 4 – Often Exceeds Expectation";
        //            }
        //            else if (rating >= 4.50)
        //            {
        //                ratingText = "Rating Scale 5 – Far Exceeds Expectation";
        //            }

        //            string finalRatingText =
        //                rating.ToString("0.00") + " (" + ratingText + ")";

        //            docText = docText.Replace("«rating»", finalRatingText);

        //            docText = docText.Replace("«rpay»", FormatINR(row["REVISEDCTC"]));
        //            docText = docText.Replace("«pay»", FormatINR(row["VARIABLEPAY"]));

        //            docText = docText.Replace("«designation»", row["DESIGNATION"]?.ToString() ?? "");
        //            docText = docText.Replace("«level»", row["LEVEL"]?.ToString() ?? "");

        //            docText = docText.Replace("«annualctc»", FormatINR(row["ANNUALCTC"]));
        //            docText = docText.Replace("«annvarpay»", FormatINR(row["ANNUALVARIABLEPAY"]));
        //            docText = docText.Replace("«ANNUALFIXEDCTC»", FormatINR(row["ANNUALFIXEDCTC"]));
        //            docText = docText.Replace("«MONTHLYFIXEDCTC»", FormatINR(row["MONTHLYFIXEDCTC"]));

        //            docText = docText.Replace("«basic»", FormatINR(row["BASIC"]));
        //            docText = docText.Replace("«HRA»", FormatINR(row["HRA"]));
        //            docText = docText.Replace("«bonus»", FormatINR(row["BONUS"]));
        //            docText = docText.Replace("«MEALALLOWANCE»", FormatINR(row["MEALALLOWANCE"]));

        //            docText = docText.Replace("«NPS»", FormatINR(row["NPS"]));
        //            docText = docText.Replace("«LTA»", FormatINR(row["LTA"]));
        //            docText = docText.Replace("«SPECIALALLOWANCE»", FormatINR(row["SPECIALALLOWANCE"]));

        //            docText = docText.Replace("«MONTHLYPAYOUT»", FormatINR(row["MONTHLYPAYOUT"]));
        //            docText = docText.Replace("«PFEMPLOYER»", FormatINR(row["PFEMPLOYER"]));
        //            docText = docText.Replace("«MONTHLYGROSS»", FormatINR(row["MONTHLYGROSS"]));
        //        }

        //        using (StreamWriter sw = new StreamWriter(doc.MainDocumentPart.GetStream(FileMode.Create)))
        //        {
        //            sw.Write(docText);
        //        }
        //    }

        //    //// 🔹 Download
        //    //Response.Clear();
        //    //Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
        //    //Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
        //    //Response.WriteFile(outputPath);
        //    //Response.End();

        //    return outputPath;
        //}

        //private void ConvertToPdf(string docPath, string pdfPath)
        //{
        //    Word.Application wordApp = new Word.Application();
        //    Word.Document doc = null;

        //    try
        //    {
        //        doc = wordApp.Documents.Open(docPath);
        //        doc.ExportAsFixedFormat(pdfPath, Word.WdExportFormat.wdExportFormatPDF);
        //    }
        //    finally
        //    {
        //        if (doc != null)
        //        {
        //            doc.Close(false);
        //        }
        //        wordApp.Quit();
        //    }
        //}

        //private void GeneratePDF(string compId, string empId, string fromDate, string toDate, string month, string year)
        //{
        //    try
        //    {
        //        DataSet ds = objInv.GETLEAVESLIP(compId, empId, fromDate, toDate, month);

        //        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        //        {
        //            return;
        //        }


        //        //string reportPath = Server.MapPath("~/Reports/SalaryslipReport.rdlc");

        //        //Response.Write(reportPath);
        //        ////string fillpaths = 
        //        ///*lblMessage.Text=*/
        //        //Boolean files = File.Exists(reportPath);
        //        //rptPrint.Visible = false;

        //        rptPrint.LocalReport.DataSources.Clear();
        //        rptPrint.ProcessingMode = ProcessingMode.Local;
        //        rptPrint.LocalReport.ReportPath = @"Reports/SalaryslipReport.rdlc";


        //        //ReportParameter[] param = new ReportParameter[]
        //        //{
        //        //    new ReportParameter("FromDate", fromDate),
        //        //    new ReportParameter("ToDate", toDate)
        //        //};

        //        //rptPrint.LocalReport.SetParameters(param);


        //        rptPrint.LocalReport.DataSources.Add(new ReportDataSource("Dsleaveslip", ds.Tables[0]));
        //        rptPrint.LocalReport.DataSources.Add(new ReportDataSource("Dsleaveslipdata", ds.Tables[1]));
        //        rptPrint.LocalReport.DataSources.Add(new ReportDataSource("DsAttendanceAbsentData", ds.Tables[2]));
        //        rptPrint.LocalReport.DataSources.Add(new ReportDataSource("DsTotalAbsentDays", ds.Tables[3]));
        //        rptPrint.LocalReport.DataSources.Add(new ReportDataSource("DsClosingbal", ds.Tables[4]));

        //        rptPrint.LocalReport.Refresh();


        //        //Warning[] warnings;
        //        //string[] streamIds;
        //        //string contentType;
        //        //string encoding;
        //        //string ext;


        //        byte[] bytes = rptPrint.LocalReport.Render("PDF", null, out string contentType, out string encoding, out string extension, out string[] streamIds, out Warning[] warnings);

        //        //byte[] bytes = rptPrint.LocalReport.Render("PDF", null, out contentType, out encoding, out ext, out streamIds, out warnings);

        //        string folderPath = Server.MapPath("~/Export/");
        //        string filePath = folderPath + empId + "_LeaveSlip_" + month + "_" + year + ".pdf";

        //        File.WriteAllBytes(filePath, bytes);
        //    }
        //    catch(Exception ex)
        //    {
        //        Response.Write("<pre>");
        //        Response.Write(ex.ToString());

        //        if (ex.InnerException != null)
        //        {
        //            Response.Write("<br/><br/>INNER:<br/>");
        //            Response.Write(ex.InnerException.ToString());
        //        }

        //        Response.Write("</pre>");
        //    }

        //}

        private void ShowMessage(string message)
        {
            lblMessage.Text = message;
            divAlert.Visible = true;
        }
    }
}