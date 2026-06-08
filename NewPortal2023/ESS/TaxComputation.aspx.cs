using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace NewPortal2023.ESS
{
    public partial class TaxComputation : System.Web.UI.Page
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
              if (Session["sCompID"]!=null)
            {
            try
            {

                //if (Session["sCompID"].ToString().Trim() == "CO000114")
                //{
                //    Response.Redirect("TempPage.aspx", true);
                //}
                if (!Page.IsPostBack)
                {



                    if (Session["sCompID"] != null)
                    {
                        try
                        {
                            if (Session["sCompID"].ToString() == "CO000057")
                            {
                                Session["sCompAID"] = "ORRA";
                            }
                            //if (Session["CompName"].ToString() == "Alumni")
                            //{
                            //    CreateDocumentsStructure();
                            //    DisplayDocuments();
                            //}
                            //else
                            //{
                            //FillMonths();
                            CreateDocumentsStructure();
                            DisplayDocuments();
                            // }

                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                            //"Error occurred in application."; 
                            //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading tax computation details.');");
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
              else
              {
                  Response.Redirect("Login.aspx");
              }
        }

        //private void FillMonths()
        //{
        //    StringBuilder sbDetails = new StringBuilder();
        //    dsMon = new DataSet();

        //    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]));
        //    System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
        //    sbDetails.Append("<ROOT>");
        //    foreach (DirectoryInfo dir in fileNames) 
        //        {
        //            if (dir.Name.ToString().ToUpper() != "FORM16" && dir.Name.ToString().ToUpper() != "INCRLETTERS" && dir.Name.ToString().ToUpper() != "FORM12BB" && dir.Name.ToString().ToUpper() != "REIMBURSEMENT" && dir.Name.ToString().ToUpper() != "DOCUMENTS" && dir.Name.ToString().ToUpper() != "TAXCOMPUTATION")
        //            {
        //                sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
        //            }
        //        }
        //    sbDetails.Append("</ROOT>");

        //    dsMon = objInv.FillMonths(sbDetails.ToString() );

        //    drpMonth.Items.Clear();
        //    drpMonth.DataTextField = "MONYEARCODE";
        //    drpMonth.DataValueField = "MONYEARNAME";
        //    drpMonth.DataSource = dsMon;
        //    drpMonth.DataBind();
        //    drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));

        //}

        protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayDocuments();
        }

        private void DisplayDocuments()
        {
            try
            {
                CreateDocumentsStructure();

                //if (Session["CompName"].ToString() == "Alumni")
                //{
                //    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGELALUMNI\\TAXCOMPUTATION";

                //    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                //    System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                //    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                //    foreach (System.IO.FileInfo fi in fileNames)
                //    {
                //        string[] strEmpcode = fi.Name.Split('_');

                //        if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                //        {
                //            DataRow drDocRow = dtDocInfo.NewRow();
                //            drDocRow["FILEPATH"] = fi.FullName;
                //            drDocRow["FILENAME"] = fi.Name;
                //            dtDocInfo.Rows.Add(drDocRow);
                //        }
                //    }
                //    this.gvViewDocDetails.DataSource = dtDocInfo;
                //    this.gvViewDocDetails.DataBind();
                //}
                //else
                //{
                //SourcePa
                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path"))  + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\TAXCOMPUTATION";
                }
                else
                {
                    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path"))  + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\TAXCOMPUTATION";
                }

                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                foreach (System.IO.FileInfo fi in fileNames)
                {
                    string[] strEmpcode = fi.Name.Split('_');
                    string id = Convert.ToString(Session["sEmpCode"]).ToUpper();
                    if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                        filePath = "../PDF Reports/" + Convert.ToString(Session["sCompID"]) + "/" + Convert.ToString(Session["sCompAID"]) + "/" +  "/TAXCOMPUTATION/" + fi.Name;
                    }
                }
                this.gvViewDocDetails.DataSource = dtDocInfo;
                this.gvViewDocDetails.DataBind();
                //}
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
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
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
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

        protected void lnkBtnOpenPreviewFile_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                LinkButton lnkpdfpath = (LinkButton)lnkBtnOpenFiles.NamingContainer.FindControl("lnkBtnOpenFile");
                Session["lnkBtnOpenFile"] = lnkpdfpath.Text;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblTSFileStorageName");
                Session["openFilePath"] = lblTSFileStorageName.Text;




                Response.Redirect("../ESS/PreviewPageTax.aspx", true);
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}