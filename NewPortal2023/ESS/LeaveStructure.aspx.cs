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
    public partial class Leave_LeaveStructure : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveStructure objInv = new NewPortal2023.ESS.LeaveStructure();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsInv = new DataSet();
        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
             if (Session["sCompID"]!=null)
            {
            if (!Page.IsPostBack)
            {
                if (Session["sCompID"] != null)
                {
                    ds = objInv.GetRoleDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
                    if (ds.Tables[0].Rows[0]["Counts"].ToString() == "0")
                    {
                        OtherDetails.Visible = true;
                        btnDtails.Visible = false;

                        FillLeave();
                    }
                    else
                    {

                        OtherDetails.Visible = false;
                        btnDtails.Visible = true;
                    }
                    try
                    {
                        string strResult = objcommon.Validate_ControlInfo("INV");

                        if (strResult.Contains("This page is currently unavailable.") == true)
                        {
                            Response.Redirect("Unavailable.aspx?strName=Investment Details");
                            return;
                        }

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
             else
             {
                 Response.Redirect("Login.aspx");
             }

        }

        private void FillLeave()
        {
            string type = "";
            if (Session["MessageExec"] != null)
            {
                type = Session["MessageExec"].ToString();
                dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], type);
                gvLeave.DataSource = dsInv;
                gvLeave.DataBind();
                OtherDetails.Visible = true;
                lnkUpdate.Visible = true;
            }
            else if (Session["MessageWorkMen"] != null)
            {
                type = Session["MessageWorkMen"].ToString();
                Session["MessageExec"] = "";
                dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], type);
                gvLeave.DataSource = dsInv;
                gvLeave.DataBind();
                lnkUpdate.Visible = false;
                OtherDetails.Visible = true;
            }
            else
            {
                type = "";
                dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"], type);
                gvLeave.DataSource = dsInv;
                gvLeave.DataBind();
                OtherDetails.Visible = true;
            }
        }



        private string MakeInvXml(GridView GV, string strType)
        {

            StringBuilder sbTaxDetails = new StringBuilder();
            string xmlInv = string.Empty;

            sbTaxDetails.Append("<ROOT>");

            foreach (GridViewRow gvr in GV.Rows)
            {
                sbTaxDetails.Append("<Rec CID='" + ReplaceSpecialCharacters(((Label)gvr.FindControl("lblId")).Text.Trim()) + "'");
                sbTaxDetails.Append(" ALLOCATED='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtAll")).Text.Trim()) + "'");
                sbTaxDetails.Append(" ISCARRY='" + (((CheckBox)gvr.FindControl("chkSel")).Checked) + "'/>");
            }

            sbTaxDetails.Append("</ROOT>");

            xmlInv = sbTaxDetails.ToString();

            return xmlInv;


        }

        protected string ReplaceSpecialCharacters(string inputString)
        {
            inputString = inputString.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&#39;").
                    Replace("\"", "&quot;");

            //Hashtable ht = new Hashtable();

            //for (int i = 0; i <= inputString.Length - 1; i++)
            //{
            //    int ascii = Convert.ToChar(inputString.Substring(i, 1));

            //    if (ascii > 125)
            //    {
            //        if (!ht.ContainsValue(ascii))
            //        {
            //            ht.Add(Convert.ToChar(inputString.Substring(i, 1)), ascii);
            //        }
            //    }
            //}

            //foreach (DictionaryEntry entry in ht)
            //{
            //    inputString = inputString.Replace(Convert.ToString(entry.Key), "~" + Convert.ToInt32(entry.Value) + "$");
            //}

            return inputString;
        }

        protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                string type = Session["MessageExec"].ToString();
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkSel = (CheckBox)e.Row.FindControl("chkSel");
                    Label lblCheck = (Label)e.Row.FindControl("lblCheck");
                    chkSel.Checked = (lblCheck.Text.Trim() == "True" ? true : false);
                    TextBox txtAll = (TextBox)e.Row.FindControl("txtAll");
                    if (type == "Exec")
                    {
                        txtAll.Enabled = true;
                        chkSel.Enabled = true;

                    }
                    else
                    {
                        txtAll.Enabled = false;
                        chkSel.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }

        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                result = objInv.UpdateLeave(MakeInvXml(gvLeave, "Leave"), "", "", (string)Session["sCompID"], (string)Session["sEmpID"]);
                if (result.ToString().Trim() == "success")
                {
                    FillLeave();
                    lblMessage.Text = "Successfuly updated leave structure.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated leave structure.');");
                }
                else
                {
                    lblMessage.Text = "Error occurred in application.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
            }
        }

        protected void btnExec_Click(object sender, EventArgs e)
        {
            try
            {

                Session["MessageExec"] = "Exec";
                Session["MessageWorkMen"] = null;
                FillLeave();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnWorkMen_Click(object sender, EventArgs e)
        {
            try
            {

                Session["MessageExec"] = null;
                Session["MessageWorkMen"] = "WorkMen";
                FillLeave();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}