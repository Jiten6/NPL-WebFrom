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
    public partial class ApprovalLocalPage : System.Web.UI.Page
    {
        NewPortal2023.Helper.ExpenseEmail obj = new NewPortal2023.Helper.ExpenseEmail();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        NewPortal2023.ESS.LocalExpenses objLoc = new NewPortal2023.ESS.LocalExpenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
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

                    getLocalData();
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
                dsExp = objLoc.GetApproverLocalList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables[0].Rows.Count > 0)
                {
                    Session["ClmType"] = dsExp.Tables[0].Rows[0]["ClmType"].ToString();
                    if (Session["ClmType"].ToString() == "CFO")
                    {
                        drpActionType.Items.Insert(3, new ListItem("Revert", "Revert"));
                    }
                    if (Session["ClmType"].ToString() == "CFO")
                    {
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

                    actionAll.Visible = true;
                    this.gvLocalClaimList.DataSource = dsExp.Tables[0];
                    this.gvLocalClaimList.DataBind();


                }
                else
                {
                    actionAll.Visible = false;
                    this.gvLocalClaimList.DataSource = null;
                    this.gvLocalClaimList.DataBind();

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
            SectionList.Visible = false;
            divFrom.Visible = true;
            divTravel.Visible = true;
            divUpload.Visible = true;
            divfileUpload.Visible = true;
            txtDate.Text = "";
            txtCashVocher.Text = "";

            txtTravelDes.Text = "";
            txtMeal.Text = "";
            txtOtherExpenses.Text = "";
            txtNameAss.Text = "";
            getCategoryType();
            getLocalReimb();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objLoc.Expenses_Date = txtDate.Text;
                objLoc.Cash_Voucher = txtCashVocher.Text;
                objLoc.Desg_Aid = Session["DESG_AID"].ToString();
                objLoc.CategoryType = Session["CATEGORY_TYPE"].ToString();
                objLoc.Travel_Description = txtTravelDes.Text;
                objLoc.Meal = txtMeal.Text;
                objLoc.TotalAmmount = txtApprovedAmt.Text;
                objLoc.Other_Expenses = txtOtherExpenses.Text;
                objLoc.Name_Bussi_Ass = txtNameAss.Text;
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
                objLoc.Status = "NULL";
                dsExp = objLoc.InsertLocClaim(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables.Count > 0)
                {
                    string entryCode = dsExp.Tables[0].Rows[0]["Claim_no"].ToString();
                    divfileUpload.Visible = false;
                    divfileDisplay.Visible = true;
                    Upload_Click(entryCode);
                    //DisplayDocuments(entryCode, lblEmpAId);

                }

            }
            catch (Exception ex)
            {

            }

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

        private void DisplayDocuments(string entryCode, string lblEmpAId)
        {
            try
            {
                CreateDocumentsStructure();

                string prefix = "";

                //dsExp = objLoc.getaPPROVELocalClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));  PARESH

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Local\\" + lblEmpAId + "\\" + entryCode + "\\";

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
            getLocalData();
        }

        protected void lnkLOCClmNoClmNo_Click(object sender, EventArgs e)
        {
            LinkButton lnkDOMClmNo = (LinkButton)sender;

            string entryAid = ((LinkButton)lnkDOMClmNo.NamingContainer.FindControl("lnkLOCClmNoClmNo")).Text;
            string lblEmpAId = ((Label)lnkDOMClmNo.NamingContainer.FindControl("lblEmpCode")).Text;
            Session["EntryAid"] = entryAid;

            objLoc.AppNo = entryAid;
            dsExp = objLoc.getaPPROVELocalClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));

            if (dsExp.Tables[0].Rows[0]["status"].ToString() == "9")
            {
                radiaoEntertainment1.Visible = true;
            }
            else
            {

                radiaoEntertainment1.Visible = true;
                radiaoEntertainment1.Enabled = false;
            }
            if (dsExp.Tables.Count > 0)
            {
                SectionList.Visible = false;
                divFrom.Visible = true;
                divUpload.Visible = true;
                divfileUpload.Visible = false;
                divfileDisplay.Visible = true;
                divTravel.Visible = true;
                btnApprove.Visible = true;
                drpActionType.SelectedValue = "";
                drpActionType.Enabled = true;
                txtRmk.Text = "";
                txtRmk.Enabled = true;

                string categoryType = dsExp.Tables[0].Rows[0]["Category_AId"].ToString();
                string desgAid = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();
                Session["CATEGORY_TYPE"] = categoryType;
                Session["DESG_AID"] = desgAid;
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
                    txtchk1.Text = string.Empty;

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
                    txtchk2.Text = string.Empty;

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
                    txtchk3.Text = string.Empty;

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
                    txtchk4.Text = string.Empty;

                }
                txtDate.Text = dsExp.Tables[0].Rows[0]["Expenses_Date"].ToString();
                txtCashVocher.Text = dsExp.Tables[0].Rows[0]["Cash_Voucher"].ToString();
                txtApprovedAmt.Text = dsExp.Tables[0].Rows[0]["ClaimApproved_Amount"].ToString();
                txtTravelDes.Text = dsExp.Tables[0].Rows[0]["Travel_Description"].ToString();
                txtMeal.Text = dsExp.Tables[0].Rows[0]["Meal"].ToString();
                txtOtherExpenses.Text = dsExp.Tables[0].Rows[0]["Other_Expenses"].ToString();
                txtadv.Text = dsExp.Tables[0].Rows[0]["ADVANCE_AMT"].ToString();
                txtNameAss.Text = dsExp.Tables[0].Rows[0]["Name_Bussi_Ass"].ToString();
                dsExp = objLoc.GetLocalReimb(Convert.ToString(Session["sCompID"]), Convert.ToString(Session["CATEGORY_TYPE"]));
                if (dsExp.Tables.Count > 0)
                {
                    grLocalReimb.DataSource = dsExp.Tables[0];
                    grLocalReimb.DataBind();
                    DisplayDocuments(entryAid, lblEmpAId);

                }
                else
                {
                    grLocalReimb.DataSource = null;
                    grLocalReimb.DataBind();
                }

            }

            Calculatetotalexp();
        }

        protected void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked == true)
            {
                txtchk1.Visible = true;
            }
        }

        protected void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk2.Checked == true)
            {
                txtchk2.Visible = true;
            }
        }

        protected void chk3_CheckedChanged(object sender, EventArgs e)
        {
            if (chk3.Checked == true)
            {
                txtchk3.Visible = true;
            }
        }

        protected void chk4_CheckedChanged(object sender, EventArgs e)
        {
            if (chk4.Checked == true)
            {
                txtchk4.Visible = true;
            }
        }

        protected void lnkBtnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (drpActionType.SelectedValue == "")
            {
                getLocalData();
                divAlert.Visible = true;
                lblMessage.Text = "Please Select Action Type.";

            }
            else
            {
                string drp = drpActionType.SelectedValue;
                string Rmk = txtRmk.Text;
                ActionGet(drp, Rmk);
                //getLocalData();
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Action Send Successfully.";
                string script = $@"<script type='text/javascript'>alert('Action Send Successfully.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                drpActionType.Enabled = false;
                txtRmk.Enabled = false;
                btnApprove.Visible = false;
                getLocalData();
                divFrom.Visible = false;
                SectionList.Visible = true;

                //drpActionType.Enabled = false;
                //txtRmk.Enabled = false;
                //btnApprove.Visible = false;
                //divFrom.Visible = false;
                //divTravel.Visible = false;
                //SectionList.Visible = true;
                //getLocalData();
                //divAlert.Visible = true;
                //lblMessage.Visible = true;
                //lblMessage.Text = "Action Send Successfully.";

            }
        }

        private void ActionGet(string drp, string Rmk)
        {

            //string radiochkent = "";
            string currentstatus = "";
            string status = "";

            dsExp = objLoc.GetChkStatus(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), (string)(Session["EntryAid"]));
            string radiochkent = dsExp.Tables[0].Rows[0]["EntertainmentChked"].ToString();

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
                Session["ClmType"] = "CEO";
            }
            else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "5")
            {
                currentstatus = "5";
                Session["ClmType"] = "CFO";
            }
            if (Session["ClmType"].ToString() == "SEQUEL")
            {
                //if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "9")
                //{
                //    if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "0")
                //    {
                //        objLoc.Status = "Reject";
                //        status = "Reject";
                //    }
                //    else
                //    {
                //        objLoc.Status = "New";
                //        status = "New";
                //    }

                //}
                //else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0")
                //{
                //    objLoc.Status = "Revert";
                //    status = "Revert";
                //}

                if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "9")
                {
                    status = "New";
                }
                else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0")
                {
                    status = "Reject";
                }
                else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "2")
                {
                    status = "Revert";
                }


                int cnt = 0;
                if (status == "New")
                {
                    if (drp == "Approve")
                    {
                        objLoc.Action = drp;
                        objLoc.CheckerRemark = Rmk;
                        objLoc.TotalAmmount = txtApprovedAmt.Text;
                        objLoc.Status = "3";
                        radiaoEntertainment1.Enabled = true;
                        if (radiaoEntertainment1.Checked == true)
                        {

                            radiochkent = "T";
                        }
                        else
                        {
                            radiochkent = "F";
                        }

                    }
                    else if (drp == "Reject")
                    {
                        objLoc.Action = drp;
                        objLoc.CheckerRemark = Rmk;
                        objLoc.TotalAmmount = txtApprovedAmt.Text;
                        objLoc.Status = "0";

                    }
                }
                else if (status == "Reject")
                {
                    if (drp == "Approve")
                    {
                        objLoc.Action = drp;
                        objLoc.CheckerRemark = Rmk;
                        objLoc.Status = "3";


                    }
                    else if (drp == "Reject")
                    {
                        objLoc.Action = drp;
                        objLoc.CheckerRemark = Rmk;
                        objLoc.Status = "0";
                    }
                }
                else if (status == "Revert")
                {
                    if (drp == "Approve")
                    {

                        objLoc.Action = drp;
                        objLoc.CheckerRemark = Rmk;
                        objLoc.TotalAmmount = txtApprovedAmt.Text;
                        if (radiaoEntertainment1.Checked == true)
                        {

                            radiochkent = "T";
                        }
                        else
                        {
                            radiochkent = "F";
                        }
                        objLoc.Status = "5";
                        objLoc.revert = "-1";
                    }
                    else if (drp == "Reject")
                    {
                        objLoc.Action = drp;
                        objLoc.CheckerRemark = Rmk;
                        objLoc.TotalAmmount = txtApprovedAmt.Text;
                        objLoc.Status = "0";
                        objLoc.revert = "";
                    }
                }
            }
            if (Session["ClmType"].ToString() == "HOD")
            {
                if (drp == "Approve")
                {
                    //if (radiochkent == "T")
                    //{
                    //    objLoc.Action = drp;
                    //    objLoc.CheckerRemark = Rmk;
                    //    objLoc.Status = "4";
                    //}
                    //else if (radiochkent == "F")
                    //{
                        objLoc.Action = drp;
                        objLoc.CheckerRemark = Rmk;
                        objLoc.Status = "5";
                   // }

                }
                else if (drp == "Reject")
                {

                    objLoc.Action = drp;
                    objLoc.CheckerRemark = Rmk;
                    objLoc.Status = "-0";

                }
            }
            if (Session["ClmType"].ToString() == "CEO")
            {
                if (drp == "Approve")
                {

                    objLoc.Action = drp;
                    objLoc.CheckerRemark = Rmk;
                    objLoc.Status = "5";


                }
                else if (drp == "Reject")
                {

                    objLoc.Action = drp;
                    objLoc.CheckerRemark = Rmk;
                    objLoc.Status = "-0";

                }
            }
            if (Session["ClmType"].ToString() == "CFO")
            {
                if (drp == "Approve")
                {

                    objLoc.Action = drp;
                    objLoc.CheckerRemark = Rmk;
                    objLoc.Status = "1";
                    objLoc.revert = "";


                }
                else if (drp == "Reject")
                {

                    objLoc.Action = drp;
                    objLoc.CheckerRemark = Rmk;
                    objLoc.Status = "-0";
                    objLoc.revert = "";

                }
                else if (drpActionType.SelectedValue == "Revert")
                {

                    objLoc.Action = drpActionType.SelectedValue;
                    objLoc.CheckerRemark = txtRmk.Text;
                    objLoc.Status = "2";
                    objLoc.revert = "-1";
                }
            }

            if (drp != "Revert")
            {
                objLoc.revert = "";
            }

            objLoc.CategoryType = Session["CATEGORY_TYPE"].ToString();
            objLoc.Desg_Aid = Session["DESG_AID"].ToString();
            dsExp = objLoc.InsertStatus(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), (string)(Session["EntryAid"]), (string)Session["ClmType"], radiochkent);

            obj.SendLocalEmail((string)(Session["EntryAid"]), Session["ClmType"].ToString(), drp, radiochkent, txtDate.Text, Session["sCompID"].ToString(), Session["sEmpID"].ToString());


        }

        //private void SENDUDATEMAIL(string ClaimNO, string Approver, string Action, string radiochkent, string fromdate)
        //{
        //    emailSend = new NewPortal2023.ESS.Email();
        //    DataSet dsmkkMail = new DataSet();
        //    DateTime date = DateTime.Now;

        //    if (Action == "Approve")
        //    {
        //        dsmkkMail = emailSend.GetEmpLocalEXpRecDe((string)Session["sCompID"], (string)Session["sEmpID"], ClaimNO, Approver, radiochkent);


        //        if (dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() != "")
        //        {
        //            string clientbodys = "Dear " + dsmkkMail.Tables[2].Rows[0]["APPROVARNAME"].ToString() + ",<br><br> Local Travel Expense is received for approval." + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + " claim is Approved by the " + Approver
        //            + ".Kindly take the action for the same through logging-in into ESS portal.<br>"
        //           + " <br>Claim Type:- Local Travel Expense"
        //                 + "<br>Claim No :- " + ClaimNO
        //                 + "<br>Travel Date :- " + fromdate
        //                 + "<br><br>ThankYou.<br><br>";
        //            string emails = dsmkkMail.Tables[2].Rows[0]["APPROVARMAIL"].ToString();
        //            string emailsCC = dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() + ',' + dsmkkMail.Tables[1].Rows[0]["EMP_EMAIL"].ToString();
        //            //string emails = "techsupport@sequelgroup.co.in";
        //            //string emailsCC = "payrollservices@sequelgroup.co.in";
        //            string subjects = "Do Not Reply: Local Travel Expense";
                   
        //            emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);

        //        }
        //    }
        //    else if (Action == "Reject")
        //    {
        //        dsmkkMail = emailSend.GetEmpLocRejectMAil((string)Session["sCompID"], (string)Session["sEmpID"], ClaimNO, Approver, radiochkent);

        //        string clientbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>Local Travel Expense is rejected by the " + dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString()
        //           + ".Kindly check the claim for the same through logging-in into ESS portal.<br>"
        //          + " <br>Claim Type:- Local Travel Expense"
        //                + "<br>Claim No :- " + ClaimNO
        //                + "<br>Travel Date :- " + fromdate
        //                + "<br><br>ThankYou.<br><br>";
        //        string emails = dsmkkMail.Tables[1].Rows[0]["EMP_EMAIL"].ToString();
        //        string emailsCC = dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString();
        //        //string emails = "techsupport@sequelgroup.co.in";
        //        //string emailsCC = "payrollservices@sequelgroup.co.in";
        //        string subjects = "Do Not Reply: Local Travel Expense";
        //        emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);
        //    }
        //}

        protected void btnSubmitAll_Click(object sender, EventArgs e)
        {
            if (drActionAll.SelectedValue == "")
            {
                //getDomesticData();
                divAlert.Visible = true;
                lblMessage.Text = "Please Select Action Type.";

            }
            else
            {
                objLoc.COMP_AID = Convert.ToString(Session["sCompID"]);
                objLoc.EmpCode = (string)(Session["sEmpID"]);
                string drp = drActionAll.SelectedValue;
                string Rmk = txtAllRmk.Text;
                objLoc.UpdateFinGlSubmit(MakeChkXml(gvLocalClaimList), drActionAll.SelectedValue, txtAllRmk.Text);
                MakeChkForEmailXml(gvLocalClaimList);
                //ActionGetAll(drp, Rmk);
                getLocalData();
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Action Send Successfully.";
                string script = $@"<script type='text/javascript'>alert('Action Send Successfully.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);


            }
        }

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
                    sbChkGlCode.Append("<CHKGLSUMBIT CODE='" + ((LinkButton)gvr.FindControl("lnkLOCClmNoClmNo")).Text.Trim() + "'/>");

                }

            }
            sbChkGlCode.Append("</ROOT>");

            xmlChkGlCode = sbChkGlCode.ToString();

            return xmlChkGlCode;
        }

        private string MakeChkForEmailXml(GridView gvList)
        {
            StringBuilder sbChkGlCode = new StringBuilder();
            StringBuilder sbUnChkGlCode = new StringBuilder();
            string xmlChkGlCode = string.Empty;
            string xmlUnChkGlCode = string.Empty;



            foreach (GridViewRow gvr in gvList.Rows)
            {
                string radiochkent = "F";
                string Action = string.Empty;
                string fromdate = string.Empty;
                string Approver = string.Empty;
                string ClaimNo = string.Empty;

                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {

                    ClaimNo = ((LinkButton)gvr.FindControl("lnkLOCClmNoClmNo")).Text.Trim();
                    fromdate = ((Label)gvr.FindControl("txFrDate")).Text.Trim();
                    Approver = Session["ClmType"].ToString();
                    Action = drActionAll.SelectedValue;

                    obj.SendLocalEmail(ClaimNo, Approver, Action, radiochkent, fromdate, Session["sCompID"].ToString(), Session["sEmpID"].ToString());
                }

            }


            return xmlChkGlCode;
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chckheader = (CheckBox)gvLocalClaimList.HeaderRow.FindControl("chkSelectAll");
            foreach (GridViewRow row in gvLocalClaimList.Rows)
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

        protected void gvLocalClaimList_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;

            if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                || (gv.ShowHeaderWhenEmpty == true))
            {
                //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (gv.ShowFooter == true && gv.Rows.Count > 0)
            {
                //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                gv.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void gvLocalClaimList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    TextBox txtRmk = (TextBox)e.Row.FindControl("txtRmk");
                    //TextBox txtdescri = (TextBox)e.Row.FindControl("txtdescri");
                    DropDownList ddls = (DropDownList)e.Row.FindControl("drpAction");
                    Label lblEmpAId = (Label)e.Row.FindControl("lblEmpAId");
                    Label TravelAmt = (Label)e.Row.FindControl("txtClmAmt");
                    Label txtAppAmtr = (Label)e.Row.FindControl("txtAppAmtr");
                    Label txtClmType = (Label)e.Row.FindControl("txtClmType");
                    Label EntertainmentChked = (Label)e.Row.FindControl("EntertainmentChked");
                    LinkButton lnkLOCClmNoClmNo = (LinkButton)e.Row.FindControl("lnkLOCClmNoClmNo");
                    //DataSet ds = new DataSet();
                    // ds = objExp.GetDOMLimit(lblEmpAId.Text, lnkDOMClmNoClmNo.Text, Convert.ToString(Session["sCompID"]));

                    if (txtClmType.Text == "SEQUEL")
                    {

                    }
                    else
                    {
                        if (txtAppAmtr.Text != "")
                        {
                            if (EntertainmentChked.Text == "T")
                            {
                                TableCell cell = e.Row.Cells[3];
                               // cell.BackColor = System.Drawing.Color.Orange;
                            }

                            else if (EntertainmentChked.Text == "F")
                            {

                            }
                        }
                    }



                  //  txtRmk.BackColor = (txtRmk.Text.Trim() == "1" ? System.Drawing.Color.LightGray : txtRmk.BackColor);
                    if (txtRmk.Text == "Submitted")
                    {
                        txtRmk.Text = " Submitted.";
                    }
                    else if (txtRmk.Text == "Rejected")
                    {
                        txtRmk.Text = " Rejected.";
                    }
                    else if (txtRmk.Text == "Approved")
                    {
                        txtRmk.Text = " Approved.";
                    }
                    else
                    {
                        txtRmk.Text = " Pending " + txtRmk.Text + " .";
                    }
                }
            }

            catch (Exception ex)
            {

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
    }
}