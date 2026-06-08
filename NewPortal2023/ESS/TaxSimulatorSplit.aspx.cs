using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class TaxSimulatorSplit : System.Web.UI.Page
    {
        NewPortal2023.ESS.Tax objTax = new NewPortal2023.ESS.Tax();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataTable dtInv = new DataTable();
        DataSet dsInv = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["sCompID"] != null)
                    {
                        string strResult = objcommon.Validate_ControlInfo("FLEX");

                        if (strResult.Contains("This page is currently unavailable.") == true)
                        {
                            Response.Redirect("Unavailable.aspx?strName=Flexi Pay Details");
                            return;
                        }
                        if ((string)Session["sCompID"] == "CO000114")
                        {
                            lnkTaxSlabManual.Visible = true;
                        }

                        FillCTC();
                        trNP.Visible = false;
                        if ((string)Session["sCompID"] == "CO000056")
                        {
                            divPerq.Visible = true;
                            divBreakup.Visible = true;
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
            lblMessage.Text = "";
            dsInv = objTax.GetTaxAmounts((string)Session["sCompID"], (string)Session["sEmpID"]);

            txtAnnualCTC.Text = dsInv.Tables[0].Rows[0][0].ToString().Replace(".0000", ".00");
            txtPerquisite.Text = (Convert.ToDouble(dsInv.Tables[14].Rows[0][0].ToString()) > 750000
                                  ? Convert.ToDouble(dsInv.Tables[14].Rows[0][0].ToString()) - 750000 : 0).ToString("0.00");
            txtAmountTaxableOld.Text = (Convert.ToDouble(dsInv.Tables[1].Rows[0][0].ToString())
                                        + Convert.ToDouble(txtPerquisite.Text)).ToString("0.00");
            txtAmountTaxfreeOld.Text = dsInv.Tables[2].Rows[0][0].ToString().Replace(".0000", ".00");
            if ((string)Session["sCompID"] == "CO000056")
            {
                if (dsInv.Tables[16].Rows.Count > 0)
                {
                    if (dsInv.Tables[16].Rows[0][0].ToString() == "0")
                    {
                        txtAmountTaxableOld.Text = (Convert.ToDouble(txtAmountTaxableOld.Text) + 2400).ToString("0.00");
                        txtAmountTaxfreeOld.Text = (Convert.ToDouble(txtAmountTaxfreeOld.Text) + 0).ToString("0.00");
                    }
                    else if (dsInv.Tables[16].Rows[0][0].ToString() == "1200")
                    {
                        txtAmountTaxableOld.Text = (Convert.ToDouble(txtAmountTaxableOld.Text) + 1200).ToString("0.00");
                        txtAmountTaxfreeOld.Text = (Convert.ToDouble(txtAmountTaxfreeOld.Text) + 1200).ToString("0.00");
                    }
                    else if (dsInv.Tables[16].Rows[0][0].ToString() == "2400")
                    {
                        txtAmountTaxableOld.Text = (Convert.ToDouble(txtAmountTaxableOld.Text) + 0).ToString("0.00");
                        txtAmountTaxfreeOld.Text = (Convert.ToDouble(txtAmountTaxfreeOld.Text) + 2400).ToString("0.00");
                    }
                }
                else
                {
                    txtAmountTaxableOld.Text = (Convert.ToDouble(txtAmountTaxableOld.Text) + 2400).ToString("0.00");
                    txtAmountTaxfreeOld.Text = (Convert.ToDouble(txtAmountTaxfreeOld.Text) + 0).ToString("0.00");
                }
            }
            txtAmountTaxableNew.Text = (Convert.ToDouble(dsInv.Tables[3].Rows[0][0].ToString())
                                        + Convert.ToDouble(txtPerquisite.Text)).ToString("0.00");
            txtAmountTaxfreeNew.Text = dsInv.Tables[4].Rows[0][0].ToString().Replace(".0000", ".00");
            hdnBasic.Value = dsInv.Tables[5].Rows[0][0].ToString().Replace(".0000", ".00");
            hdnHRA.Value = dsInv.Tables[6].Rows[0][0].ToString().Replace(".0000", ".00");
            txtRent.Text = dsInv.Tables[7].Rows[0][0].ToString().Replace(".0000", ".00");
            txt80C.Text = dsInv.Tables[8].Rows[0][0].ToString().Replace(".0000", ".00");
            txt80CEx.Text = (Convert.ToDouble(dsInv.Tables[8].Rows[0][0].ToString()) > 150000 ? 150000 : Convert.ToDouble(dsInv.Tables[8].Rows[0][0].ToString())).ToString("0.00");
            txtHousingInterest.Text = dsInv.Tables[9].Rows[0][0].ToString().Replace(".0000", ".00");
            txtHousingInterestAbs.Text = (Convert.ToDouble(dsInv.Tables[9].Rows[0][0].ToString()) < -200000 ? -200000 : Convert.ToDouble(dsInv.Tables[9].Rows[0][0].ToString())).ToString("0.00");
            double sumSelf = Math.Min(Convert.ToDouble(dsInv.Tables[11].Rows[0][0].ToString()), 25000);
            double sumParents = Math.Min(Convert.ToDouble(dsInv.Tables[12].Rows[0][0].ToString()), 50000);
            txtTotalInvExemption.Text = (sumSelf + sumParents).ToString("0.00");
            txtNPS.Text = (Convert.ToDouble(dsInv.Tables[13].Rows[0][0].ToString())).ToString("0.00");
            txtOtherExemption.Text = (Convert.ToDouble(dsInv.Tables[10].Rows[0][0].ToString())).ToString("0.00");
            //if ((string)Session["sEmpID"] == "EP011527")
            //{
            //    //if (!string.IsNullOrEmpty(dsInv.Tables[25].Rows[0][0].ToString()))
            //    //{
            //    txtSalOthInc.Text = "155030.00"; //(Convert.ToDouble(dsInv.Tables[23].Rows[0][0].ToString())).ToString("0.00");
            //    //}
            //    //else
            //    //{
            //    //    txtSalOthInc.Text = "0.00";
            //    //}
            //    if (!string.IsNullOrEmpty(dsInv.Tables[23].Rows[0][0].ToString()))
            //    {
            //        txtOthInc.Text = (Convert.ToDouble(dsInv.Tables[23].Rows[0][0].ToString())).ToString("0.00");
            //    }
            //    else
            //    {
            //        txtOthInc.Text = "0.00";
            //    }
            //    trOthInc.Visible = true;
            //    trSalOthInc.Visible = true;
            //}
            //else
            //{
            //    trOthInc.Visible = false;
            //    trSalOthInc.Visible = false;
            //}
            if (!string.IsNullOrEmpty(dsInv.Tables[25].Rows[0][0].ToString()))
            {
                txtSalOthInc.Text = (Convert.ToDouble(dsInv.Tables[25].Rows[0][0].ToString())).ToString("0.00");

            }
            else
            {
                txtSalOthInc.Text = "0.00";
            }
            if (!string.IsNullOrEmpty(dsInv.Tables[23].Rows[0][0].ToString()))
            {
                txtOthInc.Text = (Convert.ToDouble(dsInv.Tables[23].Rows[0][0].ToString())).ToString("0.00");
            }
            else
            {
                txtOthInc.Text = "0.00";
            }
            if (dsInv.Tables[15].Rows[0][0].ToString() == "O")
            {
                rdbOld.Checked = true;
            }
            else if (dsInv.Tables[15].Rows[0][0].ToString() == "N")
            {
                rdbNew.Checked = true;
            }
            else
            {
                rdbOld.Checked = false;
                rdbNew.Checked = false;
            }

            if ((string)Session["sCompID"] == "CO000114")
            {
                if (dsInv.Tables[21].Rows.Count > 0)
                {
                    string pfSum = dsInv.Tables[21].Rows[0][0].ToString().Replace(".0000", ".00");

                    if (Convert.ToDouble(pfSum) > 0)
                    {
                        double pfValue = Convert.ToDouble(pfSum);

                        txt80C.Text = (Math.Round(Convert.ToDouble(dsInv.Tables[8].Rows[0][0].ToString()) + pfValue)).ToString("0.00");
                        txt80CEx.Text = (Convert.ToDouble(txt80C.Text.Trim()) > 150000 ? 150000 : Convert.ToDouble(txt80C.Text.Trim())).ToString("0.00");
                    }
                }
            }
        }

        protected string ReplaceSpecialCharacters(string inputString)
        {
            inputString = inputString.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&#39;").
                    Replace("\"", "&quot;");

            return inputString;
        }

        protected void btnCalculateRent_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                CalculateTax();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void CalculateRentExemption()
        {
            double rent = 0;
            //TextBox txtRentExemption = new TextBox();

            if (txtRent.Text.Trim() != "" && txtRent.Text.Trim() != "0" && txtRent.Text.Trim() != "0.00")
            {
                if (double.TryParse(txtRent.Text, out rent))
                {
                    if (drpRent.SelectedValue != "0")
                    {
                        dsInv = objTax.GetTaxAmounts((string)Session["sCompID"], (string)Session["sEmpID"]);
                        double basic = Convert.ToDouble(dsInv.Tables[5].Rows[0][0].ToString().Replace(".0000", ".00"));
                        double HRA = Convert.ToDouble(dsInv.Tables[6].Rows[0][0].ToString().Replace(".0000", ".00"));
                        rent = Convert.ToDouble(txtRent.Text);
                        string location = drpRent.SelectedValue;
                        double basic40 = basic * 0.4;
                        double basic50 = basic * 0.5;
                        double rent10 = rent - (basic * 0.1);
                        double exemption = 0;

                        if (location == "1")
                        {
                            exemption = Math.Min(basic50, Math.Min(HRA, rent10));
                        }
                        else if (location == "2")
                        {
                            exemption = Math.Min(basic40, Math.Min(HRA, rent10));
                        }

                        exemption = exemption < 0 ? 0 : exemption;
                        txtHRAExemption.Text = exemption.ToString("0.00");
                        txtRentExemption.Text = txtHRAExemption.Text;
                    }
                    else
                    {
                        lblMessage.Text = "Select Metro/Non-Metro.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Select Metro/Non-Metro.');");
                    }
                }
                else
                {
                    txtRent.Text = "";
                    lblMessage.Text = "Enter valid rent amount.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Enter valid rent amount.');");
                }
            }
            else
            {
                txtHRAExemption.Text = "0.00";
                txtRentExemption.Text = "0.00";
            }
        }

        protected void btnCalculateHousingInterest_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                CalculateTax();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void CalculateHousingInterest()
        {
            double interest = 0;
            if (txtHousingInterest.Text.Trim() != "")
            {
                if (double.TryParse(txtHousingInterest.Text, out interest))
                {
                    //interest = Math.Abs(Convert.ToDouble(txtHousingInterest.Text));
                    //interest = interest > 200000 ? 200000 : interest;
                    //interest = interest * -1;
                    if (Convert.ToDouble(txtHousingInterest.Text) < -200000)
                    {
                        interest = -200000;
                    }
                    else
                    {
                        interest = Convert.ToDouble(txtHousingInterest.Text);
                    }
                    txtHousingInterestAbs.Text = interest.ToString("0.00");
                }
                else
                {
                    txtHousingInterest.Text = "";
                    txtHousingInterestAbs.Text = "";
                    lblMessage.Text = "Enter valid housing interest amount.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Enter valid housing interest amount.');");
                }
            }
            else
            {
                txtHousingInterest.Text = "0.00";
                txtHousingInterestAbs.Text = "0.00";
            }
        }

        protected void btnLetOutIncome_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                CalculateTax();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        //private void CalculateLetOutIncome()
        //{
        //    double interest = 0;
        //    if (txtLetOutIncome.Text.Trim() != "")
        //    {
        //        if (double.TryParse(txtLetOutIncome.Text, out interest) == false)
        //        {
        //            txtLetOutIncome.Text = "";
        //            lblMessage.Text = "Enter valid for let out property income.";
        //            objcommon.Display("Validate", "DisplayErrorMessage('Enter valid for let out property income.');");
        //        }
        //    }
        //    else
        //    {
        //        txtLetOutIncome.Text = "0.00";
        //    }
        //}
        //-------OTHER SALARY-------///
        private void CalculateGrossTotalIncome()
        {
            txtTotalGrossIncome.Text = (Convert.ToDouble(txtAmountTaxableOld.Text) - Convert.ToDouble(txtHRAExemption.Text) - Convert.ToDouble(txtStandardDeduction.Text)
                                            + Convert.ToDouble(txtHousingInterestAbs.Text)).ToString("0.00"); // + Convert.ToDouble(txtLetOutIncome.Text)).ToString("0.00");
        }

        private void CalculateTotalTaxableIncome()
        {
            txtTotalTaxableIncome.Text = (Convert.ToDouble(txtTotalGrossIncome.Text) - Convert.ToDouble(txtTotalInvExemption.Text.Trim() == "" ? "0" : txtTotalInvExemption.Text.Trim())
                                            - Convert.ToDouble(txt80CEx.Text.Trim() == "" ? "0" : txt80CEx.Text.Trim())
                                            - Convert.ToDouble(txtNPS.Text.Trim() == "" ? "0" : txtNPS.Text.Trim())
                                             - Convert.ToDouble(txtOtherExemption.Text.Trim() == "" ? "0" : txtOtherExemption.Text.Trim()) + Convert.ToDouble(txtSalOthInc.Text) + Convert.ToDouble(txtOthInc.Text)).ToString("0.00");
        }

        private void CalculateTotalTaxableIncomeNew()
        {
            txtTotalTaxableIncomeNew.Text = (Convert.ToDouble(txtAmountTaxableNew.Text.Trim()) + Convert.ToDouble(txtSalOthInc.Text) + Convert.ToDouble(txtOthInc.Text)).ToString("0.00");
        }

        //private void CalculateTotalTaxPayable()
        //{
        //    double taxable = Convert.ToDouble(txtTotalTaxableIncome.Text.Trim() == "" ? "0" : txtTotalTaxableIncome.Text.Trim());
        //    if (taxable <= 300000)
        //    {
        //        txtTaxOnIncome.Text = "0.00";
        //    }
        //    else if (taxable <= 350000)
        //    {
        //        txtTaxOnIncome.Text = Math.Round(((taxable - 300000) * 0.05)).ToString("0.00");
        //    }
        //    else if (taxable <= 500000)
        //    {
        //        txtTaxOnIncome.Text = Math.Round(((taxable - 250000) * 0.05)).ToString("0.00");
        //    }
        //    else if (taxable <= 1000000)
        //    {
        //        txtTaxOnIncome.Text = Math.Round((((taxable - 500000) * 0.2) + 12500)).ToString("0.00");
        //    }
        //    else
        //    {
        //        txtTaxOnIncome.Text = Math.Round((((taxable - 1000000) * 0.3) + 112500)).ToString("0.00");
        //    }
        //}

        private void CalculateTotalTaxBreakUp()
        {
            objTax = new NewPortal2023.ESS.Tax();
            DataSet ds;
            ds = objTax.CalculateTaxBreakupAlt((string)Session["sCompID"], "20202021", (string)Session["sEmpSex"], txtTotalTaxableIncome.Text.Trim() == "" ? "0" : txtTotalTaxableIncome.Text.Trim(), txtTotalTaxableIncomeNew.Text.Trim() == "" ? "0" : txtTotalTaxableIncomeNew.Text.Trim());
            if (ds.Tables.Count > 0)
            {
                gvCTC.DataSource = ds.Tables[0];
                gvCTC.DataBind();
            }
        }

        private void CalculateTotalTaxPayable()
        {
            objTax = new NewPortal2023.ESS.Tax();
            DataSet ds;
            ds = objTax.CalculateTax((string)Session["sCompID"], "20202021", (string)Session["sEmpSex"], txtTotalTaxableIncome.Text.Trim() == "" ? "0" : txtTotalTaxableIncome.Text.Trim());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTaxOnIncome.Text = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString())).ToString("0.00");
                }
            }
        }

        private void CalculateTotalTaxPayableNew()
        {
            objTax = new NewPortal2023.ESS.Tax();
            DataSet ds;
            ds = objTax.CalculateTaxNew((string)Session["sCompID"], "20202021", (string)Session["sEmpSex"], txtTotalTaxableIncomeNew.Text.Trim() == "" ? "0" : txtTotalTaxableIncomeNew.Text.Trim());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtTaxOnIncomeNew.Text = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString())).ToString("0.00");
                }
            }
        }

        private void CalculateTaxCredit()
        {
            double income = Convert.ToDouble(txtTotalTaxableIncome.Text.Trim() == "" ? "0" : txtTotalTaxableIncome.Text.Trim());

            if (income <= 500000)
            {
                txtTaxCredit.Text = Math.Round((Math.Min(Convert.ToDouble(txtTaxOnIncome.Text.Trim() == "" ? "0" : txtTaxOnIncome.Text.Trim()), 12500))).ToString("0.00");
            }
            else
            {
                txtTaxCredit.Text = "0.00";
            }
        }

        private void CalculateTaxCreditNew()
        {
            double income = Convert.ToDouble(txtTotalTaxableIncomeNew.Text.Trim() == "" ? "0" : txtTotalTaxableIncomeNew.Text.Trim());

            if (income <= 700000)
            {
                txtTaxCreditNew.Text = Math.Round((Math.Min(Convert.ToDouble(txtTaxOnIncomeNew.Text.Trim() == "" ? "0" : txtTaxOnIncomeNew.Text.Trim()), 25000))).ToString("0.00");
            }
            else
            {
                txtTaxCreditNew.Text = "0.00";
            }
        }

        //private void CalculateSurcharge()
        //{
        //    double tax = Convert.ToDouble(txtTaxOnIncome.Text.Trim() == "" ? "0" : txtTaxOnIncome.Text.Trim());
        //    double credit = Convert.ToDouble(txtTaxCredit.Text.Trim() == "" ? "0" : txtTaxCredit.Text.Trim());

        //    if (tax > 5000000)
        //    {
        //        txtSurcharge.Text = Math.Round(((tax - credit) * 0.1)).ToString("0.00");
        //    }
        //    else if (tax > 10000000)
        //    {
        //        txtSurcharge.Text = Math.Round(((tax - credit) * 0.15)).ToString("0.00");
        //    }
        //    else
        //    {
        //        txtSurcharge.Text = "0.00";
        //    }
        //}

        private void CalculateSurcharge()
        {
            double tax = Convert.ToDouble(txtTaxOnIncome.Text.Trim() == "" ? "0" : txtTaxOnIncome.Text.Trim());
            double credit = Convert.ToDouble(txtTaxCredit.Text.Trim() == "" ? "0" : txtTaxCredit.Text.Trim());
            objTax = new NewPortal2023.ESS.Tax();
            DataSet ds;
            ds = objTax.CalculateSurcharge((string)Session["sCompID"], "20202021", (string)Session["sEmpSex"],
                        txtTotalTaxableIncome.Text.Trim() == "" ? "0" : txtTotalTaxableIncome.Text.Trim(), (tax - credit).ToString());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtSurcharge.Text = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString())).ToString("0.00");
                }
            }
        }

        private void CalculateSurchargeNew()
        {
            double tax = Convert.ToDouble(txtTaxOnIncomeNew.Text.Trim() == "" ? "0" : txtTaxOnIncomeNew.Text.Trim());
            double credit = Convert.ToDouble(txtTaxCreditNew.Text.Trim() == "" ? "0" : txtTaxCreditNew.Text.Trim());
            objTax = new NewPortal2023.ESS.Tax();
            DataSet ds;
            ds = objTax.CalculateSurcharge((string)Session["sCompID"], "20202021", (string)Session["sEmpSex"],
                        txtTotalTaxableIncomeNew.Text.Trim() == "" ? "0" : txtTotalTaxableIncomeNew.Text.Trim(), (tax - credit).ToString());
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtSurchargeNew.Text = Math.Round(Convert.ToDouble(ds.Tables[0].Rows[0][0].ToString())).ToString("0.00");
                }
            }
        }

        private void CalculateEducationCess()
        {
            double tax = Convert.ToDouble(txtTaxOnIncome.Text.Trim() == "" ? "0" : txtTaxOnIncome.Text.Trim());
            double credit = Convert.ToDouble(txtTaxCredit.Text.Trim() == "" ? "0" : txtTaxCredit.Text.Trim());
            double surcharge = Convert.ToDouble(txtSurcharge.Text.Trim() == "" ? "0" : txtSurcharge.Text.Trim());

            double cess = (tax - credit + surcharge) * 0.04;
            txtEduCess.Text = Math.Round(cess).ToString("0.00");
        }

        private void CalculateEducationCessNew()
        {
            double tax = Convert.ToDouble(txtTaxOnIncomeNew.Text.Trim() == "" ? "0" : txtTaxOnIncomeNew.Text.Trim());
            double credit = Convert.ToDouble(txtTaxCreditNew.Text.Trim() == "" ? "0" : txtTaxCreditNew.Text.Trim());
            double surcharge = Convert.ToDouble(txtSurchargeNew.Text.Trim() == "" ? "0" : txtSurchargeNew.Text.Trim());

            double cess = (tax - credit + surcharge) * 0.04;
            txtEduCessNew.Text = Math.Round(cess).ToString("0.00");
        }

        private void CalculateAnnualTax()
        {
            double tax = Convert.ToDouble(txtTaxOnIncome.Text.Trim() == "" ? "0" : txtTaxOnIncome.Text.Trim());
            double credit = Convert.ToDouble(txtTaxCredit.Text.Trim() == "" ? "0" : txtTaxCredit.Text.Trim());
            double surcharge = Convert.ToDouble(txtSurcharge.Text.Trim() == "" ? "0" : txtSurcharge.Text.Trim());
            double cess = Convert.ToDouble(txtEduCess.Text.Trim() == "" ? "0" : txtEduCess.Text.Trim());

            txtAnnualTax.Text = Math.Round(tax - credit + surcharge + cess).ToString("0.00");
            txtMonthlyTax.Text = (Math.Ceiling((tax - credit + surcharge + cess) / 12)).ToString("0.00");
        }

        private void CalculateAnnualTaxNew()
        {
            double tax = Convert.ToDouble(txtTaxOnIncomeNew.Text.Trim() == "" ? "0" : txtTaxOnIncomeNew.Text.Trim());
            double credit = Convert.ToDouble(txtTaxCreditNew.Text.Trim() == "" ? "0" : txtTaxCreditNew.Text.Trim());
            double surcharge = Convert.ToDouble(txtSurchargeNew.Text.Trim() == "" ? "0" : txtSurchargeNew.Text.Trim());
            double cess = Convert.ToDouble(txtEduCessNew.Text.Trim() == "" ? "0" : txtEduCessNew.Text.Trim());

            txtAnnualTaxNew.Text = Math.Round(tax - credit + surcharge + cess).ToString("0.00");
            txtMonthlyTaxNew.Text = (Math.Ceiling((tax - credit + surcharge + cess) / 12)).ToString("0.00");
        }

        private void CalculateTax()
        {
            CalculateRentExemption();
            CalculateHousingInterest();
            CalculateGrossTotalIncome();
            CalculateTotalTaxableIncome();
            CalculateTotalTaxPayable();
            CalculateTaxCredit();
            CalculateSurcharge();
            CalculateEducationCess();
            CalculateAnnualTax();

            CalculateTotalTaxableIncomeNew();
            CalculateTotalTaxPayableNew();
            CalculateTaxCreditNew();
            CalculateSurchargeNew();
            CalculateEducationCessNew();
            CalculateAnnualTaxNew();

            CalculateTotalTaxBreakUp();
        }

        protected void btnCalculateTax_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtRent.Text.Trim() != "" && txtRent.Text.Trim() != "0" && txtRent.Text.Trim() != "0.00")
                {
                    if (drpRent.SelectedValue == "0")
                    {
                        lblMessage.Text = "Select Metro/Non-Metro.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Select Metro/Non-Metro.');");
                        return;
                    }
                }

                lblMessage.Text = "";
                CalculateTax();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "";
            }
        }

        protected void btnSaveOption_Click(object sender, EventArgs e)
        {
            try
            {
                if (!rdbOld.Checked && !rdbNew.Checked)
                {
                    lblMessage.Text = "Select Tax Option.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Select Tax Option.');");
                }
                else
                {
                    if (rdbOld.Checked)
                    {
                        objTax.SaveTaxOption((string)Session["sCompID"], (string)Session["sEmpID"], "O");
                    }
                    else if (rdbNew.Checked)
                    {
                        objTax.SaveTaxOption((string)Session["sCompID"], (string)Session["sEmpID"], "N");
                    }

                    lblMessage.Text = "Selected Tax Option saved successfully.";
                    objcommon.Display("Validate", "DisplayErrorMessage('Selected Tax Option saved successfully.');");
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
        protected void lnkTaxSlabManual_Click(object sender, EventArgs e)
        {
            if ((string)Session["sCompID"] == "CO000114")
            {
                System.IO.FileInfo fileObj = new System.IO.FileInfo(Server.MapPath("~/downloads/angel/manual.pdf"));
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=manual.pdf");
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
            }
        }
    }
}