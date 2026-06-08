using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

namespace NewPortal2023.ESS
{
    public partial class NPLEmpAttendanceDetails : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveApplication objInv = new NewPortal2023.ESS.LeaveApplication();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        NewPortal2023.ESS.NplCoApp objCoApp = new NewPortal2023.ESS.NplCoApp();
        DataSet dsInv = new DataSet();
        DataSet ds = new DataSet();
        string message;
        int Count = 0;
        string OldYear, NewYear;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    divViewList.Visible = true;
                    trSubmit1.Visible = true;
                    trEdit1.Visible = false;
                    Generated();
                }


            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }
        //private void FillStatus(string Status)
        //{
        //    ds = objInv.GetStatus(Status);
        //    drpStatus.Items.Clear();
        //    drpStatus.DataTextField = "status";
        //    drpStatus.DataValueField = "id";
        //    drpStatus.DataSource = ds;
        //    drpStatus.DataBind();
        //    drpStatus.Items.Insert(0, new ListItem("[Select One]", "-"));
        //}
        private void CalCulation()
        {

            foreach (GridViewRow Row in gvList.Rows)
            {
                if (Row.RowType == DataControlRowType.DataRow)
                {
                    TimeSpan IN_TIME;
                    TimeSpan OUT_TIME;
                    int index = Row.RowIndex;
                    if (index == 0)
                    {
                        Count = 0;
                    }
                    Label lblStat = (Label)Row.FindControl("lblStat");
                    Label lblStatusShift = (Label)Row.FindControl("lblStatusTRANS");
                    Label lblStatusTimein = (Label)Row.FindControl("lblStatusREIMB");
                    Label lblTotalWorking = (Label)Row.FindControl("lblStatusLUP");
                    Label lblouttime = (Label)Row.FindControl("lblStatusTAX");
                    Label lblot = (Label)Row.FindControl("lblStatusJA");
                    Label lblCO = (Label)Row.FindControl("lblStatus");
                    TextBox txtAttRemark = (TextBox)Row.FindControl("txtAttRemark");

                    if (lblouttime.Text != "")
                    {
                        DateTime OUTtime = Convert.ToDateTime(lblouttime.Text);
                        OUT_TIME = OUTtime.TimeOfDay;
                    }
                    else
                    {
                        DateTime OUTtime = Convert.ToDateTime("00:00:00");
                        OUT_TIME = OUTtime.TimeOfDay;
                    }
                    if (lblStatusTimein.Text != "")
                    {
                        DateTime time = Convert.ToDateTime(lblStatusTimein.Text);
                        IN_TIME = time.TimeOfDay;
                    }
                    else
                    {
                        DateTime time = Convert.ToDateTime("00:00:00");
                        IN_TIME = time.TimeOfDay;

                    }
                    if (lblStatusTimein.Text == lblouttime.Text)
                    {
                        txtAttRemark.Text = "Missing - Punch Time";
                        Row.Cells[16].BackColor = System.Drawing.Color.OrangeRed;
                    }

                    DateTime Com = Convert.ToDateTime("00:00:00");
                    TimeSpan CompareWith = Com.TimeOfDay;

                    if (lblStat.Text == "" || lblStat.Text == "0")
                    {
                        Row.Cells[17].Visible = false;
                    }
                    if (lblot.Text != "" && lblCO.Text != "")
                    {
                        if (lblot.Text == "")
                        {
                            lblot.Text = "0";
                        }
                        if (lblCO.Text == "")
                        {
                            lblCO.Text = "0";
                        }
                        double OT = Convert.ToDouble(lblot.Text);
                        double CO = Convert.ToDouble(lblCO.Text);

                        if (CO > 0)
                        {
                            txtAttRemark.Text = "CO";
                            Row.Cells[16].BackColor = System.Drawing.Color.Green;
                        }
                        if (CO > 0 && OT > 0)
                        {
                            txtAttRemark.Text = "OT + CO";
                            Row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;
                        }
                    }

                    else if (lblot.Text != "")
                    {
                        if (lblot.Text == "")
                        {
                            lblot.Text = "0";
                        }
                        if (lblCO.Text == "")
                        {
                            lblCO.Text = "0";
                        }
                        double OT = Convert.ToDouble(lblot.Text);
                        if (OT > 0)
                        {
                            txtAttRemark.Text = "OT";
                            Row.Cells[16].BackColor = System.Drawing.Color.LightPink;
                        }
                    }
                    else if (lblot.Text != "")
                    {
                        if (lblot.Text == "")
                        {
                            lblot.Text = "0";
                        }
                        if (lblCO.Text == "")
                        {
                            lblCO.Text = "0";
                        }
                        double CO = Convert.ToDouble(lblCO.Text);
                        if (CO > 0)
                        {
                            txtAttRemark.Text = "CO";
                            Row.Cells[16].BackColor = System.Drawing.Color.LightSkyBlue;
                        }
                    }


                    if (lblStatusShift.Text == "GN" || lblStatusShift.Text == "EV" || lblStatusShift.Text == "GS" || lblStatusShift.Text == "MS" || lblStatusShift.Text == "NS")
                    {
                        if (IN_TIME == CompareWith && OUT_TIME == CompareWith)
                        {
                            txtAttRemark.Text = "Absent";
                            Row.Cells[16].BackColor = System.Drawing.Color.Red;
                        }

                        if (lblouttime.Text == "" || lblStatusTimein.Text == "")
                        {
                            txtAttRemark.Text = "Rectification";
                            Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                        }
                        if (lblTotalWorking.Text != "" && lblTotalWorking.Text != "0.00")
                        {
                            double totalWorkinghours = Convert.ToDouble(lblTotalWorking.Text);
                            if (totalWorkinghours <= 4.00)
                            {
                                txtAttRemark.Text = "Half day Absent";
                                Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                            }
                        }


                    }
                    else if (lblStatusShift.Text == "WO")
                    {
                        if (IN_TIME != CompareWith || OUT_TIME != CompareWith)
                        {
                            txtAttRemark.Text = "Working on holiday";
                            Row.Cells[16].BackColor = System.Drawing.Color.Green;
                        }
                        if (IN_TIME == CompareWith || OUT_TIME == CompareWith)
                        {
                            txtAttRemark.Text = "Weekly Off";
                            Row.Cells[16].BackColor = System.Drawing.Color.LightGreen;
                        }

                    }

                }
            }
        }


        private void Co_applicationchecking()
        {
            string finYear = objcommon.GetFinalcialYear();
            string[] years = finYear.Split('.');
            OldYear = years[1];
            NewYear = years[0];

            ds = objCoApp.GetCOList(Session["sCompID"].ToString(), Session["sEmpID"].ToString(), NewYear);

            foreach (GridViewRow Row in gvList.Rows)
            {
                if (Row.RowType == DataControlRowType.DataRow)
                {
                    Label lbllblOrigin = (Label)Row.FindControl("lblOrigin");
                    TextBox txtAttRemark = (TextBox)Row.FindControl("txtAttRemark");

                    if (lbllblOrigin != null)
                    {
                        try
                        {
                            //DateTime originDate = Convert.ToDateTime(lbllblOrigin.Text);

                            foreach (DataRow dataRow in ds.Tables[0].Rows)
                            {
                                //DateTime rowDate = Convert.ToDateTime(dataRow["FR_DATE"]);

                                if (dataRow["FR_DATE"].ToString() == lbllblOrigin.Text)
                                {
                                    string status = dataRow["STATUSTYPE"].ToString();
                                    if (status == "APPROVED")
                                    {
                                        txtAttRemark.Text = "Comp Off";
                                        Row.Cells[16].BackColor = System.Drawing.Color.Green;
                                    }
                                    else if (status == "SUBMITTED")
                                    {
                                        txtAttRemark.Text = "Comp Off/Approval For Pending";
                                        Row.Cells[16].BackColor = System.Drawing.Color.Blue;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                        }

                    }
                }
            }
        }

        private void OutSideDuty()
        {

            objNps.EmpCode = Session["sEmpCode"].ToString();
            objNps.EmpName = Session["sEmpName"].ToString();
            ds = objNps.GetOutSideDuty((string)Session["sCompID"], (string)Session["sEmpCode"]);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; Convert.ToBoolean(ds.Tables[0].Rows.Count); i++)
                {
                    if (ds.Tables[0].Rows.Count != i)
                    {
                        string OutSIde = ds.Tables[0].Rows[i]["FROM_DT"].ToString();
                        string OutSIdeReason = ds.Tables[0].Rows[i]["LEAVE_REASON"].ToString();
                        string ReasonCode = ds.Tables[0].Rows[i]["REASON_ID"].ToString();

                        // Convert string to DateTime object
                        //DateTime dateTime = DateTime.ParseExact(OutSIde, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        //// Format DateTime to "DD-MM-YYYY" format
                        //string dateFormatted = dateTime.ToString("dd-MM-yyyy");
                        string OutSideRmark = ds.Tables[0].Rows[i]["ODTYPE"].ToString();
                        string STATUS = ds.Tables[0].Rows[i]["STATUS"].ToString();
                        string HOLIDAY = ds.Tables[0].Rows[i]["HOLIDAY"].ToString();

                        foreach (GridViewRow Row in gvList.Rows)
                        {
                            if (Row.RowType == DataControlRowType.DataRow)
                            {
                                int index = Row.RowIndex;
                                if (index == 0)
                                {
                                    Count = 0;
                                }
                                Label lblStat = (Label)Row.FindControl("lblOrigin");
                                Label lblStatusShift = (Label)Row.FindControl("lblStatusTRANS");
                                TextBox txtAttRemark = (TextBox)Row.FindControl("txtAttRemark");
                                string txt = txtAttRemark.Text;
                                if (OutSIdeReason != "")
                                {

                                    if (lblStat.Text == OutSIde)
                                    {
                                        if (STATUS == "2")
                                        {
                                            txtAttRemark.Text = OutSIdeReason;
                                            Row.Cells[16].BackColor = System.Drawing.Color.Green;
                                        }
                                        else
                                        {
                                            txtAttRemark.Text = OutSIdeReason + " /Approval Pending";
                                            Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                                        }
                                        if (HOLIDAY == "1")
                                        {
                                            txtAttRemark.Text = OutSideRmark + "Paid Holiday";
                                            Row.Cells[16].BackColor = System.Drawing.Color.LightPink;
                                        }
                                    }

                                }
                                else
                                {

                                    if (lblStat.Text == OutSIde)
                                    {
                                        if (STATUS == "2")
                                        {
                                            txtAttRemark.Text = OutSideRmark + " /Out Door Duty";
                                            Row.Cells[16].BackColor = System.Drawing.Color.Green;
                                        }

                                        else
                                        {
                                            txtAttRemark.Text = OutSideRmark + " /Approval Pending";
                                            Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                                        }
                                    }
                                }

                                if ((string)Session["sEmpCode"] == "NP3327" || (string)Session["sEmpCode"] == "NP3349" && lblStatusShift.Text == "PH")
                                {
                                    txtAttRemark.Text = "Paid Holiday";
                                    Row.Cells[16].BackColor = System.Drawing.Color.LightGreen;
                                }

                                //if (ReasonCode == "5" && OutSIdeReason == "Sick Leave" && STATUS == "2" && lblStat.Text == OutSIde)
                                //{
                                //    txtAttRemark.Text = "Second Half/Sick Leave";
                                //    Row.Cells[16].BackColor = System.Drawing.Color.Green;
                                //}
                                //if (ReasonCode == "4" && OutSIdeReason == "Sick Leave" && STATUS == "2" && lblStat.Text == OutSIde)
                                //{
                                //    txtAttRemark.Text = "First Half/Sick Leave";
                                //    Row.Cells[16].BackColor = System.Drawing.Color.Green;
                                //}
                                if (ReasonCode == "5" && OutSIdeReason == "Sick Leave" && STATUS == "1" && lblStat.Text == OutSIde)
                                {
                                    txtAttRemark.Text = "Second Half/Sick Leave Applied";
                                    Row.Cells[16].BackColor = System.Drawing.Color.Green;
                                }
                                if (ReasonCode == "5" && OutSIdeReason == "Sick Leave" && STATUS == "2" && lblStat.Text == OutSIde)
                                {
                                    txtAttRemark.Text = "Second Half/Sick Leave Approved";
                                    Row.Cells[16].BackColor = System.Drawing.Color.Green;
                                }
                                if (ReasonCode == "4" && OutSIdeReason == "Sick Leave" && STATUS == "1" && lblStat.Text == OutSIde)
                                {
                                    txtAttRemark.Text = "First Half/Sick Leave Applied";
                                    Row.Cells[16].BackColor = System.Drawing.Color.Green;
                                }
                                if (ReasonCode == "4" && OutSIdeReason == "Sick Leave" && STATUS == "2" && lblStat.Text == OutSIde)
                                {
                                    txtAttRemark.Text = "First Half/Sick Leave Approved";
                                    Row.Cells[16].BackColor = System.Drawing.Color.Green;
                                }
                            }



                        }
                    }
                    else
                    {
                        break;
                    }
                }

            }

        }

        public void Generated()
        {
            try
            {
                DateTime dt = DateTime.Now;


                objNps.FromDate = " ";
                objNps.ToDate = " ";
                objNps.EmpCode = Session["sEmpCode"].ToString();
                objNps.EmpName = Session["sEmpName"].ToString();
                // lblMessage.Text = Convert.ToString(StartDates) +" "+ Convert.ToString(EndDates);

                ds = objNps.GenerateEmpAttendanceReportFlag((string)Session["sCompID"], "EMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) != "")
                    {
                        lblMessage.Text = Convert.ToString(ds.Tables[0].Rows[0]["result"]);
                        gvList.DataSource = null;
                        gvList.DataBind();
                        gvList.Visible = false;
                    }
                    else if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) == "")
                    {
                        gvList.DataSource = ds;
                        gvList.DataBind();
                        gvList.Visible = true;
                        CalCulation();
                        OutSideDuty();
                        Co_applicationchecking();
                        lblMessage.Text = "Attendance Report Generated Successfully.";
                        //DivgvMultipleList.Visible = true;
                        divViewList.Visible = true;
                    }
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnGenerateAttendanceReport_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dt = DateTime.Now;

                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {

                    objNps.FromDate = txtFromDate.Text;
                    objNps.ToDate = txtToDate.Text;
                    objNps.EmpCode = Session["sEmpCode"].ToString();
                    objNps.EmpName = Session["sEmpName"].ToString();
                    ds = objNps.GenerateAttendanceReport((string)Session["sCompID"]);
                    //lblMessage.Text = Convert.ToString(txtFromDate.Text) + " " + Convert.ToString(txtFromDate.Text);
                    if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) != "")
                    {
                        lblMessage.Text = Convert.ToString(ds.Tables[0].Rows[0]["result"]);
                        gvList.DataSource = null;
                        gvList.DataBind();
                        gvList.Visible = false;
                    }
                    else if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) == "")
                    {
                        gvList.DataSource = ds;
                        gvList.DataBind();
                        gvList.Visible = true;
                        CalCulation();
                        OutSideDuty();
                        Co_applicationchecking();
                        lblMessage.Text = "Attendance Report Generated Successfully.";
                    }

                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                    gvList.Visible = false;
                    lblMessage.Text = "Please Select From Date /To Date First Then Click Generate Button";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        //protected void lnkRectification_Click(object sender, EventArgs e)
        //{
        //    lblMessage.Text = "";
        //    LinkButton lnkRequestNo = (LinkButton)sender;
        //    string lnkAppNo = ((Label)lnkRequestNo.NamingContainer.FindControl("lnkAppNo")).Text;
        //    string lblEntryAid = ((Label)lnkRequestNo.NamingContainer.FindControl("lblEntryAid")).Text;
        //    string Emp_Name = ((Label)lnkRequestNo.NamingContainer.FindControl("lblName")).Text;
        //    string Date = ((Label)lnkRequestNo.NamingContainer.FindControl("lblOrigin")).Text;
        //    string Shift_Schedule = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusTRANS")).Text;
        //    string Time_In = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusREIMB")).Text;
        //    string Time_Out = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusTAX")).Text;
        //    Session["OldShift"] = Shift_Schedule;
        //    Session["OldTime_In"] = Time_In;
        //    Session["OldTime_Out"] = Time_Out;
        //    Session["ENTRYAID"] = lblEntryAid;

        //    string Total_Working_hrs = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusLUP")).Text;
        //    string Shift_hrs = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusTRAVEL")).Text;
        //    string OT = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusJA")).Text;
        //    string CO = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatus")).Text;


        //    // Split date and time
        //    //string[] dateTimeParts = Time_In.Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);
        //    //string[] dateTimeOutParts = Time_Out.Split(new[] { "  " }, StringSplitOptions.RemoveEmptyEntries);

        //    //// Extract date and time
        //    //string datePart = dateTimeParts[0] + " " + dateTimeParts[1];
        //    //string timePart = dateTimeParts[2];

        //    //string dateOutPart = dateTimeOutParts[0] + " " + dateTimeOutParts[1];
        //    //string timeOutPart = dateTimeOutParts[2];

        //    string pattern = @"^\s*([a-zA-Z]{3} \d{1,2} \d{4})\s+([0-9:AMPamp]+)\s*$";

        //    Match matchIn = Regex.Match(Time_In, pattern);
        //    Match matchOut = Regex.Match(Time_Out, pattern);

        //    string dateInPart = matchIn.Groups[1].Value; // Extracts "Sep 15 2024"
        //    string timeInPart = matchIn.Groups[2].Value; // Extracts "10:41PM"

        //    string dateOutPart = matchOut.Groups[1].Value; // Extracts "Sep 15 2024"
        //    string timeOutPart = matchOut.Groups[2].Value; // Extracts "10:41PM"


        //    // Parse date
        //    DateTime dateOnly = DateTime.ParseExact(dateInPart, "MMM d yyyy", System.Globalization.CultureInfo.InvariantCulture);
        //    DateTime dateOutOnly = DateTime.ParseExact(dateOutPart, "MMM d yyyy", System.Globalization.CultureInfo.InvariantCulture);

        //    DateTime timeOnly = DateTime.ParseExact(timeInPart, "h:mmtt", System.Globalization.CultureInfo.InvariantCulture);
        //    DateTime timeOutOnly = DateTime.ParseExact(timeOutPart, "h:mmtt", System.Globalization.CultureInfo.InvariantCulture);

        //    string timeIn24HourFormat = timeOnly.ToString("HH:mm:ss");
        //    string timeOut24HourFormat = timeOutOnly.ToString("HH:mm:ss");

        //    // Format the date as dd MM yyyy
        //    string formattedDate = dateOnly.ToString("dd-MM-yyyy");
        //    string formattedOutDate = dateOutOnly.ToString("dd-MM-yyyy");

        //    // Format the time as HH:mm
        //    string formattedTime = timeOnly.ToString("HH:mmtt");
        //    string formattedOutTime = timeOutOnly.ToString("HH:mmtt");


        //    lblEntryId.Text = lblEntryAid;
        //    lblInTime.Text = Time_In;
        //    lblOuttime.Text = Time_Out;
        //    lblRosterSch.Text = Shift_Schedule;
        //    lblEmpCode.Text = lnkAppNo;
        //    lblEmpName.Text = Emp_Name;
        //    lblDate.Text = Date;
        //    drpShiftType.SelectedItem.Text = Shift_Schedule;
        //    txtInDate.Text = formattedDate;
        //    txtIntime.Text = timeIn24HourFormat;
        //    txtOutDate.Text = formattedOutDate;
        //    txtOuttime.Text = timeOut24HourFormat;
        //    lblTWhr.Text = Total_Working_hrs;
        //    lblShr.Text = Shift_hrs;
        //    lblOt.Text = OT;
        //    lblCo.Text = CO;
        //    //lblUpdateOT.Text = "0";
        //    txtUpdateCO.Text = CO;
        //    trEdit1.Visible = true;
        //    divViewList.Visible = false;
        //    trSubmit1.Visible = false;
        //    //trtilehead.Visible = false;
        //    //trtitlehr1.Visible = false;

        //    //    //CalculateOT();
        //}


        //public void CalculateOT()
        //{
        //    int gShifthrs = 9;
        //    int EMNShifthrs = 8;
        //    string time_In = txtInDate.Text + " " + txtIntime.Text;
        //    string time_Out = txtOutDate.Text + " " + txtOuttime.Text;
        //    if (time_In == " ")
        //    {
        //        time_In = null;
        //    }
        //    if (time_Out == " ")
        //    {
        //        time_Out = null;
        //    }
        //    //string time_In = "02-03-2023 18:29:00";
        //    //string time_Out = "03-03-2023 09:29:00";
        //    //DateTime inDateTime = Convert.ToDateTime(time_In);
        //    //DateTime outDateTime = Convert.ToDateTime(time_Out);

        //    DateTime inDateTime;
        //    DateTime.TryParse(time_In, out inDateTime);

        //    DateTime outDateTime;
        //    DateTime.TryParse(time_Out, out outDateTime);
        //    if (time_In != null && time_Out != null)
        //    {
        //        var dt = (inDateTime - outDateTime).TotalHours;
        //        int dth = Convert.ToInt16(dt);
        //        string hours = dt.ToString();
        //        double hrs1 = 0;
        //        double roundhrs = 0;
        //        if (dth < 0)
        //        {
        //            string[] hrs = hours.Split('-');
        //            hrs1 = Convert.ToDouble(hrs[1]);
        //            roundhrs = Math.Round(hrs1);
        //        }
        //        else
        //        {
        //            hrs1 = Convert.ToDouble(hours);
        //            roundhrs = Math.Round(hrs1);
        //        }
        //        lblUpdateOT.Text = roundhrs.ToString();

        //    }
        //    //if (drpShiftType.SelectedItem.Text == "GN" || drpShiftType.SelectedItem.Text == "GH" || drpShiftType.SelectedItem.Text == "GS")
        //    //{

        //    //    if (roundhrs > gShifthrs)
        //    //    {
        //    //        //if (Convert.ToDouble(lblOt.Text) > 1.00)
        //    //        //{
        //    //          //  lblUpdateOT.Text = lblOt.Text;
        //    //        //}
        //    //        //else
        //    //        //{
        //    //            lblUpdateOT.Text = "1";
        //    //       // }
        //    //    }
        //    //    else
        //    //    {
        //    //        lblUpdateOT.Text = "0";
        //    //    }
        //    //}
        //    //else if (drpShiftType.SelectedItem.Text == "EV" || drpShiftType.SelectedItem.Text == "MS" || drpShiftType.SelectedItem.Text == "NS")
        //    //{
        //    //    if (roundhrs > EMNShifthrs)
        //    //    {
        //    //        //if (Convert.ToDouble(lblOt.Text) > 1.00)
        //    //        //{
        //    //          //  lblUpdateOT.Text = lblOt.Text;
        //    //        //}
        //    //        //else
        //    //        //{
        //    //            lblUpdateOT.Text = "1";
        //    //       // }
        //    //    }
        //    //    else
        //    //    {
        //    //        lblUpdateOT.Text = "0";
        //    //    }
        //    //}
        //    //else if (drpShiftType.SelectedItem.Text == "WO" || drpShiftType.SelectedItem.Text == "PH")
        //    //{

        //    //    //if (Convert.ToDouble(lblOt.Text) > 1.00)
        //    //    //{
        //    //        //lblUpdateOT.Text = lblOt.Text;
        //    //    //}
        //    //    //else
        //    //    //{
        //    //        lblUpdateOT.Text = "1";
        //    //   // }
        //    //}
        //    //else
        //    //{
        //    //    lblUpdateOT.Text = "0";
        //    //}
        //}

        //dnyanesh

        protected void lnkRectification_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            LinkButton lnkRequestNo = (LinkButton)sender;
            string lnkAppNo = ((Label)lnkRequestNo.NamingContainer.FindControl("lnkAppNo")).Text;
            string lblEntryAid = ((Label)lnkRequestNo.NamingContainer.FindControl("lblEntryAid")).Text;
            string Emp_Name = ((Label)lnkRequestNo.NamingContainer.FindControl("lblName")).Text;
            string Date = ((Label)lnkRequestNo.NamingContainer.FindControl("lblOrigin")).Text;
            string Shift_Schedule = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusTRANS")).Text;
            string Time_In = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusREIMB")).Text;
            string Time_Out = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusTAX")).Text;
            Session["OldShift"] = Shift_Schedule;
            Session["OldTime_In"] = Time_In;
            Session["OldTime_Out"] = Time_Out;
            Session["ENTRYAID"] = lblEntryAid;

            string Total_Working_hrs = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusLUP")).Text;
            string Shift_hrs = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusTRAVEL")).Text;
            string OT = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusJA")).Text;
            string CO = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatus")).Text;

            string pattern = @"^\s*([a-zA-Z]{3} \d{1,2} \d{4})\s+([0-9:AMPamp]+)\s*$";

            Match matchIn = Regex.Match(Time_In, pattern);
            Match matchOut = Regex.Match(Time_Out, pattern);

            DateTime dateInOnly, timeInOnly, dateOutOnly, timeOutOnly;

            if (matchIn.Success)
            {
                string dateInPart = matchIn.Groups[1].Value;
                string timeInPart = matchIn.Groups[2].Value;

                dateInOnly = DateTime.ParseExact(dateInPart, "MMM d yyyy", System.Globalization.CultureInfo.InvariantCulture);
                timeInOnly = DateTime.ParseExact(timeInPart, "h:mmtt", System.Globalization.CultureInfo.InvariantCulture);

                string timeIn24HourFormat = timeInOnly.ToString("HH:mm:ss");
                string formattedDate = dateInOnly.ToString("dd-MM-yyyy");

                txtInDate.Text = formattedDate;
                txtIntime.Text = timeIn24HourFormat;
            }
            else if (Regex.IsMatch(Time_Out, @"^\s*[a-zA-Z]{3} \d{1,2} \d{4}\s*$"))
            {
                dateInOnly = DateTime.ParseExact(Time_In.Trim(), "MMM d yyyy", System.Globalization.CultureInfo.InvariantCulture);
                timeInOnly = DateTime.ParseExact("12:00AM", "h:mmtt", System.Globalization.CultureInfo.InvariantCulture);

                txtInDate.Text = dateInOnly.ToString("dd-MM-yyyy");
                txtIntime.Text = timeInOnly.ToString("HH:mm:ss");
            }
            else
            {
                txtInDate.Text = string.Empty;
                txtIntime.Text = "00:00:00";
            }

            if (matchOut.Success)
            {
                string dateOutPart = matchOut.Groups[1].Value;
                string timeOutPart = matchOut.Groups[2].Value;

                dateOutOnly = DateTime.ParseExact(dateOutPart, "MMM d yyyy", System.Globalization.CultureInfo.InvariantCulture);
                timeOutOnly = DateTime.ParseExact(timeOutPart, "h:mmtt", System.Globalization.CultureInfo.InvariantCulture);

                string timeOut24HourFormat = timeOutOnly.ToString("HH:mm:ss");
                string formattedOutDate = dateOutOnly.ToString("dd-MM-yyyy");

                txtOutDate.Text = formattedOutDate;
                txtOuttime.Text = timeOut24HourFormat;
            }
            else if (Regex.IsMatch(Time_Out, @"^\s*[a-zA-Z]{3} \d{1,2} \d{4}\s*$"))
            {
                dateOutOnly = DateTime.ParseExact(Time_Out.Trim(), "MMM d yyyy", System.Globalization.CultureInfo.InvariantCulture);
                timeOutOnly = DateTime.ParseExact("12:00AM", "h:mmtt", System.Globalization.CultureInfo.InvariantCulture);

                txtOutDate.Text = dateOutOnly.ToString("dd-MM-yyyy");
                txtOuttime.Text = timeOutOnly.ToString("HH:mm:ss");
            }
            else
            {
                txtOutDate.Text = string.Empty;
                txtOuttime.Text = "00:00:00";
            }

            lblEntryId.Text = lblEntryAid;
            lblInTime.Text = Time_In;
            lblOuttime.Text = Time_Out;
            lblRosterSch.Text = Shift_Schedule;
            lblEmpCode.Text = lnkAppNo;
            lblEmpName.Text = Emp_Name;
            lblDate.Text = Date;
            drpShiftType.SelectedItem.Text = Shift_Schedule;
            lblTWhr.Text = Total_Working_hrs;
            lblShr.Text = Shift_hrs;
            lblOt.Text = OT;
            lblCo.Text = CO;
            txtUpdateCO.Text = CO;
            trEdit1.Visible = true;
            divViewList.Visible = false;
            trSubmit1.Visible = false;
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the 
            //specified ASP.NET server control at run time. 
        }

        //private void FillType()
        //{
        //    dsInv = objInv.GetLeave((string)Session["sCompID"], (string)Session["sEmpID"]);
        //    drpLeave.Items.Clear();
        //    drpLeave.DataTextField = "LEAVE";
        //    drpLeave.DataValueField = "Cid";
        //    drpLeave.DataSource = dsInv.Tables[1];
        //    drpLeave.DataBind();
        //    drpLeave.Items.Insert(0, new ListItem("[Select One]", "0"));
        //}

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvList != null)
            {
                if (gvList.Rows.Count > 0)
                {
                    ExportGridView(gvList);
                }
                else
                {
                    lblMessage.Text = "Generate report first.";
                }
            }
        }
        private void ExportGridView(GridView gvFiles)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            //string FileName = Session["sEmpCode"].ToString() + "_" + DateTime.Now + ".xls";
            string FileName = Session["sEmpCode"].ToString() + "_Attendance_Report_" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvFiles.GridLines = GridLines.Both;
            gvFiles.HeaderStyle.Font.Bold = true;
            gvFiles.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }


        protected void btnApprove_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            try
            {
                if (ValidateData() == true)
                {
                    string time_In = txtInDate.Text + " " + txtIntime.Text;
                    string time_Out = txtOutDate.Text + " " + txtOuttime.Text;
                    //objNps.OT = lblUpdateOT.Text;
                    objNps.OT = txtUpdateOT.Text;
                    objNps.EntryAid = lblEntryId.Text;
                    objNps.Old_OT = lblOt.Text;
                    objNps.CO = txtUpdateCO.Text;
                    objNps.Old_CO = lblCo.Text;
                    objNps.Old_OutTime = lblOuttime.Text;
                    objNps.ToDate = time_Out;
                    objNps.Old_InTime = lblInTime.Text;
                    objNps.FromDate = time_In;
                    objNps.Old_Shift = lblRosterSch.Text;
                    objNps.Shift = drpShiftType.SelectedItem.Text;

                    ds = objNps.UpdateAttend((string)Session["sCompID"], (string)Session["sEmpID"], lblEmpCode.Text, lblEmpName.Text, lblDate.Text, txtRem.Text);

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            string COUNT = Convert.ToString(ds.Tables[0].Rows[0]["status"].ToString().Trim());
                            if (COUNT != "0")
                            {
                                lblMessage.Text = "Successfully Sent for Rectification.";
                                objcommon.Display("Validate", "DisplayErrorMessage('Successfully Sent for Rectification.');");
                                Session["EmpCode"] = lblEmpCode.Text;
                                // SENDUDATEMAIL(txtInDate.Text);
                                Response.Redirect("NPLEmpAttendanceDetails.aspx");

                            }
                            else
                            {
                                lblMessage.Text = "Error .";
                                objcommon.Display("Validate", "DisplayErrorMessage('Error.');");
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                objNps.FromDate = txtFromDate.Text;
                objNps.ToDate = txtToDate.Text;
                objNps.EmpCode = Session["sEmpCode"].ToString();
                objNps.EmpName = Session["sEmpName"].ToString();
                ds = objNps.GenerateAttendanceReport((string)Session["sCompID"]);
                if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) != "")
                {
                    lblMessage.Text = Convert.ToString(ds.Tables[0].Rows[0]["result"]);
                    gvList.DataSource = null;
                    gvList.DataBind();

                    trEdit1.Visible = false;
                    divViewList.Visible = true;
                    trSubmit1.Visible = true;
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) == "")
                {
                    gvList.DataSource = ds;
                    gvList.DataBind();
                    gvList.Visible = true;
                    trEdit1.Visible = false;
                    divViewList.Visible = true;
                    trSubmit1.Visible = true;
                    CalCulation();
                    OutSideDuty();
                    //lblMessage.Text = "Attendance Report Generated Successfully.";
                }

            }
            else
            {

                Generated();
                trEdit1.Visible = false;
                divViewList.Visible = true;
                trSubmit1.Visible = true;
            }

        }

        private Boolean ValidateData()
        {
            if (drpShiftType.SelectedValue == "")
            {
                lblMessage.Text = "Select Shift Schedule.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select Shift Schedule.');");
                return false;
            }
            if (txtInDate.Text.Trim() == "")
            {
                lblMessage.Text = "Enter In Date.";
                objcommon.Display("Validate", "DisplayErrorMessage('Enter In Date.');");
                return false;
            }
            if (txtIntime.Text.Trim() == "")
            {
                lblMessage.Text = "Enter In Time.";
                objcommon.Display("Validate", "DisplayErrorMessage('Enter In Time.');");
                return false;
            }
            if (txtOutDate.Text.Trim() == "")
            {
                lblMessage.Text = "Enter Out Date.";
                objcommon.Display("Validate", "DisplayErrorMessage('Enter Out Date.');");
                return false;
            }
            if (txtOuttime.Text.Trim() == "")
            {
                lblMessage.Text = "Enter Out Time.";
                objcommon.Display("Validate", "DisplayErrorMessage('Enter Out Time.');");
                return false;
            }

            return true;
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            Button lnkRequestNo = (Button)sender;
            //string Time_In = Session["OldTime_In"].ToString();
            //objNps.FromDate = Time_In;
            //string Time_Out = Session["OldTime_Out"].ToString();
            //objNps.ToDate = Time_Out;
            //objNps.EmpCode = Session["sEmpCode"].ToString();
            //objNps.EmpName = Session["sEmpName"].ToString();
            string entryAid = Session["ENTRYAID"].ToString();
            objNps.EntryAid = entryAid;

            objNps.EmpCode = Session["sEmpCode"].ToString();
            ds = objNps.DeleteAttendance((string)Session["sCompID"]);
            if (txtFromDate.Text != "" && txtToDate.Text != "")
            {
                objNps.FromDate = txtFromDate.Text;
                objNps.ToDate = txtToDate.Text;
                objNps.EmpCode = Session["sEmpCode"].ToString();
                objNps.EmpName = Session["sEmpName"].ToString();
                ds = objNps.GenerateAttendanceReport((string)Session["sCompID"]);
                if (ds.Tables[0].Rows[0]["COUNTS"].ToString() == "0")
                {
                    if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) != "")
                    {
                        lblMessage.Text = Convert.ToString(ds.Tables[0].Rows[0]["result"]);
                        gvList.DataSource = null;
                        gvList.DataBind();

                        trEdit1.Visible = false;
                        divViewList.Visible = true;
                        trSubmit1.Visible = true;
                    }
                    else if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) == "")
                    {
                        gvList.DataSource = ds;
                        gvList.DataBind();
                        gvList.Visible = true;
                        trEdit1.Visible = false;
                        divViewList.Visible = true;
                        trSubmit1.Visible = true;
                        CalCulation();

                        //lblMessage.Text = "Attendance Report Generated Successfully.";
                    }
                }
                else
                {
                    lblMessage.Text = "Error Occurs in the application";
                }

            }
            else
            {

                Generated();
                trEdit1.Visible = false;
                divViewList.Visible = true;
                trSubmit1.Visible = true;
            }

        }

        protected void txtInDate_TextChanged(object sender, EventArgs e)
        {
            //CalculateOT();
        }

        protected void txtIntime_TextChanged(object sender, EventArgs e)
        {
            //CalculateOT();
        }

        protected void txtOutDate_TextChanged(object sender, EventArgs e)
        {
            // CalculateOT();
        }

        protected void txtOuttime_TextChanged(object sender, EventArgs e)
        {
            //CalculateOT();
        }


        //private void SENDUDATEMAIL(string frDate)
        //{
        //    emailSend = new ESS.Email();
        //    DataSet dsmkkMail = new DataSet();
        //    DateTime date = DateTime.Now;
        //    dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], (string)Session["sEmpID"]);
        //    string emails = "";
        //    //if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
        //    if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
        //    {
        //        //if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
        //        //{
        //        string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();

        //        string clientbodys = "Dear " + EMPNAME + ",<br><br>Your Attendance Rectification application is submitted Successfully.<br>"
        //        + "Once action taken by your Reporting Manager, will be notified to you through e-mail or same can be checked on the ESS portal."
        //        + "<br><br> Details"
        //        + "<br>Type :- Attendance Rectification "
        //        + "<br> Applied Date :- " + date
        //        + "<br> Date :- " + frDate
        //         + "<br><br>ThankYou."
        //           + "<br>Payroll Team<br> ";
        //        emails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
        //        string subjects = "Do Not Reply: Attendance Rectification";
        //        emailSend.SendEmailNPL(emails, subjects, clientbodys);
        //        emails = "";
        //        string CHECKERMAIL = "";
        //        string checkerbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>" + EMPNAME +
        //            " Attendance Rectification application is received for your approval. Your action is requested for the same through logging-in into ESS portal."
        //            + "<br><br> Details"
        //            + "<br>Type :- Attendance Rectification "
        //            + "<br> Applied Date :- " + date
        //            + "<br> Date :- " + frDate
        //             + "<br>ThankYou."
        //           + "<br>Payroll Team<br> ";
        //        //\nWith Best Regards,\nSequel Outsourcing PVT.LTD<br> < br >
        //        CHECKERMAIL = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
        //        emailSend.SendEmailNPL(CHECKERMAIL, subjects, checkerbodys);
        //        CHECKERMAIL = "";



        //        //}
        //    }

        //}

        //private void SENDUDATEMAIL(string frDate)
        //{
        //    emailSend = new ESS.Email();
        //    DataSet dsmkkMail = new DataSet();
        //    DateTime date = DateTime.Now;
        //    dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], (string)Session["sEmpID"]);

        //    //if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
        //    if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
        //    {
        //        //if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
        //        //{
        //        string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();

        //        string clientbodys = "Dear " + EMPNAME + ",<br><br>Your Attendance Rectification details is sent Successfully.<br>"
        //           + "Once the action taken will be notified to you through mail or same can be checked through ESS portal."
        //        + "<br>Type :- Attendance Rectification "
        //        + "<br> Applied Date :- " + date
        //        + "<br> Date :- " + frDate
        //        + "<br><br>ThankYou.<br><br>";
        //        string emails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
        //        string subjects = "Do Not Reply: Attendance Rectification";
        //        emailSend.SendEmailNPL(emails, subjects, clientbodys);

        //        string CHECKERMAIL = "";
        //        string checkerbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>" + EMPNAME +
        //            " Attendance Rectification is received for approval. Kindly take the action for the same through logging-in into ESS portal."
        //            + "<br>Type :- Attendance Rectification "
        //            + "<br> Applied Date :- " + date
        //            + "<br> Date :- " + frDate
        //            + "<br><br>ThankYou.<br><br>";
        //        //\nWith Best Regards,\nSequel Outsourcing PVT.LTD<br> < br >
        //        CHECKERMAIL = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
        //        emailSend.SendEmailNPL(CHECKERMAIL, subjects, checkerbodys);



        //        //}
        //    }

        //}
        //private void SENDDELETEMAIL()
        //{
        //    emailSend = new ESS.Email();
        //    DataSet dsmkkMail = new DataSet();
        //    dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], (string)Session["sEmpID"]);


        //    if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
        //    {
        //        if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
        //        {
        //            string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();

        //            string clientbodys = "Dear " + EMPNAME + ",<br>\nYour Attendance Rectification Deatails Deleted.<br>"
        //               + "Please wait for Approval. We will notify on mail or you may check on ESS portal.<br>\nThankYou.<br>\n<br><br>";
        //            string emails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
        //            string subjects = "Attendance Rectification";
        //            emailSend.SendEmailNPL(emails, subjects, clientbodys);



        //            string checkerbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>" + EMPNAME + " Attendance Rectification Deatails Deleted" +
        //             "<br>\nThankYou.<br>\nWith Best Regards,<br><br>";

        //            emails = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
        //            emailSend.SendEmailNPL(emails, subjects, checkerbodys);


        //        }
        //    }
        //}
    }
}