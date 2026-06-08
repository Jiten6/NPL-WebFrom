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
using System.Net.Mail;
using System.IO.Compression;
using System.Net;
using Microsoft.Reporting.WebForms;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Drawing.Charts;
using DataTable = System.Data.DataTable;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
//using Xamarin.Forms;
//using System.Data.OleDb;

namespace NewPortal2023.ESS
{
    public partial class EmployeeTaskDetails : System.Web.UI.Page
    {
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();

        DateTime deactivationDate;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    divSteps.Visible = false;
                    divKRAUpload.Visible = false;
                    FillFinYear();
                    mv.SetActiveView(vwList);
                }
                catch (Exception ex)
                {

                }

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
                Response.Redirect("~/ESS/EmployeeTaskDetails.aspx", true);
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
            mv.SetActiveView(vwList);
        }
        private void GetActivationDateStatus()
        {
            DataSet dsExp = new DataSet();

            ViewState["STATUS"] = "";
            ViewState["ACCESSTYPE"] = "";

            objPmsHr.Year = ViewState["Year"].ToString();

            dsExp = objPmsHr.GetPMSEmpFlag((string)Session["sCompID"], (string)Session["sEmpID"]);

            if (dsExp.Tables[0].Rows.Count > 0)
            {
                if (dsExp.Tables[0].Rows[0]["KRA_FLAG_STATUS"].ToString() == "EMPLOYEE")
                {
                    DataSet ds = new DataSet();

                    ViewState["ACCESSTYPE"] = "EMPLOYEE";
                    ViewState["Quarter"] = drpQuarter.SelectedValue;
                    objAppraisal.Quarter = ViewState["Quarter"].ToString();
                    objAppraisal.Year = ViewState["Year"].ToString();
                    objAppraisal.Comp_Aid = Session["sCompID"].ToString();
                    objAppraisal.EmpCode = Session["sEmpCode"].ToString();

                    ds = objAppraisal.Fill_ApprisialLIst();

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        //For Status 3d
                        bool allStatus3d = ds.Tables[0].AsEnumerable()
                                             .All(row => row["STATUS"].ToString() == "3d");

                        if (allStatus3d)
                        {
                            ViewState["STATUS"] = "3d";
                        }
                        else
                        {
                            ViewState["STATUS"] = "";
                        }

                        //For Status 3
                        bool allStatus3 = ds.Tables[0].AsEnumerable()
                                            .All(row => row["STATUS"].ToString() == "3");

                        if (allStatus3)
                        {
                            btnRecall.Visible = true;
                        }
                        else
                        {
                            btnRecall.Visible = false;
                        }


                        bool shouldShowControls = false;

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string status = row["STATUS"].ToString();
                            if (status == "7" || status == "3d" || status == "0")
                            {
                                shouldShowControls = true;
                                break;
                            }
                        }

                        if (shouldShowControls)
                        {
                            showControls();
                        }
                        else
                        {
                            hideControls();
                            btnSubmitDetails.Visible = false;
                        }
                    }
                    else
                    {
                        ViewState["STATUS"] = "N";
                        showControls();
                        btnSubmitDetails.Visible = false;
                    }
                }
                else
                {
                    ViewState["ACCESSTYPE"] = "APPROVER";
                    hideControls();
                    btnSubmitDetails.Visible = false;
                }
            }

        }

        private void showControls()
        {
            BtnSave.Visible = true;
            btnClose.Visible = true;
            btnSubmitDetails.Visible = true;

            if ((string)ViewState["CYCLE_AID"] == "C0")
            {
                if (ViewState["STATUS"].ToString() == "3d" || ViewState["STATUS"].ToString() == "N")
                {
                    divKRAUpload.Visible = true;
                }
                else
                {
                    divKRAUpload.Visible = false;
                }

                gvSteps.Columns[0].Visible = true;
                divAddRow.Visible = true;
            }
            else
            {
                divKRAUpload.Visible = false;
                gvSteps.Columns[0].Visible = false;
                divAddRow.Visible = false;
            }
        }
        private void hideControls()
        {
            gvSteps.Columns[0].Visible = false;
            divAddRow.Visible = false;
            BtnSave.Visible = false;
            btnClose.Visible = false;
            divKRAUpload.Visible = false;
            //btnSubmitDetails.Visible = false;
        }

        private void Fill_Details(string index, string size)
        {
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpCode = Session["sEmpCode"].ToString();

            ds = objAppraisal.Fill_ApprisialLIst();

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

        protected void drpQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["Quarter"] = drpQuarter.SelectedValue;
                Fill_Details("1", gvList.PageSize.ToString());
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
                    drCORow["Appraisal_AID"] = ((Label)gvrCO.FindControl("lblAid")).Text;
                    drCORow["AREA"] = ((Label)gvrCO.FindControl("lblArea")).Text;
                    drCORow["METRIC"] = ((TextBox)gvrCO.FindControl("txtMetric")).Text;
                    drCORow["TARGET"] = ((TextBox)gvrCO.FindControl("txtTarget")).Text;
                    drCORow["TWT"] = ((TextBox)gvrCO.FindControl("txtwt")).Text;
                    drCORow["Weightage"] = ((TextBox)gvrCO.FindControl("txtWeightage")).Text;
                    drCORow["MIDYEARPROGRESS"] = ((TextBox)gvrCO.FindControl("txtMidYrReview")).Text;
                    drCORow["AchievedWeightage_Perct"] = ((TextBox)gvrCO.FindControl("txtAchPer")).Text;
                    drCORow["Achievement_REMARKS"] = ((TextBox)gvrCO.FindControl("txtRemarks")).Text;
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

                GetCyclePhaseDetails();
            }
            catch (Exception ex)
            {
                // Handle the exception here
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

                        drCORow["AREA"] = ((Label)gvrCO.FindControl("lblArea")).Text;
                        drCORow["METRIC"] = ((TextBox)gvrCO.FindControl("txtMetric")).Text;
                        drCORow["TARGET"] = ((TextBox)gvrCO.FindControl("txtTarget")).Text;
                        drCORow["TWT"] = ((TextBox)gvrCO.FindControl("txtwt")).Text;
                        drCORow["Weightage"] = ((TextBox)gvrCO.FindControl("txtWeightage")).Text;
                        drCORow["MIDYEARPROGRESS"] = ((TextBox)gvrCO.FindControl("txtMidYrReview")).Text;
                        drCORow["AchievedWeightage_Perct"] = ((TextBox)gvrCO.FindControl("txtAchPer")).Text;
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
               
                GetCyclePhaseDetails();
            }
            catch (Exception ex)
            {

            }
        }

        private DataTable CreateBindAdditionTable()
        {
            DataTable dtCOInfo = new DataTable();

            dtCOInfo.Columns.Add(new DataColumn("CO_Row_Id"));
            dtCOInfo.Columns.Add(new DataColumn("Appraisal_AID"));
            dtCOInfo.Columns.Add(new DataColumn("AREA"));
            dtCOInfo.Columns.Add(new DataColumn("METRIC"));
            dtCOInfo.Columns.Add(new DataColumn("TARGET"));
            dtCOInfo.Columns.Add(new DataColumn("TWT"));
            dtCOInfo.Columns.Add(new DataColumn("Weightage"));
            dtCOInfo.Columns.Add(new DataColumn("MIDYEARPROGRESS"));
            dtCOInfo.Columns.Add(new DataColumn("AchievedWeightage_Perct"));
            dtCOInfo.Columns.Add(new DataColumn("Achievement_REMARKS"));
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
                        TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                        TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                        TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                        TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        TextBox txtAchPer = (TextBox)row.FindControl("txtAchPer");

                        txtTarget.ReadOnly = false;
                        txtMetric.ReadOnly = false;
                        txtWeightage.ReadOnly = false;
                        txtMidYrReview.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtAchPer.ReadOnly = true;

                        gvSteps.Columns[0].Visible = true;
                        divAddRow.Visible = true;
                    }
                }
                else if (ds.Tables[0].Rows[0]["PHASE_AID"].ToString() == "PH00002")     //MIDYEAR
                {
                    foreach (GridViewRow row in gvSteps.Rows)
                    {
                        TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                        TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                        TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                        TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        TextBox txtAchPer = (TextBox)row.FindControl("txtAchPer");

                        txtTarget.ReadOnly = true;
                        txtMetric.ReadOnly = true;
                        txtWeightage.ReadOnly = true;
                        txtMidYrReview.ReadOnly = false;
                        txtRemarks.ReadOnly = true;
                        txtAchPer.ReadOnly = true;

                        gvSteps.Columns[0].Visible = false;
                        divAddRow.Visible = false;
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvSteps.Rows)
                    {
                        TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                        TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                        TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                        TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        TextBox txtAchPer = (TextBox)row.FindControl("txtAchPer");

                        txtTarget.ReadOnly = true;
                        txtMetric.ReadOnly = true;
                        txtWeightage.ReadOnly = true;
                        txtMidYrReview.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtAchPer.ReadOnly = true;

                        gvSteps.Columns[0].Visible = false;
                        divAddRow.Visible = false;
                    }
                }
            }
            else if ((string)ViewState["CYCLE_AID"] == "C1")
            {
                if (ds.Tables[0].Rows[0]["PHASE_AID"].ToString() == "PH00003")      //APPRAISAL
                {
                    foreach (GridViewRow row in gvSteps.Rows)
                    {
                        TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                        TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                        TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                        TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        TextBox txtAchPer = (TextBox)row.FindControl("txtAchPer");

                        txtTarget.ReadOnly = true;
                        txtMetric.ReadOnly = true;
                        txtWeightage.ReadOnly = true;
                        txtMidYrReview.ReadOnly = true;
                        txtRemarks.ReadOnly = false;
                        txtAchPer.ReadOnly = false;

                        gvSteps.Columns[0].Visible = false;
                        divAddRow.Visible = false;
                    }
                }
                else if (ds.Tables[0].Rows[0]["PHASE_AID"].ToString() == "PH00004")     //END
                {
                    foreach (GridViewRow row in gvSteps.Rows)
                    {
                        TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                        TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                        TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                        TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        TextBox txtAchPer = (TextBox)row.FindControl("txtAchPer");

                        txtTarget.ReadOnly = true;
                        txtMetric.ReadOnly = true;
                        txtWeightage.ReadOnly = true;
                        txtMidYrReview.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtAchPer.ReadOnly = true;

                        gvSteps.Columns[0].Visible = false;
                        divAddRow.Visible = false;
                    }
                }
                else
                {
                    foreach (GridViewRow row in gvSteps.Rows)
                    {
                        TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                        TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                        TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                        TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                        TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                        TextBox txtAchPer = (TextBox)row.FindControl("txtAchPer");

                        txtTarget.ReadOnly = true;
                        txtMetric.ReadOnly = true;
                        txtWeightage.ReadOnly = true;
                        txtMidYrReview.ReadOnly = true;
                        txtRemarks.ReadOnly = true;
                        txtAchPer.ReadOnly = true;

                        gvSteps.Columns[0].Visible = false;
                        divAddRow.Visible = false;
                    }
                }
            }
            else
            {
                foreach (GridViewRow row in gvSteps.Rows)
                {
                    TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                    TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                    TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                    TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    TextBox txtAchPer = (TextBox)row.FindControl("txtAchPer");

                    txtTarget.ReadOnly = true;
                    txtMetric.ReadOnly = true;
                    txtWeightage.ReadOnly = true;
                    txtMidYrReview.ReadOnly = true;
                    txtRemarks.ReadOnly = true;
                    txtAchPer.ReadOnly = true;

                    gvSteps.Columns[0].Visible = false;
                    divAddRow.Visible = false;
                }
            }
        }
        protected void btnFinancials_Click(object sender, EventArgs e)
        {
            try
            {
                GetActivationDateStatus();

                divSteps.Visible = true;
                divKRAUpload.Visible = false;

                btnFinancials.BackColor = System.Drawing.Color.Green;
                btnCompetitiveness.BackColor = System.Drawing.Color.Gray;
                btnOperationalExcellence.BackColor = System.Drawing.Color.Gray;
                btnPeople.BackColor = System.Drawing.Color.Gray;
                btnFutureReadiness.BackColor = System.Drawing.Color.Gray;

                ViewState["BUTTON"] = "1";
                ViewState["REJECTSTATUS"] = "";

                objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                DataSet ds = new DataSet();

                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.EmpMakerMID = Session["sEmpCode"].ToString();

                ds = objAppraisal.Fill_ApprisialEmp((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "Financials");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if ((string)ds.Tables[0].Rows[0]["STATUS"] == "7" || (string)ds.Tables[0].Rows[0]["STATUS"] == "3d" || (string)ds.Tables[0].Rows[0]["STATUS"] == "0")
                    {
                        ViewState["REJECTSTATUS"] = "1";

                        if ((string)ViewState["ACCESSTYPE"] == "EMPLOYEE")
                        {
                            showControls();
                            this.gvSteps.DataSource = ds.Tables[0];
                            this.gvSteps.DataBind();
                        }
                        else if ((string)ViewState["ACCESSTYPE"] == "APPROVER")
                        {
                            hideControls();
                            this.gvSteps.DataSource = ds.Tables[0];
                            this.gvSteps.DataBind();
                        }
                    }
                    else
                    {
                        hideControls();
                        this.gvSteps.DataSource = ds.Tables[0];
                        this.gvSteps.DataBind();
                    }
                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["Financials"];
                    this.gvSteps.DataBind();
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
                GetActivationDateStatus();

                divSteps.Visible = true;
                divKRAUpload.Visible = false;

                btnFinancials.BackColor = System.Drawing.Color.Gray;
                btnCompetitiveness.BackColor = System.Drawing.Color.Green;
                btnOperationalExcellence.BackColor = System.Drawing.Color.Gray;
                btnPeople.BackColor = System.Drawing.Color.Gray;
                btnFutureReadiness.BackColor = System.Drawing.Color.Gray;

                ViewState["BUTTON"] = "2";
                ViewState["REJECTSTATUS"] = "";

                objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                DataSet ds = new DataSet();

                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.EmpMakerMID = Session["sEmpCode"].ToString();

                ds = objAppraisal.Fill_ApprisialEmp((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "Competitiveness");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if ((string)ds.Tables[0].Rows[0]["STATUS"] == "7" || (string)ds.Tables[0].Rows[0]["STATUS"] == "3d" || (string)ds.Tables[0].Rows[0]["STATUS"] == "0")
                    {
                        ViewState["REJECTSTATUS"] = "1";

                        if ((string)ViewState["ACCESSTYPE"] == "EMPLOYEE")
                        {
                            showControls();
                            this.gvSteps.DataSource = ds.Tables[0];
                            this.gvSteps.DataBind();
                        }
                        else if ((string)ViewState["ACCESSTYPE"] == "APPROVER")
                        {
                            hideControls();
                            this.gvSteps.DataSource = ds.Tables[0];
                            this.gvSteps.DataBind();
                        }
                    }
                    else
                    {
                        hideControls();
                        this.gvSteps.DataSource = ds.Tables[0];
                        this.gvSteps.DataBind();
                    }

                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["Competitiveness"];
                    this.gvSteps.DataBind();
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
                GetActivationDateStatus();

                divSteps.Visible = true;
                divKRAUpload.Visible = false;

                btnFinancials.BackColor = System.Drawing.Color.Gray;
                btnCompetitiveness.BackColor = System.Drawing.Color.Gray;
                btnOperationalExcellence.BackColor = System.Drawing.Color.Green;
                btnPeople.BackColor = System.Drawing.Color.Gray;
                btnFutureReadiness.BackColor = System.Drawing.Color.Gray;

                ViewState["BUTTON"] = "3";
                ViewState["REJECTSTATUS"] = "";

                objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                DataSet ds = new DataSet();

                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.EmpMakerMID = Session["sEmpCode"].ToString();

                ds = objAppraisal.Fill_ApprisialEmp((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "Operational Excellence");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if ((string)ds.Tables[0].Rows[0]["STATUS"] == "7" || (string)ds.Tables[0].Rows[0]["STATUS"] == "3d" || (string)ds.Tables[0].Rows[0]["STATUS"] == "0")
                    {
                        ViewState["REJECTSTATUS"] = "1";

                        if ((string)ViewState["ACCESSTYPE"] == "EMPLOYEE")
                        {
                            showControls();
                            this.gvSteps.DataSource = ds.Tables[0];
                            this.gvSteps.DataBind();
                        }
                        else if ((string)ViewState["ACCESSTYPE"] == "APPROVER")
                        {
                            hideControls();
                            this.gvSteps.DataSource = ds.Tables[0];
                            this.gvSteps.DataBind();
                        }
                    }
                    else
                    {
                        hideControls();
                        this.gvSteps.DataSource = ds.Tables[0];
                        this.gvSteps.DataBind();
                    }
                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["Operational Excellence"];
                    this.gvSteps.DataBind();
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
                GetActivationDateStatus();

                divSteps.Visible = true;
                divKRAUpload.Visible = false;

                btnFinancials.BackColor = System.Drawing.Color.Gray;
                btnCompetitiveness.BackColor = System.Drawing.Color.Gray;
                btnOperationalExcellence.BackColor = System.Drawing.Color.Gray;
                btnPeople.BackColor = System.Drawing.Color.Green;
                btnFutureReadiness.BackColor = System.Drawing.Color.Gray;

                ViewState["BUTTON"] = "4";
                ViewState["REJECTSTATUS"] = "";

                objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                DataSet ds = new DataSet();

                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.EmpMakerMID = Session["sEmpCode"].ToString();

                ds = objAppraisal.Fill_ApprisialEmp((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "People");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if ((string)ds.Tables[0].Rows[0]["STATUS"] == "7" || (string)ds.Tables[0].Rows[0]["STATUS"] == "3d" || (string)ds.Tables[0].Rows[0]["STATUS"] == "0")
                    {
                        ViewState["REJECTSTATUS"] = "1";

                        if ((string)ViewState["ACCESSTYPE"] == "EMPLOYEE")
                        {
                            showControls();
                            this.gvSteps.DataSource = ds.Tables[0];
                            this.gvSteps.DataBind();
                        }
                        else if ((string)ViewState["ACCESSTYPE"] == "APPROVER")
                        {
                            hideControls();
                            this.gvSteps.DataSource = ds.Tables[0];
                            this.gvSteps.DataBind();
                        }
                    }
                    else
                    {
                        hideControls();
                        this.gvSteps.DataSource = ds.Tables[0];
                        this.gvSteps.DataBind();
                    }
                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["People"];
                    this.gvSteps.DataBind();
                }

                GetCyclePhaseDetails();
            }
            catch (Exception ex)
            {


            }
        }
        protected void btnFutureReadiness_Click(object sender, EventArgs e)
        {
            try
            {
                GetActivationDateStatus();

                divSteps.Visible = true;
                divKRAUpload.Visible = false;

                btnFinancials.BackColor = System.Drawing.Color.Gray;
                btnCompetitiveness.BackColor = System.Drawing.Color.Gray;
                btnOperationalExcellence.BackColor = System.Drawing.Color.Gray;
                btnPeople.BackColor = System.Drawing.Color.Gray;
                btnFutureReadiness.BackColor = System.Drawing.Color.Green;

                ViewState["BUTTON"] = "5";
                ViewState["REJECTSTATUS"] = "";

                objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                DataSet ds = new DataSet();

                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.EmpMakerMID = Session["sEmpCode"].ToString();

                ds = objAppraisal.Fill_ApprisialEmp((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "Future Readiness");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if ((string)ds.Tables[0].Rows[0]["STATUS"] == "7" || (string)ds.Tables[0].Rows[0]["STATUS"] == "3d" || (string)ds.Tables[0].Rows[0]["STATUS"] == "0")
                    {
                        ViewState["REJECTSTATUS"] = "1";

                        if ((string)ViewState["ACCESSTYPE"] == "EMPLOYEE")
                        {
                            showControls();
                            this.gvSteps.DataSource = ds.Tables[0];
                            this.gvSteps.DataBind();
                        }
                        else if ((string)ViewState["ACCESSTYPE"] == "APPROVER")
                        {
                            hideControls();
                            this.gvSteps.DataSource = ds.Tables[0];
                            this.gvSteps.DataBind();
                        }
                    }
                    else
                    {
                        hideControls();
                        this.gvSteps.DataSource = ds.Tables[0];
                        this.gvSteps.DataBind();
                    }
                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["Future Readiness"];
                    this.gvSteps.DataBind();
                }

                GetCyclePhaseDetails();
            }
            catch (Exception ex)
            {
                throw;
            }
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
            dtSubCaInfo.Rows[row.DataItemIndex]["txtAchPer"] = ((TextBox)gvSteps.Rows[index].Cells[8].FindControl("txtAchPer")).Text;
            //dtSubCaInfo.Rows[row.DataItemIndex]["txtBusRationaleAdd"] = ((TextBox)gvSteps.Rows[index].Cells[8].FindControl("txtBusRationaleAdd")).Text;
            //dtSubCaInfo.Rows[row.DataItemIndex]["txtNatureOfAcAdd"] = ((TextBox)gvSteps.Rows[index].Cells[9].FindControl("txtNatureOfAcAdd")).Text;
            // dtSubCaInfo.Rows[row.DataItemIndex]["txtaid"] = ((TextBox)gvSteps.Rows[index].Cells[9].FindControl("txtaid")).Text;
            dtSubCaInfo.Rows[row.DataItemIndex]["lblCODataId"] = ((Label)gvSteps.Rows[index].Cells[9].FindControl("lblCODataId")).Text;

            dtSubCaInfo.Rows[row.DataItemIndex]["lblAddCORowId"] = ((Label)gvSteps.Rows[index].Cells[9].FindControl("lblAddCORowId")).Text;

            gvSteps.EditIndex = -1;
            ShowData();

        }
        protected void MergeRowsButton_Click(object sender, EventArgs e)
        {

        }

        protected void CurrentIndividualPromiseData()
        {
            DataSet ds = new DataSet();
            string Column = "";
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
            else
            {
                Column = "";
            }


            if (Column != "")
            {
                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.EmpMakerMID = Session["sEmpCode"].ToString();

                ds = objAppraisal.Fill_ApprisialEmp((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), Column);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "7" || ds.Tables[0].Rows[0]["STATUS"].ToString() == "3d")
                    {
                        showControls();
                        btnSubmitDetails.Visible = true;
                    }
                    else if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "0")
                    {
                        showControls();
                        btnSubmitDetails.Visible = true;
                    }
                    else
                    {
                        hideControls();
                        btnSubmitDetails.Visible = false;
                    }

                    this.gvSteps.DataSource = ds.Tables[0];
                    this.gvSteps.DataBind();
                }
                else
                {
                    this.gvSteps.DataSource = null;
                    this.gvSteps.DataBind();
                }
            }
            else
            {
                divSteps.Visible = false;
            }

            if ((string)ViewState["CYCLE_AID"] == "C1")
            {
                foreach (GridViewRow row in gvSteps.Rows)
                {
                    TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                    TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                    TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                    TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    TextBox txtAchPer = (TextBox)row.FindControl("txtAchPer");

                    txtTarget.ReadOnly = true;
                    txtMetric.ReadOnly = true;
                    txtWeightage.ReadOnly = true;
                    txtRemarks.ReadOnly = false;
                    txtAchPer.ReadOnly = false;
                }
            }
            else
            {
                foreach (GridViewRow row in gvSteps.Rows)
                {
                    TextBox txtTarget = (TextBox)row.FindControl("txtTarget");
                    TextBox txtMetric = (TextBox)row.FindControl("txtMetric");
                    TextBox txtWeightage = (TextBox)row.FindControl("txtWeightage");
                    TextBox txtMidYrReview = (TextBox)row.FindControl("txtMidYrReview");
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");
                    TextBox txtAchPer = (TextBox)row.FindControl("txtAchPer");

                    txtTarget.ReadOnly = false;
                    txtMetric.ReadOnly = false;
                    txtWeightage.ReadOnly = false;
                    txtRemarks.ReadOnly = true;
                    txtAchPer.ReadOnly = true;
                }
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
                string achPerText = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtAchPer")).Text);
                string remarks = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text);

                decimal weightage = 0;
                decimal achPer = 0;
                decimal achAgainstWht = 0;

                if (weightageText != "")
                {
                    if (!decimal.TryParse(weightageText, out weightage))
                    {
                        return "Weightage must be a valid numerical value.";
                    }
                }

                if (achPerText != "")
                {
                    if (!decimal.TryParse(achPerText, out achPer))
                    {
                        return "Acheived Weightage Percentage must be a valid numerical value.";
                    }
                }

                if (decimal.TryParse(weightageText, out decimal weightageval) && decimal.TryParse(achPerText, out decimal achPerval))
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
                sbCODetails.Append(" ACHPER='" + achPer + "'");
                sbCODetails.Append(" ACHAGNSTWHT='" + achAgainstWht.ToString() + "'");
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
                DataSet ds = new DataSet();
                string Column = "";
                objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                //SaveMutipleDetaisl();
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
                    divAlert.Visible = true;

                    string message = "Weightage must be a valid numerical value.";
                    string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    objcommon.SetMessageColor(divAlert, "danger");
                    lblMessage.Text = "Weightage must be a valid numerical value.";

                    return;
                }

                else if (objAppraisal.XML == "Acheivement Percentage must be a valid numerical value.")
                {

                    objcommon = new NewPortal2023.ESS.Common();
                    divAlert.Visible = true;

                    string message = "Acheivement Percentage must be a valid numerical value.";
                    string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    objcommon.SetMessageColor(divAlert, "danger");
                    lblMessage.Text = "Acheivement Percentage must be a valid numerical value.";

                    return;
                }

                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.SECTION = "1";

                //if (ViewState["REJECTSTATUS"].ToString() == "1")
                //{
                //    objAppraisal.Status = "3";
                //}
                //else
                //{
                //    objAppraisal.Status = "3d";
                //}

                objAppraisal.Status = "3d";

                objAppraisal.RejectStatus = ViewState["REJECTSTATUS"].ToString();
                objAppraisal.Cycle = ViewState["CYCLE"].ToString();
                objAppraisal.CycleAid = ViewState["CYCLE_AID"].ToString();

                ds = objAppraisal.CreateUpdateParamApprisal(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "1");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Columns.Contains("MESSAGE") && ds.Tables[0].Rows[0]["MESSAGE"] != DBNull.Value)
                    {
                        objcommon = new NewPortal2023.ESS.Common();
                        divAlert.Visible = true;

                        string message = ds.Tables[0].Rows[0]["MESSAGE"].ToString();
                        string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        objcommon.SetMessageColor(divAlert, "danger");
                        //lblMessage.Text = "Total weightage exceed than given limit in" + " " + "'" + Column + "'" + " ";
                        lblMessage.Text = message;

                        GetActivationDateStatus();
                        CurrentIndividualPromiseData();
                        Fill_Details("1", gvList.PageSize.ToString());
                    }
                    else if (ds.Tables[0].Columns.Contains("CATMESSAGE") && ds.Tables[0].Rows[0]["CATMESSAGE"].ToString() == "Category Not Found")
                    {
                        objcommon = new NewPortal2023.ESS.Common();
                        divAlert.Visible = true;

                        string message = "Category And Designation is Not Mapped";
                        string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        objcommon.SetMessageColor(divAlert, "danger");
                        lblMessage.Text = "Category And Designation is Not Mapped";

                        GetActivationDateStatus();
                        CurrentIndividualPromiseData();
                        Fill_Details("1", gvList.PageSize.ToString());
                    }
                    else
                    {
                        objcommon = new NewPortal2023.ESS.Common();
                        divAlert.Visible = true;
                        string script = $@"<script type='text/javascript'>alert('Saved Successfully');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        objcommon.SetMessageColor(divAlert, "success");
                        lblMessage.Text = "Saved Successfully.";

                        divSteps.Visible = false;
                        GetActivationDateStatus();
                        CurrentIndividualPromiseData();
                        Fill_Details("1", gvList.PageSize.ToString());
                    }

                }
                else
                {

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
            Fill_Details("1", gvList.PageSize.ToString());
            divSteps.Visible = false;

            GetActivationDateStatus();

            //GetKraFlagDetailsExcel();
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

        private string RemovePercentageSymbol(string value)
        {
            return value.TrimEnd('%');
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
                    Label lblAchive = row.FindControl("lblAchive") as Label;
                    Label lblCalculation = row.FindControl("lblCalculation") as Label;
                    Label lblScore = row.FindControl("lblScore") as Label;
                    Label lblStatus = row.FindControl("lblStatus") as Label;


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

                        lblCalculation.Text = wht + " * 0%";
                    }
                    else if (90 == AchPerPayOut)
                    {
                        lblScore.Text = Convert.ToString(wht * (double)0.75);
                        double lblScorevalue = Convert.ToDouble(lblScore.Text);
                        lblScore.Text = lblScorevalue.ToString("F2");

                        lblCalculation.Text = wht + " * 75%";
                    }
                    else if (90 < AchPerPayOut && 100 > AchPerPayOut)
                    {
                        morePrc = AchPerPayOut - 90;
                        string morePrcvalue = morePrc.ToString("F2");
                        morePrc = double.Parse(morePrcvalue);

                        lblScore.Text = Convert.ToString(wht * ((double)0.75 + morePrc * (double)0.025));
                        double lblScorevalue = Convert.ToDouble(lblScore.Text);
                        lblScore.Text = lblScorevalue.ToString("F2");

                        lblCalculation.Text = wht + " * 75% + " + morePrc + " * 2.5%";
                    }
                    else if (100 == AchPerPayOut)
                    {
                        lblScore.Text = Convert.ToString(wht * (double)1.00);
                        double lblScorevalue = Convert.ToDouble(lblScore.Text);
                        lblScore.Text = lblScorevalue.ToString("F2");

                        lblCalculation.Text = wht + " * 100%";
                    }
                    else if (100 < AchPerPayOut && 111 > AchPerPayOut)
                    {
                        morePrc = AchPerPayOut - 100;
                        string morePrcvalue = morePrc.ToString("F2");
                        morePrc = double.Parse(morePrcvalue);

                        lblScore.Text = Convert.ToString(wht * ((double)1.00 + morePrc * (double)0.05));
                        double lblScorevalue = Convert.ToDouble(lblScore.Text);
                        lblScore.Text = lblScorevalue.ToString("F2");

                        lblCalculation.Text = wht + " * 100% + " + morePrc + " * 5%";

                    }
                    else
                    {
                        lblScore.Text = Convert.ToString(wht * (double)1.5);
                        double lblScorevalue = Convert.ToDouble(lblScore.Text);
                        lblScore.Text = lblScorevalue.ToString("F2");

                        lblCalculation.Text = wht + " * 150%";
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

                    //if (totalWeightage > (double)70)
                    //{
                    //    divAlert.Visible = true;
                    //    string script = $@"<script type='text/javascript'>alert('Total weightage exceeded than given limit');</script>";
                    //    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    //    objcommon.SetMessageColor(divAlert, "danger");
                    //    lblMessage.Text = "Total weightage exceeded than given limit.";
                    //}
                    //else
                    //{

                    //}
                }
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                if (fupldDocument.PostedFile != null)
                {
                    if (Convert.ToString(fupldDocument.PostedFile.FileName) == "")
                    {
                        lblMessage.Text = "Browse file to upload.";
                        //ShowMessage("Browse file to upload.", WarningType.Danger);
                        return;
                    }
                    else
                    {
                        //UPLOAD FILE ON SERVER
                        InsertFile();
                    }
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse file to upload.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = (ex.Message);
            }
        }

        private void InsertFile()
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
            objAppraisal.Status = "3d";
            objAppraisal.Cycle = ViewState["CYCLE"].ToString();
            objAppraisal.CycleAid = ViewState["CYCLE_AID"].ToString();

            ds = objAppraisal.GetApproverAid((string)(Session["sCompID"]), (string)(Session["sEmpCode"]));

            objAppraisal.ApprovalAid = ds.Tables[0].Rows[0]["EMP_APPR_AID"].ToString();

            message = objAppraisal.NewInsertKRA(path, Request.PhysicalApplicationPath.ToString(), Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), Column);

            divAlert.Visible = true;
            lblMessage.Text = message;
            divAlert.Visible = true;
            lblMessage.Text = message;
            if (message == "Successfuly uploaded")
            {
                objcommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                string script = $@"<script type='text/javascript'>alert('Saved Successfully');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlert, "success");
                lblMessage.Text = "Saved Successfully.";

                divKRAUpload.Visible = true;
                Fill_Details("1", gvList.PageSize.ToString());

            }
            else if (message.Contains("Weightage must be 30"))
            {
                divAlert.Visible = true;
                string script = $@"<script type='text/javascript'>alert('Total Weightage must be 30');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = "Total Weightage must be 30.";

                divKRAUpload.Visible = true;
                Fill_Details("1", gvList.PageSize.ToString());
            }
            else if (message.Contains("Weightage must be 70"))
            {
                divAlert.Visible = true;
                string script = $@"<script type='text/javascript'>alert('Total Weightage must be 70');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = "Total Weightage must be 70.";

                divKRAUpload.Visible = true;
                Fill_Details("1", gvList.PageSize.ToString());
            }
            else
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = message;
            }

            System.IO.File.Delete(path);
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fupldDocument.PostedFile != null)
                {
                    if (Convert.ToString(fupldDocument.PostedFile.FileName) == "")
                    {
                        lblMessage.Text = "Browse file to upload.";
                        //ShowMessage("Browse file to upload.", WarningType.Danger);
                        return;
                    }
                    else
                    {
                        //UPLOAD FILE ON SERVER
                        UploadFile();
                    }
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse file to upload.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Visible = true;
                lblMessage.Text = (ex.Message);
            }
        }

        private void UploadFile()
        {
            string guid;
            string path;
            string message;

            guid = Convert.ToString(Guid.NewGuid());
            guid = guid + fupldDocument.FileName;
            path = Request.PhysicalApplicationPath.ToString() + "EXCEL";
            path = path + guid;
            //fupldDocument.SaveAs(path + guid);


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

            message = objAppraisal.NewUploadEMPKRAHR(path, Request.PhysicalApplicationPath.ToString(), Convert.ToString(Session["sCompID"]));

            divAlert.Visible = true;
            lblMessage.Text = message;
            divAlert.Visible = true;
            lblMessage.Text = message;
            if (message == "Successfuly uploaded")
            {
                divAlert.Visible = true;
                lblMessage.Text = message;

            }
            else if (message.Contains("Cannot insert duplicate key row in object"))
            {
                divAlert.Visible = true;
                lblMessage.Text = "GL code cannot contain previously used numbers.";
            }
            else
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = message;
            }

            System.IO.File.Delete(path);
        }

        protected void btnSampleKRA_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";

                string openFilePath = Request.PhysicalApplicationPath.ToString() +
                Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\PMS\\" + "\\KRA\\" + "KRA_UPLOAD.xlsx";
                //Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" +
                //Convert.ToString(Session["sCompAID"]) +  "\\PMS\\" + "\\KRA\\" + "KRA_UPLOAD.xlsx";

                System.IO.FileInfo fileObj = new System.IO.FileInfo(openFilePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=KRA_UPLOAD.xlsx");
                    Response.ContentType = "text/csv";//"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; // Excel MIME type
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message; //"Error occurred in application.";
                lblMessage.BackColor = System.Drawing.Color.Tomato;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
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
                string metric = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("lblMetric")).Text);
                string target = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("lblTARGET")).Text);
                string weightageText = objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblWeightage")).Text);
                string MidYrReviewText = objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblMidYrReview")).Text);
                string achPerText = objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblAchivedWeightage")).Text);
                string remarks = objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("lblAchiveRemark")).Text);

                decimal weightage = 0;
                decimal achPer = 0;
                decimal achAgainstWht = 0;

                if (weightageText != "")
                {
                    if (!decimal.TryParse(weightageText, out weightage))
                    {
                        return "Weightage must be a valid numerical value.";
                    }
                }

                if (achPerText != "")
                {
                    if (!decimal.TryParse(achPerText, out achPer))
                    {
                        return "Acheived Weightage Percentage must be a valid numerical value.";
                    }
                }

                if (decimal.TryParse(weightageText, out decimal weightageval) && decimal.TryParse(achPerText, out decimal achPerval))
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
                sbCODetails.Append(" ACHPER='" + achPer + "'");
                sbCODetails.Append(" ACHAGNSTWHT='" + achAgainstWht.ToString() + "'");
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
                    objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();

                    objAppraisal.XML = MakeDetailsXmlAll();

                    if (objAppraisal.XML == "Weightage must be a valid numerical value.")
                    {

                        objcommon = new NewPortal2023.ESS.Common();
                        divAlert.Visible = true;

                        string message = "Weightage must be a valid numerical value.";
                        string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        objcommon.SetMessageColor(divAlert, "danger");
                        lblMessage.Text = "Weightage must be a valid numerical value.";

                        return;
                    }

                    else if (objAppraisal.XML == "Acheivement Percentage must be a valid numerical value.")
                    {

                        objcommon = new NewPortal2023.ESS.Common();
                        divAlert.Visible = true;

                        string message = "Acheivement Percentage must be a valid numerical value.";
                        string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        objcommon.SetMessageColor(divAlert, "danger");
                        lblMessage.Text = "Acheivement Percentage must be a valid numerical value.";

                        return;
                    }

                    objAppraisal.Year = ViewState["Year"].ToString();
                    objAppraisal.Quarter = ViewState["Quarter"].ToString();
                    objAppraisal.SECTION = "1";
                    objAppraisal.Status = "3";
                    objAppraisal.Cycle = ViewState["CYCLE"].ToString();
                    objAppraisal.CycleAid = ViewState["CYCLE_AID"].ToString();

                    ds = objAppraisal.CreateUpdateParamApprisal(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "1");

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Columns.Contains("MESSAGE") && ds.Tables[0].Rows[0]["MESSAGE"] != DBNull.Value)
                        {
                            objcommon = new NewPortal2023.ESS.Common();
                            divAlert.Visible = true;

                            string message = ds.Tables[0].Rows[0]["MESSAGE"].ToString();
                            string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            objcommon.SetMessageColor(divAlert, "danger");
                            lblMessage.Text = message;

                            GetActivationDateStatus();
                            CurrentIndividualPromiseData();
                            Fill_Details("1", gvList.PageSize.ToString());
                        }
                        else if (ds.Tables[0].Columns.Contains("CATMESSAGE") && ds.Tables[0].Rows[0]["CATMESSAGE"].ToString() == "Category Not Found")
                        {
                            objcommon = new NewPortal2023.ESS.Common();
                            divAlert.Visible = true;

                            string message = "Category And Designation is Not Mapped";
                            string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            objcommon.SetMessageColor(divAlert, "danger");
                            lblMessage.Text = "Category And Designation is Not Mapped";

                            GetActivationDateStatus();
                            CurrentIndividualPromiseData();
                            Fill_Details("1", gvList.PageSize.ToString());
                        }
                        else
                        {
                            objcommon = new NewPortal2023.ESS.Common();
                            divAlert.Visible = true;
                            string script = $@"<script type='text/javascript'>alert('Submitted Successfully');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            objcommon.SetMessageColor(divAlert, "success");
                            lblMessage.Text = "Submitted Successfully.";

                            SENDPMSMAIL();

                            divSteps.Visible = false;
                            GetActivationDateStatus();
                            CurrentIndividualPromiseData();
                            Fill_Details("1", gvList.PageSize.ToString());
                        }

                    }
                    else
                    {

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

                DataSet ds = new DataSet();

                ds = objAppraisal.GetMailidDetails((string)Session["sCompID"], (string)Session["sEmpID"]);

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
                    "Your KRA's has been submitted and sent to your Reporting Manager for approval.<br/><br/>" +
                    "We will notify you once it is approved.<br/><br/>" +
                    "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                    "<strong>Best Regards,<br/>HR Team</strong>";

                    emailSend.SendEmailNPLPMS(empMail, Empemail, subject, empBody);
                }

                using (MailMessage rmMail = new MailMessage())
                {
                    emailSend = new NewPortal2023.ESS.Email();

                    string subject = "Do Not Reply: PMS - KRA Approval Request";
                    string rmBody =
                    "Dear Sir/Madam,<br/><br/>" +
                    "A new 'KRA Approval' request for <strong>Mr. " + (string)Session["sEmpName"] + "</strong> has been received.<br/><br/>" +
                    "Please log in to the portal at your earliest convenience and take the necessary action to proceed with the process.<br/><br/>" +
                    "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                    "Thank you for your prompt attention to this matter.<br/><br/>" +
                    "<strong>With Best Regards,<br/>HR Team</strong>";

                    emailSend.SendEmailNPLPMS(rmMail, RMemail, subject, rmBody);
                }

            }
            catch (Exception ex)
            {
                string message = ex.Message;
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

            }
        }

        protected void btnRecall_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            ViewState["BUTTON"] = "";
            string Column = "";

            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.SECTION = "1";
            objAppraisal.Status = "3d";
            objAppraisal.Cycle = ViewState["CYCLE"].ToString();
            objAppraisal.CycleAid = ViewState["CYCLE_AID"].ToString();

            ds = objAppraisal.RecallKRAStatus(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "1");

            if (ds.Tables[0].Rows[0]["result"].ToString() == "")
            {
                string message = "Recalled Successfully";
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                divSteps.Visible = false;
                GetActivationDateStatus();
                CurrentIndividualPromiseData();
                Fill_Details("1", gvList.PageSize.ToString());
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