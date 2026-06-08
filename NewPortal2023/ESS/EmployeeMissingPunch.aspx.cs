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

namespace NewPortal2023.ESS
{
    public partial class EmployeeMissingPunch : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveApplication objInv = new NewPortal2023.ESS.LeaveApplication();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        NewPortal2023.ESS.NplCoApp objCoApp = new NewPortal2023.ESS.NplCoApp();
        NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();
        DataSet dsInv = new DataSet();
        DataSet ds = new DataSet();
        DataSet dsemp = new DataSet();

        int Count = 0;
        string OldYear, NewYear;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ds = objcommon.ProfIdOfEmp((string)Session["sCompID"], (string)Session["sEmpID"]);

                Session["PROF_ID"] = ds.Tables[0].Rows[0]["PROF_ID"].ToString();

                ds = objcommon.CheckMenuAccess((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["PROF_ID"]);

                // Get the current page's name (e.g., KISNAInvestmentDetailsAdmin.aspx)
                string currentPageName = Path.GetFileName(Request.Url.AbsolutePath);

                // Check if the MENU_URL matches the current page name
                bool accessGranted = false;

                // Iterate through all rows in the dataset
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["MENU_URL"].ToString() == currentPageName)
                    {
                        accessGranted = true; // If match found, access is granted
                        break; // Exit the loop once match is found
                    }
                }
                if (accessGranted)
                {
                    if (!Page.IsPostBack)
                    {
                        divViewList.Visible = true;
                        trSubmit1.Visible = true;
                        trEdit1.Visible = false;
                        fillEmployeeDropdown();
                        recallpagedata();
                    }
                }
                else
                {
                    // If access is denied, redirect to the previous page
                    string previousPage = Request.UrlReferrer?.ToString();

                    if (!string.IsNullOrEmpty(previousPage))
                    {
                        Response.Redirect(previousPage); // Redirect to the previous page
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                        return;
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                //Session["Error"] = ex.Message + "<br>Payslip.aspx";
                Session["Error"] = ex.Message + "<br>Payslip.aspx<br>" + Request.QueryString["key"].Replace(" ", "+");
                Response.Redirect("../ErrorPage.aspx", true);
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
                        Label lnkAppNo = (Label)Row.FindControl("lnkAppNo");
                        TextBox txtAttRemark = (TextBox)Row.FindControl("txtAttRemark");

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
                        if (lnkAppNo.Text == "00150")
                        {

                        }
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
                            Row.Cells[19].Visible = false;
                            //lnkRectification.Visible = false;
                        }
                        if (lblouttime.Text != "" && lblouttime.Text != "NULL")
                        {
                            DateTime OUTtime = Convert.ToDateTime(lblouttime.Text);
                            OUT_TIME = OUTtime.TimeOfDay;
                        }
                        else
                        {
                            DateTime OUTtime = Convert.ToDateTime("00:00:00");
                            OUT_TIME = OUTtime.TimeOfDay;
                        }
                        if (lblStatusTimein.Text != "" && lblStatusTimein.Text != "NULL")
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
                            if (lblStatusShift.Text != "WO")
                            {
                                txtAttRemark.Text = "Missing - Punch Time";
                                Row.Cells[16].BackColor = System.Drawing.Color.OrangeRed;
                            }
                        }

                        DateTime Com = Convert.ToDateTime("00:00:00");
                        TimeSpan CompareWith = Com.TimeOfDay;

                        if (lblStat.Text == "" || lblStat.Text == "0")
                        {
                            Row.Cells[19].Visible = false;
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

                            //if (CO > 0)
                            //{
                            //    lblAttRemark.Text = "CO";
                            //    Row.Cells[17].BackColor = System.Drawing.Color.Green;
                            //}
                            if (CO > 0 && OT > 0)
                            {
                                //lblAttRemark.Text = "OT";
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
                                //lblAttRemark.Text = "OT";
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
                            //double CO = Convert.ToDouble(lblCO.Text);
                            //if (CO > 0)
                            //{
                            //    lblAttRemark.Text = "CO";
                            //    Row.Cells[17].BackColor = System.Drawing.Color.LightSkyBlue;
                            //}
                        }


                        if (lblStatusShift.Text == "GN" || lblStatusShift.Text == "EV" || lblStatusShift.Text == "GS" || lblStatusShift.Text == "MS" || lblStatusShift.Text == "NS")
                        {
                            if (IN_TIME == CompareWith && OUT_TIME == CompareWith)
                            {
                                txtAttRemark.Text = "Absent";
                                Row.Cells[16].BackColor = System.Drawing.Color.Red;
                            }

                            //if (lblouttime.Text == "" || lblStatusTimein.Text == "")
                            //{
                            //    lblAttRemark.Text = "Rectification";
                            //    Row.Cells[17].BackColor = System.Drawing.Color.Yellow;
                            //}
                            if (lblouttime.Text == "" || lblStatusTimein.Text == "")
                            {
                                if (IN_TIME == CompareWith && OUT_TIME == CompareWith)
                                {
                                    txtAttRemark.Text = "Absent";
                                    Row.Cells[16].BackColor = System.Drawing.Color.Red;
                                }
                                else
                                {
                                    txtAttRemark.Text = "Missing - Punch Time";
                                    Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                                }
                            }
                            if (lblTotalWorking.Text != "" && lblTotalWorking.Text != "0.00")
                            {
                                double totalWorkinghours = Convert.ToDouble(lblTotalWorking.Text);
                                if (lblTotalWorking.Text != "" && lblTotalWorking.Text != "0.00")
                                {
                                    //if (totalWorkinghours < 6.00)
                                    //{
                                    //    txtAttRemark.Text = "Half day Absent";
                                    //    Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                                    //}
                                    if (totalWorkinghours <= 4.00)
                                    {
                                        txtAttRemark.Text = "Half day Absent";
                                        Row.Cells[16].BackColor = System.Drawing.Color.Red;
                                    }

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

            catch (Exception ex)
            {

            }


        }

        //private void OutSideDuty()
        //{
        //    objNps.EmpCode = txtEmpCode.Text;


        //    objNps.EmpName = Session["sEmpName"].ToString();

        //    ds = objNps.GetOutSideDutyfromdate((string)Session["sCompID"]);


        //    try
        //    {
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            for (int i = 0; Convert.ToBoolean(ds.Tables[0].Rows.Count); i++)
        //            {
        //                if (ds.Tables[0].Rows.Count != i)
        //                {
        //                    string OutSIde = ds.Tables[0].Rows[i]["FROM_DT"].ToString();
        //                    string OutSIdeReason = ds.Tables[0].Rows[i]["LEAVE_REASON"].ToString();

        //                    dsemp = objNps.GetEmpMid((string)Session["sCompID"], ds.Tables[0].Rows[i]["EMP_AID"].ToString());

        //                    string EpMid = dsemp.Tables[0].Rows[0]["EMP_MID"].ToString();

        //                    // Convert string to DateTime object
        //                    //DateTime dateTime = DateTime.ParseExact(OutSIde, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
        //                    //// Format DateTime to "DD-MM-YYYY" format
        //                    //string dateFormatted = dateTime.ToString("dd-MM-yyyy");
        //                    string OutSideRmark = ds.Tables[0].Rows[i]["ODTYPE"].ToString();
        //                    string STATUS = ds.Tables[0].Rows[i]["STATUS"].ToString();
        //                    string HOLIDAY = ds.Tables[0].Rows[i]["HOLIDAY"].ToString();
        //                    string Reason_id = ds.Tables[0].Rows[i]["REASON_ID"].ToString();

        //                    foreach (GridViewRow Row in gvList.Rows)
        //                    {
        //                        if (Row.RowType == DataControlRowType.DataRow)
        //                        {
        //                            int index = Row.RowIndex;
        //                            if (index == 0)
        //                            {
        //                                Count = 0;
        //                            }
        //                            Label lblStat = (Label)Row.FindControl("lblOrigin");
        //                            //Label lblAttRemark = (Label)Row.FindControl("lblAttRemark");
        //                            Label EpCode = (Label)Row.FindControl("lnkAppNo");
        //                            TextBox txtAttRemark = (TextBox)Row.FindControl("txtAttRemark");

        //                            string txt = txtAttRemark.Text;


        //                            if (OutSIdeReason != "")
        //                            {
        //                                //if(EpMid=="00150")
        //                                //{

        //                                //}

        //                                if (lblStat.Text == OutSIde && EpCode.Text == EpMid)
        //                                {
        //                                    if (STATUS == "2")
        //                                    {
        //                                        txtAttRemark.Text = OutSIdeReason;
        //                                        Row.Cells[16].BackColor = System.Drawing.Color.LightGreen;
        //                                        //if (Reason_id=="1")
        //                                        //{
        //                                        //    lblAttRemark.Text = "Not Well";
        //                                        //    Row.Cells[17].BackColor = System.Drawing.Color.LightGreen;
        //                                        //}
        //                                        //else if (Reason_id == "2")
        //                                        //{
        //                                        //    lblAttRemark.Text = "Family Function";
        //                                        //    Row.Cells[17].BackColor = System.Drawing.Color.LightGreen;
        //                                        //}
        //                                        //else if (Reason_id == "3")
        //                                        //{
        //                                        //    lblAttRemark.Text = "Other";
        //                                        //    Row.Cells[17].BackColor = System.Drawing.Color.LightGreen;
        //                                        //}
        //                                        if (Reason_id == "4")
        //                                        {
        //                                            txtAttRemark.Text = "First half /" + OutSIdeReason;
        //                                            Row.Cells[16].BackColor = System.Drawing.Color.LightGreen;
        //                                        }
        //                                        else if (Reason_id == "5")
        //                                        {
        //                                            txtAttRemark.Text = "Second half /" + OutSIdeReason;
        //                                            Row.Cells[16].BackColor = System.Drawing.Color.LightGreen;
        //                                        }
        //                                    }
        //                                    else if (HOLIDAY == "1" && EpCode.Text == EpMid)
        //                                    {
        //                                        txtAttRemark.Text = OutSideRmark + "Paid Holiday";
        //                                        Row.Cells[16].BackColor = System.Drawing.Color.LightPink;
        //                                    }
        //                                    else if (EpCode.Text == EpMid)
        //                                    {
        //                                        txtAttRemark.Text = OutSIdeReason + " /Approval Pending";
        //                                        Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
        //                                    }

        //                                }

        //                            }

        //                            else
        //                            {

        //                                if (lblStat.Text == OutSIde && EpCode.Text == EpMid)
        //                                {
        //                                    if (STATUS == "2")
        //                                    {
        //                                        txtAttRemark.Text = OutSideRmark + " /Out Door Duty";
        //                                        Row.Cells[16].BackColor = System.Drawing.Color.Green;
        //                                    }

        //                                    else if (HOLIDAY == "1" && EpCode.Text == EpMid)
        //                                    {
        //                                        txtAttRemark.Text = OutSideRmark + "Paid Holiday";
        //                                        Row.Cells[16].BackColor = System.Drawing.Color.LightPink;
        //                                    }
        //                                    else if (EpCode.Text == EpMid)
        //                                    {
        //                                        txtAttRemark.Text = OutSideRmark + " /Approval Pending";
        //                                        Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
        //                                    }

        //                                }
        //                            }

        //                        }



        //                    }
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        private void OutSideDuty()
        {
            //old
            //objNps.EmpCode = txtEmpCode.Text;
            //new
            objNps.EmpCode = drpEmpType.SelectedValue;
            objNps.EmpName = Session["sEmpName"].ToString();
            ds = objNps.GetOutSideDuty((string)Session["sCompID"], objNps.EmpCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; Convert.ToBoolean(ds.Tables[0].Rows.Count); i++)
                {
                    if (ds.Tables[0].Rows.Count != i)
                    {
                        string OutSIde = ds.Tables[0].Rows[i]["FROM_DT"].ToString();
                        string OutSIdeReason = ds.Tables[0].Rows[i]["LEAVE_REASON"].ToString();
                        string ReasonCode = ds.Tables[0].Rows[i]["REASON_ID"].ToString();
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
                               
                                if (objNps.EmpCode == "NP3327" && lblStatusShift.Text == "PH")
                                {
                                    txtAttRemark.Text = OutSideRmark + "Paid Holiday";
                                    Row.Cells[16].BackColor = System.Drawing.Color.LightPink;
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

        private void Co_applicationchecking()
        {

            string finYear = objcommon.GetFinalcialYear();
            string[] years = finYear.Split('.');
            OldYear = years[1];
            NewYear = years[0];

            ds = objCoApp.GetCOListAdmin(Session["sCompID"].ToString(), objNps.EmpCode.ToString(), NewYear);

            try
            {
                for (int i = 0; Convert.ToBoolean(ds.Tables[0].Rows.Count); i++)
                {
                    if (ds.Tables[0].Rows.Count != i)
                    {
                        string rowDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["FR_DATE"]).ToString("dd-MM-yyyy");
                        string empMid = ds.Tables[0].Rows[i]["EMP_MID"].ToString();
                        string status = ds.Tables[0].Rows[i]["STATUSTYPE"].ToString();

                        foreach (GridViewRow Row in gvList.Rows)
                        {
                            if (Row.RowType == DataControlRowType.DataRow)
                            {
                                Label lblOrigin = (Label)Row.FindControl("lblOrigin");
                                //Label lblAttRemark = (Label)Row.FindControl("lblAttRemark");
                                TextBox txtAttRemark = (TextBox)Row.FindControl("txtAttRemark");
                                Label lnkAppNo = (Label)Row.FindControl("lnkAppNo");


                                if (lblOrigin.Text == rowDate && lnkAppNo.Text == empMid)
                                {
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
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception)
            {

                throw;
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

                //if (txtFromDate.Text != "" && txtToDate.Text != "" && (txtEmpCode.Text != "" || txtEmpCode.Text == ""))
                if (txtFromDate.Text != "" && txtToDate.Text != "" && (drpEmpType.SelectedValue != "" || drpEmpType.SelectedValue == ""))

                {
                    Session["todDate"] = txtFromDate.Text;
                    Session["fromdDate"] = txtToDate.Text;
                    Session["empMid"] = drpEmpType.SelectedValue;

                    objNps.FromDate = txtFromDate.Text;
                    objNps.ToDate = txtToDate.Text;
                    //objNps.EmpCode = txtEmpCode.Text;
                    objNps.EmpCode = drpEmpType.SelectedValue;
                   ds = objNps.GenerateAttendanceMissingPunch((string)Session["sCompID"]);

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


            //DateTime combinedDateTimeIn = DateTime.Parse(Time_In);

            string inDate = "";
            string inTime = "";
            if (!string.IsNullOrEmpty(Time_In))
            {
                DateTime combinedDateTimeIn = DateTime.Parse(Time_In);
                string TimeIn = combinedDateTimeIn.ToString();
                string[] timeInDate = TimeIn.Split(' ');
                inDate = (timeInDate[0]);
                inTime = (timeInDate[1]);
                lblInTime.Text = Time_In;

            }
            else
            {
                lblInTime.Text = string.Empty;
            }

            string outDate = "";
            string outTime = "";
            if (!string.IsNullOrEmpty(Time_Out))
            {
                DateTime combinedDateTimeOut = DateTime.Parse(Time_Out);
                string TimeOut = combinedDateTimeOut.ToString();
                string[] timeOutDate = TimeOut.Split(' ');
                outDate = (timeOutDate[0]);
                outTime = (timeOutDate[1]);
                lblOuttime.Text = Time_Out;
            }
            else
            {
                lblOuttime.Text = string.Empty;
            }

            //string TimeIn = combinedDateTimeIn.ToString();
            //string[] timeInDate = TimeIn.Split(' ');
            //string inDate = (timeInDate[0]);
           // string inTime = (timeInDate[1]);

            //string outDate = "";
            //string outTime = "";
            //if (Time_Out != "")
            //{
            //    DateTime combinedDateTimeOut = DateTime.Parse(Time_Out);
            //    string TimeOut = combinedDateTimeOut.ToString();
            //    string[] timeOutDate = TimeOut.Split(' ');
            //    outDate = (timeOutDate[0]);
            //    outTime = (timeOutDate[1]);
            //}


            lblEntryId.Text = lblEntryAid;
            lblInTime.Text = Time_In;
            lblOuttime.Text = Time_Out;
            lblRosterSch.Text = Shift_Schedule;
            lblEmpCode.Text = lnkAppNo;
            lblEmpName.Text = Emp_Name;
            lblDate.Text = Date;
            drpShiftType.SelectedItem.Text = Shift_Schedule;
            // txtInDate.Text = inDate;
            if (!string.IsNullOrEmpty(inDate))
            {

                DateTime parsedinDate = DateTime.Parse(inDate);
                txtInDate.Text = parsedinDate.ToString("dd-MM-yyyy");
            }
            else
            {
                txtInDate.Text = string.Empty;
            }


            if (!string.IsNullOrEmpty(outDate))
            {
                DateTime parsedoutDate = DateTime.Parse(outDate);
                txtOutDate.Text = parsedoutDate.ToString("dd-MM-yyyy");
            }
            else
            {
                txtOutDate.Text = string.Empty;
            }

            //DateTime dateInTime = DateTime.ParseExact(inTime, "h:mm tt", CultureInfo.InvariantCulture);
            //string timeIn24 = dateInTime.ToString("HH:mm:ss"); 
            //txtIntime.Text = timeIn24;

            txtIntime.Text = inTime;
            // txtOutDate.Text = outDate;
            //DateTime dateOutTime = DateTime.ParseExact(outTime, "HH:mm:ss", CultureInfo.InvariantCulture);
            //string timeOut24 = dateOutTime.ToString("HH:mm:ss");
            //txtOuttime.Text = timeOut24;

            txtOuttime.Text = outTime;
            lblTWhr.Text = Total_Working_hrs;
            lblShr.Text = Shift_hrs;
            //lblOt.Text = OT;
            // lblCo.Text = CO;
            //lblUpdateOT.Text = "0";
            //txtUpdateCO.Text = CO;
            trEdit1.Visible = true;
            divViewList.Visible = false;
            trSubmit1.Visible = false;
            //trtilehead.Visible = false;
            //trtitlehr1.Visible = false;

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
            string FileName = "Vithal" + DateTime.Now + ".xls";
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
                    //objNps.OT = txtUpdateOT.Text;
                    objNps.EntryAid = lblEntryId.Text;
                    // objNps.Old_OT = lblOt.Text;
                    // objNps.CO = txtUpdateCO.Text;
                    // objNps.Old_CO = lblCo.Text;
                    objNps.Old_OutTime = lblOuttime.Text;
                    objNps.ToDate = time_Out;
                    objNps.Old_InTime = lblInTime.Text;
                    objNps.FromDate = time_In;
                    objNps.Old_Shift = lblRosterSch.Text;
                    objNps.Shift = drpShiftType.SelectedItem.Text;

                    ds = objNps.UpdateAttendMissingPunch((string)Session["sCompID"], (string)Session["sEmpID"], lblEmpCode.Text, lblEmpName.Text, lblDate.Text, txtRem.Text);

                    //ds = objNps.UpdateAttend((string)Session["sCompID"], (string)Session["sEmpID"], lblEmpCode.Text, lblEmpName.Text, lblDate.Text, txtRem.Text);

                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            string COUNT = Convert.ToString(ds.Tables[0].Rows[0]["status"].ToString().Trim());
                            if (COUNT != "0")
                            {
                                lblMessage.Text = "Successfully Rectified.";
                                objcommon.Display("Validate", "DisplayErrorMessage('Successfully Rectified.');");
                                Session["EmpCode"] = lblEmpCode.Text;
                                // SENDUDATEMAIL(txtInDate.Text);
                                Response.Redirect("EmployeeMissingPunch.aspx");
                                ;

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
            //if (txtFromDate.Text != "" && txtToDate.Text != "" && (txtEmpCode.Text != "" || txtEmpCode.Text == ""))
            if (txtFromDate.Text != "" && txtToDate.Text != "" && (drpEmpType.SelectedValue  != "" || drpEmpType.SelectedValue == ""))

            {
                objNps.FromDate = txtFromDate.Text;
                objNps.ToDate = txtToDate.Text;
                //objNps.EmpCode = txtEmpCode.Text;
                objNps.EmpCode = drpEmpType.SelectedValue;
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
                //objNps.EmpCode = txtEmpCode.Text;
                objNps.EmpCode = drpEmpType.SelectedValue;
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

        protected void drpEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void fillEmployeeDropdown()
        {
            objExp.fillEmployeeMid(drpEmpType);
        }



        protected void recallpagedata()
        {
            try
            {



                DateTime dt = DateTime.Now;
                if ((string)Session["todDate"] == null && (string)Session["fromdDate"] == null)
                {
                    txtFromDate.Text = "";
                    txtToDate.Text = "";
                }
                else
                {
                    txtFromDate.Text = Session["todDate"].ToString();
                    txtToDate.Text = Session["fromdDate"].ToString();
                    drpEmpType.SelectedValue = Session["empMid"].ToString();
                }


                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {

                    objNps.FromDate = txtFromDate.Text;
                    objNps.ToDate = txtToDate.Text;
                    objNps.EmpCode  = Session["empMid"].ToString();
                    //objNps.EmpCode = Session["sEmpCode"].ToString();
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

                    }
                    //Session.Remove("fromdDate");
                    //Session.Remove("todDate");
                    //Session.Remove("empMid");


                }
                else
                {
                    //gvList.DataSource = null;
                    //gvList.DataBind();
                    //gvList.Visible = false;
                    //lblMessage.Text = "Please Select From Date /To Date First Then Click Generate Button";
                }


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}
