using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace NewPortal2023.ESS
{
    public class Email
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;
        string email_aid = "";
        public void SendEmail(string[] emailTo, string subject, string body, string attachmentPath)
        {
            MailMessage mailMessage = new MailMessage();
            foreach (string id in emailTo)
            {
                mailMessage.To.Add(id);
            }
            mailMessage.From = new MailAddress("reports@sequelgroup.co.in");
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(attachmentPath, System.Net.Mime.MediaTypeNames.Application.Octet);
            mailMessage.Attachments.Add(data);

            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.UseDefaultCredentials = false;
            //smtpClient.Credentials = new System.Net.NetworkCredential("reports@sequelgroup.co.in", "sequel@123");
            //smtpClient.EnableSsl = true;
            //smtpClient.Send(mailMessage);

            // SmtpClient smtpClient = new SmtpClient("trans.briskmailer.com", 587);
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential("reports@sequelgroup.co.in", "sequel@123");
            smtpClient.EnableSsl = true;
            smtpClient.Send(mailMessage);

            data.Dispose();
            mailMessage.Dispose();
            smtpClient.Dispose();
        }


        public void SendEmailBT(string emailTo, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(emailTo);
            mailMessage.From = new MailAddress("hrrlservicesdesk@hrrl.org");
            mailMessage.Subject = subject;
            mailMessage.Body = body;


            SmtpClient smtpClient = new SmtpClient("hrrl.org", 587);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;

            smtpClient.Credentials = new System.Net.NetworkCredential("hrrlservicesdesk@hrrl.org", "Hrrl@123");
            smtpClient.EnableSsl = false;
            smtpClient.Send(mailMessage);

            mailMessage.Dispose();
            smtpClient.Dispose();
        }

        public void SendEmailNPL(string emailTo, string subject, string body)
        {
            string emailcc = "";
            string status = "SUCCESS";
            string errorBody = "";

            using (var mailMessage = new MailMessage())
            using (var smtpClient = new SmtpClient("smtp.office365.com", 587))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(emailTo))
                        throw new Exception("Primary recipient (emailTo) is empty.");

                    foreach (var email in emailTo.Split(','))
                    {
                        if (!string.IsNullOrWhiteSpace(email))
                            mailMessage.To.Add(email.Trim());
                    }

                    if (emailTo.Equals("rajiv.arora@naperol.com", StringComparison.OrdinalIgnoreCase))
                    {
                        emailcc = "jayashri.panchbhai@naperol.com";
                        mailMessage.CC.Add(emailcc);
                    }
                    else if (emailTo.Equals("surabhi.mittal@naperol.com", StringComparison.OrdinalIgnoreCase))
                    {
                        emailcc = "pankaj.marke@naperol.com";
                        mailMessage.CC.Add(emailcc);
                    }

                    mailMessage.From = new MailAddress("npl.employee@naperol.com");
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;
                    mailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                    smtpClient.Timeout = 20000;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("npl.employee@naperol.com", "Muw66004");
                    smtpClient.EnableSsl = true;

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    SendWithRetry(smtpClient, mailMessage, 2);
                }
                catch (SmtpException smtpEx)
                {
                    status = "SMTP_FAILED";

                    errorBody = "<br><b>SMTP Error:</b> " + smtpEx.Message +
                                "<br><b>Status Code:</b> " + smtpEx.StatusCode +
                                "<br><b>Inner Exception:</b> " + (smtpEx.InnerException.ToString() ?? "NULL") +
                                "<br><b>Stack Trace:</b> " + smtpEx.StackTrace;
                }
                catch (Exception ex)
                {
                    status = "FAILED";

                    errorBody = "<br><b>Error:</b> " + ex.Message +
                                "<br><b>Stack Trace:</b> " + ex.StackTrace;
                }
                finally
                {
                    // ✅ 6. Single logging point
                    InsertAfter(
                        emailTo,
                        emailcc,
                        subject + (status == "SUCCESS"),
                        status == "SUCCESS" ? body : errorBody,
                        "CO000141",
                        email_aid
                    );
                }
            }
        }

        public void SendEmailALLNPL(string emailTo, string emailsCC, string subject, string body)
        {
            string emailcc = emailsCC;
            string status = "SUCCESS";
            string errorBody = "";

            using (var mailMessage = new MailMessage())
            using (var smtpClient = new SmtpClient("smtp.office365.com", 587))
            {
                try
                {
                    if (string.IsNullOrWhiteSpace(emailTo))
                        throw new Exception("Primary recipient (emailTo) is empty.");


                    foreach (var email in emailTo.Split(','))
                    {
                        if (!string.IsNullOrWhiteSpace(email))
                            mailMessage.To.Add(email.Trim());
                    }

                    if (!string.IsNullOrWhiteSpace(emailsCC))
                    {
                        foreach (var email in emailsCC.Split(','))
                        {
                            if (!string.IsNullOrWhiteSpace(email))
                                mailMessage.CC.Add(email.Trim());
                        }
                    }

                    if (emailTo.Equals("rajiv.arora@naperol.com", StringComparison.OrdinalIgnoreCase) &&
                        (emailsCC == null || !emailsCC.Contains("jayashri.panchbhai@naperol.com")))
                    {
                        mailMessage.CC.Add("jayashri.panchbhai@naperol.com");
                    }
                    else if (emailTo.Equals("surabhi.mittal@naperol.com", StringComparison.OrdinalIgnoreCase) &&
                             (emailsCC == null || !emailsCC.Contains("pankaj.marke@naperol.com")))
                    {
                        mailMessage.CC.Add("pankaj.marke@naperol.com");
                    }

                    mailMessage.From = new MailAddress("npl.employee@naperol.com");
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    smtpClient.Timeout = 20000;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("npl.employee@naperol.com", "Muw66004");
                    smtpClient.EnableSsl = true;

                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    SendWithRetry(smtpClient, mailMessage, 2);
                }
                catch (SmtpException smtpEx)
                {
                    status = "SMTP_FAILED";

                    errorBody = "<br><b>SMTP Error:</b> " + smtpEx.Message +
                                "<br><b>Status Code:</b> " + smtpEx.StatusCode +
                                "<br><b>Inner Exception:</b> " + (smtpEx.InnerException.ToString() ?? "NULL") +
                                "<br><b>Stack Trace:</b> " + smtpEx.StackTrace;
                }
                catch (Exception ex)
                {
                    status = "FAILED";

                    errorBody = "<br><b>Error:</b> " + ex.Message +
                                "<br><b>Stack Trace:</b> " + ex.StackTrace;
                }
                finally
                {
                    // ✅ 7. Single logging point
                    InsertAfter(
                        emailTo,
                        emailcc,
                        subject + (status == "SUCCESS"),
                        status == "SUCCESS" ? body : errorBody,
                        "CO000141",
                        email_aid
                    );
                }
            }
        }

        private void SendWithRetry(SmtpClient smtpClient, MailMessage mailMessage, int retries)
        {
            for (int i = 0; i <= retries; i++)
            {
                try
                {
                    smtpClient.Send(mailMessage);
                    return;
                }
                catch (SmtpException)
                {
                    if (i == retries)
                        throw;

                    Thread.Sleep(2000);
                }
            }
        }

        public void SendEmailNPLPMS(MailMessage mail, string mailTo, string subject, string mailBody)
        {
            mail.IsBodyHtml = true;
            mail.To.Add(mailTo);
            mail.From = new MailAddress("npl.employee@naperol.com");
            mail.Subject = subject;
            mail.Body = mailBody;

            SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
            new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            smtpClient.Credentials = new System.Net.NetworkCredential("npl.employee@naperol.com", "Muw66004");
            smtpClient.EnableSsl = true;
            smtpClient.Send(mail);

            mail.Dispose();
            smtpClient.Dispose();
        }

        private DataSet InsertAfter(string emailTo, string cc, string subject, string body, string compValue, string email_aid)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMAIL_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, emailTo.ToString().Trim());
            objDBUtility.AddParameters("@CC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cc == null ? "" : cc.Trim());
            objDBUtility.AddParameters("@SUBJECT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, subject.ToString().Trim());
            objDBUtility.AddParameters("@BODY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, body.ToString().Trim());
            objDBUtility.AddParameters("@REFENTRY_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, email_aid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "AFTERINSERT");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_EMAILLOG");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        private DataSet InsertBefore(string emailTo, string cc, string subject, string body, string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMAIL_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, emailTo.ToString().Trim());
            objDBUtility.AddParameters("@CC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, cc == null ? "" : cc.Trim());
            objDBUtility.AddParameters("@SUBJECT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, subject.ToString().Trim());
            objDBUtility.AddParameters("@BODY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, body.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "BEFORINSERT");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_EMAILLOG");
            if (dsEmpData.Tables.Count > 0)
            {
                email_aid = dsEmpData.Tables[0].Rows[0]["EMAIL_AID"].ToString();
            }
            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            //Console.WriteLine(certificate);
            return true;
        }

        public DataSet GetEmpAttendanceaPPROVE(string compValue, string empValue, string id)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, id.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPATTENDANCERECAPPROVEDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetEmpDetails(string compValue, string empValue, string allId)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPEMAILDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetChekerDetails(string compValue, string empValue, string allId, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsChkData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCHECKEREMAILID");
            dsChkData = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsChkData;
        }

        public DataSet GetReortingDetails(string compValue, string empValue, string allId, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsChkData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREPORTINGEMAILID");
            dsChkData = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsChkData;
        }




        public DataSet GetTelEmpDetails(string compValue, string empValue, string allId)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELEMPEMAILDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetTelChekerDetails(string compValue, string empValue, string allId, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsChkData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELCHECKEREMAILID");
            dsChkData = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsChkData;
        }

        public DataSet GetTelMakerDetails(string compValue, string empValue, string allId, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsChkData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELMAKEREMAILID");
            dsChkData = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsChkData;
        }


        public DataSet GetTelChekerRejDetails(string compValue, string empValue, string allId, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsChkData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELCHECKEREMAILID");
            dsChkData = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsChkData;
        }



        public DataSet GetCompEmpDetails(string compValue, string empValue, string allId)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMEMPEMAILDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }


        public DataSet GetEmpAttendanceRecDe(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPATTENDANCERECDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetEmpAttendanceLeaveaPPROVE(string compValue, string empValue, string id)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, id.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPATTENDANCERECLEVAEAPPROVEDETAILS");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet getName(string compValue, string leaveType)
        {
            objDBUtility = new DBUtility();

            DataSet dsChkData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@LEAVEID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, leaveType.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLEAVETYPE");
            dsChkData = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_EMAILLOG");

            objDBUtility.ClearTransactionalParams();

            return dsChkData;
        }
        public DataSet GetrEMINDERRecDe(string compValue, string CID)
        {

            objDBUtility = new DBUtility();

            DataSet dsmkkMail = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREMINDERRECORD");
            dsmkkMail = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsmkkMail;
        }

        public DataSet GetrEMINDERRecDehr(string compValue, string CID)
        {

            objDBUtility = new DBUtility();

            DataSet dsmkkMail = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREMINDERRECORDHR");
            dsmkkMail = objDBUtility.Execute_StoreProc_DataSet("SP_NPL");

            objDBUtility.ClearTransactionalParams();

            return dsmkkMail;
        }

        public DataSet GetEmpLocalEXpRecDe(string compValue, string empValue, string ClaimNO, string Approver)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ClaimNO.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Approver.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELAPPROVEMAIL");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }



        public DataSet GetEmpTeleRejectMAil(string compValue, string empValue, string ClaimNO, string Approver)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ClaimNO.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Approver.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTELEREJECTMAIL");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetEmpLocalEXpRecDe(string compValue, string empValue, string ClaimNO, string Approver, string radiochkent)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ClaimNO.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Approver.ToString().Trim());
            objDBUtility.AddParameters("@RPT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, radiochkent.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLOCALAPPROVEMAIL");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetEmpLocRejectMAil(string compValue, string empValue, string ClaimNO, string Approver, string radiochkent)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ClaimNO.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Approver.ToString().Trim());
            objDBUtility.AddParameters("@RPT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, radiochkent.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLOCREJECTMAIL");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetEmpRejectMAil(string compValue, string empValue, string ClaimNO, string Approver, string radiochkent)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ClaimNO.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Approver.ToString().Trim());
            objDBUtility.AddParameters("@RPT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, radiochkent.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREJECTMAIL");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetEmpAttendanceRecDe(string compValue, string empValue, string ClaimNO, string Approver, string radiochkent)
        {

            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ClaimNO.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Approver.ToString().Trim());
            objDBUtility.AddParameters("@RPT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, radiochkent.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAPPROVEMAIL");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        /// <summary>
        /// ------------------------------------FOR OTP----------------------
        /// </summary>
        /// <param name="emailTo"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendEmail(string emailTo, string subject, string body)
        {
            MailMessage mailMessage = new MailMessage();

            mailMessage.To.Add(emailTo);
            mailMessage.From = new MailAddress("reports@sequelgroup.co.in");
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            mailMessage.IsBodyHtml = true;


            SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            smtpClient.Credentials = new System.Net.NetworkCredential("reports@sequelgroup.co.in", "sequel@123");
            smtpClient.EnableSsl = true;
            //smtpClient.Send(mailMessage);

            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com");
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.EnableSsl = true;
            //NetworkCredential Credential = new System.Net.NetworkCredential("hrrlservicesdesk@hrrl.org", "Hrrl@123");
            //smtpClient.UseDefaultCredentials = true;

            //smtpClient.Credentials = Credential;

            //smtpClient.Port = 587;
            //smtpClient.Send(mailMessage);

            //SmtpClient smtpClient = new SmtpClient("trans.briskmailer.com", 587);
            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            //smtpClient.UseDefaultCredentials = false;
            //smtpClient.Credentials = new System.Net.NetworkCredential("info@nettechinfo.net", @"/\$3ZKGdPrW6=*_L`/uME=>G");
            ////smtpClient.EnableSsl = true;
            //smtpClient.Send(mailMessage);


            mailMessage.Dispose();
            smtpClient.Dispose();
        }

        public DataSet GetMiscRejectMAil(string compValue, string empValue, string ClaimNO, string Approver)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ClaimNO.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Approver.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GEMISCREJECTMAIL");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public DataSet GetEmpMiscEXpRecDe(string compValue, string empValue, string ClaimNO, string Approver)
        {
            objDBUtility = new DBUtility();

            DataSet dsEmpData = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@CLAMNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ClaimNO.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Approver.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMISCAPPROVEMAIL");
            dsEmpData = objDBUtility.Execute_StoreProc_DataSet("SP_EXPENSES");

            objDBUtility.ClearTransactionalParams();

            return dsEmpData;
        }

        public void SendEmailTest(string emailTo, string subject, string body)
        {
            DataSet dsBefore = new DataSet();
            DataSet dsAfter = new DataSet();
            MailMessage mailMessage = new MailMessage();
            string emailcc = "";

            mailMessage.To.Add(emailTo);


            if (emailTo == "techsupport@sequelgroup.co.in")
            {
                //emailTo = emailTo + "," + "payrollservices@sequelgroup.co.in";
                mailMessage.To.Add("payrollservices@sequelgroup.co.in");
            }

            InsertBefore(emailTo, emailcc, subject, body, "CO000141");

            mailMessage.From = new MailAddress("npl.employee@naperol.com");
            mailMessage.Subject = subject;
            mailMessage.Body = body;
            mailMessage.IsBodyHtml = true;

            SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.UseDefaultCredentials = false;
            System.Net.ServicePointManager.ServerCertificateValidationCallback =
                new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            smtpClient.Credentials = new System.Net.NetworkCredential("npl.employee@naperol.com", "Muw66004");
            smtpClient.EnableSsl = true;

            smtpClient.Send(mailMessage);

            InsertAfter(emailTo, emailcc, subject, body, "CO000141", email_aid);
            mailMessage.Dispose();
            smtpClient.Dispose();
        }

    }


   
}