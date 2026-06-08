using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Xml;
using System.IO;

namespace NewPortal2023.ESS
{
    public partial class NPLDashboard : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        // DataSet ds;
        DataSet dsPen;
        DataSet dsAppr;
        DataSet dsAprvl;
        DataSet dsleave;
        DataSet dsattend;
        DataSet dsdom;
        DataSet dsexpense;
        DataSet dsInv = new DataSet();
        NewPortal2023.ESS.LeaveCards objInv = new NewPortal2023.ESS.LeaveCards();
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        DataSet dsLogIn = new DataSet();
        DataSet dsLogOut = new DataSet();
        DataSet dsNotifyLogIn = new DataSet();
        DataSet dsNotifyLogOut = new DataSet();
        DataSet ds = new DataSet();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sCompID"] != null)
            {
                try
                {

                    if (!Page.IsPostBack)
                    {
                        divPending.Visible = true;
                        divApproved.Visible = false;
                        divApproval.Visible = false;
                        GetEmpType();
                        goToNPLPopupAttendnace();
                        updateNs();
                        //getKRAActivationDateStatus();

                    }
                }
                catch(Exception EX)
                {

                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void updateNs()
        {
            ds = objNps.updateattendanceNs();
        }

        private void getKRAActivationDateStatus()
        {
            try
            {
                DataSet dsDateAct = new DataSet();
                DataSet dsKRAFlag = new DataSet();
                DataSet dsKRARejectedFlag = new DataSet();
                objPmsHr = new NewPortal2023.ESS.PmsHr();
                dsDateAct = objPmsHr.GetActivationDate();
                dsKRARejectedFlag = objPmsHr.GetKRARejectCount();
                //if (dsKRARejectedFlag.Tables[0].Rows.Count == 0)
                //{
                    if (dsDateAct.Tables[0].Rows[0]["EMP_DEACTIVATE_DATE"].ToString() == "1")
                    {
                    //objPmsHr.Flag = "8";
                    objPmsHr.Flag = "1";
                    objPmsHr.Type = "EMP";
                        dsKRAFlag = objPmsHr.UpdatedKRAFlag();
                        //getList();
                    }
                    if (dsDateAct.Tables[1].Rows[0]["APPR_DEACTIVATE_DATE"].ToString() == "1")
                    {

                        //txtToDate.Text = dsDateAct.Tables[3].Rows[0]["Deactivation_Date"].ToString("dd/MM/yyyy");
                        objPmsHr.Flag = "2";
                        objPmsHr.Type = "APPR";
                        dsKRAFlag = objPmsHr.UpdatedKRAFlag();
                        //getList();
                    }
               // }
                
            }


            catch (Exception ex)
            {
                objcommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                objcommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = ex.Message;
            }
        }

        private void goToNPLPopupAttendnace()
        {
            if ((string)Session["sCompID"] == "CO000141")
            {

                //Reminder_mail_leave();  //SANKET 


                DataSet ds1 = new DataSet();
                ds1 = objNps.insertcootapprover((string)Session["sCompID"]);
                //ds = objNps.UpdateLeaveBal((string)Session["sCompID"]);
                //getNextMonthRosterForGeneralShift();

                //ds = objNps.GetAttendanceZingHr((string)Session["sCompID"]);
                //ds = objNps.GetAttendanceHrs((string)Session["sCompID"]);
                //ds = objNps.UpdateFirstInLastOut((string)Session["sCompID"]);
                //ds = objNps.InsertAttendanceNPL((string)Session["sCompID"]);

                //ds = objNps.GenerateAttendanceAuto((string)Session["sCompID"]);
                ////ds = objNps.GenerateAllEmpAttendanceReport((string)Session["sCompID"]);
                if (Session["sEmpCode"].ToString() == "NP325" || Session["sEmpCode"].ToString() == "NP3348" || Session["sEmpCode"].ToString() == "NP3349" ||
                      Session["sEmpCode"].ToString() == "NP3350" || Session["sEmpCode"].ToString() == "NP3385" || Session["sEmpCode"].ToString() == "NP3391" ||
                      Session["sEmpCode"].ToString() == "NP3393" || Session["sEmpCode"].ToString() == "NP3401" || Session["sEmpCode"].ToString() == "ABC123" || 
                      Session["sEmpCode"].ToString() == "NP3327" ||Session["sEmpCode"].ToString() == "CONSP22" || Session["sEmpCode"].ToString() == "CONSP25" || 
                      Session["sEmpCode"].ToString() == "NP3409" || Session["sEmpCode"].ToString() == "NP3419" || Session["sEmpCode"].ToString() == "NP3425"|| 
                      Session["sEmpCode"].ToString() == "NP3416" || Session["sEmpCode"].ToString() == "NP3424" || Session["sEmpCode"].ToString() == "CONSP28" || 
                      Session["sEmpCode"].ToString() == "NP3428" || Session["sEmpCode"].ToString() == "NP3429" || Session["sEmpCode"].ToString() == "NP3439") 
                {
                    dsLogIn = objNps.GetZinghrAttendanceLogInSequel((string)Session["sCompID"], (string)Session["sEmpCode"]);
                    if (dsLogIn.Tables[0].Rows.Count > 0)
                    {
                        lblInTime.Text = dsLogIn.Tables[0].Rows[0]["LOGIN"].ToString();
                    }
                    dsLogOut = objNps.GetZinghrAttendanceLogOutSequel((string)Session["sCompID"], (string)Session["sEmpCode"]);
                    if (dsLogOut.Tables[0].Rows.Count > 0)
                    {
                        lblOutTime.Text = dsLogOut.Tables[0].Rows[0]["LOGOUT"].ToString();
                    }
                }
                else
                {
                    dsLogIn = objNps.GetZinghrAttendanceLogIn((string)Session["sCompID"], (string)Session["sEmpCode"]);
                    if (dsLogIn.Tables[0].Rows.Count > 0)
                    {
                        lblInTime.Text = dsLogIn.Tables[0].Rows[0]["LOGIN"].ToString();
                    }
                    dsLogOut = objNps.GetZinghrAttendanceLogOut((string)Session["sCompID"], (string)Session["sEmpCode"]);
                    if (dsLogOut.Tables[0].Rows.Count > 0)
                    {
                        lblOutTime.Text = dsLogOut.Tables[0].Rows[0]["LOGOUT"].ToString();
                    }
                }

                dsNotifyLogIn = objNps.GetZinghrAttendanceNotifyLogIn((string)Session["sCompID"], (string)Session["sEmpCode"]);
                dsNotifyLogOut = objNps.GetZinghrAttendanceNotifyLogOut((string)Session["sCompID"], (string)Session["sEmpCode"]);
                if (dsNotifyLogIn.Tables[0].Rows.Count > 0)
                {
                    if (dsNotifyLogIn.Tables[0].Rows[0]["RESULT"].ToString() == "PLEASE MARK YOUR PUNCH IN TIME FOR THE ATTENDANCE")
                    {
                        lblNotifymessage.Text = "<Marquee><h4><font color=red>PLEASE MARK YOUR PUNCH IN TIME FOR THE ATTENDANCE</font></h4></marquee>";
                        SendMailForNotifyAttendanceLogIn((string)Session["sCompID"], (string)Session["sEmpCode"]);
                    }
                    else if (dsNotifyLogIn.Tables[0].Rows[0]["RESULT"].ToString() == "LOGIN")
                    {
                        if (dsLogIn.Tables[0].Rows.Count > 0)
                        {
                            lblInTime.Text = dsLogIn.Tables[0].Rows[0]["LOGIN"].ToString();
                        }
                        else
                        {
                            lblInTime.Text = "LOGIN MISSED";
                        }
                    }
                }
                if (dsNotifyLogOut.Tables[0].Rows.Count > 0)
                {
                    if (dsNotifyLogOut.Tables[0].Rows[0]["RESULT"].ToString() == "PLEASE MARK YOUR PUNCH OUT TIME FOR THE ATTENDANCE")
                    {
                        lblNotifymessage.Text = "<Marquee><h4><font color=red>PLEASE MARK YOUR PUNCH OUT TIME FOR THE ATTENDANCE</font></h4></marquee>";
                        // SendMailForNotifyAttendanceLogOut((string)Session["sCompID"], (string)Session["sEmpCode"]);
                    }
                    else if (dsNotifyLogOut.Tables[0].Rows[0]["RESULT"].ToString() == "LOGOUT")
                    {
                        if (dsLogOut.Tables[0].Rows.Count > 0)
                        {
                            lblOutTime.Text = dsLogOut.Tables[0].Rows[0]["LOGOUT"].ToString();
                        }
                        else
                        {
                            lblOutTime.Text = "LOGOUT MISSED";
                        }
                    }

                }
                //ds = objNps.UpdateLeaveBal((string)Session["sCompID"]);

            }



            if ((string)Session["sCompID"] == "CO000141")
            {
                // imgnpllogo.Visible = true;
                //divHincolUpload.Visible = true;
                string title = "Attendance";
                //string body = "Welcome to ASPSnippets.com";
                //DataSet dszinHr = new DataSet();
                //dszinHr = objNps.GetDatetime((string)Session["sCompID"], (string)Session["sEmpCode"]);

                TimeZoneInfo timzoe = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

                DateTime indiaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timzoe);


                lblAttendDate.Text = indiaTime.ToString("dd-MM-yyyy HH:mm:ss");
                objInv.ATTENDIN = lblAttendDate.Text;
                dsInv = new DataSet();
                dsInv = objInv.GetAttendanceDetails((string)Session["sCompID"], (string)Session["sEmpCode"]);
                if (dsInv.Tables[1].Rows[0]["TIMEIN_COUNT"].ToString() == "0")
                {
                    btnAttendClick.Visible = true;
                    btnAttendClickOut.Visible = false;
                }
                else
                {
                    if (dsInv.Tables[2].Rows[0]["TIMEOUT_COUNT"].ToString() == "0")
                    {
                        btnAttendClick.Visible = false;
                        btnAttendClickOut.Visible = true;
                    }
                    else
                    {
                        btnAttendClick.Visible = false;
                        btnAttendClickOut.Visible = false;
                    }

                }
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    gvList.DataSource = dsInv;
                    gvList.DataBind();
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                }
                if (Session["sEmpCode"].ToString() == "NP150" || Session["sEmpCode"].ToString() == "NP325" || Session["sEmpCode"].ToString() == "NP3348" || Session["sEmpCode"].ToString() == "NP3349" ||
                    Session["sEmpCode"].ToString() == "NP3350" || Session["sEmpCode"].ToString() == "NP3378" || Session["sEmpCode"].ToString() == "NP3385" || Session["sEmpCode"].ToString() == "NP3391" ||
                    Session["sEmpCode"].ToString() == "NP3393" || Session["sEmpCode"].ToString() == "NP3401" || Session["sEmpCode"].ToString() == "ABC123" || Session["sEmpCode"].ToString() == "NP3327" ||
                    Session["sEmpCode"].ToString() == "CONSP22" || Session["sEmpCode"].ToString() == "CONSP25" || Session["sEmpCode"].ToString() == "NP3409" || Session["sEmpCode"].ToString() == "NP3419" || 
                    Session["sEmpCode"].ToString() == "NP3425" || Session["sEmpCode"].ToString() == "CONSP28"|| Session["sEmpCode"].ToString() == "NP3416" || Session["sEmpCode"].ToString() == "NP3424" || 
                    Session["sEmpCode"].ToString() == "NP3428" || Session["sEmpCode"].ToString() == "NP3429" || Session["sEmpCode"].ToString() == "NP3439")
                {

                }
                else
                {
                    btnAttendClick.Visible = false;
                    btnAttendClickOut.Visible = false;
                }
                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "Popup", "", false);
            }

        }

        private void SendMailForNotifyAttendanceLogOut(string compValue, string empValue)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();

            dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], (string)Session["sEmpID"]);
            if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() == "")
            {
                //if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
                //{
                string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();

                string clientbodys = "Dear " + EMPNAME + ",\n\n"
                   + "Please Mark Your Punch In Time For The Attendance." + "\n\nThankYou.\n\n";
                string emails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
                string subjects = "Attendance LogIn Notification";
                emailSend.SendEmailNPL(emails, subjects, clientbodys);
            }
        }

        private void SendMailForNotifyAttendanceLogIn(string compValue, string empValue)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();

            dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], (string)Session["sEmpID"]);
            if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() == "")
            {
                //if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
                //{
                string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();

                string clientbodys = "Dear " + EMPNAME + ",\n\n"
                   + "Please Mark Your Punch Out Time For The Attendance." + "\n\nThankYou.\n\n";
                string emails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
                string subjects = "Attendance LogOut Notification";
                emailSend.SendEmailNPL(emails, subjects, clientbodys);
            }
        }

        protected void btnAttendClick_Click(object sender, EventArgs e)
        {
            string title = "Attendance";
            //string body = "Welcome to ASPSnippets.com";
            TimeZoneInfo timzoe = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            DateTime indiaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timzoe);

            lblAttendDate.Text = indiaTime.ToString("dd-MM-yyyy HH:mm:ss");
            dsInv = new DataSet();
            DataSet dsInsert = new DataSet();
            GetIPLocation();

            if (ViewState["Loc"] != null)
            {
                objInv.GEOLOC = ViewState["Loc"].ToString();
            }
            else
            {
                objInv.GEOLOC = ViewState["Loc"].ToString();
            }

            objInv.ATTENDIN = lblAttendDate.Text;
            //if (ViewState["GEOLOCATION"] != null)
            //{
            //    objInv.GEOLOC = ViewState["GEOLOCATION"].ToString();
            //}
            //else
            //{
            //    objInv.GEOLOC = "";
            //}

            objInv.ATTENDIN = lblAttendDate.Text;

            dsInsert = objInv.InsertAttendDetailsNpl((string)Session["sCompID"], (string)Session["sEmpCode"]);
            dsLogIn = objNps.GetZinghrAttendanceLogInSequel((string)Session["sCompID"], (string)Session["sEmpCode"]);
            dsLogOut = objNps.GetZinghrAttendanceLogOutSequel((string)Session["sCompID"], (string)Session["sEmpCode"]);
            if (dsLogIn.Tables[0].Rows.Count > 0)
            {
                lblInTime.Text = dsLogIn.Tables[0].Rows[0]["LOGIN"].ToString();
            }
            if (dsLogOut.Tables[0].Rows.Count > 0)
            {
                lblOutTime.Text = dsLogOut.Tables[0].Rows[0]["LOGOUT"].ToString();
            }
            dsInv = objInv.GetAttendanceDetails((string)Session["sCompID"], (string)Session["sEmpCode"]);
            if (dsInv.Tables[1].Rows[0]["TIMEIN_COUNT"].ToString() == "0")
            {
                btnAttendClick.Visible = true;
                btnAttendClickOut.Visible = false;
            }
            else
            {
                if (dsInv.Tables[2].Rows[0]["TIMEOUT_COUNT"].ToString() == "0")
                {
                    btnAttendClick.Visible = false;
                    btnAttendClickOut.Visible = true;
                }
                else
                {
                    btnAttendClick.Visible = false;
                    btnAttendClickOut.Visible = false;
                }

            }
            if (dsInv.Tables[0].Rows.Count > 0)
            {
                gvList.DataSource = dsInv;
                gvList.DataBind();
            }
            else
            {
                gvList.DataSource = null;
                gvList.DataBind();
            }
            //btnAttendClick.Visible = false;
            //btnAttendClickOut.Visible = false;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
        }

        protected void btnAttendClickOut_Click(object sender, EventArgs e)
        {
            string title = "Attendance";
            //string body = "Welcome to ASPSnippets.com";
            TimeZoneInfo timzoe = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

            DateTime indiaTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timzoe);

            lblAttendDate.Text = indiaTime.ToString("dd-MM-yyyy HH:mm:ss");
            dsInv = new DataSet();
            DataSet dsInsert = new DataSet();
            objInv.ATTENDOUT = lblAttendDate.Text;
            //objInv.ENTRY_AID = Session["ENTRY_AID"].ToString();
            dsInsert = objInv.UpdateAttendDetailsNpl((string)Session["sCompID"], (string)Session["sEmpCode"]);
            dsLogIn = objNps.GetZinghrAttendanceLogInSequel((string)Session["sCompID"], (string)Session["sEmpCode"]);
            dsLogOut = objNps.GetZinghrAttendanceLogOutSequel((string)Session["sCompID"], (string)Session["sEmpCode"]);
            if (dsLogIn.Tables[0].Rows.Count > 0)
            {
                lblInTime.Text = dsLogIn.Tables[0].Rows[0]["LOGIN"].ToString();
            }
            if (dsLogOut.Tables[0].Rows.Count > 0)
            {
                lblOutTime.Text = dsLogOut.Tables[0].Rows[0]["LOGOUT"].ToString();
            }
            objInv.ATTENDIN = lblAttendDate.Text;
            dsInv = objInv.GetAttendanceDetails((string)Session["sCompID"], (string)Session["sEmpCode"]);
            if (dsInv.Tables[1].Rows[0]["TIMEIN_COUNT"].ToString() == "0")
            {
                btnAttendClick.Visible = true;
                btnAttendClickOut.Visible = false;
            }
            else
            {
                if (dsInv.Tables[2].Rows[0]["TIMEOUT_COUNT"].ToString() == "0")
                {
                    btnAttendClick.Visible = false;
                    btnAttendClickOut.Visible = true;
                }
                else
                {
                    btnAttendClick.Visible = false;
                    btnAttendClickOut.Visible = false;
                }

            }
            if (dsInv.Tables[0].Rows.Count > 0)
            {
                gvList.DataSource = dsInv;
                gvList.DataBind();
            }
            else
            {
                gvList.DataSource = null;
                gvList.DataBind();
            }
            //btnAttendClick.Visible = false;
            //btnAttendClickOut.Visible = false;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
        }

        private void Reminder_mail_leave()
        {
            ds = objNps.Reminder((string)Session["sCompID"]);
            //string CID = ds.Tables[0].Rows[0]["CID"].ToString();
            if (ds.Tables.Count != 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string CID = ds.Tables[0].Rows[i]["CID"].ToString();
                        emailSend = new NewPortal2023.ESS.Email();
                        DataSet dsmkkMail = new DataSet();
                        DateTime date = DateTime.Now;
                        dsmkkMail = emailSend.GetrEMINDERRecDe((string)Session["sCompID"], CID);
                        string leaveType = dsmkkMail.Tables[0].Rows[0]["LEAVE_CODE"].ToString();
                        string EMPID = dsmkkMail.Tables[0].Rows[0]["EMP_CODE"].ToString();
                        string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();
                        //string HODEMAIL = dsmkkMail.Tables[0].Rows[0]["HOD_MAIL"].ToString();

                        DataTable table = dsmkkMail.Tables[0];
                        string HtmlTAble = getHtmlFinDisp(table);

                        string subjects = "Do Not Reply: Leave Application";

                        string Newemails = "";

                        string checkerbodys = "Dear " + dsmkkMail.Tables[0].Rows[0]["HOD"].ToString() + ",<br><br>" +
                            " Please find below with the pending Applications. which needs your immediate Attention. Requesting you to take the action as soon as possible.\n"
                          + " <br> " + " <br> " + HtmlTAble
                                 + "<br><br>ThankYou.<br><br>";
                        //\nWith Best Regards,\nSequel Outsourcing PVT.LTD<br> < br >
                        Newemails = dsmkkMail.Tables[0].Rows[0]["HOD_MAIL"].ToString();
                        //Newemails = "techsupport@sequelgroup.co.in";
                        emailSend.SendEmailNPL(Newemails, subjects, checkerbodys);

                        //}


                    }

                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                    {
                        string CID = ds.Tables[1].Rows[i]["CID"].ToString();
                        emailSend = new NewPortal2023.ESS.Email();
                        DataSet dsmkkMail = new DataSet();
                        DateTime date = DateTime.Now;
                        dsmkkMail = emailSend.GetrEMINDERRecDehr((string)Session["sCompID"], CID);
                        string leaveType = dsmkkMail.Tables[0].Rows[0]["LEAVE_CODE"].ToString();
                        string EMPID = dsmkkMail.Tables[0].Rows[0]["EMP_CODE"].ToString();
                        string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();
                        //string HODEMAIL = dsmkkMail.Tables[0].Rows[0]["HOD_MAIL"].ToString();

                        DataTable table = dsmkkMail.Tables[0];
                        string HtmlTAble = getHtmlFinDispHr(table);

                        string subjects = "Do Not Reply: Leave Application";

                        string Newemails = "";

                        string checkerbodys = "Dear Sir " + ",<br><br>" +
                            " Please find below with the various Applications, Which are still pending for action    \n"
                          + "from HOD." + " <br> " + "Requesting you to kindly take further action on the same. \n" + " <br> " + " <br> " + HtmlTAble
                                 + "<br><br>ThankYou.<br><br>";
                        //\nWith Best Regards,\nSequel Outsourcing PVT.LTD<br> < br >
                        //Newemails = dsmkkMail.Tables[0].Rows[0]["HOD_MAIL"].ToString();
                        Newemails = "pankaj.marke@naperol.com";
                        //Newemails = "techsupport@sequelgroup.co.in";
                        emailSend.SendEmailNPL(Newemails, subjects, checkerbodys);

                        //}


                    }

                }
            }


        }
        public static string getHtmlFinDisp(DataTable table)
        {
            try
            {

                string messageBody = "";

                if (table.Rows.Count == 0) return messageBody;

                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align: center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\"border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";

                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "EMP_CODE" + htmlTdEnd;
                messageBody += htmlTdStart + "EMP_NAME" + htmlTdEnd;
                //messageBody += htmlTdStart + "Sol_ID" + htmlTdEnd;
                messageBody += htmlTdStart + "LEAVE_CODE" + htmlTdEnd;
                messageBody += htmlTdStart + "PENDING_DAYS" + htmlTdEnd;
                //messageBody += htmlTdStart + "Balance_LCY" + htmlTdEnd;
                //messageBody += htmlTdStart + "Balance_OCY" + htmlTdEnd;
                //messageBody += htmlTdStart + "GL_CATEGORY" + htmlTdEnd;
                //messageBody += htmlTdStart + "GL_Type" + htmlTdEnd;
                //messageBody += htmlTdStart + "Date" + htmlTdEnd;
                //messageBody += htmlTdStart + "Primary_Owner_Name" + htmlTdEnd;
                //messageBody += htmlTdStart + "Primary_Owner_EmailID" + htmlTdEnd;
                //messageBody += htmlTdStart + "Owner_Department" + htmlTdEnd;
                //messageBody += htmlTdStart + "Department_or_SM_Email_ID" + htmlTdEnd;
                //messageBody += htmlTdStart + "Certification action" + htmlTdEnd;
                //messageBody += htmlTdStart + "Comment" + htmlTdEnd;

                messageBody += htmlHeaderRowEnd;

                for (int i = 0; i <= table.Rows.Count - 1; i++)
                {
                    if (table.Rows[i][0].ToString() != "")
                    {
                        messageBody = messageBody + htmlTrStart;
                        messageBody = messageBody + htmlTdStart + table.Rows[i][0] + htmlTdEnd;
                        messageBody = messageBody + htmlTdStart + table.Rows[i][1] + htmlTdEnd;
                        messageBody = messageBody + htmlTdStart + table.Rows[i][2] + htmlTdEnd;
                        messageBody = messageBody + htmlTdStart + table.Rows[i][3] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][5] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][10] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][11] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][12] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][13] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][14] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][15] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][16] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][17] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][18] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][21] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][22] + htmlTdEnd;
                        messageBody = messageBody + htmlTrEnd;
                    }
                }
                messageBody = messageBody + htmlTableEnd;
                return messageBody;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public static string getHtmlFinDispHr(DataTable table)
        {
            try
            {

                string messageBody = "";

                if (table.Rows.Count == 0) return messageBody;

                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align: center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style=\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style=\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                string htmlTdStart = "<td style=\"border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";

                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "EMP_CODE" + htmlTdEnd;
                messageBody += htmlTdStart + "EMP_NAME" + htmlTdEnd;
                messageBody += htmlTdStart + "HOD" + htmlTdEnd;
                messageBody += htmlTdStart + "LEAVE_CODE" + htmlTdEnd;
                messageBody += htmlTdStart + "PENDING_DAYS" + htmlTdEnd;
                //messageBody += htmlTdStart + "Balance_LCY" + htmlTdEnd;
                //messageBody += htmlTdStart + "Balance_OCY" + htmlTdEnd;
                //messageBody += htmlTdStart + "GL_CATEGORY" + htmlTdEnd;
                //messageBody += htmlTdStart + "GL_Type" + htmlTdEnd;
                //messageBody += htmlTdStart + "Date" + htmlTdEnd;
                //messageBody += htmlTdStart + "Primary_Owner_Name" + htmlTdEnd;
                //messageBody += htmlTdStart + "Primary_Owner_EmailID" + htmlTdEnd;
                //messageBody += htmlTdStart + "Owner_Department" + htmlTdEnd;
                //messageBody += htmlTdStart + "Department_or_SM_Email_ID" + htmlTdEnd;
                //messageBody += htmlTdStart + "Certification action" + htmlTdEnd;
                //messageBody += htmlTdStart + "Comment" + htmlTdEnd;

                messageBody += htmlHeaderRowEnd;

                for (int i = 0; i <= table.Rows.Count - 1; i++)
                {
                    if (table.Rows[i][0].ToString() != "")
                    {
                        messageBody = messageBody + htmlTrStart;
                        messageBody = messageBody + htmlTdStart + table.Rows[i][0] + htmlTdEnd;
                        messageBody = messageBody + htmlTdStart + table.Rows[i][1] + htmlTdEnd;
                        messageBody = messageBody + htmlTdStart + table.Rows[i][2] + htmlTdEnd;
                        messageBody = messageBody + htmlTdStart + table.Rows[i][3] + htmlTdEnd;
                        messageBody = messageBody + htmlTdStart + table.Rows[i][4] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][10] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][11] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][12] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][13] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][14] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][15] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][16] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][17] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][18] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][21] + htmlTdEnd;
                        //messageBody = messageBody + htmlTdStart + table.Rows[i][22] + htmlTdEnd;
                        messageBody = messageBody + htmlTrEnd;
                    }
                }
                messageBody = messageBody + htmlTableEnd;
                return messageBody;
            }
            catch (Exception)
            {
                return null;
            }

        }


        public string GetIPLocation()
        {
            string address = "";
            try
            {
                string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(ipAddress))
                {
                    ipAddress = Request.ServerVariables["REMOTE_ADDR"];
                }
                //string ipAddress = "203.115.122.229";
                string xmlString = IPRequestHelper("http://ip-api.com/xml/" + ipAddress);
                if (!string.IsNullOrEmpty(xmlString))
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(xmlString);
                    XmlNode statusNode = xdoc.SelectSingleNode("/query/" + "status");
                    if (statusNode.InnerText == "success")
                    {
                        XmlNode cityNode = xdoc.SelectSingleNode("/query/" + "city");
                        XmlNode regionNameNode = xdoc.SelectSingleNode("/query/" + "regionName");
                        XmlNode regionNode = xdoc.SelectSingleNode("/query/" + "region");
                        XmlNode countryNode = xdoc.SelectSingleNode("/query/" + "country");
                        XmlNode countryCodeNode = xdoc.SelectSingleNode("/query/" + "countryCode");
                        XmlNode zipCode = xdoc.SelectSingleNode("/query/" + "zip");

                        address = cityNode.InnerText + ", ";
                        address += regionNameNode.InnerText + " (" + regionNode.InnerText + "), ";
                        address += countryNode.InnerText + " (" + countryCodeNode.InnerText + ")";
                        address += "Pincode (" + zipCode.InnerText + ")";
                    }
                }

                ViewState["Loc"] = address + ipAddress;

            }
            catch
            {
                address = string.Empty;
            }
            return address;
        }

        private string IPRequestHelper(string url)
        {
            string responseRead = string.Empty;
            try
            {
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                StreamReader responseSteam = new StreamReader(objResponse.GetResponseStream());
                responseRead = responseSteam.ReadToEnd();
                responseSteam.Close();
                responseSteam.Dispose();
            }
            catch
            {
                responseRead = string.Empty;
            }
            return responseRead;
        }



        protected void GetEmpType()
        {
            ds = objcommon.GetEmpType((string)Session["sCompID"], (string)Session["sEmpID"]);

            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "RM")
            {
                btnApproval.Visible = true;
            }
            else
            {
                btnApproval.Visible = false;
            }
            GetPendingCount();

        }

        protected void GetPendingCount()
        {
            dsPen = objcommon.GetPendingData((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

            lblPenAttendanceCount.Text = dsPen.Tables[0].Rows[0]["ATTENDANCE"].ToString();
            lblPenLeaveCount.Text = dsPen.Tables[1].Rows[0]["LEAVE"].ToString();
            lblPenHandsetCount.Text = dsPen.Tables[2].Rows[0]["MOBILE"].ToString();
            lblPenTeleCount.Text = dsPen.Tables[3].Rows[0]["TELE"].ToString();
            lblPenDomesticCount.Text = dsPen.Tables[4].Rows[0]["DOM"].ToString();
            lblPenLocalCount.Text = dsPen.Tables[5].Rows[0]["LOCAL"].ToString();

        }

        protected void GetApprovedCount()
        {
            dsAppr = objcommon.GetApprovedData((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

            lblApprAttendanceCount.Text = dsAppr.Tables[0].Rows[0]["ATTENDANCE"].ToString();
            lblApprLeaveCount.Text = dsAppr.Tables[1].Rows[0]["LEAVE"].ToString();
            lblApprMobCount.Text = dsAppr.Tables[2].Rows[0]["MOBILE"].ToString();
            lblApprTeleCount.Text = dsAppr.Tables[3].Rows[0]["TELE"].ToString();
            lblApprDomCount.Text = dsAppr.Tables[4].Rows[0]["DOM"].ToString();
            lblApprLocCount.Text = dsAppr.Tables[5].Rows[0]["LOCAL"].ToString();
        }

        protected void GetApprovalCount()
        {
            dsAprvl = objcommon.GetPendingApprovalData((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

            lblAprvlAttendanceCount.Text = dsAprvl.Tables[0].Rows[0]["ATTENDANCE"].ToString();
            lblAprvlLeaveCount.Text = dsAprvl.Tables[1].Rows[0]["LEAVE"].ToString();
            lblAprvlMobCount.Text = dsAprvl.Tables[2].Rows[0]["MOBILE"].ToString();
            lblAprvlTeleCount.Text = dsAprvl.Tables[3].Rows[0]["TELE"].ToString();
            lblAprvlDomCount.Text = dsAprvl.Tables[4].Rows[0]["DOM"].ToString();
            lblAprvlLocCount.Text = dsAprvl.Tables[5].Rows[0]["LOCAL"].ToString();
        }

        protected void btnPending_Click(object sender, EventArgs e)
        {
            divPending.Visible = true;
            divApproved.Visible = false;
            divApproval.Visible = false;
            GetPendingCount();
        }

        protected void btnApproved_Click(object sender, EventArgs e)
        {
            divApproved.Visible = true;
            divPending.Visible = false;
            divApproval.Visible = false;
            GetApprovedCount();
        }

        protected void btnApproval_Click(object sender, EventArgs e)
        {
            divApproval.Visible = true;
            divPending.Visible = false;
            divApproved.Visible = false;
            GetApprovalCount();
        }

        protected void lnkPenAttendance_Click(object sender, EventArgs e)
        {
            try
            {
                dsattend = objcommon.GetPendingDataattend((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

                divPending.Visible = false;
                divApproved.Visible = false;
                divApproval.Visible = false;
                divpencancel.Visible = true;
                gvattendance.DataSource = dsattend;
                gvattendance.DataBind();
                gvattendance.Visible = true;

            }
            catch (Exception ex)
            {

            }

        }
        protected void lblPenLeave_Click(object sender, EventArgs e)
        {
            try
            {
                dsleave = objcommon.GetPendingDataall((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

                divPending.Visible = false;
                divApproved.Visible = false;
                divApproval.Visible = false;
                divpencancel.Visible = true;
                gvLeave.DataSource = dsleave;
                gvLeave.DataBind();
                gvLeave.Visible = true;
            }
            catch (Exception ex)
            {

            }

        }


        protected void lblPenHandset_Click(object sender, EventArgs e)
        {
            try
            {
                dsexpense = objcommon.GetPendingDatahandset((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

                divPending.Visible = false;
                divApproved.Visible = false;
                divApproval.Visible = false;
                divpencancel.Visible = true;
                gvexpense.DataSource = dsexpense;
                gvexpense.DataBind();
                gvexpense.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }
        protected void lblPenTele_Click(object sender, EventArgs e)
        {
            try
            {
                dsexpense = objcommon.GetPendingtel((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

                divPending.Visible = false;
                divApproved.Visible = false;
                divApproval.Visible = false;
                divpencancel.Visible = true;
                gvexpense.DataSource = dsexpense;
                gvexpense.DataBind();
                gvexpense.Visible = true;
            }
            catch (Exception ex)
            {

            }

        }
        protected void lblPenLocal_Click(object sender, EventArgs e)
        {
            try
            {
                dsexpense = objcommon.GetPendingLOCAL((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

                divPending.Visible = false;
                divApproved.Visible = false;
                divApproval.Visible = false;
                divpencancel.Visible = true;
                gvexpense.DataSource = dsexpense;
                gvexpense.DataBind();
                gvexpense.Visible = true;
            }
            catch (Exception ex)
            {

            }

        }
        protected void lblPenDomestic_Click(object sender, EventArgs e)
        {
            try
            {
                dsdom = objcommon.GetPendingDOM((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

                divPending.Visible = false;
                divApproved.Visible = false;
                divApproval.Visible = false;
                divpencancel.Visible = true;

                gvDom.DataSource = dsdom;
                gvDom.DataBind();
                gvDom.Visible = true;
            }
            catch (Exception ex)
            {

            }

        }

        protected void lnkAprvlAttendance_Click(object sender, EventArgs e)
        {
            Response.Redirect("../ESS/ApprovalAttendanceEdit.aspx");
        }

        protected void lnkAprvlLeave_Click(object sender, EventArgs e)
        {
            Response.Redirect("../ESS/LeaveApproval.aspx");
        }

        protected void lnkAprvlMob_Click(object sender, EventArgs e)
        {
            Response.Redirect("../ESS/ApprovelTelephonePage.aspx");

        }
        protected void lnkAprvlTele_Click(object sender, EventArgs e)
        {
            Response.Redirect("../ESS/ApprovelTelephonePage.aspx");
        }
        protected void lnkAprvlLoc_Click(object sender, EventArgs e)
        {
            Response.Redirect("../ESS/ApprovalLocalPage.aspx");
        }

        protected void lnkAprvlDom_Click(object sender, EventArgs e)
        {
            Response.Redirect("../ESS/ApprovalDomesticPage.aspx");
        }

        protected void lnkApprAttendance_Click(object sender, EventArgs e)
        {
            try
            {
                dsattend = objcommon.GetApprovedDataATTEND((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

                divPending.Visible = false;
                divApproved.Visible = false;
                divApproval.Visible = false;
                divapprcancel.Visible = true;

                gvattendanceappr.DataSource = dsattend;
                gvattendanceappr.DataBind();
                gvattendanceappr.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkApprLeave_Click(object sender, EventArgs e)
        {
            try
            {
                dsleave = objcommon.GetApprovedDataall((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

                divPending.Visible = false;
                divApproved.Visible = false;
                divApproval.Visible = false;
                divapprcancel.Visible = true;
                gvLeaveappr.DataSource = dsleave;
                gvLeaveappr.DataBind();
                gvLeaveappr.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkApprLoc_Click(object sender, EventArgs e)
        {
            try
            {
                dsexpense = objcommon.GetApprovedloc((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

                divPending.Visible = false;
                divApproved.Visible = false;
                divApproval.Visible = false;
                divapprcancel.Visible = true;

                gvexpenseappr.DataSource = dsexpense;
                gvexpenseappr.DataBind();
                gvexpenseappr.Visible = true;
            }
            catch (Exception ex)
            {

            }

        }
        protected void lnkApprTele_Click(object sender, EventArgs e)
        {
            try
            {
                dsexpense = objcommon.GetApprovedtele((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

                divPending.Visible = false;
                divApproved.Visible = false;
                divApproval.Visible = false;
                divapprcancel.Visible = true;

                gvexpenseappr.DataSource = dsexpense;
                gvexpenseappr.DataBind();
                gvexpenseappr.Visible = true;
            }
            catch (Exception ex)
            {

            }

        }
        protected void lnkApprMob_Click(object sender, EventArgs e)
        {
            try
            {
                dsexpense = objcommon.GetApprovedmob((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

                divPending.Visible = false;
                divApproved.Visible = false;
                divApproval.Visible = false;
                divapprcancel.Visible = true;

                gvexpenseappr.DataSource = dsexpense;
                gvexpenseappr.DataBind();
                gvexpenseappr.Visible = true;
            }
            catch (Exception ex)
            {

            }

        }


        protected void lnkApprDom_Click(object sender, EventArgs e)
        {
            try
            {
                dsdom = objcommon.GetApprovedDOM((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);
                divPending.Visible = false;
                divApproved.Visible = false;
                divApproval.Visible = false;
                divapprcancel.Visible = true;

                gvDomappr.DataSource = dsdom;
                gvDomappr.DataBind();
                gvDomappr.Visible = true;
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnpenback_Click(object sender, EventArgs e)
        {
            btnPending_Click(sender, e);
            gvLeave.Visible = false;
            gvattendance.Visible = false;
            gvexpense.Visible = false;
            gvDom.Visible = false;
            gvDomappr.Visible = false;
            gvexpenseappr.Visible = false;
            gvLeaveappr.Visible = false;
            gvattendanceappr.Visible = false;
            divpencancel.Visible = false;
        }
        protected void btnapprback_Click(object sender, EventArgs e)
        {
            btnApproved_Click(sender, e);
            gvLeave.Visible = false;
            gvattendance.Visible = false;
            gvexpense.Visible = false;
            gvDom.Visible = false;
            gvDomappr.Visible = false;
            gvexpenseappr.Visible = false;
            gvLeaveappr.Visible = false;
            gvattendanceappr.Visible = false;
            divapprcancel.Visible = false;
        }
        protected void btnpenapprback_Click(object sender, EventArgs e)
        {
            btnApproval_Click(sender, e);
            gvLeave.Visible = false;
            gvattendance.Visible = false;
            gvexpense.Visible = false;
            gvDom.Visible = false;
            gvDomappr.Visible = false;
            gvexpenseappr.Visible = false;
            gvLeaveappr.Visible = false;
            gvattendanceappr.Visible = false;
            divpenapprback.Visible = false;
        }






        //protected void gvDom_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        Label lblStatus = (Label)e.Row.FindControl("lblStatus");

        //        if (lblStatus != null)
        //        {
        //            TemplateField pendingdays = (TemplateField)gvDom.Columns
        //            .Cast<DataControlField>()
        //            .SingleOrDefault(f => f.HeaderText == "Pending Days");


        //            if (lblStatus.Text == "Approved")
        //            {
        //                //e.Row.Cells[6].Visible = false;
        //                pendingdays.Visible = false;
        //            }
        //            else
        //            {
        //                //e.Row.Cells[6].Visible = true;
        //                pendingdays.Visible = true;
        //            }
        //        }
        //        else
        //        {

        //        }

        //    }
        //}
        //protected void gvDom_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblStatus = (Label)e.Row.FindControl("lblStatus");

        //        if (lblStatus != null)
        //        {
        //            // Accessing the cell directly by column index if "Pending Days" is a bound field
        //            TableCell pendingDaysCell = e.Row.Cells[6]; // Assuming "Pending Days" is at index 6

        //            if (lblStatus.Text == "Approved")
        //            {
        //                // Hide the Pending Days column
        //                pendingDaysCell.Visible = false;
        //            }
        //        }
        //    }
        //}




    }
}