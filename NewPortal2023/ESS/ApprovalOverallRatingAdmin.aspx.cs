using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using DocumentFormat.OpenXml.Spreadsheet;


namespace NewPortal2023.ESS
{
    public partial class ApprovalOverallRatingAdmin : System.Web.UI.Page
    {
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet ds = new DataSet();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sCompID"] != null)
            {
                if (!Page.IsPostBack)
                {

                    try
                    {
                        ViewState["EmpCode"] = null;
                        FillFinYear();
                        //Fill_Details("1", gvLIstVIew.PageSize.ToString());
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

            Get_EmployeeType();
            Fill_Details("1", gvList.PageSize.ToString());
            mv.SetActiveView(vwListView);
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
            objAppraisal.Flag = "HR";

            ds = objAppraisal.Fill_OverallRatEmpListHR(gvList, index, size);

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

        }

        private void Get_EmployeeType()
        {
            
            divEmpList.Visible = true;

            //int currentYear = DateTime.Now.Year;
            //string currentYearString = currentYear.ToString();
            //ViewState["Year"] = currentYearString;

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();

            ds = objAppraisal.GetSummaryListHR(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

            if (ds.Tables[0].Rows.Count > 0)
            {
                this.gvSummary.DataSource = ds;
                this.gvSummary.DataBind();
            }
            else
            {
                this.gvSummary.DataSource = null;
                this.gvSummary.DataBind();
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

        private void Fill_DetailsOverAllRating(string index, string size)
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
            objAppraisal.EmpCode = Session["sEmpID"].ToString();

            ds = objAppraisal.Fill_PLPList(gvList, index, size);
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

            //int currentYear = DateTime.Now.Year;
            //string currentYearString = currentYear.ToString();
            //ViewState["Year"] = currentYearString;

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpCode = Session["sEmpCode"].ToString();
            objAppraisal.EmpMakerMID = lblEmpCode.Text;

            GetScore("1", gvScore.PageSize.ToString());

            ds = objAppraisal.Fill_DetailsOverAllRatingHR(gvList, index, size);

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

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in this.gvList.Rows)
            {
                DropDownList drpLedBehav = gvr.FindControl("drpLedBehav") as DropDownList;
                DropDownList drpPromotion = gvr.FindControl("drpPromotion") as DropDownList;
                Session["Promotion"] = drpPromotion.Text;

                if (drpLedBehav.SelectedValue == "")
                {
                    objcommon = new NewPortal2023.ESS.Common();
                    divAlertListView.Visible = true;

                    string script = $@"<script type='text/javascript'>alert('Please Select Leadership Behaviour To Get Score');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    objcommon.SetMessageColor(divAlertListView, "danger");
                    lblMessageListView.Text = "Please Select Leadership Behaviour To Get Score";

                    return;
                }


            }
            DataSet ds = new DataSet();
            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            string column = "";
            objAppraisal.XML = MakeDetailsXml();

            //int currentYear = DateTime.Now.Year;
            //string currentYearString = currentYear.ToString();
            //ViewState["Year"] = currentYearString;

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpCode = Session["sEmpCode"].ToString();
            objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();


            ds = objAppraisal.InsertOverallRatingScore(column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

            if (ds.Tables[0].Rows[0]["result"].ToString() == "")
            {
                objcommon = new NewPortal2023.ESS.Common();
                divAlertListView.Visible = true;
                string script = $@"<script type='text/javascript'>alert('Saved Successfully');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlertListView, "success");
                lblMessageListView.Text = "Saved Successfully.";
            }
            else
            {
                objcommon = new NewPortal2023.ESS.Common();
                divAlertListView.Visible = true;

                objcommon.SetMessageColor(divAlertListView, "danger");
                lblMessageListView.Text = ds.Tables[0].Rows[0]["result"].ToString();
            }
            Get_EmployeeType();
            Fill_Details("1", gvList.PageSize.ToString());
            objcommon = new NewPortal2023.ESS.Common();


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

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                double TotalWtg = 0;
                double IndAch = 0;
                double TotalAchPer = 0;
                double TotalScore = 0;

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
                objAppraisal.EmpCode = Session["sEmpCode"].ToString();
                objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();

                ds = objAppraisal.Fill_DetailsOverAllRatingHR(gvList, index, size);

                for (int i = 0; i < gvList.Rows.Count; i++)
                {
                    GridViewRow row = gvList.Rows[i];

                    Label lblTotalWtg = row.FindControl("lblTotalWtg") as Label;
                    Label lblTotalAchPerc = row.FindControl("lblTotalAchPerc") as Label;
                    Label lblIndAch = row.FindControl("lblIndAch") as Label;
                    Label lblTotalScore = row.FindControl("lblTotalScore") as Label;
                    DropDownList drpKRARating = row.FindControl("drpKRARating") as DropDownList;
                    DropDownList drpLedBehav = row.FindControl("drpLedBehav") as DropDownList;
                    //DropDownList drpPromotion = row.FindControl("drpPromotion") as DropDownList;
                    TextBox txtPromotion = row.FindControl("txtPromotion") as TextBox;
                    TextBox txtOverRat = row.FindControl("txtOverRat") as TextBox;
                    TextBox txtPromotionRmk = row.FindControl("txtPromotionRmk") as TextBox;

                    lblTotalWtg.Text = ds.Tables[0].Rows[0]["WEIGHTAGE_INDIVIDUAL"].ToString();
                    lblTotalAchPerc.Text = ds.Tables[1].Rows[0]["ACHEIVED_INDIVIDUAL"].ToString();
                    lblTotalScore.Text = ds.Tables[2].Rows[0]["SCORE"].ToString();

                    drpKRARating.Text = ds.Tables[3].Rows[0]["KRARating"].ToString();
                    drpLedBehav.Text = ds.Tables[3].Rows[0]["Leadership_Behav"].ToString();
                    //drpPromotion.Text = ds.Tables[3].Rows[0]["Promotion"].ToString();
                    txtPromotion.Text = ds.Tables[3].Rows[0]["Promotion"].ToString();
                    txtOverRat.Text = ds.Tables[3].Rows[0]["Overall_Rating"].ToString();
                    txtPromotionRmk.Text = ds.Tables[3].Rows[0]["Reasoning"].ToString();

                    double.TryParse(RemovePercentageSymbol(lblTotalWtg.Text), out TotalWtg);
                    double.TryParse(RemovePercentageSymbol(lblTotalAchPerc.Text), out TotalAchPer);
                    double.TryParse(RemovePercentageSymbol(lblTotalScore.Text), out TotalScore);

                    IndAch = 100 * TotalScore / TotalWtg;
                    IndAch = Math.Round(IndAch, 2);

                    lblIndAch.Text = IndAch.ToString();
                    ViewState["IndAch"] = IndAch;

                    double Score = 0;
                    Score = Convert.ToDouble(lblIndAch.Text);

                    //if (Score < 75)
                    //{
                    //    drpKRARating.SelectedValue = "Needs Improvement";
                    //    ViewState["drpKRARating"] = drpKRARating.SelectedValue;
                    //}
                    //else if (75 <= Score && Score < 115)
                    //{
                    //    drpKRARating.SelectedValue = "Meets Expectations";
                    //    ViewState["drpKRARating"] = drpKRARating.SelectedValue;
                    //}
                    //else if (Score >= 115)
                    //{
                    //    drpKRARating.SelectedValue = "Exceeds Expectations";
                    //    ViewState["drpKRARating"] = drpKRARating.SelectedValue;
                    //}

                }

                //// Find the footer row and update the labels
                //Label lblTotalWeightage = e.Row.FindControl("lblTotalWeightage") as Label;
                //Label lblTotalCalculation = e.Row.FindControl("lblTotalCalculation") as Label;
                //Label lblTotalSCORE = e.Row.FindControl("lblTotalSCORE") as Label;

                //if (lblTotalWeightage != null && lblTotalCalculation != null)
                //{
                //    lblTotalWeightage.Visible = true;
                //    lblTotalCalculation.Visible = true;

                //}
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

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void gvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvPLP_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            GridViewRow row = gvList.Rows[e.RowIndex];

            DropDownList drpLedBehav = (row.FindControl("drpLedBehav") as DropDownList);
            TextBox txtOverRat = (row.FindControl("txtOverRat") as TextBox);

            objAppraisal.drpLedBehav = drpLedBehav.SelectedValue;
            objAppraisal.drpKRARating = ViewState["drpKRARating"].ToString();

            ds = objAppraisal.GetOverAllRAtingScore(objAppraisal.drpLedBehav, objAppraisal.drpKRARating);


            txtOverRat.Text = ds.Tables[0].Rows[0]["OVERALL_PERFORMANCE_RATING"].ToString();

            if (txtOverRat.Text != "")
            {
                string script = $@"<script type='text/javascript'>alert('Overall rating generated');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            }

            //DropDownList drpPromotion = (DropDownList)row.FindControl("drpPromotion");


            //    if (drpPromotion.SelectedValue == "Yes")
            //    {
            //        divDesg.Visible = true;

            //        objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            //        objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();

            //        ds = objAppraisal.GetEmpDesignation();

            //        txtOldDesg.Text = ds.Tables[0].Rows[0]["DESG_DESC"].ToString();
            //    }
            //    else
            //    {
            //        divDesg.Visible = false;
            //    }


            // Reset the GridView to display mode after updating
            //gvList.EditIndex = -1;
            //BindGridView();


        }

        protected void GetScore(string index, string size)
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
            objAppraisal.EmpCode = Session["lblEmpmid"].ToString();

            ds = objAppraisal.Fill_ApprisialLIst();

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvScore.DataSource = ds;
                gvScore.DataBind();
            }
            else
            {
                gvScore.DataSource = null;
                gvScore.DataBind();
            }
        }

        protected void gvScore_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                double totalWeightage = 0;
                double totalCalculation = 0;
                double totalScore = 0;
                double AchPer = 0;
                double AchPerPayOut = 0;
                double wht = 0;
                double morePrc = 0;
                for (int i = 0; i < gvScore.Rows.Count; i++)
                {
                    GridViewRow row = gvScore.Rows[i];

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

                    if (75 > AchPerPayOut)
                    {
                        lblScore.Text = Convert.ToString(wht * 0);
                        lblCalculation.Text = wht + " * 0%";
                    }
                    else if (75 == AchPerPayOut)
                    {
                        lblScore.Text = Convert.ToString(wht * (double)0.5);
                        lblCalculation.Text = wht + " * 50%";
                    }
                    else if (75 < AchPerPayOut && 100 > AchPerPayOut)
                    {
                        morePrc = AchPerPayOut - 75;
                        //double  s = wht * (double )1.00;
                        //double  s101= wht * (double )1.00 + 5 * (double )0.05;
                        //double  s150 = wht * (double )1.5;
                        lblScore.Text = Convert.ToString(wht * ((double)0.5 + morePrc * (double)0.02));
                        //lblScore.Text = (wht * 50.0 / 100) + (morePrc * 2.0 / 100);
                        lblCalculation.Text = wht + " * 50% + " + morePrc + " * 2%";
                    }
                    else if (100 == AchPerPayOut)
                    {
                        lblScore.Text = Convert.ToString(wht * (double)1.00);
                        lblCalculation.Text = wht + " * 100%";
                    }
                    else if (100 < AchPerPayOut && 111 > AchPerPayOut)
                    {
                        morePrc = AchPerPayOut - 100;
                        lblScore.Text = Convert.ToString(wht * (double)1.00 + morePrc * (double)0.05);
                        lblCalculation.Text = wht + " * 100% + " + morePrc + " * 5%";
                    }
                    else
                    {
                        lblScore.Text = Convert.ToString(wht * (double)1.5);
                        lblCalculation.Text = wht + " * 150%";
                    }

                    double weightage = 0;
                    double calculation = 0;
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
                    if (lblScore != null)
                    {
                        double.TryParse(RemovePercentageSymbol(lblScore.Text), out Score);
                        totalScore += Score;
                    }

                }

                Session["Score"] = totalScore.ToString();

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

        protected void lnkHisView_Click(object sender, EventArgs e)
        {
            LinkButton lnkHisView = (LinkButton)sender;
            string Emp_MID = Session["lblEmpmid"].ToString();

            string title = "LIST OF EMPLOYEES WITH THEIR PAST PERFORMANCE RATINGS";
            ds = objAppraisal.GetHistoricEmployeeWiseType(Convert.ToString(Session["sCompID"]), Emp_MID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvHistoric.DataSource = ds;
                gvHistoric.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowHistoricPopup('" + title + "');", true);
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "", false);
            }
        }

        protected void lnkLastPromView_Click(object sender, EventArgs e)
        {
            LinkButton lnkLastPromView = (LinkButton)sender;
            string Emp_MID = Session["lblEmpmid"].ToString();
            string title = "LAST PROMOTION REPORT";
            ds = objAppraisal.GetLastPRomotionEmployeeWiseType(Convert.ToString(Session["sCompID"]), Emp_MID);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvLastPromotion.DataSource = ds;
                gvLastPromotion.DataBind();

                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowLastPromotionPopup('" + title + "');", true);
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "", false);
            }
        }

