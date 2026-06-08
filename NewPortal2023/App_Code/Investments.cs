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
using System.Globalization;
using System.Data.OleDb;
using System.Text;

/// <summary>
/// Summary description for Investment
/// </summary>

namespace NewPortal2023.ESS
{
    public class Investments
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        public Investments()
        {

        }

        public DataSet GetInvestmentDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETINV");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        //Deven
        public DataSet GetInvestmentForExport(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INVEXPORT");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet Get12BBList(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "EXPORT12BB");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetSupportsList(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "EXPORTSUPPORTS");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetSupportsFilesList(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "EXPORTSUPPORTFILES");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetOtherDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETOther");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetAmountDetails(string compValue, string empValue, LinkButton lblInvDetails, LinkButton lnkRent, LinkButton lnkRentNew, LinkButton lnk12, LinkButton lnk12b)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAMOUNT");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

                if (dsInv.Tables.Count > 0)
                {

                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int cnt = 0; cnt <= dsInv.Tables[0].Rows.Count - 1; cnt++)
                        {
                            if (compValue == "CO000015")
                            {
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "INVESTMENT")
                                {
                                    lblInvDetails.Text = "2. Investment Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "").Replace(".00", "") + ")";
                                }
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "RENT")
                                {
                                    lnkRent.Text = "3. Rent Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "").Replace(".00", "") + ")";
                                }
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "12C")
                                {
                                    lnk12.Text = "4. Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "").Replace(".00", "") + ")";
                                }
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "12B")
                                {
                                    lnk12b.Text = "5. Details of Income From Previous Employer (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "").Replace(".00", "") + ")";
                                }
                            }
                            else
                            {
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "INVESTMENT")
                                {
                                    lblInvDetails.Text = "2. Investment Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "") + ")";
                                }
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "RENT")
                                {
                                    lnkRent.Text = "3. Rent Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "") + ")";
                                    lnkRentNew.Text = "3. Rent Declaration (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "") + ")";
                                }
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "12C")
                                {
                                    lnk12.Text = "4. Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "") + ")";
                                }
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "12B")
                                {
                                    lnk12b.Text = "5. Details of Income From Previous Employer"; // (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "") + ")";
                                }
                            }
                        }
                    }

                }

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetAmountDetails(string compValue, string empValue, Label lblInvDetails, Label lnkRent, Label lnk12, Label lnk12b)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETAMOUNT");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

                if (dsInv.Tables.Count > 0)
                {

                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int cnt = 0; cnt <= dsInv.Tables[0].Rows.Count - 1; cnt++)
                        {
                            if (compValue == "CO000015")
                            {
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "INVESTMENT")
                                {
                                    lblInvDetails.Text = "2. Investment Support (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "").Replace(".00", "") + ")";
                                }
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "RENT")
                                {
                                    lnkRent.Text = "3. Rent Support (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "").Replace(".00", "") + ")";
                                }
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "12C")
                                {
                                    lnk12.Text = "4. Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "").Replace(".00", "") + ")";
                                }
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "12B")
                                {
                                    lnk12b.Text = "5. Details of Income From Previous Employer (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "").Replace(".00", "") + ")";
                                }
                            }
                            else
                            {
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "INVESTMENT")
                                {
                                    lblInvDetails.Text = "2. Investment Support (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "") + ")";
                                }
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "RENT")
                                {
                                    lnkRent.Text = "3. Rent Support (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "") + ")";
                                }
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "12C")
                                {
                                    lnk12.Text = "4. Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "") + ")";
                                }
                                if (dsInv.Tables[0].Rows[cnt]["NAME"].ToString().Trim() == "12B")
                                {
                                    lnk12b.Text = "5. Details of Income From Previous Employer"; // (" + String.Format(CultureInfo.CreateSpecificCulture("hi-IN"), "{0:C}", dsInv.Tables[0].Rows[cnt]["AMOUNT"]).Replace("रु ", "") + ")";
                                }
                            }
                        }
                    }

                }

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetRentDetails(string compValue, string empValue, TextBox txtAddress)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETRENTADD");


                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");


                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            txtAddress.Text = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["ADDRESS"].ToString().Trim());
                        }
                    }
                }

                objDBUtility.ClearTransactionalParams();

                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETRENT");


                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetRentDetailsNew(string compValue, string empValue, TextBox txtAddress)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETRENTADD");


                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");


                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            txtAddress.Text = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["ADDRESS"].ToString().Trim());
                        }
                    }
                }

                objDBUtility.ClearTransactionalParams();

                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETRENTNEW");


                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetLandlordDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLANDLORD");


                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetLenderDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {

                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLENDER");


                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetTwelveDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GET12C");


                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet GetTwelveBDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GET12B");


                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");



                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public string UpdateInvestmentXL(string xmlValue, string strAddress, string strEvent, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;

            objDBUtility.AddParameters("@InvXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@ADD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, strAddress);
            objDBUtility.AddParameters("@EVENTType", DBUtilDBType.Varchar, DBUtilDirection.In, 50, strEvent);
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEINVXL");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");


            if (dsInv.Tables.Count > 0)
            {
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                    {
                        strresult = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["status"].ToString().Trim());
                    }
                }
            }

            objDBUtility.ClearTransactionalParams();

            return strresult;
        }

        public string UpdateInvestment(string xmlValue, string strAddress, string strEvent, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;

            objDBUtility.AddParameters("@InvXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@ADD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, strAddress);
            objDBUtility.AddParameters("@EVENTType", DBUtilDBType.Varchar, DBUtilDirection.In, 50, strEvent);
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEINV");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");


            if (dsInv.Tables.Count > 0)
            {
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                    {
                        strresult = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["status"].ToString().Trim());
                    }
                }
            }

            objDBUtility.ClearTransactionalParams();

            return strresult;
        }

        public string UpdateInvestmentAll(string xmlOther, string xmlInv, string xmlRent, string xmlLandlord, string xmlLoan, string xmlLender, string strAddress, string strEvent, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;
            try
            {
                objDBUtility.AddParameters("@OtherXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlOther.ToString().Trim());
                objDBUtility.AddParameters("@InvXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlInv.ToString().Trim());
                objDBUtility.AddParameters("@RentXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlRent.ToString().Trim());
                objDBUtility.AddParameters("@LandlordXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlLandlord.ToString().Trim());
                objDBUtility.AddParameters("@LoanXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlLoan.ToString().Trim());
                objDBUtility.AddParameters("@LenderXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlLender.ToString().Trim());
                objDBUtility.AddParameters("@ADD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, strAddress);
                objDBUtility.AddParameters("@EVENTType", DBUtilDBType.Varchar, DBUtilDirection.In, 50, strEvent);
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEINVALL");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");


                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            strresult = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["status"].ToString().Trim());
                        }
                    }
                }

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                strresult = ex.Message;
            }

            return strresult;
        }

        public string UpdateInvestmentAll(string xmlOther, string xmlInv, string xmlRent, string xmlLandlord, string xmlLoan, string xmlLender, string xmlPrev, string strAddress, string strEvent, string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            string strresult = string.Empty;
            try
            {
                objDBUtility.AddParameters("@OtherXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlOther.ToString().Trim());
                objDBUtility.AddParameters("@InvXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlInv.ToString().Trim());
                objDBUtility.AddParameters("@RentXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlRent.ToString().Trim());
                objDBUtility.AddParameters("@LandlordXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlLandlord.ToString().Trim());
                objDBUtility.AddParameters("@LoanXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlLoan.ToString().Trim());
                objDBUtility.AddParameters("@LenderXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlLender.ToString().Trim());
                objDBUtility.AddParameters("@PrevXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlPrev.ToString().Trim());
                objDBUtility.AddParameters("@ADD", DBUtilDBType.Varchar, DBUtilDirection.In, 50, strAddress);
                objDBUtility.AddParameters("@EVENTType", DBUtilDBType.Varchar, DBUtilDirection.In, 50, strEvent);
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEINVALL");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");


                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            strresult = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["status"].ToString().Trim());
                        }
                    }
                }

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                strresult = ex.Message;
            }

            return strresult;
        }

        public DataSet Fill_Report(string empValue, string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "PRINT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet Get_InvEmpList(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPINVLIST");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet Clear_Investment(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CLEARALL");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet Upload_12BB(string compValue, string empValue, string fileName)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FILE_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, fileName.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "HISTORY12BB");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetUploadHistory(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "LATEWARNING");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet CheckEmp(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CHECKEMP");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public void UpdateLinkDate(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATELINKDATE");

            objDBUtility.Execute_StoreProc("COMMON_SP_INVESTMENT");

            objDBUtility.ClearTransactionalParams();
        }

        public string UploadInvestmentsXL(string compValue, string strPath, string empValue)
        {
            string conn = string.Empty;
            string query = string.Empty;
            string status = string.Empty;
            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();

            conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            query = "SELECT * FROM [Declaration$]";

            using (var connection = new OleDbConnection(conn))
            {
                using (var da = new OleDbDataAdapter(query, connection))
                {
                    connection.Open();
                    da.Fill(ds);
                }
            }

            status = "";

            StringBuilder sbTaxDetails = new StringBuilder();
            string xmlInv = string.Empty;
            string rentAddr = string.Empty;

            sbTaxDetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sbTaxDetails.Append("<ROOT>");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr[12].ToString().Trim() != "")
                {
                    sbTaxDetails.Append("<Inv COMP_AID='" + compValue + "'");
                    sbTaxDetails.Append(" EMP_AID='" + empValue + "'");
                    sbTaxDetails.Append(" INV_AID='" + dr[12].ToString().Trim() + "'");
                    sbTaxDetails.Append(" DET_AID=''");
                    if (dr[14].ToString().Trim() == "Other" || dr[14].ToString().Trim() == "Landlord" || dr[14].ToString().Trim() == "Lender")
                    {
                        sbTaxDetails.Append(" AMOUNT='0'");
                    }
                    else
                    {
                        sbTaxDetails.Append(" AMOUNT='" + dr[6].ToString().Trim() + "'");
                    }
                    sbTaxDetails.Append(" INV_DESC='" + ReplaceSpecialCharacters(dr[13].ToString().Trim()) + "'");
                    if (dr[14].ToString().Trim() == "Rent")
                    {
                        sbTaxDetails.Append(" PAN='" + ReplaceSpecialCharacters(dr[7].ToString().Trim()) + "'");
                    }
                    else
                    {
                        sbTaxDetails.Append(" PAN=''");
                    }
                    if (dr[14].ToString().Trim() == "12")
                    {
                        sbTaxDetails.Append(" PROP_ADDRESS='" + ReplaceSpecialCharacters(dr[9].ToString().Trim()) + "'");
                    }
                    else
                    {
                        sbTaxDetails.Append(" PROP_ADDRESS=''");
                    }

                    if (dr[14].ToString().Trim() == "Other")
                    {
                        sbTaxDetails.Append(" OTHER_DETAILS='" + ReplaceSpecialCharacters(dr[6].ToString().Trim()) + "'");
                    }
                    else if (dr[14].ToString().Trim() == "Landlord")
                    {
                        sbTaxDetails.Append(" OTHER_DETAILS='" + ReplaceSpecialCharacters(dr[5].ToString().Trim()) + "'");
                    }
                    else if (dr[14].ToString().Trim() == "Lender")
                    {
                        sbTaxDetails.Append(" OTHER_DETAILS='" + ReplaceSpecialCharacters(dr[6].ToString().Trim()) + "'");
                    }
                    else
                    {
                        sbTaxDetails.Append(" OTHER_DETAILS=''");
                    }
                    sbTaxDetails.Append(" SORT_ORDER='0'/>");

                    if (dr[12].ToString().Trim() == "RENTADD")
                    {
                        rentAddr = dr[3].ToString().Trim();
                    }
                }
            }

            sbTaxDetails.Append("</ROOT>");

            xmlInv = sbTaxDetails.ToString();

            status = UpdateInvestmentXL(xmlInv, rentAddr, "Rent", compValue, empValue);

            return status;
        }

        private string ReplaceSpecialCharacters(string inputString)
        {
            inputString = inputString.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&#39;").
                    Replace("\"", "&quot;");

            return inputString;
        }

        public DataSet GetRegime(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CHECKREGIME");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet GetEmployeeList(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPLOYEESLISTDETAILS");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet getAllEmployeesId(string COMP_AID)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, COMP_AID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPLOYEESLISTID");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet Getpassword(string compValue, string empValue, string lblMID)
        {
            objDBUtility = new DBUtility();

            DataSet dspass = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@LBLDESC", DBUtilDBType.Varchar, DBUtilDirection.In, 50, lblMID.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPASSWORD");
            dspass = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_INVESTMENT");

            objDBUtility.ClearTransactionalParams();

            return dspass;
        }
    }

}



