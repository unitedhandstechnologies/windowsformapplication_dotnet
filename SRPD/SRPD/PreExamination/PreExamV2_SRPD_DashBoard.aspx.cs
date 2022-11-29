using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PreExamClsLib.Classes;
using Classes;
using AAASecurity;
using System.Globalization;
using System.Web.UI.HtmlControls;


namespace SRPD.PreExamination
{
    public partial class PreExamV2_SRPD_DashBoard : System.Web.UI.Page
    {
        #region Properties
        Hashtable oHt;
        DataSet ds;
        DataTable oDT;
        clsReportsDashboard oReportsDashboard;
        string todayDate = string.Empty;

        Classes.clsCommon oCommon = new clsCommon();

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                try
                {

                    SetHiddenField();                   

                    if (hidDateTime.Value != "" && hidDateTime.Value != "0")
                        txtSelectDate.Text = hidDateTime.Value;
                    else
                    {
                        string date = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);  
                        txtSelectDate.Text = date.ToString();
                    }
                    
                    LoadData();
                    
                }
                catch (Exception ex)
                {
                    throw ex.InnerException;
                }
            }

        }

        #endregion

        #region Events

        #region Not-Uploaded Papers
        protected void lbtnNotUploadedPaper_Click(object sender, EventArgs e)
        {
            if (lblTotalNotUploadedCount.Text != "0")
            {
                Server.Transfer("~/PreExamination/PreExamV2_SRPD_DashBoard__1.aspx", true);
            }
        }
        #endregion

        #region Time Table Not Published papers

        protected void lbtnTimeTableNotPublishedPapers_Click(object sender, EventArgs e)
        {
            if (lblNotPublishedCount.Text != "0")
            {
                Server.Transfer("~/PreExamination/PreExamV2_SRPD_DashBoard__4.aspx", true);
            }
        }
        #endregion

        #region Not published Venue
        protected void lbtnNotPublishedVenue_Click(object sender, EventArgs e)
        {
            if (lblTotalNotPublishVenue.Text != "0")
            {
                Server.Transfer("~/PreExamination/PreExamV2_SRPD_DashBoard__2.aspx", true);
            }
        }
        #endregion


        #region Programs with no Venue
        protected void lnkProgramsWithNoVenue_Click(object sender, EventArgs e)
        {
            if (lblProgramsWithNoVenue.Text != "0")
            {
                Server.Transfer("~/PreExamination/PreExamV2_SRPD_DashBoard__6.aspx", true);
            }
        }

        #endregion

        #region Institutes Not mapped to any Center
        protected void lnkInstitutesNotMapped_Click(object sender, EventArgs e)
        {
            if (lblInstitutesNotMappedToAnyCenter.Text != "0")
            {
                Server.Transfer("~/PreExamination/PreExamV2_SRPD_DashBoard__7.aspx", true);
            }
        }
        #endregion

        #region Date Selection

        protected void btnDateSelection_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        #endregion

        #region gvExamCount_RowCommand

        protected void gvExamCount_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            index = index - 1;
            if (e.CommandName == "View")
            {
                hidStartTime.Value = gvExamCount.DataKeys[index]["ExamStartTime_New"].ToString();
                hidEndTime.Value = gvExamCount.DataKeys[index]["ExamEndTime_New"].ToString();

                Server.Transfer("~/PreExamination/PreExamV2_SRPD_DashBoard__3.aspx", true);
            }
        }
        #endregion

        #region gvPaperDetails_RowCommand

        protected void gvPaperDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            index = index - 1;
            if (e.CommandName == "PaperDetailsView")
            {
                hidFacID.Value = gvPaperDetails.DataKeys[index]["pk_Fac_ID"].ToString();
                hidCrID.Value = gvPaperDetails.DataKeys[index]["pk_Cr_ID"].ToString();
                hidMolID.Value = gvPaperDetails.DataKeys[index]["pk_MoLrn_ID"].ToString();
                hidPtrnID.Value = gvPaperDetails.DataKeys[index]["pk_Ptrn_ID"].ToString();
                hidBrnID.Value = gvPaperDetails.DataKeys[index]["pk_Brn_ID"].ToString();
                hidCrPrDetailsID.Value = gvPaperDetails.DataKeys[index]["pk_CrPr_Details_ID"].ToString();
                hidCrPrChID.Value = gvPaperDetails.DataKeys[index]["pk_CrPrCh_ID"].ToString();
                hidExEvID.Value = gvPaperDetails.DataKeys[index]["pk_ExEv_ID"].ToString();
                hidPpPpHeadCrPrChID.Value = gvPaperDetails.DataKeys[index]["pk_Pp_PpHead_CrPrCh_ID"].ToString();
                hidStartTime.Value = gvPaperDetails.DataKeys[index]["ExamStartTime_New"].ToString();
                hidEndTime.Value = gvPaperDetails.DataKeys[index]["ExamEndTime_New"].ToString();

                Server.Transfer("~/PreExamination/PreExamV2_SRPD_DashBoard__5.aspx", true);
            }
        }

        #endregion

        #region gvPaperDetails_PageIndexChanging

        protected void gvPaperDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            LoadData();
            gvPaperDetails.PageIndex = e.NewPageIndex;
            gvPaperDetails.DataBind();


        }
        #endregion


        #endregion

        #region other function
        #region FormatDate

        string FormatDate(string date)
        {
            DateTimeFormatInfo dateTimeFormatterProvider = DateTimeFormatInfo.CurrentInfo.Clone() as DateTimeFormatInfo;

            dateTimeFormatterProvider.ShortDatePattern = "dd/MM/yyyy"; //source date format

            DateTime dateTime = DateTime.Parse(date, dateTimeFormatterProvider);

            string formatted = dateTime.ToString("dd/MM/yyyy");
            return formatted;
        }

        #endregion

        #region LoadData

        private void LoadData()
        {
            try
            {
                string uniID = clsGetSettings.UniversityID.Trim();
                hidUniId.Value = uniID;
                hidDateTime.Value = FormatDate(txtSelectDate.Text);    //String.Format("{0:yyyy-MM-dd}", str);               
                oHt = new Hashtable();
                oHt.Add("UniId", hidUniId.Value);
                oHt.Add("DateTime", hidDateTime.Value);

                oReportsDashboard = new clsReportsDashboard();
                ds = oReportsDashboard.GetSRPDDashboardCount(oHt);

                if (ds != null)
                {

                    oDT = ds.Tables[0];
                    lblPaperCode.Text = oDT.Rows[0]["TotalPaper"].ToString();
                    lblUplodedPaper.Text = oDT.Rows[0]["UplodedPaper"].ToString();
                    lblTotalNotUploadedCount.Text = oDT.Rows[0]["TotalNotUploadedCount"].ToString();
                    lblNotPublishedCount.Text = oDT.Rows[0]["NotPublishedCount"].ToString();
                    lblTotalPublishVenue.Text = oDT.Rows[0]["TotalPublishVenue"].ToString();
                    lblTotalNotPublishVenue.Text = oDT.Rows[0]["TotalNotPublishVenue"].ToString();
                    lblProgramsWithNoVenue.Text = oDT.Rows[0]["ProgramsWithNoVenue"].ToString();
                    lblInstitutesNotMappedToAnyCenter.Text = oDT.Rows[0]["InstitutesNotMappedToAnyCenter"].ToString();

                    oDT = ds.Tables[1];
                    if (oDT != null && ds.Tables[1].Rows.Count > 0)
                    {
                        gvExamCount.DataSource = oDT;
                        gvExamCount.DataBind();
                        gvExamCount.Visible = true;
                        slotExam.Visible = true;
                    }
                    else
                    {
                        gvExamCount.Visible = false;
                        slotExam.Visible = false;

                    }


                    oDT = ds.Tables[2];
                    if (oDT != null && ds.Tables[2].Rows.Count > 0)
                    {
                        gvPaperDetails.DataSource = oDT;
                        gvPaperDetails.DataBind();
                        gvPaperDetails.Visible = true;
                        slotpaper.Visible = true;
                    }
                    else
                    {
                        gvPaperDetails.Visible = false;
                        slotpaper.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            finally
            {
                oDT = null; ds = null;
            }
        }
        #endregion
        #endregion

        private void SetHiddenField()
        {

            HtmlInputHidden[] hid = new HtmlInputHidden[14];
            hid[0] = hidUniId;
            hid[1] = hidDateTime;
            hid[2] = hidStartTime;
            hid[3] = hidEndTime;
            hid[4] = hidFacID;
            hid[5] = hidCrID;
            hid[6] = hidMolID;
            hid[7] = hidPtrnID;
            hid[8] = hidBrnID;
            hid[9] = hidCrPrDetailsID;
            hid[10] = hidCrPrChID;
            hid[11] = hidExEvID;
            hid[12] = hidPpPpHeadCrPrChID;
            hid[13] = hidInstID;            
            clsCommon oCommon = new clsCommon();
            oCommon.setHiddenVariablesMPC(ref hid);

        }
    }
}