using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NewPortal2023.Helper
{
    public class ExpenseEmail
    {
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();


        public void SendDomesticEmail(string ClaimNO, string Approver, string Action, string radiochkent, string fromdate, string compId, string empId)
        {
            try
            {
                emailSend = new NewPortal2023.ESS.Email();
                DataSet dsmkkMail = new DataSet();
                DateTime date = DateTime.Now;

                if (Action == "Approve")
                {
                    dsmkkMail = emailSend.GetEmpAttendanceRecDe(compId, empId, ClaimNO, Approver, radiochkent);



                    if (dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() != "")
                    {
                        string clientbodys = "Dear Sir" + ",<br><br> Domestic Travel Expense is received for approval." + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + " claim is Approved by the " + Approver
                        + ".Kindly take the action for the same through logging-in into ESS portal.<br>"
                       + " <br>Claim Type:- Domestic Travel Expense"
                             + "<br>Claim No :- " + ClaimNO
                             + "<br>Travel Date :- " + fromdate
                             + "<br><br>ThankYou.<br><br>";

                        List<string> toEmailList = new List<string>();
                        foreach (DataRow row in dsmkkMail.Tables[2].Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(row["APPROVARMAIL"].ToString()))
                                toEmailList.Add(row["APPROVARMAIL"].ToString().Trim());
                        }

                        string emails = string.Join(",", toEmailList.Distinct());

                        List<string> ccEmailList = new List<string>();

                        foreach (DataRow row in dsmkkMail.Tables[0].Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(row["CHECKERMAIL"].ToString()))
                                ccEmailList.Add(row["CHECKERMAIL"].ToString().Trim());
                        }

                        foreach (DataRow row in dsmkkMail.Tables[1].Rows)
                        {
                            if (!string.IsNullOrWhiteSpace(row["EMP_EMAIL"].ToString()))
                                ccEmailList.Add(row["EMP_EMAIL"].ToString().Trim());
                        }

                        string emailsCC = string.Join(",", ccEmailList.Distinct());


                        string subjects = "Do Not Reply: Domestic Travel Expense";
                        emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);

                    }
                }
                else if (Action == "Reject")
                {
                    dsmkkMail = emailSend.GetEmpRejectMAil(compId, empId, ClaimNO, Approver, radiochkent);

                    string clientbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>Domestic Travel Expense is rejected by the " + dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString()
                       + ".Kindly check the claim for the same through logging-in into ESS portal.<br>"
                      + " <br>Claim Type:- Domestic Travel Expense"
                            + "<br>Claim No :- " + ClaimNO
                            + "<br>Travel Date :- " + fromdate
                            + "<br><br>ThankYou.<br><br>";
                    List<string> toEmailList = new List<string>();
                    foreach (DataRow row in dsmkkMail.Tables[1].Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["EMP_EMAIL"].ToString()))
                            toEmailList.Add(row["EMP_EMAIL"].ToString().Trim());
                    }
                    string emails = string.Join(",", toEmailList.Distinct());

                    List<string> ccEmailList = new List<string>();

                    foreach (DataRow row in dsmkkMail.Tables[0].Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["CHECKERMAIL"].ToString()))
                            ccEmailList.Add(row["CHECKERMAIL"].ToString().Trim());
                    }
                    string emailsCC = string.Join(",", ccEmailList.Distinct());
                  
                    string subjects = "Do Not Reply: Domestic Travel Expense";
                    emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public void SendLocalEmail(string ClaimNO, string Approver, string Action, string radiochkent, string fromdate, string compId, string empId)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DateTime date = DateTime.Now;

            if (Action == "Approve")
            {
                dsmkkMail = emailSend.GetEmpLocalEXpRecDe(compId, empId, ClaimNO, Approver, radiochkent);


                if (dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() != "")
                {
                    string clientbodys = "Dear Sir" + ",<br><br> Local Travel Expense is received for approval." + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + " claim is Approved by the " + Approver
                    + ".Kindly take the action for the same through logging-in into ESS portal.<br>"
                   + " <br>Claim Type:- Local Travel Expense"
                         + "<br>Claim No :- " + ClaimNO
                         + "<br>Travel Date :- " + fromdate
                         + "<br><br>ThankYou.<br><br>";
                    List<string> toEmailList = new List<string>();
                    foreach (DataRow row in dsmkkMail.Tables[2].Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["APPROVARMAIL"].ToString()))
                            toEmailList.Add(row["APPROVARMAIL"].ToString().Trim());
                    }

                    string emails = string.Join(",", toEmailList.Distinct());

                    List<string> ccEmailList = new List<string>();

                    foreach (DataRow row in dsmkkMail.Tables[0].Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["CHECKERMAIL"].ToString()))
                            ccEmailList.Add(row["CHECKERMAIL"].ToString().Trim());
                    }

                    foreach (DataRow row in dsmkkMail.Tables[1].Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["EMP_EMAIL"].ToString()))
                            ccEmailList.Add(row["EMP_EMAIL"].ToString().Trim());
                    }

                    string emailsCC = string.Join(",", ccEmailList.Distinct());
                   
                    string subjects = "Do Not Reply: Local Travel Expense";

                    emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);

                }
            }
            else if (Action == "Reject")
            {
                dsmkkMail = emailSend.GetEmpLocRejectMAil(compId, empId, ClaimNO, Approver, radiochkent);

                string clientbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>Local Travel Expense is rejected by the " + dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString()
                   + ".Kindly check the claim for the same through logging-in into ESS portal.<br>"
                  + " <br>Claim Type:- Local Travel Expense"
                        + "<br>Claim No :- " + ClaimNO
                        + "<br>Travel Date :- " + fromdate
                        + "<br><br>ThankYou.<br><br>";
                List<string> toEmailList = new List<string>();
                foreach (DataRow row in dsmkkMail.Tables[1].Rows)
                {
                    if (!string.IsNullOrWhiteSpace(row["EMP_EMAIL"].ToString()))
                        toEmailList.Add(row["EMP_EMAIL"].ToString().Trim());
                }
                string emails = string.Join(",", toEmailList.Distinct());

                List<string> ccEmailList = new List<string>();

                foreach (DataRow row in dsmkkMail.Tables[0].Rows)
                {
                    if (!string.IsNullOrWhiteSpace(row["CHECKERMAIL"].ToString()))
                        ccEmailList.Add(row["CHECKERMAIL"].ToString().Trim());
                }
                string emailsCC = string.Join(",", ccEmailList.Distinct());
              
                string subjects = "Do Not Reply: Local Travel Expense";
                emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);
            }
        }


        public void SendTelephoneEmail(string ClaimNO, string Approver, string Action, string TeleType, string fromdate, string compId, string empId)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DateTime date = DateTime.Now;

            if (Action == "Approve")
            {
                dsmkkMail = emailSend.GetEmpLocalEXpRecDe(compId, empId, ClaimNO, Approver);


                if (dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() != "")
                {
                    string clientbodys = "Dear Sir" + ",<br><br> Telephone Claim is received for approval." + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + " claim is Approved by the " + Approver
                    + ".Kindly take the action for the same through logging-in into ESS portal.<br>"
                    + " <br>Claim Type:- " + TeleType + " Expense"
                         + "<br>Claim No :- " + ClaimNO
                         + "<br>Bill Date :- " + fromdate
                         + "<br><br>ThankYou.<br><br>";
                    List<string> toEmailList = new List<string>();
                    foreach (DataRow row in dsmkkMail.Tables[2].Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["APPROVARMAIL"].ToString()))
                            toEmailList.Add(row["APPROVARMAIL"].ToString().Trim());
                    }

                    string emails = string.Join(",", toEmailList.Distinct());

                    List<string> ccEmailList = new List<string>();

                    foreach (DataRow row in dsmkkMail.Tables[0].Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["CHECKERMAIL"].ToString()))
                            ccEmailList.Add(row["CHECKERMAIL"].ToString().Trim());
                    }

                    foreach (DataRow row in dsmkkMail.Tables[1].Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["EMP_EMAIL"].ToString()))
                            ccEmailList.Add(row["EMP_EMAIL"].ToString().Trim());
                    }

                    string emailsCC = string.Join(",", ccEmailList.Distinct());
                  
                    string subjects = "Do Not Reply: Telephone Expense";
                    emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);

                }
            }
            else if (Action == "Reject")
            {
                dsmkkMail = emailSend.GetEmpTeleRejectMAil(compId, empId, ClaimNO, Approver);

                string clientbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>Telephone Claim is rejected by the " + dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString()
                   + ".Kindly check the claim for the same through logging-in into ESS portal.<br>"
                  + " <br>Claim Type:- " + TeleType + " Expense"
                        + "<br>Claim No :- " + ClaimNO
                        + "<br>Bill Date :- " + fromdate
                        + "<br><br>ThankYou.<br><br>";
                List<string> toEmailList = new List<string>();
                foreach (DataRow row in dsmkkMail.Tables[1].Rows)
                {
                    if (!string.IsNullOrWhiteSpace(row["EMP_EMAIL"].ToString()))
                        toEmailList.Add(row["EMP_EMAIL"].ToString().Trim());
                }
                string emails = string.Join(",", toEmailList.Distinct());

                List<string> ccEmailList = new List<string>();

                foreach (DataRow row in dsmkkMail.Tables[0].Rows)
                {
                    if (!string.IsNullOrWhiteSpace(row["CHECKERMAIL"].ToString()))
                        ccEmailList.Add(row["CHECKERMAIL"].ToString().Trim());
                }
                string emailsCC = string.Join(",", ccEmailList.Distinct());

                string subjects = "Do Not Reply: Telephone Expense";
                emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);
            }
        }


        public void SendMiscEmail(string ClaimNO, string Approver, string Action, string voucherDate, string compId, string empId)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DateTime date = DateTime.Now;

            if (Action == "Approve" || Action == "APPROVE")
            {
                dsmkkMail = emailSend.GetEmpMiscEXpRecDe(compId, empId, ClaimNO, Approver);


                if (dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() != "")
                {
                    string clientbodys = "Dear Sir" +  ",<br><br> A Miscellaneous Expense is received for approval." + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + " claim is Approved by the " + Approver
                    + ".Kindly take the action for the same through logging-in into ESS portal.<br>"
                   + " <br>Claim Type:- Miscellaneous Expense"
                         + "<br>Claim No :- " + ClaimNO
                         //+ "<br>Voucher Date :- " + voucherDate
                         + "<br><br>ThankYou.<br><br>";
                    List<string> toEmailList = new List<string>();
                    foreach (DataRow row in dsmkkMail.Tables[2].Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["APPROVARMAIL"].ToString()))
                            toEmailList.Add(row["APPROVARMAIL"].ToString().Trim());
                    }

                    string emails = string.Join(",", toEmailList.Distinct());

                    List<string> ccEmailList = new List<string>();

                    foreach (DataRow row in dsmkkMail.Tables[0].Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["CHECKERMAIL"].ToString()))
                            ccEmailList.Add(row["CHECKERMAIL"].ToString().Trim());
                    }

                    foreach (DataRow row in dsmkkMail.Tables[1].Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(row["EMP_EMAIL"].ToString()))
                            ccEmailList.Add(row["EMP_EMAIL"].ToString().Trim());
                    }

                    string emailsCC = string.Join(",", ccEmailList.Distinct());
                   
                    string subjects = "Do Not Reply: Miscellaneous Expense";
                    emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);

                }
            }
            else if (Action == "Reject" || Action == "REJECT")
            {
                dsmkkMail = emailSend.GetMiscRejectMAil(compId, empId, ClaimNO, Approver);

                string clientbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>A Miscellaneous Expense is rejected by the " + dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString()
                   + ".Kindly check the claim for the same through logging-in into ESS portal.<br>"
                  + " <br>Claim Type:- Miscellaneous Expense"
                        + "<br>Claim No :- " + ClaimNO
                        //+ "<br>Voucher Date :- " + voucherDate
                        + "<br><br>ThankYou.<br><br>";
                List<string> toEmailList = new List<string>();
                foreach (DataRow row in dsmkkMail.Tables[1].Rows)
                {
                    if (!string.IsNullOrWhiteSpace(row["EMP_EMAIL"].ToString()))
                        toEmailList.Add(row["EMP_EMAIL"].ToString().Trim());
                }
                string emails = string.Join(",", toEmailList.Distinct());

                List<string> ccEmailList = new List<string>();

                foreach (DataRow row in dsmkkMail.Tables[0].Rows)
                {
                    if (!string.IsNullOrWhiteSpace(row["CHECKERMAIL"].ToString()))
                        ccEmailList.Add(row["CHECKERMAIL"].ToString().Trim());
                }
                string emailsCC = string.Join(",", ccEmailList.Distinct());
           
                string subjects = "Do Not Reply: Miscellaneous Expense";
                emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);
            }
        }
    }
}