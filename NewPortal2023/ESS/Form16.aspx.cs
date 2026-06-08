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
//using PdfSharp.Pdf;
//using PdfSharp.Pdf.IO;
//using PdfSharp.Drawing;
//using System.IO;


namespace NewPortal2023.ESS
{
    public partial class Form16 : System.Web.UI.Page
    {
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();
       // NewPortal2023.ESS.NewPortalPayslip objInv = new NewPortal2023.ESS.NewPortalPayslip();
        NewPortal2023.ESS.Payslip objInv = new NewPortal2023.ESS.Payslip();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsMon;
        DataSet dsEmp;
        private string SourcePath = string.Empty;
        public string filePath = "";
        public string compAId = "";
        public string compPath = "";
        public string PdfFilePath;       
        string EMP_AID;
        string TYPE;
        string EMP_MID;
        string EmpCode;


        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["sCompID"].ToString() == "CO000114")
                {
                    FillYear();
                    CreateDocumentsStructure();
                }
                if (!Page.IsPostBack)
                {


                    if (Session["EMP_AID"] != null)
                    {
                        EMP_AID = Session["EMP_AID"].ToString();
                        dsEmp = objInv.GetEmpMid((string)Session["sCompID"], EMP_AID);
                        Session["EMP_MID"] = dsEmp.Tables[0].Rows[0]["EMP_MID"].ToString();
                        EMP_MID = Session["EMP_MID"].ToString();
                        EmpCode = Session["sEmpCode"].ToString();
                    }
                    else
                    {
                        EMP_AID = "";
                    }

                    if (Convert.ToString(Session["sCompID"]) == "CO000057")
                    {
                        trPdf.Visible = true;
                    }
                    else
                    {
                        trPdf.Visible = false;
                    }


                    if (Session["sCompID"] != null)
                    {
                        try
                        {
                            if (Convert.ToString(Session["sCompID"]) != "CO000105")
                            {
                                year.Visible = true;
                                FillYear();
                                CreateDocumentsStructure();
                            }
                            else
                            {
                                year.Visible = false;
                                DisplayDocuments();
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMessage.Text = ex.Message;
                            //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading payslip details.');");
                        }
                    }
                    else
                    {
                        Response.Redirect("Logout.aspx");
                    }
                }
            }
            catch (System.Threading.ThreadAbortException)
            {

            }
            catch (Exception ex)
            {
                //Session["Error"] = ex.Message + "<br>Payslip.aspx";
                Session["Error"] = ex.Message + "<br>Form16.aspx<br>" + Request.QueryString["key"].Replace(" ", "+");
                Response.Redirect("../ErrorPage.aspx", true);
            }
        }

        private void FillYear()
        {
            try
            {
                StringBuilder sbDetails = new StringBuilder();
                dsMon = new DataSet();

                if ((string)Session["sCompID"] == "CO000114")
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\FORM16");
                    System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");

                    sbDetails.Append("<ROOT>");
                    foreach (DirectoryInfo dir in fileNames)
                    {
                        if (dir.Name.ToString().ToUpper() != "FORM16")
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
                else
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\FORM16");
                    System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
                    sbDetails.Append("<ROOT>");
                    foreach (DirectoryInfo dir in fileNames)
                    {
                        if (dir.Name.ToString().ToUpper() != "FORM16")
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

                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]));

                System.IO.DirectoryInfo[] fileNames = dirInfo.GetDirectories("*.*");
                sbDetails.Append("<ROOT>");
                foreach (DirectoryInfo dir in fileNames)
                {
                    if (dir.Name.ToString().ToUpper() != "FORM16" && dir.Name.ToString().ToUpper() != "INCRLETTERS" && dir.Name.ToString().ToUpper() != "FORM12BB" && dir.Name.ToString().ToUpper() != "REIMBURSEMENT" && dir.Name.ToString().ToUpper() != "DOCUMENTS" && dir.Name.ToString().ToUpper() != "TAXCOMPUTATION" && dir.Name.ToString().ToUpper() != "CUMULATIVE" && dir.Name.ToString().ToUpper() != "MISREPORTS" && dir.Name.ToString().ToUpper() != "MONTHLYREPORTS" && dir.Name.ToString().ToUpper() != "SALARY ARREARS AND PLP 2022_23")
                    {
                        sbDetails.Append("<Dir Name='" + dir.Name + "'/>");
                    }
                }
                sbDetails.Append("</ROOT>");

                dsMon = objInv.FillMonths(sbDetails.ToString());

                drpMonth.Items.Clear();
                drpMonth.DataTextField = "MONYEARCODE";
                drpMonth.DataValueField = "MONYEARNAME";
                drpMonth.DataSource = dsMon;
                drpMonth.DataBind();
                drpMonth.Items.Insert(0, new ListItem("[Select One]", "0"));
                drpMonth.Items.Insert(1, new ListItem("SALARY ARREARS AND PLP 2022_23", "SALARY ARREARS AND PLP 2022_23"));
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
                DisplayDocumentsAngel();
                Session["Month_Year"] = drpMonth.SelectedValue;
            }
            else
            {
                DisplayDocuments();
                Session["Month_Year"] = drpMonth.SelectedValue;
            }
        }

        private void DisplayDocumentsAngel()
        {

            try
            {
                CreateDocumentsStructure();

                //SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "\\PAYSLIP";
                //SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "\\PAYSLIP";
                //System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + "ANGEL" + "\\" + "\\" + "FORM16" + "\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim())); //+ Convert.ToString(Session["sCompAID"]));

                //string PdfFilePath = "file:///" + localFilePath.Replace("\\", "/") + ".pdf";
                string pdfFilePath = dirInfo.FullName;
                string PdfFilePath = pdfFilePath.Replace("\\\\", "\\");

                FileInfo[] files = dirInfo.GetFiles();

                System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                foreach (System.IO.FileInfo fi in fileNames)
                {
                    string[] strEmpcode = fi.Name.Split('_');

                    string Emp_ID = Session["sEmpCode"].ToString();
                    string EmpCode = strEmpcode[0].ToString().Trim().ToUpper();
                    if (EmpCode == Emp_ID)
                    //if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                        filePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "/" + Convert.ToString(Session["sCompAID"]) + "/" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "/PAYSLIP/" + fi.Name;
                        //string filePath = string.Format("Payslip.aspx?FN={0}",  fi.Name);
                        PdfFilePath = filePath;
                    }
                }
             
                this.gvViewDocDetails.DataSource = dtDocInfo;
                this.gvViewDocDetails.DataBind();

                //// Create a PdfViewer control
                //PdfViewer pdfViewer = new PdfViewer();

                //// Add the PdfViewer to your form
                //this.Controls.Add(pdfViewer);

                //// Load and display a PDF file
                //pdfViewer.Load("path_to_your_pdf.pdf");

                ///////////////////////////////////////////////////////////////////////////////////////////////

            }
            catch (Exception ex)
            {

            }

        }

        private void CreateDocumentsStructureAngel()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("PREVIEW", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvViewDocDetails.DataSource = dtDocuments;
                gvViewDocDetails.DataBind();
            }
        }

        private void DisplayDocuments()
        {
            try
            {
                CreateDocumentsStructure();

                //SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "\\PAYSLIP";
                //SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "\\PAYSLIP";
                //System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\FORM16\\" + Convert.ToString(drpMonth.SelectedItem.Value.Trim().ToLower()) ); //+ Convert.ToString(Session["sCompAID"]));

                //string PdfFilePath = "file:///" + localFilePath.Replace("\\", "/") + ".pdf";
                string pdfFilePath = dirInfo.FullName;
                string PdfFilePath = pdfFilePath.Replace("\\\\", "\\");

                FileInfo[] files = dirInfo.GetFiles();

                System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                foreach (System.IO.FileInfo fi in fileNames)
                {
                    string[] strEmpcode = fi.Name.Split('_');

                    string Emp_ID = Session["sEmpCode"].ToString();
                    string EmpCode = strEmpcode[0].ToString().Trim().ToUpper();
                    if (EmpCode == Emp_ID)
                    //if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                        filePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "/" + Convert.ToString(Session["sCompAID"]) + "/FORM16/" + Convert.ToString(drpMonth.SelectedItem.Value.Trim()) + "/" + fi.Name;
                        //string filePath = string.Format("Payslip.aspx?FN={0}",  fi.Name);
                        PdfFilePath = filePath;
                    }
                }

                if ((string)Session["sCompID"] == "CO000114")
                {
                    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\CUMULATIVE";

                    dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                    fileNames = dirInfo.GetFiles("*.*");

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        string[] strEmpcode = fi.Name.Split('_');

                        if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                        {
                            DataRow drDocRow = dtDocInfo.NewRow();
                            drDocRow["FILEPATH"] = fi.FullName;
                            drDocRow["FILENAME"] = fi.Name;
                            dtDocInfo.Rows.Add(drDocRow);
                            //break;
                        }
                    }

                    SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\INCRLETTERS";

                    dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                    fileNames = dirInfo.GetFiles("*.*");

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
                }

                else                   
                {
                    //SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\CUMULATIVE";

                    //dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                    //fileNames = dirInfo.GetFiles("*.*");

                    //foreach (System.IO.FileInfo fi in fileNames)
                    //{
                    //    string[] strEmpcode = fi.Name.Split('_');

                    //    if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                    //    {
                    //        DataRow drDocRow = dtDocInfo.NewRow();
                    //        drDocRow["FILEPATH"] = fi.FullName;
                    //        drDocRow["FILENAME"] = fi.Name;
                    //        dtDocInfo.Rows.Add(drDocRow);
                    //        //break;
                    //    }
                    //}

                    //SourcePath = Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\INCRLETTERS";

                    //dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + SourcePath);
                    //fileNames = dirInfo.GetFiles("*.*");

                    //foreach (System.IO.FileInfo fi in fileNames)
                    //{
                    //    string[] strEmpcode = fi.Name.Split('_');

                    //    if (strEmpcode[0].ToString().Trim().ToUpper() == Convert.ToString(Session["sEmpCode"]).ToUpper())
                    //    {
                    //        DataRow drDocRow = dtDocInfo.NewRow();
                    //        drDocRow["FILEPATH"] = fi.FullName;
                    //        drDocRow["FILENAME"] = fi.Name;
                    //        dtDocInfo.Rows.Add(drDocRow);
                    //    }
                    //}
                }

                this.gvViewDocDetails.DataSource = dtDocInfo;
                this.gvViewDocDetails.DataBind();

                //// Create a PdfViewer control
                //PdfViewer pdfViewer = new PdfViewer();

                //// Add the PdfViewer to your form
                //this.Controls.Add(pdfViewer);

                //// Load and display a PDF file
                //pdfViewer.Load("path_to_your_pdf.pdf");


                ///////////////////////////////////////////////////////////////////////////////////////////////



            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkBtnOpenFile_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblTSFileStorageName");

                string openFilePath = lblTSFileStorageName.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
                                                                //openFilePath = openFilePath + ViewState["FILE_PATH"].ToString();

                //string fileName = lblTSFileStorageName.Text;
                System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + lnkBtnOpenFiles.Text);
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
            }
            catch (Exception ex)
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
                //dtDocuments.Columns.Add(new DataColumn("PREVIEW", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvViewDocDetails.DataSource = dtDocuments;
                gvViewDocDetails.DataBind();
            }
        }


        protected void lnkbtnPdfDownloadClick(object sender, EventArgs e)
        {
            try
            {

                if (Convert.ToString(Session["sCompID"]) == "CO000114")
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\ANGEL\\FORM16\\Digital_Signature_Validation_Process.pdf");
                    string openFilePath = dirInfo.ToString();// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
                                                             //openFilePath = openFilePath + ViewState["FILE_PATH"].ToString();

                    string fileName = "FORM 16 VALIDATION FORM";

                    System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", "attachment; filename=" + fileName + ".pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }
                else
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(Request.PhysicalApplicationPath.ToString() + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\" + Convert.ToString(Session["sCompAID"]) + "\\FORM16\\Digital_Signature_Validation_Process.pdf");
                    string openFilePath = dirInfo.ToString();// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
                                                             //openFilePath = openFilePath + ViewState["FILE_PATH"].ToString();

                    string fileName = "FORM 16 VALIDATION FORM";

                    System.IO.FileInfo fileObj = new System.IO.FileInfo(@openFilePath);
                    if (fileObj.Exists)
                    {
                        Response.Clear();
                        Response.Buffer = true;
                        Response.AppendHeader("content-disposition", "attachment; filename=" + fileName + ".pdf");
                        Response.ContentType = "text/csv";
                        Response.WriteFile(fileObj.FullName);
                        Response.End();
                    }
                }





            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;

            }
        }
    }
}