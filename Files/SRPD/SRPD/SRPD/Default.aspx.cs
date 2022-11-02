using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Classes;


namespace SRPD
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if ((this.txtUserName.Text != "") && (this.txtPassword.Text != ""))
            {
                clsUser user = new clsUser(this.txtUserName.Text, null);
                if (user.Exist)
                {
                    lblError.Visible = false;
                    Session["user"] = user;

                    //if (user.UserTypeCode == "1")
                    //{
                    //    clsStudentTemplate template = new clsStudentTemplate();
                    //    string url = string.Empty;
                    //    template.GetStudentDefaultTemplate(user.User_ID);
                    //    if ((template != null) && (template.TemplateName != null))
                    //    {
                    //        if (template.TemplateName.ToString().Trim() != "")
                    //        {
                    //            url = template.TemplateName.ToString().Trim() + ".aspx";
                    //        }
                    //    }
                    //    else
                    //    {
                    //        DataTable defaultTemplate = new clsTemplate().GetDefaultTemplate();
                    //        if ((defaultTemplate != null) && (defaultTemplate.Rows.Count > 0))
                    //        {
                    //            url = defaultTemplate.Rows[0]["Template_Name"].ToString().Trim() + ".aspx";
                    //        }
                    //    }
                    //    Response.Redirect(url);
                    //}
                    //Server.Transfer("PreExamination/Default1.aspx", true);


                    Response.Redirect("PreExamination/Default1.aspx", false);
                }
                else
                {
                    lblError.Visible = true;
                }
            }
            else
            {
                lblError.Visible = true;
            }

        }
    }
}
