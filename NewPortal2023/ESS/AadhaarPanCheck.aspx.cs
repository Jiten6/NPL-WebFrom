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

namespace NewPortal2023.ESS
{
    public partial class AadhaarPanCheck : System.Web.UI.Page
    {
        NewPortal2023.ESS.Expenses objExp = new NewPortal2023.ESS.Expenses();
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        DataSet dsExp = new DataSet();
        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        //RegisterAsyncTask(new PageAsyncTask(fetchDataAndDisplay));
        //        //fetchDataAndDisplay();
        //        await FetchDataAndDisplay();
        //    }
        //}
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
                //await FetchDataAndDisplay();
            }
        }



        private Boolean ValidateData()
        {

            if (txtfirst.Text.Trim() == "")
            {
                // lbl.Text = "Select From date.";
                divAlert.Visible = true;
                // objcommon.SetMessageColor(divAlertList, "danger");
                lblMessage.Text = "Enter Aadhaar No.";
                objcommon.Display("Validate", "DisplayErrorMessage('Enter 12 Digit Aadhaar No.');");
                return false;
            }
            if (txtSecond.Text.Trim() == "")
            {
                //lblMessage.Text = "Select To date.";
                divAlert.Visible = true;
                // objcommon.SetMessageColor(divAlertList, "danger");
                lblMessage.Text = "Enter Aadhaar No.";
                objcommon.Display("Validate", "DisplayErrorMessage('Enter 12 Digit Aadhaar No.');");
                return false;
            }
            if (txtThird.Text.Trim() == "")
            {
                //lblMessage.Text = "Select To date.";
                divAlert.Visible = true;
                // objcommon.SetMessageColor(divAlertList, "danger");
                lblMessage.Text = "Enter Aadhaar No.";
                objcommon.Display("Validate", "DisplayErrorMessage('Enter 12 Digit Aadhaar No.');");
                return false;
            }
            return true;
        }

        private Boolean ValidatePAN()
        {

            if (txtPanValidation.Text.Trim() == "")
            {
                // lbl.Text = "Select From date.";
                divAlert.Visible = true;
                // objcommon.SetMessageColor(divAlertList, "danger");
                lblMessage.Text = "Enter PAN No.";
                objcommon.Display("Validate", "DisplayErrorMessage('Enter 10 Digit PAN No.');");
                return false;
            }

            return true;
        }


        //protected void btnAadhaarCheck_Click(object sender, EventArgs e)
        //{
        //    if (ValidateData() == true)
        //    {

        //        string aadhaarNumber = txtfirst.Text + "-" + txtSecond.Text + "-" + txtThird.Text;
        //        objExp.AadhaarNo = txtfirst.Text + txtSecond.Text + txtThird.Text;

        //        // dsExp = objExp.GetAadhaarDetails();
        //        if (dsExp.Tables[0].Rows.Count > 0)
        //        {
        //            objcommon = new NewPortal2023.ESS.Common();
        //            divAlertSuccess.Visible = true;
        //            //  objcommon.SetMessageColor(divAlertList, "success");
        //            var clientId = "key_test_B0oSJv2dB5NfmFgoLEFojxDqFo1nbPSJ"; // Replace "YourAppId" with your actual App ID provided by UIDAI
        //            var clientSecret = "secret_test_IjpBwW2hFEPdyCLxuTgvk6WOPoZqub2s"; // Replace "YourClientSecret" with your actual Client Secret provided by UIDAI
        //            aadhaarNumber = "txtfirst.text" + "txtSecond.text" + "txtThird.text"; // Replace "AadhaarNumber" with the Aadhaar number you want to authenticate
        //            var otp = "OTP"; // Replace "OTP" with the OTP received for authentication

        //            using (var httpClient = new HttpClient())
        //            {
        //                var request = new HttpRequestMessage
        //                {
        //                    RequestUri = new Uri("https://auth.uidai.gov.in/otp/"),
        //                    Method = HttpMethod.Post,
        //                    Content = new StringContent($"aadhaar={aadhaarNumber}&otp={otp}"),
        //                    Headers =
        //                        {
        //                            { "x-request-id", Guid.NewGuid().ToString() }, // Generate a unique ID for the request
        //                            { "key_test_B0oSJv2dB5NfmFgoLEFojxDqFo1nbPSJ", clientId }, // Use your App ID provided by UIDAI
        //                            { "Accept", "application/json" } // Specify the desired response format
        //                        }
        //                };

        //                var authHeaderValue = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));
        //                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeaderValue);

        //                var response = await httpClient.SendAsync(request);
        //                var responseBody = await response.Content.ReadAsStringAsync();

        //                // Handle response
        //                Console.WriteLine(responseBody);
        //            }
        //            lblMessageSuccess.Text = dsExp.Tables[0].Rows[0]["EMP_NAME"].ToString();
        //            divAlertDanger.Visible = false;
        //            lblMessagedanger.Text = "";
        //        }
        //    }
        //    else
        //    {

        //        objcommon = new NewPortal2023.ESS.Common();
        //        divAlertDanger.Visible = true;
        //        // objcommon.SetMessageColor(divAlertList, "danger");
        //        lblMessagedanger.Text = "Not Found Aadhaar Card Holder Name. Please Check Aadhaar No.";
        //        divAlertSuccess.Visible = false;
        //        lblMessageSuccess.Text = "";
        //        // divAlertList.Visible = true;
        //    }
        //}

        protected void btnPanCheck_Click(object sender, EventArgs e)
        {
            //if (ValidatePAN() == true)
            //{
            //    // objExp.PanNo = txtPanValidation.Text;
            //    //dsExp = objExp.GetPanDetails();
            //    if (dsExp.Tables[0].Rows.Count > 0)
            //    {
            //        objcommon = new NewPortal2023.ESS.Common();
            //        divAlertSuccess.Visible = true;
            //        //objcommon.SetMessageColor(divAlertList, "success");
            //        lblMessageSuccess.Text = dsExp.Tables[0].Rows[0]["EMP_NAME"].ToString();
            //        divAlertDanger.Visible = false;
            //        lblMessagedanger.Text = "";
            //        //divAlertList.Visible = true;

            //    }
            //    else
            //    {

            //        objcommon = new NewPortal2023.ESS.Common();
            //        divAlertDanger.Visible = true;
            //        //objcommon.SetMessageColor(divAlertList, "danger");
            //        lblMessagedanger.Text = "Not Found PAN Card Holder Name. Please Check PAN No.";
            //        divAlertSuccess.Visible = false;
            //        lblMessageSuccess.Text = "";
            //        // divAlertList.Visible = true;
            //    }
            //}
            DataSet ds = new DataSet();
            ds = objExp.tokengenerate();
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


        }
        private async Task AsyncEventHandlerpan()
        {
            //var httpClient = new HttpClient();
            //httpClient.Timeout = TimeSpan.FromMinutes(60);

            //var pan_number = txtPanValidation.Text;

            ////var options = new RestClientOptions("https://api.sandbox.co.in/pans/{pan_number}/verify?consent=y&reason=For%20KYC%20of%20User");
            ////var client = new RestClient(options);
            //var url = $"https://api.sandbox.co.in/pans/{pan_number}/verify?consent=y&reason=For%20KYC%20of%20User";

            //var options = new RestClientOptions(url);
            //var client = new RestClient(options);
            //var request = new RestRequest("");
            //request.AddHeader("accept", "application/json");
            //request.AddHeader("Authorization", (string)Session["access_token"]);
            //request.AddHeader("x-api-key", "key_live_o854hOldVrJGbET81VcKQ7JL0HhKjPv5");
            //request.AddHeader("x-api-version", "1.0");
            ////string requestBodyJson = "{\"pan\": \"" + pan_number + "\"}";

            //request.AddJsonBody(request); // Pass the serialized JSON as the request body

            //var response = await client.GetAsync(request);
            //var responseData = response.Content;
            ////var responseData = await response.Content.ReadAsStringAsync();

            //dynamic responseObject = JsonConvert.DeserializeObject(responseData);

            //// Access specific properties and assign them to your label text
            //lblpan.Text = "PAN_NUMBER: " + responseObject.data.pan;
            //nameLabel.Text = "FULL_NAME: " + responseObject.data.full_name;
            //lblstatus.Text = "STATUS: " + responseObject.data.status;

            //Console.WriteLine("{0}", response.Content);
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(60);

            var pan_number = txtPanValidation.Text;

            //// Construct the URL with the pan_number variable
            ////var url = $"https://api.sandbox.co.in/pans/{pan_number}/verify?consent=y&reason=For%20KYC%20of%20User"; 


            var url = string.Format("https://api.sandbox.co.in/pans/{0}/verify?consent=y&reason=For%20KYC%20of%20User", pan_number);

            var options = new RestClientOptions(url);
            //var options = new RestClientOptions("https://api.sandbox.co.in/pans/" + pan_number + "/verify?consent=y&reason=For%20KYC%20of%20User", pan_number);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", "eyJhbGciOiJIUzUxMiJ9.eyJhdWQiOiJBUEkiLCJyZWZyZXNoX3Rva2VuIjoiZXlKaGJHY2lPaUpJVXpVeE1pSjkuZXlKaGRXUWlPaUpCVUVraUxDSnpkV0lpT2lKMFpXTm9jM1Z3Y0c5eWRFQnpaWEYxWld4bmNtOTFjQzVqYnk1cGJpSXNJbUZ3YVY5clpYa2lPaUpyWlhsZmJHbDJaVjl2T0RVMGFFOXNaRlp5U2tkaVJWUTRNVlpqUzFFM1Nrd3dTR2hMYWxCMk5TSXNJbWx6Y3lJNkltRndhUzV6WVc1a1ltOTRMbU52TG1sdUlpd2laWGh3SWpveE56TTVPVFExT0RFMExDSnBiblJsYm5RaU9pSlNSVVpTUlZOSVgxUlBTMFZPSWl3aWFXRjBJam94TnpBNE16SXpOREUwZlEueFFYdDQ4MEdzcnFIMW1yX19jUldXQlV5ZXdjWDFWaV9IOWZTSFRxakduZWhhd0VXeE9IaWpOUzFRWVNVM19ZOFBwamI2dUM4ejRTZ1pUbGlLYm8yNXciLCJzdWIiOiJ0ZWNoc3VwcG9ydEBzZXF1ZWxncm91cC5jby5pbiIsImFwaV9rZXkiOiJrZXlfbGl2ZV9vODU0aE9sZFZySkdiRVQ4MVZjS1E3SkwwSGhLalB2NSIsImlzcyI6ImFwaS5zYW5kYm94LmNvLmluIiwiZXhwIjoxNzA4NDA5ODE0LCJpbnRlbnQiOiJBQ0NFU1NfVE9LRU4iLCJpYXQiOjE3MDgzMjM0MTR9.q71BjkQz6g_uDhuDj9Yy8xdZ2LIKA5E_wnz_fivPtdbsWOFvzTNsrz_L4MQnMxO-nTZpKG3wbHv4hO1HLxpPZw");
            request.AddHeader("x-api-key", "key_live_o854hOldVrJGbET81VcKQ7JL0HhKjPv5");
            request.AddHeader("x-api-version", "1.0");
            var response = await client.GetAsync(request);

            var responseData = response.Content;
            //var responseData = await response.Content.ReadAsStringAsync();

            dynamic responseObject = JsonConvert.DeserializeObject(responseData);
            //Console.WriteLine("{0}", response.Content);
            // dynamic responseObject = JsonConvert.DeserializeObject(responseData);
            lblpan.Text = "PAN_NUMBER: " + responseObject.data.pan;
            Lblpanname.Text = "AND YOUR FULL_NAME: " + responseObject.data.full_name;
            divAlert.Visible = true;
            lblMessage.Text = lblpan.Text + " " + Lblpanname.Text;
            objcommon.SetMessageColor(divAlert, "success");
            //lblstatus.Text = "STATUS: " + responseObject.data.status;
            //var options = new RestClientOptions(url);
            //var client = new RestClient(options);
            //var request = new RestRequest("");
            //request.AddHeader("accept", "application/json");
            //request.AddHeader("Authorization", (string)Session["access_token"]);
            //request.AddHeader("x-api-key", "key_live_o854hOldVrJGbET81VcKQ7JL0HhKjPv5");
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


        protected void btnAadhar_Click(object sender, EventArgs e)
        {
            divAadhaar.Visible = true;
            divPan.Visible = false;
            divAlert.Visible = false;
            Clear();
        }

        protected void btnPan_Click(object sender, EventArgs e)
        {
            divPan.Visible = true;
            divAadhaar.Visible = false;
            divAlert.Visible = false;
            Clear();
        }

        public void Clear()
        {
            txtfirst.Text = "";
            txtSecond.Text = "";
            txtThird.Text = "";
            txtPanValidation.Text = "";
            divAlert.Visible = false;
            lblMessage.Text = "";
            
        }
        protected void btnAadhaarCheck_Click(object sender, EventArgs e)
        {
            // Call an asynchronous method and register a continuation
            DataSet ds = new DataSet();
            ds = objExp.tokengenerate();
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
            //string aadharNumber = "123456789012"; // Replace this with the Aadhaar number you want to fetch the name for
            //string uidaiEndpoint = "https://stage1.uidai.gov.in/onlineekyc/getEkyc/";

            //var client = new RestClient(uidaiEndpoint);
            //var request = new RestRequest(Method.POST);

            //request.AddHeader("content-type", "application/json");

            //// Aadhaar number is sent as part of the request
            //JObject requestBody = new JObject();
            //requestBody.Add("aadhaar", aadharNumber);

            //request.AddParameter("application/json", requestBody.ToString(), ParameterType.RequestBody);

            //RestResponse response = client.Execute(request);

            //if (response.StatusCode == HttpStatusCode.OK)
            //{
            //    JObject responseData = JObject.Parse(response.Content);

            //    if (responseData["success"].ToObject<bool>())
            //    {
            //        string name = responseData["name"].ToString();
            //        Console.WriteLine($"Name: {name}");
            //    }
            //    else
            //    {
            //        string error = responseData["message"].ToString();
            //        Console.WriteLine($"Failed to fetch name. Error: {error}");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine($"Failed to fetch name. Status code: {response.StatusCode}");
            //}

        }
        private async Task AsyncEventHandlertoken()
        {

            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(60);

            var options = new RestClientOptions("https://api.sandbox.co.in/authenticate");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("x-api-key", "key_live_o854hOldVrJGbET81VcKQ7JL0HhKjPv5");
            request.AddHeader("x-api-secret", "secret_live_eemukbhXcDRNhQTsKxZFkUJoxzTCWd6n");
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
            ds = objExp.tokenINSERT((string)Session["access_token"]);
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
            //     string AadhaarNo = txtfirst.Text + txtSecond.Text + txtThird.Text;
            //    // Your asynchronous code goes here
            //    var clientId = "key_live_o854hOldVrJGbET81VcKQ7JL0HhKjPv5";
            //    var clientSecret = "secret_live_eemukbhXcDRNhQTsKxZFkUJoxzTCWd6n";
            //    var aadhaarNumber = "4417-1969-4030";
            //    var otp = "OTP";

            //    using (var httpClient = new HttpClient())
            //    {
            //        //var request = new HttpRequestMessage
            //        //{
            //        //    RequestUri = new Uri("uidai_authentication_api_endpoint"),
            //        //    Method = HttpMethod.Post,
            //        //    Content = new StringContent($"aadhaar={aadhaarNumber}&otp={otp}"),
            //        //    Headers =
            //        //    {
            //        //        { "x-request-id", Guid.NewGuid().ToString() },
            //        //        { "key_test_B0oSJv2dB5NfmFgoLEFojxDqFo1nbPSJ", clientId },
            //        //        { "Accept", "application/json" }
            //        //    }
            //        //};
            //        var requestUri = "https://api.sandbox.co.in/kyc/aadhaar"; // Replace with the actual UIDAI authentication API endpoint

            //        var request = new HttpRequestMessage
            //        {
            //            RequestUri = new Uri(requestUri),
            //            Method = HttpMethod.Post,
            //            Content = new StringContent($"aadhaar={aadhaarNumber}&otp={otp}", Encoding.UTF8, "application/x-www-form-urlencoded")
            //        };

            //        // Add headers
            //        request.Headers.Add("x-request-id", Guid.NewGuid().ToString());
            //        request.Headers.Add("Authorization", "Bearer key_live_o854hOldVrJGbET81VcKQ7JL0HhKjPv5"); // Replace "YourAccessToken" with the actual access token or API key
            //        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //        var authHeaderValue = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{clientId}:{clientSecret}"));
            //        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authHeaderValue);

            //        var response = await httpClient.SendAsync(request);
            //        var responseBody = await response.Content.ReadAsStringAsync();

            //        // Handle response
            //        // You can update UI elements or perform other operations based on the response
            //        // Example: lblResult.Text = responseBody;
            //    }
            //using System.Net.Http.Headers;
            //var client = new HttpClient();
            //var request = new HttpRequestMessage
            //{
            //    Method = HttpMethod.Post,
            //    RequestUri = new Uri("https://api.sandbox.co.in/kyc/aadhaar/okyc/otp"),
            //    Headers =
            //    {
            //        { "accept", "application/json" },
            //        { "Authorization", "eyJhbGciOiJIUzUxMiJ9.eyJhdWQiOiJBUEkiLCJyZWZyZXNoX3Rva2VuIjoiZXlKaGJHY2lPaUpJVXpVeE1pSjkuZXlKaGRXUWlPaUpCVUVraUxDSnpkV0lpT2lKMFpXTm9jM1Z3Y0c5eWRFQnpaWEYxWld4bmNtOTFjQzVqYnk1cGJpSXNJbUZ3YVY5clpYa2lPaUpyWlhsZmRHVnpkRjlDTUc5VFNuWXlaRUkxVG1adFJtZHZURVZHYjJwNFJIRkdiekZ1WWxCVFNpSXNJbWx6Y3lJNkltRndhUzV6WVc1a1ltOTRMbU52TG1sdUlpd2laWGh3SWpveE56TTVOekUwT1RjeExDSnBiblJsYm5RaU9pSlNSVVpTUlZOSVgxUlBTMFZPSWl3aWFXRjBJam94TnpBNE1Ea3lOVGN4ZlEud1pHUTlnZzdRdjdfVkFsZGFHVFgtb21ETVp6VDNqRVhUT0VXZXRnRXowZS0zVW1YbjJxUnpybkNMbEg1SlpIeDdtTTVIUkRhcm5SeEpweFUtMF8xTEEiLCJzdWIiOiJ0ZWNoc3VwcG9ydEBzZXF1ZWxncm91cC5jby5pbiIsImFwaV9rZXkiOiJrZXlfdGVzdF9CMG9TSnYyZEI1TmZtRmdvTEVGb2p4RHFGbzFuYlBTSiIsImlzcyI6ImFwaS5zYW5kYm94LmNvLmluIiwiZXhwIjoxNzA4MTc4OTcxLCJpbnRlbnQiOiJBQ0NFU1NfVE9LRU4iLCJpYXQiOjE3MDgwOTI1NzF9.dVJ2IiczwRG8Y3-tqmDmIelRDNjq4vBG3fi3kALlzPYS1H56tIiA578ugCNvCzK5w_AtlOxv5SY56uGqVIRrjw" },
            //        { "x-api-key", "key_test_B0oSJv2dB5NfmFgoLEFojxDqFo1nbPSJ" },
            //        { "x-api-version", "1.0" },
            //    },
            //    Content = new StringContent("{\"aadhaar_number\":\"441719694030\"}")
            //    {
            //        Headers =
            //        {
            //            ContentType = new MediaTypeHeaderValue("application/json")
            //        }
            //    }
            //};
            //using (var response = await client.SendAsync(request))
            //{
            //    response.EnsureSuccessStatusCode();
            //    var body = await response.Content.ReadAsStringAsync();
            //    Console.WriteLine(body);
            //}
            //    string aadharNumber = "441719694030"; // Replace this with the Aadhaar number you want to fetch the name for

            //string apiUrl = "https://uidai.gov.in/en/my-aadhaar/avail-aadhaar-services.html"; // Replace this with the actual Aadhaar API URL
            //string apiKey = "key_live_o854hOldVrJGbET81VcKQ7JL0HhKjPv5"; // Replace with your API key

            //HttpClient client = new HttpClient();

            //var requestUrl = $"{apiUrl}?aadharNumber={aadharNumber}&apiKey={apiKey}";

            //var response = await client.GetAsync(requestUrl);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        var result = await response.Content.ReadAsStringAsync();
            //        Console.WriteLine($"Name: {result}");
            //        lblMessageSuccess.Text = ($"Name: {result}");
            //    }
            //    else
            //    {
            //        Console.WriteLine($"Failed to fetch name. Status code: {response.StatusCode}");
            //    }
            var httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromMinutes(60);

            var aadhaarNumber = txtfirst.Text + txtSecond.Text + txtThird.Text;

            var options = new RestClientOptions("https://api.sandbox.co.in/kyc/aadhaar/okyc/otp");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("Authorization", (string)Session["access_token"]);
            request.AddHeader("x-api-key", "key_live_o854hOldVrJGbET81VcKQ7JL0HhKjPv5");
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
        }
        protected void btnveryfy_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(AsyncEventHandler1));
            //var options = new RestClientOptions("https://api.sandbox.co.in/kyc/aadhaar/okyc/otp/verify");
            //var client = new RestClient(options);
            //var request = new RestRequest("");
            //request.AddHeader("accept", "application/json");
            //request.AddHeader("Authorization", "eyJhbGciOiJIUzUxMiJ9");
            //request.AddHeader("x-api-key", "key_live_o854hOldVrJGbET81VcKQ7JL0HhKjPv5");
            //request.AddHeader("x-api-version", "1.0");
            //request.AddJsonBody("{\"ref_id\":\"10563564\",\"otp\":\"505358\"}", false);
            //var response = await client.PostAsync(request);

            //Console.WriteLine("{0}", response.Content);

        }
        //private async Task AsyncEventHandler1()
        //{



        //    var options = new RestClientOptions("https://api.sandbox.co.in/kyc/aadhaar/okyc/otp/verify");
        //    var client = new RestClient(options);
        //    var request = new RestRequest("");
        //    request.AddHeader("accept", "application/json");
        //    request.AddHeader("Authorization", "eyJhbGciOiJIUzUxMiJ9.eyJhdWQiOiJBUEkiLCJyZWZyZXNoX3Rva2VuIjoiZXlKaGJHY2lPaUpJVXpVeE1pSjkuZXlKaGRXUWlPaUpCVUVraUxDSnpkV0lpT2lKMFpXTm9jM1Z3Y0c5eWRFQnpaWEYxWld4bmNtOTFjQzVqYnk1cGJpSXNJbUZ3YVY5clpYa2lPaUpyWlhsZmJHbDJaVjl2T0RVMGFFOXNaRlp5U2tkaVJWUTRNVlpqUzFFM1Nrd3dTR2hMYWxCMk5TSXNJbWx6Y3lJNkltRndhUzV6WVc1a1ltOTRMbU52TG1sdUlpd2laWGh3SWpveE56TTVOemt5T0RjeUxDSnBiblJsYm5RaU9pSlNSVVpTUlZOSVgxUlBTMFZPSWl3aWFXRjBJam94TnpBNE1UY3dORGN5ZlEuSHBQYUJHZXIxRUF6UHRjVmdKX09KU3NuU0VRT19nUWtUeFFFNThWNzQzbWNLcUlrbVpnYlpHckcxS1l2YU1Lc1paeS04WHVkM3BPSGF6UW9rYUxKUUEiLCJzdWIiOiJ0ZWNoc3VwcG9ydEBzZXF1ZWxncm91cC5jby5pbiIsImFwaV9rZXkiOiJrZXlfbGl2ZV9vODU0aE9sZFZySkdiRVQ4MVZjS1E3SkwwSGhLalB2NSIsImlzcyI6ImFwaS5zYW5kYm94LmNvLmluIiwiZXhwIjoxNzA4MjU2ODcyLCJpbnRlbnQiOiJBQ0NFU1NfVE9LRU4iLCJpYXQiOjE3MDgxNzA0NzJ9.u9LQQ72Rq9tRLIle-_Ld_C-InQdwEBzwEimr-IjTtBoL23uFB8Utvat02kY0m0BzOwwsIyCI4Mj4zWACiH_GKQ");
        //    request.AddHeader("x-api-key", "key_live_o854hOldVrJGbET81VcKQ7JL0HhKjPv5");
        //    request.AddHeader("x-api-version", "1.0");
        //    request.AddJsonBody("{\"ref_id\":\"10570701\",\"otp\":\"865304\"}", false);
        //    var response = await client.PostAsync(request);



        //    Console.WriteLine("{0}", response.Content);

        //    RegisterAsyncTask(new PageAsyncTask(FetchDataAndDisplay));

        //}

        private async Task AsyncEventHandler1()
        {
            try
            {

                var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromMinutes(60);

                var mobile_otp = txtveryfy.Text;

                var options = new RestClientOptions("https://api.sandbox.co.in/kyc/aadhaar/okyc/otp/verify");
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.AddHeader("accept", "application/json");
                request.AddHeader("Authorization", (string)Session["access_token"]);
                request.AddHeader("x-api-key", "key_live_o854hOldVrJGbET81VcKQ7JL0HhKjPv5");
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
                nameLabel.Text = "Name: " + responseObject.data.name;
                genderLabel.Text = "Gender: " + responseObject.data.gender;
                dobLabel.Text = "Date of Birth: " + responseObject.data.dob;
                addressLabel.Text = "Address: " + responseObject.data.address;

                dsExp = objExp.fillAdhharData(nameLabel.Text, genderLabel.Text, dobLabel.Text, addressLabel.Text);
            }
            catch (Exception ex)
            {
                // Handle exception
                errorLabel.Text = "An error occurred: " + ex.Message;
            }
        }

        

        public class ResponseData
        {
            public string SomeField { get; set; }
            // Add more properties as needed to match your JSON structure
        }

        // Deserialize the JSON response
        //protected async Task fetchDataAndDisplay()
        //{
        //    try
        //    {
        //        using (var httpClient = new HttpClient())
        //        {
        //            var requestData = new
        //            {
        //                ref_id = "10568133",
        //                otp = "279303"
        //            };

        //            var jsonRequest = JsonConvert.SerializeObject(requestData);

        //            var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

        //            var response = await httpClient.PostAsync("https://api.sandbox.co.in/kyc/aadhaar/okyc/otp/verify", content);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var responseData = await response.Content.ReadAsStringAsync();

        //                dynamic responseObject = JsonConvert.DeserializeObject(responseData);

        //                // Update labels with data from the response
        //                nameLabel.Text = "Name: " + responseObject.data.name;
        //                genderLabel.Text = "Gender: " + responseObject.data.gender;
        //                dobLabel.Text = "Date of Birth: " + responseObject.data.dob;
        //                addressLabel.Text = "Address: " + responseObject.data.address;
        //            }
        //            else
        //            {
        //                // Handle error
        //                // For example, you can display an error message
        //                errorLabel.Text = "Error fetching data from API.";
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle exception
        //        // For example, you can log the exception or display an error message
        //        errorLabel.Text = "An error occurred: " + ex.Message;
        //    }
        //}
        //private void fetchDataAndDisplay()
        //{
        //    Task.Run(async () =>
        //    {
        //        try
        //        {
        //            using (var httpClient = new HttpClient())
        //            {
        //                var requestData = new
        //                {
        //                    ref_id = "10570272",
        //                    otp = "828449"
        //                };

        //                var jsonRequest = JsonConvert.SerializeObject(requestData);

        //                var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

        //                var response = await httpClient.PostAsync("https://api.sandbox.co.in/kyc/aadhaar/okyc/otp/verify", content);

        //                if (response.IsSuccessStatusCode)
        //                {
        //                    var responseData = await response.Content.ReadAsStringAsync();

        //                    dynamic responseObject = JsonConvert.DeserializeObject(responseData);

        //                    // Update labels with data from the response
        //                    nameLabel.Text = "Name: " + responseObject.data.name;
        //                    genderLabel.Text = "Gender: " + responseObject.data.gender;
        //                    dobLabel.Text = "Date of Birth: " + responseObject.data.dob;
        //                    addressLabel.Text = "Address: " + responseObject.data.address;
        //                }
        //                else
        //                {
        //                    // Handle error
        //                    // For example, you can display an error message
        //                    errorLabel.Text = "Error fetching data from API.";
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // Handle exception
        //            // For example, you can log the exception or display an error message
        //            errorLabel.Text = "An error occurred: " + ex.Message;
        //        }
        //    });
        //}
        protected async Task FetchDataAndDisplay()
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var requestData = new
                    {
                        ref_id = "10571040",
                        otp = "172437"
                    };

                    var jsonRequest = JsonConvert.SerializeObject(requestData);

                    var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

                    var response = await httpClient.PostAsync("https://api.sandbox.co.in/kyc/aadhaar/okyc/otp/verify", content);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseData = await response.Content.ReadAsStringAsync();

                        dynamic responseObject = JsonConvert.DeserializeObject(responseData);

                        // Access specific properties and assign them to your label text
                        nameLabel.Text = "Name: " + responseObject.data.name;
                        genderLabel.Text = "Gender: " + responseObject.data.gender;
                        dobLabel.Text = "Date of Birth: " + responseObject.data.dob;
                        addressLabel.Text = "Address: " + responseObject.data.address;
                    }
                    else
                    {
                        // Handle error
                        errorLabel.Text = "Error fetching data from API.";
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                errorLabel.Text = "An error occurred: " + ex.Message;
            }
        }


    }

}