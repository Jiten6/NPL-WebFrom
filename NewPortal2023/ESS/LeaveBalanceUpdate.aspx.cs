using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class LeaveBalanceUpdate : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveApplication objInv = new NewPortal2023.ESS.LeaveApplication();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindEmployee();
                BindLeaveCode();
                BindYear();
                BindMonth();
            }
        }

        private void BindEmployee()
        {
            ds = objInv.Fillemployee();

            ddlEmployeeCode.Items.Clear();
            ddlEmployeeCode.DataTextField = "EMPCODE";
            ddlEmployeeCode.DataValueField = "CODE";
            ddlEmployeeCode.DataSource = ds.Tables[0];
            ddlEmployeeCode.DataBind();
            ddlEmployeeCode.Items.Insert(0, new ListItem("[Select One]", "0"));
        }

        private void BindLeaveCode()
        {
            ddlLeaveCode.Items.Add(new System.Web.UI.WebControls.ListItem("SL", "SL"));
            ddlLeaveCode.Items.Add(new System.Web.UI.WebControls.ListItem("CL", "CL"));
            ddlLeaveCode.Items.Add(new System.Web.UI.WebControls.ListItem("PL", "PL"));
            ddlLeaveCode.Items.Add(new System.Web.UI.WebControls.ListItem("TI", "TI"));
        }
        private void BindYear()
        {
            ddlYear.Items.Clear();
            ddlYear.Items.Add("-- Select --");

            for (int year = DateTime.Now.Year; year >= DateTime.Now.Year - 5; year--)
            {
                ddlYear.Items.Add(year.ToString());
            }
        }

        private void BindMonth()
        {
            ddlMonth.Items.Clear();
            ddlMonth.Items.Add("-- Select --");

            for (int i = 1; i <= 12; i++)
            {
                ddlMonth.Items.Add(
                    new System.Web.UI.WebControls.ListItem(
                        CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(i),
                        i.ToString()));
            }
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            string empCode = ddlEmployeeCode.SelectedValue;
            string leaveCode = ddlLeaveCode.SelectedValue;
            string  year = ddlYear.SelectedValue;
            string month = ddlMonth.SelectedValue;

            ds = objInv.GetLeaveData(empCode, leaveCode, year, month);

            txtOpeningBalance.Text = ds.Tables[0].Rows[0]["OPENING_BAL"].ToString();
            txtAvailed.Text = ds.Tables[0].Rows[0]["AVAILED"].ToString();
            txtClosingBalance.Text = ds.Tables[0].Rows[0]["CLOSING_BAL"].ToString();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string empCode = ddlEmployeeCode.SelectedValue;
            string leaveCode = ddlLeaveCode.SelectedValue;
            string year = ddlYear.SelectedValue;
            string month = ddlMonth.SelectedValue;

            string openingBalance = txtOpeningBalance.Text;
            string availed = txtAvailed.Text;
            string closingBalance = txtClosingBalance.Text;

            ds = objInv.UpdateLeaveData(empCode, leaveCode, year, month, openingBalance, availed, closingBalance);

            if (ds.Tables[0].Rows[0]["result"].ToString()=="")
            {
                ScriptManager.RegisterStartupScript(this, GetType(),
                "alert", "alert('Leave balance updated successfully');", true);

                ds = objInv.GetLeaveData(empCode, leaveCode, year, month);

                txtOpeningBalance.Text = ds.Tables[0].Rows[0]["OPENING_BAL"].ToString();
                txtAvailed.Text = ds.Tables[0].Rows[0]["AVAILED"].ToString();
                txtClosingBalance.Text = ds.Tables[0].Rows[0]["CLOSING_BAL"].ToString();
            }
            
        }

        
    }
}