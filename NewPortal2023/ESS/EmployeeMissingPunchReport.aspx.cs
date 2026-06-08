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
using ClosedXML.Excel;
using System.Text;
using Microsoft.Reporting.WebForms;

namespace NewPortal2023.ESS
{
    public partial class EmployeeMissingPunchReport : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveApplication objInv = new NewPortal2023.ESS.LeaveApplication();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsInv = new DataSet();
        DataSet ds = new DataSet();
        DataSet dsemp = new DataSet();
        DataSet dsUP = new DataSet();

        string message;
        int Count = 0;
        int nt = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    gvList.Visible = false;
                    trViewList.Visible = true;
                    trSubmit.Visible = true;
                    trEdit.Visible = false;
                    //Generated();     
                    //FillType();
                    //FillStatus("0");
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
            try
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
                        Label EmpCode = (Label)Row.FindControl("lnkAppNo");
                        Label lblStat = (Label)Row.FindControl("lblStat");
                        Label lblStatusShift = (Label)Row.FindControl("lblStatusTRANS");
                        Label lblStatusTimein = (Label)Row.FindControl("lblStatusREIMB");
                        Label lblTotalWorking = (Label)Row.FindControl("lblStatusLUP");
                        Label lblouttime = (Label)Row.FindControl("lblStatusTAX");
                        Label lblot = (Label)Row.FindControl("lblStatusJA");
                        Label lblCO = (Label)Row.FindControl("lblStatus");
                        Label lblAttRemark = (Label)Row.FindControl("lblAttRemark");
                        Label lblPunchRmk = (Label)Row.FindControl("lblPunchRmk");
                        LinkButton lnkRectification = (LinkButton)Row.FindControl("lnkRectification");
                        Label lbldepartment = (Label)Row.FindControl("lbldepartment");
                        //TextBox txtAttRemark = (TextBox)Row.FindControl("txtAttRemark");

                        //if (lblPunchRmk.Text == "E" || lblPunchRmk.Text == "L" && )
                        //{
                        //    lblPunchRmk.Text = "EARLY GOING";
                        //}
                        //else

                        //if (lblPunchRmk.Text == "L")
                        //{
                        //    LateCount++;

