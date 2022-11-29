using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PreExamClstLib.Services;
using System.Data;
using RKLib.ExportData;

namespace SRPD.PreExamination.Reports
{
    public partial class PreExamV2_SRPD_QuestionPaperFeedback : System.Web.UI.Page
    {
        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillExamEvent();
                txtDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        #endregion

        #region Events

        #region btnExport_Click

        protected void btnExport_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            SRVReports srvReports = new SRVReports();
            DataTable dtPaper;
            dtPaper = srvReports.SRPD_QuestionPaperFeedbackReport(txtDate.Text.ToString(), txtToDate.Text.ToString(), ddlExamEvent.SelectedItem.Value.ToString());
            if (dtPaper != null && dtPaper.Rows.Count > 0)
            {
                RKLib.ExportData.Export objExport = new RKLib.ExportData.Export();

                int[] colList = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
                objExport.ExportDetails(dtPaper, colList, Export.ExportFormat.Excel, "SRPD_PaperFeedbackDetails.xls");
            }
            else
            {
                lblMsg.CssClass = "errorNote";
                lblMsg.Text = "No data found for selected date/event combination.";
            }
        }

        #endregion

        #endregion

        #region Other Function

        #region FillExamEvent

        public void FillExamEvent()
        {
            DataTable dt;
            SRVExamEvent obj = new SRVExamEvent();
            dt = obj.ListExamEvent();

            if (dt != null && dt.Rows.Count > 0)
            {
                ListItem li = new ListItem("--Select--", "-1");
                ddlExamEvent.Items.Clear();
                ddlExamEvent.Items.Add(new ListItem("Select Exam Event", "0"));
                ddlExamEvent.DataSource = dt;
                ddlExamEvent.DataTextField = "ExamEvent";
                ddlExamEvent.DataValueField = "ExamEventID";
                ddlExamEvent.DataBind();
                ddlExamEvent.Items.Insert(0, li);
            }

        }

        #endregion

        #endregion
    }
}