using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class EmployeeDetails : System.Web.UI.Page
    {
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();
        NewPortal2023.ESS.Payslip objPay = new NewPortal2023.ESS.Payslip();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Investments objInv = new NewPortal2023.ESS.Investments();
        DataSet dsInv;
        DataSet dsMon;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["sCompID"] != null)
                {
                    try
                    {
                        lblMessage.Text = "";
                        FillMonths();

                        //FillEmployees();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading payslip details.');");
                    }
                }
            }
        }

        private void FillMonths()
        {
            StringBuilder sbDetails = new StringBuilder();
            dsMon = new DataSet();
            if ((string)Session["sCompID"] == "CO000114")
            {
                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\NPL\\");
                System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
                sbDetails.Append("<ROOT>");
                foreach (DirectoryInfo dir in fileNames)
                {
                    if (dir.Name.ToString().ToUpper() != "FORM16" && dir.Name.ToString().ToUpper() != "INCRLETTERS" && dir.Name.ToString().ToUpper() != "FORM12BB" && dir.Name.ToString().ToUpper() != "REIMBURSEMENT" && dir.Name.ToString().ToUpper() != "DOCUMENTS" && dir.Name.ToString().ToUpper() != "TAXCOMPUTATION" && dir.Name.ToString().ToUpper() != "CUMULATIVE" && dir.Name.ToString().ToUpper() != "LETTERS" && dir.Name.ToString().ToUpper() != "SALARY ARREARS AND PLP 2022_2023")
                    {
                        sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
                    }
                }
                sbDetails.Append("</ROOT>");
            }
            else
            {
                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Session["sCompAID"] + "\\");
                System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
                sbDetails.Append("<ROOT>");
                foreach (DirectoryInfo dir in fileNames)
                {
                    if (dir.Name.ToString().ToUpper() != "FORM16" && dir.Name.ToString().ToUpper() != "INCRLETTERS" && dir.Name.ToString().ToUpper() != "FORM12BB" && dir.Name.ToString().ToUpper() != "REIMBURSEMENT"
                        && dir.Name.ToString().ToUpper() != "DOCUMENTS" && dir.Name.ToString().ToUpper() != "TAXCOMPUTATION" && dir.Name.ToString().ToUpper() != "CUMULATIVE" && dir.Name.ToString().ToUpper() != "MISREPORTS"
                        && dir.Name.ToString().ToUpper() != "MONTHLYREPORTS" && dir.Name.ToString().ToUpper() != "SALARY ARREARS AND PLP 2022_2023" && dir.Name.ToString().ToUpper() != "PMS")
                    {
                        sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
                    }
                }
                sbDetails.Append("</ROOT>");
            }

            dsMon = objPay.FillMonths(sbDetails.ToString());

            drpMonth.Items.Clear();
            drpMonth.DataTextField = "MONYEARCODE";
            drpMonth.DataValueField = "MONYEARNAME";
            drpMonth.DataSource = dsMon;
            drpMonth.DataBind();
            drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));
        }

        private void FillEmployees()
        {
            DataSet ds = new DataSet();
            ds = objPay.FillEmpList(txtEmpMid.Text.Trim(), (string)Session["sCompID"]);
            if (ds.Tables[0].Rows.Count > 0)
            {
                gvEmployeeDetails.DataSource = ds;
                gvEmployeeDetails.DataBind();
            }
            else
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("EMP_AID", typeof(System.String));
                dt.Columns.Add("EMP_MID", typeof(System.String));
                dt.Columns.Add("EMP_NAME", typeof(System.String));

                DataRow dr = dt.NewRow();
                dr["EMP_AID"] = "";
                dr["EMP_MID"] = txtEmpMid.Text.Trim();
                dr["EMP_NAME"] = "";

                dt.Rows.Add(dr);

                gvEmployeeDetails.DataSource = dt;
                gvEmployeeDetails.DataBind();
            }
            ViewState["EmployeeData"] = ds;
        }

        private void FillEmployeesViewState()
        {
            gvEmployeeDetails.DataSource = (DataSet)ViewState["EmployeeData"];
            gvEmployeeDetails.DataBind();
        }

        protected void lnkBtnOpenPayslip_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                LinkButton lnkBtnOpenPayslip = (LinkButton)sender;
                //smInv.RegisterPostBackControl(lnkBtnOpenPayslip);
                Label lblEmpCode = (Label)lnkBtnOpenPayslip.NamingContainer.FindControl("lblEmpCode");

                lblMessage.Text = "";
                if (drpMonth.SelectedIndex == 0)
                {
                    lblMessage.Text = "Select Month.";
                    return;
                }

                string savePath = Request.PhysicalApplicationPath;
                if ((string)Session["sCompID"] == "CO000141")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\NPL\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "\\PAYSLIP";

                }
                else //if ((string)Session["sCompID"] == "CO000125")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Session["sCompAID"] + "\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "\\PAYSLIP";
                }
                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                if (fileNames.Length > 0)
                {
                    object fileObj = new object();
                    bool found = false;

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        string[] strEmpcode = fi.Name.Split('_');

                        if (strEmpcode[0].ToString().Trim().ToUpper() == lblEmpCode.Text.Trim())
                        {
                            fileObj = fi;
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        if (((System.IO.FileInfo)fileObj).Exists)
                        {
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AppendHeader("content-disposition", "attachment; filename=" + ((System.IO.FileInfo)fileObj).Name);
                            Response.ContentType = "text/csv";
                            Response.WriteFile(((System.IO.FileInfo)fileObj).FullName);
                            Response.End();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Payslip not found for selected month.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Payslip not found for selected month.');");
                    }
                }
                else
                {
                    lblMessage.Text = "Payslip not found for selected month.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Payslip not found for selected month.');");
                }
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkBtnOpenCumulativePayslip_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                LinkButton lnkBtnOpenPayslip = (LinkButton)sender;
                //smInv.RegisterPostBackControl(lnkBtnOpenPayslip);
                Label lblEmpCode = (Label)lnkBtnOpenPayslip.NamingContainer.FindControl("lblEmpCode");
                Label lblEmpName = (Label)lnkBtnOpenPayslip.NamingContainer.FindControl("lblEmpName");

                lblMessage.Text = "";

                Session["EMPCODECUM"] = lblEmpCode.Text.Trim();
                Session["EMPNAMECUM"] = lblEmpName.Text.Trim();

                Response.Redirect("CumulativeSingle.aspx");

                //if (drpMonth.SelectedIndex == 0)
                //{
                //    lblMessage.Text = "Select Month.";
                //    return;
                //}

                //string savePath = Request.PhysicalApplicationPath;
                //savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\CUMULATIVE";

                //System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                //System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                //if (fileNames.Length > 0)
                //{
                //    object fileObj = new object();
                //    bool found = false;

                //    foreach (System.IO.FileInfo fi in fileNames)
                //    {
                //        string[] strEmpcode = fi.Name.Split('_');

                //        if (strEmpcode[0].ToString().Trim().ToUpper() == lblEmpCode.Text.Trim())
                //        {
                //            fileObj = fi;
                //            found = true;
                //            break;
                //        }
                //    }

                //    if (found)
                //    {
                //        if (((System.IO.FileInfo)fileObj).Exists)
                //        {
                //            Response.Clear();
                //            Response.Buffer = true;
                //            Response.AppendHeader("content-disposition", "attachment; filename=" + ((System.IO.FileInfo)fileObj).Name);
                //            Response.ContentType = "text/csv";
                //            Response.WriteFile(((System.IO.FileInfo)fileObj).FullName);
                //            Response.End();
                //        }
                //    }
                //    else
                //    {
                //        lblMessage.Text = "Cumulative Payslip not found.";
                //        objcommon.Display("Validate", "DisplayErrorMessage('Cumulative Payslip not found.');");
                //    }
                //}
                //else
                //{
                //    lblMessage.Text = "Cumulative Payslip not found.";
                //    objcommon.Display("Validate", "DisplayErrorMessage('Cumulative Payslip not found.');");
                //}
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkBtnOpenAppraisalLetter_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";

                LinkButton lnkBtnOpenPayslip = (LinkButton)sender;
                //smInv.RegisterPostBackControl(lnkBtnOpenPayslip);
                Label lblEmpCode = (Label)lnkBtnOpenPayslip.NamingContainer.FindControl("lblEmpCode");
                Label lblEmpName = (Label)lnkBtnOpenPayslip.NamingContainer.FindControl("lblEmpName");

                lblMessage.Text = "";

                Session["EMPCODECUM"] = lblEmpCode.Text.Trim();
                Session["EMPNAMECUM"] = lblEmpName.Text.Trim();

                Response.Redirect("IncrementLetterSingle.aspx");

                //LinkButton lnkBtnOpenTaxComputation = (LinkButton)sender;
                ////smInv.RegisterPostBackControl(lnkBtnOpenPayslip);
                //Label lblEmpCode = (Label)lnkBtnOpenTaxComputation.NamingContainer.FindControl("lblEmpCode");

                //lblMessage.Text = "";

                //string savePath = Request.PhysicalApplicationPath;
                //savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\INCRLETTERS";

                //System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                //System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                //if (fileNames.Length > 0)
                //{
                //    object fileObj = new object();
                //    bool found = false;

                //    foreach (System.IO.FileInfo fi in fileNames)
                //    {
                //        string[] strEmpcode = fi.Name.Split('_');

                //        if (strEmpcode[0].ToString().Trim().ToUpper() == lblEmpCode.Text.Trim())
                //        {
                //            fileObj = fi;
                //            found = true;
                //            break;
                //        }
                //    }

                //    if (found)
                //    {
                //        if (((System.IO.FileInfo)fileObj).Exists)
                //        {
                //            Response.Clear();
                //            Response.Buffer = true;
                //            Response.AppendHeader("content-disposition", "attachment; filename=" + ((System.IO.FileInfo)fileObj).Name);
                //            Response.ContentType = "text/csv";
                //            Response.WriteFile(((System.IO.FileInfo)fileObj).FullName);
                //            Response.End();
                //        }
                //    }
                //    else
                //    {
                //        lblMessage.Text = "Appraisal Letter not found.";
                //        objcommon.Display("Validate", "DisplayErrorMessage('Appraisal Letter not found.');");
                //    }
                //}
                //else
                //{
                //    lblMessage.Text = "Appraisal Letter not found.";
                //    objcommon.Display("Validate", "DisplayErrorMessage('Appraisal Letter not found.');");
                //}
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkBtnOpenForm16_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";

                LinkButton lnkBtnOpenPayslip = (LinkButton)sender;
                //smInv.RegisterPostBackControl(lnkBtnOpenPayslip);
                Label lblEmpCode = (Label)lnkBtnOpenPayslip.NamingContainer.FindControl("lblEmpCode");
                Label lblEmpName = (Label)lnkBtnOpenPayslip.NamingContainer.FindControl("lblEmpName");

                lblMessage.Text = "";

                Session["EMPCODECUM"] = lblEmpCode.Text.Trim();
                Session["EMPNAMECUM"] = lblEmpName.Text.Trim();

                Response.Redirect("Form16Single.aspx");
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkBtnOpenTaxComputation_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                LinkButton lnkBtnOpenTaxComputation = (LinkButton)sender;
                //smInv.RegisterPostBackControl(lnkBtnOpenPayslip);
                Label lblEmpCode = (Label)lnkBtnOpenTaxComputation.NamingContainer.FindControl("lblEmpCode");

                lblMessage.Text = "";

                string savePath = Request.PhysicalApplicationPath;
                if ((string)Session["sCompID"] == "CO000141")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\NPL\\TAXCOMPUTATION";
                }
                else //if ((string)Session["sCompID"] == "CO000125")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Session["sCompAID"] + "\\TAXCOMPUTATION";
                }

                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                if (fileNames.Length > 0)
                {
                    object fileObj = new object();
                    bool found = false;

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        string[] strEmpcode = fi.Name.Split('_');

                        if (strEmpcode[0].ToString().Trim().ToUpper() == lblEmpCode.Text.Trim())
                        {
                            fileObj = fi;
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        if (((System.IO.FileInfo)fileObj).Exists)
                        {
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AppendHeader("content-disposition", "attachment; filename=" + ((System.IO.FileInfo)fileObj).Name);
                            Response.ContentType = "text/csv";
                            Response.WriteFile(((System.IO.FileInfo)fileObj).FullName);
                            Response.End();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Tax Computation not found.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Tax Computation not found.');");
                    }
                }
                else
                {
                    lblMessage.Text = "Tax Computation not found.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Tax Computation not found.');");
                }
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkBtnOpen12BB_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                LinkButton lnkBtnOpenTaxComputation = (LinkButton)sender;
                //smInv.RegisterPostBackControl(lnkBtnOpenPayslip);
                Label lblEmpCode = (Label)lnkBtnOpenTaxComputation.NamingContainer.FindControl("lblEmpCode");

                lblMessage.Text = "";

                string savePath = Request.PhysicalApplicationPath;

                if ((string)Session["sCompID"] == "CO000141")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\NPL\\Form12BB";
                }
                else // if ((string)Session["sCompID"] == "CO000125")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Session["sCompAID"] + "\\Form12BB";
                }


                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                if (fileNames.Length > 0)
                {
                    object fileObj = new object();
                    bool found = false;

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        string[] strEmpcode = fi.Name.Split('.');

                        if (strEmpcode[0].ToString().Trim().ToUpper() == lblEmpCode.Text.Trim().ToUpper())
                        {
                            fileObj = fi;
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        if (((System.IO.FileInfo)fileObj).Exists)
                        {
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AppendHeader("content-disposition", "attachment; filename=" + ((System.IO.FileInfo)fileObj).Name);
                            Response.ContentType = "text/csv";
                            Response.WriteFile(((System.IO.FileInfo)fileObj).FullName);
                            Response.End();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Form 12BB not found.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Form 12BB not found.');");
                    }
                }
                else
                {
                    lblMessage.Text = "Form 12BB not found.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Form 12BB not found.');");
                }
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkBtnOpenSupports_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                LinkButton lnkBtnOpenSupports = (LinkButton)sender;
                Label lblEmpCode = (Label)lnkBtnOpenSupports.NamingContainer.FindControl("lblEmpCode");
                Label lblEmpName = (Label)lnkBtnOpenSupports.NamingContainer.FindControl("lblEmpName");
                Session["SupportEmpCode"] = lblEmpCode.Text;
                Session["SupportEmpName"] = lblEmpName.Text;
                Response.Redirect("SupportView.aspx");
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkBtnOpenDiscrepancy_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                LinkButton lnkBtnOpenDiscrepancy = (LinkButton)sender;
                //smInv.RegisterPostBackControl(lnkBtnOpenPayslip);
                Label lblEmpCode = (Label)lnkBtnOpenDiscrepancy.NamingContainer.FindControl("lblEmpCode");

                lblMessage.Text = "";

                string savePath = Request.PhysicalApplicationPath;


                if ((string)Session["sCompID"] == "CO000141")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\NPL\\Documents\\Discrepancy";
                }
                else //if ((string)Session["sCompID"] == "CO000125")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Session["sCompAID"] + "\\Documents\\Discrepancy";
                }

                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                if (fileNames.Length > 0)
                {
                    object fileObj = new object();
                    bool found = false;

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        string[] strEmpcode = fi.Name.Split('_');

                        if (strEmpcode[0].ToString().Trim().ToUpper() == lblEmpCode.Text.Trim().ToUpper())
                        {
                            fileObj = fi;
                            found = true;
                            break;
                        }
                    }

                    if (found)
                    {
                        if (((System.IO.FileInfo)fileObj).Exists)
                        {
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AppendHeader("content-disposition", "attachment; filename=" + ((System.IO.FileInfo)fileObj).Name);
                            Response.ContentType = "text/csv";
                            Response.WriteFile(((System.IO.FileInfo)fileObj).FullName);
                            Response.End();
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Verification Report not found.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Verification Report not found.');");
                    }
                }
                else
                {
                    lblMessage.Text = "Verification Report not found.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Verification Report not found.');");
                }
            }
            catch (ThreadAbortException ex)
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btn12BBList_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                ExportInvestmentData("12BB");
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                lblMessage.Text = "Error occurred in an application.";
            }
        }

        protected void btnSupportList_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                ExportInvestmentData("SUPPORTS");
            }
            catch (Exception ex)
            {
                //Response.Write(ex.Message);
                lblMessage.Text = "Error occurred in an application.";
            }
        }

        private void ExportInvestmentData(string type)
        {
            if (type == "12BB")
            {
                dsInv = objInv.Get12BBList((string)Session["sCompID"]);
            }
            else if (type == "SUPPORTS")
            {
                dsInv = objInv.GetSupportsList((string)Session["sCompID"]);
            }

            if (dsInv.Tables.Count > 0)
            {
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    string attachment = "attachment; filename=" + type + ".xls";
                    Response.ClearContent();
                    Response.AddHeader("content-disposition", attachment);
                    Response.ContentType = "application/vnd.ms-excel";
                    string tab = "";
                    foreach (DataColumn dc in dsInv.Tables[0].Columns)
                    {
                        Response.Write(tab + dc.ColumnName);
                        tab = "\t";
                    }
                    Response.Write("\n");

                    int i;
                    foreach (DataRow dr in dsInv.Tables[0].Rows)
                    {
                        tab = "";
                        for (i = 0; i < dsInv.Tables[0].Columns.Count; i++)
                        {
                            //replace any new line characters from Description column                            
                            Response.Write(tab + dr[i].ToString().Replace(Environment.NewLine, ""));
                            tab = "\t";
                        }
                        Response.Write("\n");
                    }
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    lblMessage.Text = "Data does not exist.";
                }
            }
        }

        protected void btnLoadDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtEmpMid.Text.Trim() != "")
                {
                    FillEmployees();
                }
                else
                {
                    lblMessage.Text = "Enter Employee Code.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void gvEmployeeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lnkBtnOpenCumulativePayslip = (LinkButton)e.Row.FindControl("lnkBtnOpenCumulativePayslip");
                    LinkButton lnkBtnOpenForm16 = (LinkButton)e.Row.FindControl("lnkBtnOpenForm16");
                    LinkButton lnkBtnOpenAppraisalLetter = (LinkButton)e.Row.FindControl("lnkBtnOpenAppraisalLetter");


                    if ((string)Session["sCompID"] == "CO000125")
                    {

                        lnkBtnOpenCumulativePayslip.Visible = false;
                        lnkBtnOpenForm16.Visible = false;
                        lnkBtnOpenAppraisalLetter.Visible = false;

                    }
                    else if ((string)Session["sCompID"] == "CO000057")
                    {

                        lnkBtnOpenCumulativePayslip.Visible = false;
                        lnkBtnOpenForm16.Visible = false;
                        lnkBtnOpenAppraisalLetter.Visible = false;

                    }



                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }
    }
}