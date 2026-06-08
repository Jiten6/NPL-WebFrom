using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;

namespace NewPortal2023.ESS
{
    public partial class PLP_Letter : System.Web.UI.Page
    {
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();
        NewPortal2023.ESS.Payslip objInv = new NewPortal2023.ESS.Payslip();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsMon;
        private string SourcePath = string.Empty;
        public string filePath = "";
        public string compAId = "";
        public string compPath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    if (Session["sCompID"] != null)
                    {
                        try
                        {
                            FillMonths();
                            CreateDocumentsStructure();
                            //DisplayDocuments();
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Error occurred in application1.";
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        private void FillMonths()
        {
            try
            {
                StringBuilder sbDetails = new StringBuilder();
                dsMon = new DataSet();

                // System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]));

                //System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]));
                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents" + "\\PLPLETTER");

                System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
                sbDetails.Append("<ROOT>");
                foreach (DirectoryInfo dir in fileNames)
                {
                    if (dir.Name.ToString().ToUpper() != "PLPLETTER")
                    {
                        sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
                    }
                }
                sbDetails.Append("</ROOT>");

                dsMon = objInv.FillYear(sbDetails.ToString());

                drpMonth.Items.Clear();
                drpMonth.DataTextField = "YEARCODE";
                drpMonth.DataValueField = "YEARNAME";
                drpMonth.DataSource = dsMon;
                drpMonth.DataBind();
                drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)Session["sCompID"] == "CO000114")
            {
                //DisplayDocumentsAngel();
                DisplayDocuments();
            }
            else
            {
                DisplayDocuments();
            }
            Session["Month_Year"] = drpMonth.SelectedValue;
        }

        //private void FillYear()
        //{
        //    StringBuilder sbDetails = new StringBuilder();
        //    dsMon = new DataSet();

        //    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documnts" + "\\PLPLETTER");
        //    System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
        //    sbDetails.Append("<ROOT>");
        //    foreach (DirectoryInfo dir in fileNames)
        //    {
        //        if (dir.Name.ToString().ToUpper() != "PLPLETTER")
        //        {
        //            sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
        //        }
        //    }
        //    sbDetails.Append("</ROOT>");

        //    dsMon = objInv.FillYear(sbDetails.ToString());

        //    drpMonth.Items.Clear();
        //    drpMonth.DataTextField = "YEARCODE";
        //    drpMonth.DataValueField = "YEARNAME";
        //    drpMonth.DataSource = dsMon;
        //    drpMonth.DataBind();
        //    drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));

        //}

        //protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DisplayDocuments();
        //}

        private void DisplayDocuments()
        {
            try
            {
                CreateDocumentsStructure();

                SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents" + "\\PLPLETTER\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim());

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
                        filePath = "../PDF Reports/" + Convert.ToString(Session["sCompID"]) + "\\Documents" + "\\PLPLETTER\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim())+"\\" +  fi.Name;
                    }
                }

                this.gvViewDocDetails.DataSource = dtDocInfo;
                this.gvViewDocDetails.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkBtnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFile = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFile.NamingContainer.FindControl("lblTSFileStorageName");

                string openFilePath = lblTSFileStorageName.Text;

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