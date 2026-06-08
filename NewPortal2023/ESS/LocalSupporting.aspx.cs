using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Configuration;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class LocalSupporting : System.Web.UI.Page
    {
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        NewPortal2023.ESS.LocalExpenses objLoc = new NewPortal2023.ESS.LocalExpenses();
        NewPortal2023.ESS.Expenses objeXP = new NewPortal2023.ESS.Expenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsExp = new DataSet();
        private string SourcePath = string.Empty;
        private string savePath = string.Empty;
        private string SourcePath2 = string.Empty;
        private string savePath2 = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                getLocalData();
            }

        }

        private void getLocalData()
        {
            try
            {
                dsExp = objeXP.GetSupportingLocalList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables[0].Rows.Count > 0)
                {
                    this.gvLocalClaimList.DataSource = dsExp.Tables[0];
                    this.gvLocalClaimList.DataBind();
                }
                else
                {
                    this.gvLocalClaimList.DataSource = null;
                    this.gvLocalClaimList.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void getCategoryType()
        {
            try
            {
                objLoc = new NewPortal2023.ESS.LocalExpenses();
                dsExp = objLoc.GetLocCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables.Count > 0)
                {

                    Session["CATEGORY_TYPE"] = dsExp.Tables[0].Rows[0]["CATEGORY_TYPE"].ToString();
                    Session["DESG_AID"] = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();
                }


            }
            catch (Exception ex)
            {
                lblMessage.Text = "Category Not Found.";
            }
        }

        protected void lnkLOCClmNoClmNo_Click(object sender, EventArgs e)
        {
            LinkButton lnkDOMClmNo = (LinkButton)sender;

            string entryAid = ((LinkButton)lnkDOMClmNo.NamingContainer.FindControl("lnkLOCClmNoClmNo")).Text;
            string lblEmpAId = ((Label)lnkDOMClmNo.NamingContainer.FindControl("lblEmpCode")).Text;
            Session["EntryAid"] = entryAid;

            objLoc.AppNo = entryAid;
            dsExp = objLoc.getaPPROVELocalClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));

            if (dsExp.Tables[0].Rows[0]["status"].ToString() == "9")
            {
                radiaoEntertainment1.Visible = true;
            }
            else
            {

                radiaoEntertainment1.Visible = true;
                radiaoEntertainment1.Enabled = false;
            }
            if (dsExp.Tables.Count > 0)
            {
                SectionList.Visible = false;
                divFrom.Visible = true;
                divUpload.Visible = true;
                divfileUpload.Visible = false;
                divfileDisplay.Visible = true;
                divTravel.Visible = true;
               
                string categoryType = dsExp.Tables[0].Rows[0]["Category_AId"].ToString();
                string desgAid = dsExp.Tables[0].Rows[0]["DESG_AID"].ToString();
                Session["CATEGORY_TYPE"] = categoryType;
                Session["DESG_AID"] = desgAid;
                if (dsExp.Tables[0].Rows[0]["Claim1_Amount"].ToString() != "0.00")
                {
                    chk1.Checked = true;
                    txtchk1.Visible = true;
                    txtchk1.Text = dsExp.Tables[0].Rows[0]["Claim1_Amount"].ToString();
                }
                else
                {
                    chk1.Checked = false;
                    txtchk1.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["Claim2_Amount"].ToString() != "0.00")
                {
                    chk2.Checked = true;
                    txtchk2.Visible = true;
                    txtchk2.Text = dsExp.Tables[0].Rows[0]["Claim2_Amount"].ToString();
                }
                else
                {
                    chk2.Checked = false;
                    txtchk2.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["Claim3_Amount"].ToString() != "0.00")
                {
                    chk3.Checked = true;
                    txtchk3.Visible = true;
                    txtchk3.Text = dsExp.Tables[0].Rows[0]["Claim3_Amount"].ToString();
                }
                else
                {
                    chk3.Checked = false;
                    txtchk3.Visible = false;

                }
                if (dsExp.Tables[0].Rows[0]["Claim4_Amount"].ToString() != "0.00")
                {
                    chk4.Checked = true;
                    txtchk4.Visible = true;
                    txtchk4.Text = dsExp.Tables[0].Rows[0]["Claim4_Amount"].ToString();
                }
                else
                {
                    chk4.Checked = false;
                    txtchk4.Visible = false;

                }
                txtDate.Text = dsExp.Tables[0].Rows[0]["Expenses_Date"].ToString();
                txtCashVocher.Text = dsExp.Tables[0].Rows[0]["Cash_Voucher"].ToString();
                txtApprovedAmt.Text = dsExp.Tables[0].Rows[0]["ClaimApproved_Amount"].ToString();
                txtTravelDes.Text = dsExp.Tables[0].Rows[0]["Travel_Description"].ToString();
                txtMeal.Text = dsExp.Tables[0].Rows[0]["Meal"].ToString();
                txtOtherExpenses.Text = dsExp.Tables[0].Rows[0]["Other_Expenses"].ToString();
                txtadv.Text = dsExp.Tables[0].Rows[0]["ADVANCE_AMT"].ToString();
                txtNameAss.Text = dsExp.Tables[0].Rows[0]["Name_Bussi_Ass"].ToString();
                dsExp = objLoc.GetLocalReimb(Convert.ToString(Session["sCompID"]), Convert.ToString(Session["CATEGORY_TYPE"]));
                if (dsExp.Tables.Count > 0)
                {
                    grLocalReimb.DataSource = dsExp.Tables[0];
                    grLocalReimb.DataBind();
                    DisplayDocuments(entryAid, lblEmpAId);

                }
                else
                {
                    grLocalReimb.DataSource = null;
                    grLocalReimb.DataBind();
                }

            }

            Calculatetotalexp();
        }

        protected void gvLocalClaimList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    LinkButton lnkLOCClmNoClmNo = (LinkButton)e.Row.FindControl("lnkLOCClmNoClmNo");
                    Label lblEmpCode = (Label)e.Row.FindControl("lblEmpCode");
                    Label lblEmpName = (Label)e.Row.FindControl("lblEmpName");
                    Label txtClmDate = (Label)e.Row.FindControl("txtClmDate");
                    Label txFrDate = (Label)e.Row.FindControl("txFrDate");
                    Label txtClmAmt = (Label)e.Row.FindControl("txtClmAmt");
                    Label txtLOCApprAmt = (Label)e.Row.FindControl("txtLOCApprAmt");
                    Label txtFrDest = (Label)e.Row.FindControl("txtFrDest");
                    TextBox txtToDest = (TextBox)e.Row.FindControl("txtToDest");
                    Label txtLOCSTS = (Label)e.Row.FindControl("txtLOCSTS");
                }
            }

            catch (Exception ex)
            {

            }

        }

        protected void gvLocalClaimList_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;

            if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                || (gv.ShowHeaderWhenEmpty == true))
            {
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (gv.ShowFooter == true && gv.Rows.Count > 0)
            {
                gv.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }

        protected void lnkBtnOpenFiles_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblFileStorage = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorage");

                string openFilePath = lblFileStorage.Text;
              
                //string fileName = lblTSFileStorageName.Text;

                System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + lnkBtnOpenFiles.Text);
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

        private void Calculatetotalexp()
        {
            decimal chk1 = string.IsNullOrEmpty(txtchk1.Text) ? 0 : decimal.Parse(txtchk1.Text);
            decimal chk2 = string.IsNullOrEmpty(txtchk2.Text) ? 0 : decimal.Parse(txtchk2.Text);
            decimal chk3 = string.IsNullOrEmpty(txtchk3.Text) ? 0 : decimal.Parse(txtchk3.Text);
            decimal chk4 = string.IsNullOrEmpty(txtchk4.Text) ? 0 : decimal.Parse(txtchk4.Text);
            decimal meal = string.IsNullOrEmpty(txtMeal.Text) ? 0 : decimal.Parse(txtMeal.Text);
            decimal otherExpenses = string.IsNullOrEmpty(txtOtherExpenses.Text) ? 0 : decimal.Parse(txtOtherExpenses.Text);
            decimal adv = string.IsNullOrEmpty(txtadv.Text) ? 0 : decimal.Parse(txtadv.Text);

            decimal totalExpenses = chk1 + chk2 + chk3 + chk4 + meal + otherExpenses;
            decimal paid = totalExpenses - adv;
            txtTotalexp.Text = totalExpenses.ToString();
            txtpaidamt.Text = paid.ToString();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            divFrom.Visible = false;
            divTravel.Visible = false;
            SectionList.Visible = true;
            getLocalData();
        }

        protected void chk1_CheckedChanged(object sender, EventArgs e)
        {
            if (chk1.Checked == true)
            {
                txtchk1.Visible = true;
            }
        }

        protected void chk2_CheckedChanged(object sender, EventArgs e)
        {
            if (chk2.Checked == true)
            {
                txtchk2.Visible = true;
            }
        }

        protected void chk3_CheckedChanged(object sender, EventArgs e)
        {
            if (chk3.Checked == true)
            {
                txtchk3.Visible = true;
            }
        }

        protected void chk4_CheckedChanged(object sender, EventArgs e)
        {
            if (chk4.Checked == true)
            {
                txtchk4.Visible = true;
            }
        }

        private void DisplayDocuments(string entryCode, string lblEmpAId)
        {
            try
            {
                CreateDocumentsStructure();

                string prefix = "";

                //dsExp = objLoc.getaPPROVELocalClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));  PARESH

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Local\\" + lblEmpAId + "\\" + entryCode + "\\";

                if (System.IO.Directory.Exists(savePath) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles(prefix + "*");

                    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }

                    this.gvDomesticFile.DataSource = dtDocInfo;
                    this.gvDomesticFile.DataBind();

                    if (dtDocInfo.Rows.Count > 0)
                    {
                        divfileUpload.Visible = false;
                        divfileDisplay.Visible = true;


                    }
                    else
                    {
                        //trUpload.Visible = true;
                        divfileDisplay.Visible = false;
                    }
                }
                else
                {
                    //trUpload.Visible = true;
                    divfileDisplay.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void CreateDocumentsStructure()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvDomesticFile.DataSource = dtDocuments;
                gvDomesticFile.DataBind();
            }
        }

      
        


























    }
}