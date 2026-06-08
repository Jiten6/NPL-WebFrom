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
    public partial class StaffWellfare : System.Web.UI.Page
    {
        NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsExp = new DataSet();
        private string SourcePath = string.Empty;
        private string savePath = string.Empty;
        private string SourcePath2 = string.Empty;
        private string savePath2 = string.Empty;
        NewPortal2023.ESS.LocalExpenses objLoc = new NewPortal2023.ESS.LocalExpenses();


        protected void Page_Load(object sender, EventArgs e)
        {
             if (Session["sCompID"]!=null)
            {
            if (!Page.IsPostBack)
            {
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

                //getstaffwelfareData();
                //welfaretype.Visible = true;
                divFrom.Visible = false;
                Div9.Visible = false;
                Domastic.Visible = true;
                traveltype.Visible = false;
                divdom.Visible = false;
                div1.Visible = false;
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

        protected void drpTypeexpense_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpTypeexpense.SelectedValue == "Domestic")
            {
                divdom.Visible = true;
                div1.Visible = true;
                divFrom.Visible = false;
                // welfaretype.Visible = false;
                getstaffwelfareData();
                traveltype.Visible = false;
                divTravel.Visible = false;
                divEntertainment.Visible = false;
                divUploadEnter.Visible = false;
                divfileUploadEnter.Visible = false;
                Class_Travel.Visible = false;
                Section1.Visible = false;
                DivAction.Visible = false;
                Section2.Visible = false;
            }
            else if (drpTypeexpense.SelectedValue == "Local")
            {
                Div9.Visible = true;
                Section3.Visible = true;
                divdom.Visible = false;
                // welfaretype.Visible = false;
                Session["Entry_aid"] = null;

                getLocalData();

                getCategoryType();
                divAlert.Visible = false;
                traveltype.Visible = false;
                Section2.Visible = false;
                getLocalReimb();

            }
            else
            {
                //welfaretype.Visible = true;
                divFrom.Visible = false;
                Div9.Visible = false;

            }

        }

        protected void txtadvance_TextChanged(object sender, EventArgs e)
        {
            txtadvamt.Text = txtadvance.Text;
        }
        private void getstaffwelfareData()
        {
            try
            {

                dsExp = objExp.GetwelfareList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
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
                        lblClassTravel1.Visible = true;
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
            divAlert.Visible = false;
            SectionList.Visible = false;
            divFrom.Visible = true;
            traveltype.Visible = true;
            travelClass.Visible = false;
            divTravel.Visible = false;
            divEntertainment.Visible = false;
            Section1.Visible = false;
            DivAction.Visible = false;
            //traveltype.Visible = false;



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
        private Boolean ValidateTravel()
        {
            if (fupldDocument.PostedFile != null)
            {
                if (Convert.ToString(fupldDocument.PostedFile.FileName) == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse Travel file to upload.";
                    //ShowMessage("Browse file to upload.", WarningType.Danger);
                    string script = $@"<script type='text/javascript'>alert('Browse Travel file to upload.');</script>";
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
                objExp.txtligibility = elgAmount.Text;
                objExp.txtTotalexp = txtTotalexp.Text;
                objExp.TravelType = drpType.SelectedValue;
                objExp.CategoryType = Session["CATEGORY_TYPE"].ToString();
                objExp.FilingStatus = "S";
                objExp.Status = "9";
                if (Session["Entry_aid"] != null)
                {
                    objExp.EntryAid = Session["Entry_aid"].ToString();
                }
                objExp.FinYears = DateTime.Now.Year.ToString();
                dsExp = objExp.InsertStaffDomesticClaim(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
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
                    divTravel.Visible = false;
                    divEntertainment.Visible = false;
                    divUploadEnter.Visible = false;
                    divfileUploadEnter.Visible = false;
                    Class_Travel.Visible = false;
                    DivAction.Visible = false;

                    SENDUDATEMAIL(txtFromDate.Text, txtToDate.Text, drpType.SelectedItem.Text);

                    getstaffwelfareData();       // sanket change for close after submit.
                    divFrom.Visible = false;
                    SectionList.Visible = true;
                    divAlert.Visible = false;
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
            getstaffwelfareData();
            divFrom.Visible = false;
            SectionList.Visible = true;
            divAlert.Visible = false;
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
                    lblClassTravel1.Visible = true;
                    getTravelClass(TravelClass);
                    lblClassTravel1.Visible = true;
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
                claimAmount.Text = txtTravelAmt.Text;
                elgAmount.Text = txtligibility.Text;
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
                                cell.BackColor = System.Drawing.Color.Red;
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
                EntAmount = 0;
            }


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

        //// ------------- LOCAL STAFF WELFARE --------
        private void getLocalData()
        {
            try
            {
                getCategoryTypeloc();
                dsExp = objLoc.GeLocalwelfareList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
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


        private void getCategoryTypeloc()
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

                    //getLocalReimb();
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
        protected void btnAddLOCNew_Click(object sender, EventArgs e)
        {
            Div9.Visible = true;
            div5.Visible = true;
            Section4.Visible = true;
            div6.Visible = true;
            div7.Visible = true;
            SectionList.Visible = false;
            Section3.Visible = false;
            divAlert.Visible = false;
            divFrom.Visible = false;
            divTravel.Visible = false;
            divUpload.Visible = false;
            divfileUpload.Visible = false;
            btnlocsave.Visible = true;
            txtFromDate.Text = "";
            //txtCashVocher.Text = "";

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
            getCategoryTypeloc();
            getLocalReimb();

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //dsExp = objLoc.InsertLocClaim(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
            string entryCode = (string)Session["entryCode"];
            Uploadloc_Click(entryCode);
        }



        protected void btnlocsave_Click(object sender, EventArgs e)
        {
            try
            {
                objLoc.Expenses_Date = TextBox5.Text;
                objLoc.Cash_Voucher = txtCashVocher.Text;
                objLoc.Desg_Aid = Session["DESG_AID"].ToString();
                objLoc.CategoryType = Session["CATEGORY_TYPE"].ToString();
                objLoc.Travel_Description = txtTravelDes.Text;
                objLoc.Meal = txtMeal.Text;
                objLoc.advance = txtadv.Text;
                objLoc.Other_Expenses = txtOtherExpenses.Text;
                objLoc.Name_Bussi_Ass = txtNameAss.Text;
                objLoc.UserRemarks = txtUserRemarks.Text;
                if (CheckBox1.Checked == true)
                {
                    objLoc.Claim1_Amount = TextBox1.Text;
                }
                else
                {
                    objLoc.Claim1_Amount = "0";
                }
                if (CheckBox2.Checked == true)
                {
                    objLoc.Claim2_Amount = TextBox2.Text;
                }
                else
                {
                    objLoc.Claim2_Amount = "0";
                }
                if (CheckBox3.Checked == true)
                {
                    objLoc.Claim3_Amount = TextBox3.Text;
                }
                else
                {
                    objLoc.Claim3_Amount = "0";
                }
                if (CheckBox4.Checked == true)
                {
                    objLoc.Claim4_Amount = TextBox4.Text;
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


                dsExp = objLoc.InsertLocStaffClaim(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));

                if (dsExp.Tables.Count > 0)
                {
                    string entryCode = dsExp.Tables[0].Rows[0]["Claim_no"].ToString();

                    divfileUpload.Visible = false;
                    divfileDisplay.Visible = true;
                    Uploadloc_Click(entryCode);
                    DisplaylocDocuments(entryCode);
                    btnSave.Visible = false;
                    string script = $@"<script type='text/javascript'>alert('Claim Submitted Successfully.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    lblMessage.Visible = true;
                    lblMessage.Text = "Claim Submitted Successfully.";
                    //SENDUDATEMAIL(txtFromDate.Text, "Local Expense");
                    //getLocalData();
                    SectionList.Visible = false;
                    divFrom.Visible = false;
                    divAlert.Visible = false;
                    //Div9.Visible = true;
                    //Section3.Visible = true;
                }

            }
            catch (Exception ex)
            {

            }

        }
        private void SENDUDATEMAIL(string frDate, string type)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DateTime date = DateTime.Now;
            //dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], (string)Session["sEmpID"], ClaimNO, Approver, radiochkent);
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

        protected void Uploadloc_Click(string entryCode)
        {
            if (FileUpload1.PostedFile.FileName == "")
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

            System.IO.Stream fileInputStream = FileUpload1.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(FileUpload1.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(FileUpload1.FileName.Trim());

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

        private void CreateDocumentslocStructure()
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
        private void DisplaylocDocuments(string entryCode)
        {
            try
            {
                CreateDocumentslocStructure();

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
                        div7.Visible = false;
                        div8.Visible = true;


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

        protected void btnlocClose_Click(object sender, EventArgs e)
        {
            divFrom.Visible = false;
            divTravel.Visible = false;
            SectionList.Visible = false;
            Div9.Visible = true;
            Section3.Visible = true;
            divdom.Visible = false;
           
            divAlert.Visible = false;
            traveltype.Visible = false;
            Section2.Visible = false;
            //Div9.Visible = false;
            div5.Visible = false;
            Section4.Visible = false;
            div6.Visible = false;
            div7.Visible = false;
            SectionList.Visible = false;
           // Section3.Visible = false;
            divAlert.Visible = false;
            divFrom.Visible = false;
            divTravel.Visible = false;
            divUpload.Visible = false;
            divfileUpload.Visible = false;
            btnlocsave.Visible = false;
            getLocalReimb();
            getLocalData();
        }


        protected void lnkLOCClmNoClmNo_Click(object sender, EventArgs e)
        {
            LinkButton lnkDOMClmNo = (LinkButton)sender;

            string entryAid = ((LinkButton)lnkDOMClmNo.NamingContainer.FindControl("lnkLOCClmNoClmNo")).Text;
            Label txtRmk = (Label)lnkDOMClmNo.NamingContainer.FindControl("txtRmk");



            objLoc.AppNo = entryAid;
            Session["Entry_aid"] = entryAid;

            dsExp = objLoc.getLocalWELFAREClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
            if (dsExp.Tables.Count > 0)
            {
                SectionList.Visible = false;
                divFrom.Visible = false;
                divUpload.Visible = false;
                divfileUpload.Visible = false;
                divfileDisplay.Visible = false;
                divTravel.Visible = false;
                Div9.Visible = true;
                div5.Visible = true;
                Section4.Visible = true;
                div6.Visible = true;
                div7.Visible = true;
                //btnlocsave.Visible = true;
                Section3.Visible = false;
                //btnCalTtl1.Visible = false;

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

                if (dsExp.Tables[0].Rows[0]["Claim1_Amount"].ToString() != "0.0000")
                {
                    CheckBox1.Checked = true;
                    TextBox1.Visible = true;
                    TextBox1.Text = dsExp.Tables[0].Rows[0]["Claim1_Amount"].ToString();
                }
                else
                {
                    CheckBox1.Checked = false;
                    TextBox1.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["Claim2_Amount"].ToString() != "0.0000")
                {
                    CheckBox2.Checked = true;
                    TextBox2.Visible = true;
                    TextBox2.Text = dsExp.Tables[0].Rows[0]["Claim2_Amount"].ToString();
                }
                else
                {
                    CheckBox2.Checked = false;
                    TextBox2.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["Claim3_Amount"].ToString() != "0.0000")
                {
                    CheckBox3.Checked = true;
                    TextBox3.Visible = true;
                    TextBox3.Text = dsExp.Tables[0].Rows[0]["Claim3_Amount"].ToString();
                }
                else
                {
                    CheckBox3.Checked = false;
                    TextBox3.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["Claim4_Amount"].ToString() != "0.0000")
                {
                    CheckBox4.Checked = true;
                    TextBox4.Visible = true;
                    TextBox4.Text = dsExp.Tables[0].Rows[0]["Claim4_Amount"].ToString();
                }
                else
                {
                    CheckBox4.Checked = false;
                    TextBox4.Visible = false;

                }
                TextBox5.Text = dsExp.Tables[0].Rows[0]["Expenses_Date"].ToString();
                txtCashVocher.Text = dsExp.Tables[0].Rows[0]["Cash_Voucher"].ToString();

                txtTravelDes.Text = dsExp.Tables[0].Rows[0]["Travel_Description"].ToString();
                txtMeal.Text = dsExp.Tables[0].Rows[0]["Meal"].ToString();
                txtOtherExpenses.Text = dsExp.Tables[0].Rows[0]["Other_Expenses"].ToString();
                txtNameAss.Text = dsExp.Tables[0].Rows[0]["Name_Bussi_Ass"].ToString();
                txtUserRemarks.Text = dsExp.Tables[0].Rows[0]["Claim_Remark"].ToString();
                txtadv.Text = dsExp.Tables[0].Rows[0]["ADVANCE_AMT"].ToString();
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
                CalculatetotalLOCexp();

                if (dsExp.Tables.Count > 0)
                {
                    grLocalReimb.DataSource = dsExp.Tables[0];
                    grLocalReimb.DataBind();
                    DisplaylocDocuments(entryAid);
                    btnlocsave.Visible = false;
                }
                else
                {
                    grLocalReimb.DataSource = null;
                    grLocalReimb.DataBind();
                }
                if (txtRmk.Text == "Rejected")
                {
                    btnlocsave.Visible = true;
                }
                else
                {
                    btnlocsave.Visible = false;
                }
            }
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                TextBox1.Visible = true;
            }
            else
            {
                TextBox1.Visible = false;
            }
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox2.Checked == true)
            {
                TextBox2.Visible = true;
            }
            else
            {
                TextBox2.Visible = false;
            }
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox3.Checked == true)
            {
                TextBox3.Visible = true;
            }
            else
            {
                TextBox3.Visible = false;
            }
        }

        protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox4.Checked == true)
            {
                TextBox4.Visible = true;
            }
            else
            {
                TextBox4.Visible = false;
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
                            e.Row.BackColor = System.Drawing.Color.Orange;

                        }
                        else if (EntertainmentChked.Text == "F")
                        {
                            e.Row.BackColor = System.Drawing.Color.LightGreen;
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
                    else
                    {
                        txtRmk.Text = " Pending With " + txtRmk.Text + " .";
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void CalculatetotalLOCexp()
        {
            decimal chk1 = string.IsNullOrEmpty(TextBox1.Text) ? 0 : decimal.Parse(TextBox1.Text);
            decimal chk2 = string.IsNullOrEmpty(TextBox2.Text) ? 0 : decimal.Parse(TextBox2.Text);
            decimal chk3 = string.IsNullOrEmpty(TextBox3.Text) ? 0 : decimal.Parse(TextBox3.Text);
            decimal chk4 = string.IsNullOrEmpty(TextBox4.Text) ? 0 : decimal.Parse(TextBox4.Text);
            decimal meal = string.IsNullOrEmpty(txtMeal.Text) ? 0 : decimal.Parse(txtMeal.Text);
            decimal otherExpenses = string.IsNullOrEmpty(txtOtherExpenses.Text) ? 0 : decimal.Parse(txtOtherExpenses.Text);

            decimal adv = string.IsNullOrEmpty(txtadv.Text) ? 0 : decimal.Parse(txtadv.Text);

            decimal totalExpenses = chk1 + chk2 + chk3 + chk4 + meal + otherExpenses;
            decimal paid = totalExpenses - adv;

            TextBox6.Text = totalExpenses.ToString();
            txtpaidamt.Text = paid.ToString();
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalLOCexp();
        }

        protected void TextBox2_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalLOCexp();
        }

        protected void TextBox3_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalLOCexp();
        }

        protected void TextBox4_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalLOCexp();
        }

        protected void txtMeal_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalLOCexp();
        }

        protected void txtOtherExpenses_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalLOCexp();
        }

        protected void btnCalTtl1_Click(object sender, EventArgs e)
        {
            CalculatetotalLOCexp();
        }
        protected void txtadv_TextChanged(object sender, EventArgs e)
        {
            CalculatetotalLOCexp();
        }

        protected void btnback_Click(object sender, EventArgs e)
        {
            divFrom.Visible = false;
            Div9.Visible = false;
            Domastic.Visible = true;
            Section2.Visible = true;
            traveltype.Visible = false;
            divdom.Visible = false;
            div1.Visible = false;
            drpTypeexpense.SelectedValue = "";

        }
        protected void btnlocback_Click(object sender, EventArgs e)
        {
            divFrom.Visible = false;
            Div9.Visible = false;
            SectionList.Visible = true;
            Domastic.Visible = true;
            Section2.Visible = true;
            Section3.Visible = false;
            traveltype.Visible = false;
            divdom.Visible = false;
            div1.Visible = false;
            divnotes.Visible = false;
            drpTypeexpense.SelectedValue = "";

        }
    }
}