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
using System.Data.OleDb;
using System.Text;


/// <summary>
/// Summary description for FlexiPay
/// </summary>
namespace NewPortal2023.ESS
{

    public class FlexiPay
    {
        private NewPortal2023.ESS.DBUtility objDBUtility;

        public FlexiPay()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        //public DataTable GetCTCDetails(string compValue, string empValue, string empCode, TextBox txtCTC, TextBox txtBal, TextBox txtAnnCTC, DropDownList drpEffectiveDate, DropDownList drpCarLease)
        //{
        //    objDBUtility = new DBUtility();

        //    DataSet dsInv = new DataSet();
        //    DataTable dt = new DataTable();
        //    try
        //    {


        //        objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
        //        objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
        //        objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
        //        objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCTC");

        //        dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

        //         if (dsInv.Tables.Count > 0)
        //        {
        //            if (dsInv.Tables[0].Rows.Count > 0)
        //            {
        //                for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
        //                {
        //                    txtCTC.Text = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["ctc_amt"].ToString().Trim());
        //                    txtBal.Text = Convert.ToString(dsInv.Tables[2].Rows[IntRow]["BALCTC"].ToString().Trim());
        //                    txtAnnCTC.Text = Convert.ToString(dsInv.Tables[3].Rows[IntRow]["ANNCTC"].ToString().Trim());
        //                    if (dsInv.Tables[4].Rows[IntRow]["FlexAddr"].ToString() == "1")
        //                    {
        //                        drpCarLease.SelectedIndex = 1;
        //                    }
        //                    else
        //                    {
        //                        drpCarLease.SelectedIndex = 0;
        //                    }

        //                    if (dsInv.Tables[5].Rows[IntRow]["CONFIRMATION_DATE"].ToString().Contains("01/04/2014"))
        //                    {
        //                        drpEffectiveDate.SelectedIndex = 1;
        //                    }
        //                    else
        //                    {
        //                        drpEffectiveDate.SelectedIndex = 0;
        //                    }
        //                }
        //            }
        //        }

        //        dt = dsInv.Tables[1];
        //        objDBUtility.ClearTransactionalParams();

        //    }
        //    catch (Exception ex)
        //    {
        //        //CreateErrorLog("", ex.Message, "Common_Validate_Login");
        //    }

        //    return dt;
        //}

        public DataTable GetCTCDetails(string compValue, string empValue, string empCode, TextBox txtCTC, TextBox txtBal, TextBox txtAnnCTC, DropDownList drpCarLease, out bool isRent)
        {
            objDBUtility = new DBUtility();

            isRent = false;

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCTC");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            txtCTC.Text = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["ctc_amt"].ToString().Trim());
                            txtBal.Text = Convert.ToString(dsInv.Tables[2].Rows[IntRow]["BALCTC"].ToString().Trim());
                            txtAnnCTC.Text = Convert.ToString(dsInv.Tables[3].Rows[IntRow]["ANNCTC"].ToString().Trim());
                            if (dsInv.Tables[4].Rows[0][0].ToString() == "1")
                            {
                                drpCarLease.SelectedIndex = 1;
                            }
                            else
                            {
                                drpCarLease.SelectedIndex = 0;
                            }
                        }
                    }

