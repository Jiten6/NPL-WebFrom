using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Data.SqlClient;
using System.Data;


namespace NewPortal2023.ESS
{
    public partial class Login : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.Common objBl = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.PmsHr objPmsHr = new NewPortal2023.ESS.PmsHr();
        NewPortal2023.ESS.LoginParams objUser = new NewPortal2023.ESS.LoginParams();
        DataSet ds = new DataSet();
        


        protected void Page_Load(object sender, EventArgs e)
        {

            
                Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
                Response.Cache.SetValidUntilExpires(false);
                Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();

            if ((string)Session["LoginError"] != null && (string)Session["LoginError"] == "NOMENU")
            {
                lblMessage.Text = "Action invalid.";
                Session.RemoveAll();
                Session.Abandon();
            }

            if (Session["PasswordMsg"] != null)
            {
                lblMessage.Text = Session["PasswordMsg"].ToString();
                Session["PasswordMsg"] = null;
            }

        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                int cnt =0;
                if (cnt == 0)
                {
                    if (ValidateLogin() == false)
                    {
                        return;
                    }

                    txtEmpCode.Text = this.Decrypt2(Request.Form[txtEmpCode.UniqueID]);
                    txtPassword.Text = this.Decrypt(Request.Form[txtPassword.UniqueID]);

                    objBl.Validate_Login(txtCompCode.Text, txtEmpCode.Text.ToUpper(), txtPassword.Text, objUser);

                    if (objUser.EmpId != null)
                    {
                        if (objUser.EmpId.ToString() != "")
                        {
                            HttpCookie loginCookie = new HttpCookie("Login");

                            loginCookie.Values["sEmpID"] = objUser.EmpId.Trim();
                            loginCookie.Values["sCompID"] = objUser.CompId.Trim();
                            loginCookie.Values["sCompAID"] = objUser.CompsName.Trim();
                            loginCookie.Values["sEmpCode"] = objUser.EmpCode.Trim();
                            loginCookie.Values["sEmpName"] = objUser.EmpName.Trim();
                            loginCookie.Values["sJoinDate"] = objUser.JoinDate.Trim();
                            loginCookie.Values["sPAN"] = objUser.PAN.Trim();
                            loginCookie.Values["sLastLogin"] = objUser.LastLogin.Trim();
                            loginCookie.Path = Request.ApplicationPath;

                            //Set the Expiry date.
                            loginCookie.Expires = DateTime.Now.AddDays(1);
                            Response.Cookies.Add(loginCookie);

                            Session["sEmpID"] = objUser.EmpId.Trim();
                            Session["sCompID"] = objUser.CompId.Trim();
                            Session["sEmpCode"] = objUser.EmpCode.Trim();
                            Session["sEmpName"] = objUser.EmpName.Trim();
                            Session["sEmpSex"] = objUser.EmpSex.Trim();
                            Session["sDesignation"] = objUser.Designation.Trim();
                            Session["sLocation"] = objUser.Location.Trim();
                            Session["sJoinDate"] = objUser.JoinDate.Trim();
                            Session["sPAN"] = objUser.PAN.Trim();
                            Session["SMSURL"] = objUser.SMSURL.Trim();
                            Session["sGrade"] = objUser.Grade.Trim();
                            Session["sEmailId"] = objUser.EmailId.Trim();
                            Session["sLastLogin"] = objUser.LastLogin.Trim();
                            Session["sAdmin"] = objUser.IsAdmin.Trim();
                            Session["sCarLease"] = objUser.CarLeaseLimit.Trim();
                            Session["sRole"] = objUser.Role.Trim();
                            Session["IsLogin"] = true;
                            Session["sCompAID"] = objUser.CompsName.Trim();
                            //string compAId = "";
                            Session["QueryStringEmpCode"] = "";
                            Session["CompName"] = "";
                            Session["sEmpType"] = objUser.EmpType.Trim();

                            //compAId = Session["sCompID"].ToString();

                            //char[] cArray = compAId.ToCharArray();
                            //string reverse = String.Empty;
                            //for (int i = cArray.Length - 1; i > -1; i--)
                            //{
                            //    reverse += cArray[i];
                            //}



                            FormsAuthentication.SetAuthCookie(objUser.EmpName.Trim(), false);

                            objBl.UpdateLoginDateTime((string)Session["sCompID"], (string)Session["sEmpCode"], DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));

                            objPmsHr.CheckPMSdates();   //For auto closing PMS Phases as per activation dates

                            Response.Redirect("../ESS/Default.aspx");

                            //}
                        }
                        else
                        {
                            lblMessage.Text = "Employee Code or Password invalid.";
                            //objcommon.Display("Validate", "DisplayErrorMessage('Employee does not exists.');");
                        }
                    }
                    else
                    {
                        lblMessage.Text = "Employee Code or Password invalid.";
                        //objcommon.Display("Validate", "DisplayErrorMessage('Employee does not exists.');");

                    }
                }
                else
                {
                    lblMessage.Text = "The server is temporarily unable to service your request due to maintenance downtime.";
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Error occurred in application.";
            }
        }

        private Boolean ValidateLogin()
        {
            if (txtCompCode.Text.Trim() == "")
            {
                lblMessage.Text = "Enter company code.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Enter company code.');");
                return false;
            }
            if (txtCompCode.Text.Trim().ToUpper() == "HRRL")
            {
                lblMessage.Text = "Action Invalid.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Enter company code.');");
                return false;
            }
            if (txtEmpCode.Text.Trim() == "")
            {
                lblMessage.Text = "Enter employee code.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Enter employee code');");
                return false;
            }
            if (txtPassword.Text.Trim() == "")
            {
                lblMessage.Text = "Enter password.";
                //objcommon.Display("Validate", "DisplayErrorMessage('Enter Password.');");
                return false;
            }
            return true;

        }

        private string Decrypt(string encryptedText)
        {
            //Secret Key.
            string secretKey = "$DOTtEqMONgcPOPoADa0QWq#";

            //Secret Bytes.
            byte[] secretBytes = Encoding.UTF8.GetBytes(secretKey);

            //Encrypted Bytes.
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);

            //Decrypt with AES Alogorithm using Secret Key.
            using (Aes aes = Aes.Create())
            {
                aes.Key = secretBytes;
                aes.Mode = CipherMode.ECB;
                aes.Padding = PaddingMode.PKCS7;

                byte[] decryptedBytes = null;
                using (ICryptoTransform decryptor = aes.CreateDecryptor())
                {
                    decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                }
                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
        private string Decrypt2(string encryptedText)
        {
            try
            {
                // Secret Key (must be 16, 24, or 32 bytes)
                string secretKey1 = "$QRTaQqSAMdtDODoAFa0QWq#";

                // Convert secret key to bytes (24 bytes)
                byte[] keyBytes = new byte[24];
                byte[] rawKeyBytes = Encoding.UTF8.GetBytes(secretKey1);
                Array.Copy(rawKeyBytes, keyBytes, Math.Min(rawKeyBytes.Length, keyBytes.Length));

                // Clean up and fix padding on Base64 string
                encryptedText = encryptedText.Trim().Replace(" ", "+");  // Ensure proper Base64 format
                int mod4 = encryptedText.Length % 4;
                if (mod4 > 0)
                {
                    encryptedText += new string('=', 4 - mod4);  // Add padding if necessary
                }

                // Decode Base64
                byte[] encryptedBytes;
                try
                {
                    encryptedBytes = Convert.FromBase64String(encryptedText);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid Base64 string.");
                    return string.Empty;
                }

                // Validate AES block length
                if (encryptedBytes.Length % 16 != 0)
                {
                    Console.WriteLine("Encrypted data is not a multiple of AES block size.");
                    return string.Empty;
                }

                // Decrypt using AES
                using (Aes aes = Aes.Create())
                {
                    aes.Key = keyBytes;
                    aes.Mode = CipherMode.ECB;
                    aes.Padding = PaddingMode.PKCS7;

                    using (ICryptoTransform decryptor = aes.CreateDecryptor())
                    {
                        byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
                        return Encoding.UTF8.GetString(decryptedBytes);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Decryption failed: " + ex.Message);
                return string.Empty;
            }
        }
    }
}