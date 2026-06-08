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
    public partial class OverseasTravel : System.Web.UI.Page
    {
        NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();
        NewPortal2023.ESS.OverseasExpenses objOvr = new NewPortal2023.ESS.OverseasExpenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsOvr = new DataSet();
        DataSet dsExp = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
              if (Session["sCompID"]!=null)
            {
            if (!Page.IsPostBack)
            {
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                getOverseasData();
            }
            else
            {

            }
        }
             else
              {
                  Response.Redirect("Login.aspx");
              }
        }

        private void getOverseasData()
        {
            try
            {
                //string emp_aid = Session["sEmpID"].ToString();


                dsOvr = objOvr.GetTravelOverseasClaimList(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));

                if (dsOvr.Tables[0].Rows.Count > 0)
                {
                    Session["ClmType"] = dsOvr.Tables[0].Rows[0]["Status"].ToString();


                    this.gvOvrseasClaim.DataSource = dsOvr.Tables[0];
                    this.gvOvrseasClaim.DataBind();                    
                }
                else
                {
                    this.gvOvrseasClaim.DataSource = null;
                    this.gvOvrseasClaim.DataBind();
                }

                getCategoryType();
            }
            catch (Exception ex)
            {

            }
        }

        private void getCategoryType()
        {
            try
            {
                objExp = new NewPortal2023.ESS.Expenses();
                dsExp = objExp.GetCategoryType(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));
                if (dsExp.Tables.Count > 0)
                {
                    Session["CATEGORY_TYPE"] = dsExp.Tables[0].Rows[0]["CATEGORY_TYPE"].ToString();

                    if ((String)Session["CATEGORY_TYPE"]== "CT000001" || (String)Session["CATEGORY_TYPE"] == "CT000002")
                    {
                        drpType.Items.Insert(2, new ListItem("Entertainment", "Entertainment"));
                        drpType.Items.Insert(3, new ListItem("Travel + Entertainment", "Travel + Entertainment"));

                        drpClassTravel.Items.Insert(1, new ListItem("Air (Business Class)", "Air (Business Class)"));
                    }
                    else
                    {
                        drpClassTravel.Items.Insert(1, new ListItem("Air (Economy Class)", "Air (Economy Class)"));
                    }
                }


            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Category Not Found.";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                objOvr.RequisitionDate = txtDtreq.Text;
                objOvr.VisitPurpose = txtPurpose.Text;
                objOvr.VisitPlace = txtPlcvst.Text;
                objOvr.DepartdateInd = txtFromDate.Text;
                objOvr.ArvlDateInd = txtToDate.Text;
                objOvr.HODRcmd = txtRecHOD.Text;
                objOvr.FilingStatus = "S";
                objOvr.Status = "9";

                dsOvr = objOvr.InsertTravelOverseasClaim(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));

                divAlertCreate.Visible = true;
                lblMessageCreate.Visible = true;
                lblMessageCreate.Text = "Claim Submitted Successfully";

                mv.SetActiveView(vwList);


            }
            catch (Exception ex)
            {
                divAlertCreate.Visible = true;
                lblMessageCreate.Visible = true;
                lblMessageCreate.Text = "Claim Submitted Error";
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            mv.SetActiveView(vwCreate);
        }

        protected void gvOvrseasClaim_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblRemark = (Label)e.Row.FindControl("lblRemark");


                //lblRemark.BackColor = (lblRemark.Text.Trim() == "1" ? System.Drawing.Color.LightGray : lblRemark.BackColor);
                if (lblRemark != null)
                {
                    if (lblRemark.Text != "Submitted")
                    {
                        lblRemark.Text = "Submitted to " + lblRemark.Text + " level.";
                    }
                    else // No need to check again if it's equal to "Submitted"
                    {
                        lblRemark.Text = "Claim Submitted.";
                    }
                }
                else
                {
                    // Handle the case where the control is not found
                }

            }
        }



        protected void btnClose_Click(object sender, EventArgs e)
        {
            mv.SetActiveView(vwList);
            getOverseasData();
            //claimForm.Visible = false;
            //DivAction.Visible = true;
            divAlertCreate.Visible = false;
            lblMessageCreate.Text = "";            
        }

        protected void lnkTROVRClmNoClmNo_Click(object sender, EventArgs e)
        {
            mv.SetActiveView(vwCreate);
            divAlertCreate.Visible = false;
            //claimForm.Visible = true;
            LinkButton lnkTrOvrClmNo = (LinkButton)sender;

            string entryAid = ((LinkButton)lnkTrOvrClmNo.NamingContainer.FindControl("lnkTROVRClmNoClmNo")).Text;
            TextBox lblRemark = (TextBox)lnkTrOvrClmNo.NamingContainer.FindControl("lblRemark");


            objOvr.AppNo = entryAid;
            Session["Entry_aid"] = entryAid;
            dsOvr = objOvr.getTrOvrClaimById(Convert.ToString(Session["sCompID"]), (string)(Session["sEmpID"]));

            if (dsOvr.Tables.Count > 0)
            {
                txtDtreq.Text = dsOvr.Tables[0].Rows[0]["RequisitionDate"].ToString();
                txtPurpose.Text = dsOvr.Tables[0].Rows[0]["PurposeOfVisit"].ToString();
                txtPlcvst.Text = dsOvr.Tables[0].Rows[0]["Visitplace"].ToString();
                txtFromDate.Text = dsOvr.Tables[0].Rows[0]["DepartDateInd"].ToString();
                txtToDate.Text = dsOvr.Tables[0].Rows[0]["ArvlDateInd"].ToString();
                txtRecHOD.Text = dsOvr.Tables[0].Rows[0]["HOD_Recmnd"].ToString();

            }
        }

        protected void rdbSAAR_CheckedChanged(object sender, EventArgs e)
        {
            if (txtFromDate.Text == "" || txtToDate.Text == "")
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Please select the travel Date.";
                string script = $@"<script type='text/javascript'>alert('Please select the travel Date.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            }
            else
            {
                if (rdbSAAR.Checked == true)
                {
                    divSAAR.Visible = true;
                    divEastern.Visible = false;
                    divOtherCountries.Visible = false;
                    
                    divAmt.Visible = true;
                    divEligi.Visible = true;
                    divUpload.Visible = true;
                    divfileUpload.Visible = true;
                    divfileDisplay.Visible = false;

                    dsExp = objOvr.GetSAARReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_TYPE"]));
                    if (dsExp.Tables.Count > 0)
                    {
                        //dsExp.Tables[0].Rows[0][""].ToString();
                        gvSAAR.DataSource = dsExp.Tables[0];
                        gvSAAR.DataBind();
                    }
                    else
                    {
                        gvSAAR.DataSource = null;
                        gvSAAR.DataBind();

                    }

                }
                else if (rdbEastCon.Checked == true)
                {
                    divEastern.Visible = true;
                    divSAAR.Visible = false;
                    divOtherCountries.Visible = false;

                    divAmt.Visible = true;
                    divEligi.Visible = true;
                    divUpload.Visible = true;
                    divfileUpload.Visible = true;
                    divfileDisplay.Visible = false;
                    dsExp = objOvr.GetNonEasternConReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_TYPE"]));
                    if (dsExp.Tables.Count > 0)
                    {
                        gvEasternCon.DataSource = dsExp.Tables[0];
                        gvEasternCon.DataBind();
                    }
                    else
                    {
                        gvEasternCon.DataSource = null;
                        gvEasternCon.DataBind();
                    }
                }
                else if (rdbOtherCon.Checked == true)
                {
                    divOtherCountries.Visible = true;
                    divSAAR.Visible = false;
                    divEastern.Visible = false;

                    divAmt.Visible = true;
                    divEligi.Visible = true;
                    divUpload.Visible = true;
                    divfileUpload.Visible = true;
                    divfileDisplay.Visible = false;
                    dsExp = objOvr.GetOtherConReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_TYPE"]));
                    if (dsExp.Tables.Count > 0)
                    {
                        gvOtherCon.DataSource = dsExp.Tables[0];
                        gvOtherCon.DataBind();
                    }
                    else
                    {
                        gvOtherCon.DataSource = null;
                        gvOtherCon.DataBind();
                    }
                }

                CalculateTotalDays();
                CalculateEligibAmt();
            }
        }

        protected void rdbEastCon_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rdbOtherCon_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void CalculateTotalDays()
        {
            DateTime fromDate;
            DateTime toDate;

            if (DateTime.TryParse(txtFromDate.Text, out fromDate) && DateTime.TryParse(txtToDate.Text, out toDate))
            {
                if (fromDate <= toDate)
                {
                    TimeSpan timeDiff = toDate - fromDate;
                    int totalDays = timeDiff.Days + 1; // Add 1 to include the end date
                    txtNoDays.Text = totalDays.ToString();
                }
                else
                {
                    txtNoDays.Text = string.Empty;
                }
            }
            else
            {
                txtNoDays.Text = string.Empty;
            }
        }

        private void CalculateEligibAmt()
        {
            double noOfDays = Convert.ToDouble(txtNoDays.Text);
            if (rdbSAAR.Checked == true)
            {
                foreach (GridViewRow row in gvSAAR.Rows)
                {
                    Label lblAllwncAmt = (Label)row.FindControl("lblAllwncAmt");
                    double eligibAmt = Convert.ToDouble(lblAllwncAmt.Text) * (noOfDays);
                    txtligibility.Text = eligibAmt.ToString();
                }
            }
            else if (rdbEastCon.Checked == true)
            {
                foreach (GridViewRow row in gvEasternCon.Rows)
                {
                    Label lblAllwncAmt = (Label)row.FindControl("lblAllwncAmt");
                    double eligibAmt = Convert.ToDouble(lblAllwncAmt.Text) * (noOfDays);
                    txtligibility.Text = eligibAmt.ToString();
                }
            }
            else if (rdbOtherCon.Checked == true)
            {
                foreach (GridViewRow row in gvOtherCon.Rows)
                {
                    Label lblAllwncAmt = (Label)row.FindControl("lblAllwncAmt");
                    double eligibAmt = Convert.ToDouble(lblAllwncAmt.Text) * (noOfDays);
                    txtligibility.Text = eligibAmt.ToString();
                }
            }
        }

        protected void lnkBtnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorage");

                string openFilePath = lblTSFileStorageName.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
                                                                //openFilePath = openFilePath + ViewState["FILE_PATH"].ToString();

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
            catch
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void drpType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpType.SelectedValue== "Travel")
            {
                divDailyReimb.Visible = true;
                divEntertainment.Visible = false;
                divWardrobe.Visible = true;
                drpClassTravel.Visible = true;
                Label3.Visible = true;
                //lblwardrobe.Visible = true;
                lblgroup.Visible = true;
                dvgroup.Visible = true;
                div1.Visible = true;
                div2.Visible = true;
                div9.Visible = true;
                div10.Visible = true;
                div11.Visible = true;
                div12.Visible = true;

                dsExp = objOvr.GetWardrReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_TYPE"]));
                if (dsExp.Tables.Count > 0)
                {
                    gvWardrobe.DataSource = dsExp.Tables[0];
                    gvWardrobe.DataBind();
                }
                else
                {
                    gvWardrobe.DataSource = null;
                    gvWardrobe.DataBind();
                }
            }
            else if(drpType.SelectedValue == "Entertainment")
            {
                divDailyReimb.Visible = false;
                divEntertainment.Visible = true;
                div1.Visible = true;
                div2.Visible = true;
                divWardrobe.Visible = false;
                drpClassTravel.Visible = false;
                Label3.Visible = false;
                //lblwardrobe.Visible = false;
                //lblgroup.Visible = false;
                dvgroup.Visible = false;
                divAirBuisn.Visible = false;
                divAirEco.Visible = false;
                div4.Visible = true;

                dsExp = objOvr.GetEntReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_TYPE"]));
                if (dsExp.Tables.Count > 0)
                {
                    gvEnt.DataSource = dsExp.Tables[0];
                    gvEnt.DataBind();
                }
                else
                {
                    gvEnt.DataSource = null;
                    gvEnt.DataBind();
                }

                dsExp = objOvr.GetWardrReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_TYPE"]));
                if (dsExp.Tables.Count > 0)
                {
                    gvWardrobe.DataSource = dsExp.Tables[0];
                    gvWardrobe.DataBind();
                }
                else
                {
                    gvWardrobe.DataSource = null;
                    gvWardrobe.DataBind();
                }
            }
            else if (drpType.SelectedValue == "Travel + Entertainment")
            {
                divDailyReimb.Visible = true;
                divEntertainment.Visible = true;
                divWardrobe.Visible = true;
                drpClassTravel.Visible = true;
                Label3.Visible = true;
                //lblwardrobe.Visible = true;
                //lblgroup.Visible = true;
                dvgroup.Visible = true;
                div1.Visible = true;
                div2.Visible = true;
                div9.Visible = true;
                div10.Visible = true;
                div11.Visible = true;
                div3.Visible = true;
                div12.Visible = true;
                div4.Visible = true;

                dsExp = objOvr.GetOtherConReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_TYPE"]));
                if (dsExp.Tables.Count > 0)
                {
                    gvEnt.DataSource = dsExp.Tables[0];
                    gvEnt.DataBind();
                }
                else
                {
                    gvEnt.DataSource = null;
                    gvEnt.DataBind();
                }

                dsExp = objOvr.GetWardrReimb(Convert.ToString(Session["sCompID"]), (string)(Session["CATEGORY_TYPE"]));
                if (dsExp.Tables.Count > 0)
                {
                    gvWardrobe.DataSource = dsExp.Tables[0];
                    gvWardrobe.DataBind();
                }
                else
                {
                    gvWardrobe.DataSource = null;
                    gvWardrobe.DataBind();
                }
            }
        }

        protected void drpClassTravel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(drpClassTravel.SelectedValue == "Air (Business Class)")
            {
                divAirBuisn.Visible = true;
                divAirEco.Visible = false;
            }
            else if(drpClassTravel.SelectedValue == "Air (Economy Class)")
            {
                divAirEco.Visible = true;
                divAirBuisn.Visible = false;
            }
        }
    }
}