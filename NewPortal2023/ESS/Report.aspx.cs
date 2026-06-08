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
    public partial class Report : System.Web.UI.Page
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
                fillDropDown();
            }
        }

        public void fillDropDown()
        {
            objExp.fillEmployee(drpEmpType);
            //objExp.fillDepartment(drpDepartmentype);
        }

        protected bool ValidateInputs()
        {
            if (txtFromDate.Text.Trim() == "")
            {
                lblMessage.Text = "Enter From Date.";
                string script = $@"<script type='text/javascript'>alert('Please Enter the From Date.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            if (txtToDate.Text.Trim() == "")
            {
                lblMessage.Text = "Enter From Date.";
                string script = $@"<script type='text/javascript'>alert('Please Enter the To Date.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            //if (drpEmpType.SelectedValue.Trim() == "")
            //{
            //    lblMessage.Text = "Select Employee.";
            //    string script = $@"<script type='text/javascript'>alert('Please select the Employee.');</script>";
            //    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            //    return false;
            //}

            if (drpType.SelectedValue.Trim() == "")
            {
                lblMessage.Text = "Select Expense Type.";
                string script = $@"<script type='text/javascript'>alert('Please select Expense Type.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;

            }

          


            return true;
        }

        protected bool ValidateInputs1()
        {
            if (txtFromDate1.Text.Trim() == "")
            {
                lblMessage.Text = "Enter From Date.";
                string script = $@"<script type='text/javascript'>alert('Please Enter the From Date.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            if (txtToDate1.Text.Trim() == "")
            {
                lblMessage.Text = "Enter To Date.";
                string script = $@"<script type='text/javascript'>alert('Please Enter the To Date.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            return true;
        }

        protected bool ValidateInputs2()
        {
            if (txtFromDate2.Text.Trim() == "")
            {
                lblMessage.Text = "Enter From Date.";
                string script = $@"<script type='text/javascript'>alert('Please Enter the From Date.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            if (txtToDate2.Text.Trim() == "")
            {
                lblMessage.Text = "Enter To Date.";
                string script = $@"<script type='text/javascript'>alert('Please Enter the To Date.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            return true;
        }

        protected bool ValidateInputs3()
        {
            if (txtFromDate5.Text.Trim() == "")
            {
                lblMessage.Text = "Enter From Date.";
                string script = $@"<script type='text/javascript'>alert('Please Enter the From Date.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            if (txtToDate5.Text.Trim() == "")
            {
                lblMessage.Text = "Enter To Date.";
                string script = $@"<script type='text/javascript'>alert('Please Enter the To Date.');</script>";
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
            updatePaidClaimPanel.Visible = false;
            paidClaimReportPanel.Visible = false;

            if (ddlreportdrop.SelectedItem != null && ddlreportdrop.SelectedItem.Text != "")
            {
                string selectedOption = ddlreportdrop.SelectedItem.Text;

                // Show the panel based on the selected option
                if (selectedOption == "Claim Detail Report")
                {
                    claimDetailPanel.Visible = true;
                }
                else if (selectedOption == "Approved Claim Report")
                {
                    approvedClaimPanel.Visible = true;
                }
                else if (selectedOption == "Update Paid Claim")
                {
                    updatePaidClaimPanel.Visible = true;
                }
                else if (selectedOption == "Paid Claim Report")
                {
                    paidClaimReportPanel.Visible = true;
                }
                else if (selectedOption == "Unpaid Claim Report")
                {
                    UnpaidClaimReportPanel.Visible = true;
                }


            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                if (drpType.SelectedValue != "")
                {
                    Warning[] warnings;
                    string[] streamIds;
                    string contentType;
                    string encoding;
                    string extension;
                    if (drpType.SelectedValue == "Domestic")
                    {
                        if (drpEmpType.SelectedValue == "")
                        {
                            lblMessage.Text = "";
                            objExp = new NewPortal2023.ESS.Expenses();

                            objExp.FromDate = txtFromDate.Text;
                            objExp.ToDate = txtToDate.Text;
                            objExp.Type = drpType.Text;
                            objExp.EmpCode = drpEmpType.Text;
                            ds = objExp.Fill_Report();

                            rptPrint.Visible = false;
                            ReportDataSource datasourceDomAll = new ReportDataSource("dsExpDoms", ds.Tables[0]);

                            rptPrint.LocalReport.DataSources.Clear();
                            rptPrint.LocalReport.ReportPath = @"Reports/DomesticReport.rdlc";
                            rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
                            rptPrint.LocalReport.Refresh();


                            byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                            string fileName = $"DomesticReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

                            // Open generated PDF.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = contentType;
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=DomesticReport." + extension);
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();
                        }
                        else
                        {
                            lblMessage.Text = "";
                            objExp = new NewPortal2023.ESS.Expenses();


                            objExp.FromDate = txtFromDate.Text;
                            objExp.ToDate = txtToDate.Text;
                            objExp.Type = drpType.Text;
                            objExp.EmpCode = drpEmpType.Text;
                            ds = objExp.Fill_Report();

                            rptPrint.Visible = false;
                            ReportDataSource datasourceDom = new ReportDataSource("dsExpDoms", ds.Tables[0]);

                            rptPrint.LocalReport.DataSources.Clear();
                            rptPrint.LocalReport.ReportPath = @"Reports/DomesticReport.rdlc";
                            rptPrint.LocalReport.DataSources.Add(datasourceDom);
                            rptPrint.LocalReport.Refresh();

                            //Export the RDLC Report to Byte Array.

                            byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                            string fileName = $"DomesticReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

                            // Open generated PDF.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = contentType;
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();
                        }
                    }
                    if (drpType.SelectedValue == "Local")
                    {
                        if (drpEmpType.SelectedValue == "All")
                        {
                            lblMessage.Text = "";
                            objExp = new NewPortal2023.ESS.Expenses();

                            //string exportOption = "PDF";
                            //RenderingExtension extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
                            //if (extension != null)
                            //{
                            //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                            //    fieldInfo.SetValue(extension, false);
                            //}
                            //exportOption = "Word";
                            //extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
                            //if (extension != null)
                            //{
                            //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                            //    fieldInfo.SetValue(extension, false);
                            //}

                            objExp.FromDate = txtFromDate.Text;
                            objExp.ToDate = txtToDate.Text;
                            objExp.Type = drpType.Text;
                            objExp.EmpCode = drpEmpType.Text;
                            ds = objExp.Fill_Report();

                            rptPrint.Visible = false;
                            ReportDataSource datasourceLocAll = new ReportDataSource("dsExpLoc", ds.Tables[0]);

                            rptPrint.LocalReport.DataSources.Clear();
                            rptPrint.LocalReport.ReportPath = @"Reports/LocalReport.rdlc";
                            rptPrint.LocalReport.DataSources.Add(datasourceLocAll);
                            rptPrint.LocalReport.Refresh();

                            byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                            string fileName = $"LocalReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

                            // Open generated PDF.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = contentType;
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=LocalReport." + extension);
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();

                        }
                        else
                        {
                            lblMessage.Text = "";
                            objExp = new NewPortal2023.ESS.Expenses();


                            objExp.FromDate = txtFromDate.Text;
                            objExp.ToDate = txtToDate.Text;
                            objExp.Type = drpType.Text;
                            objExp.EmpCode = drpEmpType.Text;
                            ds = objExp.Fill_Report();

                            rptPrint.Visible = false;
                            ReportDataSource datasourceLoc = new ReportDataSource("dsExpLoc", ds.Tables[0]);

                            rptPrint.LocalReport.DataSources.Clear();
                            rptPrint.LocalReport.ReportPath = @"Reports/LocalReport.rdlc";
                            rptPrint.LocalReport.DataSources.Add(datasourceLoc);
                            rptPrint.LocalReport.Refresh();


                            byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                            string fileName = $"LocalReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

                            // Open generated PDF.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = contentType;
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=LocalReport." + extension);
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();
                        }
                    }
                    if (drpType.SelectedValue == "Telephone")
                    {
                        if (drpEmpType.SelectedValue == "All")
                        {
                            lblMessage.Text = "";
                            objExp = new NewPortal2023.ESS.Expenses();


                            objExp.FromDate = txtFromDate.Text;
                            objExp.ToDate = txtToDate.Text;
                            objExp.Type = drpType.Text;
                            objExp.EmpCode = drpEmpType.Text;
                            ds = objExp.Fill_Report();

                            rptPrint.Visible = false;
                            ReportDataSource datasourceTelAll = new ReportDataSource("dsExpTel", ds.Tables[0]);

                            rptPrint.LocalReport.DataSources.Clear();
                            rptPrint.LocalReport.ReportPath = @"Reports/TeleReport.rdlc";
                            rptPrint.LocalReport.DataSources.Add(datasourceTelAll);
                            rptPrint.LocalReport.Refresh();


                            byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                            string fileName = $"TeleReport_{txtFromDate.Text}_{txtToDate.Text}.xls";


                            // Open generated PDF.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = contentType;
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=TeleReport." + extension);
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();
                        }
                        else
                        {
                            lblMessage.Text = "";
                            objExp = new NewPortal2023.ESS.Expenses();

                            objExp.FromDate = txtFromDate.Text;
                            objExp.ToDate = txtToDate.Text;
                            objExp.Type = drpType.Text;
                            objExp.EmpCode = drpEmpType.Text;
                            ds = objExp.Fill_Report();

                            rptPrint.Visible = false;
                            ReportDataSource datasourceTel = new ReportDataSource("dsExpTel", ds.Tables[0]);

                            rptPrint.LocalReport.DataSources.Clear();
                            rptPrint.LocalReport.ReportPath = @"Reports/TeleReport.rdlc";
                            
                            rptPrint.LocalReport.DataSources.Add(datasourceTel);
                            rptPrint.LocalReport.Refresh();

                            byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                            string fileName = $"TeleReport_{txtFromDate.Text}_{txtToDate.Text}.xls";

                            // Open generated PDF.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = contentType;
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=TeleReport." + extension);
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();
                        }

                    }
                    if (drpType.SelectedValue == "Miscellaneous")
                    {
                        if (drpEmpType.SelectedValue == "")
                        {
                            lblMessage.Text = "";
                            objExp = new NewPortal2023.ESS.Expenses();

                            objExp.FromDate = txtFromDate.Text;
                            objExp.ToDate = txtToDate.Text;
                            objExp.Type = drpType.Text;
                            objExp.EmpCode = drpEmpType.Text;
                            ds = objExp.Fill_Report();

                            rptPrint.Visible = false;
                            ReportDataSource datasourceDomAll = new ReportDataSource("DataSet1", ds.Tables[0]);

                            rptPrint.LocalReport.DataSources.Clear();
                            rptPrint.LocalReport.ReportPath = @"Reports/ReportPettyExpenses.rdlc";
                            rptPrint.LocalReport.DataSources.Add(datasourceDomAll);
                            rptPrint.LocalReport.Refresh();


                            byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                            string fileName = $"Miscellaneous_Report_{txtFromDate.Text}_{txtToDate.Text}.xls";

                            // Open generated PDF.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = contentType;
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=RDLC." + extension);
                            //Response.AppendHeader("Content-Disposition", "attachment;  filename=Miscellaneous." + extension);
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();
                        }
                        else
                        {
                            lblMessage.Text = "";
                            objExp = new NewPortal2023.ESS.Expenses();


                            objExp.FromDate = txtFromDate.Text;
                            objExp.ToDate = txtToDate.Text;
                            objExp.Type = drpType.Text;
                            objExp.EmpCode = drpEmpType.Text;
                            ds = objExp.Fill_Report();

                            rptPrint.Visible = false;
                            ReportDataSource datasourceDom = new ReportDataSource("DataSet1", ds.Tables[0]);

                            rptPrint.LocalReport.DataSources.Clear();
                            rptPrint.LocalReport.ReportPath = @"Reports/ReportPettyExpenses.rdlc";
                            rptPrint.LocalReport.DataSources.Add(datasourceDom);
                            rptPrint.LocalReport.Refresh();

                            //Export the RDLC Report to Byte Array.

                            byte[] bytes = rptPrint.LocalReport.Render("EXCEL", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                            string fileName = $"Miscellaneous_Report_{txtFromDate.Text}_{txtToDate.Text}.xls";

                            // Open generated PDF.
                            Response.Clear();
                            Response.Buffer = true;
                            Response.Charset = "";
                            Response.Cache.SetCacheability(HttpCacheability.NoCache);
                            Response.ContentType = contentType;
                            //Response.AppendHeader("Content-Disposition", "attachment; filename=Miscellaneous." + extension);
                            Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                            Response.BinaryWrite(bytes);
                            Response.Flush();
                            Response.End();
                           
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Please Select Atleast";
                    }
                }

            }
        }

        protected void lnkDownloadrdlc_Click(object sender, EventArgs e)
        {
            
                
                lblMessage.Text = "";
                NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                if (ValidateInputs1())
                {
                    objAppraisal.FromDate = txtFromDate1.Text;
                    objAppraisal.ToDate = txtToDate1.Text;

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

                    string fileName = $"Approved_Payment_details_{txtFromDate1.Text}_{txtToDate1.Text}.xls";

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = ContentType;
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=ApprovedPaymentdetails.xls");
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            

           
        }

        private void InsertFile()
        {
            try
            {
                string guid;
                string path;
                //string message;

                guid = Convert.ToString(Guid.NewGuid());
                guid = guid + fupldDocument.FileName;
                path = Request.PhysicalApplicationPath.ToString() + "EXCEL";
                path = path + guid;
                //fupldDocument.SaveAs(path + guid);


                System.IO.Stream fileInputStream = fupldDocument.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));

                System.IO.FileStream fStream = new System.IO.FileStream(path, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write((Byte[])fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();

                string message = objAppraisal.UploadPaymentDetails(path, Request.PhysicalApplicationPath.ToString());

                if (message == "The File is Uploaded Successfully.")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "success", "alert('The File is Uploaded Successfully.');", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "error", $"alert('Error: {message}');", true);
                }


                //string successScript = "alert('The File is Uploaded Successfully.');";
                //ClientScript.RegisterStartupScript(this.GetType(), "UploadSuccess", successScript, true);
                objCommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                lblMessage.Text = message;

                System.IO.File.Delete(path);
            }
            catch(Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = ex.Message;
                lblmsg.Text = ex.Message;
            }
        }   

        protected void lnkDownloadapaidrdlcreport_Click(object sender, EventArgs e)
        {
            if (ValidateInputs2())
                {
                    NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                    objAppraisal.FromDate = txtFromDate2.Text;
                    objAppraisal.ToDate = txtToDate2.Text;


                    ds = objAppraisal.Fill_PaidRdlcReport();

                    string extension;
                    string encoding;
                    string contentType;
                    string[] streamIds;
                    Warning[] warnings;

                    ReportDataSource dataSource = new ReportDataSource("DataSet1", ds.Tables[0]);

                    rptPrint.LocalReport.DataSources.Clear();

                    rptPrint.LocalReport.ReportPath = @"Reports/ClaimPaidReport.rdlc";
                    rptPrint.LocalReport.DataSources.Add(dataSource);

                    rptPrint.LocalReport.Refresh();



                    byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                    string fileName = $"Paid_Payment_details_{txtFromDate2.Text}_{txtToDate2.Text}.xls";

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = ContentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                }
            
        }

        protected void lnkDownloadTemplate_Click(object sender, EventArgs e)
        {
            DataTable emptyDataTable = new DataTable();

            DataSet ds = new DataSet();
            ds.Tables.Add(emptyDataTable);

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

           
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = contentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=Payment_Details_Template.xls");
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (fupldDocument.HasFile)
                {
                    if (string.IsNullOrEmpty(fupldDocument.PostedFile.FileName))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Browse file to upload.');", true);
                    }
                    else
                    {
                        InsertFile();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Browse file to upload.');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", $"alert('Error: {ex.Message}');", true);
            }
        }

        protected void clearFields()
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtFromDate1.Text = "";
            txtToDate1.Text = "";
            txtFromDate2.Text = "";
            txtToDate2.Text = "";
            drpType.SelectedValue = "";
        }

        protected void unpaidclaimreportsdownload_Click(object sender, EventArgs e)
        {


            lblMessage.Text = "";
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            if (ValidateInputs3())
            {
                objAppraisal.FromDate = txtFromDate5.Text;
                objAppraisal.ToDate = txtToDate5.Text;

                //ds = objAppraisal.Fill_unpaidRdlcReport();


                string extension;
                string encoding;
                string contentType;
                string[] streamIds;
                Warning[] warnings;

                ReportDataSource dataSource = new ReportDataSource("dsUnpaidReport", ds.Tables[0]);

                rptPrint.LocalReport.DataSources.Clear();

                rptPrint.LocalReport.ReportPath = @"Reports/ReportUnpaidClaims.rdlc";
                rptPrint.LocalReport.DataSources.Add(dataSource);

                rptPrint.LocalReport.Refresh();



                byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                string fileName = $"Unpaid_Claim_Details_{txtFromDate5.Text}_{txtToDate5.Text}.xls";

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = ContentType;
                //Response.AppendHeader("Content-Disposition", "attachment; filename=ApprovedPaymentdetails.xls");
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }



        }



    }
}
