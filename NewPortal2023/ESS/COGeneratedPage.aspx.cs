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
    public partial class COGeneratedPage : System.Web.UI.Page
    {
        NewPortal2023.ESS.NplCoApp objNpl = new NewPortal2023.ESS.NplCoApp();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.DBUtility obdbutility = new NewPortal2023.ESS.DBUtility();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        DataSet dsInv = new DataSet();
        string OldYear, NewYear;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["sCompID"] != null)
                {
                    try
                    {
                        FillCOList();
                        AppList.Visible = true;
                        AppForm.Visible = false;
                        btnAddNew.Visible = true;
                        //LeaveDiv.Visible = true;
                        idlbltotaldays.Visible = true;
                        anotherleave.Visible = false;
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

        private void ClearFields()
        {
            ddlScheduleOptions.SelectedIndex = -1;
            txtFromDate.Text = "";
        }

        private void FillCOList()
        {
            string finYear = objcommon.GetFinalcialYear();
            string[] years = finYear.Split('.');
            OldYear = years[1];
            NewYear = years[0];

            dsInv = objNpl.GetCOList((string)Session["sCompID"], (string)Session["sEmpID"], NewYear);
            gvCO.DataSource = dsInv;
            gvCO.DataBind();
        }

        protected void SetDays()
        {
            try
            {
                if (ddlScheduleOptions.SelectedItem.ToString() != "")
                {
                    if (ddlScheduleOptions.SelectedItem.ToString() == "PH + 24 HRS on Weekly Off - 2 CO" || ddlScheduleOptions.SelectedItem.ToString() == "24 HRS on 2nd Weekly Off - 2 CO")
                    {

                        dsInv = objNpl.getDays((string)Session["sCompID"], (string)Session["sEmpID"], txtFRDate.Text, txtTODate.Text);
                        lblTotalDays.Text = dsInv.Tables[0].Rows[0]["NUMBEROFDAYS"].ToString();
                        if (txtFRDate.Text.Trim() != "" && txtTODate.Text.Trim() != "")
                        {
                            if (txtFRDate.Text.Trim() == txtTODate.Text.Trim())
                            {
                                idlbltotaldays.Visible = false;
                                lblMessage.Text = "Both date not should be same Please select different date.";
                                objcommon.Display("Validate", "DisplayErrorMessage('Both date not should be same Please select different date.');");
                                btnApprove.Visible = false;
                                divAlert.Visible = true;
                            }
                            else
                            {
                                idlbltotaldays.Visible = true;
                                btnApprove.Visible = true;
                                divAlert.Visible = false;
                            }

                        }
                        else
                        {
                            btnApprove.Visible = false;
                            divAlert.Visible = true;
                            lblMessage.Text = "Please select date for days count.";
                            objcommon.Display("Validate", "DisplayErrorMessage('Please select date for days count.');");
                        }
                    }
                    else if (ddlScheduleOptions.SelectedItem.ToString() == "24 Hours Duty" || ddlScheduleOptions.SelectedItem.ToString() == "PH on Weekly Off" || ddlScheduleOptions.SelectedItem.ToString() == "Working on 2nd Weekly Off")
                    {
                        dsInv = objNpl.getDays((string)Session["sCompID"], (string)Session["sEmpID"], txtFRDate.Text, txtTODate.Text);
                        lblTotalDays.Text = dsInv.Tables[0].Rows[0]["NUMBEROFDAYS"].ToString();
                        txtTODate.Text = "";
                        if (txtFRDate.Text.Trim() != "" && txtTODate.Text.Trim() == "")
                        {
                            idlbltotaldays.Visible = true;
                            btnApprove.Visible = true;
                            divAlert.Visible = false;
                        }
                        else
                        {
                            idlbltotaldays.Visible = false;
                            btnApprove.Visible = false;
                            divAlert.Visible = true;
                            lblMessage.Text = "Please select date for days count.";
                            objcommon.Display("Validate", "DisplayErrorMessage('Please select date for days count.');");
                        }
                    }

                }
                else
                {
                    lblMessage.Text = "Select CO Type.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Select CO Type.');");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                
            }
           
        }

        protected void txtFRDate_TextChanged(object sender, EventArgs e)
        {

            SetDays();
            // checkleavebalance();


        }

        protected void txtTODate_TextChanged(object sender, EventArgs e)
        {
            SetDays();
            //checkleavebalance();

        }

        //protected void txtFRDate1_TextChanged(object sender, EventArgs e)
        //{

        //}

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                //if (ValidateData() == true)
                {


                    string finYear = objcommon.GetFinalcialYear();
                    string[] years = finYear.Split('.');
                    OldYear = years[1];
                    NewYear = years[0];


                    result = objNpl.UpdateLeave((string)Session["sCompID"], (string)Session["sEmpID"], txtFromDate.Text, ddlScheduleOptions.SelectedValue, "2", NewYear, txtFRDate.Text, txtTODate.Text);
                    if (result.ToString().Trim() == "success")
                    {
                        divAlertSucc.Visible = true;
                        lblMessageSucc.Text = "Successfuly sent Co Application for approval.";
                        AppList.Visible = true;
                        AppForm.Visible = false;
                        btnAddNew.Visible = true;
                        Response.Redirect("COGeneratedPage.aspx");
                    }

                    else if (result.ToString().Trim() == "duplicate")
                    {
                        divAlert.Visible = true;
                        lblMessage.Text = "Application already exists for selected date.";
                        return;
                    }

                    else if (result.ToString().Trim() == "CO duplicate")
                    {
                        divAlert.Visible = true;
                        lblMessage.Text = "CO Application date not same as Leave Applied date.";
                        return;
                    }
                    else
                    {
                        divAlert.Visible = true;
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                    }

                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
            }

        }

        //private Boolean ValidateData()
        //{

        //    if (txtFromDate.Text.Trim() == "")
        //    {
        //        lblMessage.Text = "Select  Date.";
        //        objcommon.Display("Validate", "DisplayErrorMessage('Select Date.');");
        //        return false;
        //    }
        //    if (txtFRDate.Text.Trim() == "")
        //    {
        //        lblMessage.Text = "Select From date.";
        //        objcommon.Display("Validate", "DisplayErrorMessage('Select From date.');");
        //        return false;
        //    }
        //    if (txtTODate.Text.Trim() == "")
        //    {
        //        lblMessage.Text = "Select To date.";
        //        objcommon.Display("Validate", "DisplayErrorMessage('Select To date.');");
        //        return false;
        //    }

        //    if (ddlScheduleOptions.SelectedValue.Trim() == "")
        //    {
        //        lblMessage.Text = "Select CO Type.";
        //        objcommon.Display("Validate", "DisplayErrorMessage('Select CO Type.');");
        //        return false;
        //    }
        //    return true;
        //}

        protected void lnkcancel_Click(object sender, EventArgs e)
        {

        }

        protected void lnkstatus_Click(object sender, EventArgs e)
        {

        }

        protected void drpLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AppList.Visible = false;
            AppForm.Visible = true;
            btnAddNew.Visible = false;
            divAlert.Visible = false;
            divAlertSucc.Visible = false;
            divAlert.Visible = false;
            anotherleave.Visible = false;
            ClearFields();
        }

        protected void ddlScheduleOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            dsInv = objNpl.getDays((string)Session["sCompID"], (string)Session["sEmpID"], txtFRDate.Text, txtTODate.Text);
            lblTotalDays.Text = dsInv.Tables[0].Rows[0]["NUMBEROFDAYS"].ToString();

            if (ddlScheduleOptions.SelectedItem.ToString() != "")
            {
                int totalDays = Convert.ToInt16(lblTotalDays.Text);
                string selectedOption = ddlScheduleOptions.SelectedItem.ToString();

                //Check if total days is 2 and the selected option is for a 2 - day holiday
                if (selectedOption == "PH + 24 HRS on Weekly Off - 2 CO" || selectedOption == "24 HRS on 2nd Weekly Off - 2 CO")
                {
                    anotherleave.Visible = true;
                    //LeaveDiv.Visible = false;
                    idlbltotaldays.Visible = false;
                    btnApprove.Visible = true;
                    lblLeaveFromDate.InnerText = "First leave date :";

                }
                if (totalDays == 1 && (selectedOption == "24 Hours Duty" || selectedOption == "PH on Weekly Off" || selectedOption == "Working on 2nd Weekly Off"))
                {
                    btnApprove.Visible = false;
                    divAlert.Visible = true;
                    anotherleave.Visible = false;
                    //  LeaveDiv.Visible = true;
                    idlbltotaldays.Visible = true;
                    txtTODate.Text = "";

                }
                else if (totalDays != 1 && (selectedOption == "24 Hours Duty" || selectedOption == "PH on Weekly Off" || selectedOption == "Working on 2nd Weekly Off"))
                {
                    txtTODate.Text = "";
                    btnApprove.Visible = true;
                    divAlert.Visible = false;
                    //lblMessage.Text = "Select CO Type Correct One. Number Of Total Days Should Be 1. Not Less Than Or Not Greater Than.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Select CO Type Correct One. Number Of Total Days Should Be 1. Not Less Than Or Not Greater Than.');");
                    anotherleave.Visible = false;
                    // LeaveDiv.Visible = true;
                    idlbltotaldays.Visible = true;

                }
                //else if (totalDays != 2 && (selectedOption == "PH + 24 HRS on Weekly Off - 2 CO" || selectedOption == "24 HRS on 2nd Weekly Off - 2 CO"))
                //{
                //    btnApprove.Visible = false;
                //    divAlert.Visible = true;
                //    lblMessage.Text = "Select CO Type Correct One. Number Of Total Days Should Be 2. Not Less Than Or Not Greater Than.";
                //    objcommon.Display("Validate", "DisplayErrorMessage('Select CO Type Correct One. Number Of Total Days Should Be 2. Not Less Than Or Not Greater Than.');");
                //}

            }
            else
            {
                lblMessage.Text = "Select CO Type.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select CO Type.');");
            }

            SetDays();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            AppList.Visible = true;
            AppForm.Visible = false;
            btnAddNew.Visible = true;
            divAlert.Visible = false;
            divAlertSucc.Visible = false;
            ClearFields();
            FillCOList();
            Response.Redirect("COGeneratedPage.aspx");

        }

        protected void txtCODate_TextChanged(object sender, EventArgs e)
        {
        }

    }
}