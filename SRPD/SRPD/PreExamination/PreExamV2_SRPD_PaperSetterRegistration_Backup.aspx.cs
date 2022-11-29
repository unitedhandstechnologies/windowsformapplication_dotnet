using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SRPD_PaperSetterRegistration : System.Web.UI.Page
    {
        private DataTable dt = new DataTable("Form Entry");
        protected void Page_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("First Name");
            dt.Columns.Add("Middle Name");
            dt.Columns.Add("Last Name");
            dt.Columns.Add("Mobile Number");
            dt.Columns.Add("E-Mail ID");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.NewRow();
            dr["FirstName"] = txtFName.Text;
            dr["Middle Name"] = txtMName.Text;
            dr["Last Name"] = txtLName.Text;
            dr["Mobile Number"] = txtMobileNumber.Text;
            dr["E-Mail ID"] = txtEmailid.Text;
            dt.Rows.Add(dr);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}