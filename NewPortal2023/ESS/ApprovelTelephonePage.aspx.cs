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
    public partial class ApprovelTelephonePage : System.Web.UI.Page
    {
        NewPortal2023.Helper.ExpenseEmail obj = new NewPortal2023.Helper.ExpenseEmail();
        NewPortal2023.ESS.TelephoneExpenses objTele = new NewPortal2023.ESS.TelephoneExpenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsExp = new DataSet();
        DataSet dsExp1 = new DataSet();
        private string SourcePath = string.Empty;
        private string savePath = string.Empty;
        private string SourcePath2 = string.Empty;
        private string savePath2 = string.Empty;
        string flagType = "";
        protected void Page_Load(object sender, EventArgs e)
        
{
            if (!Page.IsPostBack)
            {
                getTelephoneData();
                string id = (string)Session["sEmpID"];
                //getFinYear();
            }
        }

        private void getTelephoneData()

        {
            try
            {
                //getCategoryType();
                getCategoryType(flagType);
                dsExp = objTele.GetApproverTeleList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables[0].Rows.Count == 0)
                {
                    btnSubmitAll.Visible = false;
                    lblActionAll.Visible = false;
                    drActionAll.Visible = false;
                    lblRemarksAll.Visible = false;
                    txtAllRmk.Visible = false;
                }
                else
                {
                    btnSubmitAll.Visible = true;
                    lblActionAll.Visible = true;
                    drActionAll.Visible = true;
                    lblRemarksAll.Visible = true;
                    txtAllRmk.Visible = true;
                }
                dsExp1 = objTele.GetRole(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp1.Tables[0].Rows.Count > 0)
                {
                    if (dsExp1.Tables[0].Rows[0]["FLAG"].ToString() == "HOD")
                    {
                        radiaoEntertainment1.Enabled = true;
                    }
                    else
                    {
                        radiaoEntertainment1.Enabled = false;
                    }
                    if (dsExp.Tables[0].Rows.Count > 0)
                    {
                        Session["ClmType"] = dsExp.Tables[0].Rows[0]["ClmType"].ToString();
                    }
                    if (Session["ClmType"].ToString() == "CFO")
                    {
                        drpActionType.Items.Insert(3, new ListItem("Revert", "Revert"));
                        drActionAll.Items.Insert(3, new ListItem("Revert", "Revert"));
                    }
                    if (Session["ClmType"].ToString() == "SEQUEL")
                    {
                        txtApprovedAmt.Enabled = true;
                        //divNote.Visible = false;
                    }
                    else
                    {
                        txtApprovedAmt.Enabled = false;
                    }


                    this.gvTelClaim.DataSource = dsExp.Tables[0];
                    this.gvTelClaim.DataBind();
                    //getCategoryType();
                    getCategoryType(flagType);
                }
                else
                {
                    this.gvTelClaim.DataSource = null;
                    this.gvTelClaim.DataBind();

                }


            }
            catch (Exception ex)
            {

            }
        }
        private void getCategoryType(string flagtype)
        {
            try
            {
                if (flagType == "Handset")
                {
                    objTele = new NewPortal2023.ESS.TelephoneExpenses();
                    dsExp = objTele.GetTeleCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), flagType);
                    if (dsExp.Tables.Count > 0)
                    {
                        Session["CATEGORY_AID"] = dsExp.Tables[0].Rows[0]["CATEGORY_AID"].ToString();
                        Session["DESG_AID"] = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();
                    }
                }
                else if (flagType == "Telephone bill")
                {

                    objTele = new NewPortal2023.ESS.TelephoneExpenses();
                    dsExp = objTele.GetTeleCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), flagType);
                    if (dsExp.Tables.Count > 0)
                    {
                        Session["CATEGORY_AID"] = dsExp.Tables[0].Rows[0]["CATEGORY_AID"].ToString();
                        Session["DESG_AID"] = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();
                    }
                }
                else
                {
                    objTele = new NewPortal2023.ESS.TelephoneExpenses();
                    dsExp = objTele.GetTeleCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), flagType);
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
        //            Session["CATEGORY_TYPE"] = dsExp.Tables[0].Rows[0]["CATEGORY_TYPE"].ToString();
        //            Session["DESG_AID"] = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Category Not Found.";
        //    }
        //}

        //public void getFinYear()
        //{
        //    DataSet ds = objTele.GetFinYear("CO000141");//((string)Session["sCompID"]);
        //    if (Session["Type"] != null)
        //    {
        //        if (Session["Type"].ToString() == "TL000001")
        //        {
        //            drpFinYearHand.Items.Clear();
        //            drpFinYearHand.DataTextField = "AID";
        //            drpFinYearHand.DataValueField = "DESC";
        //            drpFinYearHand.DataSource = ds;
        //            drpFinYearHand.DataBind();
        //            drpFinYearHand.Items.Insert(0, new ListItem("[Select One]", "0"));
        //        }
        //        else if (Session["Type"].ToString() == "TL000002")
        //        {
        //            drpFinYear.Items.Clear();
        //            drpFinYear.DataTextField = "AID";
        //            drpFinYear.DataValueField = "DESC";
        //            drpFinYear.DataSource = ds;
        //            drpFinYear.DataBind();
        //            drpFinYear.Items.Insert(0, new ListItem("[Select One]", "0"));
        //        }
        //    }
        //}

        protected void radioPrimary1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioPrimary1.Checked == true)
            {
                Session["Type"] = "TL000001";
                divHandset.Visible = true;
                divTelephone.Visible = false;
                //divAmt.Visible = true;

                divUpload.Visible = true;
                divUplaodTel.Visible = false;


                divfileUpload.Visible = true;
                divfileDisplay.Visible = false;
                divHandsetReimbFill.Visible = true;
                divTeleReimbFill.Visible = false;

                dsExp = objTele.GetHandSetReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_TYPE"]), (string)(Session["DESG_AID"]));
                if (dsExp.Tables.Count > 0)
                {
                    Session["GROUP_TYPE_AID"] = dsExp.Tables[0].Rows[0]["GROUP_TYPE_AID"].ToString();
                    Session["GROUP_TYPE"] = dsExp.Tables[0].Rows[0]["GROUP_TYPE"].ToString();
                    grHandset.DataSource = dsExp.Tables[0];
                    grHandset.DataBind();
                    // getFinYear();
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

                divUpload.Visible = false;
                divUplaodTel.Visible = true;


                divfileUploadTel.Visible = true;
                divfileDisplayTel.Visible = false;
                divHandsetReimbFill.Visible = false;
                divTeleReimbFill.Visible = true;

                dsExp = objTele.GetTeleReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_TYPE"]), (string)(Session["DESG_AID"]));
                if (dsExp.Tables.Count > 0)
                {
                    Session["TELEGROUP_TYPE_AID"] = dsExp.Tables[0].Rows[0]["GROUP_TYPE_AID"].ToString();
                    Session["TELEGROUP_TYPE"] = dsExp.Tables[0].Rows[0]["GROUP_TYPE"].ToString();
                    grTelephone.DataSource = dsExp.Tables[0];
                    grTelephone.DataBind();
                    //getFinYear();
                }
                else
                {
                    grTelephone.DataSource = null;
                    grTelephone.DataBind();
                }
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

            //sdrpFinYearHand.SelectedIndex = -1;

            txtBillNoHand.Text = "";
            txtBillDateHand.Text = "";


            //drpFinYear.SelectedIndex = -1;
            txtClaimAmtHand.Text = "";
            txtPhoneNo.Text = "";
            txtBillNo.Text = "";
            txtBillDate.Text = "";
            txtBillMonth.Text = "";
            txtClaimAmount.Text = "";
            txtdiscript.Text = "";
            radioPrimary1.Checked = false;
            radioPrimary2.Checked = false;

        }




        protected void drpFinYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (drpFinYear.SelectedItem.ToString() != "")
                //{
                //    //string id = "";
                //    //DataSet ds = objFlexi.GetReimbDataAllTelClaim((string)Session["sCompID"], (string)Session["sEmpID"], "AD000207", id);
                //    string drpFinYears = drpFinYear.SelectedItem.ToString();
                //    //DisplayDataByFinancialYear(drpFinYears);
                //    //ShowTelDataByFinYear(drpFinYears);

                //}
                //else
                //{
                //    //txtBillDate.Text = "";
                //    //txtBillPeriodStart.Text = "";
                //    //txtBillPeriodStart.Text = "";
                //}
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(ex.Message) + "');");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (radioPrimary1.Checked == true)
                {
                    objTele.Type = "TL000001";
                    //objTele.FinYears = drpFinYearHand.SelectedValue;
                    objTele.CategoryAid = Session["CATEGORY_TYPE"].ToString();
                    objTele.Desg_Aid = Session["DESG_AID"].ToString();
                    objTele.Group_Type_Aid = Session["GROUP_TYPE_AID"].ToString();
                    objTele.Group_Type = Session["GROUP_TYPE"].ToString();

                    objTele.BillNo = txtBillNoHand.Text;
                    objTele.BillDate = txtBillDateHand.Text;
                    objTele.Description_HT_Exp = txtdiscript.Text;

                    objTele.BillAmt = txtClaimAmtHand.Text;
                    objTele.FilingStatus = "S";
                    objTele.Status = "NULL";
                    dsExp = objTele.InsertHandsetClaim(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                    if (dsExp.Tables.Count > 0)
                    {
                        string entryCode = dsExp.Tables[0].Rows[0]["Claim_no"].ToString();
                        divfileUploadTel.Visible = false;
                        divfileDisplayTel.Visible = true;
                        UploadHandset_Click(entryCode);
                        //DisplayHandsetDocuments(entryCode, EmployeeID);

                    }


                }
                else if (radioPrimary2.Checked == true)
                {
                    objTele.Type = "TL000002";
                    //objTele.FinYears = drpFinYear.SelectedValue;
                    objTele.CategoryAid = Session["CATEGORY_TYPE"].ToString();
                    objTele.Desg_Aid = Session["DESG_AID"].ToString();
                    objTele.Group_Type_Aid = Session["TELEGROUP_TYPE_AID"].ToString();
                    objTele.Group_Type = Session["TELEGROUP_TYPE_AID"].ToString();
                    objTele.PhoneNumber = txtPhoneNo.Text;
                    objTele.BillNo = txtBillNo.Text;
                    objTele.BillDate = txtBillDate.Text;
                    objTele.BillMonth = txtBillMonth.Text;
                    objTele.BillAmt = txtClaimAmount.Text;
                    objTele.Description_HT_Exp = txtdiscript.Text;
                    objTele.FilingStatus = "S";
                    objTele.Status = "NULL";
                    dsExp = objTele.InsertTeleClaim(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                    if (dsExp.Tables.Count > 0)
                    {
                        string entryCode = dsExp.Tables[0].Rows[0]["Claim_no"].ToString();
                        divfileUploadTel.Visible = false;
                        divfileDisplayTel.Visible = true;
                        UploadTele_Click(entryCode);
                        //DisplayTeleDocuments(entryCode);

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

        protected void UploadTele_Click(string entryCode)
        {
            if (FileUploadTel.PostedFile.FileName == "")
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
        private void DisplayTeleDocuments(string entryCode, string EmployeeID)
        {
            try
            {
                CreateTeleDocumentsStructure();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Telephone\\TeleReimb\\" + EmployeeID + "\\" + entryCode + "\\";

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
        private void DisplayHandsetDocuments(string entryCode, string EmployeeID)
        {
            try
            {
                CreateHandsetDocumentsStructure();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Telephone\\Handset\\" + EmployeeID + "\\" + entryCode + "\\";

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
        }

        protected void lnkTelClmNoClmNo_Click(object sender, EventArgs e)
        {
            LinkButton lnkDOMClmNo = (LinkButton)sender;

            string entryAid = ((LinkButton)lnkDOMClmNo.NamingContainer.FindControl("lnkTelClmNoClmNo")).Text;
            Session["EntryAid"] = entryAid;

            Label EmployeeID = (Label)lnkDOMClmNo.NamingContainer.FindControl("lblEmpCode");
            objTele.AppNo = entryAid;
            dsExp = objTele.getTeleClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
            if (dsExp.Tables.Count > 0)
            {
                SectionList.Visible = false;
                divFrom.Visible = true;
                drpActionType.SelectedValue = "";
                lblRmk.Text = "";
                btnApprove.Visible = true;
                btnClose.Visible = true;

                string categoryType = dsExp.Tables[0].Rows[0]["Category_AId"].ToString();
                string desgAid = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();
                Session["Type"] = dsExp.Tables[0].Rows[0]["Type_AID"].ToString();
                ///////
                // drpType.Enabled = false;

                if (Session["Type"].ToString() == "TL000001")
                {
                    // getFinYear();
                    radioPrimary1.Checked = true;
                    radioPrimary2.Visible = false;
                    divHandset.Visible = true;
                    divTelephone.Visible = false;
                    divUpload.Visible = true;
                    divUplaodTel.Visible = false;
                    divfileUpload.Visible = false;
                    divfileDisplay.Visible = true;
                    divHandsetReimbFill.Visible = true;
                    divTeleReimbFill.Visible = false;
                    drpActionType.SelectedValue = "";
                    txtRmk.Text = "";

                    //drpFinYearHand.SelectedValue = dsExp.Tables[0].Rows[0]["FinYear"].ToString();
                    txtBillNoHand.Text = dsExp.Tables[0].Rows[0]["Bill_No"].ToString();
                    txtBillDateHand.Text = dsExp.Tables[0].Rows[0]["Bill_Date"].ToString();
                    txtClaimAmtHand.Text = dsExp.Tables[0].Rows[0]["Claim_Amount"].ToString();
                    txtApprovedAmt.Text = dsExp.Tables[0].Rows[0]["ClaimApproved_Amount"].ToString();
                    txtdiscript.Text = dsExp.Tables[0].Rows[0]["Description_HT_Exp"].ToString();
                    DataSet dsExphand = new DataSet();
                    dsExphand = objTele.GetHandSetReimb(Convert.ToString(Session["sCompID"]), categoryType, desgAid);
                    if (dsExp.Tables.Count > 0)
                    {
                        grHandset.DataSource = dsExphand.Tables[0];
                        grHandset.DataBind();
                        DisplayHandsetDocuments(entryAid, EmployeeID.Text);

                    }
                    else
                    {
                        grHandset.DataSource = null;
                        grHandset.DataBind();
                    }
                }
                else if (Session["Type"].ToString() == "TL000002")
                {
                    //getFinYear();
                    radioPrimary2.Checked = true;
                    radioPrimary1.Visible = false;
                    divHandset.Visible = false;
                    divTelephone.Visible = true;
                    //divAmt.Visible = true;
                    divUpload.Visible = false;
                    divUplaodTel.Visible = true;
                    divfileUploadTel.Visible = false;
                    divfileDisplayTel.Visible = true;
                    divHandsetReimbFill.Visible = false;
                    divTeleReimbFill.Visible = true;
                    drpActionType.SelectedValue = "";
                    txtRmk.Text = "";

                    //  drpFinYear.SelectedValue = dsExp.Tables[0].Rows[0]["FinYear"].ToString();
                    txtPhoneNo.Text = dsExp.Tables[0].Rows[0]["Mobile_No"].ToString();
                    txtBillNo.Text = dsExp.Tables[0].Rows[0]["Bill_No"].ToString();
                    txtBillDate.Text = dsExp.Tables[0].Rows[0]["Bill_Date"].ToString();
                    txtBillMonth.Text = dsExp.Tables[0].Rows[0]["Bill_Month"].ToString();
                    txtClaimAmount.Text = dsExp.Tables[0].Rows[0]["Claim_Amount"].ToString();
                    txtdiscript.Text = dsExp.Tables[0].Rows[0]["Description_HT_Exp"].ToString();
                    txtApprovedAmt.Text = dsExp.Tables[0].Rows[0]["ClaimApproved_Amount"].ToString();



                    DataSet dsExpTel = new DataSet();
                    dsExpTel = objTele.GetTeleReimb(Convert.ToString(Session["sCompID"]), categoryType, desgAid);
                    if (dsExp.Tables.Count > 0)
                    {
                        //txtAnulAmt.Text = dsExpTel.Tables[0].Rows[0]["Fix_Amount"].ToString();
                        //txtBal.Text = dsExpTel.Tables[0].Rows[0]["Fix_Amount"].ToString();

                        grTelephone.DataSource = dsExpTel.Tables[0];
                        grTelephone.DataBind();
                        DisplayTeleDocuments(entryAid, EmployeeID.Text);

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


            }
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpActionType.SelectedValue == "")
                {
                    getTelephoneData();
                    divAlert.Visible = true;
                    lblMessage.Text = "Please Select Action Type.";

                }
                else
                {
                    string drp = drpActionType.SelectedValue;
                    string Rmk = txtRmk.Text;
                    ActionGet(drp, Rmk);

                    divAlert.Visible = true;
                    string script = $@"<script type='text/javascript'>alert('Claim Submitted Successfully.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    lblMessage.Visible = true;
                    lblMessage.Text = "Claim Submitted Successfully.";
                    drpActionType.Enabled = false;
                    txtRmk.Enabled = false;
                    btnApprove.Visible = false;

                    // getTelephoneData();
                    divFrom.Visible = false;
                    SectionList.Visible = true;
                    getTelephoneData();



                }

            }
            catch (Exception ex)
            {
                divAlert.Visible = true;

                lblMessage.Text = ex.Message;
            }
        }
        private void ActionGet(string drp, string Rmk)
        {

            string radiochkent = "";
            string currentstatus = "";
            string status = "";
            double limit = 0;
            double limitEx = 0;
            string category_Aid = "";
            string group_Type_Aid = "";
            string group_Type = "";

            dsExp = objTele.GetChkStatus(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), (string)(Session["EntryAid"]));

            if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "9" || dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0" || dsExp.Tables[0].Rows[0]["Status"].ToString() == "2")
            {
                currentstatus = "9";
                Session["ClmType"] = "SEQUEL";
            }
            else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "3")
            {
                currentstatus = "3";
                Session["ClmType"] = "HOD";
            }
            else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "4")
            {
                currentstatus = "4";
                Session["ClmType"] = "HR";
            }
            else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "5")
            {
                currentstatus = "5";
                Session["ClmType"] = "CEO";
            }
            else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "6")
            {
                currentstatus = "6";
                Session["ClmType"] = "CFO";
            }
            if (Session["ClmType"].ToString() == "SEQUEL")
            {
                if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "9" || dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0")
                {
                    if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0")
                    {
                        objTele.Status = "Reject";
                        status = "Reject";
                    }
                    else
                    {
                        objTele.Status = "New";
                        status = "New";
                    }

                }
                else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "2")
                {
                    objTele.Status = "Revert";
                    status = "Revert";
                }

                ///
                //////////////////////////////////////////////////////////////////////////
                ///

                int cnt = 0;
                if (status == "New")
                {
                    if (drp == "Approve")
                    {
                        objTele.Action = drp;
                        objTele.CheckerRemark = Rmk;
                        objTele.TotalAmmount = txtApprovedAmt.Text;
                        objTele.Status = "3";


                    }
                    else if (drp == "Reject")
                    {
                        objTele.Action = drp;
                        objTele.CheckerRemark = Rmk;
                        objTele.TotalAmmount = txtApprovedAmt.Text;
                        objTele.Status = "0";

                    }
                }
                else if (status == "Reject")
                {
                    if (drp == "Approve")
                    {
                        objTele.Action = drp;
                        objTele.CheckerRemark = Rmk;
                        objTele.Status = "3";


                    }
                    else if (drp == "Reject")
                    {
                        objTele.Action = drp;
                        objTele.CheckerRemark = Rmk;
                        objTele.Status = "0";
                    }
                }
                else if (status == "Revert")
                {
                    if (drp == "Approve")
                    {

                        objTele.Action = drp;
                        objTele.CheckerRemark = Rmk;
                        objTele.Status = "6";
                    }
                    else if (drp == "Reject")
                    {
                        objTele.Action = drp;
                        objTele.CheckerRemark = Rmk;
                        objTele.Status = "0";
                    }
                }
            }
            if (Session["ClmType"].ToString() == "HOD")
            {
                if (drp == "Approve")
                {

                    objTele.Action = drp;
                    objTele.CheckerRemark = Rmk;

                    limitEx = Convert.ToDouble(dsExp.Tables[0].Rows[0]["Claim_Amount"].ToString());
                    category_Aid = dsExp.Tables[0].Rows[0]["CATEGORY_AID"].ToString();
                    group_Type = dsExp.Tables[0].Rows[0]["Group_Type"].ToString();
                    group_Type_Aid = dsExp.Tables[0].Rows[0]["GROUP_TYPE_AID"].ToString();
                    // limit = dsExp.Tables[0].Rows[0][""].ToString();
                    if (group_Type == "HANDSET")
                    {
                        if (category_Aid == "CT000001")
                        {
                            if (limitEx > Convert.ToDouble("50000"))
                            {
                                objTele.Status = "5";
                                radiochkent = "T";
                            }
                            else
                            {
                                objTele.Status = "4";
                                radiochkent = "F";
                            }
                        }
                        else if (category_Aid == "CT000002")
                        {
                            if (limitEx > Convert.ToDouble("15000"))
                            {
                                objTele.Status = "5";
                                radiochkent = "T";
                            }
                            else
                            {
                                objTele.Status = "4";
                                radiochkent = "F";
                            }
                        }
                    }
                    else if (group_Type == "TELE")
                    {
                        if (group_Type_Aid == "T0000003")
                        {
                            if (limitEx > Convert.ToDouble("3000"))
                            {
                                objTele.Status = "5";
                                radiochkent = "T";
                            }
                            else
                            {
                                objTele.Status = "6";
                                radiochkent = "F";
                            }
                        }
                        else if (group_Type_Aid == "T0000004")
                        {
                            if (limitEx > Convert.ToDouble("1500"))
                            {
                                objTele.Status = "5";
                                radiochkent = "T";
                            }
                            else
                            {
                                objTele.Status = "6";
                                radiochkent = "F";
                            }
                        }
                        else
                        {
                            objTele.Status = "6";
                        }
                    }




                }
                else if (drp == "Reject")
                {

                    objTele.Action = drp;
                    objTele.CheckerRemark = Rmk;
                    objTele.Status = "-0";

                }
            }

            if (Session["ClmType"].ToString() == "HR")
            {
                if (drp == "Approve")
                {

                    objTele.Action = drp;
                    objTele.CheckerRemark = Rmk;
                    objTele.Status = "6";


                }
                else if (drp == "Reject")
                {

                    objTele.Action = drp;
                    objTele.CheckerRemark = Rmk;
                    objTele.Status = "-0";

                }
            }

            if (Session["ClmType"].ToString() == "CEO")
            {
                if (drp == "Approve")
                {

                    objTele.Action = drp;
                    objTele.CheckerRemark = Rmk;
                    objTele.Status = "6";


                }
                else if (drp == "Reject")
                {

                    objTele.Action = drp;
                    objTele.CheckerRemark = Rmk;
                    objTele.Status = "-0";

                }
            }
            if (Session["ClmType"].ToString() == "CFO")
            {

                if (drp == "Approve")
                {

                    objTele.Action = drp;
                    objTele.CheckerRemark = Rmk;
                    objTele.Status = "1";


                }
                else if (drp == "Reject")
                {

                    objTele.Action = drp;
                    objTele.CheckerRemark = Rmk;
                    objTele.Status = "-0";

                }
                else if (drp == "Revert")
                {

                    objTele.Action = drp;
                    objTele.CheckerRemark = Rmk;
                    objTele.Status = "2";

                }
            }

            objTele.CategoryType = dsExp.Tables[0].Rows[0]["Category_AId"].ToString();
            objTele.Desg_Aid = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();

            dsExp = objTele.InsertStatus(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), (string)(Session["EntryAid"]), (string)Session["ClmType"], radiochkent);

            if (Session["ClmType"].ToString() == "CFO")
            {
                obj.SendTelephoneEmail((string)(Session["EntryAid"]), "FINANCE", drp, "Telephone", txtBillDate.Text, Session["sCompID"].ToString(), Session["sEmpID"].ToString());
            }
            else
            {
                obj.SendTelephoneEmail((string)(Session["EntryAid"]), Session["ClmType"].ToString(), drp, "Telephone", txtBillDate.Text, Session["sCompID"].ToString(), Session["sEmpID"].ToString());
            }
            

            //if (Session["ClmType"].ToString() == "CFO")Boolean
            //{

            //}



            //else if (drpActionType.SelectedValue == "Revert")
            //{

            //    objTele.Action = drpActionType.SelectedValue;
            //    objTele.CheckerRemark = txtRmk.Text;
            //    objTele.Status = "-1";

            //}


        }


        protected void gvTelClaim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblType = (Label)e.Row.FindControl("lblType");
                Label lblBillMonth = (Label)e.Row.FindControl("lblBillMonth");
                Label lblMobNo = (Label)e.Row.FindControl("lblMobNo");
                Label txtClmTypes = (Label)e.Row.FindControl("txtClmTypes");
                Label txtClmType = (Label)e.Row.FindControl("txtClmType");
                Label lblEmpAId = (Label)e.Row.FindControl("lblEmpAId");
                Label TravelAmt = (Label)e.Row.FindControl("txtClmAmt");
                Label txtAppAmtr = (Label)e.Row.FindControl("txtAppAmtr");
                LinkButton lnkTelClmNoClmNo = (LinkButton)e.Row.FindControl("lnkTelClmNoClmNo");
                DataSet ds = new DataSet();
                ds = objTele.GetTelLimit(lblEmpAId.Text, lnkTelClmNoClmNo.Text, Convert.ToString(Session["sCompID"]));

                if (txtClmType.Text == "SEQUEL")
                {
                    if (ds.Tables[0].Rows[0]["limit_amount"].ToString() == "0")
                    {
                        lblMessage.Text = "Limit Not Found.";
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                    }
                    else
                    {
                        if (decimal.Parse(TravelAmt.Text.Trim()) > decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                        {
                            TableCell cell = e.Row.Cells[3];
                          //  cell.BackColor = System.Drawing.Color.DeepSkyBlue;


                        }
                        else if (decimal.Parse(TravelAmt.Text.Trim()) < decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                        {

                        }
                    }
                }
                else
                {
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
                                TableCell cell = e.Row.Cells[3];
                                //cell.BackColor = System.Drawing.Color.Yellow;
                            }
                            else if (decimal.Parse(txtAppAmtr.Text.Trim()) < decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                            {
                                TableCell cell = e.Row.Cells[3];
                                //cell.BackColor = System.Drawing.Color.LightGreen;
                            }
                        }
                    }
                    else
                    {

                    }
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

        protected void lnkBtnSubmit_Click(object sender, EventArgs e)
        {
            //getTelephoneData();
            //divFrom.Visible = false;
            //SectionList.Visible = true;
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chckheader = (CheckBox)gvTelClaim.HeaderRow.FindControl("chkSelectAll");
            foreach (GridViewRow row in gvTelClaim.Rows)
            {
                CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                if (chckheader.Checked == true)
                {
                    chckrw.Checked = true;

                }
                else
                {
                    chckrw.Checked = false;
                }

            }
        }
        protected void btnSubmitAll_Click(object sender, EventArgs e)
        {
            string radiochkent = "";


            dsExp = objTele.GetChkStatus(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), (string)(Session["EntryAid"]));

            if (drActionAll.SelectedValue == "")
            {
                //getDomesticData();
                divAlert.Visible = true;
                lblMessage.Text = "Please Select Action Type.";

            }
            else
            {

                //if (radiaoEntertainment1.Checked == true)
                //{
                //    radiochkent = "T";
                //}
                //else
                //{
                //    radiochkent = "F";
                //}                        

                objTele.COMP_AID = Convert.ToString(Session["sCompID"]);
                objTele.EmpCode = (string)(Session["sEmpID"]);
                objTele.Type = radiochkent;
                string drp = drActionAll.SelectedValue;
                string Rmk = txtAllRmk.Text;
                objTele.UpdateFinGlSubmit(MakeChkXml(gvTelClaim), drActionAll.SelectedValue, txtAllRmk.Text);
                MakeChkForEmailXml(gvTelClaim);
                //ActionGetAll(drp, Rmk);
                getTelephoneData();
                divAlert.Visible = true;
                lblMessage.Text = "Approved Successfully.";


            }
        }
        private string MakeChkForEmailXml(GridView gvList)
        {
            StringBuilder sbChkGlCode = new StringBuilder();
            StringBuilder sbUnChkGlCode = new StringBuilder();
            string xmlChkGlCode = string.Empty;
            string xmlUnChkGlCode = string.Empty;



            foreach (GridViewRow gvr in gvList.Rows)
            {
                // string radiochkent = "F";
                string Action = string.Empty;
                string fromdate = string.Empty;
                string Approver = string.Empty;
                string ClaimNo = string.Empty;
                string TeleType = string.Empty;
                string rd = string.Empty;
                if (radiaoEntertainment1.Checked == true)
                {
                    rd = "T";
                }
                else
                {
                    rd = "F";
                }

                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {

                    ClaimNo = ((LinkButton)gvr.FindControl("lnkTelClmNoClmNo")).Text.Trim();
                    fromdate = ((Label)gvr.FindControl("txtFrDest")).Text.Trim();
                    Approver = ((Label)gvr.FindControl("txtClmType")).Text.Trim();
                    //TeleType = ((Label)gvr.FindControl("txtClmType")).Text.Trim();
                    TeleType = ((Label)gvr.FindControl("txtClmTypes")).Text.Trim();
                    Action = drActionAll.SelectedValue;

                    obj.SendTelephoneEmail(ClaimNo, Approver, Action, TeleType, fromdate, Session["sCompID"].ToString(), Session["sEmpID"].ToString());
                }

            }


            return xmlChkGlCode;
        }
        //private void SENDUDATEMAIL(string ClaimNO, string Approver, string Action, string TeleType, string fromdate)
        //{
        //    emailSend = new NewPortal2023.ESS.Email();
        //    DataSet dsmkkMail = new DataSet();
        //    DateTime date = DateTime.Now;

        //    if (Action == "Approve")
        //    {
        //        dsmkkMail = emailSend.GetEmpLocalEXpRecDe((string)Session["sCompID"], (string)Session["sEmpID"], ClaimNO, Approver);


        //        if (dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() != "")
        //        {
        //            string clientbodys = "Dear " + dsmkkMail.Tables[2].Rows[0]["APPROVARNAME"].ToString() + ",<br><br> Telephone Claim is received for approval." + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + " claim is Approved by the " + Approver
        //            + ".Kindly take the action for the same through logging-in into ESS portal.<br>"
        //            + " <br>Claim Type:- " + TeleType + " Expense"
        //                 + "<br>Claim No :- " + ClaimNO
        //                 + "<br>Bill Date :- " + fromdate
        //                 + "<br><br>ThankYou.<br><br>";
        //            string emails = dsmkkMail.Tables[2].Rows[0]["APPROVARMAIL"].ToString();
        //            string emailsCC = dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() + ',' + dsmkkMail.Tables[1].Rows[0]["EMP_EMAIL"].ToString();
        //            //string emails = "techsupport@sequelgroup.co.in";
        //            //string emailsCC = "techsupport@sequelgroup.co.in";
        //            string subjects = "Do Not Reply: Telephone Expense";
        //            emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);

        //        }
        //    }
        //    else if (Action == "Reject")
        //    {
        //        dsmkkMail = emailSend.GetEmpTeleRejectMAil((string)Session["sCompID"], (string)Session["sEmpID"], ClaimNO, Approver);

        //        string clientbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>Telephone Claim is rejected by the " + dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString()
        //           + ".Kindly check the claim for the same through logging-in into ESS portal.<br>"
        //          + " <br>Claim Type:- " + TeleType + " Expense"
        //                + "<br>Claim No :- " + ClaimNO
        //                + "<br>Bill Date :- " + fromdate
        //                + "<br><br>ThankYou.<br><br>";
        //        string emails = dsmkkMail.Tables[1].Rows[0]["EMP_EMAIL"].ToString();
        //        string emailsCC = dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString();
        //        //string emails = "techsupport@sequelgroup.co.in";
        //        //string emailsCC = "payrollservices@sequelgroup.co.in";
        //        string subjects = "Do Not Reply: Telephone Expense";
        //        emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);
        //    }
        //}

        private string MakeChkXml(GridView gvList)
        {
            StringBuilder sbChkGlCode = new StringBuilder();
            StringBuilder sbUnChkGlCode = new StringBuilder();
            string xmlChkGlCode = string.Empty;
            string xmlUnChkGlCode = string.Empty;

            sbChkGlCode.Append("<ROOT>");

            foreach (GridViewRow gvr in gvList.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    sbChkGlCode.Append("<CHKGLSUMBIT CODE='" + ((LinkButton)gvr.FindControl("lnkTelClmNoClmNo")).Text.Trim() + "'/>");

                }

            }
            sbChkGlCode.Append("</ROOT>");

            xmlChkGlCode = sbChkGlCode.ToString();

            return xmlChkGlCode;
        }



        //protected void gvTelClaim_PreRender(object sender, EventArgs e)
        //{
        //    GridView gv = (GridView)sender;

        //    if ((gv.ShowHeader == true && gv.Rows.Count > 0)
        //        || (gv.ShowHeaderWhenEmpty == true))
        //    {
        //        //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
        //        gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    }
        //    if (gv.ShowFooter == true && gv.Rows.Count > 0)
        //    {
        //        //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
        //        gv.FooterRow.TableSection = TableRowSection.TableFooter;
        //    }
        //}
    }
}