using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class LeaveHistoryAdmin : System.Web.UI.Page
    {
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        //NewPortal2023.ESS.LeaveApproval objNps = new NewPortal2023.ESS.LeaveApproval();



        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string fromDate = txtFromDate.Text.Trim();
                string toDate = txtToDate.Text.Trim();

                string compAid = Session["sCompID"] != null ? Session["sCompID"].ToString() : "";

                if (string.IsNullOrEmpty(fromDate) || string.IsNullOrEmpty(toDate))
                {
                    lblMessage.Text = "Please select From Date and To Date";
                    divAlert.Visible = true;
                    return;
                }

                if (string.IsNullOrEmpty(compAid))
                {
                    lblMessage.Text = "Session expired. Please login again.";
                    divAlert.Visible = true;
                    return;
                }

                DataSet ds = objNps.GetLeaveHistoryData(compAid, fromDate, toDate);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ExportToExcel(ds.Tables[0], fromDate, toDate);
                }
                else
                {
                    lblMessage.Text = "No data found";
                    divAlert.Visible = true;
                }
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                divAlert.Visible = true;
            }
        }

       
        private void ExportToExcel(DataTable dt, string fromDate, string toDate)
        {
            Response.Clear();
            Response.Buffer = true;

            string fileName = "LeaveHistory_" + fromDate + "_to_" + toDate + ".xls";

            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.Charset = "";
            Response.ContentType = "application/ms-excel";

            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            GridView gv = new GridView();
            gv.DataSource = dt;
            gv.DataBind();

            gv.RenderControl(hw);

            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }
}