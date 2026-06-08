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
using System.Collections;
using Microsoft.Reporting.WebForms;

namespace NewPortal2023.ESS
{
    public partial class AttendanceSummeryReport : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveApplication objInv = new NewPortal2023.ESS.LeaveApplication();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsInv = new DataSet();
        DataSet ds = new DataSet();
        string message;
        int Count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    gvList.Visible = true;
                    trViewList.Visible = true;
                    DivgvMultipleList.Visible = true;

                }
            }

            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }
        //private void FillStatus(string Status)
        //{
        //    ds = objInv.GetStatus(Status);
        //    drpStatus.Items.Clear();
        //    drpStatus.DataTextField = "status";
        //    drpStatus.DataValueField = "id";
        //    drpStatus.DataSource = ds;
        //    drpStatus.DataBind();
        //    drpStatus.Items.Insert(0, new ListItem("[Select One]", "-"));
        //}



        private void CalCulation()
        {
            try
            {
                foreach (GridViewRow Row in gvList.Rows)
                {
                    if (Row.RowType == DataControlRowType.DataRow)
                    {
                        TimeSpan IN_TIME;
                        TimeSpan OUT_TIME;
                        int index = Row.RowIndex;
                        if (index == 0)
                        {
                            Count = 0;
                        }
                        Label lblStat = (Label)Row.FindControl("lblStat");
                        Label lblStatusShift = (Label)Row.FindControl("lblStatusTRANS");
                        Label lblStatusTimein = (Label)Row.FindControl("lblStatusREIMB");
                        Label lblTotalWorking = (Label)Row.FindControl("lblStatusLUP");
                        Label lblouttime = (Label)Row.FindControl("lblStatusTAX");
                        Label lblot = (Label)Row.FindControl("lblStatusJA");
                        Label lblCO = (Label)Row.FindControl("lblStatus");
                        TextBox txtAttRemark = (TextBox)Row.FindControl("txtAttRemark");

                        if (lblouttime.Text != "")
                        {
                            DateTime OUTtime = Convert.ToDateTime(lblouttime.Text);
                            OUT_TIME = OUTtime.TimeOfDay;
                        }
                        else
                        {
                            DateTime OUTtime = Convert.ToDateTime("00:00:00");
                            OUT_TIME = OUTtime.TimeOfDay;
                        }
                        if (lblStatusTimein.Text != "")
                        {
                            DateTime time = Convert.ToDateTime(lblStatusTimein.Text);
                            IN_TIME = time.TimeOfDay;
                        }
                        else
                        {
                            DateTime time = Convert.ToDateTime("00:00:00");
                            IN_TIME = time.TimeOfDay;

                        }
                        if (lblStatusTimein.Text == lblouttime.Text)
                        {
                            txtAttRemark.Text = "Missing - Punch Time";
                            Row.Cells[16].BackColor = System.Drawing.Color.OrangeRed;
                        }

                        DateTime Com = Convert.ToDateTime("00:00:00");
                        TimeSpan CompareWith = Com.TimeOfDay;

                        if (lblStat.Text == "" || lblStat.Text == "0")
                        {
                            Row.Cells[17].Visible = false;
                        }
                        if (lblot.Text != "" && lblCO.Text != "")
                        {
                            if (lblot.Text == "")
                            {
                                lblot.Text = "0";
                            }
                            if (lblCO.Text == "")
                            {
                                lblCO.Text = "0";
                            }
                            double OT = Convert.ToDouble(lblot.Text);
                            double CO = Convert.ToDouble(lblCO.Text);

                            if (CO > 0)
                            {
                                txtAttRemark.Text = "CO";
                                Row.Cells[16].BackColor = System.Drawing.Color.Green;
                            }
                            if (CO > 0 && OT > 0)
                            {
                                txtAttRemark.Text = "OT + CO";
                                Row.Cells[16].BackColor = System.Drawing.Color.GreenYellow;
                            }
                        }

                        else if (lblot.Text != "")
                        {
                            if (lblot.Text == "")
                            {
                                lblot.Text = "0";
                            }
                            if (lblCO.Text == "")
                            {
                                lblCO.Text = "0";
                            }
                            double OT = Convert.ToDouble(lblot.Text);
                            if (OT > 0)
                            {
                                txtAttRemark.Text = "OT";
                                Row.Cells[16].BackColor = System.Drawing.Color.LightPink;
                            }
                        }
                        else if (lblot.Text != "")
                        {
                            if (lblot.Text == "")
                            {
                                lblot.Text = "0";
                            }
                            if (lblCO.Text == "")
                            {
                                lblCO.Text = "0";
                            }
                            double CO = Convert.ToDouble(lblCO.Text);
                            if (CO > 0)
                            {
                                txtAttRemark.Text = "CO";
                                Row.Cells[16].BackColor = System.Drawing.Color.LightSkyBlue;
                            }
                        }


                        if (lblStatusShift.Text == "GN" || lblStatusShift.Text == "EV" || lblStatusShift.Text == "GS" || lblStatusShift.Text == "MS" || lblStatusShift.Text == "NS")
                        {
                            if (IN_TIME == CompareWith && OUT_TIME == CompareWith)
                            {
                                txtAttRemark.Text = "Absent";
                                Row.Cells[16].BackColor = System.Drawing.Color.Red;
                            }

                            if (lblouttime.Text == "" || lblStatusTimein.Text == "")
                            {
                                txtAttRemark.Text = "Rectification";
                                Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                            }


                        }
                        else if (lblStatusShift.Text == "WO")
                        {
                            if (IN_TIME != CompareWith || OUT_TIME != CompareWith)
                            {
                                txtAttRemark.Text = "Working on holiday";
                                Row.Cells[16].BackColor = System.Drawing.Color.Green;
                            }
                            if (IN_TIME == CompareWith || OUT_TIME == CompareWith)
                            {
                                txtAttRemark.Text = "Weekly Off";
                                Row.Cells[16].BackColor = System.Drawing.Color.LightGreen;
                            }

                        }

                    }
                }
            }

            catch (Exception ex)
            {

            }


        }

        private void OutSideDuty()
        {

            //objNps.EmpCode = Session["sEmpCode"].ToString();
            //objNps.EmpCode = txtEmpCode.Text;
            objNps.EmpName = Session["sEmpName"].ToString();
            //ds = objNps.GetOutSideDuty((string)Session["sCompID"], (string)Session["sEmpCode"]);
            //ds = objNps.GetOutSideDuty((string)Session["sCompID"], txtEmpCode.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; Convert.ToBoolean(ds.Tables[0].Rows.Count); i++)
                {
                    if (ds.Tables[0].Rows.Count != i)
                    {
                        string OutSIde = ds.Tables[0].Rows[i]["FROM_DT"].ToString();
                        string OutSIdeReason = ds.Tables[0].Rows[i]["LEAVE_REASON"].ToString();
                        // Convert string to DateTime object
                        //DateTime dateTime = DateTime.ParseExact(OutSIde, "dd-MM-yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                        //// Format DateTime to "DD-MM-YYYY" format
                        //string dateFormatted = dateTime.ToString("dd-MM-yyyy");
                        string OutSideRmark = ds.Tables[0].Rows[i]["ODTYPE"].ToString();
                        string STATUS = ds.Tables[0].Rows[i]["STATUS"].ToString();
                        string HOLIDAY = ds.Tables[0].Rows[i]["HOLIDAY"].ToString();
                        foreach (GridViewRow Row in gvList.Rows)
                        {
                            if (Row.RowType == DataControlRowType.DataRow)
                            {
                                int index = Row.RowIndex;
                                if (index == 0)
                                {
                                    Count = 0;
                                }
                                Label lblStat = (Label)Row.FindControl("lblOrigin");
                                TextBox txtAttRemark = (TextBox)Row.FindControl("txtAttRemark");
                                string txt = txtAttRemark.Text;


                                if (OutSIdeReason != "")
                                {

                                    if (lblStat.Text == OutSIde)
                                    {
                                        if (STATUS == "2")
                                        {
                                            txtAttRemark.Text = OutSIdeReason;
                                            Row.Cells[16].BackColor = System.Drawing.Color.Green;
                                        }
                                        else
                                        {
                                            txtAttRemark.Text = OutSIdeReason + " /Approval Pending";
                                            Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                                        }
                                        if (HOLIDAY == "1")
                                        {
                                            txtAttRemark.Text = OutSideRmark + "Paid Holiday";
                                            Row.Cells[16].BackColor = System.Drawing.Color.LightPink;
                                        }
                                    }

                                }

                                else
                                {

                                    if (lblStat.Text == OutSIde)
                                    {
                                        if (STATUS == "2")
                                        {
                                            txtAttRemark.Text = OutSideRmark + " /Out Door Duty";
                                            Row.Cells[16].BackColor = System.Drawing.Color.Green;
                                        }

                                        else
                                        {
                                            txtAttRemark.Text = OutSideRmark + " /Approval Pending";
                                            Row.Cells[16].BackColor = System.Drawing.Color.Yellow;
                                        }
                                        if (HOLIDAY == "1")
                                        {
                                            txtAttRemark.Text = OutSideRmark + "Paid Holiday";
                                            Row.Cells[16].BackColor = System.Drawing.Color.LightPink;
                                        }
                                    }
                                }

                            }



                        }
                    }
                    else
                    {
                        break;
                    }
                }

            }

        }

        public void Generated()
        {
            try
            {
                DateTime dt = DateTime.Now;


                objNps.FromDate = " ";
                objNps.ToDate = " ";
                objNps.EmpCode = Session["sEmpCode"].ToString();
                objNps.EmpName = Session["sEmpName"].ToString();
                // lblMessage.Text = Convert.ToString(StartDates) +" "+ Convert.ToString(EndDates);

                ds = objNps.GenerateEmpAttendanceReportFlag((string)Session["sCompID"], "EMP");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) != "")
                    {
                        lblMessage.Text = Convert.ToString(ds.Tables[0].Rows[0]["result"]);
                        gvList.DataSource = null;
                        gvList.DataBind();
                        gvList.Visible = false;
                    }
                    else if (Convert.ToString(ds.Tables[1].Rows[0]["result"]) == "")
                    {
                        gvList.DataSource = ds;
                        gvList.DataBind();
                        gvList.Visible = true;
                        CalCulation();
                        OutSideDuty();
                        lblMessage.Text = "Attendance Report Generated Successfully.";
                    }
                }

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        protected void btnGenerateAttendanceReport_Click(object sender, EventArgs e)
        {
            try
            {

                if (drpMonth.SelectedValue == "0")
                {
                    
                    divAlert.Visible = true;
                    string script = $@"<script type='text/javascript'>alert('Please Select Month.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    lblMessage.Text = "Please Select Month";
                    divAlertSucc.Visible = false;
                    return;
                }
                else if (drpYear.SelectedValue == "0")
                {
                    
                    divAlert.Visible = true;
                    string script = $@"<script type='text/javascript'>alert('Please Select Year.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    lblMessage.Text = "Please Select Year";
                    divAlertSucc.Visible = false;
                    return;
                }

                DateTime dt = DateTime.Now;
                lblMessage.Text = "";
                if (drpMonth.SelectedValue != "0" || drpYear.SelectedValue != "0")                          
                {
                    objNps.selMonth = drpMonth.SelectedValue;
                    objNps.selYear = drpYear.SelectedValue;

                    objNps.EmpName = Session["sEmpName"].ToString();
                    ds = objNps.GenerateAttendanceSummeryReport((string)Session["sCompID"], (string)Session["sEmpCode"]);


                    rptPrint.Visible = true;
                    ReportDataSource datasource1 = new ReportDataSource("dsAttendanceSumRpt", ds.Tables[0]);
        
                    rptPrint.LocalReport.DataSources.Clear();

                    rptPrint.LocalReport.ReportPath = @"Reports/AttendanceSummaryReport.rdlc";
                    rptPrint.LocalReport.DisplayName = "Attendance_Summary_Report_" + drpMonth.SelectedItem + "_" + drpYear.SelectedValue+ "";
                    rptPrint.LocalReport.DataSources.Add(datasource1);
          

                    rptPrint.LocalReport.Refresh();

                    //gvList.DataSource = ds;
                    //gvList.DataBind();
                    //gvList.Visible = true;

                    divAlertSucc.Visible = true;
                    string script = $@"<script type='text/javascript'>alert('Attendance Report Generated Successfully.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    lblMessageSucc.Text = "Attendance Report Generated Successfully.";
                    divAlert.Visible = false;
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                    gvList.Visible = false;


                    divAlert.Visible = true;
                    string script = $@"<script type='text/javascript'>alert('Please Select Month/Year First Then Click Generate Button.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    lblMessage.Text = "Please Select Month/Year First Then Click Generate Button";
                    divAlertSucc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }





        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (gvList != null)
            {
                if (gvList.Rows.Count > 0)
                {
                    ExportGridView(gvList);
                    // ExportToExcel(gvList);
                }
                else
                {
                    lblMessage.Text = "Generate report first.";
                }
            }
        }
        private void ExportGridView(GridView gvList)
        {

            //Response.Clear();
            //Response.Buffer = true;
            //Response.ClearContent();
            //Response.ClearHeaders();
            //Response.Charset = "";
            //string FileName = "Attendance" + DateTime.Now + ".xls";
            //StringWriter strwritter = new StringWriter();
            //HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
            //gvFiles.GridLines = GridLines.Both;
            //gvFiles.HeaderStyle.Font.Bold = true;
            //gvFiles.RenderControl(htmltextwrtter);
            //Response.Write(strwritter.ToString());
            //Response.End();
            string guid;
            string path;
            //if ((string)Session["sCompID"].ToString() == "CO000141")
            //{
            //    this.gvList.AllowPaging = false;
            //    this.gvList.AllowSorting = false;
            //    this.gvList.EditIndex = -1;

            //    Response.Clear();
            //    Response.ContentType = "application/vnd.xls";
            //    Response.AddHeader("content-disposition",
            //            "attachment;filename=Attendance.xls");
            //    Response.Charset = "";
            //    StringWriter swriter = new StringWriter();
            //    HtmlTextWriter hwriter = new HtmlTextWriter(swriter);
            //    gvList.RenderControl(hwriter);
            //    Response.Write(swriter.ToString());
            //    Response.End();
            //}

            if (Session["sCompID"] != null)
            {
                try
                {
                    ds = objNps.GenerateAttendanceSummeryReport((string)Session["sCompID"], (string)Session["sEmpCode"]);

                    string extension;
                    string encoding;
                    string contentType;
                    string[] streamIds;
                    Warning[] warnings;

                    rptPrint.Visible = true;
                    ReportDataSource datasource = new ReportDataSource("dsAttendanceSumRpt", ds.Tables[0]);

                    rptPrint.LocalReport.DataSources.Clear();
                    rptPrint.ProcessingMode = ProcessingMode.Local;
                    rptPrint.LocalReport.ReportPath = @"ESS\AttendanceSummaryReport.rdlc";

                    //rptPrint.LocalReport.DisplayName = (string)Session["sEmpCode"] + "_Flexi";
                    rptPrint.LocalReport.DataSources.Add(datasource);
                    rptPrint.LocalReport.Refresh();

                    byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = ContentType;
                    //Response.AppendHeader("Content-Disposition", "attachment; filename=Attendance_Summary_Report.xls");
                    //string filename = $"Attendance_Summary_Report_{drpMonth.SelectedValue}_{drpYear.SelectedValue}.xls";
                    //Response.AppendHeader("Content-Disposition", $"attachment; filename={filename}");
                    string filename = "Attendance_Summary_Report_" + drpMonth.SelectedItem + "_" + drpYear.SelectedValue + ".xls";
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename);
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                }
                catch (Exception ex)
                {

                }

            }

        }

        protected void ExportToExcel(GridView gvList)
        {
            if ((string)Session["sCompID"].ToString() == "CO000141")
            {
                this.gvList.AllowPaging = false;
                this.gvList.AllowSorting = false;
                this.gvList.EditIndex = -1;

                DataTable dataTable = new DataTable("Attendance");

                foreach (var column in gvList.Columns)
                {
                    if (Convert.ToString(column) != "")
                    {
                        dataTable.Columns.Add(column.ToString());
                    }
                }

                foreach (GridViewRow row in gvList.Rows)
                {
                    DataRow dataRow = dataTable.NewRow();
                    for (int j = 0; j < gvList.Columns.Count; j++)
                    {
                        var cell = row.Cells[j];
                        string cellContent = GetCellTextContent(cell);
                        dataRow[j] = cellContent;
                    }
                    dataTable.Rows.Add(dataRow);
                }

                using (XLWorkbook workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add(dataTable, "Attendance");
                    worksheet.Columns().AdjustToContents();
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Attendance.xlsx");
                    Response.Charset = "";

                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        workbook.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.OutputStream);
                        memoryStream.Close();
                    }

                    Response.End();
                }

            }
        }

        private string GetCellTextContent(TableCell cell)
        {
            string content = "";

            foreach (Control control in cell.Controls)
            {
                if (control is LiteralControl)
                {
                    content += ((LiteralControl)control).Text;
                }
                else if (control is Label)
                {
                    content += ((Label)control).Text;
                }
                // Add other control types as needed

                // You might need additional logic depending on the control types used in your GridView cells
            }

            return content;
        }


        
        
         

       

        protected void txtInDate_TextChanged(object sender, EventArgs e)
        {
            //CalculateOT();
        }

        protected void txtIntime_TextChanged(object sender, EventArgs e)
        {
            //CalculateOT();
        }

        protected void txtOutDate_TextChanged(object sender, EventArgs e)
        {
            // CalculateOT();
        }

        protected void txtOuttime_TextChanged(object sender, EventArgs e)
        {
            //CalculateOT();
        }

        

        protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void DropYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}