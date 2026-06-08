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
    public partial class ApprovedORRejectOTAndCOReport : System.Web.UI.Page
    {
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsInv = new DataSet();
        DataSet dsOTAndCO = new DataSet();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session["sCompID"] != null)
                {
                    if (!Page.IsPostBack)
                    {


                    }

                }
                else
                {
                    Response.Redirect("Login.aspx");
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
                Generated();
            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void Generated()
        {
            try
            {
                if (txtDate.Text != "" && txtDateTo.Text != "")
                {

                    objNps.FromDate = txtDate.Text;
                    objNps.ToDate = txtDateTo.Text;
                    if (drpReportsType.SelectedItem.Value != " ")
                    {
                        if (drpReportsType.SelectedItem.Value == "EmployeeWise")
                        {
                            objNps.EmpCode = txtEmpCode.Text;
                        }
                        else
                        {
                            objNps.EmpCode = " ";
                        }
                        objNps.EmpName = " ";
                        if (drpReportsType.SelectedItem.Value == "EmployeeWise")
                        {
                            ds = objNps.GenerateOTAndCOReportRPTFlag((string)Session["sCompID"], "RPT");
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                lblMessage.Text = "OT And CO Report Generated Successfuly.";
                                gvLeave.DataSource = ds.Tables[0];
                                gvLeave.DataBind();
                                trOTCO.Visible = true;

                            }

                            else
                            {
                                gvLeave.DataSource = null;
                                gvLeave.DataBind();
                                lblMessage.Text = "Not Found Records.";
                                trOTCO.Visible = false;
                            }
                            //}

                        }
                        else
                        {

                            ds = objNps.GenerateOTAndCOReportRPTFlagAll((string)Session["sCompID"], (string)Session["sEmpID"], "RPT");
                            if (ds.Tables[0].Rows.Count > 0)
                            {

                                lblMessage.Text = "OT And CO Report Generated Successfuly.";
                                gvLeave.DataSource = ds.Tables[0];
                                gvLeave.DataBind();
                                trOTCO.Visible = true;
                            }

                            else
                            {
                                gvLeave.DataSource = null;
                                gvLeave.DataBind();
                                lblMessage.Text = "Not Found Records.";
                                trOTCO.Visible = false;
                            }
                        }

                    }
                    else
                    {
                        lblMessage.Text = "Please Select Search by Option ALL/EmployeeWise.";
                    }

                }
                else
                {
                    gvLeave.DataSource = null;
                    gvLeave.DataBind();

                    lblMessage.Text = "Please Select From Date /To Date First Then Click Generate Button";
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
            if (gvLeave != null)
            {
                if (gvLeave.Rows.Count > 0)
                {
                    ExportGridView(gvLeave);
                }
                else
                {
                    lblMessage.Text = "Generate report first.";
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
            string FileName = "OT And CO Reports " + txtEmpCode.Text + " " + DateTime.Now + ".xls";
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

        protected void drpReportsType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((drpReportsType.SelectedItem.Text == "EmployeeWise"))
            {
                tdEmpCode.Visible = true;
            }
            else if ((drpReportsType.SelectedItem.Text == " "))
            {
                tdEmpCode.Visible = false;
                drpOtCOType.SelectedIndex = -1;
            }
            else
            {
                tdEmpCode.Visible = false;
            }
        }
        protected void drpOtCOType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (drpOtCOType.SelectedValue == " ")
            //{
            //drpSelectType.Visible = false;
            //tdEmpCode.Visible = false;
            //drpSelectType.SelectedIndex = -1;
            if (drpOtCOType.SelectedValue == "OT")
            {
                if ((drpReportsType.SelectedItem.Text == "EmployeeWise"))
                {
                    if (txtEmpCode.Text == "")
                    {
                        lblMessage.Text = "Please Enter Employee Code.";
                    }
                    else
                    {
                        string type = "OT";
                        getEmpWiseOTCO(type);
                        drpOtCOType.Visible = true;
                    }
                }
                else if ((drpReportsType.SelectedItem.Text == "All"))
                {
                    string type = "OT";
                    getAllEmpOTCO(type);
                    drpOtCOType.Visible = true;
                }
            }
            else if (drpOtCOType.SelectedValue == "CO")
            {
                if ((drpReportsType.SelectedItem.Text == "EmployeeWise"))
                {
                    if (txtEmpCode.Text == "")
                    {
                        lblMessage.Text = "Please Enter Employee Code.";
                    }
                    else
                    {
                        string type = "CO";
                        getEmpWiseOTCO(type);
                        drpOtCOType.Visible = true;
                    }
                }
                else if ((drpReportsType.SelectedItem.Text == "All"))
                {
                    string type = "CO";
                    getAllEmpOTCO(type);
                    drpOtCOType.Visible = true;
                }
            }

            //}
            else
            {
                drpSelectType.Visible = false;
                tdEmpCode.Visible = true;
            }
        }

        private void getAllEmpOTCO(string type)
        {
            if (txtDate.Text != "" && txtDateTo.Text != "")
            {

                objNps.FromDate = txtDate.Text;
                objNps.ToDate = txtDateTo.Text;
                ds = objNps.GenerateOTAndCOReportRPTFlagAll((string)Session["sCompID"], (string)Session["sEmpID"], type);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    lblMessage.Text = "OT And CO Report Generated Successfuly.";
                    gvLeave.DataSource = ds.Tables[0];
                    gvLeave.DataBind();
                    trOTCO.Visible = true;
                }

                else
                {
                    gvLeave.DataSource = null;
                    gvLeave.DataBind();
                    lblMessage.Text = "Not Found Records.";
                    trOTCO.Visible = false;
                }
            }
            else
            {
                gvLeave.DataSource = null;
                gvLeave.DataBind();

                lblMessage.Text = "Please Select From Date /To Date First";
            }
        }

        private void getEmpWiseOTCO(string Type)
        {
            if (txtDate.Text != "" && txtDateTo.Text != "")
            {

                objNps.FromDate = txtDate.Text;
                objNps.ToDate = txtDateTo.Text;
                objNps.EmpCode = txtEmpCode.Text;
                objNps.EmpName = " ";
                ds = objNps.GenerateOTAndCOReportRPTFlag((string)Session["sCompID"], Type);
                if (ds.Tables[0].Rows.Count > 0)
                {

                    lblMessage.Text = "OT And CO Report Generated Successfuly.";
                    gvLeave.DataSource = ds.Tables[0];
                    gvLeave.DataBind();
                    drpOtCOType.Visible = true;

                }

                else
                {
                    gvLeave.DataSource = null;
                    gvLeave.DataBind();
                    lblMessage.Text = "Not Found Records.";
                    drpOtCOType.Visible = false;
                }
                //}

            }
            else
            {
                gvLeave.DataSource = null;
                gvLeave.DataBind();

                lblMessage.Text = "Please Select From Date /To Date First";
            }
        }

        protected void drpSelectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpSelectType.SelectedValue == " ")
            {
                drpReportsType.Visible = false;
                tdEmpCode.Visible = false;
            }
            else
            {
                drpReportsType.Visible = true;
                tdEmpCode.Visible = true;
            }
        }
    }
}