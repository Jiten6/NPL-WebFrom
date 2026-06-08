using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
          
            string COMP = (string)Session["sCompID"];

            lblMessage.Text = COMP;
            if (Request.QueryString["id"] != null)
            {
                ID = Convert.ToString(Request.QueryString["id"]);
                Response.Redirect("CandidatePersonalDetails.aspx?sender=me&id=" + ID);
                
            }
            else
                {
                if ((string)Session["sCompID"] == "CO000076")
                {
                    Response.Redirect("../ESS/NipponDashboard.aspx");
                }
                else
                {
                    Response.Redirect("NPLDashboard.aspx");
                }

            }
        }
    }
     
}
     