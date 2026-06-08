using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

using System.Web.Security;

using System.Web.UI.HtmlControls;

using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Services;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace NewPortal2023.ESS
{
    public partial class PettyExpensex : System.Web.UI.Page
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
                    btnAddNew.Visible = true;
                    tblNewDataEntry.Visible = false;
                    divList.Visible = true;
                    divallList.Visible = false;
                    btnBackList.Visible = false;
                    fillDropDown();

                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }


        }

        //[WebMethod]
        //public static List<string> GetEmployeeName(string empName)
        //{
        //    List<string> empResult = new List<string>(); 
        //    using (SqlConnection con = new SqlConnection(@"Server = 162.215.230.14; Database = SEQUELPAY; user id = payrollservices; pwd = soPL@786"))
        //    {
        //        using (SqlCommand cmd = new SqlCommand())
        //        {
        //            cmd.CommandText = "select Top 10 emp_fname from gm_empmast_Web where emp_fname LIKE ''+@SearchEmpName+'%'";
        //            cmd.Connection = con;
        //            con.Open();
        //            cmd.Parameters.AddWithValue("@SearchEmpName", empName);
        //            SqlDataReader dr = cmd.ExecuteReader();
        //            while (dr.Read())
        //            {
        //                empResult.Add(dr["EmployeeName"].ToString());
        //            }
        //            con.Close();
        //            return empResult;
        //        }
        //    }
        //}
        //    //public List<string> Values { get; set; }
        //private void GetValues()
        //{

        //    Values = new List<string>();

        //    Values.Add("Apple");
        //    Values.Add("Orange");
        //    Values.Add("Banana");
        //    Values.Add("Pear");
        //    Values.Add("Black Berry");
        //    Values.Add("Pineapple");
        //}

        //protected void txtSearch_TextChanged(object sender, EventArgs e)
        //{
        //    var dropDownList = sender as TextBox;
        //    var options = (from o in Values
        //                   where o.StartsWith(dropDownList.Text, StringComparison.InvariantCultureIgnoreCase)
        //                   select o).ToList();

        //    ddlSearch.DataSource = options;
        //    ddlSearch.DataBind();
        //}

        protected void lnkVoucherno_Click(object sender, EventArgs e)
        {
            LinkButton lnkVoucherNo = (LinkButton)sender;
            string id = ((Label)lnkVoucherNo.NamingContainer.FindControl("lblAppNo")).Text;
            string status = ((Label)lnkVoucherNo.NamingContainer.FindControl("lblStatusId")).Text;
            ViewState["Status"] = status;
            if (status == "0")
            {
                ViewState["Status"] = "9";
                status = "9";
            }
            if (status == "9")
            {
                btnAddNew.Visible = false;
                //btnAddNew.Text = "Add more expense";              
                divallList.Visible = true;
                tblNewDataEntry.Visible = true;
                divList.Visible = false;
                ClearAll();
                //Getdetails();
                SubmitBtn.Visible = true;

                //foreach (DataControlField col in gvallList.Columns)
                //{
                //    if (col.HeaderText == "Action")
                //        col.Visible = true;

                //}
                btnAddNew.Visible = false;
            }
            else
            {
                DisplayDocumentsEnter(id);
                btnAddNew.Visible = false;

                tblNewDataEntry.Visible = false;
                divallList.Visible = true;
                divList.Visible = false;
                ClearAll();
                SubmitBtn.Visible = false;
                btnBackList.Visible = true;

            }


            try
            {


                Session["id"] = id;
                ViewState["NEWCLAIM"] = "0";

                ds = objExp.GetPettyReimbById((string)Session["sEmpCode"], (string)Session["id"]);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (status == "9")
                    {
                        gvallList.DataSource = ds;
                        gvallList.DataBind();
                        txtTotalAmt.Text = ds.Tables[1].Rows[0]["AMOUNT"].ToString();
                        gvallList.HeaderRow.Cells[8].Visible = true;
                        gvallList.Columns[8].Visible = true;

                        gvallList.HeaderRow.Cells[9].Visible = true;
                        gvallList.Columns[9].Visible = true;
                        txtRmk.Text = ds.Tables[0].Rows[0]["EMPRMK"].ToString();
                        txtRmk.ReadOnly = false;

                    }
                    else
                    {
                        gvallList.DataSource = ds;
                        gvallList.DataBind();
                        gvallList.HeaderRow.Cells[8].Visible = false;
                        gvallList.Columns[8].Visible = false;
                        txtTotalAmt.Text = ds.Tables[1].Rows[0]["AMOUNT"].ToString();
                        txtRmk.Text = ds.Tables[0].Rows[0]["EMPRMK"].ToString();
                        gvallList.HeaderRow.Cells[9].Visible = false;
                        gvallList.Columns[9].Visible = false;
                        txtRmk.ReadOnly = true;
                    }


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

            string employeeType = Session["sEmpType"] as string;

            if (!string.IsNullOrEmpty(employeeType) && employeeType == "E0000023")
            {
                txtOnbehalfof.Enabled = false;
            }

            else
            {
                txtOnbehalfof.Enabled = true;
            }

            if (btnAddNew.Text == "Add more expense")
            {

                if (!string.IsNullOrEmpty(employeeType) && employeeType == "E0000023")
                {
                    txtOnbehalfof.Enabled = false;
                }

                else
                {
                    txtOnbehalfof.Enabled = true;
                }

                ClearAll();
                tblNewDataEntry.Visible = true;
                divallList.Visible = true;
                btnAddNew.Visible = true;
            }
            else
            {
                divList.Visible = false;
                divallList.Visible = false;
                btnAddNew.Visible = false;
                tblNewDataEntry.Visible = true;
                SubmitBtn.Visible = true;
                foreach (DataControlField col in gvallList.Columns)
                {
                    if (col.HeaderText == "Action")
                        col.Visible = true;

                }
            }


            ViewState["NEWCLAIM"] = "1";

        }

        protected void gvSummary_RowDataBound(object sender, EventArgs e)
        {


        }

        public bool IsValidNumber(string input)
        {
            // Regular expression to allow only integers and decimals
            string pattern = @"^\d+(\.\d+)?$";
            return Regex.IsMatch(input, pattern);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            lblMessage.Text = "";
            btnAddNew.Text = "Add new voucher";

            //if (drpExpensetype.SelectedValue == "")

            if (drpExpensetype.SelectedItem.Text == "")
            {
                objcommon = new NewPortal2023.ESS.Common();

                string message = "Please Select Expense Type";
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlert, "danger");

                return;
            }

            if (txtDate.Text == "")
            {
                objcommon = new NewPortal2023.ESS.Common();

                string message = "Please Select Date";
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlert, "danger");

                return;
            }

            if (txtAmount.Text == "" || !IsValidNumber(txtAmount.Text))
            {
                objcommon = new NewPortal2023.ESS.Common();

                string message = "Please Enter Amount";
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlert, "danger");

                return;
            }

            if (btnPcktReimb.Text == "Update")
            {
                btnAddNew.Visible = false;
            }
            else
            {
                btnAddNew.Visible = true;

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
                //Getdetails();

            }


            ClearAll();

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            divList.Visible = true;
            divallList.Visible = false;


            Getdetails();
            tblNewDataEntry.Visible = false;
            ViewState["NEWCLAIM"] = null;
            Session["id"] = null;
            ViewState["PATHDOC"] = null;
            ViewState["Status"] = null;

            btnAddNew.Text = "Add New Voucher";
            btnAddNew.Visible = true;

        }

        private void SaveDetails()
        {

            objExp.ExpenseType = drpExpensetype.SelectedItem.Text;
            objExp.Date = txtDate.Text.Trim();
            objExp.Onbehalfof = txtOnbehalfof.Text;
            objExp.Natureofexpense = txtNatureofexpense.Text;
            objExp.Billno = txtBillno.Text;
            objExp.Amount = txtAmount.Text;
            objExp.Status = "9";

            if (ViewState["NEWCLAIM"].ToString() == "1")
            {
                objExp.TravelType = "1";
                ds = objExp.InsertPettyReimb((string)Session["sEmpCode"], (string)Session["sEmpID"]);
                if (ds.Tables[3].Rows[0]["result"].ToString() == "")
                {
                    entryCode = ds.Tables[2].Rows[0]["Entry_Aid"].ToString();

                    if (fupUpload.PostedFile.FileName != "")
                    {
                        UploadFinFile(entryCode);
                        lnkShowDoc.Text = "Download";
                    }
                    else
                    {
                        lnkShowDoc.Text = "";
                    }

                    tblNewDataEntry.Visible = true;
                    divallList.Visible = true;
                    divList.Visible = false;
                    ClearAll();
                    SubmitBtn.Visible = true;
                    btnPcktReimb.Text = "Add";
                    drpExpensetype.SelectedIndex = -1;
                    gvallList.DataSource = ds.Tables[0];
                    gvallList.DataBind();
                    txtTotalAmt.Text = ds.Tables[1].Rows[0]["AMOUNT"].ToString();
                    btnAddNew.Visible = false;
                    lblDocAddr.Visible = false;
                    lnkShowDoc.Visible = false;

                    lblMessage.Text = "Expenses Save and Generated Voucher No Successfully";
                    objcommon.Display("Validate", "DisplayErrorMessage('Expenses Save and Generated Voucher No Successfully.');");
                    Session["id"] = ds.Tables[0].Rows[0]["EXPENSES_NO"].ToString();
                    ViewState["NEWCLAIM"] = "0";
                    SubmitBtn.Visible = true;

                    foreach (DataControlField col in gvallList.Columns)
                    {
                        if (col.HeaderText == "Action")
                            col.Visible = true;
                    }

                }
            }
            else if (ViewState["NEWCLAIM"].ToString() == "0")
            {
                string employeeType = Session["sEmpType"] as string;

                if (!string.IsNullOrEmpty(employeeType) && employeeType == "E0000023")
                {
                    txtOnbehalfof.Enabled = false;
                }

                else
                {
                    txtOnbehalfof.Enabled = true;
                }

                objExp.TravelType = "0";
                if (btnPcktReimb.Text == "Update")
                {
                    if (!string.IsNullOrEmpty(employeeType) && employeeType == "E0000023")
                    {
                        txtOnbehalfof.Enabled = false;
                    }

                    else
                    {
                        txtOnbehalfof.Enabled = true;
                    }

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


                        tblNewDataEntry.Visible = true;
                        divallList.Visible = true;
                        divList.Visible = false;
                        //ClearAll();
                        fillDropDown();
                        SubmitBtn.Visible = true;
                        btnPcktReimb.Text = "Add";
                        gvallList.DataSource = ds.Tables[0];
                        gvallList.DataBind();
                        txtTotalAmt.Text = ds.Tables[1].Rows[0]["AMOUNT"].ToString();
                        btnAddNew.Visible = false;
                        lblDocAddr.Visible = false;
                        lnkShowDoc.Visible = false;
                        objcommon.Display("Validate", "DisplayErrorMessage('Expenses Updated Successfully.');");
                        foreach (DataControlField col in gvallList.Columns)
                        {
                            if (col.HeaderText == "Action")
                            {
                                col.Visible = true;
                                col.HeaderText = "Action";
                            }


                        }

                    }

                }
                else
                {
                    objExp.Status = Session["id"].ToString();
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
                        objcommon.Display("Validate", "DisplayErrorMessage('Expenses Save Successfully.');");
                        tblNewDataEntry.Visible = true;
                        divallList.Visible = true;
                        divList.Visible = false;
                        ClearAll();
                        SubmitBtn.Visible = true;
                        btnPcktReimb.Text = "Add";
                        gvallList.DataSource = ds.Tables[0];
                        gvallList.DataBind();
                        txtTotalAmt.Text = ds.Tables[1].Rows[0]["AMOUNT"].ToString();
                        btnAddNew.Visible = false;

                        foreach (DataControlField col in gvallList.Columns)
                        {
                            if (col.HeaderText == "Action")
                                col.Visible = true;

                        }


                    }

                }
                //entryCode = Session["id"].ToString();
                //UploadEnter_Click(entryCode);
                //DisplayDocumentsEnter(entryCode);
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

        private void UploadEnter_Click(string entryCode)
        {
            if (fupldDocumentEnter.PostedFile.FileName != "")
            {
                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Expenses\\Petty\\" + "\\" + entryCode + "\\";

                System.IO.Stream fileInputStream = fupldDocumentEnter.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                fileInputStream.Close();
                /* Create Folder */

                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = Path.GetFileNameWithoutExtension(fupldDocumentEnter.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocumentEnter.FileName.Trim());

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
        }

        private void UploadFinFile(string entryCode)
        {
            string message;

            if (fupUpload.PostedFile.FileName != "")
            {
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
        }

        protected void drpExpensetype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //private void Getdetails()
        //{
        //    try
        //    {


        //        ds = objExp.GetPettyReimb((string)Session["sEmpCode"]);
        //        if (ds.Tables[0].Rows.Count > 0)
        //        {
        //            string employeeType = Session["sEmpType"] as string;

        //            if (!string.IsNullOrEmpty(employeeType) && employeeType == "E0000023")
        //            {
        //                gvList.Columns[6].Visible = true;
        //                gvList.Columns[7].Visible = false;
        //                gvList.DataSource = ds;
        //                gvList.DataBind();
        //            }

        //            else
        //           {
        //                gvList.DataSource = ds;
        //                gvList.DataBind();
        //            }


        //            //gvList.DataSource = ds;
        //            //gvList.DataBind();
        //            btnAddNew.Visible = false;


        //        }
        //        else
        //        {
        //            gvList.DataSource = null;
        //            gvList.DataBind();
        //            btnAddNew.Visible = true;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = ex.Message;
        //    }
        //}

        private void ClearAll()
        {
            drpExpensetype.SelectedIndex = -1;
            txtDate.Text = "";
            txtOnbehalfof.Text = "";
            txtNatureofexpense.Text = "";
            txtBillno.Text = "";
            txtAmount.Text = "";
            fupldDocumentEnter.Dispose();

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            string employeeType = Session["sEmpType"] as string;

            if (!string.IsNullOrEmpty(employeeType) && employeeType == "E0000023")
            {
                txtOnbehalfof.Enabled = false;
            }

            else
            {
                txtOnbehalfof.Enabled = true;
            }
            lblMessage.Text = "";

            divallList.Visible = true;

            btnAddNew.Visible = false;
            tblNewDataEntry.Visible = true;
            LinkButton lnkRequestNo = (LinkButton)sender;
            string id = ((Label)lnkRequestNo.NamingContainer.FindControl("lblEntryAid")).Text;
            ViewState["Entry_Aid"] = id;
            DataSet ds = objExp.UpdateById((string)Session["sEmpID"], id);
            if (ds.Tables[0].Rows.Count > 0)
            {
                drpExpensetype.SelectedItem.Text = ds.Tables[0].Rows[0]["EXPENSE_TYPE"].ToString();
                txtDate.Text = ds.Tables[0].Rows[0]["DATE"].ToString();
                txtOnbehalfof.Text = ds.Tables[0].Rows[0]["ON_BEHALF_OF"].ToString();
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
                objcommon.Display("Validate", "DisplayErrorMessage('Delete Successfully.');");
                gvallList.DataSource = ds.Tables[0];
                gvallList.DataBind();
                txtTotalAmt.Text = ds.Tables[1].Rows[0]["AMOUNT"].ToString();
                btnAddNew.Visible = false;
                if (ds.Tables[0].Rows.Count > 0)
                {

                }
                {
                    SubmitBtn.Visible = true;
                }
            }
            else
            {
                lblMessage.Text = "Error.";
            }
        }

        protected void SubmitBtn_Click(object sender, EventArgs e)
        {
            if (ValidatefileUpload() == false)
            {
                divAlert.Visible = true;
                lblMessage.Visible = true;
                lblMessage.Text = "Browse file to upload.";
                //ShowMessage("Browse file to upload.", WarningType.Danger);
                string script = $@"<script type='text/javascript'>alert('Browse file to upload.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                return;
            }

            objExp.AppNo = Session["id"].ToString();
            objExp.Status = "3";
            objExp.ReportingRemarks = txtRmk.Text;

            DataSet ds = objExp.FinalUpdatePettyRemb();

            if (ds.Tables[0].Rows[0]["result"].ToString() == "")
            {
                entryCode = Session["id"].ToString();
                UploadEnter_Click(entryCode);
                DisplayDocumentsEnter(entryCode);
                btnAddNew.Visible = true;
                Getdetails();
                tblNewDataEntry.Visible = false;
                divList.Visible = true;
                divallList.Visible = false;

                objcommon = new NewPortal2023.ESS.Common();

                string message = "Miscellaneous Expenses Submitted Successfully";
                string script = $@"<script type='text/javascript'>alert('{message}');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objcommon.SetMessageColor(divAlert, "success");
                lblMessage.Text = message;

                btnAddNew.Text = "Add New Voucher";
                SENDUDATEMAIL(Session["id"].ToString(), "Miscellaneous Expense");
                ViewState["NEWCLAIM"] = null;
                Session["id"] = null;
                ViewState["PATHDOC"] = null;
                ViewState["Status"] = null;

                btnAddNew.Visible = true;
                btnAddNew.Enabled = true;
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
                if (ViewState["Status"] == null)
                {
                    gvallList.HeaderRow.Cells[8].Visible = true;
                    gvallList.Columns[8].Visible = true;

                    gvallList.HeaderRow.Cells[9].Visible = true;
                    gvallList.Columns[9].Visible = true;
                }
                else
                {
                    if (ViewState["Status"].ToString() == "9")
                    {
                        gvallList.HeaderRow.Cells[8].Visible = true;
                        gvallList.Columns[8].Visible = true;

                        gvallList.HeaderRow.Cells[9].Visible = true;
                        gvallList.Columns[9].Visible = true;
                    }
                    else
                    {
                        gvallList.HeaderRow.Cells[8].Visible = false;
                        gvallList.Columns[8].Visible = false;

                        gvallList.HeaderRow.Cells[9].Visible = false;
                        gvallList.Columns[9].Visible = false;
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

        protected void btnBackList_Click(object sender, EventArgs e)
        {

            Getdetails();
            tblNewDataEntry.Visible = false;
            divList.Visible = true;
            divallList.Visible = false;
            btnBackList.Visible = false;
            Getdetails();
            ViewState["NEWCLAIM"] = null;
            Session["id"] = null;
            ViewState["PATHDOC"] = null;
            ViewState["Status"] = null;

            btnAddNew.Text = "Add New Voucher";
            btnAddNew.Visible = true;
        }

        //dnyaneshwar

        public void fillDropDown()
        {
            objExp.fillExpensetype(drpExpensetype);
        }

        private void Getdetails()
        {
            try
            {
                ds = objExp.GetPettyReimb((string)Session["sEmpCode"]);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    string employeeType = Session["sEmpType"] as string;

                    // Check employee type condition



                    gvList.DataSource = ds;
                    gvList.DataBind();


                    if (!string.IsNullOrEmpty(employeeType) && employeeType == "E0000023")
                    {
                        // Show HOD Rmk and hide HR Rmk
                        gvList.Columns[6].Visible = true;  // HOD Rmk column index 
                        gvList.Columns[7].Visible = false; // HR Rmk column index 
                    }
                    else
                    {
                        // Show HR Rmk and hide HOD Rmk
                        gvList.Columns[6].Visible = false; // HOD Rmk column index
                        gvList.Columns[7].Visible = true;  // HR Rmk column index
                    }

                    btnAddNew.Visible = false;
                }
                else
                {
                    gvList.DataSource = null;
                    gvList.DataBind();

                    btnAddNew.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        //dnyaneshwar
        private void SENDUDATEMAIL(string VoucherNo, string type)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DateTime date = DateTime.Now;

            string EMPNAME = Session["sEmpName"].ToString();

            string clientbodys = "Dear " + EMPNAME + ",<br><br>Your " + type + " Expense is Submitted Successfully.<br>"
               + "Once the action taken will be notified to you through mail or same can be checked through ESS portal."
               + "<br>Reimbursement type:- " + type
               + "<br>Applied Date :- " + date
               + "<br>Voucher number:- " + VoucherNo
               + "<br><br>ThankYou.<br><br>";
            string emails = Session["sEmailId"].ToString();
            string subjects = "Do Not Reply: Expense";
            emailSend.SendEmailNPL(emails, subjects, clientbodys);


            string checkerbodys = "Dear Payroll Team,<br><br>" + EMPNAME +
                " " + type + " Expense is received for approval. Kindly take the action for the same through logging-in into ESS portal."
                + "<br>Reimbursement type:- " + type
                + "<br>Applied Date :- " + date
                + "<br>Voucher number:- " + VoucherNo
                + "<br><br>ThankYou.<br><br>";

            emails = "payrollservices@sequelgroup.co.in";
            emailSend.SendEmailNPL(emails, subjects, checkerbodys);
        }

        private Boolean ValidatefileUpload()
        {
            if (fupldDocumentEnter.PostedFile.FileName != "" || lnkShowDoc.Text != "")
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

    }
}