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
    public partial class OverallRatingReport : System.Web.UI.Page
    {
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
              if (Session["sCompID"]!=null)
            {
            if (!Page.IsPostBack)
            {
                fillDepartment();
                divEmp.Visible = false;
            }
        }
              else
              {
                  Response.Redirect("Login.aspx");
              }
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

            ds = objAppraisal.GetOverallratingReport((string)Session["sCompID"], Emp_Code);

            rptPrint.Visible = true;
            ReportDataSource datasource1 = new ReportDataSource("dsOverallRating", ds.Tables[0]);
            rptPrint.LocalReport.DataSources.Clear();

            rptPrint.LocalReport.ReportPath = @"Reports/OverallRatingReport.rdlc";
            rptPrint.LocalReport.DisplayName = "Overall_Rating_Report";
            rptPrint.LocalReport.DataSources.Add(datasource1);

            rptPrint.LocalReport.Refresh();
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpType.SelectedValue == "")
            {
                divEmp.Visible = false;
            }
            else
            {
                divEmp.Visible = true;
            }
        }
    }
}