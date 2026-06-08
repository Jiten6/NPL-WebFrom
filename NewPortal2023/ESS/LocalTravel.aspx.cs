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
    public partial class LocalTravel : System.Web.UI.Page
    {
        NewPortal2023.ESS.LocalExpenses objLoc = new NewPortal2023.ESS.LocalExpenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsExp = new DataSet();
        private string SourcePath = string.Empty;
        private string savePath = string.Empty;
        private string SourcePath2 = string.Empty;
        private string savePath2 = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sCompID"] != null)
            { 
                if (!Page.IsPostBack)
                {
                    ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                    Session["Entry_aid"] = null;

                    getLocalData();
                    getAdvanceData();
                    getCategoryType();
                    divAlert.Visible = false;
                    Sectionadvlist.Visible = true;

                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


        private void getLocalData()

        {
            try
            {
                getCategoryType();
                dsExp = objLoc.GeLocalList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables[0].Rows.Count > 0)
                {
                    this.gvLocalClaimList.DataSource = dsExp.Tables[0];
                    this.gvLocalClaimList.DataBind();

                }
                else
                {
                    this.gvLocalClaimList.DataSource = null;
                    this.gvLocalClaimList.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void getAdvanceData()
        {
            try
            {

                dsExp = objExp.GetempadvanceList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "Local");
                if (dsExp.Tables[0].Rows.Count > 0)
                {
                    Session["Entry_aid"] = null;
                    this.gvAdvanceClaimList.DataSource = dsExp.Tables[0];
                    this.gvAdvanceClaimList.DataBind();
                    //getCategoryType();
                }
                else
                {
                    this.gvAdvanceClaimList.DataSource = null;
                    this.gvAdvanceClaimList.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void getCategoryType()
        {
            try
            {
                objLoc = new NewPortal2023.ESS.LocalExpenses();
                dsExp = objLoc.GetLocCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables.Count > 0)
                {

                    Session["CATEGORY_TYPE"] = dsExp.Tables[0].Rows[0]["CATEGORY_TYPE"].ToString();
                    Session["DESG_AID"] = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();

                    //if (Session["CATEGORY_TYPE"].ToString() == "CT000001")
                    //{
                    //    chk1.Visible = true;
                    //    chk2.Visible = true;
                    //    chk3.Visible = true;
                    //    chk4.Visible = true;
                    //}
                    //else
                    ////{
                    ////    chk1.Visible = false;
                    ////    chk2.Visible = false;
                    ////    chk3.Visible = false;
                    ////    chk4.Visible = false;
                    ////}
                    //if (Session["CATEGORY_TYPE"].ToString() == "CT000002")
                    //{
                    //    chk1.Visible = false;
                    //    chk2.Visible = true;
                    //    chk3.Visible = true;
                    //    chk4.Visible = true;
                    //}
                    ////else
                    ////{
                    ////    chk1.Visible = false;
                    ////    chk2.Visible = false;
                    ////    chk3.Visible = false;
                    ////    chk4.Visible = false;
                    ////}
                    //if (Session["CATEGORY_TYPE"].ToString() == "CT000003")
                    //{
                    //    chk1.Visible = false;
                    //    chk2.Visible = true;
                    //    chk3.Visible = false;
                    //    chk4.Visible = true;
                    //}
                    ////else
                    ////{
                    ////    chk1.Visible = false;
                    ////    chk2.Visible = false;
                    ////    chk3.Visible = false;
                    ////    chk4.Visible = false;
                    ////}

                    getLocalReimb();
                }


            }
            catch (Exception ex)
            {
                lblMessage.Text = "Category Not Found.";
            }
        }
        private void getLocalReimb()
        {
            Session["Entry_aid"] = null;
            dsExp = objLoc.GetLocalReimb(Convert.ToString(Session["sCompID"]), Convert.ToString(Session["CATEGORY_TYPE"]));


            if (dsExp.Tables.Count > 0)
            {
                grLocalReimb.DataSource = dsExp.Tables[0];
                grLocalReimb.DataBind();
            }
            else
            {
                grLocalReimb.DataSource = null;
                grLocalReimb.DataBind();

            }


        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            dsExp = objExp.GetCount(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "Local");
            string count = dsExp.Tables[0].Rows[0]["COUNT"].ToString();
            if (count != "0")
            {
                string script = $@"<script type='text/javascript'>alert('Kindly Claim for the Advance Paid.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlert, "success");
                lblMessage.Visible = true;
                lblMessage.Text = "Kindly Claim for the Advance Paid.";
                divAlert.Visible = true;

            }
            else
            {
                SectionList.Visible = false;
                divAlert.Visible = false;
                divFrom.Visible = true;
                divTravel.Visible = true;
                divUpload.Visible = true;
                divfileUpload.Visible = true;
                btnSave.Visible = true;
                txtFromDate.Text = "";
                txtCashVocher.Text = "";

                chk1.Enabled = true;
                txtchk1.Enabled = true;
                chk2.Enabled = true;
                txtchk2.Enabled = true;
                chk3.Enabled = true;
                txtchk3.Enabled = true;
                chk4.Enabled = true;
                txtchk4.Enabled = true;
                txtFromDate.Enabled = true;
                txtNameAss.Enabled = true;
                txtCashVocher.Enabled = true;
                txtTravelDes.Enabled = true;
                txtMeal.Enabled = true;
                txtOtherExpenses.Enabled = true;
                txtUserRemarks.Enabled = true;
                divfileDisplay.Visible = false;
                Sectionadvlist.Visible = false;

                txtTravelDes.Text = "";
                txtMeal.Text = "";
                txtOtherExpenses.Text = "";
                txtNameAss.Text = "";
                txtchk1.Text = "";
                txtchk2.Text = "";
                txtchk3.Text = "";
                txtchk4.Text = "";
                txtadv.Text = "";
                txtchk1.Visible = false;
                txtchk2.Visible = false;
                txtchk3.Visible = false;
                txtchk4.Visible = false;
                chk1.Checked = false;
                chk2.Checked = false;
                chk3.Checked = false;
                chk4.Checked = false;

                txtadvid.Enabled = false;
                txtadv.Enabled = false;
                txtadvvoucher.Enabled = false;
                getCategoryType();
                getLocalReimb();
            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //dsExp = objLoc.InsertLocClaim(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
            string entryCode = (string)Session["entryCode"];
            Upload_Click(entryCode);
        }


        private Boolean ValidateDocument()
        {
            if (fupldDocument.PostedFile != null)
            {
                if (Convert.ToString(fupldDocument.PostedFile.FileName) == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse file to upload.";
                    //ShowMessage("Browse file to upload.", WarningType.Danger);
                    string script = $@"<script type='text/javascript'>alert('Browse file to upload.');</script>";
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


        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objLoc.Expenses_Date = txtFromDate.Text;
                objLoc.Cash_Voucher = txtCashVocher.Text;
                objLoc.Desg_Aid = Session["DESG_AID"].ToString();
                objLoc.CategoryType = Session["CATEGORY_TYPE"].ToString();
                objLoc.Travel_Description = txtTravelDes.Text;
                if (txtMeal.Text != "")
                {
                    objLoc.Meal = txtMeal.Text;
                }
                else
                {
                    objLoc.Meal = "0";
                }

                if (txtOtherExpenses.Text != "")
                {
                    objLoc.Other_Expenses = txtOtherExpenses.Text;
                }
                else
                {
                    objLoc.Other_Expenses = "0";
                }

                objLoc.advance = txtadv.Text;
                //objLoc.Other_Expenses = txtOtherExpenses.Text;
                objLoc.Name_Bussi_Ass = txtNameAss.Text;
                objLoc.UserRemarks = txtUserRemarks.Text;
                if (chk1.Checked == true)
                {
                    objLoc.Claim1_Amount = txtchk1.Text;
                }
                else
                {
                    objLoc.Claim1_Amount = "0";
                }
                if (chk2.Checked == true)
                {
                    objLoc.Claim2_Amount = txtchk2.Text;
                }
                else
                {
                    objLoc.Claim2_Amount = "0";
                }
                if (chk3.Checked == true)
                {
                    objLoc.Claim3_Amount = txtchk3.Text;
                }
                else
                {
                    objLoc.Claim3_Amount = "0";
                }
                if (chk4.Checked == true)
                {
                    objLoc.Claim4_Amount = txtchk4.Text;
                }
                else
                {
                    objLoc.Claim4_Amount = "0";
                }

                objLoc.FilingStatus = "S";
                objLoc.Status = "9";
                if (Session["Entry_aid"] != null)
                {
                    objLoc.EntryAid = Session["Entry_aid"].ToString();
                }

                if (Session["advid"] != null)
                {
                    objLoc.advid = (string)(Session["advid"]);
                }
                if (ValidateDocument() == false)
                {
                    return;
                }

                dsExp = objLoc.InsertLocClaim(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));

                if (dsExp.Tables.Count > 0)
                {
                    string entryCode = dsExp.Tables[0].Rows[0]["Claim_no"].ToString();

                    Session["advid"] = "";
                    divfileUpload.Visible = false;
                    divfileDisplay.Visible = true;
                    Upload_Click(entryCode);
                    DisplayDocuments(entryCode);
                    btnSave.Visible = false;
                    string script = $@"<script type='text/javascript'>alert('Claim Submitted Successfully.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    lblMessage.Visible = true;
                    lblMessage.Text = "Claim Submitted Successfully.";
                    SENDUDATEMAIL(txtFromDate.Text, "Local Expense");
                    getLocalData();
                    SectionList.Visible = true;
                    divFrom.Visible = false;
                    divAlert.Visible = true;
                }

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
         

            string EMPNAME = Session["sEmpName"].ToString();

            string clientbodys = "Dear " + EMPNAME + ",<br><br>Your " + type + " Expense is Submitted Successfully.<br>"
               + "Once the action taken will be notified to you through mail or same can be checked through ESS portal."
               + "<br>Reimbursement type:- " + type
               + "<br>Applied Date :- " + date
               + "<br>Bill Date :- " + frDate
               + "<br><br>ThankYou.<br><br>";
            string emails = Session["sEmailId"].ToString();
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

        protected void Upload_Click(string entryCode)
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
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Local\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\" + entryCode + "\\";

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

        private void CreateDocumentsStructure()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvDomesticFile.DataSource = dtDocuments;
                gvDomesticFile.DataBind();
            }
        }
        private void DisplayDocuments(string entryCode)
        {
            try
            {
                CreateDocumentsStructure();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Local\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\" + entryCode + "\\";

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

                    this.gvDomesticFile.DataSource = dtDocInfo;
                    this.gvDomesticFile.DataBind();

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
        protected void lnkBtnOpenFiles_Click(object sender, EventArgs e)
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
            divFrom.Visible = false;
            divTravel.Visible = false;
            SectionList.Visible = true;
            Sectionadvlist.Visible = true;
            getLocalData();
            getAdvanceData();
        }


        protected void lnkLOCClmNoClmNo_Click(object sender, EventArgs e)
        {
            LinkButton lnkDOMClmNo = (LinkButton)sender;

            string entryAid = ((LinkButton)lnkDOMClmNo.NamingContainer.FindControl("lnkLOCClmNoClmNo")).Text;
            Label txtRmk = (Label)lnkDOMClmNo.NamingContainer.FindControl("txtRmk");



            objLoc.AppNo = entryAid;
            Session["Entry_aid"] = entryAid;

            dsExp = objLoc.getLocalClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
            if (dsExp.Tables.Count > 0)
            {
                SectionList.Visible = false;
                divFrom.Visible = true;
                divUpload.Visible = true;
                divfileUpload.Visible = false;
                divfileDisplay.Visible = true;
                divTravel.Visible = true;
                Sectionadvlist.Visible = false;
                //btnCalTtl.Visible = false;

                string categoryType = dsExp.Tables[0].Rows[0]["Category_AId"].ToString();
                string desgAid = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();


                //if (categoryType == "CT000001")
                //{
                //    chk1.Visible = true;
                //    chk2.Visible = true;
                //    chk3.Visible = true;
                //    chk4.Visible = true;
                //}
                //else
                //{
                //    chk1.Visible = false;
                //    chk2.Visible = false;
                //    chk3.Visible = false;
                //    chk4.Visible = false;
                //}
                //if (categoryType == "CT000002")
                //{
                //    chk1.Visible = false;
                //    chk2.Visible = true;
                //    chk3.Visible = true;
                //    chk4.Visible = true;
                //}
                //else
                //{
                //    chk1.Visible = false;
                //    chk2.Visible = false;
                //    chk3.Visible = false;
                //    chk4.Visible = false;
                //}
                //if (categoryType == "CT000003")
                //{
                //    chk1.Visible = false;
                //    chk2.Visible = true;
                //    chk3.Visible = false;
                //    chk4.Visible = true;
                //}
                //else
                //{
                //    chk1.Visible = false;
                //    chk2.Visible = false;
                //    chk3.Visible = false;
                //    chk4.Visible = false;
                //}

                if (dsExp.Tables[0].Rows[0]["Claim1_Amount"].ToString() != "0.00")
                {
                    chk1.Checked = true;
                    txtchk1.Visible = true;
                    txtchk1.Text = dsExp.Tables[0].Rows[0]["Claim1_Amount"].ToString();
                }
                else
                {
                    chk1.Checked = false;
                    txtchk1.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["Claim2_Amount"].ToString() != "0.00")
                {
                    chk2.Checked = true;
                    txtchk2.Visible = true;
                    txtchk2.Text = dsExp.Tables[0].Rows[0]["Claim2_Amount"].ToString();
                }
                else
                {
                    chk2.Checked = false;
                    txtchk2.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["Claim3_Amount"].ToString() != "0.00")
                {
                    chk3.Checked = true;
                    txtchk3.Visible = true;
                    txtchk3.Text = dsExp.Tables[0].Rows[0]["Claim3_Amount"].ToString();
                }
                else
                {
                    chk3.Checked = false;
                    txtchk3.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["Claim4_Amount"].ToString() != "0.00")
                {
                    chk4.Checked = true;
                    txtchk4.Visible = true;
                    txtchk4.Text = dsExp.Tables[0].Rows[0]["Claim4_Amount"].ToString();
                }
                else
                {
                    chk4.Checked = false;
                    txtchk4.Visible = false;

                }
                txtFromDate.Text = dsExp.Tables[0].Rows[0]["Expenses_Date"].ToString();
                txtCashVocher.Text = dsExp.Tables[0].Rows[0]["Cash_Voucher"].ToString();

                txtTravelDes.Text = dsExp.Tables[0].Rows[0]["Travel_Description"].ToString();
                txtMeal.Text = dsExp.Tables[0].Rows[0]["Meal"].ToString();
                txtOtherExpenses.Text = dsExp.Tables[0].Rows[0]["Other_Expenses"].ToString();
                txtNameAss.Text = dsExp.Tables[0].Rows[0]["Name_Bussi_Ass"].ToString();
                txtUserRemarks.Text = dsExp.Tables[0].Rows[0]["Claim_Remark"].ToString();
                txtadv.Text = dsExp.Tables[0].Rows[0]["ADVANCE_AMT"].ToString();
                Calculatetotalexp();
                txtTotalexp.Text = txtTotalexp.Text;

                dsExp = objLoc.GetLocalReimb(Convert.ToString(Session["sCompID"]), Convert.ToString(Session["CATEGORY_TYPE"]));

                chk1.Enabled = false;
                txtchk1.Enabled = false;
                chk2.Enabled = false;
                txtchk2.Enabled = false;
                chk3.Enabled = false;
                txtchk3.Enabled = false;
                chk4.Enabled = false;
                txtchk4.Enabled = false;
                txtFromDate.Enabled = false;
                txtNameAss.Enabled = false;
                txtCashVocher.Enabled = false;
                txtTravelDes.Enabled = false;
                txtMeal.Enabled = false;
                txtOtherExpenses.Enabled = false;
                txtUserRemarks.Enabled = false;
                txtadv.Enabled = false;

                if (dsExp.Tables.Count > 0)
                {
                    grLocalReimb.DataSource = dsExp.Tables[0];
                    grLocalReimb.DataBind();
                    DisplayDocuments(entryAid);
                    btnSave.Visible = false;
                }
                else
                {
                    grLocalReimb.DataSource = null;
                    grLocalReimb.DataBind();
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

        protected void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked == true)
            {
                txtchk1.Visible = true;
            }
            else
            {
                txtchk1.Visible = false;
            }
        }

        protected void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk2.Checked == true)
            {
                txtchk2.Visible = true;
            }
            else
            {
                txtchk2.Visible = false;
            }
        }

        protected void chk3_CheckedChanged(object sender, EventArgs e)
        {
            if (chk3.Checked == true)
            {
                txtchk3.Visible = true;
            }
            else
            {
                txtchk3.Visible = false;
            }
        }

        protected void chk4_CheckedChanged(object sender, EventArgs e)
        {
            if (chk4.Checked == true)
            {
                txtchk4.Visible = true;
            }
            else
            {
                txtchk4.Visible = false;
            }
        }

        protected void gvLocalClaimList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label txtRmk = (Label)e.Row.FindControl("txtRmk");
                    //TextBox txtdescri = (TextBox)e.Row.FindControl("txtdescri");
                    DropDownList ddls = (DropDownList)e.Row.FindControl("drpAction");
                    Label lblEmpAId = (Label)e.Row.FindControl("lblEmpAId");
                    Label txtClmAmt = (Label)e.Row.FindControl("txtClmAmt");
                    Label txtAppAmtr = (Label)e.Row.FindControl("txtAppAmtr");
                    Label EntertainmentChked = (Label)e.Row.FindControl("EntertainmentChked");
                    LinkButton lnkLOCClmNoClmNo = (LinkButton)e.Row.FindControl("lnkLOCClmNoClmNo");
                    //DataSet ds = new DataSet();
                    //ds = objLoc.GetDOMLimit(lblEmpAId.Text, lnkDOMClmNoClmNo.Text, Convert.ToString(Session["sCompID"]));
                    if (txtAppAmtr.Text != "")
                    {
                        if (EntertainmentChked.Text == "T")
                        {
                            TableCell cell = e.Row.Cells[2];
                            cell.BackColor = System.Drawing.Color.Orange;

                        }
                        else if (EntertainmentChked.Text == "F")
                        {
                            TableCell cell = e.Row.Cells[2];
                            cell.BackColor = System.Drawing.Color.LightGreen;
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
                        txtRmk.Text = "Claim Approved.";
                    }
                    else if (txtRmk.Text == "Rejected")
                    {
                        txtRmk.Text = "Claim Rejected.";
                    }
                    else if (txtRmk.Text == "ReChecked")
                    {
                        txtRmk.Text = " Claim Under ReCheck.";
                    }
                    else if (txtRmk.Text == "Reverted")
                    {
                        txtRmk.Text = " Claim Reverted.";
                    }
                    else if (txtRmk.Text == "PAID")
                    {
                        txtRmk.Text = " PAID.";
                    }
                    else
                    {
                        txtRmk.Text = " Pending With " + txtRmk.Text + " .";
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Category Not Found.";
            }

        }

        private void Calculatetotalexp()
        {
            decimal chk1 = string.IsNullOrEmpty(txtchk1.Text) ? 0 : decimal.Parse(txtchk1.Text);
            decimal chk2 = string.IsNullOrEmpty(txtchk2.Text) ? 0 : decimal.Parse(txtchk2.Text);
            decimal chk3 = string.IsNullOrEmpty(txtchk3.Text) ? 0 : decimal.Parse(txtchk3.Text);
            decimal chk4 = string.IsNullOrEmpty(txtchk4.Text) ? 0 : decimal.Parse(txtchk4.Text);
            decimal meal = string.IsNullOrEmpty(txtMeal.Text) ? 0 : decimal.Parse(txtMeal.Text);
            decimal otherExpenses = string.IsNullOrEmpty(txtOtherExpenses.Text) ? 0 : decimal.Parse(txtOtherExpenses.Text);

            decimal adv = string.IsNullOrEmpty(txtadv.Text) ? 0 : decimal.Parse(txtadv.Text);

            decimal totalExpenses = chk1 + chk2 + chk3 + chk4 + meal + otherExpenses;
            decimal paid = totalExpenses - adv;

            txtTotalexp.Text = totalExpenses.ToString();
            txtpaidamt.Text = paid.ToString();
        }

        protected void txtchk1_TextChanged(object sender, EventArgs e)
        {
            Calculatetotalexp();
        }

        protected void txtchk2_TextChanged(object sender, EventArgs e)
        {
            Calculatetotalexp();
        }

        protected void txtchk3_TextChanged(object sender, EventArgs e)
        {
            Calculatetotalexp();
        }

        protected void txtchk4_TextChanged(object sender, EventArgs e)
        {
            Calculatetotalexp();
        }

        protected void txtMeal_TextChanged(object sender, EventArgs e)
        {
            Calculatetotalexp();
        }

        protected void txtOtherExpenses_TextChanged(object sender, EventArgs e)
        {
            Calculatetotalexp();
        }

        protected void btnCalTtl_Click(object sender, EventArgs e)
        {
            Calculatetotalexp();
        }

        protected void txtadv_TextChanged(object sender, EventArgs e)
        {
            Calculatetotalexp();
        }

        protected void lnkadvClmNoClmNo_Click(object sender, EventArgs e)
        {
            LinkButton lnkDOMClmNo = (LinkButton)sender;

            string entryAid = ((LinkButton)lnkDOMClmNo.NamingContainer.FindControl("lnkadvClmNoClmNo")).Text;
            Label txtstatus = (Label)lnkDOMClmNo.NamingContainer.FindControl("txtstatus");



            objLoc.AppNo = entryAid;
            Session["Entry_aid"] = entryAid;

            dsExp = objLoc.getAdvanceClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
            if (dsExp.Tables.Count > 0)
            {
                SectionList.Visible = false;
                Sectionadvlist.Visible = false;
                divadvclaim.Visible = true;

                //string categorytype = dsExp.tables[0].rows[0]["category_aid"].tostring();
                //string desgaid = dsExp.tables[0].rows[0]["desg_aid"].tostring();


                txtDate.Text = dsExp.Tables[0].Rows[0]["advance_date"].ToString();
                txtname.Text = dsExp.Tables[0].Rows[0]["emp_name"].ToString();

                txtempcode.Text = dsExp.Tables[0].Rows[0]["emp_code"].ToString();
                drptypelist.Text = dsExp.Tables[0].Rows[0]["expense_type"].ToString();
                txtadvclaimamt.Text = dsExp.Tables[0].Rows[0]["advance_amount"].ToString();
                txtpuradv.Text = dsExp.Tables[0].Rows[0]["advance_purpose"].ToString();
                txtvoucher.Text = dsExp.Tables[0].Rows[0]["ADVANCE_VOUCHER"].ToString();
                //txtuserremarks.text = dsexp.tables[0].rows[0]["claim_remark"].tostring();
                //calculatetotalexp();
                //txttotalexp.text = txttotalexp.text;
                Session["advvoucher"] = txtvoucher.Text;

                // dsexp = objloc.getlocalreimb(convert.tostring(session["scompid"]), convert.tostring(session["category_type"]));


                txtDate.Enabled = false;
                txtname.Enabled = false;
                txtempcode.Enabled = false;
                drptypelist.Enabled = false;
                txtadvclaimamt.Enabled = false;
                txtpuradv.Enabled = false;
                //txtuserremarks.enabled = false;

                btnClose.Visible = true;

            }
        }

        protected void btnadvClose_Click(object sender, EventArgs e)
        {
            SectionList.Visible = true;
            Sectionadvlist.Visible = true;
            divadvclaim.Visible = false;

        }
        protected void btnclaim_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            LinkButton lnkRequestNo = (LinkButton)sender;
            string lnkAppNo = ((Label)lnkRequestNo.NamingContainer.FindControl("lnkappClmNo")).Text;
            string lblamount = ((Label)lnkRequestNo.NamingContainer.FindControl("txtadvAmt")).Text;
            // string voucher = ((Label)lnkRequestNo.NamingContainer.FindControl("ADVANCE_VOUCHER")).Text;

            Session["advid"] = lnkAppNo;
            Session["advamount"] = lblamount;

            txtadv.Text = (string)(Session["advamount"]);

            txtadvvoucher.Text = (string)(Session["advvoucher"]);
            txtadvid.Text = (string)(Session["advid"]);

            txtadvid.Enabled = false;
            txtadv.Enabled = false;
            txtadvvoucher.Enabled = false;
            // Session["advvoucher"] = voucher;
            SectionList.Visible = false;
            divAlert.Visible = false;
            divFrom.Visible = true;
            divTravel.Visible = true;
            divUpload.Visible = true;
            divfileUpload.Visible = true;
            btnSave.Visible = true;
            txtFromDate.Text = "";
            txtCashVocher.Text = "";

            chk1.Enabled = true;
            txtchk1.Enabled = true;
            chk2.Enabled = true;
            txtchk2.Enabled = true;
            chk3.Enabled = true;
            txtchk3.Enabled = true;
            chk4.Enabled = true;
            txtchk4.Enabled = true;
            txtFromDate.Enabled = true;
            txtNameAss.Enabled = true;
            txtCashVocher.Enabled = true;
            txtTravelDes.Enabled = true;
            txtMeal.Enabled = true;
            txtOtherExpenses.Enabled = true;
            txtUserRemarks.Enabled = true;
            divfileDisplay.Visible = false;
            Sectionadvlist.Visible = false;

            txtTravelDes.Text = "";
            txtMeal.Text = "";
            txtOtherExpenses.Text = "";
            txtNameAss.Text = "";
            txtchk1.Text = "";
            txtchk2.Text = "";
            txtchk3.Text = "";
            txtchk4.Text = "";

            txtchk1.Visible = false;
            txtchk2.Visible = false;
            txtchk3.Visible = false;
            txtchk4.Visible = false;
            chk1.Checked = false;
            chk2.Checked = false;
            chk3.Checked = false;
            chk4.Checked = false;
            getCategoryType();
            getLocalReimb();
        }
    }
}