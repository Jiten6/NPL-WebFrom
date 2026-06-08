using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class ApprovalHODKeyAccomploshments : System.Web.UI.Page
    {
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();

        DataSet dsInv = new DataSet();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["sCompID"] != null)
                {
                    if (!Page.IsPostBack)
                    {
                        FillFinYear();
                        //Fill_Details("1", gvList.PageSize.ToString());
                        mv.SetActiveView(vwListView);
                        //Fill_ACCOMPLISHMENTS();
                    }

                    //CreateBindAdditionTable();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void FillFinYear()
        {
            DataSet ds = new DataSet();

            ds = objAppraisal.GetYears((string)Session["sCompID"]);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PYDESC"].ToString() != "")
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(row["PYDESC"].ToString()))
                        {
                            drpFinancialYear.Items.Add(new ListItem(row["PYDESC"].ToString(), row["PYDESC"].ToString()));
                        }
                    }
                }
            }

            string currentFinancialYear = ds.Tables[1].Rows[0]["CYDESC"].ToString();
            string nextFinancialYear = ds.Tables[2].Rows[0]["NYDESC"].ToString();

            // Add values to the dropdown
            drpFinancialYear.Items.Add(new ListItem(currentFinancialYear, currentFinancialYear));
            drpFinancialYear.Items.Add(new ListItem(nextFinancialYear, nextFinancialYear));
        }

        protected void drpFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            if (drpFinancialYear.SelectedIndex == 0)
            {
                divKRAFin.Visible = false;
            }
            else
            {
                divKRAFin.Visible = true;
            }

            ds = objAppraisal.GetYears((string)Session["sCompID"]);

            string currentFinancialYear = ds.Tables[1].Rows[0]["CYDESC"].ToString();
            string nextFinancialYear = ds.Tables[2].Rows[0]["NYDESC"].ToString();

            if (drpFinancialYear.SelectedValue == currentFinancialYear)
            {
                lblCycle.Text = "Appraisal Cycle";
                ViewState["CYCLE"] = "Appraisal Cycle";
                ViewState["CYCLE_AID"] = "C1";
            }
            else if (drpFinancialYear.SelectedValue == nextFinancialYear)
            {
                lblCycle.Text = "KRA Cycle";
                ViewState["CYCLE"] = "KRA Cycle";
                ViewState["CYCLE_AID"] = "C0";
            }
            else
            {
                lblCycle.Text = "";
                ViewState["CYCLE"] = "";
                ViewState["CYCLE_AID"] = "";
            }

            ViewState["Year"] = drpFinancialYear.SelectedValue;

            Get_EmployeeType();
            GetActivationDateStatus();
            Fill_Details("1", gvList.PageSize.ToString());
            mv.SetActiveView(vwListView);
        }

        private void GetActivationDateStatus()
        {
            DataSet dsExp = new DataSet();

            objPmsHr.Year = ViewState["Year"].ToString();

            dsExp = objPmsHr.GetPMSEmpFlag((string)Session["sCompID"], (string)Session["sEmpID"]);

            if (dsExp.Tables[0].Rows.Count > 0)
            {
                if (dsExp.Tables[0].Rows[0]["KRA_FLAG_STATUS"].ToString() == "APPROVER")
                {
                    showControls();
                }
                else
                {
                    //hideControls();
                    showControls();
                }
            }
            else
            {

            }
        }

        private void showControls()
        {
            BtnSaveOne.Visible = true;
            btnClose.Visible = true;
            btnSubmitAll.Visible = true;
            divSubmitAll.Visible = true;
        }

        private void hideControls()
        {
            BtnSaveOne.Visible = false;
            btnClose.Visible = false;
            btnSubmitAll.Visible = false;
            divSubmitAll.Visible = false;
        }

        private void Fill_Details(string index, string size)
        {
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            //int currentYear = DateTime.Now.Year;
            //string currentYearString = currentYear.ToString();
            //ViewState["Year"] = currentYearString;

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpCode = Session["sEmpCode"].ToString();
            objAppraisal.Flag = "HOD";

            ds = objAppraisal.Fill_KeyAccomploshmentsEMPLISTHOD(gvList, index, size);

            Sec2.Visible = false;
            Sec1.Visible = true;
        }

        private void GetKraFlagDetails()
        {
            NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();
            DataSet ds = new DataSet();

            objPmsHr.Comp_Aid = Session["sCompID"].ToString();
            objPmsHr.EmpCode = Session["sEmpCode"].ToString();
            objPmsHr.Type = "KeyAccomploshments";

            //ds = objPmsHr.GetKraStatus();
            ds = objPmsHr.GetEmpKRAFlagStatus();

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["KRA_FLAG"].ToString() == "APPROVER")
                {
                    divAction.Visible = true;
                }
            }
            else if (ds.Tables[0].Rows[0]["KRA_FLAG"].ToString() != "APPROVER")
            {
                divAction.Visible = true;

            }



        }

        private void Get_EmployeeType()
        {
            //objAppraisal.Flag = "PMS";
            //ds = objAppraisal.GetEmployeeType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

            //if (ds.Tables[0].Rows[0]["TYPE"].ToString() == "HOD")
            //{
            //    divAction.Visible = true;
            //}
            //else if ((ds.Tables[0].Rows[0]["TYPE"].ToString() == "HR" || ds.Tables[0].Rows[0]["TYPE"].ToString() == "CEO") && (string)(Session["sEmpCode"]) == "NP3349")
            //{
            //    divAction.Visible = true;
            //}
            //else
            //{
            //    divAction.Visible = true;
            //    foreach (GridViewRow row in grKey.Rows)
            //    {
            //        TextBox txtRptRmk = row.FindControl("txtRptRmk") as TextBox;

            //        txtRptRmk.Enabled = false;
            //    }
            //}

            divAction.Visible = true;
        }



        protected void Fill_ACCOMPLISHMENTS()
        {
            try
            {

                objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                DataSet ds = new DataSet();

                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Comp_Aid = Session["sCompID"].ToString();
                objAppraisal.EmpCode = ViewState["EmpCode"].ToString();
                objAppraisal.Flag = "HOD";

                ds = objAppraisal.Fill_ACCOMPLISHMENTS((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "KEY ACCOMPLISHMENTS");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[1].Rows[0]["FLAG"].ToString() == "HOD")
                    {
                        this.grKey.DataSource = ds;
                        this.grKey.DataBind();
                    }
                    else
                    {
                        this.grKey.DataSource = null;
                        this.grKey.DataBind();

                        divSubmitAll.Visible = false;
                        Fill_Details("1", gvList.PageSize.ToString());
                        mv.SetActiveView(vwListView);
                    }
                }
                else
                {
                    this.grKey.DataSource = null;
                    this.grKey.DataBind();

                    divSubmitAll.Visible = false;
                    Fill_Details("1", gvList.PageSize.ToString());
                    mv.SetActiveView(vwListView);
                }
            }
            catch (Exception ex)
            {

            }
        }


        private DataTable CreateBindKeyAdditionTable()
        {
            DataTable dtCOInfo = new DataTable();

            dtCOInfo.Columns.Add(new DataColumn("CO_Row_Id"));
            dtCOInfo.Columns.Add(new DataColumn("Major contributions / Achievements not covered under KRA’s for the year"));

            dtCOInfo.Columns.Add(new DataColumn("Appraisers comments"));
            dtCOInfo.Columns.Add(new DataColumn("HR Remarks"));

            dtCOInfo.Columns.Add(new DataColumn("Doc_Data_Id"));

            return dtCOInfo;
        }


        protected void lnkBtnDeleteRowKey_Click(object sender, EventArgs e)
        {
            try
            {
                int rowCount = 1;

                DataTable dtCOInfo = this.CreateBindKeyAdditionTable();


                foreach (GridViewRow gvrCO in this.grKey.Rows)
                {
                    if (((CheckBox)gvrCO.FindControl("chkSelect")).Checked == false)
                    {
                        DataRow drCORow = dtCOInfo.NewRow();

                        drCORow["CO_Row_Id"] = rowCount;

                        drCORow["Major contributions / Achievements not covered under KRA’s for the year"] = ((TextBox)gvrCO.FindControl("txtMajor")).Text;


                        drCORow["Appraisers comments"] = ((TextBox)gvrCO.FindControl("txtRptRmk")).Text;
                        drCORow["HR Remarks"] = ((TextBox)gvrCO.FindControl("txtHRRmk")).Text;

                        drCORow["Doc_Data_Id"] = (((Label)gvrCO.FindControl("lblCODataId")).Text.Trim() == "" ? "0" : ((Label)gvrCO.FindControl("lblCODataId")).Text);

                        dtCOInfo.Rows.Add(drCORow);

                        rowCount++;
                    }
                }

                //DataRow drNewCORow = dtCOInfo.NewRow();
                //drNewCORow["CO_Row_Id"] = rowCount;
                //dtCOInfo.Rows.Add(drNewCORow);


                ViewState["AddKeYBind"] = dtCOInfo;
                this.grKey.DataSource = (DataTable)ViewState["AddKeYBind"];
                this.grKey.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkBtnAddRowKey_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCOInfo = this.CreateBindKeyAdditionTable();


                int rowCount = this.grKey.Rows.Count + 1;
                foreach (GridViewRow gvrCO in this.grKey.Rows)
                {
                    DataRow drCORow = dtCOInfo.NewRow();

                    drCORow["CO_Row_Id"] = rowCount;
                    drCORow["Major contributions / Achievements not covered under KRA’s for the year"] = ((TextBox)gvrCO.FindControl("txtMajor")).Text;


                    drCORow["Appraisers comments"] = ((TextBox)gvrCO.FindControl("txtRptRmk")).Text;
                    drCORow["HR Remarks"] = ((TextBox)gvrCO.FindControl("txtHRRmk")).Text;


                    drCORow["Doc_Data_Id"] = (((Label)gvrCO.FindControl("lblCODataId")).Text.Trim() == "" ? "0" : ((Label)gvrCO.FindControl("lblCODataId")).Text);

                    dtCOInfo.Rows.Add(drCORow);

                    rowCount++;
                }

                DataRow drNewCORow = dtCOInfo.NewRow();
                drNewCORow["CO_Row_Id"] = rowCount;
                dtCOInfo.Rows.Add(drNewCORow);


                ViewState["AddKeYBind"] = dtCOInfo;
                this.grKey.DataSource = dtCOInfo;
                this.grKey.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void ShowKeyData()
        {


            this.grKey.DataSource = (DataTable)ViewState["AddKeYBind"];
            this.grKey.DataBind();

        }

        protected void grKey_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dtSubCaInfo = new DataTable();


            dtSubCaInfo = (DataTable)ViewState["AddKeYBind"];



            //DataTable dtSubCaInfo = (DataTable)ViewState["Add"];
            int index = Convert.ToInt32(e.RowIndex);

            GridViewRow row = grKey.Rows[e.RowIndex];

            dtSubCaInfo.Rows[row.DataItemIndex]["txtMajor"] = ((TextBox)grKey.Rows[index].Cells[2].FindControl("txtMajor")).Text;

            dtSubCaInfo.Rows[row.DataItemIndex]["txtRptRmk"] = ((TextBox)grKey.Rows[index].Cells[4].FindControl("txtRptRmk")).Text;
            dtSubCaInfo.Rows[row.DataItemIndex]["txtHRRmk"] = ((TextBox)grKey.Rows[index].Cells[5].FindControl("txtHRRmk")).Text;

            dtSubCaInfo.Rows[row.DataItemIndex]["lblCODataId"] = ((Label)grKey.Rows[index].Cells[9].FindControl("lblCODataId")).Text;
            dtSubCaInfo.Rows[row.DataItemIndex]["lblAddCORowId"] = ((Label)grKey.Rows[index].Cells[9].FindControl("lblAddCORowId")).Text;

            grKey.EditIndex = -1;
            ShowKeyData();

        }


        //protected void BtnSave_Click(object sender, EventArgs e)
        //{
        //    string ErrorMsg;
        //    string Column = "";
        //    objAppraisal = new Portal.PerformanceAppraisal();
        //    //SaveMutipleDetaisl();

        //    objAppraisal.XML = MakeKeyDetailsXml();

        //    ErrorMsg = objAppraisal.CreateUpdateParamApprisal(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]), "1");

        //    objcommon = new Portal.Common();
        //    if (ErrorMsg != "")
        //    {
        //        //divAlertCreate.Visible = true;
        //        //objCommon.SetMessageColor(divAlertCreate, "danger");
        //        //lblMessageCreate.Text = HttpUtility.HtmlEncode(ErrorMsg);
        //    }

        //}

        protected void gvKey_RowEditing(object sender, GridViewEditEventArgs e)
        {

            grKey.EditIndex = e.NewEditIndex;
            ShowKeyData();
        }

        protected void gvKey_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            grKey.EditIndex = -1;
            ShowKeyData();
        }

        protected void grKey_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            GetActivationDateStatus();
            
            Button ButtonView = (Button)sender;
            Label lblEmpName = (Label)ButtonView.NamingContainer.FindControl("lblEmp_name");
            Label lblEmpCode = (Label)ButtonView.NamingContainer.FindControl("lblEmpmid");

            Session["lblEmp_name"] = lblEmpName.Text;
            Session["lblEmpmid"] = lblEmpCode.Text;

            EmpName.Text = lblEmpName.Text;
            EmpCode.Text = lblEmpCode.Text;

            Sec1.Visible = false;

            DataSet ds = new DataSet();
            string index = "1";
            string size = "25";

            //int currentYear = DateTime.Now.Year;
            //string currentYearString = currentYear.ToString();
            //ViewState["Year"] = currentYearString;

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpCode = lblEmpCode.Text;
            ViewState["EmpCode"] = lblEmpCode.Text;

            Fill_ACCOMPLISHMENTS();

            divAction.Visible = false;
            divAlertList.Visible = false;
            divSubmitAll.Visible = true;
            Sec2.Visible = true;

            mv.SetActiveView(vwList);
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("ApprovalEmployeeTaskDetails.aspx", true);

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["EmpCode"] != null)
                {

                    Response.Redirect("ApprovalTrainingAndDevelopment.aspx?EmpCode=" + (string)(ViewState["EmpCode"]), true);
                }
                else
                {
                    Response.Redirect("ApprovalTrainingAndDevelopment.aspx", true);

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            mv.SetActiveView(vwListView);
            Fill_Details("1", gvList.PageSize.ToString());
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            GetActivationDateStatus();
            Fill_Details("1", gvList.PageSize.ToString());
            mv.SetActiveView(vwListView);
        }

        protected void BtnSaveOne_Click(object sender, EventArgs e)
        {
            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            Button ButtonView = (Button)sender;
            Label lblEmpName = (Label)ButtonView.NamingContainer.FindControl("lblEmp_name");
            Label lblEmpCode = (Label)ButtonView.NamingContainer.FindControl("lblEmpmid");

            string index = "1";
            string size = "25";
            //int currentYear = DateTime.Now.Year;
            //string currentYearString = currentYear.ToString();
            //ViewState["Year"] = currentYearString;

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();

            objAppraisal.Year = ViewState["Year"].ToString();

            ds = objAppraisal.Fill_ACCOMPLISHMENTS((string)(Session["lblEmpmid"]), Convert.ToString(Session["sCompID"]), "KEY ACCOMPLISHMENTS");

            if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "3")
            {
                objAppraisal.Flag = "RM";
                objAppraisal.Status = "6";
                objAppraisal.KraFlag = "1";
                objAppraisal.EmpRptMID = (string)(Session["sEmpCode"]);
            }
            else if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "6")
            {
                objAppraisal.Flag = "HOD";
                objAppraisal.Status = "4";
                objAppraisal.KraFlag = "8";
                objAppraisal.EmpCFOMID = (string)(Session["sEmpCode"]);
            }
            else if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "4")
            {
                objAppraisal.Flag = "HR";
                objAppraisal.Status = "1";
                objAppraisal.KraFlag = "";
                objAppraisal.EmpHRMID = (string)(Session["sEmpCode"]);
            }
            else if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "5")
            {
                objAppraisal.Status = "1";
                objAppraisal.EmpCFOMID = (string)(Session["sEmpCode"]);
            }


            string ErrorMsg;
            string Column = "";

            Column = "KEY ACCOMPLISHMENTS";

            objAppraisal.XML = MakeKeyDetailsXml(grKey);
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.SECTION = "2";


            //ds = objAppraisal.GetApproverAid((string)(Session["sCompID"]), (string)(Session["sEmpCode"]));

            //objAppraisal.ApprovalAid = ds.Tables[0].Rows[0]["EMP_APPR_AID"].ToString();

            ds = objAppraisal.UPDATE_KEYACCOMPLISHMENTS(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

            objcommon = new NewPortal2023.ESS.Common();
            divAlertList.Visible = true;
            string script = $@"<script type='text/javascript'>alert('Saved Successfully');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            objcommon.SetMessageColor(divAlertList, "success");
            lblMessageList.Text = "Saved Successfully.";

            mv.SetActiveView(vwList);
            Fill_ACCOMPLISHMENTS();

        }

        //private string MakeKeyBSKILLSDetailsXml()
        //{
        //    System.Text.StringBuilder sbCODetails = new System.Text.StringBuilder();
        //    string xmlCO = string.Empty;
        //    objcommon = new NewPortal2023.ESS.Common();

        //    sbCODetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
        //    sbCODetails.Append("<ROOT>");

        //    //if (!chkAbsent.Checked)
        //    //{
        //    foreach (GridViewRow gvr in this.GVSELF.Rows)
        //    {

        //        sbCODetails.Append("<CO AID='" + ((Label)gvr.FindControl("lblAddCORowId")).Text + "'");
        //        sbCODetails.Append(" Major='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtselfAssM")).Text) + "'");

        //        sbCODetails.Append(" RptRmk='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtAssMAppriser")).Text) + "'");
        //        sbCODetails.Append(" HRRmk='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtSelfHRRmk")).Text) + "'/>");
        //    }
        //    //}

        //    sbCODetails.Append("</ROOT>");

        //    xmlCO = sbCODetails.ToString();

        //    return xmlCO;
        //}

        private string MakeDetailsXml()
        {
            System.Text.StringBuilder sbCODetails = new System.Text.StringBuilder();
            string xmlCO = string.Empty;
            objcommon = new NewPortal2023.ESS.Common();

            sbCODetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sbCODetails.Append("<ROOT>");


            foreach (GridViewRow gvr in this.grKey.Rows)
            {

                sbCODetails.Append("<CO AID='" + ((Label)gvr.FindControl("lblAddCORowId")).Text + "'");
                sbCODetails.Append(" selfAssM='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtMajor")).Text) + "'");
                sbCODetails.Append(" AssMAppriser='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRptRmk")).Text) + "'");
                sbCODetails.Append(" REMARKS='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtHRRmk")).Text) + "'/>");
            }

            sbCODetails.Append("</ROOT>");

            xmlCO = sbCODetails.ToString();

            return xmlCO;
        }

        //protected void BtnSaveTwo_Click(object sender, EventArgs e)
        //{
        //    string ErrorMsg;
        //    string Column = "";
        //    objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        //    DataSet ds = new DataSet();

        //    if (ViewState["BUTTON"].ToString() == "1")
        //    {
        //        Column = "Strenth";
        //    }
        //    else if (ViewState["BUTTON"].ToString() == "2")
        //    {
        //        Column = "aresForImpro";
        //    }
        //    else if (ViewState["BUTTON"].ToString() == "3")
        //    {
        //        Column = "Apprisiation";
        //    }

        //    objAppraisal.XML = MakeKeyBSKILLSDetailsXml();
        //    objAppraisal.Year = ViewState["Year"].ToString();
        //    objAppraisal.SECTION = "2";

        //    ds = objAppraisal.INSERT_KEYACCOMPLISHMENTS(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "1");
        //    // Fill_Details("1", gvList.PageSize.ToString());

        //}

        //protected void Btnsavethree_Click(object sender, EventArgs e)
        //{
        //    string ErrorMsg;
        //    string Column = "";
        //    objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        //    DataSet ds = new DataSet();


        //    Column = "lession";



        //    objAppraisal.Area = txtText.Text;
        //    objAppraisal.Year = ViewState["Year"].ToString();
        //    objAppraisal.SECTION = "2";

        //    ds = objAppraisal.INSERT_KEYlession(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "1");
        //}


        protected void drpQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ViewState["Quarter"] = drpQuarter.SelectedValue;
            Fill_Details("1", gvList.PageSize.ToString());
            Sec1.Visible = true;
        }

        private string MakeKeyDetailsXml(GridView gvList)
        {
            System.Text.StringBuilder sbCODetails = new System.Text.StringBuilder();
            string xmlCO = string.Empty;
            objcommon = new NewPortal2023.ESS.Common();

            sbCODetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sbCODetails.Append("<ROOT>");

            foreach (GridViewRow gvr in this.grKey.Rows)
            {

                sbCODetails.Append("<CO AID='" + ((Label)gvr.FindControl("lblAddCORowId")).Text + "'");
                sbCODetails.Append(" Major='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtMajor")).Text) + "'");
                sbCODetails.Append(" RptRmk='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRptRmk")).Text) + "'");
                sbCODetails.Append(" HRRmk='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtHRRmk")).Text) + "'/>");

            }

            sbCODetails.Append("</ROOT>");

            xmlCO = sbCODetails.ToString();

            return xmlCO;
        }

        protected void btnSubmitAll_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    //int count = 0;

                    //CheckBox chckheader = (CheckBox)grKey.HeaderRow.FindControl("chkSelectAll");
                    //foreach (GridViewRow row in grKey.Rows)
                    //{
                    //    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    //    if (chckrw.Checked == true)
                    //    {
                    //        chckrw.Checked = true;
                    //        count = 1;
                    //    }

                    //}

                    //if (count == 1)
                    //{

                    //}
                    //else
                    //{
                    //    divAlertList.Visible = true;
                    //    lblMessageList.Text = "Please select at least one checkbox before click on Submit button.";
                    //    string script = $@"<script type='text/javascript'>alert('Please select at least one checkbox before click on Submit button.');</script>";
                    //}

                    objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                    DataSet ds = new DataSet();

                    Button ButtonView = (Button)sender;
                    Label lblEmpName = (Label)ButtonView.NamingContainer.FindControl("lblEmp_name");
                    Label lblEmpCode = (Label)ButtonView.NamingContainer.FindControl("lblEmpmid");

                    string index = "1";
                    string size = "25";
                    //int currentYear = DateTime.Now.Year;
                    //string currentYearString = currentYear.ToString();
                    //ViewState["Year"] = currentYearString;

                    ViewState["Quarter"] = drpQuarter.SelectedValue;
                    objAppraisal.Quarter = ViewState["Quarter"].ToString();
                    objAppraisal.Year = ViewState["Year"].ToString();
                    objAppraisal.Cycle = ViewState["CYCLE"].ToString();
                    objAppraisal.CycleAid = ViewState["CYCLE_AID"].ToString();
                    objAppraisal.Comp_Aid = Session["sCompID"].ToString();
                    objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
                    objAppraisal.Year = ViewState["Year"].ToString();

                    objAppraisal.Status = "4";      //APPROVE REJECT STATUS GET UPDATED IN DATABASE ACC TO DROPDOWN SELECTION
                    objAppraisal.KraFlag = "8";
                    objAppraisal.EmpRptMID = (string)(Session["sEmpCode"]);
                    objAppraisal.Flag = "HOD";
                    objAppraisal.EmpHODMID = (string)(Session["sEmpCode"]);
                    objAppraisal.Remarks = txtAllRmk.Text;
                    objAppraisal.Action = drActionAll.SelectedValue;

                    string ErrorMsg;
                    string Column = "";

                    Column = "KEY ACCOMPLISHMENTS";

                    objAppraisal.XML = MakeKeyDetailsXml(grKey);
                    objAppraisal.Year = ViewState["Year"].ToString();
                    objAppraisal.SECTION = "2";

                    //ds = objAppraisal.GetApproverAid((string)(Session["sCompID"]), (string)(Session["sEmpCode"]));

                    //objAppraisal.ApprovalAid = ds.Tables[0].Rows[0]["EMP_APPR_AID"].ToString();
                    if (drActionAll.SelectedValue != "")
                    {
                        if (drActionAll.SelectedValue == "Reject" && txtAllRmk.Text == "")
                        {
                            string message = "Please enter rejection remark";
                            string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            return;
                        }
                        else
                        {
                            ds = objAppraisal.UPDATE_KEYACCOMPLISHMENTSHODF(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "1");

                            objcommon = new NewPortal2023.ESS.Common();
                            divAlertList.Visible = true;
                            objcommon.SetMessageColor(divAlertList, "success");
                            lblMessageList.Text = "Action Successfully Submitted.";

                            string message = lblMessageList.Text;
                            string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                            SENDPMSMAIL(Column, drActionAll.SelectedValue);

                            drActionAll.SelectedValue = "";
                            txtAllRmk.Text = "";

                            Fill_ACCOMPLISHMENTS();
                        }

                    }
                    else
                    {
                        objcommon = new NewPortal2023.ESS.Common();
                        divAlertList.Visible = true;
                        string script = $@"<script type='text/javascript'>alert('Please Select Action');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        objcommon.SetMessageColor(divAlertList, "danger");
                        lblMessageList.Text = "Please Select Action.";
                        mv.SetActiveView(vwList);

                    }

                }
                else
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            }
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chckheader = (CheckBox)grKey.HeaderRow.FindControl("chkSelectAll");
                foreach (GridViewRow row in grKey.Rows)
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
            catch (Exception ex)
            {

            }
        }

        private void SENDPMSMAIL(string Column, string action)
        {
            try
            {
                //DateTime date = DateTime.Now;
                //string emailEmp = "";
                //string type = "Individual Promise";

                string Empemail = "";
                string RMemail = "";
                string HODemail = "";
                string HRemail = "";

                string makerMid = Session["lblEmpmid"].ToString();
                string makerName = Session["lblEmp_name"].ToString();


                DataSet ds = new DataSet();

                ds = objAppraisal.GetMailidDetailsHOD((string)Session["sCompID"], (string)Session["sEmpID"], makerMid);

                if (ds.Tables.Count > 0)
                {
                    Empemail = (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["EMPMAILID"] != DBNull.Value) ? ds.Tables[0].Rows[0]["EMPMAILID"].ToString() : "techsupport@sequelgroup.co.in";
                    RMemail = (ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows[0]["RMMAILID"] != DBNull.Value) ? ds.Tables[1].Rows[0]["RMMAILID"].ToString() : "techsupport@sequelgroup.co.in";
                    HODemail = (ds.Tables[2].Rows.Count > 0 && ds.Tables[2].Rows[0]["HODMAILID"] != DBNull.Value) ? ds.Tables[2].Rows[0]["HODMAILID"].ToString() : "techsupport@sequelgroup.co.in";
                    HRemail = (ds.Tables[3].Rows.Count > 0 && ds.Tables[3].Rows[0]["HRMAILID"] != DBNull.Value) ? ds.Tables[3].Rows[0]["HRMAILID"].ToString() : "techsupport@sequelgroup.co.in";
                }

                if (action == "Approve")
                {
                    using (MailMessage empMail = new MailMessage())
                    {
                        emailSend = new NewPortal2023.ESS.Email();

                        string subject = "Do Not Reply: PMS - Key Accomplishments Approved";
                        string empBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that your Key Accomplishments has been approved by your HOD.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "<strong>Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(empMail, Empemail, subject, empBody);
                    }

                    using (MailMessage rmMail = new MailMessage())
                    {
                        string subject = "Do Not Reply: PMS - Key Accomplishments Approved";

                        string rmBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that Key Accomplishments has been approved by HOD for <strong>Mr. " + makerName + "</strong>.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "Thank you for your prompt attention to this matter.<br/><br/>" +
                        "<strong>With Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(rmMail, RMemail, subject, rmBody);
                    }

                    using (MailMessage hodMail = new MailMessage())
                    {
                        string subject = "Do Not Reply: PMS - Key Accomplishments Approved";

                        string hodBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that Key Accomplishments has been approved for <strong>Mr. " + makerName + "</strong>.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "Thank you for your prompt attention to this matter.<br/><br/>" +
                        "<strong>With Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(hodMail, HODemail, subject, hodBody);
                    }
                }
                else if (action == "Reject")
                {
                    using (MailMessage empMail = new MailMessage())
                    {
                        emailSend = new NewPortal2023.ESS.Email();

                        string subject = "Do Not Reply: PMS - Key Accomplishments Rejected";
                        string empBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that your Key Accomplishments has been rejected by your HOD.<br/><br/>" +
                        "Please log in to the portal at your earliest convenience and take the necessary action to proceed with the process.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "<strong>Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(empMail, Empemail, subject, empBody);
                    }

                    using (MailMessage rmMail = new MailMessage())
                    {
                        string subject = "Do Not Reply: PMS - Key Accomplishments Rejected";

                        string rmBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that Key Accomplishments has been rejected by HOD for <strong>Mr. " + makerName + "</strong>.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "Thank you for your prompt attention to this matter.<br/><br/>" +
                        "<strong>With Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(rmMail, RMemail, subject, rmBody);
                    }

                    using (MailMessage hodMail = new MailMessage())
                    {
                        string subject = "Do Not Reply: PMS - Key Accomplishments Rejected";

                        string hodBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that Key Accomplishments has been rejected for <strong>Mr. " + makerName + "</strong>.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "Thank you for your prompt attention to this matter.<br/><br/>" +
                        "<strong>With Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(hodMail, HODemail, subject, hodBody);
                    }
                }


            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

            }
        }
    }
}