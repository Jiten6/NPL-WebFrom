using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;

namespace NewPortal2023.ESS
{
    public partial class IncrementLetterSingle : System.Web.UI.Page
    {
        NewPortal2023.ESS.Payslip objInv = new NewPortal2023.ESS.Payslip();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsMon;
        private string SourcePath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["sCompID"] != null)
                {
                    try
                    {
                        lblEmployeeCode.Text = (string)Session["EMPCODECUM"];
                        lblEmployeeName.Text = (string)Session["EMPNAMECUM"];
                        Session["EMPCODECUM"] = null;
                        Session["EMPNAMECUM"] = null;
                        FillYear();
                        CreateDocumentsStructure();
                        DisplayDocuments();
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading payslip details.');");
                    }
                }
                else
                {
                    Response.Redirect("Logout.aspx");
                }
            }
        }

        private void FillYear()
        {
            StringBuilder sbDetails = new StringBuilder();
            dsMon = new DataSet();

            System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\INCRLETTERS");
            System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
            sbDetails.Append("<ROOT>");
            foreach (DirectoryInfo dir in fileNames)
            {
                if (dir.Name.ToString().ToUpper() != "INCRLETTERS")
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

        //private void FillYear()
        //{
        //    //StringBuilder sbDetails = new StringBuilder();
        //    //dsMon = new DataSet();

        //    //System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\DOCUMENTS\\LETTERS");
        //    //System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
        //    //sbDetails.Append("<ROOT>");
        //    //foreach (DirectoryInfo dir in fileNames)
        //    //{
        //    //    if (dir.Name.ToString().ToUpper() != "INCRLETTERS")
        //    //    {
        //    //        sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
        //    //    }
        //    //}
        //    //sbDetails.Append("</ROOT>");

        //    //dsMon = objInv.FillYear(sbDetails.ToString());

        //    //drpMonth.Items.Clear();
        //    //drpMonth.DataTextField = "YEARCODE";
        //    //drpMonth.DataValueField = "YEARNAME";
        //    //drpMonth.DataSource = dsMon;
        //    //drpMonth.DataBind();
        //    //drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));

        //    //dsMon = new DataSet();

        //    //System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Letters");
        //    //System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");

        //    StringBuilder sbDetails = new StringBuilder();
        //    dsMon = new DataSet();

        //    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\NPL\\");
        //    System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
        //    sbDetails.Append("<ROOT>");
        //    foreach (DirectoryInfo dir in fileNames)
        //    {
        //        if (dir.Name.ToString().ToUpper() == "INCRLETTERS")
        //        {
        //            sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
        //        }
        //    }
        //    sbDetails.Append("</ROOT>");

        //    dsMon = objInv.FillYear(sbDetails.ToString());

        //    dsMon.Tables.Add();
        //    dsMon.Tables[0].Columns.Add("YEARCODE");
        //    dsMon.Tables[0].Columns.Add("YEARNAME");

        //    foreach (DirectoryInfo dir in fileNames)
        //    {
        //        DataRow dr = dsMon.Tables[0].NewRow();
        //        dr[0] = dir.Name;
        //        dr[1] = dir.Name;
        //        dsMon.Tables[0].Rows.Add(dr);
        //    }

        //    drpMonth.Items.Clear();
        //    drpMonth.DataTextField = "YEARCODE";
        //    drpMonth.DataValueField = "YEARNAME";
        //    drpMonth.DataSource = dsMon;
        //    drpMonth.DataBind();
        //    drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));

        //}

        protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayDocuments();
        }

        //private void DisplayDocuments()
        //{
        //    try
        //    {
        //        CreateDocumentsStructure();

        //        //SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\DOCUMENTS\\LETTERS\\2022-23\\";
        //        SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\Documents\\Letters\\2022-23\\";// + Convert.ToString(drpMonth.SelectedItem.Value.Trim());

        //        System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
        //        System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

        //        DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

        //        foreach (System.IO.FileInfo fi in fileNames)
        //        {
        //            string[] strEmpcode = fi.Name.Split('_');

        //            if (strEmpcode[0].ToString().Trim().ToUpper() == lblEmployeeCode.Text.Trim().ToUpper())
        //            {
        //                DataRow drDocRow = dtDocInfo.NewRow();
        //                drDocRow["FILEPATH"] = fi.FullName;
        //                drDocRow["FILENAME"] = fi.Name;
        //                dtDocInfo.Rows.Add(drDocRow);
        //            }
        //        }

        //        this.gvViewDocDetails.DataSource = dtDocInfo;
        //        this.gvViewDocDetails.DataBind();
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        private void DisplayDocuments()
        {
            try
            {
                CreateDocumentsStructure();

                SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\INCRLETTERS\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim());

                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                foreach (System.IO.FileInfo fi in fileNames)
                {
                    string[] strEmpcode = fi.Name.Split('_');

                    if (strEmpcode[0].ToString().Trim().ToUpper() == lblEmployeeCode.Text.Trim().ToUpper())
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