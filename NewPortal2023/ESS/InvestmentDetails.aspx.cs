using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Drawing;
using System.Net;
using HBS.Encoder;

namespace NewPortal2023.ESS
{
    public partial class InvestmentDetails : System.Web.UI.Page
    {
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();
        NewPortal2023.ESS.Investments objInv = new NewPortal2023.ESS.Investments();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsInv = new DataSet();
        DataSet DsTest = new DataSet();
        DataSet dsrg = new DataSet();
        ArrayList arrFor = new ArrayList();
        private string savePath = string.Empty;
        NewPortal2023.ESS.Tax objTax = new NewPortal2023.ESS.Tax();

        List<string> ignoreForUploadCheck = new List<string>
        {
            "CO000056",
            "CO000061",
            "CO000078",
            "CO000125",
            "CO000155",
        };

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");

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

                            if (Session["MenuList"] != null && !((ArrayList)Session["MenuList"]).Contains("Investment Declaration"))
                            {
                                if (Session["MenuList"] != null && !((ArrayList)Session["MenuList"]).Contains("Investment Details"))
                                {
                                    //Response.Redirect("Logout.aspx", false);
                                }
                            }
                            if ((string)Session["sCompID"] == "CO000142")
                            {
                                FillCTC();
                                btnSaveOption.Visible = true;
                                trPe1.Visible = true;
                                trPe2.Visible = true;
                                trPe3.Visible = true;
                                trPe4.Visible = true;
                            }
                            else
                            {
                                trPe1.Visible = false;
                                trPe2.Visible = false;
                                trPe3.Visible = false;
                                trPe4.Visible = false;
                            }

                            FillAll();
                            ViewState["chk80EE"] = false;

                            btnPrintIndiafirst.Visible = false;
                            updBtnTable.Visible = false;
                            lnkDownload.Visible = false;
                            lnkManual.Visible = false;
                            lnkDeclarationsAngel.Visible = false;

                            if ((string)Session["sCompID"] == "CO000045" || (string)Session["sCompID"] == "CO000126" || (string)Session["sCompID"] == "CO000141")
                            {
                                Table5.Visible = true;
                            }
                            if ((string)Session["sCompID"] == "CO000045")
                            {
                                Table5.Visible = true;
                                btnPrintIndiafirst.Visible = true;
                            }

