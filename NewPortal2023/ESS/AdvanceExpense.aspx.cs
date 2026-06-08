using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class AdvanceExpense : System.Web.UI.Page
    {
        NewPortal2023.ESS.LocalExpenses objLoc = new NewPortal2023.ESS.LocalExpenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsExp = new DataSet();
        private string SourcePath = string.Empty;
        private string savePath = string.Empty;
        private string SourcePath2 = string.Empty;
        private string savePath2 = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sCompID"] != null)
            {
                if (!Page.IsPostBack)
                {

                    ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                    Session["Entry_aid"] = null;

                    getAdvanceData();
                    //getCategoryType();
                    divAlert.Visible = false;


                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }

        }

        private void getAdvanceData()
        {
            try
            {
                // getCategoryType();
                dsExp = objLoc.GetAdvanceList(Convert.ToString(Session["sCompID"]));
                if (dsExp.Tables[0].Rows.Count > 0)
                {
                    this.gvAdvanceClaimList.DataSource = dsExp.Tables[0];
                    this.gvAdvanceClaimList.DataBind();

                }
                else
                {
                    this.gvAdvanceClaimList.DataSource = null;
                    this.gvAdvanceClaimList.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            SectionList.Visible = false;
            divAlert.Visible = false;
            divFrom.Visible = true;
            btnSave.Visible = true;
            txtDate.Text = "";
            txtempcode.Text = "";
            txtname.Text = "";
            txtpuradv.Text = "";
            txtadvamt.Text = "";
            txtDate.Enabled = true;
            txtname.Enabled = true;
            drptype.SelectedValue = "";
            txtempcode.Enabled = true;
            txtpuradv.Enabled = true;
            txtadvamt.Enabled = true;

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                objLoc.Expenses_Date = txtDate.Text;
                objLoc.EmpCode = txtempcode.Text;
                objLoc.EmpNAME = txtname.Text;
                objLoc.expensetype = drptype.SelectedValue;
                objLoc.Travel_Description = txtpuradv.Text;
                objLoc.advance = txtadvamt.Text;
                objLoc.Cash_Voucher = txtvoucher.Text;


                objLoc.FilingStatus = "S";
                objLoc.Status = "9";
                if (Session["Entry_aid"] != null)
                {
                    objLoc.EntryAid = Session["Entry_aid"].ToString();
                }


                dsExp = objLoc.InsertAdvanceClaim(Convert.ToString(Session["sCompID"]));

                if (dsExp.Tables.Count > 0)
                {
                    string entryCode = dsExp.Tables[0].Rows[0]["ADVANCE_ID"].ToString();
                    btnSave.Visible = false;
                    string script = $@"<script type='text/javascript'>alert('Advance Claim Submitted Successfully.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    objcommon.SetMessageColor(divAlert, "success");
                    lblMessage.Visible = true;
                    lblMessage.Text = "Advance Claim Submitted Successfully.";
                    SectionList.Visible = true;
                    divFrom.Visible = false;
                    divAlert.Visible = true;

                }

            }
            catch (Exception ex)
            {

            }

        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            divFrom.Visible = false;

            SectionList.Visible = true;

        }


        protected void lnkadvClmNoClmNo_Click(object sender, EventArgs e)
        {
            LinkButton lnkDOMClmNo = (LinkButton)sender;

            string entryAid = ((LinkButton)lnkDOMClmNo.NamingContainer.FindControl("lnkadvClmNoClmNo")).Text;
            Label txtstatus = (Label)lnkDOMClmNo.NamingContainer.FindControl("txtstatus");



            objLoc.AppNo = entryAid;
            Session["Entry_aid"] = entryAid;

            dsExp = objLoc.getAdvanceClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
            if (dsExp.Tables.Count > 0)
            {
                SectionList.Visible = false;
                divFrom.Visible = true;

                //string categoryType = dsExp.Tables[0].Rows[0]["Category_AId"].ToString();
                //string desgAid = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();


                txtDate.Text = dsExp.Tables[0].Rows[0]["ADVANCE_DATE"].ToString();
                txtname.Text = dsExp.Tables[0].Rows[0]["EMP_NAME"].ToString();

                txtempcode.Text = dsExp.Tables[0].Rows[0]["EMP_CODE"].ToString();
                drptype.Text = dsExp.Tables[0].Rows[0]["EXPENSE_TYPE"].ToString();
                txtadvamt.Text = dsExp.Tables[0].Rows[0]["ADVANCE_AMOUNT"].ToString();
                txtpuradv.Text = dsExp.Tables[0].Rows[0]["ADVANCE_PURPOSE"].ToString();
                txtvoucher.Text = dsExp.Tables[0].Rows[0]["ADVANCE_VOUCHER"].ToString();
                //Calculatetotalexp();
                //txtTotalexp.Text = txtTotalexp.Text;

                // dsExp = objLoc.GetLocalReimb(Convert.ToString(Session["sCompID"]), Convert.ToString(Session["CATEGORY_TYPE"]));


                txtDate.Enabled = false;
                txtname.Enabled = false;
                txtempcode.Enabled = false;
                drptype.Enabled = false;
                txtadvamt.Enabled = false;
                txtpuradv.Enabled = false;
                //txtUserRemarks.Enabled = false;

                btnSave.Visible = false;

            }
        }

        //protected void gvLocalClaimList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {

        //            Label txtRmk = (Label)e.Row.FindControl("txtRmk");
        //            //TextBox txtdescri = (TextBox)e.Row.FindControl("txtdescri");
        //            DropDownList ddls = (DropDownList)e.Row.FindControl("drpAction");
        //            Label lblEmpAId = (Label)e.Row.FindControl("lblEmpAId");
        //            Label txtClmAmt = (Label)e.Row.FindControl("txtClmAmt");
        //            Label txtAppAmtr = (Label)e.Row.FindControl("txtAppAmtr");
        //            Label EntertainmentChked = (Label)e.Row.FindControl("EntertainmentChked");
        //            LinkButton lnkLOCClmNoClmNo = (LinkButton)e.Row.FindControl("lnkLOCClmNoClmNo");
        //            //DataSet ds = new DataSet();
        //            //ds = objLoc.GetDOMLimit(lblEmpAId.Text, lnkDOMClmNoClmNo.Text, Convert.ToString(Session["sCompID"]));
        //            if (txtAppAmtr.Text != "")
        //            {
        //                if (EntertainmentChked.Text == "T")
        //                {
        //                    TableCell cell = e.Row.Cells[2];
        //                    cell.BackColor = System.Drawing.Color.Orange;

        //                }
        //                else if (EntertainmentChked.Text == "F")
        //                {
        //                    TableCell cell = e.Row.Cells[2];
        //                    cell.BackColor = System.Drawing.Color.LightGreen;
        //                }
        //            }
        //            else
        //            {

        //            }


        //            txtRmk.BackColor = (txtRmk.Text.Trim() == "1" ? System.Drawing.Color.LightGray : txtRmk.BackColor);
        //            if (txtRmk.Text == "Submitted")
        //            {
        //                txtRmk.Text = " Submitted.";
        //            }
        //            else if (txtRmk.Text == "Approved")
        //            {
        //                txtRmk.Text = "Claim Approved.";
        //            }
        //            else if (txtRmk.Text == "Rejected")
        //            {
        //                txtRmk.Text = "Claim Rejected.";
        //            }
        //            else if (txtRmk.Text == "ReChecked")
        //            {
        //                txtRmk.Text = " Claim Under ReCheck.";
        //            }
        //            else if (txtRmk.Text == "Reverted")
        //            {
        //                txtRmk.Text = " Claim Reverted.";
        //            }
        //            else
        //            {
        //                txtRmk.Text = " Pending With " + txtRmk.Text + " .";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        protected void txtempcode_TextChanged(object sender, EventArgs e)
        {
            objLoc.EmpCode = txtempcode.Text;
            dsExp = objLoc.GetEmpName(Convert.ToString(Session["sCompID"]));
            if (dsExp.Tables[0].Rows.Count > 0)
            {
                txtname.Text = dsExp.Tables[0].Rows[0]["EMP_NAME"].ToString();

            }

        }
    }
}