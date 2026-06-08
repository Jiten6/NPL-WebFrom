using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NewPortal2023.ESS
{
    public partial class LeaveCardAdmin : System.Web.UI.Page
    {

        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        NewPortal2023.ESS.LeaveApproval objLeave = new NewPortal2023.ESS.LeaveApproval();

        DataSet ds = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindYearDropdown();
            }
        }

        private void BindYearDropdown()
        {
            ddlYear.Items.Clear();
            ddlYear.Items.Add(new ListItem("--Select Year--", ""));

            int startYear = 2021;
            int currentYear = DateTime.Now.Year;  

            for (int i = startYear; i <= currentYear; i++)
            {
                ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                string year = ddlYear.SelectedValue;
                string month = ddlMonth.SelectedValue;

                if (string.IsNullOrEmpty(year) || string.IsNullOrEmpty(month))
                {
                    lblMessage.Text = "Please select Year and Month";
                    divAlert.Visible = true;
                    return;
                }

                 ds = objLeave.GetLeaveCardData(year, month);
               

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ExportToExcel(ds.Tables[0], year, month);
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

       
        

       
        private void ExportToExcel(DataTable dt, string year, string month)
        {
            Response.Clear();
            Response.Buffer = true;

            string fileName = "LeaveCard_" + year + "_" + month + ".xls";

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

       
        public override void VerifyRenderingInServerForm(Control control)
        {
           
        }
    }
}