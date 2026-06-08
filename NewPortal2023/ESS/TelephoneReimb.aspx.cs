using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class TelephoneReimb : System.Web.UI.Page
    {
        NewPortal2023.ESS.TelephoneExpenses objTele = new NewPortal2023.ESS.TelephoneExpenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsExp = new DataSet();
        private string SourcePath = string.Empty;
        private string savePath = string.Empty;
        private string SourcePath2 = string.Empty;
        private string savePath2 = string.Empty;
        string flagType = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sCompID"] != null)
            {
                if (!Page.IsPostBack)
                {
                    getTelephoneData();
                    getFinYear();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void getTelephoneData()

        {
            try
            {
                getCategoryType(flagType);
                dsExp = objTele.GetTeleList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables[0].Rows.Count > 0)
                {
                    this.gvTelClaim.DataSource = dsExp.Tables[0];
                    this.gvTelClaim.DataBind();
                    Session["Entry_aid"] = null;
                }
                else
                {
                    this.gvTelClaim.DataSource = null;
                    this.gvTelClaim.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                divAlert.Visible = true;
            }
        }

        private void getCategoryType(string flagtype)
        {
            try
            {
                if (flagtype == "HANDSET")
                {
                    objTele = new NewPortal2023.ESS.TelephoneExpenses();
                    dsExp = objTele.GetTeleCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), "HANDSET");
                    if (dsExp.Tables.Count > 0)
                    {
                        Session["CATEGORY_AID"] = dsExp.Tables[0].Rows[0]["CATEGORY_AID"].ToString();
                        Session["DESG_AID"] = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();


                    }
                }
                else
                 if (flagtype == "TELE")
                {
                    objTele = new NewPortal2023.ESS.TelephoneExpenses();
                    dsExp = objTele.GetTeleCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), "TELEPHONE");
                    if (dsExp.Tables.Count > 0)
                    {
                        Session["CATEGORY_AID"] = dsExp.Tables[0].Rows[0]["CATEGORY_AID"].ToString();
                        Session["DESG_AID"] = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();
                    }
                }
                else

                {
                    objTele = new NewPortal2023.ESS.TelephoneExpenses();
                    dsExp = objTele.GetTeleCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), "");
                    if (dsExp.Tables.Count > 0)
                    {
                        Session["CATEGORY_AID"] = dsExp.Tables[0].Rows[0]["CATEGORY_AID"].ToString();
                        Session["DESG_AID"] = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = "Category Not Found.";
            }
        }
        //private void getCategoryType()
        //{
        //    try
        //    {
        //        objTele = new NewPortal2023.ESS.TelephoneExpenses();
        //        dsExp = objTele.GetTeleCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
        //        if (dsExp.Tables.Count > 0)
        //        {
        //            Session["CATEGORY_AID"] = dsExp.Tables[0].Rows[0]["CATEGORY_TYPE"].ToString();
        //            Session["DESG_AID"] = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Category Not Found.";
        //    }
        //}

        public void getFinYear()
        {
            DataSet ds = objTele.GetFinYear("CO000141");//((string)Session["sCompID"]);
            if (Session["Type"] != null)
            {
                if (Session["Type"].ToString() == "TL000001")
                {
                    //drpFinYearHand.Items.Clear();
                    //drpFinYearHand.DataTextField = "AID";
                    //drpFinYearHand.DataValueField = "DESC";
                    //drpFinYearHand.DataSource = ds;
                    //drpFinYearHand.DataBind();
                    //drpFinYearHand.Items.Insert(0, new ListItem("[Select One]", "0"));
                }
                else if (Session["Type"].ToString() == "TL000002")
                {
                    //drpFinYear.Items.Clear();
                    //drpFinYear.DataTextField = "AID";
                    //drpFinYear.DataValueField = "DESC";
                    //drpFinYear.DataSource = ds;
                    //drpFinYear.DataBind();
                    //drpFinYear.Items.Insert(0, new ListItem("[Select One]", "0"));
                }
            }
        }

        protected void radioPrimary1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioPrimary1.Checked == true)
                {
                    
                    flagType = "HANDSET";
                    getCategoryType(flagType);
                    objTele.Group_Type = "HANDSET";
                    dsExp = objTele.CheckHandsetLimit(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                    if (dsExp.Tables.Count > 0)
                    {

                        string countTable = dsExp.Tables.Count.ToString();
                        if (dsExp.Tables[0].Rows[0]["result"].ToString() == "Duplicate claim for the employee in the specified financial year")
                        {
                            lblMessage.Text = "Duplicate claim for the employee in the specified financial year.";
                            string script = $@"<script type='text/javascript'>alert('Duplicate claim for the employee in the specified financial year.');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            lblMessage.Text = "";

                            getTelephoneData();
                            divFrom.Visible = false;
                            SectionList.Visible = true;
                            divAlert.Visible = false;
                            lblMessage.Text = "";
                            return;
                        }
                    }
                    Session["Type"] = "TL000001";
                    divHandset.Visible = true;
                    divTelephone.Visible = false;
                    //divAmt.Visible = true;
                    btnSave.Visible = true;
                    btnClose.Visible = true;

                    divUpload.Visible = true;
                    divUplaodTel.Visible = false;


                    divfileUpload.Visible = true;
                    divfileDisplay.Visible = false;
                    divHandsetReimbFill.Visible = true;
                    divTeleReimbFill.Visible = false;
                    
                    dsExp = objTele.GetHandSetReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_AID"]), (string)(Session["DESG_AID"]));
                    if (dsExp.Tables.Count > 0)
                    {
                        Session["GROUP_TYPE_AID"] = dsExp.Tables[0].Rows[0]["GROUP_TYPE_AID"].ToString();
                        Session["GROUP_TYPE"] = dsExp.Tables[0].Rows[0]["GROUP_TYPE"].ToString();
                        grHandset.DataSource = dsExp.Tables[0];
                        grHandset.DataBind();
                        getFinYear();
                    }
                    else
                    {
                        grHandset.DataSource = null;
                        grHandset.DataBind();

                    }

                }
                if (radioPrimary2.Checked == true)
                {
                    Session["Type"] = "TL000002";
                    divHandset.Visible = false;
                    divTelephone.Visible = true;
                    // divAmt.Visible = true;
                    btnSave.Visible = true;
                    btnClose.Visible = true;

                    divUpload.Visible = false;
                    divUplaodTel.Visible = true;


                    divfileUploadTel.Visible = true;
                    divfileDisplayTel.Visible = false;
                    divHandsetReimbFill.Visible = false;
                    divTeleReimbFill.Visible = true;
                    flagType = "TELE";
                    getCategoryType(flagType);
                    dsExp = objTele.GetTeleReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_AID"]), (string)(Session["DESG_AID"]));
                    if (dsExp.Tables.Count > 0)
                    {
                        Session["TELEGROUP_TYPE_AID"] = dsExp.Tables[0].Rows[0]["GROUP_TYPE_AID"].ToString();
                        Session["GROUP_TYPE"] = dsExp.Tables[0].Rows[0]["GROUP_TYPE"].ToString();
                        grTelephone.DataSource = dsExp.Tables[0];
                        grTelephone.DataBind();
                        getFinYear();
                    }
                    else
                    {
                        grTelephone.DataSource = null;
                        grTelephone.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                divAlert.Visible = true;
            }

        }

        protected void grTelephone_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblBoarding = (Label)e.Row.FindControl("lblBoarding");


                if (lblBoarding.Text == "0.00")
                {

                    lblBoarding.Text = "At Actuals.";
                }
                else
                {

                }

            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            SectionList.Visible = false;
            divFrom.Visible = true;
            btnSave.Visible = true;
            divHandset.Visible = false;
            divTelephone.Visible = false;
            divHandsetReimbFill.Visible = false;
            divUpload.Visible = false;
            divTeleReimbFill.Visible = false;
            divUplaodTel.Visible = false;
            btnClose.Visible = false;
            btnSave.Visible = false;
            //drpFinYearHand.SelectedIndex=-1;

            txtBillNoHand.Text = "";
            txtBillDateHand.Text = "";


            //drpFinYear.SelectedIndex = -1;
            txtClaimAmtHand.Text = "";
            txtPhoneNo.Text = "";
            txtBillNo.Text = "";
            txtBillDate.Text = "";
            drpBillMonth.SelectedIndex = -1;
            txtClaimAmount.Text = "";
            radioPrimary1.Checked = false;
            radioPrimary2.Checked = false;

        }




        //protected void drpFinYear_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (drpFinYear.SelectedItem.ToString() != "")
        //        {
        //            //string id = "";
        //            //DataSet ds = objFlexi.GetReimbDataAllTelClaim((string)Session["sCompID"], (string)Session["sEmpID"], "AD000207", id);
        //            string drpFinYears = drpFinYear.SelectedItem.ToString();
        //            //DisplayDataByFinancialYear(drpFinYears);
        //            //ShowTelDataByFinYear(drpFinYears);

        //        }
        //        else
        //        {
        //            //txtBillDate.Text = "";
        //            //txtBillPeriodStart.Text = "";
        //            //txtBillPeriodStart.Text = "";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //        objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(ex.Message) + "');");
        //    }
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioPrimary1.Checked == true)
                {
                    if (ValidateHand() == false)
                    {
                        return;
                    }

                    dsExp = objTele.GetHandSetReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_AID"]), (string)(Session["DESG_AID"]));
                    if (dsExp.Tables.Count > 0)
                    {
                        Session["GROUP_TYPE_AID"] = dsExp.Tables[0].Rows[0]["GROUP_TYPE_AID"].ToString();
                        Session["GROUP_TYPE"] = dsExp.Tables[0].Rows[0]["GROUP_TYPE"].ToString();
                        grHandset.DataSource = dsExp.Tables[0];
                        grHandset.DataBind();
                        getFinYear();
                    }
                    else
                    {
                        grHandset.DataSource = null;
                        grHandset.DataBind();

                    }
                    objTele.Type = "TL000001";
                    //objTele.FinYears = drpFinYearHand.SelectedValue;
                    objTele.CategoryAid = Session["CATEGORY_AID"].ToString();
                    objTele.Desg_Aid = Session["DESG_AID"].ToString();
                    objTele.Group_Type_Aid = Session["GROUP_TYPE_AID"].ToString();
                    objTele.Group_Type = Session["GROUP_TYPE"].ToString();

                    objTele.BillNo = txtBillNoHand.Text;
                    objTele.BillDate = txtBillDateHand.Text;
                    objTele.Description_HT_Exp = txtdescript.Text;
                    objTele.BillAmt = txtClaimAmtHand.Text;
                    objTele.FilingStatus = "S";
                    objTele.Status = "9";
                    if (Session["Entry_aid"] != null)
                    {
                        objTele.EntryAid = Session["Entry_aid"].ToString();
                    }

                    if (validateHandTotalamount() == false)
                    {
                        return;
                    }
                    dsExp = objTele.InsertHandsetClaim(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                    if (dsExp.Tables.Count > 0)
                    {

                        string countTable = dsExp.Tables.Count.ToString();
                        if (dsExp.Tables[0].Rows[0]["result"].ToString() == "Duplicate claim for the employee in the specified financial year")
                        {
                            lblMessage.Text = "Duplicate claim for the employee in the specified financial year.";
                            string script = $@"<script type='text/javascript'>alert('Duplicate claim for the employee in the specified financial year.');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            lblMessage.Visible = true;
                            divAlert.Visible = true;

                        }
                        else
                        {
                            string entryCode = dsExp.Tables[0].Rows[0]["result"].ToString();
                            divfileUploadTel.Visible = false;
                            divfileDisplayTel.Visible = true;
                            UploadHandset_Click(entryCode);
                            DisplayHandsetDocuments(entryCode);
                            string script = $@"<script type='text/javascript'>alert('Claim Submitted Successfully.');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            lblMessage.Visible = true;
                            lblMessage.Text = "Claim Submitted Successfully.";
                            SENDUDATEMAIL(txtBillDateHand.Text, "Handset Expense");
                            lblMessage.Visible = true;
                            divAlert.Visible = true;
                            btnSave.Visible = false;
                            getTelephoneData();
                            divFrom.Visible = false;
                            SectionList.Visible = true;
                            divAlert.Visible = false;
                            lblMessage.Text = "";

                            getTelephoneData();
                            divFrom.Visible = false;
                            SectionList.Visible = true;
                            divAlert.Visible = false;
                            lblMessage.Text = "";
                        }


                    }


                }
                else if (radioPrimary2.Checked == true)
                {
                    if (ValidateTele() == false)
                    {
                        return;
                    }
                    objTele.Type = "TL000002";

                    dsExp = objTele.GetTeleReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_AID"]), (string)(Session["DESG_AID"]));
                    if (dsExp.Tables.Count > 0)
                    {
                        Session["TELEGROUP_TYPE_AID"] = dsExp.Tables[0].Rows[0]["GROUP_TYPE_AID"].ToString();
                        Session["GROUP_TYPE"] = dsExp.Tables[0].Rows[0]["GROUP_TYPE"].ToString();
                        grTelephone.DataSource = dsExp.Tables[0];
                        grTelephone.DataBind();
                        getFinYear();
                    }
                    else
                    {
                        grTelephone.DataSource = null;
                        grTelephone.DataBind();
                    }

                    //objTele.FinYears = drpFinYear.SelectedValue;
                    objTele.CategoryAid = Session["CATEGORY_AID"].ToString();
                    objTele.Desg_Aid = Session["DESG_AID"].ToString();
                    objTele.Group_Type_Aid = Session["TELEGROUP_TYPE_AID"].ToString();
                    objTele.Group_Type = Session["GROUP_TYPE"].ToString();
                    objTele.Description_HT_Exp = txtdiscript1.Text;
                    objTele.PhoneNumber = txtPhoneNo.Text;
                    objTele.BillNo = txtBillNo.Text;
                    objTele.BillDate = txtBillDate.Text;
                    objTele.BillMonth = drpBillMonth.SelectedValue;
                    objTele.BillAmt = txtClaimAmount.Text;
                    objTele.FilingStatus = "S";
                    objTele.Status = "9";
                    if (ViewState["Entry_aid"] != null)
                    {
                        objTele.EntryAid = ViewState["Entry_aid"].ToString();
                    }
                    if (validateTeleTotalamount() == false)
                    {
                        return;
                    }
                    dsExp = objTele.InsertTeleClaim(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                    if (dsExp.Tables.Count > 0)
                    {
                        string countTable = dsExp.Tables.Count.ToString();
                        if (dsExp.Tables[0].Rows[0]["result"].ToString() == "Only one claim allowed per month for each employee")
                        {
                            lblMessage.Text = "Only one claim allowed per month for each employee.";
                            string script = $@"<script type='text/javascript'>alert('Only one claim allowed per month for each employee.');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            lblMessage.Visible = true;
                            divAlert.Visible = true;
                        }
                        else
                        {
                            string entryCode = dsExp.Tables[0].Rows[0]["result"].ToString();
                            Session["entryCode"] = entryCode;
                            divfileUploadTel.Visible = false;
                            divfileDisplayTel.Visible = true;
                            UploadTele_Click(Session["entryCode"].ToString());
                            DisplayTeleDocuments(Session["entryCode"].ToString());
                            //UploadTele_Click(entryCode);
                            //DisplayTeleDocuments(entryCode);
                            string script = $@"<script type='text/javascript'>alert('Claim Submitted Successfully.');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            lblMessage.Visible = true;
                            lblMessage.Text = "Claim Submitted Successfully.";
                            SENDUDATEMAIL(txtBillDate.Text, "Telephone Expense");
                            lblMessage.Visible = true;
                            divAlert.Visible = true;
                            btnSave.Visible = false;
                            getTelephoneData();
                            divFrom.Visible = false;
                            SectionList.Visible = true;
                            divAlert.Visible = false;
                            lblMessage.Text = "";

                            getTelephoneData();
                            divFrom.Visible = false;
                            SectionList.Visible = true;
                            divAlert.Visible = false;
                            lblMessage.Text = "";
                        }
                    }
                }
                else
                {
                    objTele.Type = "";
                }

                getTelephoneData();
                divFrom.Visible = false;
                SectionList.Visible = true;
                divAlert.Visible = false;
                lblMessage.Text = "";

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }
        private void SENDUDATEMAIL(string frDate, string type)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DateTime date = DateTime.Now;
            //dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], (string)Session["sEmpID"]);
            //DataSet ds = new DataSet();


            string EMPNAME = Session["sEmpName"].ToString();

            string clientbodys = "Dear " + EMPNAME + ",<br><br>Your " + type + " Expense is Submitted Successfully.<br>"
               + "Once the action taken will be notified to you through mail or same can be checked through ESS portal."
               + "<br>Reimbursement type:- " + type
               + "<br>Applied Date :- " + date
               + "<br>Bill Date :- " + frDate
               + "<br><br>ThankYou.<br><br>";
             //string emails = Session["sEmailId"].ToString();
           string emails = "techsupport@sequelgroup.co.in";
            string subjects = "Do Not Reply: Expense";
            emailSend.SendEmailNPL(emails, subjects, clientbodys);


            string checkerbodys = "Dear Payroll Team,<br><br>" + EMPNAME +
                " " + type + " Expense is received for approval. Kindly take the action for the same through logging-in into ESS portal."
                + "<br>Reimbursement type:- " + type
               + "<br>Applied Date :- " + date
               + "<br>Bill Date :- " + frDate
               + "<br><br>ThankYou.<br><br>";

            emails = "payrollservices@sequelgroup.co.in";
            emailSend.SendEmailNPL(emails, subjects, checkerbodys);


        }

        protected void UploadTele_Click(string entryCode)
        {
            //if (FileUploadTel.PostedFile.FileName == "")
            //{
            //    lblMessage.Text = "Please Upload Transport document.";
            //    return;
            //}

            if (!FileUploadTel.HasFile)
            {
                lblMessage.Text = "Please Upload Transport document.";
                return;
            }



            //if (System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".PDF" ||
            //    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".JPG" ||
            //    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".JPEG" ||
            //    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".ZIP" ||
            //    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".RAR")
            //{

            //}
            //else
            //{
            //    lblMessage.Text = "Only PDF,JPG/JPEG,ZIP and RAR files allowed.";
            //    return;
            //}

            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Telephone\\TeleReimb\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\" + entryCode + "\\";

            System.IO.Stream fileInputStream = FileUploadTel.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(FileUploadTel.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(FileUploadTel.FileName.Trim());

            string filesToDelete = entryCode;
            string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
            System.IO.FileStream fStream = new System.IO.FileStream(@savePath + entryCode + "_" + fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write(fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();

        }
        private void CreateTeleDocumentsStructure()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                grTelFile.DataSource = dtDocuments;
                grTelFile.DataBind();
            }
        }
        private void DisplayTeleDocuments(string entryCode)
        {
            try
            {
                CreateTeleDocumentsStructure();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Telephone\\TeleReimb\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\" + entryCode + "\\";

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

                    this.grTelFile.DataSource = dtDocInfo;
                    this.grTelFile.DataBind();

                    if (dtDocInfo.Rows.Count > 0)
                    {
                        divfileUploadTel.Visible = false;
                        divfileDisplayTel.Visible = true;


                    }
                    else
                    {
                        //trUpload.Visible = true;
                        divfileDisplayTel.Visible = false;
                    }
                }
                else
                {
                    //trUpload.Visible = true;
                    divfileDisplayTel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkBtnOpenFilesTel_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblFileStorageTel = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorageTel");

                string openFilePath = lblFileStorageTel.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
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
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void UploadHandset_Click(string entryCode)
        {
            if (fupldDocument.PostedFile.FileName == "")
            {
                lblMessage.Text = "Please Upload Transport document.";
                return;
            }
            //if (System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".PDF" ||
            //    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".JPG" ||
            //    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".JPEG" ||
            //    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".ZIP" ||
            //    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".RAR")
            //{

            //}
            //else
            //{
            //    lblMessage.Text = "Only PDF,JPG/JPEG,ZIP and RAR files allowed.";
            //    return;
            //}

            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Telephone\\Handset\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\" + entryCode + "\\";

            System.IO.Stream fileInputStream = fupldDocument.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(fupldDocument.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocument.FileName.Trim());

            string filesToDelete = entryCode;
            string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
            System.IO.FileStream fStream = new System.IO.FileStream(@savePath + entryCode + "_" + fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write(fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();

        }
        private void CreateHandsetDocumentsStructure()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvHandsetFile.DataSource = dtDocuments;
                gvHandsetFile.DataBind();
            }
        }
        private void DisplayHandsetDocuments(string entryCode)
        {
            try
            {
                CreateHandsetDocumentsStructure();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Telephone\\Handset\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\" + entryCode + "\\";

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

                    this.gvHandsetFile.DataSource = dtDocInfo;
                    this.gvHandsetFile.DataBind();

                    if (dtDocInfo.Rows.Count > 0)
                    {
                        divfileUpload.Visible = false;
                        divfileDisplay.Visible = true;


                    }
                    else
                    {
                        //trUpload.Visible = true;
                        divfileDisplay.Visible = false;
                    }
                }
                else
                {
                    //trUpload.Visible = true;
                    divfileDisplay.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkBtnOpenFilesHandset_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblFileStorage = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorage");

                string openFilePath = lblFileStorage.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
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
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            getTelephoneData();
            divFrom.Visible = false;
            SectionList.Visible = true;
            divAlert.Visible = false;
        }

        protected void lnkTelClmNoClmNo_Click(object sender, EventArgs e)
        {
            LinkButton lnkDOMClmNo = (LinkButton)sender;

            string entryAid = ((LinkButton)lnkDOMClmNo.NamingContainer.FindControl("lnkTelClmNoClmNo")).Text;
            Label txtRmk = (Label)lnkDOMClmNo.NamingContainer.FindControl("txtRmk");



            objTele.AppNo = entryAid;
            Session["Entry_aid"] = entryAid;
            dsExp = objTele.getTeleClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
            if (dsExp.Tables.Count > 0)
            {
                SectionList.Visible = false;
                divFrom.Visible = true;
                txtdescript.Enabled = false;
                txtBillDateHand.Enabled = false;
                txtBillNoHand.Enabled = false;
                txtClaimAmtHand.Enabled = false;
                txtUserRemark.Enabled = false;
                radioPrimary1.Enabled = false;
                radioPrimary2.Enabled = false;

                txtdiscript1.Enabled = false;
                txtPhoneNo.Enabled = false;
                txtBillNo.Enabled = false;
                txtBillDate.Enabled = false;
                drpBillMonth.Enabled = false;
                txtClaimAmount.Enabled = false;


                string categoryType = dsExp.Tables[0].Rows[0]["Category_AId"].ToString();
                string desgAid = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();
                Session["Type"] = dsExp.Tables[0].Rows[0]["Type_AID"].ToString();
                ///////
                // drpType.Enabled = false;

                if (Session["Type"].ToString() == "TL000001")
                {
                    getFinYear();
                    radioPrimary1.Checked = true;
                    divTelephone.Visible = false;
                    divUpload.Visible = true;
                    divUplaodTel.Visible = false;
                    divfileUpload.Visible = false;
                    divfileDisplay.Visible = true;
                    divHandsetReimbFill.Visible = true;
                    divTeleReimbFill.Visible = false;
                    divHandset.Visible = true;
                    //drpFinYearHand.SelectedValue = dsExp.Tables[0].Rows[0]["FinYear"].ToString();
                    txtBillNoHand.Text = dsExp.Tables[0].Rows[0]["Bill_No"].ToString();
                    txtBillDateHand.Text = dsExp.Tables[0].Rows[0]["Bill_Date"].ToString();
                    txtClaimAmtHand.Text = dsExp.Tables[0].Rows[0]["Claim_Amount"].ToString();
                    txtdescript.Text = dsExp.Tables[0].Rows[0]["Description_HT_Exp"].ToString();
                    DataSet dsExphand = new DataSet();
                    dsExphand = objTele.GetHandSetReimb(Convert.ToString(Session["sCompID"]), categoryType, desgAid);
                    if (dsExp.Tables.Count > 0)
                    {
                        grHandset.DataSource = dsExphand.Tables[0];
                        grHandset.DataBind();
                        DisplayHandsetDocuments(entryAid);
                        btnSave.Visible = false;
                    }
                    else
                    {
                        grHandset.DataSource = null;
                        grHandset.DataBind();
                    }
                }
                else if (Session["Type"].ToString() == "TL000002")
                {
                    getFinYear();
                    radioPrimary2.Checked = true;
                    divHandset.Visible = false;
                    divTelephone.Visible = true;
                    //divAmt.Visible = true;
                    divUpload.Visible = false;
                    divUplaodTel.Visible = true;
                    divfileUploadTel.Visible = false;
                    divfileDisplayTel.Visible = true;
                    divHandsetReimbFill.Visible = false;
                    divTeleReimbFill.Visible = true;

                    //drpFinYear.SelectedValue = dsExp.Tables[0].Rows[0]["FinYear"].ToString();
                    txtPhoneNo.Text = dsExp.Tables[0].Rows[0]["Mobile_No"].ToString();
                    txtBillNo.Text = dsExp.Tables[0].Rows[0]["Bill_No"].ToString();
                    txtBillDate.Text = dsExp.Tables[0].Rows[0]["Bill_Date"].ToString();
                    drpBillMonth.SelectedValue = dsExp.Tables[0].Rows[0]["Bill_Month"].ToString();
                    txtClaimAmount.Text = dsExp.Tables[0].Rows[0]["Claim_Amount"].ToString();






                    DataSet dsExpTel = new DataSet();
                    dsExpTel = objTele.GetTeleReimb(Convert.ToString(Session["sCompID"]), categoryType, desgAid);
                    if (dsExp.Tables.Count > 0)
                    {
                        txtAnulAmt.Text = dsExpTel.Tables[0].Rows[0]["Fix_Amount"].ToString();
                        txtBal.Text = dsExpTel.Tables[0].Rows[0]["Fix_Amount"].ToString();

                        grTelephone.DataSource = dsExpTel.Tables[0];
                        grTelephone.DataBind();
                        DisplayTeleDocuments(entryAid);
                        btnSave.Visible = false;
                    }
                    else
                    {
                        grTelephone.DataSource = null;
                        grTelephone.DataBind();

                    }
                }
                else
                {

                    divHandset.Visible = false;
                    divTelephone.Visible = false;
                }
                if (txtRmk.Text == "Rejected")
                {
                    btnSave.Visible = true;
                }
                else
                {
                    btnSave.Visible = false;
                }

            }
        }


        protected void gvTelClaim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblType = (Label)e.Row.FindControl("lblType");
                Label lblBillMonth = (Label)e.Row.FindControl("lblBillMonth");
                Label lblMobNo = (Label)e.Row.FindControl("lblMobNo");
                Label txtRmk = (Label)e.Row.FindControl("txtRmk");
                Label lblEmpAId = (Label)e.Row.FindControl("lblEmpAId");
                Label lblClaimAmt = (Label)e.Row.FindControl("lblClaimAmt");
                Label txtAppAmtr = (Label)e.Row.FindControl("txtAppAmtr");
                LinkButton lnkTelClmNoClmNo = (LinkButton)e.Row.FindControl("lnkTelClmNoClmNo");
                DataSet ds = new DataSet();
                ds = objTele.GetTelLimit(lblEmpAId.Text, lnkTelClmNoClmNo.Text, Convert.ToString(Session["sCompID"]));

                if (txtAppAmtr.Text != "")
                {
                    if (ds.Tables[0].Rows[0]["limit_amount"].ToString() == "0")
                    {
                        lblMessage.Text = "Limit Not Found.";
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        if (decimal.Parse(txtAppAmtr.Text.Trim()) > decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                        {
                            if (e.Row.RowIndex == e.Row.RowIndex)
                            {
                                TableCell cell = e.Row.Cells[2];
                                cell.BackColor = System.Drawing.Color.Yellow;
                            }
                        }
                        else if (decimal.Parse(txtAppAmtr.Text.Trim()) < decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                        {
                            TableCell cell = e.Row.Cells[2];
                            cell.BackColor = System.Drawing.Color.LightGreen;
                        }
                    }
                }
                else
                {

                }
                txtRmk.BackColor = (txtRmk.Text.Trim() == "1" ? System.Drawing.Color.LightGray : txtRmk.BackColor);
                if (txtRmk.Text == "Submitted")
                {
                    txtRmk.Text = " Submitted.";
                }
                else if (txtRmk.Text == "Approved")
                {
                    txtRmk.Text = " Claim Approved.";
                }
                else if (txtRmk.Text == "Rejected")
                {
                    txtRmk.Text = " Claim Rejected.";
                }
                else if (txtRmk.Text == "Reverted")
                {
                    txtRmk.Text = " Claim Reverted.";
                }
                else if (txtRmk.Text == "ReChecked")
                {
                    txtRmk.Text = " Claim under ReCheck.";
                }
                else if (txtRmk.Text == "PAID")
                {
                    txtRmk.Text = " PAID.";
                }
                else
                {
                    txtRmk.Text = " Pending with " + txtRmk.Text + " .";
                }
                //if (lblType.Text == "TL000001")
                //{
                //    gvTelClaim.HeaderRow.Cells[2].Visible = false;
                //    gvTelClaim.HeaderRow.Cells[5].Visible = false;
                //    gvTelClaim.Columns[2].Visible = false;
                //    gvTelClaim.Columns[5].Visible = false;

                //}
                //else
                //{
                //    lblBillMonth.Visible = false;
                //    lblMobNo.Visible = false;
                //    gvTelClaim.HeaderRow.Cells[2].Visible = true;
                //    gvTelClaim.HeaderRow.Cells[5].Visible = true;
                //    gvTelClaim.Columns[2].Visible = true;
                //    gvTelClaim.Columns[5].Visible = true;
                //}
            }
        }

        protected void drpBillMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            objTele.Group_Type = "TELE";
            objTele.BillMonth = drpBillMonth.SelectedValue;
            dsExp = objTele.CheckBillMonth(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
            if (dsExp.Tables.Count > 0)
            {
                string countTable = dsExp.Tables.Count.ToString();
                if (dsExp.Tables[0].Rows[0]["result"].ToString() == "Only one claim allowed per month for each employee")
                {
                    lblMessage.Text = "Only one claim allowed per month for each employee.";
                    string script = $@"<script type='text/javascript'>alert('Only one claim allowed per month for each employee.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    lblMessage.Visible = false;
                    divAlert.Visible = false;
                    lblMessage.Text = "";
                }
            }
        }

        private Boolean ValidateTele()
        {
            if (FileUploadTel.PostedFile != null)
            {
                if (Convert.ToString(FileUploadTel.PostedFile.FileName) == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse Telephone file to upload.";
                    //ShowMessage("Browse file to upload.", WarningType.Danger);
                    string script = $@"<script type='text/javascript'>alert('Browse Telephone file to upload.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    return false;
                }
                else
                {

                    return true;
                }
            }
            return true;
        }

        private Boolean ValidateHand()
        {
            if (fupldDocument.PostedFile != null)
            {
                if (Convert.ToString(fupldDocument.PostedFile.FileName) == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse Handset file to upload.";
                    //ShowMessage("Browse file to upload.", WarningType.Danger);
                    string script = $@"<script type='text/javascript'>alert('Browse Handset file to upload.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    return false;
                }
                else
                {

                    return true;
                }
            }
            return true;
        }

        private Boolean validateHandTotalamount()
        {
            if (txtClaimAmtHand.Text == "")
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Please enter Handset amount.";
                string script = $@"<script type='text/javascript'>alert('Please enter Handset amount.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            else
            {

                return true;
            }
        }

        private Boolean validateTeleTotalamount()
        {
            if (txtClaimAmount.Text == "")
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Please enter Telephone amount.";
                string script = $@"<script type='text/javascript'>alert('Please enter Telephone amount.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            else
            {

                return true;
            }
        }

    }
}