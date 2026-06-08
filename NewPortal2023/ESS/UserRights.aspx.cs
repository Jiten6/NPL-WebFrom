using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace NewPortal2023.ESS
{
    public partial class UserRights : System.Web.UI.Page
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

                        mv.SetActiveView(vwList);
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
        private void FillEmp()
        {
            dsInv = objUR.GetEmpDetails((string)Session["sCompID"], drpEmp.SelectedItem.Value);
            gv.DataSource = dsInv.Tables[0];
            gv.DataBind();
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
        protected void lnkUpdate_Click(object sender, EventArgs e)
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
                    if (objUR.UpdateEmp(MakeCTCXml(gv), (string)Session["sCompID"], drpEmp.SelectedItem.Value, drpEmp.SelectedItem.Text, "") == false)
                    {
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating Flexi Pay details.');");
                        return;
                    }
                    FillEmp();

                    lblMessage.Text = "Successfuly updated user rights.";
                    objCommon.SetMessageColor(divAlert, "success");

                    string message = lblMessage.Text;
                    string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                }
                else
                {
                    //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('You clicked NO!')", true);
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

        protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                //if (e.Row.RowType == DataControlRowType.Header)
                //{
                //    ((CheckBox)e.Row.FindControl("chkSelectAll")).Attributes.Add("onclick",
                //        "javascript:SelectAll('" +
                //        ((CheckBox)e.Row.FindControl("chkSelectAll")).ClientID + "')");
                //}

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

                string var = "Alternate, Edit";
                string var1 = e.Row.RowState.ToString();
                if (var == var1)
                {
                    DropDownList drpactiontype = e.Row.FindControl("drpAction") as DropDownList;
                    drpactiontype.Enabled = true;

                    TextBox txtRemarks = e.Row.FindControl("txtRemarks") as TextBox;
                    txtRemarks.Enabled = true;

                }
                if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Edit)
                {
                    //TextBox txtCountry = e.Row.FindControl("txtCountry") as TextBox;
                    //txtCountry.Enabled = true;
                    DropDownList drpactiontype = e.Row.FindControl("drpAction") as DropDownList;
                    drpactiontype.Enabled = true;

                    TextBox txtRemarks = e.Row.FindControl("txtRemarks") as TextBox;
                    txtRemarks.Enabled = true;
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
        protected void drpEmp_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillEmp();
        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chckheader = (CheckBox)gv.HeaderRow.FindControl("chkSelectAll");
                foreach (GridViewRow row in gv.Rows)
                {
                    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    if (chckheader.Checked == true)
                    {
                        chckrw.Checked = true;

                    }
                    else
                    {
                        chckrw.Checked = false;
                    }

                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void gvDataPointList_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;

            if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                || (gv.ShowHeaderWhenEmpty == true))
            {
                //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (gv.ShowFooter == true && gv.Rows.Count > 0)
            {
                //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                gv.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void gvDataPointList_RowEditing(object sender, GridViewEditEventArgs e)
        {
            //gvDataPointList.EditIndex = e.NewEditIndex;
            //this.Fill_Details("1", gvDataPointList.PageSize.ToString());
        }

        protected void gvDataPointList_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

        }

        protected void gvDataPointList_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            //gvDataPointList.EditIndex = -1;
            //this.Fill_Details("1", gvDataPointList.PageSize.ToString());
        }
    }
}