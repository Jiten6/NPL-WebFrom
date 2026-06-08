using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;

namespace NewPortal2023.ESS
{
    public partial class HiringOfficer : System.Web.UI.Page
    {
        string title = "Select Employeement Type";
        NewPortal2023.ESS.Common objCommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.OTP objOTP = new NewPortal2023.ESS.OTP();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsExp = new DataSet();
        private string SourcePath = string.Empty;
        private string savePath = string.Empty;
        string emailid = "";
        string Gender = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!Page.IsPostBack)
                {
                    ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                    lblMessage.Text = "";
                    FillCandidate();
                    mv.SetActiveView(vwList);
                    //Fill_Details("1", gvList.PageSize.ToString());
                }

                //gvList.PageIndex = e.NewPageIndex;
                //if (ViewState["COUNT"] != null)
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "", false);
                //}
                //else
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
                //    ViewState["COUNT"] = 1;
                //}
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }

        }

        protected void FillCandidate()
        {           
            DataSet ds = new DataSet();
            ds = objOTP.FillCandidatesHR();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvList.DataSource = ds;
                gvList.DataBind();
            }
            else
            {
                gvList.DataSource = null;
                gvList.DataBind();
            }
        }

        protected void drpEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            objOTP.EmployeeType = drpEmpType.SelectedValue;
            FillCandidate();
        }

        protected void btnFullTime_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "", false);
            objOTP.EmployeeType = "Full Time";
            DataSet ds = new DataSet();
            ds = objOTP.FillCandidatesHiring();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "2")
                {
                    ViewState["EMPTYPE"] = "Full Time";
                    DataTable dsFull = ds.Tables[0];
                    DataTable fulltimeDataTable = dsFull.Clone();

                    foreach (DataRow row in dsFull.Rows)
                    {
                        if (row["EMPTYPE"].ToString() == "Full Time")
                        {
                            fulltimeDataTable.ImportRow(row);
                        }
                    }

                    gvList.DataSource = fulltimeDataTable;
                    gvList.DataBind();
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                }
            }
            else
            {
                gvList.DataSource = null;
                gvList.DataBind();
            }

            mv.SetActiveView(vwList);

        }

        protected void btnPartTime_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "", false);
            objOTP.EmployeeType = "Part Time";
            DataSet ds = new DataSet();
            ds = objOTP.FillCandidatesHiring();
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "2")
                {
                    ViewState["EMPTYPE"] = "Part Time";
                    DataTable dsPart = ds.Tables[0];
                    DataTable parttimeDataTable = dsPart.Clone();

                    foreach (DataRow row in dsPart.Rows)
                    {
                        if (row["EMPTYPE"].ToString() == "Part Time")
                        {
                            parttimeDataTable.ImportRow(row);
                        }
                    }

                    gvList.DataSource = parttimeDataTable;
                    gvList.DataBind();
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                }

            }
            else
            {
                gvList.DataSource = null;
                gvList.DataBind();
            }

            mv.SetActiveView(vwList);

        }

        private void Fill_Details(string index, string size)
        {

            objOTP = new NewPortal2023.ESS.OTP();
            DataSet ds = new DataSet();

            objOTP.FillCandidates(gvList, index, size);

        }
        private void OpenList()
        {
            //Fill_Details("1", gvList.PageSize.ToString());
            if (mv.ActiveViewIndex != 0) mv.SetActiveView(vwList);
            {
                Fill_Details("1", gvList.PageSize.ToString());
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objOTP = new NewPortal2023.ESS.OTP();
                string action = drpActionStatus.SelectedValue;

                objOTP.CaseNo = txtCaseNo.Text;
                objOTP.Rmk = txtRemarkStatus.Text;
                objOTP.ActionType = drpActionStatus.SelectedValue;
                if (action == "Approve")
                {
                    objOTP.Status = "3";
                }
                else if (action == "Reject")
                {
                    objOTP.Status = "0";
                }

                dsExp = objOTP.UpdateCandidateHiringStatus();
                if (dsExp.Tables[0].Rows[0]["result"].ToString() == "")
                {
                    SENDOFFERLETTER(txtEmail.Text);

                    objCommon.SetMessageColor(divAlert, "success");
                    lblMessage.Text = "Successfully Submitted and Offer letter is send on candidate mail Id.";
                    btnSave.Visible = false;
                    mv.SetActiveView(vwList);
                    DataSet ds = new DataSet();

                    if (ViewState["EMPTYPE"] != null)
                    {
                        objOTP.EmployeeType = ViewState["EMPTYPE"].ToString();

                        ds = objOTP.FillCandidatesHiring();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gvList.DataSource = ds.Tables[0];
                            gvList.DataBind();
                        }
                        else
                        {
                            gvList.DataSource = null;
                            gvList.DataBind();
                        }
                    }
                    else
                    {
                        ds = objOTP.FillCandidatesHiring();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            gvList.DataSource = ds.Tables[0];
                            gvList.DataBind();
                        }
                        else
                        {
                            gvList.DataSource = null;
                            gvList.DataBind();
                        }
                    }


                }
                else
                {
                    objCommon = new NewPortal2023.ESS.Common();
                    divAlert.Visible = true;
                    objCommon.SetMessageColor(divAlert, "danger");
                    lblMessage.Text = "Error.";
                }


            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                objCommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = ex.Message;
            }
        }

        private void SENDOFFERLETTER(string email)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(email);
                mailMessage.From = new MailAddress("reports@sequelgroup.co.in");
                mailMessage.Subject = "Offer Letter";
                mailMessage.Body = "Dear Sir/Madam,\n\n Your offer letter is attached as given below. Please download.\n" + " http://demo.sequelgroup.co.in/ESS/CandidateLoginPage.aspx " + "\nWith Best Regards,\nPayrollservices";

                //System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\Temp\\" + (string)(email) + "_OfferLetter.pdf",
                //                                            System.Net.Mime.MediaTypeNames.Application.Octet);

                System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\Temp\\techsupport@sequelgroup.co.in_OfferLetter.pdf",
                                                            System.Net.Mime.MediaTypeNames.Application.Octet);
                mailMessage.Attachments.Add(data);

                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                System.Net.ServicePointManager.ServerCertificateValidationCallback =
               new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                smtpClient.Credentials = new System.Net.NetworkCredential("reports@sequelgroup.co.in", "sequel@123");
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);

                data.Dispose();
                mailMessage.Dispose();
                smtpClient.Dispose();
                DataSet ds = new DataSet();
                objOTP.emailId = email;

                ds = objOTP.DownloadOfferLetter();
                if (ds.Tables[0].Rows[0]["result"].ToString() == "")
                {
                    objCommon = new NewPortal2023.ESS.Common();
                    divAlert.Visible = true;
                    objCommon.SetMessageColor(divAlert, "success");
                    lblMessage.Text = "Offer letter is send on candidate mail Id";


                }
                else
                {
                    objCommon = new NewPortal2023.ESS.Common();
                    divAlert.Visible = true;
                    objCommon.SetMessageColor(divAlert, "danger");
                    lblMessage.Text = "Error on Mail";
                }
            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                objCommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = ex.Message;

            }
        }

        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            //Console.WriteLine(certificate);
            return true;
        }




        private void Upload_ClickAadhar(string aadhar)
        {
            if (fupldDocumentAadhar.PostedFile.FileName == "")
            {
                divAlert.Visible = true;
                lblMessage.Text = "Please Upload Transport document.";
                return;
            }

            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Aadhar\\" + aadhar + "\\";

            System.IO.Stream fileInputStream = fupldDocumentAadhar.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(fupldDocumentAadhar.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocumentAadhar.FileName.Trim());

            string filesToDelete = aadhar;
            string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
            System.IO.FileStream fStream = new System.IO.FileStream(@savePath + aadhar + "_" + fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write(fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();
        }

        private void Upload_ClickPan(string aadhar)
        {
            if (fupldDocumentPan.PostedFile.FileName == "")
            {
                divAlert.Visible = true;
                lblMessage.Text = "Please Upload Transport document.";
                return;
            }

            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Pan\\" + aadhar + "\\";

            System.IO.Stream fileInputStream = fupldDocumentPan.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(fupldDocumentPan.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocumentPan.FileName.Trim());

            string filesToDelete = aadhar;
            string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
            System.IO.FileStream fStream = new System.IO.FileStream(@savePath + aadhar + "_" + fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write(fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();
        }

        private void Upload_ClickEduc(string aadhar)
        {
            if (fupldDocumentEduc.PostedFile.FileName == "")
            {
                divAlert.Visible = true;
                lblMessage.Text = "Please Upload Transport document.";
                return;
            }

            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Education\\" + aadhar + "\\";

            System.IO.Stream fileInputStream = fupldDocumentEduc.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(fupldDocumentEduc.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocumentEduc.FileName.Trim());

            string filesToDelete = aadhar;
            string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
            System.IO.FileStream fStream = new System.IO.FileStream(@savePath + aadhar + "_" + fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write(fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();
        }

        private void Upload_ClickCC(string aadhar)
        {
            if (fupldDocumentCC.PostedFile.FileName == "")
            {
                divAlert.Visible = true;
                lblMessage.Text = "Please Upload Transport document.";
                return;
            }

            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\CC\\" + aadhar + "\\";

            System.IO.Stream fileInputStream = fupldDocumentCC.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(fupldDocumentCC.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocumentCC.FileName.Trim());

            string filesToDelete = aadhar;
            string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
            System.IO.FileStream fStream = new System.IO.FileStream(@savePath + aadhar + "_" + fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write(fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();
        }

        private void CreateDocumentsStructureAsdhar()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvAadhar.DataSource = dtDocuments;
                gvAadhar.DataBind();
            }
        }
        private void DisplayDocumentsAadhar(string aadhar)
        {
            try
            {
                CreateDocumentsStructureAsdhar();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Aadhar\\" + aadhar + "\\";

                if (System.IO.Directory.Exists(savePath) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles(prefix + "*");

                    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }

                    this.gvAadhar.DataSource = dtDocInfo;
                    this.gvAadhar.DataBind();

                    if (dtDocInfo.Rows.Count > 0)
                    {
                        divfileUploadAadhar.Visible = false;
                        divAadhar.Visible = true;


                    }
                    else
                    {
                        //trUpload.Visible = true;
                        divAadhar.Visible = false;
                    }
                }
                else
                {
                    //trUpload.Visible = true;
                    divAadhar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkBtnOpenFileAadhar_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorageAadhar");

                string openFilePath = lblTSFileStorageName.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
                                                                //openFilePath = openFilePath + ViewState["FILE_PATH"].ToString();

                //string fileName = lblTSFileStorageName.Text;

                System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + lnkBtnOpenFiles.Text);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
            }
            catch
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void CreateDocumentsStructurePan()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvPan.DataSource = dtDocuments;
                gvPan.DataBind();
            }
        }
        private void DisplayDocumentsPan(string aadhar)
        {
            try
            {
                CreateDocumentsStructurePan();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Pan\\" + aadhar + "\\";

                if (System.IO.Directory.Exists(savePath) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles(prefix + "*");

                    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }

                    this.gvPan.DataSource = dtDocInfo;
                    this.gvPan.DataBind();

                    if (dtDocInfo.Rows.Count > 0)
                    {
                        divfileUploadPan.Visible = false;
                        divPan.Visible = true;


                    }
                    else
                    {
                        //trUpload.Visible = true;
                        divPan.Visible = false;
                    }
                }
                else
                {
                    //trUpload.Visible = true;
                    divPan.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkBtnOpenFilesPan_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStoragePan");

                string openFilePath = lblTSFileStorageName.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
                                                                //openFilePath = openFilePath + ViewState["FILE_PATH"].ToString();

                //string fileName = lblTSFileStorageName.Text;

                System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + lnkBtnOpenFiles.Text);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
            }
            catch
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void CreateDocumentsStructureEduc()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvEduc.DataSource = dtDocuments;
                gvEduc.DataBind();
            }
        }
        private void DisplayDocumentsEduc(string aadhar)
        {
            try
            {
                CreateDocumentsStructureEduc();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Education\\" + aadhar + "\\";

                if (System.IO.Directory.Exists(savePath) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles(prefix + "*");

                    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }

                    this.gvEduc.DataSource = dtDocInfo;
                    this.gvEduc.DataBind();

                    if (dtDocInfo.Rows.Count > 0)
                    {
                        divfileUploadEduc.Visible = false;
                        divEduc.Visible = true;


                    }
                    else
                    {
                        //trUpload.Visible = true;
                        divEduc.Visible = false;
                    }
                }
                else
                {
                    //trUpload.Visible = true;
                    divEduc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkBtnOpenFilesEduc_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorageEduc");

                string openFilePath = lblTSFileStorageName.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
                                                                //openFilePath = openFilePath + ViewState["FILE_PATH"].ToString();

                //string fileName = lblTSFileStorageName.Text;

                System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + lnkBtnOpenFiles.Text);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
            }
            catch
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }


        private void CreateDocumentsStructureCC()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvCC.DataSource = dtDocuments;
                gvCC.DataBind();
            }
        }
        private void DisplayDocumentsCC(string aadhar)
        {
            try
            {
                CreateDocumentsStructureCC();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\CC\\" + aadhar + "\\";

                if (System.IO.Directory.Exists(savePath) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles(prefix + "*");

                    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }

                    this.gvCC.DataSource = dtDocInfo;
                    this.gvCC.DataBind();

                    if (dtDocInfo.Rows.Count > 0)
                    {
                        divfileUploadCC.Visible = false;
                        divCC.Visible = true;


                    }
                    else
                    {
                        //trUpload.Visible = true;
                        divCC.Visible = false;
                    }
                }
                else
                {
                    //trUpload.Visible = true;
                    divCC.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkBtnOpenFilesCC_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorageCC");

                string openFilePath = lblTSFileStorageName.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
                                                                //openFilePath = openFilePath + ViewState["FILE_PATH"].ToString();

                //string fileName = lblTSFileStorageName.Text;

                System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + lnkBtnOpenFiles.Text);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
            }
            catch
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private bool ValidateInputs()
        {
            divAlertCreate.Visible = true;


            if (txtAadharNo.Text.Trim() == "")
            {
                divAlertCreate.Visible = true;
                lblMessageCreate.Text = "Enter Aadhar No.";
                return false;
            }
            if (txtPanNo.Text == "")
            {
                lblMessageCreate.Text = "Enter Pan No.";
                return false;
            }
            if (txtMobileNumber.Text == "")
            {
                lblMessageCreate.Text = "Enter Mobile No.";
                return false;
            }
            if (txtEmployeementType.Text == "")
            {
                lblMessageCreate.Text = "Enter Employeement Type.";
                return false;
            }


            if (drpStateOfPosting.SelectedValue.Trim() == "")
            {
                divAlertCreate.Visible = true;
                lblMessageCreate.Text = "Select State Of Posting.";
                return false;
            }
            if (drpLocationOfPosting.SelectedValue.Trim() == "")
            {
                lblMessageCreate.Text = "Select Location Of Posting.";
                return false;
            }

            if (drpDepartment.SelectedValue.Trim() == "")
            {
                lblMessageCreate.Text = "Select  Department.";
                return false;
            }
            if (drpGrade.SelectedValue.Trim() == "")
            {
                lblMessageCreate.Text = "Select Grade.";
                return false;
            }
            if (drpDegination.SelectedValue.Trim() == "")
            {
                lblMessageCreate.Text = "Select Degination.";
                return false;
            }
            if (drpCandidateCategory.SelectedValue.Trim() == "")
            {
                lblMessageCreate.Text = "Select Candidate Category.";
                return false;
            }
            if (drpSUCode.SelectedValue.Trim() == "")
            {
                lblMessageCreate.Text = "Select SU Code.";
                return false;
            }

            if (txtAdd1.Text == "")
            {
                lblMessageCreate.Text = "Enter Address 1.";
                return false;
            }
            if (txtAdd2.Text == "")
            {
                lblMessageCreate.Text = "Enter Address 2.";
                return false;
            }
            if (txtEmail.Text == "")
            {
                lblMessageCreate.Text = "Enter Email ID.";
                return false;
            }

            return true;
        }

        protected void txtAadharNoPer_Click(object sender, EventArgs e)
        {
            try
            {
                //objOTP.AadhaarNo = txtAadharNo.Text;
                //dsExp = objOTP.FillDetailsByAadhar();
                //if(dsExp.Tables[0].Rows[0]["result"].ToString()=="")
                //{


                txtPanName.Text = ViewState["panname"].ToString();
                txtAadharName.Text = ViewState["nameLabel"].ToString();
                txtDOB.Text = ViewState["dobLabel"].ToString();
                txtCountry.Text = ViewState["country"].ToString();
                txtState.Text = ViewState["state"].ToString();
                txtPIN.Text = ViewState["pincode"].ToString();
                txtAdd3.Text = ViewState["addressLabel"].ToString();
                string Gender = ViewState["genderLabel"].ToString();
                if (Gender == "M")
                {
                    rdMale.Checked = true;
                    rdFemale.Checked = false;
                }
                else if (Gender == "F")
                {
                    rdFemale.Checked = true;
                    rdMale.Checked = false;
                }

                //}
            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                objCommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            divAlertCreate.Visible = false;
            lblMessageCreate.Text = "";
            string title = "Select Employeement Type";
            ClearControl();
            mv.SetActiveView(vwCreate);
            //if (ViewState["COUNT"] != null)
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "", false);

            //}
            //else
            //{
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            ViewState["COUNT"] = 1;
            //}


        }

        private void ClearControl()
        {
            txtEmployeementType.Text = "";
            txtAadharNo.Text = "";
            txtPanNo.Text = "";
            drpStateOfPosting.SelectedIndex = -1;
            drpLocationOfPosting.SelectedIndex = -1;
            drpGrade.SelectedIndex = -1;
            drpSUCode.SelectedIndex = -1;
            drpCandidateCategory.SelectedIndex = -1;
            txtMobileNumber.Text = "";
            txtAadharNoPer.Text = "";
            txtCaseNo.Text = "";
            txtPanName.Text = "";
            txtAadharName.Text = "";
            txtDOB.Text = "";
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            txtAdd3.Text = "";
            txtCountry.Text = "";
            txtState.Text = "";
            txtPIN.Text = "";
            txtEmail.Text = "";
            txtExpDOJ.Text = "";
            txtRemk.Text = "";
            rdMale.Checked = false;
            rdFemale.Checked = false;
        }

        protected void lnkBtnView_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Submit";
                divAlert.Visible = false;
                divAlertCreate.Visible = false;
                lblMessage.Text = "";
                lblMessageCreate.Text = "";
                DataSet ds = new DataSet();
                //objobs = new MPP_NUM.Jobs();
                // FillDropdown();
                objCommon = new NewPortal2023.ESS.Common();
                //LinkButton lnkBtnEdit = (LinkButton)sender;
                //Label lblAID = (Label)lnkBtnEdit.NamingContainer.FindControl("lblAID");

                //objAssignDT.EntryAid = lblAID.Text;


                LinkButton lnkBtnView = (LinkButton)sender;
                Label lblAID = (Label)lnkBtnView.NamingContainer.FindControl("lblAID");
                ViewState["EntryAId"] = lblAID.Text;
                objOTP.EntryAId = lblAID.Text;


                ds = objOTP.Fill_CandidateDetailByID();

                if (ds.Tables[0].Rows.Count > 0)
                {


                    txtEmployeementType.Text = ds.Tables[0].Rows[0]["EMPTYPE"].ToString();
                    txtAadharNo.Text = ds.Tables[0].Rows[0]["AADHARNO"].ToString();
                    txtPanNo.Text = ds.Tables[0].Rows[0]["PANCODE"].ToString();
                    drpStateOfPosting.SelectedValue = ds.Tables[0].Rows[0]["STATEPOST"].ToString();
                    drpLocationOfPosting.SelectedValue = ds.Tables[0].Rows[0]["LOCPOST"].ToString();
                    drpDepartment.SelectedValue = ds.Tables[0].Rows[0]["DEPARTMENT"].ToString();
                    drpDegination.SelectedValue = ds.Tables[0].Rows[0]["DEGINATION"].ToString();
                    drpGrade.SelectedValue = ds.Tables[0].Rows[0]["GRADE"].ToString();
                    drpSUCode.SelectedValue = ds.Tables[0].Rows[0]["SUCODE"].ToString();
                    drpCandidateCategory.SelectedValue = ds.Tables[0].Rows[0]["CANDIDATECAT"].ToString();
                    txtMobileNumber.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
                    if (ds.Tables[0].Rows[0]["LINK_STATUS"].ToString() == "Linked")
                    {
                        lblAPLinkStatus.Text = "Aadhar-PAN is linked";
                    }
                    else
                    {
                        lblAPLinkStatus.Text = "Aadhar-PAN is Not-linked yet";
                    }

                    txtCaseNo.Text = ds.Tables[0].Rows[0]["CASE_NO"].ToString();
                    txtAadharNoPer.Text = ds.Tables[0].Rows[0]["AADHARNO"].ToString();
                    txtPanName.Text = ds.Tables[0].Rows[0]["panname"].ToString();
                    txtPanNo.Text = ds.Tables[0].Rows[0]["PANCODE"].ToString();
                    txtAadharName.Text = ds.Tables[0].Rows[0]["AADHARNAME"].ToString();
                    txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                    txtAdd1.Text = ds.Tables[0].Rows[0]["ADD1"].ToString();
                    txtAdd2.Text = ds.Tables[0].Rows[0]["ADD2"].ToString();
                    txtAdd2.Text = ds.Tables[0].Rows[0]["ADD3"].ToString();
                    txtCountry.Text = ds.Tables[0].Rows[0]["COUNTRY"].ToString();
                    txtState.Text = ds.Tables[0].Rows[0]["STATE"].ToString();
                    txtPIN.Text = ds.Tables[0].Rows[0]["PINCODE"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    txtExpDOJ.Text = ds.Tables[0].Rows[0]["EXPDOJ"].ToString();
                    txtRemk.Text = ds.Tables[0].Rows[0]["BRANCH_REMARKS"].ToString();
                    Gender = ds.Tables[0].Rows[0]["GENDER"].ToString();

                    if (Gender == "M")
                    {
                        rdMale.Checked = true;
                        rdFemale.Checked = false;
                    }
                    else if (Gender == "F")
                    {
                        rdFemale.Checked = true;
                        rdMale.Checked = false;
                    }

                    DisplayDocumentsAadhar(txtAadharNo.Text);
                    DisplayDocumentsPan(txtAadharNo.Text);
                    DisplayDocumentsEduc(txtAadharNo.Text);
                    DisplayDocumentsCC(txtAadharNo.Text);

                    divAadhar.Visible = true;
                    divPan.Visible = true;
                    divEduc.Visible = true;
                    divCC.Visible = true;

                    if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "0")
                    {
                        btnSave.Visible = true;
                        btnSave.Text = "Update";
                        divfileUploadAadhar.Visible = true;
                        divfileUploadPan.Visible = true;
                        divfileUploadEduc.Visible = true;
                        divfileUploadCC.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = true;
                    }

                    //objOTP.nameLabel = ViewState["nameLabel"].ToString();
                    //objOTP.genderLabel = ViewState["genderLabel"].ToString();
                    //objOTP.dobLabel = ViewState["dobLabel"].ToString();
                    //objOTP.addressLabel = ViewState["addressLabel"].ToString();
                    //objOTP.country = ViewState["country"].ToString();
                    //objOTP.state = ViewState["state"].ToString();
                    //objOTP.pincode = ViewState["pincode"].ToString();
                    //objOTP.panname = ViewState["panname"].ToString();

                }

                //ClearControlsEdit();
                mv.SetActiveView(vwCreate);
            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                objCommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                FillCandidate();
                mv.SetActiveView(vwList);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        protected void gvAssUserDataPointList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void lnkDnlAadhar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAadharUpload_Click(object sender, EventArgs e)
        {

        }

        protected void lnkDnlPan_Click(object sender, EventArgs e)
        {

        }

        protected void btnPanUpload_Click(object sender, EventArgs e)
        {

        }

        protected void lnkDnlEduc_Click(object sender, EventArgs e)
        {

        }

        protected void btnEducUpload_Click(object sender, EventArgs e)
        {

        }

        protected void lnkDnlCC_Click(object sender, EventArgs e)
        {

        }

        protected void btnCCUpload_Click(object sender, EventArgs e)
        {

        }


        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string var = "Alternate, Edit";
            string var1 = e.Row.RowState.ToString();
            if (var == var1)
            {
                DropDownList drpactiontype = e.Row.FindControl("drpAction") as DropDownList;
                drpactiontype.Enabled = true;

                TextBox txtRemarks = e.Row.FindControl("txtRemarks") as TextBox;
                txtRemarks.Enabled = true;

            }
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Edit)
            {
                //TextBox txtCountry = e.Row.FindControl("txtCountry") as TextBox;
                //txtCountry.Enabled = true;
                DropDownList drpactiontype = e.Row.FindControl("drpAction") as DropDownList;
                drpactiontype.Enabled = true;

                TextBox txtRemarks = e.Row.FindControl("txtRemarks") as TextBox;
                txtRemarks.Enabled = true;


            }
        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvList.EditIndex = e.NewEditIndex;
            FillCandidate();
            //this.Fill_Details("1", gvList.PageSize.ToString());
        }

        protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                objOTP = new NewPortal2023.ESS.OTP();
                GridViewRow row = gvList.Rows[e.RowIndex];
                int EntryAId = Convert.ToInt32(gvList.DataKeys[e.RowIndex].Values[0]);
                DropDownList drpactiontype = (row.FindControl("drpAction") as DropDownList);
                string action = drpactiontype.SelectedItem.Value;
                string remarks = (row.FindControl("txtRemarks") as TextBox).Text;
                Label lblcaseNo = (Label)row.FindControl("lblCaseNO");
                Label lblPanNo = (Label)row.FindControl("lblPanNo");
                objOTP.PanNo = lblPanNo.Text;

                DataSet ds = new DataSet();
                ds = objOTP.GetMailID();
                if (ds.Tables.Count > 0)
                {
                    emailid = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                }

                objOTP.CaseNo = lblcaseNo.Text;
                objOTP.Rmk = remarks;
                objOTP.ActionType = action;
                if (action == "Approve")
                {
                    objOTP.Status = "3";
                }
                else if (action == "Reject")
                {
                    objOTP.Status = "0";
                }

                dsExp = objOTP.UpdateCandidateStatus();
                if (dsExp.Tables[0].Rows[0]["result"].ToString() == "")
                {
                    SENDOFFERLETTER(emailid);

                    objCommon = new NewPortal2023.ESS.Common();
                    divAlert.Visible = true;
                    objCommon.SetMessageColor(divAlert, "success");
                    lblMessage.Text = "Successfully Submitted.";
                }
                else
                {
                    objCommon = new NewPortal2023.ESS.Common();
                    divAlert.Visible = true;
                    objCommon.SetMessageColor(divAlert, "danger");
                    lblMessage.Text = "Error.";
                }

                gvList.EditIndex = -1;
                //this.Fill_Details("1", gvList.PageSize.ToString());
                FillCandidate();
                mv.SetActiveView(vwList);

            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                objCommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = ex.Message;

            }
        }

        protected void gvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvList.EditIndex = -1;
            //this.Fill_Details("1", gvList.PageSize.ToString());
            FillCandidate();
        }

        
    }
}