using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using Microsoft.Reporting.WebForms;

namespace NewPortal2023.ESS
{
    public partial class ReportingOfficersUpdation : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();

        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        DataSet ds = null;
        protected void Page_Load(object sender, EventArgs e)
        {
             if (Session["sCompID"]!=null)
            {
            if (!Page.IsPostBack)
            {
                if (Session["sCompID"] != null)
                {

                }
                tblRo.Visible = false;
                tblEmp.Visible = false;
                fillEmployeeDropdown();
            }

        }
             else
             {
                 Response.Redirect("Login.aspx");
             }
        }

        private void FillgvReportingList()
        {
            try
            {
                ds = objNps.GetRODetails((string)Session["sCompID"]);
                gvListReportingOfficer.DataSource = ds.Tables[0];
                gvListReportingOfficer.DataBind();
                tblRo.Visible = true;
                tblEmp.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }



        protected void lnkbutonListRO(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnInvestSup = (LinkButton)sender;
                Label lblEmpMId = (Label)lnkBtnInvestSup.NamingContainer.FindControl("lblEmpId");
                string EMP_MId = lblEmpMId.Text;
                ds = objNps.GetROEmpDetails((string)Session["sCompID"], EMP_MId);
                gvEmplist.DataSource = ds.Tables[0];
                gvEmplist.DataBind();
                tblRo.Visible = false;
                tblEmp.Visible = true;
                tblup.Visible = false;
                lblMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void btnCheckOnClcik(object sender, EventArgs e)
        {
            try
            {
                FillgvReportingList();
                tblup.Visible = false;
                tblRo.Visible = true;
                tblEmp.Visible = false;
                tr.Visible = true;
                lblMessage.Text = "";
                lblReportingCode.Text = "";
                //txtsrEmpCode.Text = "";
                drpEmpType.SelectedValue = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void btnUpdateOptionOnClcik(object sender, EventArgs e)
        {
            try
            {
                tblup.Visible = true;
                tblRo.Visible = false;
                tblEmp.Visible = false;
                tr.Visible = false;
                lblMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void btnUpdateOnClcik(object sender, EventArgs e)
        {
            try
            {
                ds = objNps.UpdateReportingofficersDetails((string)Session["sCompID"], txtEmpCode.Text, txtRoCode.Text);

                tblup.Visible = true;
                tblRo.Visible = false;
                tblEmp.Visible = false;
                tr.Visible = false;
                txtRoCode.Text = "";
                txtEmpCode.Text = "";
                lblmsg.Text = "Reporting officer Update Successfully";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnSearchOnClcik(object sender, EventArgs e)
        {
            string EMP_NAME;
            string RP_NAME;
            try
            {
                //ds = objNps.GetReportingofficersDetails((string)Session["sCompID"], txtsrEmpCode.Text);
                ds = objNps.GetReportingofficersDetails((string)Session["sCompID"], drpEmpType.SelectedValue);
              

                if (ds.Tables[0].Rows.Count > 0)
                {
                    EMP_NAME = ds.Tables[0].Rows[0]["EMP_NAME"].ToString();
                }
                else
                {
                    EMP_NAME = "NOT ASSIGNED";
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    RP_NAME = ds.Tables[1].Rows[0]["RP_NAME"].ToString();
                }
                else
                {
                    RP_NAME = "TILL NOW NOT ASSIGNED";
                }
                tblup.Visible = false;
                tblRo.Visible = false;
                tblEmp.Visible = false;

                lblReportingCode.Text = " EMPLOYEE NAME IS :- " + EMP_NAME + " AND REPORTING OFFICER NAME IS :- " + RP_NAME;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "showModal", "showModal();", true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void txtEmpCodeOnTextChanged(object sender, EventArgs e)
        {
            string EMP_NAME;

            try
            {
                ds = objNps.GetEmpAndROName((string)Session["sCompID"], txtEmpCode.Text);
                if (ds.Tables[2].Rows.Count > 0)
                {
                    EMP_NAME = ds.Tables[2].Rows[0]["EMP_NAME"].ToString();
                    lblEmpN.Text = "EMPLOYEE NAME IS :- " + EMP_NAME;
                }
                else
                {
                    EMP_NAME = "NOT ASSIGNED";
                    lblEmpN.Text = "EMPLOYEE NAME IS :- " + EMP_NAME;
                }


            }
            catch
            {
            }
        }
        protected void txtRoCodeOnTextChanged(object sender, EventArgs e)
        {
            string RP_NAME;
            try
            {
                ds = objNps.GetEmpAndROName((string)Session["sCompID"], txtRoCode.Text);
                if (ds.Tables[3].Rows.Count > 0)
                {
                    RP_NAME = ds.Tables[3].Rows[0]["RP_NAME"].ToString();
                    lblRON.Text = "EMPLOYEE NAME IS :-  " + RP_NAME;
                }
                else
                {
                    RP_NAME = "TILL NOW NOT ASSIGNED";
                    lblRON.Text = "REPORTING OFFICER NAME IS :-  " + RP_NAME;
                }
            }
            catch
            {
            }

        }

        protected void drpEmpType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void fillEmployeeDropdown()
        {
            objExp.fillEmployeeMid(drpEmpType);
        }
    }
}