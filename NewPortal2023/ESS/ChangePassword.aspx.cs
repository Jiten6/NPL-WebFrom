using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace NewPortal2023.ESS
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Common objBl = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string strResult = string.Empty;

            try
            {
                if (ValidateLogin() == false)
                {
                    return;
                }

                strResult = objBl.Validate_Password(txtOldPass.Text, txtNewPass.Text, Convert.ToString(Session["sCompID"]), Convert.ToString(Session["sEmpID"]));
                lblMessage.Text = "Password successfuly changed.";
                //objcommon.Display("Validate", "DisplayErrorMessage('" + strResult  + "');");

                if (strResult.Contains("Password successfuly changed.") == true)
                {
                    Response.Redirect("../ESS/Logout.aspx");

                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private Boolean ValidateLogin()
        {
            if (txtOldPass.Text.Trim() == "")
            {
                lblMessage.Text = "Enter old password.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Enter old password.');");
                return false;
            }
            if (txtNewPass.Text.Trim() == "")
            {
                lblMessage.Text = "Enter new password.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Enter new password');");
                return false;
            }
            if (txtNewPass.Text.Length < 8)
            {
                lblMessage.Text = "Password must be atleast 8 characters long.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Old and confirm password does not match.');");
                return false;
            }
            if (txtNewPass.Text.Length > 15)
            {
                lblMessage.Text = "Password must be less than 15 characters long.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Old and confirm password does not match.');");
                return false;
            }
            if (txtConfirmPass.Text.Trim() == "")
            {
                lblMessage.Text = "Enter confirm password.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Enter confirm password.');");
                return false;
            }

            if (txtNewPass.Text.Trim() == txtOldPass.Text.Trim())
            {
                lblMessage.Text = "Old and new password can not be same.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Old and new password can not be same.');");
                return false;
            }

            if (txtNewPass.Text.Trim() != txtConfirmPass.Text.Trim())
            {
                lblMessage.Text = "New and confirm password do not match.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Old and confirm password does not match.');");
                return false;
            }

            return true;

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("../ESS/Default.aspx");
        }
    }
}