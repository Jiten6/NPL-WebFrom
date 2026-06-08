using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using RestSharp;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;

namespace NewPortal2023.ESS
{
    public partial class BranchMgr : System.Web.UI.Page
    {


        //NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();
        NewPortal2023.ESS.Common objCommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.OTP objOTP = new NewPortal2023.ESS.OTP();
        NewPortal2023.ESS.Email emailSend = new NewPortal2023.ESS.Email();
        DataSet dsExp = new DataSet();
        private string SourcePath = string.Empty;
        private string savePath = string.Empty;
        string Gender = "";
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                //gvList.PageIndex = e.NewPageIndex;
                if (!Page.IsPostBack)
                {
                    ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                    divAlert.Visible = false;
                    divAlertCreate.Visible = false;
                    lblMessage.Text = "";
                    lblMessageCreate.Text = "";

                    mv.SetActiveView(vwList);
                    Fill_Details("1", gvList.PageSize.ToString());
                }
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }

        }
        private void Fill_Details(string index, string size)
        {
            objOTP = new NewPortal2023.ESS.OTP();
            DataSet ds = new DataSet();


            objOTP.FillCandidates(gvList, index, size);


        }
        private void OpenList()
        {
            //Fill_Details("1", gvList.PageSize.ToString());
            if (mv.ActiveViewIndex != 0) mv.SetActiveView(vwList);
            {
                Fill_Details("1", gvList.PageSize.ToString());
            }

        }

        protected void btnFullTime_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "", false);
            txtEmployeementType.Text = "Full Time";

            mv.SetActiveView(vwCreate);
        }

        protected void btnPartTime_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "", false);
            txtEmployeementType.Text = "Part Time";
            //ClearControl();
            mv.SetActiveView(vwCreate);
        }


        public void txtAadharNo_TextChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    DataSet ds = new DataSet();
            //    ds = objOTP.tokengenerate();
            //    if (ds.Tables.Count != 0)
            //    {
            //        if (ds.Tables[0].Rows.Count > 0)
            //        {
            //            string COUNT = ds.Tables[0].Rows[0]["TOKEN_KEY"].ToString();
            //            Session["access_token"] = COUNT;
            //        }
            //        else
            //        {
            //            RegisterAsyncTask(new PageAsyncTask(AsyncEventHandlertoken));
            //        }

            //    }
            //    RegisterAsyncTask(new PageAsyncTask(AsyncEventHandler));
            //    mv.SetActiveView(vwCreate);
            //}
            //catch (Exception ex)
            //{

            //}
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            try
            {
                objOTP.AadhaarNo = txtAadharNo.Text;
                dsExp = objOTP.CheckAadharEntry();

                if (dsExp.Tables[0].Rows[0]["result"].ToString() == "VALID")
                {
                    DataSet ds = new DataSet();
                    ds = objOTP.tokengenerate();
                    if (ds.Tables.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string COUNT = ds.Tables[0].Rows[0]["TOKEN_KEY"].ToString();
                            Session["access_token"] = COUNT;
                        }
                        else
                        {
                            RegisterAsyncTask(new PageAsyncTask(AsyncEventHandlertoken));
                        }

                    }
                    RegisterAsyncTask(new PageAsyncTask(AsyncEventHandler));
                    mv.SetActiveView(vwCreate);
                }
                else
                {
                    divAlertCreate.Visible = true;
                    string script = $@"<script type='text/javascript'>alert('Candidate having Aadhar No." + " " + txtAadharNo.Text + " " + "Already Exist.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    objCommon.SetMessageColor(divAlertCreate, "danger");
                    lblMessageCreate.Text = "Candidate having Aadhar No." +" " + txtAadharNo.Text +" "+ "Already Exist";
                }


               
            }
            catch (Exception ex)
            {

            }
        }
        private async Task AsyncEventHandlertoken()
        {

            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(60);

            var options = new RestClientOptions("https://api.sandbox.co.in/authenticate");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-api-key", "key_live_bPHadIxn4BNcgYChErPaqU4vxEy7uEUD");
            request.AddHeader("x-api-secret", "secret_live_x54iVYgGZb6eSVTTJrKkpto3a5R8UxZ1");
            request.AddHeader("x-api-version", "1.0");


            var response = await client.PostAsync(request);

            //var responseData = response.Content;
            ////var responseData = await response.Content.ReadAsStringAsync();

            //dynamic responseObject = JsonConvert.DeserializeObject(responseData);

            //// Access specific properties and assign them to your label text

            //nameLabel.Text =  responseObject.data.access_token;
            //var response = await client.PostAsync(request);
            var responseData = response.Content;

            // Deserialize the response JSON
            var responseObject = JsonConvert.DeserializeObject<dynamic>(responseData);

            // Access the access_token property
            string accessToken = responseObject.access_token;

            Session["access_token"] = accessToken;
            // Store the access_token in session
            //HttpContext.Session.SetString("AccessToken", accessToken);

            //return Ok("Token stored in session!");
            //Console.WriteLine("{0}", response.Content);

            DataSet ds = new DataSet();
            ds = objOTP.tokenINSERT((string)Session["access_token"]);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc(options =>
            //{
            //    //options.Filters.Add(new RequestTimeoutExceptionFilter(TimeSpan.FromSeconds(60))); // Set timeout to 60 seconds
            //});
        }

        private async Task AsyncEventHandler()
        {


            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(60);

            var aadhaarNumber = txtAadharNo.Text;

            var options = new RestClientOptions("https://api.sandbox.co.in/kyc/aadhaar/okyc/otp");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", (string)Session["access_token"]);
            request.AddHeader("x-api-key", "key_live_bPHadIxn4BNcgYChErPaqU4vxEy7uEUD");
            request.AddHeader("x-api-version", "1.0");
            //    request.AddJsonBody("{\"aadhaar_number\":\"441719694030\"}", false);
            //    var response = await client.PostAsync(request);

            //    Console.WriteLine("{0}", response.Content);
            string requestBodyJson = "{\"aadhaar_number\": \"" + aadhaarNumber + "\"}";

            request.AddJsonBody(requestBodyJson); // Pass the serialized JSON as the request body

            var response = await client.PostAsync(request);
            var responseData = response.Content;
            //var responseData = await response.Content.ReadAsStringAsync();

            dynamic responseObject = JsonConvert.DeserializeObject(responseData);

            string ref_id = responseObject.data.ref_id;
            Session["ref_id"] = ref_id;

            objCommon = new NewPortal2023.ESS.Common();
            divAlertCreate.Visible = true;
            // SENDOFFERLETTER(txtEmail.Text);
            string script = $@"<script type='text/javascript'>alert('OTP Sent Successfully.');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            objCommon.SetMessageColor(divAlertCreate, "success");
            lblMessageCreate.Text = "OTP Sent Successfully.";
        }
        //protected void txAadhartOTP_TextChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        DataSet ds = new DataSet();
        //        ds = objOTP.tokengenerate();
        //        if (ds.Tables.Count != 0)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                string COUNT = ds.Tables[0].Rows[0]["TOKEN_KEY"].ToString();
        //                Session["access_token"] = COUNT;
        //            }
        //            else
        //            {
        //                RegisterAsyncTask(new PageAsyncTask(AsyncEventHandlertoken));
        //            }

        //        }
        //        RegisterAsyncTask(new PageAsyncTask(AsyncEventHandler1));
        //        mv.SetActiveView(vwCreate);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private async Task AsyncEventHandler1()
        {
            try
            {

                var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromMinutes(60);

                var mobile_otp = txAadhartOTP.Text;

                var options = new RestClientOptions("https://api.sandbox.co.in/kyc/aadhaar/okyc/otp/verify");
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.AddHeader("accept", "application/json");
                request.AddHeader("Authorization", (string)Session["access_token"]);
                request.AddHeader("x-api-key", "key_live_bPHadIxn4BNcgYChErPaqU4vxEy7uEUD");
                request.AddHeader("x-api-version", "1.0");
                //request.AddJsonBody("{\"ref_id\":\"10571868\",\"otp\":\"878361\"}", false);
                var requestBody = new { ref_id = (string)Session["ref_id"], otp = mobile_otp };
                var requestBodyJson = JsonConvert.SerializeObject(requestBody);

                request.AddJsonBody(requestBodyJson); // Pass the serialized JSON as the request body

                //var response = await client.PostAsync(request);

                var response = await client.PostAsync(request);
                var responseData = response.Content;
                //var responseData = await response.Content.ReadAsStringAsync();

                dynamic responseObject = JsonConvert.DeserializeObject(responseData);

                // Access specific properties and assign them to your label text
                string nameLabel = responseObject.data.name;
                string genderLabel = responseObject.data.gender;
                string dobLabel = responseObject.data.dob;
                string addressLabel = responseObject.data.address;
                //string country = responseObject.split_address.country;
                string country = responseObject.data.split_address.country;
                string state = responseObject.data.split_address.state;
                string pincode = responseObject.data.split_address.pincode;
                ViewState["nameLabel"] = nameLabel;
                ViewState["genderLabel"] = genderLabel;
                ViewState["dobLabel"] = dobLabel;
                ViewState["addressLabel"] = addressLabel;
                ViewState["country"] = country;
                ViewState["state"] = state;
                ViewState["pincode"] = pincode;

                objCommon = new NewPortal2023.ESS.Common();
                divAlertCreate.Visible = true;
                // SENDOFFERLETTER(txtEmail.Text);
                string script = $@"<script type='text/javascript'>alert('OTP Validated Successfully.');</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                objCommon.SetMessageColor(divAlertCreate, "success");
                lblMessageCreate.Text = "OTP Validated Successfully.";



                //dsExp = objOTP.fillAdhharData(nameLabel, genderLabel, dobLabel, addressLabel, country, state, pincode);
            }
            catch (Exception ex)
            {
                // Handle exception
                divAlertCreate.Visible = true;
                objCommon.SetMessageColor(divAlertCreate, "danger");
                lblMessageCreate.Text = "An error occurred: " + ex.Message;
            }
        }
        public class ResponseData
        {
            public string SomeField { get; set; }
            // Add more properties as needed to match your JSON structure
        }
        protected void txtPanNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objOTP.tokengenerate();
                if (ds.Tables.Count != 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string COUNT = ds.Tables[0].Rows[0]["TOKEN_KEY"].ToString();
                        Session["access_token"] = COUNT;
                    }
                    else
                    {
                        RegisterAsyncTask(new PageAsyncTask(AsyncEventHandlertoken));
                    }

                }
                RegisterAsyncTask(new PageAsyncTask(AsyncEventHandlerpan));
                //FillData();
                mv.SetActiveView(vwCreate);
            }
            catch (Exception ex)
            {

            }
        }
        private async Task AsyncEventHandlerpan()
        {

            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(60);

            var pan_number = txtPanNo.Text;

            //// Construct the URL with the pan_number variable
            ////var url = $"https://api.sandbox.co.in/pans/{pan_number}/verify?consent=y&reason=For%20KYC%20of%20User"; 


            var url = string.Format("https://api.sandbox.co.in/pans/{0}/verify?consent=y&reason=For%20KYC%20of%20User", pan_number);

            var options = new RestClientOptions(url);
            //var options = new RestClientOptions("https://api.sandbox.co.in/pans/" + pan_number + "/verify?consent=y&reason=For%20KYC%20of%20User", pan_number);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", (string)Session["access_token"]);
            request.AddHeader("x-api-key", "key_live_bPHadIxn4BNcgYChErPaqU4vxEy7uEUD");
            request.AddHeader("x-api-version", "1.0");
            var response = await client.GetAsync(request);

            var responseData = response.Content;
            //var responseData = await response.Content.ReadAsStringAsync();

            dynamic responseObject = JsonConvert.DeserializeObject(responseData);
            //Console.WriteLine("{0}", response.Content);
            // dynamic responseObject = JsonConvert.DeserializeObject(responseData);
            string lblpan = responseObject.data.pan;
            string panname = responseObject.data.full_name;

            ViewState["lblpan"] = lblpan;
            ViewState["panname"] = panname;

            //divAlertCreate.Visible = true;
            ////lblMessage.Text = lblpan.Text + " " + Lblpanname.Text;
            //objCommon.SetMessageColor(divAlertCreate, "success");

            objCommon = new NewPortal2023.ESS.Common();
            divAlertCreate.Visible = true;
            // SENDOFFERLETTER(txtEmail.Text);
            string script = $@"<script type='text/javascript'>alert('PAN Validated Successfully.');</script>";
            ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
            objCommon.SetMessageColor(divAlertCreate, "success");
            lblMessageCreate.Text = "PAN Validated Successfully.";

            httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(60);

            url = string.Format("https://api.sandbox.co.in/it-tools/pans/{0}/pan-aadhaar-status?aadhaar_number={1}", pan_number, txtAadharNo.Text);
            options = new RestClientOptions(url);
            //options = new RestClientOptions("https://api.sandbox.co.in/it-tools/pans/FGOPP7390P/pan-aadhaar-status?aadhaar_number=441719694030");
            client = new RestClient(options);
            request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", (string)Session["access_token"]);
            request.AddHeader("x-api-key", "key_live_bPHadIxn4BNcgYChErPaqU4vxEy7uEUD");
            request.AddHeader("x-api-version", "1.0");
            response = await client.GetAsync(request);

            responseData = response.Content;
            //var responseData = await response.Content.ReadAsStringAsync();

            responseObject = JsonConvert.DeserializeObject(responseData);
            //Console.WriteLine("{0}", response.Content);
            // dynamic responseObject = JsonConvert.DeserializeObject(responseData);
            //string LinkStatus = responseObject.data.message;
            //lblLinkStatus = LinkStatus;

            //string LinkStatus = responseObject.data.message;
            //lblLinkStatus.Controls.Clear(); // Clear any existing content
            //lblLinkStatus.Controls.Add(new LiteralControl(LinkStatus));

            string LinkStatus = responseObject.data.message;

            if(LinkStatus== "PAN not linked with Aadhaar. Please click on Link Aadhaar link to link your Aadhaar with PAN.")
            {
                lblAPLinkStatus.Text = "Aadhar-PAN is not linked yet";
                ViewState["LinkStatus"] = "Not Linked";
            }
            else if(LinkStatus == "The PAN entered is inactive. Please contact your Accessing Officer to activate the PAN." ||LinkStatus== "The PAN entered is inactive. Please contact your Accessing Officer to activate the PAN.")
            {
                lblAPLinkStatus.Text = "Aadhar is not matching with PAN";
                ViewState["LinkStatus"] = "Linked";
            }
            else
            {
                lblAPLinkStatus.Text = "Aadhar-PAN is linked";
                ViewState["LinkStatus"] = "Linked";
            }
            

            FillData();

            // dsExp = objOTP.fillPanData(lblpan, panname);
            //lblstatus.Text = "STATUS: " + responseObject.data.status;
            //var options = new RestClientOptions(url);
            //var client = new RestClient(options);
            //var request = new RestRequest("");
            //request.AddHeader("accept", "application/json");
            //request.AddHeader("Authorization", (string)Session["access_token"]);
            //request.AddHeader("x-api-key", "key_live_bPHadIxn4BNcgYChErPaqU4vxEy7uEUD");
            //request.AddHeader("x-api-version", "1.0");

            //var response = await client.GetAsync(request);
            ////var response = await httpClient.GetAsync(request.Content);

            //if (response.IsSuccessStatusCode)
            //{
            //    var responseData = await response.Content.ReadAsStringAsync();

            //    dynamic responseObject = JsonConvert.DeserializeObject(responseData);

            //    // Access specific properties and assign them to your label text
            //    lblpan.Text = "PAN_NUMBER: " + responseObject.data.pan;
            //    nameLabel.Text = "FULL_NAME: " + responseObject.data.full_name;
            //    lblstatus.Text = "STATUS: " + responseObject.data.status;
            //}
            //else
            //{
            //    // Handle the case where the request was not successful
            //    // For example, you can display an error message
            //    // or handle specific HTTP status codes accordingly
            //    errorLabel.Text = "Error: " + response.StatusCode;
            //}

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateInputs())
                {
                    objOTP.EmployeeType = txtEmployeementType.Text;
                    objOTP.AadhaarNo = txtAadharNo.Text;
                    objOTP.PanNo = txtPanNo.Text;
                    objOTP.StateOfPosting = drpStateOfPosting.SelectedValue;
                    objOTP.LocationOfPosting = drpLocationOfPosting.SelectedValue;
                    objOTP.Degination = drpDegination.SelectedValue;
                    objOTP.Department = drpDepartment.SelectedValue;
                    objOTP.Grade = drpGrade.SelectedValue;
                    objOTP.SUCode = drpSUCode.SelectedValue;
                    objOTP.CandidateCategory = drpCandidateCategory.SelectedValue;
                    objOTP.Mobile = txtMobileNumber.Text;
                    objOTP.nameLabel = ViewState["nameLabel"].ToString();
                    objOTP.genderLabel = ViewState["genderLabel"].ToString();
                    objOTP.dobLabel = ViewState["dobLabel"].ToString();
                    objOTP.addressLabel = ViewState["addressLabel"].ToString();
                    objOTP.country = ViewState["country"].ToString();
                    objOTP.state = ViewState["state"].ToString();
                    objOTP.pincode = ViewState["pincode"].ToString();
                    objOTP.panname = ViewState["panname"].ToString();
                    objOTP.LinkStatus = ViewState["LinkStatus"].ToString();
                    objOTP.Add1 = txtAdd1.Text;
                    objOTP.Add2 = txtAdd2.Text;
                    objOTP.Add3 = txtAdd3.Text;
                    objOTP.emailId = txtEmail.Text;
                    objOTP.ExcpDOJ = txtExpDOJ.Text;
                    objOTP.Rmk = txtRemk.Text;
                    

                    if (btnSave.Text == "Save")
                    {
                        dsExp = objOTP.InsertCandidateDetails();
                        if (dsExp.Tables[0].Rows[0]["result"].ToString() == "")
                        {
                            Upload_ClickAadhar(txtAadharNo.Text);
                            DisplayDocumentsAadhar(txtAadharNo.Text);
                            Upload_ClickPan(txtAadharNo.Text);
                            DisplayDocumentsPan(txtAadharNo.Text);
                            Upload_ClickEduc(txtAadharNo.Text);
                            DisplayDocumentsEduc(txtAadharNo.Text);
                            Upload_ClickCC(txtAadharNo.Text);
                            DisplayDocumentsCC(txtAadharNo.Text);
                            objCommon = new NewPortal2023.ESS.Common();
                            divAlert.Visible = true;
                            // SENDOFFERLETTER(txtEmail.Text);
                            objCommon.SetMessageColor(divAlert, "success");
                            lblMessage.Text = "Successfully Submitted.";
                            btnSave.Visible = false;
                            mv.SetActiveView(vwList);
                            Fill_Details("1", gvList.PageSize.ToString());
                        }
                        else
                        {
                            if (dsExp.Tables[0].Rows[0]["result"].ToString() == "DUPLICATE")
                            {
                                objCommon = new NewPortal2023.ESS.Common();
                                divAlert.Visible = true;
                                objCommon.SetMessageColor(divAlert, "danger");
                                lblMessage.Text = "Already Exits.";
                            }
                            else
                            {
                                objCommon = new NewPortal2023.ESS.Common();
                                divAlert.Visible = true;
                                objCommon.SetMessageColor(divAlert, "danger");
                                lblMessage.Text = "Error.";
                            }
                        }
                    }
                    else if (btnSave.Text == "Update")
                    {
                        objOTP.EntryAId = ViewState["EntryAId"].ToString();
                        string count = objOTP.EntryAId.ToString();
                        dsExp = objOTP.UpdateCandidateDetails();
                        if (dsExp.Tables[0].Rows[0]["result"].ToString() == "")
                        {
                            Upload_ClickAadhar(txtAadharNo.Text);
                            DisplayDocumentsAadhar(txtAadharNo.Text);
                            Upload_ClickPan(txtAadharNo.Text);
                            DisplayDocumentsPan(txtAadharNo.Text);
                            Upload_ClickEduc(txtAadharNo.Text);
                            DisplayDocumentsEduc(txtAadharNo.Text);
                            Upload_ClickCC(txtAadharNo.Text);
                            DisplayDocumentsCC(txtAadharNo.Text);
                            objCommon = new NewPortal2023.ESS.Common();
                            divAlert.Visible = true;
                            objCommon.SetMessageColor(divAlert, "success");
                            lblMessage.Text = "Successfully Updated.";
                            mv.SetActiveView(vwList);
                            Fill_Details("1", gvList.PageSize.ToString());
                        }
                        else
                        {
                            objCommon = new NewPortal2023.ESS.Common();
                            divAlert.Visible = true;
                            objCommon.SetMessageColor(divAlert, "danger");
                            lblMessage.Text = "Error.";
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                objCommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = ex.Message;
            }
        }

        //private void SENDOFFERLETTER(string email)
        //{
        //    try
        //    {
        //        MailMessage mailMessage = new MailMessage();
        //        mailMessage.To.Add(email);
        //        mailMessage.From = new MailAddress("reports@sequelgroup.co.in");
        //        mailMessage.Subject = "Offer Letter";
        //        mailMessage.Body = "Dear Sir/Madam,\n\n Your offer letter is attached as given below. Please download.\n" + " http://localhost:49272/ESS/CandidateLoginPage.aspx " + "\nWith Best Regards,\nPayrollservices";
        //        System.Net.Mail.Attachment data = new System.Net.Mail.Attachment(Request.PhysicalApplicationPath.ToString() + @"\" + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + @"\Temp\" + (string)(email) + "_OfferLetter.pdf",
        //                                                    System.Net.Mime.MediaTypeNames.Application.Octet);
        //        mailMessage.Attachments.Add(data);

        //        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
        //        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        //        smtpClient.UseDefaultCredentials = false;
        //        System.Net.ServicePointManager.ServerCertificateValidationCallback =
        //       new System.Net.Security.RemoteCertificateValidationCallback(RemoteServerCertificateValidationCallback);
        //        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //        smtpClient.Credentials = new System.Net.NetworkCredential("reports@sequelgroup.co.in", "sequel@123");
        //        smtpClient.EnableSsl = true;
        //        smtpClient.Send(mailMessage);

        //        data.Dispose();
        //        mailMessage.Dispose();
        //        smtpClient.Dispose();
        //        DataSet ds = new DataSet();
        //        objOTP.emailId = email;

        //        ds = objOTP.DownloadOfferLetter();
        //        if (ds.Tables[0].Rows[0]["result"].ToString() == "")
        //        {
        //            objCommon = new NewPortal2023.ESS.Common();
        //            divAlert.Visible = true;
        //            objCommon.SetMessageColor(divAlert, "success");
        //            lblMessage.Text = "Offer letter is send on candidate mail Id";


        //        }
        //        else
        //        {
        //            objCommon = new NewPortal2023.ESS.Common();
        //            divAlert.Visible = true;
        //            objCommon.SetMessageColor(divAlert, "danger");
        //            lblMessage.Text = "Error on Mail";
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        objCommon = new NewPortal2023.ESS.Common();
        //        divAlert.Visible = true;
        //        objCommon.SetMessageColor(divAlert, "danger");
        //        lblMessage.Text = ex.Message;
        //    }
        //}

        //private bool RemoteServerCertificateValidationCallback(object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
        //{
        //    //Console.WriteLine(certificate);
        //    return true;
        //}

        private void Upload_ClickAadhar(string aadhar)
        {
            if (fupldDocumentAadhar.PostedFile.FileName == "")
            {
                divAlert.Visible = true;
                lblMessage.Text = "Please Upload Transport document.";
                return;
            }

            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Aadhar\\" + aadhar + "\\";

            System.IO.Stream fileInputStream = fupldDocumentAadhar.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(fupldDocumentAadhar.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocumentAadhar.FileName.Trim());

            string filesToDelete = aadhar;
            string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
            System.IO.FileStream fStream = new System.IO.FileStream(@savePath + aadhar + "_" + fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write(fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();
        }

        private void Upload_ClickPan(string aadhar)
        {
            if (fupldDocumentPan.PostedFile.FileName == "")
            {
                divAlert.Visible = true;
                lblMessage.Text = "Please Upload Transport document.";
                return;
            }

            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Pan\\" + aadhar + "\\";

            System.IO.Stream fileInputStream = fupldDocumentPan.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(fupldDocumentPan.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocumentPan.FileName.Trim());

            string filesToDelete = aadhar;
            string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
            System.IO.FileStream fStream = new System.IO.FileStream(@savePath + aadhar + "_" + fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write(fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();
        }

        private void Upload_ClickEduc(string aadhar)
        {
            if (fupldDocumentEduc.PostedFile.FileName == "")
            {
                divAlert.Visible = true;
                lblMessage.Text = "Please Upload Transport document.";
                return;
            }

            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Education\\" + aadhar + "\\";

            System.IO.Stream fileInputStream = fupldDocumentEduc.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(fupldDocumentEduc.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocumentEduc.FileName.Trim());

            string filesToDelete = aadhar;
            string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
            System.IO.FileStream fStream = new System.IO.FileStream(@savePath + aadhar + "_" + fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write(fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();
        }

        private void Upload_ClickCC(string aadhar)
        {
            if (fupldDocumentCC.PostedFile.FileName == "")
            {
                divAlert.Visible = true;
                lblMessage.Text = "Please Upload Transport document.";
                return;
            }

            savePath = Request.PhysicalApplicationPath;
            savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\CC\\" + aadhar + "\\";

            System.IO.Stream fileInputStream = fupldDocumentCC.PostedFile.InputStream;
            Byte[] fileContent = new Byte[fileInputStream.Length];
            fileInputStream.Read(fileContent, 0, Convert.ToInt32(fileInputStream.Length));
            fileInputStream.Close();
            /* Create Folder */

            if (System.IO.Directory.Exists(@savePath) == false)
            {
                System.IO.Directory.CreateDirectory(@savePath);
            }

            DateTime indianTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            string fileName = Path.GetFileNameWithoutExtension(fupldDocumentCC.FileName.Trim().Replace(" ", "_")) + "_"
                + indianTime.ToString("ddMMyyyyHHmmss") + Path.GetExtension(fupldDocumentCC.FileName.Trim());

            string filesToDelete = aadhar;
            string[] fileList = System.IO.Directory.GetFiles(@savePath, filesToDelete);
            foreach (string file in fileList)
            {
                System.IO.File.Delete(file);
            }
            System.IO.FileStream fStream = new System.IO.FileStream(@savePath + aadhar + "_" + fileName, System.IO.FileMode.Create);
            System.IO.BinaryWriter bWriter = new System.IO.BinaryWriter(fStream);
            bWriter.Write(fileContent);
            fStream.Flush();
            bWriter.Flush();
            fStream.Close();
            bWriter.Close();
        }

        private void CreateDocumentsStructureAsdhar()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvAadhar.DataSource = dtDocuments;
                gvAadhar.DataBind();
            }
        }
        private void DisplayDocumentsAadhar(string aadhar)
        {
            try
            {
                CreateDocumentsStructureAsdhar();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Aadhar\\" + aadhar + "\\";

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

                    this.gvAadhar.DataSource = dtDocInfo;
                    this.gvAadhar.DataBind();

                    if (dtDocInfo.Rows.Count > 0)
                    {
                        divfileUploadAadhar.Visible = false;
                        divAadhar.Visible = true;


                    }
                    else
                    {
                        //trUpload.Visible = true;
                        divAadhar.Visible = false;
                    }
                }
                else
                {
                    //trUpload.Visible = true;
                    divAadhar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkBtnOpenFileAadhar_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorageAadhar");

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
            catch
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void CreateDocumentsStructurePan()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvPan.DataSource = dtDocuments;
                gvPan.DataBind();
            }
        }
        private void DisplayDocumentsPan(string aadhar)
        {
            try
            {
                CreateDocumentsStructurePan();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Pan\\" + aadhar + "\\";

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

                    this.gvPan.DataSource = dtDocInfo;
                    this.gvPan.DataBind();

                    if (dtDocInfo.Rows.Count > 0)
                    {
                        divfileUploadPan.Visible = false;
                        divPan.Visible = true;


                    }
                    else
                    {
                        //trUpload.Visible = true;
                        divPan.Visible = false;
                    }
                }
                else
                {
                    //trUpload.Visible = true;
                    divPan.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkBtnOpenFilesPan_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStoragePan");

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
            catch
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private void CreateDocumentsStructureEduc()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvEduc.DataSource = dtDocuments;
                gvEduc.DataBind();
            }
        }
        private void DisplayDocumentsEduc(string aadhar)
        {
            try
            {
                CreateDocumentsStructureEduc();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\Education\\" + aadhar + "\\";

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

                    this.gvEduc.DataSource = dtDocInfo;
                    this.gvEduc.DataBind();

                    if (dtDocInfo.Rows.Count > 0)
                    {
                        divfileUploadEduc.Visible = false;
                        divEduc.Visible = true;


                    }
                    else
                    {
                        //trUpload.Visible = true;
                        divEduc.Visible = false;
                    }
                }
                else
                {
                    //trUpload.Visible = true;
                    divEduc.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkBtnOpenFilesEduc_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorageEduc");

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
            catch
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }


        private void CreateDocumentsStructureCC()
        {
            using (DataTable dtDocuments = new DataTable())
            {
                dtDocuments.Columns.Add(new DataColumn("FILEPATH", System.Type.GetType("System.String")));
                dtDocuments.Columns.Add(new DataColumn("FILENAME", System.Type.GetType("System.String")));

                ViewState["Documents"] = dtDocuments;
                gvCC.DataSource = dtDocuments;
                gvCC.DataBind();
            }
        }
        private void DisplayDocumentsCC(string aadhar)
        {
            try
            {
                CreateDocumentsStructureCC();

                string prefix = "";



                savePath = Request.PhysicalApplicationPath;
                savePath = savePath + Convert.ToString(ConfigurationManager.AppSettings.Get("Path")) + Convert.ToString(Session["sCompID"]) + "\\Documents\\CC\\" + aadhar + "\\";

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

                    this.gvCC.DataSource = dtDocInfo;
                    this.gvCC.DataBind();

                    if (dtDocInfo.Rows.Count > 0)
                    {
                        divfileUploadCC.Visible = false;
                        divCC.Visible = true;


                    }
                    else
                    {
                        //trUpload.Visible = true;
                        divCC.Visible = false;
                    }
                }
                else
                {
                    //trUpload.Visible = true;
                    divCC.Visible = false;
                }
            }
            catch (Exception ex)
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }
        protected void lnkBtnOpenFilesCC_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnkBtnOpenFiles = (LinkButton)sender;
                Label lblTSFileStorageName = (Label)lnkBtnOpenFiles.NamingContainer.FindControl("lblFileStorageCC");

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
            catch
            {
                divAlert.Visible = true;
                lblMessage.Text = "Error occurred in application.";
            }
        }




        private bool ValidateInputs()
        {
            divAlertCreate.Visible = true;


            if (txtAadharNo.Text.Trim() == "")
            {
                divAlertCreate.Visible = true;
                lblMessageCreate.Text = "Enter Aadhar No.";
                return false;
            }
            if (txtPanNo.Text == "")
            {
                lblMessageCreate.Text = "Enter Pan No.";
                return false;
            }
            if (txtMobileNumber.Text == "")
            {
                lblMessageCreate.Text = "Enter Mobile No.";
                return false;
            }
            if (txtEmployeementType.Text == "")
            {
                lblMessageCreate.Text = "Enter Employeement Type.";
                return false;
            }


            if (drpStateOfPosting.SelectedValue.Trim() == "")
            {
                divAlertCreate.Visible = true;
                lblMessageCreate.Text = "Select State Of Posting.";
                return false;
            }
            if (drpLocationOfPosting.SelectedValue.Trim() == "")
            {
                lblMessageCreate.Text = "Select Location Of Posting.";
                return false;
            }

            if (drpDepartment.SelectedValue.Trim() == "")
            {
                lblMessageCreate.Text = "Select  Department.";
                return false;
            }
            if (drpGrade.SelectedValue.Trim() == "")
            {
                lblMessageCreate.Text = "Select Grade.";
                return false;
            }
            if (drpDegination.SelectedValue.Trim() == "")
            {
                lblMessageCreate.Text = "Select Degination.";
                return false;
            }
            if (drpCandidateCategory.SelectedValue.Trim() == "")
            {
                lblMessageCreate.Text = "Select Candidate Category.";
                return false;
            }
            if (drpSUCode.SelectedValue.Trim() == "")
            {
                lblMessageCreate.Text = "Select SU Code.";
                return false;
            }

            //if (txtAdd1.Text == "")
            //{
            //    lblMessageCreate.Text = "Enter Address 1.";
            //    return false;
            //}
            //if (txtAdd2.Text == "")
            //{
            //    lblMessageCreate.Text = "Enter Address 2.";
            //    return false;
            //}
            if (txtEmail.Text == "")
            {
                lblMessageCreate.Text = "Enter Email ID.";
                return false;
            }

            return true;
        }

        protected void txtAadharNoPer_Click(object sender, EventArgs e)
        {
            try
            {
                //objOTP.AadhaarNo = txtAadharNo.Text;
                //dsExp = objOTP.FillDetailsByAadhar();
                //if(dsExp.Tables[0].Rows[0]["result"].ToString()=="")
                //{

                txtAadharNoPer.Text = txtAadharNo.Text;
                txtPanName.Text = ViewState["panname"].ToString();
                txtAadharName.Text = ViewState["nameLabel"].ToString();
                txtDOB.Text = ViewState["dobLabel"].ToString();
                txtCountry.Text = ViewState["country"].ToString();
                txtState.Text = ViewState["state"].ToString();
                txtPIN.Text = ViewState["pincode"].ToString();
                txtAdd3.Text = ViewState["addressLabel"].ToString();
                Gender = ViewState["genderLabel"].ToString();

                if (Gender == "M")
                {
                    rdMale.Checked = true;
                    rdFemale.Checked = false;
                }
                else if (Gender == "F")
                {
                    rdFemale.Checked = true;
                    rdMale.Checked = false;
                }

                //}
            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                objCommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            btnSave.Text = "Save";
            btnSave.Visible = true;
            btnCheck.Visible = true;
            btnValidatePAN.Visible = true;
            divOTP.Visible = true;

            divAadhar.Visible = false;
            divPan.Visible = false;
            divEduc.Visible = false;
            divCC.Visible = false;

            divfileUploadAadhar.Visible = true;
            divfileUploadPan.Visible = true;
            divfileUploadEduc.Visible = true;
            divfileUploadCC.Visible = true;

            divAlert.Visible = false;
            divAlertCreate.Visible = false;
            lblMessage.Text = "";
            lblMessageCreate.Text = "";
            string title = "Select Employeement Type";
            ClearControl();
            mv.SetActiveView(vwCreate);

            //if (ViewState["COUNT"] != null)
            //{
            //    ClientScript.RegisterStartupScript(this.GetType(), "Popup", "", false);

            //}
            //else
            //{
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup('" + title + "');", true);
            ViewState["COUNT"] = 1;
            //}


        }

        private void ClearControl()
        {
            txtEmployeementType.Text = "";
            txtAadharNo.Text = "";
            txtPanNo.Text = "";
            txAadhartOTP.Text = "";
            drpStateOfPosting.SelectedIndex = -1;
            drpLocationOfPosting.SelectedIndex = -1;
            drpGrade.SelectedIndex = -1;
            drpSUCode.SelectedIndex = -1;
            drpCandidateCategory.SelectedIndex = -1;
            txtMobileNumber.Text = "";
            txtAadharNoPer.Text = "";
            txtCaseNo.Text = "";
            txtPanName.Text = "";
            txtAadharName.Text = "";
            txtDOB.Text = "";
            txtAdd1.Text = "";
            txtAdd2.Text = "";
            txtAdd3.Text = "";
            txtCountry.Text = "";
            txtState.Text = "";
            txtPIN.Text = "";
            txtEmail.Text = "";
            txtExpDOJ.Text = "";
            txtRemk.Text = "";
            lblAPLinkStatus.Text = "";
            rdMale.Checked = false;
            rdFemale.Checked = false;
        }

        protected void lnkBtnView_Click(object sender, EventArgs e)
        {
            try
            {
                btnSave.Text = "Update";
                divAlert.Visible = false;
                divAlertCreate.Visible = false;
                lblMessage.Text = "";
                lblMessageCreate.Text = "";
                DataSet ds = new DataSet();
                //objobs = new MPP_NUM.Jobs();
                // FillDropdown();
                objCommon = new NewPortal2023.ESS.Common();
                //LinkButton lnkBtnEdit = (LinkButton)sender;
                //Label lblAID = (Label)lnkBtnEdit.NamingContainer.FindControl("lblAID");

                //objAssignDT.EntryAid = lblAID.Text;


                LinkButton lnkBtnView = (LinkButton)sender;
                Label lblAID = (Label)lnkBtnView.NamingContainer.FindControl("lblAID");
                ViewState["EntryAId"] = lblAID.Text;
                objOTP.EntryAId = lblAID.Text;


                ds = objOTP.Fill_CandidateDetailByID();

                if (ds.Tables[0].Rows.Count > 0)
                {


                    txtEmployeementType.Text = ds.Tables[0].Rows[0]["EMPTYPE"].ToString();
                    txtAadharNo.Text = ds.Tables[0].Rows[0]["AADHARNO"].ToString();
                    txtPanNo.Text = ds.Tables[0].Rows[0]["PANCODE"].ToString();
                    drpStateOfPosting.SelectedValue = ds.Tables[0].Rows[0]["STATEPOST"].ToString();
                    drpLocationOfPosting.SelectedValue = ds.Tables[0].Rows[0]["LOCPOST"].ToString();
                    drpDepartment.SelectedValue = ds.Tables[0].Rows[0]["DEPARTMENT"].ToString();
                    drpDegination.SelectedValue = ds.Tables[0].Rows[0]["DEGINATION"].ToString();
                    drpGrade.SelectedValue = ds.Tables[0].Rows[0]["GRADE"].ToString();
                    drpSUCode.SelectedValue = ds.Tables[0].Rows[0]["SUCODE"].ToString();
                    drpCandidateCategory.SelectedValue = ds.Tables[0].Rows[0]["CANDIDATECAT"].ToString();
                    txtMobileNumber.Text = ds.Tables[0].Rows[0]["MOBILE"].ToString();
                    if (ds.Tables[0].Rows[0]["LINK_STATUS"].ToString() == "Linked")
                    {
                        lblAPLinkStatus.Text = "Aadhar-PAN is linked";
                    }
                    else
                    {
                        lblAPLinkStatus.Text = "Aadhar-PAN is Not-linked yet";
                    }

                    txtCaseNo.Text = ds.Tables[0].Rows[0]["CASE_NO"].ToString();
                    txtPanNo.Text = ds.Tables[0].Rows[0]["PANCODE"].ToString();
                    txtAadharName.Text = ds.Tables[0].Rows[0]["AADHARNAME"].ToString();
                    txtDOB.Text = ds.Tables[0].Rows[0]["DOB"].ToString();
                    txtAdd1.Text = ds.Tables[0].Rows[0]["ADD1"].ToString();
                    txtAdd2.Text = ds.Tables[0].Rows[0]["ADD2"].ToString();
                    txtAdd2.Text = ds.Tables[0].Rows[0]["ADD3"].ToString();
                    txtCountry.Text = ds.Tables[0].Rows[0]["COUNTRY"].ToString();
                    txtState.Text = ds.Tables[0].Rows[0]["STATE"].ToString();
                    txtPIN.Text = ds.Tables[0].Rows[0]["PINCODE"].ToString();
                    txtEmail.Text = ds.Tables[0].Rows[0]["EMAILID"].ToString();
                    txtExpDOJ.Text = ds.Tables[0].Rows[0]["EXPDOJ"].ToString();
                    txtRemk.Text = ds.Tables[0].Rows[0]["BRANCH_REMARKS"].ToString();
                    Gender = ds.Tables[0].Rows[0]["GENDER"].ToString();
                    txtPanName.Text = ds.Tables[0].Rows[0]["panname"].ToString();
                    btnCheck.Visible = false;
                    btnValidatePAN.Visible = false;
                    divOTP.Visible = false;
                    //txtAadharName.Text = ViewState["nameLabel"].ToString();

                    if (Gender == "M")
                    {
                        rdMale.Checked = true;
                        rdFemale.Checked = false;
                    }
                    else if (Gender == "F")
                    {
                        rdFemale.Checked = true;
                        rdMale.Checked = false;
                    }

                    divAadhar.Visible = true;
                    divPan.Visible = true;
                    divEduc.Visible = true;
                    divCC.Visible = true;

                    divfileUploadAadhar.Visible = false;
                    divfileUploadPan.Visible = false;
                    divfileUploadEduc.Visible = false;
                    divfileUploadCC.Visible = false;

                    DisplayDocumentsAadhar(txtAadharNo.Text);
                    DisplayDocumentsPan(txtAadharNo.Text);
                    DisplayDocumentsEduc(txtAadharNo.Text);
                    DisplayDocumentsCC(txtAadharNo.Text);
                    if (ds.Tables[0].Rows[0]["STATUS"].ToString() == "0")
                    {
                        btnSave.Visible = true;
                        btnSave.Text = "Update";
                        divfileUploadAadhar.Visible = true;
                        divfileUploadPan.Visible = true;
                        divfileUploadEduc.Visible = true;
                        divfileUploadCC.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = false;
                    }

                    //objOTP.nameLabel = ViewState["nameLabel"].ToString();
                    //objOTP.genderLabel = ViewState["genderLabel"].ToString();
                    //objOTP.dobLabel = ViewState["dobLabel"].ToString();
                    //objOTP.addressLabel = ViewState["addressLabel"].ToString();
                    //objOTP.country = ViewState["country"].ToString();
                    //objOTP.state = ViewState["state"].ToString();
                    //objOTP.pincode = ViewState["pincode"].ToString();
                    //objOTP.panname = ViewState["panname"].ToString();

                }

                //ClearControlsEdit();
                mv.SetActiveView(vwCreate);
            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                objCommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = ex.Message;
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                OpenList();
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }


        protected void gvAssUserDataPointList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
        protected void lnkDnlAadhar_Click(object sender, EventArgs e)
        {

        }

        protected void btnAadharUpload_Click(object sender, EventArgs e)
        {

        }

        protected void lnkDnlPan_Click(object sender, EventArgs e)
        {

        }

        protected void btnPanUpload_Click(object sender, EventArgs e)
        {

        }

        protected void lnkDnlEduc_Click(object sender, EventArgs e)
        {

        }

        protected void btnEducUpload_Click(object sender, EventArgs e)
        {

        }

        protected void lnkDnlCC_Click(object sender, EventArgs e)
        {

        }

        protected void btnCCUpload_Click(object sender, EventArgs e)
        {

        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = objOTP.tokengenerate();
                if (ds.Tables.Count != 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string COUNT = ds.Tables[0].Rows[0]["TOKEN_KEY"].ToString();
                        Session["access_token"] = COUNT;
                    }
                    else
                    {
                        RegisterAsyncTask(new PageAsyncTask(AsyncEventHandlertoken));
                    }

                }
                RegisterAsyncTask(new PageAsyncTask(AsyncEventHandler1));

                mv.SetActiveView(vwCreate);
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnValidatePAN_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAadharNo.Text == "")
                {
                    objCommon = new NewPortal2023.ESS.Common();
                    divAlertCreate.Visible = true;
                    string script = $@"<script type='text/javascript'>alert('Please Validate Aadhar First.');</script>";
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", script);
                    objCommon.SetMessageColor(divAlert, "danger");
                    lblMessageCreate.Text = "Please Validate Aadhar First";
                }
                else
                {
                    DataSet ds = new DataSet();
                    ds = objOTP.tokengenerate();
                    if (ds.Tables.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string COUNT = ds.Tables[0].Rows[0]["TOKEN_KEY"].ToString();
                            Session["access_token"] = COUNT;
                        }
                        else
                        {
                            RegisterAsyncTask(new PageAsyncTask(AsyncEventHandlertoken));
                        }

                    }
                    RegisterAsyncTask(new PageAsyncTask(AsyncEventHandlerpan));
                    //FillData();
                    mv.SetActiveView(vwCreate);
                }

            }
            catch (Exception ex)
            {

            }
        }

        public void FillData()
        {
            try
            {
                //objOTP.AadhaarNo = txtAadharNo.Text;
                //dsExp = objOTP.FillDetailsByAadhar();
                //if(dsExp.Tables[0].Rows[0]["result"].ToString()=="")
                //{

                txtAadharNoPer.Text = txtAadharNo.Text;
                txtPanName.Text = ViewState["panname"].ToString();
                txtAadharName.Text = ViewState["nameLabel"].ToString();
                txtDOB.Text = ViewState["dobLabel"].ToString();
                txtCountry.Text = ViewState["country"].ToString();
                txtState.Text = ViewState["state"].ToString();
                txtPIN.Text = ViewState["pincode"].ToString();
                txtAdd1.Text = ViewState["addressLabel"].ToString();
                Gender = ViewState["genderLabel"].ToString();

                if (Gender == "M")
                {
                    rdMale.Checked = true;
                    rdFemale.Checked = false;
                }
                else if (Gender == "F")
                {
                    rdFemale.Checked = true;
                    rdMale.Checked = false;
                }

                //}
            }
            catch (Exception ex)
            {
                objCommon = new NewPortal2023.ESS.Common();
                divAlert.Visible = true;
                objCommon.SetMessageColor(divAlert, "danger");
                lblMessage.Text = ex.Message;
            }
        }

        
    }
}
