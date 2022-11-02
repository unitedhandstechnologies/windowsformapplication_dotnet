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

using PreExamClstLib.Services;
using PreExamV3WebCtrl.WebCtrl;
using Classes;
using Sancharak;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SecureQuestionPaperDownload : System.Web.UI.Page
    {

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            clsUser user = new clsUser();
            user = (clsUser)Session["user"];

            if (user.UserTypeCode == "2")
            {
                hidVenueID.Value = user.UserReferenceID;
                hidVenueName.Value = user.Name;
                // hidVenueCode.Value = user.

                SRVSecurePaper srv = new SRVSecurePaper();

                DataTable dt = srv.ListCollegeCode(hidVenueID.Value);

                if (dt != null && dt.Rows.Count > 0)
                {
                    hidVenueCode.Value = dt.Rows[0]["inst_code"].ToString();
                }

                Server.Transfer("PreExamV2_SecureQuestionPaperDownload__3.aspx");
            }
            else if (user.UserTypeCode == "0")
            {
                hidVenueID.Value = "-1";
                hidVenueName.Value = "";
                hidVenueCode.Value = "";

                Server.Transfer("PreExamV2_SecureQuestionPaperDownload__3.aspx");
            }

            ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
            PreExamV2_SearchInstitute SearchInstiute = (PreExamV2_SearchInstitute)contentPlaceHolder.FindControl("PreExamV2_SearchInstitute1");
            SearchInstiute.gvData.RowCommand += new GridViewCommandEventHandler(this.gvData_RowCommand);
        }
        #endregion

        #region Events

        #region gvData_RowCommand

        private void gvData_RowCommand(object source, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkButSelect")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
                PreExamV2_SearchInstitute SearchInstiute = (PreExamV2_SearchInstitute)contentPlaceHolder.FindControl("PreExamV2_SearchInstitute1");
                GridViewRow row = SearchInstiute.gvData.Rows[index];
                hidVenueID.Value = row.Cells[1].Text;
                hidVenueName.Value = row.Cells[3].Text;
                hidVenueCode.Value = row.Cells[2].Text;
                Server.Transfer("PreExamV2_SecureQuestionPaperDownload__3.aspx");
            }
        }

        #endregion

        #endregion
    }
}