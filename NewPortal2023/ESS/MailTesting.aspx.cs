using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class MailTesting : System.Web.UI.Page
    {
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendMail_Click(object sender, EventArgs e)
        {
            SENDTESTMAIL();
        }


        private void SENDTESTMAIL()
        {
            try
            {
                emailSend = new NewPortal2023.ESS.Email();

                string EMPNAME = "Test User";
                string type = "Travel";
                string frDate = DateTime.Now.ToString("dd-MM-yyyy");
                DateTime date = DateTime.Now;

                string clientBody = "Dear " + EMPNAME + ",<br><br>This is a <b>test mail</b> to confirm your " + type + " Expense submission flow.<br>"
                    + "This is only for testing purposes. No actual reimbursement is processed.<br>"
                    + "<br>Reimbursement type: " + type
                    + "<br>Applied Date: " + date.ToString("dd-MM-yyyy hh:mm tt")
                    + "<br>Bill Date: " + frDate
                    + "<br><br>Thank you.<br><br>";

                string testEmailTo = "techsupport @sequelgroup.co.in"; 
                string subject = "TEST MAIL: Expense Submission Confirmation";

                // Send to user
                emailSend.SendEmailNPL(testEmailTo, subject, clientBody);

                //System.Threading.Thread.Sleep(500);

                string checkerBody = "Dear Payroll Team,<br><br>This is a <b>test notification</b> for " + EMPNAME +
                    "'s " + type + " Expense submission.<br>No action is needed.<br>"
                    + "<br>Reimbursement type: " + type
                    + "<br>Applied Date: " + date.ToString("dd-MM-yyyy hh:mm tt")
                    + "<br>Bill Date: " + frDate
                    + "<br><br>Thank you.<br><br>";

                string checkerEmail = "payrollservices@sequelgroup.co.in";  // another test recipient

                emailSend.SendEmailNPL(checkerEmail, subject, checkerBody);

                lblMessage.Text = "Test mail sent successfully.";
                lblMessage.ForeColor = System.Drawing.Color.Green;
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error sending test mail: " + ex.Message;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}