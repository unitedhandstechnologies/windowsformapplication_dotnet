using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using PreExamClstLib.BusinessObjects;
using PreExamClstLib.Services;
using System.Data;
using RKLib.ExportData;

namespace SRPD.PreExamination.Reports
{
    public partial class PreExamV2_SRPD_PaperNotUploadedReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillExamEvent();
            }
        }

        #region FillExamEvent

        /// <summary>
        /// Fill Academic Year Wise Exam Event.
        /// </summary>
        public void FillExamEvent()
        {
            SRVExamEvent obj = new SRVExamEvent();
            DataTable dt = obj.ListExamEvent();

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

                ddlEvent.Items.Clear();
                ddlEvent.Items.Add(new ListItem("Select Exam Event", "0"));
                ddlEvent.DataSource = dt;
                ddlEvent.DataTextField = "ExamEvent";
                ddlEvent.DataValueField = "ExamEventID";
                ddlEvent.DataBind();
                ddlEvent.Items.Insert(0, li);
            }
        }

        #endregion

        #region Generate Report For Time table define but paper not uploaded
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            SRVReports srvReports = new SRVReports();
            DataTable dtPaper;
            dtPaper = srvReports.SRPD_QuestionPaperNotUploadedReport(txtDate.Text.ToString(), txtToDate.Text.ToString(), ddlExamEvent.SelectedItem.Value.ToString());
            if (dtPaper != null && dtPaper.Rows.Count > 0)
            {
                RKLib.ExportData.Export objExport = new RKLib.ExportData.Export();

                objExport.ExportDetails(dtPaper, Export.ExportFormat.Excel, "SRPD_PaperNotUploadedDetails.xls");
            }
            else
            {
                lblMsg.CssClass = "errorNote";
                lblMsg.Text = "No Data found for selected date/event combination.";
            }
        }
        #endregion

        #region Generate Report For Time table not defined
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            //PreExamV2_SRPD_TimeTableNotDefined_Report

            lblMsg.Text = "";
            SRVSecurePaper srv = new SRVSecurePaper();
            DataTable dtPaper;
            dtPaper = srv.SRPD_TimeTableNotDefined(ddlEvent.SelectedItem.Value.ToString());
            if (dtPaper != null && dtPaper.Rows.Count > 0)
            {
                RKLib.ExportData.Export objExport = new RKLib.ExportData.Export();

                objExport.ExportDetails(dtPaper, Export.ExportFormat.Excel, "SRPD_TimeTableNotDefinedDetails.xls");
            }
            else
            {
                lblMsg.CssClass = "errorNote";
                lblMsg.Text = "No Data found for selected event.";
            }
        }
        #endregion
    }
}