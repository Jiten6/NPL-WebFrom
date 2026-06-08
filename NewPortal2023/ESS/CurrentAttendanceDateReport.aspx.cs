using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Configuration;
using System.Data;
using System.Globalization;
using ClosedXML.Excel;
using System.Text;
using Microsoft.Reporting.WebForms;

namespace NewPortal2023.ESS
{
    public partial class CurrentAttendanceDateReport : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveUpload objInv = new NewPortal2023.ESS.LeaveUpload();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.Loc objLocId = new NewPortal2023.ESS.Loc();
        NewPortal2023.ESS.Loc locations = new NewPortal2023.ESS.Loc();

        DataSet dsInv = new DataSet();
        DataSet dsInvS = new DataSet();
        int count = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["sCompID"] != null)
            {
            if(!Page.IsPostBack)
            {
                FillLeave();
                FillSummary();
                //GetCurrentAttendance();
                
            }
        }
             else
            {
                Response.Redirect("Login.aspx");
            }

        }

        private void FillSummary()
        {

            dsInv = objNps.GetCurrentdateAttendanceSummary((string)Session["sCompID"]);
            txtExecutive.Text = "";
            txtWorkMen.Text = "";
            DataSet dsInv1 = new DataSet();
            dsInv1 = objNps.GetInsertAbsentAttendanceSummary((string)Session["sCompID"]);
           // dsInv = objNps.GetCurrentdateAbsentAttendanceSummary((string)Session["sCompID"]);


            if (dsInv.Tables.Count > 0)
            {
                txtExecutive.Text = dsInv.Tables[0].Rows[0]["Executive"].ToString();
                txtWorkMen.Text = dsInv.Tables[1].Rows[0]["WorkMen"].ToString();
            }

        }

        private void GetCurrentAttendance()
        {
            dsInv = objNps.GetCurrentdateAttendanceStatus((string)Session["sCompID"], (string)Session["sEmpID"]);

            if (dsInv.Tables.Count > 0)
            {
                //rptPrint.Visible = true;
                //ReportDataSource datasource1 = new ReportDataSource("Dsattendanced", dsInv.Tables[0]);

                //rptPrint.LocalReport.DataSources.Clear();

                //rptPrint.LocalReport.ReportPath = @"Reports/attendancedetailsReport.rdlc";
                //rptPrint.LocalReport.DisplayName = "Today_Attendance_Report";
                //rptPrint.LocalReport.DataSources.Add(datasource1);


                //rptPrint.LocalReport.Refresh();

                divAlert.Visible = true;
                string script = $@"<script type='text/javascript'>alert('Attendance Details Report Generated Successfully.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                lblMessage.Text = "Attendance Details Report Generated Successfully.";
                divAlert.Visible = false;
            }
            else
            {
               
            }

        }
        private void FillLeave()
        {
            //drpDepartment.SelectedIndex = -1;
            //drpLocation.SelectedIndex = -1;
            objNps.Status = "";// drpType.SelectedValue;
            objNps.EmpName = "";
            //if (rbPresent.Checked == true)
            //{
                dsInv = objNps.GetCurrentdateAttendanceStatus((string)Session["sCompID"], (string)Session["sEmpID"]);
                if (dsInv.Tables.Count > 0)
                {
                    gvLeave.DataSource = dsInv;
                    gvLeave.DataBind();
                    //tdDepartment.Visible = false;
                    //tdLocation.Visible = false;
                }
                else
                {
                    gvLeave.DataSource = null;
                    gvLeave.DataBind();
                    //tdDepartment.Visible = false;
                    //tdLocation.Visible = false;
                }
            //}
            //else if (rbAbsent.Checked == true)
            //{
            //    //dsInv = objNps.GetCurrentdateaAbsentAttendanceStatus((string)Session["sCompID"], (string)Session["sEmpID"]);
            //    lblMessage.Text = "Temporary unavailable this filter";
            //}

            //dsInv = objNps.GetCurrentdateAttendance((string)Session["sCompID"], (string)Session["sEmpID"]);
            //gvLeave.DataSource = dsInv;
            //gvLeave.DataBind();
        }
        protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.BackColor = System.Drawing.Color.White;



            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                //if (rbAbsent.Checked == true)
                //{
                //    e.Row.Cells[2].Text = "DATE";
                //}
            }
        }
    }
}