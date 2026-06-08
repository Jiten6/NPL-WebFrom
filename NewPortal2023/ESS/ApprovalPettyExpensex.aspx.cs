using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
namespace NewPortal2023.ESS
{
    public partial class ApprovalPettyExpensex : System.Web.UI.Page
    {
        
        DataSet ds = new DataSet();
        NewPortal2023.Helper.ExpenseEmail obj = new NewPortal2023.Helper.ExpenseEmail();
        NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        string entryCode = "";
        string filename = "";
        string savePath = "";
        string Approver = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sCompID"] != null)
            {
                if (!Page.IsPostBack)
                {
                    btnAddNew.Visible = false;
                    Getdetails();
                    tblNewDataEntry.Visible = false;
                    divList.Visible = true;
                    divallList.Visible = false;
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }


        }


        protected void lnkVoucherno_Click(object sender, EventArgs e)
        {
            if (true)
            {

            }
            btnAddNew.Visible = false;
            btnAddNew.Text = "Add more expense";
            tblNewDataEntry.Visible = false;
            divallList.Visible = true;
            divList.Visible = false;
            ClearAll();
            SubmitBtn.Visible = true;


            try
            {
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
                    btnAddNew.Visible = false;


                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }



        }

        protected void btnAddNewVoucher_Click(object sender, EventArgs e)
        {
            if (btnAddNew.Text == "Add more expense")
            {
                ClearAll();
                tblNewDataEntry.Visible = false;
                divallList.Visible = true;
                btnAddNew.Visible = false;
            }
            else
            {
                divList.Visible = false;
                divallList.Visible = false;
                btnAddNew.Visible = false;
                tblNewDataEntry.Visible = false;
            }


            ViewState["NEWCLAIM"] = "1";

        }

        protected void gvSummary_RowDataBound(object sender, EventArgs e)
        {


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            btnAddNew.Text = "Add new voucher";
            if (btnPcktReimb.Text == "Update")
            {
                btnAddNew.Visible = false;
            }
            else
            {
                btnAddNew.Visible = false;

            }
            tblNewDataEntry.Visible = false;

            //gvList.Visible = true;
            divList.Visible = true;
            divallList.Visible = false;

            SaveDetails();
            if (btnPcktReimb.Text == "Update")
            {

            }
            else
            {
                Getdetails();

            }


            ClearAll();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divallList.Visible = false;

            btnAddNew.Visible = false;

            tblNewDataEntry.Visible = false;
            ViewState["NEWCLAIM"] = null;
            Session["id"] = null;
            ViewState["PATHDOC"] = null;
            btnAddNew.Text = "Add New Voucher";
        }

