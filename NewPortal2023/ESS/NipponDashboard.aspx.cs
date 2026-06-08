using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class NipponDashboard : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objCommon;
        NewPortal2023.ESS.Approval objAPPR;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sEmpID"] == null)
            {
                Response.Redirect("Login.aspx", true);
            }
            else
            {
                //if (Session["message"].ToString() != "")
                //{
                //    objCommon = new NewPortal2023.ESS.Common();

                //    divAlert.Visible = true;
                //    objCommon.SetMessageColor(divAlert, "success");
                //    lblMessage.Text = Session["message"].ToString();
                //}

                if (!Page.IsPostBack)
                {
                    objAPPR = new NewPortal2023.ESS.Approval();
                    objCommon = new NewPortal2023.ESS.Common();
                    //objAPPR.ProfID = Session["ProfId"].ToString();
                    objAPPR.EmpCode = Session["sEmpCode"].ToString();
                    //objAPPR.ApproveRoleStatus();
                    //Session["STATUS"] = objAPPR.Status;
                    //Session["USERLEVEL"] = objAPPR.UserLevel;
                    //Session["LEVEL"] = objAPPR.Level;
                    //Session["USERTYPE"] = objAPPR.UserType;

                    Fill_Details();
                    //SetRights();

                }

            }
        }

        protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                //gvList.PageIndex = e.NewPageIndex;
                lblMessage.Text = "";
                Fill_Details();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        private void SetRights()
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string[] urlSlip = url.Split('/');
            string page = urlSlip[urlSlip.Length - 1];
            string cr = "";
            string vw = "";
            string up = "";
            string de = "";

            DataTable dt = (DataTable)Session["Menu"];
            DataRow[] dr;
            if (dt != null)
            {
                dr = dt.Select("menu_url='" + page + "'");
                if (dr.Length > 0)
                {

                    ViewState["cr"] = cr;
                    ViewState["vw"] = vw;
                    ViewState["up"] = up;
                    ViewState["de"] = de;
                }
            }
        }
        protected void lnkMyDocumemt_Click(object sender, EventArgs e)
        {
            Response.Redirect("HiringOfficer.aspx", true);
        }

        protected void lnkCandidateRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("BranchMgr.aspx", true);
        }

        private void Fill_Details()
        {
            objCommon = new NewPortal2023.ESS.Common();
            DataSet ds = new DataSet();
            //string Level = Session["LEVEL"].ToString();
            //string USerlevel = (Session["LEVEL"].ToString());
            //string EMPCODE = Session["sEmpCode"].ToString();
            //string EMPNAME = Session["sEmpName"].ToString();
            //string EMAIL = Session["EmpEmail"].ToString();




            //if (USerlevel == "5")
            //{
                divFinMakerAssignDataPoint.Visible = true;

            lnkCandidateRegister.Text = "Candidate Registration";
            lnkMyDocumemt.Text = "My Candidates";
            lnkAssignCXO.Text = "Joining Date Confirmation";
            linkAssignPendingMaker.Text = "Upload Document";
            lnkAssignHOD.Text = "CFR";
            lnkAssignChecker.Text = "Reports";

            //ds = objAPPR.FillDashBoard(EMPCODE, Level, EMPNAME, EMAIL);
            //if (ds.Tables.Count > 0)
            //{
            //------------------------------------------------Assign DataPoint Finance Maker--------------------------------------------------

            //lnkAssignFinanceApproved.Text = ds.Tables[1].Rows[0]["Approved"].ToString() + " " + "Approved";
            //lnkAssignFinaceReject.Text = ds.Tables[2].Rows[0]["Rejected"].ToString() + " " + "Rejected";
            //lnkAssignCXO.Text = ds.Tables[4].Rows[0]["PendingCXO"].ToString() + " " + "Assign DataPoint For CXO-Pending";
            //lnkAssignHOD.Text = ds.Tables[5].Rows[0]["PendingHOD"].ToString() + " " + "Assign DataPoint For HOD-Pending";
            //lnkAssignChecker.Text = ds.Tables[6].Rows[0]["PendingChecker"].ToString() + " " + "Assign DataPoint For Checker-Pending";
            //LinkTotalFin.Text = ds.Tables[0].Rows[0]["TotalAssignDataPoint"].ToString() + " " + "Total Assign DataPoint";

            ////------------------------------------------------Tranche DataPoint Finance Maker--------------------------------------------------
            //linkAssignPendingMaker.Text = ds.Tables[3].Rows[0]["Pending"].ToString() + " " + "Pending Trance DataPoint";
            //lnkTranceTotalDataPoint.Text = ds.Tables[7].Rows[0]["TotalTrancheDataPoint"].ToString() + " " + "Total Tranche DataPoint";
            //lnkTranchApprovedFinanceMaker.Text = ds.Tables[8].Rows[0]["Approved"].ToString() + " " + "Approved";
            //lnkTranchRejectFinanceMaker.Text = ds.Tables[9].Rows[0]["Rejected"].ToString() + " " + "Rejected";
            //lnkTranchPendingPrimaryFinanceMaker.Text = ds.Tables[13].Rows[0]["PendingPrimary"].ToString() + " " + "DataPoint For Primary-Pending";
            //lnkTranchPendingSecondaryFinanceMaker.Text = ds.Tables[12].Rows[0]["PendingSecondary"].ToString() + " " + "DataPoint For Secondary-Pending ";
            //lnkTranchPendingHODFinanceMaker.Text = ds.Tables[11].Rows[0]["PendingHOD"].ToString() + " " + "DataPoint For HOD-Pending ";
            //lnkTranchPendingCXOFinanceMaker.Text = ds.Tables[10].Rows[0]["PendingCXO"].ToString() + " " + "DataPoint For CXO-Pending ";
            // }
            //}
            //else
            //{

            //}

        }

       
    }
}