        private string MakeDetailsXml()
        {
            System.Text.StringBuilder sbCODetails = new System.Text.StringBuilder();
            string xmlCO = string.Empty;
            objcommon = new NewPortal2023.ESS.Common();

            sbCODetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sbCODetails.Append("<ROOT>");

            foreach (GridViewRow gvr in this.gvList.Rows)
            {
                sbCODetails.Append("<CO APPRAISAL_AID='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtaid")).Text) + "'");
                sbCODetails.Append(" COLUMN='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblArea")).Text) + "'");
                sbCODetails.Append(" WEIGHTAGE='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblTotalWtg")).Text) + "'");
                sbCODetails.Append(" ACHPER='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblTotalScore")).Text) + "'");
                sbCODetails.Append(" INDACHPER='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblIndAch")).Text) + "'");

                double indAchAdd = Convert.ToDouble(((Label)gvr.FindControl("lblIndAch")).Text);
                double extraScore = Convert.ToDouble(((TextBox)gvr.FindControl("txtExtraScore")).Text);
                double totalScore = indAchAdd + extraScore;

                sbCODetails.Append(" INDACHPERADD='" + objcommon.ReplaceSpecialCharacters(totalScore.ToString()) + "'");

                sbCODetails.Append(" ADDPER='" + objcommon.ReplaceSpecialCharacters(extraScore.ToString()) + "'");
                sbCODetails.Append(" DRPKRARATING='" + objcommon.ReplaceSpecialCharacters(((DropDownList)gvr.FindControl("drpKRARating")).Text) + "'");
                sbCODetails.Append(" DRPLEDBEHAV='" + objcommon.ReplaceSpecialCharacters(((DropDownList)gvr.FindControl("drpLedBehav")).Text) + "'");
                sbCODetails.Append(" TXTOVERRAT='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtOverRat")).Text) + "'");
                sbCODetails.Append(" DRPPROMOTION='" + objcommon.ReplaceSpecialCharacters(((DropDownList)gvr.FindControl("drpPromotion")).Text) + "'");
                sbCODetails.Append(" TXTPROMOTIONRMK='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtPromotionRmk")).Text) + "'/>");
                
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
                    foreach (GridViewRow gvr in this.gvList.Rows)
                    {
                        DropDownList drpKRARating = gvr.FindControl("drpKRARating") as DropDownList;
                        DropDownList drpLedBehav = gvr.FindControl("drpLedBehav") as DropDownList;
                        DropDownList drpPromotion = gvr.FindControl("drpPromotion") as DropDownList;
                        TextBox txtExtraScore = gvr.FindControl("txtExtraScore") as TextBox;

                        Session["Promotion"] = drpPromotion.Text;

                        if (drpKRARating.SelectedValue == "")
                        {
                            objcommon = new NewPortal2023.ESS.Common();
                            divAlertListView.Visible = true;

                            string script = $@"<script type='text/javascript'>alert('Please Select KRA Rating To Get Score');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            objcommon.SetMessageColor(divAlertListView, "danger");
                            lblMessageListView.Text = "Please Select KRA Rating To Get Score";

                            return;
                        }

                        if (drpLedBehav.SelectedValue == "")
                        {
                            objcommon = new NewPortal2023.ESS.Common();
                            divAlertListView.Visible = true;

                            string script = $@"<script type='text/javascript'>alert('Please Select Leadership Behaviour To Get Score');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            objcommon.SetMessageColor(divAlertListView, "danger");
                            lblMessageListView.Text = "Please Select Leadership Behaviour To Get Score";

                            return;
                        }

                        if (drpPromotion.SelectedValue == "")
                        {
                            objcommon = new NewPortal2023.ESS.Common();
                            divAlertListView.Visible = true;

                            string script = $@"<script type='text/javascript'>alert('Please Select Promotion value');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            objcommon.SetMessageColor(divAlertListView, "danger");
                            lblMessageListView.Text = "Please Select Promotion value";

                            return;
                        }

                        if (txtExtraScore.Text == "")
                        {
                            objcommon = new NewPortal2023.ESS.Common();
                            divAlertListView.Visible = true;

                            string script = $@"<script type='text/javascript'>alert('Please Enter Valid Value In Additional Achievement %');</script>";
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                            objcommon.SetMessageColor(divAlertListView, "danger");
                            lblMessageListView.Text = "Please Enter Valid Value In Additional Achievement %";

                            return;
                        }

                    }

                    DataSet ds = new DataSet();
                    objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
                    string column = "";
                    objAppraisal.XML = MakeDetailsXml();

                    //int currentYear = DateTime.Now.Year;
                    //string currentYearString = currentYear.ToString();
                    //ViewState["Year"] = currentYearString;

                    ViewState["Quarter"] = drpQuarter.SelectedValue;
                    objAppraisal.Quarter = ViewState["Quarter"].ToString();
                    objAppraisal.Year = ViewState["Year"].ToString();
                    objAppraisal.Comp_Aid = Session["sCompID"].ToString();
                    objAppraisal.EmpCode = Session["sEmpCode"].ToString();
                    objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();

                    if (drActionAll.SelectedValue == "Approve")
                    {
                        objAppraisal.Status = "1";
                        objAppraisal.KraFlag = "0";
                    }
                    else if (drActionAll.SelectedValue == "Reject")
                    {
                        objAppraisal.Status = "0";
                        objAppraisal.KraFlag = "0";
                    }


                    objAppraisal.Remarks = txtAllRmk.Text;
                    objAppraisal.Action = drActionAll.SelectedValue;


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
                            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
                            objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
                            objAppraisal.OldDesg = txtOldDesg.Text;

                            if (drpDesgList.SelectedValue == "-1")
                            {
                                if (txtOtherDesg.Text == "")
                                {
                                    string message = "Please enter Other Designation";
                                    string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                                    return;
                                }
                                else
                                {
                                    objAppraisal.NewDesgAid = drpDesgList.SelectedValue;
                                    objAppraisal.NewDesgDesc = txtOtherDesg.Text;
                                }
                            }
                            else
                            {
                                objAppraisal.NewDesgAid = drpDesgList.SelectedValue;
                            }
                            
                            //ds = objAppraisal.UpdateNewDesignation();

                            ds = objAppraisal.InsertOverallRatingScoreHR(column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

                            if (ds.Tables[0].Rows[0]["result"].ToString() == "")
                            {
                                objcommon = new NewPortal2023.ESS.Common();
                                divAlertListView.Visible = true;
                                objcommon.SetMessageColor(divAlertListView, "success");
                                lblMessageListView.Text = "Action Successfully Submitted.";

                                string message = lblMessageListView.Text;
                                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                                SENDPMSMAIL(drActionAll.SelectedValue);

                                divDesg.Visible = false;
                                drActionAll.SelectedValue = "";
                                drpDesgList.SelectedValue = "0";
                                txtAllRmk.Text = "";
                                txtOldDesg.Text = "";
                                Get_EmployeeType();
                                Fill_Details("1", gvList.PageSize.ToString());
                                mv.SetActiveView(vwListView);
                                //accordionTwo.Visible = false;
                                //accordion.Visible = true;
                            }
                            else
                            {
                                objcommon = new NewPortal2023.ESS.Common();
                                divAlertListView.Visible = true;

                                objcommon.SetMessageColor(divAlertListView, "danger");
                                lblMessageListView.Text = ds.Tables[0].Rows[0]["result"].ToString();
                            }

                            
                        }
                       
                    }
                    else
                    {
                        objcommon = new NewPortal2023.ESS.Common();
                        divAlertListView.Visible = true;
                        string script = $@"<script type='text/javascript'>alert('Please Select Action');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        objcommon.SetMessageColor(divAlertListView, "danger");
                        lblMessageListView.Text = "Please Select Action.";
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

        protected void drpPromotion_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpPromotion = (DropDownList)sender;

            GridViewRow row = (GridViewRow)drpPromotion.NamingContainer;


            if (drpPromotion.SelectedValue == "Yes")
            {
                objAppraisal.Comp_Aid = Session["sCompID"].ToString();
                objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();

                ds = objAppraisal.GetEmpDesignation();

                txtOldDesg.Text = ds.Tables[0].Rows[0]["DESG_DESC"].ToString();

                divDesg.Visible = true;
                fillDeSignation();
            }
            else
            {
                divDesg.Visible = false;
            }
        }

        private void fillDeSignation()
        {
            ds = objAppraisal.FillDeSignation();
            drpDesgList.Items.Clear();
            drpDesgList.DataTextField = "NAME";
            drpDesgList.DataValueField = "CODE";
            drpDesgList.DataSource = ds.Tables[0];
            drpDesgList.DataBind();
            drpDesgList.Items.Insert(0, new ListItem("[Select One]", "0"));
            drpDesgList.Items.Add(new ListItem("Other", "-1"));

        }

        protected void drpDesgList_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            if (drpDesgList.SelectedValue == "0")
            {
                btnSubmitAll.Visible = false;
            }
            else
            {
                btnSubmitAll.Visible = true;
            }

            if (drpDesgList.SelectedValue == "-1")
            {
                txtOtherDesg.Visible = true;
            }
            else
            {
                txtOtherDesg.Visible = false;
            }
        }

        protected void btnSubmitDesg_Click(object sender, EventArgs e)
        {
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
            objAppraisal.OldDesg = txtOldDesg.Text;
            objAppraisal.NewDesgAid = drpDesgList.SelectedValue;

            ds = objAppraisal.UpdateNewDesignation();
        }

        protected void gvDataPointList_PreRender(object sender, EventArgs e)
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

        protected void drpKRARating_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            foreach (GridViewRow gvr in this.gvList.Rows)
            {
                DropDownList drpKRARating = (gvr.FindControl("drpKRARating") as DropDownList);
                DropDownList drpLedBehav = (gvr.FindControl("drpLedBehav") as DropDownList);
                TextBox txtOverRat = (gvr.FindControl("txtOverRat") as TextBox);

                objAppraisal.drpKRARating = drpKRARating.SelectedValue;       //ViewState["drpKRARating"].ToString();
                objAppraisal.drpLedBehav = drpLedBehav.SelectedValue;

                if (drpKRARating.SelectedValue != "" && drpLedBehav.SelectedValue != "")
                {
                    ds = objAppraisal.GetOverAllRAtingScore(objAppraisal.drpLedBehav, objAppraisal.drpKRARating);

                    txtOverRat.Text = ds.Tables[0].Rows[0]["OVERALL_PERFORMANCE_RATING"].ToString();

                    //if (txtOverRat.Text != "")
                    //{
                    //    string script = $@"<script type='text/javascript'>alert('Overall rating generated');</script>";
                    //    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    //}
                }
                else
                {
                    string script = $@"<script type='text/javascript'>alert('Please Select KRA rating and Leadership Behaviour');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                    txtOverRat.Text = "0";
                }

            }
        }

        protected void drpLedBehav_SelectedIndexChanged(object sender, EventArgs e)
        {
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            foreach (GridViewRow gvr in this.gvList.Rows)
            {
                DropDownList drpKRARating = (gvr.FindControl("drpKRARating") as DropDownList);
                DropDownList drpLedBehav = (gvr.FindControl("drpLedBehav") as DropDownList);
                TextBox txtOverRat = (gvr.FindControl("txtOverRat") as TextBox);

                objAppraisal.drpKRARating = drpKRARating.SelectedValue;       //ViewState["drpKRARating"].ToString();
                objAppraisal.drpLedBehav = drpLedBehav.SelectedValue;

                if (drpKRARating.SelectedValue != "" && drpLedBehav.SelectedValue != "")
                {
                    ds = objAppraisal.GetOverAllRAtingScore(objAppraisal.drpLedBehav, objAppraisal.drpKRARating);

                    txtOverRat.Text = ds.Tables[0].Rows[0]["OVERALL_PERFORMANCE_RATING"].ToString();

                    //if (txtOverRat.Text != "")
                    //{
                    //    string script = $@"<script type='text/javascript'>alert('Overall rating generated');</script>";
                    //    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    //}
                }
                else
                {
                    string script = $@"<script type='text/javascript'>alert('Please Select KRA rating and Leadership Behaviour');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                    txtOverRat.Text = "0";
                }

            }

        }

        private void SENDPMSMAIL(string action)
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

                ds = objAppraisal.GetMailidDetailsHR((string)Session["sCompID"], (string)Session["sEmpID"], makerMid);

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

                        string subject = "Do Not Reply: PMS - Overall Rating Approved";
                        string empBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that your Overall Rating has been approved by your HR.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "<strong>Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(empMail, Empemail, subject, empBody);
                    }

                    using (MailMessage rmMail = new MailMessage())
                    {
                        string subject = "Do Not Reply: PMS - Overall Rating Approved";

                        string rmBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that Overall Rating has been approved by HR for <strong>Mr. " + makerName + "</strong>.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "Thank you for your prompt attention to this matter.<br/><br/>" +
                        "<strong>With Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(rmMail, RMemail, subject, rmBody);
                    }

                    using (MailMessage hodMail = new MailMessage())
                    {
                        string subject = "Do Not Reply: PMS - Overall Rating Approved";

                        string hodBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that Overall Rating has been approved by HR for <strong>Mr. " + makerName + "</strong>.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "Thank you for your prompt attention to this matter.<br/><br/>" +
                        "<strong>With Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(hodMail, HODemail, subject, hodBody);
                    }

                    using (MailMessage hrMail = new MailMessage())
                    {
                        string subject = "Do Not Reply: PMS - Overall Rating Approval Status";

                        string hodBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that Overall Rating has been approved for <strong>Mr. " + makerName + "</strong>.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "Thank you for your prompt attention to this matter.<br/><br/>" +
                        "<strong>With Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(hrMail, HRemail, subject, hodBody);
                    }
                }
                else if (action == "Reject")
                {
                    using (MailMessage empMail = new MailMessage())
                    {
                        emailSend = new NewPortal2023.ESS.Email();

                        string subject = "Do Not Reply: PMS - Overall Rating Rejected";
                        string empBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that your Overall Rating has been rejected by your HR.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "<strong>Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(empMail, Empemail, subject, empBody);
                    }

                    using (MailMessage rmMail = new MailMessage())
                    {
                        string subject = "Do Not Reply: PMS - Overall Rating Rejected";

                        string rmBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that Overall Rating has been rejected by HR for <strong>Mr. " + makerName + "</strong>.<br/><br/>" +
                        "Please log in to the portal at your earliest convenience and take the necessary action to proceed with the process.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "Thank you for your prompt attention to this matter.<br/><br/>" +
                        "<strong>With Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(rmMail, RMemail, subject, rmBody);
                    }

                    using (MailMessage hodMail = new MailMessage())
                    {
                        string subject = "Do Not Reply: PMS - Overall Rating Rejected";

                        string hodBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that Overall Rating has been rejected by HR for <strong>Mr. " + makerName + "</strong>.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "Thank you for your prompt attention to this matter.<br/><br/>" +
                        "<strong>With Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(hodMail, HODemail, subject, hodBody);
                    }

                    using (MailMessage hrMail = new MailMessage())
                    {
                        string subject = "Do Not Reply: PMS - Overall Rating Rejected";

                        string hodBody =
                        "Dear Sir/Madam,<br/><br/>" +
                        "This is to inform you that Overall Rating has been rejected for <strong>Mr. " + makerName + "</strong>.<br/><br/>" +
                        "<strong>Portal Access:</strong> <a href='https://npl.sequelgroup.co.in/ESS/Login.aspx'>https://npl.sequelgroup.co.in/ESS/Login.aspx</a><br/><br/>" +
                        "Thank you for your prompt attention to this matter.<br/><br/>" +
                        "<strong>With Best Regards,<br/>HR Team</strong>";

                        emailSend.SendEmailNPLPMS(hrMail, HRemail, subject, hodBody);
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

        protected void txtExtraScore_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void btnAddExtraScore_Click(object sender, EventArgs e)
        {
            NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
            DataSet ds = new DataSet();

            double IndAch = 0;

            foreach (GridViewRow gvr in this.gvList.Rows)
            {
                Label lblIndAch = gvr.FindControl("lblIndAch") as Label;
                Label lblIndAchAdd = gvr.FindControl("lblIndAchAdd") as Label;
                DropDownList drpKRARating = gvr.FindControl("drpKRARating") as DropDownList;
                TextBox txtExtraScore = gvr.FindControl("txtExtraScore") as TextBox;

                double indAch = 0, extra = 0;

                double.TryParse(ViewState["IndAch"].ToString(), out indAch);
                double.TryParse(txtExtraScore.Text, out extra);

                double Score = indAch + extra;
                lblIndAchAdd.Text = Score.ToString("0.00");

                if (Score < 75)
                {
                    drpKRARating.SelectedValue = "Needs Improvement";
                    ViewState["drpKRARating"] = drpKRARating.SelectedValue;
                }
                else if (75 <= Score && Score < 115)
                {
                    drpKRARating.SelectedValue = "Meets Expectations";
                    ViewState["drpKRARating"] = drpKRARating.SelectedValue;
                }
                else if (Score >= 115)
                {
                    drpKRARating.SelectedValue = "Exceeds Expectations";
                    ViewState["drpKRARating"] = drpKRARating.SelectedValue;
                }

            }
        }
    }
}