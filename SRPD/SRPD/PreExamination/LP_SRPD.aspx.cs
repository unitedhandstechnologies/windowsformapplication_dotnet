using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;

namespace SRPD.PreExamination
{
    public partial class LP_SRPD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            clsUser user = new clsUser();
            user = (clsUser)Session["user"];

            if (user.UserTypeCode == "0")
            {
                Server.Transfer("~/PreExamination/PreExamV2_SRPD_DashBoard.aspx", false);
            }
        }

       
    }
}