                    if (dsInv.Tables[6].Rows.Count > 0)
                    {
                        if (Convert.ToDouble(dsInv.Tables[6].Rows[0][0].ToString()) > 0)
                        {
                            isRent = true;
                        }
                        else
                        {
                            isRent = false;
                        }
                    }
                    else
                    {
                        isRent = false;
                    }
                }

                dt = dsInv.Tables[1];
                objDBUtility.ClearTransactionalParams();

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dt;
        }

        public DataSet GetNewCTCDetails(string compValue, string empValue, string empCode, string allAid, TextBox txtCTC, TextBox txtBal, TextBox txtAnnCTC, DropDownList drpCarLease, out bool isRent)
        {
            objDBUtility = new DBUtility();

            isRent = false;

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
                objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allAid.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCTC");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");
                if (dsInv.Tables.Count > 0)
                {
                    txtCTC.Text = Convert.ToString(dsInv.Tables[1].Rows[0]["TOTAL"].ToString().Trim());
                    //txtAnnCTC.Text = Convert.ToString(dsInv.Tables[2].Rows[0]["ALLWDED_AID"].ToString().Trim());

                    objDBUtility.ClearTransactionalParams();

                }
            }


            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }
            return dsInv;
        }


        public DataTable GetCTCDetailsIndiafirst(string compValue, string empValue, string empCode, TextBox txtCTC, TextBox txtBal, TextBox txtAnnCTC, DropDownList drpCarLease)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCTCINDIAFIRST");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

                if (dsInv.Tables.Count > 0)
                {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            txtCTC.Text = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["ctc_amt"].ToString().Trim());
                            txtBal.Text = Convert.ToString(dsInv.Tables[2].Rows[IntRow]["BALCTC"].ToString().Trim());
                            txtAnnCTC.Text = Convert.ToString(dsInv.Tables[3].Rows[IntRow]["ANNCTC"].ToString().Trim());
                            if (dsInv.Tables[4].Rows[0][0].ToString() == "1")
                            {
                                drpCarLease.SelectedIndex = 1;
                            }
                            else
                            {
                                drpCarLease.SelectedIndex = 0;
                            }
                        }
                    }
                }

                dt = dsInv.Tables[1];
                objDBUtility.ClearTransactionalParams();

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dt;
        }

        public DataSet GetSupportDetails(string compValue, string empValue, TextBox txtLTANo, TextBox txtLTAAmt, TextBox txtTelAmt, TextBox txtFuelAmt, TextBox txtDriverAmt,
                                                                            Label txtLTAAmtVer, Label txtTelAmtVer, Label txtFuelAmtVer, Label txtDriverAmtVer,
                                                                            Label txtLTAAmtRej, Label txtTelAmtRej, Label txtFuelAmtRej, Label txtDriverAmtRej,
                                                                            Label txtLTARem, Label txtTelRem, Label txtFuelRem, Label txtDriverRem)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSUPPORTAMOUNT");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            if (dsInv.Tables.Count == 4)
            {
                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    txtLTAAmt.Text = dsInv.Tables[0].Rows[0][0].ToString().Replace(".0000", "");
                    txtLTAAmtVer.Text = dsInv.Tables[0].Rows[0][1].ToString().Replace(".0000", "");
                    txtLTAAmtRej.Text = dsInv.Tables[0].Rows[0][2].ToString().Replace(".0000", "");
                    txtLTARem.Text = dsInv.Tables[0].Rows[0][3].ToString().Replace(".0000", "");
                    txtLTANo.Text = dsInv.Tables[0].Rows[0][4].ToString().Replace(".0000", "");

                }

                if (dsInv.Tables[1].Rows.Count > 0)
                {
                    txtTelAmt.Text = dsInv.Tables[1].Rows[0][0].ToString().Replace(".0000", "");
                    txtTelAmtVer.Text = dsInv.Tables[1].Rows[0][1].ToString().Replace(".0000", "");
                    txtTelAmtRej.Text = dsInv.Tables[1].Rows[0][2].ToString().Replace(".0000", "");
                    txtTelRem.Text = dsInv.Tables[1].Rows[0][3].ToString().Replace(".0000", "");
                }

                if (dsInv.Tables[2].Rows.Count > 0)
                {
                    txtFuelAmt.Text = dsInv.Tables[2].Rows[0][0].ToString().Replace(".0000", "");
                    txtFuelAmtVer.Text = dsInv.Tables[2].Rows[0][1].ToString().Replace(".0000", "");
                    txtFuelAmtRej.Text = dsInv.Tables[2].Rows[0][2].ToString().Replace(".0000", "");
                    txtFuelRem.Text = dsInv.Tables[2].Rows[0][3].ToString().Replace(".0000", "");
                }

                if (dsInv.Tables[3].Rows.Count > 0)
                {
                    txtDriverAmt.Text = dsInv.Tables[3].Rows[0][0].ToString().Replace(".0000", "");
                    txtDriverAmtVer.Text = dsInv.Tables[3].Rows[0][1].ToString().Replace(".0000", "");
                    txtDriverAmtRej.Text = dsInv.Tables[3].Rows[0][2].ToString().Replace(".0000", "");
                    txtDriverRem.Text = dsInv.Tables[3].Rows[0][3].ToString().Replace(".0000", "");
                }
            }

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataTable GetCTCDetailsAngel(string compValue, string empValue, string empCode, TextBox txtCTC, TextBox txtBal, TextBox txtAnnCTC, Label lblAmtMnt, Label lblAmtAnn)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCTCANGEL");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

                if (dsInv.Tables.Count > 0)
                    {
                    if (dsInv.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            txtCTC.Text = Convert.ToString(dsInv.Tables[0].Rows[IntRow]["ctc_amt"].ToString().Trim());
                            txtBal.Text = Convert.ToString(dsInv.Tables[5].Rows[IntRow]["BALCTC"].ToString().Trim());
                            txtAnnCTC.Text = Convert.ToString(dsInv.Tables[6].Rows[IntRow]["ANNCTC"].ToString().Trim());
                            lblAmtMnt.Text = Convert.ToString(dsInv.Tables[3].Rows[IntRow]["Fix_Amount"].ToString().Trim());
                            // lblAmtAnn.Text = Convert.ToString(dsInv.Tables[4].Rows[IntRow]["Fix_Amount"].ToString().Trim());
                            //if (dsInv.Tables[4].Rows[IntRow]["FlexAddr"].ToString() == "1")
                            //{
                            //    drpCarLease.SelectedIndex = 1;
                            //}
                            //else
                            //{
                            //    drpCarLease.SelectedIndex = 0;
                            //}
                        }
                    }
                }

                dt = dsInv.Tables[1];
                //dt = dsInv.Tables[1];
                objDBUtility.ClearTransactionalParams();

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dt;
        }


        public DataTable GetCTCDetailsAngel1(string compValue, string empValue, string empCode, TextBox txtCTC, TextBox txtBal, TextBox txtAnnCTC, Label lblAmtMnt1, Label lblAmtAnn1)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv1 = new DataSet();
            DataTable dt1 = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCTCANGEL");

                dsInv1 = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

                if (dsInv1.Tables.Count > 0)
                {
                    if (dsInv1.Tables[0].Rows.Count > 0)
                    {
                        for (int IntRow = 0; IntRow <= dsInv1.Tables[0].Rows.Count - 1; IntRow++)
                        {
                            txtCTC.Text = Convert.ToString(dsInv1.Tables[0].Rows[IntRow]["ctc_amt"].ToString().Trim());
                            txtBal.Text = Convert.ToString(dsInv1.Tables[5].Rows[IntRow]["BALCTC"].ToString().Trim());
                            txtAnnCTC.Text = Convert.ToString(dsInv1.Tables[6].Rows[IntRow]["ANNCTC"].ToString().Trim());
                            lblAmtMnt1.Text = Convert.ToString(dsInv1.Tables[4].Rows[IntRow]["Fix_Amount"].ToString().Trim());
                            // lblAmtAnn.Text = Convert.ToString(dsInv.Tables[4].Rows[IntRow]["Fix_Amount"].ToString().Trim());
                            //if (dsInv.Tables[4].Rows[IntRow]["FlexAddr"].ToString() == "1")
                            //{
                            //    drpCarLease.SelectedIndex = 1;
                            //}
                            //else
                            //{
                            //    drpCarLease.SelectedIndex = 0;
                            //}
                        }
                    }
                }

                dt1 = dsInv1.Tables[2];

                objDBUtility.ClearTransactionalParams();

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dt1;
        }



        public DataSet GetCTCDetailsPrint(string compValue, string empValue, string empCode)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empCode.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCTCPRINT");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

                objDBUtility.ClearTransactionalParams();

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public string GetFlexiAmountFromGrade(string compValue, string empValue, string allAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETFLEXIAMOUNTFROMGRADE");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsInv.Tables[0].Rows[0][0].ToString();
        }

        public DataTable GetFlexiCompensation(string compValue, string empValue, DropDownList drpFlexiHeads)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETALLWGRADE");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

                dt = dsInv.Tables[0];

                drpFlexiHeads.Items.Clear();
                drpFlexiHeads.DataTextField = "DESC";
                drpFlexiHeads.DataValueField = "AID";
                drpFlexiHeads.DataSource = dsInv.Tables[1];
                drpFlexiHeads.DataBind();
                drpFlexiHeads.Items.Insert(0, new ListItem("[Select One]", ""));

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dt;
        }

        public DataSet GetFlexiCompensationBills(string compValue, string empValue, string allAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETBILLS");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetFlexiCompensationPrint(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETALLWGRADEPRINT");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

                objDBUtility.ClearTransactionalParams();

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public DataSet Validate_Alternate(string compValue, string Empid, string allId)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            try
            {


                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
                objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "VALIDATEALTER");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

                objDBUtility.ClearTransactionalParams();

                return dsInv;

            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dsInv;
        }

        public Boolean UpdateFlexipay(string xmlValue, string carLease, string CompId, string Empid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@CTCXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@CAR_LEASE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, carLease.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEFLEXI");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return true;
        }

        public Boolean UpdateFlexipaySupport(string AllId, string CompId, string EmpId, string amount)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpId.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AllId.ToString().Trim());
            objDBUtility.AddParameters("@AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, amount.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTSUPPORTAMOUNT");


            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return true;
        }

        public Boolean UpdateFlexipaySupport(string AllId, string CompId, string EmpId, string amount, string noOfIndividuals)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EmpId.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, AllId.ToString().Trim());
            objDBUtility.AddParameters("@AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, amount.ToString().Trim());
            objDBUtility.AddParameters("@LTA_NO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, noOfIndividuals.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTSUPPORTAMOUNT");


            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return true;
        }

        public Boolean UpdateFlexipayAngel(string xmlValue, string CompId, string Empid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            try
            {
                objDBUtility.AddParameters("@CTCXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
                objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEFLEXIANGEL");
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                return false;
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return true;
        }

        public Boolean UpdateFlexipayCompensation(string xmlValue, string CompId, string Empid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@CTCXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECOMP");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return true;
        }

        public Boolean UpdateFlexipayCompensation(string xmlValue, string CompId, string Empid, string allAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@CTCXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECOMP");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return true;
        }

        public DataSet Upload_Bills(string compValue, string empValue, string fileName, string invYear, string drpQuarter)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FILE_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, fileName.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, invYear.ToString().Trim());
            objDBUtility.AddParameters("@QUT", DBUtilDBType.Varchar, DBUtilDirection.In, 100, drpQuarter.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "HISTORYBILLS");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet UploadDocuments(string compValue, string empValue, string fileName, string invYear, string uploadType)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FILE_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, fileName.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, invYear.ToString().Trim());
            objDBUtility.AddParameters("@UPLOADTYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 100, uploadType.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CARUPLOADDOCUMENTS");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet InsertNewRecord(string compValue, string empValue, int flag, string year)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FLAG", DBUtilDBType.Varchar, DBUtilDirection.In, 100, flag.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, year.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTNEWRECORDFORCARDOC");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }



        public DataSet GetCarRecord(string compValue, string empValue, string year)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, year.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCARREC");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet Upload_Bills_Delete(string compValue, string empValue, string fileName, string invYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FILE_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, fileName.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, invYear.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "HISTORYBILLSDELETE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataTable GetFlexiCompensationVerified(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETSUPPORTSRECEIVED");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            return dsInv.Tables[0];
        }

        public bool UploadFlexiCompensationXL(string compValue, string strPath, string empValue, out string error)
        {
            string conn = string.Empty;
            string query = string.Empty;
            bool status = false;

            //DataSet ds = new DataSet();
            //objDBUtility = new DBUtility();

            //conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            //query = "SELECT * FROM [FUEL ALLOWANCE$]";

            //using (var connection = new OleDbConnection(conn))
            //{
            //    using (var da = new OleDbDataAdapter(query, connection))
            //    {
            //        connection.Open();
            //        da.Fill(ds);
            //    }
            //}

            //error = "";

            //StringBuilder sbTaxDetails = new StringBuilder();
            //string xmlInv = string.Empty;
            //string rentAddr = string.Empty;

            //sbTaxDetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            //sbTaxDetails.Append("<ROOT>");

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    if (ds.Tables[0].Rows.IndexOf(dr) > 6)
            //    {
            //        if (dr[0].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "")
            //        {
            //            DateTime billDate;
            //            if (!(DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "dd-MM-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate) ||
            //                  DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "M-d-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate)))
            //            {
            //                error = "Date invalid." + dr[2].ToString();
            //                return false;
            //            }

            //            DateTime dtPrev = new DateTime(2021, 3, 31);
            //            if (billDate <= dtPrev)
            //            {
            //                error = "Only bills for current financial year allowed (FUEL ALLOWANCE).";
            //                return false;
            //            }

            //            sbTaxDetails.Append("<Flexi COMP_AID='" + compValue + "'");
            //            sbTaxDetails.Append(" EMP_AID='" + empValue + "'");
            //            sbTaxDetails.Append(" ALLWDED_AID='AD000035'");
            //            sbTaxDetails.Append(" PARTICUALRS='" + ReplaceSpecialCharacters(dr[0].ToString().Trim()) + "'");
            //            sbTaxDetails.Append(" PARTICUALRS2=''");
            //            sbTaxDetails.Append(" BILL_NO='" + dr[1].ToString().Trim() + "'");
            //            sbTaxDetails.Append(" BILL_DATE='" + billDate.ToString("dd-MM-yyyy") + "'");
            //            //sbTaxDetails.Append(" AMOUNT_ACCEPTED='" + ((TextBox)gvr.FindControl("txtAcceptedAmount")).Text.Trim() + "'");
            //            //sbTaxDetails.Append(" REMARKS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text.Trim()) + "'");
            //            //sbTaxDetails.Append(" DATE_SAVED='" + ((Label)gvr.FindControl("lblDateSaved")).Text.Trim() + "'");
            //            sbTaxDetails.Append(" AMOUNT='" + dr[3].ToString().Trim() + "'/>");
            //        }
            //    }
            //}

            //ds = new DataSet();
            //conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            //query = "SELECT * FROM [LEAVE TRAVEL ALLOWANCE$]";

            //using (var connection = new OleDbConnection(conn))
            //{
            //    using (var da = new OleDbDataAdapter(query, connection))
            //    {
            //        connection.Open();
            //        da.Fill(ds);
            //    }
            //}

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    if (ds.Tables[0].Rows.IndexOf(dr) > 6)
            //    {
            //        if (dr[0].ToString().Trim() != "" && dr[3].ToString().Trim() != "" && dr[4].ToString().Trim() != "")
            //        {
            //            DateTime billDate;
            //            if (!(DateTime.TryParseExact(dr[3].ToString().Trim().Replace("/", "-"), "dd-MM-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate) ||
            //                  DateTime.TryParseExact(dr[3].ToString().Trim().Replace("/", "-"), "M-d-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate)))
            //            {
            //                error = "Date invalid." + dr[3].ToString();
            //                return false;
            //            }

            //            DateTime dtPrev = new DateTime(2021, 3, 31);
            //            if (billDate <= dtPrev)
            //            {
            //                error = "Only bills for current financial year allowed (LEAVE TRAVEL ALLOWANCE).";
            //                return false;
            //            }

            //            sbTaxDetails.Append("<Flexi COMP_AID='" + compValue + "'");
            //            sbTaxDetails.Append(" EMP_AID='" + empValue + "'");
            //            sbTaxDetails.Append(" ALLWDED_AID='AD000159'");
            //            sbTaxDetails.Append(" PARTICUALRS='" + ReplaceSpecialCharacters(dr[0].ToString().Trim()) + "'");
            //            sbTaxDetails.Append(" PARTICUALRS2='" + ReplaceSpecialCharacters(dr[1].ToString().Trim()) + "'");
            //            sbTaxDetails.Append(" BILL_NO='" + dr[2].ToString().Trim() + "'");
            //            sbTaxDetails.Append(" BILL_DATE='" + billDate.ToString("dd-MM-yyyy") + "'");
            //            //sbTaxDetails.Append(" AMOUNT_ACCEPTED='" + ((TextBox)gvr.FindControl("txtAcceptedAmount")).Text.Trim() + "'");
            //            //sbTaxDetails.Append(" REMARKS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text.Trim()) + "'");
            //            //sbTaxDetails.Append(" DATE_SAVED='" + ((Label)gvr.FindControl("lblDateSaved")).Text.Trim() + "'");
            //            sbTaxDetails.Append(" AMOUNT='" + dr[4].ToString().Trim() + "'/>");
            //        }
            //    }
            //}

            //ds = new DataSet();
            //conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            //query = "SELECT * FROM [VEHICLE MAINTENANCE$]";

            //using (var connection = new OleDbConnection(conn))
            //{
            //    using (var da = new OleDbDataAdapter(query, connection))
            //    {
            //        connection.Open();
            //        da.Fill(ds);
            //    }
            //}

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    if (ds.Tables[0].Rows.IndexOf(dr) > 6)
            //    {
            //        if (dr[0].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "")
            //        {
            //            DateTime billDate;
            //            if (!(DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "dd-MM-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate) ||
            //                  DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "M-d-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate)))
            //            {
            //                error = "Date invalid." + dr[2].ToString();
            //                return false;
            //            }

            //            DateTime dtPrev = new DateTime(2021, 3, 31);
            //            if (billDate <= dtPrev)
            //            {
            //                error = "Only bills for current financial year allowed (VEHICLE MAINTENANCE).";
            //                return false;
            //            }

            //            sbTaxDetails.Append("<Flexi COMP_AID='" + compValue + "'");
            //            sbTaxDetails.Append(" EMP_AID='" + empValue + "'");
            //            sbTaxDetails.Append(" ALLWDED_AID='AD000182'");
            //            sbTaxDetails.Append(" PARTICUALRS='" + ReplaceSpecialCharacters(dr[0].ToString().Trim()) + "'");
            //            sbTaxDetails.Append(" PARTICUALRS2=''");
            //            sbTaxDetails.Append(" BILL_NO='" + dr[1].ToString().Trim() + "'");
            //            sbTaxDetails.Append(" BILL_DATE='" + billDate.ToString("dd-MM-yyyy") + "'");
            //            //sbTaxDetails.Append(" AMOUNT_ACCEPTED='" + ((TextBox)gvr.FindControl("txtAcceptedAmount")).Text.Trim() + "'");
            //            //sbTaxDetails.Append(" REMARKS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text.Trim()) + "'");
            //            //sbTaxDetails.Append(" DATE_SAVED='" + ((Label)gvr.FindControl("lblDateSaved")).Text.Trim() + "'");
            //            sbTaxDetails.Append(" AMOUNT='" + dr[3].ToString().Trim() + "'/>");
            //        }
            //    }
            //}

            //ds = new DataSet();
            //conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            //query = "SELECT * FROM [TELEPHONE ALLOWANCE$]";

            //using (var connection = new OleDbConnection(conn))
            //{
            //    using (var da = new OleDbDataAdapter(query, connection))
            //    {
            //        connection.Open();
            //        da.Fill(ds);
            //    }
            //}

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    if (ds.Tables[0].Rows.IndexOf(dr) > 6)
            //    {
            //        if (dr[0].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "")
            //        {
            //            DateTime billDate;
            //            if (!(DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "dd-MM-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate) ||
            //                  DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "M-d-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate)))
            //            {
            //                error = "Date invalid." + dr[2].ToString();
            //                return false;
            //            }

            //            DateTime dtPrev = new DateTime(2021, 3, 31);
            //            if (billDate <= dtPrev)
            //            {
            //                error = "Only bills for current financial year allowed (TELEPHONE ALLOWANCE).";
            //                return false;
            //            }

            //            sbTaxDetails.Append("<Flexi COMP_AID='" + compValue + "'");
            //            sbTaxDetails.Append(" EMP_AID='" + empValue + "'");
            //            sbTaxDetails.Append(" ALLWDED_AID='AD000207'");
            //            sbTaxDetails.Append(" PARTICUALRS='" + ReplaceSpecialCharacters(dr[0].ToString().Trim()) + "'");
            //            sbTaxDetails.Append(" PARTICUALRS2=''");
            //            sbTaxDetails.Append(" BILL_NO='" + dr[1].ToString().Trim() + "'");
            //            sbTaxDetails.Append(" BILL_DATE='" + billDate.ToString("dd-MM-yyyy") + "'");
            //            //sbTaxDetails.Append(" AMOUNT_ACCEPTED='" + ((TextBox)gvr.FindControl("txtAcceptedAmount")).Text.Trim() + "'");
            //            //sbTaxDetails.Append(" REMARKS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text.Trim()) + "'");
            //            //sbTaxDetails.Append(" DATE_SAVED='" + ((Label)gvr.FindControl("lblDateSaved")).Text.Trim() + "'");
            //            sbTaxDetails.Append(" AMOUNT='" + dr[3].ToString().Trim() + "'/>");
            //        }
            //    }
            //}

            //ds = new DataSet();
            //conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            //query = "SELECT * FROM [DRIVER ALLOWANCE$]";

            //using (var connection = new OleDbConnection(conn))
            //{
            //    using (var da = new OleDbDataAdapter(query, connection))
            //    {
            //        connection.Open();
            //        da.Fill(ds);
            //    }
            //}

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    if (ds.Tables[0].Rows.IndexOf(dr) > 6)
            //    {
            //        if (dr[0].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "")
            //        {
            //            DateTime billDate;
            //            if (!(DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "dd-MM-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate) ||
            //                  DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "M-d-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate)))
            //            {
            //                error = "Date invalid." + dr[2].ToString();
            //                return false;
            //            }

            //            DateTime dtPrev = new DateTime(2021, 3, 31);
            //            if (billDate <= dtPrev)
            //            {
            //                error = "Only bills for current financial year allowed (DRIVER ALLOWANCE)." + dr[2].ToString();
            //                return false;
            //            }

            //            sbTaxDetails.Append("<Flexi COMP_AID='" + compValue + "'");
            //            sbTaxDetails.Append(" EMP_AID='" + empValue + "'");
            //            sbTaxDetails.Append(" ALLWDED_AID='AD000153'");
            //            sbTaxDetails.Append(" PARTICUALRS='" + ReplaceSpecialCharacters(dr[0].ToString().Trim()) + "'");
            //            sbTaxDetails.Append(" PARTICUALRS2=''");
            //            sbTaxDetails.Append(" BILL_NO='" + dr[1].ToString().Trim() + "'");
            //            sbTaxDetails.Append(" BILL_DATE='" + billDate.ToString("dd-MM-yyyy") + "'");
            //            //sbTaxDetails.Append(" AMOUNT_ACCEPTED='" + ((TextBox)gvr.FindControl("txtAcceptedAmount")).Text.Trim() + "'");
            //            //sbTaxDetails.Append(" REMARKS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text.Trim()) + "'");
            //            //sbTaxDetails.Append(" DATE_SAVED='" + ((Label)gvr.FindControl("lblDateSaved")).Text.Trim() + "'");
            //            sbTaxDetails.Append(" AMOUNT='" + dr[3].ToString().Trim() + "'/>");
            //        }
            //    }
            //}

            //ds = new DataSet();
            //conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            //query = "SELECT * FROM [MEETING ALLOWANCE$]";

            //using (var connection = new OleDbConnection(conn))
            //{
            //    using (var da = new OleDbDataAdapter(query, connection))
            //    {
            //        connection.Open();
            //        da.Fill(ds);
            //    }
            //}

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    if (ds.Tables[0].Rows.IndexOf(dr) > 6)
            //    {
            //        if (dr[0].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "")
            //        {
            //            DateTime billDate;
            //            if (!(DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "dd-MM-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate) ||
            //                  DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "M-d-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate)))
            //            {
            //                error = "Date invalid." + dr[2].ToString();
            //                return false;
            //            }

            //            DateTime dtPrev = new DateTime(2021, 3, 31);
            //            if (billDate <= dtPrev)
            //            {
            //                error = "Only bills for current financial year allowed (MEETING ALLOWANCE).";
            //                return false;
            //            }

            //            sbTaxDetails.Append("<Flexi COMP_AID='" + compValue + "'");
            //            sbTaxDetails.Append(" EMP_AID='" + empValue + "'");
            //            sbTaxDetails.Append(" ALLWDED_AID='AD000003'");
            //            sbTaxDetails.Append(" PARTICUALRS='" + ReplaceSpecialCharacters(dr[0].ToString().Trim()) + "'");
            //            sbTaxDetails.Append(" PARTICUALRS2=''");
            //            sbTaxDetails.Append(" BILL_NO='" + dr[1].ToString().Trim() + "'");
            //            sbTaxDetails.Append(" BILL_DATE='" + billDate.ToString("dd-MM-yyyy") + "'");
            //            //sbTaxDetails.Append(" AMOUNT_ACCEPTED='" + ((TextBox)gvr.FindControl("txtAcceptedAmount")).Text.Trim() + "'");
            //            //sbTaxDetails.Append(" REMARKS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text.Trim()) + "'");
            //            //sbTaxDetails.Append(" DATE_SAVED='" + ((Label)gvr.FindControl("lblDateSaved")).Text.Trim() + "'");
            //            sbTaxDetails.Append(" AMOUNT='" + dr[3].ToString().Trim() + "'/>");
            //        }
            //    }
            //}

            //ds = new DataSet();
            //conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            //query = "SELECT * FROM [BOOKS AND PERIODICALS$]";

            //using (var connection = new OleDbConnection(conn))
            //{
            //    using (var da = new OleDbDataAdapter(query, connection))
            //    {
            //        connection.Open();
            //        da.Fill(ds);
            //    }
            //}

            //foreach (DataRow dr in ds.Tables[0].Rows)
            //{
            //    if (ds.Tables[0].Rows.IndexOf(dr) > 6)
            //    {
            //        if (dr[0].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "")
            //        {
            //            DateTime billDate;
            //            if (!(DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "dd-MM-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate) ||
            //                  DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "M-d-yyyy",
            //                                                   System.Globalization.CultureInfo.InvariantCulture,
            //                                                   System.Globalization.DateTimeStyles.None, out billDate)))
            //            {
            //                error = "Date invalid." + dr[2].ToString();
            //                return false;
            //            }

            //            DateTime dtPrev = new DateTime(2021, 3, 31);
            //            if (billDate <= dtPrev)
            //            {
            //                error = "Only bills for current financial year allowed (BOOKS AND PERIODICALS).";
            //                return false;
            //            }

            //            sbTaxDetails.Append("<Flexi COMP_AID='" + compValue + "'");
            //            sbTaxDetails.Append(" EMP_AID='" + empValue + "'");
            //            sbTaxDetails.Append(" ALLWDED_AID='AD000140'");
            //            sbTaxDetails.Append(" PARTICUALRS='" + ReplaceSpecialCharacters(dr[0].ToString().Trim()) + "'");
            //            sbTaxDetails.Append(" PARTICUALRS2=''");
            //            sbTaxDetails.Append(" BILL_NO='" + dr[1].ToString().Trim() + "'");
            //            sbTaxDetails.Append(" BILL_DATE='" + billDate.ToString("dd-MM-yyyy") + "'");
            //            //sbTaxDetails.Append(" AMOUNT_ACCEPTED='" + ((TextBox)gvr.FindControl("txtAcceptedAmount")).Text.Trim() + "'");
            //            //sbTaxDetails.Append(" REMARKS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text.Trim()) + "'");
            //            //sbTaxDetails.Append(" DATE_SAVED='" + ((Label)gvr.FindControl("lblDateSaved")).Text.Trim() + "'");
            //            sbTaxDetails.Append(" AMOUNT='" + dr[3].ToString().Trim() + "'/>");
            //        }
            //    }
            //}

            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();

            ds = new DataSet();
            conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            query = "SELECT * FROM [CAR WASHING ALLOWANCE$]";

            using (var connection = new OleDbConnection(conn))
            {
                using (var da = new OleDbDataAdapter(query, connection))
                {
                    connection.Open();
                    da.Fill(ds);
                }
            }

            error = "";

            StringBuilder sbTaxDetails = new StringBuilder();
            string xmlInv = string.Empty;
            string rentAddr = string.Empty;

            sbTaxDetails.Append("<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?>");
            sbTaxDetails.Append("<ROOT>");

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (ds.Tables[0].Rows.IndexOf(dr) > 6)
                {
                    if (dr[0].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "")
                    {
                        DateTime billDate;
                        if (!(DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "dd-MM-yyyy",
                                                               System.Globalization.CultureInfo.InvariantCulture,
                                                               System.Globalization.DateTimeStyles.None, out billDate) ||
                              DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "M-d-yyyy",
                                                               System.Globalization.CultureInfo.InvariantCulture,
                                                               System.Globalization.DateTimeStyles.None, out billDate)))
                        {
                            error = "Date invalid." + dr[2].ToString();
                            return false;
                        }

                        DateTime dtPrev = new DateTime(2021, 3, 31);
                        if (billDate <= dtPrev)
                        {
                            error = "Only bills for current financial year allowed (CAR WASHING ALLOWANCE).";
                            return false;
                        }

                        sbTaxDetails.Append("<Flexi COMP_AID='" + compValue + "'");
                        sbTaxDetails.Append(" EMP_AID='" + empValue + "'");
                        sbTaxDetails.Append(" ALLWDED_AID='AD000152'");
                        sbTaxDetails.Append(" PARTICUALRS='" + ReplaceSpecialCharacters(dr[0].ToString().Trim()) + "'");
                        sbTaxDetails.Append(" PARTICUALRS2=''");
                        sbTaxDetails.Append(" BILL_NO='" + dr[1].ToString().Trim() + "'");
                        sbTaxDetails.Append(" BILL_DATE='" + billDate.ToString("dd-MM-yyyy") + "'");
                        //sbTaxDetails.Append(" AMOUNT_ACCEPTED='" + ((TextBox)gvr.FindControl("txtAcceptedAmount")).Text.Trim() + "'");
                        //sbTaxDetails.Append(" REMARKS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text.Trim()) + "'");
                        //sbTaxDetails.Append(" DATE_SAVED='" + ((Label)gvr.FindControl("lblDateSaved")).Text.Trim() + "'");
                        sbTaxDetails.Append(" AMOUNT='" + dr[3].ToString().Trim() + "'/>");
                    }
                }
            }

            ds = new DataSet();
            conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            query = "SELECT * FROM [CAR FUEL MAINTENANCE$]";

            using (var connection = new OleDbConnection(conn))
            {
                using (var da = new OleDbDataAdapter(query, connection))
                {
                    connection.Open();
                    da.Fill(ds);
                }
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (ds.Tables[0].Rows.IndexOf(dr) > 6)
                {
                    if (dr[0].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "")
                    {
                        DateTime billDate;
                        if (!(DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "dd-MM-yyyy",
                                                               System.Globalization.CultureInfo.InvariantCulture,
                                                               System.Globalization.DateTimeStyles.None, out billDate) ||
                              DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "M-d-yyyy",
                                                               System.Globalization.CultureInfo.InvariantCulture,
                                                               System.Globalization.DateTimeStyles.None, out billDate)))
                        {
                            error = "Date invalid." + dr[2].ToString();
                            return false;
                        }

                        DateTime dtPrev = new DateTime(2021, 3, 31);
                        if (billDate <= dtPrev)
                        {
                            error = "Only bills for current financial year allowed (CAR MAINTENANCE).";
                            return false;
                        }

                        sbTaxDetails.Append("<Flexi COMP_AID='" + compValue + "'");
                        sbTaxDetails.Append(" EMP_AID='" + empValue + "'");
                        sbTaxDetails.Append(" ALLWDED_AID='AD000230'");
                        sbTaxDetails.Append(" PARTICUALRS='" + ReplaceSpecialCharacters(dr[0].ToString().Trim()) + "'");
                        sbTaxDetails.Append(" PARTICUALRS2=''");
                        sbTaxDetails.Append(" BILL_NO='" + dr[1].ToString().Trim() + "'");
                        sbTaxDetails.Append(" BILL_DATE='" + billDate.ToString("dd-MM-yyyy") + "'");
                        //sbTaxDetails.Append(" AMOUNT_ACCEPTED='" + ((TextBox)gvr.FindControl("txtAcceptedAmount")).Text.Trim() + "'");
                        //sbTaxDetails.Append(" REMARKS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text.Trim()) + "'");
                        //sbTaxDetails.Append(" DATE_SAVED='" + ((Label)gvr.FindControl("lblDateSaved")).Text.Trim() + "'");
                        sbTaxDetails.Append(" AMOUNT='" + dr[3].ToString().Trim() + "'/>");
                    }
                }
            }

            ds = new DataSet();
            conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            query = "SELECT * FROM [CAR DRIVER ALLOWANCE$]";

            using (var connection = new OleDbConnection(conn))
            {
                using (var da = new OleDbDataAdapter(query, connection))
                {
                    connection.Open();
                    da.Fill(ds);
                }
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (ds.Tables[0].Rows.IndexOf(dr) > 6)
                {
                    if (dr[0].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "")
                    {
                        DateTime billDate;
                        if (!(DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "dd-MM-yyyy",
                                                               System.Globalization.CultureInfo.InvariantCulture,
                                                               System.Globalization.DateTimeStyles.None, out billDate) ||
                              DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "M-d-yyyy",
                                                               System.Globalization.CultureInfo.InvariantCulture,
                                                               System.Globalization.DateTimeStyles.None, out billDate)))
                        {
                            error = "Date invalid." + dr[2].ToString();
                            return false;
                        }

                        DateTime dtPrev = new DateTime(2021, 3, 31);
                        if (billDate <= dtPrev)
                        {
                            error = "Only bills for current financial year allowed (CAR DRIVER ALLOWANCE).";
                            return false;
                        }

                        sbTaxDetails.Append("<Flexi COMP_AID='" + compValue + "'");
                        sbTaxDetails.Append(" EMP_AID='" + empValue + "'");
                        sbTaxDetails.Append(" ALLWDED_AID='AD000153'");
                        sbTaxDetails.Append(" PARTICUALRS='" + ReplaceSpecialCharacters(dr[0].ToString().Trim()) + "'");
                        sbTaxDetails.Append(" PARTICUALRS2=''");
                        sbTaxDetails.Append(" BILL_NO='" + dr[1].ToString().Trim() + "'");
                        sbTaxDetails.Append(" BILL_DATE='" + billDate.ToString("dd-MM-yyyy") + "'");
                        //sbTaxDetails.Append(" AMOUNT_ACCEPTED='" + ((TextBox)gvr.FindControl("txtAcceptedAmount")).Text.Trim() + "'");
                        //sbTaxDetails.Append(" REMARKS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text.Trim()) + "'");
                        //sbTaxDetails.Append(" DATE_SAVED='" + ((Label)gvr.FindControl("lblDateSaved")).Text.Trim() + "'");
                        sbTaxDetails.Append(" AMOUNT='" + dr[3].ToString().Trim() + "'/>");
                    }
                }
            }

            ds = new DataSet();
            conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + strPath + ";Extended Properties=\"Excel 12.0;HDR=YES;\"";
            query = "SELECT * FROM [RESIDENTIAL TELEPHONE ALLOWANCE$]";

            using (var connection = new OleDbConnection(conn))
            {
                using (var da = new OleDbDataAdapter(query, connection))
                {
                    connection.Open();
                    da.Fill(ds);
                }
            }

            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (ds.Tables[0].Rows.IndexOf(dr) > 6)
                {
                    if (dr[0].ToString().Trim() != "" && dr[2].ToString().Trim() != "" && dr[3].ToString().Trim() != "")
                    {
                        DateTime billDate;
                        if (!(DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "dd-MM-yyyy",
                                                               System.Globalization.CultureInfo.InvariantCulture,
                                                               System.Globalization.DateTimeStyles.None, out billDate) ||
                              DateTime.TryParseExact(dr[2].ToString().Trim().Replace("/", "-"), "M-d-yyyy",
                                                               System.Globalization.CultureInfo.InvariantCulture,
                                                               System.Globalization.DateTimeStyles.None, out billDate)))
                        {
                            error = "Date invalid." + dr[2].ToString();
                            return false;
                        }

                        DateTime dtPrev = new DateTime(2021, 3, 31);
                        if (billDate <= dtPrev)
                        {
                            error = "Only bills for current financial year allowed (RESIDENTIAL TELEPHONE ALLOWANCE).";
                            return false;
                        }

                        sbTaxDetails.Append("<Flexi COMP_AID='" + compValue + "'");
                        sbTaxDetails.Append(" EMP_AID='" + empValue + "'");
                        sbTaxDetails.Append(" ALLWDED_AID='AD000238'");
                        sbTaxDetails.Append(" PARTICUALRS='" + ReplaceSpecialCharacters(dr[0].ToString().Trim()) + "'");
                        sbTaxDetails.Append(" PARTICUALRS2=''");
                        sbTaxDetails.Append(" BILL_NO='" + dr[1].ToString().Trim() + "'");
                        sbTaxDetails.Append(" BILL_DATE='" + billDate.ToString("dd-MM-yyyy") + "'");
                        //sbTaxDetails.Append(" AMOUNT_ACCEPTED='" + ((TextBox)gvr.FindControl("txtAcceptedAmount")).Text.Trim() + "'");
                        //sbTaxDetails.Append(" REMARKS='" + ReplaceSpecialCharacters(((TextBox)gvr.FindControl("txtRemarks")).Text.Trim()) + "'");
                        //sbTaxDetails.Append(" DATE_SAVED='" + ((Label)gvr.FindControl("lblDateSaved")).Text.Trim() + "'");
                        sbTaxDetails.Append(" AMOUNT='" + dr[3].ToString().Trim() + "'/>");
                    }
                }
            }



            sbTaxDetails.Append("</ROOT>");

            xmlInv = sbTaxDetails.ToString();

            status = UpdateFlexipayCompensation(xmlInv, compValue, empValue);

            return status;
        }

        public DataSet UpdateMonthlyReimb(string xmlValue, string type, string CompId, string Empid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@CTCXml", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, xmlValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, type.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEMONTHLYAMT");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetMonthlyReimb(string type, string CompId, string Empid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, type.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMONTHLYAMT");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        private string ReplaceSpecialCharacters(string inputString)
        {
            inputString = inputString.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("'", "&#39;").
                    Replace("\"", "&quot;");

            return inputString;
        }

        public DataSet GetLTALimit(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLTALIMIT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet LoadLTAData(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLTADATA");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public void UpdateLTAData(string CompId, string Empid, string limit, string opt, string amt)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@CAR_LEASE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, limit.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, opt.ToString().Trim());
            objDBUtility.AddParameters("@AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, amt.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTLTA");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();
        }

        public DataSet GetReimbData(string compValue, string empValue, string period)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, period.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREIMBDATA");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetReimbDataAll(string compValue, string empValue, string period, string allId)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, period.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREIMBDATAALL");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet GetReimbDataAllTelClaim(string compValue, string empValue, string allId, string finyear, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@FINYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, finyear.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPHONEBILLCLAIM");



            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }



        public DataSet GetReimbDataAllTelClaimByFinYear(string compValue, string empValue, string allId, string entryAid, string finyear)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@FINYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, finyear.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPHONEBILLCLAIMBYID");
            //objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPHONEBILLCLAIM");



            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetFinYear(string compValue, string empValue, string allId, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "CURRENTFINYEAR");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet GetYearById(string compValue, string empValue, string allId, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETYEARBYID");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet GetReimbYearById(string compValue, string empValue, string allId, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREIMBYEARBYID");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet GetReimbDataAllTelClaimById(string compValue, string empValue, string allId, string entryAid, string finyear)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@FINYEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, finyear.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPHONEBILLCLAIMBYID");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet getCountReimbByFinyear(string compValue, string empValue, string allId, string drpFinYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@FINYEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpFinYear.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOUNTPHONEBILLCLAIMBYFINEYEAR");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet GetReimbDataAllTelClaimByFinYear(string compValue, string empValue, string allId, string drpFinYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@FINYEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpFinYear.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPHONEBILLCLAIMBYFINEYEAR");

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet GetReimbDataAllCompPurClaim(string compValue, string empValue, string allId, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMPPURCLAIM");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetReimbDataBusinessTravel(string compValue, string empValue, string allId, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETBUSINESSTRAVELCLAIM");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetReimbDataBusinessTravelReport(string compValue, string empValue, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETBUSINESSTRAVELCLAIMREPORT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GentReimbDataBusinessTravelReport(string compValue, string empValue, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GENTBUSINESSTRAVELCLAIMREPORT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetReimbDataAllReport(string compValue, string empValue, string period, string allId)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, period.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "REPORTREIMBDATAALL");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet InsertReimbDataAll(string compValue, string empValue, string period, string allId)
        {
            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@LOCATION", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EmpLocn == null ? "" : EmpLocn.ToString().Trim());
            objDBUtility.AddParameters("@EMPADDR", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EmpAddr == null ? "" : EmpAddr.ToString().Trim());
            objDBUtility.AddParameters("@VENDOR", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Vendor == null ? "" : Vendor.ToString().Trim());
            objDBUtility.AddParameters("@INVNO", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, InvoiceNo == null ? "" : InvoiceNo.ToString().Trim());
            objDBUtility.AddParameters("@INVDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, InvoiceDate == null ? "" : InvoiceDate.ToString().Trim());

            objDBUtility.AddParameters("@PHONENO", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, PhoneNumber == null ? "" : PhoneNumber.ToString().Trim());
            objDBUtility.AddParameters("@BILLNO", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, BillNo == null ? "" : BillNo.ToString().Trim());
            objDBUtility.AddParameters("@BILLAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, BillAmt == null ? "" : BillAmt.ToString().Trim());
            objDBUtility.AddParameters("@BILLDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, BillDate == null ? "" : BillDate.ToString().Trim());
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate == null ? "" : FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate == null ? "" : ToDate.ToString().Trim());

            objDBUtility.AddParameters("@BRAND", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Brand == null ? "" : Brand.ToString().Trim());
            objDBUtility.AddParameters("@BRANDOTHER", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, BrandOther == null ? "" : BrandOther.ToString().Trim());
            objDBUtility.AddParameters("@AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Amount == null ? "" : Amount.ToString().Trim());
            objDBUtility.AddParameters("@ISFINAL", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, IsFinal.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FilingStatus.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYCODE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EntryCode.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, period.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@FINANCIALYEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FinYears.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTREIMBDATAALL");

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public void UpdateReimbDataAll(string compValue, string empValue, string period, string allId)
        {
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@LOCATION", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EmpLocn == null ? "" : EmpLocn.ToString().Trim());
            objDBUtility.AddParameters("@EMPADDR", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EmpAddr == null ? "" : EmpAddr.ToString().Trim());
            objDBUtility.AddParameters("@VENDOR", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Vendor == null ? "" : Vendor.ToString().Trim());
            objDBUtility.AddParameters("@INVNO", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, InvoiceNo == null ? "" : InvoiceNo.ToString().Trim());
            objDBUtility.AddParameters("@INVDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, InvoiceDate == null ? "" : InvoiceDate.ToString().Trim());

            objDBUtility.AddParameters("@PHONENO", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, PhoneNumber == null ? "" : PhoneNumber.ToString().Trim());
            objDBUtility.AddParameters("@BILLNO", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, BillNo == null ? "" : BillNo.ToString().Trim());
            objDBUtility.AddParameters("@BILLAMT", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, BillAmt == null ? "" : BillAmt.ToString().Trim());
            objDBUtility.AddParameters("@BILLDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, BillDate == null ? "" : BillDate.ToString().Trim());
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate == null ? "" : FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate == null ? "" : ToDate.ToString().Trim());

            objDBUtility.AddParameters("@BRAND", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Brand == null ? "" : Brand.ToString().Trim());
            objDBUtility.AddParameters("@BRANDOTHER", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, BrandOther == null ? "" : BrandOther.ToString().Trim());
            objDBUtility.AddParameters("@AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Amount == null ? "" : Amount.ToString().Trim());
            objDBUtility.AddParameters("@ISFINAL", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, IsFinal.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FilingStatus.ToString().Trim());
            objDBUtility.AddParameters("@FINANCIALYEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, FinYears.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEREIMBDATAALL");

            objDBUtility.Execute_StoreProc("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();
        }


        public DataSet InsertReimbDataBT(string compValue, string empValue, string saveType, string allId)
        {
            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@TRAVELMODE1", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TravelMode1.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELMODE2", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TravelMode2.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELMODEOTHER", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TravelModeOther.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELFROM", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, From.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELFROMTEXT", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromText.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELTO", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, To.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELTOTEXT", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToText.ToString().Trim());
            objDBUtility.AddParameters("@DEPARTUREDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DepartureDate.ToString().Trim());
            objDBUtility.AddParameters("@DEPARTURETIME", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DepartureTime.ToString().Trim());
            objDBUtility.AddParameters("@DEPARTUREDATEDEST", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DepartureDateDest.ToString().Trim());
            objDBUtility.AddParameters("@DEPARTURETIMEDEST", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DepartureTimeDest.ToString().Trim());
            objDBUtility.AddParameters("@REACHDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ReachDate.ToString().Trim());
            objDBUtility.AddParameters("@REACHTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ReachTime.ToString().Trim());
            objDBUtility.AddParameters("@REACHDATEDEST", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ReachDateDest.ToString().Trim());
            objDBUtility.AddParameters("@REACHTIMEDEST", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ReachTimeDest.ToString().Trim());
            objDBUtility.AddParameters("@STAYSDAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, StayDays.ToString().Trim());
            objDBUtility.AddParameters("@DAILYALLOWANCEDAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DailyAllowanceDays.ToString().Trim());

            objDBUtility.AddParameters("@VISITREASON", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, VisitReason.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Remarks.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELEXPENSE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TravelExpense.ToString().Trim());
            objDBUtility.AddParameters("@HOTELEXPENSE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, HotelExpense.ToString().Trim());
            objDBUtility.AddParameters("@DAILYALLOWANCE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DailyAllowance.ToString().Trim());
            objDBUtility.AddParameters("@OTHEREXPENSE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, OtherExpense.ToString().Trim());
            objDBUtility.AddParameters("@ISFINAL", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, IsFinal.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FilingStatus.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            // objDBUtility.AddParameters("@TRAVELXML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TRAVELXML.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ("INSERTBUSINESSTRAVELCLAIM"));
            //objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, (saveType == "0" ? "INSERTBUSINESSTRAVELCLAIM" : "UPDATEBUSINESSTRAVELCLAIM"));

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet InsertReimbDataBTMultiple(string compValue, string empValue, string saveType, string allId)
        {
            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();
            objDBUtility.AddParameters("@TRAVELMODE1", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TravelMode1.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELMODE2", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TravelMode2.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELMODEOTHER", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TravelModeOther.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELFROM", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, From.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELFROMTEXT", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromText.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELTO", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, To.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELTOTEXT", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToText.ToString().Trim());
            objDBUtility.AddParameters("@DEPARTUREDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DepartureDate.ToString().Trim());
            objDBUtility.AddParameters("@DEPARTURETIME", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DepartureTime.ToString().Trim());
            objDBUtility.AddParameters("@DEPARTUREDATEDEST", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DepartureDateDest.ToString().Trim());
            objDBUtility.AddParameters("@DEPARTURETIMEDEST", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DepartureTimeDest.ToString().Trim());
            objDBUtility.AddParameters("@REACHDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ReachDate.ToString().Trim());
            objDBUtility.AddParameters("@REACHTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ReachTime.ToString().Trim());
            objDBUtility.AddParameters("@REACHDATEDEST", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ReachDateDest.ToString().Trim());
            objDBUtility.AddParameters("@REACHTIMEDEST", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ReachTimeDest.ToString().Trim());
            objDBUtility.AddParameters("@STAYSDAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, StayDays.ToString().Trim());
            objDBUtility.AddParameters("@DAILYALLOWANCEDAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DailyAllowanceDays.ToString().Trim());

            objDBUtility.AddParameters("@VISITREASON", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, VisitReason.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Remarks.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELEXPENSE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TravelExpense.ToString().Trim());
            objDBUtility.AddParameters("@HOTELEXPENSE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, HotelExpense.ToString().Trim());
            objDBUtility.AddParameters("@DAILYALLOWANCE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DailyAllowance.ToString().Trim());
            objDBUtility.AddParameters("@OTHEREXPENSE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, OtherExpense.ToString().Trim());
            objDBUtility.AddParameters("@ISFINAL", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, IsFinal.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FilingStatus.ToString().Trim());
            //objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ("INSERTMULTIPLEBUSINESSTRAVELCLAIM"));

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet UpdateReimbDataBT(string compValue, string empValue, string saveType, string allId)
        {
            DataSet ds = new DataSet();
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@TRAVELMODE1", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TravelMode1.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELMODE2", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TravelMode2.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELMODEOTHER", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TravelModeOther.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELFROM", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, From.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELFROMTEXT", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromText.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELTO", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, To.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELTOTEXT", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToText.ToString().Trim());
            objDBUtility.AddParameters("@DEPARTUREDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DepartureDate.ToString().Trim());
            objDBUtility.AddParameters("@DEPARTURETIME", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DepartureTime.ToString().Trim());
            objDBUtility.AddParameters("@DEPARTUREDATEDEST", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DepartureDateDest.ToString().Trim());
            objDBUtility.AddParameters("@DEPARTURETIMEDEST", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DepartureTimeDest.ToString().Trim());
            objDBUtility.AddParameters("@REACHDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ReachDate.ToString().Trim());
            objDBUtility.AddParameters("@REACHTIME", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ReachTime.ToString().Trim());
            objDBUtility.AddParameters("@REACHDATEDEST", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ReachDateDest.ToString().Trim());
            objDBUtility.AddParameters("@REACHTIMEDEST", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ReachTimeDest.ToString().Trim());
            objDBUtility.AddParameters("@STAYSDAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, StayDays.ToString().Trim());
            objDBUtility.AddParameters("@DAILYALLOWANCEDAYS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DailyAllowanceDays.ToString().Trim());

            objDBUtility.AddParameters("@VISITREASON", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, VisitReason.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Remarks.ToString().Trim());
            objDBUtility.AddParameters("@TRAVELEXPENSE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TravelExpense.ToString().Trim());
            objDBUtility.AddParameters("@HOTELEXPENSE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, HotelExpense.ToString().Trim());
            objDBUtility.AddParameters("@DAILYALLOWANCE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, DailyAllowance.ToString().Trim());
            objDBUtility.AddParameters("@OTHEREXPENSE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, OtherExpense.ToString().Trim());
            objDBUtility.AddParameters("@ISFINAL", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, IsFinal.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FilingStatus.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            //  objDBUtility.AddParameters("@TRAVELXML", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, TRAVELXML.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());


            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, ("UPDATEBUSINESSTRAVELCLAIM"));

            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet Upload_Reimb(string compValue, string empValue, string fileName, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FILENAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, fileName.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "HISTORYREIMB");
            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet Upload_ReimbTravel(string compValue, string empValue, string fileName, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet ds = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@FILENAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, fileName.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "HISTORYREIMBTRAVEL");
            ds = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return ds;
        }

        public DataSet GetPhoneList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPHONELIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet ValidatePhoneNumber(string compValue, string empValue, string phoneNo)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@PHONENO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, phoneNo.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "VALIDATEPHONENO");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet ValidatePhoneType(string compValue, string empValue, string phoneType)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@PHONETYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, phoneType.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "VALIDATEPHONETYPE");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public void InsertPhoneDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@PHONENO", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, PhoneNumber.ToString().Trim());
            objDBUtility.AddParameters("@PHONETYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, PhoneType.ToString().Trim());
            objDBUtility.AddParameters("@PROVIDER", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ServiceProvider.ToString().Trim());
            objDBUtility.AddParameters("@BILLDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, BillDate.ToString().Trim());
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@ACTIVEDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ActiveDate.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTPHONEDETAILS");

            objDBUtility.Execute_StoreProc("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();
        }

        public void UpdatePhoneDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@PHONENO", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, PhoneNumber.ToString().Trim());
            objDBUtility.AddParameters("@PHONETYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, PhoneType.ToString().Trim());
            objDBUtility.AddParameters("@PROVIDER", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ServiceProvider.ToString().Trim());
            objDBUtility.AddParameters("@BILLDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, BillDate.ToString().Trim());
            objDBUtility.AddParameters("@FROMDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FromDate.ToString().Trim());
            objDBUtility.AddParameters("@TODATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ToDate.ToString().Trim());
            objDBUtility.AddParameters("@ACTIVEDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, ActiveDate.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EntryAid.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEPHONEDETAILS");

            objDBUtility.Execute_StoreProc("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();
        }

        public void DeactivatePhone(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "DEACTIVATEPHONE");

            objDBUtility.Execute_StoreProc("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();
        }

        public DataSet GetPhoneView(string aid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, aid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPHONEVIEW");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPhoneDropdown(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPHONELISTDROPDOWN");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPhoneBillCycle(string compValue, string empValue, string phoneNo)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@PHONENO", DBUtilDBType.Varchar, DBUtilDirection.In, 50, phoneNo.ToString().Trim());

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPHONEBILLPERIOD");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPhoneBillClaimList(string compValue, string empValue, string drpFinYears)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@FINYEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpFinYears.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPHONEBILLCLAIMLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPhoneBillClaimCheckingList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPHONECLAIMFORCHECKINGLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPhoneBillClaimApproverList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPHONECLAIMFORAPPROVERLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetCompPurClaimCheckingList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMPPURFORCHECKINGLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetCompPurClaimApproverList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMPPURFORAPPROVERLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetBusinessTravelClaimCheckingList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETBTFORCHECKINGLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetBusinessTravelClaimReviewerList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETBTFORREVIEWLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetBusinessTravelClaimApproverList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETBTFORAPPROVERLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet SetClaimChecked(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@CHECKED", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Remarks.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECLAIMCHECKED");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet SetClaimApproved(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@CHECKED", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Remarks.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECLAIMAPPROVED");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet SetClaimCheckedBT(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@CHECKED", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Remarks.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECLAIMCHECKEDBT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet SetClaimReviewedBT(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@CHECKED", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Remarks.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECLAIMREVIEWEDBT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet SetClaimApprovedBT(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@CHECKED", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Remarks.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECLAIMAPPROVEDBT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet GETEMPTRAVELSDETAILS(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPTRAVELSDETAILS");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet GetReportingOfficerName(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREPORTINGOFFICERNAMES");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataTable GetBusinessTravelCityList(DropDownList drpList)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETBTCITY");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            dt = dsInv.Tables[0];

            drpList.Items.Clear();
            drpList.DataTextField = "DESC";
            drpList.DataValueField = "AID";
            drpList.DataSource = dsInv.Tables[0];
            drpList.DataBind();
            drpList.Items.Insert(0, new ListItem("[Select One]", ""));
            drpList.Items.Add(new ListItem("Other", "9999"));

            objDBUtility.ClearTransactionalParams();

            return dt;
        }


        public DataTable GetFinancialYearList(string compValue, string empValue, string allId, DropDownList drpSelectFinancialYear)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETFINANCIALYEAR");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            dt = dsInv.Tables[0];

            drpSelectFinancialYear.Items.Clear();
            drpSelectFinancialYear.DataTextField = "DES";
            drpSelectFinancialYear.DataValueField = "AID";
            drpSelectFinancialYear.DataSource = dsInv.Tables[0];
            drpSelectFinancialYear.DataBind();
            drpSelectFinancialYear.Items.Insert(0, new ListItem("[Select Year]", ""));
            //drpSelectFinancialYear.Items.Add(new ListItem("Other", "9999"));

            objDBUtility.ClearTransactionalParams();

            return dt;
        }

        public DataSet GetBusinessTravelClaimList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETBTCLAIMLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet GetMultipleBusinessTravelClaimList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETMTBTCLAIMLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetBusinessTravelEntitlement(string compValue, string empValue, string city)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@LOCATION", DBUtilDBType.Varchar, DBUtilDirection.In, 50, city.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETBTENTITLEMENT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetRole(string compValue, string empValue, string allwValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allwValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETROLEFORREIMB");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataTable GetEMPDetailsAngel(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPAID");
            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");
            dt = dsInv.Tables[0];
            objDBUtility.ClearTransactionalParams();

            return dt;
        }

      
        public DataSet GetEmpDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPPOCKETDETAILS");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPockAppDate(string compValue, string empValue, string allwValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPOCKETREIMBDATE");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allwValue.ToString().Trim());
            //objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, invYear.ToString().Trim());
            //objDBUtility.AddParameters("@CURRENTMONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 100, month.ToString().Trim());
            objDBUtility.AddParameters("@DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 100, Date.ToString().Trim());
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet GetPockAppID(string compValue, string empValue, string allwValue, string invYear, string month)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPOCKETREIMBIDDETAIL");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allwValue.ToString().Trim());
            objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, invYear.ToString().Trim());
            objDBUtility.AddParameters("@CURRENTMONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 100, month.ToString().Trim());
            
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet CreateInsertPocketReimb(string compValue, string empValue, string allwValue,string empName, string empAid)
        {
            objDBUtility = new DBUtility();
            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTNEWPOCKETREIMB");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allwValue.ToString().Trim());
            objDBUtility.AddParameters("@DAYNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DayName.ToString().Trim());
            objDBUtility.AddParameters("@DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Date.ToString().Trim());
            objDBUtility.AddParameters("@DAYCATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DayCategory.ToString().Trim());
            objDBUtility.AddParameters("@FROMHRS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Fromhrs.ToString().Trim());
            objDBUtility.AddParameters("@TOHRS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Tohrs.ToString().Trim());
            objDBUtility.AddParameters("@TOTAL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Total.ToString().Trim());
            objDBUtility.AddParameters("@PARTICULARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Particulars.ToString().Trim());
            objDBUtility.AddParameters("@REMBDIS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, RembDis.ToString().Trim());
            //objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, invYear.ToString().Trim());
            //objDBUtility.AddParameters("@CURRENTMONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 100, month.ToString().Trim());
            objDBUtility.AddParameters("@EMP_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, empName.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, empAid.ToString().Trim());
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet InsertSamePocketReimbAppId(string compValue, string empValue, string allwValue, string empName, string empAid)
        {
            objDBUtility = new DBUtility();
            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTSAMEPOCKETREIMBID");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_NAME", DBUtilDBType.Varchar, DBUtilDirection.In, 100, empName.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allwValue.ToString().Trim());
            objDBUtility.AddParameters("@DAYNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DayName.ToString().Trim());
            objDBUtility.AddParameters("@DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Date.ToString().Trim());
            objDBUtility.AddParameters("@DAYCATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DayCategory.ToString().Trim());
            objDBUtility.AddParameters("@FROMHRS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Fromhrs.ToString().Trim());
            objDBUtility.AddParameters("@TOHRS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Tohrs.ToString().Trim());
            objDBUtility.AddParameters("@TOTAL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Total.ToString().Trim());
            objDBUtility.AddParameters("@PARTICULARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Particulars.ToString().Trim());
            objDBUtility.AddParameters("@REMBDIS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, RembDis.ToString().Trim());
            //objDBUtility.AddParameters("@INV_YEAR", DBUtilDBType.Varchar, DBUtilDirection.In, 100, invYear.ToString().Trim());
            //objDBUtility.AddParameters("@CURRENTMONTH", DBUtilDBType.Varchar, DBUtilDirection.In, 100, month.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empAid.ToString().Trim());

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPocketClaimList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPOCKETREIMBLIST");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
           
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        //public DataSet GetMonthYear(string compValue, string empValue, string allwValue)
        //{
        //    objDBUtility = new DBUtility();

        //    DataSet dsLogin = null;

        //    objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPOCKETREIMBMONTHYEAR");
        //    objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
        //    objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
        //    objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allwValue.ToString().Trim());

        //    dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

        //    objDBUtility.ClearTransactionalParams();

        //    return dsLogin;
        //}
           //public DataSet GetPocketClaimByYear(string compValue, string empValue, string pocketAppAid)
            public DataSet GetPocketClaimByYear(string compValue,  string pocketAppAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPOCKETCLAIMLISTBYID");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
           // objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@POCKET_APP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pocketAppAid.ToString().Trim());

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPocketClaimById(string compValue, string empValue, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPOCKETCLAIMBYID");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, entryAid.ToString().Trim());
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet UpdateSamePocketReimbAppId(string compValue, string empValue, string entryAid)
        {
            objDBUtility = new DBUtility();
            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATESAMEPOCKETREIMBID");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@DAYNAME", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DayName.ToString().Trim());
            objDBUtility.AddParameters("@DATE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Date.ToString().Trim());
            objDBUtility.AddParameters("@DAYCATEGORY", DBUtilDBType.Varchar, DBUtilDirection.In, 50, DayCategory.ToString().Trim());
            objDBUtility.AddParameters("@FROMHRS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Fromhrs.ToString().Trim());
            objDBUtility.AddParameters("@TOHRS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Tohrs.ToString().Trim());
            objDBUtility.AddParameters("@TOTAL", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Total.ToString().Trim());
            objDBUtility.AddParameters("@PARTICULARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Particulars.ToString().Trim());
            objDBUtility.AddParameters("@REMBDIS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, RembDis.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 100, entryAid.ToString().Trim());
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet SetClaimReviewedPKT(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@POCKET_APP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@CHECKED", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Remarks.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECLAIMREVIEWEDPKT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet SetClaimApprovePKT(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@POCKET_APP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@CHECKED", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Status.ToString().Trim());
            objDBUtility.AddParameters("@REMARKS", DBUtilDBType.Varchar, DBUtilDirection.In, 500, Remarks.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATECLAIMAPPROVEDPKT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPktClaimReviewerList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPKTFORREVIEWLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPktClaimApproverList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPKTFORAPPROVERLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }


        public DataSet GetReportingUnderWiseId(string compValue, string empValue,string roleType)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREPORTINGWISEID");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ROLE_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, roleType.ToString().Trim());

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPktRole(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();
            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETROLEFORPKT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetReportingRoleWiseData(string compValue, string empValue,string roleType)
        {
            
            objDBUtility = new DBUtility();
            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ROLE_TYPE", DBUtilDBType.Varchar, DBUtilDirection.In, 50, roleType.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREPORTINGWISEIDDETAILS");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            
            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetOutOFPocketAuditTrial(string compValue, string empValue, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "OUTOFPOCKETAUDITTRAIL");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet FinalUpdatePocketClaimByYear(string compValue, string empValue, string pocketAppAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEFINALSUBMIT");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());

            objDBUtility.AddParameters("@POCKET_APP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pocketAppAid.ToString().Trim());

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetPockreimnIdSumbitStatus(string compValue, string empValue, string pocketAppAid)
        {
           objDBUtility = new DBUtility();

        DataSet dsLogin = null;

        objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPOCKETREIMBIDSUBMITSTATUS");
        objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
        objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());

        objDBUtility.AddParameters("@POCKET_APP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pocketAppAid.ToString().Trim());

        dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

        objDBUtility.ClearTransactionalParams();

        return dsLogin;
        }

        public DataSet GetPocketClaimCountById(string compValue, string pocketAppAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETFINALPKTSUBMITCOUNT");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@POCKET_APP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pocketAppAid.ToString().Trim());

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet GetTotalTimeCalculation(string compValue, string empValue, string pocketAppAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETTOTALCALCULATION");
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            //objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@POCKET_APP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pocketAppAid.ToString().Trim());

            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet UpdateValidation(string compValue, string empValue, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEVALIDATION");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet GetOutOfPocketReport(string compValue, string empValue, string pocketAppAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@POCKET_APP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pocketAppAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETOUTOFPOCKETREPORT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet GetReportingOfficerDetails(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETPCTREPORTINGOFFICERNAME");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet GetMonthAndYear(string compValue, string empValue, string pocketAppAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@POCKET_APP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, pocketAppAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETREPORTMONTHANDYEAR");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public void InsertReimbcCompMaintDataAll(string compValue, string empValue, string period, string allId)
        {
            objDBUtility = new DBUtility();


            objDBUtility.AddParameters("@BRAND", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Brand == null ? "" : Brand.ToString().Trim());
            objDBUtility.AddParameters("@BRANDOTHER", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, BrandOther == null ? "" : BrandOther.ToString().Trim());
            objDBUtility.AddParameters("@AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Amount == null ? "" : Amount.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@INVDATE", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, InvoiceDate == null ? "" : InvoiceDate.ToString().Trim());
            //objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTEREIMBDCOMPMAINTDATA");

            objDBUtility.Execute_StoreProc("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();
        }
        public DataSet GetCompMainBillClaimList(string compValue, string empValue, string drpFinYears)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@FINYEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpFinYears.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMPMAINBILLCLAIMLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet GetComMainStatus(string compValue, string empValue, string EntryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMMAINSTATUS");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet GetCompMainDeltailsById(string compValue, string empValue, string allId, string drpFinYears, string entriAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@FINYEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpFinYears.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entriAid.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMPMAINDETAILSBYID");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet GetCompMainAmount(string compValue, string empValue)
        {

            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            //objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMPMAINAVAILEDAMOUNT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;

        }
        public void UpdateReimbcCompMaintDataAll(string compValue, string empValue, string period, string allId)
        {
            objDBUtility = new DBUtility();


            objDBUtility.AddParameters("@BRAND", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Brand == null ? "" : Brand.ToString().Trim());
            objDBUtility.AddParameters("@BRANDOTHER", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, BrandOther == null ? "" : BrandOther.ToString().Trim());
            objDBUtility.AddParameters("@AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, Amount == null ? "" : Amount.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, EntryAid.ToString().Trim());
            objDBUtility.AddParameters("@ISFINAL", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, IsFinal.ToString().Trim());
            objDBUtility.AddParameters("@FILINGSTATUS", DBUtilDBType.Varchar, DBUtilDirection.In, 80000, FilingStatus.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "UPDATEREIMBDCOMPMAINTDATA");

            objDBUtility.Execute_StoreProc("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();
        }
        public DataSet GetCompMainRepairReport(string compValue, string empValue, string entryAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMMAINREPAIRREPORT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet GetCompRepairMainDeltailsById(string compValue, string empValue, string allId, string drpFinYears, string entriAid)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@FINYEARS", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpFinYears.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entriAid.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMPREPAIRMAINDETAILSBYID");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet GetCompRepairMainAmount(string compValue, string empValue, string entryAid)
        {

            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_MID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ENTRYAID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, entryAid.ToString().Trim());
            //objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allId.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMPREPAIRMAINAVAILEDAMOUNT");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;

        }
        public DataSet GetCompMaintBillClaimCheckingList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMPMAINTCLAIMFORCHECKINGLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet GetCompMaintBillClaimApproverList(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETCOMPMAINTCLAIMFORAPPROVERLIST");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }
        public DataSet GetPaswords(string compValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsLogin = null;

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETEMPLOYEEPASSWORD");
            dsLogin = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsLogin;
        }

        public DataSet getQuarters(string CompId, string Empid)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETQUARTER");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

   

        public DataSet InsertFlexiCompensationBillsAmt(string compValue, string empValue, string allAid, string drpQtr)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
            objDBUtility.AddParameters("@ALL_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allAid.ToString().Trim());
            objDBUtility.AddParameters("@AMT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Amount.ToString().Trim());
            objDBUtility.AddParameters("@QTR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpQtr.ToString().Trim());
            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "INSERTFLEXICOMPSENSATIONBILLAMT");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();


            return dsInv;
        }
        public DataSet GetFlexiCompensationPreQtr(string CompId, string Empid, string drpQtr)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@QTR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, drpQtr.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETQTRAMT");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }
        public DataSet Fill_ExcelReport(string compValue, string empValue)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();

            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());

            objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETALLFINYEARREPORT");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }


        public DataTable GetFlexiCompensationQTR(string compValue, string empValue, DropDownList drpFlexiHeads, string qtr)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();
            DataTable dt = new DataTable();
            try
            {
                objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compValue.ToString().Trim());
                objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, empValue.ToString().Trim());
                objDBUtility.AddParameters("@QTR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, qtr.ToString().Trim());
                objDBUtility.AddParameters("@EVENT", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETALLWGRADEQTR");

                dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

                dt = dsInv.Tables[0];

                drpFlexiHeads.Items.Clear();
                drpFlexiHeads.DataTextField = "DESC";
                drpFlexiHeads.DataValueField = "AID";
                drpFlexiHeads.DataSource = dsInv.Tables[1];
                drpFlexiHeads.DataBind();
                drpFlexiHeads.Items.Insert(0, new ListItem("[Select One]", ""));

                objDBUtility.ClearTransactionalParams();
            }
            catch (Exception ex)
            {
                //CreateErrorLog("", ex.Message, "Common_Validate_Login");
            }

            return dt;
        }


        public DataSet GetAllwYearlyAmt(string CompId, string Empid, string allwId)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, CompId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@ALLWDED_ID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, allwId.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETQTRSUM");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();

            return dsInv;
        }

        public DataSet GetLATAMT(string compId, string Empid, string qtr)
        {
            objDBUtility = new DBUtility();

            DataSet dsInv = new DataSet();


            objDBUtility.AddParameters("@QTR", DBUtilDBType.Varchar, DBUtilDirection.In, 50, qtr.ToString().Trim());
            objDBUtility.AddParameters("@COMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, compId.ToString().Trim());
            objDBUtility.AddParameters("@EMP_AID", DBUtilDBType.Varchar, DBUtilDirection.In, 50, Empid.ToString().Trim());
            objDBUtility.AddParameters("@Event", DBUtilDBType.Varchar, DBUtilDirection.In, 50, "GETLTAAMT");

            dsInv = objDBUtility.Execute_StoreProc_DataSet("COMMON_SP_FLEXIPAY");

            objDBUtility.ClearTransactionalParams();
            return dsInv;
        }

        string _EmpLocn;
        string _EmpAddr;
        string _Amount;
        string _Vendor;
        string _InvoiceNo;
        string _InvoiceDate;
        string _Brand;
        string _BrandOther;
        string _IsFinal;
        string _FilingStatus;
        string _Status;
        string _Remarks;

        string _PhoneNumber;
        string _PhoneType;
        string _ServiceProvider;
        string _BillNo;
        string _BillAmt;
        string _BillDate;
        string _FromDate;
        string _ToDate;
        string _ActiveDate;
        string _Active;

        string _TravelMode1;
        string _TravelMode2;
        string _TravelModeOther;
        string _From;
        string _FromText;
        string _To;
        string _ToText;
        string _DepartureDate;
        string _DepartureDateDest;
        string _ReachDate;
        string _ReachDateDest;
        string _DepartureTime;
        string _DepartureTimeDest;
        string _ReachTime;
        string _ReachTimeDest;
        string _StayDays;
        string _VisitReason;
        string _TravelExpense;
        string _HotelExpense;
        string _DailyAllowance;
        string _OtherExpense;

        string _FinYears;
        string _EntryAid;
        string _EntryCode;
        string _TRAVELXML;
        string _DailyAllowanceDays;


        string _DayName;
        string _Date;
        string _Fromhrs;
        string _Tohrs;
        string _Total;
        string _Particulars;
        string _RembDis;
        string _DayCategory;
        string _EmpNAME;
        string _EmpCode;

        public string EmpLocn
        {
            get { return _EmpLocn; }
            set { _EmpLocn = value; }
        }

        public string EmpAddr
        {
            get { return _EmpAddr; }
            set { _EmpAddr = value; }
        }

        public string Amount
        {
            get { return _Amount; }
            set { _Amount = value; }
        }

        public string Vendor
        {
            get { return _Vendor; }
            set { _Vendor = value; }
        }

        public string InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }

        public string InvoiceDate
        {
            get { return _InvoiceDate; }
            set { _InvoiceDate = value; }
        }

        public string Brand
        {
            get { return _Brand; }
            set { _Brand = value; }
        }

        public string BrandOther
        {
            get { return _BrandOther; }
            set { _BrandOther = value; }
        }

        public string IsFinal
        {
            get { return _IsFinal; }
            set { _IsFinal = value; }
        }

        public string FilingStatus
        {
            get { return _FilingStatus; }
            set { _FilingStatus = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public string Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value; }
        }

        public string PhoneType
        {
            get { return _PhoneType; }
            set { _PhoneType = value; }
        }

        public string ServiceProvider
        {
            get { return _ServiceProvider; }
            set { _ServiceProvider = value; }
        }

        public string BillNo
        {
            get { return _BillNo; }
            set { _BillNo = value; }
        }

        public string BillAmt
        {
            get { return _BillAmt; }
            set { _BillAmt = value; }
        }

        public string BillDate
        {
            get { return _BillDate; }
            set { _BillDate = value; }
        }

        public string FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }

        public string ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }

        public string ActiveDate
        {
            get { return _ActiveDate; }
            set { _ActiveDate = value; }
        }

        public string Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        public string EntryAid
        {
            get { return _EntryAid; }
            set { _EntryAid = value; }
        }

        public string EntryCode
        {
            get { return _EntryCode; }
            set { _EntryCode = value; }
        }

        public string TravelMode1
        {
            get { return _TravelMode1; }
            set { _TravelMode1 = value; }
        }

        public string TravelMode2
        {
            get { return _TravelMode2; }
            set { _TravelMode2 = value; }
        }

        public string TravelModeOther
        {
            get { return _TravelModeOther; }
            set { _TravelModeOther = value; }
        }

        public string From
        {
            get { return _From; }
            set { _From = value; }
        }

        public string FromText
        {
            get { return _FromText; }
            set { _FromText = value; }
        }

        public string To
        {
            get { return _To; }
            set { _To = value; }
        }

        public string ToText
        {
            get { return _ToText; }
            set { _ToText = value; }
        }

        public string DepartureDate
        {
            get { return _DepartureDate; }
            set { _DepartureDate = value; }
        }

        public string DepartureTime
        {
            get { return _DepartureTime; }
            set { _DepartureTime = value; }
        }

        public string DepartureDateDest
        {
            get { return _DepartureDateDest; }
            set { _DepartureDateDest = value; }
        }

        public string DepartureTimeDest
        {
            get { return _DepartureTimeDest; }
            set { _DepartureTimeDest = value; }
        }

        public string ReachDate
        {
            get { return _ReachDate; }
            set { _ReachDate = value; }
        }

        public string ReachTime
        {
            get { return _ReachTime; }
            set { _ReachTime = value; }
        }

        public string ReachDateDest
        {
            get { return _ReachDateDest; }
            set { _ReachDateDest = value; }
        }

        public string ReachTimeDest
        {
            get { return _ReachTimeDest; }
            set { _ReachTimeDest = value; }
        }

        public string DailyAllowanceDays
        {
            get { return _DailyAllowanceDays; }
            set { _DailyAllowanceDays = value; }
        }

        public string StayDays
        {
            get { return _StayDays; }
            set { _StayDays = value; }
        }

        public string VisitReason
        {
            get { return _VisitReason; }
            set { _VisitReason = value; }
        }

        public string TravelExpense
        {
            get { return _TravelExpense; }
            set { _TravelExpense = value; }
        }

        public string HotelExpense
        {
            get { return _HotelExpense; }
            set { _HotelExpense = value; }
        }

        public string DailyAllowance
        {
            get { return _DailyAllowance; }
            set { _DailyAllowance = value; }
        }

        public string OtherExpense
        {
            get { return _OtherExpense; }
            set { _OtherExpense = value; }
        }

        public string TRAVELXML
        {
            get { return _TRAVELXML; }
            set { _TRAVELXML = value; }
        }


        public string FinYears
        {
            get { return _FinYears; }
            set { _FinYears = value; }
        }

        public string DayName
        {
            get { return _DayName; }
            set { _DayName = value; }
        }
        public string Date
        {
            get { return _Date; }
            set { _Date = value; }
        }
        public string DayCategory
        {
            get { return _DayCategory; }
            set { _DayCategory = value; }
        }
        public string Fromhrs
        {
            get { return _Fromhrs; }
            set { _Fromhrs = value; }
        }
        public string Tohrs
        {
            get { return _Tohrs; }
            set { _Tohrs = value; }
        }
        public string Total
        {
            get { return _Total; }
            set { _Total = value; }
        }
        public string Particulars
        {
            get { return _Particulars; }
            set { _Particulars = value; }
        }
        public string RembDis
        {
            get { return _RembDis; }
            set { _RembDis = value; }
        }

        public string EmpNAME
        {
            get { return _EmpNAME; }
            set { _EmpNAME = value; }
        }
        public string EmpCode
        {
            get { return _EmpCode; }
            set { _EmpCode = value; }
        }


    }
}