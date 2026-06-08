using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace NewPortal2023.ESS
{
    public partial class NPL_OutSideDuty : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveApplication objInv = new NewPortal2023.ESS.LeaveApplication();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.DBUtility obdbutility = new NewPortal2023.ESS.DBUtility();
        DataSet dsInv = new DataSet();
        string OldYear, NewYear;
        string ID = "0";
        string joindate;
        string leaveAddDate;
        string status;
        int TotalMonth, lastUpdateTotalMonth;
        double count;

        protected void Page_Load(object sender, EventArgs e)
        {
              if (Session["sCompID"]!=null)
            {
            
            if (!Page.IsPostBack)
            {
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                if (Session["sCompID"] != null)
                {
                    try
                    {
                        AppList.Visible = true;
                        AppForm.Visible = false;

                        FillLeave();

                        //GetDetail();
                        string strResult = objcommon.Validate_ControlInfo("INV");
                        ID = Convert.ToString(Request.QueryString["id"]);
                        if (strResult.Contains("This page is currently unavailable.") == true)
                        {
                            Response.Redirect("Unavailable.aspx?strName=Investment Details");
                            return;
                        }

                        if (ID != "" && ID != null)
                        {
                            FillDetails();
                        }
                        else
                        {
                            FillStatus("0");
                        }
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
              else
              {
                  Response.Redirect("Login.aspx");
              }
        }

        private void LockDetails()
        {
            //txtFromDate.ReadOnly = true;
            //txtToDate.ReadOnly = true;

            //txtRem.ReadOnly = true;
            //drpStatus.Enabled = true;


            //btnFrom.Visible = false;
            //btnTo.Visible = false;
            //drpLeave.Enabled = false;

        }
        private void FillDetails()
        {
            DataSet ds = new DataSet();


            ds = objNps.GetDetails((string)Session["sCompID"], (string)Session["sEmpID"], ID);

            if (ds.Tables.Count > 0)
            {
                for (int cnt = 0; cnt <= ds.Tables[0].Rows.Count - 1; cnt++)
                {
                    txtFromDate.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["FROM_DT"]);
                    txtToDate.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["TO_DATE"]);

                    txtRem.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["REMARKS"]);
                    FillStatus(Convert.ToString("0"));
                    //FillStatus(Convert.ToString(ds.Tables[0].Rows[cnt]["STATUS"]));
                    drpStatus.SelectedValue = Convert.ToString(ds.Tables[0].Rows[cnt]["STATUS"]);
                    drpType.SelectedValue = Convert.ToString(ds.Tables[0].Rows[cnt]["ODTYPE"]);
                    if (drpType.SelectedValue == "Specific_Time")
                    {
                        trTime1.Visible = true;
                        trTotalhrTime.Visible = true;
                        lblNotifymessage.Text = "Note:- Please insert From Time or To Time in the Textbox only 24. hours format.";
                        txtFormTime.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["From_Time"]);
                        txtToTime.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["To_Time"]);
                        txtTotalHrTime.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["TOTAL_HRS"]);
                    }
                    else
                    {
                        trTime1.Visible = false;
                        trTotalhrTime.Visible = false;
                        // lblNotifymessage.Text = "Note:- Please insert From Time or To Time in the Textbox only 24. hours format.";
                    }

                    lblcrdate.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["CREATEDDT"]);
                    lblLeaves.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["TOTALDAYS"]);
                    if (Convert.ToString(ds.Tables[0].Rows[cnt]["STATUS"]) != "0")
                    {
                        LockDetails();
                    }
                    AppList.Visible = false;
                    AppForm.Visible = true;
                    btnAddNew.Visible = false;
                }

            }
            else
            {
                throw new Exception("Error");

            }
        }

        private void GetDetail()
        {
            DataSet ds = new DataSet();
            ds = objNps.GetDetailEmps((string)Session["sCompID"], (string)Session["sEmpID"], ID);
            if (ds.Tables.Count > 0)
            {
                for (int cnt = 0; cnt <= ds.Tables[0].Rows.Count - 1; cnt++)
                {
                    //joindate = Convert.ToString(ds.Tables[0].Rows[cnt]["Date"]);
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int cnt1 = 0; cnt1 <= ds.Tables[1].Rows.Count - 1; cnt1++)
                    {

                        leaveAddDate = Convert.ToString(ds.Tables[1].Rows[cnt1]["Leave_Add_Date"]);
                        //joindate = Convert.ToString(ds.Tables[1].Rows[cnt1]["Date"]);
                        status = Convert.ToString(ds.Tables[1].Rows[cnt1]["status"]);
                    }
                }
                string currentDate = DateTime.Now.ToString();

                //string[] strDateTime = joindate.Split(' ');
                //string[] leaveUpdateDateTime = leaveAddDate.Split(' ');
                //string[] endDateTime = currentDate.Split(' ');

                DateTime strDate = Convert.ToDateTime(joindate);
                DateTime leavelastUpdateDate = Convert.ToDateTime(leaveAddDate);
                DateTime endDate = Convert.ToDateTime(currentDate);


                if (leavelastUpdateDate != null && endDate != null)
                {
                    getMonthlastUpdate(leavelastUpdateDate, endDate);
                    if (lastUpdateTotalMonth >= 0)
                    {
                        for (int i = 0; i < lastUpdateTotalMonth; i++)
                        {
                            count = count + 2.667;

                        }
                    }
                }
                else if (strDate != null && endDate != null)
                {
                    getMonthstart(strDate, endDate);
                    for (int i = 0; i < TotalMonth; i++)
                    {
                        count = count + 2.667;
                        if (TotalMonth == 2.667)
                        {
                            DataSet ds1 = new DataSet();
                            // ds1 = obdbutility.
                        }
                        else if (TotalMonth == 5.334)
                        {
                        }
                        else if (TotalMonth == 8.001)
                        {

                        }
                    }
                }

            }
        }

        private int getMonthlastUpdate(DateTime leaveUpdateDate, DateTime endDate)
        {
            lastUpdateTotalMonth = Math.Abs((leaveUpdateDate.Month - endDate.Month) + 12 * (leaveUpdateDate.Year - endDate.Year));
            return lastUpdateTotalMonth;
        }

        private int getMonthstart(DateTime strDate, DateTime endDate)
        {

            TotalMonth = Math.Abs((strDate.Month - endDate.Month) + 12 * (strDate.Year - endDate.Year));
            return TotalMonth;
        }

        private void FillStatus(string Status)
        {
            dsInv = objInv.GetStatus(Status);
            drpStatus.Items.Clear();
            drpStatus.DataTextField = "status";
            drpStatus.DataValueField = "id";
            drpStatus.DataSource = dsInv;
            drpStatus.DataBind();
            drpStatus.Items.Insert(0, new ListItem("[Select One]", "-"));
        }

        protected void SetDays()
        {
            if (txtFromDate.Text.Trim() != "" && txtToDate.Text.Trim() != "")
            {
                lblLeaves.Text = " ";
            }
        }


        protected void drpStatus_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //if (ValidateData() == true)
            //{
            //    SetDays();
            //}
        }

        protected void txtFromDate_OnTextChanged(object sender, EventArgs e)
        {
            if (ValidateData() == true)
            {
                SetDays();
            }
        }
        protected void txtToDate_OnTextChanged(object sender, EventArgs e)
        {
            if (ValidateData() == true)
            {
                SetDays();
            }
        }

        private Boolean ValidateData()
        {
            if (drpType.SelectedValue == "0")
            {
                lblMessage.Text = "Please Select OutDoor Type.";
                objcommon.Display("Validate", "DisplayErrorMessage('Please Select OutDoor Type.');");
                return false;
            }
            if (txtFromDate.Text.Trim() == "")
            {
                lblMessage.Text = "Select From date.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select From date.');");
                return false;
            }
            if (txtToDate.Text.Trim() == "")
            {
                lblMessage.Text = "Select To date.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select To date.');");
                return false;
            }
            //if (Convert.ToDateTime(txtFromDate.Text) >= Convert.ToDateTime(txtToDate.Text))
            //{
            //    lblMessage.Text = "From date can not be greater than to date.";
            //    objcommon.Display("Validate", "DisplayErrorMessage('From date can not be greater than to date.');");
            //    return false;
            //}
            //if (objcommon.ValidateFinalcialYear(Convert.ToDateTime(txtFromDate.Text)) == false)
            //{
            //    lblMessage.Text = "From date is not within current financial year.";
            //    objcommon.Display("Validate", "DisplayErrorMessage('From date is not within current financial year.');");
            //    return false;

            //}
            //if (objcommon.ValidateFinalcialYear(Convert.ToDateTime(txtToDate.Text)) == false)
            //{
            //    lblMessage.Text = "To date is not within current financial year.";
            //    objcommon.Display("Validate", "DisplayErrorMessage('To date is not within current financial year.');");
            //    return false;

            //}

            if (drpStatus.SelectedValue.Trim() == "-")
            {
                lblMessage.Text = "Select status.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select status.');");
                return false;
            }
            return true;
        }
        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                if (ValidateData() == true)
                {
                    
                    DateTime frDate = DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", null);//Convert.ToDateTime(txtFromDate.Text.Trim()).Date;
                    DateTime toDate = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", null); //Convert.ToDateTime(txtToDate.Text.Trim()).Date;
                    if (frDate <= toDate)
                    {
                        string finYear = objcommon.GetFinalcialYear();
                        string[] years = finYear.Split('.');
                        OldYear = years[1];
                        NewYear = years[0];

                        if (Convert.ToString(Request.QueryString["id"]) != "" && Convert.ToString(Request.QueryString["id"]) != null)
                        {
                            result = objNps.UpdateLeaveStatus((string)Session["sCompID"], (string)Session["sEmpName"], (string)Session["sEmpID"], drpStatus.SelectedValue, Convert.ToString(Request.QueryString["id"]), txtFromDate.Text, txtToDate.Text, txtFormTime.Text, txtToTime.Text, txtTotalHrTime.Text, txtRem.Text, drpType.SelectedValue, NewYear, lblLeaves.Text);
                        }
                        else
                        {
                            result = objNps.UpdateLeave((string)Session["sCompID"], (string)Session["sEmpName"], (string)Session["sEmpID"], txtFromDate.Text, txtToDate.Text, txtFormTime.Text, txtToTime.Text, txtTotalHrTime.Text, txtRem.Text, drpStatus.SelectedValue, drpType.SelectedValue, NewYear, lblLeaves.Text);
                        }
                        //result = "";
                        if (result.ToString().Trim() == "success")
                        {
                            lblMessage.Text = "Successfuly sent Out Side Duty application for approval.";
                            objcommon.Display("Validate", "DisplayErrorMessage('Successfuly sent Out Side Duty application for approval.');");
                            if (Convert.ToString(Request.QueryString["id"]) != "" && Convert.ToString(Request.QueryString["id"]) != null && (Convert.ToString(Request.QueryString["sender"]) == "" || Convert.ToString(Request.QueryString["sender"]) == null))
                            {
                                APPROVEMAIL(txtFromDate.Text, txtToDate.Text, drpType.SelectedItem.Text);
                                Response.Redirect("NPL_OutSideDutyApproval.aspx");

                            }
                            else
                            {
                                SENDUDATEMAIL(txtFromDate.Text, txtToDate.Text, drpType.SelectedItem.Text);
                                Response.Redirect("outSideDutyHistory.aspx");

                            }
                        }
                        else if (result.ToString().Trim() == "duplicate")
                        {
                            lblMessage.Text = "Application already exists for selected date.";
                            return;
                        }
                        else
                        {
                            lblMessage.Text = "Error occurred in application.";
                            //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                        }
                    }
                    else
                    {
                        divAlert.Visible = true;
                        lblMessage.Text = "";
                        lblMessage.Text = "To Date " + txtToDate.Text + " should not to be less than From Date. " + txtFromDate.Text;

                    }
                        
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
            }

        }

        private void SENDUDATEMAIL(string frDate, string toDate, string oDType)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DateTime date = DateTime.Now;
            dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], (string)Session["sEmpID"]);

            //if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            {
                //if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
                //{
                string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();

                string clientbodys = "Dear " + EMPNAME + ",<br><br>Your Out Door Duty Application is Submitted Successfully.<br>"
                   + "Once action taken by your Reporting Manager, will be notified to you through e-mail or same can be checked on the ESS portal."
                   + "<br><br> Details"
                   + " <br> Out Door Duty Type :-" + oDType
                   + " <br> Applied Date :-" + date
                   + "<br> From Date :- " + frDate
                   + "<br> To Date :- " + toDate
                   + "<br><br>ThankYou."
                   + "< br >Payroll Team< br > ";
                string emails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
                string subjects = "Do Not Reply: Out Door Duty Application";
                emailSend.SendEmailNPL(emails, subjects, clientbodys);


                string checkerbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>" + EMPNAME + " Out door Application is received for your approval.Your action is requested for the same through logging-in into ESS portal."
                   + "<br><br> Details"
                   + " <br> Out Door Duty Type :-" + oDType
                   + " <br> Applied Date :-" + date
                   + "<br> From Date :- " + frDate
                   + "<br> To Date :- " + toDate
                   + "<br><br>ThankYou."
                   + "< br >Payroll Team< br > ";
                //\nWith Best Regards,\nSequel Outsourcing PVT.LTD<br> < br >
                emails = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
                emailSend.SendEmailNPL(emails, subjects, checkerbodys);

                //}
            }

        }

        private void APPROVEMAIL(string frDate, string toDate, string oDType)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            string id = Convert.ToString(Request.QueryString["id"]);
            dsmkkMail = emailSend.GetEmpAttendanceaPPROVE((string)Session["sCompID"], (string)Session["sEmpID"], id);

            //if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            {
                //if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
                //{
                string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();

                string clientbodys = "Dear " + EMPNAME + ",<br><br>Out Door Duty Application is " + drpStatus.SelectedItem.Text + " Succussfully.<br>"
                   + "<br><br> Details"
                   + " <br> Out Door Type :-" + oDType
                   // + " <br> Applied Date :-" + date
                   + "<br> From Date :- " + frDate
                   + "<br> To Date :- " + toDate
                   + "<br><br>ThankYou."
                   + "< br >Payroll Team< br > ";
                string emails = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
                string subjects = "Do Not Reply: Out Door Duty Application";
                //emailSend.SendEmailNPL(emails, subjects, clientbodys);

                emails = "";
                string checkerbodys = "Dear User,<br><br>" + EMPNAME + "<br>has Reviewed your Out Door Duty Application.Your Out Door Duty appplication is " + drpStatus.SelectedItem.Text + "."
                     + "<br><br> Details"
                    + " <br> Out Door Type :-" + oDType
                   //+ " <br> Applied Date :-" + date
                   + "<br> From Date :- " + frDate
                   + "<br> To Date :- " + toDate
                   + "<br><br>ThankYou."
                   + "< br >Payroll Team< br > ";
                //\nWith Best Regards,\nSequel Outsourcing PVT.LTD<br> < br >
                emails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
                emailSend.SendEmailNPL(emails, subjects, checkerbodys);



                //}
            }

        }

        protected void btnDraft_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                if (ValidateData() == true)
                {

                    string finYear = objcommon.GetFinalcialYear();
                    string[] years = finYear.Split('.');
                    OldYear = years[1];
                    NewYear = years[0];

                    result = objNps.UpdateLeave((string)Session["sCompID"], (string)Session["sEmpName"], (string)Session["sEmpID"], txtFromDate.Text, txtToDate.Text, txtFormTime.Text, txtToTime.Text, txtTotalHrTime.Text, txtRem.Text, "0", drpType.SelectedValue, NewYear, lblLeaves.Text);
                    if (result.ToString().Trim() == "success")
                    {
                        lblMessage.Text = "Successfuly saved application.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Successfuly saved application.');");
                        Response.Redirect("NPL_OutSideDutyEdit.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
            }
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            SetDays();
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            SetDays();
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpType.SelectedValue == "Specific_Time")
            {
                Clear();
                trTime1.Visible = true;
                trTotalhrTime.Visible = true;
                lblNotifymessage.Text = "Note:- Please insert From Time or To Time in the Textbox only 24. hours format.";
            }
            else
            {
                Clear();
                trTime1.Visible = false;
                trTotalhrTime.Visible = false;
                lblNotifymessage.Text = "";
            }

        }

        private void Clear()
        {
            txtFromDate.Text = "";
            txtFormTime.Text = "";
            txtToDate.Text = "";
            txtToTime.Text = "";
            txtTotalHrTime.Text = "";
            lblMessage.Text = "";
        }

        protected void txtFormTime_TextChanged(object sender, EventArgs e)
        {
            TimeSpan ts1 = TimeSpan.Parse(txtFormTime.Text);

            if (txtToTime.Text != "")
            {
                TimeSpan ts2 = TimeSpan.Parse(txtToTime.Text);
                string totaltexttotime = txtToTime.Text;
                string[] totalhrsmintime = totaltexttotime.Split(':');
                string tohrs = totalhrsmintime[0];
                string tomin = totalhrsmintime[1];
                ///From hrs ///////////////
                string totaltextFromtime = txtFormTime.Text;
                string[] totalhrsFrommintime = totaltextFromtime.Split(':');
                string Frmhrs = totalhrsFrommintime[0];
                string frmmin = totalhrsFrommintime[1];


                var startTime = new TimeSpan(Convert.ToInt16(Frmhrs), Convert.ToInt16(frmmin), 0);
                var endTime = new TimeSpan(Convert.ToInt16(tohrs), Convert.ToInt16(tomin), 0);
                var hours24 = new TimeSpan(24, 0, 0);
                var difference = endTime.Subtract(startTime);
                difference = (difference.Duration() != difference) ? hours24.Subtract(difference.Duration()) : difference;
                txtTotalHrTime.Text = difference.ToString();
                lblMessage.Text = "";
            }
            else
            {
                lblMessage.Text = "Please Enter Out Door To Time";
            }
        }

        protected void txtToTime_TextChanged(object sender, EventArgs e)
        {
            TimeSpan ts2 = TimeSpan.Parse(txtToTime.Text);
            if (txtFormTime.Text != "")
            {
                TimeSpan ts1 = TimeSpan.Parse(txtFormTime.Text);
                string totaltexttotime = txtToTime.Text;
                string[] totalhrsmintime = totaltexttotime.Split(':');
                string tohrs = totalhrsmintime[0];
                string tomin = totalhrsmintime[1];
                ///From hrs ///////////////
                string totaltextFromtime = txtFormTime.Text;
                string[] totalhrsFrommintime = totaltextFromtime.Split(':');
                string Frmhrs = totalhrsFrommintime[0];
                string frmmin = totalhrsFrommintime[1];


                var startTime = new TimeSpan(Convert.ToInt16(Frmhrs), Convert.ToInt16(frmmin), 0);
                var endTime = new TimeSpan(Convert.ToInt16(tohrs), Convert.ToInt16(tomin), 0);
                var hours24 = new TimeSpan(24, 0, 0);
                var difference = endTime.Subtract(startTime);
                difference = (difference.Duration() != difference) ? hours24.Subtract(difference.Duration()) : difference;
                txtTotalHrTime.Text = difference.ToString();
                lblMessage.Text = "";
            }
            else
            {
                lblMessage.Text = "Please Enter Out Door From Time";
            }
        }

        private void FillLeave()
        {
            string finYear = objcommon.GetFinalcialYear();
            string[] years = finYear.Split('.');
            OldYear = years[1];
            NewYear = years[0];

            dsInv = objNps.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], NewYear, OldYear);
            gvLeave.DataSource = dsInv;
            gvLeave.DataBind();
        }

        protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int cnt = 0;
            if (e.Row.RowType == DataControlRowType.Header)
            {
                foreach (TableCell cell in e.Row.Cells)
                {
                    cell.CssClass = "GridViewHeader";
                    cell.HorizontalAlign = HorizontalAlign.Center;

                }
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton hyper = (LinkButton)e.Row.FindControl("lnkstatus");
                LinkButton cancel = (LinkButton)e.Row.FindControl("lnkcancel");
                hyper.Enabled = false;

                if (hyper.Text == "Approved" || hyper.Text == "Draft" || hyper.Text == "Rejected")
                {
                    hyper.Enabled = true;
                }

                if (hyper.Text == "Submitted")
                {
                    cancel.Visible = true;
                }
                else
                {
                    cancel.Visible = false;
                }

                foreach (TableCell cell in e.Row.Cells)
                {
                    if (cnt == 0 || cnt == 1)
                    {
                        cell.CssClass = "GridViewItem";
                        cell.HorizontalAlign = HorizontalAlign.Center;
                    }
                    else
                    {
                        cell.CssClass = "GridViewItem";
                        cell.HorizontalAlign = HorizontalAlign.Center;
                    }

                    if (cnt + 1 == e.Row.Cells.Count && cell.Text == "No")
                    {
                        cell.BackColor = System.Drawing.Color.Wheat;
                    }
                    cnt = cnt + 1;
                }
            }

        }

        protected void lnkstatus_Click(object sender, EventArgs e)
        {
            LinkButton lnkstatus = (LinkButton)sender;
            Label lblid = (Label)lnkstatus.NamingContainer.FindControl("lblId");
            Response.Redirect("NPLOutSideDuty.aspx?sender=me&id=" + lblid.Text);

        }

        protected void lnkcancel_Click1(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkcancel = (LinkButton)sender;
                Label lblid = (Label)lnkcancel.NamingContainer.FindControl("lblId");
                objNps.CancelLeave(lblid.Text);
                FillLeave();
                lblMessage.Text = "Out Side Duty application cancelled.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AppList.Visible = false;
            AppForm.Visible = true;
            btnAddNew.Visible = false;
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            AppList.Visible = true;
            AppForm.Visible = false;
            btnAddNew.Visible = true;
            string script = "<script type='text/javascript'>setTimeout(function() { hideLoader(); }, 1000);</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "HideLoaderScript", script);
        }
    }
}