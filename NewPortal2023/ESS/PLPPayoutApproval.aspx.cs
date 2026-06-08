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
    public partial class PLPPayoutApproval : System.Web.UI.Page
    {
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sCompID"] != null)
            {
                if (!Page.IsPostBack)
                {
                    try
                    {
                        FillFinYear();
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

            drpEmpType.SelectedValue = "1";
            Get_EmployeeType();
            Fill_Details("1", gvLIstVIew.PageSize.ToString());
            mv.SetActiveView(vwListView);
        }

        private void Get_EmployeeType()
        {
            objAppraisal.Flag = "PMS";
            ds = objAppraisal.GetEmployeeType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

            if (ds.Tables[0].Rows[0]["TYPE"].ToString() == "HR")
            {
                divCompFAct.Visible = true;

                objAppraisal.Year = ViewState["Year"].ToString();
                ds = objAppraisal.GetCompFactor(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]));

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
            else
            {
                divCompFAct.Visible = false;
            }
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
            objAppraisal.Flag = "PMS";
            ds = objAppraisal.Fill_PLPEmployeeList(gvLIstVIew, index, size);

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

        protected void drpQuarter_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["Quarter"] = drpQuarter.SelectedValue;
                Fill_DetailsPLP("1", gvPLP.PageSize.ToString());
            }

            catch (Exception ex)
            {

            }

        }


        private void Fill_DetailsPLP(string index, string size)
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

            ds = objAppraisal.Fill_PLPList(gvPLP, index, size);
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

            ds = objAppraisal.Fill_PLPList(gvPLP, index, size);

            if (ds.Tables[0].Rows.Count > 0)
            {
                gvPLP.DataSource = ds;
                gvPLP.DataBind();
            }
            else
            {
                gvPLP.DataSource = null;
                gvPLP.DataBind();
            }

        }

        protected void gvPLP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                double totalTarget = 0;
                double totalAchieved = 0;
                double totalScore = 0;
                double AchPer = 0;
                double AchPerPayOut = 0;
                double wht = 0;
                double morePrc = 0;
                //int currentYear = DateTime.Now.Year;
                //string currentYearString = currentYear.ToString();
                //ViewState["Year"] = currentYearString;

                ViewState["Quarter"] = drpQuarter.SelectedValue;
                objAppraisal.Quarter = ViewState["Quarter"].ToString();
                objAppraisal.Year = ViewState["Year"].ToString();
                objAppraisal.Comp_Aid = Session["sCompID"].ToString();
                objAppraisal.EmpCode = Session["sEmpCode"].ToString();
                objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();

                ds = objAppraisal.GetIndvidualPercentage((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]));

                for (int i = 0; i < gvPLP.Rows.Count; i++)
                {
                    GridViewRow row = gvPLP.Rows[i];

                    Label lblTarget = row.FindControl("lblTarget") as Label;
                    Label lblAchieved = row.FindControl("lblAchieved") as Label;


                    Label lblArea = row.FindControl("lblArea") as Label;

                    if (lblArea.Text == "Individual")
                    {
                        lblAchieved.Text = ds.Tables[3].Rows[0]["ACHEIVED_INDIVIDUAL"].ToString();
                    }

                    double Target = 0;
                    double Achieved = 0;


                    if (lblTarget != null)
                    {
                        double.TryParse(RemovePercentageSymbol(lblTarget.Text), out Target);
                        totalTarget += Target;
                    }

                    if (lblAchieved != null)
                    {
                        double.TryParse(RemovePercentageSymbol(lblAchieved.Text), out Achieved);
                        totalAchieved += Achieved;
                    }


                }

                // Find the footer row and update the labels
                Label lblTotalTarget = e.Row.FindControl("lblTotalTarget") as Label;
                Label lblTotalAchieved = e.Row.FindControl("lblTotalAchieved") as Label;


                if (lblTotalTarget != null && lblTotalAchieved != null)
                {
                    lblTotalTarget.Visible = true;
                    lblTotalAchieved.Visible = true;
                    lblTotalTarget.Text = totalTarget.ToString();
                    lblTotalAchieved.Text = totalAchieved.ToString();
                }
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();

            //int currentYear = DateTime.Now.Year;
            //string currentYearString = currentYear.ToString();
            //ViewState["Year"] = currentYearString;

            ViewState["Quarter"] = drpQuarter.SelectedValue;
            objAppraisal.Quarter = ViewState["Quarter"].ToString();
            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Comp_Aid = Session["sCompID"].ToString();
            objAppraisal.EmpCode = Session["sEmpCode"].ToString();
            objAppraisal.EmpMakerMID = Session["lblEmpmid"].ToString();
            objAppraisal.EmpHRMID = Session["sEmpCode"].ToString();

            ds = objAppraisal.GetIndvidualPercentage((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]));
            objAppraisal.tarIndiv = ds.Tables[0].Rows[0]["INDIVIDUAL"].ToString();
            objAppraisal.tarCompFact = ds.Tables[1].Rows[0]["COMPANYFACTOR"].ToString();
            objAppraisal.tarCompProm = ds.Tables[2].Rows[0]["COMPANYPROMISE"].ToString();

            ds = objAppraisal.GetApproverAid((string)(Session["sCompID"]), (string)(Session["sEmpCode"]));

            objAppraisal.ApprovalAid = ds.Tables[0].Rows[0]["EMP_APPR_AID"].ToString();

            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.Quarter = ViewState["Quarter"].ToString();

            ds = objAppraisal.InsertPLPStatus((string)(Session["sEmpCode"]), Convert.ToString(Session["sCompID"]));
            Fill_DetailsPLP("1", gvPLP.PageSize.ToString());

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

        private string MakeDetailsXml()
        {
            System.Text.StringBuilder sbCODetails = new System.Text.StringBuilder();
            string xmlCO = string.Empty;
            objcommon = new NewPortal2023.ESS.Common();

            sbCODetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sbCODetails.Append("<ROOT>");


            foreach (GridViewRow gvr in this.gvPLP.Rows)
            {
                sbCODetails.Append("<CO Area='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblArea")).Text) + "'");
                sbCODetails.Append(" Target='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtTarget")).Text) + "'");
                sbCODetails.Append(" Acheived='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtAcheived")).Text) + "'/>");
            }


            sbCODetails.Append("</ROOT>");

            xmlCO = sbCODetails.ToString();

            return xmlCO;
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
                for (int i = 0; i < gvPLP.Rows.Count; i++)
                {
                    GridViewRow row = gvPLP.Rows[i];

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

        private void BindGridViewgvPLP()
        {
            // Assuming you have some data source (e.g., DataTable)
            DataTable dt = BindPLPData();

            // Bind the data to the GridView
            gvPLP.DataSource = dt;
            gvPLP.DataBind();
        }

        private DataTable BindPLPData()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("AREA", typeof(string));
            dt.Columns.Add("ABC", typeof(string));
            dt.Columns.Add("XYZ", typeof(string));

            dt.Rows.Add("Individual");
            dt.Rows.Add("Company Factor");
            dt.Rows.Add("Company Promise ");

            return dt;
        }

        protected void drpEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpEmpType.SelectedValue == "ABOVE SR MANAGER")
            {
                divAboveSr.Visible = true;
                divBelowSr.Visible = false;
            }
            else if (drpEmpType.SelectedValue == "BELOW SR MANAGER")
            {
                divBelowSr.Visible = true;
                divAboveSr.Visible = false;
            }
            else
            {
                divAboveSr.Visible = false;
                divBelowSr.Visible = false;
            }
        }

        protected void btnSubmitAbv_Click(object sender, EventArgs e)
        {
            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();

            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.txtCompFactAbv = txtCompFactAbv30.Text;
            objAppraisal.txtCompPromAbv = txtCompFactAbv20.Text;
            objAppraisal.txtCompPromAbv20 = txtCompPromAbv.Text;
            string EmpType = drpEmpType.SelectedValue;

            ds = objAppraisal.FillPLPDataAbv(EmpType, (string)Session["sEmpCode"]);

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

            mv.SetActiveView(vwListView);
            Fill_Details("1", gvLIstVIew.PageSize.ToString());
            Get_EmployeeType();
            drpEmpType.SelectedValue = drpEmpType.SelectedValue;

        }

        protected void btnSubmitBlw_Click(object sender, EventArgs e)
        {
            objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();

            objAppraisal.Year = ViewState["Year"].ToString();
            objAppraisal.txtCompFactBlw = txtCompFactBlw18.Text;
            objAppraisal.txtCompPromBlw = txtCompFactBlw12.Text;
            string EmpType = drpEmpType.SelectedValue;

            ds = objAppraisal.FillPLPDataBlw(EmpType, (string)Session["sEmpCode"]);

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

            mv.SetActiveView(vwListView);
            Fill_Details("1", gvLIstVIew.PageSize.ToString());
            Get_EmployeeType();
            drpEmpType.SelectedValue = drpEmpType.SelectedValue;
        }

        //protected void txtAcheived_TextChanged(object sender, EventArgs e)
        //{
        //    TextBox txtTarget = (TextBox)sender;
        //    objAppraisal txtTarget.Text;
        //}
    }
}