        private void SaveDetails()
        {




            objExp.ExpenseType = drpExpensetype.SelectedItem.Text;
            objExp.Date = txtDate.Text.Trim();
            //objExp.Onbehalfof = txtOnbehalfof.Text;
            objExp.Natureofexpense = txtNatureofexpense.Text;
            objExp.Billno = txtBillno.Text;
            objExp.Amount = txtAmount.Text;
            objExp.Status = "9";
            if (ViewState["NEWCLAIM"].ToString() == "1")
            {
                objExp.TravelType = "1";
                ds = objExp.InsertPettyReimb((string)Session["sEmpCode"], (string)Session["sEmpID"]);
                if (ds.Tables[1].Rows[0]["result"].ToString() == "")
                {
                    entryCode = ds.Tables[0].Rows[0]["Entry_Aid"].ToString();
                    if (fupUpload.PostedFile != null)
                    {

                        UploadFinFile(entryCode);

                    }
                    lblMessage.Text = "Expenses Save and Generated Voucher No Successfully";

                }
            }
            else if (ViewState["NEWCLAIM"].ToString() == "0")
            {
                objExp.TravelType = "0";
                if (btnPcktReimb.Text == "Update")
                {
                    objExp.AppNo = ViewState["Entry_Aid"].ToString();
                    ds = objExp.UpdatePettyReimb();
                    if (ds.Tables[2].Rows[0]["result"].ToString() == "")
                    {
                        //divallList.Visible = true;
                        //btnAddNew.Visible = false;
                        //tblNewDataEntry.Visible = true;
                        //SubmitBtn.Visible = true;

                        entryCode = ViewState["Entry_Aid"].ToString();
                        if (fupUpload.PostedFile != null)
                        {


                            UploadFinFile(entryCode);

                        }


                        tblNewDataEntry.Visible = false;
                        divallList.Visible = true;
                        divList.Visible = false;
                        ClearAll();
                        SubmitBtn.Visible = true;
                        btnPcktReimb.Text = "Add";
                        gvallList.DataSource = ds.Tables[0];
                        gvallList.DataBind();
                        txtTotalAmt.Text = ds.Tables[1].Rows[0]["AMOUNT"].ToString();
                        btnAddNew.Visible = false;
                        lblDocAddr.Visible = false;
                        lnkShowDoc.Visible = false;
                    }

                }
                else
                {
                    ds = objExp.InsertPettyReimb((string)Session["sEmpCode"], (string)Session["sEmpID"]);
                    if (ds.Tables[3].Rows[0]["result"].ToString() == "")
                    {

                        entryCode = ds.Tables[2].Rows[0]["ENTRY_AID"].ToString();
                        if (fupUpload.PostedFile != null)
                        {
                            //if (Convert.ToString(fupUpload.PostedFile.FileName) == "")
                            //{

                            UploadFinFile(entryCode);
                            //}
                        }

                        lblMessage.Text = "Expenses Save Successfully";
                        tblNewDataEntry.Visible = false;
                        divallList.Visible = true;
                        divList.Visible = false;
                        ClearAll();
                        SubmitBtn.Visible = true;
                        btnPcktReimb.Text = "Add";
                        gvallList.DataSource = ds.Tables[0];
                        gvallList.DataBind();
                        txtTotalAmt.Text = ds.Tables[1].Rows[0]["AMOUNT"].ToString();
                        btnAddNew.Visible = false;

                    }

                }
            }


        }

