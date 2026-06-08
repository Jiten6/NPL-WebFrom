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
//using HBS.Encoder;
using Microsoft.Reporting.WebForms;

namespace NewPortal2023.ESS
{
    public partial class Letters : System.Web.UI.Page
    {
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();
        NewPortal2023.ESS.Payslip objInv = new NewPortal2023.ESS.Payslip();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsMon;
        private string SourcePath = string.Empty;
        //StringBuilder sbDetails = new StringBuilder();
        public string filePath;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!Page.IsPostBack)
                {
                    year.Visible = true;
                    FillYear();
                    CreateDocumentsStructure();
                    //DisplayDocuments();
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void FillYear()
        {
            StringBuilder sbDetails = new StringBuilder();
            dsMon = new DataSet();

            if ((string)Session["sCompID"] == "CO000114")
            {
                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Letters");
                System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");

                dsMon.Tables.Add(); 
                dsMon.Tables[0].Columns.Add("YEARCODE");
                dsMon.Tables[0].Columns.Add("YEARNAME");

               // sbDetails.Append("<ROOT>");
                foreach (DirectoryInfo dir in fileNames)
                {
                    DataRow dr = dsMon.Tables[0].NewRow();
                    dr[0] = dir.Name;
                    dr[0] = dir.Name;
                    dsMon.Tables[0].Rows.Add(dr);
                    //sbDetails.Append("<Dir Name='" + dir.Name + "'/>");


                }
                //sbDetails.Append("</ROOT>");

                //dsMon = objInv.FillYear(sbDetails.ToString());

                drpMonth.Items.Clear();
                drpMonth.DataTextField = "YEARCODE";
                drpMonth.DataValueField = "YEARNAME";
                drpMonth.DataSource = dsMon;
                drpMonth.DataBind();
                drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));

                //dsMon.Tables.Add();
                //sbDetails.Append("<ROOT>");
                //foreach (DirectoryInfo dir in fileNames)
                //{
                //    if (dir.Name.ToString().ToUpper() != "LETTERS")
                //    {
                //        sbDetails.Append("<'" + dir.Name + "'>");
                //    }
                //}
                //sbDetails.Append("</ROOT>");

                //dsMon = objInv.FillYear(sbDetails.ToString());

                //drpMonth.Items.Clear();
                //drpMonth.DataTextField = "YEARCODE";
                //drpMonth.DataValueField = "YEARNAME";
                //drpMonth.DataSource = dsMon;
                //drpMonth.DataBind();
                //drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));
            }
            else
            {
                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Letters");
                System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");

                dsMon.Tables.Add();
                dsMon.Tables[0].Columns.Add("YEARCODE");
                dsMon.Tables[0].Columns.Add("YEARNAME");

                //sbDetails.Append("<ROOT>");
                foreach (DirectoryInfo dir in fileNames)
                {
                    DataRow dr = dsMon.Tables[0].NewRow();
                    dr[0] = dir.Name;
                    dr[1] = dir.Name;
                    dsMon.Tables[0].Rows.Add(dr);
                    //sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
                }
                //sbDetails.Append("</ROOT>");

                //dsMon = objInv.FillYear(sbDetails.ToString());

                drpMonth.Items.Clear();
                drpMonth.DataTextField = "YEARCODE";
                drpMonth.DataValueField = "YEARNAME";
                drpMonth.DataSource = dsMon;
                drpMonth.DataBind();
                drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));
            }

        }

        protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayDocuments();
        }

        private void DisplayDocuments()
        {
            try
            {
                CreateDocumentsStructure();

                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + "PDF Reports\\" + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Letters\\" + Convert.ToString(drpMonth.SelectedItem.Text.Trim());

                }
                else
                {
                    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\Documents\\Letters\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim());

                }

                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");
               
                DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                foreach (System.IO.FileInfo fi in fileNames)
                {
                    string[] strEmpcode = fi.Name.Split('_');

                    if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                        
                    }
                }

                this.gvViewDocDetails.DataSource = dtDocInfo;
                this.gvViewDocDetails.DataBind();
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void lnkBtnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFile = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFile.NamingContainer.FindControl("lblTSFileStorageName");

                string openFilePath = lblTSFileStorageName.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
                                                                //openFilePath = openFilePath + ViewState["FILE_PATH"].ToString();

                //string fileName = lblTSFileStorageName.Text;

                System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", "attachment; filename=" + lnkBtnOpenFile.Text);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
            }
            catch
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void CreateDocumentsStructure()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvViewDocDetails.DataSource = dtDocuments;
                gvViewDocDetails.DataBind();
            }
        }
    }
}

