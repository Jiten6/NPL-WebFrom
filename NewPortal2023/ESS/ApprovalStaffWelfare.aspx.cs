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
    public partial class ApprovalStaffWelfare : System.Web.UI.Page
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
            if (Session["sCompID"] != null)
            {
                if (!Page.IsPostBack)
                {
                    ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

                    //getstaffwelfareData();
                    //welfaretype.Visible = true;
                    divFrom.Visible = false;
                    //Div9.Visible = false;
                    Domastic.Visible = true;
                    divSqlNote.Visible = false;
                    divNote.Visible = false;
                    div1.Visible = false;
                    Divdom.Visible = false;
                    Divlocal.Visible = false;
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
                //divdom.Visible = true;
                divSqlNote.Visible = true;
                divNote.Visible = true;
                div1.Visible = true;
                Divdom.Visible = true;
                divFrom.Visible = false;
                // welfaretype.Visible = false;
                getstaffwelfareData();
                //traveltype.Visible = false;
                //btnAddNew.Visible = false;
                divTravel.Visible = false;
                divEntertainment.Visible = false;
                divUploadEnter.Visible = false;
                divfileUploadEnter.Visible = false;
                //Class_Travel.Visible = false;
                //Section1.Visible = false;
                //DivAction.Visible = false;
                Section2.Visible = false;
            }
            else if (drpTypeexpense.SelectedValue == "Local")
            {
                //Div9.Visible = true;
                Section3.Visible = false;
                Divlocal.Visible = true;
                //btnAddLOCNew.Visible = false;
                divSqlNote.Visible = false;
                divNote.Visible = false;
                div1.Visible = false;
                Divdom.Visible = false;
                //divdom.Visible = false;
                // welfaretype.Visible = false;
                Session["Entry_aid"] = null;

                getLocalwlfareData();

                //getCategoryType();
                //divAlert.Visible = false;
                //traveltype.Visible = false;
                Section2.Visible = false;
                getLocalReimb();

            }
            else
            {
                //welfaretype.Visible = true;
                divFrom.Visible = false;
                //Div9.Visible = false;

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
                string emp_aid = Session["sEmpID"].ToString();
                //if ((string)(Session["sEmpID"]) == "ABCD1234")
                //{
                dsExp = objExp.GetSeqApproverDomesticwelfareList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                //}
                //else if ((string)(Session["sEmpID"]) == "ABCD1234")
                //{
                //    dsExp = objExp.GetSeqApproverDomesticList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                //}(string)(Session["EntryAid"])
                //else if ((string)(Session["sEmpID"]) == "ABCD1234")
                //{
                //    dsExp = objExp.GetSeqApproverDomesticList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                //}
                if (dsExp.Tables[0].Rows.Count > 0)
                {
                    Session["ClmType"] = dsExp.Tables[0].Rows[0]["Status"].ToString();
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
                        divNote.Visible = false;
                        divSqlNote.Visible = true;
                    }
                    else
                    {
                        txtApprovedAmt.Enabled = false;
                        divSqlNote.Visible = false;
                        divNote.Visible = true;
                    }
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
                DropDownList ddls = (DropDownList)e.Row.FindControl("drpAction");
                Label lblEmpAId = (Label)e.Row.FindControl("lblEmpAId");
                Label TravelAmt = (Label)e.Row.FindControl("txtClmAmt");
                Label txtClmType = (Label)e.Row.FindControl("txtClmType");
                Label txtAppAmtr = (Label)e.Row.FindControl("txtAppAmtr");
                Label EntertainmentChked = (Label)e.Row.FindControl("EntertainmentChked");
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
                        if (decimal.Parse(TravelAmt.Text.Trim()) > decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                        {
                            TableCell cell = e.Row.Cells[4];
                            cell.BackColor = System.Drawing.Color.DeepSkyBlue;
                        }
                        else if (decimal.Parse(TravelAmt.Text.Trim()) < decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
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
                                    TableCell cell = e.Row.Cells[4];
                                    cell.BackColor = System.Drawing.Color.Red;
                                }
                                else if (decimal.Parse(txtAppAmtr.Text.Trim()) < decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                                {
                                    TableCell cell = e.Row.Cells[4];
                                    cell.BackColor = System.Drawing.Color.Orange;
                                }
                            }
                            else
                            {
                                TableCell cell = e.Row.Cells[4];
                                cell.BackColor = System.Drawing.Color.Orange;
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
                                    TableCell cell = e.Row.Cells[4];
                                    cell.BackColor = System.Drawing.Color.Yellow;
                                }
                                else if (decimal.Parse(txtAppAmtr.Text.Trim()) < decimal.Parse(ds.Tables[0].Rows[0]["limit_amount"].ToString()))
                                {
                                    TableCell cell = e.Row.Cells[4];
                                    cell.BackColor = System.Drawing.Color.LightGreen;
                                }
                            }
                            else
                            {
                                TableCell cell = e.Row.Cells[4];
                                cell.BackColor = System.Drawing.Color.LightGreen;
                            }
                        }
                        else
                        {

                        }
                    }

                }

                if (Session["ClmType"].ToString() == "CFO")
                {
                    ddls.Items.Insert(3, new ListItem("Revert", "Revert"));
                }

            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            getstaffwelfareData();
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
                btnApprove.Visible = true;
                drpActionType.SelectedValue = "";
                drpActionType.Enabled = true;
                txtRmk.Text = "";
                txtRmk.Enabled = true;
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
                            if (dsExp.Tables[0].Rows[0]["Claim1_Amount"].ToString() != "0.0000")
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
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Air (Economy Class)")
                        {
                            if (dsExp.Tables[0].Rows[0]["Claim2_Amount"].ToString() != "0.0000")
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
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Rail (AC 1st Class)")
                        {
                            if (dsExp.Tables[0].Rows[0]["Claim3_Amount"].ToString() != "0.0000")
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
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Rail (AC 2nd Class)")
                        {
                            if (dsExp.Tables[0].Rows[0]["Claim4_Amount"].ToString() != "0.0000")
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
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Rail (AC 3rd Class)")
                        {
                            if (dsExp.Tables[0].Rows[0]["Claim5_Amount"].ToString() != "0.0000")
                            {
                                chk5.Checked = true;
                                txtchk5.Visible = true;
                                txtchk5.Text = dsExp.Tables[0].Rows[0]["Claim5_Amount"].ToString();
                            }
                            else
                            {
                                chk5.Checked = false;
                                txtchk5.Visible = false;

                            }
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Rail(AC Chair Car)")
                        {
                            if (dsExp.Tables[0].Rows[0]["Claim6_Amount"].ToString() != "0.0000")
                            {
                                chk6.Checked = true;
                                txtchk6.Visible = true;
                                txtchk6.Text = dsExp.Tables[0].Rows[0]["Claim6_Amount"].ToString();
                            }
                            else
                            {
                                chk6.Checked = false;
                                txtchk6.Visible = false;

                            }
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Bus")
                        {
                            if (dsExp.Tables[0].Rows[0]["Claim7_Amount"].ToString() != "0.0000")
                            {
                                chk7.Checked = true;
                                txtchk7.Visible = true;
                                txtchk7.Text = dsExp.Tables[0].Rows[0]["Claim7_Amount"].ToString();
                            }
                            else
                            {
                                chk7.Checked = false;
                                txtchk7.Visible = false;

                            }
                        }
                        else if (dsExp.Tables[1].Rows[i]["TARVEL_CLASS"].ToString() == "Others")
                        {
                            if (dsExp.Tables[0].Rows[0]["Claim8_Amount"].ToString() != "0.0000")
                            {
                                chk8.Checked = true;
                                txtchk8.Visible = true;
                                txtchk8.Text = dsExp.Tables[0].Rows[0]["Claim8_Amount"].ToString();
                            }
                            else
                            {
                                chk8.Checked = false;
                                txtchk8.Visible = false;

                            }
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



                dsExp = objExp.GetMetroReimb(Convert.ToString(Session["sCompID"]), categoryType);
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
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            if (drpActionType.SelectedValue == "")
            {
                getstaffwelfareData();
                divAlert.Visible = true;
                lblMessage.Text = "Please Select Action Type.";
                divAlert.Visible = true;
                lblMessage.Visible = true;

            }
            else
            {
                string drp = drpActionType.SelectedValue;
                string Rmk = txtRmk.Text;
                ActionGet(drp, Rmk);
                //getstaffwelfareData();
                divAlert.Visible = true;
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Action Send Successfully.";
                string script = $@"<script type='text/javascript'>alert('Action Send Successfully.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                drpActionType.Enabled = false;
                txtRmk.Enabled = false;
                btnApprove.Visible = false;
                getstaffwelfareData();
                divFrom.Visible = false;
                SectionList.Visible = true;

            }
        }

        private void ActionGet(string drp, string Rmk)
        {

            //string radiochkent = "";
            string currentstatus = "";
            string status = "";
            //if (radiaoEntertainment1.Checked == true)
            //{
            //    radiochkent = "T";
            //}
            //else
            //{
            //    radiochkent = "F";
            //}

            dsExp = objExp.GetChkStatuswelfare(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), (string)(Session["EntryAid"]));
            string radiochkent = dsExp.Tables[0].Rows[0]["EntertainmentChked"].ToString();

            if (Session["ClmType"].ToString() == "SEQUEL")
            {
                if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "9" || dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0" || dsExp.Tables[0].Rows[0]["Status"].ToString() == "2")
                {
                    currentstatus = "9";
                    Session["ClmType"] = "SEQUEL";

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

                }

                //else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "9" && dsExp.Tables[0].Rows[0]["Reverts"].ToString() != "NULL")
                //{
                //    status = "Revert";
                //}
                //else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0" && dsExp.Tables[0].Rows[0]["Reverts"].ToString() == "")
                //{
                //    if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0")
                //    {
                //        status = "Reject";
                //    }
                //    else
                //    {
                //        status = "New";
                //    }

                //}
                //else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0" && dsExp.Tables[0].Rows[0]["Reverts"].ToString() != "NULL")
                //{
                //    if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0")
                //    {
                //        status = "Reject";
                //    }
                //    else
                //    {
                //        status = "New";
                //    }

                //}

                ///
                //////////////////////////////////////////////////////////////////////////
                ///

                int cnt = 0;
                if (status == "New")
                {

                    if (drp == "Approve")
                    {
                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        objExp.TotalAmmount = txtApprovedAmt.Text;
                        objExp.Status = "3";
                        radiaoEntertainment1.Enabled = true;
                        if (radiaoEntertainment1.Checked == true)
                        {

                            //objExp.TotalAmmount = txtApprovedAmt.Text;
                            //objExp.Status = "4";
                            radiochkent = "T";
                        }
                        else
                        {

                            //objExp.TotalAmmount = txtApprovedAmt.Text;
                            //objExp.Status = "3";
                            radiochkent = "F";
                        }
                    }
                    else if (drp == "Reject")
                    {
                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        //radiochkent = "F";
                        objExp.Status = "0";

                    }
                }
                else if (status == "Reject")
                {
                    if (drp == "Approve")
                    {
                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        objExp.TotalAmmount = txtApprovedAmt.Text;
                        objExp.Status = "3";

                        //if (radiaoEntertainment1.Checked == true)
                        //{
                        //    objExp.Status = "4";
                        //    //radiochkent = "T";
                        //}
                        //else
                        //{
                        //    objExp.Status = "3";
                        //    //radiochkent = "F";
                        //}
                    }
                    else if (drp == "Reject")
                    {
                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        objExp.TotalAmmount = txtApprovedAmt.Text;
                        //radiochkent = "F";
                        objExp.Status = "0";
                    }
                }
                else if (status == "Revert")
                {
                    if (drp == "Approve")
                    {

                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        objExp.TotalAmmount = txtApprovedAmt.Text;
                        objExp.Revert = "";
                        objExp.Status = "5";
                    }
                    else if (drp == "Reject")
                    {
                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        objExp.TotalAmmount = txtApprovedAmt.Text;
                        objExp.Revert = "";
                        objExp.Status = "0";
                    }
                }
            }
            if (Session["ClmType"].ToString() == "HOD")
            {
                radiaoEntertainment1.Enabled = false;
                if (drp == "Approve")
                {
                    if (radiochkent == "T")
                    {
                        if (Convert.ToDecimal(ViewState["totalClaimAmt"]) > Convert.ToDecimal(ViewState["eligibAmt"]))
                        {

                            objExp.Action = drp;
                            objExp.CheckerRemark = Rmk;
                            //radiochkent = "F";
                            objExp.Status = "4";

                        }
                        else
                        {
                            objExp.Action = drp;
                            objExp.CheckerRemark = Rmk;
                            //radiochkent = "F";
                            objExp.Status = "4";
                        }
                    }
                    else if (radiochkent == "F")
                    {

                        if (Convert.ToDecimal(ViewState["totalClaimAmt"]) > Convert.ToDecimal(ViewState["eligibAmt"]))
                        {

                            objExp.Action = drp;
                            objExp.CheckerRemark = Rmk;
                            //radiochkent = "F";
                            objExp.Status = "4";

                        }
                        else
                        {
                            objExp.Action = drp;
                            objExp.CheckerRemark = Rmk;
                            //radiochkent = "F";
                            objExp.Status = "5";
                        }
                    }


                }
                else if (drp == "Reject")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    //radiochkent = "F";
                    objExp.Status = "-0";

                }
            }
            if (Session["ClmType"].ToString() == "CEO")
            {
                radiaoEntertainment1.Enabled = false;
                if (drp == "Approve")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    // radiochkent = "F";
                    objExp.Status = "5";


                }
                else if (drp == "Reject")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    //radiochkent = "F";
                    objExp.Status = "-0";

                }
            }
            if (Session["ClmType"].ToString() == "CFO")
            {
                radiaoEntertainment1.Enabled = false;
                if (drp == "Approve")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    // radiochkent = "F";
                    objExp.Status = "1";

                }
                else if (drp == "Reject")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    //radiochkent = "F";
                    objExp.Status = "-0";

                }
                else if (drp == "Revert")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    //radiochkent = "F";
                    objExp.Status = "2";
                    objExp.Revert = "-1";

                }
            }

            if (drp != "Revert")
            {
                objExp.Revert = "";
            }


            dsExp = objExp.InsertwelfareStatus(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), (string)(Session["EntryAid"]), (string)Session["ClmType"], radiochkent);

            SENDUDATEMAIL((string)(Session["EntryAid"]), Session["ClmType"].ToString(), drp, radiochkent, txtFromDate.Text);


        }
        private void SENDUDATEMAIL(string ClaimNO, string Approver, string Action, string radiochkent, string fromdate)
        {
            try
            {
                emailSend = new NewPortal2023.ESS.Email();
                DataSet dsmkkMail = new DataSet();
                DateTime date = DateTime.Now;

                if (Action == "Approve")
                {
                    dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], (string)Session["sEmpID"], ClaimNO, Approver, radiochkent);


                    if (dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() != "")
                    {
                        string clientbodys = "Dear " + dsmkkMail.Tables[2].Rows[0]["APPROVARNAME"].ToString() + ",<br><br> Domestic Travel Expense is received for approval." + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + " claim is Approved by the " + Approver
                        + ".Kindly take the action for the same through logging-in into ESS portal.<br>"
                       + " <br>Claim Type:- Domestic Travel Expense"
                             + "<br>Claim No :- " + ClaimNO
                             + "<br>Travel Date :- " + fromdate
                             + "<br><br>ThankYou.<br><br>";
                        //string emails = dsmkkMail.Tables[2].Rows[0]["APPROVARMAIL"].ToString();
                        //string emailsCC = dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() + ',' + dsmkkMail.Tables[1].Rows[0]["EMP_EMAIL"].ToString();
                        string emails = "techsupport@sequelgroup.co.in";
                        string emailsCC = "techsupport@sequelgroup.co.in";
                        string subjects = "Do Not Reply: Domestic Travel Expense";
                        emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);

                    }
                }
                else if (Action == "Reject")
                {
                    dsmkkMail = emailSend.GetEmpRejectMAil((string)Session["sCompID"], (string)Session["sEmpID"], ClaimNO, Approver, radiochkent);

                    string clientbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>Domestic Travel Expense is rejected by the " + dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString()
                       + ".Kindly check the claim for the same through logging-in into ESS portal.<br>"
                      + " <br>Claim Type:- Domestic Travel Expense"
                            + "<br>Claim No :- " + ClaimNO
                            + "<br>Travel Date :- " + fromdate
                            + "<br><br>ThankYou.<br><br>";
                    //string emails = dsmkkMail.Tables[1].Rows[0]["EMP_EMAIL"].ToString();
                    //string emailsCC = dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString();
                    string emails = "techsupport@sequelgroup.co.in";
                    string emailsCC = "payrollservices@sequelgroup.co.in";
                    string subjects = "Do Not Reply: Domestic Travel Expense";
                    emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkBtnSubmit_Click(object sender, EventArgs e)
        {
            LinkButton linkButton = (LinkButton)sender;
            string drpAction = ((DropDownList)linkButton.NamingContainer.FindControl("drpAction")).SelectedValue;
            DropDownList drpGiftCard = (DropDownList)linkButton.NamingContainer.FindControl("drpAction");
            GridViewRow clickedRow = ((LinkButton)sender).NamingContainer as GridViewRow;
            ////DropDownList drpAction = (linkButton.FindControl("drpAction") as DropDownList);
            //DropDownList drpActions = (DropDownList)clickedRow.FindControl("drpAction");
            string drpAction1 = ((DropDownList)clickedRow.FindControl("drpAction")).SelectedValue;
            string drpActionType = drpGiftCard.SelectedValue;

            TextBox txtRmk = (linkButton.FindControl("txtRmk") as TextBox);

            //string drps = drpAction.SelectedValue;

            string entryAid = ((LinkButton)linkButton.NamingContainer.FindControl("lnkDOMClmNoClmNo")).Text;
            string txtClmType = ((Label)linkButton.NamingContainer.FindControl("txtClmType")).Text;

            Session["EntryAid"] = entryAid;
            Session["ClmType"] = txtClmType;

            if (drpActionType == "")
            {
                getstaffwelfareData();
                divAlert.Visible = true;
                lblMessage.Text = "Please Select Action Type.";
                divAlert.Visible = true;
                lblMessage.Visible = true;


            }
            else
            {
                string drp = drpActionType;
                string Rmk = txtRmk.Text;
                ActionGet(drp, Rmk);
                getstaffwelfareData();
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Claim Submit Successfully.";
            }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chckheader = (CheckBox)gvDomClaim.HeaderRow.FindControl("chkSelectAll");
            foreach (GridViewRow row in gvDomClaim.Rows)
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
            try
            {

                if (drActionAll.SelectedValue == "")
                {
                    //getstaffwelfareData();
                    divAlert.Visible = true;
                    lblMessage.Text = "Please Select Action Type.";
                    divAlert.Visible = true;
                    lblMessage.Visible = true;

                }
                else
                {
                    string drp = drActionAll.SelectedValue;
                    string Rmk = txtAllRmk.Text;
                    objExp.UpdatewelfareFinGlSubmit(MakeChkXml(gvDomClaim), drActionAll.SelectedValue, txtAllRmk.Text, (string)Session["ClmType"]);
                    MakeChkForEmailXml(gvDomClaim);
                    //ActionGetAll(drp, Rmk);
                    getstaffwelfareData();
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Action Send Successfully.";
                    string script = $@"<script type='text/javascript'>alert('Action Send Successfully.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);


                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = ex.Message;
            }


        }

        private void ActionGetAll(string drp, string Rmk)
        {

            string radiochkent = "";
            string currentstatus = "";
            string status = "";
            if (radiaoEntertainment1.Checked == true)
            {
                radiochkent = "T";
            }
            else
            {
                radiochkent = "F";
            }
            dsExp = objExp.GetChkStatuswelfare(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), (string)(Session["EntryAid"]));
            //if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "9")
            //{
            //    currentstatus = "9";
            //}
            //else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "3")
            //{
            //    currentstatus = "3";
            //}
            //else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "4")
            //{
            //    currentstatus = "4";
            //}
            //else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "5")
            //{
            //    currentstatus = "5";
            //}
            if (Session["ClmType"].ToString() == "SEQUEL")
            {
                if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "9" && dsExp.Tables[0].Rows[0]["Reverts"].ToString() == "")
                {
                    if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "0")
                    {
                        status = "Reject";
                    }
                    else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0")
                    {
                        status = "Revert";
                    }
                    else
                    {
                        status = "New";
                    }

                }
                else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "2" && dsExp.Tables[0].Rows[0]["Reverts"].ToString() != "NULL")
                {
                    status = "Revert";
                }
                else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0" && dsExp.Tables[0].Rows[0]["Reverts"].ToString() == "")
                {
                    if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0")
                    {
                        status = "Reject";
                    }
                    else
                    {
                        status = "New";
                    }

                }
                else if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0" && dsExp.Tables[0].Rows[0]["Reverts"].ToString() != "NULL")
                {
                    if (dsExp.Tables[0].Rows[0]["Status"].ToString() == "-0")
                    {
                        status = "Reject";
                    }
                    else
                    {
                        status = "New";
                    }

                }

                ///
                //////////////////////////////////////////////////////////////////////////
                ///

                int cnt = 0;
                if (status == "New")
                {
                    if (drp == "Approve")
                    {
                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        if (radiaoEntertainment1.Checked == true)
                        {
                            objExp.Status = "4";
                            //radiochkent = "T";
                        }
                        else
                        {
                            objExp.Status = "3";
                            //radiochkent = "F";
                        }
                    }
                    else if (drp == "Reject")
                    {
                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        //radiochkent = "F";
                        objExp.Status = "0";

                    }
                }
                else if (status == "Reject")
                {
                    if (drp == "Approve")
                    {
                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        if (radiaoEntertainment1.Checked == true)
                        {
                            objExp.Status = "4";
                            //radiochkent = "T";
                        }
                        else
                        {
                            objExp.Status = "3";
                            //radiochkent = "F";
                        }
                    }
                    else if (drp == "Reject")
                    {
                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        //radiochkent = "F";
                        objExp.Status = "0";
                    }
                }
                else if (status == "Revert")
                {
                    if (drp == "Approve")
                    {

                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        //radiochkent = "T";
                        objExp.Status = "5";
                    }
                    else if (drp == "Reject")
                    {
                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        //radiochkent = "F";
                        objExp.Status = "0";
                    }
                }
                else
                {
                    if (drp == "Approve")
                    {

                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        //radiochkent = "T";
                        objExp.Status = "5";
                    }
                    else if (drp == "Reject")
                    {
                        objExp.Action = drp;
                        objExp.CheckerRemark = Rmk;
                        //radiochkent = "F";
                        objExp.Status = "0";
                    }
                }
            }
            if (Session["ClmType"].ToString() == "HOD")
            {
                if (drp == "Approve")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    //radiochkent = "F";
                    objExp.Status = "5";


                }
                else if (drp == "Reject")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    //radiochkent = "F";
                    objExp.Status = "-0";

                }
            }
            if (Session["ClmType"].ToString() == "CEO")
            {
                if (drp == "Approve")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    // radiochkent = "F";
                    objExp.Status = "5";


                }
                else if (drp == "Reject")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    //radiochkent = "F";
                    objExp.Status = "-0";

                }
            }
            if (Session["ClmType"].ToString() == "CFO")
            {
                if (drp == "Approve")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    // radiochkent = "F";
                    objExp.Status = "1";

                }
                else if (drp == "Reject")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    //radiochkent = "F";
                    objExp.Status = "-0";

                }
                else if (drp == "Revert")
                {

                    objExp.Action = drp;
                    objExp.CheckerRemark = Rmk;
                    //radiochkent = "F";
                    objExp.Status = "9";
                    //objExp.Revert = "-1";

                }
            }
            objExp.UpdatewelfareFinGlSubmit(MakeChkXml(gvDomClaim), drActionAll.SelectedValue, txtAllRmk.Text, (string)Session["ClmType"]);


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
                    sbChkGlCode.Append("<CHKGLSUMBIT CODE='" + ((LinkButton)gvr.FindControl("lnkDOMClmNoClmNo")).Text.Trim() + "'/>");

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
                string radiochkent = "";
                string Action = string.Empty;
                string fromdate = string.Empty;
                string Approver = string.Empty;
                string ClaimNo = string.Empty;

                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {

                    ClaimNo = ((LinkButton)gvr.FindControl("lnkDOMClmNoClmNo")).Text.Trim();
                    fromdate = ((Label)gvr.FindControl("txFrDate")).Text.Trim();
                    Approver = ((Label)gvr.FindControl("txtClmType")).Text.Trim();
                    Action = drActionAll.SelectedValue;

                    SENDUDATEMAIL(ClaimNo, Approver, Action, radiochkent, fromdate);
                }

            }


            return xmlChkGlCode;
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

        protected void gvDomClaim_RowCommand(object sender, GridViewCommandEventArgs e)
        {

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

        //// ------------- LOCAL STAFF WELFARE --------
        /// <summary>
        /// 
        /// 
        /// </summary>
        private void getLocalwlfareData()

        {
            try
            {
                getCategoryType();
                dsExp = objLoc.GetApproverLocalwelfareList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables[0].Rows.Count > 0)
                {
                    Session["ClmType"] = dsExp.Tables[0].Rows[0]["ClmType"].ToString();
                    if (Session["ClmType"].ToString() == "CFO")
                    {
                        drpactiontypeloc.Items.Insert(3, new ListItem("Revert", "Revert"));
                    }
                    if (Session["ClmType"].ToString() == "CFO")
                    {
                        drActionAll.Items.Insert(3, new ListItem("Revert", "Revert"));
                    }

                    if (Session["ClmType"].ToString() == "SEQUEL")
                    {
                        txtApprovedAmt.Enabled = true;
                        divNote.Visible = false;
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

        private void CreatelocDocumentsStructure()
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
        private void DisplaylocDocuments(string entryCode, string lblEmpAId)
        {
            try
            {
                CreatelocDocumentsStructure();

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

        protected void btnLocClose_Click(object sender, EventArgs e)
        {
            divFrom.Visible = false;
            divTravel.Visible = false;
            SectionList.Visible = true;
            getLocalwlfareData();
        }


        protected void lnkLOCClmNoClmNo_Click(object sender, EventArgs e)
        {
            LinkButton lnkDOMClmNo = (LinkButton)sender;

            string entryAid = ((LinkButton)lnkDOMClmNo.NamingContainer.FindControl("lnkLOCClmNoClmNo")).Text;
            string lblEmpAId = ((Label)lnkDOMClmNo.NamingContainer.FindControl("lblEmpCode")).Text;
            Session["EntryAid"] = entryAid;

            objLoc.AppNo = entryAid;
            dsExp = objLoc.getaPPROVELocalwelfareClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));

            if (dsExp.Tables[0].Rows[0]["status"].ToString() == "9")
            {
                radiaoEntertainmentloc1.Visible = true;
            }
            else
            {

                radiaoEntertainmentloc1.Visible = true;
                radiaoEntertainmentloc1.Enabled = false;
            }
            if (dsExp.Tables.Count > 0)
            {
                Section1.Visible = false;
                SectionList.Visible = false;
                divFrom.Visible = false;
                divlocalform.Visible = true;
                divUpload.Visible = true;
                divfileUpload.Visible = false;
                divfileDisplay.Visible = true;
                divTravel.Visible = true;
                btnApprove.Visible = true;
                drpactiontypeloc.SelectedValue = "";
                drpactiontypeloc.Enabled = true;
                txtRmk.Text = "";
                txtRmk.Enabled = true;

                string categoryType = dsExp.Tables[0].Rows[0]["Category_AId"].ToString();
                string desgAid = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();
                Session["CATEGORY_TYPE"] = categoryType;
                Session["DESG_AID"] = desgAid;
                if (dsExp.Tables[0].Rows[0]["Claim1_Amount"].ToString() != "0.0000")
                {
                    CheckBox1.Checked = true;
                    TextBox2.Visible = true;
                    TextBox2.Text = dsExp.Tables[0].Rows[0]["Claim1_Amount"].ToString();
                }
                else
                {
                    CheckBox1.Checked = false;
                    TextBox2.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["Claim2_Amount"].ToString() != "0.0000")
                {
                    CheckBox2.Checked = true;
                    TextBox3.Visible = true;
                    TextBox3.Text = dsExp.Tables[0].Rows[0]["Claim2_Amount"].ToString();
                }
                else
                {
                    CheckBox2.Checked = false;
                    TextBox3.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["Claim3_Amount"].ToString() != "0.0000")
                {
                    CheckBox3.Checked = true;
                    TextBox4.Visible = true;
                    TextBox4.Text = dsExp.Tables[0].Rows[0]["Claim3_Amount"].ToString();
                }
                else
                {
                    CheckBox3.Checked = false;
                    TextBox4.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["Claim4_Amount"].ToString() != "0.0000")
                {
                    CheckBox4.Checked = true;
                    TextBox5.Visible = true;
                    TextBox5.Text = dsExp.Tables[0].Rows[0]["Claim4_Amount"].ToString();
                }
                else
                {
                    CheckBox4.Checked = false;
                    TextBox5.Visible = false;

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
                    DisplaylocDocuments(entryAid, lblEmpAId);

                }
                else
                {
                    grLocalReimb.DataSource = null;
                    grLocalReimb.DataBind();
                }

            }

            Calculatetotalexp();
        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked == true)
            {
                TextBox2.Visible = true;
            }
        }

        protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox2.Checked == true)
            {
                TextBox3.Visible = true;
            }
        }

        protected void CheckBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox3.Checked == true)
            {
                TextBox4.Visible = true;
            }
        }

        protected void CheckBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox4.Checked == true)
            {
                TextBox5.Visible = true;
            }
        }

        protected void lnkBtnlocSubmit_Click(object sender, EventArgs e)
        {

        }
        protected void btnLocApprove_Click(object sender, EventArgs e)
        {
            if (drpactiontypeloc.SelectedValue == "")
            {
                getLocalwlfareData();
                divAlert.Visible = true;
                lblMessage.Text = "Please Select Action Type.";

            }
            else
            {
                string drp = drpactiontypeloc.SelectedValue;
                string Rmk = txtRmk.Text;
                ActionlocGet(drp, Rmk);
                //getLocalData();
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Action Send Successfully.";
                string script = $@"<script type='text/javascript'>alert('Action Send Successfully.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                drpactiontypeloc.Enabled = false;
                txtrmkloc.Enabled = false;
                btnLocApprove.Visible = false;
                getLocalwlfareData();
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
        private void ActionlocGet(string drp, string Rmk)
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
                        radiaoEntertainmentloc1.Enabled = true;
                        if (radiaoEntertainmentloc1.Checked == true)
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
                        if (radiaoEntertainmentloc1.Checked == true)
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
                    if (radiochkent == "T")
                    {
                        objLoc.Action = drp;
                        objLoc.CheckerRemark = Rmk;
                        objLoc.Status = "4";
                    }
                    else if (radiochkent == "F")
                    {
                        objLoc.Action = drp;
                        objLoc.CheckerRemark = Rmk;
                        objLoc.Status = "5";
                    }

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
                else if (drpactiontypeloc.SelectedValue == "Revert")
                {

                    objLoc.Action = drpactiontypeloc.SelectedValue;
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

            //SENDUDATEMAIL((string)(Session["EntryAid"]), Session["ClmType"].ToString(), drp, radiochkent, txtDate.Text);


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
        //            //string emails = dsmkkMail.Tables[2].Rows[0]["APPROVARMAIL"].ToString();
        //            //string emailsCC = dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() + ',' + dsmkkMail.Tables[1].Rows[0]["EMP_EMAIL"].ToString();
        //            string emails = "techsupport@sequelgroup.co.in";
        //            string emailsCC = "techsupport@sequelgroup.co.in";
        //            string subjects = "Do Not Reply: Domestic Travel Expense";
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
        //        //string emails = dsmkkMail.Tables[1].Rows[0]["EMP_EMAIL"].ToString();
        //        //string emailsCC = dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString();
        //        string emails = "techsupport@sequelgroup.co.in";
        //        string emailsCC = "payrollservices@sequelgroup.co.in";
        //        string subjects = "Do Not Reply: Domestic Travel Expense";
        //        emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);
        //    }
        //}

        protected void btnlocSubmitAll_Click(object sender, EventArgs e)
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
                objLoc.UpdateFinGlSubmit(MakelocChkXml(gvLocalClaimList), drActionAll.SelectedValue, txtAllRmk.Text);
                MakeLocChkForEmailXml(gvLocalClaimList);
                //ActionGetAll(drp, Rmk);
                getLocalwlfareData();
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Action Send Successfully.";
                string script = $@"<script type='text/javascript'>alert('Action Send Successfully.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);


            }
        }
        private string MakelocChkXml(GridView gvList)
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
        private string MakeLocChkForEmailXml(GridView gvList)
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

                    //SENDUDATEMAIL(ClaimNo, Approver, Action, radiochkent, fromdate);
                }

            }


            return xmlChkGlCode;
        }

        protected void chklocSelectAll_CheckedChanged(object sender, EventArgs e)
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
                                cell.BackColor = System.Drawing.Color.Orange;
                            }

                            else if (EntertainmentChked.Text == "F")
                            {

                            }
                        }
                    }



                    txtRmk.BackColor = (txtRmk.Text.Trim() == "1" ? System.Drawing.Color.LightGray : txtRmk.BackColor);
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
            decimal chk1 = string.IsNullOrEmpty(TextBox2.Text) ? 0 : decimal.Parse(TextBox2.Text);
            decimal chk2 = string.IsNullOrEmpty(TextBox3.Text) ? 0 : decimal.Parse(TextBox3.Text);
            decimal chk3 = string.IsNullOrEmpty(TextBox4.Text) ? 0 : decimal.Parse(TextBox4.Text);
            decimal chk4 = string.IsNullOrEmpty(TextBox5.Text) ? 0 : decimal.Parse(TextBox5.Text);
            decimal meal = string.IsNullOrEmpty(txtMeal.Text) ? 0 : decimal.Parse(txtMeal.Text);
            decimal otherExpenses = string.IsNullOrEmpty(txtOtherExpenses.Text) ? 0 : decimal.Parse(txtOtherExpenses.Text);
            decimal adv = string.IsNullOrEmpty(txtadv.Text) ? 0 : decimal.Parse(txtadv.Text);

            decimal totalExpenses = chk1 + chk2 + chk3 + chk4 + meal + otherExpenses;
            decimal paid = totalExpenses - adv;
            txtlocaltotal.Text = totalExpenses.ToString();
            txtpaidamt.Text = paid.ToString();
        }

        //protected void btnback_Click(object sender, EventArgs e)
        //{
        //    divFrom.Visible = false;
        //    Div9.Visible = false;
        //    Domastic.Visible = true;
        //    Section2.Visible = true;
        //    //traveltype.Visible = false;
        //    //divdom.Visible = false;
        //    div1.Visible = false;
        //    drpTypeexpense.SelectedValue = "";

        //}
        //protected void btnlocback_Click(object sender, EventArgs e)
        //{
        //    divFrom.Visible = false;
        //    Div9.Visible = false;
        //    SectionList.Visible = true;
        //    Domastic.Visible = true;
        //    Section2.Visible = true;
        //    Section3.Visible = false;
        //    //traveltype.Visible = false;
        //    //divdom.Visible = false;
        //    div1.Visible = false;
        //    divnotes.Visible = false;
        //    drpTypeexpense.SelectedValue = "";

        //}
    }
}
