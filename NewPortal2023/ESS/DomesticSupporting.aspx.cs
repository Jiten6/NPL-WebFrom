using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;


namespace NewPortal2023.ESS
{
    public partial class DomesticSupporting : System.Web.UI.Page
    {
        NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsExp = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                getDomesticData();

            }
        }

        private void getDomesticData()
        {
            try
            {
                string emp_aid = Session["sEmpID"].ToString();

                dsExp = objExp.GetSeqSupportingomesticList(Convert.ToString(Session["sCompID"]));

                if (dsExp.Tables[0].Rows.Count > 0)
                {
                    this.gvDomClaim.DataSource = dsExp.Tables[0];
                    this.gvDomClaim.DataBind();
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

        protected void gvDomClaim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                
                Label lblEmpAId = (Label)e.Row.FindControl("lblEmpAId");
                Label txtClmAmt = (Label)e.Row.FindControl("txtClmAmt");
                Label txtClmType = (Label)e.Row.FindControl("txtClmType");
                Label txtAppAmtr = (Label)e.Row.FindControl("txtAppAmtr");
                Label EntertainmentChked = (Label)e.Row.FindControl("EntertainmentChked");
                Label lblEmpName = (Label)e.Row.FindControl("lblEmpName");
                Label lblEmpCodes = (Label)e.Row.FindControl("lblEmpCodes");
                LinkButton lnkDOMClmNoClmNo = (LinkButton)e.Row.FindControl("lnkDOMClmNoClmNo");
                DataSet ds = new DataSet();
                ds = objExp.GetDOMLimit(lblEmpAId.Text, lnkDOMClmNoClmNo.Text, Convert.ToString(Session["sCompID"]));

                if (txtClmType.Text == "SEQUEL")
                {
                    if (ds.Tables[0].Rows[0]["limit_amount"].ToString() == "0")
                    {
                        lblMessage.Text = "Limit Not Found.";
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                    }
                    else if (ds.Tables[0].Rows[0]["limit_amount"].ToString() != "")
                    {
                        if (decimal.Parse(txtClmAmt.Text.Trim()) > decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                        {
                            //TableCell cell = e.Row.Cells[4];
                            //cell.BackColor = System.Drawing.Color.DeepSkyBlue;
                        }
                        else if (decimal.Parse(txtClmAmt.Text.Trim()) < decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                        {

                        }
                    }
                    else
                    {

                    }
                }
                else
                {
                    if (EntertainmentChked.Text == "T")
                    {
                        if (txtAppAmtr.Text != "")
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
                                    //TableCell cell = e.Row.Cells[4];
                                    //cell.BackColor = System.Drawing.Color.Red;
                                }
                                else if (decimal.Parse(txtAppAmtr.Text.Trim()) < decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                                {
                                    //TableCell cell = e.Row.Cells[4];
                                    //cell.BackColor = System.Drawing.Color.Orange;
                                }
                            }
                            else
                            {
                                //TableCell cell = e.Row.Cells[4];
                                //cell.BackColor = System.Drawing.Color.Orange;
                            }
                        }
                        else
                        {

                        }
                    }

                    else if (EntertainmentChked.Text == "F")
                    {
                        if (txtAppAmtr.Text != "")
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
                                    //TableCell cell = e.Row.Cells[4];
                                    //cell.BackColor = System.Drawing.Color.Yellow;
                                }
                                else if (decimal.Parse(txtAppAmtr.Text.Trim()) < decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                                {
                                    //TableCell cell = e.Row.Cells[4];
                                    //cell.BackColor = System.Drawing.Color.LightGreen;
                                }
                            }
                            else
                            {
                                //TableCell cell = e.Row.Cells[4];
                                //cell.BackColor = System.Drawing.Color.LightGreen;
                            }
                        }
                        else
                        {

                        }
                    }

                }

                

            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            getDomesticData();
            divFrom.Visible = false;
            SectionList.Visible = true;
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
                string savePath = "";


                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Domestic\\" + lblEmpAId + "\\" + entryCode + "\\";

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
                divAlert.Visible = true;
                lblMessage.Visible = true;
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

        private void DisplayDocumentsEnter(string entryCode, string lblEmpAId)
        {
            try
            {
                CreateDocumentsStructureEnter();

                string prefix = "";
                string savePath = "";


                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Domestic\\Entertainment\\" + lblEmpAId + "\\" + entryCode + "\\";

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

            divAlert.Visible = false;
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
            LinkButton lnkDOMClmNo = (LinkButton)sender;

            string entryAid = ((LinkButton)lnkDOMClmNo.NamingContainer.FindControl("lnkDOMClmNoClmNo")).Text;
            string txtClmType = ((Label)lnkDOMClmNo.NamingContainer.FindControl("txtClmType")).Text;
            string lblEmpAId = ((Label)lnkDOMClmNo.NamingContainer.FindControl("lblEmpCodes")).Text;
            SectionList.Visible = false;
            Session["EntryAid"] = entryAid;
            Session["ClmType"] = txtClmType;
            objExp.AppNo = entryAid;
            dsExp = objExp.getDomClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));

            if (dsExp.Tables[0].Rows[0]["status"].ToString() == "9")
            {
                if (dsExp.Tables[0].Rows[0]["TravelType"].ToString() == "Travel")
                {
                    radiaoEntertainment1.Visible = false;
                }
                else
                {
                    radiaoEntertainment1.Visible = true;
                }

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
                string categoryType = dsExp.Tables[0].Rows[0]["CategoryType"].ToString();
                string radioEntertain = dsExp.Tables[0].Rows[0]["EntertainmentChked"].ToString();
               
              
                if (radioEntertain == "T")
                {
                    radiaoEntertainment1.Checked = true;

                }
                else
                {
                    radiaoEntertainment1.Checked = false;
                }
                drpType.SelectedValue = dsExp.Tables[0].Rows[0]["TravelType"].ToString();
                string TravelClass = dsExp.Tables[0].Rows[0]["TravelClass"].ToString();
                if (dsExp.Tables[0].Rows[0]["TravelType"].ToString() == "Entertainment")
                {
                    divFrom.Visible = true;
                    divEntertainment.Visible = true;
                }
                else if (dsExp.Tables[0].Rows[0]["TravelType"].ToString() == "Travel + Entertainment")
                {
                    divFrom.Visible = true;
                    divEntertainment.Visible = true;
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
                    DisplayDocumentsEnter(entryAid, lblEmpAId);


                    //divEntertainment.Visible = true;
                    //DisplayDocumentsEnter(entryAid);

                }
                else if (dsExp.Tables[0].Rows[0]["TravelType"].ToString() == "Travel + Entertainment")
                {
                    divUpload.Visible = true;
                    divfileUpload.Visible = false;
                    divfileDisplay.Visible = true;
                    //Upload_Click(entryAid);
                    DisplayDocuments(entryAid, lblEmpAId);

                    divfileUploadEnter.Visible = false;
                    divEntertainment.Visible = true;
                    divUploadEnter.Visible = true;
                    divfileDisplayEnter.Visible = true;
                    //UploadEnter_Click(entryAid);
                    DisplayDocumentsEnter(entryAid, lblEmpAId);



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
                    DisplayDocuments(entryAid, lblEmpAId);


                    //divFrom.Visible = true;
                    //DisplayDocuments(entryAid);

                }

                if (TravelClass != " ")
                {

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
                //txtTravelAmt.Text = dsExp.Tables[0].Rows[0]["TravelAmt"].ToString();
                object travelValue = dsExp.Tables[0].Rows[0]["TravelAmt"];
                txtTravelAmt.Text = (travelValue != null && !string.IsNullOrEmpty(travelValue.ToString())) ? travelValue.ToString() : "0";
                object traveladvValue = dsExp.Tables[0].Rows[0]["ADVANCE_AMT"];
                txtadvance.Text = (traveladvValue != null && !string.IsNullOrEmpty(traveladvValue.ToString())) ? traveladvValue.ToString() : "0";

                txtNoDays.Text = dsExp.Tables[0].Rows[0]["NoDays"].ToString();
                //txtEntAmount.Text = dsExp.Tables[0].Rows[0]["Entertainment"].ToString();
                object entertainmentValue = dsExp.Tables[0].Rows[0]["Entertainment"];
                txtEntAmount.Text = (entertainmentValue != null && !string.IsNullOrEmpty(entertainmentValue.ToString())) ? entertainmentValue.ToString() : "0";

                txtUserEligiRemark.Text = dsExp.Tables[0].Rows[0]["EntertainmmentRmk"].ToString();
                txtEnterDesc.Text = dsExp.Tables[0].Rows[0]["Entertainment_DESC"].ToString();
                txtApprovedAmt.Text = dsExp.Tables[0].Rows[0]["ApprovedAmmount"].ToString();
                txtUserTravelRemarks.Text = dsExp.Tables[0].Rows[0]["TravelRmk"].ToString();
                txtdiscript.Text = dsExp.Tables[0].Rows[0]["DESCRIPTION_EXP"].ToString();
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


                DataSet dsExp1 = new DataSet();
                dsExp1 = objExp.GetMetroReimb(Convert.ToString(Session["sCompID"]), categoryType);
                //double totalClaimAmt = (Convert.ToDouble(txtTravelAmt.Text) + Convert.ToDouble(txtEntAmount.Text));



                ViewState["totalClaimAmt"] = totalClaimAmt;
                txtligibility.Enabled = false;
                if (MetroType == "T0000001")
                {
                    radioPrimary1.Checked = true;
                    divTravel.Visible = true;
                    divMetro.Visible = true;
                    divAmt.Visible = true;
                    divEligi.Visible = true;
                    div1.Visible = true;
                    divfileDisplay.Visible = true;

                    if (dsExp1.Tables.Count > 0)
                    {
                        grMetro.DataSource = dsExp1.Tables[0];
                        grMetro.DataBind();
                        double noOfDays = Convert.ToDouble(txtNoDays.Text);

                        foreach (GridViewRow row in grMetro.Rows)
                        {
                            Label lblLodging = (Label)row.FindControl("lblLodging");
                            Label lblBoarding = (Label)row.FindControl("lblBoarding");
                            Label lblMiscellaneous = (Label)row.FindControl("lblMiscellaneous");
                            double eligibAmt = (Convert.ToDouble(lblLodging.Text) + Convert.ToDouble(lblBoarding.Text) + Convert.ToDouble(lblMiscellaneous.Text)) * (noOfDays);

                            //txtligibility.Text = eligibAmt.ToString();
                            txtligibility.Text = eligibAmt.ToString();
                            elgAmount.Text = eligibAmt.ToString();
                            ViewState["eligibAmt"] = eligibAmt;
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
                    div1.Visible = true;
                    divfileDisplay.Visible = true;
                    double noOfDays = Convert.ToDouble(txtNoDays.Text);
                    if (dsExp1.Tables.Count > 0)
                    {
                        grNonMetro.DataSource = dsExp1.Tables[0];
                        grNonMetro.DataBind();
                        foreach (GridViewRow row in grNonMetro.Rows)
                        {
                            Label lblNonLodging = (Label)row.FindControl("lblNonLodging");
                            Label lblNonBoarding = (Label)row.FindControl("lblNonBoarding");
                            Label lblNonMiscellaneous = (Label)row.FindControl("lblNonMiscellaneous");
                            double eligibAmt = (Convert.ToDouble(lblNonLodging.Text) + Convert.ToDouble(lblNonBoarding.Text) + Convert.ToDouble(lblNonMiscellaneous.Text)) * (noOfDays);
                            txtligibility.Text = eligibAmt.ToString();
                            elgAmount.Text = eligibAmt.ToString();
                            ViewState["eligibAmt"] = eligibAmt;
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

                CalculatetotalDom();


            }

        }

        protected void gvDomClaim_PreRender(object sender, EventArgs e)
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

            //claimAmount.Text = claimAmt.ToString();
            txtadvamt.Text = txtadvance.Text;
            txtTotalexp.Text = totalExpenses.ToString();

            //decimal totalExpenses = chk1 + chk2 + chk3 + chk4 + chk5 + chk6 + chk7 + chk8 + travelAmt + EntAmount;

            //txtadvamt.Text = txtadvance.Text;
            //txtTotalexp.Text = totalExpenses.ToString();
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



    }
}