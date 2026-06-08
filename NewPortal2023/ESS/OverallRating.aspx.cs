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
    public partial class OverallRating : System.Web.UI.Page
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
            objAppraisal.EmpCode = Session["sEmpID"].ToString();

            ds = objAppraisal.Fill_ApprisialLIst();
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


        protected void BtnSave_Click(object sender, EventArgs e)
        {
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

            ds = objAppraisal.CreateUpdateParamApprisal(Column, Convert.ToString(Session["sCompID"]), (string)(Session["sEmpCode"]), "1");
            Fill_Details("1", gvList.PageSize.ToString());
            objcommon = new NewPortal2023.ESS.Common();
            //if (ErrorMsg != "")
            //{
            //    //divAlertCreate.Visible = true;
            //    //objCommon.SetMessageColor(divAlertCreate, "danger");
            //    //lblMessageCreate.Text = HttpUtility.HtmlEncode(ErrorMsg);
            //}

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

            //if (!chkAbsent.Checked)
            //{
            foreach (GridViewRow gvr in this.gvList.Rows)
            {


                sbCODetails.Append("<CO Appraisal_AID='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtaid")).Text) + "'");
                sbCODetails.Append(" Metric='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtMetric")).Text) + "'");
                sbCODetails.Append(" Target='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtTarget")).Text) + "'");
                sbCODetails.Append(" twt='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtwt")).Text) + "'");
                sbCODetails.Append(" Weightage='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtWeightage")).Text) + "'");
                sbCODetails.Append(" REMARKS='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text) + "'");
                sbCODetails.Append(" AchPer='" + objcommon.ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtAchPer")).Text) + "'/>");
            }
            //}

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

        protected void gvList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {

        }

        protected void gvList_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void gvPLP_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}