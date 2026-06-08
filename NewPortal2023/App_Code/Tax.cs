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

namespace NewPortal2023.ESS
{
    public class Tax
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        public Tax()
        {

        }

        public DataSet GetCTCDetails(string compValue, string empValue, string empCode, TextBox txtCTC)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCTC");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            if (dsInv.Tables.Count > 0)
            {
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                    {
                        txtCTC.Text = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["CTC_AMT"].ToString().Trim());
                    }
                }
            }

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetCTCDetailsCustom(string compValue, string empValue, string empCode, TextBox txtCTC, string empGrade)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
            objDBUtility.AddParameters("@EMP_GRADE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empGrade.ToString().Trim());
            objDBUtility.AddParameters("@CTC_CUSTOM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, txtCTC.Text.Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCTCCUSTOM");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            if (dsInv.Tables.Count > 0)
            {
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                    {
                        txtCTC.Text = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["CTC_AMT"].ToString().Trim());
                    }
                }
            }
            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetTaxAmounts(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTAXAMOUNTS");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet CalculateTax(string compValue, string accYear, string sex, string amount)
        {
            objDBUtility = new DBUtility();

            DataSet ds = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@ACC_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, accYear.ToString().Trim());
            objDBUtility.AddParameters("@SEX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, sex.ToString().Trim());
            objDBUtility.AddParameters("@INCOME_AMOUNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, amount.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CALCULATETAX");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet CalculateTaxNew(string compValue, string accYear, string sex, string amount)
        {
            objDBUtility = new DBUtility();

            DataSet ds = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@ACC_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, accYear.ToString().Trim());
            objDBUtility.AddParameters("@SEX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, sex.ToString().Trim());
            objDBUtility.AddParameters("@INCOME_AMOUNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, amount.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CALCULATETAXNEW");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet CalculateTaxBreakup(string compValue, string accYear, string sex, string amount, string amountNew)
        {
            objDBUtility = new DBUtility();

            DataSet ds = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@ACC_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, accYear.ToString().Trim());
            objDBUtility.AddParameters("@SEX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, sex.ToString().Trim());
            objDBUtility.AddParameters("@INCOME_AMOUNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, amount.ToString().Trim());
            objDBUtility.AddParameters("@INCOME_AMOUNT_NEW", DBUtilDBType.Varchar, DBUtilDirection.In, 50, amountNew.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CALCULATETAXBREAKUP");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet CalculateTaxBreakupAlt(string compValue, string accYear, string sex, string amount, string amountNew)
        {
            objDBUtility = new DBUtility();

            DataSet ds = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@ACC_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, accYear.ToString().Trim());
            objDBUtility.AddParameters("@SEX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, sex.ToString().Trim());
            objDBUtility.AddParameters("@INCOME_AMOUNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, amount.ToString().Trim());
            objDBUtility.AddParameters("@INCOME_AMOUNT_NEW", DBUtilDBType.Varchar, DBUtilDirection.In, 50, amountNew.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CALCULATETAXBREAKUPALT");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet CalculateSurcharge(string compValue, string accYear, string sex, string amount, string tax)
        {
            objDBUtility = new DBUtility();

            DataSet ds = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@ACC_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, accYear.ToString().Trim());
            objDBUtility.AddParameters("@SEX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, sex.ToString().Trim());
            objDBUtility.AddParameters("@INCOME_AMOUNT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, amount.ToString().Trim());
            objDBUtility.AddParameters("@TAX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, tax.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CALCULATESURCHARGE");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public void SaveTaxOption(string compValue, string empValue, string taxOption)
        {
            objDBUtility = new DBUtility();

            DataSet ds = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@TAX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, taxOption.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "SAVETAXOPTION");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();
        }

        public DataSet CreateITForm(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet ds = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAMEFIRST", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpNameFirst.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAMEMIDDLE", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpNameMiddle.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAMELAST", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpNameLast.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAMEFIRSTFATHER", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpNameFirstFather.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAMEMIDDLEFATHER", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpNameMiddleFather.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAMELASTFATHER", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpNameLastFather.ToString().Trim());
            objDBUtility.AddParameters("@BIRTHDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BirthDate.ToString().Trim());
            objDBUtility.AddParameters("@GENDER", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Gender.ToString().Trim());
            objDBUtility.AddParameters("@ADDRESS", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Address.ToString().Trim());
            objDBUtility.AddParameters("@STATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, State.ToString().Trim());
            objDBUtility.AddParameters("@PINCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PinCode.ToString().Trim());
            objDBUtility.AddParameters("@MOBILENO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, MobileNo.ToString().Trim());
            objDBUtility.AddParameters("@OFFICENO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OfficeNo.ToString().Trim());
            objDBUtility.AddParameters("@EMAILID", DBUtilDBType.Varchar, DBUtilDirection.In, 255, EmailId.ToString().Trim());
            objDBUtility.AddParameters("@EMPLOYERNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmployerName.ToString().Trim());
            objDBUtility.AddParameters("@LOCATION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Location.ToString().Trim());
            objDBUtility.AddParameters("@EFILINGUSERID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EfilingUserId.ToString().Trim());
            objDBUtility.AddParameters("@EFILINGPASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EfilingPassword.ToString().Trim());
            objDBUtility.AddParameters("@AADHAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AADHAR.ToString().Trim());
            objDBUtility.AddParameters("@ISPARTNER", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsPartner.ToString().Trim());
            objDBUtility.AddParameters("@PARTNERDETAILS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PartnerDetails.ToString().Trim());
            objDBUtility.AddParameters("@ISSHARES", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsShares.ToString().Trim());
            objDBUtility.AddParameters("@SHARESDETAILS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, SharesDetails.ToString().Trim());
            objDBUtility.AddParameters("@ISDIRECTOR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsDirector.ToString().Trim());
            objDBUtility.AddParameters("@DIRECTORDETAILS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DirectorDetails.ToString().Trim());
            objDBUtility.AddParameters("@ISPROPERTY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsProperty.ToString().Trim());
            objDBUtility.AddParameters("@PROPERTYDETAILS", DBUtilDBType.Varchar, DBUtilDirection.In, 100, PropertyDetails.ToString().Trim());
            objDBUtility.AddParameters("@ISAGGREGATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsAggregate.ToString().Trim());
            objDBUtility.AddParameters("@AGGREGATEAMOUNT", DBUtilDBType.Varchar, DBUtilDirection.In, 100, AggregateAmount.ToString().Trim());
            objDBUtility.AddParameters("@ISEXPENDITURE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsExpenditure.ToString().Trim());
            objDBUtility.AddParameters("@EXPENDITUREAMOUNT", DBUtilDBType.Varchar, DBUtilDirection.In, 100, ExpenditureAmount.ToString().Trim());
            objDBUtility.AddParameters("@ISELECTRICAL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsElectrical.ToString().Trim());
            objDBUtility.AddParameters("@ELECTRICALAMOUNT", DBUtilDBType.Varchar, DBUtilDirection.In, 100, ElectricalAmount.ToString().Trim());
            objDBUtility.AddParameters("@ACC1BANK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account1BankName.ToString().Trim());
            objDBUtility.AddParameters("@ACC1ACCNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account1AccNo.ToString().Trim());
            objDBUtility.AddParameters("@ACC1MICR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account1MICR.ToString().Trim());
            objDBUtility.AddParameters("@ACC1IFSC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account1IFSC.ToString().Trim());
            objDBUtility.AddParameters("@ACC2BANK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account2BankName.ToString().Trim());
            objDBUtility.AddParameters("@ACC2ACCNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account2AccNo.ToString().Trim());
            objDBUtility.AddParameters("@ACC2MICR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account2MICR.ToString().Trim());
            objDBUtility.AddParameters("@ACC2IFSC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account2IFSC.ToString().Trim());
            objDBUtility.AddParameters("@ACC3BANK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account3BankName.ToString().Trim());
            objDBUtility.AddParameters("@ACC3ACCNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account3AccNo.ToString().Trim());
            objDBUtility.AddParameters("@ACC3MICR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account3MICR.ToString().Trim());
            objDBUtility.AddParameters("@ACC3IFSC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account3IFSC.ToString().Trim());
            objDBUtility.AddParameters("@INTERESTSAVINGS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, InterestSavings.ToString().Trim());
            objDBUtility.AddParameters("@INTERESTFD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, InterestFD.ToString().Trim());
            objDBUtility.AddParameters("@OTHERINCOME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OtherIncome.ToString().Trim());
            objDBUtility.AddParameters("@REFRANDOM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, RefRandom.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CREATEITFORM");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet UpdateITForm(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet ds = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAMEFIRST", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpNameFirst.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAMEMIDDLE", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpNameMiddle.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAMELAST", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpNameLast.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAMEFIRSTFATHER", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpNameFirstFather.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAMEMIDDLEFATHER", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpNameMiddleFather.ToString().Trim());
            objDBUtility.AddParameters("@EMPNAMELASTFATHER", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmpNameLastFather.ToString().Trim());
            objDBUtility.AddParameters("@BIRTHDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, BirthDate.ToString().Trim());
            objDBUtility.AddParameters("@GENDER", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Gender.ToString().Trim());
            objDBUtility.AddParameters("@ADDRESS", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Address.ToString().Trim());
            objDBUtility.AddParameters("@STATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, State.ToString().Trim());
            objDBUtility.AddParameters("@PINCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, PinCode.ToString().Trim());
            objDBUtility.AddParameters("@MOBILENO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, MobileNo.ToString().Trim());
            objDBUtility.AddParameters("@OFFICENO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OfficeNo.ToString().Trim());
            objDBUtility.AddParameters("@EMAILID", DBUtilDBType.Varchar, DBUtilDirection.In, 255, EmailId.ToString().Trim());
            objDBUtility.AddParameters("@EMPLOYERNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, EmployerName.ToString().Trim());
            objDBUtility.AddParameters("@LOCATION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Location.ToString().Trim());
            objDBUtility.AddParameters("@EFILINGUSERID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EfilingUserId.ToString().Trim());
            objDBUtility.AddParameters("@EFILINGPASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EfilingPassword.ToString().Trim());
            objDBUtility.AddParameters("@AADHAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AADHAR.ToString().Trim());
            objDBUtility.AddParameters("@ISPARTNER", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsPartner.ToString().Trim());
            objDBUtility.AddParameters("@PARTNERDETAILS", DBUtilDBType.Varchar, DBUtilDirection.In, 100, PartnerDetails.ToString().Trim());
            objDBUtility.AddParameters("@ISSHARES", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsShares.ToString().Trim());
            objDBUtility.AddParameters("@SHARESDETAILS", DBUtilDBType.Varchar, DBUtilDirection.In, 100, SharesDetails.ToString().Trim());
            objDBUtility.AddParameters("@ISDIRECTOR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsDirector.ToString().Trim());
            objDBUtility.AddParameters("@DIRECTORDETAILS", DBUtilDBType.Varchar, DBUtilDirection.In, 100, DirectorDetails.ToString().Trim());
            objDBUtility.AddParameters("@ISPROPERTY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsProperty.ToString().Trim());
            objDBUtility.AddParameters("@PROPERTYDETAILS", DBUtilDBType.Varchar, DBUtilDirection.In, 100, PropertyDetails.ToString().Trim());
            objDBUtility.AddParameters("@ISAGGREGATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsAggregate.ToString().Trim());
            objDBUtility.AddParameters("@AGGREGATEAMOUNT", DBUtilDBType.Varchar, DBUtilDirection.In, 100, AggregateAmount.ToString().Trim());
            objDBUtility.AddParameters("@ISEXPENDITURE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsExpenditure.ToString().Trim());
            objDBUtility.AddParameters("@EXPENDITUREAMOUNT", DBUtilDBType.Varchar, DBUtilDirection.In, 100, ExpenditureAmount.ToString().Trim());
            objDBUtility.AddParameters("@ISELECTRICAL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, IsElectrical.ToString().Trim());
            objDBUtility.AddParameters("@ELECTRICALAMOUNT", DBUtilDBType.Varchar, DBUtilDirection.In, 100, ElectricalAmount.ToString().Trim());
            objDBUtility.AddParameters("@ACC1BANK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account1BankName.ToString().Trim());
            objDBUtility.AddParameters("@ACC1ACCNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account1AccNo.ToString().Trim());
            objDBUtility.AddParameters("@ACC1MICR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account1MICR.ToString().Trim());
            objDBUtility.AddParameters("@ACC1IFSC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account1IFSC.ToString().Trim());
            objDBUtility.AddParameters("@ACC2BANK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account2BankName.ToString().Trim());
            objDBUtility.AddParameters("@ACC2ACCNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account2AccNo.ToString().Trim());
            objDBUtility.AddParameters("@ACC2MICR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account2MICR.ToString().Trim());
            objDBUtility.AddParameters("@ACC2IFSC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account2IFSC.ToString().Trim());
            objDBUtility.AddParameters("@ACC3BANK", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account3BankName.ToString().Trim());
            objDBUtility.AddParameters("@ACC3ACCNO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account3AccNo.ToString().Trim());
            objDBUtility.AddParameters("@ACC3MICR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account3MICR.ToString().Trim());
            objDBUtility.AddParameters("@ACC3IFSC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Account3IFSC.ToString().Trim());
            objDBUtility.AddParameters("@INTERESTSAVINGS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, InterestSavings.ToString().Trim());
            objDBUtility.AddParameters("@INTERESTFD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, InterestFD.ToString().Trim());
            objDBUtility.AddParameters("@OTHERINCOME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, OtherIncome.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryId.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEITFORM");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet UpdateITFormFinal(string refNo, string transId)
        {
            objDBUtility = new DBUtility();

            DataSet ds = new DataSet();

            objDBUtility.AddParameters("@REFRANDOM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, refNo.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, transId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEITFORMFINAL");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public void GetITForm(string refNo)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            objDBUtility.AddParameters("@REFRANDOM", DBUtilDBType.Varchar, DBUtilDirection.In, 50, refNo);
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETITFORM");
            
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            if (dsLogin.Tables.Count > 0)
            {
                if (dsLogin.Tables[0].Rows.Count > 0)
                {
                    EntryId = dsLogin.Tables[0].Rows[0]["ENTRY_AID"].ToString().Trim();
                    EmpNameFirst = dsLogin.Tables[0].Rows[0]["FIRST_NAME"].ToString().Trim();
                    EmpNameMiddle = dsLogin.Tables[0].Rows[0]["MIDDLE_NAME"].ToString().Trim();
                    EmpNameLast = dsLogin.Tables[0].Rows[0]["LAST_NAME"].ToString().Trim();
                    EmpNameFirstFather = dsLogin.Tables[0].Rows[0]["FIRST_NAME_FATHER"].ToString().Trim();
                    EmpNameMiddleFather = dsLogin.Tables[0].Rows[0]["MIDDLE_NAME_FATHER"].ToString().Trim();
                    EmpNameLastFather = dsLogin.Tables[0].Rows[0]["LAST_NAME_FATHER"].ToString().Trim();
                    BirthDate = dsLogin.Tables[0].Rows[0]["BIRTH_DATE"].ToString().Trim();
                    Gender = dsLogin.Tables[0].Rows[0]["GENDER"].ToString().Trim();
                    Address = dsLogin.Tables[0].Rows[0]["COMMUNICATION_ADDRESS"].ToString().Trim();
                    State = dsLogin.Tables[0].Rows[0]["EMP_STATE"].ToString().Trim();
                    PinCode = dsLogin.Tables[0].Rows[0]["PIN_CODE"].ToString().Trim();
                    MobileNo = dsLogin.Tables[0].Rows[0]["MOBILE_NO"].ToString().Trim();
                    OfficeNo = dsLogin.Tables[0].Rows[0]["OFFICE_NO"].ToString().Trim();
                    EmailId = dsLogin.Tables[0].Rows[0]["EMAIL_ID"].ToString().Trim();
                    EmployerName = dsLogin.Tables[0].Rows[0]["EMPLOYER_NAME"].ToString().Trim();
                    Location = dsLogin.Tables[0].Rows[0]["EMPLOYER_LOCATION"].ToString().Trim();
                    EfilingUserId = dsLogin.Tables[0].Rows[0]["EFILING_USER"].ToString().Trim();
                    EfilingPassword = dsLogin.Tables[0].Rows[0]["EFILING_PASS"].ToString().Trim();
                    AADHAR = dsLogin.Tables[0].Rows[0]["AADHAR_NO"].ToString().Trim();
                    IsPartner = dsLogin.Tables[0].Rows[0]["IS_PARTNER"].ToString().Trim();
                    PartnerDetails = dsLogin.Tables[0].Rows[0]["PARTNER_DETAILS"].ToString().Trim();
                    IsShares = dsLogin.Tables[0].Rows[0]["IS_SHARES"].ToString().Trim();
                    SharesDetails = dsLogin.Tables[0].Rows[0]["SHARES_DETAILS"].ToString().Trim();
                    IsDirector = dsLogin.Tables[0].Rows[0]["IS_DIRECTOR"].ToString().Trim();
                    DirectorDetails = dsLogin.Tables[0].Rows[0]["DIRECTOR_DETAILS"].ToString().Trim();
                    IsProperty = dsLogin.Tables[0].Rows[0]["IS_PROPERTY"].ToString().Trim();
                    PropertyDetails = dsLogin.Tables[0].Rows[0]["PROPERTY_DETAILS"].ToString().Trim();
                    IsAggregate = dsLogin.Tables[0].Rows[0]["IS_AGGREGATE"].ToString().Trim();
                    AggregateAmount = dsLogin.Tables[0].Rows[0]["AGGREGATE_AMOUNT"].ToString().Trim();
                    IsExpenditure = dsLogin.Tables[0].Rows[0]["IS_EXPENDITURE"].ToString().Trim();
                    ExpenditureAmount = dsLogin.Tables[0].Rows[0]["EXPENDITURE_AMOUNT"].ToString().Trim();
                    IsElectrical = dsLogin.Tables[0].Rows[0]["IS_ELECTRICAL"].ToString().Trim();
                    ElectricalAmount = dsLogin.Tables[0].Rows[0]["ELECTRICAL_AMOUNT"].ToString().Trim();
                    Account1BankName = dsLogin.Tables[0].Rows[0]["ACCOUNT1_BANK_NAME"].ToString().Trim();
                    Account1AccNo = dsLogin.Tables[0].Rows[0]["ACCOUNT1_ACC_NO"].ToString().Trim();
                    Account1MICR = dsLogin.Tables[0].Rows[0]["ACCOUNT1_MICR"].ToString().Trim();
                    Account1IFSC = dsLogin.Tables[0].Rows[0]["ACCOUNT1_IFSC"].ToString().Trim();
                    Account2BankName = dsLogin.Tables[0].Rows[0]["ACCOUNT2_BANK_NAME"].ToString().Trim();
                    Account2AccNo = dsLogin.Tables[0].Rows[0]["ACCOUNT2_ACC_NO"].ToString().Trim();
                    Account2MICR = dsLogin.Tables[0].Rows[0]["ACCOUNT2_MICR"].ToString().Trim();
                    Account2IFSC = dsLogin.Tables[0].Rows[0]["ACCOUNT2_IFSC"].ToString().Trim();
                    Account3BankName = dsLogin.Tables[0].Rows[0]["ACCOUNT3_BANK_NAME"].ToString().Trim();
                    Account3AccNo = dsLogin.Tables[0].Rows[0]["ACCOUNT3_ACC_NO"].ToString().Trim();
                    Account3MICR = dsLogin.Tables[0].Rows[0]["ACCOUNT3_MICR"].ToString().Trim();
                    Account3IFSC = dsLogin.Tables[0].Rows[0]["ACCOUNT3_IFSC"].ToString().Trim();
                    InterestSavings = dsLogin.Tables[0].Rows[0]["INTEREST_SAVINGS"].ToString().Trim();
                    InterestFD = dsLogin.Tables[0].Rows[0]["INTEREST_FD"].ToString().Trim();
                    OtherIncome = dsLogin.Tables[0].Rows[0]["OTHER_INCOME"].ToString().Trim();
                    RefNo = dsLogin.Tables[0].Rows[0]["REFERENCE_NO"].ToString().Trim();
                    RefRandom = dsLogin.Tables[0].Rows[0]["TRANSACTION_ID"].ToString().Trim();
                }
            }

            objDBUtility.ClearTransactionalParams();
        }

        public DataSet GetITRFilingData(string from, string to)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, from);
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, to);
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETITFORMDATA");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetITRFilingDataFull()
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETITFORMDATAFULL");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public void Validate_Login_EmpDetails(string compValue, string userValue, string passValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, userValue.ToString().Trim());
            objDBUtility.AddParameters("@PASSWORD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, passValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "LOGINEMPDETAILS");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_GET_EMPLOYEE");

            if (dsLogin.Tables.Count > 0)
            {
                if (dsLogin.Tables[0].Rows.Count > 0)
                {
                    EmpAid = dsLogin.Tables[0].Rows[0]["EMP_AID"].ToString().Trim();
                    CompAid = dsLogin.Tables[0].Rows[0]["COMP_AID"].ToString().Trim();
                    BirthDate = dsLogin.Tables[0].Rows[0]["BIRTH_DATE"].ToString().Trim();
                    Gender = dsLogin.Tables[0].Rows[0]["SEX"].ToString().Trim();
                    Address = dsLogin.Tables[0].Rows[0]["EMP_ADDRESS"].ToString().Trim();
                    EfilingUserId = dsLogin.Tables[0].Rows[0]["PAN_NUMBER"].ToString().Trim();
                    EmailId = dsLogin.Tables[0].Rows[0]["EMAIL_ID"].ToString().Trim();
                }
            }

            objDBUtility.ClearTransactionalParams();
        }

        public DataSet GetPDFAmounts(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPDFAMOUNTS");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public void PDF(string compValue, string empValue, string taxOption)
        {
            objDBUtility = new DBUtility();

            DataSet ds = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@TAX", DBUtilDBType.Varchar, DBUtilDirection.In, 50, taxOption.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "SAVEPDFOPTION");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_TAX");

            objDBUtility.ClearTransactionalParams();
        }

        string _CompAid;
        string _EmpAid;
        string _EmpNameFirst;
        string _EmpNameMiddle;
        string _EmpNameLast;
        string _EmpNameFirstFather;
        string _EmpNameMiddleFather;
        string _EmpNameLastFather;
        string _BirthDate;
        string _Gender;
        string _Address;
        string _State;
        string _PinCode;
        string _MobileNo;
        string _OfficeNo;
        string _EmailId;
        string _EmployerName;
        string _Location;
        string _EfilingUserId;
        string _EfilingPassword;
        string _AADHAR;
        string _IsPartner;
        string _PartnerDetails;
        string _IsShares;
        string _SharesDetails;
        string _IsDirector;
        string _DirectorDetails;
        string _IsProperty;
        string _PropertyDetails;
        string _IsAggregate;
        string _AggregateAmount;
        string _IsExpenditure;
        string _ExpenditureAmount;
        string _IsElectrical;
        string _ElectricalAmount;
        string _Account1BankName;
        string _Account1AccNo;
        string _Account1MICR;
        string _Account1IFSC;
        string _Account2BankName;
        string _Account2AccNo;
        string _Account2MICR;
        string _Account2IFSC;
        string _Account3BankName;
        string _Account3AccNo;
        string _Account3MICR;
        string _Account3IFSC;
        string _InterestSavings;
        string _InterestFD;
        string _OtherIncome;
        string _RefNo;
        string _RefRandom;
        string _EntryId;

        public string CompAid
        {
            get { return _CompAid; }
            set { _CompAid = value; }
        }

        public string EmpAid
        {
            get { return _EmpAid; }
            set { _EmpAid = value; }
        }

        public string EmpNameFirst
        {
            get { return _EmpNameFirst; }
            set { _EmpNameFirst = value; }
        }

        public string EmpNameMiddle
        {
            get { return _EmpNameMiddle; }
            set { _EmpNameMiddle = value; }
        }

        public string EmpNameLast
        {
            get { return _EmpNameLast; }
            set { _EmpNameLast = value; }
        }

        public string EmpNameFirstFather
        {
            get { return _EmpNameFirstFather; }
            set { _EmpNameFirstFather = value; }
        }

        public string EmpNameMiddleFather
        {
            get { return _EmpNameMiddleFather; }
            set { _EmpNameMiddleFather = value; }
        }

        public string EmpNameLastFather
        {
            get { return _EmpNameLastFather; }
            set { _EmpNameLastFather = value; }
        }

        public string BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }

        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; }
        }

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        public string State
        {
            get { return _State; }
            set { _State = value; }
        }
        public string PinCode
        {
            get { return _PinCode; }
            set { _PinCode = value; }
        }
        public string MobileNo
        {
            get { return _MobileNo; }
            set { _MobileNo = value; }
        }
        public string OfficeNo
        {
            get { return _OfficeNo; }
            set { _OfficeNo = value; }
        }
        public string EmailId
        {
            get { return _EmailId; }
            set { _EmailId = value; }
        }
        public string EmployerName
        {
            get { return _EmployerName; }
            set { _EmployerName = value; }
        }
        public string Location
        {
            get { return _Location; }
            set { _Location = value; }
        }
        public string EfilingUserId
        {
            get { return _EfilingUserId; }
            set { _EfilingUserId = value; }
        }
        public string EfilingPassword
        {
            get { return _EfilingPassword; }
            set { _EfilingPassword = value; }
        }
        public string AADHAR
        {
            get { return _AADHAR; }
            set { _AADHAR = value; }
        }
        public string IsPartner
        {
            get { return _IsPartner; }
            set { _IsPartner = value; }
        }
        public string PartnerDetails
        {
            get { return _PartnerDetails; }
            set { _PartnerDetails = value; }
        }
        public string IsShares
        {
            get { return _IsShares; }
            set { _IsShares = value; }
        }
        public string SharesDetails
        {
            get { return _SharesDetails; }
            set { _SharesDetails = value; }
        }
        public string IsDirector
        {
            get { return _IsDirector; }
            set { _IsDirector = value; }
        }
        public string DirectorDetails
        {
            get { return _DirectorDetails; }
            set { _DirectorDetails = value; }
        }
        public string IsProperty
        {
            get { return _IsProperty; }
            set { _IsProperty = value; }
        }
        public string PropertyDetails
        {
            get { return _PropertyDetails; }
            set { _PropertyDetails = value; }
        }

        public string IsAggregate
        {
            get { return _IsAggregate; }
            set { _IsAggregate = value; }
        }
        public string AggregateAmount
        {
            get { return _AggregateAmount; }
            set { _AggregateAmount = value; }
        }
        public string IsExpenditure
        {
            get { return _IsExpenditure; }
            set { _IsExpenditure = value; }
        }
        public string ExpenditureAmount
        {
            get { return _ExpenditureAmount; }
            set { _ExpenditureAmount = value; }
        }
        public string IsElectrical
        {
            get { return _IsElectrical; }
            set { _IsElectrical = value; }
        }
        public string ElectricalAmount
        {
            get { return _ElectricalAmount; }
            set { _ElectricalAmount = value; }
        }

        public string Account1BankName
        {
            get { return _Account1BankName; }
            set { _Account1BankName = value; }
        }
        public string Account1AccNo
        {
            get { return _Account1AccNo; }
            set { _Account1AccNo = value; }
        }
        public string Account1MICR
        {
            get { return _Account1MICR; }
            set { _Account1MICR = value; }
        }
        public string Account1IFSC
        {
            get { return _Account1IFSC; }
            set { _Account1IFSC = value; }
        }
        public string Account2BankName
        {
            get { return _Account2BankName; }
            set { _Account2BankName = value; }
        }
        public string Account2AccNo
        {
            get { return _Account2AccNo; }
            set { _Account2AccNo = value; }
        }
        public string Account2MICR
        {
            get { return _Account2MICR; }
            set { _Account2MICR = value; }
        }
        public string Account2IFSC
        {
            get { return _Account2IFSC; }
            set { _Account2IFSC = value; }
        }
        public string Account3BankName
        {
            get { return _Account3BankName; }
            set { _Account3BankName = value; }
        }
        public string Account3AccNo
        {
            get { return _Account3AccNo; }
            set { _Account3AccNo = value; }
        }
        public string Account3MICR
        {
            get { return _Account3MICR; }
            set { _Account3MICR = value; }
        }
        public string Account3IFSC
        {
            get { return _Account3IFSC; }
            set { _Account3IFSC = value; }
        }
        public string InterestSavings
        {
            get { return _InterestSavings; }
            set { _InterestSavings = value; }
        }
        public string InterestFD
        {
            get { return _InterestFD; }
            set { _InterestFD = value; }
        }
        public string OtherIncome
        {
            get { return _OtherIncome; }
            set { _OtherIncome = value; }
        }
        public string RefNo
        {
            get { return _RefNo; }
            set { _RefNo = value; }
        }
        public string RefRandom
        {
            get { return _RefRandom; }
            set { _RefRandom = value; }
        }
        public string EntryId
        {
            get { return _EntryId; }
            set { _EntryId = value; }
        }
    }
}