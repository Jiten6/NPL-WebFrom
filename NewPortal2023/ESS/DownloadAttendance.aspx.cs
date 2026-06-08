using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class DownloadAttendance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            byte[] bytes = Session["reportBytes"] as byte[];
            string contentType = Session["contentType"] as string;
            string fileName = Session["filename"] as string;

            if (bytes != null)
            {
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = contentType;
                Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }




        }

      

    }
}