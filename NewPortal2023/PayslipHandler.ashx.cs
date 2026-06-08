using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace NewPortal2023
{
    /// <summary>
    /// Summary description for PayslipHandler
    /// </summary>
    public class PayslipHandler : IHttpHandler
    {
        private static readonly string secretKey = NewPortal2023.ESS.KeyManager.Get("secretKey");
        public void ProcessRequest(HttpContext context)
        {
            string file = HttpUtility.UrlDecode(context.Request.QueryString["file"]);

            string expiryTicks = context.Request.QueryString["expiry"];
            string sig = context.Request.QueryString["sig"];
        

            if (string.IsNullOrEmpty(file) || string.IsNullOrEmpty(expiryTicks) || string.IsNullOrEmpty(sig))
            {
                context.Response.StatusCode = 400;
                context.Response.Write("Invalid request");
                return;
            }


            if (!long.TryParse(expiryTicks, out long expiryUnix) || DateTimeOffset.UtcNow.ToUnixTimeSeconds() > expiryUnix)
            {
                context.Response.StatusCode = 403;
                context.Response.Write("Link expired");
                return;
            }



            string expectedSig = GenerateSignature(file, expiryTicks, secretKey);
            if (!string.Equals(sig, expectedSig, StringComparison.Ordinal))
            {
                context.Response.StatusCode = 403;
                context.Response.Write("Invalid signature");
                return;
            }

            string fileVirtualPath = context.Request.QueryString["file"];
            string filePath = context.Server.MapPath(fileVirtualPath);
            context.Response.ContentType = "application/pdf";
            context.Response.WriteFile(filePath);
            context.Response.End();
        }

        private static string GenerateSignature(string file, string expiryTicks, string secretKey)
        {
            string normalizedFile = file.Replace("\\", "/").Trim();
            string data = normalizedFile + "|" + expiryTicks;
            /*   string data = fileName + "|" + expiryTicks;*/
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey)))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(hash);
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}