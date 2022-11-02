using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PreExamClstLib.Services;
using System.Data;
using Classes;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SecureQuestionPaperUpload : System.Web.UI.Page
    {

        #region Variables

        DataTable dt = null;
        string uniID = clsGetSettings.UniversityID.ToString();
        public static DataTable dtStudentDetails = new DataTable("dt");
        string sConfiguration = string.Empty;
        clsUser user = null;
        string Flag = string.Empty;
        //string RdbFlag = string.Empty;

        #endregion

        #region Page_Load

        protected void Page_Load(object sender, EventArgs e)
        {

            
            //Fetching user.
            user = new clsUser();
            user = (clsUser)Session["user"];                   
            lblSubHeader.Text = "";
            lblSubHeader.Text = "";
            lblMsg.Text = "";
            //rblist.Items[0].Selected = true;
         
            CrSelectionCtrl.btnProceed.Click += new EventHandler(btnProceed_Click);
            if (!IsPostBack)
            {
                rdbProgramwise.Checked = true;
                FillExamEvent();
            }
        }

        #endregion

        #region Events


        #region FillExamEvent

        /// <summary>
        /// Fill Academic Year Wise Exam Event.
        /// </summary>
        public void FillExamEvent()
        {
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

        #region btnProceed_Click
        protected void btnProceed_Click(object sender, EventArgs e)
        {
            hidRdbFlag.Value = "1";
            //rblist.Visible = false;
            btnNext.Visible = true;
            SetVariables();
            CrSelectionCtrl.MemorizeInSession();
            try
            {
                if (IsConfiguredForSRPD())
                {
                    divCourseSelection.Style.Add("display", "none");
                   
                    //divCourseSelection.Style.Add("display", "none");
                    divAssMthAssType.Style.Add("display", "block");
                    //Fill radion buttonlist with Assessment Method Assessment Type Combination
                    FillCourseWiseAssessmentMethodAssessmentType();
                }
                else
                {
                    lblMsg.Visible = true;
                    lblMsg.Text = "Selected Course Part Term is not configured for SRPD";
                    lblMsg.CssClass = "errorNote";
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }
        #endregion

        #region btnNext_Click
        protected void btnNext_Click(object sender, EventArgs e)
        {
            string[] strArray = new string[3];
            strArray = rbtListAssementMethodCA.SelectedValue.ToString().Split(',');
            hidAssMthID.Value = strArray[0];
            hidAssTypeID.Value = strArray[1];
            hidTchLrnMthID.Value = strArray[2];
            hidAssMthAssTypeName.Value = rbtListAssementMethodCA.SelectedItem.Text.ToString();
            hidflag.Value = "1";

            Server.Transfer("PreExamV2_SecureQuestionPaperUpload__1.aspx", true);
        }
        #endregion

        #region btnNextPaperCodeWise_Click
        protected void btnNextPaperCodeWise_Click(object sender, EventArgs e)
        {
            string[] strArray = new string[3];
            strArray = rbtListAssementMethodCAPpCodeWise.SelectedValue.ToString().Split(',');
            hidAssMthID.Value = strArray[0];
            hidAssTypeID.Value = strArray[1];
            hidTchLrnMthID.Value = strArray[2];
            hidAssMthAssTypeName.Value = rbtListAssementMethodCAPpCodeWise.SelectedItem.Text.ToString();
            hidflag.Value = "2";


            Server.Transfer("PreExamV2_SecureQuestionPaperUpload__1.aspx", true);
        }
        #endregion

        #region btnNextPaperDateWise_Click
        protected void btnNextPaperDateWise_Click(object sender, EventArgs e)
        {
            string[] strArray = new string[3];
            strArray = rbtListAssementMethodCADateWise.SelectedValue.ToString().Split(',');
            hidAssMthID.Value = strArray[0];
            hidAssTypeID.Value = strArray[1];
            hidTchLrnMthID.Value = strArray[2];
            hidAssMthAssTypeName.Value = rbtListAssementMethodCADateWise.SelectedItem.Text.ToString();
            hidflag.Value = "3";


            Server.Transfer("PreExamV2_SecureQuestionPaperUpload__1.aspx", true);
        }
        #endregion

        #region btnBckToCrSelection_Click
        protected void btnBckToCrSelection_Click(object sender, EventArgs e)
        {
            divCourseSelection.Style.Add("display", "block");
            divAssMthAssType.Style.Add("display", "none");
            rdbdiv.Visible = true;         
            lblSubHeader.Text = "";
        }
        #endregion


        #region btnBckToPpCodeSelection_Click
        protected void btnBckToPpCodeSelection_Click(object sender, EventArgs e)
        {
            divpapercodewiseselection.Style.Add("display", "block");
            divAssMthAssTypePaperCodeWise.Style.Add("display", "none");
            rdbdiv.Visible = true;         
            lblSubHeader.Text = "";
        }
        #endregion

        #region btnBckTodateSelection_Click
        protected void btnBckTodateSelection_Click(object sender, EventArgs e)
        {
            divdatewiseselection.Style.Add("display", "block");
            divAssMthAssTypeDateWise.Style.Add("display", "none");
            rdbdiv.Visible = true;         
            lblSubHeader.Text = "";
        }
        #endregion

        #endregion

        #region ddlExamEvent_SelectedIndexChanged

        protected void ddlExamEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            //divCourseSelection.Visible = false;
            lblSubHeader.Text = "";
            hidExamEventName.Value = ddlExamEvent.SelectedItem.Text;
            lblSubHeader.Text += " for " + hidExamEventName.Value;          
        }

        #endregion

        #region Functions

        #region SetVariables

        public void SetVariables()
        {
            if (CrSelectionCtrl.FacultyID != "")
            {
                hidFacID.Value = CrSelectionCtrl.FacultyID;
                hidCrID.Value = CrSelectionCtrl.CourseID.Split(',')[0];
                hidMoLrnID.Value = CrSelectionCtrl.CourseID.Split(',')[1];
                hidPtrnID.Value = CrSelectionCtrl.CourseID.Split(',')[2];
                hidBrnID.Value = CrSelectionCtrl.BranchID;
                hidCrPrDetailsID.Value = CrSelectionCtrl.CoursePartDetailsID;
                hidCrPrChID.Value = CrSelectionCtrl.CoursePartChildID;
                hidExEvID.Value = CrSelectionCtrl.ExamEventID;
                hidExamEvent.Value = CrSelectionCtrl.SelectedEventName;
                hidCourseName.Value = CrSelectionCtrl.SelectedCourseName;
                hidCoursePartName.Value = CrSelectionCtrl.SelectedCoursePartDetailsName;
                hidCoursePartTermName.Value = CrSelectionCtrl.SelectedCoursePartChildName;

                if (CrSelectionCtrl.SelectedCoursePartDetailsName == CrSelectionCtrl.SelectedCoursePartChildName)
                {
                    lblSubHeader.Text = "for [" + CrSelectionCtrl.SelectedEventName + "] - " + CrSelectionCtrl.SelectedCourseName + " - " + CrSelectionCtrl.SelectedCoursePartDetailsName;
                    hidPageDescription.Value = lblSubHeader.Text.Trim();
                }
                else
                {
                    lblSubHeader.Text = "for [" + CrSelectionCtrl.SelectedEventName + "] - " + CrSelectionCtrl.SelectedCourseName + " - " + CrSelectionCtrl.SelectedCoursePartDetailsName + " - " + CrSelectionCtrl.SelectedCoursePartChildName;
                    hidPageDescription.Value = lblSubHeader.Text.Trim();
                }

                hidIsEventOpen.Value = CrSelectionCtrl.IsEventOpen;
            }
        }



        #endregion

        #region FillCourseWiseAssessmentMethodAssessmentType

        /// <summary>
        /// Filling Course Wise Assessment Method Assessment Type Combination
        /// </summary>
        void FillCourseWiseAssessmentMethodAssessmentType()
        {
            SRVSecurePaper srvSecure = new SRVSecurePaper();

            DataTable dt = new DataTable();
            dt = srvSecure.ListTlmAmAt(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExEvID.Value);
            if (dt != null && dt.Rows.Count > 0)
            {
                 divAssMthAssType.Style.Add("display", "block");
                 rdbdiv.Visible = false;                
                 btnNext.Enabled = true;
                lblMsg.Visible = false;
                rbtListAssementMethodCA.Visible = true;
                rbtListAssementMethodCA.DataSource = dt;
                rbtListAssementMethodCA.DataTextField = "Text";
                rbtListAssementMethodCA.DataValueField = "Value";
                rbtListAssementMethodCA.DataBind();
                rbtListAssementMethodCA.SelectedIndex = 0;

            }
            else
            {
                btnNext.Enabled = false;
                rbtListAssementMethodCA.Visible = false;
                lblMsg.Visible = true;
                lblMsg.Text = "No assessment method and assessment type combination is exists for selected criteria.";
                lblMsg.CssClass = "errorNote";
            }
        }

        #endregion



        #region FillCourseWiseAssessmentMethodAssessmentType PaperCodeWise  : Akhilesh 19/10/2019

        /// <summary>
        /// Filling Course Wise Assessment Method Assessment Type Combination PaperCodeWise And Event Wise
        /// </summary>
        void FillCourseWiseAssessmentMethodAssessmentType_PaperCodeWise()
        {
            rdbdiv.Visible = false;
            hidExEvID.Value = ddlExamEvent.SelectedValue;
            string papercode = Txtpapercode.Text;
            hidppcode.Value = papercode;
            divAssMthAssType.Style.Add("display", "none");
            divCourseSelection.Style.Add("display", "none");
           // divAssMthAssType.Style.Add("display", "none");
            divdatewiseselection.Style.Add("display", "none");
            SRVSecurePaper srvSecure = new SRVSecurePaper();

            DataTable dt = new DataTable();
            dt = srvSecure.ListTlmAmAt_PaperCodeWise(papercode,hidExEvID.Value);
            if (dt != null && dt.Rows.Count > 0)
            {
                divAssMthAssTypePaperCodeWise.Style.Add("display", "block");
                btnNext.Enabled = true;
                lblMsg.Visible = false;
                rbtListAssementMethodCAPpCodeWise.Visible = true;
                rbtListAssementMethodCAPpCodeWise.DataSource = dt;
                rbtListAssementMethodCAPpCodeWise.DataTextField = "Text";
                rbtListAssementMethodCAPpCodeWise.DataValueField = "Value";
                rbtListAssementMethodCAPpCodeWise.DataBind();
                rbtListAssementMethodCAPpCodeWise.SelectedIndex = 0;

            }
            else
            {
                divdatewiseselection.Style.Add("display", "none");
                divpapercodewiseselection.Style.Add("display", "block");
                btnNext.Enabled = false;
                rbtListAssementMethodCAPpCodeWise.Visible = false;
                lblMsg.Visible = true;
                lblMsg.Text = "No assessment method and assessment type combination is exists for selected criteria.";
                lblMsg.CssClass = "errorNote";
            }
        }

        #endregion


        #region FillCourseWiseAssessmentMethodAssessmentType DateExEvWise  : Akhilesh 19/10/2019

        /// <summary>
        /// Filling Course Wise Assessment Method Assessment Type Combination PaperCodeWise And Event Wise
        /// </summary>
        void FillCourseWiseAssessmentMethodAssessmentType_DateExEvWise()
        {
            rdbdiv.Visible = false;
            hidExEvID.Value = ddlEvent.SelectedValue;
            string date = txtDate.Text;
            hiddate.Value = date;
            divCourseSelection.Style.Add("display", "none");
            divAssMthAssType.Style.Add("display", "none");
            divdatewiseselection.Style.Add("display", "none");
            divAssMthAssTypePaperCodeWise.Style.Add("display", "none");
            SRVSecurePaper srvSecure = new SRVSecurePaper();

            DataTable dt = new DataTable();
            dt = srvSecure.ListTlmAmAt_DateExEvWise(date, hidExEvID.Value);
            if (dt != null && dt.Rows.Count > 0)
            {
                divAssMthAssTypePaperCodeWise.Style.Add("display", "none");
                divAssMthAssTypeDateWise.Style.Add("display", "block");
                btnNext.Enabled = true;
                lblMsg.Visible = false;
                rbtListAssementMethodCADateWise.Visible = true;
                rbtListAssementMethodCADateWise.DataSource = dt;
                rbtListAssementMethodCADateWise.DataTextField = "Text";
                rbtListAssementMethodCADateWise.DataValueField = "Value";
                rbtListAssementMethodCADateWise.DataBind();
                rbtListAssementMethodCADateWise.SelectedIndex = 0;

            }
            else
            {
                divpapercodewiseselection.Style.Add("display", "none");
                divdatewiseselection.Style.Add("display", "block");
                btnNext.Enabled = false;
                rbtListAssementMethodCADateWise.Visible = false;
                lblMsg.Visible = true;
                lblMsg.Text = "No assessment method and assessment type combination is exists for selected criteria.";
                lblMsg.CssClass = "errorNote";
            }
        }

        #endregion

        public bool IsConfiguredForSRPD()
        {
            bool IsConfigured = false;

            SRVSecurePaper srvSecure = new SRVSecurePaper();

            DataTable dt = new DataTable();
            dt = srvSecure.IsConfiguredForSRPD(hidFacID.Value, hidCrID.Value, hidMoLrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExEvID.Value);

            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["Config"].ToString() == "1")
                    IsConfigured = true;
                else
                    IsConfigured = false;
            }
            else
                IsConfigured = false;

            return IsConfigured;
        }

        #endregion    

        #region  btnppcodeproceed_Click   : Akhilesh 19/10/2009
        protected void btnppcodeproceed_Click(object sender, EventArgs e)
        {
            hidRdbFlag.Value = "2";
            divpapercodewiseselection.Style.Add("display", "none");
               // divAssMthAssTypePaperCodeWise.Style.Add("display","block");               
                //Fill radion buttonlist with Assessment Method Assessment Type Combination
                FillCourseWiseAssessmentMethodAssessmentType_PaperCodeWise();
        }
         #endregion  

        #region ddlEvent_SelectedIndexChanged : Akhilesh
        protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblSubHeader.Text = "";
            hidExamEventName.Value = ddlEvent.SelectedItem.Text;
            lblSubHeader.Text += " for " + hidExamEventName.Value;


         

        }
        #endregion

        #region rdbProgramwise_CheckedChanged
        protected void rdbProgramwise_CheckedChanged(object sender, EventArgs e)
        {
          
            //RdbFlag = "1";
            divAssMthAssType.Style.Add("display", "none");
            divAssMthAssTypePaperCodeWise.Style.Add("display", "none");
            divAssMthAssTypeDateWise.Style.Add("display", "none");
            divpapercodewiseselection.Style.Add("display", "none");
            divdatewiseselection.Style.Add("display", "none");
            divCourseSelection.Style.Add("display", "block");
              
        }
        #endregion

        #region rdbpapercodewise_CheckedChanged
        protected void rdbpapercodewise_CheckedChanged(object sender, EventArgs e)
        {
            
           // RdbFlag = "2";
            divAssMthAssType.Style.Add("display", "none");
            divAssMthAssTypePaperCodeWise.Style.Add("display", "none");
            divAssMthAssTypeDateWise.Style.Add("display", "none");
            divpapercodewiseselection.Style.Add("display", "block");
            divdatewiseselection.Style.Add("display", "none");
            divCourseSelection.Style.Add("display", "none");
        }
        #endregion

        #region  rdbdatewise_CheckedChanged
        protected void rdbdatewise_CheckedChanged(object sender, EventArgs e)
        {
           
          //  RdbFlag = "3";
            divpapercodewiseselection.Style.Add("display", "none");
            divdatewiseselection.Style.Add("display", "block");
            divCourseSelection.Style.Add("display", "none");
            divAssMthAssType.Style.Add("display", "none");
            divAssMthAssTypePaperCodeWise.Style.Add("display", "none");
            divAssMthAssTypeDateWise.Style.Add("display", "none");
        }
         #endregion

        #region btndateproceed_Click
        protected void btndateproceed_Click(object sender, EventArgs e)
        {
            hidRdbFlag.Value = "3";
            divpapercodewiseselection.Style.Add("display", "none");
           // divAssMthAssTypeDateWise.Style.Add("display", "block");
            //Fill radion buttonlist with Assessment Method Assessment Type Combination
            FillCourseWiseAssessmentMethodAssessmentType_DateExEvWise();
        }
        #endregion

    }
}