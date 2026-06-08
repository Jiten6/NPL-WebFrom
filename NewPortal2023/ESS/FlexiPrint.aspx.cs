using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Text;

namespace NewPortal2023.ESS
{
    public partial class FlexiPrint : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objcommon;
        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";
                if (!IsPostBack)
                {
                    DataTable dtSAL = new DataTable();
                    DataTable dtFLX = new DataTable();
                    DataTable dtOTH = new DataTable();

                    NewPortal2023.ESS.FlexiPay objBl = new NewPortal2023.ESS.FlexiPay();
                    ds = objBl.GetCTCDetailsPrint((string)Session["sCompID"], (string)Session["sEmpID"], (string)Session["sEmpCode"]);

                    dtSAL = ds.Tables[1].Clone();
                    dtFLX = ds.Tables[1].Clone();
                    dtOTH = ds.Tables[1].Clone();

                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        if (dr["REPORT_FLAG"].ToString() == "SAL")
                        {
                            dtSAL.Rows.Add(dr.ItemArray);
                        }

                        if (dr["REPORT_FLAG"].ToString() == "FLX")
                        {
                            dtFLX.Rows.Add(dr.ItemArray);
                        }

                        if (dr["REPORT_FLAG"].ToString() == "OTH")
                        {
                            dtOTH.Rows.Add(dr.ItemArray);
                        }
                    }

                    DataView dv = dtSAL.DefaultView;
                    dv.Sort = "REPORT_ORDER ASC";
                    dtSAL = dv.ToTable();

                    rptPrint.Visible = true;
                    ReportDataSource datasourceEmp = new ReportDataSource("dsflexi_dtEmpDetails", ds.Tables[6]);
                    ReportDataSource datasourceSal = new ReportDataSource("dsflexi_dtFlexiDetailsSAL", dtSAL);
                    ReportDataSource datasourceFlx = new ReportDataSource("dsflexi_dtFlexiDetailsFLX", dtFLX);
                    ReportDataSource datasourceOth = new ReportDataSource("dsflexi_dtFlexiDetailsOTH", dtOTH);
                    ReportParameter[] param = new ReportParameter[3];
                    param[0] = new ReportParameter("AnnCTC", Convert.ToDecimal((string)Session["AnnCTC"]).ToString("0.00"), false);
                    param[1] = new ReportParameter("MonCTC", Convert.ToDecimal((string)Session["MonCTC"]).ToString("0.00"), false);
                    param[2] = new ReportParameter("Gross", Convert.ToDecimal((string)Session["Gross"]).ToString("0.00"), false);

                    Session["AnnCTC"] = "0";
                    Session["MonCTC"] = "0";
                    Session["Gross"] = "0";

                    rptPrint.LocalReport.DataSources.Clear();
                    rptPrint.LocalReport.ReportPath = @"ESS\Flexi.rdlc";
                    rptPrint.LocalReport.DisplayName = ds.Tables[6].Rows[0][0] + " " + ds.Tables[6].Rows[0][1];

                    rptPrint.LocalReport.SetParameters(param);
                    rptPrint.LocalReport.DataSources.Add(datasourceEmp);
                    rptPrint.LocalReport.DataSources.Add(datasourceSal);
                    rptPrint.LocalReport.DataSources.Add(datasourceFlx);
                    rptPrint.LocalReport.DataSources.Add(datasourceOth);
                    rptPrint.LocalReport.Refresh();
                }
            }
            catch (Exception ex)
            {
                objcommon = new NewPortal2023.ESS.Common();
                lblMessage.Text = ex.Message;
                objcommon.Display("Validate", "DisplayErrorMessage('" + objcommon.EncodeJsString("Error occurred in application.") + "');");
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Flexipay.aspx");
        }
    }
}