                            if ((string)Session["sCompID"] == "CO000141")
                            {
                                Note.Visible = false;
                                SelTaxOpt.Visible = false;
                                lnkManual.Visible = false;
                                btnPrintIndiafirst.Visible = false;
                                lnkUpdateOther.Visible = false;
                                lnkUpdate.Visible = false;
                                lnkUpdateRent.Visible = false;
                                lnkUpdate12.Visible = false;
                                lnkUpdate12B.Visible = false;

                            }

                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = "Error occurred in application.";
                            //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
                        }
                    }
                    else
                    {
                        if (Request.QueryString["key"] != null)
                        {
                            Session["Error"] = Request.QueryString["key"].Replace(" ", "+") + "<br>Employee does not exists.";
                            Response.Redirect("../ErrorPage.aspx", true);
                        }
                        else
                        {
                            lblMessage.Text = "Error occurred in application.";
                        }
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                if (Request.QueryString["key"] != null)
                {
                    Session["Error"] = ex.Message + "<br>InvestmentDetails.aspx<br>" + Request.QueryString["key"].Replace(" ", "+");
                    Response.Redirect("../ErrorPage.aspx", true);
                }
                else
                {
                    lblMessage.Text = "Error occurred in application.";
                }
            }
        }

        protected void lnkFAQ_Click(object sender, EventArgs e)
        {
            try
            {
                if ((string)Session["sCompID"] == "CO000141")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/NPL/FAQ_Investment_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=FAQ_Investment_Proofs.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
            }
        }

        private void FillInvestments()
        {
            dsInv = objInv.GetInvestmentDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            gvInv.DataSource = dsInv;
            gvInv.DataBind();
        }
        private void Fill_Amounts()
        {
            objInv.GetAmountDetails((string)Session["sCompID"], (string)Session["sEmpID"], lblInvDetails, lnkRent, lnkRentNew, lnk12, lnk12b);
        }
        private void FillOther()
        {
            dsInv = objInv.GetOtherDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            gvOther.DataSource = dsInv;
            gvOther.DataBind();

            foreach (GridViewRow gvr in this.gvOther.Rows)
            {

                TextBox txtPAN = (TextBox)gvr.FindControl("txtAmt");
                Label headId = (Label)gvr.FindControl("lblId");

                if (headId.Text == "OT000003")
                {
                    if ((string)Session["sCompID"] == "CO000015" || (string)Session["sCompID"] == "CO000114")
                    {
                        txtPAN.Text = (string)Session["sPAN"];

                        txtPAN.ReadOnly = true;
                    }
                    break;
                }
            }
        }
        private void FillRents()
        {
            dsInv = objInv.GetRentDetails((string)Session["sCompID"], (string)Session["sEmpID"], txtAddress);
            gvRent.DataSource = dsInv;
            gvRent.DataBind();
            dsInv = new DataSet();
            dsInv = objInv.GetLandlordDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            if (Session["sCompID"].ToString() == "CO000061" || Session["sCompID"].ToString() == "CO000060")
            {
                lblNotes.Visible = true;
                lblNotes.Text = " Note: Landlord Address Proof & PAN copy is must, if the monthly rent amount is above Inr. 8,333/-  & Registered Lease Agreement is must if Rent is above Inr.10,000/- ";
            }
            else if (Session["sCompID"].ToString() == "CO000015")
            {
                lblNotes.Visible = true;
                lblNotes.Text = " Note: Landlord Address Proof & PAN copy is must, if the monthly rent amount is above Inr. 8,333/- ";

            }
            gvLandlordDetails.DataSource = dsInv;
            gvLandlordDetails.DataBind();
        }
        private void FillRentsNew()
        {
            dsInv = objInv.GetRentDetailsNew((string)Session["sCompID"], (string)Session["sEmpID"], txtAddressNew);
            gvRentNew.DataSource = dsInv;
            gvRentNew.DataBind();
            //dsInv = new DataSet();
            //dsInv = objInv.GetLandlordDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            //gvLandlordDetailsNew.DataSource = dsInv;
            //gvLandlordDetailsNew.DataBind();
        }
        private void FillTwelve()
        {
            dsInv = objInv.GetTwelveDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            gvtwelve.DataSource = dsInv;
            gvtwelve.DataBind();
            dsInv = new DataSet();
            dsInv = objInv.GetLenderDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            gvLenderDetails.DataSource = dsInv;
            gvLenderDetails.DataBind();
        }
        private void FillBTwelve()
        {
            dsInv = objInv.GetTwelveBDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            gvTwelB.DataSource = dsInv;
            gvTwelB.DataBind();
        }
        private void FillTwelveB()
        {
            dsInv = objInv.GetTwelveBDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            gvTwelB.DataSource = dsInv;
            gvTwelB.DataBind();
        }

        private void RegisterGridviewUploadControls()
        {
            foreach (GridViewRow gvr in gvInv.Rows)
            {
                ImageButton up = (ImageButton)gvr.FindControl("btnUpload");
                LinkButton ln = (LinkButton)gvr.FindControl("lnkShowDoc");
                smInv.RegisterPostBackControl(up);
                smInv.RegisterPostBackControl(ln);
            }
        }

        private void FillAll()
        {

            FillOther();
            FillInvestments();
            if (Session["Message"] != null)
            {
                lblMessage.Text = Session["Message"].ToString();
            }
            else
            {
                lblMessage.Text = "";
            }

            string SourcePath = "";
            if ((string)Session["sCompID"] == "CO000114")
            {
                SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Declaration\\";
            }
            else
            {
                SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Declaration\\";
            }
            string filesToFind = (string)Session["sEmpCode"] + "_*";


            CalculateInvestments(gvInv, "INV");
            FillRents();
            CalculateInvestments(gvRent, "RENT");


            FillTwelve();
            CalculateInvestments(gvtwelve, "12C");


            if ((string)Session["sCompID"] == "CO000141")
            {
                FillBTwelve();
                CalculateInvestments(gvTwelB, "12B");
                if (ViewState["CTCTOAMT"] != null)
                {
                    lnk12.Text = "4. Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", ViewState["CTCTOAMT"].ToString()).Replace("रु ", "").Replace(".00", "") + ")";
                }

                SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                filesToFind = "12B_*";
                if (Directory.Exists(SourcePath))
                {
                    string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
                    if (fileList.Length > 0)
                    {
                        lnkDownload12BSupport.Visible = true;
                    }
                    else
                    {
                        lnkDownload12BSupport.Visible = false;
                    }
                }
                else
                {
                    lnkDownload12BSupport.Visible = false;
                }
            }

            //ToggleUploadDisplay(CheckForm12BB());

            Fill_Amounts();
            ViewState["chk80EE"] = false;

            lnkUpdateOther.Visible = false;
            lnkUpdate.Visible = false;
            lnkUpdateRent.Visible = false;
            lnkUpdate12.Visible = false;
            lnkUpdate12B.Visible = false;

            btnPrint.Visible = false;
            lnkManual.Visible = false;
            lnkFAQ.Visible = false;

            if ((string)Session["sCompID"] == "CO000141") //orra
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
            }


            lnkFAQ.Visible = true;

        }

        protected void lblInvDetails_Click(object sender, EventArgs e)
        {
            try
            {
                if (InvDetails.Visible == false)
                {
                    FillInvestments();
                    CalculateInvestments(gvInv, "INV");
                }
                InvDetails.Visible = (InvDetails.Visible == true ? false : true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
            }

        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                //if (Validate() == false)
                //{
                //    return;
                //}
                foreach (GridViewRow gvr in this.gvInv.Rows)
                {
                    Label lblId = (Label)gvr.FindControl("lblId");
                    TextBox txtAmt = (TextBox)gvr.FindControl("txtAmt");
                    CheckBox chkAgree = (CheckBox)gvr.FindControl("chkAgree");
                    Label lblDesc = (Label)gvr.FindControl("lblDesc");

                    if (lblId.Text == "I9999999")
                    {
                        if (!chkAgree.Checked)
                        {
                            lblMessage.Text = "You must agree to the disclaimer before submitting.";
                            objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
                            return;
                        }
                    }
                }

                result = objInv.UpdateInvestment(MakeInvXml(gvInv, ""), "", "", (string)Session["sCompID"], (string)Session["sEmpID"]);
                if (result.ToString().Trim() == "success")
                {
                    FillInvestments();
                    CalculateInvestments(gvInv, "INV");
                    lblMessage.Text = "Successfuly updated investments.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments.');");
                }
                else
                {
                    lblMessage.Text = "Error occurred in application.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
            }
        }

        private Boolean Validate()
        {
            double amount = 0;
            double limit = 0;

            foreach (GridViewRow gvr in this.gvInv.Rows)
            {

                amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                limit = Convert.ToDouble((((Label)gvr.FindControl("lblLimit")).Text == "" || ((Label)gvr.FindControl("lblLimit")).Text == "0.00" ? "0" : ((Label)gvr.FindControl("lblLimit")).Text));

                if (limit > 0)
                {
                    if (amount > limit)
                    {
                        lblMessage.Text = "Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + "');");
                        return false;
                    }
                }

            }

            return true;
        }

        private bool ValidateTextChanged(object sender)
        {
            //double amount = 0;
            //double limit = 0;
            TextBox txtAmt = (TextBox)sender;
            Label lblLimit = (Label)txtAmt.NamingContainer.FindControl("lblLimit");
            Label lblDesc = (Label)txtAmt.NamingContainer.FindControl("lblDesc");
            Label lblId = (Label)txtAmt.NamingContainer.FindControl("lblId");
            double amt = 0;

            if (lblId.Text.Contains("L0000003") || lblId.Text.Contains("L0000006") || lblId.Text.Contains("LN000003"))
            {
                //if (txtAmt.Text.Length > 10)
                //{
                //    objcommon.Display("Validate", "DisplayErrorMessage('Invalid PAN.');");
                //    lblMessage.Text = "Invalid PAN.";
                //    return false;
                //}
                string panPattern = @"^[A-Z]{5}[0-9]{4}[A-Z]$";
                if (!System.Text.RegularExpressions.Regex.IsMatch(txtAmt.Text, panPattern))
                {
                    objcommon.Display("Validate", "DisplayErrorMessage('Invalid PAN.');");
                    lblMessage.Text = "Invalid PAN.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Invalid PAN.');");
                    lblMessage.BackColor = System.Drawing.Color.Tomato;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    return false;
                }
            }
            else if (double.TryParse(txtAmt.Text, out amt) && Convert.ToDouble(txtAmt.Text) > Convert.ToDouble(lblLimit.Text))
            {
                objcommon.Display("Validate", "DisplayErrorMessage('Limit for " + objcommon.EncodeJsString(lblDesc.Text) + " is " + lblLimit.Text + "');");
                return false;
            }

            //foreach (GridViewRow gvr in this.gvInv.Rows)
            //{

            //    amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
            //    limit = Convert.ToDouble((((Label)gvr.FindControl("lblLimit")).Text == "" || ((Label)gvr.FindControl("lblLimit")).Text == "0.00" ? "0" : ((Label)gvr.FindControl("lblLimit")).Text));

            //    if (limit > 0)
            //    {
            //        if (amount > limit)
            //        {
            //            //lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
            //            objcommon.Display("Validate", "DisplayErrorMessage('Limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is " + ((Label)gvr.FindControl("lblLimit")).Text + "');");
            //            return false;
            //        }
            //    }

            //}

            return true;
        }
        private bool gvOther_txtAmt_OnTextChanged(object sender)
        {

            TextBox txtAmt = (TextBox)sender;
            Label lblLimit = (Label)txtAmt.NamingContainer.FindControl("lblLimit");
            Label lblDesc = (Label)txtAmt.NamingContainer.FindControl("lblDesc");
            Label lblId = (Label)txtAmt.NamingContainer.FindControl("lblId");
            double amt = 0;

            if (lblId.Text.Contains("L0000003") || lblId.Text.Contains("L0000006") || lblId.Text.Contains("LN000003"))
            {
                if (txtAmt.Text.Length > 10)
                {
                    objcommon.Display("Validate", "DisplayErrorMessage('Invalid PAN.');");
                    lblMessage.Text = "Invalid PAN.";
                    return false;
                }
            }
            else if (double.TryParse(txtAmt.Text, out amt) && Convert.ToDouble(txtAmt.Text) > Convert.ToDouble(lblLimit.Text))
            {
                objcommon.Display("Validate", "DisplayErrorMessage('Limit for " + objcommon.EncodeJsString(lblDesc.Text) + " is " + lblLimit.Text + "');");
                return false;
            }



            return true;
        }

        private Boolean ValidateOther()
        {
            string entrytype = "";
            long intValue = 0;
            long entrylength = 0;

            foreach (GridViewRow gvr in this.gvOther.Rows)
            {

                entrytype = ((Label)gvr.FindControl("lblentrytype")).Text;
                entrylength = Convert.ToInt64(((Label)gvr.FindControl("lblentrylen")).Text);
                Label lblId = (Label)gvr.FindControl("lblId");
                TextBox txtAmt = (TextBox)gvr.FindControl("txtAmt");
                int i;

                if (lblId.Text == "OT000001")
                {
                    if (txtAmt.Text.Trim() != "" && int.TryParse(txtAmt.Text.Trim(), out i) == false)
                    {
                        lblMessage.Text = "Value for Number of Children Studying must be a number.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Value for Number of Children Studying must be a number.');");
                        return false;
                    }
                }

                if (lblId.Text == "OT000003")
                {
                    if (txtAmt.Text.Length != 10)
                    {
                        lblMessage.Text = "Employee Information - Invalid Pan";
                        objcommon.Display("Validate", "DisplayErrorMessage('Employee Information - Invalid Pan');");
                        divAlert.Visible = false;

                        return false;
                    }
                }

                if (lblId.Text == "OT000004")
                {
                    if (txtAmt.Text.Length != 10)
                    {
                        lblMessage.Text = "Employee Information - Please Enter Correct Mobile Number";
                        objcommon.Display("Validate", "DisplayErrorMessage('Employee Information - Please Enter Correct Mobile Number');");
                        return false;
                    }
                }

                if (entrytype != "NA")
                {
                    if (entrytype == "N")
                    {
                        try
                        {
                            intValue = Convert.ToInt64((((TextBox)gvr.FindControl("txtAmt")).Text.Trim() == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));

                            if (((Label)gvr.FindControl("lblEntryMode")).Text == "C" && ((TextBox)gvr.FindControl("txtAmt")).Text.Trim().Length == 0)
                            {
                                lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.";
                                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.');");
                                return false;
                            }

                            if (entrylength != ((TextBox)gvr.FindControl("txtAmt")).Text.Trim().Length && entrylength != -1)
                            {
                                lblMessage.Text = "Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                                objcommon.Display("Validate", "DisplayErrorMessage('Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = "Error occurred in application.";
                            //objcommon.Display("Validate", "DisplayErrorMessage('Enter data in number for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                            return false;
                        }
                    }
                }
            }

            return true;
        }
        private Boolean ValidateTwelve()
        {

            double amount = 0;
            double limit = 0;
            string propAddress = "";
            string entrytype = "";
            long entrylength = 0;
            string loan = "";

            //foreach (GridViewRow gvr in this.gvtwelve.Rows)
            //{
            //    Label lblId = (Label)gvr.FindControl("lblId");
            //    TextBox txtAmt = (TextBox)gvr.FindControl("txtAmt");
            //    CheckBox chkAgree = (CheckBox)gvr.FindControl("chkAgree");
            //    Label lblDesc = (Label)gvr.FindControl("lblDesc");

            //    if (lblId.Text == "F9999999")
            //    {
            //        if (!chkAgree.Checked)
            //        {
            //            lblMessage.Text = "You must agree to the disclaimer before submitting.";
            //            objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
            //            return false;
            //        }
            //    }
            //}

            foreach (GridViewRow gvr in this.gvtwelve.Rows)
            {

                amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                limit = Convert.ToDouble((((Label)gvr.FindControl("lblLimit")).Text == "" || ((Label)gvr.FindControl("lblLimit")).Text == "0.00" ? "0" : ((Label)gvr.FindControl("lblLimit")).Text));
                propAddress = ((TextBox)gvr.FindControl("txtPropAdd")).Text.Trim();

                if (limit > 0)
                {
                    if (amount > limit)
                    {
                        lblMessage.Text = "Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + "');");
                        return false;
                    }
                }
                if (((Label)gvr.FindControl("lblId")).Text.Trim() == "F0000003")
                {
                    loan = amount.ToString();
                    if (amount < 0)
                    {
                        lblMessage.Text = "Enter positive amount.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Enter positive amount.');");
                        return false;
                    }
                }
                if (((TextBox)gvr.FindControl("txtPropAdd")).Visible == true && propAddress.Trim() == "")
                {
                    if (loan != "0")
                    {
                        lblMessage.Text = "Enter property address.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Enter property address.');");
                        return false;
                    }
                }
                if (((Label)gvr.FindControl("lblId")).Text.Trim() == "F0000051" && ((string)Session["sCompID"] == "CO000056"))
                {
                    foreach (GridViewRow grid in this.gvtwelve.Rows)
                    {
                        if (((Label)grid.FindControl("lblId")).Text.Trim() == "F0000057")
                        {
                            double intAmt = Convert.ToDouble((((TextBox)grid.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)grid.FindControl("txtAmt")).Text));
                            if (intAmt > 0)
                            {
                                if (amount <= 0)
                                {
                                    lblMessage.Text = "Enter Annual Rent as per Municipal valuation / Annual Fair Rent in that locality / Actual Annual rent received(whichever is higher).";
                                    objcommon.Display("Validate", "DisplayErrorMessage('Enter Annual Rent as per Municipal valuation / Annual Fair Rent in that locality / Actual Annual rent received(whichever is higher).');");
                                    return false;
                                }
                            }
                        }
                    }
                }

            }

            foreach (GridViewRow gvr in this.gvLenderDetails.Rows)
            {
                entrytype = ((Label)gvr.FindControl("lblentrytype")).Text;
                entrylength = Convert.ToInt64(((Label)gvr.FindControl("lblentrylen")).Text);

                try
                {
                    if (((Label)gvr.FindControl("lblEntryMode")).Text == "C" && ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length == 0)
                    {
                        if (loan != "0")
                        {
                            lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.";
                            objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.');");
                            return false;
                        }
                    }

                    if (entrylength != ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length && entrylength != -1)
                    {
                        lblMessage.Text = "Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error occurred in application.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Enter data in number for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                    return false;
                }
            }

            return true;
        }
        private Boolean ValidateTwelveB()
        {
            double amount = 0;
            double limit = 0;

            foreach (GridViewRow gvr in this.gvTwelB.Rows)
            {

                amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                limit = Convert.ToDouble((((Label)gvr.FindControl("lblLimit")).Text == "" || ((Label)gvr.FindControl("lblLimit")).Text == "0.00" ? "0" : ((Label)gvr.FindControl("lblLimit")).Text));

                if (limit > 0)
                {
                    if (amount > limit)
                    {
                        lblMessage.Text = "Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + "');");
                        return false;
                    }
                }

            }

            return true;
        }
        //private Boolean ValidateRent()
        //{
        //    double amount = 0;
        //    double limit = 0;
        //    double landLimit = 0;
        //    string PAN = "";
        //    string INV = "";
        //    string DESC = "";
        //    string entrytype = "";
        //    int entrylength = 0;


        //    if (txtAddress.Text.Trim() == "")
        //    {
        //        lblMessage.Text = "Please enter rented property address.";
        //        objcommon.Display("Validate", "DisplayErrorMessage('Please enter rented property address.');");
        //        return false;
        //    }

        //    double maxRent = 0;
        //    foreach (GridViewRow gvr in this.gvRent.Rows)
        //    {

        //        amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
        //        PAN = ((TextBox)gvr.FindControl("txtPAN")).Text.Trim();
        //        limit = Convert.ToDouble((((Label)gvr.FindControl("lblLimit")).Text == "" || ((Label)gvr.FindControl("lblLimit")).Text == "0.00" ? "0" : ((Label)gvr.FindControl("lblLimit")).Text));
        //        landLimit = Convert.ToDouble((((Label)gvr.FindControl("lblLimitLand")).Text == "" || ((Label)gvr.FindControl("lblLimitLand")).Text == "0.00" ? "0" : ((Label)gvr.FindControl("lblLimitLand")).Text));

        //        if (limit > 0)
        //        {
        //            if (amount > limit)
        //            {
        //                lblMessage.Text = "Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
        //                objcommon.Display("Validate", "DisplayErrorMessage('Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + "');");
        //                return false;
        //            }
        //        }

        //        if (landLimit > 0)
        //        {
        //            if (amount > landLimit && PAN.Trim() == "")
        //            {
        //                lblMessage.Text = "Please enter PAN of Landlord when rent amount is greater than Landlord limit.";
        //                objcommon.Display("Validate", "DisplayErrorMessage('Please enter PAN of Landlord when rent amount is greater than Landlord limit.');");
        //                return false;
        //            }
        //        }

        //        if (maxRent < amount)
        //        {
        //            maxRent = amount;
        //        }
        //    }

        //    foreach (GridViewRow gvr in this.gvLandlordDetails.Rows)
        //    {

        //        PAN = ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim();
        //        INV = ((Label)gvr.FindControl("lblId")).Text.Trim();
        //        DESC = ((Label)gvr.FindControl("lblDesc")).Text.Trim();

        //        if (INV.Contains("L0000003") || INV.Contains("L0000006"))
        //        {
        //            if (PAN.Length > 10)
        //            {
        //                objcommon.Display("Validate", "DisplayErrorMessage('" + DESC + " - Invalid PAN.');");
        //                lblMessage.Text = DESC + " - Invalid PAN.";
        //                return false;
        //            }

        //            if (INV.Contains("L0000003") && maxRent > 8333)
        //            {
        //                if (PAN.Trim() == "")
        //                {
        //                    lblMessage.Text = "Please enter First Landlord PAN when rent amount is greater than Landlord limit.";
        //                    objcommon.Display("Validate", "DisplayErrorMessage('Please enter First Landlord PAN when rent amount is greater than Landlord limit.');");
        //                    return false;
        //                }
        //            }
        //        }

        //        entrytype = ((Label)gvr.FindControl("lblentrytype")).Text;
        //        entrylength = Convert.ToInt32(((Label)gvr.FindControl("lblentrylen")).Text);

        //        try
        //        {
        //            if (((Label)gvr.FindControl("lblEntryMode")).Text == "C" && ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length == 0)
        //            {
        //                lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.";
        //                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.');");
        //                return false;
        //            }

        //            if (entrylength != ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length && entrylength != -1)
        //            {
        //                lblMessage.Text = "Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
        //                objcommon.Display("Validate", "DisplayErrorMessage('Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
        //                return false;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            lblMessage.Text = "Error occurred in application.";
        //            //objcommon.Display("Validate", "DisplayErrorMessage('Enter data in number for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
        //            return false;
        //        }
        //    }

        //    return true;
        //}
        private Boolean ValidateRentNew()
        {
            double amount = 0;
            double limit = 0;
            double landLimit = 0;
            string PAN = "";
            string Name = "";

            //if (txtAddress.Text.Trim() == "")
            //{
            //    lblMessage.Text = "Please enter rented property address.";
            //    objcommon.Display("Validate", "DisplayErrorMessage('Please enter rented property address.');");
            //}

            foreach (GridViewRow gvr in this.gvRentNew.Rows)
            {

                amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                PAN = ((TextBox)gvr.FindControl("txtPAN")).Text.Trim();
                Name = ((TextBox)gvr.FindControl("txtName")).Text.Trim();
                limit = Convert.ToDouble((((Label)gvr.FindControl("lblLimit")).Text == "" || ((Label)gvr.FindControl("lblLimit")).Text == "0.00" ? "0" : ((Label)gvr.FindControl("lblLimit")).Text));
                landLimit = Convert.ToDouble((((Label)gvr.FindControl("lblLimitLand")).Text == "" || ((Label)gvr.FindControl("lblLimitLand")).Text == "0.00" ? "0" : ((Label)gvr.FindControl("lblLimitLand")).Text));

                if (limit > 0)
                {
                    if (amount > limit)
                    {
                        lblMessage.Text = "Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + "');");
                        return false;
                    }
                }

                if (landLimit > 0)
                {
                    if (amount > landLimit && (PAN.Trim() == "" || Name.Trim() == ""))
                    {
                        lblMessage.Text = "Please enter Name and PAN of Landlord when rent amount is greater than Landlord limit.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Please enter Name and PAN of Landlord when rent amount is greater than Landlord limit.');");
                        return false;
                    }
                }
            }

            //foreach (GridViewRow gvr in this.gvLandlordDetails.Rows)
            //{

            //    PAN = ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim();
            //    INV = ((Label)gvr.FindControl("lblId")).Text.Trim();
            //    DESC = ((Label)gvr.FindControl("lblDesc")).Text.Trim();

            //    if (INV.Contains("L0000003") || INV.Contains("L0000006"))
            //    {
            //        if (PAN.Length > 10)
            //        {
            //            objcommon.Display("Validate", "DisplayErrorMessage('" + DESC + " - Invalid PAN.');");
            //            lblMessage.Text = DESC + " - Invalid PAN.";
            //            return false;
            //        }
            //    }

            //    entrytype = ((Label)gvr.FindControl("lblentrytype")).Text;
            //    entrylength = Convert.ToInt64(((Label)gvr.FindControl("lblentrylen")).Text);

            //    try
            //    {
            //        if (((Label)gvr.FindControl("lblEntryMode")).Text == "C" && ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length == 0)
            //        {
            //            lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.";
            //            objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.');");
            //            return false;
            //        }

            //        if (entrylength != ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length && entrylength != -1)
            //        {
            //            lblMessage.Text = "Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
            //            objcommon.Display("Validate", "DisplayErrorMessage('Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
            //            return false;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        lblMessage.Text = "Error occurred in application.";
            //        //objcommon.Display("Validate", "DisplayErrorMessage('Enter data in number for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
            //        return false;
            //    }
            //}

            return true;
        }

        private string MakeInvXml(GridView GV, string strType)
        {

            StringBuilder sbTaxDetails = new StringBuilder();
            string xmlInv = string.Empty;

            sbTaxDetails.Append("<ROOT>");

            foreach (GridViewRow gvr in GV.Rows)
            {
                sbTaxDetails.Append("<Inv COMP_AID='" + (string)Session["sCompID"] + "'");
                sbTaxDetails.Append(" EMP_AID='" + (string)Session["sEmpID"] + "'");
                sbTaxDetails.Append(" INV_AID='" + ((Label)gvr.FindControl("lblId")).Text.Trim() + "'");
                sbTaxDetails.Append(" DET_AID='" + ((Label)gvr.FindControl("lbldetId")).Text.Trim() + "'");
                if (strType == "Other" || strType == "Landlord" || strType == "Lender")
                {
                    sbTaxDetails.Append(" AMOUNT='0'");
                }
                else if (((Label)gvr.FindControl("lblId")).Text.Trim() == "F0000059")
                {
                    sbTaxDetails.Append(" AMOUNT='" + (((CheckBox)gvr.FindControl("chkIsSelect")).Checked == true ? "1" : "0") + "'");
                }
                else
                {
                    sbTaxDetails.Append(" AMOUNT='" + ((TextBox)gvr.FindControl("txtAmt")).Text.Trim() + "'");
                }
                sbTaxDetails.Append(" INV_DESC='" + ReplaceSpecialCharacters(((Label)gvr.FindControl("lblMID")).Text.Trim()) + "'");
                if (strType == "Rent" || strType == "RentNew")
                {
                    sbTaxDetails.Append(" PAN='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtPAN")).Text.Trim()) + "'");
                }
                else
                {
                    sbTaxDetails.Append(" PAN=''");
                }
                if (strType == "12")
                {
                    sbTaxDetails.Append(" PROP_ADDRESS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtPropAdd")).Text.Trim()) + "'");
                }
                else if (strType == "RentNew")
                {
                    sbTaxDetails.Append(" PROP_ADDRESS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtAddr")).Text.Trim()) + "'");
                }
                else
                {
                    sbTaxDetails.Append(" PROP_ADDRESS=''");
                }
                if (strType == "Other")
                {
                    sbTaxDetails.Append(" OTHER_DETAILS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtAmt")).Text.Trim()) + "'");
                }
                else if (strType == "Landlord" || strType == "Lender")
                {
                    sbTaxDetails.Append(" OTHER_DETAILS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim()) + "'");
                }
                else if (strType == "RentNew")
                {
                    sbTaxDetails.Append(" OTHER_DETAILS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtName")).Text.Trim()) + "'");
                }
                else
                {
                    sbTaxDetails.Append(" OTHER_DETAILS=''");
                }
                sbTaxDetails.Append(" SORT_ORDER='" + ((Label)gvr.FindControl("lblsort")).Text.Trim() + "'/>");


            }

            sbTaxDetails.Append("</ROOT>");

            xmlInv = sbTaxDetails.ToString();

            return xmlInv;


        }

        protected string ReplaceSpecialCharacters(string inputString)
        {
            inputString = inputString.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&#39;").
                    Replace("\"", "&quot;");

            //Hashtable ht = new Hashtable();

            //for (int i = 0; i <= inputString.Length - 1; i++)
            //{
            //    int ascii = Convert.ToChar(inputString.Substring(i, 1));

            //    if (ascii > 125)
            //    {
            //        if (!ht.ContainsValue(ascii))
            //        {
            //            ht.Add(Convert.ToChar(inputString.Substring(i, 1)), ascii);
            //        }
            //    }
            //}

            //foreach (DictionaryEntry entry in ht)
            //{
            //    inputString = inputString.Replace(Convert.ToString(entry.Key), "~" + Convert.ToInt32(entry.Value) + "$");
            //}

            return inputString;
        }

        protected void lnkRent_Click(object sender, EventArgs e)
        {
            try
            {
                if (RentDetails.Visible == false)
                {
                    if ((string)Session["sCompID"] == "CO000056" || (string)Session["sCompID"] == "CO000015")
                    {
                        objcommon.Display("Validate", "DisplayErrorMessage('Please note: Rent - Kindly provide full name, Residential Address & PAN (Rent above 1 Lakh). Interest on housing loan - Kindly provide Bank name, address and PAN(if available).');");
                    }
                    FillRents();
                    CalculateInvestments(gvRent, "RENT");
                }
                RentDetails.Visible = (RentDetails.Visible == true ? false : true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
            }
        }
        protected void lnkRentNew_Click(object sender, EventArgs e)
        {
            try
            {
                if (RentDetailsNew.Visible == false)
                {
                    FillRentsNew();
                    CalculateInvestments(gvRentNew, "RENT");
                }
                RentDetailsNew.Visible = (RentDetailsNew.Visible == true ? false : true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
            }
        }
        protected void lnkUpdateRent_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                if (ValidateRent() == false)
                {
                    return;
                }
                result = objInv.UpdateInvestment(MakeInvXml(gvRent, "Rent"), txtAddress.Text.Trim(), "Rent", (string)Session["sCompID"], (string)Session["sEmpID"]);
                if (result.ToString().Trim() == "success")
                {
                    result = objInv.UpdateInvestment(MakeInvXml(gvLandlordDetails, "Landlord"), "", "", (string)Session["sCompID"], (string)Session["sEmpID"]);
                    if (result.ToString().Trim() == "success")
                    {
                        FillRents();
                        CalculateInvestments(gvRent, "RENT");
                        lblMessage.Text = "Successfuly updated rent.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated rent.');");
                    }
                    else
                    {
                        lblMessage.Text = "Error occurred in application.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating rent.');");
                    }
                }
                else
                {
                    lblMessage.Text = "Error occurred in application.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating rent.');");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void lnkUpdateRentNew_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                if (ValidateRentNew() == false)
                {
                    return;
                }
                result = objInv.UpdateInvestment(MakeInvXml(gvRentNew, "RentNew"), txtAddressNew.Text.Trim(), "RentNew", (string)Session["sCompID"], (string)Session["sEmpID"]);
                if (result.ToString().Trim() == "success")
                {
                    FillRentsNew();
                    CalculateInvestments(gvRentNew, "RENTNEW");
                    lblMessage.Text = "Successfuly updated rent.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated rent.');");
                }
                else
                {
                    lblMessage.Text = "Error occurred in application.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating rent.');");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void lnk12_Click(object sender, EventArgs e)
        {
            try
            {
                if (TwelveDetails.Visible == false)
                {
                    FillTwelve();
                    CalculateInvestments(gvtwelve, "12C");
                }
                TwelveDetails.Visible = (TwelveDetails.Visible == true ? false : true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkUpdate12_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                if (ValidateTwelve() == false)
                {
                    return;
                }
                result = objInv.UpdateInvestment(MakeInvXml(gvtwelve, "12"), "", "", (string)Session["sCompID"], (string)Session["sEmpID"]);
                if (result.ToString().Trim() == "success")
                {
                    result = objInv.UpdateInvestment(MakeInvXml(gvLenderDetails, "Lender"), "", "", (string)Session["sCompID"], (string)Session["sEmpID"]);
                    if (result.ToString().Trim() == "success")
                    {
                        FillTwelve();
                        CalculateInvestments(gvtwelve, "12C");
                        lblMessage.Text = "Successfuly updated investments.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments.');");
                    }
                    else
                    {
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                    }
                }
                else
                {
                    lblMessage.Text = "Error occurred in application.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
            }
        }

        protected void gvOther_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblId = (Label)e.Row.FindControl("lblId");
                    TextBox txtAmt = (TextBox)e.Row.FindControl("txtAmt");
                    CheckBox chkAgree = (CheckBox)e.Row.FindControl("chkAgree");
                    Label lblDesc = (Label)e.Row.FindControl("lblDesc");

                    if (lblId.Text == "OT999999")
                    {
                        txtAmt.Visible = false;
                        chkAgree.Visible = true;
                        lblDesc.Text = "Disclaimer: <br />" + lblDesc.Text;
                        lblDesc.Font.Bold = true;
                    }

                    if (lblId.Text == "OT000006")
                    {
                        txtAmt.Visible = false;
                        chkAgree.Visible = true;
                    }
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void gvtwelve_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtPropAdd = (TextBox)e.Row.FindControl("txtPropAdd");
                    Label lblisprop = (Label)e.Row.FindControl("lblisprop");
                    CheckBox chkIsSelect = (CheckBox)e.Row.FindControl("chkIsSelect");

                    txtPropAdd.Visible = (lblisprop.Text.Trim() == "1" ? true : false);

                    TextBox txtAmt = (TextBox)e.Row.FindControl("txtAmt");
                    Label lblHead = (Label)e.Row.FindControl("lblHead");
                    Label lblId = (Label)e.Row.FindControl("lblId");
                    Label lblRead = (Label)e.Row.FindControl("lblRead");
                    Label lblDesc = (Label)e.Row.FindControl("lblDesc");
                    Label lblFormula = (Label)e.Row.FindControl("lblFormula");

                    CheckBox chkAgree = (CheckBox)e.Row.FindControl("chkAgree");

                    if (Convert.ToString(lblFormula.Text) != "")
                    {
                        arrFor.Add(lblFormula.Text);
                    }

                    if (lblId.Text.Trim() != "F0000055")
                    {
                        lblDesc.Font.Bold = (lblHead.Text.Trim() == "1" ? true : false);
                    }

                    txtAmt.Visible = (lblHead.Text.Trim() == "0" ? true : false);

                    txtAmt.ReadOnly = (lblRead.Text.Trim() == "1" ? true : false);

                    txtAmt.BackColor = (lblRead.Text.Trim() == "1" ? System.Drawing.Color.LightGray : txtAmt.BackColor);

                    if (lblId.Text.Trim() == "F0000059")
                    {
                        txtAmt.Visible = false;
                        chkIsSelect.Visible = true;
                        if (txtAmt.Text.Trim() == "1.00")
                        {
                            chkIsSelect.Checked = true;
                            ViewState["chk80EE"] = true;
                        }
                        else
                        {
                            chkIsSelect.Checked = false;
                            ViewState["chk80EE"] = false;
                        }
                    }
                    if ((string)Session["sCompId"] == "CO000015")
                    {
                        txtAmt.Text = txtAmt.Text.Replace(".00", "");
                    }

                    if (lblId.Text == "F9999999")
                    {
                        txtAmt.Visible = false;
                        chkAgree.Visible = true;
                        lblDesc.Text = "Disclaimer: <br />" + lblDesc.Text;
                        lblDesc.Font.Bold = true;
                    }
                }
                if (e.Row.RowType == DataControlRowType.Footer)
                {
                    ViewState["Formula"] = arrFor;
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void gvInv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtAmt = (TextBox)e.Row.FindControl("txtAmt");
                    Label lblHead = (Label)e.Row.FindControl("lblHead");
                    Label lblDesc = (Label)e.Row.FindControl("lblDesc");
                    Label lblLimit = (Label)e.Row.FindControl("lblLimit");
                    Label lblId = (Label)e.Row.FindControl("lblId");

                    CheckBox chkAgree = (CheckBox)e.Row.FindControl("chkAgree");

                    lblDesc.Font.Bold = (lblHead.Text.Trim() == "1" ? true : false);

                    txtAmt.Visible = (lblHead.Text.Trim() == "0" ? true : false);

                    if ((string)Session["sCompId"] == "CO000015")
                    {
                        txtAmt.Text = txtAmt.Text.Replace(".00", "");
                        lblLimit.Text = lblLimit.Text.Replace(".00", "");
                    }

                    if (lblId.Text == "I9999999")
                    {
                        txtAmt.Visible = false;
                        chkAgree.Visible = true;
                        lblDesc.Text = "Disclaimer: <br />" + lblDesc.Text;
                        lblDesc.Font.Bold = true;
                    }
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void lnkUpdate12B_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                if (CheckPrevAmount())
                {
                    if (lnkDownload12BSupport.Visible == false)
                    {
                        lblMessage.Text = "Please upload previous employer final settlement proof.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Please upload previous employer final settlement proof.');");
                        return;
                    }
                }

                if (ValidateTwelveB() == false)
                {
                    return;
                }
                result = objInv.UpdateInvestment(MakeInvXml(gvTwelB, ""), "", "", (string)Session["sCompID"], (string)Session["sEmpID"]);
                if (result.ToString().Trim() == "success")
                {
                    FillTwelveB();
                    CalculateInvestments(gvTwelB, "12B");
                    lblMessage.Text = "Successfuly updated investments.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments.');");
                }
                else
                {
                    lblMessage.Text = "Error occurred in application.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
            }
        }

        protected void lnk12b_Click(object sender, EventArgs e)
        {
            try
            {
                if (TwelveBDetails.Visible == false)
                {
                    FillTwelveB();
                    CalculateInvestments(gvTwelB, "12B");
                }
                TwelveBDetails.Visible = (TwelveBDetails.Visible == true ? false : true);
                string SourcePath = null;
                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                }
                else
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                }
                string filesToFind = "12B_*";
                if (Directory.Exists(SourcePath))
                {
                    string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
                    if (fileList.Length > 0)
                    {
                        lnkDownload12BSupport.Visible = true;
                    }
                    else
                    {
                        lnkDownload12BSupport.Visible = false;
                    }
                }
                else
                {
                    lnkDownload12BSupport.Visible = false;
                }

                if (CheckPrevAmount())
                {
                    div12BNote.Visible = true;
                    div12BUpload.Visible = true;
                }
                else
                {
                    div12BNote.Visible = false;
                    div12BUpload.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
            }
        }

        protected void lnkUpdateOther_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                if (ValidateOther() == false)
                {
                    return;
                }
                result = objInv.UpdateInvestment(MakeInvXml(gvOther, "Other"), "", "", (string)Session["sCompID"], (string)Session["sEmpID"]);
                if (result.ToString().Trim() == "success")
                {
                    FillOther();
                    lblMessage.Text = "Successfuly updated investments.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments.');");
                }
                else
                {
                    lblMessage.Text = result;
                    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
            }
        }

        protected void lnkOther_Click(object sender, EventArgs e)
        {
            try
            {
                if (OtherDetails.Visible == false)
                {
                    FillOther();
                }
                OtherDetails.Visible = (OtherDetails.Visible == true ? false : true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
            }
        }

        protected void chkSel_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                string value = "";
                CheckBox chkSel = (CheckBox)sender;
                if (chkSel.Checked == true)
                {
                    foreach (GridViewRow gvr in this.gvRent.Rows)
                    {
                        if (gvr.RowIndex == 0)
                        {
                            value = ((TextBox)gvr.FindControl("txtAmt")).Text;
                        }
                        else
                        {
                            if (((CheckBox)gvr.FindControl("chkAgree")).Visible == false)
                            {
                                ((TextBox)gvr.FindControl("txtAmt")).Text = value;
                            }
                            else
                            {
                                ((TextBox)gvr.FindControl("txtAmt")).Text = "";
                            }
                        }

                    }
                    CalculateInvestments(gvRent, "RENT");
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void chkPan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string value = "";
                CheckBox chkPan = (CheckBox)sender;
                if (chkPan.Checked == true)
                {
                    foreach (GridViewRow gvr in this.gvRent.Rows)
                    {
                        if (gvr.RowIndex == 0)
                        {
                            value = ((TextBox)gvr.FindControl("txtPAN")).Text.Trim();
                        }
                        else
                        {
                            ((TextBox)gvr.FindControl("txtPAN")).Text = value.ToString();
                        }
                    }
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void chkSelNew_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                double value = 0;
                CheckBox chkSel = (CheckBox)sender;
                if (chkSel.Checked == true)
                {
                    foreach (GridViewRow gvr in this.gvRentNew.Rows)
                    {
                        if (gvr.RowIndex == 0)
                        {
                            value = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text.Trim() == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                        }
                        else
                        {
                            ((TextBox)gvr.FindControl("txtAmt")).Text = value.ToString();
                        }

                    }
                    CalculateInvestments(gvRentNew, "RENTNEW");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                //lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void chkPanNew_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string value = "";
                CheckBox chkPan = (CheckBox)sender;
                if (chkPan.Checked == true)
                {
                    foreach (GridViewRow gvr in this.gvRentNew.Rows)
                    {
                        if (gvr.RowIndex == 0)
                        {
                            value = ((TextBox)gvr.FindControl("txtPAN")).Text.Trim();
                        }
                        else
                        {
                            ((TextBox)gvr.FindControl("txtPAN")).Text = value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                //lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void chkName_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string value = "";
                CheckBox chkName = (CheckBox)sender;
                if (chkName.Checked == true)
                {
                    foreach (GridViewRow gvr in this.gvRentNew.Rows)
                    {
                        if (gvr.RowIndex == 0)
                        {
                            value = ((TextBox)gvr.FindControl("txtName")).Text.Trim();
                        }
                        else
                        {
                            ((TextBox)gvr.FindControl("txtName")).Text = value.ToString();
                        }
                    }
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void chkAddr_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                string value = "";
                CheckBox chkAddr = (CheckBox)sender;
                if (chkAddr.Checked == true)
                {
                    foreach (GridViewRow gvr in this.gvRentNew.Rows)
                    {
                        if (gvr.RowIndex == 0)
                        {
                            value = ((TextBox)gvr.FindControl("txtAddr")).Text.Trim();
                        }
                        else
                        {
                            ((TextBox)gvr.FindControl("txtAddr")).Text = value.ToString();
                        }
                    }
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private Boolean CalculateInvestments(GridView gv, string strType)
        {
            double CTCAmt = 0;
            double amt = 0;
            for (int i = 0; i <= gv.Rows.Count - 1; i++)
            {
                GridViewRow gvr = gv.Rows[i];

                if (strType == "12C")
                {
                    if (((Label)gvr.FindControl("lblIsTotal")).Text == "1")
                    {
                        CTCAmt = CTCAmt + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                    }
                }
                else
                {
                    CTCAmt = CTCAmt + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                }

                if (((Label)gvr.FindControl("lblId")).Text == "F0000003")
                {
                    amt = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                }

                if (((Label)gvr.FindControl("lblId")).Text == "F0000050")
                {
                    ((TextBox)gvr.FindControl("txtAmt")).Text = (((amt > 200000) ? 200000 : amt) * -1).ToString("0.00");
                }
            }

            if (strType == "12C")
            {
                if (CTCAmt < -200000)
                {
                    CTCAmt = -200000;
                }
            }

            GridViewRow frow = gv.FooterRow;

            ((Label)frow.FindControl("lblTot")).Text = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "");// CTCAmt.ToString();

            if (strType == "INV")
            {
                lblInvDetails.Text = "2. Investment Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "") + ")";
            }
            if (strType == "RENT")
            {
                lnkRent.Text = "3. Rent Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "") + ")";
            }
            if (strType == "RENTNEW")
            {
                lnkRentNew.Text = "3. Rent Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "") + ")";
            }
            if (strType == "12C")
            {
                lnk12.Text = "4. Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "") + ")";
            }
            if (strType == "12B")
            {
                lnk12b.Text = "5. Details of Income From Previous Employer"; // (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "") + ")";
            }

            if ((string)Session["sCompId"] == "CO000015")
            {
                ((Label)frow.FindControl("lblTot")).Text = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "").Replace(".00", "");// CTCAmt.ToString();

                if (strType == "INV")
                {
                    lblInvDetails.Text = "2. Investment Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "").Replace(".00", "") + ")";
                }
                if (strType == "RENT")
                {
                    lnkRent.Text = "3. Rent Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "").Replace(".00", "") + ")";
                }
                if (strType == "12C")
                {
                    lnk12.Text = "4. Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "").Replace(".00", "") + ")";
                }
                if (strType == "12B")
                {
                    lnk12b.Text = "5. Details of Income From Previous Employer (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "").Replace(".00", "") + ")";
                }
            }
            return true;
        }

        protected void txtAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateInvestments(gvInv, "INV");
                CheckAmount();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void txtAmtLnd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ValidateTextChanged(sender);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void txtRentAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateInvestments(gvRent, "RENT");
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void txtRentAmtNew_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateInvestments(gvRentNew, "RENTNEW");
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void txttwelveAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateGridColumn();
                //CalculateSelfPropertyIncome();
                ValidateTwelve();
                CalculateInvestments(gvtwelve, "12C");
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void CalculateSelfPropertyIncome()
        {
            foreach (GridViewRow row in gvtwelve.Rows)
            {
                double amt = 0;

                TextBox txtVal = (TextBox)row.FindControl("txtAmt");
                Label lblId = (Label)row.FindControl("lblId");
                if (lblId.Text.Trim() == "F0000003")
                {
                    amt = Convert.ToDouble(txtVal.Text.Trim() == "" ? "0" : txtVal.Text.Trim()) > 200000 ? 200000 : Convert.ToDouble(txtVal.Text.Trim() == "" ? "0" : txtVal.Text.Trim());
                }

                if (lblId.Text.Trim() == "F0000050")
                {
                    txtVal.Text = (amt * -1).ToString();
                    break;
                }
            }
        }

        protected void txtTwelvBAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                CalculateInvestments(gvTwelB, "12B");

                if (CheckPrevAmount())
                {
                    div12BNote.Visible = true;
                    div12BUpload.Visible = true;
                }
                else
                {
                    div12BNote.Visible = false;
                    div12BUpload.Visible = false;
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void CalculateGridColumn()
        {
            ArrayList arrList = new ArrayList();
            double sum = 0;
            double inv = 0;
            int keyNum = 1;
            int FooterCnt = 0;
            double ee80limit = 0;
            string strFormula = string.Empty;
            string splitVal = string.Empty;
            string opr = string.Empty;
            string hashVal = string.Empty;
            Hashtable ht = new Hashtable();

            arrList = (ArrayList)ViewState["Formula"];

            foreach (GridViewRow row in gvtwelve.Rows)
            {
                TextBox txtVal = (TextBox)row.FindControl("txtAmt");
                Label lblFormula = (Label)row.FindControl("lblFormula");
                Label lblId = (Label)row.FindControl("lblId");
                if (lblId.Text.Trim() == "F0000003")
                {
                    if ((bool)ViewState["chk80EE"] == true)
                    {
                        ee80limit = 250000;
                    }
                    else
                    {
                        ee80limit = 200000;
                    }
                    inv = Convert.ToDouble(txtVal.Text.Trim() == "" ? "0" : txtVal.Text.Trim()) > ee80limit ? ee80limit : Convert.ToDouble(txtVal.Text.Trim() == "" ? "0" : txtVal.Text.Trim());
                }

                if (lblFormula.Text.Trim() != "")
                {
                    strFormula = (string)arrList[FooterCnt];
                    string[] strSplit = strFormula.Split('/');
                    sum = 0;
                    for (int cnt = 0; cnt < strSplit.Length; cnt++)
                    {
                        if (strSplit[cnt] == "+")
                        {
                            opr = "+";
                        }
                        else if (strSplit[cnt] == "-")
                        {
                            opr = "-";
                        }
                        else if (strSplit[cnt] == "*")
                        {
                            opr = "*";
                        }
                        else if (strSplit[cnt] == "%")
                        {
                            opr = "%";
                        }
                        else
                        {
                            if (opr != "")
                            {
                                if (Convert.ToString(strSplit[cnt]).Contains("#") == true)
                                {
                                    hashVal = Convert.ToString(strSplit[cnt]).Replace("#", "");
                                    hashVal = (hashVal == "" ? "0" : hashVal);
                                }
                                else
                                {
                                    hashVal = (string)ht[strSplit[cnt]];
                                    hashVal = (hashVal == "" ? "0" : hashVal);
                                    hashVal = lblId.Text.Trim() == "F0000050" ? Convert.ToString(inv) : hashVal;
                                }
                                sum = calculate(opr, sum, Convert.ToDouble(hashVal));
                                opr = "";
                            }
                            else
                            {
                                if (Convert.ToString(strSplit[cnt]).Contains("#") == true)
                                {
                                    hashVal = Convert.ToString(strSplit[cnt]).Replace("#", "");
                                    hashVal = (hashVal == "" ? "0" : hashVal);
                                }
                                else
                                {
                                    hashVal = (string)ht[strSplit[cnt]];
                                    hashVal = (hashVal == "" ? "0" : hashVal);
                                }
                                sum = Convert.ToDouble(hashVal);
                            }
                        }
                    }

                    txtVal.Text = Convert.ToString(sum);
                    sum = 0;
                    FooterCnt += 1;
                }

                ht.Add(Convert.ToString(keyNum), txtVal.Text);
                keyNum += 1;
            }
        }

        private double calculate(string opr, double amount1, double amount2)
        {

            if (opr == "+")
            {
                amount1 = amount1 + amount2;
            }
            else if (opr == "-")
            {
                amount1 = amount1 - amount2;
            }
            else if (opr == "*")
            {
                amount1 = amount1 * amount2;
            }
            else if (opr == "%")
            {
                amount1 = (amount1 * amount2) / 100;
            }
            return amount1;
        }

        protected void chkIsSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            ViewState["chk80EE"] = chk.Checked == true ? true : false;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("Print12BB.aspx", false);
        }

        protected void btnPrintIndiafirst_Click(object sender, EventArgs e)
        {
            Response.Redirect("Print12BB.aspx", false);
        }

        protected void btnUpload12BB_Click(object sender, EventArgs e)
        {
            Response.Redirect("Upload12BB.aspx", false);
        }

        protected void btnUploadInv_Click(object sender, EventArgs e)
        {
            Response.Redirect("Support.aspx", false);
        }

        protected void btnClearInv_Click(object sender, EventArgs e)
        {
            try
            {
                string confirmValue = Request.Form["confirm_value"];
                if (confirmValue.Trim() == "Yes")
                {
                    string filePath;
                    if ((string)Session["sCompID"] == "CO000114")
                    {

                        filePath = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf";

                    }
                    else
                    {

                        filePath = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf";

                    }
                    if (File.Exists(filePath)) File.Delete(filePath);


                    if ((string)Session["sCompID"] == "CO000114")
                    {


                        var directories = Directory.GetDirectories(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\");
                        foreach (var item in directories)
                        {
                            DirectoryInfo di = new DirectoryInfo(item + "\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\");
                            if (di.Exists)
                                di.Delete(true);
                        }

                        objInv.Clear_Investment((string)Session["sCompID"], (string)Session["sEmpID"]);
                        FillOther();
                        Fill_Amounts();
                        lblMessage.Text = "All data cleared successfully";
                    }
                    else
                    {


                        var directories = Directory.GetDirectories(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\");
                        foreach (var item in directories)
                        {
                            DirectoryInfo di = new DirectoryInfo(item + "\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\");
                            if (di.Exists)
                                di.Delete(true);
                        }

                        objInv.Clear_Investment((string)Session["sCompID"], (string)Session["sEmpID"]);
                        FillOther();
                        Fill_Amounts();
                        lblMessage.Text = "All data cleared successfully";
                    }


                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvRent_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtAmt = (TextBox)e.Row.FindControl("txtAmt");
                    TextBox txtPAN = (TextBox)e.Row.FindControl("txtPAN");
                    Label lblLimit = (Label)e.Row.FindControl("lblLimitLand");
                    Label lblId = (Label)e.Row.FindControl("lblId");
                    Label lblDesc = (Label)e.Row.FindControl("lblDesc");
                    CheckBox chkAgree = (CheckBox)e.Row.FindControl("chkAgree");

                    if (lblId.Text == "PS999999")
                    {
                        txtAmt.Visible = false;
                        txtPAN.Visible = false;
                        chkAgree.Visible = true;
                        lblDesc.Text = "Disclaimer: <br />" + lblDesc.Text;
                        lblDesc.Font.Bold = true;
                    }

                    if ((string)Session["sCompId"] == "CO000015")
                    {
                        txtAmt.Text = txtAmt.Text.Replace(".00", "");
                        lblLimit.Text = lblLimit.Text.Replace(".00", "");
                    }
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void gvRentNew_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtAmt = (TextBox)e.Row.FindControl("txtAmt");
                    Label lblLimit = (Label)e.Row.FindControl("lblLimitLand");

                    if ((string)Session["sCompId"] == "CO000015")
                    {
                        txtAmt.Text = txtAmt.Text.Replace(".00", "");
                        lblLimit.Text = lblLimit.Text.Replace(".00", "");
                    }
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void lnkDownload_Click(object sender, EventArgs e)
        {
            try
            {
                smInv.RegisterPostBackControl(lnkDownload);

                if ((string)Session["sCompID"] == "CO000056")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/indiafirst/Investments_Support_Forms.zip"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Investments_Support_Forms.zip");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                //else if ((string)Session["sCompID"] == "CO000101")
                //{
                //    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/hincol/HINCOL_DECLARATION_FORMS.zip"));
                //    if (fileObj.Exists)
                //    {
                //        Response.Clear();
                //        Response.Buffer = true;
                //        Response.AppendHeader("content-disposition", @"attachment; filename=HINCOL_DECLARATION_FORMS.zip");
                //        Response.ContentType = "text/csv";
                //        Response.WriteFile(fileObj.FullName);
                //        Response.End();
                //    }
                //}

                //byte[] Content = File.ReadAllBytes(Server.MapPath("~/downloads/indiafirst/Investments_Support_Forms.zip"));
                //Response.ContentType = "application/octet-stream";
                //Response.AddHeader("content-disposition", "attachment; filename=" + "Investments_Support_Forms.zip");
                //Response.BufferOutput = true;
                //Response.OutputStream.Write(Content, 0, Content.Length);
                //Response.Flush();
                //Response.End();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void lnkManual_Click(object sender, EventArgs e)
        {
            try
            {
                smInv.RegisterPostBackControl(lnkManual);

                if ((string)Session["sCompID"] == "CO000114")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/angel/Investment_Proof_Submission_Procedure.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Investment_Proof_Submission_Procedure.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000141")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/NPL/Investment_Proof_submission_Procedure.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Investment_Proof_submission_Procedure.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000106")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/staubli/Investment_Proof_submission_Procedure.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Investment_Proof_submission_Procedure.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }

                //byte[] Content = File.ReadAllBytes(Server.MapPath("~/downloads/indiafirst/Investments_Support_Forms.zip"));
                //Response.ContentType = "application/octet-stream";
                //Response.AddHeader("content-disposition", "attachment; filename=" + "Investments_Support_Forms.zip");
                //Response.BufferOutput = true;
                //Response.OutputStream.Write(Content, 0, Content.Length);
                //Response.Flush();
                //Response.End();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void btnDiscrepancy_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("DiscrepancyReport.aspx", true);
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void lnkDeclarationsAngel_Click(object sender, EventArgs e)
        {
            try
            {
                smInv.RegisterPostBackControl(lnkDeclarationsAngel);

                if ((string)Session["sCompID"] == "CO000114")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/angel/Declaration_Documents.zip"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_Documents.zip");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void btnFlexiReport_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("FlexiReport.aspx", true);
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void btnClearRent_Click(object sender, EventArgs e)
        {
            try
            {
                NewPortal2023.ESS.Investments objInv = new NewPortal2023.ESS.Investments();

                foreach (GridViewRow gvr in this.gvLandlordDetails.Rows)
                {
                    ((TextBox)gvr.FindControl("txtAmtLnd")).Text = "";

                }

                foreach (GridViewRow gvr in this.gvRent.Rows)
                {
                    ((TextBox)gvr.FindControl("txtAmt")).Text = "";
                    ((TextBox)gvr.FindControl("txtPAN")).Text = "";
                }

                txtAddress.Text = "";

                string result;
                result = objInv.UpdateInvestment(MakeInvXml(gvRent, "Rent"), txtAddress.Text.Trim(), "Rent", (string)Session["sCompID"], (string)Session["sEmpID"]);
                result = objInv.UpdateInvestment(MakeInvXml(gvLandlordDetails, "Landlord"), "", "", (string)Session["sCompID"], (string)Session["sEmpID"]);

                if (result.ToString().Trim() == "success")
                {
                    lblMessage.Text = "Rent data cleared successfully.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Rent data cleared successfully.');");
                    CalculateInvestments(gvRent, "RENT");
                    lnkRent.Text = "3. Rent Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", 0.00).Replace("रु ", "") + ")";
                }
                else
                {
                    lblMessage.Text = "Error occurred in application.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating rent.');");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnUpload12BSupport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";

                if (fupPrev.PostedFile != null)
                {
                    if (Convert.ToString(fupPrev.PostedFile.FileName) == "")
                    {
                        lblMessage.Text = "Browse file to upload.";
                        return;
                    }
                    else
                    {
                        UploadFile12B();
                        lnkDownload12BSupport.Visible = true;
                    }
                }
                else
                {
                    lblMessage.Text = "Browse file to upload.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void UploadFile12B()
        {
            try
            {
                NewPortal2023.ESS.Support objSup = new NewPortal2023.ESS.Support();

                string savePath;
                if (fupPrev.PostedFile.FileName == "")
                {
                    lblMessage.Text = "Browse document.";
                    return;
                }

                //int fCount = Directory.GetFiles(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Documents\\Declaration", Convert.ToString(Session["sEmpCode"]) + "*", SearchOption.AllDirectories).Length;
                //if (fCount < 1)
                //{
                //    lblMessage.Text = "Upload Investment Support Form first before uploading supporting documents.";
                //    return;
                //}

                //string pathPDF = "";
                //string pathZIP = "";
                //string pathRAR = "";
                //string path7Z = "";
                //string pathJPG = "";
                //string pathJPEG = "";
                //pathPDF = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf";
                //pathZIP = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".zip";
                //pathRAR = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".rar";
                //path7Z = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".7z";
                //pathJPG = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpg";
                //pathJPEG = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpeg";

                //if (File.Exists(pathPDF) || File.Exists(pathZIP) || File.Exists(pathRAR) || File.Exists(path7Z) || File.Exists(pathJPG) || File.Exists(pathJPEG))
                //{

                //}
                //else
                //{
                //    lblMessage.Text = "Upload signed Form 12BB before uploading supporting documents.";
                //    return;
                //}

                if (System.IO.Path.GetExtension(fupPrev.PostedFile.FileName).ToUpper() == ".JPEG" || System.IO.Path.GetExtension(fupPrev.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupPrev.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupPrev.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupPrev.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupPrev.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    lblMessage.Text = "Only PDF,JPG/JPEG,7Z,ZIP and RAR files allowed.";
                    return;
                }

                savePath = Request.PhysicalApplicationPath;
                if ((string)Session["sCompID"] == "CO000114")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                }
                else
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                }
                //savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                System.IO.Stream fileInputStream = fupPrev.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                fileInputStream.Close();
                /* Create Folder */

                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = Path.GetFileNameWithoutExtension(fupPrev.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupPrev.FileName.Trim());

                string filesToDelete = "12B_*";
                string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
                foreach (string file in fileList)
                {
                    System.IO.File.Delete(file);
                }

                //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                //{
                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + "12B_" + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();
                objSup.Upload_Support_Password((string)Session["sCompID"], (string)Session["sEmpID"], "12B_" + fileName, "2021-2022", txtPassword12B.Text.Trim());
                //DisplayDocuments();
                lblMessage.Text = "File uploaded successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;  //"Error occurred in application.";
            }
        }

        protected void lnkDownload12BSupport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";

                string SourcePath = "";
                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                }
                else
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                }
                if (Directory.Exists(SourcePath))
                {
                    string filesToDelete = "12B_*";
                    string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToDelete);
                    foreach (string file in fileList)
                    {
                        System.IO.FileInfo fileObj = new System.IO.FileInfo(file);
                        if (fileObj.Exists)
                        {
                            Response.Clear();
                            Response.Buffer = true;
                            Response.AppendHeader("content-disposition", @"attachment; filename=" + Path.GetFileName(file));
                            Response.ContentType = "text/csv";
                            Response.WriteFile(fileObj.FullName);
                            Response.End();
                        }
                        else
                        {
                            lblMessage.Text = "File not found.";
                        }
                    }
                }
                else
                {
                    lblMessage.Text = "File not found.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void btnSaveOption_Click(object sender, EventArgs e)
        {
            try
            {
                if (!RadioButton1.Checked && !RadioButton2.Checked)
                {
                    lblMessage.Text = "Select Tax Option.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Select Tax Option.');");
                }
                else
                {
                    if (RadioButton1.Checked)
                    {
                        objTax.SaveTaxOption((string)Session["sCompID"], (string)Session["sEmpID"], "O");
                        RadioButton1.Checked = true;

                    }
                    else if (RadioButton2.Checked)
                    {
                        objTax.SaveTaxOption((string)Session["sCompID"], (string)Session["sEmpID"], "N");
                        RadioButton2.Checked = true;
                    }

                    lblMessage.Text = "Selected Tax Option saved successfully.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Selected Tax Option saved successfully.');");

                    Response.Redirect("InvestmentDetails.aspx", false);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        private void FillCTC()
        {
            try
            {
                lblMessage.Text = "";
                dsInv = objTax.GetPDFAmounts((string)Session["sCompID"], (string)Session["sEmpID"]);
                if (dsInv.Tables[0].Rows[0][0].ToString() == "O")
                {
                    RadioButton1.Checked = true;
                }
                else if (dsInv.Tables[0].Rows[0][0].ToString() == "N")
                {
                    RadioButton2.Checked = true;
                }
                else
                {
                    RadioButton1.Checked = false;
                    RadioButton2.Checked = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void CheckAmount()
        {
            double amount = 0;

            foreach (GridViewRow gvr in this.gvInv.Rows)
            {

                Label lblId = (Label)gvr.FindControl("lblId");
                TextBox txtAmt = (TextBox)gvr.FindControl("txtAmt");


                if (lblId != null && txtAmt != null)
                {

                    double enteredAmount = 0;
                    if (double.TryParse(txtAmt.Text, out enteredAmount) && enteredAmount > 0)
                    {

                        double limit = 0;


                        switch (lblId.Text)
                        {
                            case "I0000027":
                                limit = 125000;
                                break;
                            case "I0000041":
                                limit = 100000;
                                break;
                            case "I0000036":
                                limit = 125000;
                                break;
                            case "I0000043":
                                limit = 150000;
                                break;
                            default:
                                limit = double.MaxValue;
                                break;
                        }


                        if (enteredAmount > limit)
                        {
                            lblMessage.Text = $"Amount cannot exceed {limit}.";
                            objcommon.Display("Validate", $"DisplayErrorMessage('Amount cannot exceed {limit}.');");


                            txtAmt.Text = "";
                            return;
                        }

                        if (lblId.Text == "I0000034")
                        {
                            DisableTextBox("I0000003");
                        }

                        else if (lblId.Text == "I0000003")
                        {
                            DisableTextBox("I0000034");
                        }
                    }
                    else
                    {
                        if (lblId.Text == "I0000034")
                        {
                            EnableTextBox("I0000003");
                        }
                        else if (lblId.Text == "I0000003")
                        {
                            EnableTextBox("I0000034");
                        }
                    }
                }
            }
        }

        private void DisableTextBox(string invAid)
        {

            foreach (GridViewRow row in gvInv.Rows)
            {

                Label lbl = (Label)row.FindControl("lblId");
                TextBox txtAmt = (TextBox)row.FindControl("txtAmt");

                if (lbl != null && txtAmt != null && lbl.Text == invAid)
                {
                    txtAmt.Enabled = false;
                    txtAmt.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }

        private void EnableTextBox(string invAid)
        {

            foreach (GridViewRow row in gvInv.Rows)
            {

                Label lbl = (Label)row.FindControl("lblId");
                TextBox txtAmt = (TextBox)row.FindControl("txtAmt");

                if (lbl != null && txtAmt != null && lbl.Text == invAid)
                {
                    txtAmt.Enabled = true;
                    txtAmt.BackColor = System.Drawing.Color.White;
                }
            }
        }

        protected void txtPAN_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string value = "";
                foreach (GridViewRow gvr in this.gvRent.Rows)
                {
                    TextBox txtPAN = (TextBox)gvr.FindControl("txtPAN");
                    string isValid = txtPAN.Text;

                    if (txtPAN.Text.Trim().Length != 10 && txtPAN.Text.Trim() != "")
                    {
                        lblMessage.Text = "Invalid PAN (Rent Declaretion)";
                        objcommon.Display("Validate", "DisplayErrorMessage('Invalid PAN(Rent Declaretion)');");
                        txtPAN.Text = "";
                    }

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private Boolean CheckPrevAmount()
        {
            double amount = 0;
            foreach (GridViewRow gvr in this.gvTwelB.Rows)
            {
                if (((Label)gvr.FindControl("lblId")).Text == "AD000001")
                {
                    amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                    break;
                }
            }

            if (amount != 0)
            {
                return true;
            }

            return false;
        }

        private bool CheckFileUploads()
        {
            if (CheckPrevAmount())
            {
                if (lnkDownload12BSupport.Visible == false)
                {
                    lblMessage.Text = "Please upload previous employer final settlement proof.";
                    lblMessage.BackColor = System.Drawing.Color.Tomato;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    objcommon.Display("Validate", "DisplayErrorMessage('Please upload previous employer final settlement proof.');");
                    return false;
                }
            }

            return true;
        }

        protected void btnSubmitAll_Click(object sender, EventArgs e)
        {
            try
            {
                SubmitAll();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void SubmitAll()
        {
            string result;
            try
            {
                if (ValidateOther() == false) //COMMENTED FOR 2ND PHASE OF INVESTMENT
                {
                    return;
                }
                if (ValidateRent() == false)
                {
                    return;
                }
                if (ValidateTwelve() == false)
                {
                    return;
                }
                if (ValidateTwelveB() == false)
                {
                    return;
                }

                if (!ignoreForUploadCheck.Contains(Convert.ToString(Session["sCompID"])))
                {
                    if (CheckFileUploads() == false)
                    {
                        return;
                    }
                }

                string other = "";
                string inv = "";
                string rent = "";
                string landlord = "";
                string twelve = "";
                string lender = "";
                string prev = "";

                gvOther.Visible = true;
                gvInv.Visible = true;
                gvRent.Visible = true;
                gvLandlordDetails.Visible = true;
                gvtwelve.Visible = true;
                gvLenderDetails.Visible = true;
                gvTwelB.Visible = true;

                other = MakeInvXml(gvOther, "Other");
                inv = MakeInvXml(gvInv, "");
                rent = MakeInvXml(gvRent, "Rent");
                landlord = MakeInvXml(gvLandlordDetails, "Landlord");
                twelve = MakeInvXml(gvtwelve, "12");
                lender = MakeInvXml(gvLenderDetails, "Lender");
                prev = MakeInvXml(gvTwelB, "");

                result = objInv.UpdateInvestmentAll(other, inv, rent, landlord, twelve, lender, prev,
                            txtAddress.Text.Trim(), "Rent", (string)Session["sCompID"], (string)Session["sEmpID"]);

                if (result.ToString().Trim() == "success")
                {
                    try
                    {
                        if (result.ToString().Trim() == "success")
                        {

                            Response.Redirect("TaxSimulator.aspx?ID=INVD", true);

                            //lblMessage.Text = "Successfuly updated investments details.";

                            //lblMessage.BackColor = System.Drawing.Color.GreenYellow;
                            //lblMessage.ForeColor = System.Drawing.Color.DarkGreen;
                            //objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments details.');");

                            //Session["Message"] = "Successfuly updated investments details.";
                        }
                        else
                        {
                            lblMessage.Text = result;
                            lblMessage.BackColor = System.Drawing.Color.Tomato;
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            objcommon.Display("Validate", "DisplayErrorMessage('" + result + "');");
                        }
                        FillAll();
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GenerateAndEmailForm12BB()
        {
            try
            {
                Response.Redirect("Print12BBSingle.aspx");
            }
            catch (ThreadAbortException tex)
            {
                if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                {
                    System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                }
            }
        }

        private void LocalReport_SubreportProcessing(object sender, SubreportProcessingEventArgs e)
        {
            RegisterGridviewUploadControls();

            e.DataSources.Add(new ReportDataSource("dsEmp_dt80C", DsTest.Tables[1]));
            e.DataSources.Add(new ReportDataSource("dsEmp_dt80CCD", DsTest.Tables[2]));
            e.DataSources.Add(new ReportDataSource("dsEmp_dtOtherSec", DsTest.Tables[3]));
            e.DataSources.Add(new ReportDataSource("dsEmp_dtForceVisible", DsTest.Tables[4]));
            if ((string)Session["sCompId"] == "CO000056")
            {
                e.DataSources.Add(new ReportDataSource("dsEmp_dtFlexiReimb", DsTest.Tables[5]));
            }
        }

        private Boolean ValidateRent()
        {
            double amount = 0;
            double limit = 0;
            double landLimit = 0;
            string PAN = "";
            string INV = "";
            string DESC = "";
            string entrytype = "";
            int entrylength = 0;
            string landlord1 = "";
            string landlord2 = "";


            foreach (GridViewRow gvr in this.gvRent.Rows)
            {
                amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));

                if (amount != 0)
                {
                    if (txtAddress.Text.Trim() == "")
                    {
                        lblMessage.Text = "Please enter rented property address.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Please enter rented property address.');");
                        return false;
                    }

                    foreach (GridViewRow gvr1 in this.gvLandlordDetails.Rows)
                    {

                        landlord1 = ((TextBox)gvr1.FindControl("txtAmtLnd")).Text.Trim();
                        landlord2 = ((TextBox)gvr1.FindControl("txtAmtLnd")).Text.Trim();
                        Label lblId = (Label)gvr1.FindControl("lblid");


                        if (amount > 0)
                        {

                            if (lblId != null && lblId.Text.Trim() == "L0000001")
                            {
                                if (string.IsNullOrWhiteSpace(landlord1))
                                {
                                    lblMessage.Text = "First Landlord Name is mandatory.";
                                    objcommon.Display("Validate", "DisplayErrorMessage('First Landlord Name is mandatory.');");
                                    return false;
                                }
                            }

                            if (lblId != null && lblId.Text.Trim() == "L0000002")
                            {
                                if (string.IsNullOrWhiteSpace(landlord1))
                                {
                                    lblMessage.Text = "First Landlord Address is mandatory.";
                                    objcommon.Display("Validate", "DisplayErrorMessage('First Landlord Address is mandatory.');");
                                    return false;
                                }
                            }
                        }


                    }



                }


            }





            double maxRent = 0;
            foreach (GridViewRow gvr in this.gvRent.Rows)
            {

                amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                PAN = ((TextBox)gvr.FindControl("txtPAN")).Text.Trim();
                limit = Convert.ToDouble((((Label)gvr.FindControl("lblLimit")).Text == "" || ((Label)gvr.FindControl("lblLimit")).Text == "0.00" ? "0" : ((Label)gvr.FindControl("lblLimit")).Text));
                landLimit = Convert.ToDouble((((Label)gvr.FindControl("lblLimitLand")).Text == "" || ((Label)gvr.FindControl("lblLimitLand")).Text == "0.00" ? "0" : ((Label)gvr.FindControl("lblLimitLand")).Text));

                if (limit > 0)
                {
                    if (amount > limit)
                    {
                        lblMessage.Text = "Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + "');");
                        return false;
                    }
                }

                if (landLimit > 0)
                {
                    if (amount > landLimit && PAN.Trim() == "")
                    {
                        lblMessage.Text = "Please enter PAN of Landlord when rent amount is greater than Landlord limit.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Please enter PAN of Landlord when rent amount is greater than Landlord limit.');");
                        return false;
                    }
                }

                if (maxRent < amount)
                {
                    maxRent = amount;
                }
            }

            foreach (GridViewRow gvr in this.gvLandlordDetails.Rows)
            {

                PAN = ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim();
                INV = ((Label)gvr.FindControl("lblId")).Text.Trim();
                DESC = ((Label)gvr.FindControl("lblDesc")).Text.Trim();

                if (INV.Contains("L0000003") || INV.Contains("L0000006"))
                {
                    if (PAN.Length > 10)
                    {
                        objcommon.Display("Validate", "DisplayErrorMessage('" + DESC + " - Invalid PAN.');");
                        lblMessage.Text = DESC + " - Invalid PAN.";
                        return false;
                    }

                    if (INV.Contains("L0000003") && maxRent > 8333)
                    {
                        if (PAN.Trim() == "")
                        {
                            lblMessage.Text = "Please enter First Landlord PAN when rent amount is greater than Landlord limit.";
                            objcommon.Display("Validate", "DisplayErrorMessage('Please enter First Landlord PAN when rent amount is greater than Landlord limit.');");
                            return false;
                        }
                    }
                }

                entrytype = ((Label)gvr.FindControl("lblentrytype")).Text;
                entrylength = Convert.ToInt32(((Label)gvr.FindControl("lblentrylen")).Text);

                try
                {
                    //if (((Label)gvr.FindControl("lblEntryMode")).Text == "C" && ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length == 0)
                    //{
                    //    lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.";
                    //    objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.');");
                    //    return false;
                    //}

                    if (entrylength != ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length && entrylength != -1)
                    {
                        lblMessage.Text = "Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "Error occurred in application.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Enter data in number for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                    return false;
                }
            }

            return true;
        }
    }
}