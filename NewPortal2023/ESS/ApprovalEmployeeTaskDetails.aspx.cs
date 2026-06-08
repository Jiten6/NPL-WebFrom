using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using DocumentFormat.OpenXml.Wordprocessing;

namespace NewPortal2023.ESS
{
    public partial class ApprovalEmployeeTaskDetails : System.Web.UI.Page
    {
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sCompID"] != null)
            {
                if (!Page.IsPostBack)
                {
                    try
                    {
                        ViewState["EmpCode"] = null;
                        ViewState["Year"] = "";
                        GetKraFlagDetailsExcel();
                        divSteps.Visible = false;
                        Get_EmployeeType();
                        FillFinYear();
                        Fill_Details("1", gvLIstVIew.PageSize.ToString());
                        mv.SetActiveView(vwListView);

                        divKRAUploadHR.Visible = false;
                        btnUploadHR.Enabled = false;
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
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
            else
            {
                Response.Redirect("~/ESS/ApprovalEmployeeTaskDetails.aspx", true);
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
                divSteps.Visible = false;
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
                    showControls();       //DISABLED AS LOGIC CHANGED THAT PERIOD WISE WINDOW WILL NOT OPEN AND CLOSE
                }
                else
                {
                    //hideControls();       //DISABLED AS LOGIC CHANGED THAT PERIOD WISE WINDOW WILL NOT OPEN AND CLOSE
                    showControls();
                }
            }
            else
            {

            }
        }

        private void showControls()
        {
            BtnSave.Visible = true;
            btnClose.Visible = true;
            btnSubmitDetails.Visible = true;
        }

        private void hideControls()
        {
            BtnSave.Visible = false;
            btnClose.Visible = false;
            btnSubmitDetails.Visible = false;
        }

        private void Fill_Details(string index, string size)
        {
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();
            DataSet ds = new DataSet();

            objPmsHr.Type = "";
            objPmsHr.Comp_Aid = Session["sCompID"].ToString();
            objPmsHr.EmpCode = Session["sEmpCode"].ToString();
            objPmsHr.Year = ViewState["Year"].ToString();

            ds = objPmsHr.GetEmpKRAFlagStatus();

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewState["Quarter"] = drpQuarter.SelectedValue;
                ViewState["Year"] = drpFinancialYear.SelectedValue;
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Comp_Aid = Session["sCompID"].ToString();
                objAppraisal.EmpCode = Session["sEmpCode"].ToString();
                objAppraisal.Flag = "RM";

                ds = objAppraisal.Fill_ApprisialLIstEMPLIST(gvLIstVIew, index, size);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.gvLIstVIew.DataSource = ds;
                    this.gvLIstVIew.DataBind();
                }
                else
                {
                    this.gvLIstVIew.DataSource = null;
                    this.gvLIstVIew.DataBind();
                }

                accordionTwo.Visible = false;
                accordion.Visible = true;
                btnUploadRM.Enabled = true;
                btnUploadHR.Enabled = true;
                divKRAUploadRM.Visible = false;
                divKRAUploadHR.Visible = false;
            }
        }

