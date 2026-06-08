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
    public partial class DomesticTravel : System.Web.UI.Page
    {
        NewPortal2023.ESS.LocalExpenses objLoc = new NewPortal2023.ESS.LocalExpenses();
        NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
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
                    getDomesticData();
                    getAdvanceData();

                    divTravel.Visible = false;
                    divEntertainment.Visible = false;
                    divUploadEnter.Visible = false;
                    divfileUploadEnter.Visible = false;
                    Class_Travel.Visible = false;
                    Section1.Visible = false;
                    DivAction.Visible = false;
                    Sectionadvlist.Visible = true;
                }
                else
                {

                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }

        private void getDomesticData()
        {
            try
            {

                dsExp = objExp.GetDomesticList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables[0].Rows.Count > 0)
                {
                    Session["Entry_aid"] = null;
                    this.gvDomClaim.DataSource = dsExp.Tables[0];
                    this.gvDomClaim.DataBind();
                    getCategoryType();
                }
                else
                {
                    this.gvDomClaim.DataSource = null;
                    this.gvDomClaim.DataBind();
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

                dsExp = objExp.GetempadvanceList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "Domestic");
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
                objExp = new NewPortal2023.ESS.Expenses();
                dsExp = objExp.GetCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables.Count > 0)
                {
                    Session["CATEGORY_TYPE"] = dsExp.Tables[0].Rows[0]["CATEGORY_TYPE"].ToString();
                    if (dsExp.Tables[1].Rows.Count > 0)
                    {
                        lblClassTravel.Visible = true;
                        for (int i = 0; i < dsExp.Tables[1].Rows.Count; i++)
                        {
                            if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Air (Business Class)")
                            {
                                chk1.Visible = true;
                                //txtchk1.Visible = true;
                                divChk1.Attributes["class"] = chk1.Visible ? "icheck-primary d-inline col-sm-3" : "icheck-primary d-inline col-sm-3 hidden";
                                divTxtChk1.Attributes["class"] = txtchk1.Visible ? "col-sm-3" : "col-sm-3 hidden";
                            }
                            else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Air (Economy Class)")
                            {
                                chk2.Visible = true;
                                //txtchk2.Visible = true;
                            }
                            else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Rail (AC 1st Class)")
                            {
                                chk3.Visible = true;
                                //txtchk3.Visible = true;
                            }
                            else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Rail (AC 2nd Class)")
                            {
                                chk4.Visible = true;
                                //txtchk4.Visible = true;
                            }
                            else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Rail (AC 3rd Class)")
                            {
                                chk5.Visible = true;
                                //txtchk5.Visible = true;
                            }
                            else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Rail (AC Chair Car)")
                            {
                                chk6.Visible = true;
                                //txtchk6.Visible = true;
                            }
                            else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Bus")
                            {
                                chk7.Visible = true;
                                //txtchk7.Visible = true;
                            }
                            else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Other")
                            {
                                chk8.Visible = true;
                                //txtchk8.Visible = true;
                            }

                        }
                    }
                }


            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Category Not Found.";
            }
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {

            DivAction.Visible = true;

            if (drpType.SelectedValue == "Travel")
            {
                ClearAll();

                divTravel.Visible = true;
                divEntertainment.Visible = false;
                divUploadEnter.Visible = false;
                divfileUploadEnter.Visible = false;
                Class_Travel.Visible = true;
                radioPrimary1.Checked = false;
                radioPrimary2.Checked = false;
                travelClass.Visible = true;
                divTravel.Visible = true;
                divfileUpload.Visible = true;
                divfileDisplay.Visible = false;

                eligibility.Visible = true;
                claimAmt.Visible = true;
                TotalExpns.Visible = true;
                Sectionadvlist.Visible = false;
            }
            else if (drpType.SelectedValue == "Entertainment")
            {
                ClearAll();

                divTravel.Visible = false;
                divEntertainment.Visible = true;
                divUploadEnter.Visible = true;
                divfileUploadEnter.Visible = true;
                Class_Travel.Visible = false;
                divfileUploadEnter.Visible = true;
                divfileDisplayEnter.Visible = false;

                eligibility.Visible = false;
                claimAmt.Visible = false;
                TotalExpns.Visible = false;
                Sectionadvlist.Visible = false;

            }
            else if (drpType.SelectedValue == "Travel + Entertainment")
            {
                ClearAll();

                divTravel.Visible = true;
                divEntertainment.Visible = true;
                divUploadEnter.Visible = true;
                divfileUploadEnter.Visible = true;
                Class_Travel.Visible = true;
                radioPrimary1.Checked = false;
                radioPrimary2.Checked = false;
                travelClass.Visible = true;
                divTravel.Visible = true;
                divfileUpload.Visible = true;
                divfileDisplay.Visible = false;
                divfileUploadEnter.Visible = true;
                divfileDisplayEnter.Visible = false;

                eligibility.Visible = true;
                claimAmt.Visible = true;
                TotalExpns.Visible = true;
                Sectionadvlist.Visible = false;

            }
            else
            {
                divTravel.Visible = false;
                divEntertainment.Visible = false;
                divUploadEnter.Visible = false;
                divfileUploadEnter.Visible = false;
                Class_Travel.Visible = false;


            }
            chk1.Checked = false;
            chk2.Checked = false;
            chk3.Checked = false;
            chk4.Checked = false;
            chk5.Checked = false;
            chk6.Checked = false;
            chk7.Checked = false;
            chk8.Checked = false;
            txtchk1.Visible = false;
            txtchk2.Visible = false;
            txtchk3.Visible = false;
            txtchk4.Visible = false;
            txtchk5.Visible = false;
            txtchk6.Visible = false;
            txtchk7.Visible = false;
            txtchk8.Visible = false;
            divMetro.Visible = false;
            ClearAllControls(Page);




            txtadvance.Text = (string)(Session["advamount"]);

            txtadvvoucher.Text = (string)(Session["advvoucher"]);
            txtadvid.Text = (string)(Session["advid"]);

            txtadvid.Enabled = false;
            txtadvance.Enabled = false;
            txtadvvoucher.Enabled = false;


        }

        protected void drpClassTravel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void lnkBtnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void lnkBtnView_Click(object sender, EventArgs e)
        {

        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            dsExp = objExp.GetCount(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "Domestic");
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
                divAlert.Visible = false;
                SectionList.Visible = false;
                divFrom.Visible = true;
                traveltype.Visible = true;
                travelClass.Visible = false;
                divTravel.Visible = false;
                divEntertainment.Visible = false;
                Section1.Visible = false;
                DivAction.Visible = false;
                Sectionadvlist.Visible = false;


                //divTravel.Visible = false;
                //divEntertainment.Visible = false;
                //divfileDisplay.Visible = false;

                getCategoryType();
                drpType.SelectedIndex = -1;
                drpType.Enabled = true;
                txtFromDate.Text = "";
                txtToDate.Text = "";
                txtStartDest.Text = "";
                txtEndDest.Text = "";
                txtdescription.Text = "";
                txtTravelAmt.Text = "";
                txtEntAmount.Text = "";
                txtTravelAmt.Text = "";
                txtUserEligiRemark.Text = "";
                txtNoDays.Text = "";
                txtligibility.Text = "";
                chk1.Checked = false;
                chk2.Checked = false;
                chk3.Checked = false;
                chk4.Checked = false;
                chk5.Checked = false;
                chk6.Checked = false;
                chk7.Checked = false;
                chk8.Checked = false;
                txtchk1.Visible = false;
                txtchk2.Visible = false;
                txtchk3.Visible = false;
                txtchk4.Visible = false;
                txtchk5.Visible = false;
                txtchk6.Visible = false;
                txtchk7.Visible = false;
                txtchk8.Visible = false;
                txtchk1.Text = "";
                txtchk2.Text = "";
                txtchk3.Text = "";
                txtchk4.Text = "";
                txtchk5.Text = "";
                txtchk6.Text = "";
                txtchk7.Text = "";
                txtchk8.Text = "";

                txtadvid.Enabled = false;
                txtadvance.Enabled = false;
                txtadvvoucher.Enabled = false;
                btnSave.Visible = true;
                txtadvance.Text = "";
            }
        }

        private void ClearAll()
        {
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtStartDest.Text = "";
            txtEndDest.Text = "";
            txtdescription.Text = "";
            txtTravelAmt.Text = "";
            txtEntAmount.Text = "";
            txtTravelAmt.Text = "";
            txtUserEligiRemark.Text = "";
            txtNoDays.Text = "";
            txtligibility.Text = "";
            txtadvance.Text = "";

            chk1.Checked = false;
            chk2.Checked = false;
            chk3.Checked = false;
            chk4.Checked = false;
            chk5.Checked = false;
            chk6.Checked = false;
            chk7.Checked = false;
            chk8.Checked = false;

            txtchk1.Visible = false;
            txtchk2.Visible = false;
            txtchk3.Visible = false;
            txtchk4.Visible = false;
            txtchk5.Visible = false;
            txtchk6.Visible = false;
            txtchk7.Visible = false;
            txtchk8.Visible = false;

            txtchk1.Text = "";
            txtchk2.Text = "";
            txtchk3.Text = "";
            txtchk4.Text = "";
            txtchk5.Text = "";
            txtchk6.Text = "";
            txtchk7.Text = "";
            txtchk8.Text = "";

            chk1.Enabled = true;
            chk2.Enabled = true;
            chk3.Enabled = true;
            chk4.Enabled = true;
            chk5.Enabled = true;
            chk6.Enabled = true;
            chk7.Enabled = true;
            chk8.Enabled = true;

            txtchk1.Enabled = true;
            txtchk2.Enabled = true;
            txtchk3.Enabled = true;
            txtchk4.Enabled = true;
            txtchk5.Enabled = true;
            txtchk6.Enabled = true;
            txtchk7.Enabled = true;
            txtchk8.Enabled = true;

            txtdescription.Enabled = true;
            txtUserEligiRemark.Enabled = true;
            txtFromDate.Enabled = true;
            txtToDate.Enabled = true;
            txtStartDest.Enabled = true;
            txtEndDest.Enabled = true;
            radioPrimary1.Enabled = true;
            radioPrimary2.Enabled = true;
            txtTravelAmt.Enabled = true;
            txtUserTravelRemarks.Enabled = true;
            txtEnterDesc.Enabled = true;
            txtEntAmount.Enabled = true;
            txtUserEligiRemark.Enabled = true;
        }


        private void CalculateTotalDays()
        {
            DateTime fromDate;
            DateTime toDate;

            if (DateTime.TryParse(txtFromDate.Text, out fromDate) && DateTime.TryParse(txtToDate.Text, out toDate))
            {
                if (fromDate <= toDate)
                {
                    TimeSpan timeDiff = toDate - fromDate;
                    int totalDays = timeDiff.Days + 1; // Add 1 to include the end date
                    txtNoDays.Text = totalDays.ToString();
                }
                else
                {
                    txtNoDays.Text = string.Empty;
                }
            }
            else
            {
                txtNoDays.Text = string.Empty;
            }
        }
        protected void radioPrimary1_CheckedChanged(object sender, EventArgs e)
        {


            if (txtFromDate.Text == "" || txtToDate.Text == "")
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Please select the travel Date.";
                string script = $@"<script type='text/javascript'>alert('Please select the travel Date.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            }
            else
            {
                if (radioPrimary1.Checked == true)
                {
                    divMetro.Visible = true;
                    divNonMetro.Visible = false;
                    divAmt.Visible = true;
                    divEligi.Visible = true;
                    divadv.Visible = true;
                    divUpload.Visible = true;
                    divfileUpload.Visible = true;
                    divfileDisplay.Visible = false;

                    dsExp = objExp.GetMetroReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_TYPE"]));
                    if (dsExp.Tables.Count > 0)
                    {
                        //dsExp.Tables[0].Rows[0][""].ToString();
                        grMetro.DataSource = dsExp.Tables[0];
                        grMetro.DataBind();
                    }
                    else
                    {
                        grMetro.DataSource = null;
                        grMetro.DataBind();

                    }

                }
                if (radioPrimary2.Checked == true)
                {
                    divMetro.Visible = false;
                    divNonMetro.Visible = true;
                    divAmt.Visible = true;
                    divEligi.Visible = true;
                    divadv.Visible = true;
                    divUpload.Visible = true;
                    divfileUpload.Visible = true;
                    divfileDisplay.Visible = false;
                    dsExp = objExp.GetNonMetroReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_TYPE"]));
                    if (dsExp.Tables.Count > 0)
                    {
                        grNonMetro.DataSource = dsExp.Tables[0];
                        grNonMetro.DataBind();
                    }
                    else
                    {
                        grMetro.DataSource = null;
                        grMetro.DataBind();
                    }
                }
                CalculateTotalDays();
                CalculateEligibAmt();
            }
        }
        protected void Upload_Click(string entryCode)
        {
            if (fupldDocument.PostedFile.FileName == "")
            {
                divAlert.Visible = true;
                lblMessage.Text = "Please Upload Transport document.";
                return;
            }

            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Domestic\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\" + entryCode + "\\";

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


        protected void UploadEnter_Click(string entryCode)
        {
            if (fupldDocumentEnter.PostedFile.FileName == "")
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
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Domestic\\Entertainment\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\" + entryCode + "\\";

            System.IO.Stream fileInputStream = fupldDocumentEnter.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(fupldDocumentEnter.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocumentEnter.FileName.Trim());

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
        //private Boolean ValidateTravel()
        //{
        //    if (fupldDocument.PostedFile != null)
        //    {
        //        if (Convert.ToString(fupldDocument.PostedFile.FileName) == "")
        //        {
        //            divAlert.Visible = true;
        //            lblMessage.Visible = true;
        //            lblMessage.Text = "Browse Travel file to upload.";
        //            //ShowMessage("Browse file to upload.", WarningType.Danger);
        //            string script = $@"<script type='text/javascript'>alert('Browse Travel file to upload.');</script>";
        //            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //            return false;
        //        }
        //        else
        //        {

        //            return true;
        //        }
        //    }
        //    return true;
        //}
        private Boolean ValidateEntrtaimnet()
        {
            if (fupldDocumentEnter.PostedFile != null)
            {
                if (Convert.ToString(fupldDocumentEnter.PostedFile.FileName) == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse Entertainment file to upload.";
                    //ShowMessage("Browse file to upload.", WarningType.Danger);

                    string script = $@"<script type='text/javascript'>alert('Browse Entertainment file to upload.');</script>";
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
                if (drpType.SelectedValue == "Entertainment")
                {
                    if (ValidateEntrtaimnet() == false)
                    {
                        return;
                    }
                    objExp.EntAmount = txtEntAmount.Text;
                    objExp.UserEligiRemark = txtUserEligiRemark.Text;
                    objExp.FromDate = txtFromDate.Text;
                    objExp.DESCRIPTION_EXP = txtdescription.Text;
                    objExp.ToDate = txtToDate.Text;
                    objExp.From = txtStartDest.Text;
                    objExp.To = txtEndDest.Text;
                    objExp.MetroOrNonMetro = "";
                    objExp.StayDays = "";
                    objExp.TravelAmount = "";
                    objExp.advance = txtadvance.Text;
                    objExp.UserTravelRemarks = "";
                    objExp.EntermDesc = txtEnterDesc.Text;
                    string strCheckValue = "";

                    if (chk1.Checked == true)
                    {
                        objExp.Claim1_Amount = txtchk1.Text;
                    }
                    else
                    {
                        objExp.Claim1_Amount = "0";
                    }
                    if (chk2.Checked == true)
                    {
                        objExp.Claim2_Amount = txtchk2.Text;
                    }
                    else
                    {
                        objExp.Claim2_Amount = "0";
                    }
                    if (chk3.Checked == true)
                    {
                        objExp.Claim3_Amount = txtchk3.Text;
                    }
                    else
                    {
                        objExp.Claim3_Amount = "0";
                    }
                    if (chk4.Checked == true)
                    {
                        objExp.Claim4_Amount = txtchk4.Text;
                    }
                    else
                    {
                        objExp.Claim4_Amount = "0";
                    }
                    if (chk5.Checked == true)
                    {
                        objExp.Claim5_Amount = txtchk5.Text;
                    }
                    else
                    {
                        objExp.Claim5_Amount = "0";
                    }
                    if (chk6.Checked == true)
                    {
                        objExp.Claim6_Amount = txtchk6.Text;
                    }
                    else
                    {
                        objExp.Claim6_Amount = "0";
                    }
                    if (chk7.Checked == true)
                    {
                        objExp.Claim7_Amount = txtchk7.Text;
                    }
                    else
                    {
                        objExp.Claim7_Amount = "0";
                    }
                    if (chk8.Checked == true)
                    {
                        objExp.Claim8_Amount = txtchk8.Text;
                    }
                    else
                    {
                        objExp.Claim8_Amount = "0";
                    }

                    if (chk1.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk1";
                    }
                    if (chk2.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk2";
                    }
                    if (chk3.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk3";
                    }
                    if (chk4.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk4";
                    }
                    if (chk5.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk5";
                    }
                    if (chk6.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk6";
                    }
                    if (chk7.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk7";
                    }
                    if (chk8.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk8";
                    }

                    strCheckValue = strCheckValue.TrimStart(',');
                    objExp.TravelClass = strCheckValue;
                }
                else if (drpType.SelectedValue == "Travel + Entertainment")
                {
                    if (ValidateDates() == false)
                    {
                        return;
                    }
                    if (ValidateTravel() == false)
                    {
                        return;
                    }
                    if (ValidateEntrtaimnet() == false)
                    {
                        return;
                    }
                    objExp.FromDate = txtFromDate.Text;
                    objExp.ToDate = txtToDate.Text;
                    objExp.From = txtStartDest.Text;
                    objExp.To = txtEndDest.Text;
                    objExp.DESCRIPTION_EXP = txtdescription.Text;
                    objExp.TravelAmount = txtTravelAmt.Text;
                    objExp.EntAmount = txtEntAmount.Text;
                    objExp.advance = txtadvance.Text;
                    objExp.UserTravelRemarks = txtUserTravelRemarks.Text;
                    objExp.UserEligiRemark = txtUserEligiRemark.Text;

                    objExp.EntermDesc = txtEnterDesc.Text;
                    string strCheckValue = "";

                    if (chk1.Checked == true)
                    {
                        objExp.Claim1_Amount = txtchk1.Text;
                    }
                    else
                    {
                        objExp.Claim1_Amount = "0";
                    }
                    if (chk2.Checked == true)
                    {
                        objExp.Claim2_Amount = txtchk2.Text;
                    }
                    else
                    {
                        objExp.Claim2_Amount = "0";
                    }
                    if (chk3.Checked == true)
                    {
                        objExp.Claim3_Amount = txtchk3.Text;
                    }
                    else
                    {
                        objExp.Claim3_Amount = "0";
                    }
                    if (chk4.Checked == true)
                    {
                        objExp.Claim4_Amount = txtchk4.Text;
                    }
                    else
                    {
                        objExp.Claim4_Amount = "0";
                    }
                    if (chk5.Checked == true)
                    {
                        objExp.Claim5_Amount = txtchk5.Text;
                    }
                    else
                    {
                        objExp.Claim5_Amount = "0";
                    }
                    if (chk6.Checked == true)
                    {
                        objExp.Claim6_Amount = txtchk6.Text;
                    }
                    else
                    {
                        objExp.Claim6_Amount = "0";
                    }
                    if (chk7.Checked == true)
                    {
                        objExp.Claim7_Amount = txtchk7.Text;
                    }
                    else
                    {
                        objExp.Claim7_Amount = "0";
                    }
                    if (chk8.Checked == true)
                    {
                        objExp.Claim8_Amount = txtchk8.Text;
                    }
                    else
                    {
                        objExp.Claim8_Amount = "0";
                    }


                    if (chk1.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk1";
                    }
                    if (chk2.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk2";
                    }
                    if (chk3.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk3";
                    }
                    if (chk4.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk4";
                    }
                    if (chk5.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk5";
                    }
                    if (chk6.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk6";
                    }
                    if (chk7.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk7";
                    }
                    if (chk8.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk8";
                    }


                    strCheckValue = strCheckValue.TrimStart(',');
                    objExp.TravelClass = strCheckValue;
                    if (radioPrimary1.Checked == true)
                    {
                        objExp.MetroOrNonMetro = "T0000001";
                    }
                    else if (radioPrimary2.Checked == true)
                    {
                        objExp.MetroOrNonMetro = "T0000002";
                    }
                    else
                    {
                        objExp.MetroOrNonMetro = "";
                    }

                    objExp.StayDays = txtNoDays.Text;


                }
                else if (drpType.SelectedValue == "Travel")
                {
                    if (ValidateDates() == false)
                    {
                        return;
                    }

                    if (ValidateTravel() == false)
                    {
                        return;
                    }
                    objExp.FromDate = txtFromDate.Text;
                    objExp.ToDate = txtToDate.Text;
                    objExp.From = txtStartDest.Text;
                    objExp.To = txtEndDest.Text;
                    objExp.advance = txtadvance.Text;
                    objExp.DESCRIPTION_EXP = txtdescription.Text;
                    string strCheckValue = "";

                    if (chk1.Checked == true)
                    {
                        objExp.Claim1_Amount = txtchk1.Text;
                    }
                    else
                    {
                        objExp.Claim1_Amount = "0";
                    }
                    if (chk2.Checked == true)
                    {
                        objExp.Claim2_Amount = txtchk2.Text;
                    }
                    else
                    {
                        objExp.Claim2_Amount = "0";
                    }
                    if (chk3.Checked == true)
                    {
                        objExp.Claim3_Amount = txtchk3.Text;
                    }
                    else
                    {
                        objExp.Claim3_Amount = "0";
                    }
                    if (chk4.Checked == true)
                    {
                        objExp.Claim4_Amount = txtchk4.Text;
                    }
                    else
                    {
                        objExp.Claim4_Amount = "0";
                    }
                    if (chk5.Checked == true)
                    {
                        objExp.Claim5_Amount = txtchk5.Text;
                    }
                    else
                    {
                        objExp.Claim5_Amount = "0";
                    }
                    if (chk6.Checked == true)
                    {
                        objExp.Claim6_Amount = txtchk6.Text;
                    }
                    else
                    {
                        objExp.Claim6_Amount = "0";
                    }
                    if (chk7.Checked == true)
                    {
                        objExp.Claim7_Amount = txtchk7.Text;
                    }
                    else
                    {
                        objExp.Claim7_Amount = "0";
                    }
                    if (chk8.Checked == true)
                    {
                        objExp.Claim8_Amount = txtchk8.Text;
                    }
                    else
                    {
                        objExp.Claim8_Amount = "0";
                    }

                    if (chk1.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk1";
                    }
                    if (chk2.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk2";
                    }
                    if (chk3.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk3";
                    }
                    if (chk4.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk4";
                    }
                    if (chk5.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk5";
                    }
                    if (chk6.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk6";
                    }
                    if (chk7.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk7";
                    }
                    if (chk8.Checked)
                    {
                        strCheckValue = strCheckValue + "," + "chk8";
                    }


                    strCheckValue = strCheckValue.TrimStart(',');
                    objExp.TravelClass = strCheckValue;
                    if (radioPrimary1.Checked == true)
                    {
                        objExp.MetroOrNonMetro = "T0000001";
                    }
                    else if (radioPrimary2.Checked == true)
                    {
                        objExp.MetroOrNonMetro = "T0000002";
                    }
                    else
                    {
                        objExp.MetroOrNonMetro = "";
                    }

                    objExp.StayDays = txtNoDays.Text;
                    objExp.TravelAmount = txtTravelAmt.Text;
                    objExp.EntAmount = "";
                    objExp.UserTravelRemarks = txtTravelAmt.Text;
                    objExp.UserEligiRemark = "";
                }
                objExp.txtligibility = txtligibility.Text;//elgAmount.Text;
                objExp.txtTotalexp = txtTotalexp.Text;
                objExp.TravelType = drpType.SelectedValue;
                objExp.CategoryType = Session["CATEGORY_TYPE"].ToString();
                objExp.FilingStatus = "S";
                objExp.Status = "9";
                if (Session["Entry_aid"] != null)
                {
                    objExp.EntryAid = Session["Entry_aid"].ToString();
                }

                if (Session["advid"] != null)
                {
                    objLoc.advid = (string)(Session["advid"]);
                }

                objExp.FinYears = DateTime.Now.Year.ToString();
                if (drpType.SelectedValue == "Travel")
                {
                    if (validateTravelTotalamount() == false)
                    {
                        return;
                    }
                }
                else if (drpType.SelectedValue == "Entertainment")
                {
                    if (validateEntTotalamount() == false)
                    {
                        return;
                    }

                }
                else if (drpType.SelectedValue == "Travel + Entertainment")
                {
                    if (validateTravelTotalamount() == false)
                    {
                        return;
                    }
                    if (validateEntTotalamount() == false)
                    {
                        return;
                    }

                }

                dsExp = objExp.InsertDomesticClaim(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables.Count > 0)
                {
                    string entryCode = dsExp.Tables[0].Rows[0]["App_AID"].ToString();
                    if (drpType.SelectedValue == "Entertainment")
                    {
                        divEntertainment.Visible = true;
                        divUploadEnter.Visible = true;
                        divfileDisplayEnter.Visible = true;
                        UploadEnter_Click(entryCode);
                        DisplayDocumentsEnter(entryCode);
                        //btnSave.Visible = false;
                    }
                    else if (drpType.SelectedValue == "Travel + Entertainment")
                    {
                        divfileUpload.Visible = false;
                        divfileDisplay.Visible = true;
                        Upload_Click(entryCode);
                        DisplayDocuments(entryCode);

                        divfileUploadEnter.Visible = false;
                        divEntertainment.Visible = true;
                        divUploadEnter.Visible = true;
                        divfileDisplayEnter.Visible = true;
                        UploadEnter_Click(entryCode);
                        DisplayDocumentsEnter(entryCode);

                        //btnSave.Visible = false;

                    }
                    else if (drpType.SelectedValue == "Travel")
                    {
                        divfileUpload.Visible = false;
                        divfileDisplay.Visible = true;
                        Upload_Click(entryCode);
                        DisplayDocuments(entryCode);
                        //btnSave.Visible = false;
                    }
                    divAlert.Visible = true;
                    btnSave.Visible = false;

                    string script = $@"<script type='text/javascript'>alert('Claim Submitted Successfully.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    lblMessage.Visible = true;
                    lblMessage.Text = "Claim Submitted Successfully";
                    // getDomesticData();
                    Session["advid"] = "";
                    divTravel.Visible = false;
                    divEntertainment.Visible = false;
                    divUploadEnter.Visible = false;
                    divfileUploadEnter.Visible = false;
                    Class_Travel.Visible = false;
                    DivAction.Visible = false;

                    SENDUDATEMAIL(txtFromDate.Text, txtToDate.Text, drpType.SelectedItem.Text);

                    getDomesticData();       // sanket change for close after submit.
                    getAdvanceData();
                    divFrom.Visible = false;
                    SectionList.Visible = true;
                    divAlert.Visible = false;
                    Sectionadvlist.Visible = true;
                    divadvclaim.Visible = false;
                    lblMessage.Text = "";

                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Claim Submitted Error";
            }

        }

        private void SENDUDATEMAIL(string frDate, string toDate, string type)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DateTime date = DateTime.Now;

            string EMPNAME = Session["sEmpName"].ToString();

            string clientbodys = "Dear " + EMPNAME + ",<br><br>Your " + type + " Expense is Submitted Successfully.<br>"
               + "Once the action taken will be notified to you through mail or same can be checked through ESS portal."
               + " <br> Type: -" + type
               + "  <br> Applied Date :- " + date
                     + "<br> From Date :- " + frDate
                     + "<br> To Date :- " + toDate
                     + "<br><br>ThankYou.<br><br>";
            //string emails = Session["sEmailId"].ToString();
            string emails = "techsupport@sequelgroup.co.in";
            string subjects = "Do Not Reply: Expense";
            emailSend.SendEmailNPL(emails, subjects, clientbodys);


            string checkerbodys = "Dear Payroll Team,<br><br>" + EMPNAME +
                " " + type + " Expense is received for approval. Kindly take the action for the same through logging-in into ESS portal."
              + " <br> Type: -" + type
              + "  <br>Applied Date :- " + date
                     + "<br> From Date :- " + frDate
                     + "<br> To Date :- " + toDate
                     + "<br><br>ThankYou.<br><br>";

            emails = "payrollservices@sequelgroup.co.in";
            emailSend.SendEmailNPL(emails, subjects, checkerbodys);


        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            getDomesticData();
            getAdvanceData();
            divFrom.Visible = false;
            SectionList.Visible = true;
            divAlert.Visible = false;
            Sectionadvlist.Visible = true;
            divadvclaim.Visible = false;
            lblMessage.Text = "";
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
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Domestic\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\" + entryCode + "\\";

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
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkBtnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorage");

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


        private void CreateDocumentsStructureEnter()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvEntertainment.DataSource = dtDocuments;
                gvEntertainment.DataBind();
            }
        }
        private void DisplayDocumentsEnter(string entryCode)
        {
            try
            {
                CreateDocumentsStructureEnter();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Domestic\\Entertainment\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\" + entryCode + "\\";

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

                    this.gvEntertainment.DataSource = dtDocInfo;
                    this.gvEntertainment.DataBind();

                    if (dtDocInfo.Rows.Count > 0)
                    {
                        divfileUploadEnter.Visible = false;
                        divfileDisplayEnter.Visible = true;


                    }
                    else
                    {
                        //trUpload.Visible = true;
                        divfileDisplayEnter.Visible = false;
                    }
                }
                else
                {
                    //trUpload.Visible = true;
                    divfileDisplayEnter.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkBtnOpenFileEnter_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblFileStorageEnter = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorageEnter");

                string openFilePath = lblFileStorageEnter.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
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

        protected void lnkDOMClmNoClmNo_Click(object sender, EventArgs e)
        {
            DivAction.Visible = true;
            divAlert.Visible = false;
            btnClose.Visible = true;
            btnClose.Enabled = true;
            drpType.Enabled = false;
            txtdescription.Enabled = false;
            chk1.Enabled = false;
            chk2.Enabled = false;
            chk3.Enabled = false;
            chk4.Enabled = false;
            chk5.Enabled = false;
            chk6.Enabled = false;
            chk7.Enabled = false;
            chk8.Enabled = false;
            txtchk1.Enabled = false;
            txtchk2.Enabled = false;
            txtchk3.Enabled = false;
            txtchk4.Enabled = false;
            txtchk5.Enabled = false;
            txtchk6.Enabled = false;
            txtchk7.Enabled = false;
            txtchk8.Enabled = false;
            txtadvance.Enabled = false;
            txtFromDate.Enabled = false;
            txtToDate.Enabled = false;
            txtStartDest.Enabled = false;
            txtEndDest.Enabled = false;
            radioPrimary1.Enabled = false;
            radioPrimary2.Enabled = false;
            txtTravelAmt.Enabled = false;
            txtUserTravelRemarks.Enabled = false;
            txtEnterDesc.Enabled = false;
            txtEntAmount.Enabled = false;
            txtUserEligiRemark.Enabled = false;
            LinkButton lnkDOMClmNo = (LinkButton)sender;

            string entryAid = ((LinkButton)lnkDOMClmNo.NamingContainer.FindControl("lnkDOMClmNoClmNo")).Text;
            TextBox txtRmk = (TextBox)lnkDOMClmNo.NamingContainer.FindControl("txtRmk");


            objExp.AppNo = entryAid;
            dsExp = objExp.getDomClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
            Section1.Visible = true;
            Session["Entry_aid"] = entryAid;

            if (dsExp.Tables.Count > 0)
            {
                SectionList.Visible = false;
                Sectionadvlist.Visible = false;

                string categoryType = dsExp.Tables[0].Rows[0]["CategoryType"].ToString();
                // drpType.Enabled = false;
                drpType.SelectedValue = dsExp.Tables[0].Rows[0]["TravelType"].ToString();

                if (dsExp.Tables[0].Rows[0]["TravelType"].ToString() == "Entertainment")
                {
                    divFrom.Visible = true;
                    divEntertainment.Visible = true;
                }
                else if (dsExp.Tables[0].Rows[0]["TravelType"].ToString() == "Travel + Entertainment")
                {
                    divFrom.Visible = true;
                    divEntertainment.Visible = true;
                    Class_Travel.Visible = true;
                }
                else
                {
                    divFrom.Visible = true;
                    divEntertainment.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["TravelType"].ToString() == "Entertainment")
                {
                    divEntertainment.Visible = true;
                    divUploadEnter.Visible = true;
                    divfileDisplayEnter.Visible = true;
                    //UploadEnter_Click(entryAid);
                    DisplayDocumentsEnter(entryAid);
                    btnSave.Visible = false;

                    //divEntertainment.Visible = true;
                    //DisplayDocumentsEnter(entryAid);

                }
                else if (dsExp.Tables[0].Rows[0]["TravelType"].ToString() == "Travel + Entertainment")
                {
                    divUpload.Visible = true;
                    divfileUpload.Visible = false;
                    divfileDisplay.Visible = true;
                    //Upload_Click(entryAid);
                    DisplayDocuments(entryAid);
                    Class_Travel.Visible = true;
                    divfileUploadEnter.Visible = false;
                    divEntertainment.Visible = true;
                    divUploadEnter.Visible = true;
                    divfileDisplayEnter.Visible = true;
                    //UploadEnter_Click(entryAid);
                    DisplayDocumentsEnter(entryAid);

                    btnSave.Visible = false;

                    //divFrom.Visible = true;
                    //divEntertainment.Visible = true;
                    //DisplayDocuments(entryAid);
                    //DisplayDocumentsEnter(entryAid);


                }
                else
                {
                    divUpload.Visible = true;
                    divfileUpload.Visible = false;
                    divfileDisplay.Visible = true;
                    //Upload_Click(entryAid);
                    DisplayDocuments(entryAid);
                    btnSave.Visible = false;

                    //divFrom.Visible = true;
                    //DisplayDocuments(entryAid);

                }
                string TravelClass = dsExp.Tables[0].Rows[0]["TravelClass"].ToString();
                if (TravelClass != " ")
                {
                    Class_Travel.Visible = true;
                    lblClassTravel.Visible = true;
                    getTravelClass(TravelClass);
                    lblClassTravel.Visible = true;
                    for (int i = 0; i < dsExp.Tables[1].Rows.Count; i++)
                    {
                        if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Air (Business Class)")
                        {
                            chk1.Visible = true;
                            txtchk1.Visible = true;
                            txtchk1.Text = dsExp.Tables[0].Rows[0]["Claim1_Amount"].ToString();
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Air (Economy Class)")
                        {
                            chk2.Visible = true;
                            txtchk2.Visible = true;
                            txtchk2.Text = dsExp.Tables[0].Rows[0]["Claim2_Amount"].ToString();
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Rail (AC 1st Class)")
                        {
                            chk3.Visible = true;
                            txtchk3.Visible = true;
                            txtchk3.Text = dsExp.Tables[0].Rows[0]["Claim3_Amount"].ToString();
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Rail (AC 2nd Class)")
                        {
                            chk4.Visible = true;
                            txtchk4.Visible = true;
                            txtchk4.Text = dsExp.Tables[0].Rows[0]["Claim4_Amount"].ToString();
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Rail (AC 3rd Class)")
                        {
                            chk5.Visible = true;
                            txtchk5.Visible = true;
                            txtchk5.Text = dsExp.Tables[0].Rows[0]["Claim5_Amount"].ToString();
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Rail (AC Chair Car)")
                        {
                            chk6.Visible = true;
                            txtchk6.Visible = true;
                            txtchk6.Text = dsExp.Tables[0].Rows[0]["Claim6_Amount"].ToString();
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Bus")
                        {
                            chk7.Visible = true;
                            txtchk7.Visible = true;
                            txtchk7.Text = dsExp.Tables[0].Rows[0]["Claim7_Amount"].ToString();
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Others")
                        {
                            chk8.Visible = true;
                            txtchk8.Visible = true;
                            txtchk8.Text = dsExp.Tables[0].Rows[0]["Claim8_Amount"].ToString();
                        }

                    }
                }
                txtFromDate.Text = dsExp.Tables[0].Rows[0]["FromDate"].ToString();
                txtToDate.Text = dsExp.Tables[0].Rows[0]["ToDate"].ToString();
                txtStartDest.Text = dsExp.Tables[0].Rows[0]["StartDestination"].ToString();
                txtEndDest.Text = dsExp.Tables[0].Rows[0]["EndDestination"].ToString();
                string MetroType = dsExp.Tables[0].Rows[0]["MetroType"].ToString();
                txtTravelAmt.Text = dsExp.Tables[0].Rows[0]["TravelAmt"].ToString();
                txtNoDays.Text = dsExp.Tables[0].Rows[0]["NoDays"].ToString();
                txtEntAmount.Text = dsExp.Tables[0].Rows[0]["Entertainment"].ToString();
                txtUserEligiRemark.Text = dsExp.Tables[0].Rows[0]["EntertainmmentRmk"].ToString();
                txtUserTravelRemarks.Text = dsExp.Tables[0].Rows[0]["TravelRmk"].ToString();
                txtdescription.Text = dsExp.Tables[0].Rows[0]["DESCRIPTION_EXP"].ToString();
                txtApprovedAmt.Text = dsExp.Tables[0].Rows[0]["ApprovedAmmount"].ToString();
                txtEnterDesc.Text = dsExp.Tables[0].Rows[0]["Entertainment_DESC"].ToString();
                txtadvance.Text = dsExp.Tables[0].Rows[0]["ADVANCE_AMT"].ToString();

                double totalClaimAmt = 0;
                if (dsExp.Tables[0].Rows[0]["TravelType"].ToString() == "Travel + Entertainment")
                {
                    totalClaimAmt = (Convert.ToDouble(txtTravelAmt.Text) + Convert.ToDouble(txtEntAmount.Text));
                    elgAmount.Text = (dsExp.Tables[0].Rows[0]["Eligibility"] ?? 0).ToString();
                    if (elgAmount.Text == "")
                    {
                        elgAmount.Text = "0";
                    }

                    double Total = Convert.ToDouble(totalClaimAmt);
                    claimAmount.Text = Total.ToString();
                    txtadvamt.Text = txtadvance.Text;
                    eligibility.Visible = true;
                    TotalExpns.Visible = true;
                    CalculatetotalDom();
                    txtTotalexp.Text = txtTotalexp.Text;
                }
                else if (dsExp.Tables[0].Rows[0]["TravelType"].ToString() == "Entertainment")
                {
                    totalClaimAmt = Convert.ToDouble(txtEntAmount.Text);
                    double Total = Convert.ToDouble(totalClaimAmt);
                    claimAmount.Text = Total.ToString();
                    txtadvamt.Text = txtadvance.Text;
                    eligibility.Visible = false;
                    TotalExpns.Visible = false;
                }
                else if (dsExp.Tables[0].Rows[0]["TravelType"].ToString() == "Travel")
                {
                    totalClaimAmt = (Convert.ToDouble(txtTravelAmt.Text));
                    elgAmount.Text = (dsExp.Tables[0].Rows[0]["Eligibility"] ?? 0).ToString();
                    if (elgAmount.Text == "")
                    {
                        elgAmount.Text = "0";
                    }
                    double Total = Convert.ToDouble(totalClaimAmt);
                    claimAmount.Text = Total.ToString();
                    txtadvamt.Text = txtadvance.Text;
                    eligibility.Visible = true;
                    TotalExpns.Visible = true;
                    CalculatetotalDom();
                    txtTotalexp.Text = txtTotalexp.Text;

                }

                dsExp = objExp.GetMetroReimb(Convert.ToString(Session["sCompID"]), categoryType);
                txtligibility.Enabled = false;
                if (MetroType == "T0000001")
                {
                    radioPrimary1.Checked = true;
                    divTravel.Visible = true;
                    divMetro.Visible = true;
                    divAmt.Visible = true;
                    divEligi.Visible = true;
                    divfileDisplay.Visible = true;

                    if (dsExp.Tables.Count > 0)
                    {
                        grMetro.DataSource = dsExp.Tables[0];
                        grMetro.DataBind();
                        double noOfDays = Convert.ToDouble(txtNoDays.Text);

                        foreach (GridViewRow row in grMetro.Rows)
                        {
                            Label lblLodging = (Label)row.FindControl("lblLodging");
                            Label lblBoarding = (Label)row.FindControl("lblBoarding");
                            Label lblMiscellaneous = (Label)row.FindControl("lblMiscellaneous");
                            double eligibAmt = (Convert.ToDouble(lblLodging.Text) + Convert.ToDouble(lblBoarding.Text) + Convert.ToDouble(lblMiscellaneous.Text)) * (noOfDays);
                            txtligibility.Text = eligibAmt.ToString();

                        }


                    }
                    else
                    {
                        grMetro.DataSource = null;
                        grMetro.DataBind();

                    }

                }
                else if (MetroType == "T0000002")
                {
                    radioPrimary2.Checked = true;
                    divTravel.Visible = true;
                    divNonMetro.Visible = true;
                    divAmt.Visible = true;
                    divEligi.Visible = true;
                    divfileDisplay.Visible = true;
                    double noOfDays = Convert.ToDouble(txtNoDays.Text);
                    if (dsExp.Tables.Count > 0)
                    {
                        grNonMetro.DataSource = dsExp.Tables[0];
                        grNonMetro.DataBind();
                        foreach (GridViewRow row in grNonMetro.Rows)
                        {
                            Label lblNonLodging = (Label)row.FindControl("lblNonLodging");
                            Label lblNonBoarding = (Label)row.FindControl("lblNonBoarding");
                            Label lblNonMiscellaneous = (Label)row.FindControl("lblNonMiscellaneous");
                            double eligibAmt = (Convert.ToDouble(lblNonLodging.Text) + Convert.ToDouble(lblNonBoarding.Text) + Convert.ToDouble(lblNonMiscellaneous.Text)) * (noOfDays);
                            txtligibility.Text = eligibAmt.ToString();
                        }
                    }
                    else
                    {
                        grNonMetro.DataSource = null;
                        grNonMetro.DataBind();

                    }

                }
                else if (MetroType == "")
                {
                    divTravel.Visible = false;
                    divMetro.Visible = false;
                    divNonMetro.Visible = false;
                }
                else
                {
                    divTravel.Visible = false;
                    divMetro.Visible = false;
                    divNonMetro.Visible = false;
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

        public void getTravelClass(string str)
        {
            try
            {
                Char[] delimiters = { ',' };
                String[] result = str.Split(delimiters);
                for (int i = 0; i < result.Length; i++)
                {

                    if (result[i] == "chk1")
                    {
                        chk1.Checked = true;
                    }
                    else if (result[i] == "chk2")
                    {
                        chk2.Checked = true;
                    }
                    else if (result[i] == "chk3")
                    {
                        chk3.Checked = true;
                    }
                    else if (result[i] == "chk4")
                    {
                        chk4.Checked = true;
                    }
                    else if (result[i] == "chk5")
                    {
                        chk5.Checked = true;
                    }
                    else if (result[i] == "chk6")
                    {
                        chk6.Checked = true;
                    }
                    else if (result[i] == "chk7")
                    {
                        chk7.Checked = true;
                    }
                    else if (result[i] == "chk8")
                    {
                        chk8.Checked = true;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void CalculateEligibAmt()
        {
            double noOfDays = Convert.ToDouble(txtNoDays.Text);
            if (radioPrimary1.Checked == true)
            {
                foreach (GridViewRow row in grMetro.Rows)
                {
                    Label lblLodging = (Label)row.FindControl("lblLodging");
                    Label lblBoarding = (Label)row.FindControl("lblBoarding");
                    Label lblMiscellaneous = (Label)row.FindControl("lblMiscellaneous");
                    double eligibAmt = (Convert.ToDouble(lblLodging.Text) + Convert.ToDouble(lblBoarding.Text) + Convert.ToDouble(lblMiscellaneous.Text)) * (noOfDays);
                    txtligibility.Text = eligibAmt.ToString();
                    elgAmount.Text = eligibAmt.ToString();
                }
            }
            else if (radioPrimary2.Checked == true)
            {
                foreach (GridViewRow row in grNonMetro.Rows)
                {
                    Label lblNonLodging = (Label)row.FindControl("lblNonLodging");
                    Label lblNonBoarding = (Label)row.FindControl("lblNonBoarding");
                    Label lblNonMiscellaneous = (Label)row.FindControl("lblNonMiscellaneous");
                    double eligibAmt = (Convert.ToDouble(lblNonLodging.Text) + Convert.ToDouble(lblNonBoarding.Text) + Convert.ToDouble(lblNonMiscellaneous.Text)) * (noOfDays);
                    txtligibility.Text = eligibAmt.ToString();
                    elgAmount.Text = eligibAmt.ToString();
                }
            }
        }

        protected void txtTravelAmt_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtTravelAmt.Text, out decimal travelAmount) && decimal.TryParse(txtligibility.Text, out decimal eligibilityAmount))
            {
                // Check if eligibility amount is less than travel amount
                if (eligibilityAmount < travelAmount)
                {
                    divAlert.Visible = true;
                    lblMessage.Text = "Claim amount is higher than the Limit!";
                    lblMessage.Visible = true;
                    string script = $@"<script type='text/javascript'>alert('Claim amount is higher than the Limit!');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    lblMessage.Visible = true;
                }
                else
                {
                    divAlert.Visible = false;
                    lblMessage.Text = "";
                    lblMessage.Visible = false;
                }
                CalculatetotalDom();
                //claimAmount.Text = txtTravelAmt.Text;
                //txtadvamt.Text = txtadvance.Text;
                //elgAmount.Text = txtligibility.Text;
            }
            else
            {
                divAlert.Visible = true;
                lblMessage.Text = "Invalid input format!";
                lblMessage.Visible = true;
            }
        }
        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            CalculateTotalDays();
            CalculateEligibAmt();
        }
        protected void txtadvance_TextChanged(object sender, EventArgs e)
        {
            txtadvamt.Text = txtadvance.Text;
        }
        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtToDate.Text != "")
                {
                    CalculateTotalDays();
                    CalculateEligibAmt();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void gvDomClaim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                TextBox txtRmk = (TextBox)e.Row.FindControl("txtRmk");
                //TextBox txtdescri = (TextBox)e.Row.FindControl("txtdescri");
                DropDownList ddls = (DropDownList)e.Row.FindControl("drpAction");
                Label lblEmpAId = (Label)e.Row.FindControl("lblEmpAId");
                Label TravelAmt = (Label)e.Row.FindControl("txtClmAmt");
                Label txtAppAmtr = (Label)e.Row.FindControl("txtAppAmtr");
                Label EntertainmentChked = (Label)e.Row.FindControl("EntertainmentChked");
                LinkButton lnkDOMClmNoClmNo = (LinkButton)e.Row.FindControl("lnkDOMClmNoClmNo");
                DataSet ds = new DataSet();
                ds = objExp.GetDOMLimit(lblEmpAId.Text, lnkDOMClmNoClmNo.Text, Convert.ToString(Session["sCompID"]));
                if (txtAppAmtr.Text != "")
                {
                    if (EntertainmentChked.Text == "T")
                    {
                        if (ds.Tables[0].Rows[0]["limit_amount"].ToString() == "0")
                        {
                            lblMessage.Text = "Limit Not Found.";
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                        }
                        else if (ds.Tables[0].Rows[0]["limit_amount"].ToString() != "")
                        {
                            if (decimal.Parse(txtAppAmtr.Text.Trim()) > decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                            {
                                //e.Row.BackColor = System.Drawing.Color.Red;

                                TableCell cell = e.Row.Cells[2];
                                cell.BackColor = System.Drawing.Color.Yellow;
                            }
                            else if (decimal.Parse(txtAppAmtr.Text.Trim()) < decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                            {
                                TableCell cell = e.Row.Cells[2];
                                cell.BackColor = System.Drawing.Color.Orange;
                            }
                        }
                        else
                        {
                            TableCell cell = e.Row.Cells[2];
                            cell.BackColor = System.Drawing.Color.Orange;
                        }
                    }

                    else if (EntertainmentChked.Text == "F")
                    {
                        if (ds.Tables[0].Rows[0]["limit_amount"].ToString() == "0")
                        {
                            lblMessage.Text = "Limit Not Found.";
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                        }
                        else if (ds.Tables[0].Rows[0]["limit_amount"].ToString() != "")
                        {
                            if (decimal.Parse(txtAppAmtr.Text.Trim()) > decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                            {
                                TableCell cell = e.Row.Cells[2];
                                cell.BackColor = System.Drawing.Color.Yellow;
                            }
                            else if (decimal.Parse(txtAppAmtr.Text.Trim()) < decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                            {
                                TableCell cell = e.Row.Cells[2];
                                cell.BackColor = System.Drawing.Color.LightGreen;
                            }
                        }
                        else
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

        protected void chk5_CheckedChanged(object sender, EventArgs e)
        {
            if (chk5.Checked == true)
            {
                txtchk5.Visible = true;
            }
            else
            {
                txtchk5.Visible = false;
            }
        }

        protected void chk6_CheckedChanged(object sender, EventArgs e)
        {
            if (chk6.Checked == true)
            {
                txtchk6.Visible = true;
            }
            else
            {
                txtchk6.Visible = false;
            }
        }

        protected void chk7_CheckedChanged(object sender, EventArgs e)
        {
            if (chk7.Checked == true)
            {
                txtchk7.Visible = true;
            }
            else
            {
                txtchk7.Visible = false;
            }
        }

        protected void chk8_CheckedChanged(object sender, EventArgs e)
        {
            if (chk8.Checked == true)
            {
                txtchk8.Visible = true;
            }
            else
            {
                txtchk8.Visible = false;
            }
        }

        private void CalculatetotalDom()
        {
            decimal chk1 = string.IsNullOrEmpty(txtchk1.Text) ? 0 : decimal.Parse(txtchk1.Text);
            decimal chk2 = string.IsNullOrEmpty(txtchk2.Text) ? 0 : decimal.Parse(txtchk2.Text);
            decimal chk3 = string.IsNullOrEmpty(txtchk3.Text) ? 0 : decimal.Parse(txtchk3.Text);
            decimal chk4 = string.IsNullOrEmpty(txtchk4.Text) ? 0 : decimal.Parse(txtchk4.Text);
            decimal chk5 = string.IsNullOrEmpty(txtchk5.Text) ? 0 : decimal.Parse(txtchk5.Text);
            decimal chk6 = string.IsNullOrEmpty(txtchk6.Text) ? 0 : decimal.Parse(txtchk6.Text);
            decimal chk7 = string.IsNullOrEmpty(txtchk7.Text) ? 0 : decimal.Parse(txtchk7.Text);
            decimal chk8 = string.IsNullOrEmpty(txtchk8.Text) ? 0 : decimal.Parse(txtchk8.Text);
            // decimal txtTravelAmt = decimal.TryParse(txtTravelAmt.Text, out decimal travelAmount); 
            if (decimal.TryParse(txtTravelAmt.Text, out decimal travelAmt))
            {
                decimal txtTravelAmt = travelAmt;
            }
            else
            {
                travelAmt = 0;
            }
            if (decimal.TryParse(txtEntAmount.Text, out decimal EntAmount))
            {
                decimal txtEntAmount = EntAmount;
            }
            else
            {
                EntAmount = 0;
            }
            if (decimal.TryParse(txtadvamt.Text, out decimal EntadvAmount))
            {
                decimal txtadvamt = EntadvAmount;
            }
            else
            {
                // EntAmount = 0;
            }

            //if (EntAmount.ToString() != "0" || EntAmount.ToString() != "0.00")
            //{

            //}
            //else
            //{
            //    EntAmount = 0;
            //}


            decimal claimAmt = travelAmt + EntAmount;
            decimal totalExpenses = chk1 + chk2 + chk3 + chk4 + chk5 + chk6 + chk7 + chk8 + travelAmt + EntAmount - EntadvAmount;

            claimAmount.Text = claimAmt.ToString();
            txtadvamt.Text = txtadvance.Text;
            txtTotalexp.Text = totalExpenses.ToString();
        }

        protected void btnCalTtl_Click(object sender, EventArgs e)
        {
            CalculatetotalDom();
        }


        protected void txtchk1_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalDom();
        }

        protected void txtchk2_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalDom();
        }

        protected void txtchk3_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalDom();
        }

        protected void txtchk4_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalDom();
        }

        protected void txtchk5_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalDom();
        }

        protected void txtchk6_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalDom();
        }

        protected void txtchk7_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalDom();
        }

        protected void txtchk8_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalDom();
        }

        protected void txtEntAmount_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtTravelAmt.Text, out decimal travelAmount) && decimal.TryParse(txtligibility.Text, out decimal eligibilityAmount))
            {
                // Check if eligibility amount is less than travel amount
                if (eligibilityAmount < travelAmount)
                {
                    divAlert.Visible = true;
                    lblMessage.Text = "Claim amount is higher than the Limit!";
                    // Use direct JavaScript alert here
                    string script = $@"<script type='text/javascript'>alert('Text changed to: {txtEntAmount}');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    lblMessage.Visible = true;
                }
                else
                {
                    divAlert.Visible = false;
                    lblMessage.Text = "";
                    lblMessage.Visible = false;
                }
                CalculatetotalDom();

            }
            else
            {
                divAlert.Visible = true;
                lblMessage.Text = "Invalid input format!";
                lblMessage.Visible = true;
            }
        }

        protected void ClearAllControls(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if (c == drpType) // Skip clearing the DropDownList drpType
                {
                    continue;
                }
                if (c is TextBox)
                {
                    ((TextBox)c).Text = "";
                }
                else if (c is DropDownList)
                {
                    ((DropDownList)c).SelectedIndex = 0;
                }
                else if (c is CheckBox)
                {
                    ((CheckBox)c).Checked = false;
                }
                else if (c is RadioButton)
                {
                    ((RadioButton)c).Checked = false;
                }
                else if (c is RadioButtonList)
                {
                    ((RadioButtonList)c).SelectedIndex = -1;
                }
                else if (c is CheckBoxList)
                {
                    CheckBoxList checkBoxList = (CheckBoxList)c;
                    foreach (ListItem item in checkBoxList.Items)
                    {
                        item.Selected = false;
                    }
                }
                else if (c is FileUpload)
                {
                    // Clear the FileUpload control by setting its HasFile property to false
                    ((FileUpload)c).Visible = false;
                    ((FileUpload)c).Visible = true;
                }
                else if (c.Controls.Count > 0)
                {
                    ClearAllControls(c); // Recursively clear controls in child controls
                }
            }
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
            // Session["advvoucher"] = voucher;


            divAlert.Visible = false;
            SectionList.Visible = false;
            divFrom.Visible = true;
            traveltype.Visible = true;
            travelClass.Visible = false;
            divTravel.Visible = false;
            divEntertainment.Visible = false;
            Section1.Visible = false;
            DivAction.Visible = false;
            Sectionadvlist.Visible = false;


            //divTravel.Visible = false;
            //divEntertainment.Visible = false;
            //divfileDisplay.Visible = false;

            getCategoryType();
            drpType.SelectedIndex = -1;
            drpType.Enabled = true;
            txtFromDate.Text = "";
            txtToDate.Text = "";
            txtStartDest.Text = "";
            txtEndDest.Text = "";
            txtdescription.Text = "";
            txtTravelAmt.Text = "";
            txtEntAmount.Text = "";
            txtTravelAmt.Text = "";
            txtUserEligiRemark.Text = "";
            txtNoDays.Text = "";
            txtligibility.Text = "";
            chk1.Checked = false;
            chk2.Checked = false;
            chk3.Checked = false;
            chk4.Checked = false;
            chk5.Checked = false;
            chk6.Checked = false;
            chk7.Checked = false;
            chk8.Checked = false;
            txtchk1.Visible = false;
            txtchk2.Visible = false;
            txtchk3.Visible = false;
            txtchk4.Visible = false;
            txtchk5.Visible = false;
            txtchk6.Visible = false;
            txtchk7.Visible = false;
            txtchk8.Visible = false;
            txtchk1.Text = "";
            txtchk2.Text = "";
            txtchk3.Text = "";
            txtchk4.Text = "";
            txtchk5.Text = "";
            txtchk6.Text = "";
            txtchk7.Text = "";
            txtchk8.Text = "";
            btnSave.Visible = true;
            txtadvance.Text = "";
        }

        private Boolean ValidateDates()
        {

            if (txtFromDate.Text == "" || txtToDate.Text == "")
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Please select the travel Date.";
                string script = $@"<script type='text/javascript'>alert('Please select the travel Date.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            else
            {

                return true;
            }

        }

        private Boolean validateTravelTotalamount()
        {
            if (txtTotalexp.Text == "")
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Please enter any amount.";
                string script = $@"<script type='text/javascript'>alert('Please enter any amount.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            else
            {

                return true;
            }
        }

        private Boolean validateEntTotalamount()
        {
            if (txtEntAmount.Text == "")
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Please enter any enterainment amount.";
                string script = $@"<script type='text/javascript'>alert('Please enter any enterainment amount.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }
            else
            {

                return true;
            }
        }

        private Boolean ValidateTravel()
        {
            // 1️⃣ Validate Radio Buttons: At least one must be selected
            if (!radioPrimary1.Checked && !radioPrimary2.Checked)
            {
                string script = "<script type='text/javascript'>alert('Select city(Metro/Non-Metro) type.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }

            // 2️⃣ Validate File Upload: File must be selected
            if (fupldDocument.PostedFile == null || string.IsNullOrWhiteSpace(fupldDocument.PostedFile.FileName))
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Browse Travel file to upload.";

                string script = "<script type='text/javascript'>alert('Browse Travel file to upload.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return false;
            }

            // ✅ If both conditions are met, return true (validation successful)
            return true;
        }


    }
}