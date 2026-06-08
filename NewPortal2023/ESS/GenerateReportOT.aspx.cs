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
    public partial class GenerateReportOT : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveUpload objInv = new NewPortal2023.ESS.LeaveUpload();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsInv = new DataSet();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        // ESS.Email emailSend = new ESS.Email();
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
                        trList.Visible = false;
                        trViewList.Visible = false;
                        tblActionType.Visible = false;
                        trViewListOTCO.Visible = false;
                        trViewListByOTAndCOWise.Visible = false;
                        trback.Visible = false;
                        trdate.Visible = false;
                        trStatusOTCO.Visible = false;

                        trSerachBy.Visible = true;
                        FillLeave();
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

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDate.Text == "")
                {
                    lblMessage.Text = "Select from date";
                    return;
                }
                if (txtDateTo.Text == "")
                {
                    lblMessage.Text = "Select to date";
                    return;
                }

                //if (Convert.ToDateTime(txtDate.Text) > Convert.ToDateTime(txtDateTo.Text))
                //{
                //    lblMessage.Text = "From date cannot be greater that to date";
                //    return;
                //}
                // FillLeave();
                
                getOTAndCORptEmpWise();
                lblMessage.Text = "";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void FillLeave()
        {
            DateTime today = DateTime.Today;
            txtDate.Text = " ";
            txtDateTo.Text = " ";

            dsInv = objInv.GetOTCOReport((string)Session["sCompID"], (string)Session["sEmpID"], txtDate.Text, txtDateTo.Text);
            gvLeave.DataSource = dsInv;
            gvLeave.DataBind();
            trList.Visible = true;
            trViewList.Visible = false;
            trViewListOTCO.Visible = false;
            tblActionType.Visible = false;
            trViewListByOTAndCOWise.Visible = false;
            trback.Visible = false;
            trSerachBy.Visible = true;
            trStatusOTCO.Visible = false;
            trdate.Visible = false;
            //gvList.HeaderRow.Cells[8].Visible = false;
            //gvList.Columns[8].Visible = false;
            //gvList.HeaderRow.Cells[9].Visible = false;
            //gvList.Columns[9].Visible = false;

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
            string FileName = "Total OT AND CO Report " + DateTime.Now + ".xls";
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


        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {

            }
            objInv.Type = "";//drpType.SelectedValue;
            objInv.From = txtDate.Text;
            objInv.To = txtDateTo.Text;
            dsInv = objInv.GetOTCOReportByFilter((string)Session["sCompID"], (string)Session["sEmpID"]);
            gvLeave.DataSource = dsInv;
            gvLeave.DataBind();
        }

        private bool ValidateInputs()
        {
            //if (drpType.SelectedValue.Trim() == "")
            //{
            //    lblMessage.Text = "Select Type.";
            //    objcommon.Display("Validate", "DisplayErrorMessage('Select Type.');");
            //    return false;
            //}
            if (txtDate.Text.Trim() == "")
            {
                lblMessage.Text = "Enter From hrs.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select From Date.');");
                return false;
            }
            if (txtDateTo.Text.Trim() == "")
            {
                lblMessage.Text = "Enter To hrs.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select To Date.');");
                return false;
            }
            return true;
        }

        protected void lnkApprove_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            LinkButton lnkRequestNo = (LinkButton)sender;
            string EmpCode = ((Label)lnkRequestNo.NamingContainer.FindControl("lblCode")).Text;
            Session["EMPCODE"] = EmpCode;
            string EmpName = ((Label)lnkRequestNo.NamingContainer.FindControl("lblName")).Text;
            //string OT = ((Label)lnkRequestNo.NamingContainer.FindControl("lblOT")).Text;
            //string CO = ((Label)lnkRequestNo.NamingContainer.FindControl("lblCO")).Text;
            //txtDate.Text = "25-03-2023";
            //txtDateTo.Text = "24-04-2023";
            getOTAndCORptEmpWise();



        }

        private void getOTAndCORptEmpWise()
        {
            string EmpCode = Session["EMPCODE"].ToString();
            string FromDate = txtDate.Text;
            string ToDate = txtDateTo.Text;
            dsInv = objInv.GetOTAndCOReportEmpWise(EmpCode, FromDate, ToDate, "OT");
            if (dsInv.Tables.Count > 0)
            {
                txtOT.Text = dsInv.Tables[1].Rows[0]["OT"].ToString();
                txtCO.Text = dsInv.Tables[1].Rows[0]["CO"].ToString();
                trViewListOTCO.Visible = true;
            }
            if (dsInv.Tables[0].Rows.Count > 0)
            {
                trViewList.Visible = true;
                gvList.DataSource = dsInv.Tables[0];
                gvList.DataBind();
                gvList.HeaderRow.Cells[8].Visible = false;
                gvList.Columns[8].Visible = false;
                gvList.HeaderRow.Cells[9].Visible = false;
                gvList.Columns[9].Visible = false;

                gvList.HeaderRow.Cells[10].Visible = false;
                gvList.Columns[10].Visible = false;
                gvList.HeaderRow.Cells[11].Visible = false;
                gvList.Columns[11].Visible = false;

                gvList.HeaderRow.Cells[12].Visible = false;
                gvList.Columns[12].Visible = false;
            }
            else
            {
                trViewList.Visible = true;
                gvList.DataSource = null;
                gvList.DataBind();
            }
            trList.Visible = false;
            trViewList.Visible = true;
            trback.Visible = true;
            trViewListOTCO.Visible = true;
            trViewListByOTAndCOWise.Visible = true;
            trSerachBy.Visible = false;
            trStatusOTCO.Visible = true;

            tdApproval.Visible = false;
            tdRejected.Visible = false;
            tdPending.Visible = false;
            tdApproval.Visible = false;
            tdRejected.Visible = false;
            tdPending.Visible = false;
            trdate.Visible = true;
        }

        protected void lnkRectification_Click(object sender, EventArgs e)
        {


        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
            CheckBox chckheader = (CheckBox)gvList.HeaderRow.FindControl("chkSelectAll");
            foreach (GridViewRow row in gvList.Rows)
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

        protected void drpStatus_TextChanged(object sender, EventArgs e)
        {
            btnSubmit.Enabled = true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }

        protected void drpAction_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                LinkButton lnkRequestNo = (LinkButton)sender;
                string EmpCode = ((Label)lnkRequestNo.NamingContainer.FindControl("lblCode")).Text;
                string EmpName = ((Label)lnkRequestNo.NamingContainer.FindControl("lblName")).Text;
                string Date = ((Label)lnkRequestNo.NamingContainer.FindControl("lblDate")).Text;
                string TimeIN = ((Label)lnkRequestNo.NamingContainer.FindControl("lblTimeIN")).Text;
                string OT = ((Label)lnkRequestNo.NamingContainer.FindControl("lblOT")).Text;
                string CO = ((Label)lnkRequestNo.NamingContainer.FindControl("lblCO")).Text;
                string FromDate = txtDate.Text;
                string ToDate = txtDateTo.Text;
                DropDownList drpAction = ((DropDownList)lnkRequestNo.NamingContainer.FindControl("drpAction"));
                string Action = drpAction.SelectedValue;
                string Remarks = ((TextBox)lnkRequestNo.NamingContainer.FindControl("txtRemarks")).Text;
                string rptType = drpOtCOType.SelectedValue;
                if (rptType == "CO")
                {
                    if (CO != "0")
                    {
                        if (Action != "")
                        {
                            if (Remarks != "")
                            {
                                dsInv = objInv.UpDateOTAndCOReportEmpWise(EmpCode, FromDate, ToDate, TimeIN, EmpName, Date, OT, CO, Action, Remarks, rptType);
                                if (dsInv.Tables.Count > 0)
                                {
                                    txtOT.Text = dsInv.Tables[1].Rows[0]["OT"].ToString();
                                    txtCO.Text = dsInv.Tables[1].Rows[0]["CO"].ToString();
                                    gvList.DataSource = dsInv;
                                    gvList.DataBind();
                                    trList.Visible = false;
                                    trViewList.Visible = true;
                                    trback.Visible = true;
                                    trViewListByOTAndCOWise.Visible = true;
                                    trSerachBy.Visible = false;
                                    trViewListOTCO.Visible = true;
                                    trStatusOTCO.Visible = true;
                                    // tblActionType.Visible = true;
                                    // SendMail(EmpCode, FromDate, ToDate, EmpName, Date, OT, CO, Action, Remarks);
                                    getStatusOTCO();
                                    lblMessage.Text = Action + " OT Successfully.";
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Please Enter Remarks";
                            }
                        }
                        else if (Action == "")
                        {

                            dsInv = objInv.UpDateOTAndCOReportEmpWise(EmpCode, FromDate, ToDate, TimeIN, EmpName, Date, OT, CO, Action, Remarks, rptType);
                            if (dsInv.Tables.Count > 0)
                            {
                                txtOT.Text = dsInv.Tables[1].Rows[0]["OT"].ToString();
                                txtCO.Text = dsInv.Tables[1].Rows[0]["CO"].ToString();
                                gvList.DataSource = dsInv;
                                gvList.DataBind();
                                trList.Visible = false;
                                trViewList.Visible = true;
                                trback.Visible = true;
                                trViewListByOTAndCOWise.Visible = true;
                                trSerachBy.Visible = false;
                                trViewListOTCO.Visible = true;
                                trStatusOTCO.Visible = true;
                                // tblActionType.Visible = true;
                                getStatusOTCO();
                                lblMessage.Text = Action + " OT Successfully.";
                            }
                        }
                    }
                    else
                    {
                        lblMessage.Text = "For taking action CO value should be greater than zero ";
                    }
                }
                else if (rptType == "OT")
                {
                    if (OT != "0")
                    {
                        if (Action != "")
                        {
                            if (Remarks != "")
                            {
                                dsInv = objInv.UpDateOTAndCOReportEmpWise(EmpCode, FromDate, ToDate, TimeIN, EmpName, Date, OT, CO, Action, Remarks, rptType);
                                if (dsInv.Tables.Count > 0)
                                {
                                    txtOT.Text = dsInv.Tables[1].Rows[0]["OT"].ToString();
                                    txtCO.Text = dsInv.Tables[1].Rows[0]["CO"].ToString();
                                    gvList.DataSource = dsInv;
                                    gvList.DataBind();
                                    trList.Visible = false;
                                    trViewList.Visible = true;
                                    trback.Visible = true;
                                    trViewListByOTAndCOWise.Visible = true;
                                    trSerachBy.Visible = false;
                                    trViewListOTCO.Visible = true;
                                    trStatusOTCO.Visible = true;
                                    // tblActionType.Visible = true;
                                    // SendMail(EmpCode, FromDate, ToDate, EmpName, Date, OT, CO, Action, Remarks);
                                    getStatusOTCO();
                                    lblMessage.Text = Action + " OT Successfully.";
                                }
                            }
                            else
                            {
                                lblMessage.Text = "Please Enter Remarks";
                            }
                        }
                        else if (Action == "")
                        {

                            dsInv = objInv.UpDateOTAndCOReportEmpWise(EmpCode, FromDate, ToDate, TimeIN, EmpName, Date, OT, CO, Action, Remarks, rptType);
                            if (dsInv.Tables.Count > 0)
                            {
                                txtOT.Text = dsInv.Tables[1].Rows[0]["OT"].ToString();
                                txtCO.Text = dsInv.Tables[1].Rows[0]["CO"].ToString();
                                gvList.DataSource = dsInv;
                                gvList.DataBind();
                                trList.Visible = false;
                                trViewList.Visible = true;
                                trback.Visible = true;
                                trViewListByOTAndCOWise.Visible = true;
                                trSerachBy.Visible = false;
                                trViewListOTCO.Visible = true;
                                trStatusOTCO.Visible = true;
                                // tblActionType.Visible = true;
                                getStatusOTCO();
                                lblMessage.Text = Action + " OT Successfully.";
                            }
                        }

                    }
                    else
                    {
                        lblMessage.Text = "For taking action OT value should be greater than zero ";
                    }
                }







            }
            catch (Exception ex)
            {

            }
        }

        //private void SendMail(string empCode, string fromDate, string toDate, string empName, string date, string oT, string cO, string action, string remarks)
        //{
        //    emailSend = new ESS.Email();
        //    DataSet dsMail = emailSend.GetTelEmpDetails((string)Session["sCompID"], (string)Session["sEmpID"], "AD000207");
        //    if (dsMail.Tables[0].Rows[0]["CLIENTEMAIL"].ToString() != "")
        //    {
        //        if (dsMail.Tables[3].Rows[0]["CHECKERMAIL"].ToString() != "")
        //        {
        //            string clientbodys = "Dear " + dsMail.Tables[0].Rows[0]["EMP_FNAME"].ToString() + ",\n\nYour Attendance OT OR CO on this date is approve   :-" + dsMail.Tables[1].Rows[0]["APPLICATION_NO"].ToString() + "  Send Successful.\n"
        //               + "Please wait for Approvel. We will Notify On Mail or Can You Check Hrrl Portal Site.\nThankYou.\n\nWith Best Regards,\nHPCL Rajasthan Refinery Limited";
        //            string emails = dsMail.Tables[0].Rows[0]["CLIENTEMAIL"].ToString();
        //            string subjects = "For Telephone Reimbursement Claim";
        //            string checkerbodys = "Dear Sir/Madam,\n\nPlease Check than Approve Telephone Reimbursement Claim.\n"
        //            + "Telephone Reimbursement Claim  Application ID is-:" + dsMail.Tables[1].Rows[0]["APPLICATION_NO"].ToString() + "\n"
        //                + "\nThankYou.\n\nWith Best Regards,\n" + dsMail.Tables[0].Rows[0]["EMP_FNAME"].ToString() + "";

        //            emailSend.SendEmailBT(emails, subjects, clientbodys);
        //            for (int i = 0; Convert.ToBoolean(dsMail.Tables[2].Rows.Count); i++)
        //            {
        //                if (dsMail.Tables[2].Rows.Count != i)
        //                {
        //                    String Emp_Aid = dsMail.Tables[2].Rows[i]["EMP_AID"].ToString();
        //                    DataSet dsTelMail = emailSend.GetTelEmpDetails((string)Session["sCompID"], Emp_Aid, "AD000207");

        //                    string emailChecker = dsMail.Tables[3].Rows[0]["CHECKERMAIL"].ToString();
        //                    emailSend.SendEmailBT(emailChecker, subjects, checkerbodys);
        //                }
        //                else
        //                {
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //}

        protected void drpOtCOType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpOtCOType.SelectedValue == "OT")
            {
                dsInv = objInv.GetReport((string)Session["sCompID"], (string)Session["EMPCODE"], txtDate.Text, txtDateTo.Text, "OT");
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    gvList.DataSource = dsInv;
                    gvList.DataBind();
                    gvList.HeaderRow.Cells[9].Visible = false;
                    gvList.Columns[9].Visible = false;
                    gvList.HeaderRow.Cells[8].Visible = true;
                    gvList.Columns[8].Visible = true;
                    gvList.HeaderRow.Cells[10].Visible = true;
                    gvList.Columns[10].Visible = true;
                    gvList.HeaderRow.Cells[11].Visible = true;
                    gvList.Columns[11].Visible = true;
                    gvList.HeaderRow.Cells[12].Visible = true;
                    gvList.Columns[12].Visible = true;
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                }

                trList.Visible = false;
                trViewList.Visible = true;
                trback.Visible = true;
                trViewListOTCO.Visible = true;
                trViewListByOTAndCOWise.Visible = true;
                trSerachBy.Visible = false;
                tblActionType.Visible = false;
                trStatusOTCO.Visible = true;



            }
            else if (drpOtCOType.SelectedValue == "CO")
            {
                dsInv = objInv.GetReport((string)Session["sCompID"], (string)Session["EMPCODE"], txtDate.Text, txtDateTo.Text, "CO");
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    gvList.DataSource = dsInv;
                    gvList.DataBind();
                    gvList.HeaderRow.Cells[9].Visible = false;
                    gvList.Columns[9].Visible = false;
                    gvList.HeaderRow.Cells[8].Visible = true;
                    gvList.Columns[8].Visible = true;
                    gvList.HeaderRow.Cells[10].Visible = true;
                    gvList.Columns[10].Visible = true;
                    gvList.HeaderRow.Cells[11].Visible = true;
                    gvList.Columns[11].Visible = true;
                    gvList.HeaderRow.Cells[12].Visible = true;
                    gvList.Columns[12].Visible = true;
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                }
                trList.Visible = false;
                trViewList.Visible = true;
                trback.Visible = true;
                trViewListOTCO.Visible = true;
                trViewListByOTAndCOWise.Visible = true;
                trSerachBy.Visible = false;
                tblActionType.Visible = false;
                trStatusOTCO.Visible = true;


            }
            else if (drpOtCOType.SelectedValue == "")
            {
                dsInv = objInv.GetReport((string)Session["sCompID"], (string)Session["EMPCODE"], txtDate.Text, txtDateTo.Text, "");
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    gvList.DataSource = dsInv;
                    gvList.DataBind();
                    gvList.HeaderRow.Cells[9].Visible = false;
                    gvList.Columns[9].Visible = false;
                    gvList.HeaderRow.Cells[8].Visible = true;
                    gvList.Columns[8].Visible = true;
                    gvList.HeaderRow.Cells[10].Visible = true;
                    gvList.Columns[10].Visible = true;
                    gvList.HeaderRow.Cells[11].Visible = true;
                    gvList.Columns[11].Visible = true;
                    gvList.HeaderRow.Cells[12].Visible = true;
                    gvList.Columns[12].Visible = true;
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                }
                trList.Visible = false;
                trViewList.Visible = true;

                trViewListOTCO.Visible = true;
                trViewListByOTAndCOWise.Visible = true;
                trback.Visible = true;
                trSerachBy.Visible = false;
                trStatusOTCO.Visible = true;


                tblActionType.Visible = false;


            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            dsInv = objInv.GetOTCOReport((string)Session["sCompID"], (string)Session["sEmpID"], txtDate.Text, txtDateTo.Text);
            gvLeave.DataSource = dsInv;
            gvLeave.DataBind();
            trList.Visible = true;
            trViewList.Visible = false;
            trViewListOTCO.Visible = false;
            tblActionType.Visible = false;
            trViewListByOTAndCOWise.Visible = false;
            trback.Visible = true;
            trSerachBy.Visible = true;
            trStatusOTCO.Visible = false;
            trback.Visible = false;
            trdate.Visible = false;
        }

        protected void drpStatusOTCO_SelectedIndexChanged(object sender, EventArgs e)
        {
            getStatusOTCO();

        }

        private void getStatusOTCO()
        {
            if (drpStatusOTCO.SelectedValue == "Hide Status")
            {
                tdApproval.Visible = false;
                tdRejected.Visible = false;
                tdPending.Visible = false;

            }
            else if (drpStatusOTCO.SelectedValue == "Show Status")
            {
                dsInv = objInv.GetOTCOStatus((string)Session["sCompID"], (string)Session["EMPCODE"], txtDate.Text, txtDateTo.Text);
                tdApproval.Visible = true;
                tdRejected.Visible = true;
                tdPending.Visible = true;
                txtApprovalOT.Text = dsInv.Tables[0].Rows[0]["ApproveOT"].ToString();
                txtApprovalCO.Text = dsInv.Tables[3].Rows[0]["ApproveCO"].ToString();
                txtRejectedOT.Text = dsInv.Tables[1].Rows[0]["RejectOT"].ToString();
                txtRejectedCO.Text = dsInv.Tables[4].Rows[0]["RejectCO"].ToString();
                txtPendingOT.Text = dsInv.Tables[2].Rows[0]["pendingOT"].ToString();
                txtPendingCO.Text = dsInv.Tables[5].Rows[0]["pendingCO"].ToString();
            }
        }

        protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if ((string)Session["sCompID"].ToString() == "CO000142")
                {

                    gvLeave.HeaderRow.Cells[2].Visible = false;
                    gvLeave.Columns[2].Visible = false;
                }

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            objNps.EmpName = txtSearchName.Text;
            dsInv = objNps.getName((string)Session["sCompID"], (string)Session["sEmpID"]);
            gvLeave.DataSource = dsInv;
            gvLeave.DataBind();
            trList.Visible = true;
            trViewList.Visible = false;
            trViewListOTCO.Visible = false;
            tblActionType.Visible = false;
            trViewListByOTAndCOWise.Visible = false;
            trSerachBy.Visible = true;
            trback.Visible = false;
            trStatusOTCO.Visible = false;
            trdate.Visible = false;
            trdate.Visible = false;

        }
    }
}