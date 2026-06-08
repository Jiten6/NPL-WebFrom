using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
namespace NewPortal2023
{
    public partial class PreviewPageTax : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lnkBtnOpenFile"].ToString() != "")
            {
                if (Session["openFilePath"].ToString() != "")
                {
                    string openFilePath = Session["openFilePath"].ToString();
                    string lnkBtnOpenFile = Session["lnkBtnOpenFile"].ToString();
                    //string url = string.Format("TempPage.aspx?FN={0}", lnkBtnOpenFile);
                    //string script = "<script type='text/javascript'>window.open('" + url + "', '_blank');</script>";
                    //this.ClientScript.RegisterStartupScript(this.GetType(), "script", script);


                    string savePath = Request.PhysicalApplicationPath;

                    //savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\" + monthYear + "\\PAYSLIP\\" + lnkBtnOpenFile;

                    //savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) +"\\" + monthYear + "\\PAYSLIP\\" + lnkBtnOpenFile;
                    //savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\" + monthYear + "\\PAYSLIP\\" + lnkBtnOpenFile;

                    if ((string)Session["sCompID"] == "CO000114")
                    {
                        savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\" + "\\TAXCOMPUTATION\\" + lnkBtnOpenFile;
                    }
                    else
                    {
                        savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\" + "\\TAXCOMPUTATION\\" + lnkBtnOpenFile;
                    }


                    // string filePath = Server.MapPath("E:\Projects\Portal\Portal\Portal\PDF Reports\CO000114\APR 2019\PAYSLIP\E83243_APR2019.pdf/") + Request.QueryString["FN"];
                    this.Response.ContentType = "application/pdf";
                    this.Response.AppendHeader("Content-Disposition;", "attachment;filename=" + lnkBtnOpenFile);
                    this.Response.WriteFile(savePath);
                    this.Response.End();
                }

            }
        }
    }
}