                        //    if (LateCount > 2)
                        //    {
                        //        lblPunchRmk.Visible = true;
                        //        lblPunchRmk.Text = "LATE COMING";
                        //    }
                        //}
                        if (lblPunchRmk.Text == "L")
                        {
                            lblPunchRmk.Visible = true;
                            lblPunchRmk.Text = "LATE COMING";
                        }
                        else if (lblPunchRmk.Text == "E")
                        {
                            lblPunchRmk.Visible = true;
                            lblPunchRmk.Text = "EARLY GOING";
                        }
                        else
                        {
                            Row.Cells[18].Visible = false;
                            //lnkRectification.Visible = false;
                        }
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
                            lblAttRemark.Text = "Missing - Punch Time";
                            Row.Cells[16].BackColor = System.Drawing.Color.OrangeRed;
                        }

                        DateTime Com = Convert.ToDateTime("00:00:00");
                        TimeSpan CompareWith = Com.TimeOfDay;

                        if (lblStat.Text == "" || lblStat.Text == "0")
                        {
                            Row.Cells[18].Visible = false;
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
                                lblAttRemark.Text = "CO";
                                Row.Cells[16].BackColor = System.Drawing.Color.Green;
                            }
                            if (CO > 0 && OT > 0)
                            {
                                lblAttRemark.Text = "OT + CO";
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
                                lblAttRemark.Text = "OT";
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
                                lblAttRemark.Text = "CO";
                                Row.Cells[16].BackColor = System.Drawing.Color.LightSkyBlue;
                            }
                        }


                        if (lblStatusShift.Text == "GN" || lblStatusShift.Text == "EV" || lblStatusShift.Text == "GS" || lblStatusShift.Text == "MS" || lblStatusShift.Text == "NS")
                        {
                            if (IN_TIME == CompareWith && OUT_TIME == CompareWith)
                            {
                                lblAttRemark.Text = "Absent";
                                Row.Cells[16].BackColor = System.Drawing.Color.Red;
                            }

                            //if (lblouttime.Text == "" || lblStatusTimein.Text == "")
                            //{
                            //    lblAttRemark.Text = "Rectification";
                            //    Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                            //}
                            if (lblouttime.Text == "" || lblStatusTimein.Text == "")
                            {
                                lblAttRemark.Text = "Missing - Punch Time";
                                Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                            }


                        }
                        else if (lblStatusShift.Text == "WO")
                        {
                            if (IN_TIME != CompareWith || OUT_TIME != CompareWith)
                            {
                                lblAttRemark.Text = "Working on holiday";
                                Row.Cells[16].BackColor = System.Drawing.Color.Green;
                            }
                            if (IN_TIME == CompareWith || OUT_TIME == CompareWith)
                            {
                                lblAttRemark.Text = "Weekly Off";
                                Row.Cells[16].BackColor = System.Drawing.Color.LightGreen;
                            }

                        }

                    }
                }
            }

            catch (Exception ex)
            {

            }


        }

        private void OutSideDuty()
        {

            //objNps.EmpCode = Session["sEmpCode"].ToString();
            objNps.EmpCode = txtEmpCode.Text;

            objNps.EmpName = Session["sEmpName"].ToString();
            //ds = objNps.GetOutSideDuty((string)Session["sCompID"], (string)Session["sEmpCode"]);
            objNps.FromDate = txtFromDate.Text;
            objNps.ToDate = txtToDate.Text;
            ds = objNps.GetOutSideDutyfromdate((string)Session["sCompID"]);

            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; Convert.ToBoolean(ds.Tables[0].Rows.Count); i++)
                {
                    if (ds.Tables[0].Rows.Count != i)
                    {
                        string OutSIde = ds.Tables[0].Rows[i]["FROM_DT"].ToString();
                        string OutSIdeReason = ds.Tables[0].Rows[i]["LEAVE_REASON"].ToString();

                        dsemp = objNps.GetEmpMid((string)Session["sCompID"], ds.Tables[0].Rows[i]["EMP_AID"].ToString());

                        string EpMid = dsemp.Tables[0].Rows[0]["EMP_MID"].ToString();

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
                                Label lblAttRemark = (Label)Row.FindControl("lblAttRemark");
                                Label EpCode = (Label)Row.FindControl("lnkAppNo");

                                string txt = lblAttRemark.Text;


                                if (OutSIdeReason != "")
                                {

                                    if (lblStat.Text == OutSIde && EpCode.Text == EpMid)
                                    {
                                        if (STATUS == "2")
                                        {
                                            lblAttRemark.Text = OutSIdeReason;
                                            Row.Cells[16].BackColor = System.Drawing.Color.LightGreen;
                                        }
                                        else if (HOLIDAY == "1" && EpCode.Text == EpMid)
                                        {
                                            lblAttRemark.Text = OutSideRmark + "Paid Holiday";
                                            Row.Cells[16].BackColor = System.Drawing.Color.LightPink;
                                        }
                                        else if (EpCode.Text == EpMid)
                                        {
                                            lblAttRemark.Text = OutSIdeReason + " /Approval Pending";
                                            Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                                        }

                                    }

                                }

                                else
                                {

                                    if (lblStat.Text == OutSIde && EpCode.Text == EpMid)
                                    {
                                        if (STATUS == "2")
                                        {
                                            lblAttRemark.Text = OutSideRmark + " /Out Door Duty";
                                            Row.Cells[16].BackColor = System.Drawing.Color.Green;
                                        }

                                        else if (HOLIDAY == "1" && EpCode.Text == EpMid)
                                        {
                                            lblAttRemark.Text = OutSideRmark + "Paid Holiday";
                                            Row.Cells[16].BackColor = System.Drawing.Color.LightPink;
                                        }
                                        else if (EpCode.Text == EpMid)
                                        {
                                            lblAttRemark.Text = OutSideRmark + " /Approval Pending";
                                            Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                                        }

                                    }
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
                        lblMessage.Text = "Attendance Report Generated Successfully.";
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

                if (txtFromDate.Text != "" && txtToDate.Text != "" && (txtEmpCode.Text != "" || txtEmpCode.Text == ""))
                {
                    objNps.FromDate = txtFromDate.Text;
                    objNps.ToDate = txtToDate.Text;
                    objNps.EmpCode = txtEmpCode.Text;
                    string Epcode;
                    if (txtEmpCode.Text != "")
                    {
                        Epcode = txtEmpCode.Text;
                    }
                    else
                    {
                        Epcode = "";
                    }

                    DateTime frmDate = DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string FromDate = frmDate.ToString("dd-MMM");

                    DateTime toDate = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                    string ToDate = toDate.ToString("dd-MMM");


                    objNps.EmpName = Session["sEmpName"].ToString();
                    ds = objNps.GenerateAttendanceReport((string)Session["sCompID"]);

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
                        gvList.Visible = false;
                        CalCulation();
                        OutSideDuty();

                        dsUP = objNps.UpdateMissPunch(MakeChkXml(gvList));
                            
                        rptPrint.Visible = true;
                        ReportDataSource datasource1 = new ReportDataSource("dsEmployeeMsPnch", dsUP.Tables[0]);

                        rptPrint.LocalReport.DataSources.Clear();

                        rptPrint.LocalReport.ReportPath = @"Reports/EmployeeMissingPunchReport.rdlc";
                        rptPrint.LocalReport.DisplayName = "Employee_Missing_Punch_Report_" + FromDate + "/" + ToDate + "/" + Epcode + "";
                        rptPrint.LocalReport.DataSources.Add(datasource1);

                        rptPrint.LocalReport.Refresh();


                        divAlertSucc.Visible = true;
                        string script = $@"<script type='text/javascript'>alert('Attendance Report Generated Successfully.');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        lblMessageSucc.Text = "Attendance Report Generated Successfully.";
                        divAlert.Visible = false;
                    }
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                    gvList.Visible = false;
                    lblMessage.Text = "Please Select From Date /To Date /Emp Code First Then Click Generate Button";
                }
            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private string MakeChkXml(GridView gvList)
        {
            StringBuilder sbMissPunch = new StringBuilder();
            string xmlMissPunch = string.Empty;
            sbMissPunch.Append("<ROOT>");

            foreach (GridViewRow gvr in gvList.Rows)
            {
                sbMissPunch.Append("<MSPNCH EMP_CODE='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lnkAppNo")).Text.Trim()) + "'");
                sbMissPunch.Append(" EMP_NAME='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblName")).Text.Trim()) + "'");
                sbMissPunch.Append(" DAYS='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lbldays")).Text.Trim()) + "'");
                sbMissPunch.Append(" DATE='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblOrigin")).Text.Trim()) + "'");
                sbMissPunch.Append(" SHIFT_SCHEDULE='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblStatusTRANS")).Text.Trim()) + "'");
                sbMissPunch.Append(" TIME_IN='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblStatusREIMB")).Text.Trim()) + "'");
                sbMissPunch.Append(" TIME_OUT='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblStatusTAX")).Text.Trim()) + "'");
                sbMissPunch.Append(" TOTAL_WR_HR='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblStatusLUP")).Text.Trim()) + "'");
                sbMissPunch.Append(" SHIFT_HRS='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblStatusTRAVEL")).Text.Trim()) + "'");
                sbMissPunch.Append(" OT='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblStatusJA")).Text.Trim()) + "'");
                sbMissPunch.Append(" CO='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblStatus")).Text.Trim()) + "'");
                sbMissPunch.Append(" REMARK='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblAttRemark")).Text.Trim()) + "'");
                sbMissPunch.Append(" PUNCH_STATUS='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lblPunchSts")).Text.Trim()) + "'");
                sbMissPunch.Append(" Department='" + objcommon.ReplaceSpecialCharacters(((Label)gvr.FindControl("lbldepartment")).Text.Trim()) + "'/>");
            }

            sbMissPunch.Append("</ROOT>");

            xmlMissPunch = sbMissPunch.ToString();

            return xmlMissPunch;
        }

        private string GetTemplateFieldDataField(TemplateField templateField)
        {
            return templateField.SortExpression;
        }

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

            string[] timeInDate = Time_In.Split(' ');
            string inDate = (timeInDate[0]);
            string inTime = (timeInDate[1]);

            string outDate = "";
            string outTime = "";
            if (Time_Out != "")
            {
                string[] timeOutDate = Time_Out.Split(' ');
                outDate = (timeOutDate[0]);
                outTime = (timeOutDate[1]);
            }


            lblEntryId.Text = lblEntryAid;
            lblInTime.Text = Time_In;
            lblOuttime.Text = Time_Out;
            lblRosterSch.Text = Shift_Schedule;
            lblEmpCode.Text = lnkAppNo;
            lblEmpName.Text = Emp_Name;
            lblDate.Text = Date;
            drpShiftType.SelectedItem.Text = Shift_Schedule;
            txtInDate.Text = inDate;
            txtIntime.Text = inTime;
            txtOutDate.Text = outDate;
            txtOuttime.Text = outTime;
            lblTWhr.Text = Total_Working_hrs;
            lblShr.Text = Shift_hrs;
            lblOt.Text = OT;
            lblCo.Text = CO;
            //lblUpdateOT.Text = "0";
            txtUpdateCO.Text = CO;
            trEdit.Visible = true;
            trViewList.Visible = false;
            trSubmit.Visible = false;
            //trtilehead.Visible = false;
            //trtitlehr.Visible = false;

            //    //CalculateOT();
        }

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
                    ExportToExcel(gvList);
                }
                else
                {
                    lblMessage.Text = "Generate report first.";
                }
            }
        }

        private void ExportGridView(GridView gvList)
        {

            //Response.Clear();
            //Response.Buffer = true;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Charset = "";
            //string FileName = "Attendance" + DateTime.Now + ".xls";
            //StringWriter strwritter = new StringWriter();
            //HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            //gvFiles.GridLines = GridLines.Both;
            //gvFiles.HeaderStyle.Font.Bold = true;
            //gvFiles.RenderControl(htmltextwrtter);
            //Response.Write(strwritter.ToString());
            //Response.End();
            string guid;
            string path;
            if ((string)Session["sCompID"].ToString() == "CO000141")
            {
                this.gvList.AllowPaging = false;
                this.gvList.AllowSorting = false;
                this.gvList.EditIndex = -1;

                Response.Clear();
                Response.ContentType = "application/vnd.xls";
                Response.AddHeader("content-disposition",
                        "attachment;filename=Attendance.xls");
                Response.Charset = "";
                StringWriter swriter = new StringWriter();
                HtmlTextWriter hwriter = new HtmlTextWriter(swriter);
                gvList.RenderControl(hwriter);
                Response.Write(swriter.ToString());
                Response.End();
            }
        }

        protected void ExportToExcel(GridView gvList)
        {
            //if ((string)Session["sCompID"].ToString() == "CO000141")
            //{
            //    this.gvList.AllowPaging = false;
            //    this.gvList.AllowSorting = false;
            //    this.gvList.EditIndex = -1;

            //    DataTable dataTable = new DataTable("Attendance");


            //    foreach (DataControlField column in gvList.Columns)
            //    {
            //        if (column.Visible)
            //        {
            //            string columnDescription = column.ToString();
            //            dataTable.Columns.Add(columnDescription);
            //        }
            //    }


            //    foreach (GridViewRow row in gvList.Rows)
            //    {
            //        DataRow dataRow = dataTable.NewRow();
            //        nt = 0;
            //        Count = 0;
            //        foreach (DataControlField column in gvList.Columns)
            //        {    
            //            if (column.Visible)
            //            {
            //                //Count  += Count;
            //                int j = Count;
            //                int i = nt;
            //                var cell = row.Cells[i];
            //                string cellContent = GetCellTextContent(cell);
            //                dataRow[j] = cellContent;
            //                Count++;
            //            }
            //            nt++;      
            //        }

            //        dataTable.Rows.Add(dataRow);
            //    }


            //    using (XLWorkbook workbook = new XLWorkbook())
            //    {
            //        var worksheet = workbook.Worksheets.Add(dataTable, "Attendance");
            //        worksheet.Columns().AdjustToContents();

            //        Response.Clear();
            //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //        Response.AddHeader("content-disposition", "attachment;filename=Attendance.xlsx");
            //        Response.Charset = "";

            //        using (MemoryStream memoryStream = new MemoryStream())
            //        {
            //            workbook.SaveAs(memoryStream);
            //            memoryStream.WriteTo(Response.OutputStream);
            //            memoryStream.Close();
            //        }

            //        Response.End();
            //    }

            //}
        }

        private string GetCellTextContent(TableCell cell)
        {
            string content = "";

            foreach (Control control in cell.Controls)
            {
                if (control is LiteralControl)
                {
                    content += ((LiteralControl)control).Text;
                }
                else if (control is Label)
                {
                    content += ((Label)control).Text;
                }
            }

            return content;
        }

        protected override void Render(HtmlTextWriter writer)
        {
            // Allow the base class to handle the rendering
            base.Render(writer);
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

                    ds = objNps.UpdateAttend((string)Session["sCompID"], (string)Session["sEmpCode"], txtEmpCode.Text, lblEmpCode.Text, lblEmpName.Text, lblDate.Text, txtRem.Text);

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
                                SENDUDATEMAIL(txtInDate.Text);
                                Response.Redirect("AttendanceEdit.aspx");

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
                objNps.EmpCode = txtEmpCode.Text;
                objNps.EmpName = Session["sEmpName"].ToString();
                ds = objNps.GenerateAttendanceReport((string)Session["sCompID"]);
                if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) != "")
                {
                    lblMessage.Text = Convert.ToString(ds.Tables[0].Rows[0]["result"]);
                    gvList.DataSource = null;
                    gvList.DataBind();

                    trEdit.Visible = false;
                    trViewList.Visible = true;
                    trSubmit.Visible = true;
                }
                else if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) == "")
                {
                    gvList.DataSource = ds;
                    gvList.DataBind();
                    gvList.Visible = true;
                    trEdit.Visible = false;
                    trViewList.Visible = true;
                    trSubmit.Visible = true;
                    CalCulation();
                    OutSideDuty();
                    //lblMessage.Text = "Attendance Report Generated Successfully.";
                }

            }
            else
            {

                Generated();
                trEdit.Visible = false;
                trViewList.Visible = true;
                trSubmit.Visible = true;
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

            objNps.EmpCode = txtEmpCode.Text;
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

                        trEdit.Visible = false;
                        trViewList.Visible = true;
                        trSubmit.Visible = true;
                    }
                    else if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) == "")
                    {
                        gvList.DataSource = ds;
                        gvList.DataBind();
                        gvList.Visible = true;
                        trEdit.Visible = false;
                        trViewList.Visible = true;
                        trSubmit.Visible = true;
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
                trEdit.Visible = false;
                trViewList.Visible = true;
                trSubmit.Visible = true;
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

        private void SENDUDATEMAIL(string frDate)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DateTime date = DateTime.Now;
            dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], txtEmpCode.Text);

            //if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            {
                //if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
                //{
                string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();

                string clientbodys = "Dear " + EMPNAME + ",<br><br>Your Attendance Rectification details is sent Successfully.<br>"
                   + "Once a action taken will be notified to you through mail or same can be chack on ESS portal."
                    + "<br>Type :- Attendance Rectification "
                    + "<br> Applied Date :- " + date
                    + "<br> Date :- " + frDate
                    + "<br><br>ThankYou.<br><br>";
                string emails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
                string subjects = "Do Not Reply: Attendance Rectification";
                emailSend.SendEmailNPL(emails, subjects, clientbodys);


                string checkerbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>" + EMPNAME +
                    " Attendance Rectification Deatails updated.Please Check than Approve Attendance Claim."
                    + "<br>Type :- Attendance Rectification "
                    + "<br> Applied Date :- " + date
                    + "<br> Date :- " + frDate
                    + "<br><br>ThankYou.<br><br>";
                //\nWith Best Regards,\nSequel Outsourcing PVT.LTD<br> < br >
                emails = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
                emailSend.SendEmailNPL(emails, subjects, checkerbodys);



                //}
            }

        }

        private void SENDDELETEMAIL()
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], txtEmpCode.Text);


            if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            {
                if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
                {
                    string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();

                    string clientbodys = "Dear " + EMPNAME + ",<br>\nYour Attendance Rectification Deatails Deleted.<br>"
                       + "Please wait for Approval. We will notify on mail or you may check on ESS portal.<br>\nThankYou.<br>\n<br><br>";
                    string emails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
                    string subjects = "Attendance Rectification";
                    emailSend.SendEmailNPL(emails, subjects, clientbodys);



                    string checkerbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>" + EMPNAME + " Attendance Rectification Deatails Deleted" +
                     "<br>\nThankYou.<br>\nWith Best Regards,<br><br>";

                    emails = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
                    emailSend.SendEmailNPL(emails, subjects, checkerbodys);


                }
            }
        }
    }
}