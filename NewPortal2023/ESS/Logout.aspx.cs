using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {


            string confirmScript = "if(confirm('Are you sure you want to Log-out ?')) { window.location.href = 'Login.aspx'; }";
            ClientScript.RegisterStartupScript(this.GetType(), "confirmRedirect", confirmScript, true);
            //Response.Redirect("Login.aspx");
        }
    }
}