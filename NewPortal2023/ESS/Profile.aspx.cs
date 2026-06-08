using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.IO;

namespace NewPortal2023.ESS
{
    public partial class Profile : System.Web.UI.Page
    {
        NewPortal2023.ESS.Common objcommon = new NewPortal2023.ESS.Common();
        NewPortal2023.ESS.NPS_ShiftRoster objNps = new NewPortal2023.ESS.NPS_ShiftRoster();
        DataSet dsInv = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["sCompID"] != null)
                {
                    try
                    {
                        renderPhoto();
                        getEmpProfileDetails();
                    }
                    catch (Exception ex)
                    {
                        //lblMessage.Text = ex.Message;
                    }
                }
            }
        }

        private void renderPhoto()
        {
            string compid = Session["sCompID"].ToString();
            string empmid = Session["sEmpCode"].ToString();

            string folderPath = Request.PhysicalApplicationPath + ConfigurationManager.AppSettings["Path"] +
                        compid + "\\" + Session["sCompAID"] + "\\Documents\\ProfilePhoto\\" + empmid;

            if (Directory.Exists(folderPath))
            {
                string[] imageFiles = Directory.GetFiles(folderPath, "*.*", SearchOption.TopDirectoryOnly)
                                               .Where(f => f.EndsWith(".jpg") || f.EndsWith(".png") ||
                                                           f.EndsWith(".jpeg") || f.EndsWith(".gif"))
                                               .ToArray();

                if (imageFiles.Length > 0)
                {
                    string fileName = Path.GetFileName(imageFiles[0]);

                    string virtualPath = $"~/PDF Reports/CO000141/NPL/Documents/ProfilePhoto/{empmid}/{fileName}";

                    imgProfile.ImageUrl = ResolveUrl(virtualPath);
                }
                else
                {
                    imgProfile.ImageUrl = ResolveUrl("~/Images/default-profile.jpg");
                }
            }
            else
            {
                imgProfile.ImageUrl = ResolveUrl("~/Images/default-profile.jpg");
            }

        }


        private void getEmpProfileDetails()
        {
            try
            {
                dsInv = objNps.GetEmpProfileDetails((string)Session["sCompID"], (string)Session["sEmpID"]);
                if (dsInv.Tables.Count > 0)
                {
                    txtEmpCode.Text = dsInv.Tables[0].Rows[0]["EMP_MID"].ToString();
                    txtEmpName.Text = dsInv.Tables[0].Rows[0]["EMP_NAME"].ToString();
                    txtEmailId.Text = dsInv.Tables[0].Rows[0]["CORRESPONDENCE_EMAIL1"].ToString();
                    txtBirthDate.Text = dsInv.Tables[0].Rows[0]["BIRTH_DATE"].ToString();
                    txtPanNumber.Text = dsInv.Tables[0].Rows[0]["PAN_NUMBER"].ToString();
                    txtJoinDate.Text = dsInv.Tables[0].Rows[0]["JOIN_DATE"].ToString();
                    txtDesgination.Text = dsInv.Tables[0].Rows[0]["DESG_DESC"].ToString();
                    txtDepartment.Text = dsInv.Tables[0].Rows[0]["DEPT_DESC"].ToString();
                    txtLocation.Text = dsInv.Tables[0].Rows[0]["LOC_DESC"].ToString();
                    //txtEmpType.Text = dsInv.Tables[0].Rows[0]["EMP_TYPE"].ToString();
                    txtHDCode.Text = dsInv.Tables[1].Rows[0]["EMP_MID"].ToString();
                    txtHDName.Text = dsInv.Tables[1].Rows[0]["EMP_NAME"].ToString();
                    if (dsInv.Tables[2].Rows.Count > 0)
                    {
                        gvProfileDteails.DataSource = dsInv.Tables[2];
                        gvProfileDteails.DataBind();
                        OtherDetails.Visible = true;
                        //OtherTitle.Visible = true;
                    }
                    else
                    {
                        gvProfileDteails.DataSource = null;
                        gvProfileDteails.DataBind();
                        OtherDetails.Visible = false;
                        //OtherTitle.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                //lblMessage.Text = ex.Message;
            }
        }
    }
}