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
using PreExamV3WebCtrl.WebCtrl;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SecureQuestionPaperDownload__1 : System.Web.UI.Page
    {
        #region Variables

        string[] strArray = new string[3];
        string uniID = clsGetSettings.UniversityID.ToString();

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetPage();
            }

            #region Code for Course selection control

            CrSelectionCtrl.btnProceed.Click += new EventHandler(btnProceed_Click);
            CrSelectionCtrl.btnProceed.Text = "Proceed>>";

            #endregion
        }

        #endregion

        #region Events

        #region btnProceed_Click

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            //Setting hidden variable.
            SetHiddenVariables();

            Server.Transfer("PreExamV2_SecureQuestionPaperDownload__2.aspx");

        }

        #endregion

        #endregion

        #region Other Functions

        #region  SetPage

        void SetPage()
        {
            ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");

            hidVenueID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidVenueID")).Value;
            hidVenueName.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidVenueName")).Value;
            hidVenueCode.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidVenueCode")).Value;

            lblSubHeader.Text = " for " + hidVenueName.Value;

        }

        #endregion

        #region SetHiddenVariables

        void SetHiddenVariables()
        {

            string[] strArray = new string[3];

            hidEventID.Value = CrSelectionCtrl.ExamEventID;
            hidFacultyID.Value = CrSelectionCtrl.FacultyID;
            strArray = CrSelectionCtrl.CourseID.Split(',');
            hidCourseID.Value = strArray[0];
            hidMolrnID.Value = strArray[1];
            hidPtrnID.Value = strArray[2];
            hidBrnID.Value = CrSelectionCtrl.BranchID;
            hidCrPrDetailsID.Value = CrSelectionCtrl.CoursePartDetailsID;
            hidCrPrChID.Value = CrSelectionCtrl.CoursePartChildID;
            hidExamEvent.Value = CrSelectionCtrl.SelectedEventName;
            hidIsEventOpen.Value = CrSelectionCtrl.IsEventOpen;

            if (CrSelectionCtrl.SelectedCoursePartDetailsName == CrSelectionCtrl.SelectedCoursePartChildName)
            {
                hidPageDescription.Value = lblSubHeader.Text + " - " + CrSelectionCtrl.SelectedCourseName + " - " + CrSelectionCtrl.SelectedCoursePartDetailsName;
            }
            else
            {
                hidPageDescription.Value = lblSubHeader.Text + " - " + CrSelectionCtrl.SelectedCourseName + " - " + CrSelectionCtrl.SelectedCoursePartDetailsName + " - " + CrSelectionCtrl.SelectedCoursePartChildName;
            }

        }

        #endregion

        #endregion
    }
}