        private void UploadFinFile(string entryCode)
        {
            string message;


            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Petty\\" + "\\" + entryCode + "\\";

            System.IO.Stream fileInputStream = fupUpload.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(fupUpload.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupUpload.FileName.Trim());

            string filesToDelete = entryCode + "_*";
            string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
            System.IO.FileStream fStream = new System.IO.FileStream(@savePath + entryCode + "_" + fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write(fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();
        }

        protected void drpExpensetype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Getdetails()
        {
            try
            {

                DataSet dsStatus = new DataSet();
                dsStatus = objExp.GetStatus((string)Session["sCompID"], (string)Session["sEmpID"]);
                if (Session["sEmpCode"].ToString() == "ABC123")
                {
                    objExp.Type = "3";
                    txtApprovedAmt.ReadOnly = false;
                    ds = objExp.GetApproverList((string)Session["sEmpID"]);
                }
                //else if (Session["sEmpCode"].ToString() == "NP230")
                else if (Session["sEmpCode"].ToString() == "CONSP29")
                {
                    txtApprovedAmt.ReadOnly = true;
                    objExp.Type = "5";
                    ds = objExp.GetApproverList((string)Session["sEmpID"]);
                }

                else if (Session["sEmpCode"].ToString() == "NP3435")
                {
                    txtApprovedAmt.ReadOnly = true;
                    objExp.Type = "5";
                    ds = objExp.GetApproverList((string)Session["sEmpID"]);
                }

                else if (Session["sEmpCode"].ToString() == "NP3403")
                {
                    txtApprovedAmt.ReadOnly = true;
                    objExp.Type = "6";
                    ds = objExp.GetApproverList((string)Session["sEmpID"]);
                }
                else
                {
                    if (dsStatus.Tables[0].Rows[0]["COUNTS"].ToString() != "0")
                    {
                        txtApprovedAmt.ReadOnly = true;
                        objExp.Type = "4";
                        ds = objExp.GetApproverList((string)Session["sEmpID"]);
                    }
                }





                if (ds.Tables[0].Rows.Count > 0)
                {
                    gvList.DataSource = ds;
                    gvList.DataBind();
                    //if (Session["sEmpCode"].ToString() == "ABC123" || Session["sEmpCode"].ToString() == "NP230")
                    if (Session["sEmpCode"].ToString() == "ABC123" || Session["sEmpCode"].ToString() == "CONSP29" || Session["sEmpCode"].ToString() == "NP3435")
                    {
                        gvList.Columns[9].Visible = true;  // HOD Rmk column index 
                        gvList.Columns[10].Visible = true; // HR Rmk column index 
                    }
                    else if (Session["sEmpCode"].ToString() == "NP3403")
                    {
                        gvList.Columns[9].Visible = false;  // HOD Rmk column index 
                        gvList.Columns[10].Visible = true; // HR Rmk column index 
                    }
                    else
                    {
                        gvList.Columns[9].Visible = true;  // HOD Rmk column index 
                        gvList.Columns[10].Visible = false; // HR Rmk column index 
                    }
                    btnAddNew.Visible = false;


                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();
                    btnAddNew.Visible = false;

                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        //protected void gvList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblAppNo = (Label)e.Row.FindControl("lblAppNo");
        //        objExp.AppNo = lblAppNo.Text;
        //        DataSet dsEmpTypes = new DataSet();
        //        dsEmpTypes = objExp.GetEmpTypeCode();

        //        if (dsEmpTypes.Tables[0].Rows[0]["EmpType"].ToString() == "E0000023")
        //        {
        //            gvList.Columns[8].Visible = true;  // HOD Rmk column index 
        //            gvList.Columns[9].Visible = false; // HR Rmk column index 
        //        }
        //    }
        //}

        private void ClearAll()
        {
            drpExpensetype.SelectedItem.Text = "Select one";
            txtDate.Text = "";
            //txtOnbehalfof.Text = "";
            txtNatureofexpense.Text = "";
            txtBillno.Text = "";
            txtAmount.Text = "";
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";

            divallList.Visible = true;

            btnAddNew.Visible = false;
            tblNewDataEntry.Visible = false;
            LinkButton lnkRequestNo = (LinkButton)sender;
            string id = ((Label)lnkRequestNo.NamingContainer.FindControl("lblEntryAid")).Text;
            ViewState["Entry_Aid"] = id;
            DataSet ds = objExp.UpdateById((string)Session["sEmpID"], id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                drpExpensetype.SelectedItem.Text = ds.Tables[0].Rows[0]["EXPENSE_TYPE"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["DATE"].ToString();
                //txtOnbehalfof.Text = ds.Tables[0].Rows[0]["ON_BEHALF_OF"].ToString();
                txtNatureofexpense.Text = ds.Tables[0].Rows[0]["NATURE_OF_EXPENSE"].ToString();
                txtBillno.Text = ds.Tables[0].Rows[0]["BILL_NUMBER"].ToString();
                txtAmount.Text = ds.Tables[0].Rows[0]["AMOUNT"].ToString();
                btnPcktReimb.Text = "Update";
                SubmitBtn.Visible = false;
                string SourcePath = "";

                SourcePath = Request.PhysicalApplicationPath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Petty\\" + "\\" + id + "\\";

                string filesToFind = id + "_*";
                if (Directory.Exists(SourcePath))
                {
                    string[] fileList = System.IO.Directory.GetFiles(SourcePath, filesToFind);
                    foreach (string file in fileList)
                    {
                        lnkShowDoc.ToolTip = Path.GetFileName(file);
                        lnkShowDoc.Visible = true;
                        lblDocAddr.Text = file;
                        ViewState["PATHDOC"] = file;
                    }
                }
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton lnkRequestNo = (LinkButton)sender;
            string id = ((Label)lnkRequestNo.NamingContainer.FindControl("lblEntryAid")).Text;
            ViewState["Entry_Aid"] = id;
            DataSet ds = objExp.DeleteByEntryId((string)Session["sEmpID"], id);
            if (ds.Tables[2].Rows[0]["result"].ToString() == "")
            {
                lblMessage.Text = "Delete Voucher Successfully";
                gvallList.DataSource = ds.Tables[0];
                gvallList.DataBind();
                txtTotalAmt.Text = ds.Tables[1].Rows[0]["AMOUNT"].ToString();
                btnAddNew.Visible = false;
            }
            else
            {
                lblMessage.Text = "Error.";
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



        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            
            objExp.AppNo = Session["id"].ToString();

            objExp.ReportingRemarks = txtRmk.Text;
            objExp.Action = "APPROVE";

            if (txtApprovedAmt.Text != "")
            {

                DataSet dsStatus = new DataSet();
                dsStatus = objExp.GetStatus((string)Session["sCompID"], (string)Session["sEmpID"]);
                if (Session["sEmpCode"].ToString() == "ABC123")
                {
                    DataSet dsEmpType = new DataSet();
                    dsEmpType = objExp.GetEmpTypeCode();
                    if (dsEmpType.Tables[0].Rows[0]["EmpType"].ToString() == "E0000023" || dsEmpType.Tables[0].Rows[0]["EmpType"].ToString() == "E0000024")
                    {
                        objExp.TotalAmmount = txtApprovedAmt.Text;

                        if (dsEmpType.Tables[0].Rows[0]["EMP_MID"].ToString() == "CONSP20" || dsEmpType.Tables[0].Rows[0]["EMP_MID"].ToString() == "CONSP24")
                        {
                            objExp.Status = "6";
                        }
                        else
                        {
                            objExp.Status = "3";
                        }
                       //objExp.Status = "3";
                        objExp.Type = "3";
                        Approver = "SEQUEL";

                    }
                    else
                    {
                        objExp.TotalAmmount = txtApprovedAmt.Text;
                        objExp.Status = "6";
                        objExp.Type = "3";

                        Approver = "SEQUEL";
                    }

                }
                //else if (Session["sEmpCode"].ToString() == "NP230")
                else if (Session["sEmpCode"].ToString() == "CONSP29")
                {
                    objExp.Status = "1";
                    objExp.Type = "5";

                    Approver = "FINANCE";

                }

                else if (Session["sEmpCode"].ToString() == "NP3435")
                {
                    objExp.Status = "1";
                    objExp.Type = "5";

                    Approver = "FINANCE";

                }
                else if (Session["sEmpCode"].ToString() == "NP3403")
                {
                    objExp.Status = "6";
                    objExp.Type = "6";
                    Approver = "HR";
                }
                else
                {
                    if (dsStatus.Tables[0].Rows[0]["COUNTS"].ToString() != "0")
                    {
                        objExp.Status = "4";
                        objExp.Type = "4";
                        Approver = "HOD";
                    }
                }

                DataSet ds = objExp.ApproverAction((string)Session["sEmpCode"]);
                if (ds.Tables[0].Rows[0]["result"].ToString() == "")
                {

                    Getdetails();
                    tblNewDataEntry.Visible = false;
                    divList.Visible = true;
                    divallList.Visible = false;
                    lblMessage.Text = "Action Submitted Successfully";
                    btnAddNew.Text = "Add New Voucher";
                    obj.SendMiscEmail(objExp.AppNo, Approver, objExp.Action, "", Session["sCompID"].ToString(), Session["sEmpID"].ToString());
                    //MakeChkForEmailXml(gvallList);
                    ViewState["NEWCLAIM"] = null;
                    Session["id"] = null;
                    ViewState["PATHDOC"] = null;
                }
                else
                {
                    lblMessage.Text = "Error.";
                }
            }
            else
            {
                lblMessage.Text = "Please Enter Approved Amount.";
            }


        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            objExp.AppNo = Session["id"].ToString();
            objExp.Status = "0";
            objExp.ReportingRemarks = txtRmk.Text;
            objExp.Action = "REJECT";


            DataSet dsStatus = new DataSet();
            dsStatus = objExp.GetStatus((string)Session["sCompID"], (string)Session["sEmpID"]);
            if (Session["sEmpCode"].ToString() == "ABC123")
            {
                objExp.TotalAmmount = "";
                objExp.Type = "3";
                Approver = "SEQUEL";

            }
            //else if (Session["sEmpCode"].ToString() == "NP230")
            else if (Session["sEmpCode"].ToString() == "CONSP29")
            {
                objExp.Type = "5";
                Approver = "FINANCE";
            }

            else if (Session["sEmpCode"].ToString() == "NP3435")
            {
                objExp.Type = "5";
                Approver = "FINANCE";
            }
            else if (Session["sEmpCode"].ToString() == "NP3403")
            {

                objExp.Type = "6";
                Approver = "HR";

            }
            else
            {
                if (dsStatus.Tables[0].Rows[0]["COUNTS"].ToString() != "0")
                {
                    objExp.Type = "4";
                    Approver = "HOD";
                }
            }


            DataSet ds = objExp.ApproverAction((string)Session["sEmpCode"]);
            if (ds.Tables[0].Rows[0]["result"].ToString() == "")
            {

                Getdetails();
                tblNewDataEntry.Visible = false;
                divList.Visible = true;
                divallList.Visible = false;
                lblMessage.Text = "Voucher is Rejected";
                btnAddNew.Text = "Add New Voucher";
                obj.SendMiscEmail(objExp.AppNo, Approver, objExp.Action, "", Session["sCompID"].ToString(), Session["sEmpID"].ToString());
                ViewState["NEWCLAIM"] = null;
                Session["id"] = null;
                ViewState["PATHDOC"] = null;
            }
            else
            {
                lblMessage.Text = "Error.";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divallList.Visible = false;

            btnAddNew.Visible = false;

            tblNewDataEntry.Visible = false;
            ViewState["NEWCLAIM"] = null;
            Session["id"] = null;
            ViewState["PATHDOC"] = null;

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

        protected void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox chckheader = (CheckBox)gvList.HeaderRow.FindControl("chkSelectAll");
                foreach (GridViewRow row in gvList.Rows)
                {
                    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    if (chckheader.Checked == true)
                    {
                        chckrw.Checked = true;

                    }
                    else
                    {
                        chckrw.Checked = false;
                    }

                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnSubmitAll_Click(object sender, EventArgs e)
        {
            int count = 0;
            try
            {
                CheckBox chckheader = (CheckBox)gvList.HeaderRow.FindControl("chkSelectAll");
                foreach (GridViewRow row in gvList.Rows)
                {
                    CheckBox chckrw = (CheckBox)row.FindControl("chkSelect");
                    if (chckrw.Checked == true)
                    {
                        chckrw.Checked = true;
                        count = 1;
                    }
                   
                }

                if (count == 1)
                {
                    if (drActionAll.SelectedValue == "")
                    {

                        //divAlert.Visible = true;
                        string script = $@"<script type='text/javascript'>alert('Please Select Action Type.');</script>";
                        //lblMessage.Text = "Please Select Action Type.";
                        //divAlert.Visible = true;
                        //lblMessage.Visible = true;

                    }
                    else
                    {
                        string drp = drActionAll.SelectedValue;
                        string Rmk = txtAllRmk.Text;



                        DataSet dsStatus = new DataSet();
                        dsStatus = objExp.GetStatus((string)Session["sCompID"], (string)Session["sEmpID"]);
                        if (Session["sEmpCode"].ToString() == "ABC123")
                        {
                            objExp.Type = "3";
                        }
                        //else if (Session["sEmpCode"].ToString() == "NP230")
                        else if (Session["sEmpCode"].ToString() == "CONSP29")
                        {
                            objExp.Type = "5";
                        }

                        else if (Session["sEmpCode"].ToString() == "NP3435")
                        {
                            objExp.Type = "5";
                        }

                        else if (Session["sEmpCode"].ToString() == "NP3403")
                        {
                            objExp.Type = "6";
                        }
                        else
                        {
                            if (dsStatus.Tables[0].Rows[0]["COUNTS"].ToString() != "0")
                            {
                                objExp.Type = "4";
                            }
                        }

                        objExp.UpdateAllPettyExpSubmit(MakeChkXml(gvList), drActionAll.SelectedValue, txtAllRmk.Text, (string)Session["sEmpCode"]);

                        //MakeChkForEmailXml(gvDomClaim);
                        //ActionGetAll(drp, Rmk);
                        MakeChkForEmailXml(gvList);
                        divAlert.Visible = true;
                        lblMessage.Visible = true;
                        lblMessage.Text = "Action Send Successfully.";
                        string script = $@"<script type='text/javascript'>alert('Action Submitted Successfully.');</script>";
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                        Getdetails();


                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Visible = true;
                    lblMessage.Text = "Please select at least one checkbox before click on Submit button.";
                    string script = $@"<script type='text/javascript'>alert('Please select at least one checkbox before click on Submit button.');</script>";
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = ex.Message;
            }
        }
        private string MakeChkXml(GridView gvList)
        {
            StringBuilder sbChkGlCode = new StringBuilder();
            StringBuilder sbUnChkGlCode = new StringBuilder();
            string xmlChkGlCode = string.Empty;
            string xmlUnChkGlCode = string.Empty;

            sbChkGlCode.Append("<ROOT>");

            foreach (GridViewRow gvr in gvList.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {
                    sbChkGlCode.Append("<CHKGLSUMBIT CODE='" + ((Label)gvr.FindControl("lblAppNo")).Text.Trim() + "'/>");

                }

            }
            sbChkGlCode.Append("</ROOT>");

            xmlChkGlCode = sbChkGlCode.ToString();

            return xmlChkGlCode;
        }

        private string MakeChkForEmailXml(GridView gvList)
        {
            StringBuilder sbChkGlCode = new StringBuilder();
            StringBuilder sbUnChkGlCode = new StringBuilder();
            string xmlChkGlCode = string.Empty;
            string xmlUnChkGlCode = string.Empty;



            foreach (GridViewRow gvr in gvList.Rows)
            {
                string Action = string.Empty;
                string voucherDate = string.Empty;
                string Approver = string.Empty;
                string ClaimNo = string.Empty;

                if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                {

                    ClaimNo = ((LinkButton)gvr.FindControl("lnkVoucherNo")).Text.Trim();

                    Approver = ((TextBox)gvr.FindControl("txtMISCSTS")).Text.Trim();

                    Action = drActionAll.SelectedValue;

                    voucherDate = ((Label)gvr.FindControl("lblVoucherDate")).Text.Trim();

                    obj.SendMiscEmail(ClaimNo, Approver, Action, voucherDate, Session["sCompID"].ToString(), Session["sEmpID"].ToString());
                }

            }


            return xmlChkGlCode;
        }

        //private void SENDUDATEMAIL(string ClaimNO, string Approver, string Action, string voucherDate)
        //{
        //    emailSend = new NewPortal2023.ESS.Email();
        //    DataSet dsmkkMail = new DataSet();
        //    DateTime date = DateTime.Now;

        //    if (Action == "Approve" || Action == "APPROVE")
        //    {
        //        dsmkkMail = emailSend.GetEmpMiscEXpRecDe((string)Session["sCompID"], (string)Session["sEmpID"], ClaimNO, Approver);


        //        if (dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() != "")
        //        {
        //            string clientbodys = "Dear " + dsmkkMail.Tables[2].Rows[0]["APPROVARNAME"].ToString() + ",<br><br> A Miscellaneous Expense is received for approval." + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + " claim is Approved by the " + Approver
        //            + ".Kindly take the action for the same through logging-in into ESS portal.<br>"
        //           + " <br>Claim Type:- Miscellaneous Expense"
        //                 + "<br>Claim No :- " + ClaimNO
        //                 //+ "<br>Voucher Date :- " + voucherDate
        //                 + "<br><br>ThankYou.<br><br>";
        //            string emails = dsmkkMail.Tables[2].Rows[0]["APPROVARMAIL"].ToString();
        //            string emailsCC = dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString() + ',' + dsmkkMail.Tables[1].Rows[0]["EMP_EMAIL"].ToString();
        //            //string emails = "techsupport@sequelgroup.co.in";
        //            //string emailsCC = "payrollservices@sequelgroup.co.in";
        //            string subjects = "Do Not Reply: Miscellaneous Expense";
        //            emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);

        //        }
        //    }
        //    else if (Action == "Reject" || Action == "REJECT")
        //    {
        //        dsmkkMail = emailSend.GetMiscRejectMAil((string)Session["sCompID"], (string)Session["sEmpID"], ClaimNO, Approver);

        //        string clientbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>A Miscellaneous Expense is rejected by the " + dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString()
        //           + ".Kindly check the claim for the same through logging-in into ESS portal.<br>"
        //          + " <br>Claim Type:- Miscellaneous Expense"
        //                + "<br>Claim No :- " + ClaimNO
        //                //+ "<br>Voucher Date :- " + voucherDate
        //                + "<br><br>ThankYou.<br><br>";
        //        string emails = dsmkkMail.Tables[1].Rows[0]["EMP_EMAIL"].ToString();
        //        string emailsCC = dsmkkMail.Tables[0].Rows[0]["CHECKERMAIL"].ToString();
        //        //string emails = "techsupport@sequelgroup.co.in";
        //        //string emailsCC = "payrollservices@sequelgroup.co.in";

        //        string subjects = "Do Not Reply: Miscellaneous Expense";
        //        emailSend.SendEmailALLNPL(emails, emailsCC, subjects, clientbodys);
        //    }
        //}
    }
}