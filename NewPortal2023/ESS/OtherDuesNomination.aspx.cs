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
using Microsoft.Reporting.WebForms;


namespace NewPortal2023.ESS
{
    public partial class OtherDuesNomination : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sCompID"]!=null)
            {
            if (!Page.IsPostBack)
            {
                if (Session["sCompID"] != null)
                {
                    gvNominees.DataSource = null;
                    gvNominees.DataBind();
                    gvNomineesCont.DataSource = null;
                    gvNomineesCont.DataBind();
                }
            }
        }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {

        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {

        }

        protected void btnUploadForm_Click(object sender, EventArgs e)
        {

        }

        protected void btnDownloadForm_Click(object sender, EventArgs e)
        {

        }

        private DataTable CreateEmptyStructure()
        {
            DataTable dtDocuments = new DataTable();
            dtDocuments.Columns.Add(new DataColumn("NOMINEE_NAME", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("NOMINEE_ADDR", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("RELATIONSHIP", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("NOMINEE_AGE", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("PERCANTAGE", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("GUARDIAN", System.Type.GetType("System.String")));
            dtDocuments.Columns.Add(new DataColumn("Doc_Data_Id", System.Type.GetType("System.String")));

            return dtDocuments;
        }

        protected void lnkBtnAddDocRow_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCOInfo = CreateEmptyStructure();
                //objCommon = new ESS.Common();
                //lblMessage.Text = "";

                Int32 rowCount = 1;

                foreach (GridViewRow gvrCO in this.gvNominees.Rows)
                {
                    DataRow drCORow = dtCOInfo.NewRow();

                    drCORow["NOMINEE_NAME"] = ((TextBox)gvrCO.FindControl("txtNomineeName")).Text;
                    drCORow["NOMINEE_ADDR"] = ((TextBox)gvrCO.FindControl("txtNomineeAddr")).Text;
                    drCORow["RELATIONSHIP"] = ((DropDownList)gvrCO.FindControl("drpRelationship")).Text;
                    drCORow["NOMINEE_AGE"] = ((TextBox)gvrCO.FindControl("txtNomineeAge")).Text;
                    drCORow["PERCANTAGE"] = ((TextBox)gvrCO.FindControl("txtNomineePerc")).Text;
                    drCORow["GUARDIAN"] = ((TextBox)gvrCO.FindControl("txtNomineeGuardian")).Text;
                    drCORow["Doc_Data_Id"] = ((Label)gvrCO.FindControl("lblDocDataId")).Text;

                    dtCOInfo.Rows.Add(drCORow);

                    rowCount += 1;
                }

                DataRow drNewCORow = dtCOInfo.NewRow();
                drNewCORow["NOMINEE_NAME"] = "";
                drNewCORow["NOMINEE_ADDR"] = "";
                drNewCORow["RELATIONSHIP"] = "";
                drNewCORow["NOMINEE_AGE"] = "";
                drNewCORow["PERCANTAGE"] = "";
                drNewCORow["GUARDIAN"] = "";
                drNewCORow["Doc_Data_Id"] = Guid.NewGuid().ToString();
                dtCOInfo.Rows.Add(drNewCORow);

                ViewState["Nominees"] = dtCOInfo;
                this.gvNominees.DataSource = dtCOInfo;
                this.gvNominees.DataBind();
            }
            catch (Exception ex)
            {
                //objCommon = new ESS.Common();
                //lblMessage.Text = ex.Message;
                //objCommon.Display("Validate", "DisplayErrorMessage('" + objCommon.EncodeJsString(ex.Message) + "');");
            }
        }

        protected void lnkBtnDeleteDocRow_Click(object sender, EventArgs e)
        {
            try
            {
                int rowCount = 1;

                DataTable dtCOInfo = CreateEmptyStructure();
                //lblMessage.Text = "";

                foreach (GridViewRow gvrCO in this.gvNominees.Rows)
                {
                    if (((CheckBox)gvrCO.FindControl("chkSelect")).Checked == false)
                    {
                        DataRow drCORow = dtCOInfo.NewRow();

                        drCORow["NOMINEE_NAME"] = ((TextBox)gvrCO.FindControl("txtNomineeName")).Text;
                        drCORow["NOMINEE_ADDR"] = ((TextBox)gvrCO.FindControl("txtNomineeAddr")).Text;
                        drCORow["RELATIONSHIP"] = ((DropDownList)gvrCO.FindControl("drpRelationship")).Text;
                        drCORow["NOMINEE_AGE"] = ((TextBox)gvrCO.FindControl("txtNomineeAge")).Text;
                        drCORow["PERCANTAGE"] = ((TextBox)gvrCO.FindControl("txtNomineePerc")).Text;
                        drCORow["GUARDIAN"] = ((TextBox)gvrCO.FindControl("txtNomineeGuardian")).Text;
                        drCORow["Doc_Data_Id"] = ((Label)gvrCO.FindControl("lblDocDataId")).Text;

                        dtCOInfo.Rows.Add(drCORow);

                        rowCount++;
                    }
                }

                ViewState["Nominees"] = dtCOInfo;
                this.gvNominees.DataSource = (DataTable)ViewState["Nominees"];
                this.gvNominees.DataBind();
            }
            catch (Exception ex)
            {
                //objCommon = new ESS.Common();
                //lblMessage.Text = "Error occurred in application.";
                //objCommon.Display("Validate", "DisplayErrorMessage('" + objCommon.EncodeJsString(ex.Message) + "');");
            }
        }

        protected void lnkBtnAddDocRowCont_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtCOInfo = CreateEmptyStructure();
                //objCommon = new ESS.Common();
                //lblMessage.Text = "";

                Int32 rowCount = 1;

                foreach (GridViewRow gvrCO in this.gvNomineesCont.Rows)
                {
                    DataRow drCORow = dtCOInfo.NewRow();

                    drCORow["NOMINEE_NAME"] = ((TextBox)gvrCO.FindControl("txtNomineeName")).Text;
                    drCORow["NOMINEE_ADDR"] = ((TextBox)gvrCO.FindControl("txtNomineeAddr")).Text;
                    drCORow["RELATIONSHIP"] = ((DropDownList)gvrCO.FindControl("drpRelationship")).Text;
                    drCORow["NOMINEE_AGE"] = ((TextBox)gvrCO.FindControl("txtNomineeAge")).Text;
                    drCORow["PERCANTAGE"] = ((TextBox)gvrCO.FindControl("txtNomineePerc")).Text;
                    drCORow["GUARDIAN"] = ((TextBox)gvrCO.FindControl("txtNomineeGuardian")).Text;
                    drCORow["Doc_Data_Id"] = ((Label)gvrCO.FindControl("lblDocDataId")).Text;

                    dtCOInfo.Rows.Add(drCORow);

                    rowCount += 1;
                }

                DataRow drNewCORow = dtCOInfo.NewRow();
                drNewCORow["NOMINEE_NAME"] = "";
                drNewCORow["NOMINEE_ADDR"] = "";
                drNewCORow["RELATIONSHIP"] = "";
                drNewCORow["NOMINEE_AGE"] = "";
                drNewCORow["PERCANTAGE"] = "";
                drNewCORow["GUARDIAN"] = "";
                drNewCORow["Doc_Data_Id"] = Guid.NewGuid().ToString();
                dtCOInfo.Rows.Add(drNewCORow);

                ViewState["NomineesCont"] = dtCOInfo;
                this.gvNomineesCont.DataSource = dtCOInfo;
                this.gvNomineesCont.DataBind();
            }
            catch (Exception ex)
            {
                //objCommon = new ESS.Common();
                //lblMessage.Text = "Error occurred in application.";
                //objCommon.Display("Validate", "DisplayErrorMessage('" + objCommon.EncodeJsString(ex.Message) + "');");
            }
        }

        protected void lnkBtnDeleteDocRowCont_Click(object sender, EventArgs e)
        {
            try
            {
                int rowCount = 1;

                DataTable dtCOInfo = CreateEmptyStructure();
                //lblMessage.Text = "";

                foreach (GridViewRow gvrCO in this.gvNomineesCont.Rows)
                {
                    if (((CheckBox)gvrCO.FindControl("chkSelect")).Checked == false)
                    {
                        DataRow drCORow = dtCOInfo.NewRow();

                        drCORow["NOMINEE_NAME"] = ((TextBox)gvrCO.FindControl("txtNomineeName")).Text;
                        drCORow["NOMINEE_ADDR"] = ((TextBox)gvrCO.FindControl("txtNomineeAddr")).Text;
                        drCORow["RELATIONSHIP"] = ((DropDownList)gvrCO.FindControl("drpRelationship")).Text;
                        drCORow["NOMINEE_AGE"] = ((TextBox)gvrCO.FindControl("txtNomineeAge")).Text;
                        drCORow["PERCANTAGE"] = ((TextBox)gvrCO.FindControl("txtNomineePerc")).Text;
                        drCORow["GUARDIAN"] = ((TextBox)gvrCO.FindControl("txtNomineeGuardian")).Text;
                        drCORow["Doc_Data_Id"] = ((Label)gvrCO.FindControl("lblDocDataId")).Text;

                        dtCOInfo.Rows.Add(drCORow);

                        rowCount++;
                    }
                }

                ViewState["NomineesCont"] = dtCOInfo;
                this.gvNomineesCont.DataSource = (DataTable)ViewState["NomineesCont"];
                this.gvNomineesCont.DataBind();
            }
            catch (Exception ex)
            {
                //objCommon = new ESS.Common();
                //lblMessage.Text = "Error occurred in application.";
                //objCommon.Display("Validate", "DisplayErrorMessage('" + objCommon.EncodeJsString(ex.Message) + "');");
            }
        }
    }
}