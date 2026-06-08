using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
//using HBS.Encoder;
using Microsoft.Reporting.WebForms;


namespace NewPortal2023.ESS
{
    public partial class FlexiSupport : System.Web.UI.Page
    {
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();
        NewPortal2023.ESS.FlexiPay objFlexi = new NewPortal2023.ESS.FlexiPay();
        NewPortal2023.ESS.Common objCommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Support objInv = new NewPortal2023.ESS.Support();
        DataTable dtInv = new DataTable();
        DataSet dsInv = new DataSet();
        private string SourcePath = string.Empty;
        private string savePath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Session["Error"] = "Action not allowed";
                //Response.Redirect("../ErrorPage.aspx", true);

                if (!Page.IsPostBack)
                {
                    //if (Request.QueryString["key"] != null)
                    //{
                    //    string key = Request.QueryString["key"].Replace(" ", "+");
                    //    string keyDecrypted = TokenManager.DecryptStringAES(key.Trim(), "#hsenid$");

                    //    string[] id = new string[4];
                    //    id = keyDecrypted.Split(';');

                    //    string empid = id[0].Replace("empid=", "").Trim();
                    //    string token = id[2].Replace("token=", "").Trim();

                    //    DateTime dtToken = DateTime.ParseExact(token.Replace("+", " "), "M/d/yyyy h:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);
                    //    DateTime dtTokenNow = dtToken.AddMinutes(330);
                    //    DateTime dtTokenPlus = dtToken.AddMinutes(345);

                    //    if (DateTime.Now.AddMinutes(15) < dtTokenNow)
                    //    {
                    //        lblMessage.Text = "Token invalid.";
                    //        Session["Error"] = "Token invalid.";
                    //        Response.Redirect("../ErrorPage.aspx", true);
                    //    }
                    //    else if (DateTime.Now > dtTokenPlus)
                    //    {
                    //        lblMessage.Text = "Token invalid.";
                    //        Session["Error"] = "Token invalid.";
                    //        Response.Redirect("../ErrorPage.aspx", true);
                    //    }

                    //    objCommon.Validate_Login("ANGEL", empid, objUser);

                    //    if (objUser.EmpId != null)
                    //    {
                    //        if (objUser.EmpId.ToString() != "")
                    //        {
                    //            Session["sEmpID"] = objUser.EmpId.Trim();
                    //            Session["sCompID"] = objUser.CompId.Trim();
                    //            Session["sEmpCode"] = objUser.EmpCode.Trim();
                    //            Session["sEmpName"] = objUser.EmpName.Trim();
                    //            Session["sDesignation"] = objUser.Designation.Trim();
                    //            Session["sLocation"] = objUser.Location.Trim();
                    //            Session["sJoinDate"] = objUser.JoinDate.Trim();
                    //            Session["sPAN"] = objUser.PAN.Trim();
                    //            Session["SMSURL"] = objUser.SMSURL.Trim();
                    //            Session["sGrade"] = objUser.Grade.Trim();
                    //            Session["sEmailId"] = objUser.EmailId.Trim();
                    //            Session["sLastLogin"] = objUser.LastLogin.Trim();
                    //            Session["IsLogin"] = true;

                    //            if ((string)Session["sEmpCode"] != "A00001")
                    //            {
                    //                ArrayList ArrListName = new ArrayList();
                    //                ArrayList ArrListValue = new ArrayList();

                    //                objCommon.Get_Menu(ArrListName, ArrListValue, (string)(Session["sCompID"]), (string)(Session["sEmpID"]));

                    //                if (!ArrListValue.Contains(objCommon.GetCurrentPageName(Request.Url.AbsolutePath)))
                    //                {
                    //                    //Session["Error"] = "Not allowed.";
                    //                    //Response.Redirect("../ErrorPage.aspx", true);

                    //                    ViewState["UploadRights"] = false;

                    //                    rowButtons.Visible = false;
                    //                    btnSubmit.Visible = false;
                    //                    rowFileUpload.Visible = false;
                    //                    rowButtonUpload.Visible = false;
                    //                }
                    //                else
                    //                {
                    //                    ViewState["UploadRights"] = true;

                    //                    rowButtons.Visible = false;
                    //                    btnSubmit.Visible = false;
                    //                    rowFileUpload.Visible = true;
                    //                    rowButtonUpload.Visible = true;
                    //                }
                    //            }

                    //            FormsAuthentication.SetAuthCookie(objUser.EmpName.Trim(), false);

                    //            objCommon.UpdateLoginDateTime((string)Session["sCompID"], (string)Session["sEmpCode"], DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), HttpUtility.HtmlEncode(keyDecrypted));

                    //            ViewState["isExternalAccess"] = true;

                    //            if ((string)Session["sCompID"] == "CO000114")
                    //            {
                    //                Menu navMenu = (Menu)Master.FindControl("NavigationMenu");
                    //                navMenu.Visible = false;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            lblMessage.Text = "Employee does not exists (EMPBLANK).";
                    //            Session["Error"] = "Employee does not exists (EMPBLANK).";
                    //            Response.Redirect("../ErrorPage.aspx", true);
                    //        }
                    //    }
                    //    else
                    //    {
                    //        lblMessage.Text = "Employee does not exists (EMPNULL).";
                    //        Session["Error"] = "Employee does not exists (EMPNULL).";
                    //        Response.Redirect("../ErrorPage.aspx", true);
                    //    }
                    //}

                    if (Session["sCompID"] != null)
                    {
                        string strResult = objCommon.Validate_ControlInfo("FLEX");

                        if (strResult.Contains("This page is currently unavailable.") == true)
                        {
                            Response.Redirect("Unavailable.aspx?strName=Flexi Pay Details");
                            return;
                        }

                        FillCTC();
                        if ((string)Session["sCompID"] == "CO000114")
                        {
                            instructionsDiv.Visible = true;
                            divOtherComp.Visible = false;
                            divUplSupp.Visible = true;
                            trQuarter.Visible = false;
                            trQut.Visible = false;
                            msg.Visible = false;
                        }
                        else
                        {
                            instructionsDiv.Visible = false;
                            divOtherComp.Visible = true;
                            divUplSupp.Visible = false;
                            getQuarter();
                            trQut.Visible = true;
                            trQuarter.Visible = true;
                            msg.Visible = true;
                        }

                        //if ((string)Session["sEmpCode"] != "A00001")
                        //{
                        ArrayList ArrListName = new ArrayList();
                        ArrayList ArrListValue = new ArrayList();

                        //objCommon.Get_Menu(ArrListName, ArrListValue, (string)(Session["sCompID"]), (string)(Session["sEmpID"]));

                        if (!ArrListValue.Contains(objCommon.GetCurrentPageName(Request.Url.AbsolutePath)))
                        {
                            //Session["Error"] = "Not allowed.";
                            //Response.Redirect("../ErrorPage.aspx", true);

                            ViewState["UploadRights"] = false;

                            rowButtons.Visible = false;
                            btnSubmit.Visible = false;
                            if (Session["sCompID"].ToString() == "CO000125")
                            {
                                rowFileUpload.Visible = true;
                            }
                            else
                            {
                                rowFileUpload.Visible = false;
                            }

                            //rowButtonUpload.Visible = false;
                        }
                        else
                        {
                            ViewState["UploadRights"] = true;

                            rowButtons.Visible = false;
                            btnSubmit.Visible = false;
                            if (Session["sCompID"].ToString() == "CO000125")
                            {
                                rowFileUpload.Visible = true;
                            }
                            else
                            {
                                rowFileUpload.Visible = false;
                            }
                            //rowButtonUpload.Visible = true;
                        }
                        //}
                    }
                    else
                    {
                        Response.Redirect("Logout.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }

        }

        private void FillCTC()
        {
            getQuarter();

            //if ((string)Session["sCompID"] != "CO000125")
            //{

            //    dtInv = objFlexi.GetFlexiCompensationVerified((string)Session["sCompID"], (string)Session["sEmpID"]);
            //    gvReceived.DataSource = dtInv;
            //    gvReceived.DataBind();
            //    dtInv = objFlexi.GetFlexiCompensation((string)Session["sCompID"], (string)Session["sEmpID"], drpFlexiHeads);
            //    gvCTC.DataSource = dtInv;
            //    gvCTC.DataBind();

            //}
            //else
            //{
            //    gvReceived.DataSource = null;
            //    gvReceived.DataBind();
            //    gvCTC.DataSource = null;
            //    gvCTC.DataBind();


            //}

            dtInv = objFlexi.GetFlexiCompensationVerified((string)Session["sCompID"], (string)Session["sEmpID"]);

            gvReceived.DataSource = dtInv;
            gvReceived.DataBind();
            if ((string)Session["sCompID"] == "CO000125")
            {
                dtInv = objFlexi.GetFlexiCompensationQTR((string)Session["sCompID"], (string)Session["sEmpID"], drpFlexiHeads, (string)ViewState["QTR"]);

                gvCTC.DataSource = dtInv;
                gvCTC.DataBind();

                gvBills.DataSource = null;
                gvBills.DataBind();
            }
            else
            {
                dtInv = objFlexi.GetFlexiCompensation((string)Session["sCompID"], (string)Session["sEmpID"], drpFlexiHeads);

                gvCTC.DataSource = dtInv;
                gvCTC.DataBind();

                gvBills.DataSource = null;
                gvBills.DataBind();

            }

            rowButtons.Visible = false;

            DisplayDocuments();
            DisplayDocumentsLTA();

            btnPrint.Visible = false;
            btnSubmit.Visible = false;

            reqDocs.Visible = false;

            DisplayDocumentsPrev();
        }

        protected void gvCTC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtAmountAccepted = (TextBox)e.Row.FindControl("txtAmountAccepted");
                TextBox txtAmountRejected = (TextBox)e.Row.FindControl("txtAmountRejected");
                Label lblAID = (Label)e.Row.FindControl("lblAID");
                Label lblDesc = (Label)e.Row.FindControl("lblDesc");
                if (lblAID.Text == "AD000159")
                {
                    tblLTA.Visible = true;
                    ViewState["LTAAID"] = lblAID.Text;
                    lblDesc.Text = lblDesc.Text + " (LTA APPLICATION IS MANDATORY)";
                }

                if ((string)Session["sCompID"] == "CO000125")
                {
                    txtAmountAccepted.Enabled = false;
                    txtAmountRejected.Enabled = false;
                    txtAmountAccepted.BackColor = System.Drawing.Color.LightGray;
                    txtAmountRejected.BackColor = System.Drawing.Color.LightGray;
                }
            }
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpFlexiHeads.SelectedIndex != 0)
                {
                    objCommon = new NewPortal2023.ESS.Common();
                    ViewState["FLEXISELECT"] = drpFlexiHeads.SelectedValue;

                    foreach (GridViewRow gvr in gvBills.Rows)
                    {
                        TextBox txtBillNo = (TextBox)gvr.FindControl("txtBillNo");
                        TextBox txtBillDate = (TextBox)gvr.FindControl("txtBillDate");

                        foreach (GridViewRow gvrin in gvBills.Rows)
                        {
                            if (!gvr.Equals(gvrin))
                            {
                                TextBox txtBillNoIn = (TextBox)gvrin.FindControl("txtBillNo");
                                TextBox txtBillDateIn = (TextBox)gvrin.FindControl("txtBillDate");

                                DateTime billDate;

                                if (txtBillDate.Text == txtBillDateIn.Text && txtBillNo.Text == txtBillNoIn.Text)
                                {
                                    lblMessage.Text = "Repeat bills not allowed.";
                                    objCommon.Display("Validate", "DisplayErrorMessage('Repeat bills not allowed.');");
                                    gvrin.BackColor = System.Drawing.Color.Yellow;
                                    return;
                                }
                            }
                        }
                    }

                    string xml = MakeCTCXml(gvBills, "");
                    if (xml != "")
                    {
                        if (objFlexi.UpdateFlexipayCompensation(xml, (string)Session["sCompID"], (string)Session["sEmpID"], drpFlexiHeads.SelectedValue) == false)
                        {
                            lblMessage.Text = "Error occurred in application.";
                            return;
                        }
                        FillCTC();
                        drpFlexiHeads.SelectedValue = (string)ViewState["FLEXISELECT"];
                        FillBills();
                        lblMessage.Text = "Details updated successfully.";
                        objCommon.Display("Validate", "DisplayErrorMessage('Details updated successfully.');");
                    }
                    else
                    {
                        lblMessage.Text = "Details for Particulars/Bill No./Bill Date/Amount are mandatory.";
                        objCommon.Display("Validate", "DisplayErrorMessage('Details for Particulars/Bill No./Bill Date/Amount are mandatory.');");
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating Flexi Pay details.');");
            }

        }

