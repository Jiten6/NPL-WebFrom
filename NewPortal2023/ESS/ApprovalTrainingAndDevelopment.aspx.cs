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
    public partial class ApprovalTrainingAndDevelopment : System.Web.UI.Page
    {
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();

        DataSet dsInv = new DataSet();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sCompID"] != null)
            {
                if (!Page.IsPostBack)
                {
                    try
                    {
                        Get_EmployeeType();
                        //GetKraFlagDetails();

                        FillFinYear();
                        Fill_Details("1", gvLIstVIew.PageSize.ToString());
                        mv.SetActiveView(vwListView);
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
            BtnSaveChanges.Visible = true;
            btnClose.Visible = true;
        }

        private void hideControls()
        {
            BtnSaveChanges.Visible = false;
            btnClose.Visible = false;
        }

        private void Fill_Details(string index, string size)
        {
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            //int currentYear = DateTime.Now.Year;
            //string currentYearString = currentYear.ToString();
            //ViewState["Year"] = currentYearString;

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            ViewState["Year"] = drpFinancialYear.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpCode = Session["sEmpID"].ToString();
            objAppraisal.EmpAID = Session["sEmpID"].ToString();
            objAppraisal.Flag = "RM";

            ds = objAppraisal.Fill_ApprovalTrainingAndDevList((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]));

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows[0]["FLAG"].ToString() == "RM")
                {
                    this.gvLIstVIew.DataSource = ds;
                    this.gvLIstVIew.DataBind();
                    mv.SetActiveView(vwListView);
                }
                else
                {
                    this.gvLIstVIew.DataSource = null;
                    this.gvLIstVIew.DataBind();
                }
            }
            else
            {
                this.gvLIstVIew.DataSource = null;
                this.gvLIstVIew.DataBind();
                mv.SetActiveView(vwListView);
            }
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
                divAction.Visible = false;

            }

        }

        private void Get_EmployeeType()
        {
            //objAppraisal.Flag = "PMS";
            //ds = objAppraisal.GetEmployeeType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

            //if (ds.Tables[0].Rows[0]["TYPE"].ToString() == "RM")
            //{
            //    divAction.Visible = true;
            //}
            //else if (ds.Tables[0].Rows[0]["TYPE"].ToString() == "HOD")
            //{
            //    divAction.Visible = true;
            //}
            //else if ((ds.Tables[0].Rows[0]["TYPE"].ToString() == "HOD" || ds.Tables[0].Rows[0]["TYPE"].ToString() == "CEO") && (string)(Session["sEmpCode"]) == "NP3349")
            //{
            //    divAction.Visible = true;
            //}
            //else if (ds.Tables[0].Rows[0]["TYPE"].ToString() == "HR")
            //{
            //    divAction.Visible = true;
            //}
            //else
            //{
            //    divAction.Visible = false;
            //    foreach (GridViewRow row in GVSELF.Rows)
            //    {
            //        TextBox txtRptRmk = row.FindControl("txtRptRmk") as TextBox;

            //        txtRptRmk.Enabled = false;
            //    }
            //}

            divAction.Visible = true;
        }

        private DataTable CreateBindAdditionTable()
        {
            DataTable dtCOInfo = new DataTable();

            dtCOInfo.Columns.Add(new DataColumn("CO_Row_Id"));
            dtCOInfo.Columns.Add(new DataColumn("List down the training programs attended during the year"));
            dtCOInfo.Columns.Add(new DataColumn("Elaborate on how exposure to these programs has improved effectiveness in your work area"));
            dtCOInfo.Columns.Add(new DataColumn("Appraisers comments"));
            dtCOInfo.Columns.Add(new DataColumn("HR Remarks"));

            dtCOInfo.Columns.Add(new DataColumn("Doc_Data_Id"));

            return dtCOInfo;
        }

        protected void grTrainDev_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        /// <summary>
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        protected void GVSELF_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GVSELF.EditIndex = -1;
            GVSELFShowData();
        }

        protected void GVSELF_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void GVSELF_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GVSELF.EditIndex = e.NewEditIndex;
            GVSELFShowData();
        }

        protected void GVSELF_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void GVSELFShowData()
        {

            if (ViewState["BUTTON"].ToString() == "1")
            {

                this.GVSELF.DataSource = (DataTable)ViewState["FunTechSkill"];
                this.GVSELF.DataBind();
            }
            else if (ViewState["BUTTON"].ToString() == "2")
            {

                this.GVSELF.DataSource = (DataTable)ViewState["ManBehaSkill"];
                this.GVSELF.DataBind();
            }
            else if (ViewState["BUTTON"].ToString() == "3")
            {

                this.GVSELF.DataSource = (DataTable)ViewState["Apprisiation"];
                this.GVSELF.DataBind();
            }

            //this.GVSELF.DataSource = (DataTable)ViewState["GVSELFAddBind"];
            //this.GVSELF.DataBind();

        }

        private DataTable GVSELF_CreateBindTable()
        {
            DataTable dtCOInfo = new DataTable();

            dtCOInfo.Columns.Add(new DataColumn("CO_Row_Id"));
            dtCOInfo.Columns.Add(new DataColumn("selfAssM"));
            dtCOInfo.Columns.Add(new DataColumn("REMARKS"));
            dtCOInfo.Columns.Add(new DataColumn("AssMAppriser"));
            dtCOInfo.Columns.Add(new DataColumn("SelfHRRmk"));
            dtCOInfo.Columns.Add(new DataColumn("Doc_Data_Id"));

            return dtCOInfo;
        }

        protected void linkAddRow_Click(object sender, EventArgs e)
        {

            try
            {
                DataTable dtCOInfo = this.GVSELF_CreateBindTable();

                int rowCount = this.GVSELF.Rows.Count + 1;
                foreach (GridViewRow gvrCO in this.GVSELF.Rows)
                {
                    DataRow drCORow = dtCOInfo.NewRow();


                    drCORow["CO_Row_Id"] = rowCount;

                    drCORow["selfAssM"] = ((TextBox)gvrCO.FindControl("txtselfAssM")).Text;
                    drCORow["REMARKS"] = ((TextBox)gvrCO.FindControl("txtRmk")).Text;
                    drCORow["AssMAppriser"] = ((TextBox)gvrCO.FindControl("txtAssMAppriser")).Text;
                    drCORow["SelfHRRmk"] = ((TextBox)gvrCO.FindControl("txtSelfHRRmk")).Text;

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

                    ViewState["FunTechSkill"] = dtCOInfo;
                    ViewStateTable = (DataTable)ViewState["FunTechSkill"];
                }
                if (ViewState["BUTTON"].ToString() == "2")
                {

                    ViewState["ManBehaSkill"] = dtCOInfo;
                    ViewStateTable = (DataTable)ViewState["ManBehaSkill"];
                }

                if (ViewStateTable != null)
                {
                    this.GVSELF.DataSource = ViewStateTable;
                    this.GVSELF.DataBind();
                }

                //ViewState["AddBind"] = dtCOInfo;
                //this.gvSteps.DataSource = (DataTable)ViewState["AddBind"];
                //this.gvSteps.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkDeleteRows_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCOInfo = this.GVSELF_CreateBindTable();


                int rowCount = this.GVSELF.Rows.Count + 1;
                foreach (GridViewRow gvrCO in this.GVSELF.Rows)
                {
                    if (((CheckBox)gvrCO.FindControl("chkSelect")).Checked == false)
                    {
                        DataRow drCORow = dtCOInfo.NewRow();

                        drCORow["CO_Row_Id"] = rowCount;
                        drCORow["selfAssM"] = ((TextBox)gvrCO.FindControl("txtselfAssM")).Text;
                        drCORow["REMARKS"] = ((TextBox)gvrCO.FindControl("txtRmk")).Text;
                        drCORow["AssMAppriser"] = ((TextBox)gvrCO.FindControl("txtAssMAppriser")).Text;

                        drCORow["SelfHRRmk"] = ((TextBox)gvrCO.FindControl("txtSelfHRRmk")).Text;

                        drCORow["Doc_Data_Id"] = (((Label)gvrCO.FindControl("lblCODataId")).Text.Trim() == "" ? "0" : ((Label)gvrCO.FindControl("lblCODataId")).Text);

                        dtCOInfo.Rows.Add(drCORow);

                        rowCount++;
                    }
                }



                if (ViewState["BUTTON"].ToString() == "1")
                {
                    ViewState["FunTechSkill"] = dtCOInfo;
                    this.GVSELF.DataSource = (DataTable)ViewState["FunTechSkill"];
                    this.GVSELF.DataBind();
                }
                else if (ViewState["BUTTON"].ToString() == "2")
                {
                    ViewState["ManBehaSkill"] = dtCOInfo;
                    this.GVSELF.DataSource = (DataTable)ViewState["ManBehaSkill"];
                    this.GVSELF.DataBind();
                }


            }
            catch (Exception ex)
            {

            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            mv.SetActiveView(vwList);
            divGVSELF.Visible = false;

            Button ButtonView = (Button)sender;
            Label lblEmpName = (Label)ButtonView.NamingContainer.FindControl("lblEmp_name");
            Label lblEmpCode = (Label)ButtonView.NamingContainer.FindControl("lblEmpmid");

            Session["lblEmp_name"] = lblEmpName.Text;
            Session["lblEmpmid"] = lblEmpCode.Text;

            EmpName.Text = lblEmpName.Text;
            EmpCode.Text = lblEmpCode.Text;

            DataSet ds = new DataSet();
            string index = "1";
            string size = "25";
            string Column = "";

            //int currentYear = DateTime.Now.Year;
            //string currentYearString = currentYear.ToString();
            //ViewState["Year"] = currentYearString;

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            //objAppraisal.EmpCode = lblEmpCode.Text;
            //ViewState["EmpCode"] = lblEmpCode.Text;

            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.EmpCode = Session["sEmpCode"].ToString();
            objAppraisal.EmpMakerMID = lblEmpCode.Text;
            objAppraisal.Flag = "RM";

            ds = objAppraisal.Fill_TrainingAndDevAllApproval((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]));

            if (ds.Tables[0].Rows.Count > 0)
            {

                if (ds.Tables[1].Rows[0]["FLAG"].ToString() == "RM")
                {
                    this.gvList.DataSource = ds;
                    this.gvList.DataBind();
                }
                else
                {
                    this.gvList.DataSource = null;
                    this.gvList.DataBind();
                }
            }
            else
            {
                this.gvList.DataSource = (DataTable)ViewState["FunTechSkill"];
                this.gvList.DataBind();
            }

            divAlertList.Visible = false;

        }

        protected void btnFunTech_Click(object sender, EventArgs e)
        {
            mv.SetActiveView(vwList);
            divGVSELF.Visible = true;
            btnFunTech.BackColor = System.Drawing.Color.Green;
            btnManBehaSkill.BackColor = System.Drawing.Color.Gray;

            ViewState["BUTTON"] = "1";
            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();
            string Column = "";

            if (ViewState["BUTTON"].ToString() == "1")
            {
                Column = "FunTechSkill";
            }
            else if (ViewState["BUTTON"].ToString() == "2")
            {
                Column = "ManBehaSkill";
            }

            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.EmpCode = Session["sEmpCode"].ToString();
            objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
            objAppraisal.Flag = "RM";

            ds = objAppraisal.Fill_TrainingAndDevApproval((string)(Session["lblEmpmid"]), Convert.ToString(Session["sCompID"]), Column);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.GVSELF.DataSource = ds;
                this.GVSELF.DataBind();
                GetActivationDateStatus();
            }
            else
            {
                this.GVSELF.DataSource = null;
                this.GVSELF.DataBind();
                hideControls();
            }

        }

        protected void btnManBehaSkill_Click(object sender, EventArgs e)
        {
            mv.SetActiveView(vwList);
            divGVSELF.Visible = true;
            btnFunTech.BackColor = System.Drawing.Color.Gray;
            btnManBehaSkill.BackColor = System.Drawing.Color.Green;

            ViewState["BUTTON"] = "2";
            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();
            string Column = "";

            if (ViewState["BUTTON"].ToString() == "1")
            {
                Column = "FunTechSkill";
            }
            else if (ViewState["BUTTON"].ToString() == "2")
            {
                Column = "ManBehaSkill";
            }

            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.EmpCode = Session["sEmpCode"].ToString();
            objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
            objAppraisal.Flag = "RM";

            ds = objAppraisal.Fill_TrainingAndDevApproval((string)(Session["lblEmpmid"]), Convert.ToString(Session["sCompID"]), Column);
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.GVSELF.DataSource = ds.Tables[0];
                this.GVSELF.DataBind();
                GetActivationDateStatus();
            }
            else
            {
                this.GVSELF.DataSource = null;
                this.GVSELF.DataBind();
                hideControls();
            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            divGVSELF.Visible = false;
            GetActivationDateStatus();
            EmployeeDetailsList();
            mv.SetActiveView(vwList);
        }

        protected void EmployeeDetailsList()
        {
            DataSet ds = new DataSet();
            string index = "1";
            string size = "25";
            string Column = "";

            //int currentYear = DateTime.Now.Year;
            //string currentYearString = currentYear.ToString();
            //ViewState["Year"] = currentYearString;

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpCode = Session["sEmpCode"].ToString();
            objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
            objAppraisal.Flag = "RM";

            ds = objAppraisal.Fill_TrainingAndDevAllApproval((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]));

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[1].Rows[0]["FLAG"].ToString() == "RM")
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
            else
            {
                this.gvList.DataSource = null;
                this.gvList.DataBind();

                Fill_Details("1", gvLIstVIew.PageSize.ToString());
                mv.SetActiveView(vwListView);
            }
        }

        protected void BtnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    DataSet ds = new DataSet();
                    string Column = "";
                    objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                    objcommon = new NewPortal2023.ESS.Common();

                    if (ViewState["BUTTON"].ToString() == "1")
                    {
                        Column = "FunTechSkill";
                    }
                    else if (ViewState["BUTTON"].ToString() == "2")
                    {
                        Column = "ManBehaSkill";
                    }


                    //int currentYear = DateTime.Now.Year;
                    //string currentYearString = currentYear.ToString();
                    //ViewState["Year"] = currentYearString;

                    ViewState["Quarter"] = drpQuarter.SelectedValue;
                    objAppraisal.Quarter = ViewState["Quarter"].ToString();
                    objAppraisal.Year = ViewState["Year"].ToString();
                    objAppraisal.Comp_Aid = Session["sCompID"].ToString();
                    objAppraisal.EmpCode = Session["sEmpCode"].ToString();
                    objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();

                    objAppraisal.Flag = "RM";

                    ds = objAppraisal.Fill_TrainingAndDevApproval((string)(Session["lblEmpmid"]), Convert.ToString(Session["sCompID"]), Column);

                    if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "3")
                    {
                        if ((string)Session["sEmpID"] == "EP000136")        //RAJIV ARORA
                        {
                            objAppraisal.Flag = "RM";
                            objAppraisal.Status = "4";
                            objAppraisal.KraFlag = "1";
                            objAppraisal.EmpRptMID = (string)(Session["sEmpCode"]);
                        }
                        else
                        {
                            objAppraisal.Flag = "RM";
                            objAppraisal.Status = "6";
                            objAppraisal.KraFlag = "1";
                            objAppraisal.EmpRptMID = (string)(Session["sEmpCode"]);
                        }
                    }

                    objAppraisal.XML = MakeXml();
                    objAppraisal.Year = ViewState["Year"].ToString();
                    objAppraisal.Quarter = ViewState["Quarter"].ToString();
                    objAppraisal.SECTION = "3";

                    ds = objAppraisal.UpdateParamApprisalTraninAndDev(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Columns.Contains("RESULT") && ds.Tables[0].Rows[0]["RESULT"] != DBNull.Value)
                        {
                            objcommon = new NewPortal2023.ESS.Common();
                            divAlertList.Visible = true;
                            lblMessageList.Text = "Action Successfully Submitted.";
                            objcommon.SetMessageColor(divAlertList, "success");

                            string message = lblMessageList.Text;
                            string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                            SENDPMSMAIL(Column);

                            divGVSELF.Visible = false;
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

        private string MakeXml()
        {
            System.Text.StringBuilder sbCODetails = new System.Text.StringBuilder();
            string xmlCO = string.Empty;
            objcommon = new NewPortal2023.ESS.Common();

            sbCODetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sbCODetails.Append("<ROOT>");

            //if (!chkAbsent.Checked)
            //{
            foreach (GridViewRow gvr in this.GVSELF.Rows)
            {

                sbCODetails.Append("<CO AID='" + ((Label)gvr.FindControl("lblAddCORowId")).Text + "'");
                sbCODetails.Append(" selfAssM='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtselfAssM")).Text) + "'");
                sbCODetails.Append(" AssMAppriser='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtAssMAppriser")).Text) + "'");
                sbCODetails.Append(" REMARKS='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtSelfHRRmk")).Text) + "'/>");
            }
            //}

            sbCODetails.Append("</ROOT>");

            xmlCO = sbCODetails.ToString();

            return xmlCO;
        }

        protected void drpQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["Quarter"] = drpQuarter.SelectedValue;
                Fill_Details("1", GVSELF.PageSize.ToString());
            }

            catch (Exception ex)
            {

            }
        }

        protected void btnPrev_Click(object sender, EventArgs e)
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

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["EmpCode"] != null)
                {

                    Response.Redirect("ApprovalCompetenciesPage.aspx?EmpCode=" + (string)(ViewState["EmpCode"]), true);
                }
                else
                {
                    Response.Redirect("ApprovalCompetenciesPage.aspx", true);

                }
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

        private void SENDPMSMAIL(string Column)
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

                if (Column == "FunTechSkill")
                {
                    Column = "Functional / Technical Skills";
                }
                else if (Column == "ManBehaSkill")
                {
                    Column = "Managerial / Behavioral Skills";
                }

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

                    string subject = "Do Not Reply: PMS - Training And Development Submission Status";
                    string empBody =
                    "Dear Sir/Madam,<br/><br/>" +
                    "Your Training And Development for " + Column + " has been approved by your Reporting Manager and sent for HOD's approval.<br/><br/>" +
                    "We will notify you once it is approved.<br/><br/>" +
                    "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                    "<strong>Best Regards,<br/>HR Team</strong>";

                    emailSend.SendEmailNPLPMS(empMail, Empemail, subject, empBody);
                }

                using (MailMessage rmMail = new MailMessage())
                {
                    string subject = "Do Not Reply: PMS - Training And Development Approval Status";

                    string rmBody =
                    "Dear Sir/Madam,<br/><br/>" +
                    "This is to inform you that Training And Development for " + Column + " has been approved for <strong>Mr. " + makerName + "</strong>.<br/><br/>" +
                    "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                    "Thank you for your prompt attention to this matter.<br/><br/>" +
                    "<strong>With Best Regards,<br/>HR Team</strong>";

                    emailSend.SendEmailNPLPMS(rmMail, RMemail, subject, rmBody);
                }

                using (MailMessage hodMail = new MailMessage())
                {
                    string subject = "Do Not Reply: PMS - Training And Development Approval Request";

                    string hodBody =
                    "Dear Sir/Madam,<br/><br/>" +
                    "A new Approval request for Training And Development for " + Column + " for <strong>Mr. " + makerName + "</strong> has been received.<br/><br/>" +
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
    }
}