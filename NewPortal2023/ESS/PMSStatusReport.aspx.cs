using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Windows.Forms;
using Microsoft.Reporting.WebForms;

namespace NewPortal2023.ESS
{
    public partial class PMSStatusReport : System.Web.UI.Page
    {
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ViewState["Year"] = "";
                FillFinYear();
            }
        }

        protected void drpReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpReportType.SelectedValue != "")
            {
                if (drpReportType.SelectedValue == "CycleWise")
                {
                    divKRAFin.Visible = true;
                    divFinYear.Visible = false;
                    divEmp.Visible = false;
                }
                else if (drpReportType.SelectedValue == "YearWise")
                {
                    divKRAFin.Visible = true;
                    divFinYear.Visible = true;
                    divEmp.Visible = false;
                }
            }
            else
            {
                divKRAFin.Visible = false;
                divFinYear.Visible = false;
                divEmp.Visible = false;
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

            drpType.SelectedValue = "";
            txtEmpCode.Text = "";

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

            fillDepartment();
            divEmp.Visible = false;
        }

        private void fillDepartment()
        {
            ds = objAppraisal.FillDepartment();
            drpDptList.Items.Clear();
            drpDptList.DataTextField = "NAME";
            drpDptList.DataValueField = "CODE";
            drpDptList.DataSource = ds.Tables[0];
            drpDptList.DataBind();
            drpDptList.Items.Insert(0, new ListItem("[Select One]", "0"));

        }

        protected void drpDptList_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblEmpCode.Visible = true;

            if (drpDptList.SelectedValue == "0")
            {
                txtEmpCode.Visible = true;
                drpEmpCode.Visible = false;

            }
            else
            {
                txtEmpCode.Visible = false;
                drpEmpCode.Visible = true;
                fillemployee();
            }

        }

        private void fillemployee()
        {
            objAppraisal.DeptID = drpDptList.SelectedValue;
            ds = objAppraisal.Fillemployee();
            drpEmpCode.Items.Clear();
            drpEmpCode.DataTextField = "EMPCODE";
            drpEmpCode.DataValueField = "CODE";
            drpEmpCode.DataSource = ds.Tables[0];
            drpEmpCode.DataBind();
            drpEmpCode.Items.Insert(0, new ListItem("[Select One]", "0"));
        }

        protected void drpEmpCode_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            string Emp_Code;

            if (txtEmpCode.Text == "")
            {
                Emp_Code = drpEmpCode.SelectedValue;
            }
            else
            {
                Emp_Code = txtEmpCode.Text;
            }

            objAppraisal.Year = ViewState["Year"].ToString();
            ds = objAppraisal.GetPMSStatusReport((string)Session["sCompID"], Emp_Code);

            rptPrint.Visible = true;
            ReportDataSource datasource1 = new ReportDataSource("dsPMSStatus", ds.Tables[0]);

            rptPrint.LocalReport.DataSources.Clear();

            rptPrint.LocalReport.ReportPath = @"Reports/PMSStatusReport.rdlc";
            rptPrint.LocalReport.DisplayName = "Employee_PMS_Status_Report";
            rptPrint.LocalReport.DataSources.Add(datasource1);

            rptPrint.LocalReport.Refresh();
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpType.SelectedValue == "")
            {
                divEmp.Visible = false;
                drpEmpCode.ClearSelection();
                drpEmpCode.SelectedIndex = -1;
                txtEmpCode.Text = "";
            }
            else
            {
                divEmp.Visible = true;
            }
        }


    }
}