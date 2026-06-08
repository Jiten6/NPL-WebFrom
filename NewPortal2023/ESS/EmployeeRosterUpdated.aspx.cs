using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;


namespace NewPortal2023.ESS
{
    public partial class EmployeeRosterUpdated : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();
        DataSet dsInv = new DataSet();
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ds = objcommon.ProfIdOfEmp((string)Session["sCompID"], (string)Session["sEmpID"]);

                Session["PROF_ID"] = ds.Tables[0].Rows[0]["PROF_ID"].ToString();

                ds = objcommon.CheckMenuAccess((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["PROF_ID"]);

                // Get the current page's name (e.g., KISNAInvestmentDetailsAdmin.aspx)
                string currentPageName = Path.GetFileName(Request.Url.AbsolutePath);

                // Check if the MENU_URL matches the current page name
                bool accessGranted = false;

                // Iterate through all rows in the dataset
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row["MENU_URL"].ToString() == currentPageName)
                    {
                        accessGranted = true; // If match found, access is granted
                        break; // Exit the loop once match is found
                    }
                }
                if (accessGranted)
                {
                    if (Session["sCompID"] != null)
                    {
                        if (!Page.IsPostBack)
                        {
                            if (Session["sCompID"] != null)
                            {
                                try
                                {

                                    FillRoster();
                                    fillEmployeeDropdown();
                                }
                                catch (Exception ex)
                                {
                                    lblMessage.Text = "Error occurred in application.";
                                }
                            }
                            else
                            {
                                Response.Redirect("Logout.aspx");
                            }
                        }
                        else
                        {
                            //Response.Redirect("Login.aspx");
                        }

                    }

                }
                else
                {
                    // If access is denied, redirect to the previous page
                    string previousPage = Request.UrlReferrer?.ToString();

                    if (!string.IsNullOrEmpty(previousPage))
                    {
                        Response.Redirect(previousPage); // Redirect to the previous page
                    }
                    else
                    {
                        Response.Redirect("Default.aspx");
                        return;
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                //Session["Error"] = ex.Message + "<br>Payslip.aspx";
                Session["Error"] = ex.Message + "<br>Payslip.aspx<br>" + Request.QueryString["key"].Replace(" ", "+");
                Response.Redirect("../ErrorPage.aspx", true);
            }


            

        }

       

        private void FillRoster()
        {
            try
            {

                objNps.EmpCode = Session["sEmpID"].ToString();

                dsInv = objNps.GetRosterReportHRWiseTillDate((string)Session["sCompID"]);
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    gvRoster.DataSource = dsInv;
                    gvRoster.DataBind();

                    lblMessage.Text = "Shift Roster Report Generated Successfuly.";
                }


                else
                {
                    gvRoster.DataSource = null;
                    gvRoster.DataBind();

                    lblMessage.Text = "Please Select From Date /To Date First Then Click Generate Button";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }


        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpType.SelectedValue == "")
            {
                divEmp.Visible = false;
            }
            else
            {

                divEmp.Visible = true;
            }
        }

        protected void btnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFromDate.Text == "")
                {
                    lblMessage.Text = "Select from date";
                    objcommon.Display("Validate", "DisplayErrorMessage('Select from date.');");
                    return;

                }
                if (txtToDate.Text == "")
                {
                    lblMessage.Text = "Select to date";
                    objcommon.Display("Validate", "DisplayErrorMessage('Select to date.');");
                    return;
                }


                FillDetails();
                lblMessage.Text = "";
                divAlert.Visible = false;
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
                if (txtFromDate.Text != "" && txtToDate.Text != "")
                {
                    objNps.FromDate = txtFromDate.Text;
                    objNps.ToDate = txtToDate.Text;

                    if (drpType.SelectedItem.Value != " ")
                    {
                        if (drpType.SelectedItem.Value == "EmployeeWise")
                        {
                            //objNps.EmpCode = txtEmpCode.Text;
                            objNps.EmpCode = drpEmpType.SelectedValue;
                            
                        }
                        else
                        {
                            objNps.EmpCode = " ";
                        }

                        if (drpType.SelectedItem.Value == "EmployeeWise")
                        {
                            dsInv = objNps.GetRosterReportEmpWise((string)Session["sCompID"]);
                            if (dsInv.Tables[0].Rows.Count > 0)
                            {
                                gvRoster.DataSource = dsInv;
                                gvRoster.DataBind();
                                lblMessage.Text = "Shift Roster Report Generated Successfuly.";
                            }
                            else
                            {
                                gvRoster.DataSource = null;
                                gvRoster.DataBind();
                                lblMessage.Text = "NOT FOUND";
                            }
                        }
                        else
                        {
                            dsInv = objNps.GetRosterReportAllWise((string)Session["sCompID"]);
                            if (dsInv.Tables[0].Rows.Count > 0)
                            {
                                gvRoster.DataSource = dsInv;
                                gvRoster.DataBind();
                                lblMessage.Text = "Shift Roster Report Generated Successfuly.";
                            }
                            else
                            {
                                gvRoster.DataSource = null;
                                gvRoster.DataBind();
                                lblMessage.Text = "NOT FOUND";
                            }

                        }
                    }

                }
                else
                {
                    gvRoster.DataSource = null;
                    gvRoster.DataBind();
                    lblMessage.Text = "Please Select From Date /To Date First Then Click Generate Button";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
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

        //dnyaneshwar
        //protected void lnkUpdate_Click(object sender, EventArgs e)
        //{
        //    LinkButton lnkRequestNo = (LinkButton)sender;
        //    string EmpCode = ((Label)lnkRequestNo.NamingContainer.FindControl("lnkAppNo")).Text;
        //    string oldShift = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusTRANS")).Text;
        //    string dateValue = ((Label)lnkRequestNo.NamingContainer.FindControl("lblOrigin")).Text;
        //    string ddlNewShift = ((DropDownList)lnkRequestNo.NamingContainer.FindControl("ddlNewShift")).SelectedValue;
        //    if (ddlNewShift == "null")
        //    {
        //        if (oldShift == ddlNewShift)
        //        {
        //            lblMessage.Text = "Error .";
        //            objcommon.Display("Validate", "DisplayErrorMessage('You have selected same old shift');");
        //        }
        //        else
        //        {
        //            dsInv = objNps.UpdateShift((string)Session["sCompID"], EmpCode, dateValue, ddlNewShift);
        //            lblMessage.Text = "Update successful.";
        //            objcommon.Display("Success", "DisplaySuccessMessage('Update successful.')");
        //        }
        //    }
        //    else
        //    {
        //        lblMessage.Text = "Please first select any one shift type.";
        //        objcommon.Display("Success", "DisplaySuccessMessage('Please first select any one shift type.')");
        //    }
        //}

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            LinkButton lnkRequestNo = (LinkButton)sender;
            string EmpCode = ((Label)lnkRequestNo.NamingContainer.FindControl("lnkAppNo")).Text;
            string oldShift = ((Label)lnkRequestNo.NamingContainer.FindControl("lblStatusTRANS")).Text;
            string dateValue = ((Label)lnkRequestNo.NamingContainer.FindControl("lblOrigin")).Text;
            string ddlNewShift = ((DropDownList)lnkRequestNo.NamingContainer.FindControl("ddlNewShift")).SelectedValue;
            if (ddlNewShift != "")
            {
                if (oldShift == ddlNewShift)
                {
                    divAlert.Visible = true;
                    lblMessage.Text = "You have selected same old shift .";
                    objcommon.Display("Validate", "DisplayErrorMessage('You have selected same old shift');");
                }
                else
                {
                    dsInv = objNps.UpdateShift((string)Session["sCompID"], EmpCode, dateValue, ddlNewShift);
                    divAlert.Visible = true;
                    lblMessage.Text = "Update successful.";
                    objcommon.Display("Success", "DisplaySuccessMessage('Update successful.')");
                }
            }
            else
            {
                divAlert.Visible = true;
                lblMessage.Text = "Please first select any one shift type.";
                objcommon.Display("Success", "DisplaySuccessMessage('Please first select any one shift type.')");
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