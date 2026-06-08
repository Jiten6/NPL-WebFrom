using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class Emailchecking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public void SENDUDATEMAIL(string VoucherNo, string type)
        {
            Email emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DateTime date = DateTime.Now;

            string EMPNAME = "abc";

            string clientbodys = "Dear " + EMPNAME + ",<br><br>Your " + type + " Expense is Submitted Successfully.<br>"
               + "Once the action taken will be notified to you through mail or same can be checked through ESS portal."
               + "<br>Reimbursement type:- " + type
               + "<br>Applied Date :- " + date
               + "<br>Voucher number:- " + VoucherNo
               + "<br><br>ThankYou.<br><br>";
            string emails = "techsupport@sequelgroup.co.in";
            string subjects = "Do Not Reply: Expense";
            emailSend.SendEmailTest(emails, subjects, clientbodys);


            string checkerbodys = "Dear Payroll Team,<br><br>" + EMPNAME +
                " " + type + " Expense is received for approval. Kindly take the action for the same through logging-in into ESS portal."
                + "<br>Reimbursement type:- " + type
                + "<br>Applied Date :- " + date
                + "<br>Voucher number:- " + VoucherNo
                + "<br><br>ThankYou.<br><br>";

            emails = "payrollservices@sequelgroup.co.in";
            emailSend.SendEmailTest(emails, subjects, checkerbodys);
        }

        //public void SubmitBtn_OnClick(object sender, EventArgs e)
        //{
        //    string VoucherNo = "A1B2C3";
        //    string type = "testtype";
        //    SENDUDATEMAIL(VoucherNo,type);
        //}

        public void SubmitBtn_OnClick(object sender, EventArgs e)
        {
            try
            {
                string VoucherNo = "A1B2C3";
                string type = "testtype";
                SENDUDATEMAIL(VoucherNo, type);
            }
            catch (Exception ex)
            {
                lblmessage.Visible = true;
                lblmessage.Text = "Error: " + ex.Message;
            }
        }

    }
}