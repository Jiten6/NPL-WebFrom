using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;


namespace NewPortal2023.ESS
{
    public partial class ReportsPB : System.Web.UI.Page
    {
        NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();
        NewPortal2023.ESS.Common objCommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();

        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //fillDropDown();
            }
        }

        public void fillDropDown()
        {
            //objExp.fillEmployee(drpEmpType);
            //objExp.fillDepartment(drpDepartmentype);
        }

        protected bool ValidateInputs()
        {

            //if (drpEmpType.SelectedValue.Trim() == "")
            //{
            //   // lblMessage.Text = "Select Employee.";
            //    string script = $@"<script type='text/javascript'>alert('Please select the Employee.');</script>";
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            //    return false;
            //}

            if (drpType.SelectedValue.Trim() == "")
            {
                // lblMessage.Text = "Select Expense Type.";
                string script = $@"<script type='text/javascript'>alert('Please select age Type.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;

            }
            return true;
        }

        protected bool ValidateInputs1()
        {
            if (drpTenure.SelectedValue.Trim() == "")
            {
                //lblMessage.Text = "Enter From Date.";
                string script = $@"<script type='text/javascript'>alert('Please select tenure');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            //if (drpEmpTypeTenure.SelectedValue.Trim() == "")
            //{
            //    string script = $@"<script type='text/javascript'>alert('Please select employee type.');</script>";
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            //    return false;
            //}
            return true;
        }

        protected bool ValidateInputs2()
        {
            if (ddlMonths.SelectedValue.Trim() == "")
            {
                //lblMessage.Text = "Enter From Date.";
                string script = $@"<script type='text/javascript'>alert('Please Select Date.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            if (drpEmpType3.SelectedValue.Trim() == "")
            {
                //lblMessage.Text = "Enter To Date.";
                string script = $@"<script type='text/javascript'>alert('Please Select Employee Type.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            return true;
        }

        protected bool ValidateInputs3()
        {
            if (DropDownList1.SelectedValue.Trim() == "")
            {
                //lblMessage.Text = "Enter From Date.";
                string script = $@"<script type='text/javascript'>alert('Please Select Date.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            //if (DropDownList2.SelectedValue.Trim() == "")
            //{
            //    //lblMessage.Text = "Enter To Date.";
            //    string script = $@"<script type='text/javascript'>alert('Please Select Employee Type.');</script>";
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            //    return false;
            //}
            return true;
        }

        protected bool ValidateInputs4()
        {
            if (DropDownList1.SelectedValue.Trim() == "")
            {
                string script = $@"<script type='text/javascript'>alert('Please Select Report');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }

            return true;
        }

        protected bool ValidateInputs5()
        {
            if (ddlmonthyear.SelectedValue.Trim() == "")
            {
                string script = $@"<script type='text/javascript'>alert('Please Select Year');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }

            return true;
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void drpType_Report_Selectedchanges(object sender, EventArgs e)
        {
            // Hide all panels initially
            clearFields();
            claimDetailPanel.Visible = false;
            approvedClaimPanel.Visible = false;
            //updatePaidClaimPanel.Visible = false;
            paidClaimReportPanel.Visible = false;
            PaidCtcpanel.Visible = false;
            Panelact.Visible = false;
            PanelNaA.Visible = false;
            Panelpreavg.Visible = false;
            PanelAttendence.Visible = false;

            if (ddlreportdrop.SelectedItem != null && ddlreportdrop.SelectedItem.Text != "")
            {
                string selectedOption = ddlreportdrop.SelectedItem.Text;

                // Show the panel based on the selected option
                if (selectedOption == "Employee Age Profiling")
                {
                    claimDetailPanel.Visible = true;
                }
                else if (selectedOption == "Employee Tenure")
                {
                    approvedClaimPanel.Visible = true;
                }
                else if (selectedOption == "Update Paid Claim")
                {
                    //updatePaidClaimPanel.Visible = true;
                }
                else if (selectedOption == "Ot Hours And Ot Amount")
                {
                    paidClaimReportPanel.Visible = true;
                }
                else if (selectedOption == "Paid CTC")
                {
                    PaidCtcpanel.Visible = true;
                }
                else if (selectedOption == "New joinee And Attrition")
                {
                    PanelNaA.Visible = true;
                }
                else if (selectedOption == "Monthly head count")
                {
                    Panelact.Visible = true;
                }
                else if (selectedOption == "Today's Attendence")
                {
                    PanelAttendence.Visible = true;
                }
                else if (selectedOption == "Previous Month Average Cost")
                {
                    Panelpreavg.Visible = true;
                }
            }
        }

        //protected void btnGenerate_Click(object sender, EventArgs e)
        //{
        //    if (ValidateInputs())
        //    {
        //        if (drpType.SelectedValue != "")
        //        {
        //            Warning[] warnings;
        //            string[] streamIds;
        //            string contentType;
        //            string encoding;
        //            string extension;
        //            if (drpType.SelectedValue == "Domestic")
        //            {
        //                if (drpEmpType.SelectedValue == "")
        //                {
        //                    lblMessage.Text = "";
        //                    objExp = new NewPortal2023.ESS.Expenses();

        //                    objExp.FromDate = txtFromDate.Text;
        //                    objExp.ToDate = txtToDate.Text;
        //                    objExp.Type = drpType.Text;
        //                    objExp.EmpCode = drpEmpType.Text;
        //                    ds = objExp.Fill_Report();

        //                    rptPrint.Visible = false;
        //                    ReportDataSource datasourceDomAll = new ReportDataSource("dsExpDoms", ds.Tables[0]);

        //                    rptPrint.LocalReport.DataSources.Clear();
        //                    rptPrint.LocalReport.ReportPath = @"Reports/DomesticReport.rdlc";
        //                    rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
        //                    rptPrint.LocalReport.Refresh();


        //                    byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

        //                    string fileName = $"DomesticReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

        //                    // Open generated PDF.
        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.Charset = "";
        //                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //                    Response.ContentType = contentType;
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=DomesticReport." + extension);
        //                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //                    Response.BinaryWrite(bytes);
        //                    Response.Flush();
        //                    Response.End();
        //                }
        //                else
        //                {
        //                    lblMessage.Text = "";
        //                    objExp = new NewPortal2023.ESS.Expenses();


        //                    objExp.FromDate = txtFromDate.Text;
        //                    objExp.ToDate = txtToDate.Text;
        //                    objExp.Type = drpType.Text;
        //                    objExp.EmpCode = drpEmpType.Text;
        //                    ds = objExp.Fill_Report();

        //                    rptPrint.Visible = false;
        //                    ReportDataSource datasourceDom = new ReportDataSource("dsExpDoms", ds.Tables[0]);

        //                    rptPrint.LocalReport.DataSources.Clear();
        //                    rptPrint.LocalReport.ReportPath = @"Reports/DomesticReport.rdlc";
        //                    rptPrint.LocalReport.DataSources.Add(datasourceDom);
        //                    rptPrint.LocalReport.Refresh();

        //                    //Export the RDLC Report to Byte Array.

        //                    byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

        //                    string fileName = $"DomesticReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

        //                    // Open generated PDF.
        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.Charset = "";
        //                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //                    Response.ContentType = contentType;
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
        //                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //                    Response.BinaryWrite(bytes);
        //                    Response.Flush();
        //                    Response.End();
        //                }
        //            }
        //            if (drpType.SelectedValue == "Local")
        //            {
        //                if (drpEmpType.SelectedValue == "All")
        //                {
        //                    lblMessage.Text = "";
        //                    objExp = new NewPortal2023.ESS.Expenses();

        //                    //string exportOption = "PDF";
        //                    //RenderingExtension extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
        //                    //if (extension != null)
        //                    //{
        //                    //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        //                    //    fieldInfo.SetValue(extension, false);
        //                    //}
        //                    //exportOption = "Word";
        //                    //extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
        //                    //if (extension != null)
        //                    //{
        //                    //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        //                    //    fieldInfo.SetValue(extension, false);
        //                    //}

        //                    objExp.FromDate = txtFromDate.Text;
        //                    objExp.ToDate = txtToDate.Text;
        //                    objExp.Type = drpType.Text;
        //                    objExp.EmpCode = drpEmpType.Text;
        //                    ds = objExp.Fill_Report();

        //                    rptPrint.Visible = false;
        //                    ReportDataSource datasourceLocAll = new ReportDataSource("dsExpLoc", ds.Tables[0]);

        //                    rptPrint.LocalReport.DataSources.Clear();
        //                    rptPrint.LocalReport.ReportPath = @"Reports/LocalReport.rdlc";
        //                    rptPrint.LocalReport.DataSources.Add(datasourceLocAll);
        //                    rptPrint.LocalReport.Refresh();

        //                    byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

        //                    string fileName = $"LocalReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

        //                    // Open generated PDF.
        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.Charset = "";
        //                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //                    Response.ContentType = contentType;
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=LocalReport." + extension);
        //                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //                    Response.BinaryWrite(bytes);
        //                    Response.Flush();
        //                    Response.End();

        //                }
        //                else
        //                {
        //                    lblMessage.Text = "";
        //                    objExp = new NewPortal2023.ESS.Expenses();


        //                    objExp.FromDate = txtFromDate.Text;
        //                    objExp.ToDate = txtToDate.Text;
        //                    objExp.Type = drpType.Text;
        //                    objExp.EmpCode = drpEmpType.Text;
        //                    ds = objExp.Fill_Report();

        //                    rptPrint.Visible = false;
        //                    ReportDataSource datasourceLoc = new ReportDataSource("dsExpLoc", ds.Tables[0]);

        //                    rptPrint.LocalReport.DataSources.Clear();
        //                    rptPrint.LocalReport.ReportPath = @"Reports/LocalReport.rdlc";
        //                    rptPrint.LocalReport.DataSources.Add(datasourceLoc);
        //                    rptPrint.LocalReport.Refresh();


        //                    byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

        //                    string fileName = $"LocalReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

        //                    // Open generated PDF.
        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.Charset = "";
        //                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //                    Response.ContentType = contentType;
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=LocalReport." + extension);
        //                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //                    Response.BinaryWrite(bytes);
        //                    Response.Flush();
        //                    Response.End();
        //                }
        //            }
        //            if (drpType.SelectedValue == "Telephone")
        //            {
        //                if (drpEmpType.SelectedValue == "All")
        //                {
        //                    lblMessage.Text = "";
        //                    objExp = new NewPortal2023.ESS.Expenses();


        //                    objExp.FromDate = txtFromDate.Text;
        //                    objExp.ToDate = txtToDate.Text;
        //                    objExp.Type = drpType.Text;
        //                    objExp.EmpCode = drpEmpType.Text;
        //                    ds = objExp.Fill_Report();

        //                    rptPrint.Visible = false;
        //                    ReportDataSource datasourceTelAll = new ReportDataSource("dsExpTel", ds.Tables[0]);

        //                    rptPrint.LocalReport.DataSources.Clear();
        //                    rptPrint.LocalReport.ReportPath = @"Reports/TeleReport.rdlc";
        //                    rptPrint.LocalReport.DataSources.Add(datasourceTelAll);
        //                    rptPrint.LocalReport.Refresh();


        //                    byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

        //                    string fileName = $"TeleReport_{txtFromDate.Text}_{txtToDate.Text}.xls";


        //                    // Open generated PDF.
        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.Charset = "";
        //                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //                    Response.ContentType = contentType;
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=TeleReport." + extension);
        //                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //                    Response.BinaryWrite(bytes);
        //                    Response.Flush();
        //                    Response.End();
        //                }
        //                else
        //                {
        //                    lblMessage.Text = "";
        //                    objExp = new NewPortal2023.ESS.Expenses();

        //                    objExp.FromDate = txtFromDate.Text;
        //                    objExp.ToDate = txtToDate.Text;
        //                    objExp.Type = drpType.Text;
        //                    objExp.EmpCode = drpEmpType.Text;
        //                    ds = objExp.Fill_Report();

        //                    rptPrint.Visible = false;
        //                    ReportDataSource datasourceTel = new ReportDataSource("dsExpTel", ds.Tables[0]);

        //                    rptPrint.LocalReport.DataSources.Clear();
        //                    rptPrint.LocalReport.ReportPath = @"Reports/TeleReport.rdlc";

        //                    rptPrint.LocalReport.DataSources.Add(datasourceTel);
        //                    rptPrint.LocalReport.Refresh();

        //                    byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

        //                    string fileName = $"TeleReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

        //                    // Open generated PDF.
        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.Charset = "";
        //                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //                    Response.ContentType = contentType;
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=TeleReport." + extension);
        //                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //                    Response.BinaryWrite(bytes);
        //                    Response.Flush();
        //                    Response.End();
        //                }

        //            }
        //            if (drpType.SelectedValue == "Miscellaneous")
        //            {
        //                if (drpEmpType.SelectedValue == "")
        //                {
        //                    lblMessage.Text = "";
        //                    objExp = new NewPortal2023.ESS.Expenses();

        //                    objExp.FromDate = txtFromDate.Text;
        //                    objExp.ToDate = txtToDate.Text;
        //                    objExp.Type = drpType.Text;
        //                    objExp.EmpCode = drpEmpType.Text;
        //                    ds = objExp.Fill_Report();

        //                    rptPrint.Visible = false;
        //                    ReportDataSource datasourceDomAll = new ReportDataSource("DataSet1", ds.Tables[0]);

        //                    rptPrint.LocalReport.DataSources.Clear();
        //                    rptPrint.LocalReport.ReportPath = @"Reports/ReportPettyExpenses.rdlc";
        //                    rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
        //                    rptPrint.LocalReport.Refresh();


        //                    byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

        //                    string fileName = $"Miscellaneous_Report_{txtFromDate.Text}_{txtToDate.Text}.xls";

        //                    // Open generated PDF.
        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.Charset = "";
        //                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //                    Response.ContentType = contentType;
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
        //                    //Response.AppendHeader("Content-Disposition", "attachment;  filename=Miscellaneous." + extension);
        //                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //                    Response.BinaryWrite(bytes);
        //                    Response.Flush();
        //                    Response.End();
        //                }
        //                else
        //                {
        //                    lblMessage.Text = "";
        //                    objExp = new NewPortal2023.ESS.Expenses();


        //                    objExp.FromDate = txtFromDate.Text;
        //                    objExp.ToDate = txtToDate.Text;
        //                    objExp.Type = drpType.Text;
        //                    objExp.EmpCode = drpEmpType.Text;
        //                    ds = objExp.Fill_Report();

        //                    rptPrint.Visible = false;
        //                    ReportDataSource datasourceDom = new ReportDataSource("DataSet1", ds.Tables[0]);

        //                    rptPrint.LocalReport.DataSources.Clear();
        //                    rptPrint.LocalReport.ReportPath = @"Reports/ReportPettyExpenses.rdlc";
        //                    rptPrint.LocalReport.DataSources.Add(datasourceDom);
        //                    rptPrint.LocalReport.Refresh();

        //                    //Export the RDLC Report to Byte Array.

        //                    byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

        //                    string fileName = $"Miscellaneous_Report_{txtFromDate.Text}_{txtToDate.Text}.xls";

        //                    // Open generated PDF.
        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.Charset = "";
        //                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //                    Response.ContentType = contentType;
        //                    //Response.AppendHeader("Content-Disposition", "attachment; filename=Miscellaneous." + extension);
        //                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //                    Response.BinaryWrite(bytes);
        //                    Response.Flush();
        //                    Response.End();

        //                }
        //            }
        //            else
        //            {
        //                lblMessage.Text = "Please Select Atleast";
        //            }
        //        }

        //    }
        //}

        protected void lnkDownloadrdlc_Click(object sender, EventArgs e)
        {


            lblMessage.Text = "";
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();

            //objAppraisal.FromDate = txtFromDate1.Text;
            //objAppraisal.ToDate = txtToDate1.Text;

            ds = objAppraisal.Fill_RdlcReport();


            string extension;
            string encoding;
            string contentType;
            string[] streamIds;
            Warning[] warnings;

            ReportDataSource dataSource = new ReportDataSource("DataSet", ds.Tables[0]);

            rptPrint.LocalReport.DataSources.Clear();

            rptPrint.LocalReport.ReportPath = @"Reports/ReportExpense.rdlc";
            rptPrint.LocalReport.DataSources.Add(dataSource);

            rptPrint.LocalReport.Refresh();



            byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

            //string fileName = $"Approved_Payment_details_{txtFromDate1.Text}_{txtToDate1.Text}.xls";

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = ContentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=ApprovedPaymentdetails.xls");
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        //private void InsertFile()
        //{
        //    try
        //    {
        //        string guid;
        //        string path;
        //        //string message;

        //        guid = Convert.ToString(Guid.NewGuid());
        //        guid = guid + fupldDocument.FileName;
        //        path = Request.PhysicalApplicationPath.ToString() + "EXCEL";
        //        path = path + guid;
        //        //fupldDocument.SaveAs(path + guid);


        //        System.IO.Stream fileInputStream = fupldDocument.PostedFile.InputStream;
        //        Byte[] fileContent = new Byte[fileInputStream.Length];
        //        fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));

        //        System.IO.FileStream fStream = new System.IO.FileStream(path, System.IO.FileMode.Create);
        //        System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
        //        bWriter.Write((Byte[])fileContent);
        //        fStream.Flush();
        //        bWriter.Flush();
        //        fStream.Close();
        //        bWriter.Close();

        //        string message = objAppraisal.UploadPaymentDetails(path, Request.PhysicalApplicationPath.ToString());

        //        if (message == "The File is Uploaded Successfully.")
        //        {
        //            ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('The File is Uploaded Successfully.');", true);
        //        }
        //        else
        //        {
        //            ClientScript.RegisterStartupScript(this.GetType(), "error", $"alert('Error: {message}');", true);
        //        }


        //        //string successScript = "alert('The File is Uploaded Successfully.');";
        //        //ClientScript.RegisterStartupScript(this.GetType(), "UploadSuccess", successScript, true);
        //        objCommon = new NewPortal2023.ESS.Common();
        //        divAlert.Visible = true;
        //        lblMessage.Text = message;

        //        System.IO.File.Delete(path);
        //    }
        //    catch(Exception ex)
        //    {
        //        divAlert.Visible = true;
        //        lblMessage.Text = ex.Message;
        //        lblmsg.Text = ex.Message;
        //    }
        //}   

        //protected void lnkDownloadapaidrdlcreport_Click(object sender, EventArgs e)
        //{
        //    if (ValidateInputs2())
        //        {
        //            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        //            objAppraisal.FromDate = txtFromDate2.Text;
        //            objAppraisal.ToDate = txtToDate2.Text;


        //            ds = objAppraisal.Fill_PaidRdlcReport();

        //            string extension;
        //            string encoding;
        //            string contentType;
        //            string[] streamIds;
        //            Warning[] warnings;

        //            ReportDataSource dataSource = new ReportDataSource("DataSet1", ds.Tables[0]);

        //            rptPrint.LocalReport.DataSources.Clear();

        //            rptPrint.LocalReport.ReportPath = @"Reports/ClaimPaidReport.rdlc";
        //            rptPrint.LocalReport.DataSources.Add(dataSource);

        //            rptPrint.LocalReport.Refresh();



        //            byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

        //            string fileName = $"Paid_Payment_details_{txtFromDate2.Text}_{txtToDate2.Text}.xls";

        //            Response.Clear();
        //            Response.Buffer = true;
        //            Response.Charset = "";
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //            Response.ContentType = ContentType;
        //            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        //            Response.BinaryWrite(bytes);
        //            Response.Flush();
        //            Response.End();

        //        }

        //}

        //protected void lnkDownloadTemplate_Click(object sender, EventArgs e)
        //{
        //    DataTable emptyDataTable = new DataTable();

        //    DataSet ds = new DataSet();
        //    ds.Tables.Add(emptyDataTable);

        //    string extension;
        //    string encoding;
        //    string contentType;
        //    string[] streamIds;
        //    Warning[] warnings;


        //    ReportDataSource dataSource = new ReportDataSource("DataSet", ds.Tables[0]);

        //    rptPrint.LocalReport.DataSources.Clear();
        //    rptPrint.LocalReport.ReportPath = @"Reports/ReportExpense.rdlc";
        //    rptPrint.LocalReport.DataSources.Add(dataSource);
        //    rptPrint.LocalReport.Refresh();

        //    byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);


        //    Response.Clear();
        //    Response.Buffer = true;
        //    Response.Charset = "";
        //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    Response.ContentType = contentType;
        //    Response.AppendHeader("Content-Disposition", "attachment; filename=Payment_Details_Template.xls");
        //    Response.BinaryWrite(bytes);
        //    Response.Flush();
        //    Response.End();
        //}

        //protected void btnInsert_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (fupldDocument.HasFile)
        //        {
        //            if (string.IsNullOrEmpty(fupldDocument.PostedFile.FileName))
        //            {
        //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Browse file to upload.');", true);
        //            }
        //            else
        //            {
        //                InsertFile();
        //            }
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Browse file to upload.');", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
        //    }
        //}

        protected void clearFields()
        {
            //txtFromDate.Text = "";
            //txtToDate.Text = "";
            //txtFromDate1.Text = "";
            //txtToDate1.Text = "";
            //txtFromDate2.Text = "";
            //txtToDate2.Text = "";
            drpType.SelectedValue = "";
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {

            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;

            if (ValidateInputs())
            {
                objExp = new NewPortal2023.ESS.Expenses();
                objExp.Type = drpType.SelectedValue;
                //objExp.EmpCode = drpEmpType.SelectedValue;
                ds = objExp.Fill_AgeReport();

                rptPrint.Visible = false;
                ReportDataSource datasourceDomAll = new ReportDataSource("dtAgeProfiling", ds.Tables[0]);

                rptPrint.LocalReport.DataSources.Clear();
                rptPrint.LocalReport.ReportPath = @"Reports/ReportEmpAgeProfiling.rdlc";
                rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
                rptPrint.LocalReport.Refresh();


                byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                //string fileName = $"DomesticReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

                // Open generated PDF.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Age_Profiling_Data.xls");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }

        protected void btnGeneratetenure_Click(object sender, EventArgs e)
        {
            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;

            if (ValidateInputs1())
            {
                objExp = new NewPortal2023.ESS.Expenses();
                objExp.Type = drpTenure.SelectedValue;
                //objExp.EmpCode = drpEmpTypeTenure.SelectedValue;
                ds = objExp.Fill_ReportTenure();

                rptPrint.Visible = false;
                ReportDataSource datasourceDomAll = new ReportDataSource("dtEmpTenure", ds.Tables[0]);

                rptPrint.LocalReport.DataSources.Clear();
                rptPrint.LocalReport.ReportPath = @"Reports/ReportEmpTenure.rdlc";
                rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
                rptPrint.LocalReport.Refresh();


                byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                //string fileName = $"DomesticReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

                // Open generated PDF.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Employee_Tenure_Data.xls");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

            }





        }

        protected void btnGenerateOtandAmt_Click(object sender, EventArgs e)
        {

            if (ValidateInputs2())
            {
                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string extension;

                objExp = new NewPortal2023.ESS.Expenses();

                objExp.EmpCode = drpEmpType3.SelectedValue;
                objExp.MONTH = ddlMonths.SelectedValue;
                string Emp_Code = drpEmpType3.SelectedItem.ToString();
                string Month_Year = ddlMonths.SelectedItem.ToString();
                ds = objExp.Fill_ReportOtandAmt();

                rptPrint.Visible = false;
                ReportDataSource datasourceDomAll = new ReportDataSource("dtOtAndAmt", ds.Tables[0]);

                rptPrint.LocalReport.DataSources.Clear();
                rptPrint.LocalReport.ReportPath = @"Reports/ReportOtAndAmtrdlc.rdlc";
                rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
                rptPrint.LocalReport.Refresh();


                byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                string fileName = $"Ot_Hours_And_Ot_Amount_{Emp_Code}_{Month_Year}.xls";

                // Open generated PDF.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();


            }


        }

        protected void btnGeneratePaidctc_Click(object sender, EventArgs e)
        {

            if (ValidateInputs3())
            {
                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string extension;

                objExp = new NewPortal2023.ESS.Expenses();

                objExp.MONTH = DropDownList1.SelectedValue;
                string month = DropDownList1.SelectedItem.ToString();
                ds = objExp.Fill_ReportPaidctc();

                rptPrint.Visible = false;
                ReportDataSource datasourceDomAll = new ReportDataSource("dtPaidCtc", ds.Tables[0]);

                rptPrint.LocalReport.DataSources.Clear();
                rptPrint.LocalReport.ReportPath = @"Reports/ReportPaidCtc.rdlc";
                rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
                rptPrint.LocalReport.Refresh();


                byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                string fileName = $"Paid_CTC_{month}.xls";

                // Open generated PDF.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();


            }


        }

        protected void btnGenerateNaA_Click(object sender, EventArgs e)
        {
            if (DropDownList2.SelectedValue == "New_joiners")
            {
                if (ValidateInputs4())
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string contentType;
                    string encoding;
                    string extension;

                    objExp = new NewPortal2023.ESS.Expenses();

                    objExp.Type = DropDownList2.SelectedValue;
                    ds = objExp.Fill_ReporNjAndAtt();

                    rptPrint.Visible = false;
                    ReportDataSource datasourceDomAll = new ReportDataSource("dtNewJoinee", ds.Tables[0]);

                    rptPrint.LocalReport.DataSources.Clear();
                    rptPrint.LocalReport.ReportPath = @"Reports/ReportNewJoinee.rdlc";
                    rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
                    rptPrint.LocalReport.Refresh();


                    byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                    //string fileName = $"DomesticReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

                    // Open generated PDF.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "New_Joinee_data.xls");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                }
            }
            else if (DropDownList2.SelectedValue == "Attrition")
            {
                if (ValidateInputs4())
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string contentType;
                    string encoding;
                    string extension;

                    objExp = new NewPortal2023.ESS.Expenses();

                    objExp.Type = DropDownList2.SelectedValue;
                    ds = objExp.Fill_ReporNjAndAtt();

                    rptPrint.Visible = false;
                    ReportDataSource datasourceDomAll = new ReportDataSource("dtLeaveData", ds.Tables[0]);

                    rptPrint.LocalReport.DataSources.Clear();
                    rptPrint.LocalReport.ReportPath = @"Reports/ReportLeaveData.rdlc";
                    rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
                    rptPrint.LocalReport.Refresh();


                    byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                    //string fileName = $"DomesticReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

                    // Open generated PDF.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + "ATTRITION.xls");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                }
            }
        }

        protected void btnGenerAct_Click(object sender, EventArgs e)
        {
            if (ValidateInputs5())
            {
                Warning[] warnings;
                string[] streamIds;
                string contentType;
                string encoding;
                string extension;

                objExp = new NewPortal2023.ESS.Expenses();


                string dateyear = ddlmonthyear.SelectedValue;
                string monthyear = ddlmonthyear.SelectedItem.ToString();
                string[] dateParts = dateyear.Split('-');  // Split the string at the hyphen
                string year = dateParts[0];  // First part is the year
                string month = dateParts[1];  // Second part is the month

                string joinDateString;

                if (month == "2")
                {
                   joinDateString = year + "-" + month.PadLeft(2, '0') + "-28";  // Format: YYYY-MM-30
                }
                else
                {
                    joinDateString = year + "-" + month.PadLeft(2, '0') + "-30";  // Format: YYYY-MM-30
                }

               


                // Convert the string to a DateTime object
                DateTime joinDate = DateTime.ParseExact(joinDateString, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
                string joinPdate = joinDate.ToString("yyyy-MM-dd");

               
                ds = objExp.Fill_Reportact(month, year,joinPdate);

                rptPrint.Visible = false;
                ReportDataSource datasourceDomAll = new ReportDataSource("dtActiveEmp", ds.Tables[0]);

                rptPrint.LocalReport.DataSources.Clear();
                rptPrint.LocalReport.ReportPath = @"Reports/ReportActiveEmp.rdlc";
                rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
                rptPrint.LocalReport.Refresh();


                byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                string fileName = $"Monthly_head_count_{monthyear}.xls";

                //string fileName = $"Monthly_head_count.xls";

                // Open generated PDF.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                //Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Active_employees.xls");
                //Response.AppendHeader("Content-Disposition", "attachment; fileName");
                Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();

            }
        }

        protected void btnGenerateAttendence_Click(object sender, EventArgs e)
        {
          
            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;

            NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
            objNps.Status = "";
            objNps.EmpName = "";

            ds = objNps.GetCurrentdateAttendanceStatus((string)Session["sCompID"], (string)Session["sEmpID"]);

            rptPrint.Visible = false;
            ReportDataSource datasourceDomAll = new ReportDataSource("dtTodayattendence", ds.Tables[0]);

            rptPrint.LocalReport.DataSources.Clear();
            rptPrint.LocalReport.ReportPath = @"Reports/ReportTodaysAttendence.rdlc";
            rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
            rptPrint.LocalReport.Refresh();


            byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

               
            string fileName = $"Attendance_{DateTime.Now:dd_MMM_yyyy}.xls";


            // Open generated PDF.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Active_employees.xls");
            //Response.AppendHeader("Content-Disposition", "attachment; fileName");
            Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

            
        }

        protected void btnGenerateAvgCost_Click(object sender, EventArgs e)
        {

            Warning[] warnings;
            string[] streamIds;
            string contentType;
            string encoding;
            string extension;

            ds = objExp.Fill_ReportPreviousMonthCost();

            rptPrint.Visible = false;
            ReportDataSource datasourceDomAll = new ReportDataSource("dtPaidCtc", ds.Tables[0]);

            rptPrint.LocalReport.DataSources.Clear();
            rptPrint.LocalReport.ReportPath = @"Reports/ReportPaidCtc.rdlc";
            rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
            rptPrint.LocalReport.Refresh();


            byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);


            string previousMonth = DateTime.Now.AddMonths(-1).ToString("MMM yyyy").ToUpper();
            string fileName = $"Average_Cost_Of_{previousMonth}.xls";



            // Open generated PDF.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Active_employees.xls");
            //Response.AppendHeader("Content-Disposition", "attachment; fileName");
            Response.AppendHeader("Content-Disposition", $"attachment; filename={fileName}");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();


        }
    }
}




    

