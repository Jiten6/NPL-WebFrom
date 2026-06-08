using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class PersonalDetails : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objCommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.OTP objOTP = new NewPortal2023.ESS.OTP();
        DataSet dsExp = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
              if (Session["sCompID"]!=null)
            {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            try
            {
                //if (Session["sEmpID"] == null)
                //{
                //    Response.Redirect("Login.aspx", true);
                //}
                //else
                //{
                divAlertCreate.Visible = false;
                lblMessageCreate.Text = "";


                if (!Page.IsPostBack)
                {

                    mv.SetActiveView(vwCreate);

                    if (Request.QueryString["id"] != null)
                    {
                        ID = Convert.ToString(Request.QueryString["id"]);
                        if (ID != "")
                        {
                            DataSet ds = new DataSet();
                            objCommon = new NewPortal2023.ESS.Common();
                            objOTP.EntryAId = ID;


                            ds = objOTP.Fill_CandidateDetailByID();

                            if (ds.Tables[0].Rows.Count > 0)
                            {
                                btnSubmit.Text = "Update";

                                txtAadharNo.Text = ds.Tables[0].Rows[0]["PAGE_SIZE"].ToString();
                                txtCaseNo.Text = ds.Tables[0].Rows[0]["PAGE_SIZE"].ToString();
                                txtAadharName.Text = ds.Tables[0].Rows[0]["PAGE_SIZE"].ToString();
                                //txtPanName.SelectedValue = ds.Tables[0].Rows[0]["PAGE_SIZE"].ToString();
                                //txtDOB.SelectedValue = ds.Tables[0].Rows[0]["PAGE_SIZE"].ToString();
                                //txtG.SelectedValue = ds.Tables[0].Rows[0]["PAGE_SIZE"].ToString();
                                //drpSUCode.SelectedValue = ds.Tables[0].Rows[0]["PAGE_SIZE"].ToString();
                                //drpCandidateCategory.SelectedValue = ds.Tables[0].Rows[0]["PAGE_SIZE"].ToString();
                                //txtMobileNumber.Text = ds.Tables[0].Rows[0]["PAGE_SIZE"].ToString();

                            }

                        }
                        else if (Request.QueryString["Aadhid"] != null)
                        {
                            ID = Convert.ToString(Request.QueryString["Aadhid"]);
                            if (ID != "")
                            {

                            }

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                lblMessageCreate.Text = ex.Message;
            }

        }
              else
              {
                  Response.Redirect("Login.aspx");
              }
        }


        private void ClearControl()
        {

            //txtEmployeementType.Text = "";
            //txtAadharNo.Text = "";
            //txtPanNo.Text = "";
            //drpStateOfPosting.SelectedIndex = -1;
            //drpLocationOfPosting.SelectedIndex = -1;
            //drpGrade.SelectedIndex = -1;
            //drpSUCode.SelectedIndex = -1;
            //drpCandidateCategory.SelectedIndex = -1;
            //txtMobileNumber.Text = "";

        }
    }
}