        private void GetKraFlagDetailsExcel()
        {
            NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();
            DataSet ds = new DataSet();

            objPmsHr.Type = "";
            objPmsHr.Comp_Aid = Session["sCompID"].ToString();
            objPmsHr.EmpCode = Session["sEmpCode"].ToString();

            //ds = objPmsHr.GetKraStatus();
            ds = objPmsHr.GetEmpKRAFlagStatus();

            if (ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[0].Rows[0]["KRA_FLAG"].ToString() == "APPROVER")
                {
                    btnUploadRM.Enabled = false;
                    btnUploadHR.Enabled = false;
                    divKRAUploadRM.Visible = false;
                    divKRAUploadHR.Visible = false;

                }
                else if (ds.Tables[0].Rows[0]["KRA_FLAG"].ToString() != "APPROVER")
                {
                    btnUploadRM.Enabled = false;
                    btnUploadHR.Enabled = false;
                    divKRAUploadRM.Visible = false;
                    divKRAUploadHR.Visible = false;
                }


            }


        }

        private void Get_EmployeeType()
        {
            DataSet ds = new DataSet();

            ds = objAppraisal.GetEmployeeType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

            if (ds.Tables[0].Rows[0]["TYPE"].ToString() == "RM")
            {
                divKRAUploadRM.Visible = false;
                divAction.Visible = true;
                divKRAUploadHR.Visible = false;
            }
            else if (ds.Tables[0].Rows[0]["TYPE"].ToString() == "HOD")
            {
                divKRAUploadRM.Visible = false;
                divAction.Visible = true;
                divKRAUploadHR.Visible = false;
            }
            else
            {
                divKRAUploadRM.Visible = false;
                divAction.Visible = true;
                divKRAUploadHR.Visible = false;
            }


        }

        private void GetKraFlagDetails()
        {
            NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();
            DataSet ds = new DataSet();

            objPmsHr.Comp_Aid = Session["sCompID"].ToString();
            objPmsHr.EmpCode = Session["lblEmpmid"].ToString();
            //objPmsHr.Type = "Appraisal";

            string Column = "";

            if (ViewState["BUTTON"].ToString() == "1")
            {
                Column = "Financials";
            }
            else if (ViewState["BUTTON"].ToString() == "2")
            {
                Column = "Competitiveness";
            }
            else if (ViewState["BUTTON"].ToString() == "3")
            {
                Column = "Operational Excellence";
            }
            else if (ViewState["BUTTON"].ToString() == "4")
            {
                Column = "People";
            }
            else if (ViewState["BUTTON"].ToString() == "5")
            {
                Column = "Future Readiness";
            }

            objPmsHr.Type = Column;

            ds = objPmsHr.GetKraStatus();

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (GridViewRow row in gvSteps.Rows)
                {
                    TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                    TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                    TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    TextBox txtAchPer = (TextBox)row.FindControl("txtAchivePer");
                    TextBox txtModiMDRecom = (TextBox)row.FindControl("txtModiMDRecom");
                    TextBox txtRemark = (TextBox)row.FindControl("txtRemark");

                    if (ds.Tables[0].Rows[0]["KRA_FLAG"].ToString() == "APPROVER")
                    {
                        txtMetric.ReadOnly = true;
                        txtTarget.ReadOnly = true;
                        txtWeightage.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtAchPer.ReadOnly = false;
                        txtModiMDRecom.ReadOnly = false;
                        txtRemark.ReadOnly = false;
                        BtnSave.Visible = true;
                    }
                    else if (ds.Tables[0].Rows[0]["KRA_FLAG"].ToString() == "HR")
                    {
                        txtMetric.ReadOnly = true;
                        txtTarget.ReadOnly = true;
                        txtWeightage.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtAchPer.ReadOnly = false;
                        txtModiMDRecom.ReadOnly = false;
                        txtRemark.ReadOnly = false;
                        BtnSave.Visible = true;
                    }
                    else
                    {
                        if (ds.Tables[0].Rows[0]["KRA_FLAG"].ToString() != "APPROVER")
                        {
                            txtMetric.ReadOnly = true;
                            txtTarget.ReadOnly = true;
                            txtWeightage.ReadOnly = true;
                            txtRemarks.ReadOnly = true;
                            txtAchPer.ReadOnly = true;
                            txtModiMDRecom.ReadOnly = true;
                            txtRemark.ReadOnly = true;
                            BtnSave.Visible = false;
                        }
                    }
                }

            }
            else
            {

            }

        }

        protected void drpQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mv.SetActiveView(vwList);
                ViewState["Quarter"] = drpQuarter.SelectedValue;
                Fill_Details("1", gvLIstVIew.PageSize.ToString());
                accordion.Visible = true;
            }

            catch (Exception ex)
            {

            }

        }

        protected void btnList_Click(object sender, EventArgs e)
        {
            mv.SetActiveView(vwListView);
            Fill_Details("1", gvLIstVIew.PageSize.ToString());
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            mv.SetActiveView(vwList);
            accordionTwo.Visible = true;
            accordion.Visible = false;

            DataSet ds = new DataSet();
            string index = "1";
            string size = "25";
            Button ButtonView = (Button)sender;

            Label lblEmpName = (Label)ButtonView.NamingContainer.FindControl("lblEmp_name");
            Label lblEmpCode = (Label)ButtonView.NamingContainer.FindControl("lblEmpmid");

            Session["lblEmp_name"] = lblEmpName.Text;
            Session["lblEmpmid"] = lblEmpCode.Text;

            EmpName.Text = lblEmpName.Text;
            EmpCode.Text = lblEmpCode.Text;

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpCode = Session["sEmpCode"].ToString();
            objAppraisal.EmpMakerMID = lblEmpCode.Text;
            ViewState["EmpCode"] = lblEmpCode.Text;
            objAppraisal.Flag = "RM";

            ds = objAppraisal.Fill_ApprisialLIstApproval(gvList, index, size);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvList.DataSource = ds;
                gvList.DataBind();
            }
            else
            {
                gvList.DataSource = null;
                gvList.DataBind();
            }

        }

        protected void lnkBtnDeleteRowSteps_Click(object sender, EventArgs e)
        {
            try
            {
                int rowCount = 1;

                DataTable dtCOInfo = this.CreateBindAdditionTable();


                foreach (GridViewRow gvrCO in this.gvSteps.Rows)
                {
                    if (((CheckBox)gvrCO.FindControl("chkSelect")).Checked == false)
                    {
                        DataRow drCORow = dtCOInfo.NewRow();

                        drCORow["CO_Row_Id"] = rowCount;

                        drCORow["AREA"] = ((TextBox)gvrCO.FindControl("txtArea")).Text;
                        drCORow["METRIC"] = ((TextBox)gvrCO.FindControl("txtMetric")).Text;
                        drCORow["TARGET"] = ((TextBox)gvrCO.FindControl("txtTarget")).Text;
                        drCORow["TWT"] = ((TextBox)gvrCO.FindControl("txtwt")).Text;
                        drCORow["Weightage"] = ((TextBox)gvrCO.FindControl("txtWeightage")).Text;
                        drCORow["Achievement_REMARKS"] = ((TextBox)gvrCO.FindControl("txtRemarks")).Text;

                        drCORow["Doc_Data_Id"] = (((Label)gvrCO.FindControl("lblCODataId")).Text.Trim() == "" ? "0" : ((Label)gvrCO.FindControl("lblCODataId")).Text);

                        dtCOInfo.Rows.Add(drCORow);

                        rowCount++;
                    }
                }



                if (ViewState["BUTTON"].ToString() == "1")
                {
                    ViewState["Finanace"] = dtCOInfo;
                    this.gvSteps.DataSource = (DataTable)ViewState["Finanace"];
                    this.gvSteps.DataBind();
                }
                else if (ViewState["BUTTON"].ToString() == "2")
                {
                    ViewState["Competitiveness"] = dtCOInfo;
                    this.gvSteps.DataSource = (DataTable)ViewState["Competitiveness"];
                    this.gvSteps.DataBind();
                }
                else if (ViewState["BUTTON"].ToString() == "3")
                {
                    ViewState["Operational"] = dtCOInfo;
                    this.gvSteps.DataSource = (DataTable)ViewState["Operational"];
                    this.gvSteps.DataBind();
                }
                else if (ViewState["BUTTON"].ToString() == "4")
                {
                    ViewState["People"] = dtCOInfo;
                    this.gvSteps.DataSource = (DataTable)ViewState["People"];
                    this.gvSteps.DataBind();
                }
                else if (ViewState["BUTTON"].ToString() == "5")
                {
                    ViewState["FutureReadiness"] = dtCOInfo;
                    this.gvSteps.DataSource = (DataTable)ViewState["FutureReadiness"];
                    this.gvSteps.DataBind();
                }
                //ViewState["AddBind"] = dtCOInfo;
                //this.gvSteps.DataSource = (DataTable)ViewState["AddBind"];
                //this.gvSteps.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkBtnAddRowSteps_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCOInfo = this.CreateBindAdditionTable();

                int rowCount = this.gvSteps.Rows.Count + 1;
                foreach (GridViewRow gvrCO in this.gvSteps.Rows)
                {
                    DataRow drCORow = dtCOInfo.NewRow();

                    drCORow["CO_Row_Id"] = rowCount;
                    drCORow["AREA"] = ((TextBox)gvrCO.FindControl("txtArea")).Text;
                    drCORow["METRIC"] = ((TextBox)gvrCO.FindControl("txtMetric")).Text;
                    drCORow["TARGET"] = ((TextBox)gvrCO.FindControl("txtTarget")).Text;
                    drCORow["TWT"] = ((TextBox)gvrCO.FindControl("txtwt")).Text;
                    drCORow["Weightage"] = ((TextBox)gvrCO.FindControl("txtWeightage")).Text;
                    drCORow["Achievement_REMARKS"] = ((TextBox)gvrCO.FindControl("txtRemarks")).Text;
                    drCORow["Appraisal_AID"] = ((TextBox)gvrCO.FindControl("txtaid")).Text;
                    drCORow["Doc_Data_Id"] = (((Label)gvrCO.FindControl("lblCODataId")).Text.Trim() == "" ? "0" : ((Label)gvrCO.FindControl("lblCODataId")).Text);

                    dtCOInfo.Rows.Add(drCORow);
                    rowCount++;
                }

                DataRow drNewCORow = dtCOInfo.NewRow();
                drNewCORow["CO_Row_Id"] = rowCount;
                dtCOInfo.Rows.Add(drNewCORow);

                DataTable ViewStateTable = null;

                if (ViewState["BUTTON"].ToString() == "1")
                {

                    ViewState["Finanace"] = dtCOInfo;
                    ViewStateTable = (DataTable)ViewState["Finanace"];
                }
                else if (ViewState["BUTTON"].ToString() == "2")
                {

                    ViewState["Competitiveness"] = dtCOInfo;
                    ViewStateTable = (DataTable)ViewState["Competitiveness"];
                }
                else if (ViewState["BUTTON"].ToString() == "3")
                {

                    ViewState["Operational"] = dtCOInfo;
                    ViewStateTable = (DataTable)ViewState["Operational"];

                }
                else if (ViewState["BUTTON"].ToString() == "4")
                {

                    ViewState["People"] = dtCOInfo;
                    ViewStateTable = (DataTable)ViewState["People"];
                }
                else if (ViewState["BUTTON"].ToString() == "5")
                {

                    ViewState["FutureReadiness"] = dtCOInfo;
                    ViewStateTable = (DataTable)ViewState["FutureReadiness"];
                }

                if (ViewStateTable != null)
                {
                    this.gvSteps.DataSource = ViewStateTable;
                    this.gvSteps.DataBind();
                }
            }
            catch (Exception ex)
            {
                // Handle the exception here
            }

        }

        private DataTable CreateBindAdditionTable()
        {
            DataTable dtCOInfo = new DataTable();

            dtCOInfo.Columns.Add(new DataColumn("CO_Row_Id"));
            dtCOInfo.Columns.Add(new DataColumn("AREA"));
            dtCOInfo.Columns.Add(new DataColumn("METRIC"));
            dtCOInfo.Columns.Add(new DataColumn("TARGET"));
            dtCOInfo.Columns.Add(new DataColumn("TWT"));
            dtCOInfo.Columns.Add(new DataColumn("Weightage"));
            dtCOInfo.Columns.Add(new DataColumn("Achievement_REMARKS"));
            dtCOInfo.Columns.Add(new DataColumn("Appraisal_AID"));
            dtCOInfo.Columns.Add(new DataColumn("Doc_Data_Id"));

            return dtCOInfo;
        }

        protected void GetCyclePhaseDetails()
        {
            DataSet ds = new DataSet();
            ds = objAppraisal.GetCyclePhaseDetails((string)Session["sCompID"], (string)ViewState["Year"]);

            if ((string)ViewState["CYCLE_AID"] == "C0")
            {
                if (ds.Tables[0].Rows[0]["PHASE_AID"].ToString() == "PH00001")  //START
                {
                    foreach (GridViewRow row in gvSteps.Rows)
                    {
                        TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                        TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                        TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                        TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                        TextBox txtMidYrReviewRM = (TextBox)row.FindControl("txtMidYrReviewRM");
                        TextBox txtAchivePer = (TextBox)row.FindControl("txtAchivePer");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        TextBox txtModiMDRecom = (TextBox)row.FindControl("txtModiMDRecom");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");

                        txtMetric.ReadOnly = false;
                        txtTarget.ReadOnly = false;
                        txtWeightage.ReadOnly = false;
                        txtMidYrReview.ReadOnly = true;
                        txtMidYrReviewRM.ReadOnly = true;
                        txtAchivePer.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtModiMDRecom.ReadOnly = true;
                        txtRemark.ReadOnly = false;
                    }
                }
                else if (ds.Tables[0].Rows[0]["PHASE_AID"].ToString() == "PH00002")     //MIDYEAR
                {
                    foreach (GridViewRow row in gvSteps.Rows)
                    {
                        TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                        TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                        TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                        TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                        TextBox txtMidYrReviewRM = (TextBox)row.FindControl("txtMidYrReviewRM");
                        TextBox txtAchivePer = (TextBox)row.FindControl("txtAchivePer");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        TextBox txtModiMDRecom = (TextBox)row.FindControl("txtModiMDRecom");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");

                        txtMetric.ReadOnly = true;
                        txtTarget.ReadOnly = true;
                        txtWeightage.ReadOnly = true;
                        txtMidYrReview.ReadOnly = true;
                        txtMidYrReviewRM.ReadOnly = false;
                        txtAchivePer.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtModiMDRecom.ReadOnly = true;
                        txtRemark.ReadOnly = false;
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvSteps.Rows)
                    {
                        TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                        TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                        TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                        TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                        TextBox txtMidYrReviewRM = (TextBox)row.FindControl("txtMidYrReviewRM");
                        TextBox txtAchivePer = (TextBox)row.FindControl("txtAchivePer");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        TextBox txtModiMDRecom = (TextBox)row.FindControl("txtModiMDRecom");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");

                        txtMetric.ReadOnly = true;
                        txtTarget.ReadOnly = true;
                        txtWeightage.ReadOnly = true;
                        txtMidYrReview.ReadOnly = true;
                        txtMidYrReviewRM.ReadOnly = true;
                        txtAchivePer.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtModiMDRecom.ReadOnly = true;
                        txtRemark.ReadOnly = true;
                    }
                }
            }
            else if ((string)ViewState["CYCLE_AID"] == "C1")
            {
                if (ds.Tables[0].Rows[0]["PHASE_AID"].ToString() == "PH00003")      //APPRAISAL
                {
                    foreach (GridViewRow row in gvSteps.Rows)
                    {
                        TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                        TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                        TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                        TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                        TextBox txtMidYrReviewRM = (TextBox)row.FindControl("txtMidYrReviewRM");
                        TextBox txtAchivePer = (TextBox)row.FindControl("txtAchivePer");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        TextBox txtModiMDRecom = (TextBox)row.FindControl("txtModiMDRecom");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");

                        txtMetric.ReadOnly = true;
                        txtTarget.ReadOnly = true;
                        txtWeightage.ReadOnly = true;
                        txtMidYrReview.ReadOnly = true;
                        txtMidYrReviewRM.ReadOnly = true;
                        txtAchivePer.ReadOnly = false;
                        txtRemarks.ReadOnly = false;
                        txtModiMDRecom.ReadOnly = false;
                        txtRemark.ReadOnly = false;
                    }
                }
                else if (ds.Tables[0].Rows[0]["PHASE_AID"].ToString() == "PH00004")     //END
                {
                    foreach (GridViewRow row in gvSteps.Rows)
                    {
                        TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                        TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                        TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                        TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                        TextBox txtMidYrReviewRM = (TextBox)row.FindControl("txtMidYrReviewRM");
                        TextBox txtAchivePer = (TextBox)row.FindControl("txtAchivePer");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        TextBox txtModiMDRecom = (TextBox)row.FindControl("txtModiMDRecom");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");

                        txtMetric.ReadOnly = true;
                        txtTarget.ReadOnly = true;
                        txtWeightage.ReadOnly = true;
                        txtMidYrReview.ReadOnly = true;
                        txtMidYrReviewRM.ReadOnly = true;
                        txtAchivePer.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtModiMDRecom.ReadOnly = true;
                        txtRemark.ReadOnly = false;
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvSteps.Rows)
                    {
                        TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                        TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                        TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                        TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                        TextBox txtMidYrReviewRM = (TextBox)row.FindControl("txtMidYrReviewRM");
                        TextBox txtAchivePer = (TextBox)row.FindControl("txtAchivePer");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        TextBox txtModiMDRecom = (TextBox)row.FindControl("txtModiMDRecom");
                        TextBox txtRemark = (TextBox)row.FindControl("txtRemark");

                        txtMetric.ReadOnly = true;
                        txtTarget.ReadOnly = true;
                        txtWeightage.ReadOnly = true;
                        txtMidYrReview.ReadOnly = true;
                        txtMidYrReviewRM.ReadOnly = true;
                        txtAchivePer.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtModiMDRecom.ReadOnly = true;
                        txtRemark.ReadOnly = true;
                    }
                }
            }
            else
            {
                foreach (GridViewRow row in gvSteps.Rows)
                {
                    TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                    TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                    TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                    TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                    TextBox txtMidYrReviewRM = (TextBox)row.FindControl("txtMidYrReviewRM");
                    TextBox txtAchivePer = (TextBox)row.FindControl("txtAchivePer");
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    TextBox txtModiMDRecom = (TextBox)row.FindControl("txtModiMDRecom");
                    TextBox txtRemark = (TextBox)row.FindControl("txtRemark");

                    txtMetric.ReadOnly = true;
                    txtTarget.ReadOnly = true;
                    txtWeightage.ReadOnly = true;
                    txtMidYrReview.ReadOnly = true;
                    txtMidYrReviewRM.ReadOnly = true;
                    txtAchivePer.ReadOnly = true;
                    txtRemarks.ReadOnly = true;
                    txtModiMDRecom.ReadOnly = true;
                    txtRemark.ReadOnly = true;
                }
            }
        }
        protected void btnFinancials_Click(object sender, EventArgs e)
        {
            try
            {
                divSteps.Visible = true;

                btnFinancials.BackColor = System.Drawing.Color.Green;
                btnCompetitiveness.BackColor = System.Drawing.Color.Gray;
                btnOperationalExcellence.BackColor = System.Drawing.Color.Gray;
                btnPeople.BackColor = System.Drawing.Color.Gray;
                btnFutureReadiness.BackColor = System.Drawing.Color.Gray;

                ViewState["BUTTON"] = "1";
                objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                DataSet ds = new DataSet();

                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.EmpCode = Session["sEmpCode"].ToString();
                objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
                objAppraisal.Flag = "RM";

                ds = objAppraisal.Fill_Apprisial(Convert.ToString(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "Financials");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.gvSteps.DataSource = ds.Tables[0];
                    this.gvSteps.DataBind();
                    GetActivationDateStatus();
                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["Financials"];
                    this.gvSteps.DataBind();
                    BtnSave.Visible = false;
                    btnClose.Visible = false;
                }

                GetCyclePhaseDetails();
            }
            catch (Exception ex)
            {


            }
        }
        protected void btnCompetitiveness_Click(object sender, EventArgs e)
        {
            try
            {
                divSteps.Visible = true;

                btnFinancials.BackColor = System.Drawing.Color.Gray;
                btnCompetitiveness.BackColor = System.Drawing.Color.Green;
                btnOperationalExcellence.BackColor = System.Drawing.Color.Gray;
                btnPeople.BackColor = System.Drawing.Color.Gray;
                btnFutureReadiness.BackColor = System.Drawing.Color.Gray;

                ViewState["BUTTON"] = "2";
                objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                DataSet ds = new DataSet();

                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.EmpCode = Session["sEmpCode"].ToString();
                objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
                objAppraisal.Flag = "RM";

                ds = objAppraisal.Fill_Apprisial((string)(ViewState["EmpCode"]), Convert.ToString(Session["sCompID"]), "Competitiveness");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.gvSteps.DataSource = ds.Tables[0];
                    this.gvSteps.DataBind();
                    GetActivationDateStatus();
                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["Competitiveness"];
                    this.gvSteps.DataBind();
                    BtnSave.Visible = false;
                    btnClose.Visible = false;
                }

                GetCyclePhaseDetails();
            }
            catch (Exception ex)
            {


            }
        }
        protected void btnOperationalExcellence_Click(object sender, EventArgs e)
        {
            try
            {
                divSteps.Visible = true;

                btnFinancials.BackColor = System.Drawing.Color.Gray;
                btnCompetitiveness.BackColor = System.Drawing.Color.Gray;
                btnOperationalExcellence.BackColor = System.Drawing.Color.Green;
                btnPeople.BackColor = System.Drawing.Color.Gray;
                btnFutureReadiness.BackColor = System.Drawing.Color.Gray;

                ViewState["BUTTON"] = "3";

                objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                DataSet ds = new DataSet();

                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.EmpCode = Session["sEmpCode"].ToString();
                objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
                objAppraisal.Flag = "RM";

                ds = objAppraisal.Fill_Apprisial((string)(ViewState["EmpCode"]), Convert.ToString(Session["sCompID"]), "Operational Excellence");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.gvSteps.DataSource = ds.Tables[0];
                    this.gvSteps.DataBind();
                    GetActivationDateStatus();
                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["Operational Excellence"];
                    this.gvSteps.DataBind();
                    BtnSave.Visible = false;
                    btnClose.Visible = false;
                }

                GetCyclePhaseDetails();
            }
            catch (Exception ex)
            {


            }
        }
        protected void btnPeople_Click(object sender, EventArgs e)
        {
            try
            {
                divSteps.Visible = true;

                btnFinancials.BackColor = System.Drawing.Color.Gray;
                btnCompetitiveness.BackColor = System.Drawing.Color.Gray;
                btnOperationalExcellence.BackColor = System.Drawing.Color.Gray;
                btnPeople.BackColor = System.Drawing.Color.Green;
                btnFutureReadiness.BackColor = System.Drawing.Color.Gray;

                ViewState["BUTTON"] = "4";
                objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                DataSet ds = new DataSet();

                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.EmpCode = Session["sEmpCode"].ToString();
                objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
                objAppraisal.Flag = "RM";

                ds = objAppraisal.Fill_Apprisial((string)(ViewState["EmpCode"]), Convert.ToString(Session["sCompID"]), "People");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.gvSteps.DataSource = ds.Tables[0];
                    this.gvSteps.DataBind();
                    GetActivationDateStatus();
                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["People"];
                    this.gvSteps.DataBind();
                    BtnSave.Visible = false;
                    btnClose.Visible = false;
                }

                GetCyclePhaseDetails();
            }
            catch (Exception ex)
            {


            }
        }
        protected void btnFutureReadiness_Click(object sender, EventArgs e)
        {
            divSteps.Visible = true;

            btnFinancials.BackColor = System.Drawing.Color.Gray;
            btnCompetitiveness.BackColor = System.Drawing.Color.Gray;
            btnOperationalExcellence.BackColor = System.Drawing.Color.Gray;
            btnPeople.BackColor = System.Drawing.Color.Gray;
            btnFutureReadiness.BackColor = System.Drawing.Color.Green;

            ViewState["BUTTON"] = "5";
            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.EmpCode = Session["sEmpCode"].ToString();
            objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
            objAppraisal.Flag = "RM";

            ds = objAppraisal.Fill_Apprisial((string)(ViewState["EmpCode"]), Convert.ToString(Session["sCompID"]), "Future Readiness");
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.gvSteps.DataSource = ds.Tables[0];
                this.gvSteps.DataBind();
                GetActivationDateStatus();

            }
            else
            {
                this.gvSteps.DataSource = (DataTable)ViewState["Future Readiness"];
                this.gvSteps.DataBind();
                BtnSave.Visible = false;
                btnClose.Visible = false;
            }

            GetCyclePhaseDetails();
        }

        protected void ShowData()
        {
            if (ViewState["BUTTON"].ToString() == "1")
            {
                this.gvSteps.DataSource = (DataTable)ViewState["Finanace"];
                this.gvSteps.DataBind();
            }
            else if (ViewState["BUTTON"].ToString() == "2")
            {

                this.gvSteps.DataSource = (DataTable)ViewState["Competitiveness"];
                this.gvSteps.DataBind();
            }
            else if (ViewState["BUTTON"].ToString() == "3")
            {

                this.gvSteps.DataSource = (DataTable)ViewState["Operational"];
                this.gvSteps.DataBind();
            }
            else if (ViewState["BUTTON"].ToString() == "4")
            {

                this.gvSteps.DataSource = (DataTable)ViewState["People"];
                this.gvSteps.DataBind();
            }
            else if (ViewState["BUTTON"].ToString() == "5")
            {

                this.gvSteps.DataSource = (DataTable)ViewState["FutureReadiness"];
                this.gvSteps.DataBind();
            }

        }

        protected void gvSteps_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dtSubCaInfo = new DataTable();

            if (ViewState["BUTTON"].ToString() == "1")
            {
                dtSubCaInfo = (DataTable)ViewState["Finanace"];
            }
            else if (ViewState["BUTTON"].ToString() == "2")
            {
                dtSubCaInfo = (DataTable)ViewState["Competitiveness"];
            }
            else if (ViewState["BUTTON"].ToString() == "3")
            {
                dtSubCaInfo = (DataTable)ViewState["Operational"];
            }
            else if (ViewState["BUTTON"].ToString() == "4")
            {
                dtSubCaInfo = (DataTable)ViewState["People"];
            }
            else if (ViewState["BUTTON"].ToString() == "5")
            {
                dtSubCaInfo = (DataTable)ViewState["FutureReadiness"];
            }


            //DataTable dtSubCaInfo = (DataTable)ViewState["Add"];
            int index = Convert.ToInt32(e.RowIndex);

            GridViewRow row = gvSteps.Rows[e.RowIndex];

            dtSubCaInfo.Rows[row.DataItemIndex]["txtArea"] = ((TextBox)gvSteps.Rows[index].Cells[2].FindControl("txtArea")).Text;
            dtSubCaInfo.Rows[row.DataItemIndex]["txtMetric"] = ((TextBox)gvSteps.Rows[index].Cells[3].FindControl("txtMetric")).Text;
            dtSubCaInfo.Rows[row.DataItemIndex]["txtTarget"] = ((TextBox)gvSteps.Rows[index].Cells[4].FindControl("txtTarget")).Text;
            dtSubCaInfo.Rows[row.DataItemIndex]["txtwt"] = ((TextBox)gvSteps.Rows[index].Cells[5].FindControl("txtwt")).Text;
            dtSubCaInfo.Rows[row.DataItemIndex]["txtWeightage"] = ((TextBox)gvSteps.Rows[index].Cells[6].FindControl("txtWeightage")).Text;
            dtSubCaInfo.Rows[row.DataItemIndex]["txtRemarks"] = ((TextBox)gvSteps.Rows[index].Cells[7].FindControl("txtRemarks")).Text;
            dtSubCaInfo.Rows[row.DataItemIndex]["txtBusRationaleAdd"] = ((TextBox)gvSteps.Rows[index].Cells[8].FindControl("txtBusRationaleAdd")).Text;
            dtSubCaInfo.Rows[row.DataItemIndex]["txtNatureOfAcAdd"] = ((TextBox)gvSteps.Rows[index].Cells[9].FindControl("txtNatureOfAcAdd")).Text;
            dtSubCaInfo.Rows[row.DataItemIndex]["txtaid"] = ((TextBox)gvSteps.Rows[index].Cells[9].FindControl("txtaid")).Text;
            dtSubCaInfo.Rows[row.DataItemIndex]["lblCODataId"] = ((Label)gvSteps.Rows[index].Cells[9].FindControl("lblCODataId")).Text;

            dtSubCaInfo.Rows[row.DataItemIndex]["lblAddCORowId"] = ((Label)gvSteps.Rows[index].Cells[9].FindControl("lblAddCORowId")).Text;

            gvSteps.EditIndex = -1;
            ShowData();

        }

        protected void EmployeeDetailsList()
        {
            DataSet ds = new DataSet();

            string index = "1";
            string size = "25";

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpCode = Session["sEmpCode"].ToString();
            objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
            objAppraisal.Flag = "RM";

            ds = objAppraisal.Fill_ApprisialLIstApproval(gvList, index, size);

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.gvList.DataSource = ds;
                this.gvList.DataBind();
            }
            else
            {
                this.gvList.DataSource = null;
                this.gvList.DataBind();

                Fill_Details("1", gvLIstVIew.PageSize.ToString());
                mv.SetActiveView(vwListView);
            }
        }

        private string MakeDetailsXml(string Column)
        {
            System.Text.StringBuilder sbCODetails = new System.Text.StringBuilder();
            string xmlCO = string.Empty;
            objcommon = new NewPortal2023.ESS.Common();

            sbCODetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sbCODetails.Append("<ROOT>");

            foreach (GridViewRow gvr in this.gvSteps.Rows)
            {
                string appAID = objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblAid")).Text);
                string area = Column;
                string metric = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtMetric")).Text);
                string target = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtTarget")).Text);
                string weightageText = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtWeightage")).Text);
                string MidYrReviewText = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtMidYrReview")).Text);
                string MidYrReviewRMText = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtMidYrReviewRM")).Text);
                string achivePerText = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtAchivePer")).Text);
                string modiMDRecom = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtModiMDRecom")).Text);
                string remarks = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemark")).Text);

                decimal weightage = 0;
                decimal achivePer = 0;
                decimal achAgainstWht = 0;

                if (weightageText != "")
                {
                    if (!decimal.TryParse(weightageText, out weightage))
                    {
                        return "Weightage must be a valid numerical value.";
                    }
                }

                if (achivePerText != "")
                {
                    if (!decimal.TryParse(achivePerText, out achivePer))
                    {
                        return "Acheivement Percentage must be a valid numerical value.";
                    }
                }

                if (decimal.TryParse(weightageText, out decimal weightageval) && decimal.TryParse(achivePerText, out decimal achPerval))
                {
                    achAgainstWht = weightageval * achPerval / 100;
                }
                else
                {
                    achAgainstWht = 0;
                }

                sbCODetails.Append("<CO APPRAISAL_AID='" + appAID + "'");
                sbCODetails.Append(" AREA='" + area + "'");
                sbCODetails.Append(" METRIC='" + metric + "'");
                sbCODetails.Append(" TARGET='" + target + "'");
                sbCODetails.Append(" WEIGHTAGE='" + weightage + "'");
                sbCODetails.Append(" MIDYEARRIVIEW='" + MidYrReviewText + "'");
                sbCODetails.Append(" MIDYEARRIVIEWRM='" + MidYrReviewRMText + "'");
                sbCODetails.Append(" ACHIEVEDWEIGHTAGEPERCT='" + achivePer + "'");
                sbCODetails.Append(" ACHIEVEMENTPERCT='" + achAgainstWht.ToString() + "'");
                sbCODetails.Append(" MODIMDRECOM='" + modiMDRecom + "'");
                sbCODetails.Append(" REMARKS='" + remarks + "'/>");
            }

            sbCODetails.Append("</ROOT>");

            xmlCO = sbCODetails.ToString();

            return xmlCO;
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    DataSet ds = new DataSet();
                    string Column = "";
                    DataSet dsInv = new DataSet();

                    objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();

                    if (ViewState["BUTTON"].ToString() == "1")
                    {
                        Column = "Financials";
                    }
                    else if (ViewState["BUTTON"].ToString() == "2")
                    {
                        Column = "Competitiveness";
                    }
                    else if (ViewState["BUTTON"].ToString() == "3")
                    {
                        Column = "Operational Excellence";
                    }
                    else if (ViewState["BUTTON"].ToString() == "4")
                    {
                        Column = "People";
                    }
                    else if (ViewState["BUTTON"].ToString() == "5")
                    {
                        Column = "Future Readiness";
                    }

                    objAppraisal.XML = MakeDetailsXml(Column);

                    if (objAppraisal.XML == "Weightage must be a valid numerical value.")
                    {

                        objcommon = new NewPortal2023.ESS.Common();
                        divAlertList.Visible = true;

                        string message = "Weightage must be a valid numerical value.";
                        string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        objcommon.SetMessageColor(divAlertList, "danger");
                        lblMessageList.Text = "Weightage must be a valid numerical value.";

                        return;
                    }
                    else if (objAppraisal.XML == "Acheivement Percentage must be a valid numerical value.")
                    {

                        objcommon = new NewPortal2023.ESS.Common();
                        divAlertList.Visible = true;

                        string message1 = "Acheivement Percentage must be a valid numerical value.";
                        string scriptA = $@"<script type='text/javascript'>alert('{message1}');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", scriptA);
                        objcommon.SetMessageColor(divAlertList, "danger");
                        lblMessageList.Text = "Acheivement Percentage must be a valid numerical value.";

                        return;
                    }

                    objAppraisal.Year = ViewState["Year"].ToString();
                    objAppraisal.Quarter = ViewState["Quarter"].ToString();
                    objAppraisal.Comp_Aid = Session["sCompID"].ToString();
                    objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();

                    if ((string)Session["sEmpID"] == "EP000136")        //RAJIV ARORA
                    {
                        objAppraisal.SECTION = "1";
                        objAppraisal.Flag = "RM";
                        objAppraisal.Status = "4d";
                        objAppraisal.KraFlag = "1";
                        objAppraisal.EmpRptMID = (string)(Session["sEmpCode"]);
                    }
                    else
                    {
                        objAppraisal.SECTION = "1";
                        objAppraisal.Flag = "RM";
                        objAppraisal.Status = "6d";
                        objAppraisal.KraFlag = "1";
                        objAppraisal.EmpRptMID = (string)(Session["sEmpCode"]);
                    }

                    ds = objAppraisal.UpdateParamApprisalRM(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Columns.Contains("MESSAGE") && ds.Tables[0].Rows[0]["MESSAGE"] != DBNull.Value)
                        {
                            objcommon = new NewPortal2023.ESS.Common();
                            divAlertList.Visible = true;
                            lblMessageList.Text = ds.Tables[0].Rows[0]["MESSAGE"].ToString();
                            objcommon.SetMessageColor(divAlertList, "danger");

                            string message = lblMessageList.Text;
                            string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                            divSteps.Visible = false;
                            EmployeeDetailsList();
                        }
                        else
                        {
                            objcommon = new NewPortal2023.ESS.Common();
                            divAlertList.Visible = true;
                            lblMessageList.Text = "Saved Successfully.";
                            objcommon.SetMessageColor(divAlertList, "success");

                            string message = lblMessageList.Text;
                            string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                            divSteps.Visible = false;
                            EmployeeDetailsList();
                        }
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

        protected void btnClose_Click(object sender, EventArgs e)
        {
            mv.SetActiveView(vwList);
            divSteps.Visible = false;
            GetActivationDateStatus();
            EmployeeDetailsList();
        }

        protected void gvSteps_RowEditing(object sender, GridViewEditEventArgs e)
        {

            gvSteps.EditIndex = e.NewEditIndex;
            ShowData();
        }
        protected void gvSteps_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    objAppraisal = new Portal.Appraisal_Letter();

            //    Label lblAddCORowId = (Label)e.Row.FindControl("lblCODataId");
            //    Label lblCODataId = (Label)e.Row.FindControl("lblCODataId");
            //    TextBox txtArea = (TextBox)e.Row.FindControl("txtArea");
            //    TextBox txtMetric = (TextBox)e.Row.FindControl("txtMetric");
            //    TextBox txtTarget = (TextBox)e.Row.FindControl("txtTarget");
            //    TextBox txtwt = (TextBox)e.Row.FindControl("txtwt");
            //    TextBox txtWeightage = (TextBox)e.Row.FindControl("txtWeightage");
            //    TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");
            //    objAppraisal = new Portal.Appraisal_Letter();
            //    DataSet ds = new DataSet();
            //    string column = "";
            //    if (ViewState["BUTTON"].ToString() == "1")
            //    {
            //        column = "Finanace";

            //    }
            //    if (ViewState["BUTTON"].ToString() == "2")
            //    {
            //        column = "Competitiveness";
            //    }
            //    if (ViewState["BUTTON"].ToString() == "3")
            //    {
            //        column = "Operational";
            //    }
            //    if (ViewState["BUTTON"].ToString() == "4")
            //    {
            //        column = "People";
            //    }
            //    if (ViewState["BUTTON"].ToString() == "5")
            //    {
            //        column = "FutureReadiness";
            //    }

            //    ds = objAppraisal.Fill_Apprisial((string)(Session["sEmpID"]), Convert.ToString(Session["sCompID"]), column);

            //    ViewState["Add"] = ds.Tables[0];

            //    foreach (DataRow drTax in ((DataTable)ViewState["Add"]).Rows)
            //    {
            //        if (int.Parse(drTax["CO_Row_Id"].ToString()) == int.Parse(lblAddCORowId.Text))
            //        {
            //            txtArea.Text = drTax["AREA"].ToString();
            //            txtMetric.Text = drTax["METRIC"].ToString();
            //            txtTarget.Text = drTax["TARGET"].ToString();
            //            txtRemarks.Text = drTax["Achievement_REMARKS"].ToString();
            //            txtWeightage.Text = drTax["Weightage"].ToString();
            //            txtwt.Text = drTax["TWT"].ToString();
            //            break;
            //        }
            //    }
            //}
        }
        protected void gvSteps_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvSteps.EditIndex = -1;
            ShowData();
        }
        protected void gvLIstVIew_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                double totalWeightage = 0;
                double totalCalculation = 0;
                double totalAch = 0;
                double totalScore = 0;
                double AchPer = 0;
                double AchPerPayOut = 0;
                double wht = 0;
                double morePrc = 0;

                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    GridViewRow row = gvList.Rows[i];

                    Label lblWeightage = row.FindControl("lblWeightage") as Label;
                    Label lblCalculation = row.FindControl("lblCalculation") as Label;
                    Label lblScore = row.FindControl("lblScore") as Label;
                    Label lblAchive = row.FindControl("lblAchive") as Label;

                    if (lblAchive.Text == "0")
                    {
                        wht = 0;
                    }
                    else
                    {
                        double.TryParse(RemovePercentageSymbol(lblAchive.Text), out AchPer);
                    }

                    if (lblWeightage.Text == "0")
                    {
                        wht = 0;
                    }
                    else
                    {
                        double.TryParse(RemovePercentageSymbol(lblWeightage.Text), out wht);
                    }

                    if (wht == 0)
                    {
                        AchPerPayOut = 0;
                    }
                    else
                    {
                        AchPerPayOut = 100 * AchPer / wht;
                    }

                    if (90 > AchPerPayOut)
                    {
                        lblScore.Text = Convert.ToString(wht * 0);
                        double lblScorevalue = Convert.ToDouble(lblScore.Text);
                        lblScore.Text = lblScorevalue.ToString("F2");

                        lblCalculation.Text = wht + " * 0% = " + lblScore.Text;
                    }
                    else if (90 == AchPerPayOut)
                    {
                        lblScore.Text = Convert.ToString(wht * (double)0.75);
                        double lblScorevalue = Convert.ToDouble(lblScore.Text);
                        lblScore.Text = lblScorevalue.ToString("F2");

                        lblCalculation.Text = wht + " * 75% = " + lblScore.Text;
                    }
                    else if (90 < AchPerPayOut && 100 > AchPerPayOut)
                    {
                        morePrc = AchPerPayOut - 90;
                        string morePrcvalue = morePrc.ToString("F2");
                        morePrc = double.Parse(morePrcvalue);

                        lblScore.Text = Convert.ToString(wht * ((double)0.75 + morePrc * (double)0.025));
                        double lblScorevalue = Convert.ToDouble(lblScore.Text);
                        lblScore.Text = lblScorevalue.ToString("F2");

                        lblCalculation.Text = wht + " * (75% + " + morePrc + " * 2.5%) = " + lblScore.Text;
                    }
                    else if (100 == AchPerPayOut)
                    {
                        lblScore.Text = Convert.ToString(wht * (double)1.00);
                        double lblScorevalue = Convert.ToDouble(lblScore.Text);
                        lblScore.Text = lblScorevalue.ToString("F2");

                        lblCalculation.Text = wht + " * 100% = " + lblScore.Text;
                    }
                    else if (100 < AchPerPayOut && 111 > AchPerPayOut)
                    {
                        morePrc = AchPerPayOut - 100;
                        string morePrcvalue = morePrc.ToString("F2");
                        morePrc = double.Parse(morePrcvalue);

                        lblScore.Text = Convert.ToString(wht * ((double)1.00 + morePrc * (double)0.05));
                        double lblScorevalue = Convert.ToDouble(lblScore.Text);
                        lblScore.Text = lblScorevalue.ToString("F2");

                        lblCalculation.Text = wht + " * (100% + " + morePrc + " * 5%) = " + lblScore.Text;

                    }
                    else
                    {
                        lblScore.Text = Convert.ToString(wht * (double)1.5);
                        double lblScorevalue = Convert.ToDouble(lblScore.Text);
                        lblScore.Text = lblScorevalue.ToString("F2");

                        lblCalculation.Text = wht + " * 150% = " + lblScore.Text;
                    }

                    double weightage = 0;
                    double calculation = 0;
                    double Ach = 0;
                    double Score = 0;

                    if (lblWeightage != null)
                    {
                        double.TryParse(RemovePercentageSymbol(lblWeightage.Text), out weightage);
                        totalWeightage += weightage;
                    }

                    if (lblCalculation != null)
                    {
                        double.TryParse(RemovePercentageSymbol(lblCalculation.Text), out calculation);
                        totalCalculation += calculation;
                    }

                    if (lblAchive != null)
                    {
                        double.TryParse(RemovePercentageSymbol(lblAchive.Text), out Ach);
                        totalAch += Ach;
                    }

                    if (lblScore != null)
                    {
                        double.TryParse(RemovePercentageSymbol(lblScore.Text), out Score);
                        totalScore += Score;
                    }

                }

                // Find the footer row and update the labels
                Label lblTotalWeightage = e.Row.FindControl("lblTotalWeightage") as Label;
                Label lblTotalCalculation = e.Row.FindControl("lblTotalCalculation") as Label;
                Label lblTotalAchive = e.Row.FindControl("lblTotalAchive") as Label;
                Label lblTotalSCORE = e.Row.FindControl("lblTotalSCORE") as Label;

                if (lblTotalWeightage != null && lblTotalCalculation != null)
                {
                    lblTotalWeightage.Visible = true;
                    lblTotalCalculation.Visible = true;
                    lblTotalWeightage.Text = totalWeightage.ToString();
                    lblTotalAchive.Text = totalAch.ToString();
                    lblTotalSCORE.Text = totalScore.ToString();

                }
            }
        }

        private string RemovePercentageSymbol(string value)
        {
            return value.TrimEnd('%');
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["EmpCode"] != null)
                {

                    Response.Redirect("ApprovalKey_Accomploshments.aspx?EmpCode=" + (string)(ViewState["EmpCode"]), true);
                }
                else
                {
                    Response.Redirect("ApprovalKey_Accomploshments.aspx", true);

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            mv.SetActiveView(vwList);
            //btnPrev.Visible = false;
            Fill_Details("1", gvLIstVIew.PageSize.ToString());
        }

        protected void btnUploadRM_Click(object sender, EventArgs e)
        {
            try
            {
                if (fupldDocument.PostedFile != null)
                {
                    if (Convert.ToString(fupldDocument.PostedFile.FileName) == "")
                    {
                        lblMessageList.Text = "Browse file to upload.";
                        //ShowMessage("Browse file to upload.", WarningType.Danger);
                        return;
                    }
                    else
                    {
                        //UPLOAD FILE ON SERVER
                        UploadFileRM();
                    }
                }
                else
                {
                    lblMessageList.Visible = true;
                    lblMessageList.Text = "Browse file to upload.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageList.Visible = true;
                lblMessageList.Text = (ex.Message);
            }
        }

        private void UploadFileRM()
        {
            string guid;
            string path;
            string message;

            guid = Convert.ToString(Guid.NewGuid());
            guid = guid + fupldDocument.FileName;
            path = Request.PhysicalApplicationPath.ToString() + "EXCEL";
            path = path + guid;
            fupldDocument.SaveAs(path + guid);


            System.IO.Stream fileInputStream = fupldDocument.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));

            System.IO.FileStream fStream = new System.IO.FileStream(path, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write((Byte[])fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();


            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();
            string Column = "";

            //objAppraisal.XML = MakeDetailsXml();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.SECTION = "1";
            objAppraisal.Status = "6";

            message = objAppraisal.NewUploadEMPKRARM(path, Request.PhysicalApplicationPath.ToString(), Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));


            divAlertListView.Visible = true;
            lblMessageListView.Text = message;
            divAlertListView.Visible = true;
            lblMessageListView.Text = message;
            if (message == "Successfuly uploaded")
            {
                objcommon = new NewPortal2023.ESS.Common();
                divAlertListView.Visible = true;
                string script = $@"<script type='text/javascript'>alert('Saved Successfully');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlertListView, "success");
                lblMessageListView.Text = "Saved Successfully.";

                divKRAUploadHR.Visible = false;
                Fill_Details("1", gvList.PageSize.ToString());

            }
            else
            {
                divAlertListView.Visible = true;
                string script = $@"<script type='text/javascript'>alert('Total weightage exceeded than given limit');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlertListView, "danger");
                lblMessageListView.Text = message;
            }


            System.IO.File.Delete(path);
        }

        protected void btnUploadHR_Click(object sender, EventArgs e)
        {
            try
            {
                if (fupldDocument.PostedFile != null)
                {
                    if (Convert.ToString(fupldDocument.PostedFile.FileName) == "")
                    {
                        lblMessageList.Text = "Browse file to upload.";
                        //ShowMessage("Browse file to upload.", WarningType.Danger);
                        return;
                    }
                    else
                    {
                        //UPLOAD FILE ON SERVER
                        UploadFileHR();
                    }
                }
                else
                {
                    lblMessageList.Visible = true;
                    lblMessageList.Text = "Browse file to upload.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageList.Visible = true;
                lblMessageList.Text = (ex.Message);
            }
        }

        private void UploadFileHR()
        {
            string guid;
            string path;
            string message;

            guid = Convert.ToString(Guid.NewGuid());
            guid = guid + fupldDocument.FileName;
            path = Request.PhysicalApplicationPath.ToString() + "EXCEL";
            path = path + guid;
            fupldDocument.SaveAs(path + guid);


            System.IO.Stream fileInputStream = fupldDocument.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));

            System.IO.FileStream fStream = new System.IO.FileStream(path, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write((Byte[])fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();


            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();
            string Column = "";

            //objAppraisal.XML = MakeDetailsXml();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.SECTION = "1";
            objAppraisal.Status = "3";
            objAppraisal.Cycle = ViewState["CYCLE"].ToString();
            objAppraisal.CycleAid = ViewState["CYCLE_AID"].ToString();

            message = objAppraisal.NewUploadEMPKRAHR(path, Request.PhysicalApplicationPath.ToString(), Convert.ToString(Session["sCompID"]));


            divAlertListView.Visible = true;
            lblMessageListView.Text = message;
            divAlertListView.Visible = true;
            lblMessageListView.Text = message;
            if (message == "Successfuly uploaded")
            {
                objcommon = new NewPortal2023.ESS.Common();
                divAlertListView.Visible = true;
                string script = $@"<script type='text/javascript'>alert('Saved Successfully');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlertListView, "success");
                lblMessageListView.Text = "Saved Successfully.";

                divKRAUploadHR.Visible = false;
                Fill_Details("1", gvList.PageSize.ToString());

            }
            else
            {
                divAlertListView.Visible = true;
                string script = $@"<script type='text/javascript'>alert('Total weightage exceeded than given limit');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlertListView, "danger");
                lblMessageListView.Text = message;
            }


            System.IO.File.Delete(path);
        }

        protected void btnSampleKRA_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageListView.Text = "";


                string openFilePath = Request.PhysicalApplicationPath.ToString() +
                          Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" +
                          Convert.ToString(Session["sCompAID"]) + "\\PMS\\" + "\\KRA\\" + "KRA_UPLOAD.xlsx";

                System.IO.FileInfo fileObj = new System.IO.FileInfo(openFilePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=KRA_UPLOAD.xlsx");
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // Excel MIME type
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }


            }
            catch (Exception ex)
            {
                lblMessageListView.Text = "Error occurred in application.";
                lblMessageListView.BackColor = System.Drawing.Color.Tomato;
                lblMessageListView.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void lnkEmpList_Click(object sender, EventArgs e)
        {
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            ds = objAppraisal.GettAllEmployeeAppraisalList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

            string rptEmp_Code = (string)(Session["sEmpCode"]);
            string extension;
            string encoding;
            string contentType;
            string[] streamIds;
            Warning[] warnings;

            rptPrint.Visible = true;
            ReportDataSource datasource = new ReportDataSource("dsKRAApproval", ds.Tables[0]);

            rptPrint.LocalReport.DataSources.Clear();
            rptPrint.ProcessingMode = ProcessingMode.Local;
            rptPrint.LocalReport.ReportPath = @"Reports\KRA_UPLOAD.rdlc";
            rptPrint.LocalReport.DataSources.Add(datasource);
            rptPrint.LocalReport.Refresh();

            byte[] bytes = rptPrint.LocalReport.Render(
                "Excel",
                "<DeviceInfo><SheetName>KRA_UPLOAD</SheetName></DeviceInfo>",
                out contentType,
                out encoding,
                out extension,
                out streamIds,
                out warnings
            );

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = ContentType;
            string filename = "KRA_Approval(All Employees Under" + " " + rptEmp_Code + ").xls";
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();



        }

        private string MakeDetailsXmlAll()
        {
            System.Text.StringBuilder sbCODetails = new System.Text.StringBuilder();
            string xmlCO = string.Empty;
            objcommon = new NewPortal2023.ESS.Common();

            sbCODetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sbCODetails.Append("<ROOT>");

            foreach (GridViewRow gvr in this.gvList.Rows)
            {
                string appAID = objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblAid")).Text);
                string area = objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblArea")).Text);
                string metric = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtMetric")).Text);
                string target = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("lblTARGET")).Text);
                string weightageText = objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblWeightage")).Text);
                string MidYrReviewText = objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblMidYrReview")).Text);
                string MidYrReviewRMText = objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblMidYrReviewRM")).Text);
                string achivePerText = objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblAchivedWeightage")).Text);
                string modiMDRecom = objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblApprComm")).Text);
                string remarks = objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblRemarks")).Text);

                decimal weightage = 0;
                decimal achivePer = 0;
                decimal achAgainstWht = 0;

                if (weightageText != "")
                {
                    if (!decimal.TryParse(weightageText, out weightage))
                    {
                        return "Weightage must be a valid numerical value.";
                    }
                }

                if (achivePerText != "")
                {
                    if (!decimal.TryParse(achivePerText, out achivePer))
                    {
                        return "Acheivement Percentage must be a valid numerical value.";
                    }
                }

                if (decimal.TryParse(weightageText, out decimal weightageval) && decimal.TryParse(achivePerText, out decimal achPerval))
                {
                    achAgainstWht = weightageval * achPerval / 100;
                }
                else
                {
                    achAgainstWht = 0;
                }

                sbCODetails.Append("<CO APPRAISAL_AID='" + appAID + "'");
                sbCODetails.Append(" AREA='" + area + "'");
                sbCODetails.Append(" METRIC='" + metric + "'");
                sbCODetails.Append(" TARGET='" + target + "'");
                sbCODetails.Append(" WEIGHTAGE='" + weightage + "'");
                sbCODetails.Append(" MIDYEARRIVIEW='" + MidYrReviewText + "'");
                sbCODetails.Append(" MIDYEARRIVIEWRM='" + MidYrReviewRMText + "'");
                sbCODetails.Append(" ACHIEVEDWEIGHTAGEPERCT='" + achivePer + "'");
                sbCODetails.Append(" ACHIEVEMENTPERCT='" + achAgainstWht.ToString() + "'");
                sbCODetails.Append(" MODIMDRECOM='" + modiMDRecom + "'");
                sbCODetails.Append(" REMARKS='" + remarks + "'/>");
            }

            sbCODetails.Append("</ROOT>");

            xmlCO = sbCODetails.ToString();

            return xmlCO;
        }
        protected void btnSubmitDetails_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    DataSet ds = new DataSet();
                    ViewState["BUTTON"] = "";
                    string Column = "";
                    DataSet dsInv = new DataSet();

                    objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();

                    objAppraisal.XML = MakeDetailsXmlAll();

                    if (objAppraisal.XML == "Weightage must be a valid numerical value.")
                    {

                        objcommon = new NewPortal2023.ESS.Common();
                        divAlertList.Visible = true;

                        string message = "Weightage must be a valid numerical value.";
                        string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        objcommon.SetMessageColor(divAlertList, "danger");
                        lblMessageList.Text = "Weightage must be a valid numerical value.";

                        return;
                    }

                    else if (objAppraisal.XML == "Acheivement Percentage must be a valid numerical value.")
                    {

                        objcommon = new NewPortal2023.ESS.Common();
                        divAlertList.Visible = true;

                        string message = "Acheivement Percentage must be a valid numerical value.";
                        string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        objcommon.SetMessageColor(divAlertList, "danger");
                        lblMessageList.Text = "Acheivement Percentage must be a valid numerical value.";

                        return;
                    }

                    objAppraisal.Year = ViewState["Year"].ToString();
                    objAppraisal.Quarter = ViewState["Quarter"].ToString();
                    objAppraisal.Comp_Aid = Session["sCompID"].ToString();
                    objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();

                    if ((string)Session["sEmpID"] == "EP000136")        //RAJIV ARORA
                    {
                        objAppraisal.SECTION = "1";
                        objAppraisal.Flag = "RM";
                        objAppraisal.Status = "4";
                        objAppraisal.KraFlag = "1";
                        objAppraisal.EmpRptMID = (string)(Session["sEmpCode"]);
                    }
                    else
                    {
                        objAppraisal.SECTION = "1";
                        objAppraisal.Flag = "RM";
                        objAppraisal.Status = "6";
                        objAppraisal.KraFlag = "1";
                        objAppraisal.EmpRptMID = (string)(Session["sEmpCode"]);
                    }

                    ds = objAppraisal.UpdateParamApprisalRM(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Columns.Contains("MESSAGE") && ds.Tables[0].Rows[0]["MESSAGE"] != DBNull.Value)
                        {
                            objcommon = new NewPortal2023.ESS.Common();
                            divAlertList.Visible = true;
                            lblMessageList.Text = ds.Tables[0].Rows[0]["MESSAGE"].ToString();
                            objcommon.SetMessageColor(divAlertList, "danger");

                            string message = lblMessageList.Text;
                            string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                            divSteps.Visible = false;
                            EmployeeDetailsList();
                        }
                        else
                        {
                            objcommon = new NewPortal2023.ESS.Common();
                            divAlertList.Visible = true;
                            lblMessageList.Text = "Sumbitted Successfully.";
                            objcommon.SetMessageColor(divAlertList, "success");

                            string message = lblMessageList.Text;
                            string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                            SENDPMSMAIL();

                            divSteps.Visible = false;
                            EmployeeDetailsList();
                        }
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

        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            //Console.WriteLine(certificate);
            return true;
        }
        private void SENDPMSMAIL()
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

                ds = objAppraisal.GetMailidDetailsRM((string)Session["sCompID"], (string)Session["sEmpID"], makerMid);

                if (ds.Tables.Count > 0)
                {
                    Empemail = (ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["EMPMAILID"] != DBNull.Value) ? ds.Tables[0].Rows[0]["EMPMAILID"].ToString() : "techsupport@sequelgroup.co.in";
                    RMemail = (ds.Tables[1].Rows.Count > 0 && ds.Tables[1].Rows[0]["RMMAILID"] != DBNull.Value) ? ds.Tables[1].Rows[0]["RMMAILID"].ToString() : "techsupport@sequelgroup.co.in";
                    HODemail = (ds.Tables[2].Rows.Count > 0 && ds.Tables[2].Rows[0]["HODMAILID"] != DBNull.Value) ? ds.Tables[2].Rows[0]["HODMAILID"].ToString() : "techsupport@sequelgroup.co.in";
                    HRemail = (ds.Tables[3].Rows.Count > 0 && ds.Tables[3].Rows[0]["HRMAILID"] != DBNull.Value) ? ds.Tables[3].Rows[0]["HRMAILID"].ToString() : "techsupport@sequelgroup.co.in";
                }

                using (MailMessage empMail = new MailMessage())
                {
                    emailSend = new NewPortal2023.ESS.Email();

                    string subject = "Do Not Reply: PMS - KRA Submission Status";
                    string empBody =
                    "Dear Sir/Madam,<br/><br/>" +
                    "Your KRA's has been approved by your Reporting Manager and sent for HOD's approval.<br/><br/>" +
                    "We will notify you once it is approved.<br/><br/>" +
                    "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                    "<strong>Best Regards,<br/>HR Team</strong>";

                    emailSend.SendEmailNPLPMS(empMail, Empemail, subject, empBody);
                }

                using (MailMessage rmMail = new MailMessage())
                {
                    string subject = "Do Not Reply: PMS - KRA Approval Status";

                    string rmBody =
                    "Dear Sir/Madam,<br/><br/>" +
                    "This is to inform you that KRA's has been approved for <strong>Mr. " + makerName + "</strong>.<br/><br/>" +
                    "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                    "Thank you for your prompt attention to this matter.<br/><br/>" +
                    "<strong>With Best Regards,<br/>HR Team</strong>";

                    emailSend.SendEmailNPLPMS(rmMail, RMemail, subject, rmBody);
                }

                using (MailMessage hodMail = new MailMessage())
                {
                    string subject = "Do Not Reply: PMS - KRA Approval Request";

                    string hodBody =
                    "Dear Sir/Madam,<br/><br/>" +
                    "A new KRA Approval request for <strong>Mr. " + makerName + "</strong> has been received.<br/><br/>" +
                    "Please log in to the portal at your earliest convenience and take the necessary action to proceed with the process.<br/><br/>" +
                    "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                    "Thank you for your prompt attention to this matter.<br/><br/>" +
                    "<strong>With Best Regards,<br/>HR Team</strong>";

                    emailSend.SendEmailNPLPMS(hodMail, HODemail, subject, hodBody);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

            }
        }

        protected void lnkManual_Click(object sender, EventArgs e)
        {
            try
            {
                System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/npl/PMS_Guidelines.pdf"));
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=PMS_Guidelines.pdf");
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
            }
            catch (Exception ex)
            {

            }
        }

    }
}