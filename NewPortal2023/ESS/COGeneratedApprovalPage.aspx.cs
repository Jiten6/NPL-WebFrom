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
    public partial class COGeneratedApprovalPage : System.Web.UI.Page
    {
        NewPortal2023.ESS.NplCoApp objNpl = new NewPortal2023.ESS.NplCoApp();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.DBUtility obdbutility = new NewPortal2023.ESS.DBUtility();
        DataSet dsInv = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["sCompID"] != null)
                {
                    FillPendingCoList();

                }
            }
        }

        private void FillPendingCoList()
        {
            //lblMessage.Text = "";
            //dsInv = objInv.GetLeaveDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            //gvLeave.DataSource = dsInv;
            //gvLeave.DataBind();

            dsInv = objNpl.GetApproverCOList((string)Session["sCompID"], (string)Session["sEmpID"]);
            //dsInv = objInv.GetAttendanceEditDetails((string)Session["sCompID"], (string)Session["EmpCode"]);
            gvLeave.DataSource = dsInv;
            gvLeave.DataBind();

        }

        //protected void gvLeave_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    gvLeave.EditIndex = e.NewEditIndex;
        //    this.FillPendingCoList();
        //}

    

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
            this.FillPendingCoList();
        }


        protected void gvLeave_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            

        }


        protected void lnkSubmit_Click(object sender, EventArgs e)
        {
            LinkButton lnkSubmit = (LinkButton)sender;
            string CID = ((Label)lnkSubmit.NamingContainer.FindControl("lblId")).Text;

           
            DropDownList drpactiontype = (lnkSubmit.NamingContainer.FindControl("grddrpAction") as DropDownList);
            string txtRemark = (lnkSubmit.NamingContainer.FindControl("txtRemarks") as TextBox).Text;

            Label lblEntryAId = (lnkSubmit.NamingContainer.FindControl("lblId") as Label);

            string Remark = txtRemark;
            string action = drpactiontype.SelectedItem.Value;

            if (drpactiontype.SelectedItem.Value != "")
            {

                if (action == "Approve")
                {
                    if (Remark.ToString() != "")
                    {
                        if (objNpl.UpdateAttendanceStatus((string)Session["sCompID"], (string)Session["sEmpID"], Remark, "1", CID.ToString()) == false)
                        {
                            divAlertDan.Visible = true;
                            divAlert.Visible = false;
                            lblMessageDan.Text = "Error occurred in application.";
                            return;
                        }
                        lblMessage.Text = "CO Approved Successfuly.";
                        divAlert.Visible = true;
                        divAlertDan.Visible = false;
                    }
                    else
                    {
                        divAlertDan.Visible = true;
                        divAlert.Visible = false;
                        lblMessageDan.Text = "Please insert Remaks";
                        return;
                    }
                }
                else if (action == "Reject")
                {
                    if (Remark.ToString() != "")
                    {
                        if (objNpl.UpdateAttendanceStatus((string)Session["sCompID"], (string)Session["sEmpID"], Remark, "0", CID.ToString()) == false)
                        {
                            divAlertDan.Visible = true;
                            divAlert.Visible = false;
                            lblMessageDan.Text = "Error occurred in application.";
                            return;
                        }
                        lblMessage.Text = "CO Rejected.";
                        divAlert.Visible = true;
                        divAlertDan.Visible = false;
                    }
                    else
                    {
                        divAlertDan.Visible = true;
                        divAlert.Visible = false;
                        lblMessageDan.Text = "Please insert Remaks";
                        return;
                    }
                }
                //gvLeave.EditIndex = -1;
                this.FillPendingCoList();
            }
            else
            {
                divAlertDan.Visible = true;
                divAlert.Visible = false;
                lblMessageDan.Text = "Please Select Action";
                return;
            }
        }
    }
}