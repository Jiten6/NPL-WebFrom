using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class NewPassword : System.Web.UI.Page
    {

        NewPortal2023.ESS.Common objBl = new NewPortal2023.ESS.Common();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["COMC"] == null || Session["EMPC"] == null)
            {
                Response.Redirect("../ESS/Logout.aspx");
            }

        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtNewPass.Text.Trim() == "")
                {
                    lblMessage.Text = "Enter new password.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Enter new password');");
                    return;
                }
                if (txtNewPass.Text.Length < 8)
                {
                    lblMessage.Text = "Password must be atleast 8 characters long.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Old and confirm password does not match.');");
                    return;
                }
                if (txtNewPass.Text.Length > 15)
                {
                    lblMessage.Text = "Password must be less than 15 characters long.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Old and confirm password does not match.');");
                    return;
                }
                if (txtRepPass.Text.Trim() == "")
                {
                    lblMessage.Text = "Enter repeat password.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Enter confirm password.');");
                    return;
                }
                if (txtNewPass.Text.Trim() != txtRepPass.Text.Trim())
                {
                    lblMessage.Text = "New and repeat password do not match.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Old and confirm password does not match.');");
                    return;
                }

                if (txtNewPass.Text == txtRepPass.Text)
                {
                    if (objBl.Reset_Password(Session["COMC"].ToString().Trim(), Session["EMPC"].ToString().Trim(), txtNewPass.Text) == true)
                    {
                        Session["COMC"] = null;
                        Session["EMPC"] = null;
                        Response.Redirect("../ESS/Logout.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "Passwords do not match.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }


    }



    
}