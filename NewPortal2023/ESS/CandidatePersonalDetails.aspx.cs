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

namespace NewPortal2023.ESS
{
    public partial class CandidatePersonalDetails : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objCommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.OTP objOTP = new NewPortal2023.ESS.OTP();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsExp = new DataSet();
        private string SourcePath = string.Empty;
        private string savePath = string.Empty;
        string emailId = "";
        string Gender = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //gvList.PageIndex = e.NewPageIndex;
                if (!Page.IsPostBack)
                {
                    DataSet ds = new DataSet();
                    if (Request.QueryString["id"] != null)
                    {
                        ID = Convert.ToString(Request.QueryString["id"]);
                        objOTP.PanNo = ID;
                    }

                    ds = objOTP.GetMailID();
                    if(ds.Tables.Count>0)
                    {
                        emailId = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                        ViewState["emailId"] = emailId;
                        SendActivationEmail(emailId);
                        ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                        lblMessage.Text = "";
                        string title = "OTP VALIDATE";
                        ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
                    }
                    else
                    {
                        Response.Redirect("CandidateLoginPage.aspx");
                    }
                    
                    
                    
                }
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }
        }



        protected void btnValidate_Click(object sender, EventArgs e)
        {
            
            DataSet ds = new DataSet();
            objOTP.emailId = ViewState["emailId"].ToString();
            objOTP.Option = txtOTP.Text;
            ds = objOTP.CheckOTP();
            if (ds.Tables[0].Rows[0]["FOUND"].ToString() != "0")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "", false);
                txtOTP.Style["border"] = "4px solid green";

                mv.SetActiveView(vwCreate);

                if (Request.QueryString["id"] != null)
                {
                    ID = Convert.ToString(Request.QueryString["id"]);
                }
                viewDetail(ID);
            }
            else
            {
                txtOTP.Style["border"] = "4px solid red";
                lblNotifymessage.Text = "Enter OTP is Invalid.";
                objCommon.Display("Validate", "DisplayErrorMessage('Enter OTP is Invalid.');");
            }
            
        }

        private void SendActivationEmail(string emailId)
        {
            string activationCode = this.GenerateOTP();
            DataSet ds = new DataSet();
            objOTP.emailId = emailId;
            objOTP.Option = activationCode;
            ds = objOTP.InsertOTP();
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DateTime date = DateTime.Now;

            string EMPNAME = emailId;

            string body = "Dear User,\n\n";
            body += "<br /><b>Following is your OTP.";
            body += "<br /><b>" + activationCode + "</b>";
            body += "<br /><br />Thanks";

            string subjects = "Account Activation";
            emailSend.SendEmail(emailId, subjects, body);
            objCommon.Display("Validate", "DisplayErrorMessage('OTP is send on your Email Id.');");

            
            lblNotifymessage.Text = "OTP is send on your Email Id.";


        }

        protected string GenerateOTP()
        {
            string characters = "1234567890";
            string otp = string.Empty;
            for (int i = 0; i < 5; i++)
            {
                string character = string.Empty;
                do
                {
                    int index = new Random().Next(0, characters.Length);
                    character = characters.ToCharArray()[index].ToString();
                } while (otp.IndexOf(character) != -1);
                otp += character;
            }
            return otp;
        }

        private void viewDetail(string Id)
        {
            try
            {
                btnSave.Text = "Update";
                objCommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                objCommon.SetMessageColor(divAlert, "success");
           
               
                lblMessage.Text = "Now you can update your personal details.";
                
                DataSet ds = new DataSet();
                //objobs = new MPP_NUM.Jobs();
                // FillDropdown();
                objCommon = new NewPortal2023.ESS.Common();
                //LinkButton lnkBtnEdit = (LinkButton)sender;
                //Label lblAID = (Label)lnkBtnEdit.NamingContainer.FindControl("lblAID");

                //objAssignDT.EntryAid = lblAID.Text;


              
                ViewState["EntryAId"] = Id;
                objOTP.EntryAId = Id;


                ds = objOTP.Fill_CandidateDetailByPanID();

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

                    txtCaseNo.Text = ds.Tables[0].Rows[0]["CASE_NO"].ToString();
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
                    txtPanName.Text = ds.Tables[0].Rows[0]["panname"].ToString();
                    //txtAadharName.Text = ViewState["nameLabel"].ToString();
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
                    divfileUploadAadhar.Visible = true;
                    divfileUploadPan.Visible = true;
                    divfileUploadEduc.Visible = true;
                    divfileUploadCC.Visible = true;
                    btnSave.Visible = true;
                        btnSave.Text = "Update";
                   

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

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                objOTP.AadhaarNo = ViewState["EntryAId"].ToString();
                objOTP.Rmk = txtRemk.Text;
                DataSet ds = new DataSet();
                ds = objOTP.AcceptCandidateDetailByAadharID();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    objCommon = new NewPortal2023.ESS.Common();
                    divAlert.Visible = true;
                    objCommon.SetMessageColor(divAlert, "success");
                    lblMessage.Text = "Your Acceptance Successfully Submitted.";
                    btnSave.Visible = false;
                    btnAccept.Visible = false;
                    btnReject.Visible = false;
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

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                objOTP.AadhaarNo = ViewState["EntryAId"].ToString();
                objOTP.Rmk = txtRemk.Text;
                DataSet ds = new DataSet();
                ds = objOTP.RejectCandidateDetailByAadharID();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    objCommon = new NewPortal2023.ESS.Common();
                    divAlert.Visible = true;
                    objCommon.SetMessageColor(divAlert, "success");
                    lblMessage.Text = "Your Action is Submitted.";
                    btnSave.Visible = false;
                    btnAccept.Visible = false;
                    btnReject.Visible = false;
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (ValidateInputs())
                //{
                    objOTP.EmployeeType = txtEmployeementType.Text;
                    objOTP.AadhaarNo = txtAadharNo.Text;
                    objOTP.PanNo = txtPanNo.Text;
                    objOTP.StateOfPosting = drpStateOfPosting.SelectedValue;
                    objOTP.LocationOfPosting = drpLocationOfPosting.SelectedValue;
                    objOTP.Degination = drpDegination.SelectedValue;
                    objOTP.Department = drpDepartment.SelectedValue;
                    objOTP.Grade = drpGrade.SelectedValue;
                    objOTP.SUCode = drpSUCode.SelectedValue;
                    objOTP.CandidateCategory = drpCandidateCategory.SelectedValue;
                    objOTP.Mobile = txtMobileNumber.Text;
                 
                    objOTP.Add1 = txtAdd1.Text;
                    objOTP.Add2 = txtAdd2.Text;
                    objOTP.Add3 = txtAdd3.Text;
                    objOTP.emailId = txtEmail.Text;
                    objOTP.ExcpDOJ = txtExpDOJ.Text;
                    objOTP.Rmk = txtRemk.Text;


                    if (btnSave.Text == "Save")
                    {
                        dsExp = objOTP.InsertCandidateDetails();
                        if (dsExp.Tables[0].Rows[0]["result"].ToString() == "")
                        {
                            Upload_ClickAadhar(txtAadharNo.Text);
                            DisplayDocumentsAadhar(txtAadharNo.Text);
                            Upload_ClickPan(txtAadharNo.Text);
                            DisplayDocumentsPan(txtAadharNo.Text);
                            Upload_ClickEduc(txtAadharNo.Text);
                            DisplayDocumentsEduc(txtAadharNo.Text);
                            Upload_ClickCC(txtAadharNo.Text);
                            DisplayDocumentsCC(txtAadharNo.Text);
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
                    }
                    else if (btnSave.Text == "Update")
                    {
                        objOTP.EntryAId = ViewState["EntryAId"].ToString();
                        string count = objOTP.EntryAId.ToString();
                        dsExp = objOTP.UpdateCandidateStatus();
                        if (dsExp.Tables[0].Rows[0]["result"].ToString() == "")
                        {
                            Upload_ClickAadhar(txtAadharNo.Text);
                            DisplayDocumentsAadhar(txtAadharNo.Text);
                            Upload_ClickPan(txtAadharNo.Text);
                            DisplayDocumentsPan(txtAadharNo.Text);
                            Upload_ClickEduc(txtAadharNo.Text);
                            DisplayDocumentsEduc(txtAadharNo.Text);
                            Upload_ClickCC(txtAadharNo.Text);
                            DisplayDocumentsCC(txtAadharNo.Text);
                            objCommon = new NewPortal2023.ESS.Common();
                            divAlert.Visible = true;
                            objCommon.SetMessageColor(divAlert, "success");
                            lblMessage.Text = "Successfully Updated.";
                            btnSave.Visible = false;
                            btnAccept.Visible = false;
                            btnReject.Visible = false;
                        }
                        else
                        {
                            objCommon = new NewPortal2023.ESS.Common();
                            divAlert.Visible = true;
                            objCommon.SetMessageColor(divAlert, "danger");
                            lblMessage.Text = "Error.";
                        }


                   // }
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
        private bool ValidateInputs()
        {
            divAlert.Visible = true;


            if (txtAadharNo.Text.Trim() == "")
            {
                divAlert.Visible = true;
                lblMessage.Text = "Enter Aadhar No.";
                return false;
            }
            if (txtPanNo.Text == "")
            {
                lblMessage.Text = "Enter Pan No.";
                return false;
            }
            if (txtMobileNumber.Text == "")
            {
                lblMessage.Text = "Enter Mobile No.";
                return false;
            }
            if (txtEmployeementType.Text == "")
            {
                lblMessage.Text = "Enter Employeement Type.";
                return false;
            }


            if (drpStateOfPosting.SelectedValue.Trim() == "")
            {
                divAlert.Visible = true;
                lblMessage.Text = "Select State Of Posting.";
                return false;
            }
            if (drpLocationOfPosting.SelectedValue.Trim() == "")
            {
                lblMessage.Text = "Select Location Of Posting.";
                return false;
            }

            if (drpDepartment.SelectedValue.Trim() == "")
            {
                lblMessage.Text = "Select  Department.";
                return false;
            }
            if (drpGrade.SelectedValue.Trim() == "")
            {
                lblMessage.Text = "Select Grade.";
                return false;
            }
            if (drpDegination.SelectedValue.Trim() == "")
            {
                lblMessage.Text = "Select Degination.";
                return false;
            }
            if (drpCandidateCategory.SelectedValue.Trim() == "")
            {
                lblMessage.Text = "Select Candidate Category.";
                return false;
            }
            if (drpSUCode.SelectedValue.Trim() == "")
            {
                lblMessage.Text = "Select SU Code.";
                return false;
            }

            if (txtAdd1.Text == "")
            {
                lblMessage.Text = "Enter Address 1.";
                return false;
            }
            if (txtAdd2.Text == "")
            {
                lblMessage.Text = "Enter Address 2.";
                return false;
            }
            if (txtEmail.Text == "")
            {
                lblMessage.Text = "Enter Email ID.";
                return false;
            }

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
                    divAadhar.Visible = true;

                    //if (dtDocInfo.Rows.Count > 0)
                    //{
                    //    divfileUploadAadhar.Visible = false;
                    //    divAadhar.Visible = true;


                    //}
                    //else
                    //{
                    //    //trUpload.Visible = true;
                    //    divAadhar.Visible = false;
                    //}
                }
                //else
                //{
                //    //trUpload.Visible = true;
                //    divAadhar.Visible = false;
                //}
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
                    divPan.Visible = true;

                    //if (dtDocInfo.Rows.Count > 0)
                    //{
                    //    divfileUploadPan.Visible = false;
                    //    divPan.Visible = true;


                    //}
                    //else
                    //{
                    //    //trUpload.Visible = true;
                    //    divPan.Visible = false;
                    //}
                }
                //else
                //{
                //    //trUpload.Visible = true;
                //    divPan.Visible = false;
                //}
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
                    divEduc.Visible = true;

                    //if (dtDocInfo.Rows.Count > 0)
                    //{
                    //    divfileUploadEduc.Visible = false;
                    //    divEduc.Visible = true;


                    //}
                    //else
                    //{
                    //    //trUpload.Visible = true;
                    //    divEduc.Visible = false;
                    //}
                }
                //else
                //{
                //    //trUpload.Visible = true;
                //    divEduc.Visible = false;
                //}
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
                    divCC.Visible = true;

                    //if (dtDocInfo.Rows.Count > 0)
                    //{
                    //    divfileUploadCC.Visible = false;
                    //    divCC.Visible = true;


                    //}
                    //else
                    //{
                    //    //trUpload.Visible = true;
                    //    divCC.Visible = false;
                    //}
                }
                //else
                //{
                //    //trUpload.Visible = true;
                //    divCC.Visible = false;
                //}
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

        
    }
}