using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace NewPortal2023.ESS
{
    public partial class CandidateLoginPage : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Common objBl = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();
        NewPortal2023.ESS.OTP objOTP = new NewPortal2023.ESS.OTP();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValidateLogin() == false)
                {
                    return;
                }

               ds=  objOTP.Candidate_Login(txtEmpCode.Text, txtPassword.Text);
                if(ds.Tables.Count>0)
                {
                    //Response.Redirect("Default.aspx");
                    Response.Redirect("Default.aspx?sender=me&id=" + txtEmpCode.Text);
                }
                //if (objUser.EmpId != null)
                //{
                //    if (objUser.EmpId.ToString() != "")
                //    {
                //    }



                //}
                //}
                else
                {
                    lblMessage.Text = "Employee Code or Password invalid.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Employee does not exists.');");
                }
            }


            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private Boolean ValidateLogin()
        {
            //if (txtCompCode.Text.Trim() == "")
            //{
            //    lblMessage.Text = "Enter company code.";
            //    //objcommon.Display("Validate", "DisplayErrorMessage('Enter company code.');");
            //    return false;
            //}

            if (txtEmpCode.Text.Trim() == "")
            {
                lblMessage.Text = "Enter employee code.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Enter employee code');");
                return false;
            }
            if (txtPassword.Text.Trim() == "")
            {
                lblMessage.Text = "Enter password.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Enter Password.');");
                return false;
            }

            return true;

        }
    }
}