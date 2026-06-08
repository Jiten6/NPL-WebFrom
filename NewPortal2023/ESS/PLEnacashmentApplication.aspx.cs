using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using System.Globalization;

namespace NewPortal2023.ESS
{
    public partial class PLEnacashmentApplication : System.Web.UI.Page
    {
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        NewPortal2023.ESS.LeaveApplication objInv = new NewPortal2023.ESS.LeaveApplication();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.DBUtility obdbutility = new NewPortal2023.ESS.DBUtility();
        DataSet dsInv = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["sCompID"] != null)
                {
                    try
                    {
                        FillDetails();

                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
                    }
                }
                else
                {
                    Response.Redirect("Logout.aspx");
                }
            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string result;
            string NewYear = drpYear.SelectedItem.Text;
            string NewMonth = drpMonth.SelectedValue.ToString();
            string status = drpLeaveType.SelectedValue;
            dsInv = objInv.UpdateEnacashment((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, NewMonth, status, txtenacash1.Text, txtBal.Text);
            if (dsInv.Tables[0].Rows[0]["result"].ToString() == "")
            {
                lblMessage.Text = "Successfuly Updated Enacashment.";
                objcommon.Display("Validate", "DisplayErrorMessage('Successfuly saved application.');");

            }
            else
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
            }

        }
        private void FillDetails()
        {

            try
            {
                objInv.GetFinancialYearList((string)Session["sCompID"], (string)Session["sEmpID"], "", drpYear);
                objInv.GetMonthList((string)Session["sCompID"], (string)Session["sEmpID"], "", drpMonth);
                objInv.GetPLLeave((string)Session["sCompID"], (string)Session["sEmpID"], "", drpLeaveType);

            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application'" + ex + "'.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {


        }
        protected void txtenacash_TextChanged(object sender, EventArgs e)
        {
            SetDays();
        }

        protected void SetDays()
        {
            if (txtenacash1.Text.Trim() != "" && txtopening1.Text.Trim() != "")
            {
                Double encash = Convert.ToDouble(txtenacash1.Text);
                if (encash <= Convert.ToDouble(txtopening1.Text))
                {
                    txtBal.Text = Convert.ToString((Convert.ToDouble(txtopening1.Text) - Convert.ToDouble(txtenacash1.Text)));
                }
                else
                {
                    lblMessage.Text = "Enacash days Greater from opening Balance.";
                }
            }
        }

        protected void drpLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string NewYear = drpYear.SelectedItem.Text;
            string NewMonth = drpMonth.SelectedValue.ToString();
            string status = drpLeaveType.SelectedValue;
            if (NewYear != "[Select One] " && NewMonth != "")
            {
                dsInv = objInv.GetPLOpeningBal((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, NewMonth, status);

                txtopening1.Text = Convert.ToString(dsInv.Tables[0].Rows[0]["OPENING_BAL"]);
            }
            else
            {
                lblMessage.Text = " Please Select Year and Month.";
            }


        }

        protected void txtenacash_TextChanged1(object sender, EventArgs e)
        {

        }
    }
}