using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class CompanyFactor : System.Web.UI.Page
    {
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    mv.SetActiveView(vwList);
                    Fill_Details("1", gvList.PageSize.ToString());
                    divSteps.Visible = false;
                }
                catch (Exception ex)
                {


                }

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
        //private void CalculateAndDisplayTotals()
        //{
        //    decimal totalWeightage = 0;
        //    decimal totalCalculation = 0;

        //    foreach (GridViewRow row in gvList.Rows)
        //    {
        //        Label lblWeightage = (Label)row.FindControl("lblWeightage");
        //        Label lblCalculation = (Label)row.FindControl("lblCalculation");

        //        if (lblWeightage != null && lblCalculation != null)
        //        {
        //            decimal weightage = 0;
        //            decimal calculation = 0;

        //            // Parse values and calculate totals
        //            decimal.TryParse(lblWeightage.Text, out weightage);
        //            decimal.TryParse(lblCalculation.Text, out calculation);

        //            totalWeightage += weightage;
        //            totalCalculation += calculation;
        //        }
        //    }

        //    // Bind totals to the footer labels
        //    Label lblTotalWeightage = (Label)gvList.FooterRow.FindControl("lblTotalWeightage");
        //    Label lblTotalCalculation = (Label)gvList.FooterRow.FindControl("lblTotalCalculation");

        //    if (lblTotalWeightage != null && lblTotalCalculation != null)
        //    {
        //        lblTotalWeightage.Text = totalWeightage.ToString();
        //        lblTotalCalculation.Text = totalCalculation.ToString();
        //    }
        //}

        private void Fill_Details(string index, string size)
        {
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            int currentYear = DateTime.Now.Year;
            string currentYearString = currentYear.ToString();
            ViewState["Quarter"] = drpQuarter.SelectedValue;
            ViewState["Year"] = currentYearString;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = currentYearString;
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
                        drCORow["Achievement_Perct"] = ((TextBox)gvrCO.FindControl("txtAchPer")).Text;

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
                    drCORow["Achievement_Perct"] = ((TextBox)gvrCO.FindControl("txtAchPer")).Text;
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
            dtCOInfo.Columns.Add(new DataColumn("Achievement_Perct"));
            dtCOInfo.Columns.Add(new DataColumn("Appraisal_AID"));
            dtCOInfo.Columns.Add(new DataColumn("Doc_Data_Id"));

            return dtCOInfo;
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
                objAppraisal.EmpMakerMID = Session["sEmpCode"].ToString();

                ds = objAppraisal.Fill_Apprisial((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "Finanace");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.gvSteps.DataSource = ds.Tables[0];
                    this.gvSteps.DataBind();
                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["Finanace"];
                    this.gvSteps.DataBind();
                }
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
                objAppraisal.EmpMakerMID = Session["sEmpCode"].ToString();

                ds = objAppraisal.Fill_Apprisial((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "Competitiveness");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.gvSteps.DataSource = ds.Tables[0];
                    this.gvSteps.DataBind();

                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["Competitiveness"];
                    this.gvSteps.DataBind();
                }
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
                objAppraisal.EmpMakerMID = Session["sEmpCode"].ToString();

                ds = objAppraisal.Fill_Apprisial((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "Operational");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.gvSteps.DataSource = ds.Tables[0];
                    this.gvSteps.DataBind();
                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["Operational"];
                    this.gvSteps.DataBind();
                }
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
                objAppraisal.EmpMakerMID = Session["sEmpCode"].ToString();

                ds = objAppraisal.Fill_Apprisial((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "People");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    this.gvSteps.DataSource = ds.Tables[0];
                    this.gvSteps.DataBind();
                }
                else
                {
                    this.gvSteps.DataSource = (DataTable)ViewState["People"];
                    this.gvSteps.DataBind();
                }
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
            objAppraisal.EmpMakerMID = Session["sEmpCode"].ToString();

            ds = objAppraisal.Fill_Apprisial((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "FutureReadiness");
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.gvSteps.DataSource = ds.Tables[0];
                this.gvSteps.DataBind();
            }
            else
            {
                this.gvSteps.DataSource = (DataTable)ViewState["FutureReadiness"];
                this.gvSteps.DataBind();
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
                Column = "Finanace";
            }
            else if (ViewState["BUTTON"].ToString() == "2")
            {
                Column = "Competitiveness";
            }
            else if (ViewState["BUTTON"].ToString() == "3")
            {
                Column = "Operational";
            }
            else if (ViewState["BUTTON"].ToString() == "4")
            {
                Column = "People";
            }
            else if (ViewState["BUTTON"].ToString() == "5")
            {
                Column = "FutureReadiness";
            }

            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.EmpMakerMID = Session["sEmpCode"].ToString();

            ds = objAppraisal.Fill_Apprisial((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), Column);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.gvSteps.DataSource = ds.Tables[0];
                this.gvSteps.DataBind();
            }
            else
            {
                this.gvSteps.DataSource = (DataTable)ViewState["FutureReadiness"];
                this.gvSteps.DataBind();
            }
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            CheckWeightage();

            DataSet ds = new DataSet();
            string Column = "";
            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            //SaveMutipleDetaisl();
            if (ViewState["BUTTON"].ToString() == "1")
            {
                Column = "Finanace";
            }
            else if (ViewState["BUTTON"].ToString() == "2")
            {
                Column = "Competitiveness";
            }
            else if (ViewState["BUTTON"].ToString() == "3")
            {
                Column = "Operational";
            }
            else if (ViewState["BUTTON"].ToString() == "4")
            {
                Column = "People";
            }
            else if (ViewState["BUTTON"].ToString() == "5")
            {
                Column = "FutureReadiness";
            }

            objAppraisal.XML = MakeDetailsXml();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.SECTION = "1";
            objAppraisal.Status = "3";

            ds = objAppraisal.GetApproverAid((string)(Session["sCompID"]), (string)(Session["sEmpCode"]));

            objAppraisal.ApprovalAid = ds.Tables[0].Rows[0]["EMP_APPR_AID"].ToString();

            ds = objAppraisal.CreateUpdateParamApprisal(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "1");

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["MESSAGE"].ToString() == "Weightage Exceed")
                {
                    objcommon = new NewPortal2023.ESS.Common();
                    divAlert.Visible = true;
                    //string script = $@"<script type='text/javascript'>alert('Total weightage exceeded than given limit');</script>";
                    string message = "Total weightage exceeded than given limit in " + $"'{Column}'";
                    string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    objcommon.SetMessageColor(divAlert, "danger");
                    lblMessage.Text = "Total weightage exceeded than given limit in" + " " + "'" + Column + "'" + " ";

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
                    Fill_Details("1", gvList.PageSize.ToString());
                }
            }
            else
            {

            }

        }

        private string MakeDetailsXml()
        {
            System.Text.StringBuilder sbCODetails = new System.Text.StringBuilder();
            string xmlCO = string.Empty;
            objcommon = new NewPortal2023.ESS.Common();

            sbCODetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sbCODetails.Append("<ROOT>");

            //if (!chkAbsent.Checked)
            //{
            foreach (GridViewRow gvr in this.gvSteps.Rows)
            {
                sbCODetails.Append("<CO Appraisal_AID='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtaid")).Text) + "'");
                sbCODetails.Append(" Metric='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtMetric")).Text) + "'");
                sbCODetails.Append(" Target='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtTarget")).Text) + "'");
                sbCODetails.Append(" twt='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtwt")).Text) + "'");
                sbCODetails.Append(" Weightage='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtWeightage")).Text) + "'");
                sbCODetails.Append(" REMARKS='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text) + "'");
                sbCODetails.Append(" AchPer='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtAchPer")).Text) + "'/>");
            }

            sbCODetails.Append("</ROOT>");

            xmlCO = sbCODetails.ToString();

            return xmlCO;
        }

        protected void CheckWeightage()
        {
            foreach (GridViewRow gvr in this.gvSteps.Rows)
            {
                //string whtg = gvr.FindControl("txtWeightage").ToString();
                TextBox txtWeightage = gvr.FindControl("txtWeightage") as TextBox;
                string whtg = txtWeightage.Text;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            mv.SetActiveView(vwList);
            Fill_Details("1", gvList.PageSize.ToString());
            divSteps.Visible = false;
        }

        //private void SaveMutipleDetaisl()
        //{
        //    objAppraisal = new Portal.Appraisal_Letter();
        //    DataSet dsIns = new DataSet();
        //    DataTable dt = new DataTable();


        //    if (gvSteps.Rows.Count > 0)
        //    {
        //        int rowCount = gvSteps.Rows.Count;
        //        int i = 0;
        //        for (i = 0; i < gvSteps.Rows.Count; i++)
        //        {

        //            if (((TextBox)gvSteps.Rows[i].Cells[3].FindControl("txtArea")).Text == "&nbsp;")
        //            {
        //                objAppraisal.Area = "";

        //            }
        //            else
        //            {
        //                objAppraisal.Area = ((TextBox)gvSteps.Rows[i].Cells[3].FindControl("txtArea")).Text;
        //            }
        //            if (((TextBox)gvSteps.Rows[i].Cells[4].FindControl("txtMetric")).Text == "&nbsp;")
        //            {

        //                objAppraisal.Metric = "";
        //                break;
        //            }
        //            else
        //            {

        //                objAppraisal.Metric = ((TextBox)gvSteps.Rows[i].Cells[4].FindControl("txtMetric")).Text;
        //            }
        //            if (((TextBox)gvSteps.Rows[i].Cells[5].FindControl("txtTarget")).Text == "&nbsp;")
        //            {

        //                objAppraisal.Target = "";
        //            }
        //            else
        //            {

        //                objAppraisal.Target = ((TextBox)gvSteps.Rows[i].Cells[5].FindControl("txtTarget")).Text;
        //            }
        //            if (((TextBox)gvSteps.Rows[i].Cells[6].FindControl("txtwt")).Text == "&nbsp;")
        //            {
        //                objAppraisal.wt = "";
        //            }
        //            else
        //            {
        //                objAppraisal.wt = ((TextBox)gvSteps.Rows[i].Cells[6].FindControl("txtwt")).Text;
        //            }
        //            if (((TextBox)gvSteps.Rows[i].Cells[6].FindControl("txtWeightage")).Text == "&nbsp;")
        //            {
        //                objAppraisal.Weightage = "";
        //            }
        //            else
        //            {
        //                objAppraisal.Weightage = ((TextBox)gvSteps.Rows[i].Cells[6].FindControl("txtWeightage")).Text;
        //            }
        //            if (((TextBox)gvSteps.Rows[i].Cells[6].FindControl("txtRemarks")).Text == "&nbsp;")
        //            {
        //                objAppraisal.Remarks = "";
        //            }
        //            else
        //            {
        //                objAppraisal.Remarks = ((TextBox)gvSteps.Rows[i].Cells[6].FindControl("txtRemarks")).Text;
        //            }
        //        }

        //        dsIns = objAppraisal.UpdateGLTransactionId();
        //        string countTable = dsIns.Tables.Count.ToString();
        //        if (dsIns.Tables.Count > 3)
        //        {

        //            if (dsIns != null)
        //            {
        //                string Glid = "";
        //                if (Convert.ToInt16(countTable) > 3)
        //                {
        //                    if (Convert.ToInt16(dsIns.Tables[1].Rows[0]["COUNTS"].ToString()) > 0)
        //                    {
        //                        if (dsIns.Tables[2].Rows.Count > 0)
        //                        {
        //                            for (int j = 0; j < dsIns.Tables[2].Rows.Count; j++)
        //                            {
        //                                if (j <= dsIns.Tables[2].Rows.Count)
        //                                {
        //                                    Glid = Glid + dsIns.Tables[2].Rows[j]["GL_CODE"].ToString() + ",";
        //                                }
        //                                else
        //                                {
        //                                    break;
        //                                }
        //                            }


        //                        }
        //                    }
        //                }
        //            }

        //        }
        //        else
        //        {

        //        }
        //    }


        //}



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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                decimal totalWeightage = 0;
                decimal totalCalculation = 0;
                decimal totalScore = 0;
                decimal AchPer = 0;
                decimal AchPerPayOut = 0;
                decimal wht = 0;
                decimal morePrc = 0;
                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    GridViewRow row = gvList.Rows[i];

                    Label lblWeightage = row.FindControl("lblWeightage") as Label;
                    Label lblCalculation = row.FindControl("lblCalculation") as Label;
                    Label lblScore = row.FindControl("lblScore") as Label;
                    Label lblAchive = row.FindControl("lblAchive") as Label;

                    decimal.TryParse(RemovePercentageSymbol(lblAchive.Text), out AchPer); /*AchPer =Convert.ToDecimal(lblAchive.Text);*/
                    decimal.TryParse(RemovePercentageSymbol(lblWeightage.Text), out wht);
                    //wht = Convert.ToDecimal(lblWeightage.Text);

                    AchPerPayOut = 100 * AchPer / wht;

                    if (75 > AchPerPayOut)
                    {
                        lblScore.Text = Convert.ToString(wht * 0);
                        lblCalculation.Text = wht + " * 0%";
                    }
                    else if (75 == AchPerPayOut)
                    {
                        lblScore.Text = Convert.ToString(wht * (decimal)0.5);
                        lblCalculation.Text = wht + " * 50%";
                    }
                    else if (75 < AchPerPayOut && 100 > AchPerPayOut)
                    {
                        morePrc = AchPerPayOut - 75;
                        //decimal s = wht * (decimal)1.00;
                        //decimal s101= wht * (decimal)1.00 + 5 * (decimal)0.05;
                        //decimal s150 = wht * (decimal)1.5;
                        lblScore.Text = Convert.ToString(wht * ((decimal)0.5 + morePrc * (decimal)0.02));
                        //lblScore.Text = (wht * 50.0 / 100) + (morePrc * 2.0 / 100);
                        lblCalculation.Text = wht + " * 50% + " + morePrc + " * 2%";
                    }
                    else if (100 == AchPerPayOut)
                    {
                        lblScore.Text = Convert.ToString(wht * (decimal)1.00);
                        lblCalculation.Text = wht + " * 100%";
                    }
                    else if (100 < AchPerPayOut && 111 > AchPerPayOut)
                    {
                        morePrc = AchPerPayOut - 100;
                        lblScore.Text = Convert.ToString(wht * (decimal)1.00 + morePrc * (decimal)0.05);
                        lblCalculation.Text = wht + " * 100% + " + morePrc + " * 5%";
                    }
                    else
                    {
                        lblScore.Text = Convert.ToString(wht * (decimal)1.5);
                        lblCalculation.Text = wht + " * 150%";
                    }

                    decimal weightage = 0;
                    decimal calculation = 0;
                    decimal Score = 0;

                    if (lblWeightage != null)
                    {
                        decimal.TryParse(RemovePercentageSymbol(lblWeightage.Text), out weightage);
                        totalWeightage += weightage;
                    }

                    if (lblCalculation != null)
                    {
                        decimal.TryParse(RemovePercentageSymbol(lblCalculation.Text), out calculation);
                        totalCalculation += calculation;
                    }
                    if (lblScore != null)
                    {
                        decimal.TryParse(RemovePercentageSymbol(lblScore.Text), out Score);
                        totalScore += Score;
                    }




                }

                // Find the footer row and update the labels
                Label lblTotalWeightage = e.Row.FindControl("lblTotalWeightage") as Label;
                Label lblTotalCalculation = e.Row.FindControl("lblTotalCalculation") as Label;
                Label lblTotalSCORE = e.Row.FindControl("lblTotalSCORE") as Label;

                if (lblTotalWeightage != null && lblTotalCalculation != null)
                {
                    lblTotalWeightage.Visible = true;
                    lblTotalCalculation.Visible = true;
                    lblTotalWeightage.Text = totalWeightage.ToString();
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
                Response.Redirect("Key_Accomploshments.aspx", true);
            }
            catch (Exception EX)
            {

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

            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();
            string Column = "";

            if (ViewState["BUTTON"].ToString() == "1")
            {
                Column = "Finanace";
            }
            else if (ViewState["BUTTON"].ToString() == "2")
            {
                Column = "Competitiveness";
            }
            else if (ViewState["BUTTON"].ToString() == "3")
            {
                Column = "Operational";
            }
            else if (ViewState["BUTTON"].ToString() == "4")
            {
                Column = "People";
            }
            else if (ViewState["BUTTON"].ToString() == "5")
            {
                Column = "FutureReadiness";
            }

            objAppraisal.XML = MakeDetailsXml();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.SECTION = "1";
            objAppraisal.Status = "3";

            ds = objAppraisal.GetApproverAid((string)(Session["sCompID"]), (string)(Session["sEmpCode"]));

            objAppraisal.ApprovalAid = ds.Tables[0].Rows[0]["EMP_APPR_AID"].ToString();

            message = objAppraisal.NewInsertKRA(path, Request.PhysicalApplicationPath.ToString(), Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), Column);

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
    }
}