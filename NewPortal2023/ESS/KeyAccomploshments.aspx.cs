using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class Key_Accomploshments : System.Web.UI.Page
    {
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();

        DataSet dsInv = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    //Fill_Details("1", gvList.PageSize.ToString());
                    //Fill_ACCOMPLISHMENTSEMP();
                    FillFinYear();
                    mv.SetActiveView(vwList);
                    //CreateBindAdditionTable();
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

            GetActivationDateStatus();
            Fill_ACCOMPLISHMENTSEMP();
            Fill_Details("1", gvList.PageSize.ToString());
            mv.SetActiveView(vwList);
        }

        private void GetActivationDateStatus()
        {
            DataSet dsExp = new DataSet();

            objPmsHr.Year = ViewState["Year"].ToString();

            dsExp = objPmsHr.GetPMSEmpFlag((string)Session["sCompID"], (string)Session["sEmpID"]);

            if (dsExp.Tables[0].Rows.Count > 0)
            {
                if (dsExp.Tables[0].Rows[0]["KRA_FLAG_STATUS"].ToString() == "EMPLOYEE")
                {
                    DataSet ds = new DataSet();

                    objAppraisal.Year = ViewState["Year"].ToString();
                    objAppraisal.Comp_Aid = Session["sCompID"].ToString();
                    objAppraisal.EmpCode = Session["sEmpID"].ToString();

                    ds = objAppraisal.Fill_KeyAccList((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "KEY ACCOMPLISHMENTS");

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        bool shouldShowControls = false;

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            string status = row["STATUS"].ToString();
                            if (status == "3")
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
                        }
                    }
                        
                }
                else
                {
                    hideControls();
                }
            }
            else
            {

            }
        }

        private void showControls()
        {
            BtnSaveOne.Visible = true;
            divAddRow.Visible = true;
        }

        private void hideControls()
        {
            BtnSaveOne.Visible = false;
            divAddRow.Visible = false;
        }

        protected void Fill_ACCOMPLISHMENTSEMP()
        {
            try
            {
                GetActivationDateStatus();

                objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                DataSet ds = new DataSet();

                objAppraisal.Year = ViewState["Year"].ToString();
                ds = objAppraisal.Fill_ACCOMPLISHMENTSEMP((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "KEY ACCOMPLISHMENTS");

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if ((string)ds.Tables[0].Rows[0]["STATUS"] == "0")
                    {
                        BtnSaveOne.Visible = true;
                        divAddRow.Visible = true;
                    }

                    this.grKey.DataSource = ds.Tables[0];
                    this.grKey.DataBind();
                }
                else
                {
                    this.grKey.DataSource = null;
                    this.grKey.DataBind();

                    BtnSaveOne.Visible = false;
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void Fill_Details(string index, string size)
        {
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            //int currentYear = DateTime.Now.Year;
            //string currentYearString = currentYear.ToString();
            //ViewState["Year"] = currentYearString;

            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpCode = Session["sEmpID"].ToString();

            ds = objAppraisal.Fill_KeyAccList((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "KEY ACCOMPLISHMENTS");

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

        private void GetKraFlagDetails()
        {
            NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();
            DataSet ds = new DataSet();

            objPmsHr.Comp_Aid = Session["sCompID"].ToString();
            objPmsHr.EmpCode = Session["sEmpCode"].ToString();
            objPmsHr.Type = "KeyAccomploshments";

            ds = objPmsHr.GetKraStatus();

            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (GridViewRow row in grKey.Rows)
                {
                    TextBox txtMajor = (TextBox)row.FindControl("txtMajor");
                    TextBox txtRptRmk = (TextBox)row.FindControl("txtRptRmk");

                    if (ds.Tables[0].Rows[0]["KRA_FLAG"].ToString() == "EMPLOYEE")
                    {
                        txtMajor.ReadOnly = false;
                        txtRptRmk.ReadOnly = false;

                        BtnSaveOne.Visible = true;
                        lnkBtnAddRowKey.Visible = true;
                        Label1.Visible = true;
                        lnkBtnDeleteRowKey.Visible = true;
                    }
                    if (ds.Tables[0].Rows[0]["KRA_FLAG"].ToString() != "EMPLOYEE")
                    {
                        txtMajor.ReadOnly = true;
                        txtRptRmk.ReadOnly = true;

                        BtnSaveOne.Visible = false;
                        lnkBtnAddRowKey.Visible = false;
                        Label1.Visible = false;
                        lnkBtnDeleteRowKey.Visible = false;
                    }
                }

            }
            else
            {

            }



        }

        //protected void Fill_lESSION()
        //{
        //    try
        //    {

        //        objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        //        DataSet ds = new DataSet();

        //        objAppraisal.Year = ViewState["Year"].ToString();
        //        ds = objAppraisal.Fill_ACCOMPLISHMENTS((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "lESSION");
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            txtText.Text = ds.Tables[0].Rows[0]["Major_contributions_Achievements_not_covered_under_KRAs_for_the_year"].ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}




        //protected void GVSELF_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    GVSELF.EditIndex = -1;
        //    GVSELFShowData();
        //}

        //protected void GVSELF_RowDataBound(object sender, GridViewRowEventArgs e)
        //{

        //}

        //protected void GVSELF_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    GVSELF.EditIndex = e.NewEditIndex;
        //    GVSELFShowData();
        //}

        //protected void GVSELF_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{

        //}

        //protected void GVSELFShowData()
        //{

        //    if (ViewState["BUTTON"].ToString() == "1")
        //    {

        //        this.GVSELF.DataSource = (DataTable)ViewState["Strenth"];
        //        this.GVSELF.DataBind();
        //    }
        //    else if (ViewState["BUTTON"].ToString() == "2")
        //    {

        //        this.GVSELF.DataSource = (DataTable)ViewState["aresForImpro"];
        //        this.GVSELF.DataBind();
        //    }
        //    else if (ViewState["BUTTON"].ToString() == "3")
        //    {

        //        this.GVSELF.DataSource = (DataTable)ViewState["Apprisiation"];
        //        this.GVSELF.DataBind();
        //    }

        //    //this.GVSELF.DataSource = (DataTable)ViewState["GVSELFAddBind"];
        //    //this.GVSELF.DataBind();

        //}

        //private DataTable GVSELF_CreateBindTable()
        //{
        //    DataTable dtCOInfo = new DataTable();

        //    dtCOInfo.Columns.Add(new DataColumn("CO_Row_Id"));
        //    dtCOInfo.Columns.Add(new DataColumn("selfAssM"));
        //    dtCOInfo.Columns.Add(new DataColumn("AssMAppriser"));
        //    dtCOInfo.Columns.Add(new DataColumn("SelfHRRmk"));
        //    dtCOInfo.Columns.Add(new DataColumn("Doc_Data_Id"));

        //    return dtCOInfo;
        //}

        //protected void linkAddRow_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        int rowCount = 1;

        //        DataTable dtCOInfo = this.GVSELF_CreateBindTable();


        //        foreach (GridViewRow gvrCO in this.GVSELF.Rows)
        //        {
        //            if (((CheckBox)gvrCO.FindControl("chkSelect")).Checked == false)
        //            {
        //                DataRow drCORow = dtCOInfo.NewRow();

        //                drCORow["CO_Row_Id"] = rowCount;

        //                drCORow["selfAssM"] = ((TextBox)gvrCO.FindControl("txtselfAssM")).Text;
        //                drCORow["AssMAppriser"] = ((TextBox)gvrCO.FindControl("txtAssMAppriser")).Text;
        //                drCORow["SelfHRRmk"] = ((TextBox)gvrCO.FindControl("txtSelfHRRmk")).Text;

        //                drCORow["Doc_Data_Id"] = (((Label)gvrCO.FindControl("lblCODataId")).Text.Trim() == "" ? "0" : ((Label)gvrCO.FindControl("lblCODataId")).Text);

        //                dtCOInfo.Rows.Add(drCORow);

        //                rowCount++;
        //            }
        //        }

        //        DataRow drNewCORow = dtCOInfo.NewRow();
        //        drNewCORow["CO_Row_Id"] = rowCount;
        //        dtCOInfo.Rows.Add(drNewCORow);

        //        if (ViewState["BUTTON"].ToString() == "1")
        //        {
        //            ViewState["Strenth"] = dtCOInfo;
        //            this.GVSELF.DataSource = (DataTable)ViewState["Strenth"];
        //            this.GVSELF.DataBind();
        //        }
        //        else if (ViewState["BUTTON"].ToString() == "2")
        //        {
        //            ViewState["aresForImpro"] = dtCOInfo;
        //            this.GVSELF.DataSource = (DataTable)ViewState["aresForImpro"];
        //            this.GVSELF.DataBind();
        //        }
        //        else if (ViewState["BUTTON"].ToString() == "3")
        //        {
        //            ViewState["Apprisiation"] = dtCOInfo;
        //            this.GVSELF.DataSource = (DataTable)ViewState["Apprisiation"];
        //            this.GVSELF.DataBind();
        //        }

        //        //ViewState["AddBind"] = dtCOInfo;
        //        //this.gvSteps.DataSource = (DataTable)ViewState["AddBind"];
        //        //this.gvSteps.DataBind();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //protected void lnkDeleteRows_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataTable dtCOInfo = this.GVSELF_CreateBindTable();


        //        int rowCount = this.GVSELF.Rows.Count + 1;
        //        foreach (GridViewRow gvrCO in this.GVSELF.Rows)
        //        {
        //            DataRow drCORow = dtCOInfo.NewRow();

        //            drCORow["CO_Row_Id"] = rowCount;
        //            drCORow["selfAssM"] = ((TextBox)gvrCO.FindControl("txtselfAssM")).Text;
        //            drCORow["AssMAppriser"] = ((TextBox)gvrCO.FindControl("txtAssMAppriser")).Text;

        //            drCORow["SelfHRRmk"] = ((TextBox)gvrCO.FindControl("txtSelfHRRmk")).Text;

        //            drCORow["Doc_Data_Id"] = (((Label)gvrCO.FindControl("lblCODataId")).Text.Trim() == "" ? "0" : ((Label)gvrCO.FindControl("lblCODataId")).Text);

        //            dtCOInfo.Rows.Add(drCORow);

        //            rowCount++;
        //        }



        //        if (ViewState["BUTTON"].ToString() == "1")
        //        {
        //            ViewState["Strenth"] = dtCOInfo;
        //            this.GVSELF.DataSource = (DataTable)ViewState["Strenth"];
        //            this.GVSELF.DataBind();
        //        }
        //        else if (ViewState["BUTTON"].ToString() == "2")
        //        {
        //            ViewState["aresForImpro"] = dtCOInfo;
        //            this.GVSELF.DataSource = (DataTable)ViewState["aresForImpro"];
        //            this.GVSELF.DataBind();
        //        }
        //        else if (ViewState["BUTTON"].ToString() == "3")
        //        {
        //            ViewState["Apprisiation"] = dtCOInfo;
        //            this.GVSELF.DataSource = (DataTable)ViewState["Apprisiation"];
        //            this.GVSELF.DataBind();
        //        }



        //        //ViewState["AddBind"] = dtCOInfo;
        //        //this.gvSteps.DataSource = dtCOInfo;
        //        //this.gvSteps.DataBind();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //protected void btnStrenth_Click(object sender, EventArgs e)
        //{
        //    btnStrenth.BackColor = System.Drawing.Color.Green;
        //    btnaresForImpro.BackColor = System.Drawing.Color.Gray;
        //    btnCApprisiation.BackColor = System.Drawing.Color.Gray;

        //    try
        //    {
        //        ViewState["BUTTON"] = "1";
        //        objAppraisal = new Portal.PerformanceAppraisal();
        //        DataSet ds = new DataSet();

        //        objAppraisal.Year = ViewState["Year"].ToString();
        //        ds = objAppraisal.Fill_ACCOMPLISHMENTS((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "Strenth");
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            this.GVSELF.DataSource = ds.Tables[0];
        //            this.GVSELF.DataBind();
        //        }
        //        else
        //        {
        //            this.GVSELF.DataSource = (DataTable)ViewState["Strenth"];
        //            this.GVSELF.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {


        //    }


        //}

        //protected void btnaresForImpro_Click(object sender, EventArgs e)
        //{
        //    btnStrenth.BackColor = System.Drawing.Color.Gray;
        //    btnaresForImpro.BackColor = System.Drawing.Color.Green;
        //    btnCApprisiation.BackColor = System.Drawing.Color.Gray;
        //    ViewState["BUTTON"] = "2";
        //    objAppraisal = new Portal.PerformanceAppraisal();
        //    DataSet ds = new DataSet();

        //    objAppraisal.Year = ViewState["Year"].ToString();
        //    ds = objAppraisal.Fill_ACCOMPLISHMENTS((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "aresForImpro");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        this.GVSELF.DataSource = ds.Tables[0];
        //        this.GVSELF.DataBind();
        //    }
        //    else
        //    {
        //        this.GVSELF.DataSource = (DataTable)ViewState["aresForImpro"];
        //        this.GVSELF.DataBind();
        //    }
        //}

        //protected void btnCApprisiation_Click(object sender, EventArgs e)
        //{
        //    btnStrenth.BackColor = System.Drawing.Color.Gray;
        //    btnaresForImpro.BackColor = System.Drawing.Color.Gray;
        //    btnCApprisiation.BackColor = System.Drawing.Color.Green;
        //    ViewState["BUTTON"] = "3";
        //    objAppraisal = new Portal.PerformanceAppraisal();
        //    DataSet ds = new DataSet();
        //    objAppraisal.Year = ViewState["Year"].ToString();
        //    ds = objAppraisal.Fill_ACCOMPLISHMENTS((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]), "Apprisiation");
        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        this.GVSELF.DataSource = ds.Tables[0];
        //        this.GVSELF.DataBind();
        //    }
        //    else
        //    {
        //        this.GVSELF.DataSource = (DataTable)ViewState["Apprisiation"];
        //        this.GVSELF.DataBind();
        //    }
        //}


        ///////////////////////

        private DataTable CreateBindKeyAdditionTable()
        {
            DataTable dtCOInfo = new DataTable();

            dtCOInfo.Columns.Add(new DataColumn("CO_Row_Id"));
            dtCOInfo.Columns.Add(new DataColumn("Major_contributions_Achievements_not_covered_under_KRAs_for_the_year"));
            dtCOInfo.Columns.Add(new DataColumn("REMARKS"));
            dtCOInfo.Columns.Add(new DataColumn("Appraisers_comments"));
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
                        drCORow["Major_contributions_Achievements_not_covered_under_KRAs_for_the_year"] = ((TextBox)gvrCO.FindControl("txtMajor")).Text;
                        drCORow["REMARKS"] = ((TextBox)gvrCO.FindControl("txtRmk")).Text;
                        drCORow["Appraisers_comments"] = ((TextBox)gvrCO.FindControl("txtRptRmk")).Text;
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
                BtnSaveOne.Visible = true;

                DataTable dtCOInfo = this.CreateBindKeyAdditionTable();

                int rowCount = this.grKey.Rows.Count + 1;
                foreach (GridViewRow gvrCO in this.grKey.Rows)
                {
                    DataRow drCORow = dtCOInfo.NewRow();

                    drCORow["CO_Row_Id"] = rowCount;
                    drCORow["Major_contributions_Achievements_not_covered_under_KRAs_for_the_year"] = ((TextBox)gvrCO.FindControl("txtMajor")).Text;
                    drCORow["REMARKS"] = ((TextBox)gvrCO.FindControl("txtRmk")).Text;
                    drCORow["Appraisers_comments"] = ((TextBox)gvrCO.FindControl("txtRptRmk")).Text;
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

        protected void BtnSaveOne_Click(object sender, EventArgs e)
        {
            string ErrorMsg;
            string Column = "";
            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            Column = "KEY ACCOMPLISHMENTS";

            objAppraisal.XML = MakeKeyDetailsXml();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.SECTION = "2";
            objAppraisal.Status = "3";
            objAppraisal.KraFlag = "0";
            objAppraisal.Cycle = ViewState["CYCLE"].ToString();
            objAppraisal.CycleAid = ViewState["CYCLE_AID"].ToString();

            ds = objAppraisal.GetApproverAid((string)(Session["sCompID"]), (string)(Session["sEmpCode"]));

            objAppraisal.ApprovalAid = ds.Tables[0].Rows[0]["EMP_APPR_AID"].ToString();

            ds = objAppraisal.INSERT_KEYACCOMPLISHMENTS(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "1");

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "")
                {
                    objcommon = new NewPortal2023.ESS.Common();
                    divAlert.Visible = true;
                    string script = $@"<script type='text/javascript'>alert('Saved Successfully');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    objcommon.SetMessageColor(divAlert, "success");
                    lblMessage.Text = "Saved Successfully.";

                    SENDPMSMAIL();

                    Fill_Details("1", gvList.PageSize.ToString());
                }
                
            }

        }

        private string MakeKeyDetailsXml()
        {
            System.Text.StringBuilder sbCODetails = new System.Text.StringBuilder();
            string xmlCO = string.Empty;
            objcommon = new NewPortal2023.ESS.Common();

            sbCODetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sbCODetails.Append("<ROOT>");

            //if (!chkAbsent.Checked)
            //{
            foreach (GridViewRow gvr in this.grKey.Rows)
            {

                sbCODetails.Append("<CO AID='" + ((Label)gvr.FindControl("lblAddCORowId")).Text + "'");
                sbCODetails.Append(" Major='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtMajor")).Text) + "'");
                sbCODetails.Append(" Remark='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRmk")).Text) + "'");
                sbCODetails.Append(" RptRmk='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRptRmk")).Text) + "'");
                sbCODetails.Append(" HRRmk='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtHRRmk")).Text) + "'/>");
            }
            //}

            sbCODetails.Append("</ROOT>");

            xmlCO = sbCODetails.ToString();

            return xmlCO;
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
        //    objAppraisal = new MakeKeyBSKILLSDetailsXml.PerformanceAppraisal();
        //    DataSet ds = new DataSet();


        //    Column = "lession";



        //    objAppraisal.Area = txtText.Text;
        //    objAppraisal.Year = ViewState["Year"].ToString();
        //    objAppraisal.SECTION = "2";

        //    ds = objAppraisal.INSERT_KEYlession(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "1");
        //}

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("EmployeeTaskDetails.aspx", true);
            }
            catch (Exception EX)
            {

            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("TrainingaAndDevelopment.aspx", true);
            }
            catch (Exception EX)
            {

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

                    string subject = "Do Not Reply: PMS - Key Accomplishment Submission Status";
                    string empBody =
                     "Dear Sir/Madam,<br/><br/>" +
                     "Your Key Accomplishment has been submitted and sent to your Reporting Manager for approval.<br/><br/>" +
                     "We will notify you once it is approved.<br/><br/>" +
                     "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                     "<strong>Best Regards,<br/>HR Team</strong>";

                    emailSend.SendEmailNPLPMS(empMail, Empemail, subject, empBody);
                }

                using (MailMessage rmMail = new MailMessage())
                {
                    emailSend = new NewPortal2023.ESS.Email();

                    string subject = "Do Not Reply: PMS - Key Accomplishment Approval Request";
                    string rmBody =
                   "Dear Sir/Madam,<br/><br/>" +
                   "A new Key Accomplishment Approval request for <strong>Mr. " + (string)Session["sEmpName"] + "</strong> has been received.<br/><br/>" +
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
    }
}