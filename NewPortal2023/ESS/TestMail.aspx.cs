using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class TestMail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string emailTo = txtEmailTo.Text.Trim();
                string emailCC = txtEmailCC.Text.Trim();
                string subject = txtSubject.Text.Trim();
                string body = txtBody.Text.Trim();

                Email objEmail = new Email(); // your class

                objEmail.SendEmailALLNPL(emailTo, emailCC, subject, body);

                lblMessage.ForeColor = System.Drawing.Color.Green;
                lblMessage.Text = "Email sent successfully!";
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Error: " + ex.Message;
            }
        }
    }
}