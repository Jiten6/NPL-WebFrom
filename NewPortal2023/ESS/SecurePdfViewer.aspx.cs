using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;


namespace NewPortal2023.ESS
{
    public partial class TEST : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            String pdfPath = Session["pdfPath"].ToString();
            if (!File.Exists(pdfPath))
            {
                Response.StatusCode = 404;
                Response.End();
                return;
            }

            byte[] fileBytes = File.ReadAllBytes(pdfPath);

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "inline; filename=UserReport.pdf");
            Response.OutputStream.Write(fileBytes, 0, fileBytes.Length);
            Response.Flush();
            Response.End();
        }

    }

    
}