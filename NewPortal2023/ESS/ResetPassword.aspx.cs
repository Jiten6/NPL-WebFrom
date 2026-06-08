using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using System.Net.Mail;

namespace NewPortal2023.ESS
{
    

    public partial class ResetPassword : System.Web.UI.Page



    {
        NewPortal2023.ESS.Common objBl = new NewPortal2023.ESS.Common();
        //ESS.Common objBl = new ESS.Common();
        DataSet dsMail = new DataSet();
        DataSet dsPassword = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //protected void btnVerify_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        bool isVerified = false;


        //        if (!ValidateLogin())
        //        {
        //            lblMessage.Visible = true;
        //            return;
        //        }

        //        objBl.Verify_User_V2(txtCompCode.Text.Trim(), txtEmpCode.Text.Trim(), txtPAN.Text.Trim(), txtJoiningDate.Text.Trim(), out isVerified);

        //        if (isVerified)
        //        {

        //            Session["COMC"] = txtCompCode.Text.Trim();
        //            Session["EMPC"] = txtEmpCode.Text.Trim();
        //            //  Response.Redirect("../ESS/ChangePassword.aspx", false);
        //            Response.Redirect("../ESS/NewPassword.aspx", false);

        //        }
        //        else
        //        {
        //            lblMessage.Text = "Employee does not exist.";
        //            lblMessage.Visible = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Error occurred in application: ";
        //       // lblMessage.Visible = true;

        //    }
        //}

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            try
            {
                NewPortal2023.ESS.Common objBl = new NewPortal2023.ESS.Common();
                string email = "";
                string password = "";

                if (txtCompCode.Text.Trim().ToUpper() != "ASUS")
                {
                    bool isVerified = false;
                    if (ValidateLogin() == false)
                    {
                        return;
                    }

                    objBl.Verify_User_V2(txtCompCode.Text.Trim(), txtEmpCode.Text.Trim(), txtPAN.Text.Trim(), txtJoiningDate.Text.Trim(), out isVerified);

                    if (isVerified)
                    {
                        dsMail = objBl.GetEmial(txtEmpCode.Text.Trim(), txtCompCode.Text.Trim());
                        if (dsMail.Tables[0].Rows[0]["EMAIL_ID"].ToString() != "")
                        {
                            Random rnd = new Random();
                            password = System.Web.Security.Membership.GeneratePassword(8, 0);
                            password = System.Text.RegularExpressions.Regex.Replace(password, @"[^a-zA-Z0-9]", m => rnd.Next(0, 9).ToString());


                            dsPassword = objBl.PasswordSaved(txtEmpCode.Text.Trim(), txtCompCode.Text.Trim(), password);

                            if (dsPassword.Tables[0].Rows[0]["result"].ToString() == "")
                            {
                                email = dsMail.Tables[0].Rows[0]["EMAIL_ID"].ToString();
                                MailMessage mailMessage = new MailMessage();
                                mailMessage.To.Add(email);
                                //mailMessage.To.Add("techsupport@sequelgroup.co.in");
                                //mailMessage.CC.Add("reports@sequelgroup.co.in");


                                mailMessage.From = new MailAddress("reports@sequelgroup.co.in");
                                mailMessage.Subject = "Reset Password - ESS Portal";

                                mailMessage.Body = "Hi,\n\nThe system has successfully reset your ESS Portal password. Please find below with New Password.\nRequesting you to kindly change the password after successful login.. \n\nNew Password: " + password + "\n\nThank you!\n\nRegards";


                                SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
                                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                                smtpClient.UseDefaultCredentials = false;
                                smtpClient.Credentials = new System.Net.NetworkCredential("reports@sequelgroup.co.in", "sequel@123");
                                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                                smtpClient.EnableSsl = true;
                                smtpClient.Send(mailMessage);
                                mailMessage.Dispose();
                                smtpClient.Dispose();

                                Session["PasswordMsg"] = "New Password has been successfully reset and send on your registered Mail ID.";

                                //lblMessage.Text = "New Password has been successfully reset and send on your registered Mail ID.";
                                //objBl.Display("Validate", "New Password has been successfully generated and send on your registered Mail ID.");
                                Response.Redirect("../ESS/login.aspx", true);
                            }
                            else
                            {
                                lblMessage.Text = "Again Click on Verify button.";
                            }

                            //SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);


                            ////smtpClient.EnableSsl = true;
                            //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            //smtpClient.UseDefaultCredentials = false;

                            //smtpClient.Credentials = new System.Net.NetworkCredential("devseal@devseal.co.in", "qidm cbpi tmuv hggd");
                            //System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                            //smtpClient.EnableSsl = true;
                            //smtpClient.Send(mailMessage);

                        }
                        else
                        {
                            lblMessage.Text = "We could not process your request because the email ID you provided is not registered in the application.To update your email ID, please contact the Payroll Department.";
                        }




                        //Response.Redirect("../ESS/NewPassword.aspx", false);
                    }
                    else
                    {
                        lblMessage.Text = "Employee does not exists.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Employee does not exists.');");
                    }
                }
                else
                {
                    lblMessage.Text = "Not allowed.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }


        private Boolean ValidateLogin()
        {
            if (txtCompCode.Text.Trim() == "")
            {
                lblMessage.Text = "Enter company code.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Enter company code.');");
                lblMessage.Visible = true;
                return false;
            }
            if (txtEmpCode.Text.Trim() == "")
            {
                lblMessage.Text = "Enter employee code.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Enter employee code');");
                lblMessage.Visible = true;
                return false;
            }
            if (txtPAN.Text.Trim() == "" && txtJoiningDate.Text.Trim() == "")
            {
                lblMessage.Text = "Enter PAN or Joining Date.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Enter PAN.');");
                lblMessage.Visible = true;
                return false;
            }
            return true;

        }

    }


}