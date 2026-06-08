using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class MisellaneousSupporting : System.Web.UI.Page
    {


        DataSet ds = new DataSet();
        NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();

        string entryCode = "";
        string filename = "";
        string savePath = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sCompID"] != null)
            {
                if (!Page.IsPostBack)
                {
                    Getdetails();
                    extradata.Visible = false;
                    btnClose.Visible = false;
                    gvList.Visible = true;
                }  
            }
            else
            {
                Response.Redirect("Login.aspx");
            }


        }

        protected void lnkVoucherno_Click(object sender, EventArgs e)
        {
         
            try
            {
                gvList.Visible = false;
                extradata.Visible = true;
                btnClose.Visible = true;
                LinkButton lnkVoucherNo = (LinkButton)sender;
                string id = ((Label)lnkVoucherNo.NamingContainer.FindControl("lblAppNo")).Text;

                Session["id"] = id;
                ViewState["NEWCLAIM"] = "0";

                ds = objExp.GetPettyReimbById((string)Session["sEmpCode"], (string)Session["id"]);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvallList.DataSource = ds;
                    gvallList.DataBind();
                    if (Session["sEmpCode"].ToString() == "ABC123")
                    {
                        txtApprovedAmt.Text = ds.Tables[1].Rows[0]["AMOUNT"].ToString();
                    }
                    else
                    {
                        txtApprovedAmt.Text = ds.Tables[0].Rows[0]["Approved_Amount"].ToString();
                    }

                    txtTotalAmt.Text = ds.Tables[1].Rows[0]["AMOUNT"].ToString();
                    entryCode = Session["id"].ToString();
                    txtEmpRmk.Text = ds.Tables[0].Rows[0]["EMPRMK"].ToString();
                    DisplayDocumentsEnter(entryCode);

                }
                else
                {
                    gvallList.DataSource = null;
                    gvallList.DataBind();

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }



        }

        protected void gvSummary_RowDataBound(object sender, EventArgs e)
        {


        }

        protected void drpExpensetype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Getdetails()
        {
            try
            {
                ds = objExp.GetSupportingList(Convert.ToString(Session["sCompID"]));
                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvList.DataSource = ds;
                    gvList.DataBind();
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void gvallList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnkShowDoc = (LinkButton)e.Row.FindControl("lnkShowDoc");
                Label lblDocAddr = (Label)e.Row.FindControl("lblDocAddr");
                Label lblId = (Label)e.Row.FindControl("lblEntryAid");


                string SourcePath = "";

                SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Petty\\" + "\\" + lblId.Text + "\\";

                string filesToFind = lblId.Text.Trim() + "_*";
                if (Directory.Exists(SourcePath))
                {
                    string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
                    foreach (string file in fileList)
                    {
                        lnkShowDoc.ToolTip = Path.GetFileName(file);
                        lnkShowDoc.Visible = true;
                        lblDocAddr.Text = file;
                    }
                }

            }
        }

        protected void lnkShowDoc_Click1(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";


                LinkButton lnkShowDoc = (LinkButton)sender;
                Label lblDocAddr = (Label)(lnkShowDoc.NamingContainer.FindControl("lblDocAddr"));

                string SourcePath = lblDocAddr.Text.Trim();

                System.IO.FileInfo fileObj = new System.IO.FileInfo(SourcePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + Path.GetFileName(SourcePath));
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
                else
                {
                    lblMessage.Text = "File not found.";
                    objcommon.Display("Validate", "File not found.');");
                    lblMessage.BackColor = System.Drawing.Color.Tomato;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                lblMessage.BackColor = System.Drawing.Color.Tomato;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void lnkShowDoc_Click(object sender, EventArgs e)
        {
            try
            {
                lblMessage.Text = "";


                //LinkButton lnkShowDoc = (LinkButton)sender;
                //Label lblDocAddr = (Label)(lnkShowDoc.NamingContainer.FindControl("lblDocAddr"));

                string SourcePath = ViewState["PATHDOC"].ToString();

                System.IO.FileInfo fileObj = new System.IO.FileInfo(SourcePath);
                if (fileObj.Exists)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + Path.GetFileName(SourcePath));
                    Response.ContentType = "text/csv";
                    Response.WriteFile(fileObj.FullName);
                    Response.End();
                }
                else
                {
                    lblMessage.Text = "File not found.";
                    objcommon.Display("Validate", "File not found.');");
                    lblMessage.BackColor = System.Drawing.Color.Tomato;
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                lblMessage.BackColor = System.Drawing.Color.Tomato;
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void CreateDocumentsStructureEnter()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvEntertainment.DataSource = dtDocuments;
                gvEntertainment.DataBind();
            }
        }

        protected void lnkBtnOpenFileEnter_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblFileStorageEnter = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorageEnter");

                string openFilePath = lblFileStorageEnter.Text;// Request.PhysicalApplicationPath.Substring(0, Request.PhysicalApplicationPath.IndexOf("IN4REASPX"));
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
            catch
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void DisplayDocumentsEnter(string entryCode)
        {
            try
            {
                CreateDocumentsStructureEnter();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Petty\\" + "\\" + entryCode + "\\";

                if (System.IO.Directory.Exists(savePath) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles(prefix + "*");

                    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }

                    this.gvEntertainment.DataSource = dtDocInfo;
                    this.gvEntertainment.DataBind();

                    //if (dtDocInfo.Rows.Count > 0)
                    //{
                    //    divfileUploadEnter.Visible = false;
                    //    divfileDisplayEnter.Visible = true;


                    //}
                    //else
                    //{
                    //    //trUpload.Visible = true;
                    //    divfileDisplayEnter.Visible = false;
                    //}
                }
                else
                {

                    //divfileDisplayEnter.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            extradata.Visible = false;
            btnClose.Visible = false;
            gvList.Visible = true;
        }























    }
}