        private string MakeCTCXml(GridView GV, string strType)
        {
            objCommon = new NewPortal2023.ESS.Common();
            StringBuilder sbTaxDetails = new StringBuilder();
            string xmlInv = string.Empty;

            sbTaxDetails.Append("<ROOT>");

            foreach (GridViewRow gvr in GV.Rows)
            {
                gvr.BackColor = System.Drawing.Color.White;

                DateTime billDate;
                if (!DateTime.TryParseExact(((TextBox)gvr.FindControl("txtBillDate")).Text.Trim().Replace("/", "-"), "dd-MM-yyyy",
                                                       System.Globalization.CultureInfo.InvariantCulture,
                                                       System.Globalization.DateTimeStyles.None, out billDate))
                {
                    lblMessage.Text = "Date invalid.";
                    objCommon.Display("Validate", "DisplayErrorMessage('Date invalid.');");
                    gvr.BackColor = System.Drawing.Color.Yellow;
                    return "";
                }

                DateTime dtPrev = new DateTime(2019, 3, 31);
                if (billDate <= dtPrev)
                {
                    lblMessage.Text = "Only bills for current financial year allowed.";
                    objCommon.Display("Validate", "DisplayErrorMessage('Only bills for current financial year allowed.');");
                    gvr.BackColor = System.Drawing.Color.Yellow;
                    return "";
                }

                if (((TextBox)gvr.FindControl("txtParticulars")).Text.Trim() != "" && ((TextBox)gvr.FindControl("txtBillNo")).Text.Trim() != "" &&
                        ((TextBox)gvr.FindControl("txtBillDate")).Text.Trim() != "" && ((TextBox)gvr.FindControl("txtAmount")).Text.Trim() != "")
                {
                    sbTaxDetails.Append("<Flexi COMP_AID='" + (string)Session["sCompID"] + "'");
                    sbTaxDetails.Append(" EMP_AID='" + (string)Session["sEmpID"] + "'");
                    sbTaxDetails.Append(" ALLWDED_AID='" + drpFlexiHeads.SelectedValue + "'");
                    sbTaxDetails.Append(" PARTICUALRS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtParticulars")).Text.Trim()) + "'");
                    sbTaxDetails.Append(" PARTICUALRS2='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtParticulars2")).Text.Trim()) + "'");
                    sbTaxDetails.Append(" BILL_NO='" + ((TextBox)gvr.FindControl("txtBillNo")).Text.Trim() + "'");
                    sbTaxDetails.Append(" BILL_DATE='" + ((TextBox)gvr.FindControl("txtBillDate")).Text.Trim() + "'");
                    //sbTaxDetails.Append(" AMOUNT_ACCEPTED='" + ((TextBox)gvr.FindControl("txtAcceptedAmount")).Text.Trim() + "'");
                    //sbTaxDetails.Append(" REMARKS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text.Trim()) + "'");
                    sbTaxDetails.Append(" DATE_SAVED='" + ((Label)gvr.FindControl("lblDateSaved")).Text.Trim() + "'");
                    sbTaxDetails.Append(" AMOUNT='" + ((TextBox)gvr.FindControl("txtAmount")).Text.Trim() + "'/>");
                }
                else
                {
                    return "";
                }
            }

            sbTaxDetails.Append("</ROOT>");

            xmlInv = sbTaxDetails.ToString();

            return xmlInv;
        }

        protected string ReplaceSpecialCharacters(string inputString)
        {
            inputString = inputString.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&#39;").
                    Replace("\"", "&quot;");

            return inputString;
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double Limit = 0;
                double Amount = 0;

                TextBox txtAmount = (TextBox)sender;
                Label lblLimit = (Label)txtAmount.NamingContainer.FindControl("lblLimit");
                Label lbldetId = (Label)txtAmount.NamingContainer.FindControl("lbldetId");

                Limit = Convert.ToDouble((lblLimit.Text.Trim() == "" ? "0" : lblLimit.Text.Trim()));
                Amount = Convert.ToDouble((txtAmount.Text.Trim() == "" ? "0" : txtAmount.Text.Trim()));

                if (Amount > Limit)
                {
                    txtAmount.Text = "";
                    objCommon.Display("Validate", "DisplayErrorMessage('Entered amount cannot be greater than eligibility.');");
                    getQuarter();
                }
                else
                {
                    if (Session["sCompID"].ToString() == "CO000125")
                    {
                        if (drpQuarterType.SelectedValue != "0")
                        {
                            objFlexi.Amount = Convert.ToString(Amount);
                            DataSet ds = objFlexi.InsertFlexiCompensationBillsAmt((string)Session["sCompID"], (string)Session["sEmpID"], lbldetId.Text, drpQuarterType.SelectedValue);
                            getQuarter();
                        }
                        else
                        {
                            objCommon.Display("Validate", "DisplayErrorMessage('Please Select First Quarter Then Insert Submitted Bills.');");
                            lblMessage.Text = "Please Select First Quarter Then Insert Submitted Bills.";
                            getQuarter();
                        }

                    }
                    else
                    {
                        objFlexi.Amount = Convert.ToString(Amount);
                        string drpQtr = "";
                        DataSet ds = objFlexi.InsertFlexiCompensationBillsAmt((string)Session["sCompID"], (string)Session["sEmpID"], lbldetId.Text, drpQtr);
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnFlexiPrint_Click(object sender, EventArgs e)
        {
            Response.Redirect("FlexiCompensationPrint.aspx", false);
        }

        private DataTable CreateEmptyDocumentsStructure()
        {
            DataTable dtDocuments = new DataTable();
            dtDocuments.Columns.Add(new DataColumn("PARTICUALRS", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("PARTICUALRS2", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("BILL_NO", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("BILL_DATE", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("AMOUNT", System.Type.GetType("System.String")));
            //dtDocuments.Columns.Add(new DataColumn("AMOUNT_ACCEPTED", System.Type.GetType("System.String")));
            //dtDocuments.Columns.Add(new DataColumn("AMOUNT_REJECTED", System.Type.GetType("System.String")));
            //dtDocuments.Columns.Add(new DataColumn("REMARKS", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("ALLWDED_AID", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("DATE_SAVED", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("Doc_Data_Id", System.Type.GetType("System.String")));

            return dtDocuments;
        }

        protected void lnkBtnAddDocRow_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpFlexiHeads.SelectedIndex == 0)
                {
                    objCommon = new NewPortal2023.ESS.Common();
                    lblMessage.Text = "Select Flexi Head.";
                    objCommon.Display("Validate", "DisplayErrorMessage('" + objCommon.EncodeJsString("Select Flexi Head.") + "');");
                }

                DataTable dtCOInfo = CreateEmptyDocumentsStructure();
                objCommon = new NewPortal2023.ESS.Common();
                lblMessage.Text = "";

                Int32 rowCount = 1;

                foreach (GridViewRow gvrCO in this.gvBills.Rows)
                {
                    DataRow drCORow = dtCOInfo.NewRow();

                    drCORow["PARTICUALRS"] = ((TextBox)gvrCO.FindControl("txtParticulars")).Text;
                    drCORow["PARTICUALRS2"] = ((TextBox)gvrCO.FindControl("txtParticulars2")).Text;
                    drCORow["BILL_NO"] = ((TextBox)gvrCO.FindControl("txtBillNo")).Text;
                    drCORow["BILL_DATE"] = ((TextBox)gvrCO.FindControl("txtBillDate")).Text;
                    drCORow["AMOUNT"] = ((TextBox)gvrCO.FindControl("txtAmount")).Text;
                    //drCORow["AMOUNT_ACCEPTED"] = ((TextBox)gvrCO.FindControl("txtAcceptedAmount")).Text;
                    //drCORow["AMOUNT_REJECTED"] = ((TextBox)gvrCO.FindControl("txtRejectedAmount")).Text;
                    //drCORow["REMARKS"] = ((TextBox)gvrCO.FindControl("txtRemarks")).Text;
                    drCORow["ALLWDED_AID"] = ((Label)gvrCO.FindControl("lbldetId")).Text;
                    drCORow["DATE_SAVED"] = ((Label)gvrCO.FindControl("lblDateSaved")).Text;
                    drCORow["Doc_Data_Id"] = ((Label)gvrCO.FindControl("lblDocDataId")).Text;

                    dtCOInfo.Rows.Add(drCORow);

                    rowCount += 1;
                }

                DataRow drNewCORow = dtCOInfo.NewRow();
                drNewCORow["Doc_Data_Id"] = Guid.NewGuid().ToString();
                drNewCORow["PARTICUALRS"] = "";
                drNewCORow["PARTICUALRS2"] = "";
                drNewCORow["BILL_NO"] = "";
                drNewCORow["BILL_DATE"] = "";
                drNewCORow["AMOUNT"] = "";
                //drNewCORow["AMOUNT_ACCEPTED"] = "";
                //drNewCORow["AMOUNT_REJECTED"] = "";
                //drNewCORow["REMARKS"] = "";
                drNewCORow["ALLWDED_AID"] = "";
                drNewCORow["DATE_SAVED"] = "";
                dtCOInfo.Rows.Add(drNewCORow);

                ViewState["Bills"] = dtCOInfo;
                this.gvBills.DataSource = dtCOInfo;
                this.gvBills.DataBind();
            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();
                lblMessage.Text = "Error occurred in application.";
                objCommon.Display("Validate", "DisplayErrorMessage('" + objCommon.EncodeJsString("Error occurred in application.") + "');");
            }
        }

        protected void lnkBtnDeleteDocRow_Click(object sender, EventArgs e)
        {
            try
            {
                int rowCount = 1;

                DataTable dtCOInfo = CreateEmptyDocumentsStructure();
                lblMessage.Text = "";

                foreach (GridViewRow gvrCO in this.gvBills.Rows)
                {
                    if (((CheckBox)gvrCO.FindControl("chkSelect")).Checked == false)
                    {
                        DataRow drCORow = dtCOInfo.NewRow();

                        drCORow["PARTICUALRS"] = ((TextBox)gvrCO.FindControl("txtParticulars")).Text;
                        drCORow["PARTICUALRS2"] = ((TextBox)gvrCO.FindControl("txtParticulars2")).Text;
                        drCORow["BILL_NO"] = ((TextBox)gvrCO.FindControl("txtBillNo")).Text;
                        drCORow["BILL_DATE"] = ((TextBox)gvrCO.FindControl("txtBillDate")).Text;
                        drCORow["AMOUNT"] = ((TextBox)gvrCO.FindControl("txtAmount")).Text;
                        //drCORow["AMOUNT_ACCEPTED"] = ((TextBox)gvrCO.FindControl("txtAcceptedAmount")).Text;
                        //drCORow["AMOUNT_REJECTED"] = ((TextBox)gvrCO.FindControl("txtRejectedAmount")).Text;
                        //drCORow["REMARKS"] = ((TextBox)gvrCO.FindControl("txtRemarks")).Text;
                        drCORow["ALLWDED_AID"] = ((Label)gvrCO.FindControl("lbldetId")).Text;
                        drCORow["DATE_SAVED"] = ((Label)gvrCO.FindControl("lblDateSaved")).Text;
                        drCORow["Doc_Data_Id"] = ((Label)gvrCO.FindControl("lblDocDataId")).Text;

                        dtCOInfo.Rows.Add(drCORow);

                        rowCount++;
                    }
                }

                ViewState["Bills"] = dtCOInfo;

                this.gvBills.DataSource = (DataTable)ViewState["Bills"];
                this.gvBills.DataBind();
            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();
                lblMessage.Text = "Error occurred in application.";
                objCommon.Display("Validate", "DisplayErrorMessage('" + objCommon.EncodeJsString("Error occurred in application.") + "');");
            }
        }

        protected void drpFlexiHeads_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                FillBills();

                if (drpFlexiHeads.SelectedValue == "AD000035") // FUEL ALLOWANCE
                {
                    reqDocs.Visible = true;
                    fuelDocs.Visible = true;
                    vehicleDocs.Visible = false;
                    telDocs.Visible = false;
                    driverDocs.Visible = false;
                    meetingDocs.Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000182") // VEHICLE MAINTAINANCE
                {
                    reqDocs.Visible = true;
                    fuelDocs.Visible = false;
                    vehicleDocs.Visible = true;
                    telDocs.Visible = false;
                    driverDocs.Visible = false;
                    meetingDocs.Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000207") // TELEPHONE ALLOWANCE
                {
                    reqDocs.Visible = true;
                    fuelDocs.Visible = false;
                    vehicleDocs.Visible = false;
                    telDocs.Visible = true;
                    driverDocs.Visible = false;
                    meetingDocs.Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000153") // DRIVER S ALLW
                {
                    reqDocs.Visible = true;
                    fuelDocs.Visible = false;
                    vehicleDocs.Visible = false;
                    telDocs.Visible = false;
                    driverDocs.Visible = true;
                    meetingDocs.Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000003") // MEETING ALLOW
                {
                    reqDocs.Visible = true;
                    fuelDocs.Visible = false;
                    vehicleDocs.Visible = false;
                    telDocs.Visible = false;
                    driverDocs.Visible = false;
                    meetingDocs.Visible = true;
                }
                else
                {
                    reqDocs.Visible = false;
                }
            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();
                lblMessage.Text = ex.Message;
                objCommon.Display("Validate", "DisplayErrorMessage('" + objCommon.EncodeJsString("Error occurred in application.") + "');");
            }
        }

        private void FillBills()
        {
            if (drpFlexiHeads.SelectedIndex > 0)
            {
                objFlexi = new NewPortal2023.ESS.FlexiPay();

                ViewState["Bills"] = null;

                gvBills.DataSource = null;
                gvBills.DataBind();

                txtEligibility.Text = "";

                DataSet ds = objFlexi.GetFlexiCompensationBills((string)Session["sCompID"], (string)Session["sEmpID"], drpFlexiHeads.SelectedValue);

                ViewState["Bills"] = ds.Tables[0];

                gvBills.DataSource = ds.Tables[0];
                gvBills.DataBind();

                txtEligibility.Text = ds.Tables[1].Rows[0][0].ToString();

                if ((bool)ViewState["UploadRights"] == false)
                {
                    rowButtons.Visible = false;
                    btnSubmit.Visible = false;
                }
                else
                {
                    rowButtons.Visible = true;
                    btnSubmit.Visible = false;
                }
            }
            else
            {
                ViewState["Bills"] = null;

                gvBills.DataSource = null;
                gvBills.DataBind();

                txtEligibility.Text = "";

                rowButtons.Visible = false;

                btnSubmit.Visible = false;
            }
        }

        protected void lnkBtnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFile = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFile.NamingContainer.FindControl("lblTSFileStorageName");

                string openFilePath = lblTSFileStorageName.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
                                                                //openFilePath = openFilePath + ViewState["FILE_PATH"].ToString();

                //string fileName = lblTSFileStorageName.Text;

                System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + lnkBtnOpenFile.Text);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                    getQuarter();
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
                getQuarter();
            }
        }

        private void CreateDocumentsStructure()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvViewDocDetails.DataSource = dtDocuments;
                gvViewDocDetails.DataBind();
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (fupldDocument.PostedFile.FileName == "")
                {
                    lblMessage.Text = "Browse document";
                    getQuarter();
                    return;
                }
                if ((string)Session["sCompID"] != "CO000114")
                {
                    if (drpQuarterType.SelectedItem.Text == "Select Quarter")
                    {
                        lblMessage.Text = "First Select Quarter Type Then Click Submit Button";
                        objCommon.Display("Validate", "DisplayErrorMessage('First Select Quarter Type Then Click Upload Option.');");
                        getQuarter();
                        return;
                    }
                }
                //if (fupldDocument.PostedFile.ContentLength > 10485760)
                //{
                //    lblMessage.Text = "File size should be less than 5MB.";
                //    return;
                //}

                if (System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".PDF" ||
                    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".JPG" ||
                    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".JPEG" ||
                    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".ZIP"
                    )
                {

                }
                else
                {
                    lblMessage.Text = "Only PDF,JPG/JPEG,ZIP and RAR files allowed.";
                    return;
                }

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) +
                            Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Reimbursement\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                System.IO.Stream fileInputStream = fupldDocument.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                fileInputStream.Close();
                /* Create Folder */

                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = Path.GetFileNameWithoutExtension(fupldDocument.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocument.FileName.Trim());

                //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                //{
                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();
                if ((string)Session["sCompID"] == "CO000114")
                {
                    objFlexi.Upload_Bills((string)Session["sCompID"], (string)Session["sEmpID"], fileName, "2023-2024", "");
                }
                else
                {
                    objFlexi.Upload_Bills((string)Session["sCompID"], (string)Session["sEmpID"], fileName, "2023-2024", drpQuarterType.SelectedValue);
                }
                DisplayDocuments();
                getQuarter();
                lblMessage.Text = "";
                //}
                //else
                //{
                //    lblMessage.Text = "Please delete previous files before uploading new file.";
                //}
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;  //"Error occurred in application.";
                getQuarter();
            }
        }

        private void DisplayDocuments()
        {
            try
            {
                CreateDocumentsStructure();

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Reimbursement\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                if (System.IO.Directory.Exists(@savePath) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }

                    this.gvViewDocDetails.DataSource = dtDocInfo;
                    this.gvViewDocDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void lnkBtnDeleteFile_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnDeleteFile = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnDeleteFile.NamingContainer.FindControl("lblTSFileStorageName");

                string openFilePath = lblTSFileStorageName.Text;

                System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                if (fileObj.Exists)
                {
                    fileObj.Delete();
                    DisplayDocuments();
                    objFlexi.Upload_Bills_Delete((string)Session["sCompID"], (string)Session["sEmpID"], fileObj.Name, "2019-2020");
                    getQuarter();
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
                getQuarter();
            }
        }

        protected void gvViewDocDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                LinkButton lb = e.Row.FindControl("lnkBtnOpenFile") as LinkButton;
                if (lb != null)
                    ScriptManager.GetCurrent(this).RegisterPostBackControl(lb);
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void gvBills_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                if (drpFlexiHeads.SelectedValue == "AD000035") // FUEL ALLOWANCE
                {
                    e.Row.Cells[1].Text = "Fuel Station";
                    e.Row.Cells[2].Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000159") // LEAVE TRAVEL ALLW
                {
                    e.Row.Cells[1].Text = "From";
                    e.Row.Cells[2].Visible = true;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000182") // VEHICLE MAINTAINANCE
                {
                    e.Row.Cells[1].Text = "Service Provider";
                    e.Row.Cells[2].Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000207") // TELEPHONE ALLOWANCE
                {
                    e.Row.Cells[1].Text = "Service Provider";
                    e.Row.Cells[2].Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000153") // DRIVER S ALLW
                {
                    e.Row.Cells[1].Text = "Driver Name";
                    e.Row.Cells[2].Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000003") // MEETING ALLOW
                {
                    e.Row.Cells[1].Text = "Hotel Name";
                    e.Row.Cells[2].Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000140") // BOOKS AND PERIODICALS
                {
                    e.Row.Cells[1].Text = "Vendor Name";
                    e.Row.Cells[2].Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000067") // PROFESSIONAL ATTIRE
                {
                    e.Row.Cells[1].Text = "Vendor Name";
                    e.Row.Cells[2].Visible = false;
                }
            }

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (drpFlexiHeads.SelectedValue == "AD000035") // FUEL ALLOWANCE
                {
                    e.Row.Cells[2].Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000159") // LEAVE TRAVEL ALLW
                {
                    e.Row.Cells[2].Visible = true;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000182") // VEHICLE MAINTAINANCE
                {
                    e.Row.Cells[2].Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000207") // TELEPHONE ALLOWANCE
                {
                    e.Row.Cells[2].Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000153") // DRIVER S ALLW
                {
                    e.Row.Cells[2].Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000003") // MEETING ALLOW
                {
                    e.Row.Cells[2].Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000140") // BOOKS AND PERIODICALS
                {
                    e.Row.Cells[2].Visible = false;
                }
                else if (drpFlexiHeads.SelectedValue == "AD000067") // PROFESSIONAL ATTIRE
                {
                    e.Row.Cells[2].Visible = false;
                }
            }
        }

        private void CreateDocumentsStructurePrev()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["DocumentsPrev"] = dtDocuments;
                gvPrevFiles.DataSource = dtDocuments;
                gvPrevFiles.DataBind();
            }
        }

        private void DisplayDocumentsPrev()
        {
            CreateDocumentsStructurePrev();

            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Reimbursement\\Archive\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

            string savePath2 = Request.PhysicalApplicationPath;
            savePath2 = savePath2 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Reimbursement\\Archive2\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

            string savePath3 = Request.PhysicalApplicationPath;
            savePath3 = savePath3 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Reimbursement\\Archive3\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

            string savePath4 = Request.PhysicalApplicationPath;
            savePath4 = savePath4 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Reimbursement\\Archive4\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

            string savePath5 = Request.PhysicalApplicationPath;
            savePath5 = savePath5 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Reimbursement\\Archive5\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

            DataTable dtDocInfo = ((DataTable)ViewState["DocumentsPrev"]);

            if (System.IO.Directory.Exists(@savePath) == true)
            {
                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                foreach (System.IO.FileInfo fi in fileNames)
                {
                    DataRow drDocRow = dtDocInfo.NewRow();
                    drDocRow["FILEPATH"] = fi.FullName;
                    drDocRow["FILENAME"] = fi.Name;
                    dtDocInfo.Rows.Add(drDocRow);
                }
            }

            if (System.IO.Directory.Exists(@savePath2) == true)
            {
                System.IO.DirectoryInfo dirInfo2 = new DirectoryInfo(savePath2);
                System.IO.FileInfo[] fileNames2 = dirInfo2.GetFiles("*.*");

                foreach (System.IO.FileInfo fi in fileNames2)
                {
                    DataRow drDocRow = dtDocInfo.NewRow();
                    drDocRow["FILEPATH"] = fi.FullName;
                    drDocRow["FILENAME"] = fi.Name;
                    dtDocInfo.Rows.Add(drDocRow);
                }
            }

            if (System.IO.Directory.Exists(@savePath3) == true)
            {
                System.IO.DirectoryInfo dirInfo3 = new DirectoryInfo(savePath3);
                System.IO.FileInfo[] fileNames3 = dirInfo3.GetFiles("*.*");

                foreach (System.IO.FileInfo fi in fileNames3)
                {
                    DataRow drDocRow = dtDocInfo.NewRow();
                    drDocRow["FILEPATH"] = fi.FullName;
                    drDocRow["FILENAME"] = fi.Name;
                    dtDocInfo.Rows.Add(drDocRow);
                }
            }

            if (System.IO.Directory.Exists(@savePath4) == true)
            {
                System.IO.DirectoryInfo dirInfo4 = new DirectoryInfo(savePath4);
                System.IO.FileInfo[] fileNames4 = dirInfo4.GetFiles("*.*");

                foreach (System.IO.FileInfo fi in fileNames4)
                {
                    DataRow drDocRow = dtDocInfo.NewRow();
                    drDocRow["FILEPATH"] = fi.FullName;
                    drDocRow["FILENAME"] = fi.Name;
                    dtDocInfo.Rows.Add(drDocRow);
                }
            }

            if (System.IO.Directory.Exists(@savePath5) == true)
            {
                System.IO.DirectoryInfo dirInfo5 = new DirectoryInfo(savePath5);
                System.IO.FileInfo[] fileNames5 = dirInfo5.GetFiles("*.*");

                foreach (System.IO.FileInfo fi in fileNames5)
                {
                    DataRow drDocRow = dtDocInfo.NewRow();
                    drDocRow["FILEPATH"] = fi.FullName;
                    drDocRow["FILENAME"] = fi.Name;
                    dtDocInfo.Rows.Add(drDocRow);
                }
            }

            this.gvPrevFiles.DataSource = dtDocInfo;
            this.gvPrevFiles.DataBind();
        }

        protected void lnkBtnOpenFilePrev_Click(object sender, EventArgs e)
        {
            LinkButton lnkBtnOpenFile = (LinkButton)sender;
            Label lblTSFileStorageName = (Label)lnkBtnOpenFile.NamingContainer.FindControl("lblTSFileStorageName");

            string openFilePath = lblTSFileStorageName.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
                                                            //openFilePath = openFilePath + ViewState["FILE_PATH"].ToString();

            //string fileName = lblTSFileStorageName.Text;

            System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
            if (fileObj.Exists)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AppendHeader("content-disposition", @"attachment; filename=" + lnkBtnOpenFile.Text);
                Response.ContentType = "text/csv";
                Response.WriteFile(fileObj.FullName);
                Response.End();
            }
        }

        protected void gvPrevFiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lb = e.Row.FindControl("lnkBtnOpenFile") as LinkButton;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lb);
            }
        }
        protected void lnkXLTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                smInv.RegisterPostBackControl((LinkButton)sender);

                if ((string)Session["sCompID"] == "CO000114")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/angel/reimbursement.xlsx"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=reimbursement.xlsx");
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
        protected void btnUploadXL_Click(object sender, EventArgs e)
        {
            try
            {
                if (fupInvXL.PostedFile != null)
                {
                    if (Convert.ToString(fupInvXL.PostedFile.FileName) == "")
                    {
                        lblMessage.Text = "Browse file to upload.";
                        return;
                    }
                    else
                    {
                        //UPLOAD FILE ON SERVER
                        if (System.IO.Path.GetExtension(fupInvXL.PostedFile.FileName).ToUpper() == ".XLSX")
                        {
                            string error = "";
                            if (UploadFile(out error))
                            {
                                FillCTC();
                                lblMessage.Text = "Supports Form uploaded successfully.";
                            }
                            else
                            {
                                lblMessage.Text = error;
                            }
                        }
                        else
                        {
                            lblMessage.Text = "File invalid.";
                            return;
                        }
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
                lblMessage.Text = ex.Message;
            }
        }

        private bool UploadFile(out string error)
        {
            //string guid;
            string path;

            //guid = Convert.ToString(Guid.NewGuid());
            //guid = guid + fupInvXL.FileName;
            path = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + (string)Session["sCompID"] + "\\ANGEL\\Documents\\Reimbursement\\Declaration\\";
            path = path + (string)Session["sEmpCode"] + "_FlexiComp.xlsx";

            System.IO.Stream fileInputStream = fupInvXL.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));

            System.IO.FileStream fStream = new System.IO.FileStream(path, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write((Byte[])fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();

            return objFlexi.UploadFlexiCompensationXL((string)Session["sCompID"], path, (string)Session["sEmpID"], out error);
        }

        protected void lnkXLUploaded_Click(object sender, EventArgs e)
        {
            try
            {
                smInv.RegisterPostBackControl((LinkButton)sender);

                string SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Reimbursement\\Declaration";

                System.IO.FileInfo fileObj = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + "_FlexiComp.xlsx");
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + Convert.ToString(Session["sEmpCode"]) + "_FlexiComp.xlsx");
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
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

        protected void lnkFAQ_Click(object sender, EventArgs e)
        {
            try
            {
                smInv.RegisterPostBackControl((LinkButton)sender);

                if ((string)Session["sCompID"] == "CO000114")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/angel/FAQ_Reimbursement.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=FAQ_Reimbursement.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000125")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/arcil/FAQ_FlexiBenefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=FAQ_FlexiBenefits.pdf");
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

        private void getQuarter()
        {
            dsInv = objFlexi.getQuarters((string)Session["sCompID"], (string)Session["sEmpID"]);
            if (dsInv.Tables.Count > 0)
            {
                if (1 == Convert.ToInt16(dsInv.Tables[0].Rows[0]["Quarters"]))
                {
                    //Set the default item as selected.


                    //Disable the default item.
                    drpQuarterType.Items[0].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[4].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[2].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[3].Attributes["disabled"] = "disabled";
                    ViewState["QTR"] = "First Quarter";

                }
                else if (2 == Convert.ToInt16(dsInv.Tables[0].Rows[0]["Quarters"]))
                {
                    //Set the default item as selected.


                    //Disable the default item.
                    drpQuarterType.Items[0].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[1].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[4].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[3].Attributes["disabled"] = "disabled";
                    ViewState["QTR"] = "Second Quarter";

                }
                else if (3 == Convert.ToInt16(dsInv.Tables[0].Rows[0]["Quarters"]))
                {


                    //Disable the default item.
                    drpQuarterType.Items[0].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[1].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[4].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[2].Attributes["disabled"] = "disabled";
                    ViewState["QTR"] = "Thrid Quarter";

                }
                else if (4 == Convert.ToInt16(dsInv.Tables[0].Rows[0]["Quarters"]))
                {


                    //Disable the default item.
                    drpQuarterType.Items[0].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[1].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[2].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[3].Attributes["disabled"] = "disabled";
                    ViewState["QTR"] = "Fourth Quarter";
                }
            }
            else
            {
                lblMessage.Text = "Not Found";
            }
        }

        protected void drpQuarterType_SelectedIndexChanged(object sender, EventArgs e)
        {

            dsInv = objFlexi.getQuarters((string)Session["sCompID"], (string)Session["sEmpID"]);
            if (dsInv.Tables.Count > 0)
            {
                if (1 == Convert.ToInt16(dsInv.Tables[0].Rows[0]["Quarters"]))
                {
                    //Set the default item as selected.


                    //Disable the default item.
                    drpQuarterType.Items[0].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[4].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[2].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[3].Attributes["disabled"] = "disabled";
                    ViewState["QTR"] = "First Quarter";

                }
                else if (2 == Convert.ToInt16(dsInv.Tables[0].Rows[0]["Quarters"]))
                {
                    //Set the default item as selected.


                    //Disable the default item.
                    drpQuarterType.Items[0].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[1].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[4].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[3].Attributes["disabled"] = "disabled";
                    ViewState["QTR"] = "Second Quarter";

                }
                else if (3 == Convert.ToInt16(dsInv.Tables[0].Rows[0]["Quarters"]))
                {


                    //Disable the default item.
                    drpQuarterType.Items[0].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[1].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[4].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[2].Attributes["disabled"] = "disabled";
                    ViewState["QTR"] = "Thrid Quarter";

                }
                else if (4 == Convert.ToInt16(dsInv.Tables[0].Rows[0]["Quarters"]))
                {


                    //Disable the default item.
                    drpQuarterType.Items[0].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[1].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[2].Attributes["disabled"] = "disabled";
                    drpQuarterType.Items[3].Attributes["disabled"] = "disabled";
                    ViewState["QTR"] = "Fourth Quarter";
                }
            }
            else
            {
                lblMessage.Text = "Not Found";
            }
        }



        protected void drpPreviousQtr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objFlexi.GetFlexiCompensationPreQtr((string)Session["sCompID"], (string)Session["sEmpID"], drpPreviousQtr.SelectedValue);
                gvPrevious.DataSource = ds.Tables[0];
                gvPrevious.DataBind();
                getQuarter();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void btnAllSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (drpQuarterType.SelectedValue != "0")
                {
                    if (ViewState["LTAAID"] == null)
                    {
                        if (fupldDocument.PostedFile.FileName == "")
                        {

                            lblMessage.Text = "Browse document";
                            objCommon.Display("Validate", "DisplayErrorMessage('First Browse document Type Then Click Submit Button.');");
                            getQuarter();
                            return;
                        }
                        if ((string)Session["sCompID"] != "CO000114")
                        {
                            if (drpQuarterType.SelectedItem.Text == "Select Quarter")
                            {
                                lblMessage.Text = "First Select Quarter Type Then Click Submit Button.";
                                objCommon.Display("Validate", "DisplayErrorMessage('First Select Quarter Type Then Click Submit Button.');");
                                getQuarter();
                                return;
                            }
                        }
                        //if (fupldDocument.PostedFile.ContentLength > 10485760)
                        //{
                        //    lblMessage.Text = "File size should be less than 5MB.";
                        //    return;
                        //}

                        if (System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".PDF" ||
                            System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".JPG" ||
                            System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".JPEG" ||
                            System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".ZIP"
                            )
                        {

                        }
                        else
                        {
                            lblMessage.Text = "Only PDF,JPG/JPEG,ZIP and RAR files allowed.";
                            getQuarter();
                            return;
                        }

                        savePath = Request.PhysicalApplicationPath;
                        savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) +
                                    Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Reimbursement\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                        System.IO.Stream fileInputStream = fupldDocument.PostedFile.InputStream;
                        Byte[] fileContent = new Byte[fileInputStream.Length];
                        fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                        fileInputStream.Close();
                        /* Create Folder */

                        if (System.IO.Directory.Exists(@savePath) == false)
                        {
                            System.IO.Directory.CreateDirectory(@savePath);
                        }

                        DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                        string fileName = Path.GetFileNameWithoutExtension(fupldDocument.FileName.Trim().Replace(" ", "_")) + "_"
                            + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocument.FileName.Trim());

                        //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                        //{
                        System.IO.FileStream fStream = new System.IO.FileStream(@savePath + fileName, System.IO.FileMode.Create);
                        System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                        bWriter.Write(fileContent);
                        fStream.Flush();
                        bWriter.Flush();
                        fStream.Close();
                        bWriter.Close();
                        if ((string)Session["sCompID"] == "CO000114")
                        {
                            objFlexi.Upload_Bills((string)Session["sCompID"], (string)Session["sEmpID"], fileName, "2023-2024", "");
                        }
                        else
                        {
                            objFlexi.Upload_Bills((string)Session["sCompID"], (string)Session["sEmpID"], fileName, "2023-2024", drpQuarterType.SelectedValue);
                            lblMessage.Text = "Submitted Successfully";
                            objCommon.Display("Validate", "DisplayErrorMessage('Submitted Successfully.');");
                        }
                        DisplayDocuments();
                        getQuarter();
                    }
                    else
                    {
                        DataSet dsLTAAmt = new DataSet();
                        dsLTAAmt = objFlexi.GetLATAMT((string)Session["sCompID"], (string)Session["sEmpID"], drpPreviousQtr.SelectedValue);


                        if (Convert.ToInt16(dsLTAAmt.Tables[0].Rows[0]["AMOUNT"]) > 0)
                        {
                            if (FileUpload1.PostedFile.FileName == "")
                            {
                                lblMessage.Text = "Browse and Uplaod LTA document";
                                objCommon.Display("Validate", "DisplayErrorMessage('First Browse and Uplaod LTA document Then Click Submit Button.');");
                                getQuarter();
                                return;
                            }
                            else
                            {
                                UploadLTA();
                            }

                        }
                        if (fupldDocument.PostedFile.FileName == "")
                        {

                            lblMessage.Text = "Browse document";
                            objCommon.Display("Validate", "DisplayErrorMessage('First Browse document Type Then Click Submit Button.');");
                            getQuarter();
                            return;
                        }

                        //if (FileUpload1.PostedFile.FileName == "")
                        //{
                        //    lblMessage.Text = "Browse and Uplaod LTA document";
                        //    objCommon.Display("Validate", "DisplayErrorMessage('First Browse and Uplaod LTA document Then Click Submit Button.');");
                        //    getQuarter();
                        //    return;
                        //}
                        //UploadLTA();


                        if ((string)Session["sCompID"] != "CO000114")
                        {
                            if (drpQuarterType.SelectedItem.Text == "Select Quarter")
                            {
                                lblMessage.Text = "First Select Quarter Type Then Click Submit Button.";
                                objCommon.Display("Validate", "DisplayErrorMessage('First Select Quarter Type Then Click Submit Button.');");
                                getQuarter();
                                return;
                            }
                        }
                        //if (fupldDocument.PostedFile.ContentLength > 10485760)
                        //{
                        //    lblMessage.Text = "File size should be less than 5MB.";
                        //    return;
                        //}

                        if (System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".PDF" ||
                            System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".JPG" ||
                            System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".JPEG" ||
                            System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".ZIP"
                            )
                        {

                        }
                        else
                        {
                            lblMessage.Text = "Only PDF,JPG/JPEG,ZIP and RAR files allowed.";
                            getQuarter();
                            return;
                        }

                        savePath = Request.PhysicalApplicationPath;
                        savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) +
                                    Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Reimbursement\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                        System.IO.Stream fileInputStream = fupldDocument.PostedFile.InputStream;
                        Byte[] fileContent = new Byte[fileInputStream.Length];
                        fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                        fileInputStream.Close();
                        /* Create Folder */

                        if (System.IO.Directory.Exists(@savePath) == false)
                        {
                            System.IO.Directory.CreateDirectory(@savePath);
                        }

                        DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                        string fileName = Path.GetFileNameWithoutExtension(fupldDocument.FileName.Trim().Replace(" ", "_")) + "_"
                            + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocument.FileName.Trim());

                        //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                        //{
                        System.IO.FileStream fStream = new System.IO.FileStream(@savePath + fileName, System.IO.FileMode.Create);
                        System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                        bWriter.Write(fileContent);
                        fStream.Flush();
                        bWriter.Flush();
                        fStream.Close();
                        bWriter.Close();
                        if ((string)Session["sCompID"] == "CO000114")
                        {
                            objFlexi.Upload_Bills((string)Session["sCompID"], (string)Session["sEmpID"], fileName, "2023-2024", "");
                        }
                        else
                        {
                            objFlexi.Upload_Bills((string)Session["sCompID"], (string)Session["sEmpID"], fileName, "2023-2024", drpQuarterType.SelectedValue);
                            lblMessage.Text = "Submitted Successfully";
                            objCommon.Display("Validate", "DisplayErrorMessage('Submitted Successfully.');");
                        }
                        DisplayDocuments();
                        getQuarter();
                    }
                }
                else
                {
                    lblMessage.Text = "Please Select First Quarter Then Click Submit Button.";
                    getQuarter();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;  //"Error occurred in application.";
                getQuarter();
            }
        }

        private void UploadLTA()
        {
            try
            {
                if (FileUpload1.PostedFile.FileName == "")
                {
                    lblMessage.Text = "Browse document";
                    return;
                }

                if (System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToUpper() == ".PDF")
                {

                }
                else
                {
                    lblMessage.Text = "Only PDF files allowed.";
                    return;
                }

                savePath = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + (string)Session["sCompAID"] + "\\Documents\\Reimbursement\\Declaration\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                //savePath = savePath + (string)Session["sEmpCode"];

                System.IO.Stream fileInputStream = FileUpload1.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));


                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = "LTC_" + Path.GetFileNameWithoutExtension(fupldDocument.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(FileUpload1.FileName.Trim());

                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();

                DisplayDocumentsLTA();
                lblMessage.Text = "";

                lblMessage.Text = "LTC Form uploaded successfully.";
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnlnkDwnl_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet ds = new DataSet();
                // objFlexi.Upload_Bills((string)Session["sCompID"], (string)Session["sEmpID"], fileName, "2023-2024", "");

                ds = objFlexi.Fill_ExcelReport((string)Session["sCompID"], (string)Session["sEmpID"]);
                string extension;
                string encoding;
                string contentType;
                string[] streamIds;
                Warning[] warnings;

                ReportDataSource dataSource = new ReportDataSource("dsFlexiSupportDetails", ds.Tables[0]);

                rptPrint.LocalReport.DataSources.Clear();

                rptPrint.LocalReport.ReportPath = @"ESS\FlexiSupportsReport.rdlc";
                rptPrint.LocalReport.DataSources.Add(dataSource);

                rptPrint.LocalReport.Refresh();



                byte[] bytes = rptPrint.LocalReport.Render("Excel", null, out contentType, out encoding, out extension, out streamIds, out warnings);

                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = ContentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=Flexi Support Report.xls");
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void gvPrevious_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                foreach (GridViewRow Row in gvPrevious.Rows)
                {
                    if (Row.RowType == DataControlRowType.DataRow)
                    {
                        Label lblAid = (Label)Row.FindControl("lblAid");
                        Label lblLimit = (Label)Row.FindControl("lblLimit");
                        ds = objFlexi.GetAllwYearlyAmt((string)Session["sCompID"], (string)Session["sEmpID"], lblAid.Text);
                        lblLimit.Text = ds.Tables[0].Rows[0]["AMT"].ToString(); ;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }



        protected void lnkLTCForm_Click(object sender, EventArgs e)
        {
            try
            {
                smInv.RegisterPostBackControl((LinkButton)sender);

                if ((string)Session["sCompID"] == "CO000045")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/sicom/LTA_Application_Form.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=LTA_Application_Form.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000125")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/arcil/LTA_Application_Form.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=LTA_Application_Form.pdf");
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

        protected void btnUploadLTCForm_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.PostedFile.FileName == "")
                {
                    lblMessage.Text = "Browse document";
                    return;
                }

                if (System.IO.Path.GetExtension(FileUpload1.PostedFile.FileName).ToUpper() == ".PDF")
                {

                }
                else
                {
                    lblMessage.Text = "Only PDF files allowed.";
                    return;
                }

                savePath = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + (string)Session["sCompAID"] + "\\Documents\\Reimbursement\\Declaration\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                //savePath = savePath + (string)Session["sEmpCode"];

                System.IO.Stream fileInputStream = FileUpload1.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));


                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = "LTC_" + Path.GetFileNameWithoutExtension(fupldDocument.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(FileUpload1.FileName.Trim());

                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();

                DisplayDocumentsLTA();
                lblMessage.Text = "";

                lblMessage.Text = "LTC Form uploaded successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;  //"Error occurred in application.";
            }
        }

        private void CreateDocumentsLTAStructure()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvViewDocDetails.DataSource = dtDocuments;
                gvViewDocDetails.DataBind();
            }
        }

        private void DisplayDocumentsLTA()
        {
            try
            {
                CreateDocumentsLTAStructure();

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Reimbursement\\Declaration\\" + Convert.ToString(Session["sEmpCode"]).ToUpper();

                if (System.IO.Directory.Exists(@savePath) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles("LTC_*.*");

                    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }

                    this.gvViewLTADocDetails.DataSource = dtDocInfo;
                    this.gvViewLTADocDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void lnkBtnOpenFileLTA_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFile = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFile.NamingContainer.FindControl("lblLTATSFileStorageName");

                string openFilePath = lblTSFileStorageName.Text;

                System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + lnkBtnOpenFile.Text);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void lnkBtnDeleteFileLTA_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnDeleteFile = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnDeleteFile.NamingContainer.FindControl("lblLTATSFileStorageName");

                string openFilePath = lblTSFileStorageName.Text;

                System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                if (fileObj.Exists)
                {
                    fileObj.Delete();
                    DisplayDocuments();
                    objInv.Upload_Support_Delete((string)Session["sCompID"], (string)Session["sEmpID"], fileObj.Name, "2021-2022");
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }
    }
}