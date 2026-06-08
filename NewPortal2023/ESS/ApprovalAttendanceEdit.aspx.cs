using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class ApprovalAttendanceEdit : System.Web.UI.Page
    {
        NewPortal2023.ESS.LeaveApproval objLA = new NewPortal2023.ESS.LeaveApproval();
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
                    FillPendingAttendance();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
        }

        private void FillPendingAttendance()
        {
            //lblMessage.Text = "";
            //dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            //gvLeave.DataSource = dsInv;
            //gvLeave.DataBind();

            dsInv = objInv.GETATTENDAPPROVAL((string)Session["sCompID"], (string)Session["sEmpID"]);
            //dsInv = objInv.GetAttendanceEditDetails((string)Session["sCompID"], (string)Session["EmpCode"]);
            gvLeave.DataSource = dsInv;
            gvLeave.DataBind();

        }

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox chckheader = (CheckBox)gvLeave.HeaderRow.FindControl("chkSelectAll");
            foreach (GridViewRow row in gvLeave.Rows)
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


        private string MakeCTCXml(GridView GV, string strType)
        {

            StringBuilder sbTaxDetails = new StringBuilder();
            string xmlInv = string.Empty;

            sbTaxDetails.Append("<ROOT>");

            foreach (GridViewRow gvr in GV.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true && ((CheckBox)gvr.FindControl("chkSelect")).Visible == true)
                {

                    sbTaxDetails.Append("<Attendance COMP_AID='" + (string)Session["sCompID"] + "'");
                    sbTaxDetails.Append(" EmpCode='" + ((Label)gvr.FindControl("lblEmpCode")).Text.Trim() + "'");
                    //sbTaxDetails.Append(" EmpName='" + ((Label)gvr.FindControl("lblEmpName")).Text.Trim() + "'");
                    sbTaxDetails.Append(" Date='" + ((Label)gvr.FindControl("lblDate")).Text.Trim() + "'");
                    sbTaxDetails.Append(" Shift='" + ((Label)gvr.FindControl("lblShift")).Text.Trim() + "'");
                    sbTaxDetails.Append(" TimeIn='" + ((Label)gvr.FindControl("lblTimeIn")).Text.Trim() + "'");
                    sbTaxDetails.Append(" TimeOut='" + ((Label)gvr.FindControl("lblTimeOut")).Text.Trim() + "'/>");
                    //sbTaxDetails.Append(" Status='" + "1" + "'/>");

                }

            }
            sbTaxDetails.Append("</ROOT>");

            xmlInv = sbTaxDetails.ToString();

            return xmlInv;


        }

        protected void gvLeave_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvLeave.EditIndex = e.NewEditIndex;
            this.FillPendingAttendance();
        }

        protected void gvLeave_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = gvLeave.Rows[e.RowIndex];
            int EntryAId = Convert.ToInt32(gvLeave.DataKeys[e.RowIndex].Values[0]);
            DropDownList drpactiontype = (row.FindControl("grddrpAction") as DropDownList);
            string txtRemark = (row.FindControl("txtRemarks") as TextBox).Text;
            Label lblupdateShift = (row.FindControl("lblupdateShift") as Label);
            Label lblUpdateTimeIn = (row.FindControl("lblUpdateTimeIn") as Label);
            Label lblUpdateTimeOut = (row.FindControl("lblUpdateTimeOut") as Label);
            Label lblUpdateOT = (row.FindControl("lblUpdateOT") as Label);
            Label lblUpdateCO = (row.FindControl("lblUpdateCO") as Label);
            Label lblEntryAId = (row.FindControl("lblEntryAid") as Label);

            string Remark = txtRemark;
            string action = drpactiontype.SelectedItem.Value;
            objInv.Shift = lblupdateShift.Text;
            objInv.From = lblUpdateTimeIn.Text;
            objInv.To = lblUpdateTimeOut.Text;
            objInv.OT = lblUpdateOT.Text;
            objInv.CO = lblUpdateCO.Text;
            if (drpactiontype.SelectedItem.Value != "")
            {

                if (action == "Approve")
                {
                    if (objInv.UpdateAttendanceStatus(EntryAId.ToString(), Remark, "1", (string)Session["sCompID"], (string)Session["sEmpID"]) == false)
                    {
                        lblMessage.Text = "Error occurred in application.";
                        return;
                    }
                    lblMessage.Text = "Attendance Successfuly Approved.";
                }
                else if (action == "Reject")
                {
                    if (Remark.ToString() != "")
                    {
                        if (objInv.UpdateAttendanceStatus(EntryAId.ToString(), Remark, "0", (string)Session["sCompID"], (string)Session["sEmpID"]) == false)
                        {
                            lblMessage.Text = "Error occurred in application.";
                            return;
                        }
                        lblMessage.Text = "Attendance Rejected.";
                    }
                    else
                    {

                        lblMessage.Text = "Please insert Remaks";
                        return;
                    }
                }
                gvLeave.EditIndex = -1;
                this.FillPendingAttendance();
            }
            else
            {

                lblMessage.Text = "Please Select Action";
                return;
            }

        }
        protected void btnReject_Click(object sender, EventArgs e)
        {
            //if (objInv.UpdateAttendanceStatus("", txtAllRemarks.ToString(), "0", (string)Session["sCompID"], (string)Session["sEmpID"]) == false)
            //{
            //    lblMessage.Text = "Error occurred in application.";
            //    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating Flexi Pay details.');");
            //    return;
            //}
            //FillPendingAttendance();
            //lblMessage.Text = "Attendance Rejected.";
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string status = "";
            string Message = "";


            string Remark = txtAllRemarks.Text;
            if (drpAllAction.SelectedValue != "")
            {

                if (drpAllAction.SelectedValue == "Approve")
                {
                    status = "1"; ;
                    Message = "Attendance Approved Successfully .";
                }
                else if (drpAllAction.SelectedValue == "Reject")
                {
                    status = "0";
                    Message = "Attendance Rejected.";
                }
                if (Remark.ToString() != "")
                {
                    if (objInv.UpdateAttendanceWithoutEntryAid("", Remark, status, (string)Session["sCompID"], (string)Session["sEmpID"]) == false)
                    {
                        lblMessage.Text = "Error occurred in application.";
                        return;
                    }
                }
                else
                {

                    lblMessage.Text = "Please insert Remaks";
                    return;
                }

                FillPendingAttendance();
                lblMessage.Text = Message;

            }
            else
            {

                lblMessage.Text = "Please Select Action";
                return;
            }

        }
        protected void gvLeave_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string var = "Alternate, Edit";
            string var1 = e.Row.RowState.ToString();
            if (var == var1)
            {
                DropDownList drpactiontype = e.Row.FindControl("grddrpAction") as DropDownList;
                TextBox txtRemark = e.Row.FindControl("txtRemarks") as TextBox;
                drpactiontype.Enabled = true;
                txtRemark.Enabled = true;
            }
            if (e.Row.RowType == DataControlRowType.DataRow && e.Row.RowState == DataControlRowState.Edit)
            {

                DropDownList drpactiontype = e.Row.FindControl("grddrpAction") as DropDownList;
                TextBox txtRemark = e.Row.FindControl("txtRemarks") as TextBox;
                drpactiontype.Enabled = true;
                txtRemark.Enabled = true;
            }
        }

        protected void gvLeave_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvLeave.EditIndex = -1;
            this.FillPendingAttendance();
        }



        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSelect.Checked == true)
            {
                foreach (GridViewRow rows in gvLeave.Rows)
                {
                    if (rows.RowType == DataControlRowType.DataRow)
                    {
                        rows.Cells[17].Enabled = false;
                        rows.Cells[18].Enabled = false;
                        rows.Cells[21].Enabled = false;
                        rows.Cells[22].Enabled = false;
                        drpAllAction.Enabled = true;
                        txtAllRemarks.Enabled = true;
                    }
                }
            }
            else
            {
                foreach (GridViewRow rows in gvLeave.Rows)
                {
                    if (rows.RowType == DataControlRowType.DataRow)
                    {
                        rows.Cells[17].Enabled = true;
                        rows.Cells[18].Enabled = true;
                        rows.Cells[21].Enabled = true;
                        rows.Cells[22].Enabled = true;
                        drpAllAction.Enabled = false;
                        txtAllRemarks.Enabled = false;
                        txtAllRemarks.Text = "";
                        drpAllAction.SelectedValue = "";
                    }
                }
            }
        }

        protected void lnkView_Click(object sender, EventArgs e)
        {
            LinkButton lnkRequestNo = (LinkButton)sender;
            string lblEmpCode = ((Label)lnkRequestNo.NamingContainer.FindControl("lblEmpCode")).Text;
            string lblDate = ((Label)lnkRequestNo.NamingContainer.FindControl("lblDate")).Text;
            objNps.EmpCode = lblEmpCode;
            objNps.Date = lblDate;
            dsInv = objNps.GETAPPROVALOTANDCO((string)Session["sCompID"], (string)Session["sEmpID"]);
            if (dsInv.Tables[0].Rows.Count > 0)
            {
                lblEmpCodes.Text = dsInv.Tables[0].Rows[0]["EMP_MID"].ToString();
                lblAppOT.Text = dsInv.Tables[0].Rows[0]["OT"].ToString();
                lblAppCO.Text = dsInv.Tables[0].Rows[0]["CO"].ToString();
                if (lblAppOT.Text == "" && lblAppCO.Text == "")
                {
                    lblAppOT.Text = "0";
                    lblAppCO.Text = "0";
                }
                //lblEmpCodes.Text= dsInv.Tables[0].Rows[0]["EMP_CODE"].ToString();
                //if (dsInv.Tables[1].Rows.Count > 0)
                //{
                //    gvAbsectList.DataSource = dsInv.Tables[1];
                //    gvAbsectList.DataBind();
                //}
                //else
                //{
                //    gvAbsectList.DataSource = null;
                //    gvAbsectList.DataBind();
                //}
            }
            string title = "SUM OF OT AND CO";
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
        }

        /// <summary>
        /// //////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkApp_Click(object sender, EventArgs e)
        {
            int count = 0;
            if (ViewState["countadd"] != null)
            {
                count += Convert.ToInt32(ViewState["countadd"].ToString()) + 1;
            }
            else
            {
                count = 1;
            }
            int countadd = count % 2;
            ViewState["countadd"] = count;
            if (countadd == 0)
            {
                divApproTCO.Visible = false;
            }
            else
            {
                divApproTCO.Visible = true;
                FILLApprovedList();
            }
        }

        private void FILLApprovedList()
        {
            DataSet ds = new DataSet();


            ds = objLA.GetApprAttendOTCO((string)Session["sCompID"], (string)Session["sEmpID"]);
            if (ds.Tables.Count > 0)
            {
                grApprOTCO.DataSource = ds;
                grApprOTCO.DataBind();
            }
            else
            {
                grApprOTCO.DataSource = null;
                grApprOTCO.DataBind();
            }
        }

        protected void gvLeave_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.FillPendingAttendance();
        }
    }
}