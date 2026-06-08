using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using System.Data.SqlClient;

namespace NewPortal2023.ESS
{
    public partial class Flexipay : System.Web.UI.Page
    {
        NewPortal2023.ESS.FlexiPay objInv = new NewPortal2023.ESS.FlexiPay();
        NewPortal2023.ESS.Support objSup = new NewPortal2023.ESS.Support();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataTable dtInv = new DataTable();
        DataSet dsInv = new DataSet();
        DataSet dtNewInv = new DataSet();
        double totAmt = 0;
        double totamountflexi = 0;
        private string savePath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["sCompID"] != null)
                    {
                        try
                        {
                            string strResult = objcommon.Validate_ControlInfo("FLEX");

                            if (strResult.Contains("This page is currently unavailable.") == true)
                            {
                                Response.Redirect("Unavailable.aspx?strName=Flexi Pay Details");
                                return;
                            }

                            FillCTC();
                        }

                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                            //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading flexi pay details.');");
                        }
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
            totAmt = 0;
            bool rent = false;
            dtNewInv = new DataSet();

            string all_aid = "";
            //dtInv = objInv.GetCTCDetails((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"], txtCTC, txtBal, txtAnnCTC, drpEffectiveDate, drpCarLease);
            if ((string)Session["sCompID"].ToString() == "CO000125")
            {
                dtNewInv = objInv.GetNewCTCDetails((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"], all_aid, txtCTC, txtBal, txtAnnCTC, drpCarLease, out rent);

                gvNewCTC.DataSource = dtNewInv;
                gvNewCTC.DataBind();

                OldCTC.Visible = false;
                NewCTC.Visible = true;
                trOldtol.Visible = false;
                btnFlexiPrint.Visible = false;
                trNewtol.Visible = true;
                txtTotalAmount.Text = txtCTC.Text;
                trAgree1.Visible = false;

            }
            else
            {
                dtInv = objInv.GetCTCDetails((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"], txtCTC, txtBal, txtAnnCTC, drpCarLease, out rent);
                ViewState["ISRENT"] = rent;
                gvCTC.DataSource = dtInv;
                gvCTC.DataBind();
                OldCTC.Visible = true;
                NewCTC.Visible = false;
                trOldtol.Visible = true;
                trNewtol.Visible = false;
                //txtAnnCTC.Text =Convert.ToString(totAmt); 
                txtAnnCTC.Text = txtAnnCTC.Text.Replace(".00", "");
                ViewState["BALAMT"] = txtBal.Text;
                txtMonCTC.Text = Convert.ToString(Math.Round(Convert.ToDouble((txtAnnCTC.Text.Trim() == "" ? "0" : txtAnnCTC.Text)) / 12, 0));
                CalculateGross();
                trAgree1.Visible = true;
                //ResetSpecialAllowance();
                //if ((string)Session["sGrade"] == "L1" || (string)Session["sGrade"] == "L2")
                //{
                //    drpCarLease.SelectedValue = "No";
                //    drpCarLease.Enabled = false;
                //}

                ViewState["CarLease"] = drpCarLease.SelectedIndex;
                SetCarDetailsView();
            }
            //FillSupportDetails();
            //DisplayDocumentsPrev();
        }

        private void FillSupportDetails()
        {
            objInv.GetSupportDetails((string)Session["sCompID"], (string)Session["sEmpID"], txtLTANo, txtLTAAmt, txtTelAmt, txtFuelAmt, txtDriverAmt,
                                                                                            lblLTAAmtVer, lblTelAmtVer, lblFuelAmtVer, lblDriverAmtVer,
                                                                                            lblLTAAmtRej, lblTelAmtRej, lblFuelAmtRej, lblDriverAmtRej,
                                                                                            lblLTARemarks, lblTelRemarks, lblFuelRemarks, lblDriverRemarks);

            string SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
            string filesToFind = "LTA_*";
            if (Directory.Exists(SourcePath))
            {
                string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
                foreach (string file in fileList)
                {
                    lnkDownloadLTA.Visible = true;
                }
            }

            filesToFind = "TEL_*";
            if (Directory.Exists(SourcePath))
            {
                string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
                foreach (string file in fileList)
                {
                    lnkDownloadTel.Visible = true;
                }
            }

            filesToFind = "FUEL_*";
            if (Directory.Exists(SourcePath))
            {
                string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
                foreach (string file in fileList)
                {
                    lnkDownloadFuel.Visible = true;
                }
            }

            filesToFind = "DRV_*";
            if (Directory.Exists(SourcePath))
            {
                string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
                foreach (string file in fileList)
                {
                    lnkDownloadDriver.Visible = true;
                }
            }
        }

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if ((string)Session["sCompID"].ToString() != "CO000125")
                {
                    if (chkAgree.Checked == false)
                    {
                        lblMessage.Text = "You must agree to the disclaimer before submitting.";
                        objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
                        return;
                    }
                }
                if ((string)Session["sCompID"].ToString() != "CO000125")
                {
                    if (ValidateAlternate() == true)
                    {
                        if (objInv.UpdateFlexipay(MakeCTCXml(gvCTC, ""), drpCarLease.SelectedItem.Text == "Yes" ? "1" : "0", (string)Session["sCompID"], (string)Session["sEmpID"]) == false)
                        {
                            lblMessage.Text = "Error occurred in application.";
                            //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating Flexi Pay details.');");
                            return;
                        }
                        FillCTC();
                        lblMessage.Text = "Successfully updated flexi pay details.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Successfully updated flexi pay details.');");

                        Session["AnnCTC"] = txtAnnCTC.Text;
                        Session["MonCTC"] = txtMonCTC.Text;
                        Session["Gross"] = txtGross.Text;
                        Response.Redirect("FlexiPrint.aspx");
                    }
                }
                else
                {
                    if (objInv.UpdateFlexipay(MakeCTCXml(gvNewCTC, ""), drpCarLease.SelectedItem.Text == "Yes" ? "1" : "0", (string)Session["sCompID"], (string)Session["sEmpID"]) == false)
                    {
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating Flexi Pay details.');");
                        return;
                    }
                    FillCTC();
                    lblMessage.Text = "Successfully updated flexi pay details.";
                }

                ////Update Effective Date
                //SqlConnection conn = new SqlConnection("Server=118.67.248.121;Database=sequelpay;uid=payrollservices;pwd=gokarna6193");            
                //SqlCommand cmd = conn.CreateCommand();
                //cmd.CommandText = "UPDATE dbo.GM_EMPMAST_WEB SET CONFIRMATION_DATE=@date WHERE COMP_AID=@comp_aid AND EMP_MID=@emp_mid";
                //conn.Open();
                //cmd.Parameters.AddWithValue("@date", Convert.ToDateTime(drpEffectiveDate.SelectedValue));
                //cmd.Parameters.AddWithValue("@comp_aid", (string)Session["sCompID"]);
                //cmd.Parameters.AddWithValue("@emp_mid", (string)Session["sEmpCode"]);
                //cmd.ExecuteNonQuery();
                //conn.Close();

                //Update Car Lease Status
                //SqlConnection conn = new SqlConnection("Server=118.67.248.121;Database=sequelpay;uid=payrollservices;pwd=gokarna6193");      
                //SqlCommand cmd1 = conn.CreateCommand();            
                //conn.Open();
                //string s = drpCarLease.SelectedItem.Text;
                //if (s == "Yes")
                //{
                //    cmd1.CommandText = "UPDATE dbo.PM_FLEXYPAY_DETAILS SET FlexAddr = '1' WHERE COMP_AID = @comp_aid AND EMP_AID = @emp_aid AND ALLWDED_AID = 'AD000001'";
                //    cmd1.Parameters.AddWithValue("@comp_aid", (string)Session["sCompID"]);
                //    cmd1.Parameters.AddWithValue("@emp_aid", (string)Session["sEmpID"]);
                //    cmd1.ExecuteNonQuery();
                //}
                //else
                //{
                //    cmd1.CommandText = "UPDATE dbo.PM_FLEXYPAY_DETAILS SET FlexAddr = '' WHERE COMP_AID = @comp_aid AND EMP_AID = @emp_aid AND ALLWDED_AID = 'AD000001'";
                //    cmd1.Parameters.AddWithValue("@comp_aid", (string)Session["sCompID"]);
                //    cmd1.Parameters.AddWithValue("@emp_aid", (string)Session["sEmpID"]);
                //    cmd1.ExecuteNonQuery();
                //}

                //conn.Close();           

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating Flexi Pay details.');");
            }

        }
        private string MakeCTCXml(GridView GV, string strType)
        {

            StringBuilder sbTaxDetails = new StringBuilder();
            string xmlInv = string.Empty;

            sbTaxDetails.Append("<ROOT>");
            if ((string)Session["sCompID"].ToString() != "CO000125")
            {
                foreach (GridViewRow gvr in GV.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true && ((CheckBox)gvr.FindControl("chkSelect")).Visible == true)
                    {
                        string cardType = "";
                        if (((Label)gvr.FindControl("lbldetId")).Text.Trim() == "AD000079")
                        {
                            cardType = (((DropDownList)gvr.FindControl("drpCardType")).SelectedItem.Text == "[Select]" ? "" : ((DropDownList)gvr.FindControl("drpCardType")).SelectedItem.Text);
                        }
                        else if (((Label)gvr.FindControl("lbldetId")).Text.Trim() == "AD000130")
                        {
                            cardType = (((DropDownList)gvr.FindControl("drpGiftCard")).SelectedItem.Text == "[Select]" ? "" : ((DropDownList)gvr.FindControl("drpGiftCard")).SelectedItem.Text);
                        }

                        sbTaxDetails.Append("<Flexi COMP_AID='" + (string)Session["sCompID"] + "'");
                        sbTaxDetails.Append(" EMP_AID='" + (string)Session["sEmpID"] + "'");
                        sbTaxDetails.Append(" ALLWDED_AID='" + ((Label)gvr.FindControl("lbldetId")).Text.Trim() + "'");
                        sbTaxDetails.Append(" Fix_Amount='" + ((Label)gvr.FindControl("lblLimit")).Text.Trim() + "'");
                        sbTaxDetails.Append(" Amount='" + ((TextBox)gvr.FindControl("txtAmount")).Text.Trim() + "'");
                        sbTaxDetails.Append(" FlexAddr='" + (((TextBox)gvr.FindControl("txtAddr")).Visible == true ? ((TextBox)gvr.FindControl("txtAddr")).Text.Trim() : "") + "'");
                        sbTaxDetails.Append(" FlexPin='" + (((TextBox)gvr.FindControl("txtPin")).Visible == true ? ((TextBox)gvr.FindControl("txtPin")).Text.Trim() : "") + "'");
                        sbTaxDetails.Append(" PCNT='" + (((TextBox)gvr.FindControl("txtpcnt")).Visible == true ? ((TextBox)gvr.FindControl("txtpcnt")).Text.Trim() : ((Label)gvr.FindControl("lblpct")).Text.Trim()) + "'");
                        sbTaxDetails.Append(" CARD='" + cardType + "'/>");
                    }
                }
            }
            else if ((string)Session["sCompID"].ToString() == "CO000125")
            {
                foreach (GridViewRow gvr in GV.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkNewSelect")).Checked == true && ((CheckBox)gvr.FindControl("chkNewSelect")).Visible == true)
                    {
                        string cardType = "";
                        //if (((Label)gvr.FindControl("lblNewdetId")).Text.Trim() == "AD000079")
                        //{
                        //    cardType = (((DropDownList)gvr.FindControl("drpCardType")).SelectedItem.Text == "[Select]" ? "" : ((DropDownList)gvr.FindControl("drpCardType")).SelectedItem.Text);
                        //}
                        //else if (((Label)gvr.FindControl("lbldetId")).Text.Trim() == "AD000130")
                        //{
                        //    cardType = (((DropDownList)gvr.FindControl("drpGiftCard")).SelectedItem.Text == "[Select]" ? "" : ((DropDownList)gvr.FindControl("drpGiftCard")).SelectedItem.Text);
                        //}

                        sbTaxDetails.Append("<Flexi COMP_AID='" + (string)Session["sCompID"] + "'");
                        sbTaxDetails.Append(" EMP_AID='" + (string)Session["sEmpID"] + "'");
                        sbTaxDetails.Append(" ALLWDED_AID='" + ((Label)gvr.FindControl("lblNewdetId")).Text.Trim() + "'");
                        sbTaxDetails.Append(" Fix_Amount='" + ((Label)gvr.FindControl("lblNewLimit")).Text.Trim() + "'");
                        sbTaxDetails.Append(" Amount='" + " " + "'");
                        sbTaxDetails.Append(" FlexAddr='" + " " + "'");
                        sbTaxDetails.Append(" FlexPin='" + " " + "'");
                        sbTaxDetails.Append(" PCNT='" + " " + "'");
                        sbTaxDetails.Append(" CARD='" + cardType + "'/>");
                    }
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

        protected void gvCTC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Label lblDesc = (Label)e.Row.FindControl("lblDesc");
                    Label lbldetId = (Label)e.Row.FindControl("lbldetId");
                    Label lblGroup = (Label)e.Row.FindControl("lblGroup");
                    Label lblSel = (Label)e.Row.FindControl("lblSel");
                    Label lblLimit = (Label)e.Row.FindControl("lblLimit");
                    Label lbledit = (Label)e.Row.FindControl("lbledit");

                    Label lblshow = (Label)e.Row.FindControl("lblshow");
                    Label lblman = (Label)e.Row.FindControl("lblman");
                    TextBox txtAddr = (TextBox)e.Row.FindControl("txtAddr");
                    TextBox txtPin = (TextBox)e.Row.FindControl("txtPin");

                    TextBox txtAmount = (TextBox)e.Row.FindControl("txtAmount");
                    TextBox txtpcnt = (TextBox)e.Row.FindControl("txtpcnt");
                    DropDownList drpCardType = (DropDownList)e.Row.FindControl("drpCardType");
                    DropDownList drpGiftCard = (DropDownList)e.Row.FindControl("drpGiftCard");
                    DropDownList drpIsCar = (DropDownList)e.Row.FindControl("drpIsCar");
                    DropDownList drpCC = (DropDownList)e.Row.FindControl("drpCC");
                    DropDownList drpStatutoryPF = (DropDownList)e.Row.FindControl("drpStatutoryPF");
                    Label lblCardType = (Label)e.Row.FindControl("lblCardType");

                    CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkSelect");
                    if (lbldetId.Text == "AD000001")
                    {
                        Session["BASICAMT"] = lblLimit.Text;
                    }
                    if (lbldetId.Text == "AD000011")
                    {
                        if (txtAmount.Text == "21600.00")
                        {
                            drpStatutoryPF.SelectedIndex = 1;
                        }
                        else
                        {
                            drpStatutoryPF.SelectedIndex = 0;
                        }
                        double amt = Convert.ToDouble(Session["BASICAMT"].ToString());
                        Session["STATUTORYPFAMT"] = lblLimit.Text;
                        if (amt >= Convert.ToDouble(18000))
                        {
                            drpStatutoryPF.Visible = true;
                            drpStatutoryPF.SelectedValue = lblCardType.Text == "" ? "[Select]" : lblCardType.Text;
                        }
                        else if (amt < Convert.ToDouble(18000))
                        {
                            drpStatutoryPF.Visible = false;
                        }

                    }
                    if (lbldetId.Text == "AD000177")
                    {
                        Session["SPECIALALLOW"] = lblLimit.Text;
                    }

                    txtpcnt.Visible = false;
                    chkSelect.Visible = (lbldetId.Text.Trim() == "00000000" ? false : true);
                    txtAmount.Visible = (lbldetId.Text.Trim() == "00000000" ? false : true);

                    txtAddr.Visible = (lblshow.Text.Trim() == "0" ? false : true);
                    txtPin.Visible = (lblshow.Text.Trim() == "0" ? false : true);

                    //if (lblGroup.Text.Trim() != "C" )
                    //{
                    //    txtAmount.ReadOnly = false;
                    //}
                    //else
                    //{
                    txtAmount.ReadOnly = true;
                    txtpcnt.ReadOnly = true;
                    txtAddr.ReadOnly = true;
                    txtPin.ReadOnly = true;
                    //}

                    lblDesc.Font.Bold = (lbldetId.Text.Trim() == "00000000" || lblGroup.Text.Trim() == "B" ? true : false);
                    chkSelect.Enabled = (lblGroup.Text.Trim() == "C" || lblGroup.Text.Trim() == "B" || lblGroup.Text.Trim() == "OG" ? false : true);
                    chkSelect.Checked = (lblGroup.Text.Trim() == "C" || lblGroup.Text.Trim() == "B" || lblGroup.Text.Trim() == "OG" ? true : false);

                    if (lbldetId.Text == "AD000011")
                    {
                        lblDesc.Font.Bold = true;
                    }
                    //lblLimit.Text = (Convert.ToDouble(txtAmount.Text) == 0 ? "" : txtAmount.Text);   
                    if (lblSel.Text.Trim() == "1")
                    {
                        chkSelect.Checked = true;
                    }

                    if (chkSelect.Checked == true && lblGroup.Text.Trim() != "C" && lblGroup.Text.Trim() != "OG")
                    {
                        txtAmount.ReadOnly = false;
                        txtpcnt.ReadOnly = false;
                        txtAddr.ReadOnly = false;
                        txtPin.ReadOnly = false;
                    }

                    //if (lblGroup.Text.Trim() != "C" && lblGroup.Text.Trim() != "B" &&  lblGroup.Text.Trim() != "OG")
                    if (lblGroup.Text.Trim() != "B" && lblGroup.Text.Trim() != "OG")
                    {
                        if (chkSelect.Checked == false)
                        {
                            txtAmount.Text = "0";
                            txtpcnt.Text = "0";
                        }

                        if (lbledit.Text.Trim() == "1")
                        {
                            txtpcnt.Visible = true;
                            txtpcnt.Text = (chkSelect.Checked == true ? txtpcnt.Text : "0");


                            if (lbldetId.Text == "AD000002" && (bool)ViewState["ISRENT"] == true)
                            {
                                txtpcnt.ReadOnly = false;
                            }
                            else
                            {
                                txtpcnt.ReadOnly = true;
                            }
                        }
                        else
                        {
                            txtpcnt.Visible = false;
                        }
                    }

                    if (txtAmount.Text.ToString() != "" && lbldetId.Text.Trim() != "AD000012" && lbldetId.Text.Trim() != "AD000130")
                    {
                        totAmt = totAmt + Convert.ToDouble(txtAmount.Text);
                    }
                    else
                    {
                        totAmt = totAmt + Convert.ToDouble(0);
                    }

                    if (chkSelect.Checked)
                    {
                        if (lbldetId.Text == "AD000079")
                        {
                            drpCardType.Visible = true;
                            drpCardType.SelectedValue = lblCardType.Text == "" ? "[Select]" : lblCardType.Text;
                        }
                        if (lbldetId.Text == "AD000130")
                        {
                            drpGiftCard.Visible = true;
                            drpGiftCard.SelectedValue = lblCardType.Text == "" ? "[Select]" : lblCardType.Text;
                        }
                    }

                    if (lbldetId.Text == "AD000149")
                    {
                        chkSelect.Visible = false;
                        txtAmount.Visible = false;
                        drpIsCar.Visible = true;
                    }

                    if (lbldetId.Text == "AD000148")
                    {
                        chkSelect.Visible = false;
                        e.Row.Visible = false;
                        txtAmount.Visible = false;
                        drpCC.Visible = true;
                    }

                    if (lbldetId.Text == "AD000150")
                    {
                        e.Row.Visible = false;
                        chkSelect.Enabled = false;
                        txtAmount.ReadOnly = true;
                        ViewState["AD000150"] = txtAmount.Text;
                    }

                    if (lbldetId.Text == "AD000153")
                    {
                        e.Row.Visible = false;
                        chkSelect.Enabled = false;
                        txtAmount.ReadOnly = true;
                        ViewState["AD000153"] = txtAmount.Text;

                        if ((string)Session["sGrade"] == "L3" || (string)Session["sGrade"] == "L4" || (string)Session["sGrade"] == "L5")
                        {
                            chkSelect.Checked = true;
                        }
                        else
                        {
                            chkSelect.Checked = false;
                        }
                    }

                    if (lbldetId.Text == "AD000079" || lbldetId.Text == "AD000159" || lbldetId.Text == "AD000207")
                    {
                        txtAmount.ReadOnly = false;
                    }
                    else
                    {
                        txtAmount.ReadOnly = true;
                    }

                    if (lbldetId.Text == "AD000142")
                    {
                        chkSelect.Enabled = false;
                        txtAmount.ReadOnly = true;
                        ViewState["AD000142"] = txtAmount.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkSelect = (CheckBox)sender;
                Label lblDesc = (Label)chkSelect.NamingContainer.FindControl("lblDesc");
                TextBox txtAmount = (TextBox)chkSelect.NamingContainer.FindControl("txtAmount");
                TextBox txtpcnt = (TextBox)chkSelect.NamingContainer.FindControl("txtpcnt");
                DropDownList drpCardType = (DropDownList)chkSelect.NamingContainer.FindControl("drpCardType");
                DropDownList drpGiftCard = (DropDownList)chkSelect.NamingContainer.FindControl("drpGiftCard");
                TextBox txtAddr = (TextBox)chkSelect.NamingContainer.FindControl("txtAddr");
                TextBox txtPin = (TextBox)chkSelect.NamingContainer.FindControl("txtPin");
                Label lbldetId = (Label)chkSelect.NamingContainer.FindControl("lbldetId");

                if (chkSelect.Checked == true)
                {
                    if (Validate_Alternate(((Label)chkSelect.NamingContainer.FindControl("lbldetId")).Text.Trim()) != "")
                    {
                        chkSelect.Checked = false;
                        txtAmount.ReadOnly = true;
                        txtpcnt.ReadOnly = true;
                    }
                    else
                    {
                        if (lbldetId.Text.Trim() == "AD000012")
                        {
                            //string basic_amt = "";
                            //for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
                            //{
                            //    GridViewRow gvr = gvCTC.Rows[i];

                            //    if (((Label)gvr.FindControl("lblDesc")).Text == "BASIC")
                            //    {
                            //        basic_amt = ((TextBox)gvr.FindControl("txtAmount")).Text;
                            //        break;
                            //    }
                            //}
                            //double volpf_amt = (Convert.ToDouble(basic_amt) * 88) / 100;
                            double volpf_amt = 0;
                            txtAmount.Text = volpf_amt.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                            txtAmount.ReadOnly = true;
                            txtpcnt.ReadOnly = false;
                            //txtpcnt.Text = "88";
                        }
                        else if (lbldetId.Text.Trim() == "AD000262")
                        {
                            //string basic_amt = "";
                            //for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
                            //{
                            //    GridViewRow gvr = gvCTC.Rows[i];

                            //    if (((Label)gvr.FindControl("lblDesc")).Text == "BASIC")
                            //    {
                            //        basic_amt = ((TextBox)gvr.FindControl("txtAmount")).Text;
                            //        break;
                            //    }
                            //}
                            //double nps_amt = (Convert.ToDouble(basic_amt) * 10) / 100;
                            double nps_amt = 0;
                            txtAmount.Text = nps_amt.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                            txtAmount.ReadOnly = true;
                            txtpcnt.ReadOnly = false;
                            //txtpcnt.Text = "10";
                        }
                        else if (lbldetId.Text.Trim() == "AD000234")
                        {
                            string basic_amt = "";
                            for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
                            {
                                GridViewRow gvr = gvCTC.Rows[i];

                                if (((Label)gvr.FindControl("lblDesc")).Text == "BASIC")
                                {
                                    basic_amt = ((TextBox)gvr.FindControl("txtAmount")).Text;
                                    break;
                                }
                            }
                            double sup_amt = (Convert.ToDouble(basic_amt) * 12) / 100;
                            txtAmount.Text = sup_amt.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);
                            txtAmount.ReadOnly = true;
                            txtpcnt.ReadOnly = true;
                            //txtpcnt.Text = "10";
                        }
                    }

                    txtAddr.ReadOnly = false;
                    txtPin.ReadOnly = false;
                    if (lbldetId.Text == "AD000079" || lbldetId.Text == "AD000159" || lbldetId.Text == "AD000150"
                                    || lbldetId.Text == "AD000153" || lbldetId.Text == "AD000032" || lbldetId.Text == "AD000207" || lbldetId.Text == "AD000152")
                    {
                        txtAmount.ReadOnly = false;
                    }
                    if (lbldetId.Text == "AD000079")
                    {
                        drpCardType.Visible = true;
                    }

                    if (lbldetId.Text == "AD000130")
                    {
                        drpGiftCard.Visible = true;
                        txtAmount.Text = ((Label)chkSelect.NamingContainer.FindControl("lblLimit")).Text;
                    }


                }
                else
                {
                    txtAmount.ReadOnly = true;
                    txtpcnt.ReadOnly = true;
                    txtAmount.Text = "0";
                    txtpcnt.Text = "0";
                    txtAddr.Text = "";
                    txtPin.Text = "";
                    txtAddr.ReadOnly = true;
                    txtPin.ReadOnly = true;
                    if (lbldetId.Text == "AD000079")
                    {
                        drpCardType.Visible = false;
                    }
                    if (lbldetId.Text == "AD000130")
                    {
                        drpGiftCard.Visible = false;
                    }
                }

                if (ResetSpecialAllowance() == false)
                {
                    chkSelect.Checked = false;
                    txtAmount.ReadOnly = true;
                    txtpcnt.ReadOnly = true;
                }

                CalculateGross();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private bool ValidateAlternate()
        {
            string strMsg = "";
            int sel = 0;
            int man = 0;
            string DDLmsg = "";
            for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
            {
                GridViewRow gvr = gvCTC.Rows[i];

                if (((Label)gvr.FindControl("lblGroup")).Text != "OG" && ((Label)gvr.FindControl("lblGroup")).Text != "OA")
                {


                    if (((Label)gvr.FindControl("lblGroup")).Text == "A")
                    {
                        if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                        {
                            sel = sel + 1;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }

            for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
            {
                GridViewRow gvr = gvCTC.Rows[i];
                if (((Label)gvr.FindControl("lbldetId")).Text == "AD000002")
                {
                    double pc = Convert.ToDouble(((TextBox)gvr.FindControl("txtpcnt")).Text);

                    if (pc < 6 || pc > 50)
                    {
                        lblMessage.Text = objcommon.EncodeJsString("HRA % value must between 6 and 50.");
                        objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString("HRA % value must between 6 and 50."));
                        return false;
                    }
                }

                if (((Label)gvr.FindControl("lbldetId")).Text == "AD000079")
                {
                    if (((CheckBox)gvr.FindControl("chkSelect")).Checked)
                    {
                        if (((DropDownList)gvr.FindControl("drpCardType")).SelectedItem.Text == "[Select]")
                        {
                            DDLmsg = "Select option for Food Card/Gift Card.";
                        }
                    }
                }

                if (((Label)gvr.FindControl("lbldetId")).Text == "AD000130")
                {
                    if (((CheckBox)gvr.FindControl("chkSelect")).Checked)
                    {
                        if (((DropDownList)gvr.FindControl("drpGiftCard")).SelectedItem.Text == "[Select]")
                        {
                            DDLmsg = "Select option for Food Card/Gift Card.";
                        }
                    }
                }
            }

            for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
            {
                GridViewRow gvr = gvCTC.Rows[i];

                if (((Label)gvr.FindControl("lblGroup")).Text != "OG" && ((Label)gvr.FindControl("lblGroup")).Text != "OA")
                {
                    if (((Label)gvr.FindControl("lblshow")).Text == "1" && ((Label)gvr.FindControl("lblman")).Text == "1")
                    {
                        if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                        {
                            if (((TextBox)gvr.FindControl("txtAddr")).Text.Trim() == "" || ((TextBox)gvr.FindControl("txtPin")).Text.Trim() == "")
                            {
                                man = 1;
                                break;
                            }
                        }
                    }
                }
            }
            //if (sel == 0)
            //{
            //    strMsg = "Please select at least one allowance in Annexure C.";
            //    lblMessage.Text =strMsg ;
            //    objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(strMsg) + "');");
            //    return false;
            //}
            if (man == 1)
            {
                strMsg = "Please enter both address and pin code.";
                lblMessage.Text = strMsg;
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(strMsg) + "');");
                return false;
            }
            else if (DDLmsg != "")
            {
                strMsg = DDLmsg;
                lblMessage.Text = strMsg;
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(strMsg) + "');");
                return false;
            }
            else
            {
                return true;
            }
        }

        private string Validate_Alternate(string allId)
        {
            string strMsg = string.Empty;
            Boolean isFound = false;
            try
            {
                if ((string)Session["sCompID"].ToString() != "CO000125")
                {
                    dsInv = objInv.Validate_Alternate((string)Session["sCompID"], (string)Session["sEmpID"], allId);

                    if (dsInv.Tables.Count > 0)
                    {
                        if (dsInv.Tables[0].Rows.Count > 0)
                        {
                            for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                            {

                                if (Convert.ToString(dsInv.Tables[0].Rows[IntRow]["ALLWDED_AID"].ToString().Trim()) == allId)
                                {
                                    isFound = true;
                                }

                                //for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
                                //{
                                //    GridViewRow gvr = gvCTC.Rows[i];

                                //    if (((Label)gvr.FindControl("lblGroup")).Text == "A" && ((Label)gvr.FindControl("lblGroup")).Text != allId)
                                //    {
                                //        if (((Label)gvr.FindControl("lbldetId")).Text == Convert.ToString(dsInv.Tables[0].Rows[IntRow]["ALLWDED_AID"].ToString().Trim()) && ((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                                //        {
                                //            strMsg = "Can not select this allowance when " + ((Label)gvr.FindControl("lblDesc")).Text + " is selected.";
                                //            objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(strMsg) + "');");
                                //            return "Invalid";
                                //        }
                                //    }
                                //}

                            }


                        }


                        if (isFound == false)
                        {
                            strMsg = "Can not select this allowance for your grade.";
                            lblMessage.Text = strMsg;
                            objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(strMsg) + "');");
                            return "Invalid";
                        }

                        if (dsInv.Tables[1].Rows.Count > 0)
                        {
                            for (int IntRow = 0; IntRow <= dsInv.Tables[1].Rows.Count - 1; IntRow++)
                            {
                                for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
                                {
                                    GridViewRow gvr = gvCTC.Rows[i];

                                    if (((Label)gvr.FindControl("lblGroup")).Text != "OG" && ((Label)gvr.FindControl("lblGroup")).Text != "OA")
                                    {
                                        if (((Label)gvr.FindControl("lblGroup")).Text == "A" && ((Label)gvr.FindControl("lblGroup")).Text != allId)
                                        {
                                            if (((Label)gvr.FindControl("lbldetId")).Text == Convert.ToString(dsInv.Tables[1].Rows[IntRow]["NOT_ALLWDED_AID"].ToString().Trim()) && ((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                                            {
                                                strMsg = "Can not select this allowance when " + ((Label)gvr.FindControl("lblDesc")).Text + " is selected.";
                                                lblMessage.Text = strMsg;
                                                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(strMsg) + "');");
                                                return "Invalid";
                                            }
                                        }
                                    }
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                strMsg = ex.Message.ToString();
            }
            return strMsg;
        }

        private double CalculateAmount(string headAid, double pct)
        {
            double amount = 0;
            for (int i = 0; i <= this.gvCTC.Rows.Count - 1; i++)
            {
                GridViewRow gvr = gvCTC.Rows[i];

                if (headAid == ((Label)gvr.FindControl("lbldetId")).Text)
                {
                    amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));

                    amount = Math.Round((amount * pct) / 100, 0);

                    break;
                }
            }

            return amount;
        }

        private void CalculateGross()
        {
            double amount = 0;
            double totamount = 0;
            for (int i = 0; i <= this.gvCTC.Rows.Count - 1; i++)
            {
                GridViewRow gvr = gvCTC.Rows[i];
                Label lblid = (Label)gvr.FindControl("lbldetId");
                string allwId = lblid.Text;
                Label lblgr = (Label)gvr.FindControl("lblgross");
                string grId = lblgr.Text;
                if (((Label)gvr.FindControl("lblgross")).Text == "1" && ((Label)gvr.FindControl("lbldetId")).Text != "AD000012")
                {
                    amount = amount + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
                }
                totamount = totamount + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
            }

            amount = Math.Round(amount / 12);
            txtGross.Text = Convert.ToString(amount);
            //txtAnnCTC.Text = Convert.ToString(totamount); 
        }

        //private Boolean ResetSpecialAllowance()
        //{
        //    double CTCAmt = 0;
        //    for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
        //    {
        //        GridViewRow gvr = gvCTC.Rows[i];

        //        if (((Label)gvr.FindControl("lblGroup")).Text != "B" && ((Label)gvr.FindControl("lblGroup")).Text != "C")
        //        {
        //            if (((Label)gvr.FindControl("lblGroup")).Text != "OG" && ((Label)gvr.FindControl("lblGroup")).Text != "O"
        //                    && ((Label)gvr.FindControl("lbldetId")).Text != "AD000130" && ((Label)gvr.FindControl("lbldetId")).Text != "AD000012")
        //            {
        //                if ((((CheckBox)gvr.FindControl("chkSelect")).Checked == true))
        //                {
        //                    CTCAmt = CTCAmt + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
        //                }
        //            }
        //            //((TextBox)gvr.FindControl("txtAmount")).Text = "";
        //        }
        //        else if (((Label)gvr.FindControl("lbldetId")).Text == "AD000002")
        //        {
        //            CTCAmt = CTCAmt + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
        //        }
        //    }

        //    for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
        //    {
        //        GridViewRow gvr = gvCTC.Rows[i];

        //        if (((Label)gvr.FindControl("lblGroup")).Text == "B")
        //        {
        //            if (Convert.ToDouble(ViewState["BALAMT"]) - CTCAmt >= 0)
        //            {
        //                ((TextBox)gvr.FindControl("txtAmount")).Text = Convert.ToString(Math.Round(Convert.ToDouble(ViewState["BALAMT"]) - CTCAmt));
        //               // ((TextBox)gvr.FindControl("txtAmount")).Text = Convert.ToString(Math.Round(Convert.ToDouble(ViewState["BALAMT"])));
        //                return true;
        //            }
        //            else
        //            {
        //                lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " can not be less than zero.";
        //                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " can not be less than zero.');");
        //                return false;
        //            }
        //        }
        //    }

        //    return true;
        //}
        private Boolean ResetSpecialAllowance()
        {
            double CTCAmt = 0;
            for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
            {
                GridViewRow gvr = gvCTC.Rows[i];




                if (((Label)gvr.FindControl("lblGroup")).Text != "B" && ((Label)gvr.FindControl("lblGroup")).Text != "C")
                {
                    if (((Label)gvr.FindControl("lblGroup")).Text != "OG" && ((Label)gvr.FindControl("lblGroup")).Text != "O"
                            && ((Label)gvr.FindControl("lbldetId")).Text != "AD000130" && ((Label)gvr.FindControl("lbldetId")).Text != "AD000012")
                    {
                        if ((((CheckBox)gvr.FindControl("chkSelect")).Checked == true))
                        {
                            CTCAmt = CTCAmt + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
                        }
                    }
                    //((TextBox)gvr.FindControl("txtAmount")).Text = "";
                }
                else if (((Label)gvr.FindControl("lbldetId")).Text == "AD000002")
                {
                    CTCAmt = CTCAmt + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
                }
            }

            for (int i = this.gvCTC.Rows.Count - 1; i >= 0; i--)
            {

                GridViewRow gvr = gvCTC.Rows[i];

                if (((Label)gvr.FindControl("lblGroup")).Text == "B")
                {
                    if (Convert.ToDouble(ViewState["BALAMT"]) - CTCAmt >= 0)
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).Text = Convert.ToString(Math.Round(Convert.ToDouble(ViewState["BALAMT"]) - CTCAmt));
                        //((TextBox)gvr.FindControl("txtAmount")).Text = Convert.ToString(Math.Round(Convert.ToDouble(ViewState["BALAMT"])));
                        return true;
                    }
                    else
                    {
                        lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " can not be less than zero.";
                        objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " can not be less than zero.');");
                        return false;
                    }
                }
            }

            return true;
        }

        protected void txtpcnt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double Limit = 0;
                double Amount = 0;

                TextBox txtpcnt = (TextBox)sender;
                Label lblpct = (Label)txtpcnt.NamingContainer.FindControl("lblpct");
                Label lblhead = (Label)txtpcnt.NamingContainer.FindControl("lblhead");
                TextBox txtAmount = (TextBox)txtpcnt.NamingContainer.FindControl("txtAmount");

                Limit = Convert.ToDouble((lblpct.Text.Trim() == "" ? "0" : lblpct.Text.Trim()));
                Amount = Convert.ToDouble((txtpcnt.Text.Trim() == "" ? "0" : txtpcnt.Text.Trim()));

                if (((Label)((TextBox)sender).NamingContainer.FindControl("lbldetId")).Text == "AD000002")
                {
                    if (Amount < 6 || Amount > 50)
                    {
                        lblMessage.Text = objcommon.EncodeJsString("HRA % value must between 6 and 50.");
                        objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString("HRA % value must between 6 and 50."));
                        txtpcnt.Text = "0";
                        txtAmount.Text = "0";
                        return;
                    }
                }

                if (Limit != 0 && Amount > Limit)
                {
                    lblMessage.Text = objcommon.EncodeJsString(((Label)txtpcnt.NamingContainer.FindControl("lblDesc")).Text) + " % can not be greater than limit.";
                    objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)txtpcnt.NamingContainer.FindControl("lblDesc")).Text) + " % can not be greater than limit.');");
                    txtpcnt.Text = "0";
                    txtAmount.Text = "0";
                    return;
                }

                txtAmount.Text = Convert.ToString(CalculateAmount(lblhead.Text.Trim(), Amount));

                ResetSpecialAllowance();
                CalculateGross();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void txtAmount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double Limit = 0;
                double Amount = 0;

                TextBox txtAmount = (TextBox)sender;
                Label lblLimit = (Label)txtAmount.NamingContainer.FindControl("lblLimit");
                Label lblDesc = (Label)txtAmount.NamingContainer.FindControl("lblDesc");
                Label lbldetId = (Label)txtAmount.NamingContainer.FindControl("lbldetId");
                DropDownList drpIsCar = (DropDownList)txtAmount.NamingContainer.FindControl("drpIsCar");

                Limit = Convert.ToDouble((lblLimit.Text.Trim() == "" ? "0" : lblLimit.Text.Trim()));
                Amount = Convert.ToDouble((txtAmount.Text.Trim() == "" ? "0" : txtAmount.Text.Trim()));

                if (lbldetId.Text.Trim() == "AD000150")
                {
                    Limit = 28800;
                }

                if (Convert.ToDouble(((string)ViewState["AD000142"]).Trim() == "" ? "0" : ((string)ViewState["AD000142"]).Trim()) > 0 && (lbldetId.Text == "AD000150" || lbldetId.Text == "AD000153"))
                {

                }
                else if (Limit != 0 && Amount > Limit)
                {
                    lblMessage.Text = objcommon.EncodeJsString(((Label)txtAmount.NamingContainer.FindControl("lblDesc")).Text) + " amount can not be greater than limit.";
                    objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)txtAmount.NamingContainer.FindControl("lblDesc")).Text) + " amount can not be greater than limit.');");
                    txtAmount.Text = "0";
                    return;
                }

                if (lblDesc.Text == "SODEXO")
                {
                    if (Amount % 6000 != 0)
                    {
                        if (Convert.ToString(Amount) != "26400")
                        {
                            lblMessage.Text = objcommon.EncodeJsString("SODEXO amount should be among 6000,12000,18000,24000 or 26400.");
                            objcommon.Display("Validate", "DisplayErrorMessage('SODEXO amount should be among 6000,12000,18000,24000 or 26400.')");
                            txtAmount.Text = "0";
                            return;
                        }
                    }
                }

                if (Convert.ToDouble(((string)ViewState["AD000142"]).Trim() == "" ? "0" : ((string)ViewState["AD000142"]).Trim()) > 0)
                {
                    double fuel = 0;
                    double driver = 0;
                    double limit = 0;

                    if ((string)Session["sGrade"] == "L3")
                    {
                        limit = 400000;
                    }
                    else if ((string)Session["sGrade"] == "L4" || (string)Session["sGrade"] == "L5")
                    {
                        limit = 600000;
                    }

                    foreach (GridViewRow gvr in gvCTC.Rows)
                    {
                        if (((Label)gvr.FindControl("lbldetId")).Text == "AD000150")
                        {
                            fuel = Convert.ToDouble(((TextBox)gvr.FindControl("txtAmount")).Text.Trim() == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text.Trim());
                        }

                        if (((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                        {
                            driver = Convert.ToDouble(((TextBox)gvr.FindControl("txtAmount")).Text.Trim() == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text.Trim());
                        }
                    }

                    if ((fuel + driver) > limit)
                    {
                        lblMessage.Text = objcommon.EncodeJsString("Sum of Car Maintenance and Fuel and Driver Salary fields cannot be greater than " + limit + ".");
                        objcommon.Display("Validate", "DisplayErrorMessage('Sum of Car Maintenance and Fuel and Driver Salary fields cannot be greater than " + limit + ".')");
                        txtAmount.Text = "0";
                        return;
                    }
                }

                if (Convert.ToDouble(((string)ViewState["AD000142"]).Trim() == "" ? "0" : ((string)ViewState["AD000142"]).Trim()) == 0)
                {
                    SetCarDetails();
                }
                ResetSpecialAllowance();
                CalculateGross();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnFlexiPrint_Click(object sender, EventArgs e)
        {
            Session["AnnCTC"] = txtAnnCTC.Text;
            Session["MonCTC"] = txtMonCTC.Text;
            Session["Gross"] = txtGross.Text;

            Response.Redirect("FlexiPrint.aspx", false);
        }

        protected void drpIsCar_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpCar = (DropDownList)sender;

            if (drpCar.SelectedValue == "Yes")
            {
                foreach (GridViewRow gvr in gvCTC.Rows)
                {
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000148")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).Text = (string)ViewState["AD000148"];
                        ((CheckBox)gvr.FindControl("chkSelect")).Checked = true;
                        gvr.Visible = true;
                    }
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000150")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).Text = (string)ViewState["AD000150"];
                        ((CheckBox)gvr.FindControl("chkSelect")).Checked = true;
                        gvr.Visible = true;
                    }
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).Text = (string)ViewState["AD000153"];
                        ((DropDownList)gvr.FindControl("drpIsDriver")).Text = Convert.ToDouble(ViewState["AD000153"]) == 0 ? "No" : "Yes";
                        ((DropDownList)gvr.FindControl("drpIsDriver")).Visible = true;
                        ((CheckBox)gvr.FindControl("chkSelect")).Checked = true;
                        if ((string)Session["sGrade"] != "L2")
                        {
                            gvr.Visible = true;
                        }
                        else
                        {
                            gvr.Visible = false;
                        }
                    }
                }
            }
            else
            {
                foreach (GridViewRow gvr in gvCTC.Rows)
                {
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000148" || ((Label)gvr.FindControl("lbldetId")).Text == "AD000150" || ((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).Text = "0";
                        gvr.Visible = false;
                    }
                }
            }
        }

        protected void drpCC_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpCC = (DropDownList)sender;

            foreach (GridViewRow gvr in gvCTC.Rows)
            {
                if (((Label)gvr.FindControl("lbldetId")).Text == "AD000150")
                {
                    //if (Convert.ToDouble(Session["sCarLease"]) > 0)
                    //{
                    //    ((TextBox)gvr.FindControl("txtAmount")).ReadOnly = false;
                    //}
                    //else
                    //{
                    if (drpCC.SelectedValue == "CC-")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).Text = "21600";
                    }
                    else if (drpCC.SelectedValue == "CC+")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).Text = "28800";
                    }
                    else
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).Text = "0";
                    }
                    //}
                }
            }
        }

        protected void drpIsDriver_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpIsDriver = (DropDownList)sender;

            if (drpIsDriver.SelectedValue == "Yes")
            {
                //if (Convert.ToDouble(Session["sCarLease"]) > 0)
                //{
                //    ((TextBox)drpIsDriver.NamingContainer.FindControl("txtAmount")).ReadOnly = false;
                //}
                //else
                //{
                ((TextBox)drpIsDriver.NamingContainer.FindControl("txtAmount")).Text = "10800";
                //}
            }
            else
            {
                ((TextBox)drpIsDriver.NamingContainer.FindControl("txtAmount")).Text = "0";
            }
        }

        public void SetCarDetails()
        {
            DropDownList drpIsCar = new DropDownList();
            DropDownList drpCC = new DropDownList();

            //if ((int)ViewState["CarLease"] != 1)
            //{
            foreach (GridViewRow gvr in gvCTC.Rows)
            {
                if (((Label)gvr.FindControl("lbldetId")).Text == "AD000149")
                {
                    drpIsCar = (DropDownList)gvr.FindControl("drpIsCar");
                }

                if (((Label)gvr.FindControl("lbldetId")).Text == "AD000148")
                {
                    drpCC = (DropDownList)gvr.FindControl("drpCC");
                }
            }

            foreach (GridViewRow gvr in gvCTC.Rows)
            {
                if (drpIsCar.SelectedValue == "Yes")
                {
                    if (drpCC.SelectedValue == "CC-" && ((Label)gvr.FindControl("lbldetId")).Text == "AD000150")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).Text = "21600";
                    }
                    else if (drpCC.SelectedValue == "CC+" && ((Label)gvr.FindControl("lbldetId")).Text == "AD000150")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).Text = "28800";
                    }
                    else if (drpCC.SelectedValue == "[Select]" && ((Label)gvr.FindControl("lbldetId")).Text == "AD000150")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).Text = "0";
                    }
                }
                else
                {
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000148" || ((Label)gvr.FindControl("lbldetId")).Text == "AD000150" || ((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).Text = "0";
                    }
                }
            }
            //}
        }

        public void SetCarDetailsView()
        {
            TextBox txtCarFuel = new TextBox();
            TextBox txtDriverSal = new TextBox();

            if (Convert.ToDouble(((string)ViewState["AD000142"]).Trim() == "" ? "0" : ((string)ViewState["AD000142"]).Trim()) > 0)
            {
                foreach (GridViewRow gvr in gvCTC.Rows)
                {
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000148")
                    {
                        gvr.Visible = false;
                    }
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000149")
                    {
                        gvr.Visible = false;
                    }

                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000150")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).ReadOnly = false;
                        gvr.Visible = true;
                    }
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).ReadOnly = false;
                        gvr.Visible = true;
                    }
                }
            }
            else
            {
                foreach (GridViewRow gvr in gvCTC.Rows)
                {
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000150")
                    {
                        txtCarFuel = (TextBox)gvr.FindControl("txtAmount");
                    }
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                    {
                        txtDriverSal = (TextBox)gvr.FindControl("txtAmount");
                    }
                }

                foreach (GridViewRow gvr in gvCTC.Rows)
                {
                    //if (Convert.ToDouble(Session["sCarLease"]) > 0)
                    //{
                    //    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000149")
                    //    {
                    //        ((DropDownList)gvr.FindControl("drpIsCar")).SelectedValue = "Yes";
                    //        gvr.Visible = false;
                    //    }

                    //    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000148")
                    //    {
                    //        gvr.Visible = false;
                    //    }

                    //    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000150" || ((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                    //    {
                    //        gvr.Visible = true;
                    //        ((TextBox)gvr.FindControl("txtAmount")).ReadOnly = false;
                    //    }

                    //    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                    //    {
                    //        ((DropDownList)gvr.FindControl("drpIsDriver")).Visible = false;
                    //    }
                    //}
                    //else 
                    if (Convert.ToDouble(txtCarFuel.Text) == 21600 || Convert.ToDouble(txtCarFuel.Text) == 28800 || Convert.ToDouble(txtDriverSal.Text) > 0)
                    {
                        if (((Label)gvr.FindControl("lbldetId")).Text == "AD000149")
                        {
                            ((DropDownList)gvr.FindControl("drpIsCar")).SelectedValue = "Yes";
                        }

                        if (((Label)gvr.FindControl("lbldetId")).Text == "AD000148" || ((Label)gvr.FindControl("lbldetId")).Text == "AD000150" || ((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                        {
                            gvr.Visible = true;
                        }

                        if (Convert.ToDouble(txtCarFuel.Text) == 21600)
                        {
                            if (((Label)gvr.FindControl("lbldetId")).Text == "AD000148")
                            {
                                DropDownList drpCC = (DropDownList)gvr.FindControl("drpCC");
                                drpCC.SelectedValue = "CC-";
                            }
                        }
                        else if (Convert.ToDouble(txtCarFuel.Text) == 28800)
                        {
                            if (((Label)gvr.FindControl("lbldetId")).Text == "AD000148")
                            {
                                DropDownList drpCC = (DropDownList)gvr.FindControl("drpCC");
                                drpCC.SelectedValue = "CC+";
                            }
                        }
                        else
                        {
                            if (((Label)gvr.FindControl("lbldetId")).Text == "AD000148")
                            {
                                DropDownList drpCC = (DropDownList)gvr.FindControl("drpCC");
                                drpCC.SelectedValue = "[Select]";
                            }
                        }

                        if (((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                        {
                            ((DropDownList)gvr.FindControl("drpIsDriver")).Visible = true;

                            if (Convert.ToDouble(((TextBox)gvr.FindControl("txtAmount")).Text) > 0)
                            {
                                ((DropDownList)gvr.FindControl("drpIsDriver")).SelectedValue = "Yes";
                            }
                            else
                            {
                                ((DropDownList)gvr.FindControl("drpIsDriver")).SelectedValue = "No";
                            }
                        }
                    }
                    else
                    {
                        if (((Label)gvr.FindControl("lbldetId")).Text == "AD000149")
                        {
                            ((DropDownList)gvr.FindControl("drpIsCar")).SelectedValue = "No";
                        }

                        if (((Label)gvr.FindControl("lbldetId")).Text == "AD000148" || ((Label)gvr.FindControl("lbldetId")).Text == "AD000150" || ((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                        {
                            gvr.Visible = false;
                        }
                    }
                }
            }

            if ((string)Session["sGrade"] == "L1")
            {
                foreach (GridViewRow gvr in gvCTC.Rows)
                {
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000148")
                    {
                        gvr.Visible = false;
                    }
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000149")
                    {
                        gvr.Visible = false;
                    }
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000150")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).ReadOnly = false;
                        gvr.Visible = false;
                    }
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).ReadOnly = false;
                        gvr.Visible = false;
                    }
                }
            }

            if ((string)Session["sGrade"] == "L2")
            {
                foreach (GridViewRow gvr in gvCTC.Rows)
                {
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000153")
                    {
                        ((TextBox)gvr.FindControl("txtAmount")).ReadOnly = false;
                        gvr.Visible = false;
                    }
                }
            }
        }

        protected void btnUploadLTA_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (txtLTANo.Text.Trim() == "" || txtLTAAmt.Text.Trim() == "")
                {
                    lblMessage.Text = "Both No. of Individuals and LTA amount fields are mandatory.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Both No. of Individuals and LTA amount fields are mandatory.');");
                    return;
                }

                if (chkAgree.Checked == false)
                {
                    lblMessage.Text = "You must agree to the disclaimer before submitting.";
                    objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
                    return;
                }

                if (fupLTA.PostedFile != null)
                {
                    if (Convert.ToString(fupLTA.PostedFile.FileName) == "")
                    {
                        lblMessage.Text = "Browse file to upload.";
                        return;
                    }
                    else
                    {
                        UploadFileLTA();
                        FillSupportDetails();
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

        private void UploadFileLTA()
        {
            try
            {
                string savePath;
                if (fupLTA.PostedFile.FileName == "")
                {
                    lblMessage.Text = "Browse document.";
                    return;
                }

                if (txtLTAAmt.Text.Trim() == "")
                {
                    lblMessage.Text = "Enter LTA Amount.";
                    return;
                }

                if (System.IO.Path.GetExtension(fupLTA.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupLTA.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupLTA.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupLTA.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupLTA.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    lblMessage.Text = "Only PDF,JPG,7Z,ZIP and RAR files allowed.";
                    return;
                }

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                System.IO.Stream fileInputStream = fupLTA.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                fileInputStream.Close();
                /* Create Folder */

                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = Path.GetFileNameWithoutExtension(fupLTA.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupLTA.FileName.Trim());

                string filesToDelete = "LTA_*";
                string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
                foreach (string file in fileList)
                {
                    System.IO.File.Delete(file);
                }

                //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                //{
                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + "LTA_" + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();

                objInv.UpdateFlexipaySupport("AD000159", (string)Session["sCompID"], (string)Session["sEmpID"], txtLTAAmt.Text.Trim(), txtLTANo.Text.Trim());
                objSup.Upload_Support_Flexi((string)Session["sCompID"], (string)Session["sEmpID"], "LTA_" + fileName, "2020-2021");
                //DisplayDocuments();
                lblMessage.Text = "File uploaded successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;  //ex.Message;
            }
        }

        protected void btnUploadTel_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (chkAgree.Checked == false)
                {
                    lblMessage.Text = "You must agree to the disclaimer before submitting.";
                    objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
                    return;
                }

                if (fupTel.PostedFile != null)
                {
                    if (Convert.ToString(fupTel.PostedFile.FileName) == "")
                    {
                        lblMessage.Text = "Browse file to upload.";
                        return;
                    }
                    else
                    {
                        UploadFileTel();
                        FillSupportDetails();
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

        private void UploadFileTel()
        {
            try
            {
                string savePath;
                if (fupTel.PostedFile.FileName == "")
                {
                    lblMessage.Text = "Browse document.";
                    return;
                }

                if (txtTelAmt.Text.Trim() == "")
                {
                    lblMessage.Text = "Enter Telephone Reimbursement Amount.";
                    return;
                }

                if (System.IO.Path.GetExtension(fupTel.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupTel.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupTel.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupTel.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupTel.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    lblMessage.Text = "Only PDF,JPG,7Z,ZIP and RAR files allowed.";
                    return;
                }

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                System.IO.Stream fileInputStream = fupTel.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                fileInputStream.Close();
                /* Create Folder */

                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = Path.GetFileNameWithoutExtension(fupTel.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupTel.FileName.Trim());

                string filesToDelete = "TEL_*";
                string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
                foreach (string file in fileList)
                {
                    System.IO.File.Delete(file);
                }

                //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                //{
                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + "TEL_" + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();
                objInv.UpdateFlexipaySupport("AD000207", (string)Session["sCompID"], (string)Session["sEmpID"], txtTelAmt.Text.Trim());
                objSup.Upload_Support_Flexi((string)Session["sCompID"], (string)Session["sEmpID"], "TEL_" + fileName, "2020-2021");
                //DisplayDocuments();
                lblMessage.Text = "File uploaded successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;  //ex.Message;
            }
        }

        protected void btnUploadFuel_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (chkAgree.Checked == false)
                {
                    lblMessage.Text = "You must agree to the disclaimer before submitting.";
                    objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
                    return;
                }

                if (fupFuel.PostedFile != null)
                {
                    if (Convert.ToString(fupFuel.PostedFile.FileName) == "")
                    {
                        lblMessage.Text = "Browse file to upload.";
                        return;
                    }
                    else
                    {
                        UploadFileFuel();
                        FillSupportDetails();
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

        private void UploadFileFuel()
        {
            try
            {
                string savePath;
                if (fupFuel.PostedFile.FileName == "")
                {
                    lblMessage.Text = "Browse document.";
                    return;
                }

                if (txtFuelAmt.Text.Trim() == "")
                {
                    lblMessage.Text = "Enter Car Maintenance and Fuel Amount.";
                    return;
                }

                if (System.IO.Path.GetExtension(fupFuel.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupFuel.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupFuel.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupFuel.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupFuel.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    lblMessage.Text = "Only PDF,JPG,7Z,ZIP and RAR files allowed.";
                    return;
                }

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                System.IO.Stream fileInputStream = fupFuel.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                fileInputStream.Close();
                /* Create Folder */

                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = Path.GetFileNameWithoutExtension(fupFuel.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupFuel.FileName.Trim());

                string filesToDelete = "FUEL_*";
                string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
                foreach (string file in fileList)
                {
                    System.IO.File.Delete(file);
                }

                //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                //{
                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + "FUEL_" + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();
                objInv.UpdateFlexipaySupport("AD000150", (string)Session["sCompID"], (string)Session["sEmpID"], txtFuelAmt.Text.Trim());
                objSup.Upload_Support_Flexi((string)Session["sCompID"], (string)Session["sEmpID"], "FUEL_" + fileName, "2020-2021");
                //DisplayDocuments();
                lblMessage.Text = "File uploaded successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;  //ex.Message;
            }
        }

        protected void btnUploadDriver_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (chkAgree.Checked == false)
                {
                    lblMessage.Text = "You must agree to the disclaimer before submitting.";
                    objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
                    return;
                }

                if (fupDriver.PostedFile != null)
                {
                    if (Convert.ToString(fupDriver.PostedFile.FileName) == "")
                    {
                        lblMessage.Text = "Browse file to upload.";
                        return;
                    }
                    else
                    {
                        UploadFileDriver();
                        FillSupportDetails();
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

        private void UploadFileDriver()
        {
            try
            {
                string savePath;
                if (fupDriver.PostedFile.FileName == "")
                {
                    lblMessage.Text = "Browse document.";
                    return;
                }

                if (txtDriverAmt.Text.Trim() == "")
                {
                    lblMessage.Text = "Enter Driver Salary Amount.";
                    return;
                }

                if (System.IO.Path.GetExtension(fupDriver.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupDriver.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupDriver.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupDriver.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupDriver.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    lblMessage.Text = "Only PDF,JPG,7Z,ZIP and RAR files allowed.";
                    return;
                }

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                System.IO.Stream fileInputStream = fupDriver.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                fileInputStream.Close();
                /* Create Folder */

                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = Path.GetFileNameWithoutExtension(fupDriver.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupDriver.FileName.Trim());

                string filesToDelete = "DRV_*";
                string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
                foreach (string file in fileList)
                {
                    System.IO.File.Delete(file);
                }

                //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                //{
                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + "DRV_" + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();
                objInv.UpdateFlexipaySupport("AD000153", (string)Session["sCompID"], (string)Session["sEmpID"], txtDriverAmt.Text.Trim());
                objSup.Upload_Support_Flexi((string)Session["sCompID"], (string)Session["sEmpID"], "DRV_" + fileName, "2020-2021");
                //DisplayDocuments();
                lblMessage.Text = "File uploaded successfully.";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;  //ex.Message;
            }
        }

        protected void lnkDownloadLTA_Click(object sender, EventArgs e)
        {
            try
            {
                string SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                string filesToDelete = "LTA_*";
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkDownloadTel_Click(object sender, EventArgs e)
        {
            try
            {
                string SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                string filesToDelete = "TEL_*";
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkDownloadFuel_Click(object sender, EventArgs e)
        {
            try
            {
                string SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                string filesToDelete = "FUEL_*";
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkDownloadDriver_Click(object sender, EventArgs e)
        {
            try
            {
                string SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                string filesToDelete = "DRV_*";
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
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
            try
            {
                CreateDocumentsStructurePrev();

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                string savePath2 = Request.PhysicalApplicationPath;
                savePath2 = savePath2 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive2\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                string savePath3 = Request.PhysicalApplicationPath;
                savePath3 = savePath3 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive3\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                string savePath4 = Request.PhysicalApplicationPath;
                savePath4 = savePath4 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive4\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                string savePath5 = Request.PhysicalApplicationPath;
                savePath5 = savePath5 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive5\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

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
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath2);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }
                }

                if (System.IO.Directory.Exists(@savePath3) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath3);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }
                }

                if (System.IO.Directory.Exists(@savePath4) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath4);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }
                }

                if (System.IO.Directory.Exists(@savePath5) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath5);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }
                }

                this.gvPrevFiles.DataSource = dtDocInfo;
                this.gvPrevFiles.DataBind();

                foreach (GridViewRow gr in gvPrevFiles.Rows)
                {
                    LinkButton b = gr.FindControl("lnkBtnOpenFile") as LinkButton;
                    this.smInv.RegisterPostBackControl(b);
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkBtnOpenFilePrev_Click(object sender, EventArgs e)
        {
            try
            {
                smInv.RegisterPostBackControl((LinkButton)sender);

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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void lnkForms_Click(object sender, EventArgs e)
        {
            try
            {
                smInv.RegisterPostBackControl((LinkButton)sender);

                System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/indiafirst/FLEXI_FORMS.zip"));
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=FLEXI_FORMS.zip");
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void lnkFAQLTA_Click(object sender, EventArgs e)
        {
            try
            {
                smInv.RegisterPostBackControl((LinkButton)sender);

                System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/indiafirst/FAQs-LTA_Scheme_Revised.pdf"));
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=FAQs-LTA_Scheme_Revised.pdf");
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void grNewCTC_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataSet ds = new DataSet();
            bool rent = false;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    Label lbldetId = (Label)e.Row.FindControl("lblNewdetId");

                    CheckBox chkSelect = (CheckBox)e.Row.FindControl("chkNewSelect");
                    ds = objInv.GetNewCTCDetails((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"], lbldetId.Text, txtCTC, txtBal, txtAnnCTC, drpCarLease, out rent);
                    if (Convert.ToInt32(ds.Tables[2].Rows[0]["ALLWDED_AID"].ToString()) >= 1)
                    {
                        chkSelect.Checked = true;
                        if (chkSelect.Checked == true)
                        {
                            totamountflexi = totamountflexi + Convert.ToDouble((((Label)e.Row.FindControl("lblNewLimit")).Text == "" ? "0" : ((Label)e.Row.FindControl("lblNewLimit")).Text));
                        }
                    }
                    txtNewTotalAmount.Text = Convert.ToString(totamountflexi) + ".00";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }


        protected void chkNewSelect_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chkSelect = (CheckBox)sender;

                //  CalculateNewGross();
                for (int i = 0; i <= this.gvNewCTC.Rows.Count - 1; i++)
                {
                    GridViewRow gvr = gvNewCTC.Rows[i];
                    CheckBox chk = (CheckBox)gvr.FindControl("chkNewSelect");
                    Label lblDesc = (Label)gvr.FindControl("lblNewDesc");
                    //Label txtAmount = (Label)gvr.FindControl("lblNewLimit");
                    Label lbldetId = (Label)gvr.FindControl("lblNewdetId");

                    if (chk.Checked == true)
                    {
                        //var number = 123.46;
                        //String.Format(number % 1 == 0 ? "{0:0}" : "{0:0.00}", number)


                        totamountflexi = totamountflexi + Convert.ToDouble((((Label)gvr.FindControl("lblNewLimit")).Text == "" ? "0" : ((Label)gvr.FindControl("lblNewLimit")).Text));
                    }


                    txtNewTotalAmount.Text = Convert.ToString(totamountflexi) + ".00";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        //private void CalculateNewGross()
        //{

        //    double amount = 0;
        //    double totamount = 0;

        //    for (int i = 0; i <= this.gvNewCTC.Rows.Count - 1; i++)
        //    {
        //        GridViewRow gvr = gvNewCTC.Rows[i];

        //        //if (txtTotalAmount.Text == "1" && ((Label)gvr.FindControl("lblNewdetId")).Text != "AD000012")
        //        //{
        //        //    amount = amount + Convert.ToDouble((((Label)gvr.FindControl("lblNewLimit")).Text == "" ? "0" : ((Label)gvr.FindControl("lblNewLimit")).Text));
        //        //}
        //        totamount = totamount + Convert.ToDouble((((Label)gvr.FindControl("lblNewLimit")).Text == "" ? "0" : ((Label)gvr.FindControl("lblNewLimit")).Text));
        //    }

        //    amount = Math.Round(amount / 12);
        //    txtNewTotalAmount.Text = Convert.ToString(totamount);
        //    //txtAnnCTC.Text = Convert.ToString(totamount); 

        //}

        protected void drpStatutoryPF_SelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList drpStatutoryPF = (DropDownList)sender;

            double number;
            double amt = Convert.ToDouble(Session["BASICAMT"].ToString());
            //double amtSpAllw = Convert.ToDouble(Session["SPECIALALLOW"].ToString());
            //double amtPF = Convert.ToDouble(Session["STATUTORYPFAMT"].ToString());
            double amtSpAllw = 0;
            double amtPF = 0;
            //ViewState["DrpStatutoryPF"] = drpStatutoryPF.SelectedValue;
            //string drp = ViewState["DrpStatutoryPF"].ToString();
            if (drpStatutoryPF.SelectedValue == "Yes")
            {
                foreach (GridViewRow gvr in gvCTC.Rows)
                {
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000177")
                    {

                        amtSpAllw = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
                    }
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000011")
                    {
                        amtPF = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
                    }


                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000011")
                    {
                        number = (Convert.ToDouble(Session["BASICAMT"].ToString()) * 12) / 100;
                        if (drpStatutoryPF.SelectedValue == "Yes")
                        {
                            ((TextBox)gvr.FindControl("txtAmount")).Text = Math.Round(number).ToString();
                        }


                        else
                        {
                            ((TextBox)gvr.FindControl("txtAmount")).Text = "0";
                        }
                        //}
                    }
                    //if (((Label)gvr.FindControl("lbldetId")).Text == "AD000177")
                    //{
                    //    number = (Convert.ToDouble(Session["BASICAMT"].ToString()) * 12) / 100;
                    //    ((TextBox)gvr.FindControl("txtAmount")).Text = (amtSpAllw + amtPF - Math.Round(number)).ToString();
                    //    //ViewState["BALAMTA"] = (amtSpAllw + amtPF - Math.Round(number)).ToString();
                    //    //string spamt = ViewState["BALAMTA"].ToString();
                    //}

                }
            }
            else if (drpStatutoryPF.SelectedValue == "No")
            {
                foreach (GridViewRow gvr in gvCTC.Rows)
                {
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000177")
                    {
                        amtSpAllw = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
                    }
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000011")
                    {
                        amtPF = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmount")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmount")).Text));
                    }
                    number = (Convert.ToDouble(Session["BASICAMT"].ToString()) * 12) / 100;
                    if (((Label)gvr.FindControl("lbldetId")).Text == "AD000011")
                    {

                        if (drpStatutoryPF.SelectedValue == "No")
                        {
                            ((TextBox)gvr.FindControl("txtAmount")).Text = "21600";


                        }
                    }
                    //if (((Label)gvr.FindControl("lbldetId")).Text == "AD000177")
                    //{

                    //    ((TextBox)gvr.FindControl("txtAmount")).Text = (amtSpAllw + amtPF - 21600).ToString();
                    //    //ViewState["BALAMTA"] = (amtSpAllw + amtPF - 21600).ToString();
                    //    //string spamt = ViewState["BALAMTA"].ToString();
                    //}

                }
            }

        }
    }
}