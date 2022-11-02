using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SRPD.PreExamination
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Button1.Visible = true;
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Server.Transfer("~/PreExamination/LP_SRPD.aspx", false);
        }
    }
}