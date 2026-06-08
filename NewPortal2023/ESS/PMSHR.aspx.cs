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
using System.Xml;

namespace NewPortal2023.ESS
{
    public partial class PMSHR : System.Web.UI.Page
    {
        NewPortal2023.ESS.PerformanceAppraisal objAppraisal = new NewPortal2023.ESS.PerformanceAppraisal();
        NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();

        DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["sCompID"] != null)
            {
                if (!Page.IsPostBack)
                {
                    //getKRAActivationDateStatus();
                    FillFinYear();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        private void FillFinYear()
        {
            DataSet ds = new DataSet();

            ds = objAppraisal.GetYears((string)Session["sCompID"]);

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["PYDESC"].ToString() != "")
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (!string.IsNullOrEmpty(row["PYDESC"].ToString()))
                        {
                            drpFinancialYear.Items.Add(new ListItem(row["PYDESC"].ToString(), row["PYDESC"].ToString()));
                        }
                    }
                }
            }

            string currentFinancialYear = ds.Tables[1].Rows[0]["CYDESC"].ToString();
            string nextFinancialYear = ds.Tables[2].Rows[0]["NYDESC"].ToString();

            // Add values to the dropdown
            drpFinancialYear.Items.Add(new ListItem(currentFinancialYear, currentFinancialYear));
            drpFinancialYear.Items.Add(new ListItem(nextFinancialYear, nextFinancialYear));
        }

        protected void drpFinancialYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            divAlert.Visible = false;
            lblMessage.Text = "";

            DataSet ds = new DataSet();

            if (drpFinancialYear.SelectedIndex == 0)
            {
                divKRAFin.Visible = false;
            }
            else
            {
                divKRAFin.Visible = true;
            }

            ds = objAppraisal.GetYears((string)Session["sCompID"]);

            string currentFinancialYear = ds.Tables[1].Rows[0]["CYDESC"].ToString();
            string nextFinancialYear = ds.Tables[2].Rows[0]["NYDESC"].ToString();

            if (drpFinancialYear.SelectedValue == currentFinancialYear)
            {
                lblCycle.Text = "Appraisal Cycle";
                ViewState["CYCLE"] = "Appraisal Cycle";
                ViewState["CYCLE_AID"] = "C1";
            }
            else if (drpFinancialYear.SelectedValue == nextFinancialYear)
            {
                lblCycle.Text = "KRA Cycle";
                ViewState["CYCLE"] = "KRA Cycle";
                ViewState["CYCLE_AID"] = "C0";
            }
            else
            {
                lblCycle.Text = "";
                ViewState["CYCLE"] = "";
                ViewState["CYCLE_AID"] = "";
            }

            ViewState["Year"] = drpFinancialYear.SelectedValue;

            //Get_EmployeeType();
            //Fill_Details("1", gvList.PageSize.ToString());

            getList();
            mv.SetActiveView(vwList);
        }

        private void getList()
        {
            try
            {
                objPmsHr = new NewPortal2023.ESS.PmsHr();

                DataSet ds = new DataSet();
                DataSet dsKRA = new DataSet();
                DataSet dsEx = new DataSet();

                dsKRA = objPmsHr.GetKRAFlagStatus();
                objPmsHr.KRA_Activation_Flag = dsKRA.Tables[0].Rows[0]["KRAFlag"].ToString();

                objPmsHr.Year = ViewState["Year"].ToString();
                dsEx = objPmsHr.GetPmsActivationDates();

                //string fromDateEmp = dsEx.Tables[0].Rows[0]["FROMDATEEMP"].ToString();
                //DateTime fromDate = DateTime.ParseExact(fromDateEmp, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                //txtFromDateEmp.Text = fromDate.ToString("dd-MMMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

                //string toDateEmp = dsEx.Tables[1].Rows[0]["TODATEEMP"].ToString();
                //DateTime toDateE = DateTime.ParseExact(toDateEmp, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                //txtToDateEmp.Text = toDateE.ToString("dd-MMMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

                //string fromDateAppr = dsEx.Tables[2].Rows[0]["FROMDATEAPPR"].ToString();
                //DateTime fromDateA = DateTime.ParseExact(fromDateAppr, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                //txtFromDateAppr.Text = fromDateA.ToString("dd-MMMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

                //string toDateAppr = dsEx.Tables[3].Rows[0]["TODATEAPPR"].ToString();
                //DateTime toDateA = DateTime.ParseExact(toDateAppr, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                //txtToDateAppr.Text = toDateA.ToString("dd-MMMM-yyyy", System.Globalization.CultureInfo.InvariantCulture);


                txtFromDateEmp.Text = dsEx.Tables[0].Rows[0]["FROMDATEEMP"].ToString();
                txtToDateEmp.Text = dsEx.Tables[1].Rows[0]["TODATEEMP"].ToString();
                txtFromDateAppr.Text = dsEx.Tables[2].Rows[0]["FROMDATEAPPR"].ToString();
                txtToDateAppr.Text = dsEx.Tables[3].Rows[0]["TODATEAPPR"].ToString();

                objPmsHr.Year = ViewState["Year"].ToString();
                ds = objPmsHr.GetList();

                if (ds.Tables[1].Rows.Count > 0)
                {
                    gvList.DataSource = ds.Tables[1];
                    gvList.DataBind();
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();

                }

            }
            catch (Exception ex)
            {
                objcommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                objcommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = ex.Message;
            }
        }

        protected void txtToDateEmp_TextChanged(object sender, EventArgs e)
        {
            //txtFromDateAppr.Text = txtToDateEmp.Text;

            DateTime toDate;
            if (DateTime.TryParseExact(txtToDateEmp.Text, "dd-MMMM-yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out toDate))
            {
                //txtFromDateAppr.Text = toDate.AddDays(1).ToString("dd-MMMM-yyyy"); // Format as needed
                txtFromDateAppr.Text = txtFromDateEmp.Text;
            }
            else
            {
                txtFromDateAppr.Text = ""; // Handle invalid date input
            }
        }

        protected void btnSubmitDates_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();

            objPmsHr.FromDateEmp = txtFromDateEmp.Text;
            objPmsHr.ToDateEmp = txtToDateEmp.Text;
            objPmsHr.FromDateAppr = txtFromDateAppr.Text;
            objPmsHr.ToDateAppr = txtToDateAppr.Text;
            objPmsHr.Year = ViewState["Year"].ToString();

            ds = objPmsHr.UpdatePMSActivationDates();

            if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "")
            {
                getList();

                objcommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                string message = "PMS activation dates successfully updated";
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlert, "success");
                lblMessage.Text = "PMS activation dates successfully updated";
            }

        }

        protected void getKRAFlagStatus()
        {
            try
            {
                DataSet ds = new DataSet();
                objPmsHr = new NewPortal2023.ESS.PmsHr();

                ds = objPmsHr.GetKRAFlagStatus();
                objPmsHr.KRA_Activation_Flag = ds.Tables[0].Rows[0]["KRAFlag"].ToString();
                getList();

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void drpAction_SelectedIndexChanged1(object sender, EventArgs e)
        {
            try
            {
                DropDownList lnkBtnOpenFiles = (DropDownList)sender;
                Label lblAID = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("txtEmpID");
                objPmsHr.EmpCode = lblAID.Text;

                DropDownList drpactiontype = lnkBtnOpenFiles.NamingContainer.FindControl("drpAction") as DropDownList;
                string action = drpactiontype.SelectedItem.Value;

                ds = objPmsHr.GetKraStatus();



                if (action == "Reporting Manager")
                {
                    objPmsHr.Year = ViewState["Year"].ToString();
                    objPmsHr.Flag = "1";
                    objPmsHr.Type = "RM";
                    objPmsHr.KRA_Flag_Status = "APPROVER";
                    objPmsHr.EmpCode = lblAID.Text;
                    ds = objPmsHr.UpdateKRAFlagOneEmp();

                    if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "")
                    {
                        getList();
                        objcommon = new NewPortal2023.ESS.Common();
                        divAlert.Visible = true;
                        string message = "Enable Successfully. ";
                        string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        objcommon.SetMessageColor(divAlert, "success");
                        lblMessage.Text = "Enable Successfully.";
                    }
                }
                else if (action == "Employee")
                {
                    objPmsHr.Year = ViewState["Year"].ToString();
                    objPmsHr.Flag = "0";
                    objPmsHr.Type = "EMP";
                    objPmsHr.KRA_Flag_Status = "EMPLOYEE";
                    objPmsHr.EmpCode = lblAID.Text;
                    ds = objPmsHr.UpdateKRAFlagOneEmp();

                    if (ds.Tables[0].Rows[0]["RESULT"].ToString() == "")
                    {
                        objcommon = new NewPortal2023.ESS.Common();
                        divAlert.Visible = true;
                        string message = "Enable Successfully. ";
                        string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        objcommon.SetMessageColor(divAlert, "success");
                        lblMessage.Text = "Enable Successfully.";
                    }
                }

                getList();


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        //private void getKRAActivationDateStatus()
        //{
        //    try
        //    {
        //        DataSet dsDateAct = new DataSet();
        //        DataSet dsKRAFlag = new DataSet();
        //        DataSet dsKRARejectedFlag = new DataSet();
        //        objPmsHr = new NewPortal2023.ESS.PmsHr();

        //        dsDateAct = objPmsHr.GetActivationDate();
        //        dsKRARejectedFlag = objPmsHr.GetKRARejectCount();

        //        string EmpdateOnly;
        //        string ApprdateOnly;

        //        string EmpDeactivateDate = dsDateAct.Tables[4].Rows[0]["EMP_DEACTIVATE_DATE"].ToString() == null ? "" : dsDateAct.Tables[4].Rows[0]["EMP_DEACTIVATE_DATE"].ToString();

        //        if(EmpDeactivateDate != "")
        //        {
        //            DateTime EmpdateValue = DateTime.ParseExact(EmpDeactivateDate, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        //            EmpdateOnly = EmpdateValue.ToString("dd-MM-yyyy");
        //            txtFromDate.Enabled = true;
        //        }
        //        else
        //        {
        //            EmpdateOnly = "";
        //            txtFromDate.Enabled = false;
        //        }

        //        string ApprDeactivateDate = dsDateAct.Tables[5].Rows[0]["APPR_DEACTIVATE_DATE"].ToString() == null ? "" : dsDateAct.Tables[5].Rows[0]["APPR_DEACTIVATE_DATE"].ToString();

        //        if (ApprDeactivateDate != "")
        //        {
        //            DateTime ApprdateValue = DateTime.ParseExact(ApprDeactivateDate, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
        //            ApprdateOnly = ApprdateValue.ToString("dd-MM-yyyy");
        //            txtToDate.Enabled = true;
        //        }
        //        else
        //        {
        //            ApprdateOnly = "";
        //            txtToDate.Enabled = false;
        //        }


        //        if (dsKRARejectedFlag.Tables[0].Rows[0]["KRA_REJECT_ID"].ToString() == "0")
        //        {
        //            if (dsDateAct.Tables[0].Rows[0]["EMP_DEACTIVATE_DATE"].ToString() == "1")
        //            {
        //                // var date = Convert.ToDateTime(dsDateAct.Tables[2].Rows[0]["Deactivation_Date"].ToString());
        //                txtFromDate.Text = EmpdateOnly;
        //                txtToDate.Text = ApprdateOnly;

        //                objPmsHr.Flag = "1";
        //                objPmsHr.Type = "EMP";
        //                dsKRAFlag = objPmsHr.UpdatedKRAFlagAll();

        //                EmpChk.Checked = false;
        //                ApprChk.Checked = true;
        //                //getList();
        //            }
        //            else
        //            {
        //                txtFromDate.Text = EmpdateOnly;
        //                txtToDate.Text = ApprdateOnly;
        //                //EmpChk.Checked = false;
        //                //ApprChk.Checked = false;
        //            }

        //            if (dsDateAct.Tables[1].Rows[0]["APPR_DEACTIVATE_DATE"].ToString() == "1")
        //            {
        //                //var date = Convert.ToDateTime(dsDateAct.Tables[3].Rows[0]["Deactivation_Date"].ToString());
        //                //txtToDate.Text = dsDateAct.Tables[3].Rows[0]["Deactivation_Date"].ToString("dd/MM/yyyy");

        //                txtFromDate.Text = EmpdateOnly;
        //                txtToDate.Text = ApprdateOnly;

        //                objPmsHr.Flag = "2";
        //                objPmsHr.Type = "APPR";
        //                dsKRAFlag = objPmsHr.UpdatedKRAFlagAll();

        //                EmpChk.Checked = false;
        //                ApprChk.Checked = false;
        //                //getList();
        //            }
        //            else
        //            {
        //                txtFromDate.Text = EmpdateOnly;
        //                txtToDate.Text = ApprdateOnly;
        //                //EmpChk.Checked = false;
        //                //ApprChk.Checked = false;
        //            }

        //        }
        //    }


        //    catch (Exception ex)
        //    {
        //        objcommon = new NewPortal2023.ESS.Common();
        //        divAlert.Visible = true;
        //        objcommon.SetMessageColor(divAlert, "danger");
        //        lblMessage.Text = ex.Message;
        //    }
        //}

        //protected void txtFromDate_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ds = new DataSet();
        //        objPmsHr = new NewPortal2023.ESS.PmsHr();
        //        if (EmpChk.Checked == true)
        //        {
        //            objPmsHr.ToDate = txtFromDate.Text;
        //            objPmsHr.Type = "EMP";
        //            objPmsHr.Comp_Aid = Session["sCompID"].ToString();
        //            objPmsHr.EmpCode = Session["sEmpCode"].ToString();

        //            ds = objPmsHr.InsertDateKRAFlag();

        //            string message = "Enable KRA Inputs Disable Date is set Successfully. ";
        //            string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //            objcommon.SetMessageColor(divAlert, "success");
        //            lblMessage.Text = "Enable KRA Inputs Disable Date is set Successfully. ";
        //        }
        //    }

        //    catch (Exception ex)
        //    {

        //    }
        //}

        //protected void txtToDate_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        ds = new DataSet();
        //        objPmsHr = new NewPortal2023.ESS.PmsHr();
        //        if (ApprChk.Checked == true)
        //        {
        //            objPmsHr.ToDate = txtToDate.Text;
        //            objPmsHr.Type = "APPR";
        //            objPmsHr.Comp_Aid = Session["sCompID"].ToString();
        //            objPmsHr.EmpCode = Session["sEmpCode"].ToString();

        //            ds = objPmsHr.InsertDateKRAFlag();

        //            objcommon = new NewPortal2023.ESS.Common();
        //            divAlert.Visible = true;

        //            string message = "Enable KRA Approval Disable Date is set Successfully. ";
        //            string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //            objcommon.SetMessageColor(divAlert, "success");
        //            lblMessage.Text = "Enable KRA Approval Disable Date is set Successfully. ";
        //        }
        //    }

        //    catch (Exception ex)
        //    {

        //    }
        //}

        //protected void EmpChk_CheckedChanged(object sender, EventArgs e)
        //{
        //    ds = new DataSet();
        //    objPmsHr = new NewPortal2023.ESS.PmsHr();
        //    try
        //    {
        //        if (EmpChk.Checked == true)
        //        {

        //            //lblEmpEnDiS.Visible = false;
        //            txtFromDate.Enabled = true;
        //            objPmsHr.Flag = "0";
        //            objPmsHr.Type = "EMP";

        //            ds = objPmsHr.UpdatedKRAFlagAll();

        //            if (ds.Tables[0].Rows[0]["KRAFLAG"].ToString() == "0")
        //            {
        //                EmpChk.Checked = true;
        //                //dnyaneshwar commented             lblEmpEnDiS.Text = "Disable";
        //                //dnyaneshwar commented           lblEmpEnDiS.ForeColor = System.Drawing.Color.Red;

        //                objcommon = new NewPortal2023.ESS.Common();
        //                divAlert.Visible = true;
        //                objPmsHr.KRA_Activation_Flag = "0";

        //                string message = "Enable Successfully. ";
        //                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //                objcommon.SetMessageColor(divAlert, "success");
        //                lblMessage.Text = "Enable Successfully. ";
        //            }
        //            else
        //            {
        //                EmpChk.Checked = false;
        //                //dnyaneshwar commented   lblEmpEnDiS.Text = "Enable";
        //                //dnyaneshwar commented       lblEmpEnDiS.ForeColor = System.Drawing.Color.Green;

        //                objcommon = new NewPortal2023.ESS.Common();
        //                divAlert.Visible = true;


        //                string message = "Disable Successfully. ";
        //                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //                objcommon.SetMessageColor(divAlert, "danger");
        //                lblMessage.Text = "Enable Successfully. ";
        //            }
        //        }
        //        else
        //        {

        //            objPmsHr.Flag = "";
        //            objPmsHr.Type = "EMP";
        //            txtFromDate.Enabled = false;
        //            ds = objPmsHr.UpdatedKRAFlagAll();
        //            if (ds.Tables[0].Rows[0]["KRAFLAG"].ToString() == "")
        //            {
        //                EmpChk.Checked = false;
        //                ApprChk.Checked = false;
        //                //dnyaneshwar commented       lblEmpEnDiS.Text = "Enable";
        //                lblApprEnDiS.Text = "Enable";
        //                //dnyaneshwar commented      lblEmpEnDiS.ForeColor = System.Drawing.Color.Green;
        //                lblApprEnDiS.ForeColor = System.Drawing.Color.Green;

        //                objcommon = new NewPortal2023.ESS.Common();
        //                divAlert.Visible = true;

        //                objPmsHr.ToDate = "";
        //                objPmsHr.Type = "EMP";
        //                objPmsHr.Comp_Aid = Session["sCompID"].ToString();
        //                objPmsHr.EmpCode = Session["sEmpCode"].ToString();

        //                ds = objPmsHr.UpdateDateKRAFlag();

        //                txtFromDate.Text = "";
        //                txtFromDate.Enabled = false;

        //                string message = "Disable Successfully. ";
        //                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //                objcommon.SetMessageColor(divAlert, "danger");
        //                lblMessage.Text = "Disabled Successfully. ";
        //            }
        //            else
        //            {
        //                EmpChk.Checked = false;
        //                ApprChk.Checked = false;
        //                //dnyaneshwar commented             lblEmpEnDiS.Text = "Enable";
        //                lblApprEnDiS.Text = "Enable";
        //                //dnyaneshwar commented          lblEmpEnDiS.ForeColor = System.Drawing.Color.Green;
        //                lblApprEnDiS.ForeColor = System.Drawing.Color.Green;

        //                objcommon = new NewPortal2023.ESS.Common();
        //                divAlert.Visible = true;

        //                objPmsHr.ToDate = "";
        //                objPmsHr.Type = "APPR";
        //                objPmsHr.Comp_Aid = Session["sCompID"].ToString();
        //                objPmsHr.EmpCode = Session["sEmpCode"].ToString();

        //                ds = objPmsHr.UpdateDateKRAFlag();

        //                txtFromDate.Text = "";
        //                txtFromDate.Enabled = false;

        //                string message = "Disable Successfully. ";
        //                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //                objcommon.SetMessageColor(divAlert, "danger");
        //                lblMessage.Text = "Disabled Successfully. ";
        //            }
        //        }
        //        //objPmsHr.KRA_Activation_Flag = "";
        //        getList();
        //    }
        //    catch (Exception ex)
        //    {
        //        objcommon = new NewPortal2023.ESS.Common();
        //        divAlert.Visible = true;
        //        objcommon.SetMessageColor(divAlert, "danger");
        //        lblMessage.Text = ex.Message;
        //    }
        //}

        //protected void ApprChk_CheckedChanged(object sender, EventArgs e)
        //{
        //    ds = new DataSet();
        //    objPmsHr = new NewPortal2023.ESS.PmsHr();
        //    try
        //    {
        //        if (ApprChk.Checked == true)
        //        {
        //            //lblApprEnDiS.Visible = false;
        //            txtToDate.Enabled = true;

        //            objPmsHr.Flag = "1";
        //            objPmsHr.Type = "APPR";

        //            ds = objPmsHr.UpdatedKRAFlagAll();

        //            if (ds.Tables[0].Rows[0]["KRAFLAG"].ToString() == "1")
        //            {
        //                ApprChk.Checked = true;
        //                lblApprEnDiS.Text = "Enable";
        //                lblApprEnDiS.ForeColor = System.Drawing.Color.Red;
        //                objcommon = new NewPortal2023.ESS.Common();
        //                divAlert.Visible = true;

        //                EmpChk.Checked = false;
        //                txtFromDate.Enabled = false;
        //                txtFromDate.Text = "";
        //                objPmsHr.ToDate = "";
        //                objPmsHr.Type = "EMP";
        //                objPmsHr.Comp_Aid = Session["sCompID"].ToString();
        //                objPmsHr.EmpCode = Session["sEmpCode"].ToString();

        //                ds = objPmsHr.UpdateDateKRAFlag();

        //                string message = "Enable Successfully. ";
        //                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //                objcommon.SetMessageColor(divAlert, "success");
        //                lblMessage.Text = "Enable Successfully. ";
        //            }
        //            else
        //            {
        //                ApprChk.Checked = false;
        //                lblApprEnDiS.Text = "Disabled";
        //                lblApprEnDiS.ForeColor = System.Drawing.Color.Green;

        //                objcommon = new NewPortal2023.ESS.Common();
        //                divAlert.Visible = true;

        //                string message = "Disabled Successfully. ";
        //                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //                objcommon.SetMessageColor(divAlert, "success");
        //                lblMessage.Text = "Disabled Successfully. ";
        //            }

        //        }
        //        else
        //        {

        //            objPmsHr.Type = "APPR   ";
        //            objPmsHr.Flag = "2";
        //            txtToDate.Enabled = false;

        //            ds = objPmsHr.UpdatedKRAFlagAll();

        //            if (ds.Tables[0].Rows[0]["KRAFLAG"].ToString() == "2")
        //            {
        //                ApprChk.Checked = false;
        //                lblApprEnDiS.Text = "Enable";
        //                lblApprEnDiS.ForeColor = System.Drawing.Color.Green;

        //                objcommon = new NewPortal2023.ESS.Common();
        //                divAlert.Visible = true;

        //                objPmsHr.KRA_Activation_Flag = "2";

        //                objPmsHr.ToDate = "";
        //                objPmsHr.Type = "APPR";
        //                objPmsHr.Comp_Aid = Session["sCompID"].ToString();
        //                objPmsHr.EmpCode = Session["sEmpCode"].ToString();

        //                ds = objPmsHr.UpdateDateKRAFlag();

        //                txtToDate.Text = "";
        //                txtToDate.Enabled = false;

        //                string message = "Disabled Successfully. ";
        //                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //                objcommon.SetMessageColor(divAlert, "danger");
        //                lblMessage.Text = "Disabled Successfully. ";
        //            }
        //            else
        //            {
        //                ApprChk.Checked = false;
        //                lblApprEnDiS.Text = "Enable";
        //                lblApprEnDiS.ForeColor = System.Drawing.Color.Green;

        //                objcommon = new NewPortal2023.ESS.Common();
        //                divAlert.Visible = true;

        //                objPmsHr.ToDate = "";
        //                objPmsHr.Type = "EMP";
        //                objPmsHr.Comp_Aid = Session["sCompID"].ToString();
        //                objPmsHr.EmpCode = Session["sEmpCode"].ToString();

        //                ds = objPmsHr.UpdateDateKRAFlag();

        //                txtToDate.Text = "";
        //                txtToDate.Enabled = false;

        //                string message = "Disabled Successfully. ";
        //                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //                objcommon.SetMessageColor(divAlert, "danger");
        //                lblMessage.Text = "Disabled Successfully. ";
        //            }
        //        }
        //        getList();
        //    }

        //    catch (Exception ex)
        //    {
        //        objcommon = new NewPortal2023.ESS.Common();
        //        divAlert.Visible = true;
        //        objcommon.SetMessageColor(divAlert, "danger");
        //        lblMessage.Text = ex.Message;
        //    }

        //}

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;

            ds = new DataSet();
            objPmsHr = new NewPortal2023.ESS.PmsHr();
            try
            {
                CheckBox chckheader = (CheckBox)gvList.HeaderRow.FindControl("chkSelectAll");
                foreach (GridViewRow row in gvList.Rows)
                {
                    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    if (chckrw.Checked == true)
                    {
                        chckrw.Checked = true;
                        count = 1;
                    }

                }

                if (count == 1)
                {
                    if (drActionAll.SelectedValue == "")
                    {

                        //divAlert.Visible = true;
                        string script = $@"<script type='text/javascript'>alert('Please Select Action Type.');</script>";
                        //lblMessage.Text = "Please Select Action Type.";
                        //divAlert.Visible = true;
                        //lblMessage.Visible = true;

                    }
                    else
                    {

                        //objPmsHr.Flag = "";
                        //objPmsHr.Type = "EMP";
                        //ds = objPmsHr.UpdateKRAFlagOneEmp(MakeChkXml(gvList), "Enable", (string)Session["sEmpCode"]);
                        ////ds = objPmsHr.UpdatedKRAFlagAll();
                        //if (ds.Tables[0].Rows[0]["KRAFLAG"].ToString() == "")
                        //{
                        //    getList();
                        //    //EmpChk.Checked = false;
                        //    //ApprChk.Checked = false;
                        //    //lblEmpEnDiS.Text = "Enable";
                        //    //lblApprEnDiS.Text = "Enable";
                        //    //lblEmpEnDiS.ForeColor = System.Drawing.Color.Green;
                        //    //lblApprEnDiS.ForeColor = System.Drawing.Color.Green;

                        //    //objcommon = new NewPortal2023.ESS.Common();
                        //    //divAlert.Visible = true;

                        //    //string message = "Enable Successfully. ";
                        //    //string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                        //    //ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        //    //objcommon.SetMessageColor(divAlert, "danger");
                        //    //lblMessage.Text = "Enable Successfully. ";
                        //}
                        //else
                        //{
                        //    getList();
                        //    //EmpChk.Checked = false;
                        //    //ApprChk.Checked = false;
                        //    //lblEmpEnDiS.Text = "Enable";
                        //    //lblApprEnDiS.Text = "Enable";
                        //    //lblEmpEnDiS.ForeColor = System.Drawing.Color.Green;
                        //    //lblApprEnDiS.ForeColor = System.Drawing.Color.Green;

                        //    objcommon = new NewPortal2023.ESS.Common();
                        //    divAlert.Visible = true;

                        //    string message = "Enable Successfully. ";
                        //    string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                        //    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        //    objcommon.SetMessageColor(divAlert, "green");
                        //    lblMessage.Text = "Enable Successfully. ";


                        //}

                    }

                    //MakeChkForEmailXml(gvDomClaim);
                    //ActionGetAll(drp, Rmk);

                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please select at least one checkbox before click on Submit button.";
                    string script = $@"<script type='text/javascript'>alert('Please select at least one checkbox before click on Submit button.');</script>";
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = ex.Message;
            }
        }



        //private string MakeChkXml(GridView gvList)
        //{
        //    StringBuilder sbChkGlCode = new StringBuilder();
        //    StringBuilder sbUnChkGlCode = new StringBuilder();
        //    string xmlChkGlCode = string.Empty;
        //    string xmlUnChkGlCode = string.Empty;

        //    sbChkGlCode.Append("<ROOT>");

        //    foreach (GridViewRow gvr in gvList.Rows)
        //    {
        //        if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
        //        {
        //            sbChkGlCode.Append("<CHKGLSUMBIT CODE='" + ((Label)gvr.FindControl("txtEmpID")).Text.Trim() + "'/>");


        //        }

        //    }
        //    sbChkGlCode.Append("</ROOT>");

        //    xmlChkGlCode = sbChkGlCode.ToString();

        //    return xmlChkGlCode;
        //}

        //protected void btnClose_Click(object sender, EventArgs e)
        //{

        //}

        //protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        CheckBox chckheader = (CheckBox)gvList.HeaderRow.FindControl("chkSelectAll");
        //        foreach (GridViewRow row in gvList.Rows)
        //        {
        //            CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
        //            if (chckheader.Checked == true)
        //            {
        //                chckrw.Checked = true;

        //            }
        //            else
        //            {
        //                chckrw.Checked = false;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //protected void drpAction_SelectedIndexChanged1(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DropDownList lnkBtnOpenFiles = (DropDownList)sender;
        //        Label lblAID = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("txtEmpID");

        //        DropDownList drpactiontype = lnkBtnOpenFiles.NamingContainer.FindControl("drpAction") as DropDownList;
        //        //TextBox remarks = (TextBox)lnkBtnOpenFiles.NamingContainer.FindControl("txtRemarks");
        //        string action = drpactiontype.SelectedItem.Value;
        //        //string remarks = (row.FindControl("txtRemarks") as TextBox).Text;


        //        if (action == "Reporting Manager")
        //        {
        //            objPmsHr.Flag = "1";
        //            objPmsHr.Type = "RM";
        //            objPmsHr.EmpCode = lblAID.Text;
        //            ds = objPmsHr.UpdateKRAFlagOneEmp();
        //            //ds = objPmsHr.UpdatedKRAFlagAll();
        //            if (ds.Tables[0].Rows[0]["KRAFLAG"].ToString() == "1")
        //            {
        //                getList();
        //                objcommon = new NewPortal2023.ESS.Common();
        //                divAlert.Visible = true;
        //                string message = "Rejected KRA To " + action;
        //                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //                objcommon.SetMessageColor(divAlert, "success");
        //                lblMessage.Text = "Rejected KRA To " + action;
        //            }
        //            else
        //            {

        //            }
        //        }
        //        else if (action == "Employee")
        //        {
        //            objPmsHr.Flag = "0";
        //            objPmsHr.Type = "Employee";
        //            objPmsHr.EmpCode = lblAID.Text;
        //            ds = objPmsHr.UpdateKRAFlagOneEmp();
        //            //ds = objPmsHr.UpdatedKRAFlagAll();
        //            if (ds.Tables[0].Rows[0]["KRAFLAG"].ToString() == "")
        //            {
        //                getList();
        //                objcommon = new NewPortal2023.ESS.Common();
        //                divAlert.Visible = true;
        //                string message = "Rejected KRA To " + action;
        //                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //                objcommon.SetMessageColor(divAlert, "success");
        //                lblMessage.Text = "Rejected KRA To " + action;
        //            }
        //            else
        //            {
        //                getList();
        //                objcommon = new NewPortal2023.ESS.Common();
        //                divAlert.Visible = true;
        //                string message = "KRA input is enabled for " + action;
        //                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //                objcommon.SetMessageColor(divAlert, "success");
        //            }
        //        }
        //        else
        //        {
        //            objcommon = new NewPortal2023.ESS.Common();
        //            divAlert.Visible = true;
        //            string message = "Please Select atleast One KRA to Reject. ";
        //            string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //            objcommon.SetMessageColor(divAlert, "danger");
        //            lblMessage.Text = "Please Select atleast One KRA to Reject. ";
        //        }




        //    }
        //    catch (Exception ex)
        //    {
        //        objcommon = new NewPortal2023.ESS.Common();
        //        divAlert.Visible = true;

        //        string message = "Enable Successfully. ";
        //        string script = $@"<script type='text/javascript'>alert('{message}');</script>";
        //        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
        //        objcommon.SetMessageColor(divAlert, "green");
        //        lblMessage.Text = "Enable Successfully. ";

        //    }
        //}

        protected void gvDataPointList_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;

            if ((gv.ShowHeader == true && gv.Rows.Count > 0)
                || (gv.ShowHeaderWhenEmpty == true))
            {
                //Force GridView to use <thead> instead of <tbody> - 11/03/2013 - MCR.
                gv.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (gv.ShowFooter == true && gv.Rows.Count > 0)
            {
                //Force GridView to use <tfoot> instead of <tbody> - 11/03/2013 - MCR.
                gv.FooterRow.TableSection = TableRowSection.TableFooter;
            }
        }


    }
}
