using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Configuration;
using System.IO;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class TelephoneSupporting : System.Web.UI.Page
    {
        NewPortal2023.ESS.TelephoneExpenses objTele = new NewPortal2023.ESS.TelephoneExpenses();
        NewPortal2023.ESS.Expenses objeXP = new NewPortal2023.ESS.Expenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
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
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                getTelephoneData();
            }

        }

        private void getTelephoneData()
        {
            try
            {
                getCategoryType(flagType);

                dsExp = objeXP.GetSupportingTeleList(Convert.ToString(Session["sCompID"]));

                if (dsExp.Tables[0].Rows.Count > 0)
                {
                    this.gvTelClaim.DataSource = dsExp.Tables[0];
                    this.gvTelClaim.DataBind();
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
                        Session["CATEGORY_AID"] = dsExp.Tables[0].Rows[0]["CATEGORY_TYPE"].ToString();
                        /* Session["CATEGORY_AID"] = dsExp.Tables[0].Rows[0]["CATEGORY_AID"].ToString()*/
                        ;
                        Session["DESG_AID"] = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();

                    }
                }
                else if (flagType == "Telephone bill")
                {

                    objTele = new NewPortal2023.ESS.TelephoneExpenses();
                    dsExp = objTele.GetTeleCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), flagType);
                    if (dsExp.Tables.Count > 0)
                    {
                        Session["CATEGORY_AID"] = dsExp.Tables[0].Rows[0]["CATEGORY_TYPE"].ToString();
                        Session["DESG_AID"] = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();
                    }
                }
                else
                {
                    objTele = new NewPortal2023.ESS.TelephoneExpenses();
                    dsExp = objTele.GetTeleCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), flagType);
                    if (dsExp.Tables.Count > 0)
                    {
                        Session["CATEGORY_AID"] = dsExp.Tables[0].Rows[0]["CATEGORY_TYPE"].ToString();
                        /* Session["CATEGORY_AID"] = dsExp.Tables[0].Rows[0]["CATEGORY_AID"].ToString()*/
                        ;
                        Session["DESG_AID"] = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();

                    }
                }


            }
            catch (Exception ex)
            {
                lblMessage.Text = "Category Not Found.";
            }
        }

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
               // drpActionType.SelectedValue = "";
               // lblRmk.Text = "";
               // btnApprove.Visible = true;
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
                   /// drpActionType.SelectedValue = "";
                   // txtRmk.Text = "";

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
                   // drpActionType.SelectedValue = "";
                   // txtRmk.Text = "";

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
       
        protected void gvTelClaim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label txtAppAmtr = (Label)e.Row.FindControl("txtAppAmtr");
                    Label TravelAmt = (Label)e.Row.FindControl("txtClmAmt");
                    Label lblBillMonth = (Label)e.Row.FindControl("lblBillMonth");
                    Label txtFrDest = (Label)e.Row.FindControl("txtFrDest");
                    Label txFrDate = (Label)e.Row.FindControl("txFrDate");
                    Label lblMobNo = (Label)e.Row.FindControl("lblMobNo");
                    Label txtClmType = (Label)e.Row.FindControl("txtClmType");
                    Label lblEmpName = (Label)e.Row.FindControl("lblEmpName");
                    Label lblEmpCode = (Label)e.Row.FindControl("lblEmpCode");
                    LinkButton lnkTelClmNoClmNo = (LinkButton)e.Row.FindControl("lnkTelClmNoClmNo");
                }
            }
            catch (Exception ex)
            {

            }
        }

   


      


   


        








    }
}