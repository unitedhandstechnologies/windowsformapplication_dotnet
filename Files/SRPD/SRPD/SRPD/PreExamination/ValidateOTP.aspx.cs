using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Web.UI.HtmlControls;
using Classes;

namespace SRPD.PreExamination
{
    public partial class ValidateOTP : System.Web.UI.Page
    {
        string[] retrunKeys = new string[1];
        DataTable oDT = new DataTable();
        string sNo;
        clsSupervisor oSupervisor = new clsSupervisor();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["SeqNO"] != null)
            {
                sNo = Request.QueryString["SeqNO"].ToString();

            }

        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            Hashtable oHt = new Hashtable();
            oHt["Seq_No"] = sNo;
            oHt["OTP"] = OTP.Text;
            retrunKeys = oSupervisor.UpdateSupervisiorVerifyStatus(oHt);
            if (retrunKeys[0] == "Y")
            {
                lblNote.Text = "Staus Verified.";
                lblNote.CssClass = "saveNote";
                lblNote.Visible = true;

            }
            else
            {
                lblNote.Text = "InValid OTP.";
                lblNote.CssClass = "errorNote";
                lblNote.Visible = true;
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            //string script = "javascript:self.close();window.opener.location.reload(true);";
            //if (!ClientScript.IsClientScriptBlockRegistered("REFRESH_PARENT"))
            //    ClientScript.RegisterClientScriptBlock(typeof(string), "REFRESH_PARENT", script, true);


            string script = "this.window.opener.location=this.window.opener.location;this.window.close();";
            if (!ClientScript.IsClientScriptBlockRegistered("REFRESH_PARENT"))
                ClientScript.RegisterClientScriptBlock(typeof(string), null, script, true);

        }
    }
}