using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;

namespace NewPortal2023.ESS
{
    public partial class GenerateUploadRosterRptWise : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveUpload objInv = new NewPortal2023.ESS.LeaveUpload();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        DataSet dsInv = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["sCompID"] != null)
                {
                    try
                    {
                        string strResult = objcommon.Validate_ControlInfo("INV");

                        if (strResult.Contains("This page is currently unavailable.") == true)
                        {
                            Response.Redirect("Unavailable.aspx?strName=Investment Details");
                            return;
                        }
                        FillRoster();
                    }
                    catch (Exception ex)
                    {
                        divAlertDan.Visible = true;
                        lblMessageDan.Text = "Error occurred in application.";
                    }
                }
                else
                {
                    Response.Redirect("Logout.aspx");
                }
            }
            
        }
        private void FillRoster()
        {
            try
            {

                objNps.EmpCode = Session["sEmpID"].ToString();
                //objNps.EmpName = Session["sEmpName"].ToString();
                dsInv = objNps.GetRosterReportWiseTillDate((string)Session["sCompID"]);
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    gvRoster.DataSource = dsInv;
                    gvRoster.DataBind();
                    //gvList.Visible = true;
                    //CalCulation();
                    divAlert.Visible = true;
                    lblMessage.Text = "Shift Roster Report Generated Successfuly.";
                    divAlertDan.Visible = false;
                }


                else
                {
                    gvRoster.DataSource = null;
                    gvRoster.DataBind();
                    //gvList.Visible = false;
                    divAlertDan.Visible = true;
                    lblMessageDan.Text = "Please Select From Date /To Date First Then Click Generate Button";
                    divAlert.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }


        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDate.Text == "")
                {
                    divAlertDan.Visible = true;
                    lblMessageDan.Text = "Select from date";
                    divAlert.Visible = false;
                    return;
                }
                if (txtDateTo.Text == "")
                {
                    divAlertDan.Visible = true;
                    lblMessageDan.Text = "Select to date";
                    divAlert.Visible = false;
                    return;
                }

                //if (Convert.ToDateTime(txtDate.Text) > Convert.ToDateTime(txtDateTo.Text))
                //{
                //    lblMessage.Text = "From date cannot be greater that to date";
                //    return;
                //}
                FillDetails();
                lblMessage.Text = "Shift Roster Report Generated SuccessfulLy.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void FillDetails()
        {
            try
            {
                if (txtDate.Text != "" && txtDateTo.Text != "")
                {
                    objNps.FromDate = txtDate.Text;
                    objNps.ToDate = txtDateTo.Text;
                    objNps.EmpCode = Session["sEmpID"].ToString();
                    //objNps.EmpName = Session["sEmpName"].ToString();
                    dsInv = objNps.GetRosterReportWise((string)Session["sCompID"]);
                    //if (Convert.ToString(dsInv.Tables[1].Rows[0]["result"]) != "")
                    //{
                    //    lblMessage.Text = Convert.ToString(dsInv.Tables[0].Rows[0]["result"]);
                    //    gvRoster.DataSource = null;
                    //    gvRoster.DataBind();
                    //    //gvRoster.Visible = false;
                    //}
                    //else if (Convert.ToString(dsInv.Tables[1].Rows[0]["result"]) == "")
                    //{
                    gvRoster.DataSource = dsInv;
                    gvRoster.DataBind();
                    //gvList.Visible = true;
                    //CalCulation();
                    divAlert.Visible = true;
                    lblMessage.Text = "Shift Roster Report Generated SuccessfulLy.";
                    divAlertDan.Visible = false;
                    //}

                }
                else
                {
                    gvRoster.DataSource = null;
                    gvRoster.DataBind();
                    //gvList.Visible = false;
                    divAlertDan.Visible = true;
                    lblMessageDan.Text = "Please Select From Date /To Date First Then Click Generate Button";
                    divAlert.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }


        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            // Confirms that an HtmlForm control is rendered for the 
            //specified ASP.NET server control at run time. 
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvRoster != null)
            {
                if (gvRoster.Rows.Count > 0)
                {
                    ExportGridView(gvRoster);
                }
                else
                {
                    divAlertDan.Visible = true;
                    lblMessageDan.Text = "Generate report first.";
                    divAlert.Visible = false;
                }
            }
        }
        private void ExportGridView(GridView gvFiles)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Charset = "";
            string FileName = "Shift Roster Report Employee Wise " + DateTime.Now + ".xls";
            StringWriter strwritter = new StringWriter();
            HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            gvFiles.GridLines = GridLines.Both;
            gvFiles.HeaderStyle.Font.Bold = true;
            gvFiles.RenderControl(htmltextwrtter);
            Response.Write(strwritter.ToString());
            Response.End();

        }
    }
}