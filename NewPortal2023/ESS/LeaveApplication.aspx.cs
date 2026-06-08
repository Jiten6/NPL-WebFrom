using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Xml;
using System.IO;
using System.Text;
using System.Globalization;

namespace NewPortal2023.ESS
{
    public partial class LeaveApplication1 : System.Web.UI.Page
    {
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        NewPortal2023.ESS.LeaveApplication objInv = new NewPortal2023.ESS.LeaveApplication();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.DBUtility obdbutility = new NewPortal2023.ESS.DBUtility();

        DataSet dsInv = new DataSet();
        DataSet ds = new DataSet();
        DataSet dsleave = new DataSet();

        string OldYear, NewYear;
        string ID = "0";
        string joindate;
        string leaveAddDate;
        string status;
        int TotalMonth, lastUpdateTotalMonth;
        double count;
        private string SourcePath = string.Empty;
        private string savePath = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["sCompID"] != null)
                {
                    try
                    {


                        //GetDetail();
                        string strResult = objcommon.Validate_ControlInfo("INV");
                        ID = Convert.ToString(Request.QueryString["id"]);
                        if (strResult.Contains("This page is currently unavailable.") == true)
                        {
                            Response.Redirect("Unavailable.aspx?strName=Investment Details");
                            return;
                        }

                        FillReason();
                        FillType();
                        FileUpload.Visible = false;
                        lblUpload.Visible = false;

                        uploadedDoc.Visible = true;
                        //DisplayDocuments();

                        if (ID != "" && ID != null)
                        {
                            FillDetails();
                        }
                        else
                        {
                            FillStatus("0");
                        }
                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while loading investment details.');");
                    }
                }
                else
                {
                    Response.Redirect("Logout.aspx");
                }
            }
        }

        private void LockDetails()
        {
            txtDate.ReadOnly = true;
            txtToDt.ReadOnly = true;
            drpReason.Enabled = false;
            txtAdd.ReadOnly = true;
            txtRem.ReadOnly = true;
            drpStatus.Enabled = true;
            //btnFrom.Visible = false;
            //btnTo.Visible = false;
            drpLeave.Enabled = false;

        }

        private void FillDetails()
        {
            DataSet ds = new DataSet();


            ds = objInv.GetDetails((string)Session["sCompID"], (string)Session["sEmpID"], ID);

            if (ds.Tables.Count > 0)
            {
                for (int cnt = 0; cnt <= ds.Tables[0].Rows.Count - 1; cnt++)
                {
                    txtDate.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["FROM_DT"]);
                    txtToDt.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["TO_DATE"]);
                    drpReason.SelectedValue = Convert.ToString(ds.Tables[0].Rows[cnt]["REASON_ID"]);
                    txtAdd.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["ADDRESS"]);
                    txtRem.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["REMARKS"]);
                    //FillStatus(Convert.ToString(ds.Tables[0].Rows[cnt]["STATUS"]));
                    FillStatus("5");
                    drpStatus.SelectedValue = Convert.ToString(ds.Tables[0].Rows[cnt]["STATUS"]);
                    drpLeave.SelectedValue = Convert.ToString(ds.Tables[0].Rows[cnt]["LEAVE_TYPE"]);
                    lblcrdate.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["CREATEDDT"]);
                    lblLeaves.Text = Convert.ToString(ds.Tables[0].Rows[cnt]["DIFF"]);

                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        string emp_code = ds.Tables[1].Rows[0]["EMP_MID"].ToString();
                        DisplayDocumentsChk(emp_code, ID);
                        uploadedDoc.Visible = true;
                    }

                    if (Convert.ToString(ds.Tables[0].Rows[cnt]["STATUS"]) != "0")
                    {
                        LockDetails();
                    }
                }

            }
            else
            {
                throw new Exception("Error");

            }
        }

        private void GetDetail()
        {
            DataSet ds = new DataSet();
            ds = objInv.GetDetailEmps((string)Session["sCompID"], (string)Session["sEmpID"], ID);
            if (ds.Tables.Count > 0)
            {
                for (int cnt = 0; cnt <= ds.Tables[0].Rows.Count - 1; cnt++)
                {
                    joindate = Convert.ToString(ds.Tables[0].Rows[cnt]["Date"]);
                }

                if (ds.Tables[1].Rows.Count > 0)
                {
                    for (int cnt1 = 0; cnt1 <= ds.Tables[1].Rows.Count - 1; cnt1++)
                    {

                        leaveAddDate = Convert.ToString(ds.Tables[1].Rows[cnt1]["Leave_Add_Date"]);
                        //joindate = Convert.ToString(ds.Tables[1].Rows[cnt1]["Date"]);
                        status = Convert.ToString(ds.Tables[1].Rows[cnt1]["status"]);
                    }
                }
                string currentDate = DateTime.Now.ToString();

                //string[] strDateTime = joindate.Split(' ');
                //string[] leaveUpdateDateTime = leaveAddDate.Split(' ');
                //string[] endDateTime = currentDate.Split(' ');

                DateTime strDate = Convert.ToDateTime(joindate);
                DateTime leavelastUpdateDate = Convert.ToDateTime(leaveAddDate);
                DateTime endDate = Convert.ToDateTime(currentDate);


                if (leavelastUpdateDate != null && endDate != null)
                {
                    getMonthlastUpdate(leavelastUpdateDate, endDate);
                    if (lastUpdateTotalMonth >= 0)
                    {
                        for (int i = 0; i < lastUpdateTotalMonth; i++)
                        {
                            count = count + 2.667;

                        }
                    }
                }
                else if (strDate != null && endDate != null)
                {
                    getMonthstart(strDate, endDate);
                    for (int i = 0; i < TotalMonth; i++)
                    {
                        count = count + 2.667;
                        if (TotalMonth == 2.667)
                        {
                            DataSet ds1 = new DataSet();
                            // ds1 = obdbutility.
                        }
                        else if (TotalMonth == 5.334)
                        {
                        }
                        else if (TotalMonth == 8.001)
                        {

                        }
                    }
                }

            }
        }

        private int getMonthlastUpdate(DateTime leaveUpdateDate, DateTime endDate)
        {
            lastUpdateTotalMonth = Math.Abs((leaveUpdateDate.Month - endDate.Month) + 12 * (leaveUpdateDate.Year - endDate.Year));
            return lastUpdateTotalMonth;
        }

        private int getMonthstart(DateTime strDate, DateTime endDate)
        {

            TotalMonth = Math.Abs((strDate.Month - endDate.Month) + 12 * (strDate.Year - endDate.Year));
            return TotalMonth;
        }

        private void FillReason()
        {
            dsInv = objInv.GetType((string)Session["sCompID"], (string)Session["sEmpID"]);
            drpReason.Items.Clear();
            drpReason.DataTextField = "REASON";
            drpReason.DataValueField = "Cid";
            drpReason.DataSource = dsInv;
            drpReason.DataBind();
            drpReason.Items.Insert(0, new ListItem("[Select One]", "0"));
        }

        private void FillType()
        {
            dsInv = objInv.GetLeave((string)Session["sCompID"], (string)Session["sEmpID"]);
            drpLeave.Items.Clear();
            drpLeave.DataTextField = "LEAVE";
            drpLeave.DataValueField = "Cid";
            drpLeave.DataSource = dsInv.Tables[0];
            drpLeave.DataBind();
            drpLeave.Items.Insert(0, new ListItem("[Select One]", "0"));
        }

        private void FillStatus(string Status)
        {
            dsInv = objInv.GetStatus(Status);
            drpStatus.Items.Clear();
            drpStatus.DataTextField = "status";
            drpStatus.DataValueField = "id";
            drpStatus.DataSource = dsInv;
            drpStatus.DataBind();
            drpStatus.Items.Insert(0, new ListItem("[Select One]", "-"));
        }

        //protected void btnFrom_Click(object sender, EventArgs e)
        //{
        //    calFrom.Visible = true;
        //}
        //protected void calFrom_SelectionChanged(object sender, EventArgs e)
        //{
        //    txtDate.Text = calFrom.SelectedDate.Date.ToString("dd-MMM-yyyy");
        //    calFrom.Visible = false;
        //    SetDays();
        //    checkleavebalance();

        //}
        //protected void calTo_SelectionChanged(object sender, EventArgs e)
        //{
        //    txtToDt.Text = calTo.SelectedDate.Date.ToString("dd-MMM-yyyy");

        //    calTo.Visible = false;
        //    SetDays();
        //    checkleavebalance();
        //}

        protected void SetDays()
        {
            if (txtDate.Text.Trim() != "" && txtToDt.Text.Trim() != "")
            {
                dsInv = objNps.getDays((string)Session["sCompID"], (string)Session["sEmpID"], txtDate.Text, txtToDt.Text);
                //lblLeaves.Text = Convert.ToString((Convert.ToDateTime(txtToDt.Text) - Convert.ToDateTime(txtDate.Text)).Days + 1);
                lblLeaves.Text = dsInv.Tables[0].Rows[0]["NUMBEROFDAYS"].ToString();
                if ((Convert.ToInt16(lblLeaves.Text) >= 3) && (drpLeave.SelectedItem.ToString() == "SL"))
                {
                    FileUpload.Visible = true;
                    lblUpload.Visible = true;
                    uploadedDoc.Visible = true;
                }
                else
                {
                    FileUpload.Visible = false;
                    lblUpload.Visible = false;
                    uploadedDoc.Visible = false;
                }
                ViewState["LEAVECOUNT"] = lblLeaves.Text;

            }
        }

        //protected void btnTo_Click(object sender, EventArgs e)
        //{
        //    calTo.Visible = true;
        //}

        private Boolean ValidateData()
        {

            if (txtDate.Text.Trim() == "")
            {
                lblMessage.Text = "Select From date.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select From date.');");
                return false;
            }
            if (txtToDt.Text.Trim() == "")
            {
                lblMessage.Text = "Select To date.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select To date.');");
                return false;
            }
            //if (Convert.ToDateTime(txtDate.Text) > Convert.ToDateTime(txtToDt.Text))
            //{
            //    lblMessage.Text = "From date can not be greater than to date.";
            //    objcommon.Display("Validate", "DisplayErrorMessage('From date can not be greater than to date.');");
            //    return false;
            //}
            //if (objcommon.ValidateCalendarYear(Convert.ToDateTime(txtDate.Text)) == false)
            //{
            //    lblMessage.Text = "From date is not within current Calendar year.";
            //    objcommon.Display("Validate", "DisplayErrorMessage('From date is not within current Calendar year.');");
            //    return false;

            //}
            //if (objcommon.ValidateCalendarYear(Convert.ToDateTime(txtToDt.Text)) == false)
            //{
            //    lblMessage.Text = "To date is not within current Calendar year.";
            //    objcommon.Display("Validate", "DisplayErrorMessage('To date is not within current Calendar year.');");
            //    return false;

            //}
            if (drpLeave.SelectedValue.Trim() == "0")
            {
                lblMessage.Text = "Select Leave Type.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select Leave Type.');");
                return false;
            }
            if (drpReason.SelectedValue.Trim() == "0")
            {
                lblMessage.Text = "Select Reason.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select Reason.');");
                return false;
            }
            if (drpStatus.SelectedValue.Trim() == "-")
            {
                lblMessage.Text = "Select status.";
                objcommon.Display("Validate", "DisplayErrorMessage('Select status.');");
                return false;
            }
            return true;
        }

        //protected void btnApprove_Click(object sender, EventArgs e)
        //{
        //    string result;
        //    try
        //    {
        //        if (ValidateData() == true)
        //        {

        //            string finYear = objcommon.GetFinalcialYear();
        //            string[] years = finYear.Split('.');
        //            OldYear = years[1];
        //            NewYear = years[0];

        //            if (Convert.ToString(Request.QueryString["id"]) != "" && Convert.ToString(Request.QueryString["id"]) != null)
        //            {
        //                result = objInv.UpdateLeaveStatus((string)Session["sCompID"], (string)Session["sEmpID"], drpStatus.SelectedValue, Convert.ToString(Request.QueryString["id"]), txtDate.Text, txtToDt.Text, drpReason.SelectedValue, txtAdd.Text, txtRem.Text, NewYear, drpLeave.SelectedValue, lblLeaves.Text);
        //            }
        //            else
        //            {
        //                result = objInv.UpdateLeave((string)Session["sCompID"], (string)Session["sEmpID"], txtDate.Text, txtToDt.Text, drpReason.SelectedValue, txtAdd.Text, txtRem.Text, drpStatus.SelectedValue, NewYear, drpLeave.SelectedValue, lblLeaves.Text);
        //            }
        //            if (result.ToString().Trim() == "success")
        //            {
        //                lblMessage.Text = "Successfuly sent leave application for approval.";
        //                if (drpStatus.SelectedItem.Text == "Submit")
        //                {
        //                    SENDUDATEMAIL();
        //                }
        //                else if (drpStatus.SelectedItem.Text == "Approve")
        //                {
        //                    dsInv = objNps.LeaveUpdatePreviousMonth((string)Session["sCompID"]);
        //                    APPROVEMAIL();
        //                }
        //                objcommon.Display("Validate", "DisplayErrorMessage('Successfuly sent leave application for approval.');");
        //                if (Convert.ToString(Request.QueryString["id"]) != "" && Convert.ToString(Request.QueryString["id"]) != null && (Convert.ToString(Request.QueryString["sender"]) == "" || Convert.ToString(Request.QueryString["sender"]) == null))
        //                {
        //                    Response.Redirect("LeaveApproval.aspx");
        //                }
        //                else
        //                {
        //                    Response.Redirect("LeaveHistory.aspx");
        //                }
        //            }
        //            else if (result.ToString().Trim() == "duplicate")
        //            {
        //                lblMessage.Text = "Application already exists for selected date.";
        //                return;
        //            }
        //            else
        //            {
        //                lblMessage.Text = "Error occurred in application.";
        //                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblMessage.Text = "Error occurred in application.";
        //        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
        //    }

        //}

        //private void SENDUDATEMAIL()
        //{
        //    emailSend = new ESS.Email();
        //    DataSet dsmkkMail = new DataSet();

        //    dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], (string)Session["sEmpID"]);

        //    //if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
        //    if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
        //    {
        //        //if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
        //        //{
        //        string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();

        //        string clientbodys = "Dear " + EMPNAME + ",<br><br>Your Leave Application is Submitted Successfully.<br>"
        //           + "Once the action taken will be notified to you through mail or same can be checked through ESS portal.<br><br>ThankYou.<br><br>";
        //        string emails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
        //        string subjects = "Do Not Reply: Leave Application";
        //        emailSend.SendEmailNPL(emails, subjects, clientbodys);


        //        string checkerbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>" + EMPNAME + 
        //            " Leave Application is received for approval. Kindly take the action for the same through logging-in into ESS portal." +
        //         "<br><br>ThankYou.<br>";
        //        //\nWith Best Regards,\nSequel Outsourcing PVT.LTD<br> < br >
        //        emails = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
        //        emailSend.SendEmailNPL(emails, subjects, checkerbodys);

        //        //}
        //    }

        //}

        //private void APPROVEMAIL()
        //{
        //    emailSend = new ESS.Email();
        //    DataSet dsmkkMail = new DataSet();
        //    string id = Convert.ToString(Request.QueryString["id"]);
        //    string Status = string.Empty;
        //    dsmkkMail = emailSend.GetEmpAttendanceaPPROVE((string)Session["sCompID"], (string)Session["sEmpID"], id);

        //    //if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
        //    if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
        //    {
        //        //if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
        //        //{
        //        if (drpStatus.SelectedItem.Text == "Approve")
        //        {
        //            Status = "Approved";

        //        }
        //        else if (drpStatus.SelectedItem.Text == "Reject")
        //        {
        //            Status = "Rejected";
        //        }
        //        string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();

        //        string clientbodys = "Dear " + EMPNAME + ",<br><br>Leave Application is " + Status + " Succussfully.<br>"
        //           + "<br><br>ThankYou.<br><br>";
        //        string emails = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
        //        string subjects = "Do Not Reply:Leave Application";
        //        emailSend.SendEmailNPL(emails, subjects, clientbodys);


        //        string checkerbodys = "Dear User,<br><br>" + EMPNAME + " has Reviewed your Leave Application.Your leave appplication is " + Status +
        //         "<br><br>ThankYou.<br>";
        //        //\nWith Best Regards,\nSequel Outsourcing PVT.LTD<br> < br >
        //        emails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
        //        emailSend.SendEmailNPL(emails, subjects, checkerbodys);



        //        //}
        //    }

        //}

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                if (ValidateData() == true)
                {


                    string finYear = objcommon.GetFinalcialYear();
                    string[] years = finYear.Split('.');
                    OldYear = years[1];
                    NewYear = years[0];

                    //if ((Convert.ToInt16(lblLeaves.Text) >= 3) && (drpLeave.SelectedItem.ToString() == "SL"))
                    if ((Convert.ToDecimal(lblLeaves.Text) >= 3) && (drpLeave.SelectedItem.ToString() == "SL"))
                    {
                        if (fupldDocument.PostedFile.FileName == "")
                        {
                            divAlert.Visible = true;
                            lblMessage.Text = "Browse document";
                            objcommon.Display("Validate", "DisplayErrorMessage('First Browse document Type Then Click Submit Button.');");

                            return;
                        }
                    }

                    if (Convert.ToString(Request.QueryString["id"]) != "" && Convert.ToString(Request.QueryString["id"]) != null)
                    {
                        result = objInv.UpdateLeaveStatus((string)Session["sCompID"], (string)Session["sEmpID"], drpStatus.SelectedValue, Convert.ToString(Request.QueryString["id"]), txtDate.Text, txtToDt.Text, drpReason.SelectedValue, txtAdd.Text, txtRem.Text, NewYear, drpLeave.SelectedValue, lblLeaves.Text);
                    }
                    else
                    {
                        result = objInv.UpdateLeave((string)Session["sCompID"], (string)Session["sEmpID"], txtDate.Text, txtToDt.Text, drpReason.SelectedValue, txtAdd.Text, txtRem.Text, drpStatus.SelectedValue, NewYear, drpLeave.SelectedValue, lblLeaves.Text);

                    }
                    if (result.ToString().Trim() == "success")
                    {
                        divAlertSucc.Visible = true;
                        lblMessageSucc.Text = "Successfuly sent leave application for approval.";
                        dsInv = objInv.GetId((string)Session["sCompID"], (string)Session["sEmpID"], txtDate.Text, txtToDt.Text, drpReason.SelectedValue, txtAdd.Text, txtRem.Text, drpStatus.SelectedValue, NewYear, drpLeave.SelectedValue, lblLeaves.Text);
                        if (dsInv.Tables[0].Rows[0]["CID"].ToString() != "")
                        {
                            string CID = dsInv.Tables[0].Rows[0]["CID"].ToString();
                            UploadFile(CID);
                            DisplayDocuments(CID);
                        }


                        if (drpStatus.SelectedItem.Text == "Submit")
                        {
                            SENDUDATEMAIL(txtDate.Text, txtToDt.Text, drpLeave.SelectedItem.Text);

                        }
                        else if (drpStatus.SelectedItem.Text == "Approve")
                        {
                            dsInv = objNps.LeaveUpdatePreviousMonth((string)Session["sCompID"]);
                            APPROVEMAIL(txtDate.Text, txtToDt.Text, drpLeave.SelectedItem.Text, lblcrdate.Text);
                        }
                        objcommon.Display("Validate", "DisplayErrorMessage('Successfuly sent leave application for approval.');");
                        if (Convert.ToString(Request.QueryString["id"]) != "" && Convert.ToString(Request.QueryString["id"]) != null && (Convert.ToString(Request.QueryString["sender"]) == "" || Convert.ToString(Request.QueryString["sender"]) == null))
                        {
                            Response.Redirect("LeaveApproval.aspx");
                        }
                        else
                        {
                            Response.Redirect("LeaveHistory.aspx");
                        }
                    }
                    else if (result.ToString().Trim() == "duplicate")
                    {
                        divAlert.Visible = true;
                        lblMessage.Text = "Application already exists for selected date.";
                        return;
                    }
                    else
                    {
                        divAlert.Visible = true;
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                    }
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
            }

        }

        private void UploadFile(string Id)
        {
            try
            {
                //if (fupldDocument.PostedFile.FileName == "")
                //{

                //    lblMessage.Text = "Browse document";
                //    objcommon.Display("Validate", "DisplayErrorMessage('First Browse document Type Then Click Submit Button.');");

                //    return;
                //}
                if (System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".PDF" ||
                    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".JPG" ||
                    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".JPEG" ||
                    System.IO.Path.GetExtension(fupldDocument.PostedFile.FileName).ToUpper() == ".ZIP"
                    )
                {

                }
                else
                {
                    lblMessage.Text = "Only PDF,JPG/JPEG,ZIP and RAR files allowed.";
                    return;
                }

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) +
                            Convert.ToString(Session["sCompID"]) + "\\NPL\\Documents\\Medical_Certificate\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\" + Id + "\\";

                System.IO.Stream fileInputStream = fupldDocument.PostedFile.InputStream;
                Byte[] fileContent = new Byte[fileInputStream.Length];
                fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
                fileInputStream.Close();
                /* Create Folder */

                if (System.IO.Directory.Exists(@savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(@savePath);
                }

                DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
                string fileName = Path.GetFileNameWithoutExtension(fupldDocument.FileName.Trim().Replace(" ", "_")) + "_"
                    + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocument.FileName.Trim());

                //if (Directory.EnumerateFiles(@savePath).Count() == 0)
                //{

                string filesToDelete = Id;
                string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
                foreach (string file in fileList)
                {
                    System.IO.File.Delete(file);
                }
                System.IO.FileStream fStream = new System.IO.FileStream(@savePath + Id + "_" + fileName, System.IO.FileMode.Create);
                System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
                bWriter.Write(fileContent);
                fStream.Flush();
                bWriter.Flush();
                fStream.Close();
                bWriter.Close();



                lblMessage.Text = "Submitted Successfully";
                objcommon.Display("Validate", "DisplayErrorMessage('Submitted Successfully.');");


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }

        private void DisplayDocuments(string Id)
        {
            try
            {
                CreateDocumentsStructure();

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\NPL\\Documents\\Medical_Certificate\\" + Convert.ToString(Session["sEmpCode"]).ToUpper() + "\\" + Id + "\\";

                if (System.IO.Directory.Exists(@savePath) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }

                    this.gvViewDocDetails.DataSource = dtDocInfo;
                    this.gvViewDocDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void DisplayDocumentsChk(string empCode, string Id)
        {
            try
            {
                CreateDocumentsStructure();

                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\NPL\\Documents\\Medical_Certificate\\" + empCode.ToUpper() + "\\" + Id + "\\";

                if (System.IO.Directory.Exists(@savePath) == true)
                {
                    System.IO.DirectoryInfo dirInfo = new DirectoryInfo(savePath);
                    System.IO.FileInfo[] fileNames = dirInfo.GetFiles("*.*");

                    DataTable dtDocInfo = ((DataTable)ViewState["Documents"]);

                    foreach (System.IO.FileInfo fi in fileNames)
                    {
                        DataRow drDocRow = dtDocInfo.NewRow();
                        drDocRow["FILEPATH"] = fi.FullName;
                        drDocRow["FILENAME"] = fi.Name;
                        dtDocInfo.Rows.Add(drDocRow);
                    }

                    this.gvViewDocDetails.DataSource = dtDocInfo;
                    this.gvViewDocDetails.DataBind();
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

                ViewState["Documents"] = dtDocuments;
                gvViewDocDetails.DataSource = dtDocuments;
                gvViewDocDetails.DataBind();
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
                    Response.AppendHeader("content-disposition", @"attachment; filename=" + lnkBtnOpenFile.Text);
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

        private void SENDUDATEMAIL(string frDate, string toDate, string leaveType)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DateTime date = DateTime.Now;
            dsmkkMail = emailSend.GetEmpAttendanceRecDe((string)Session["sCompID"], (string)Session["sEmpID"]);
            DataSet ds = new DataSet();
            ds = emailSend.getName((string)Session["sCompID"], leaveType);
            if (ds.Tables.Count > 0)
            {
                leaveType = ds.Tables[0].Rows[0]["LEAVE_NAME"].ToString();
            }
            //if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            {
                if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
                {
                    string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();


                    string clientbodys = "Dear " + EMPNAME + ",<br><br>Your Leave Application is Submitted Successfully.<br>"
                       + "Once the action taken will be notified to you through mail or same can be checked through ESS portal."
                       + " <br> Leave Type: -" + leaveType
                       + "  <br> Applied Date :-" + date
                             + "<br> From Date :- " + frDate
                             + "<br> To Date :- " + toDate
                             + "<br><br>ThankYou.<br><br>";
                    string emails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
                    string subjects = "Do Not Reply: Leave Application";
                    emailSend.SendEmailNPL(emails, subjects, clientbodys);

                    string Newemails = "";

                    string checkerbodys = "Dear " + dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString() + ",<br><br>" + EMPNAME +
                        " Leave Application is received for approval. Kindly take the action for the same through logging-in into ESS portal."
                      + " <br> Leave Type: -" + leaveType
                      + "  <br> Applied Date :-" + date
                             + "<br> From Date :- " + frDate
                             + "<br> To Date :- " + toDate
                             + "<br><br>ThankYou.<br><br>";
                    //\nWith Best Regards,\nSequel Outsourcing PVT.LTD<br> < br >
                    Newemails = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
                    emailSend.SendEmailNPL(Newemails, subjects, checkerbodys);

                }
            }

        }

        private void APPROVEMAIL(string frDate, string toDate, string leaveType, string crDate)
        {
            emailSend = new NewPortal2023.ESS.Email();
            DataSet dsmkkMail = new DataSet();
            DataSet ds = new DataSet();
            string id = Convert.ToString(Request.QueryString["id"]);
            string Status = string.Empty;
            dsmkkMail = emailSend.GetEmpAttendanceLeaveaPPROVE((string)Session["sCompID"], (string)Session["sEmpID"], id);
            ds = emailSend.getName((string)Session["sCompID"], leaveType);
            if (ds.Tables.Count > 0)
            {
                leaveType = ds.Tables[0].Rows[0]["LEAVE_NAME"].ToString();
            }
            //if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            if (dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString() != "")
            {
                if (dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString() != "")
                {
                    if (drpStatus.SelectedItem.Text == "Approve")
                    {
                        Status = "Approved";

                    }
                    else if (drpStatus.SelectedItem.Text == "Reject")
                    {
                        Status = "Rejected";
                    }
                    string EMPNAME = dsmkkMail.Tables[0].Rows[0]["EMP_NAME"].ToString();
                    string UserName = dsmkkMail.Tables[1].Rows[0]["EMP_NAME"].ToString();

                    string clientbodys = "Dear " + EMPNAME + ",<br><br>" + UserName + " Leave Application is " + Status + " Succussfully.<br>"
                    + " <br> Leave Type: -" + leaveType
                    + "  <br> Applied Date :-" + crDate
                             + "<br> From Date :- " + frDate
                             + "<br> To Date :- " + toDate
                             + "<br><br>ThankYou.<br><br>";
                    string emails = dsmkkMail.Tables[1].Rows[0]["CHECKERMAIL"].ToString();
                    string subjects = "Do Not Reply:Leave Application";
                    emailSend.SendEmailNPL(emails, subjects, clientbodys);

                    string Newemails = "";
                    string checkerbodys = "Dear " + UserName + ",<br><br>" + EMPNAME + " has Reviewed your Leave Application.Your leave appplication is " + Status
                   + " <br> Leave Type: -" + leaveType
                    + "  <br> Applied Date :-" + crDate
                             + "<br> From Date :- " + frDate
                             + "<br> To Date :- " + toDate
                             + "<br><br>ThankYou.<br><br>";
                    //\nWith Best Regards,\nSequel Outsourcing PVT.LTD<br> < br >
                    Newemails = dsmkkMail.Tables[0].Rows[0]["EMP_EMAIL"].ToString();
                    emailSend.SendEmailNPL(Newemails, subjects, checkerbodys);



                }
            }

        }

        protected void btnDraft_Click(object sender, EventArgs e)
        {
            string result;
            try
            {
                if (ValidateData() == true)
                {

                    string finYear = objcommon.GetFinalcialYear();
                    string[] years = finYear.Split('.');
                    OldYear = years[1];
                    NewYear = years[0];

                    result = objInv.UpdateLeave((string)Session["sCompID"], (string)Session["sEmpID"], txtDate.Text, txtToDt.Text, drpReason.SelectedValue, txtAdd.Text, txtRem.Text, "0", NewYear, drpLeave.SelectedValue, lblLeaves.Text);
                    if (result.ToString().Trim() == "success")
                    {
                        lblMessage.Text = "Successfuly saved application.";
                        objcommon.Display("Validate", "DisplayErrorMessage('Successfuly saved application.');");
                        Response.Redirect("LeaveHistory.aspx");
                    }
                    else
                    {
                        lblMessage.Text = "Error occurred in application.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
                    }
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Problem occured while updating investment details.');");
            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            divAlert.Visible = false;
            lblMessage.Text = "";
            
            DateTime frDate = Convert.ToDateTime(txtDate.Text.Trim()).Date;
            if (txtToDt.Text != "")
            {
                DateTime toDate = Convert.ToDateTime(txtToDt.Text.Trim()).Date;
                if (frDate <= toDate)
                {
                    SetDays();
                    checkleavebalance();
                    btnApprove.Visible = true;
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Text = "";
                    lblMessage.Text = "To Date " + txtToDt.Text + " should not to be less than From Date . " +txtDate.Text;
                    FileUpload.Visible = false;
                    lblUpload.Visible = false;
                    uploadedDoc.Visible = false;
                    btnApprove.Visible = false;
                    
                }
            }



        }

        protected void txtToDt_TextChanged(object sender, EventArgs e)
        {
            //SetDays();
            //checkleavebalance();
            divAlert.Visible = false;
            lblMessage.Text = "";
            DateTime frDate = Convert.ToDateTime(txtDate.Text.Trim()).Date;
            DateTime toDate = Convert.ToDateTime(txtToDt.Text.Trim()).Date;
            if (frDate <= toDate)
            {
                SetDays();
                checkleavebalance();
                //btnApprove.Visible = true; // DNYANESHWAR ADDED THIS
            }
            else
            {
                divAlert.Visible = true;
                lblMessage.Text = "";
                lblMessage.Text = "To Date " + txtToDt.Text + " should not to be less than From Date. " + txtDate.Text;
                FileUpload.Visible = false;
                lblUpload.Visible = false;
                uploadedDoc.Visible = false;
                btnApprove.Visible = false;
               
            }

        }

        protected void drpLeave_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((txtDate.Text != "") && (txtToDt.Text != ""))
            {
                DateTime frDate = Convert.ToDateTime(txtDate.Text.Trim()).Date;
                DateTime toDate = Convert.ToDateTime(txtToDt.Text.Trim()).Date;
                if (frDate <= toDate)
                {
                    ViewState["LEAVECODE"] = drpLeave.SelectedValue;
                    checkleavebalance(); //btnApprove.Visible = true;
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Text = "";
                    lblMessage.Text = "To Date " + txtToDt.Text + " should not to be less than From Date. " + txtDate.Text;
                    FileUpload.Visible = false;
                    lblUpload.Visible = false;
                    uploadedDoc.Visible = false;
                    btnApprove.Visible = false;

                }

                if ((drpReason.SelectedItem.ToString() == "First half") || (drpReason.SelectedItem.ToString() == "Second half"))
                {
                    drpReason.SelectedIndex = 0;
                    SetDays();
                }
            }

            //string Leave_Code = drpLeave.SelectedItem.Text;
            //string frmDate = txtDate.Text;
            //string toDate = txtToDt.Text;
            //string leaveDays = lblLeaves.Text;

            //lblMessage.Text = "";
            ////ds = emailSend.getName((string)Session["sCompID"], leaveType);
            //dsInv = objNps.getLeaveCount((string)Session["sCompID"], (string)Session["sEmpID"], drpLeave.SelectedItem.Text);

            //string EMP_CODE = Convert.ToString(dsInv.Tables[1].Rows[0]["EMP_MID"]);
            //double closingBal = Convert.ToDouble(dsInv.Tables[0].Rows[0]["Closing_bal"]);


            //try
            //{
            //    //if ((Convert.ToInt16(lblLeaves.Text) >= 3) && (drpLeave.SelectedItem.ToString() == "SL"))
            //    //{
            //    //    FileUpload.Visible = true;
            //    //    lblUpload.Visible = true;
            //    //    uploadedDoc.Visible = true;

            //    //}
            //    //else
            //    //{
            //    //    FileUpload.Visible = false;
            //    //    lblUpload.Visible = false;
            //    //    uploadedDoc.Visible = false;
            //    //}

            //    //if ((Convert.ToInt16(lblLeaves.Text) >= 5) && (drpLeave.SelectedItem.ToString() == "PL" || drpLeave.SelectedItem.ToString() == "TI"))
            //    //{
            //    //    if (closingBal >= Convert.ToDouble(leaveDays))
            //    //    {
            //    //        //FileUpload.Visible = true;
            //    //        lblUpload.Visible = true;
            //    //        uploadedDoc.Visible = true;
            //    //    }

            //    //    else
            //    //    {
            //    //        lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
            //    //        FileUpload.Visible = false;
            //    //        lblUpload.Visible = false;
            //    //        uploadedDoc.Visible = false;
            //    //        btnApprove.Visible = false;
            //    //    }
            //    //}
            //    //else
            //    //{
            //    //    lblMessage.Text = "PL and TI Minimum day should be 5 not Less then ";
            //    //    FileUpload.Visible = false;
            //    //    lblUpload.Visible = false;
            //    //    uploadedDoc.Visible = false;
            //    //    btnApprove.Visible = false;
            //    //}

            //    ////if (closingBal != Convert.ToDouble(0) && (drpLeave.SelectedItem.ToString() != "") && (drpLeave.SelectedItem.ToString() != "SL"))
            //    //if (closingBal != Convert.ToDouble(0) && (drpLeave.SelectedItem.ToString() != ""))
            //    //{
            //    //    if (closingBal >= Convert.ToDouble(leaveDays))
            //    //    {
            //    //        btnApprove.Visible = true;
            //    //    }

            //    //    else
            //    //    {
            //    //        lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
            //    //        FileUpload.Visible = false;
            //    //        lblUpload.Visible = false;
            //    //        uploadedDoc.Visible = false;
            //    //        btnApprove.Visible = false;
            //    //    }

            //    //}



            //    //else
            //    //{
            //    //    lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";

            //    //    btnApprove.Visible = false;
            //    //}
            //    if ((Convert.ToInt16(lblLeaves.Text) <= 2) && (drpLeave.SelectedItem.ToString() == "SL"))
            //    {
            //        if (closingBal >= Convert.ToDouble(leaveDays))
            //        {
            //            //FileUpload.Visible = true;
            //            //lblUpload.Visible = true;
            //            uploadedDoc.Visible = true;
            //            btnApprove.Visible = true;
            //            lblMessage.Text = "";
            //        }

            //        else
            //        {
            //            lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
            //            FileUpload.Visible = false;
            //            lblUpload.Visible = false;
            //            uploadedDoc.Visible = false;
            //            btnApprove.Visible = false;
            //        }

            //    }

            //    else if (closingBal != Convert.ToDouble(0) && (drpLeave.SelectedItem.ToString() != "") && (drpLeave.SelectedItem.ToString() != "SL"))
            //    {
            //        if ((Convert.ToInt16(lblLeaves.Text) >= 5) && (drpLeave.SelectedItem.ToString() == "PL" || drpLeave.SelectedItem.ToString() == "TI") &&  (Convert.ToString(EMP_CODE)!= "GET" || Convert.ToString(EMP_CODE) != "CON"))
            //        {
            //            if (closingBal >= Convert.ToDouble(leaveDays))
            //            {
            //                //FileUpload.Visible = true;
            //                //lblUpload.Visible = true;
            //                uploadedDoc.Visible = true;
            //                btnApprove.Visible = true;
            //                lblMessage.Text = "";
            //            }

            //            else
            //            {
            //                lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
            //                FileUpload.Visible = false;
            //                lblUpload.Visible = false;
            //                uploadedDoc.Visible = false;
            //                btnApprove.Visible = false;
            //            }

            //        }
            //        else if ((drpLeave.SelectedItem.ToString() == "PL") && (Convert.ToString(EMP_CODE) == "GET" || Convert.ToString(EMP_CODE) == "CON") )
            //        {
            //            if (closingBal >= Convert.ToDouble(leaveDays))
            //            {
            //                //FileUpload.Visible = true;
            //                //lblUpload.Visible = true;
            //                uploadedDoc.Visible = true;
            //                btnApprove.Visible = true;
            //                lblMessage.Text = "";
            //            }

            //            else
            //            {
            //                lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
            //                FileUpload.Visible = false;
            //                lblUpload.Visible = false;
            //                uploadedDoc.Visible = false;
            //                btnApprove.Visible = false;
            //            }

            //        }

            //        else if ((Convert.ToInt16(lblLeaves.Text) <= 4) && (drpLeave.SelectedItem.ToString() == "CL"))
            //        {
            //            if (closingBal >= Convert.ToDouble(leaveDays))
            //            {
            //                //FileUpload.Visible = true;
            //                //lblUpload.Visible = true;
            //                uploadedDoc.Visible = true;
            //                btnApprove.Visible = true;
            //                lblMessage.Text = "";
            //            }

            //            else
            //            {
            //                lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
            //                FileUpload.Visible = false;
            //                lblUpload.Visible = false;
            //                uploadedDoc.Visible = false;
            //                btnApprove.Visible = false;
            //            }

            //        }
            //        else if (drpLeave.SelectedItem.ToString() != "PL" && drpLeave.SelectedItem.ToString() != "TI" && drpLeave.SelectedItem.ToString() != "CL")
            //        {
            //            if (closingBal >= Convert.ToDouble(leaveDays))
            //            {
            //                //FileUpload.Visible = true;
            //                //lblUpload.Visible = true;
            //                uploadedDoc.Visible = true;
            //                btnApprove.Visible = true;
            //                lblMessage.Text = "";
            //            }

            //            else
            //            {
            //                lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
            //                FileUpload.Visible = false;
            //                lblUpload.Visible = false;
            //                uploadedDoc.Visible = false;
            //                btnApprove.Visible = false;
            //            }

            //        }               
            //        else
            //        {
            //            if(drpLeave.SelectedItem.ToString() == "CL")
            //            {
            //                lblMessage.Text = "The maximum period to apply for CL should be 4 days";
            //            }
            //            else if (drpLeave.SelectedItem.ToString() == "PL" || drpLeave.SelectedItem.ToString() == "TI")
            //            {
            //                lblMessage.Text = "The minimum period to apply for PL or TI should be 5 days.";
            //            }


            //            FileUpload.Visible = false;
            //            lblUpload.Visible = false;
            //            uploadedDoc.Visible = false;
            //            btnApprove.Visible = false;

            //        }


            //    }

            //    else
            //    {
            //        if (drpLeave.SelectedItem.ToString() == "SL")
            //        {
            //            lblMessage.Text = "Kindly upload medical Certificate";
            //            FileUpload.Visible = true;
            //            lblUpload.Visible = true;
            //            uploadedDoc.Visible = true;
            //            btnApprove.Visible = true;
            //        }
            //        else
            //        {
            //            lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
            //            FileUpload.Visible = false;
            //            lblUpload.Visible = false;
            //            uploadedDoc.Visible = false;
            //            btnApprove.Visible = false;
            //        }

            //    }





            //}
            //catch (Exception ex)
            //{
            //    lblMessage.Text = ex.Message;
            //}
        }

        //protected void btnAddNew_Click(object sender, EventArgs e)
        //{
        //    AppList.Visible = false;
        //    AppForm.Visible = true;
        //    btnAddNew.Visible = false;

        //}

        //protected void drpReason_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    dsInv = objNps.getLeaveCount((string)Session["sCompID"], (string)Session["sEmpID"], drpLeave.SelectedItem.Text);
        //    double closingBal = Convert.ToDouble(dsInv.Tables[0].Rows[0]["Closing_bal"]);
        //    if ((drpReason.SelectedItem.ToString() == "First half") || (drpReason.SelectedItem.ToString() == "Second half"))
        //    {
        //        if (closingBal >= 0.5)
        //        {
        //            //lblLeaves.Text = "0.5";
        //            btnApprove.Visible = true;
        //            lblMessage.Text = "";

        //        }
        //        else
        //        {
        //            lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
        //            btnApprove.Visible = false;

        //        }

        //    }
        //    else
        //    {

        //    }

        //}

        protected void drpReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drpLeave.SelectedValue == "0")
            {
                lblMessage.Text = "Select first leave type";
                divAlert.Visible = true;
                drpReason.SelectedValue = "0";
                return;
            }
            dsInv = objNps.getLeaveCount((string)Session["sCompID"], (string)Session["sEmpID"], drpLeave.SelectedItem.Text);
            dsInv = objNps.getLeaveCount((string)Session["sCompID"], (string)Session["sEmpID"], drpLeave.SelectedItem.Text);
            string EMP_CODE = Convert.ToString(dsInv.Tables[1].Rows[0]["EMP_MID"]);
            double closingBal = Convert.ToDouble(dsInv.Tables[0].Rows[0]["Closing_bal"]);

            if ((drpReason.SelectedItem.ToString() == "First half") || (drpReason.SelectedItem.ToString() == "Second half"))
            {
                if ((drpLeave.SelectedItem.Text == "PL" && (EMP_CODE == "GET" || EMP_CODE == "CON")) || drpLeave.SelectedItem.Text == "SL" || drpLeave.SelectedItem.Text == "CL")
                {
                    if (closingBal >= 0.5)
                    {
                        lblLeaves.Text = "0.5";
                        btnApprove.Visible = true;
                        divAlert.Visible = false;
                    }
                    else
                    {
                        lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
                        btnApprove.Visible = false;
                        return;
                    }
                }
                else
                {
                    SetDays();
                    dsInv = objNps.getLeaveCount((string)Session["sCompID"], (string)Session["sEmpID"], drpLeave.SelectedItem.Text);
                    closingBal = Convert.ToDouble(dsInv.Tables[0].Rows[0]["Closing_bal"]);

                    if (closingBal <= Convert.ToDouble(lblLeaves.Text))
                    {
                        btnApprove.Visible = false;
                    }
                }
            }
            else
            {
                SetDays();
                dsInv = objNps.getLeaveCount((string)Session["sCompID"], (string)Session["sEmpID"], drpLeave.SelectedItem.Text);
                closingBal = Convert.ToDouble(dsInv.Tables[0].Rows[0]["Closing_bal"]);

                if (closingBal <= Convert.ToDouble(lblLeaves.Text))
                {
                    btnApprove.Visible = false;
                }

            }

        }

        //protected void checkleavebalance()
        //{


        //    string Leave_Code = drpLeave.SelectedItem.Text;
        //    string frmDate = txtDate.Text;
        //    string toDate = txtToDt.Text;
        //    if( (frmDate != "") && (toDate != "") && (Leave_Code!= "[Select One]"))
        //    {
        //        string leaveDays = lblLeaves.Text;
        //        divAlert.Visible = true;
        //        lblMessage.Text = "";

        //        //ds = emailSend.getName((string)Session["sCompID"], leaveType);
        //        dsInv = objNps.getLeaveCount((string)Session["sCompID"], (string)Session["sEmpID"], drpLeave.SelectedItem.Text);
        //        if (dsInv.Tables[0].Rows.Count > 0)
        //        {
        //            string EMP_CODE = Convert.ToString(dsInv.Tables[1].Rows[0]["EMP_MID"]);
        //            double closingBal = Convert.ToDouble(dsInv.Tables[0].Rows[0]["Closing_bal"]);


        //            try
        //            {
        //                //if ((Convert.ToInt16(lblLeaves.Text) >= 3) && (drpLeave.SelectedItem.ToString() == "SL"))
        //                //{
        //                //    FileUpload.Visible = true;
        //                //    lblUpload.Visible = true;
        //                //    uploadedDoc.Visible = true;

        //                //}
        //                //else
        //                //{
        //                //    FileUpload.Visible = false;
        //                //    lblUpload.Visible = false;
        //                //    uploadedDoc.Visible = false;
        //                //}

        //                //if ((Convert.ToInt16(lblLeaves.Text) >= 5) && (drpLeave.SelectedItem.ToString() == "PL" || drpLeave.SelectedItem.ToString() == "TI"))
        //                //{
        //                //    if (closingBal >= Convert.ToDouble(leaveDays))
        //                //    {
        //                //        //FileUpload.Visible = true;
        //                //        lblUpload.Visible = true;
        //                //        uploadedDoc.Visible = true;
        //                //    }

        //                //    else
        //                //    {
        //                //        lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
        //                //        FileUpload.Visible = false;
        //                //        lblUpload.Visible = false;
        //                //        uploadedDoc.Visible = false;
        //                //        btnApprove.Visible = false;
        //                //    }
        //                //}
        //                //else
        //                //{
        //                //    lblMessage.Text = "PL and TI Minimum day should be 5 not Less then ";
        //                //    FileUpload.Visible = false;
        //                //    lblUpload.Visible = false;
        //                //    uploadedDoc.Visible = false;
        //                //    btnApprove.Visible = false;
        //                //}

        //                ////if (closingBal != Convert.ToDouble(0) && (drpLeave.SelectedItem.ToString() != "") && (drpLeave.SelectedItem.ToString() != "SL"))
        //                //if (closingBal != Convert.ToDouble(0) && (drpLeave.SelectedItem.ToString() != ""))
        //                //{
        //                //    if (closingBal >= Convert.ToDouble(leaveDays))
        //                //    {
        //                //        btnApprove.Visible = true;
        //                //    }

        //                //    else
        //                //    {
        //                //        lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
        //                //        FileUpload.Visible = false;
        //                //        lblUpload.Visible = false;
        //                //        uploadedDoc.Visible = false;
        //                //        btnApprove.Visible = false;
        //                //    }

        //                //}



        //                //else
        //                //{
        //                //    lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";

        //                //    btnApprove.Visible = false;
        //                //}
        //                if ((Convert.ToInt16(lblLeaves.Text) <= 2) && (drpLeave.SelectedItem.ToString() == "SL"))
        //                {
        //                    if (closingBal >= Convert.ToDouble(leaveDays))
        //                    {
        //                        //FileUpload.Visible = true;
        //                        //lblUpload.Visible = true;
        //                        uploadedDoc.Visible = true;
        //                        btnApprove.Visible = true;
        //                        divAlert.Visible = false;
        //                        lblMessage.Text = "";
        //                    }

        //                    else
        //                    {
        //                        lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
        //                        FileUpload.Visible = false;
        //                        lblUpload.Visible = false;
        //                        uploadedDoc.Visible = false;
        //                        btnApprove.Visible = false;
        //                    }

        //                }

        //                else if (closingBal != Convert.ToDouble(0) && (drpLeave.SelectedItem.ToString() != "") && (drpLeave.SelectedItem.ToString() != "SL"))
        //                {
        //                    if ((Convert.ToInt16(lblLeaves.Text) >= 5) && (drpLeave.SelectedItem.ToString() == "PL" || drpLeave.SelectedItem.ToString() == "TI") && (Convert.ToString(EMP_CODE) != "GET" || Convert.ToString(EMP_CODE) != "CON"))
        //                    {
        //                        if (closingBal >= Convert.ToDouble(leaveDays))
        //                        {
        //                            //FileUpload.Visible = true;
        //                            //lblUpload.Visible = true;
        //                            uploadedDoc.Visible = true;
        //                            btnApprove.Visible = true;
        //                            divAlert.Visible = false;
        //                            lblMessage.Text = "";
        //                        }

        //                        else
        //                        {
        //                            lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
        //                            FileUpload.Visible = false;
        //                            lblUpload.Visible = false;
        //                            uploadedDoc.Visible = false;
        //                            btnApprove.Visible = false;
        //                        }

        //                    }
        //                    else if ((drpLeave.SelectedItem.ToString() == "PL") && (Convert.ToString(EMP_CODE) == "GET" || Convert.ToString(EMP_CODE) == "CON"))
        //                    {
        //                        if (closingBal >= Convert.ToDouble(leaveDays))
        //                        {
        //                            //FileUpload.Visible = true;
        //                            //lblUpload.Visible = true;
        //                            uploadedDoc.Visible = true;
        //                            btnApprove.Visible = true;
        //                            divAlert.Visible = false;
        //                            lblMessage.Text = "";
        //                        }

        //                        else
        //                        {
        //                            lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
        //                            FileUpload.Visible = false;
        //                            lblUpload.Visible = false;
        //                            uploadedDoc.Visible = false;
        //                            btnApprove.Visible = false;
        //                        }

        //                    }

        //                    else if ((Convert.ToInt16(lblLeaves.Text) <= 4) && (drpLeave.SelectedItem.ToString() == "CL"))
        //                    {
        //                        if (closingBal >= Convert.ToDouble(leaveDays))
        //                        {
        //                            //FileUpload.Visible = true;
        //                            //lblUpload.Visible = true;
        //                            uploadedDoc.Visible = true;
        //                            btnApprove.Visible = true;
        //                            divAlert.Visible = false;
        //                            lblMessage.Text = "";
        //                        }

        //                        else
        //                        {

        //                            lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
        //                            FileUpload.Visible = false;
        //                            lblUpload.Visible = false;
        //                            uploadedDoc.Visible = false;
        //                            btnApprove.Visible = false;
        //                        }

        //                    }
        //                    else if (drpLeave.SelectedItem.ToString() != "PL" && drpLeave.SelectedItem.ToString() != "TI" && drpLeave.SelectedItem.ToString() != "CL")
        //                    {
        //                        if (closingBal >= Convert.ToDouble(leaveDays))
        //                        {
        //                            //FileUpload.Visible = true;
        //                            //lblUpload.Visible = true;
        //                            uploadedDoc.Visible = true;
        //                            btnApprove.Visible = true;
        //                            divAlert.Visible = false;
        //                            lblMessage.Text = "";
        //                        }

        //                        else
        //                        {
        //                            lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
        //                            FileUpload.Visible = false;
        //                            lblUpload.Visible = false;
        //                            uploadedDoc.Visible = false;
        //                            btnApprove.Visible = false;
        //                        }

        //                    }
        //                    else if (drpLeave.SelectedItem.ToString() == "CO")
        //                    {
        //                        if (closingBal >= Convert.ToDouble(leaveDays))
        //                        {
        //                            //FileUpload.Visible = true;
        //                            //lblUpload.Visible = true;
        //                            uploadedDoc.Visible = false;
        //                            btnApprove.Visible = true;
        //                            divAlert.Visible = false;
        //                            lblMessage.Text = "";
        //                        }

        //                        else
        //                        {
        //                            lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
        //                            FileUpload.Visible = false;
        //                            lblUpload.Visible = false;
        //                            uploadedDoc.Visible = false;
        //                            btnApprove.Visible = false;
        //                        }

        //                    }
        //                    else
        //                    {
        //                        if (drpLeave.SelectedItem.ToString() == "CL")
        //                        {
        //                            lblMessage.Text = "The maximum period to apply for CL should be 4 days";
        //                        }
        //                        else if (drpLeave.SelectedItem.ToString() == "PL" || drpLeave.SelectedItem.ToString() == "TI")
        //                        {
        //                            lblMessage.Text = "The minimum period to apply for PL or TI should be 5 days.";
        //                        }


        //                        FileUpload.Visible = false;
        //                        lblUpload.Visible = false;
        //                        uploadedDoc.Visible = false;
        //                        btnApprove.Visible = false;

        //                    }


        //                }

        //                else
        //                {
        //                    if (drpLeave.SelectedItem.ToString() == "SL")
        //                    {
        //                        lblMessage.Text = "Kindly upload medical Certificate";
        //                        FileUpload.Visible = true;
        //                        lblUpload.Visible = true;
        //                        uploadedDoc.Visible = true;
        //                        btnApprove.Visible = true;
        //                    }
        //                    else
        //                    {
        //                        lblMessage.Text = "You Have" + " " + closingBal + " " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
        //                        FileUpload.Visible = false;
        //                        lblUpload.Visible = false;
        //                        uploadedDoc.Visible = false;
        //                        btnApprove.Visible = false;
        //                    }

        //                }





        //            }
        //            catch (Exception ex)
        //            {
        //                divAlert.Visible = true;
        //                lblMessage.Text = ex.Message;
        //            }

        //        }
        //        else
        //        {
        //            divAlert.Visible = true;
        //            lblMessage.Text = "You Have" + " 0 " + drpLeave.SelectedItem.Text + " " + "Leave Balance Left";
        //        }
        //    }

        //}

        protected void checkleavebalance()
        {
            string Leave_Code = drpLeave.SelectedItem.Text;
            string frmDate = txtDate.Text;
            string toDate = txtToDt.Text;

            if ((frmDate != "") && (toDate != "") && (Leave_Code != "[Select One]"))
            {
                string leaveDays = lblLeaves.Text;
                divAlert.Visible = true;
                lblMessage.Text = "";

                dsInv = objNps.getLeaveCount((string)Session["sCompID"], (string)Session["sEmpID"], Leave_Code);
                //dsleave = objNps.checkLeaveBal((string)Session["sCompID"], (string)Session["sEmpCode"], Leave_Code, ViewState["LEAVECODE"].ToString());
                dsleave = objNps.checkLeaveBal((string)Session["sCompID"], (string)Session["sEmpCode"], Leave_Code);
                string result = Convert.ToString(dsleave.Tables[0].Rows[0]["result"]);

                if (dsInv.Tables[0].Rows.Count > 0)
                {
                    string EMP_CODE = Convert.ToString(dsInv.Tables[1].Rows[0]["EMP_MID"]);
                    double closingBal = Convert.ToDouble(dsInv.Tables[0].Rows[0]["Closing_bal"]);

                    try
                    {
                        DateTime startDate = DateTime.Parse(frmDate);
                        DateTime endDate = DateTime.Parse(toDate);
                        int totalLeaveDays = 0;



                        // Calculate total leave days, skipping weekends only for "PL"
                        if (Leave_Code == "PL" || Leave_Code == "TI" || Leave_Code == "SL" || Leave_Code == "CL")
                        {
                            if ((Session["sEmpType"].ToString() == "E0000023" || Session["sEmpType"].ToString() == "E0000024" || Session["sEmpType"].ToString() == "E0000025" ) && 
                                (Session["sEmpCode"].ToString() != "NP140" &&
                                 Session["sEmpCode"].ToString() != "NP155" &&
                                 Session["sEmpCode"].ToString() != "NP177" &&
                                 Session["sEmpCode"].ToString() != "NP188" &&
                                 Session["sEmpCode"].ToString() != "NP190" &&
                                 Session["sEmpCode"].ToString() != "NP317" &&
                                 Session["sEmpCode"].ToString() != "NP3312" &&
                                 Session["sEmpCode"].ToString() != "NP3400" &&
                                 Session["sEmpCode"].ToString() != "NP3405" &&
                                 Session["sEmpCode"].ToString() != "NP3406" &&
                                 Session["sEmpCode"].ToString() != "NP3417"))
                            {
                                for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
                                {
                                    if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                                    {

                                        totalLeaveDays++;
                                    }
                                }

                                leaveDays = totalLeaveDays.ToString(); // Update leaveDays only if this condition is true
                                lblLeaves.Text = leaveDays;

                                if (lblLeaves.Text == "0")
                                {
                                    FileUpload.Visible = false;
                                    lblUpload.Visible = false;
                                    uploadedDoc.Visible = false;
                                    btnApprove.Visible = false;
                                    lblMessage.Text = "You have selected 0 leave days.";
                                    return;
                                }
                            }
                            else
                            {
                                ds = objNps.getShiftRoster((string)Session["sCompID"], (string)Session["sEmpCode"], frmDate, toDate, ViewState["LEAVECOUNT"].ToString());

                                if (ds.Tables[0].Rows[0]["TOTAL_LEAVE_DAYS"].ToString() == "")
                                {
                                    lblLeaves.Text = leaveDays;
                                }
                                else
                                {
                                    lblLeaves.Text = ds.Tables[0].Rows[0]["TOTAL_LEAVE_DAYS"].ToString();
                                }
                            }

                        }


                        else
                        {
                            // Count total days including weekends for other leave types
                            totalLeaveDays = (endDate - startDate).Days + 1; // Include both start and end dates
                            leaveDays = totalLeaveDays.ToString(); // Update leaveDays for other types
                            lblLeaves.Text = leaveDays;
                        }

                        //lblLeaves.Text = leaveDays; // Update the leaveDays label


                        if ((Convert.ToInt16(lblLeaves.Text) <= 2) && (Leave_Code == "SL"))
                        {
                            if (closingBal >= Convert.ToDouble(leaveDays) && result == "1") // added here dnyaneshwar
                            //if (closingBal >= Convert.ToDouble(leaveDays))
                            {
                                uploadedDoc.Visible = true;
                                btnApprove.Visible = true;
                                divAlert.Visible = false;
                                lblMessage.Text = "";
                            }
                            else
                            {
                                lblMessage.Text = "You Have " + closingBal + " " + Leave_Code + " Leave Balance Left";
                                FileUpload.Visible = false;
                                lblUpload.Visible = false;
                                uploadedDoc.Visible = false;
                                btnApprove.Visible = false;
                            }
                        }
                        else if (closingBal != Convert.ToDouble(0) && Leave_Code != "SL")
                        {
                            if ((Convert.ToInt16(lblLeaves.Text) >= 5) && (Leave_Code == "PL" || Leave_Code == "TI") && (EMP_CODE != "GET" && EMP_CODE != "CON"))
                            {
                                if (closingBal >= Convert.ToDouble(leaveDays) && result == "1") // added here dnyaneshwar
                                //if (closingBal >= Convert.ToDouble(leaveDays))
                                {
                                    uploadedDoc.Visible = true;
                                    btnApprove.Visible = true;
                                    divAlert.Visible = false;
                                    lblMessage.Text = "";
                                }
                                else
                                {
                                    lblMessage.Text = "You Have " + closingBal + " " + Leave_Code + " Leave Balance Left";
                                    FileUpload.Visible = false;
                                    lblUpload.Visible = false;
                                    uploadedDoc.Visible = false;
                                    btnApprove.Visible = false;
                                }
                            }
                            else if (Leave_Code == "PL" && (EMP_CODE == "GET" || EMP_CODE == "CON"))
                            {
                                if (closingBal >= Convert.ToDouble(leaveDays) && result == "1") // added here dnyaneshwar
                                    //if (closingBal >= Convert.ToDouble(leaveDays))
                                {
                                    uploadedDoc.Visible = true;
                                    btnApprove.Visible = true;
                                    divAlert.Visible = false;
                                    lblMessage.Text = "";
                                }
                                else
                                {
                                    lblMessage.Text = "You Have " + closingBal + " " + Leave_Code + " Leave Balance Left";
                                    FileUpload.Visible = false;
                                    lblUpload.Visible = false;
                                    uploadedDoc.Visible = false;
                                    btnApprove.Visible = false;
                                }
                            }
                            else if (Convert.ToInt16(lblLeaves.Text) <= 4 && Leave_Code == "CL")
                            {
                                if (closingBal >= Convert.ToDouble(leaveDays) && result == "1") // added here dnyaneshwar
                                //if (closingBal >= Convert.ToDouble(leaveDays))
                                {
                                    uploadedDoc.Visible = true;
                                    btnApprove.Visible = true;
                                    divAlert.Visible = false;
                                    lblMessage.Text = "";
                                }
                                else
                                {
                                    lblMessage.Text = "You Have " + closingBal + " " + Leave_Code + " Leave Balance Left";
                                    FileUpload.Visible = false;
                                    lblUpload.Visible = false;
                                    uploadedDoc.Visible = false;
                                    btnApprove.Visible = false;
                                }
                            }
                            else if (Leave_Code != "PL" && Leave_Code != "TI" && Leave_Code != "CL")
                            {
                                if (closingBal >= Convert.ToDouble(leaveDays) && result == "1") // added here dnyaneshwar
                                   // if (closingBal >= Convert.ToDouble(leaveDays))
                                {
                                    uploadedDoc.Visible = true;
                                    btnApprove.Visible = true;
                                    divAlert.Visible = false;
                                    lblMessage.Text = "";
                                }
                                else
                                {
                                    lblMessage.Text = "You Have " + closingBal + " " + Leave_Code + " Leave Balance Left";
                                    FileUpload.Visible = false;
                                    lblUpload.Visible = false;
                                    uploadedDoc.Visible = false;
                                    btnApprove.Visible = false;
                                }
                            }
                            else if (Leave_Code == "CO")
                            {
                                if (closingBal >= Convert.ToDouble(leaveDays) && result == "1") // added here dnyaneshwar
                                   // if (closingBal >= Convert.ToDouble(leaveDays) )
                                {
                                    uploadedDoc.Visible = false;
                                    btnApprove.Visible = true;
                                    divAlert.Visible = false;
                                    lblMessage.Text = "";
                                }
                                else
                                {
                                    lblMessage.Text = "You Have " + closingBal + " " + Leave_Code + " Leave Balance Left";
                                    FileUpload.Visible = false;
                                    lblUpload.Visible = false;
                                    uploadedDoc.Visible = false;
                                    btnApprove.Visible = false;
                                }
                            }
                            else
                            {
                                if (Leave_Code == "CL")
                                {
                                    lblMessage.Text = "The maximum period to apply for CL should be 4 days";
                                    leaveDays = totalLeaveDays.ToString();
                                }
                                else if (Leave_Code == "PL" || Leave_Code == "TI")
                                {
                                    lblMessage.Text = "The minimum period to apply for PL or TI should be 5 days.";
                                    leaveDays = totalLeaveDays.ToString();
                                }

                                FileUpload.Visible = false;
                                lblUpload.Visible = false;
                                uploadedDoc.Visible = false;
                                btnApprove.Visible = false;
                            }
                        }
                        else
                        {
                          if (Leave_Code == "SL" && closingBal >= Convert.ToDouble(leaveDays) && result == "1") // added here dnyaneshwar

                               // if (Leave_Code == "SL" && closingBal >= Convert.ToDouble(leaveDays))
                                {
                                lblMessage.Text = "Kindly upload medical Certificate";
                                FileUpload.Visible = true;
                                lblUpload.Visible = true;
                                uploadedDoc.Visible = true;
                                btnApprove.Visible = true;
                            }
                            else
                            {
                                lblMessage.Text = "You Have " + closingBal + " " + Leave_Code + " Leave Balance Left";
                                FileUpload.Visible = false;
                                lblUpload.Visible = false;
                                uploadedDoc.Visible = false;
                                btnApprove.Visible = false;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        divAlert.Visible = true;
                        lblMessage.Text = ex.Message;
                    }
                }
                else
                {
                    divAlert.Visible = true;
                    lblMessage.Text = "You Have 0 " + Leave_Code + " Leave Balance Left";
                    FileUpload.Visible = false;
                    lblUpload.Visible = false;
                    uploadedDoc.Visible = false;
                    btnApprove.Visible = false;
                }
            }
        }
    }
        
}