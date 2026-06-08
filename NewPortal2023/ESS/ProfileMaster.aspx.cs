using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using System.Globalization;

namespace NewPortal2023.ESS
{
    public partial class ProfileMaster : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objCommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.User objUR = new NewPortal2023.ESS.User();
        NewPortal2023.ESS.OTP objOTP = new NewPortal2023.ESS.OTP();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();

        DataSet ds = new DataSet();
        DataSet dsInv = new DataSet();

        private string SourcePath = string.Empty;
        private string savePath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["sCompID"] != null)
                {
                    try
                    {
                        string strResult = objCommon.Validate_ControlInfo("INV");

                        if (strResult.Contains("This page is currently unavailable.") == true)
                        {
                            Response.Redirect("Unavailable.aspx?strName=Investment Details");
                            return;
                        }
                        FillProfile();


                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
                    }
                }
                else
                {
                    Response.Redirect("Logout.aspx");
                }
            }
        }

        private void FillProfile()
        {
            dsInv = objUR.GetProfile((string)Session["sCompID"], (string)Session["sEmpID"]);

            drpEmp.Items.Clear();
            drpEmp.DataTextField = "PROF_DESC";
            drpEmp.DataValueField = "PROF_ID";
            drpEmp.DataSource = dsInv;
            drpEmp.DataBind();
            drpEmp.Items.Insert(0, new ListItem("[Select One]", "0"));

        }
        private void FillLeave()
        {
            dsInv = objUR.GetLeaveDetails((string)Session["sCompID"], drpEmp.SelectedItem.Value);
            gv.DataSource = dsInv.Tables[0];
            // gv.DataSource = dsInv.Tables[1];
            gv.DataBind();
        }

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblMenuID = (Label)e.Row.FindControl("lblMenuID");
                CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");

                chkSelect.Checked = true;
                if (lblMenuID.Text == "0")
                {
                    chkSelect.Checked = false;
                }


            }
        }

        protected void drpEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillLeave();
        }

        private string MakeCTCXml(GridView gv)
        {
            StringBuilder sbTaxDetails = new StringBuilder();
            string xmlInv = string.Empty;

            sbTaxDetails.Append("<ROOT>");

            foreach (GridViewRow gvr in gv.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    sbTaxDetails.Append("<Rec CID='" + ((Label)gvr.FindControl("lblID")).Text.Trim() + "'/>");
                }
            }
            sbTaxDetails.Append("</ROOT>");

            xmlInv = sbTaxDetails.ToString();

            return xmlInv;
        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue == "Yes")
                {
                    if (Convert.ToString(drpEmp.SelectedItem.Value) == "0")
                    {
                        lblMessage.Text = "Select profile.";
                        return;
                    }
                    if (objUR.Update(MakeCTCXml(gv), (string)Session["sCompID"], drpEmp.SelectedItem.Value) == false)
                    {
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating Flexi Pay details.');");
                        return;
                    }
                    FillLeave();
                    lblMessage.Text = "Successfuly updated user rights.";
                    objCommon.SetMessageColor(divAlert, "success");

                    string message = lblMessage.Text;
                    string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }
                else
                {
                    
                }

            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();

                string message = ex.Message;
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);

                lblMessage.Text = ex.Message;
                objCommon.SetMessageColor(divAlert, "danger");
            }
        }
    }
}