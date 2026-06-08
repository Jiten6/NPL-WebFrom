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

namespace NewPortal2023.ESS
{
    public partial class InvestmentDetailsUploadsNF : System.Web.UI.Page
    {
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();
        NewPortal2023.ESS.Investments objInv = new NewPortal2023.ESS.Investments();
        NewPortal2023.ESS.FlexiPay objFlexi = new NewPortal2023.ESS.FlexiPay();
        NewPortal2023.ESS.Support objSup = new NewPortal2023.ESS.Support();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Tax objTax = new NewPortal2023.ESS.Tax();
        DataSet dsInv = new DataSet();
        DataSet DsTest = new DataSet();
        DataSet dsrg = new DataSet();
        ArrayList arrFor = new ArrayList();
        private string savePath = string.Empty;

        List<string> ignoreForUploadCheck = new List<string>
    {
        "CO000056",
        "CO000061",
        "CO000078",
        "CO000125",
        "CO000114",
    };

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!Page.IsPostBack)
                {
                    ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                    

                    if (Session["sCompID"].ToString() != null)
                    {
                        string strResult = objcommon.Validate_ControlInfo("INV");

                        if (strResult.Contains("This page is currently unavailable.") == true)
                        {
                            Response.Redirect("Unavailable.aspx?strName=Investment Details");
                            return;
                        }
                        //lnkDownloadRentSupport.Text = "";
                        //lnkDownloadLoanSupport.Text = "";
                        //lnkDownloadLTA.Text = "";
                        //lnkDownloadDriver.Text = "";
                        //lnkDownloadFuel.Text = "";
                        //lnkDownloadTel.Text = "";

                        if (Session["sCompID"].ToString() == "CO000114")
                        {
                            //if ((Session["MenuList"] != null && !((ArrayList)Session["MenuList"]).Contains("Investment Declaration Supports")))
                            //{
                            //    if ((Session["MenuList"] != null && !((ArrayList)Session["MenuList"]).Contains("Investment Declaration")))
                            //    {
                            //        Response.Redirect("Logout.aspx", false);
                            //    }
                            //}
                            if ((Session["MenuList"] != null && !((ArrayList)Session["MenuList"]).Contains("Investment Supports")))
                            {
                                if ((Session["MenuList"] != null && !((ArrayList)Session["MenuList"]).Contains("Investment Declaration")))
                                {
                                    Response.Redirect("Logout.aspx", false);
                                }
                            }
                        }
                        else
                        {
                            if ((Session["MenuList"] != null && !((ArrayList)Session["MenuList"]).Contains("Investment Supports")))
                            {
                                if ((Session["MenuList"] != null && !((ArrayList)Session["MenuList"]).Contains("Investment Details")))
                                {
                                    Response.Redirect("Logout.aspx", false);
                                }
                            }

                            OtherDetails.Visible = false;
                            InvDetails.Visible = false;
                            RentDetails.Visible = false;
                            TwelveDetails.Visible = false;
                            TwelveBDetails.Visible = false;

                        }

                        FillAll();
                    }
                    else
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Error occurred in application (COMP).";
                        objcommon.Display("Validate", "DisplayErrorMessage('Error occurred in application (COMP).');");
                        divAlertSucc.Visible = false;

                    }
                }

            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
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
                lblMessageSucc.Text = Session["Message"].ToString();
            }
            else
            {
                lblMessageSucc.Text = "";
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
            //if (Directory.Exists(SourcePath))
            //{
            //    string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
            //    if (fileList.Length > 0)
            //    {
            //        lnkXLTemplate.Visible = false;
            //    }
            //    else
            //    {
            //        lnkXLTemplate.Visible = true;
            //    }
            //}

            CalculateInvestments(gvInv, "INV");
            FillRents();
            CalculateInvestments(gvRent, "RENT");


            if ((string)Session["sCompID"] == "CO000114")
            {
                SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
            }
            else
            {
                SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
            }
            filesToFind = "RENT_*";
            if (Directory.Exists(SourcePath))
            {
                string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
                if (fileList.Length > 0)
                {
                    lnkDownloadRentSupport.Visible = true;
                }
                else
                {
                    lnkDownloadRentSupport.Visible = false;
                }
            }
            else
            {
                lnkDownloadRentSupport.Visible = false;
            }

            FillTwelve();
            CalculateInvestments(gvtwelve, "12C");

            if ((string)Session["sCompID"] == "CO000114")
            {
                SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
            }
            else
            {
                SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
            }
            filesToFind = "LOAN_*";
            if (Directory.Exists(SourcePath))
            {
                string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
                if (fileList.Length > 0)
                {
                    lnkDownloadLoanSupport.Visible = true;
                }
                else
                {
                    lnkDownloadLoanSupport.Visible = false;
                }
            }
            else
            {
                lnkDownloadLoanSupport.Visible = false;
            }

            if ((string)Session["sCompID"] == "CO000114" || (string)Session["sCompID"] == "CO000125" || (string)Session["sCompID"] == "CO000141")
            {
                FillBTwelve();
                CalculateInvestments(gvTwelB, "12B");
                if (ViewState["CTCTOAMT"] != null)
                {
                    lnk12.Text = "4. Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", ViewState["CTCTOAMT"].ToString()).Replace("रु ", "").Replace(".00", "") + ")";
                }
                tbl12B.Visible = true;


                if ((string)Session["sCompID"] == "CO000114" || (string)Session["sCompID"] == "CO000125")
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                }
                else
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                }
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

            //lnkUpdateOther.Visible = false;
            //lnkUpdate.Visible = false;
            //lnkUpdateRent.Visible = false;
            //lnkUpdate12.Visible = false;
            //lnkUpdate12B.Visible = false;

            btnPrint.Visible = false;
            lnkManual.Visible = false;
            lnkFAQ.Visible = false;
            lnkDownloadDeclarationForms.Visible = false;
            lnkDownloadSelfOccupationDeclaration.Visible = false;

            supportsFlexi.Visible = false;
            filesFlexi.Visible = false;

            if ((string)Session["sCompID"] == "CO000015")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
                trUndertaking.Visible = true;
            }
            else if ((string)Session["sCompID"] == "CO000056")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = true;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
                tbl12BB.Visible = false;
                rentDocs.Visible = false;
                loanDocs.Visible = false;

                supportsFlexi.Visible = true;
                filesFlexi.Visible = true;

                FillSupportDetails();
            }
            else if ((string)Session["sCompID"] == "CO000057")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = true;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
                tbl12BB.Visible = false;
            }
            else if ((string)Session["sCompID"] == "CO000060")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;

                lnkDownloadSelfOccupationDeclaration.Visible = false;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
            }
            else if ((string)Session["sCompID"] == "CO000061")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = false;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
            }
            else if ((string)Session["sCompID"] == "CO000141")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                tbl12B.Visible = true;
                //lnkDownloadSelfOccupationDeclaration.Visible = true;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
            }
            else if ((string)Session["sCompID"] == "CO000078")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = true;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
            }
            else if ((string)Session["sCompID"] == "CO000090")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = true;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
            }
            else if ((string)Session["sCompID"] == "CO000101")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadDeclarationForms.Visible = true;
                lnkDownloadHRADeclaration.Visible = false;
                loanDocs.Visible = false;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
            }
            else if ((string)Session["sCompID"] == "CO000106")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = true;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
            }
            else if ((string)Session["sCompID"] == "CO000108")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = true;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
                tbl12BB.Visible = false;
            }
            else if ((string)Session["sCompID"] == "CO000114")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                instructionsDiv.Visible = true;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
                //tbl12BB.Visible = false;
                div12BB.Visible = false;

            }
            else if ((string)Session["sCompID"] == "CO000122")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = true;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
            }
            else if ((string)Session["sCompID"] == "CO000123")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = true;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
            }
            else if ((string)Session["sCompID"] == "CO000045")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
            }
            else if ((string)Session["sCompID"] == "CO000125")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = false;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
                //FillTax();
                //HidePasswordFields();
            }
            else if ((string)Session["sCompID"] == "CO000126")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = true;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
                tbl12BB.Visible = false;
            }
            else if ((string)Session["sCompID"] == "CO000129") //Deven
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = true;
                lnkDownloadSelfOccupationDeclaration.Visible = false;
                //loanDocs.Visible = true;						
                prevTable.Visible = true;
                DisplayDocumentsPrev();
                tbl12BB.Visible = true;
            }
            else if ((string)Session["sCompID"] == "CO000131")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = false;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
                //HidePasswordFields();
            }
            else if ((string)Session["sCompID"] == "CO000132")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = false;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
                //HidePasswordFields();
            }
            else if ((string)Session["sCompID"] == "CO000134")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = false;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
                //HidePasswordFields();
            }
            else if ((string)Session["sCompID"] == "CO000135")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = false;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
                //HidePasswordFields();
            }
            else if ((string)Session["sCompID"] == "CO000136")
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                lnkDownloadSelfOccupationDeclaration.Visible = true;
                prevTable.Visible = true;
                DisplayDocumentsPrev();
            }
            else
            {
                btnPrint.Visible = true;
                lnkManual.Visible = true;
                lnkFAQ.Visible = false;
                rentDocs.Visible = false;
                loanDocs.Visible = false;
            }

            lnkFAQ.Visible = true;

            if ((string)Session["sCompID"] == "CO000045" || (string)Session["sCompID"] == "CO000056" || (string)Session["sCompID"] == "CO000078" || (string)Session["sCompID"] == "CO000141")
            {
                disclaimer.Visible = false;
            }
            else
            {
                disclaimer.Visible = true;
            }

            DataSet dsRegime = objInv.GetRegime((string)Session["sCompID"], (string)Session["sEmpID"]);
            if (dsRegime.Tables[0].Rows.Count > 0)
            {
                Session["REGIME"] = dsRegime.Tables[0].Rows[0][0].ToString();
                if (dsRegime.Tables[0].Rows[0][0].ToString() == "N")
                {
                    //btnUploadXL.Enabled = false;
                    if ((string)Session["sCompID"] == "CO000114")
                    {
                        btnUploadForm12BB.Enabled = true;
                        btnUploadUndertaking.Enabled = true;
                        btnUploadRentSupport.Enabled = true;
                        btnUploadLoanSupport.Enabled = true;
                        btnUpload12BSupport.Enabled = true;
                    }
                    else if ((string) Session["sCompID"] == "CO000141")
                    {
                        btnUploadForm12BB.Enabled = true;
                        btnUploadUndertaking.Enabled = true;
                        btnUploadRentSupport.Enabled = true;
                        btnUploadLoanSupport.Enabled = true;
                        btnUpload12BSupport.Enabled = true;
                    }
                    else
                    {
                        btnUploadForm12BB.Enabled = false;
                        btnUploadUndertaking.Enabled = false;
                        btnUploadRentSupport.Enabled = false;
                        btnUploadLoanSupport.Enabled = false;
                        btnUpload12BSupport.Enabled = false;
                    }
                    foreach (GridViewRow gvr in gvInv.Rows)
                    {
                        ((TextBox)gvr.FindControl("txtAmt")).ReadOnly = false;
                        ((FileUpload)gvr.FindControl("fupUpload")).Enabled = false;
                        ((ImageButton)gvr.FindControl("btnUpload")).Enabled = false;
                    }
                }
                else
                {
                    //btnUploadXL.Enabled = true;
                    btnUploadForm12BB.Enabled = true;
                    btnUploadUndertaking.Enabled = true;
                    btnUploadRentSupport.Enabled = true;
                    btnUploadLoanSupport.Enabled = true;
                    btnUpload12BSupport.Enabled = true;

                    //foreach (GridViewRow gvr in gvInv.Rows)
                    //{
                    //    ((TextBox)gvr.FindControl("txtAmt")).ReadOnly = false;
                    //    ((FileUpload)gvr.FindControl("fupUpload")).Enabled = true;
                    //    ((ImageButton)gvr.FindControl("btnUpload")).Enabled = true;
                    //}

                    foreach (GridViewRow gvr in gvInv.Rows)
                    {

                        TextBox txtAmt = ((TextBox)gvr.FindControl("txtAmt"));

                        //int perValue = Convert.ToInt32(dsInv.Tables[0].Rows[i]["AMOUNT"].ToString());
                        if (Convert.ToDouble(txtAmt.Text.Trim() == "" ? "0" : txtAmt.Text.Trim()) == 0)
                        {

                            ((FileUpload)gvr.FindControl("fupUpload")).Enabled = false;
                            ((ImageButton)gvr.FindControl("btnUpload")).Enabled = false;
                        }
                        else
                        {

                            ((FileUpload)gvr.FindControl("fupUpload")).Enabled = true;
                            ((ImageButton)gvr.FindControl("btnUpload")).Enabled = true;
                        }
                    }
                }
            }

            if (CheckRentAmount())
            {
                divRentNote.Visible = true;
                divRentUpload.Visible = true;
            }
            else
            {
                divRentNote.Visible = false;
                divRentUpload.Visible = false;
            }

            if (CheckLoanAmount())
            {
                divLoanNote.Visible = true;
                divLoanUpload.Visible = true;
            }
            else
            {
                divLoanNote.Visible = true;
                divLoanUpload.Visible = true;
            }

            if ((string)Session["sCompID"] == "CO000114")
            {
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

            DisplayDocumentsPrevFlexi();

            //if ((string)Session["sCompID"] == "CO000056")
            //{
            //    DisableUpdate();
            //}

            RegisterGridviewUploadControls();
        }

        //private void FillTax()
        //{
        //    RegisterGridviewUploadControls();
        //    lblMessageSucc.Text = "";
        //    if (Session["sCompID"].ToString() == "CO000125")
        //    {
        //        instructionsArcil.Visible = true;
        //        Note.Visible = true;
        //        SelTaxOpt.Visible = true;

        //        dsrg = objTax.Getregime((string)Session["sCompID"], (string)Session["sEmpID"]);
        //        if (dsrg.Tables[0].Rows[0][0].ToString() == "O")
        //        {
        //            rdbOld.Checked = true;
        //        }
        //        else if (dsrg.Tables[0].Rows[0][0].ToString() == "N")
        //        {
        //            rdbNew.Checked = true;
        //        }
        //        else
        //        {
        //            rdbOld.Checked = false;
        //            rdbNew.Checked = false;
        //        }
        //    }

        //}

        private void DisableUpdate()
        {

            btnUploadUndertaking.Enabled = false;
            btnUploadRentSupport.Enabled = false;
            btnUploadLoanSupport.Enabled = false;
            btnUpload12BSupport.Enabled = false;
            btnSubmitAll.Enabled = false;
            btnUploadForm12BB.Enabled = false;

            foreach (GridViewRow gvr in gvInv.Rows)
            {
                ImageButton btnUpload = (ImageButton)gvr.FindControl("btnUpload");
                TextBox txtAmt = (TextBox)gvr.FindControl("txtAmt");
                btnUpload.Enabled = false;
                txtAmt.ReadOnly = true;
            }

            txtLTAAmt.ReadOnly = true;
            txtTelAmt.ReadOnly = true;
            txtFuelAmt.ReadOnly = true;
            txtDriverAmt.ReadOnly = true;

            btnUploadLTA.Enabled = false;
            btnUploadTel.Enabled = false;
            btnUploadFuel.Enabled = false;
            btnUploadDriver.Enabled = false;
        }

        private void HidePasswordFields()
        {
            gvInv.Columns[14].Visible = false;
            txtPasswordRent.Visible = false;
            txtPasswordLoan.Visible = false;
        }

        private void FillInvestments()
        {
            dsInv = objInv.GetInvestmentDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            gvInv.DataSource = dsInv;
            gvInv.DataBind();
        }

        private void Fill_Amounts()
        {
            DataSet ds = new DataSet();
            //ds = objInv.GetAmountDetails((string)Session["sCompID"], (string)Session["sEmpID"], lblInvDetailsLabel, lblRentLabel, lbl12Label, lbl12bLable);            
            objInv.GetAmountDetails((string)Session["sCompID"], (string)Session["sEmpID"], lblInvDetails, lnkRent, lnkRentNew, lnk12, lnk12b);
        }

        private void FillOther()
        {
            dsInv = objInv.GetOtherDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            gvOther.DataSource = dsInv;
            gvOther.DataBind();

            //foreach (GridViewRow gvr in this.gvOther.Rows)
            //{

            //    TextBox txtPAN = (TextBox)gvr.FindControl("txtAmt");
            //    Label headId = (Label)gvr.FindControl("lblId");

            //    if (headId.Text == "OT000003")
            //    {
            //        if ((string)Session["sCompID"] == "CO000015" || (string)Session["sCompID"] == "CO000114")
            //        {
            //            txtPAN.Text = (string)Session["sPAN"];

            //            txtPAN.ReadOnly = true;
            //        }
            //        break;
            //    }
            //}
        }

        private void FillRents()
        {
            dsInv = objInv.GetRentDetails((string)Session["sCompID"], (string)Session["sEmpID"], txtAddress);
            gvRent.DataSource = dsInv;
            gvRent.DataBind();
            dsInv = new DataSet();
            dsInv = objInv.GetLandlordDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
            gvLandlordDetails.DataSource = dsInv;
            gvLandlordDetails.DataBind();
        }

        //private void FillRentsNew()
        //{
        //    dsInv = objInv.GetRentDetailsNew((string)Session["sCompID"], (string)Session["sEmpID"], txtAddressNew);
        //    gvRentNew.DataSource = dsInv;
        //    gvRentNew.DataBind();
        //    //dsInv = new DataSet();
        //    //dsInv = objInv.GetLandlordDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
        //    //gvLandlordDetailsNew.DataSource = dsInv;
        //    //gvLandlordDetailsNew.DataBind();
        //}

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

        

        protected void lnkUpdate_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                //if (Validate() == false)
                //{
                //    return;
                //}
                lblMessageSucc.Text = "";
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
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = "You must agree to the disclaimer before submitting.";
                            objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
                            divAlertSucc.Visible = false;
                            return;
                        }
                    }
                }

                result = objInv.UpdateInvestment(MakeInvXml(gvInv, ""), "", "", (string)Session["sCompID"], (string)Session["sEmpID"]);
                if (result.ToString().Trim() == "success")
                {
                    FillInvestments();
                    CalculateInvestments(gvInv, "INV");

                    divAlertSucc.Visible = true;
                    lblMessageSucc.Visible = true;
                    lblMessageSucc.Text = "Successfuly updated investments.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments.');");
                    divAlert.Visible = false;
                }
                else
                {
                    lblMessageSucc.Text = "Error occurred in application.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                objcommon.Display("Validate", "DisplayErrorMessage('Error occurred in application.');");
                divAlertSucc.Visible = false;
            }
        }

        //private Boolean Validate()
        //{
        //    double amount = 0;
        //    double limit = 0;

        //    foreach (GridViewRow gvr in this.gvInv.Rows)
        //    {

        //        amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
        //        limit = Convert.ToDouble((((Label)gvr.FindControl("lblLimit")).Text == "" || ((Label)gvr.FindControl("lblLimit")).Text == "0.00" ? "0" : ((Label)gvr.FindControl("lblLimit")).Text));

        //        if (limit > 0)
        //        {
        //            if (amount > limit)
        //            {
        //                lblMessageSucc.Text = "Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
        //                objcommon.Display("Validate", "DisplayErrorMessage('Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + "');");
        //                return false;
        //            }
        //        }

        //    }

        //    return true;
        //}

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
                if (txtAmt.Text.Length > 10)
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;                    
                    lblMessage.Text = "Invalid PAN.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Invalid PAN.');");
                    divAlertSucc.Visible = false;
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
            //            //lblMessageSucc.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
            //            objcommon.Display("Validate", "DisplayErrorMessage('Limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is " + ((Label)gvr.FindControl("lblLimit")).Text + "');");
            //            return false;
            //        }
            //    }

            //}

            return true;
        }

        private Boolean ValidateOther()
        {
            string entrytype = "";
            long intValue = 0;
            long entrylength = 0;

            //foreach (GridViewRow gvr in this.gvOther.Rows)
            //{
            //    Label lblId = (Label)gvr.FindControl("lblId");
            //    TextBox txtAmt = (TextBox)gvr.FindControl("txtAmt");
            //    CheckBox chkAgree = (CheckBox)gvr.FindControl("chkAgree");
            //    Label lblDesc = (Label)gvr.FindControl("lblDesc");

            //    if (lblId.Text == "OT999999")
            //    {
            //        if (!chkAgree.Checked)
            //        {
            //            lblMessageSucc.Text = "You must agree to the disclaimer before submitting.";
            //            objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
            //            return false;
            //        }
            //    }
            //}

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
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Value for Number of Children Studying must be a number.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Value for Number of Children Studying must be a number.');");
                        divAlertSucc.Visible = false;
                        return false;
                    }
                }

                if (lblId.Text == "OT000003")
                {
                    if (txtAmt.Text.Trim().Length > 10)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Employee Information - Invalid PAN";
                        objcommon.Display("Validate", "DisplayErrorMessage('Employee Information - Invalid PAN');");
                        divAlertSucc.Visible = false;
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
                                divAlert.Visible = true;
                                lblMessage.Visible = true;
                                lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.";
                                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.');");
                                divAlertSucc.Visible = false;
                                return false;
                            }

                            if (entrylength != ((TextBox)gvr.FindControl("txtAmt")).Text.Trim().Length && entrylength != -1)
                            {
                                divAlert.Visible = true;
                                lblMessage.Visible = true;
                                lblMessage.Text = "Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                                objcommon.Display("Validate", "DisplayErrorMessage('Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                                divAlertSucc.Visible = false;
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = "Error occurred in application.";
                            //objcommon.Display("Validate", "DisplayErrorMessage('Enter data in number for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                            divAlertSucc.Visible = false;
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
            //            lblMessageSucc.Text = "You must agree to the disclaimer before submitting.";
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
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + "');");
                        divAlertSucc.Visible = false;
                        return false;
                    }
                }
                if (((Label)gvr.FindControl("lblId")).Text.Trim() == "F0000003")
                {
                    loan = amount.ToString();
                    if (amount < 0)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Enter positive amount.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Enter positive amount.');");
                        divAlertSucc.Visible = false;
                        return false;
                    }

                    if (((TextBox)gvr.FindControl("txtPropAdd")).Visible == true && propAddress.Trim() == "")
                    {
                        if (loan != "0")
                        {
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = "Enter property address.";
                            objcommon.Display("Validate", "DisplayErrorMessage('Enter property address.');");
                            divAlertSucc.Visible = false;
                            return false;
                        }
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
                                    divAlert.Visible = true;
                                    lblMessage.Visible = true;
                                    lblMessage.Text = "Enter Annual Rent as per Municipal valuation / Annual Fair Rent in that locality / Actual Annual rent received(whichever is higher).";
                                    objcommon.Display("Validate", "DisplayErrorMessage('Enter Annual Rent as per Municipal valuation / Annual Fair Rent in that locality / Actual Annual rent received(whichever is higher).');");
                                    divAlertSucc.Visible = false;
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
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.";
                            objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.');");
                            divAlertSucc.Visible = false;
                            return false;
                        }
                    }

                    if (entrylength != ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length && entrylength != -1)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                        divAlertSucc.Visible = false;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Error occurred in application.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Enter data in number for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                    divAlertSucc.Visible = false;
                    return false;
                }
            }

            return true;
        }

        private Boolean ValidateTwelveForUpload()
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
            //            lblMessageSucc.Text = "You must agree to the disclaimer before submitting.";
            //            objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
            //            return false;
            //        }
            //    }
            //}
            double sum = 0;

            foreach (GridViewRow gvr in this.gvtwelve.Rows)
            {
                amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                limit = Convert.ToDouble((((Label)gvr.FindControl("lblLimit")).Text == "" || ((Label)gvr.FindControl("lblLimit")).Text == "0.00" ? "0" : ((Label)gvr.FindControl("lblLimit")).Text));
                propAddress = ((TextBox)gvr.FindControl("txtPropAdd")).Text.Trim();

                if (((Label)gvr.FindControl("lblId")).Text.Trim() != "F0000050")
                {
                    sum = sum + amount;
                }

                if (limit > 0)
                {
                    if (amount > limit)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + "');");
                        divAlertSucc.Visible = false;
                        return false;
                    }
                }
                if (((Label)gvr.FindControl("lblId")).Text.Trim() == "F0000003")
                {
                    loan = amount.ToString();
                    if (amount < 0)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Enter positive amount.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Enter positive amount.');");
                        divAlertSucc.Visible = false;
                        return false;
                    }
                }
                if (((TextBox)gvr.FindControl("txtPropAdd")).Visible == true && propAddress.Trim() == "")
                {
                    if (loan != "0")
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Enter property address.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Enter property address.');");
                        divAlertSucc.Visible = false;
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
                                    divAlert.Visible = true;
                                    lblMessage.Visible = true;
                                    lblMessage.Text = "Enter Annual Rent as per Municipal valuation / Annual Fair Rent in that locality / Actual Annual rent received(whichever is higher).";
                                    objcommon.Display("Validate", "DisplayErrorMessage('Enter Annual Rent as per Municipal valuation / Annual Fair Rent in that locality / Actual Annual rent received(whichever is higher).');");
                                    divAlertSucc.Visible = false;
                                    return false;
                                }
                            }
                        }
                    }
                }
            }

            if (sum == 0)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Enter amounts in 'Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary' section before uploading documents.";
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString("Enter amounts in 'Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary\' section before uploading documents.") + "');");
                divAlertSucc.Visible = false;
                return false;
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
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.";
                            objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.');");
                            divAlertSucc.Visible = false;
                            return false;
                        }
                    }

                    if (entrylength != ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length && entrylength != -1)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                        divAlertSucc.Visible = false;
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Error occurred in application.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Enter data in number for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                    divAlertSucc.Visible = false;
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
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + "');");
                        divAlertSucc.Visible = false;
                        return false;
                    }
                }

            }

            return true;
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

            //foreach (GridViewRow gvr in this.gvRent.Rows)
            //{
            //    Label lblId = (Label)gvr.FindControl("lblId");
            //    TextBox txtAmt = (TextBox)gvr.FindControl("txtAmt");
            //    CheckBox chkAgree = (CheckBox)gvr.FindControl("chkAgree");
            //    Label lblDesc = (Label)gvr.FindControl("lblDesc");

            //    if (lblId.Text == "PS999999")
            //    {
            //        if (!chkAgree.Checked)
            //        {
            //            lblMessageSucc.Text = "You must agree to the disclaimer before submitting.";
            //            objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
            //            return false;
            //        }
            //    }
            //}

            double maxRent = 0;
            double sumRent = 0;
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
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + "');");
                        divAlertSucc.Visible = false;
                        return false;
                    }
                }

                if (landLimit > 0)
                {
                    if (amount > landLimit && PAN.Trim() == "")
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Please enter PAN of Landlord when rent amount is greater than Landlord limit.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Please enter PAN of Landlord when rent amount is greater than Landlord limit.');");
                        divAlertSucc.Visible = false;
                        return false;
                    }
                }

                if (maxRent < amount)
                {
                    maxRent = amount;
                }
                sumRent += amount;
            }

            if (sumRent > 0)
            {
                if (txtAddress.Text.Trim() == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please enter rented property address.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Please enter rented property address.');");
                    divAlertSucc.Visible = false;
                    return false;
                }
            }

            foreach (GridViewRow gvr in this.gvLandlordDetails.Rows)
            {

                PAN = ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim();
                INV = ((Label)gvr.FindControl("lblId")).Text.Trim();
                DESC = ((Label)gvr.FindControl("lblDesc")).Text.Trim();

                if (INV.Contains("L0000003") || INV.Contains("L0000006"))
                {
                    if (PAN.Length > 0)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        objcommon.Display("Validate", "DisplayErrorMessage('" + DESC + " - Invalid PAN.');");
                        lblMessage.Text = DESC + " - Invalid PAN.";
                        divAlertSucc.Visible = false;
                        return false;
                    }

                    if (INV.Contains("L0000003") && maxRent > 8333)
                    {
                        if (PAN.Trim() == "")
                        {
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = "Please enter First Landlord PAN when rent amount is greater than Landlord limit.";
                            objcommon.Display("Validate", "DisplayErrorMessage('Please enter First Landlord PAN when rent amount is greater than Landlord limit.');");
                            divAlertSucc.Visible = false;
                            return false;
                        }
                    }
                }

                entrytype = ((Label)gvr.FindControl("lblentrytype")).Text;
                entrylength = Convert.ToInt32(((Label)gvr.FindControl("lblentrylen")).Text);

                if (sumRent > 0)
                {
                    try
                    {
                        if (((Label)gvr.FindControl("lblEntryMode")).Text == "C" && ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length == 0)
                        {
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.";
                            objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.');");
                            divAlertSucc.Visible = false;
                            return false;
                        }

                        if (entrylength != ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length && entrylength != -1)
                        {
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = "Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                            objcommon.Display("Validate", "DisplayErrorMessage('Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                            divAlertSucc.Visible = false;
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Enter data in number for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                        divAlertSucc.Visible = false;
                        return false;
                    }
                }
            }

            return true;
        }

        private Boolean CheckRentAmount()
        {
            double sumRent = 0;
            foreach (GridViewRow gvr in this.gvRent.Rows)
            {
                double amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                sumRent += amount;
            }

            if (sumRent > 0)
            {
                return true;
            }

            return false;
        }

        private Boolean CheckLoanAmount()
        {
            if ((string)Session["sCompID"] == "CO000056")
            {
                foreach (GridViewRow gvr in this.gvtwelve.Rows)
                {
                    if (((Label)gvr.FindControl("lblId")).Text == "F0000003" || ((Label)gvr.FindControl("lblId")).Text == "F0000057")
                    {
                        double amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                        if (amount > 0)
                            return true;
                    }
                }
            }
            else
            {
                double sumRent = 0;
                foreach (GridViewRow gvr in this.gvtwelve.Rows)
                {
                    if (((Label)gvr.FindControl("lblId")).Text != "F0000003")
                    {
                        double amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                        sumRent += amount;
                    }
                }

                if (sumRent != 0)
                {
                    return true;
                }
            }

            return false;
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

        private Boolean ValidateRentForUpload()
        {
            double amount = 0;
            double limit = 0;
            double landLimit = 0;
            string PAN = "";
            string INV = "";
            string DESC = "";
            string entrytype = "";
            int entrylength = 0;

            //foreach (GridViewRow gvr in this.gvRent.Rows)
            //{
            //    Label lblId = (Label)gvr.FindControl("lblId");
            //    TextBox txtAmt = (TextBox)gvr.FindControl("txtAmt");
            //    CheckBox chkAgree = (CheckBox)gvr.FindControl("chkAgree");
            //    Label lblDesc = (Label)gvr.FindControl("lblDesc");

            //    if (lblId.Text == "PS999999")
            //    {
            //        if (!chkAgree.Checked)
            //        {
            //            lblMessageSucc.Text = "You must agree to the disclaimer before submitting.";
            //            objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
            //            return false;
            //        }
            //    }
            //}

            double maxRent = 0;
            double sumRent = 0;
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
                        lblMessageSucc.Text = "Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                        objcommon.Display("Validate", "DisplayErrorMessage('Amount cannot be greater than limit for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + "');");
                        lblMessageSucc.BackColor = System.Drawing.Color.Tomato;
                        lblMessageSucc.ForeColor = System.Drawing.Color.Red;
                        return false;
                    }
                }

                if (landLimit > 0)
                {
                    if (amount > landLimit && PAN.Trim() == "")
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Please enter PAN of Landlord when rent amount is greater than Landlord limit.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Please enter PAN of Landlord when rent amount is greater than Landlord limit.');");
                        divAlertSucc.Visible = false;
                        return false;
                    }
                }

                if (maxRent < amount)
                {
                    maxRent = amount;
                }
                sumRent += amount;
            }

            if (sumRent == 0)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Enter amounts in 'Rent Declaration' section before uploading documents.";
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString("Enter amounts in 'Rent Declaration' section before uploading documents.") + "');");
                divAlertSucc.Visible = false;
                return false;
            }

            if (sumRent > 0)
            {
                if (txtAddress.Text.Trim() == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please enter rented property address.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Please enter rented property address.');");
                    divAlertSucc.Visible = false;
                    return false;
                }

                if (txtAddress.Text.Trim() == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please enter rented property address.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Please enter rented property address.');");
                    divAlertSucc.Visible = false;
                    return false;
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
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        objcommon.Display("Validate", "DisplayErrorMessage('" + DESC + " - Invalid PAN.');");
                        lblMessage.Text = DESC + " - Invalid PAN.";
                        divAlertSucc.Visible = false;
                        return false;
                    }

                    if (INV.Contains("L0000003") && maxRent > 8333)
                    {
                        if (PAN.Trim() == "")
                        {
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = "Please enter First Landlord PAN when rent amount is greater than Landlord limit.";
                            objcommon.Display("Validate", "DisplayErrorMessage('Please enter First Landlord PAN when rent amount is greater than Landlord limit.');");
                            divAlertSucc.Visible = false;
                            return false;
                        }
                    }
                }

                entrytype = ((Label)gvr.FindControl("lblentrytype")).Text;
                entrylength = Convert.ToInt32(((Label)gvr.FindControl("lblentrylen")).Text);

                if (sumRent > 0)
                {
                    try
                    {
                        string ENTRY = ((Label)gvr.FindControl("lblEntryMode")).Text;
                        int LEN = ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length;
                        if (((Label)gvr.FindControl("lblEntryMode")).Text == "C" && ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length == 0)
                        {
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.";
                            objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.');");
                            divAlertSucc.Visible = false;
                            return false;
                        }
                        if (INV == "L0000002")
                        {
                            if (((Label)gvr.FindControl("lblEntryMode")).Text == "N" && ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length == 0)
                            {
                                divAlert.Visible = true;
                                lblMessage.Visible = true;
                                lblMessage.Text = objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.";
                                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + " is mandatory.');");
                                divAlertSucc.Visible = false;
                                return false;
                            }
                        }
                        if (entrylength != ((TextBox)gvr.FindControl("txtAmtLnd")).Text.Trim().Length && entrylength != -1)
                        {
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = "Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text);
                            objcommon.Display("Validate", "DisplayErrorMessage('Enter data in proper length for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                            divAlertSucc.Visible = false;
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Enter data in number for " + objcommon.EncodeJsString(((Label)gvr.FindControl("lblDesc")).Text) + ".');");
                        divAlertSucc.Visible = false;
                        return false;
                    }
                }
            }

            return true;
        }

        private string MakeInvXml(GridView GV, string strType)
        {

            StringBuilder sbTaxDetails = new StringBuilder();
            string xmlInv = string.Empty;

            sbTaxDetails.Append("<ROOT>");

            foreach (GridViewRow gvr in GV.Rows)
            {
                //TextBox txamt=(TextBox)gvr.FindControl("txtAmt");
                //string amt = txamt.Text;
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

            return inputString;
        }

        

        //protected void lnkRentNew_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (RentDetailsNew.Visible == false)
        //        {
        //            FillRentsNew();
        //            CalculateInvestments(gvRentNew, "RENT");
        //        }
        //        RentDetailsNew.Visible = (RentDetailsNew.Visible == true ? false : true);
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessageSucc.Text = "Error occurred in application.";
        //        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
        //    }
        //}

        protected void lnkUpdateRent_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                lblMessageSucc.Text = "";
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

                        divAlertSucc.Visible = true;
                        lblMessageSucc.Visible = true;
                        lblMessageSucc.Text = "Successfuly updated rent.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated rent.');");
                        divAlert.Visible = false;
                    }
                    else
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Error occurred in application.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating rent.');");
                        divAlertSucc.Visible = false;
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Error occurred in application.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating rent.');");
                    divAlertSucc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        //protected void lnkUpdateRentNew_Click(object sender, EventArgs e)
        //{
        //    string result;
        //    try
        //    {
        //        if (ValidateRentNew() == false)
        //        {
        //            return;
        //        }
        //        result = objInv.UpdateInvestment(MakeInvXml(gvRentNew, "RentNew"), txtAddressNew.Text.Trim(), "RentNew", (string)Session["sCompID"], (string)Session["sEmpID"]);
        //        if (result.ToString().Trim() == "success")
        //        {
        //            FillRentsNew();
        //            CalculateInvestments(gvRentNew, "RENTNEW");
        //            lblMessageSucc.Text = "Successfuly updated rent.";
        //            objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated rent.');");
        //        }
        //        else
        //        {
        //            lblMessageSucc.Text = "Error occurred in application.";
        //            objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating rent.');");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessageSucc.Text = ex.Message;
        //    }
        //}

        

        protected void lnkUpdate12_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                lblMessageSucc.Text = "";
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

                        divAlertSucc.Visible = true;
                        lblMessageSucc.Visible = true;
                        lblMessageSucc.Text = "Successfuly updated investments.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments.');");
                        divAlert.Visible = false;
                    }
                    else
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                        divAlertSucc.Visible = false;
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Error occurred in application.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                    divAlertSucc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                divAlertSucc.Visible = false;
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

                    if ((string)Session["sCompID"] == "CO000125")
                    {
                        if (lblId.Text == "OT000003")
                        {
                            e.Row.Visible = false;
                        }
                    }

                    if (lblId.Text == "OT000005")
                    {
                        e.Row.Visible = false;
                    }

                    if (lblId.Text == "OT999999")
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
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
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

                    //txtAmt.ReadOnly = (lblRead.Text.Trim() == "1" ? true : false);

                    txtAmt.BackColor = (lblRead.Text.Trim() == "1" ? System.Drawing.Color.LightGray : txtAmt.BackColor);
                    if (Session["sCompID"].ToString() == "CO000129" || Session["sCompID"].ToString() == "CO000108" || Session["sCompID"].ToString() == "CO000060" || Session["sCompID"].ToString() == "CO000061" ||
                    Session["sCompID"].ToString() == "CO000125")
                    {
                        gvtwelve.HeaderRow.Cells[16].Visible = false;
                        gvtwelve.Columns[16].Visible = false;
                    }
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
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        protected void gvInv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtAmt = (TextBox)e.Row.FindControl("txtAmt");
                Label lblAmtPerm = (Label)e.Row.FindControl("lblAmtPerm");
                Label lblAmtPend = (Label)e.Row.FindControl("lblAmtPend");
                Label lblAmtRej = (Label)e.Row.FindControl("lblAmtRej");
                TextBox txtPassword = (TextBox)e.Row.FindControl("txtPassword");
                LinkButton lnkShowDoc = (LinkButton)e.Row.FindControl("lnkShowDoc");
                Label lblDocAddr = (Label)e.Row.FindControl("lblDocAddr");
                ImageButton btnUpload = (ImageButton)e.Row.FindControl("btnUpload");
                FileUpload fupUpload = (FileUpload)e.Row.FindControl("fupUpload");
                Label lblHead = (Label)e.Row.FindControl("lblHead");
                Label lblDesc = (Label)e.Row.FindControl("lblDesc");
                Label lblTooltip = (Label)e.Row.FindControl("lblTooltip");
                Label lblLimit = (Label)e.Row.FindControl("lblLimit");
                Label lblId = (Label)e.Row.FindControl("lblId");
                Label lblMID = (Label)e.Row.FindControl("lblMID");

                smInv.RegisterPostBackControl(btnUpload);
                smInv.RegisterPostBackControl(lnkShowDoc);

                e.Row.ToolTip = lblTooltip.Text;

                CheckBox chkAgree = (CheckBox)e.Row.FindControl("chkAgree");

                lblDesc.Font.Bold = (lblHead.Text.Trim() == "1" ? true : false);

                txtAmt.Visible = (lblHead.Text.Trim() == "0" ? true : false);
                lblAmtPerm.Visible = (lblHead.Text.Trim() == "0" ? true : false);
                lblAmtPend.Visible = (lblHead.Text.Trim() == "0" ? true : false);
                lblAmtRej.Visible = (lblHead.Text.Trim() == "0" ? true : false);
                txtPassword.Visible = (lblHead.Text.Trim() == "0" ? true : false);
                btnUpload.Visible = (lblHead.Text.Trim() == "0" ? true : false);
                fupUpload.Visible = (lblHead.Text.Trim() == "0" ? true : false);

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

                string SourcePath = "";
                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                }
                else
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                }
                string filesToFind = lblMID.Text.Trim() + "_*";
                if (Directory.Exists(SourcePath))
                {
                    string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
                    foreach (string file in fileList)
                    {
                        lnkShowDoc.ToolTip = Path.GetFileName(file);
                        lnkShowDoc.Visible = true;
                        lblDocAddr.Text = file;
                    }
                }

                if (Convert.ToDouble(txtAmt.Text.Trim() == "" ? "0" : txtAmt.Text.Trim()) == 0)
                {
                    fupUpload.Enabled = false;
                    txtPassword.Enabled = false;
                    btnUpload.Enabled = false;

                    lnkShowDoc.Visible = false;
                }
                else
                {
                    txtPassword.Enabled = true;
                    fupUpload.Enabled = true;
                    btnUpload.Enabled = true;
                }

                if (Session["sCompID"].ToString() == "CO000129" || Session["sCompID"].ToString() == "CO000108" || Session["sCompID"].ToString() == "CO000060" || Session["sCompID"].ToString() == "CO000061" ||
                    Session["sCompID"].ToString() == "CO000125")
                {
                    gvInv.HeaderRow.Cells[12].Visible = false;
                    gvInv.Columns[12].Visible = false;
                }


                DataSet dspass = new DataSet();
                dspass = objInv.Getpassword((string)Session["sCompID"], (string)Session["sEmpID"], lblMID.Text);

                if (dspass.Tables.Count > 0)
                {
                    if (txtAmt.Text.Trim() != "0.00")
                    {
                        txtPassword.Text = dspass.Tables[0].Rows[0]["PASSWORDS"].ToString();
                    }
                    else
                    {
                        txtPassword.Text = "";
                    }
                }

                //if (Session["REGIME"] != null && (string)Session["REGIME"] == "N")
                //{
                //    fupUpload.Enabled = false;
                //    txtPassword.Enabled = false;
                //    btnUpload.Enabled = false;

                //    lnkShowDoc.Visible = false;
                //}
            }
        }

        protected void lnkUpdate12B_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                lblMessageSucc.Text = "";
                if (ValidateTwelveB() == false)
                {
                    return;
                }

                result = objInv.UpdateInvestment(MakeInvXml(gvTwelB, ""), "", "", (string)Session["sCompID"], (string)Session["sEmpID"]);
                if (result.ToString().Trim() == "success")
                {
                    FillTwelveB();
                    CalculateInvestments(gvTwelB, "12B");

                    divAlertSucc.Visible = true;
                    lblMessageSucc.Visible = true;
                    lblMessageSucc.Text = "Successfuly updated investments.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments.');");
                    divAlert.Visible = false;
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Error occurred in application.";
                    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                    divAlertSucc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                divAlertSucc.Visible = false;
            }
        }

        

        protected void lnkUpdateOther_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                lblMessageSucc.Text = "";
                if (ValidateOther() == false)
                {
                    return;
                }

                result = objInv.UpdateInvestment(MakeInvXml(gvOther, "Other"), "", "", (string)Session["sCompID"], (string)Session["sEmpID"]);
                if (result.ToString().Trim() == "success")
                {
                    divAlertSucc.Visible = true;
                    lblMessageSucc.Visible = true;
                    FillOther();
                    lblMessageSucc.Text = "Successfuly updated investments.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments.');");
                    divAlert.Visible = false;
                }
                else
                {
                    lblMessageSucc.Text = result;
                    //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                divAlertSucc.Visible = false;
            }
        }

        

        protected void chkSel_CheckedChanged(object sender, EventArgs e)
        {

            try
            {
                RegisterGridviewUploadControls();

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
                lblMessageSucc.Text = "Error occurred in application.";
                lblMessageSucc.BackColor = System.Drawing.Color.Tomato;
                lblMessageSucc.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void chkPan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                RegisterGridviewUploadControls();

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
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        //protected void chkSelNew_CheckedChanged(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        double value = 0;
        //        CheckBox chkSel = (CheckBox)sender;
        //        if (chkSel.Checked == true)
        //        {
        //            foreach (GridViewRow gvr in this.gvRentNew.Rows)
        //            {
        //                if (gvr.RowIndex == 0)
        //                {
        //                    value = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text.Trim() == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
        //                }
        //                else
        //                {
        //                    ((TextBox)gvr.FindControl("txtAmt")).Text = value.ToString();
        //                }

        //            }
        //            CalculateInvestments(gvRentNew, "RENTNEW");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessageSucc.Text = ex.Message;
        //        //lblMessageSucc.Text = "Error occurred in application.";
        //    }
        //}

        //protected void chkPanNew_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string value = "";
        //        CheckBox chkPan = (CheckBox)sender;
        //        if (chkPan.Checked == true)
        //        {
        //            foreach (GridViewRow gvr in this.gvRentNew.Rows)
        //            {
        //                if (gvr.RowIndex == 0)
        //                {
        //                    value = ((TextBox)gvr.FindControl("txtPAN")).Text.Trim();
        //                }
        //                else
        //                {
        //                    ((TextBox)gvr.FindControl("txtPAN")).Text = value.ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessageSucc.Text = ex.Message;
        //        //lblMessageSucc.Text = "Error occurred in application.";
        //    }
        //}

        //protected void chkName_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string value = "";
        //        CheckBox chkName = (CheckBox)sender;
        //        if (chkName.Checked == true)
        //        {
        //            foreach (GridViewRow gvr in this.gvRentNew.Rows)
        //            {
        //                if (gvr.RowIndex == 0)
        //                {
        //                    value = ((TextBox)gvr.FindControl("txtName")).Text.Trim();
        //                }
        //                else
        //                {
        //                    ((TextBox)gvr.FindControl("txtName")).Text = value.ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        lblMessageSucc.Text = "Error occurred in application.";
        //    }
        //}

        //protected void chkAddr_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        string value = "";
        //        CheckBox chkAddr = (CheckBox)sender;
        //        if (chkAddr.Checked == true)
        //        {
        //            foreach (GridViewRow gvr in this.gvRentNew.Rows)
        //            {
        //                if (gvr.RowIndex == 0)
        //                {
        //                    value = ((TextBox)gvr.FindControl("txtAddr")).Text.Trim();
        //                }
        //                else
        //                {
        //                    ((TextBox)gvr.FindControl("txtAddr")).Text = value.ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        lblMessageSucc.Text = "Error occurred in application.";
        //    }
        //}

        private Boolean CalculateInvestments(GridView gv, string strType)
        {

            try
            {
                double CTCAmt = 0;
                double CTCPer = 0;
                double CTCPen = 0;
                double CTCRej = 0;
                double amt = 0;
                double peramt = 0;
                double penamt = 0;
                double rejamt = 0;
                double AmtPerm = 0;
                double AmtPend = 0;
                double AmtRej = 0;
                double TotalAmtPerm = 0;
                double TotalAmtPend = 0;
                double TotalAmtRej = 0;
                int txtAmtValue;

                for (int i = 0; i <= gv.Rows.Count - 1; i++)
                {
                    GridViewRow gvr = gv.Rows[i];
                    //double CTAMT = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text));
                    if (strType == "12C")
                    {
                        if (((Label)gvr.FindControl("lblIsTotal")).Text == "1")
                        {
                            CTCAmt = CTCAmt + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                            CTCPer = CTCPer + Convert.ToDouble((((Label)gvr.FindControl("lblAmtPerm")).Text == "" ? "0" : ((Label)gvr.FindControl("lblAmtPerm")).Text)); //paresh
                            CTCPen = CTCPen + Convert.ToDouble((((Label)gvr.FindControl("lblAmtPend")).Text == "" ? "0" : ((Label)gvr.FindControl("lblAmtPend")).Text)); //paresh
                            CTCRej = CTCRej + Convert.ToDouble((((Label)gvr.FindControl("lblAmtRej")).Text == "" ? "0" : ((Label)gvr.FindControl("lblAmtRej")).Text)); //paresh
                        }

                        //if (((Label)gvr.FindControl("lblIsTotal")).Text == "1" && Convert.ToDouble(((TextBox)gvr.FindControl("txtAmt")).Text) < 0)
                        //{
                        //    CTCAmt = -200000;
                        //}                   
                    }
                    else
                    {
                        CTCAmt = CTCAmt + Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                        CTCPer = CTCPer + Convert.ToDouble((((Label)gvr.FindControl("lblAmtPerm")).Text == "" ? "0" : ((Label)gvr.FindControl("lblAmtPerm")).Text)); //paresh
                        CTCPen = CTCPen + Convert.ToDouble((((Label)gvr.FindControl("lblAmtPend")).Text == "" ? "0" : ((Label)gvr.FindControl("lblAmtPend")).Text)); //paresh
                        CTCRej = CTCRej + Convert.ToDouble((((Label)gvr.FindControl("lblAmtRej")).Text == "" ? "0" : ((Label)gvr.FindControl("lblAmtRej")).Text)); //paresh
                    }

                    if (((Label)gvr.FindControl("lblId")).Text == "F0000003")
                    {
                        amt = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                        peramt = Convert.ToDouble((((Label)gvr.FindControl("lblAmtPerm")).Text == "" ? "0" : ((Label)gvr.FindControl("lblAmtPerm")).Text));
                        penamt = Convert.ToDouble((((Label)gvr.FindControl("lblAmtPend")).Text == "" ? "0" : ((Label)gvr.FindControl("lblAmtPend")).Text));
                        rejamt = Convert.ToDouble((((Label)gvr.FindControl("lblAmtRej")).Text == "" ? "0" : ((Label)gvr.FindControl("lblAmtRej")).Text));

                    }


                    if (((Label)gvr.FindControl("lblId")).Text == "F0000050")
                    {
                        ((TextBox)gvr.FindControl("txtAmt")).Text = (((amt > 200000) ? 200000 : amt) * -1).ToString("0.00");
                        ((Label)gvr.FindControl("lblAmtPerm")).Text = (((peramt > 200000) ? 200000 : peramt) * -1).ToString("0.00");
                        ((Label)gvr.FindControl("lblAmtPend")).Text = (((penamt > 200000) ? 200000 : penamt) * -1).ToString("0.00");
                        ((Label)gvr.FindControl("lblAmtRej")).Text = (((rejamt > 200000) ? 200000 : rejamt) * -1).ToString("0.00");
                        ((Label)gvr.FindControl("lblDesc")).Visible = false;
                        ((Label)gvr.FindControl("lblLimit")).Visible = false;
                        ((Label)gvr.FindControl("lbltxtAmt")).Visible = false;
                        ((TextBox)gvr.FindControl("txtAmt")).Visible = false;
                        ((TextBox)gvr.FindControl("txtPropAdd")).Visible = false;
                        ((Label)gvr.FindControl("lblAmtPerm")).Visible = false;
                        ((Label)gvr.FindControl("lblAmtPend")).Visible = false;
                        ((Label)gvr.FindControl("lblAmtRej")).Visible = false;
                        ((Label)gvr.FindControl("lblRemarks")).Visible = false;
                    }



                    Label lblAmtPerm = (Label)gvr.FindControl("lblAmtPerm");
                    Label lblAmtPend = (Label)gvr.FindControl("lblAmtPend");

                    AmtPerm = Convert.ToDouble((((Label)gvr.FindControl("lblAmtPerm")).Text));
                    AmtPend = Convert.ToDouble((((Label)gvr.FindControl("lblAmtPend")).Text));
                    AmtRej = Convert.ToDouble((((Label)gvr.FindControl("lblAmtRej")).Text));

                    if (AmtPerm.ToString() != "0")
                    {
                        TotalAmtPerm = TotalAmtPerm + AmtPerm;

                    }
                    if (AmtPend.ToString() != "0")
                    {
                        TotalAmtPend = TotalAmtPend + AmtPend;

                    }
                    if (AmtRej.ToString() != "0")
                    {
                        TotalAmtRej = TotalAmtRej + AmtRej;

                    }

                    if (strType == "12C")
                    {
                        if (CTCAmt < -200000)
                        {
                            CTCAmt = -200000;
                        }
                    }

                }

                GridViewRow footerRow = gv.FooterRow;

                if (footerRow != null)
                {

                    Label lblTot = (Label)footerRow.FindControl("lblTot");
                    if (lblTot != null)
                    {
                        lblTot.Text = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "");
                    }
                    else
                    {

                    }

                    Label lblAprAmt = (Label)footerRow.FindControl("lblAprAmt");
                    if (lblAprAmt != null)
                    {
                        lblAprAmt.Text = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", TotalAmtPerm).Replace("रु ", "");
                    }
                    else
                    {

                    }

                    Label lblPndAmt = (Label)footerRow.FindControl("lblPndAmt");
                    if (lblPndAmt != null)
                    {
                        lblPndAmt.Text = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", TotalAmtPend).Replace("रु ", "");
                    }
                    else
                    {

                    }

                    Label lblRjcAmt = (Label)footerRow.FindControl("lblRjcAmt");
                    if (lblRjcAmt != null)
                    {
                        lblRjcAmt.Text = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", TotalAmtRej).Replace("रु ", "");
                    }
                    else
                    {

                    }
                }
                else
                {

                }


                if (strType == "INV")
                {
                    lblInvDetails.Text = "2. Investment Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "") + ")";
                }
                if (strType == "RENT")
                {
                    lnkRent.Text = "3. Rent Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "") + ")";
                }
                //if (strType == "RENTNEW")
                //{
                //    lnkRentNew.Text = "3. Rent Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "") + ")";
                //}
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
                    if (footerRow != null)
                    {

                        Label lblTot = (Label)footerRow.FindControl("lblTot");
                        if (lblTot != null)
                        {
                            lblTot.Text = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "");
                        }
                        else
                        {

                        }

                        Label lblAprAmt = (Label)footerRow.FindControl("lblAprAmt");
                        if (lblAprAmt != null)
                        {
                            lblAprAmt.Text = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "");
                        }
                        else
                        {

                        }

                        Label lblPndAmt = (Label)footerRow.FindControl("lblPndAmt");
                        if (lblPndAmt != null)
                        {
                            lblPndAmt.Text = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "");
                        }
                        else
                        {

                        }

                        Label lblRjcAmt = (Label)footerRow.FindControl("lblRjcAmt");
                        if (lblRjcAmt != null)
                        {
                            lblRjcAmt.Text = String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", CTCAmt).Replace("रु ", "");
                        }
                        else
                        {

                        }
                    }

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


            }



            catch (Exception ex)
            {

            }
            return true;
        }

        protected void txtAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {

                TextBox txtAmt = (TextBox)sender;
                FileUpload fupUpload = (FileUpload)txtAmt.NamingContainer.FindControl("fupUpload");
                TextBox txtPassword = (TextBox)txtAmt.NamingContainer.FindControl("txtPassword");
                ImageButton btnUpload = (ImageButton)txtAmt.NamingContainer.FindControl("btnUpload");

                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                if (Convert.ToDouble(txtAmt.Text.Trim() == "" ? "0" : txtAmt.Text.Trim()) > 0)
                {

                    fupUpload.Enabled = true;
                    txtPassword.Enabled = true;
                    btnUpload.Enabled = true;
                }
                else
                {
                    fupUpload.Enabled = false;
                    txtPassword.Enabled = false;
                    btnUpload.Enabled = false;
                }

                //if (Session["REGIME"] != null && (string)Session["REGIME"] == "N")
                //{
                //    fupUpload.Enabled = false;
                //    txtPassword.Enabled = false;
                //    btnUpload.Enabled = false;
                //}

                CalculateInvestments(gvInv, "INV");
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        protected void txtAmtLnd_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                ValidateTextChanged(sender);
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        protected void txtRentAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();
                lnkDownloadRentSupport.Text = "";
                CalculateInvestments(gvRent, "RENT");

                if (CheckRentAmount())
                {
                    divRentNote.Visible = true;
                    divRentUpload.Visible = true;
                    btnUploadRentSupport.Enabled = true;

                }
                else
                {
                    divRentNote.Visible = false;
                    divRentUpload.Visible = false;
                }
            }
            catch
            {
                lblMessageSucc.Text = "Error occurred in application.";
                lblMessageSucc.BackColor = System.Drawing.Color.Tomato;
                lblMessageSucc.ForeColor = System.Drawing.Color.Red;
            }
        }

        //protected void txtRentAmtNew_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CalculateInvestments(gvRentNew, "RENTNEW");
        //    }
        //    catch
        //    {
        //        lblMessageSucc.Text = "Error occurred in application.";
        //    }
        //}

        protected void txttwelveAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                divLoanNote.Visible = true;
                divLoanUpload.Visible = true;
                RegisterGridviewUploadControls();

                CalculateGridColumn();
                //CalculateSelfPropertyIncome();
                ValidateTwelve();
                lnkDownloadLoanSupport.Text = "";
                CalculateInvestments(gvtwelve, "12C");

                //if (CheckLoanAmount())
                //{
                //    divLoanNote.Visible = true;
                //    divLoanUpload.Visible = true;
                //}
                //else
                //{
                //    divLoanNote.Visible = false;
                //    divLoanUpload.Visible = false;
                //}
            }
            catch
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
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
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                CalculateInvestments(gvTwelB, "12B");

                if (CheckPrevAmount())
                {
                    div12BNote.Visible = true;
                    div12BUpload.Visible = true;

                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Upload previous employer supporting document for income tax.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Upload previous employer supporting document for income tax.');");
                    divAlertSucc.Visible = false;
                }
                else
                {
                    div12BNote.Visible = false;
                    div12BUpload.Visible = false;
                }
            }
            catch
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
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
                    double txtAmt = Convert.ToDouble(txtVal.Text);
                    (ViewState["txtLON"]) = txtAmt;

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

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Print12BB.aspx", false);
            lblMessageSucc.Text = "";
            RegisterGridviewUploadControls();

            smInv.RegisterPostBackControl((LinkButton)sender);

            try
            {
                string compName = "";
                if (Session["sCompAID"] != null)
                {
                    compName = Session["sCompAID"].ToString();
                }
                //string exportOption = "Excel";
                //RenderingExtension extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
                //if (extension != null)
                //{
                //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //    fieldInfo.SetValue(extension, false);
                //}
                //exportOption = "Word";
                //extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
                //if (extension != null)
                //{
                //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //    fieldInfo.SetValue(extension, false);
                //}

                lblMessageSucc.Text = "";

                NewPortal2023.ESS.Investments objBl = new NewPortal2023.ESS.Investments();

                DataSet DsEmp = new DataSet();
                DsTest = objBl.Fill_Report((string)(Session["sEmpID"]), (string)(Session["sCompID"]));

                ReportViewer rptPrint = new ReportViewer();
                if ((string)Session["sCompID"] == "CO000056")
                {
                    ReportDataSource datasource = new ReportDataSource("dsEmp_dtEmpInd", DsTest.Tables[0]);
                    rptPrint.LocalReport.DataSources.Clear();
                    rptPrint.LocalReport.ReportPath = @"ESS\ReportIndiafirst.rdlc";
                    rptPrint.LocalReport.DisplayName = (string)(Session["sEmpCode"]) + "_Form12BB";
                    rptPrint.LocalReport.DataSources.Add(datasource);
                    rptPrint.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                    //rptPrint.LocalReport.Refresh();

                    Warning[] warnings;
                    string[] streamIds;
                    string contentType;
                    string encoding;
                    string ext;

                    //Export the RDLC Report to Byte Array.
                    byte[] bytes = rptPrint.LocalReport.Render("PDF", null, out contentType, out encoding, out ext, out streamIds, out warnings);

                    System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf", bytes);

                    //Download the RDLC Report in Word, Excel, PDF and Image formats.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                    if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                    {
                        System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                    }
                }
                else if ((string)Session["sCompID"] == "CO000060")
                {
                    ReportDataSource datasource = new ReportDataSource("dsEmp_dtEmp", DsTest.Tables[0]);
                    rptPrint.LocalReport.DataSources.Clear();
                    rptPrint.LocalReport.ReportPath = @"ESS\Report12BB.rdlc";
                    rptPrint.LocalReport.DisplayName = (string)(Session["sEmpCode"]) + "_Form12BB";
                    rptPrint.LocalReport.DataSources.Add(datasource);
                    rptPrint.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                    //rptPrint.LocalReport.Refresh();

                    Warning[] warnings;
                    string[] streamIds;
                    string contentType;
                    string encoding;
                    string ext;

                    //Export the RDLC Report to Byte Array.
                    byte[] bytes = rptPrint.LocalReport.Render("PDF", null, out contentType, out encoding, out ext, out streamIds, out warnings);
                    //SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\" + "Documents\\Declaration\\Form12BB";

                    System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + compName + "\\" + "Documents\\Declaration\\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf", bytes);

                    //Download the RDLC Report in Word, Excel, PDF and Image formats.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                    if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                    {
                        System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                    }
                }
                else if ((string)Session["sCompID"] == "CO000114")
                {
                    ReportDataSource datasource = new ReportDataSource("dsEmp_dtEmp", DsTest.Tables[0]);
                    rptPrint.LocalReport.DataSources.Clear();
                    rptPrint.LocalReport.ReportPath = @"ESS\Report12BB.rdlc";
                    rptPrint.LocalReport.DisplayName = (string)(Session["sEmpCode"]) + "_Form12BB";
                    rptPrint.LocalReport.DataSources.Add(datasource);
                    rptPrint.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                    //rptPrint.LocalReport.Refresh();

                    Warning[] warnings;
                    string[] streamIds;
                    string contentType;
                    string encoding;
                    string ext;

                    //Export the RDLC Report to Byte Array.
                    byte[] bytes = rptPrint.LocalReport.Render("PDF", null, out contentType, out encoding, out ext, out streamIds, out warnings);
                    //SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\" + "Documents\\Declaration\\Form12BB";

                    //System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath.ToString() + @"\"  +Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + compName + "\\" + "Documents\\Declaration\\" + (string)(Session["sEmpCode"]) + "\\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf", bytes);
                    //System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath.ToString() + @"\"  +Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + compName + "\\" + "Documents\\Declaration\\" + "\\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf", bytes);
                    System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Declaration\\" + Convert.ToString(Session["sEmpCode"]) + "_Form12BB.pdf", bytes);
                    //Download the RDLC Report in Word, Excel, PDF and Image formats.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                    if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                    {
                        System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                    }
                }
                else
                {
                    ReportDataSource datasource = new ReportDataSource("dsEmp_dtEmp", DsTest.Tables[0]);
                    rptPrint.LocalReport.DataSources.Clear();
                    rptPrint.LocalReport.ReportPath = @"ESS\Report12BB.rdlc";
                    rptPrint.LocalReport.DisplayName = (string)(Session["sEmpCode"]) + "_Form12BB";
                    rptPrint.LocalReport.DataSources.Add(datasource);
                    rptPrint.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                    //rptPrint.LocalReport.Refresh();

                    Warning[] warnings;
                    string[] streamIds;
                    string contentType;
                    string encoding;
                    string ext;

                    //Export the RDLC Report to Byte Array.
                    byte[] bytes = rptPrint.LocalReport.Render("PDF", null, out contentType, out encoding, out ext, out streamIds, out warnings);
                    //SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\" + "Documents\\Declaration\\Form12BB";

                    System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + compName + "\\" + "Documents\\Declaration\\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf", bytes);

                    //Download the RDLC Report in Word, Excel, PDF and Image formats.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                    if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                    {
                        System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                    }
                }
            }
            catch (ThreadAbortException tex)
            {
                if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                {
                    System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                }
            }
            catch (Exception ex)
            {
                objcommon = new NewPortal2023.ESS.Common();

                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = ex.Message;
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString("Error occurred in application.") + "');");
                divAlertSucc.Visible = false;
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

                    if (Session["sCompID"].ToString() == "CO000129" || Session["sCompID"].ToString() == "CO000108" || Session["sCompID"].ToString() == "CO000060" || Session["sCompID"].ToString() == "CO000061" ||
                     Session["sCompID"].ToString() == "CO000125")
                    {
                        gvInv.HeaderRow.Cells[12].Visible = false;
                        gvInv.Columns[12].Visible = false;
                    }
                }
            }
            catch
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString("Error occurred in application.") + "');");
                divAlertSucc.Visible = false;
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
                lblMessageSucc.Text = "Error occurred in application.";
            }
        }

        //protected void btnUploadXL_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (fupInvXL.PostedFile != null)
        //        {
        //            if (Convert.ToString(fupInvXL.PostedFile.FileName) == "")
        //            {
        //                lblMessageSucc.Text = "Browse file to upload.";
        //                return;
        //            }
        //            else
        //            {
        //                //UPLOAD FILE ON SERVER
        //                if (System.IO.Path.GetExtension(fupInvXL.PostedFile.FileName).ToUpper() == ".XLSX")
        //                {
        //                    if (UploadFile() == "success")
        //                    {
        //                        FillOther();
        //                        FillInvestments();
        //                        CalculateInvestments(gvInv, "INV");
        //                        FillRents();
        //                        CalculateInvestments(gvRent, "RENT");
        //                        FillTwelve();
        //                        CalculateInvestments(gvtwelve, "12C");
        //                        if ((string)Session["sCompID"] == "CO000114")
        //                        {
        //                            FillBTwelve();
        //                            CalculateInvestments(gvTwelB, "12B");
        //                            tbl12B.Visible = true;
        //                        }
        //                        lblMessageSucc.Text = "Investment Supports Form uploaded successfully.";
        //                    }
        //                }
        //                else
        //                {
        //                    lblMessageSucc.Text = "File invalid.";
        //                    return;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            lblMessageSucc.Text = "Browse file to upload.";
        //            return;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessageSucc.Text = ex.Message;
        //    }
        //}

        //private string UploadFile()
        //{
        //    //string guid;
        //    string path;

        //    //guid = Convert.ToString(Guid.NewGuid());
        //    //guid = guid + fupInvXL.FileName;
        //    path = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + (string)Session["sCompID"] + "\\Documents\\Declaration\\";
        //    path = path + (string)Session["sEmpCode"] + "_Declaration.xlsx";

        //    System.IO.Stream fileInputStream = fupInvXL.PostedFile.InputStream;
        //    Byte[] fileContent = new Byte[fileInputStream.Length];
        //    fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));

        //    System.IO.FileStream fStream = new System.IO.FileStream(path, System.IO.FileMode.Create);
        //    System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
        //    bWriter.Write((Byte[])fileContent);
        //    fStream.Flush();
        //    bWriter.Flush();
        //    fStream.Close();
        //    bWriter.Close();

        //    return objInv.UploadInvestmentsXL((string)Session["sCompID"], path, (string)Session["sEmpID"]);
        //}

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";

                RegisterGridviewUploadControls();

                string savePath;
                ImageButton btnUpload = (ImageButton)sender;
                TextBox txtAmt = (TextBox)(btnUpload.NamingContainer.FindControl("txtAmt"));
                FileUpload fupUpload = (FileUpload)(btnUpload.NamingContainer.FindControl("fupUpload"));
                LinkButton lnkShowDoc = (LinkButton)(btnUpload.NamingContainer.FindControl("lnkShowDoc"));
                Label lblDocAddr = (Label)(btnUpload.NamingContainer.FindControl("lblDocAddr"));
                TextBox txtPassword = (TextBox)(btnUpload.NamingContainer.FindControl("txtPassword"));
                Label lblMID = (Label)(btnUpload.NamingContainer.FindControl("lblMID"));

                if (fupUpload.PostedFile.FileName == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "UPLOAD FAILURE. Browse document.";
                    objcommon.Display("Validate", "DisplayErrorMessage('UPLOAD FAILURE. Browse document.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                if (Convert.ToDouble(txtAmt.Text.Trim() == "" ? "0" : txtAmt.Text.Trim()) <= 0)
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "UPLOAD FAILURE. Enter amount first to upload documents.";
                    objcommon.Display("Validate", "DisplayErrorMessage('UPLOAD FAILURE. Enter amount first to upload documents.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                //if (fupldDocument.PostedFile.ContentLength > 10485760)
                //{
                //    lblMessageSucc.Text = "File size should be less than 5MB.";
                //    return;
                //}

                //int fCount = Directory.GetFiles(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Documents\\Declaration", Convert.ToString(Session["sEmpCode"]) + "*", SearchOption.AllDirectories).Length;
                //if (fCount < 1)
                //{
                //    lblMessageSucc.Text = "Upload Investment Support Form first before uploading supporting documents.";
                //    return;
                //}

                //if ((string)Session["sCompID"] != "CO000056")
                //{
                //    string pathPDF = "";
                //    string pathZIP = "";
                //    string pathRAR = "";
                //    string path7Z = "";
                //    string pathJPG = "";
                //    string pathJPEG = "";
                //    pathPDF = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf";
                //    pathZIP = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".zip";
                //    pathRAR = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".rar";
                //    path7Z = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".7z";
                //    pathJPG = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpg";
                //    pathJPEG = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpeg";

                //    if (File.Exists(pathPDF) || File.Exists(pathZIP) || File.Exists(pathRAR) || File.Exists(path7Z) || File.Exists(pathJPG) || File.Exists(pathJPEG))
                //    {

                //    }
                //    else
                //    {
                //        lblMessageSucc.Text = "Upload signed Form 12BB before uploading supporting documents.";
                //        return;
                //    }
                //}

                if (System.IO.Path.GetExtension(fupUpload.PostedFile.FileName).ToUpper() == ".JPEG" || System.IO.Path.GetExtension(fupUpload.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupUpload.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupUpload.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupUpload.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupUpload.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Only PDF,JPG/JPEG,7Z,ZIP and RAR files allowed.";
                    divAlertSucc.Visible = false;
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

                System.IO.Stream fileInputStream = fupUpload.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                fileInputStream.Close();
                /* Create Folder */

                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = Path.GetFileNameWithoutExtension(fupUpload.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupUpload.FileName.Trim());

                string filesToDelete = lblMID.Text.Trim() + "_*";
                string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
                foreach (string file in fileList)
                {
                    System.IO.File.Delete(file);
                }

                //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                //{
                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + lblMID.Text.Trim() + "_" + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();
                objSup.Upload_Support_Password((string)Session["sCompID"], (string)Session["sEmpID"], lblMID.Text.Trim() + "_" + fileName, "2021-2022", txtPassword.Text.Trim());
                //DisplayDocuments();

                divAlertSucc.Visible = true;
                lblMessageSucc.Visible = true;
                lblMessageSucc.Text = "File uploaded successfully.";
                objcommon.Display("Validate", "DisplayErrorMessage('File uploaded successfully.');");
                divAlert.Visible = false;
                //FillInvestments();
                //CalculateInvestments(gvInv, "INV");


                string SourcePath = "";
                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                }
                else
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                }

                string filesToFind = lblMID.Text.Trim() + "_*";
                if (Directory.Exists(SourcePath))
                {
                    fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
                    foreach (string file in fileList)
                    {
                        lnkShowDoc.ToolTip = Path.GetFileName(file);
                        lnkShowDoc.Visible = true;
                        lblDocAddr.Text = file;
                    }
                }

                lnkShowDoc.Visible = true;
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;  //"Error occurred in application.";
            }
        }

        protected void btnUploadForm12BB_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";

                RegisterGridviewUploadControls();

                if (fup12BB.PostedFile != null)
                {
                    if (Convert.ToString(fup12BB.PostedFile.FileName) == "")
                    {
                        lblMessageSucc.Text = "Browse file to upload.";
                        return;
                    }
                    else
                    {
                        if (disclaimer.Visible == true)
                        {
                            if (!chkAgree.Checked)
                            {
                                divAlert.Visible = true;
                                lblMessage.Visible = true;
                                lblMessage.Text = "You must agree to the disclaimer before uploading Form 12BB.";
                                objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before uploading Form 12BB.');");
                                divAlertSucc.Visible = false;
                                return;
                            }
                        }

                        UploadFile12BB();
                        FillAll();

                        divAlertSucc.Visible = true;
                        lblMessageSucc.Visible = true;
                        lblMessageSucc.Text = "Form 12BB uploaded successfully.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Form 12BB uploaded successfully.');");
                        divAlert.Visible = false;
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse file to upload.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                    divAlertSucc.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;

            }
        }

        private void UploadFile12BB()
        {
            try
            {
                if (System.IO.Path.GetExtension(fup12BB.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fup12BB.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fup12BB.PostedFile.FileName).ToUpper() == ".RAR" || System.IO.Path.GetExtension(fup12BB.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fup12BB.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fup12BB.PostedFile.FileName).ToUpper() == ".JPEG")
                {
                    string path;
                    string ext = System.IO.Path.GetExtension(fup12BB.FileName);
                    if ((string)Session["sCompID"] == "CO000114")
                    {
                        path = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ext;

                        if (File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf"))
                        {
                            File.Delete(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf");
                        }

                        if (File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".zip"))
                        {
                            File.Delete(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".zip");
                        }

                        if (File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".rar"))
                        {
                            File.Delete(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".rar");
                        }

                        if (File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".7z"))
                        {
                            File.Delete(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".7z");
                        }

                        if (File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpeg"))
                        {
                            File.Delete(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpeg");
                        }

                        if (File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpg"))
                        {
                            File.Delete(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpg");
                        }
                    }
                    else
                    {
                        path = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ext;

                        if (File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf"))
                        {
                            File.Delete(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf");
                        }

                        if (File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".zip"))
                        {
                            File.Delete(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".zip");
                        }

                        if (File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".rar"))
                        {
                            File.Delete(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".rar");
                        }

                        if (File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".7z"))
                        {
                            File.Delete(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".7z");
                        }

                        if (File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpeg"))
                        {
                            File.Delete(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpeg");
                        }

                        if (File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpg"))
                        {
                            File.Delete(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpg");
                        }
                    }


                    string directoryPath = Path.Combine(Request.PhysicalApplicationPath, "PDF Reports", Convert.ToString(Session["sCompID"]), Convert.ToString(Session["sCompAID"]), "Form12BB");
                    Directory.CreateDirectory(directoryPath);

                    path = Path.Combine(directoryPath, Convert.ToString(Session["sEmpCode"]) + ext);

                    System.IO.Stream fileInputStream = fup12BB.PostedFile.InputStream;
                    Byte[] fileContent = new Byte[fileInputStream.Length];
                    fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));

                    System.IO.FileStream fStream = new System.IO.FileStream(path, System.IO.FileMode.Create);
                    System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                    bWriter.Write((Byte[])fileContent);
                    fStream.Flush();
                    bWriter.Flush();
                    fStream.Close();
                    bWriter.Close();

                    objInv.Upload_12BB((string)Session["sCompID"], (string)Session["sEmpID"], Convert.ToString(Session["sEmpCode"]) + ext);
                    SendSuccessMail();

                    divAlertSucc.Visible = true;
                    lblMessageSucc.Visible = true;
                    lblMessageSucc.Text = "Form 12BB uploaded successfully.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Form 12BB uploaded successfully.');");
                    divAlert.Visible = false;
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Only PDF,JPG/JPEG,7Z,ZIP and RAR files can be uploaded.";
                    objcommon.Display("Validate", "Only PDF,JPG/JPEG,7Z,ZIP and RAR files can be uploaded.');");
                    divAlertSucc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        private void SendSuccessMail()
        {
            if (((string)Session["sEmailId"]).Trim() != "")
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(((string)Session["sEmailId"]).Trim());
                mailMessage.From = new MailAddress("reports@sequelgroup.co.in");
                mailMessage.Subject = "Acknowledgement for Investment for F.Y. 2024-2025";
                mailMessage.Body = "Dear Sir/Madam,\n\nForm 12BB for 2024-2025 has been received.\n\nWith Best Regards,\nPayrollservices";

                //Attachment data = new Attachment(
                //             "PATH_TO_YOUR_FILE",
                //             System.Net.Mime.MediaTypeNames.Application.Octet);

                SmtpClient smtpClient = new SmtpClient("trans.briskmailer.com", 587);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new System.Net.NetworkCredential("info@nettechinfo.net", @"/\$3ZKGdPrW6=*_L`/uME=>G");
                //smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);
            }
        }

        protected void btnDownloadForm12BB_Click(object sender, EventArgs e)
        {
            string SourcePath = "";
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                string compName = "";
                if (Session["sCompAID"] != null)
                {
                    compName = Session["sCompAID"].ToString();
                }
                // string SourcePath = "";
                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\" + "Documents\\Declaration";
                }
                else
                {
                    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + compName + "\\Form12BB";
                }

                //if ((string)Session["sCompID"] == "CO000060" || (string)Session["sCompID"] == "CO000061" || (string)Session["sCompID"] == "CO000114")
                //{
                //    //SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + compName + "\\" + "Documents\\Declaration\\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf";
                // SourcePath = "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + compName + "\\" + "Documents\\Declaration"; // + (string)(Session["sEmpCode"]) + "_Form12BB.pdf";
                //}
                //else
                //{
                //    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB";
                //}	
                System.IO.FileInfo fileObj = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf");
                System.IO.FileInfo fileObjZip = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + ".zip");
                System.IO.FileInfo fileObjRar = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + ".rar");
                System.IO.FileInfo fileObj7z = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + ".7z");
                System.IO.FileInfo fileObjJpeg = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + ".jpeg");
                System.IO.FileInfo fileObjJpg = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + ".jpg");

                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + fileObj.Name);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
                else if (fileObjZip.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + fileObjZip.Name);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObjZip.FullName);
                    Response.End();
                }
                else if (fileObjRar.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + fileObjRar.Name);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObjRar.FullName);
                    Response.End();
                }
                else if (fileObj7z.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + fileObj7z.Name);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj7z.FullName);
                    Response.End();
                }
                else if (fileObjJpeg.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + fileObjJpeg.Name);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObjJpeg.FullName);
                    Response.End();
                }
                else if (fileObjJpg.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + fileObjJpg.Name);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObjJpg.FullName);
                    Response.End();
                }
                else
                {
                    lblMessageSucc.Text = "File not found.";
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
                //lblMessageSucc.Text = SourcePath.ToString();
            }
        }

        private void UploadFileRent()
        {
            try
            {
                string savePath;
                if (fupRent.PostedFile.FileName == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse document.";
                    objcommon.Display("Validate", "Browse document.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                //int fCount = Directory.GetFiles(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Documents\\Declaration", Convert.ToString(Session["sEmpCode"]) + "*", SearchOption.AllDirectories).Length;
                //if (fCount < 1)
                //{
                //    lblMessageSucc.Text = "Upload Investment Support Form first before uploading supporting documents.";
                //    return;
                //}

                //if ((string)Session["sCompID"] != "CO000056")
                //{
                //    string pathPDF = "";
                //    string pathZIP = "";
                //    string pathRAR = "";
                //    string path7Z = "";
                //    string pathJPG = "";
                //    string pathJPEG = "";
                //    pathPDF = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf";
                //    pathZIP = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".zip";
                //    pathRAR = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".rar";
                //    path7Z = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".7z";
                //    pathJPG = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpg";
                //    pathJPEG = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpeg";

                //    if (File.Exists(pathPDF) || File.Exists(pathZIP) || File.Exists(pathRAR) || File.Exists(path7Z) || File.Exists(pathJPG) || File.Exists(pathJPEG))
                //    {

                //    }
                //    else
                //    {
                //        lblMessageSucc.Text = "Upload signed Form 12BB before uploading supporting documents.";
                //        return;
                //    }
                //}

                if (System.IO.Path.GetExtension(fupRent.PostedFile.FileName).ToUpper() == ".JPEG" || System.IO.Path.GetExtension(fupRent.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupRent.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupRent.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupRent.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupRent.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Only PDF,JPG/JPEG,7Z,ZIP and RAR files allowed.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Only PDF,JPG/JPEG,7Z,ZIP and RAR files allowed.');");
                    divAlertSucc.Visible = false;
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
                System.IO.Stream fileInputStream = fupRent.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                fileInputStream.Close();
                /* Create Folder */

                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = Path.GetFileNameWithoutExtension(fupRent.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupRent.FileName.Trim());

                string filesToDelete = "RENT_*";
                string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
                foreach (string file in fileList)
                {
                    System.IO.File.Delete(file);
                }

                //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                //{
                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + "RENT_" + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();
                objSup.Upload_Support_Password((string)Session["sCompID"], (string)Session["sEmpID"], "RENT_" + fileName, "2024-2025", txtPasswordRent.Text.Trim());
                //DisplayDocuments();

                divAlertSucc.Visible = true;
                lblMessageSucc.Visible = true;
                lblMessageSucc.Text = "File uploaded successfully.";
                objcommon.Display("Validate", "DisplayErrorMessage('File uploaded successfully.');");
                divAlert.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;  //"Error occurred in application.";
            }
        }

        protected void btnUploadRentSupport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                if (ValidateRentForUpload() == false)
                {
                    return;
                }

                if (fupRent.PostedFile != null)
                {
                    if (Convert.ToString(fupRent.PostedFile.FileName) == "")
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Browse file to upload.";
                        objcommon.Display("Validate", "Browse file to upload.');");
                        divAlertSucc.Visible = false;
                        return;
                    }
                    else
                    {
                        UploadFileRent();
                        lnkDownloadRentSupport.Text = "Download Saved Rent Documents";
                        lnkDownloadRentSupport.Visible = true;
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse file to upload.";
                    objcommon.Display("Validate", "Browse file to upload.');");
                    divAlertSucc.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        protected void lnkDownloadRentSupport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();


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
                    string filesToDelete = "RENT_*";
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
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = "File not found.";
                            objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                            divAlertSucc.Visible = false;
                        }
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "File not found.";
                    objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                    divAlertSucc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        private void UploadFileLoan()
        {
            try
            {
                string savePath;
                if (fupLoan.PostedFile.FileName == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse document.";
                    objcommon.Display("Validate", "Browse document.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                //int fCount = Directory.GetFiles(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Documents\\Declaration", Convert.ToString(Session["sEmpCode"]) + "*", SearchOption.AllDirectories).Length;
                //if (fCount < 1)
                //{
                //    lblMessageSucc.Text = "Upload Investment Support Form first before uploading supporting documents.";
                //    return;
                //}

                //if ((string)Session["sCompID"] != "CO000056")
                //{
                //    string pathPDF = "";
                //    string pathZIP = "";
                //    string pathRAR = "";
                //    string path7Z = "";
                //    string pathJPG = "";
                //    string pathJPEG = "";
                //    pathPDF = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf";
                //    pathZIP = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".zip";
                //    pathRAR = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".rar";
                //    path7Z = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".7z";
                //    pathJPG = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpg";
                //    pathJPEG = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".jpeg";

                //    if (File.Exists(pathPDF) || File.Exists(pathZIP) || File.Exists(pathRAR) || File.Exists(path7Z) || File.Exists(pathJPG) || File.Exists(pathJPEG))
                //    {

                //    }
                //    else
                //    {
                //        lblMessageSucc.Text = "Upload signed Form 12BB before uploading supporting documents.";
                //        return;
                //    }
                //}

                if (System.IO.Path.GetExtension(fupLoan.PostedFile.FileName).ToUpper() == ".JPEG" || System.IO.Path.GetExtension(fupLoan.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupLoan.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupLoan.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupLoan.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupLoan.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Only PDF,JPG/JPEG,7Z,ZIP and RAR files allowed.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Only PDF,JPG/JPEG,7Z,ZIP and RAR files allowed.');");
                    divAlertSucc.Visible = false;
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
                System.IO.Stream fileInputStream = fupLoan.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                fileInputStream.Close();
                /* Create Folder */

                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = Path.GetFileNameWithoutExtension(fupLoan.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupLoan.FileName.Trim());

                string filesToDelete = "LOAN_*";
                string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
                foreach (string file in fileList)
                {
                    System.IO.File.Delete(file);
                }

                //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                //{
                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + "LOAN_" + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();
                objSup.Upload_Support_Password((string)Session["sCompID"], (string)Session["sEmpID"], "LOAN_" + fileName, "2024-2025", txtPasswordLoan.Text.Trim());
                //DisplayDocuments();

                divAlertSucc.Visible = true;
                lblMessageSucc.Visible = true;
                lblMessageSucc.Text = "File uploaded successfully.";
                objcommon.Display("Validate", "DisplayErrorMessage('File uploaded successfully.');");
                divAlert.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;  //"Error occurred in application.";
            }
        }

        protected void btnUploadLoanSupport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                if (ValidateTwelveForUpload() == false)
                {
                    return;
                }

                if (fupLoan.PostedFile != null)
                {
                    if (Convert.ToString(fupLoan.PostedFile.FileName) == "")
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Browse file to upload.";
                        objcommon.Display("Validate", "DisplayErrorMessage ('Browse file to upload.');");
                        divAlertSucc.Visible = false;

                        return;
                    }
                    else
                    {
           
                        UploadFileLoan();
                        lnkDownloadLoanSupport.Text = "Download Saved Support Documents";
                        objcommon.Display("Validate", "DisplayErrorMessage ('Download Saved Support Documents.');");
       
                        lnkDownloadLoanSupport.Visible = true;
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse file to upload.";
                    objcommon.Display("Validate", "DisplayErrorMessage ('Browse file to upload.');");
                    divAlertSucc.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                objcommon.Display("Validate", "DisplayErrorMessage ('Error occurred in application.');");
                divAlertSucc.Visible = false;
            }
        }

        protected void lnkDownloadLoanSupport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();


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
                    string filesToDelete = "LOAN_*";
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
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = "File not found.";
                            divAlertSucc.Visible = false;
                        }
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "File not found.";
                    objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                    divAlertSucc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                objcommon.Display("Validate", "DisplayErrorMessage('Error occurred in application.');");
                divAlertSucc.Visible = false;
            }
        }

        protected void lnkShowDoc_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                LinkButton lnkShowDoc = (LinkButton)sender;
                Label lblDocAddr = (Label)(lnkShowDoc.NamingContainer.FindControl("lblDocAddr"));

                string SourcePath = lblDocAddr.Text.Trim();

                System.IO.FileInfo fileObj = new System.IO.FileInfo(SourcePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + Path.GetFileName(SourcePath));
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "File not found.";
                    objcommon.Display("Validate", "File not found.');");
                    divAlertSucc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        protected void lnkDownloadLoanDeclaration_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                smInv.RegisterPostBackControl((LinkButton)sender);
                RegisterGridviewUploadControls();

                if ((string)Session["sCompID"] == "CO000015")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/issl/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000056")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/indiafirst/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000057")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/orra/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000060")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/asustech/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000061")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/asus/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000141")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/npl/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000078")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/staubli/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000090")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/navitus/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000106")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/omnino/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000108")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/iftas/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000114")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/angel/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000122")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/chocospoon/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000123")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/infinite/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000045")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/sicom/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000125")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/arcil/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000126")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/hrrl/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000129")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/hplng/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000131")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/iiml/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000132")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/ciel/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000134")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/afenpl/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000135")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/ifsf/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }

                else if ((string)Session["sCompID"] == "CO000136") //MADMAN
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/madman/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000078") //STAUBLI
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/staubli/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000090") //NEPL
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/navitus/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000015") //ISSL
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/issl/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000131") //IIML
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/iiml/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000140") //DAIA
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/daia/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000123") //IOSPL
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/infinite/Joint_Housing_Loan_Declaration.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Joint_Housing_Loan_Declaration.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }

            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        protected void lnkDownloadHRADeclaration_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                smInv.RegisterPostBackControl((LinkButton)sender);
                RegisterGridviewUploadControls();

                if ((string)Session["sCompID"] == "CO000015")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/issl/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits_in_Same_City.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits_in_Same_City.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000056")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/indiafirst/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits_in_Same_City.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits_in_Same_City.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000057")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/orra/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000060")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/asustech/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000061")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/asus/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000141")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/npl/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000078")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/staubli/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000090")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/navitus/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000106")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/omnino/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000108")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/iftas/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000114")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/angel/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits_in_Same_City.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits_in_Same_City.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000122")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/chocospoon/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000123")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/infinite/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000045")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/sicom/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000125")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/arcil/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000126")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/hrrl/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000129")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/hplng/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000132")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/ciel/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000134")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/afenpl/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000135")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/ifsf/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }

                else if ((string)Session["sCompID"] == "CO000136") //MADMAN
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/madman/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000078") //STAUBLI
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/staubli/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000090") //NEPL
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/navitus/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000015") //ISSL
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/issl/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000131") //IIML
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/iiml/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000140") //DAIA
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/daia/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000123") //IOSPL
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/infinite/Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_claiming_both_HRA_and_Housing_Loan_benefits.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        protected void lnkFAQ_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                smInv.RegisterPostBackControl((LinkButton)sender);
                RegisterGridviewUploadControls();

                if ((string)Session["sCompID"] == "CO000056")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/indiafirst/FAQ_Reimbursement_and_Investment_Proofs.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=FAQ_Reimbursement_and_Investment_Proofs.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000060")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/asustech/FAQ_Investment_Proofs.pdf"));
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

                else if ((string)Session["sCompID"] == "CO000061")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/asus/FAQ_Investment_Proofs.pdf"));
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

                else if ((string)Session["sCompID"] == "CO000114")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/angel/FAQ_Investment_Proofs.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000125")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/arcil/FAQ_Investment_Proofs.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000141")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/npl/FAQ_Investment_Proofs.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000136") //MADMAN
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/madman/FAQ_Investment_Proofs.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000078") //STAUBLI
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/staubli/FAQ_Investment_Proofs.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000090") //NEPL
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/navitus/FAQ_Investment_Proofs.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000015") //ISSL
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/issl/FAQ_Investment_Proofs.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000131") //IIML
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/iiml/FAQ_Investment_Proofs.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000140") //DAIA
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/daia/FAQ_Investment_Proofs.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000123") //IOSPL
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/infinite/FAQ_Investment_Proofs.pdf"));
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
                else
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/all/FAQ_Investment_Proofs.pdf"));
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
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        protected void lnkManual_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                smInv.RegisterPostBackControl((LinkButton)sender);
                RegisterGridviewUploadControls();

                if ((string)Session["sCompID"] == "CO000057" || (string)Session["sCompID"] == "CO000108")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/all/Investment_Proof_Submission_Procedure_No12BB.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000056")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/indiafirst/Investment_Proof_Submission_Procedure.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000114")
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
                else if ((string)Session["sCompID"] == "CO000125")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/arcil/Investment_Proof_Submission_Procedure_Arcil.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000061") //asusind
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/asus/Investment_Proof_Submission_Procedure.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000060") //asustech
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/asus/Investment_Proof_Submission_Procedure.pdf"));
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
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/npl/Investment_Proof_Submission_Procedure.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000136") //MADMAN
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/madman/Investment_Proof_Submission_Procedure.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000078") //STAUBLI
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/staubli/Investment_Proof_Submission_Procedure.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000090") //NEPL
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/navitus/Investment_Proof_Submission_Procedure.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000015") //ISSL
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/issl/Investment_Proof_Submission_Procedure.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000131") //IIML
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/iiml/Investment_Proof_Submission_Procedure.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000140") //DAIA
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/daia/Investment_Proof_Submission_Procedure.pdf"));
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
                else if ((string)Session["sCompID"] == "CO000123") //IOSPL
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/infinite/Investment_Proof_Submission_Procedure.pdf"));
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
                else
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/all/Investment_Proof_Submission_Procedure.pdf"));
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

            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = "Error occurred in application.";
                lblMessageSucc.BackColor = System.Drawing.Color.Tomato;
                lblMessageSucc.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void lnkDownloadSelfOccupationDeclaration_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                smInv.RegisterPostBackControl((LinkButton)sender);
                RegisterGridviewUploadControls();

                if ((string)Session["sCompID"] == "CO000057")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/orra/Declaration_for_Self_Occupation_of_House_Property.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_Self_Occupation_of_House_Property.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000060")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/asus/Declaration_for_Self_Occupation_of_House_Property.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_Self_Occupation_of_House_Property.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000061")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/asus/Declaration_for_Self_Occupation_of_House_Property.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_Self_Occupation_of_House_Property.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000078")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/staubli/Declaration_for_Self_Occupation_of_House_Property.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_Self_Occupation_of_House_Property.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000106")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/omnino/Declaration_for_Self_Occupation_of_House_Property.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_Self_Occupation_of_House_Property.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000108")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/iftas/Declaration_for_Self_Occupation_of_House_Property.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_Self_Occupation_of_House_Property.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000122")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/chocospoon/Declaration_for_Self_Occupation_of_House_Property.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_Self_Occupation_of_House_Property.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000123")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/infinite/Declaration_for_Self_Occupation_of_House_Property.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_Self_Occupation_of_House_Property.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000125")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/arcil/Declaration_for_Self_Occupation_of_House_Property.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_Self_Occupation_of_House_Property.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000126")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/hrrl/Declaration_for_Self_Occupation_of_House_Property.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_Self_Occupation_of_House_Property.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000090")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/navitus/Declaration_for_Self_Occupation_of_House_Property.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_Self_Occupation_of_House_Property.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else if ((string)Session["sCompID"] == "CO000136")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/madman/Declaration_for_Self_Occupation_of_House_Property.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_for_Self_Occupation_of_House_Property.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        protected void lnkDownloadDeclarationForms_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                smInv.RegisterPostBackControl((LinkButton)sender);
                RegisterGridviewUploadControls();

                if ((string)Session["sCompID"] == "CO000101")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/hincol/Declaration_Forms.zip"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Declaration_Forms.zip");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;

            }
        }

        protected void btnUpload12BSupport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                if (fupPrev.PostedFile != null)
                {
                    if (Convert.ToString(fupPrev.PostedFile.FileName) == "")
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Browse file to upload.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                        divAlertSucc.Visible = false;
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
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse file to upload.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                    divAlertSucc.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        private void UploadFile12B()
        {
            try
            {
                string savePath;
                if (fupPrev.PostedFile.FileName == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse document.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse document.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                //int fCount = Directory.GetFiles(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Documents\\Declaration", Convert.ToString(Session["sEmpCode"]) + "*", SearchOption.AllDirectories).Length;
                //if (fCount < 1)
                //{
                //    lblMessageSucc.Text = "Upload Investment Support Form first before uploading supporting documents.";
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
                //    lblMessageSucc.Text = "Upload signed Form 12BB before uploading supporting documents.";
                //    return;
                //}

                if (System.IO.Path.GetExtension(fupPrev.PostedFile.FileName).ToUpper() == ".JPEG" || System.IO.Path.GetExtension(fupPrev.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupPrev.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupPrev.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupPrev.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupPrev.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Only PDF,JPG/JPEG,7Z,ZIP and RAR files allowed.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Only PDF,JPG/JPEG,7Z,ZIP and RAR files allowed.');");
                    divAlertSucc.Visible = false;
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
                objSup.Upload_Support_Password((string)Session["sCompID"], (string)Session["sEmpID"], "12B_" + fileName, "2024-2025", txtPassword12B.Text.Trim());
                //DisplayDocuments();

                divAlertSucc.Visible = true;
                lblMessageSucc.Visible = true;
                lblMessageSucc.Text = "File uploaded successfully.";
                objcommon.Display("Validate", "DisplayErrorMessage('File uploaded successfully.');");
                divAlert.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;  //"Error occurred in application.";
            }
        }

        protected void lnkDownload12BSupport_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();


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
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = "File not found.";
                            objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                            divAlertSucc.Visible = false;
                        }
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "File not found.";
                    objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                    divAlertSucc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
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

                DataTable dtDocInfo = ((DataTable)ViewState["DocumentsPrev"]);
                string savePath2 = "";
                string savePath3 = "";
                string savePath4 = "";
                string savePath5 = "";
                if ((string)Session["sCompID"] == "CO000114")
                {
                    savePath = Request.PhysicalApplicationPath;
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Archive\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                    savePath2 = Request.PhysicalApplicationPath;
                    savePath2 = savePath2 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Archive2\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                    savePath3 = Request.PhysicalApplicationPath;
                    savePath3 = savePath3 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Archive3\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                    savePath4 = Request.PhysicalApplicationPath;
                    savePath4 = savePath4 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Archive4\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                    savePath5 = Request.PhysicalApplicationPath;
                    savePath5 = savePath5 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Archive5\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                }
                else
                {
                    savePath = Request.PhysicalApplicationPath;
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                    savePath2 = Request.PhysicalApplicationPath;
                    savePath2 = savePath2 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive2\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                    savePath3 = Request.PhysicalApplicationPath;
                    savePath3 = savePath3 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive3\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                    savePath4 = Request.PhysicalApplicationPath;
                    savePath4 = savePath4 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive4\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                    savePath5 = Request.PhysicalApplicationPath;
                    savePath5 = savePath5 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive5\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";

                }

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
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                // objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments.');");
                divAlertSucc.Visible = false;

            }
        }

        protected void lnkBtnOpenFilePrev_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

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
            catch
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                // objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments.');");
                divAlertSucc.Visible = false;
            }
        }

        protected void btnUndertaking_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                smInv.RegisterPostBackControl((LinkButton)sender);
                RegisterGridviewUploadControls();

                if ((string)Session["sCompID"] == "CO000015")
                {
                    System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/issl/Undertaking_for_payments_due_in_March_2021.pdf"));
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", @"attachment; filename=Undertaking_for_payments_due_in_March_2022.pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments.');");
                divAlertSucc.Visible = false;
            }
        }

        protected void btnUploadUndertaking_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                if (fupUndertaking.PostedFile != null)
                {
                    if (Convert.ToString(fupUndertaking.PostedFile.FileName) == "")
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Browse file to upload.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                        divAlertSucc.Visible = false;
                        return;
                    }
                    else
                    {
                        UploadUndertaking();
                        btnDownloadUndertaking.Visible = true;
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse file to upload.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                    divAlertSucc.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        private void UploadUndertaking()
        {
            try
            {
                string savePath;
                if (fupUndertaking.PostedFile.FileName == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse document.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse document.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                //int fCount = Directory.GetFiles(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Documents\\Declaration", Convert.ToString(Session["sEmpCode"]) + "*", SearchOption.AllDirectories).Length;
                //if (fCount < 1)
                //{
                //    lblMessageSucc.Text = "Upload Investment Support Form first before uploading undertaking.";
                //    return;
                //}

                //string pathPDF = "";
                //string pathZIP = "";
                //pathPDF = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf";
                //pathZIP = Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\Form12BB\\" + Convert.ToString(Session["sEmpCode"]) + ".zip";

                //if (File.Exists(pathPDF) || File.Exists(pathZIP))
                //{

                //}
                //else
                //{
                //    lblMessageSucc.Text = "Upload signed Form 12BB before uploading undertaking.";
                //    return;
                //}

                if (System.IO.Path.GetExtension(fupUndertaking.PostedFile.FileName).ToUpper() == ".JPEG" || System.IO.Path.GetExtension(fupUndertaking.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupUndertaking.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupUndertaking.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupUndertaking.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupUndertaking.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Only PDF,JPG/JPEG,7Z,ZIP and RAR files allowed.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Only PDF,JPG/JPEG,7Z,ZIP and RAR files allowed.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                savePath = Request.PhysicalApplicationPath;
                //savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                if ((string)Session["sCompID"] == "CO000114")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                }
                else
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\";
                }
                System.IO.Stream fileInputStream = fupUndertaking.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                fileInputStream.Close();
                /* Create Folder */

                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = Path.GetFileNameWithoutExtension(fupUndertaking.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupUndertaking.FileName.Trim());

                string filesToDelete = "UNDER_*";
                string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
                foreach (string file in fileList)
                {
                    System.IO.File.Delete(file);
                }

                //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                //{
                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + "UNDER_" + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();
                objSup.Upload_Support_Password((string)Session["sCompID"], (string)Session["sEmpID"], "UNDER_" + fileName, "2021-2022", "");
                //DisplayDocuments();

                divAlertSucc.Visible = true;
                lblMessageSucc.Visible = true;
                lblMessageSucc.Text = "File uploaded successfully.";
                objcommon.Display("Validate", "DisplayErrorMessage('File uploaded successfully.');");
                divAlert.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;  //"Error occurred in application.";
            }
        }

        protected void btnDownloadUndertaking_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();


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
                    string filesToDelete = "UNDER_*";
                    string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToDelete);
                    if (fileList.Length > 0)
                    {
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
                                lblMessageSucc.Text = "File not found.";
                                objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                                lblMessageSucc.BackColor = System.Drawing.Color.Tomato;
                                lblMessageSucc.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                    else
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "File not found.";
                        objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                        divAlertSucc.Visible = false;

                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "File not found.";
                    objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                    divAlertSucc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                divAlertSucc.Visible = false;
            }
        }

        protected void btnSubmitAll_Click(object sender, EventArgs e)
        {

            int invtotal = 0;
            int invcount = 0;
            int rentotal = 0;
            int rencount = 0;
            int inttotal = 0;
            int intcount = 0;
            int ltacount = 0;
            int telcount = 0;
            int fuelcount = 0;
            int drivcount = 0;

            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();
                foreach (GridViewRow grv in gvInv.Rows)
                {
                    if (grv.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtAmt = grv.FindControl("txtAmt") as TextBox;
                        Label lbltxtAmt = (Label)grv.FindControl("lbltxtAmt");
                        FileUpload fupUpload = (FileUpload)txtAmt.FindControl("fupUpload");
                        if (lbltxtAmt.Text != txtAmt.Text)
                        {
                            //txtAmt.Text = lbltxtAmt.Text;
                            if (txtAmt.Text != "0" && txtAmt.Text != "" && txtAmt.Text != "0.00")
                            {
                                Label lblDocAddr = (Label)grv.FindControl("lblDocAddr");
                                if (lblDocAddr.Text == "")
                                {
                                    if (grv.RowIndex == grv.RowIndex)
                                    {
                                        grv.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                        txtAmt.Text = lbltxtAmt.Text;
                                    }

                                    invtotal = invcount + 1;
                                }
                            }
                        }
                        else
                        {
                            if (grv.RowIndex == grv.RowIndex)
                            {
                                grv.BackColor = System.Drawing.Color.Transparent;

                            }

                        }
                    }
                }


                foreach (GridViewRow grvRent in gvRent.Rows)
                {
                    if (grvRent.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtAmt = grvRent.FindControl("txtAmt") as TextBox;
                        Label lbltxtAmt = (Label)grvRent.FindControl("lbltxtAmt");
                        //FileUpload fupUpload = (FileUpload)txtAmt.FindControl("fupUpload");
                        if (lbltxtAmt.Text != txtAmt.Text)
                        {
                            //txtAmt.Text = lbltxtAmt.Text;
                            //Label lblDocAddr = (Label)grvRent.FindControl("lblDocAddr");
                            if (txtAmt.Text != "0" && txtAmt.Text != "" && txtAmt.Text != "0.00")
                            {
                                if (lnkDownloadRentSupport.Text == "")
                                {
                                    if (grvRent.RowIndex == grvRent.RowIndex)
                                    {
                                        //grvRent.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                        grvRent.BackColor = System.Drawing.Color.Transparent;
                                        txtAmt.Text = lbltxtAmt.Text;
                                    }

                                    rentotal = rencount + 1;
                                }
                            }
                        }
                        else
                        {
                            if (grvRent.RowIndex == grvRent.RowIndex)
                            {
                                grvRent.BackColor = System.Drawing.Color.Transparent;

                            }

                        }

                    }
                }


                foreach (GridViewRow grvtwelve in gvtwelve.Rows)
                {
                    if (grvtwelve.RowType == DataControlRowType.DataRow)
                    {
                        TextBox txtAmt = grvtwelve.FindControl("txtAmt") as TextBox;
                        Label lbltxtAmt = (Label)grvtwelve.FindControl("lbltxtAmt");
                        //FileUpload fupUpload = (FileUpload)txtAmt.FindControl("fupUpload");
                        if (lbltxtAmt.Text != txtAmt.Text)
                        {
                            //txtAmt.Text = lbltxtAmt.Text;
                            //Label lblDocAddr = (Label)grvRent.FindControl("lblDocAddr");
                            if (txtAmt.Text != "0" && txtAmt.Text != "" && txtAmt.Text != "0.00")
                            {
                                if (lnkDownloadLoanSupport.Text == "")
                                {
                                    if (grvtwelve.RowIndex == grvtwelve.RowIndex)
                                    {
                                        //grvtwelve.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                        grvtwelve.BackColor = System.Drawing.Color.Transparent;
                                        txtAmt.Text = lbltxtAmt.Text;
                                    }

                                    inttotal = intcount + 1;
                                }
                            }
                        }
                        else
                        {
                            if (grvtwelve.RowIndex == grvtwelve.RowIndex)
                            {
                                grvtwelve.BackColor = System.Drawing.Color.Transparent;

                            }

                        }
                    }
                }

                if (Session["sCompID"].ToString() == "CO000056")
                {
                    string taamt = "";
                    if (Session["TAAmt"].ToString() != "" || Session["TAAmt"].ToString() != null)
                    {
                        taamt = Session["TAAmt"].ToString();
                    }

                    string Ltaamount = txtLTAAmt.Text;
                    Session["UpTaAmt"] = Ltaamount;
                    if (taamt != Ltaamount)
                    {
                        // txtLTAAmt.Text = Session["TAAmt"].ToString();
                        if (Ltaamount != "0" && Ltaamount != "" && Ltaamount != "0.00")
                        {

                            if (lnkDownloadLTA.Text == "")
                            {
                                //trLTA.BorderColor = (System.Drawing.Color.SkyBlue).ToString();
                                txtLTAAmt.Text = Session["TAAmt"].ToString();
                                fupLTA.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                txtLTANo.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                txtLTAAmt.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lblLTAAmtVer.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lblLTAAmtRej.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lblLTARemarks.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                btnUploadLTA.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lnkDownloadLTA.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                ltacount = 1;
                            }
                        }
                    }
                    else
                    {
                        fupLTA.BackColor = System.Drawing.Color.Transparent;
                        txtLTANo.BackColor = System.Drawing.Color.Transparent;
                        txtLTAAmt.BackColor = System.Drawing.Color.Transparent;
                        lblLTAAmtVer.BackColor = System.Drawing.Color.Transparent;
                        lblLTAAmtRej.BackColor = System.Drawing.Color.Transparent;
                        lblLTARemarks.BackColor = System.Drawing.Color.Transparent;
                        btnUploadLTA.BackColor = System.Drawing.Color.Transparent;
                        lnkDownloadLTA.BackColor = System.Drawing.Color.Transparent;
                    }

                    string TelAmt = "";
                    if (Session["TelAmt"].ToString() != "" || Session["TelAmt"].ToString() != null)
                    {
                        TelAmt = Session["TelAmt"].ToString();
                    }


                    string TelAmount = txtTelAmt.Text;
                    if (TelAmt != TelAmount)
                    {
                        //txtTelAmt.Text = Session["TelAmt"].ToString();
                        if (TelAmount != "0" && TelAmount != "" && TelAmount != "0.00")
                        {

                            if (lnkDownloadTel.Text == "")
                            {
                                //trLTA.BorderColor = (System.Drawing.Color.SkyBlue).ToString();
                                txtTelAmt.Text = Session["TelAmt"].ToString();
                                fupTel.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                txtTelAmt.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lblTelAmtVer.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lblTelAmtRej.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lblTelRemarks.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                btnUploadTel.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lnkDownloadTel.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");

                                telcount = 1;
                            }
                        }
                    }
                    else
                    {
                        fupTel.BackColor = System.Drawing.Color.Transparent;
                        txtTelAmt.BackColor = System.Drawing.Color.Transparent;
                        lblTelAmtVer.BackColor = System.Drawing.Color.Transparent;
                        lblTelAmtRej.BackColor = System.Drawing.Color.Transparent;
                        lblTelRemarks.BackColor = System.Drawing.Color.Transparent;
                        btnUploadTel.BackColor = System.Drawing.Color.Transparent;
                        lnkDownloadTel.BackColor = System.Drawing.Color.Transparent;

                    }
                    string FuelAmt = "";
                    if (Session["FuelAmt"].ToString() != "" || Session["FuelAmt"].ToString() != null)
                    {
                        FuelAmt = Session["FuelAmt"].ToString();
                    }


                    string FuelAmount = txtFuelAmt.Text;
                    if (FuelAmt != FuelAmount)
                    {
                        //txtFuelAmt.Text = Session["FuelAmt"].ToString();
                        if (FuelAmount != "0" && FuelAmount != "" && FuelAmount != "0.00")
                        {

                            if (lnkDownloadFuel.Text == "")
                            {

                                txtFuelAmt.Text = Session["FuelAmt"].ToString();
                                lblFuelAmtVer.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lblFuelAmtRej.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lblFuelRemarks.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                btnUploadFuel.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lnkDownloadFuel.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                btnUploadTel.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                fupFuel.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");

                                fuelcount = 1;
                            }
                        }
                    }
                    else
                    {
                        txtFuelAmt.BackColor = System.Drawing.Color.Transparent;
                        lblFuelAmtVer.BackColor = System.Drawing.Color.Transparent;
                        lblFuelAmtRej.BackColor = System.Drawing.Color.Transparent;
                        lblFuelRemarks.BackColor = System.Drawing.Color.Transparent;
                        btnUploadFuel.BackColor = System.Drawing.Color.Transparent;
                        lnkDownloadFuel.BackColor = System.Drawing.Color.Transparent;
                        fupFuel.BackColor = System.Drawing.Color.Transparent;

                    }

                    string DriverAmt = "";
                    if (Session["DriverAmt"].ToString() != "" || Session["DriverAmt"].ToString() != null)
                    {
                        DriverAmt = Session["DriverAmt"].ToString();
                    }


                    string DriverAmount = txtDriverAmt.Text;
                    if (DriverAmt != DriverAmount)
                    {
                        //txtDriverAmt.Text = Session["DriverAmt"].ToString();
                        if (DriverAmount != "0" && DriverAmount != "" && DriverAmount != "0.00")
                        {

                            if (lnkDownloadDriver.Text == "")
                            {

                                txtDriverAmt.Text = Session["DriverAmt"].ToString();
                                txtDriverAmt.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lblDriverAmtVer.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lblDriverAmtRej.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lblDriverRemarks.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                fupDriver.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                btnUploadDriver.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");
                                lnkDownloadDriver.BackColor = System.Drawing.ColorTranslator.FromHtml("#A1DCF2");


                                drivcount = 1;
                            }
                        }
                    }
                    else
                    {
                        txtDriverAmt.BackColor = System.Drawing.Color.Transparent;
                        lblDriverAmtVer.BackColor = System.Drawing.Color.Transparent;
                        lblDriverAmtRej.BackColor = System.Drawing.Color.Transparent;
                        lblDriverRemarks.BackColor = System.Drawing.Color.Transparent;
                        fupDriver.BackColor = System.Drawing.Color.Transparent;
                        btnUploadDriver.BackColor = System.Drawing.Color.Transparent;
                        lnkDownloadDriver.BackColor = System.Drawing.Color.Transparent;

                    }
                }
                else
                {
                    ltacount = 0; telcount = 0; fuelcount = 0; drivcount = 0;
                }
                if (invtotal == 0 && rentotal == 0 && inttotal == 0 && ltacount == 0 && telcount == 0 && fuelcount == 0 && drivcount == 0)
                {
                    SubmitAll();
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please First Upload File Then Submit";
                    objcommon.Display("Validate", "DisplayErrorMessage('Please First Upload File Then Submit');");
                    divAlertSucc.Visible = false;
                }

            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        private void SubmitAll()
        {
            string result;
            try
            {
                if (!chkAgreAll.Checked)
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "You must agree to the disclaimer before submitting.";
                    objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                if (ValidateOther() == false)
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
                // result = "success";


                if (result.ToString().Trim() == "success")
                {
                    try
                    {
                        if (result.ToString().Trim() == "success")
                        {
                            if ((string)Session["sCompID"] == "CO000056" || (string)Session["sCompID"] == "CO000126" || (string)Session["sCompID"] == "CO000141")
                            {
                                GenerateAndEmailForm12BB();
                            }
                            else
                            {
                                GenerateForm12BB();
                                divAlertSucc.Visible = true;
                                lblMessageSucc.Visible = true;
                                lblMessageSucc.Text = "Successfuly updated investments details.";                               
                                objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments details.');");
                                Session["Message"] = "Successfuly updated investments details.";
                                divAlert.Visible = false;
                            }

                            divAlertSucc.Visible = true;
                            lblMessageSucc.Visible = true;
                            lblMessageSucc.Text = "Successfuly updated investments details.";
                            objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments details.');");
                            Session["Message"] = "Successfuly updated investments details.";
                            divAlert.Visible = false;
                        }
                        else
                        {
                            divAlert.Visible = true;
                            lblMessage.Visible = true;
                            lblMessage.Text = result;
                            objcommon.Display("Validate", "DisplayErrorMessage('" + result + "');");
                            divAlertSucc.Visible = false;
                        }
                        chkAgreAll.Checked = false;
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

        private bool CheckForm12BB()
        {

            string SourcePath = "";
            if ((string)Session["sCompID"] == "CO000114")
            {
                SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Form12BB";
            }
            else
            {
                SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB";
            }
            System.IO.FileInfo fileObj = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + ".pdf");
            System.IO.FileInfo fileObjZip = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + ".zip");
            System.IO.FileInfo fileObjRar = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + ".rar");
            System.IO.FileInfo fileObj7z = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + ".7z");
            System.IO.FileInfo fileObjJpeg = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + ".jpeg");
            System.IO.FileInfo fileObjJpg = new System.IO.FileInfo(Request.PhysicalApplicationPath.ToString() + SourcePath + "\\" + Convert.ToString(Session["sEmpCode"]) + ".jpg");

            if (fileObj.Exists)
            {
                return true;
            }
            else if (fileObjZip.Exists)
            {
                return true;
            }
            else if (fileObjRar.Exists)
            {
                return true;
            }
            else if (fileObj7z.Exists)
            {
                return true;
            }
            else if (fileObjJpeg.Exists)
            {
                return true;
            }
            else if (fileObjJpg.Exists)
            {
                return true;
            }

            return false;
        }

        //private void ToggleUploadDisplay(bool status)
        //{
        //    if (status)
        //    {
        //        divInvNote.Visible = true;
        //        divRentNote.Visible = true;
        //        divRentUpload.Visible = true;
        //        divLoanNote.Visible = true;
        //        divLoanUpload.Visible = true;
        //        div12BNote.Visible = true;
        //        div12BUpload.Visible = true;
        //    }
        //    else
        //    {
        //        divInvNote.Visible = false;
        //        divRentNote.Visible = false;
        //        divRentUpload.Visible = false;
        //        divLoanNote.Visible = false;
        //        divLoanUpload.Visible = false;
        //        div12BNote.Visible = false;
        //        div12BUpload.Visible = false;
        //    }
        //}

        private void GenerateForm12BB()
        {
            try
            {
                string compName = "";
                if (Session["sCompAID"] != null)
                {
                    compName = Session["sCompAID"].ToString();
                }

                //string exportOption = "Excel";
                //RenderingExtension extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
                //if (extension != null)
                //{
                //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //    fieldInfo.SetValue(extension, false);
                //}
                //exportOption = "Word";
                //extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
                //if (extension != null)
                //{
                //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //    fieldInfo.SetValue(extension, false);
                //}

                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                NewPortal2023.ESS.Investments objBl = new NewPortal2023.ESS.Investments();

                DataSet DsEmp = new DataSet();
                DsTest = objBl.Fill_Report((string)(Session["sEmpID"]), (string)(Session["sCompID"]));

                ReportViewer rptPrint = new ReportViewer();
                if ((string)Session["sCompID"] == "CO000056")
                {
                    ReportDataSource datasource = new ReportDataSource("dsEmp_dtEmpInd", DsTest.Tables[0]);
                    rptPrint.LocalReport.DataSources.Clear();
                    rptPrint.LocalReport.ReportPath = @"ESS\ReportIndiafirst.rdlc";
                    rptPrint.LocalReport.DisplayName = (string)(Session["sEmpCode"]) + "_Form12BB";
                    rptPrint.LocalReport.DataSources.Add(datasource);
                    rptPrint.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                    //rptPrint.LocalReport.Refresh();

                    Warning[] warnings;
                    string[] streamIds;
                    string contentType;
                    string encoding;
                    string ext;

                    //Export the RDLC Report to Byte Array.
                    byte[] bytes = rptPrint.LocalReport.Render("PDF", null, out contentType, out encoding, out ext, out streamIds, out warnings);

                    System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf", bytes);

                    //Download the RDLC Report in Word, Excel, PDF and Image formats.
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = contentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();

                    if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                    {
                        System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                    }
                }
                //else
                //{
                //    if ((string)Session["sCompID"] != "CO000114" && (string)Session["sCompID"] != "CO000060")
                //    {
                //        ReportDataSource datasource = new ReportDataSource("dsEmp_dtEmp", DsTest.Tables[0]);
                //        rptPrint.LocalReport.DataSources.Clear();
                //        rptPrint.LocalReport.ReportPath = @"ESS\Report12BB.rdlc";
                //        rptPrint.LocalReport.DisplayName = (string)(Session["sEmpCode"]) + "_Form12BB";
                //        rptPrint.LocalReport.DataSources.Add(datasource);
                //        rptPrint.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                //        //rptPrint.LocalReport.Refresh();

                //        Warning[] warnings;
                //        string[] streamIds;
                //        string contentType;
                //        string encoding;
                //        string ext;

                //        //Export the RDLC Report to Byte Array.
                //        byte[] bytes = rptPrint.LocalReport.Render("PDF", null, out contentType, out encoding, out ext, out streamIds, out warnings);

                //        System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + compName + "\\Documents\\Declaration\\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf", bytes);

                //        ////Download the RDLC Report in Word, Excel, PDF and Image formats.
                //        Response.Clear();
                //        Response.Buffer = true;
                //        Response.Charset = "";
                //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //        Response.ContentType = contentType;
                //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                //        Response.BinaryWrite(bytes);
                //        Response.Flush();
                //        Response.End();

                //        //if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                //        //{
                //        //    System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                //        //}
                //    }
                //if ((string)Session["sCompID"] == "CO000114")
                //{
                //    ReportDataSource datasource = new ReportDataSource("dsEmp_dtEmp", DsTest.Tables[0]);
                //    rptPrint.LocalReport.DataSources.Clear();
                //    rptPrint.LocalReport.ReportPath = @"ESS\Report12BB.rdlc";
                //    rptPrint.LocalReport.DisplayName = (string)(Session["sEmpCode"]) + "_Form12BB";
                //    rptPrint.LocalReport.DataSources.Add(datasource);
                //    rptPrint.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                //    //rptPrint.LocalReport.Refresh();

                //    Warning[] warnings;
                //    string[] streamIds;
                //    string contentType;
                //    string encoding;
                //    string ext;

                //    //Export the RDLC Report to Byte Array.
                //    byte[] bytes = rptPrint.LocalReport.Render("PDF", null, out contentType, out encoding, out ext, out streamIds, out warnings);

                //    System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + "ANGEL" + "\\Documents\\Declaration\\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf", bytes);

                //    ////Download the RDLC Report in Word, Excel, PDF and Image formats.
                //    Response.Clear();
                //    Response.Buffer = true;
                //    Response.Charset = "";
                //    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //    Response.ContentType = contentType;
                //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                //    Response.BinaryWrite(bytes);
                //    Response.Flush();
                //    Response.End();

                //    //if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                //    //{
                //    //    System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                //    //}
                //}

                // }

            }
            catch (ThreadAbortException tex)
            {
                if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                {
                    System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                }
            }
        }

        private void GenerateAndEmailForm12BB()
        {
            try
            {
                //string exportOption = "Excel";
                //RenderingExtension extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
                //if (extension != null)
                //{
                //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //    fieldInfo.SetValue(extension, false);
                //}
                //exportOption = "Word";
                //extension = rptPrint.LocalReport.ListRenderingExtensions().ToList().Find(x => x.Name.Equals(exportOption, StringComparison.CurrentCultureIgnoreCase));
                //if (extension != null)
                //{
                //    System.Reflection.FieldInfo fieldInfo = extension.GetType().GetField("m_isVisible", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                //    fieldInfo.SetValue(extension, false);
                //}

                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                NewPortal2023.ESS.Investments objBl = new NewPortal2023.ESS.Investments();

                DataSet DsEmp = new DataSet();
                DsTest = objBl.Fill_Report((string)(Session["sEmpID"]), (string)(Session["sCompID"]));

                ReportViewer rptPrint = new ReportViewer();

                if ((string)Session["sCompID"] == "CO000141")
                {
                    ReportDataSource datasource = new ReportDataSource("dsEmp_dtEmp", DsTest.Tables[0]);
                    rptPrint.LocalReport.DataSources.Clear();
                    rptPrint.LocalReport.ReportPath = @"ESS\Report12BB.rdlc";
                    rptPrint.LocalReport.DisplayName = (string)(Session["sEmpCode"]) + "_Form12BB";
                    rptPrint.LocalReport.DataSources.Add(datasource);
                    rptPrint.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                    //rptPrint.LocalReport.Refresh();

                    Warning[] warnings;
                    string[] streamIds;
                    string contentType;
                    string encoding;
                    string ext;

                    //Export the RDLC Report to Byte Array.
                    byte[] bytes = rptPrint.LocalReport.Render("PDF", null, out contentType, out encoding, out ext, out streamIds, out warnings);

                    System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf", bytes);

                    //Email
                    if (((string)Session["sEmailId"]).Trim() != "")
                    {
                        if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                        {
                            string mailheader = ((string)Session["sEmailId"]).Trim();
                            MailMessage mailMessage = new MailMessage();
                            mailMessage.To.Add(((string)Session["sEmailId"]).Trim());
                            //mailMessage.From = new MailAddress("reports@sequelgroup.co.in");
                            mailMessage.From = new MailAddress("npl.employee@naperol.com");
                            mailMessage.Subject = "Acknowledgement for Investment - FY 2024 - 2025";
                            mailMessage.Body = "Dear Sir/Madam,\n\nThis mail is towards acknowledgement of Investments uploaded by you in Investment portal.\n\n"
                                             + "You are requested to review and validate the same with enclosed Form 12BB.\n\nIn case of any changes, you are requested to relogin to the portal and make necessary changes wherever required within timelines.\n\n"
                                             + "Irrespective of multiple submission attempts, your last submitted record shall be considered for final salary processing.\n\n"
                                             + "In case of any queries, please write to chareesh@hrrl.in.\n\nRegards,\nPayroll Services";

                            System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(Request.PhysicalApplicationPath.ToString() + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Form12BB\\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf",
                                                                System.Net.Mime.MediaTypeNames.Application.Octet);
                            mailMessage.Attachments.Add(data);

                            SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
                            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = new System.Net.NetworkCredential("npl.employee@naperol.com", "Muw66004");
                            smtpClient.EnableSsl = true;
                            smtpClient.Send(mailMessage);

                            data.Dispose();
                            mailMessage.Dispose();
                            smtpClient.Dispose();
                        }
                    }

                    //if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                    //{
                    //    System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                    //}
                }
                else
                {
                    ReportDataSource datasource = new ReportDataSource("dsEmp_dtEmp", DsTest.Tables[0]);
                    rptPrint.LocalReport.DataSources.Clear();
                    rptPrint.LocalReport.ReportPath = @"ESS\Report12BB.rdlc";
                    rptPrint.LocalReport.DisplayName = (string)(Session["sEmpCode"]) + "_Form12BB";
                    rptPrint.LocalReport.DataSources.Add(datasource);
                    rptPrint.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(LocalReport_SubreportProcessing);
                    rptPrint.LocalReport.Refresh();

                    Warning[] warnings;
                    string[] streamIds;
                    string contentType;
                    string encoding;
                    string ext;

                    //Export the RDLC Report to Byte Array.
                    byte[] bytes = rptPrint.LocalReport.Render("PDF", null, out contentType, out encoding, out ext, out streamIds, out warnings);

                    //System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf", bytes);
                    System.IO.File.WriteAllBytes(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf", bytes);
                    //Email
                    if (((string)Session["sEmailId"]).Trim() != "")
                    {
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.To.Add(((string)Session["sEmailId"]).Trim());
                        mailMessage.From = new MailAddress("npl.employee@naperol.com");
                        mailMessage.Subject = "Acknowledgement for Investments for F.Y. 2024-2025";
                        mailMessage.Body = "Dear Sir/Madam,\n\n Investments received for 2024-2025.\n\nWith Best Regards,\nPayrollservices";

                        System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf",
                                                                System.Net.Mime.MediaTypeNames.Application.Octet);
                        mailMessage.Attachments.Add(data);

                        SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtpClient.UseDefaultCredentials = false;
                        //smtpClient.Credentials = new System.Net.NetworkCredential("info@nettechinfo.net", @"/\$3ZKGdPrW6=*_L`/uME=>G");
                        smtpClient.Credentials = new System.Net.NetworkCredential("npl.employee@naperol.com", "Muw66004");
                        smtpClient.EnableSsl = true;
                        smtpClient.Send(mailMessage);

                        data.Dispose();
                        mailMessage.Dispose();
                        smtpClient.Dispose();
                    }

                    if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                    {
                        System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                    }
                }
            }
            catch (ThreadAbortException text)
            {
                if (System.IO.File.Exists(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf"))
                {
                    System.IO.File.Delete(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(Session["sEmpCode"]) + "_Form12BB.pdf");
                }
            }
        }

        protected void gvLandlordDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblId = (Label)e.Row.FindControl("lblId");
                Label lblDesc = (Label)e.Row.FindControl("lblDesc");
                Label lblAsterisk = (Label)e.Row.FindControl("lblAsterisk");
                Label lblAsterisk2 = (Label)e.Row.FindControl("lblAsterisk2");
                Label lblDescAsterisk = (Label)e.Row.FindControl("lblDescAsterisk");

                if (lblId.Text == "L0000001" || lblId.Text == "L0000002")
                {
                    lblDescAsterisk.Visible = true;
                    lblAsterisk.Visible = true;
                }
                else
                {

                    lblDescAsterisk.Visible = false;
                    lblAsterisk.Visible = false;
                    lblAsterisk2.Visible = true;

                }
            }
        }

        private bool CheckFileUploads()
        {
            foreach (GridViewRow gvr in this.gvInv.Rows)
            {
                double amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                LinkButton lnkShowDoc = (LinkButton)gvr.FindControl("lnkShowDoc");

                if (amount > 0)
                {
                    if (lnkShowDoc.Visible == false)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Uploading file is mandatory for every investment opted before submitting.";
                        divAlertSucc.Visible = false;
                        objcommon.Display("Validate", "DisplayErrorMessage('Uploading file is mandatory for every investment opted before submitting.');");
                        return false;
                    }
                }
            }

            double sumRent = 0;
            foreach (GridViewRow gvr in this.gvRent.Rows)
            {
                double amount = Convert.ToDouble((((TextBox)gvr.FindControl("txtAmt")).Text == "" ? "0" : ((TextBox)gvr.FindControl("txtAmt")).Text));
                sumRent += amount;
            }

            if (sumRent > 0)
            {
                if (lnkDownloadRentSupport.Visible == false)
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please upload rent documents before submitting.";
                    divAlertSucc.Visible = false;
                    objcommon.Display("Validate", "DisplayErrorMessage('Please upload rent documents before submitting.');");
                    return false;
                }
            }

            if (CheckLoanAmount())
            {
                if (lnkDownloadLoanSupport.Visible == false)
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please upload documents in the 'Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary' section before submitting.";
                    divAlertSucc.Visible = false;
                    objcommon.Display("Validate", "DisplayErrorMessage('Please upload documents in the Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary section before submitting.');");
                    return false;
                }
            }

            if ((string)Session["sCompID"] == "CO000056")
            {
                if (Convert.ToDouble(txtLTAAmt.Text.Trim() == "" ? "0" : txtLTAAmt.Text.Trim()) != 0)
                {
                    if (lnkDownloadLTA.Visible == false)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Please upload LTA supports before submitting.";
                        divAlertSucc.Visible = false;
                        objcommon.Display("Validate", "DisplayErrorMessage('Please upload LTA supports before submitting.');");
                        return false;
                    }
                }

                if (Convert.ToDouble(txtTelAmt.Text.Trim() == "" ? "0" : txtTelAmt.Text.Trim()) != 0)
                {
                    if (lnkDownloadTel.Visible == false)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Please upload telephone supports before submitting.";
                        divAlertSucc.Visible = false;
                        objcommon.Display("Validate", "DisplayErrorMessage('Please upload telephone supports before submitting.');");
                        return false;
                    }
                }

                if (Convert.ToDouble(txtFuelAmt.Text.Trim() == "" ? "0" : txtFuelAmt.Text.Trim()) != 0)
                {
                    if (lnkDownloadFuel.Visible == false)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Please upload fuel supports before submitting.";
                        divAlertSucc.Visible = false;
                        objcommon.Display("Validate", "DisplayErrorMessage('Please upload fuel supports before submitting.');");
                        return false;
                    }
                }

                if (Convert.ToDouble(txtDriverAmt.Text.Trim() == "" ? "0" : txtDriverAmt.Text.Trim()) != 0)
                {
                    if (lnkDownloadDriver.Visible == false)
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Please upload driver documents before submitting.";
                        divAlertSucc.Visible = false;
                        objcommon.Display("Validate", "DisplayErrorMessage('Please upload driver documents before submitting.');");
                        return false;
                    }
                }
            }

            if (CheckPrevAmount())
            {
                if (lnkDownload12BSupport.Visible == false)
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please upload previous employer final settlement proof.";
                    divAlertSucc.Visible = false;
                    objcommon.Display("Validate", "DisplayErrorMessage('Please upload previous employer final settlement proof.');");
                    return false;
                }
            }

            return true;
        }

        //FLEXI SUPPORTS CODE

        protected void btnUploadLTA_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                if (txtLTANo.Text.Trim() == "" || txtLTAAmt.Text.Trim() == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Both No. of Individuals and LTA amount fields are mandatory.";
                    divAlertSucc.Visible = false;
                    objcommon.Display("Validate", "DisplayErrorMessage('Both No. of Individuals and LTA amount fields are mandatory.');");
                    return;
                }

                //if (chkAgree.Checked == false)
                //{
                //    lblMessageSucc.Text = "You must agree to the disclaimer before submitting.";
                //    objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
                //    return;
                //}

                if (fupLTA.PostedFile != null)
                {
                    if (Convert.ToString(fupLTA.PostedFile.FileName) == "")
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Browse file to upload.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                        divAlertSucc.Visible = false;
                        return;
                    }
                    else
                    {
                        UploadFileLTA();
                        FillSupportDetails();
                        lnkDownloadLTA.Text = "View";
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse file to upload.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                    divAlertSucc.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        private void UploadFileLTA()
        {
            try
            {
                string savePath;
                if (fupLTA.PostedFile.FileName == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse document.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse document.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                if (txtLTAAmt.Text.Trim() == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Enter LTA Amount.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Enter LTA Amount.');");
           
                    return;
                }

                if (System.IO.Path.GetExtension(fupLTA.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupLTA.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupLTA.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupLTA.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupLTA.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Only PDF,JPG,7Z,ZIP and RAR files allowed.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Only PDF,JPG,7Z,ZIP and RAR files allowed.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                savePath = Request.PhysicalApplicationPath;

                if ((string)Session["sCompID"] == "CO000114")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
                else
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
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

                //objFlexi.UpdateFlexipaySupport("AD000159", (string)Session["sCompID"], (string)Session["sEmpID"], txtLTAAmt.Text.Trim(), txtLTANo.Text.Trim());
                objSup.Upload_Support_Flexi((string)Session["sCompID"], (string)Session["sEmpID"], "LTA_" + fileName, "2024-2025");
                //DisplayDocuments();

                divAlertSucc.Visible = true;
                lblMessageSucc.Visible = true;
                lblMessageSucc.Text = "File uploaded successfully.";
                objcommon.Display("Validate", "DisplayErrorMessage('File uploaded successfully.');");
                divAlert.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;  //ex.Message;
            }
        }

        protected void btnUploadTel_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                //if (chkAgree.Checked == false)
                //{
                //    lblMessageSucc.Text = "You must agree to the disclaimer before submitting.";
                //    objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
                //    return;
                //}

                if (fupTel.PostedFile != null)
                {
                    if (Convert.ToString(fupTel.PostedFile.FileName) == "")
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Browse file to upload.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                        divAlertSucc.Visible = false;
                        return;
                    }
                    else
                    {
                        UploadFileTel();
                        FillSupportDetails();
                        lnkDownloadTel.Text = "View";
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse file to upload.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                    divAlertSucc.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        private void UploadFileTel()
        {
            try
            {
                string savePath;
                if (fupTel.PostedFile.FileName == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse document.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse document.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                if (txtTelAmt.Text.Trim() == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Enter Telephone Reimbursement Amount.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Enter Telephone Reimbursement Amount.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                if (System.IO.Path.GetExtension(fupTel.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupTel.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupTel.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupTel.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupTel.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Only PDF,JPG,7Z,ZIP and RAR files allowed.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Only PDF,JPG,7Z,ZIP and RAR files allowed.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                savePath = Request.PhysicalApplicationPath;

                if ((string)Session["sCompID"] == "CO000114")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
                else
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
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
                //objFlexi.UpdateFlexipaySupport("AD000207", (string)Session["sCompID"], (string)Session["sEmpID"], txtTelAmt.Text.Trim());
                objSup.Upload_Support_Flexi((string)Session["sCompID"], (string)Session["sEmpID"], "TEL_" + fileName, "2024-2025");
                //DisplayDocuments();

                divAlertSucc.Visible = true;
                lblMessageSucc.Visible = true;
                lblMessageSucc.Text = "File uploaded successfully.";
                objcommon.Display("Validate", "DisplayErrorMessage('File uploaded successfully.');");
                divAlert.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;  //ex.Message;
            }
        }

        protected void btnUploadFuel_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                //if (chkAgree.Checked == false)
                //{
                //    lblMessageSucc.Text = "You must agree to the disclaimer before submitting.";
                //    objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
                //    return;
                //}

                if (fupFuel.PostedFile != null)
                {
                    if (Convert.ToString(fupFuel.PostedFile.FileName) == "")
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Browse file to upload.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                        divAlertSucc.Visible = false;
                        return;
                    }
                    else
                    {
                        UploadFileFuel();
                        FillSupportDetails();
                        lnkDownloadFuel.Text = "View";
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse file to upload.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                    divAlertSucc.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        private void UploadFileFuel()
        {
            try
            {
                string savePath;
                if (fupFuel.PostedFile.FileName == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse document.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse document.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                if (txtFuelAmt.Text.Trim() == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Enter Car Maintenance and Fuel Amount.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Enter Car Maintenance and Fuel Amount.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                if (System.IO.Path.GetExtension(fupFuel.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupFuel.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupFuel.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupFuel.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupFuel.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Only PDF,JPG,7Z,ZIP and RAR files allowed.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Only PDF,JPG,7Z,ZIP and RAR files allowed.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                savePath = Request.PhysicalApplicationPath;

                if ((string)Session["sCompID"] == "CO000114")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
                else
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
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
                //objFlexi.UpdateFlexipaySupport("AD000150", (string)Session["sCompID"], (string)Session["sEmpID"], txtFuelAmt.Text.Trim());
                objSup.Upload_Support_Flexi((string)Session["sCompID"], (string)Session["sEmpID"], "FUEL_" + fileName, "2024-2025");
                //DisplayDocuments();

                divAlertSucc.Visible = true;
                lblMessageSucc.Visible = true;
                lblMessageSucc.Text = "File uploaded successfully.";
                objcommon.Display("Validate", "DisplayErrorMessage('File uploaded successfully.');");
                divAlert.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;  //ex.Message;
            }
        }

        protected void btnUploadDriver_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                RegisterGridviewUploadControls();

                //if (chkAgree.Checked == false)
                //{
                //    lblMessageSucc.Text = "You must agree to the disclaimer before submitting.";
                //    objcommon.Display("Validate", "DisplayErrorMessage('You must agree to the disclaimer before submitting.');");
                //    return;
                //}

                if (fupDriver.PostedFile != null)
                {
                    if (Convert.ToString(fupDriver.PostedFile.FileName) == "")
                    {
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Browse file to upload.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                        divAlertSucc.Visible = false;
                        return;
                    }
                    else
                    {
                        UploadFileDriver();
                        FillSupportDetails();
                        lnkDownloadDriver.Text = "View";
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse file to upload.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse file to upload.');");
                    divAlertSucc.Visible = false;
                    return;
                }
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        private void UploadFileDriver()
        {
            try
            {
                string savePath;
                if (fupDriver.PostedFile.FileName == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Browse document.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Browse document.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                if (txtDriverAmt.Text.Trim() == "")
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Enter Driver Salary Amount.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Enter Driver Salary Amount.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                if (System.IO.Path.GetExtension(fupDriver.PostedFile.FileName).ToUpper() == ".7Z" || System.IO.Path.GetExtension(fupDriver.PostedFile.FileName).ToUpper() == ".PDF" || System.IO.Path.GetExtension(fupDriver.PostedFile.FileName).ToUpper() == ".JPG" || System.IO.Path.GetExtension(fupDriver.PostedFile.FileName).ToUpper() == ".ZIP" || System.IO.Path.GetExtension(fupDriver.PostedFile.FileName).ToUpper() == ".RAR")
                {

                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Only PDF,JPG,7Z,ZIP and RAR files allowed.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Only PDF,JPG,7Z,ZIP and RAR files allowed.');");
                    divAlertSucc.Visible = false;
                    return;
                }

                savePath = Request.PhysicalApplicationPath;
                if ((string)Session["sCompID"] == "CO000114")
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
                else
                {
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
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
                //objFlexi.UpdateFlexipaySupport("AD000153", (string)Session["sCompID"], (string)Session["sEmpID"], txtDriverAmt.Text.Trim());
                objSup.Upload_Support_Flexi((string)Session["sCompID"], (string)Session["sEmpID"], "DRV_" + fileName, "2024-2025");
                //DisplayDocuments();

                divAlertSucc.Visible = true;
                lblMessageSucc.Visible = true;
                lblMessageSucc.Text = "File uploaded successfully.";
                objcommon.Display("Validate", "DisplayErrorMessage('File uploaded successfully.');");
                divAlert.Visible = false;
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;  //ex.Message;
            }
        }

        protected void lnkDownloadLTA_Click(object sender, EventArgs e)
        {
            try
            {
                RegisterGridviewUploadControls();
                string SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

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
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "File not found.";
                        objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                        divAlertSucc.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        protected void lnkDownloadTel_Click(object sender, EventArgs e)
        {
            try
            {
                RegisterGridviewUploadControls();
                string SourcePath = "";
                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
                else
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
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
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "File not found.";
                        objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                        divAlertSucc.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        protected void lnkDownloadFuel_Click(object sender, EventArgs e)
        {
            try
            {
                RegisterGridviewUploadControls();
                string SourcePath = "";
                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
                else
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
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
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "File not found.";
                        objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                        divAlertSucc.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        protected void lnkDownloadDriver_Click(object sender, EventArgs e)
        {
            try
            {
                RegisterGridviewUploadControls();
                string SourcePath = "";
                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
                else
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
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
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "File not found.";
                        objcommon.Display("Validate", "DisplayErrorMessage('File not found.');");
                        divAlertSucc.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        private void CreateDocumentsStructurePrevFlexi()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["DocumentsPrev"] = dtDocuments;
                gvPrevFilesFlexi.DataSource = dtDocuments;
                gvPrevFilesFlexi.DataBind();
            }
        }

        private void DisplayDocumentsPrevFlexi()
        {
            try
            {
                string savePath2 = "";
                string savePath3 = "";
                string savePath4 = "";
                string savePath5 = "";
                CreateDocumentsStructurePrevFlexi();
                if ((string)Session["sCompID"] == "CO000114")
                {
                    savePath = Request.PhysicalApplicationPath;
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Archive\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                    savePath2 = Request.PhysicalApplicationPath;
                    savePath2 = savePath2 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Archive2\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                    savePath3 = Request.PhysicalApplicationPath;
                    savePath3 = savePath3 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Archive3\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                    savePath4 = Request.PhysicalApplicationPath;
                    savePath4 = savePath4 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Archive4\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                    savePath5 = Request.PhysicalApplicationPath;
                    savePath5 = savePath5 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Archive5\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                }
                else
                {
                    savePath = Request.PhysicalApplicationPath;
                    savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                    savePath2 = Request.PhysicalApplicationPath;
                    savePath2 = savePath2 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive2\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                    savePath3 = Request.PhysicalApplicationPath;
                    savePath3 = savePath3 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive3\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                    savePath4 = Request.PhysicalApplicationPath;
                    savePath4 = savePath4 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive4\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                    savePath5 = Request.PhysicalApplicationPath;
                    savePath5 = savePath5 + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Archive5\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";

                }

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

                this.gvPrevFilesFlexi.DataSource = dtDocInfo;
                this.gvPrevFilesFlexi.DataBind();

                foreach (GridViewRow gr in gvPrevFilesFlexi.Rows)
                {
                    LinkButton b = gr.FindControl("lnkBtnOpenFile") as LinkButton;
                    this.smInv.RegisterPostBackControl(b);
                }
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        protected void lnkBtnOpenFilePrevFlexi_Click(object sender, EventArgs e)
        {
            try
            {
                RegisterGridviewUploadControls();
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
                lblMessageSucc.Text = ex.Message;
            }
        }

        protected void lnkForms_Click(object sender, EventArgs e)
        {
            try
            {
                RegisterGridviewUploadControls();
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
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                objcommon.Display("Validate", "DisplayErrorMessage('Error occurred in application.');");
                divAlertSucc.Visible = false;
            }
        }

        protected void lnkFAQLTA_Click(object sender, EventArgs e)
        {
            try
            {
                RegisterGridviewUploadControls();
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
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                objcommon.Display("Validate", "DisplayErrorMessage('Error occurred in application.');");
                divAlertSucc.Visible = false;
            }
        }

        private void FillSupportDetails()
        {
            try
            {
                objFlexi.GetSupportDetails((string)Session["sCompID"], (string)Session["sEmpID"], txtLTANo, txtLTAAmt, txtTelAmt, txtFuelAmt, txtDriverAmt,
                                                                                                lblLTAAmtVer, lblTelAmtVer, lblFuelAmtVer, lblDriverAmtVer,
                                                                                                lblLTAAmtRej, lblTelAmtRej, lblFuelAmtRej, lblDriverAmtRej,
                                                                                                lblLTARemarks, lblTelRemarks, lblFuelRemarks, lblDriverRemarks);
                Session["TAAmt"] = txtLTAAmt.Text;
                Session["TelAmt"] = txtTelAmt.Text;
                Session["FuelAmt"] = txtFuelAmt.Text;
                Session["DriverAmt"] = txtDriverAmt.Text;
                //string amt = Session["TAAmt"].ToString();
                //string Amtamt = Session["LTAAmount"].ToString();


                if (Session["TAAmt"].ToString() != "")
                {
                    if (ViewState["LTAAmount"] != null)
                    {
                        if (Session["TAAmt"] == ViewState["LTAAmount"])
                        {
                            txtLTAAmt.Text = Session["TAAmt"].ToString();
                        }
                        else
                        {
                            txtLTAAmt.Text = ViewState["LTAAmount"].ToString();
                        }
                    }
                    else
                    {
                        Session["TAAmt"] = txtLTAAmt.Text;
                    }
                }
                else
                {
                    Session["TAAmt"] = txtLTAAmt.Text;
                }

                if (Session["TelAmt"].ToString() != "")
                {
                    if (ViewState["TelAmount"] != null)
                    {
                        if (Session["TelAmt"] == ViewState["TelAmount"])
                        {
                            txtTelAmt.Text = Session["TelAmt"].ToString();
                        }
                        else
                        {
                            txtTelAmt.Text = ViewState["TelAmount"].ToString();
                        }
                    }
                    else
                    {
                        Session["TelAmt"] = txtTelAmt.Text;
                    }
                }
                else
                {
                    Session["TelAmt"] = txtTelAmt.Text;
                }

                if (Session["FuelAmt"].ToString() != "")
                {
                    if (ViewState["FuelAmount"] != null)
                    {
                        if (Session["FuelAmt"] == ViewState["FuelAmount"])
                        {
                            txtFuelAmt.Text = Session["FuelAmt"].ToString();
                        }
                        else
                        {
                            txtFuelAmt.Text = ViewState["FuelAmount"].ToString();
                        }
                    }
                    else
                    {
                        Session["FuelAmt"] = txtFuelAmt.Text;
                    }
                }
                else
                {
                    Session["FuelAmt"] = txtFuelAmt.Text;
                }
                if (Session["DriverAmt"].ToString() != "")
                {
                    if (ViewState["DriverAmount"] != null)
                    {
                        if (Session["DriverAmt"] == ViewState["DriverAmount"])
                        {
                            txtDriverAmt.Text = Session["DriverAmt"].ToString();
                        }
                        else
                        {
                            txtDriverAmt.Text = ViewState["DriverAmount"].ToString();
                        }
                    }
                    else
                    {
                        Session["DriverAmt"] = txtDriverAmt.Text;
                    }
                }
                else
                {
                    Session["DriverAmt"] = txtDriverAmt.Text;
                }




                string SourcePath = "";
                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }
                else
                {
                    SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\FLEXI\\";
                }


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

                if (Convert.ToDouble(txtLTAAmt.Text.Trim() == "" ? "0" : txtLTAAmt.Text.Trim()) == 0)
                {
                    fupLTA.Enabled = false;
                    btnUploadLTA.Enabled = false;
                    lnkDownloadLTA.Enabled = false;
                }
                else
                {
                    fupLTA.Enabled = true;
                    btnUploadLTA.Enabled = true;
                    lnkDownloadLTA.Enabled = true;
                }

                if (Convert.ToDouble(txtTelAmt.Text.Trim() == "" ? "0" : txtTelAmt.Text.Trim()) == 0)
                {
                    fupTel.Enabled = false;
                    btnUploadTel.Enabled = false;
                    lnkDownloadTel.Enabled = false;
                }
                else
                {
                    fupTel.Enabled = true;
                    btnUploadTel.Enabled = true;
                    lnkDownloadTel.Enabled = true;
                }

                if (Convert.ToDouble(txtFuelAmt.Text.Trim() == "" ? "0" : txtFuelAmt.Text.Trim()) == 0)
                {
                    fupFuel.Enabled = false;
                    btnUploadFuel.Enabled = false;
                    lnkDownloadFuel.Enabled = false;
                }
                else
                {
                    fupFuel.Enabled = true;
                    btnUploadFuel.Enabled = true;
                    lnkDownloadFuel.Enabled = true;
                }

                if (Convert.ToDouble(txtDriverAmt.Text.Trim() == "" ? "0" : txtDriverAmt.Text.Trim()) == 0)
                {
                    fupDriver.Enabled = false;
                    btnUploadDriver.Enabled = false;
                    lnkDownloadDriver.Enabled = false;
                }
                else
                {
                    fupDriver.Enabled = true;
                    btnUploadDriver.Enabled = true;
                    lnkDownloadDriver.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
            }
        }

        protected void txtLTAAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["LTAAmount"] = txtLTAAmt.Text;
                RegisterGridviewUploadControls();

                if (Convert.ToDouble(txtLTAAmt.Text.Trim() == "" ? "0" : txtLTAAmt.Text.Trim()) == 0)
                {
                    fupLTA.Enabled = false;
                    btnUploadLTA.Enabled = false;
                }
                else
                {
                    fupLTA.Enabled = true;
                    btnUploadLTA.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Error occurred in application.');");
                divAlertSucc.Visible = false;
            }
        }

        protected void txtTelAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["TelAmount"] = txtTelAmt.Text;
                RegisterGridviewUploadControls();

                if (Convert.ToDouble(txtTelAmt.Text.Trim() == "" ? "0" : txtTelAmt.Text.Trim()) == 0)
                {
                    fupTel.Enabled = false;
                    btnUploadTel.Enabled = false;
                }
                else
                {
                    fupTel.Enabled = true;
                    btnUploadTel.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Successfuly updated investments.');");
                divAlertSucc.Visible = false;
            }
        }

        protected void txtFuelAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["FuelAmount"] = txtFuelAmt.Text;
                RegisterGridviewUploadControls();

                if (Convert.ToDouble(txtFuelAmt.Text.Trim() == "" ? "0" : txtFuelAmt.Text.Trim()) == 0)
                {
                    fupFuel.Enabled = false;
                    btnUploadFuel.Enabled = false;
                }
                else
                {
                    fupFuel.Enabled = true;
                    btnUploadFuel.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        protected void txtDriverAmt_TextChanged(object sender, EventArgs e)
        {
            try
            {
                ViewState["DriverAmount"] = txtDriverAmt.Text;
                RegisterGridviewUploadControls();

                if (Convert.ToDouble(txtDriverAmt.Text.Trim() == "" ? "0" : txtDriverAmt.Text.Trim()) == 0)
                {
                    fupDriver.Enabled = false;
                    btnUploadDriver.Enabled = false;
                }
                else
                {
                    fupDriver.Enabled = true;
                    btnUploadDriver.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        //protected void btnSaveOption_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (!rdbOld.Checked && !rdbNew.Checked)
        //        {
        //            lblMessageSucc.Text = "Select Tax Option.";
        //            objcommon.Display("Validate", "DisplayErrorMessage('Select Tax Option.');");
        //        }
        //        else
        //        {
        //            if (rdbOld.Checked)
        //            {
        //                objTax.SaveTaxOption((string)Session["sCompID"], (string)Session["sEmpID"], "O");


        //            }
        //            else if (rdbNew.Checked)
        //            {
        //                objTax.SaveTaxOption((string)Session["sCompID"], (string)Session["sEmpID"], "N");
        //            }

        //            lblMessageSucc.Text = "Selected Tax Option saved successfully.";
        //            objcommon.Display("Validate", "DisplayErrorMessage('Selected Tax Option saved successfully.');");
        //            lblMessageSucc.BackColor = System.Drawing.Color.GreenYellow;
        //            lblMessageSucc.ForeColor = System.Drawing.Color.DarkGreen;

        //            //Response.Redirect("InvestmentDetails.aspx", false);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessageSucc.Text = ex.Message;
        //    }
        //}

        protected void lnkOther_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                if (OtherDetails.Visible == false)
                {
                    FillOther();
                }
                OtherDetails.Visible = (OtherDetails.Visible == true ? false : true);
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
                divAlertSucc.Visible = false;
            }
        }

        protected void lblInvDetails_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                if (InvDetails.Visible == false)
                {
                    FillInvestments();
                    CalculateInvestments(gvInv, "INV");
                }
                InvDetails.Visible = (InvDetails.Visible == true ? false : true);
            }
            catch (Exception ex)
            {
                lblMessageSucc.Text = ex.Message;
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
            }
        }

        protected void lnkRent_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
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
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
                divAlertSucc.Visible = false;
            }
        }

        protected void lnk12_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                if (TwelveDetails.Visible == false)
                {
                    FillTwelve();
                    CalculateInvestments(gvtwelve, "12C");
                }
                TwelveDetails.Visible = (TwelveDetails.Visible == true ? false : true);
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                divAlertSucc.Visible = false;
            }
        }

        protected void lnk12b_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessageSucc.Text = "";
                if (TwelveBDetails.Visible == false)
                {
                    FillTwelveB();
                    CalculateInvestments(gvTwelB, "12B");
                }
                TwelveBDetails.Visible = (TwelveBDetails.Visible == true ? false : true);
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
                divAlertSucc.Visible = false;
            }
        }

        protected void lnkRentNew_Click(object sender, EventArgs e)
        {

        }
    }
}