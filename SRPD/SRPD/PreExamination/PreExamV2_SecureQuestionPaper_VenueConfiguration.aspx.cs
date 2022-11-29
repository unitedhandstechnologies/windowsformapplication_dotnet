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
using Classes;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SecureQuestionPaper_VenueConfiguration : System.Web.UI.Page
    {
        #region Variables

        string[] strArray = new string[3];

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            PreExamV2_EventWiseCourseSelection1.trExamAppearanceType.Visible = false;
            PreExamV2_EventWiseCourseSelection1.btnProceed.Click += new EventHandler(btnProceed_Click);
            PreExamV2_EventWiseCourseSelection1.btnProceed.Text = "Proceed";
        }

        #endregion

        #region Events

        #region btnProceed_Click

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            if (PreExamV2_EventWiseCourseSelection1.ExamEventID != "")
            {

                hidExamEventID.Value = PreExamV2_EventWiseCourseSelection1.ExamEventID;
                hidFacID.Value = PreExamV2_EventWiseCourseSelection1.FacultyID;
                strArray = PreExamV2_EventWiseCourseSelection1.CourseID.Split(',');
                hidCrID.Value = strArray[0];
                hidMoLrnID.Value = strArray[1];
                hidPtrnID.Value = strArray[2];
                hidBrnID.Value = PreExamV2_EventWiseCourseSelection1.BranchID;
                hidCrPrDetailsID.Value = PreExamV2_EventWiseCourseSelection1.CoursePartDetailsID;
                hidCrPrChID.Value = PreExamV2_EventWiseCourseSelection1.CoursePartChildID;
                hidIsEventOpen.Value = PreExamV2_EventWiseCourseSelection1.IsEventOpen;

                if (PreExamV2_EventWiseCourseSelection1.SelectedCoursePartDetailsName == PreExamV2_EventWiseCourseSelection1.SelectedCoursePartChildName)
                {
                    hidCourseDetails.Value = PreExamV2_EventWiseCourseSelection1.SelectedCourseName + " - " + PreExamV2_EventWiseCourseSelection1.SelectedCoursePartDetailsName + " - " + PreExamV2_EventWiseCourseSelection1.SelectedEventName + ".";
                }
                else
                {
                    hidCourseDetails.Value = PreExamV2_EventWiseCourseSelection1.SelectedCourseName + " - " + PreExamV2_EventWiseCourseSelection1.SelectedCoursePartDetailsName + " - " + PreExamV2_EventWiseCourseSelection1.SelectedCoursePartChildName + " - " + PreExamV2_EventWiseCourseSelection1.SelectedEventName + ".";
                }
                lblSubHeader.Text = hidCourseDetails.Value;

                PreExamV2_EventWiseCourseSelection1.MemorizeInSession();

                SRVSecurePaper srvExamForm = new SRVSecurePaper();
                DataTable dt = srvExamForm.GetVenuewiseDataConfiguration(hidExamEventID.Value, hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["VenueConfiguration"].ToString() == "1" || dt.Rows[0]["VenueConfiguration"].ToString() == "True")
                    {
                        rbtnDataStatus.Items[1].Selected = true;
                        lblNote.Text = "SRPD configuration is already done for Venues without student data.";
                        lblNote.CssClass = "saveNote";
                    }
                    else if (dt.Rows[0]["VenueConfiguration"].ToString() == "0" || dt.Rows[0]["VenueConfiguration"].ToString() == "False")
                    {
                        rbtnDataStatus.Items[0].Selected = true;
                        lblNote.Text = "SRPD configuration is already done for Venues with student data.";
                        lblNote.CssClass = "saveNote";
                    }

                    btnSave.Enabled = false;
                }
                tblConfig.Visible = true;
                tblFacultySearch.Visible = false;
                divConfig.Style.Add("display", "block");
            }
        }

        #endregion

        #region btnSave_Click

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bool IsDataAvailable = true;

            if (rbtnDataStatus.SelectedItem.Value.Trim() == "0")
                validate();

            if (IsDataAvailable)
            {
                tblConfig.Visible = true;
                divConfig.Style.Add("display", "block");
                tblFacultySearch.Visible = false;
                string userID = ((clsUser)Session["user"]).User_ID;
                SRVSecurePaper srvSecurePaper = new SRVSecurePaper();
                int status = srvSecurePaper.SaveVenuewiseDataConfiguration(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExamEventID.Value, rbtnDataStatus.SelectedItem.Value.Trim(), userID);
                if (status > 0)
                {
                    divMSG.Visible = true;
                    lblMSG.Text = "Information saved successfully.";
                    lblMSG.CssClass = "saveNote";
                    btnSave.Enabled = false;
                }
                else
                {
                    divMSG.Visible = true;
                    lblMSG.Text = "Information cannot saved successfully.";
                    lblMSG.CssClass = "errorNote";

                }
            }
            else
            {
                divMSG.Visible = true;
                lblMSG.Text = "SRPD for Venue With Student Data Cannot be Saved.No student data available with venue.";
                lblMSG.CssClass = "errorNote";
            }

        }

        #endregion

        #endregion

        public bool validate()
        {
            SRVSecurePaper srvSecurePaper = new SRVSecurePaper();
            try
            {
                DataTable dt = srvSecurePaper.GetStudentwiseVenueCount(hidExamEventID.Value, hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (Convert.ToInt32(dt.Rows[0]["StudentCount"].ToString()) > 0)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception)
            {

            }

            return true;
        }
    }
}