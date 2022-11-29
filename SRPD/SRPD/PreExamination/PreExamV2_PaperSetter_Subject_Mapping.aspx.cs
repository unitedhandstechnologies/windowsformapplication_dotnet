using Classes;
using PreExamClsLib.Services;
using PreExamClstLib.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_PaperSetter_Subject_Mapping : System.Web.UI.Page
    {
        #region Variables
        ScriptManager scriptManager;
        string[] strCourse;
        string pp_setterId = "51";
        Hashtable oHt = new Hashtable();
        #endregion

        #region Public Properties
        /// <summary>
        /// Sets the script manager of the user control.
        /// </summary>
        public ScriptManager ScriptManager
        {
            set { scriptManager = value; }
        }

        public string ExamEventID
        {
            get
            {
                return ddlEvent.SelectedValue.ToString();
            }
        }
        public string EventName
        {
            get
            {
                return ddlEvent.SelectedItem.ToString();
            }
        }
        public string FacultyID
        {
            get
            {
                return ddlFaculty.SelectedValue.ToString();
            }
        }
        public string CourseID
        {
            get
            {
                return ddlCourseName.SelectedValue.ToString();
            }
        }
        public string BranchID
        {
            get
            {
                return ddlBranch.SelectedValue.ToString();
            }
        }
        public string CoursePartDetailsID
        {
            get
            {
                return ddlCoursePart.SelectedValue.ToString();
            }
        }
        public string CoursePartChildID
        {
            get
            {
                return ddlCoursePartTerm.SelectedValue.ToString();
            }
        }

        public string SelectedPaperID
        {
            get { return ddlpaper.SelectedValue.ToString(); }
        }
        public string SelectedPaperName
        {
            get { return ddlpaper.SelectedItem.Text.ToString(); }
        }
        public string SelectedFacultyName
        {
            get { return ddlFaculty.SelectedItem.Text.ToString(); }
        }
        public string SelectedCoursePartDetailsName
        {
            get { return ddlCoursePart.SelectedItem.Text.ToString(); }
        }
        public string SelectedCoursePartChildName
        {
            get { return ddlCoursePartTerm.SelectedItem.Text.ToString(); }
        }
        public string SelectedCourseName
        {
            get
            {
                string brn = ddlBranch.SelectedValue.ToString() != "0" ? " - " + ddlBranch.SelectedItem.Text.ToString() : "";
                return ddlCourseName.SelectedItem.Text.ToString() + brn;
            }
        }
        public string SelectedBranchName
        {
            get { return ddlBranch.SelectedItem.Text.ToString(); }
        }

        public string IsEventOpen
        {
            get { return "1"; }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(SRVExamForm));
            Ajax.Utility.RegisterTypeForAjax(typeof(SRVScheduleCenter));
            if (!IsPostBack)
            {
                fillExamEvent();
                hidInstID.Value = "19";

                FillGrid();
                if (gvCoursePaper.Rows.Count > 0)
                {
                    divDropDown.Style.Add("display", "none");
                    divGrid.Style.Add("display", "block");
                    lnkExistingReq.Enabled = false;
                    lnkNewRequest.Enabled = true;
                }

            }
            #region Code for Course selection control

            //CrSelectionCtrl.btnProceed.Click += new EventHandler(btnProceed_Click);
            //CrSelectionCtrl.btnProceed.Text = "Proceed";

            #endregion
        }
        #region Events

        protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("1");
            if (ddlEvent.SelectedValue.ToString() != "-1")
            {
                FillEventWisefaculty(ExamEventID);
                if (scriptManager != null)
                {
                    scriptManager.SetFocus(ddlEvent);
                }

                hidEventID.Value = ddlEvent.SelectedItem.Value.Trim();
            }
        }
        protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("2");
            if (ddlFaculty.SelectedValue.ToString() != "-1")
                FillfacultywiseCourse(ExamEventID, FacultyID);
            if (scriptManager != null)
                scriptManager.SetFocus(ddlFaculty);
            hidFacultyID.Value = ddlFaculty.SelectedValue;
        }

        protected void ddlCourseName_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("3");
            if (ddlCourseName.SelectedValue.ToString() != "-1")
            {
                string[] strCourse = ddlCourseName.SelectedValue.Trim().Split(',');
                fillCoursewisebranch(ExamEventID, FacultyID, strCourse[0], strCourse[1], strCourse[2]);
                if (scriptManager != null)
                {
                    scriptManager.SetFocus(ddlCourseName);
                }
                hidCourseID.Value = ddlCourseName.SelectedValue;
            }
        }

        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("4");
            if (ddlBranch.SelectedValue.ToString() != "-1")
            {
                string[] strCourse = ddlCourseName.SelectedValue.Trim().Split(',');
                fillCoursewisePart(ExamEventID, FacultyID, strCourse[0], strCourse[1], strCourse[2], BranchID);
                if (scriptManager != null)
                {
                    scriptManager.SetFocus(ddlBranch);
                }
                hidBranchID.Value = ddlBranch.SelectedValue;
            }
        }

        protected void ddlCoursePart_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("5");
            if (ddlCoursePart.SelectedValue.ToString() != "-1")
            {
                string[] strCourse = ddlCourseName.SelectedValue.Trim().Split(',');
                fillCoursePartWiseTerm(ExamEventID, ddlFaculty.SelectedItem.Value.Trim(), strCourse[0], strCourse[1], strCourse[2], BranchID, CoursePartDetailsID);
                if (scriptManager != null)
                {
                    scriptManager.SetFocus(ddlCoursePart);
                }

                hidCrPrDetailsID.Value = ddlCoursePart.SelectedItem.Value.Trim();
            }
        }

        string Inst_ID = "19";
        protected void ddlCoursePartTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("6");

            if (ddlCoursePartTerm.SelectedValue.ToString() != "-1")
            {
                string[] strCourse = ddlCourseName.SelectedValue.Trim().Split(',');
                fillExamPaper(FacultyID, strCourse[0], strCourse[1], strCourse[2], BranchID, CoursePartDetailsID, CoursePartChildID, Inst_ID);
                if (scriptManager != null)
                {
                    scriptManager.SetFocus(ddlCoursePartTerm);
                }

                hidCrPrChID.Value = ddlCoursePartTerm.SelectedItem.Value.Trim();
            }

        }

        protected void ddlpaper_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("7");
            if (ddlpaper.SelectedValue.ToString() != "-1")
            {
                if (scriptManager != null)
                {
                    scriptManager.SetFocus(ddlpaper);
                }

                hidPaperID.Value = ddlpaper.SelectedItem.Value.Trim();
            }
        }
        #endregion


        #region Other Functions
        #region fillExamEvent

        public void fillExamEvent()
        {
            SRVExamEvent srv = new SRVExamEvent();
            DataTable dt = srv.ListExamEvent();

            ddlEvent.Items.Clear();
            ddlEvent.Items.Add(new ListItem("--- Select ---", "-1"));

            foreach (DataRow row in dt.Rows)
            {
                ddlEvent.Items.Add(new ListItem(row["ExamEvent"].ToString(), row["ExamEventID"].ToString()));
            }
        }

        #endregion
        #region FillFaculty
        private void FillEventWisefaculty(string Event)
        {
            DataTable dt = SRVScheduleCenter.ExamEventWiseFacultyList(ExamEventID);
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add(new ListItem("--- Select ---", "-1"));

            foreach (DataRow row in dt.Rows)
            {
                ddlFaculty.Items.Add(new ListItem(row["Text"].ToString(), row["Value"].ToString()));
            }
        }
        #endregion

        #region FillfacultywiseCourse

        void FillfacultywiseCourse(string ExamEvent, string Faculty)
        {
            DataTable dt = SRVSchedule.ExamEventWiseCourse(ExamEventID, FacultyID);
            ddlCourseName.Items.Clear();
            ddlCourseName.Items.Add(new ListItem("--- Select ---", "-1"));
            foreach (DataRow row in dt.Rows)
            {
                ddlCourseName.Items.Add(new ListItem(row["Text"].ToString(), row["Value"].ToString()));
            }
        }

        #endregion

        #region fillCoursewisebranch

        void fillCoursewisebranch(string ExamEvent, string Faculty, string Cr_ID, string MoLrn_ID, string Ptrn_ID)
        {
            DataTable dt = SRVSchedule.ExamEventWiseCourseWiseBranch(ExamEventID, FacultyID, Cr_ID, MoLrn_ID, Ptrn_ID);
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("--- Select ---", "-1"));
            foreach (DataRow row in dt.Rows)
            {
                ddlBranch.Items.Add(new ListItem(row["Text"].ToString(), row["Value"].ToString()));
            }
        }

        #endregion


        #region fillCoursewisePart

        void fillCoursewisePart(string ExamEvent, string Faculty, string Course, string MoLrn_ID, string Ptrn_ID, string BranchID)
        {
            DataTable dt = SRVSchedule.ExamEventWiseCourseWisePart(ExamEventID, FacultyID, Course, MoLrn_ID, Ptrn_ID, BranchID);
            if (dt.Rows.Count > 0)
            {
                ddlCoursePart.Items.Clear();
                ddlCoursePart.Items.Add(new ListItem("--- Select ---", "-1"));
                foreach (DataRow row in dt.Rows)
                {
                    ddlCoursePart.Items.Add(new ListItem(row["Text"].ToString(), row["Value"].ToString()));
                }
            }
            else
            {
                ddlCoursePart.Items.Clear();
            }
        }
        #endregion

        #region fillCoursePartWiseTerm

        void fillCoursePartWiseTerm(string ExamEvent, string Faculty, string Course, string MoLrn_ID, string Ptrn_ID, string BranchID, string CoursePartDetailsID)
        {
            DataTable dt = SRVSchedule.ExamEventWiseCourseWisePartTerm(ExamEvent, Faculty, Course, MoLrn_ID, Ptrn_ID, BranchID, CoursePartDetailsID);
            if (dt.Rows.Count > 0)
            {
                ddlCoursePartTerm.Items.Clear();
                ddlCoursePartTerm.Items.Add(new ListItem("--- Select ---", "-1"));
                foreach (DataRow row in dt.Rows)
                {
                    ddlCoursePartTerm.Items.Add(new ListItem(row["Text"].ToString(), row["Value"].ToString()));
                }
            }
            else
            {
                ddlCoursePartTerm.Items.Clear();
            }
        }
        #endregion


        #region fillExamPaper

        public void fillExamPaper(string facID, string courseID, string MOLrnID, string patternID, string branchID, string coursePart, string coursePartTerm, string Inst_ID)
        {
            DataTable dtExamPaper = SRVScheduleCenter.ListCourseWisePaper(facID, courseID, MOLrnID, patternID, branchID, coursePart, coursePartTerm, Inst_ID);
            ddlpaper.Items.Clear();
            ddlpaper.Items.Add(new ListItem("--- Select ---", "-1"));
            foreach (DataRow drExamPaper in dtExamPaper.Rows)
            {
                ddlpaper.Items.Add(new ListItem(drExamPaper["Text"].ToString(), drExamPaper["Value"].ToString()));
            }
        }

        #endregion




        #region InitializeDropDown

        public void InitializeDropDown(string ch)
        {


            switch (ch)
            {
                case "0":// initialize all

                    ddlEvent.Items.Clear();
                    ddlEvent.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlFaculty.Items.Clear();
                    ddlFaculty.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlCourseName.Items.Clear();
                    ddlCourseName.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlCoursePart.Items.Clear();
                    ddlCoursePart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlCoursePartTerm.Items.Clear();
                    ddlCoursePartTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlpaper.Items.Clear();
                    ddlpaper.Items.Add(new ListItem("--- Select ---", "-1"));
                    break;

                #region Case '1'
                case "1":

                    ddlFaculty.Items.Clear();
                    ddlFaculty.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlCourseName.Items.Clear();
                    ddlCourseName.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlCoursePart.Items.Clear();
                    ddlCoursePart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlCoursePartTerm.Items.Clear();
                    ddlCoursePartTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlpaper.Items.Clear();
                    ddlpaper.Items.Add(new ListItem("--- Select ---", "-1"));

                    break;
                #endregion

                #region Case '2'
                case "2":

                    ddlCourseName.Items.Clear();
                    ddlCourseName.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlCoursePart.Items.Clear();
                    ddlCoursePart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlCoursePartTerm.Items.Clear();
                    ddlCoursePartTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlpaper.Items.Clear();
                    ddlpaper.Items.Add(new ListItem("--- Select ---", "-1"));

                    break;
                #endregion

                #region Case '3'
                case "3":

                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlCoursePart.Items.Clear();
                    ddlCoursePart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlCoursePartTerm.Items.Clear();
                    ddlCoursePartTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlpaper.Items.Clear();
                    ddlpaper.Items.Add(new ListItem("--- Select ---", "-1"));

                    break;
                #endregion

                #region Case '4'
                case "4":

                    ddlCoursePart.Items.Clear();
                    ddlCoursePart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlCoursePartTerm.Items.Clear();
                    ddlCoursePartTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlpaper.Items.Clear();
                    ddlpaper.Items.Add(new ListItem("--- Select ---", "-1"));

                    break;
                #endregion

                #region Case '5'
                case "5":
                    ddlCoursePartTerm.Items.Clear();
                    ddlCoursePartTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlpaper.Items.Clear();
                    ddlpaper.Items.Add(new ListItem("--- Select ---", "-1"));

                    break;
                #endregion
                #region Case '6'
                case "6":

                    ddlpaper.Items.Clear();
                    ddlpaper.Items.Add(new ListItem("--- Select ---", "-1"));

                    break;
                    #endregion


            }
        }


        #endregion

        #endregion
        #region btnSave_Click
        // Added By GaneswarM on 09 Nov 2022
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Check Paper list Present in database or not
            SRVSecurePaper svr = new SRVSecurePaper();


            string[] strCourse = ddlCourseName.SelectedValue.Trim().Split(',');
            DataTable dt = svr.InsertRequestPaperSetterStatus(FacultyID, strCourse[0], strCourse[1], strCourse[2], BranchID, CoursePartDetailsID,
                                                    CoursePartChildID, ExamEventID,
                                                    SelectedPaperID, pp_setterId, Inst_ID, ((clsUser)Session["User"]).User_ID);

            if (dt != null && dt.Rows.Count > 0)
            {
                string STATUSMESSAGE = dt.Rows[0]["STATUSMESSAGE"].ToString();
                lblMsg.Text = STATUSMESSAGE;
                if (STATUSMESSAGE == "Request Created Successfully.")
                {
                    lblMsg.ForeColor = Color.Green;
                }
                else
                {
                    lblMsg.ForeColor = Color.Red;
                }
                //ClearSelection();
            }

        }
        #endregion
        #region lnkNewRequest_Click
        protected void lnkNewRequest_Click(object sender, EventArgs e)
        {
            divDropDown.Style.Add("display", "block");
            divGrid.Style.Add("display", "none");
            lnkExistingReq.Enabled = true;
            lnkNewRequest.Enabled = false;
            ClearSelection();
        }
        #endregion
        #region lnkExistingReq_Click    
        protected void lnkExistingReq_Click(object sender, EventArgs e)
        {
            FillGrid();
            divDropDown.Style.Add("display", "none");
            divGrid.Style.Add("display", "block");
            lnkExistingReq.Enabled = false;
            lnkNewRequest.Enabled = true;
        }
        #endregion

        #region ClearSelection: Yojana on 20 Jul 2019 for #143435

        private void ClearSelection()
        {
            lblMsg.Text = "";
            ddlFaculty.SelectedValue = "-1";
            InitializeDropDown("1");
        }


        #endregion

        #region FillGrid
        public void FillGrid()
        {
            DataTable dtList = new DataTable();
            SRVSecurePaper svr = new SRVSecurePaper();
            dtList = svr.ListCourseWisePaperRequest(Inst_ID);
            if (dtList != null)
            {
                if (dtList.Rows.Count > 0)
                {
                    gvCoursePaper.DataSource = dtList;
                    gvCoursePaper.DataBind();
                    divDropDown.Style.Add("display", "none");
                    divGrid.Style.Add("display", "block");
                    lnkNewRequest.Enabled = true;
                    lnkExistingReq.Enabled = false;
                }
                else
                {
                    divDropDown.Style.Add("display", "block");
                    divGrid.Style.Add("display", "none");
                    lnkNewRequest.Enabled = false;
                    lnkExistingReq.Enabled = true;
                }
            }
            else
            {
                divDropDown.Style.Add("display", "block");
                divGrid.Style.Add("display", "none");
                lnkNewRequest.Enabled = false;
                lnkExistingReq.Enabled = true;
            }
        }
        #endregion

        protected void gvCoursePaper_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //WebControl wc = e.CommandSource as WebControl;
            //GridViewRow row = wc.NamingContainer as GridViewRow;

            //if (row != null)
            //{
            if (e.CommandName == "Delete")
            {
                WebControl wc = e.CommandSource as WebControl;
                GridViewRow row = wc.NamingContainer as GridViewRow;
                int index = row.RowIndex;
                string fk_PpSetterId = e.CommandArgument.ToString();
                GridViewRow selectedRow = gvCoursePaper.Rows[index];
                HiddenField hidPpSetterId = selectedRow.FindControl("hidPpSetterId") as HiddenField;
                if (hidPpSetterId.Value != "")
                {
                    oHt["Pk_Uni_ID"] = clsGetSettings.UniversityID.ToString();
                    oHt["Pk_Inst_ID"] = hidInstID.Value;
                    oHt["fk_PpSetterId"] = fk_PpSetterId;
                    oHt["Created_By"] = ((clsUser)Session["User"]).User_ID;

                    SRVSecurePaper svr = new SRVSecurePaper();
                    DataTable dt = svr.DeleteCourseWisePaperRequest(hidInstID.Value, hidPpSetterId.Value, ((clsUser)Session["User"]).User_ID);

                    //gvCoursePaper.DeleteRow(row.RowIndex);
                }
                FillGrid();
            }
            else if (e.CommandName == "Accept")
            {
                WebControl wc = e.CommandSource as WebControl;
                GridViewRow row = wc.NamingContainer as GridViewRow;
                int index = row.RowIndex;
                GridViewRow selectedRow = gvCoursePaper.Rows[index];
                HiddenField hidPpSetterId = selectedRow.FindControl("hidPpSetterId") as HiddenField;

                if (hidPpSetterId.Value != "")
                {
                    oHt["Pk_Uni_ID"] = clsGetSettings.UniversityID.ToString();
                    oHt["Pk_Inst_ID"] = hidInstID.Value;
                    oHt["fk_PpSetterId"] = hidPpSetterId.Value;
                    oHt["Created_By"] = ((clsUser)Session["User"]).User_ID;

                    SRVSecurePaper svr = new SRVSecurePaper();

                }
            }
            //}

        }

        protected void gvCoursePaper_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvCoursePaper.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lblApproveStatus = (Label)row.FindControl("lblApproveStatus");
                    LinkButton btnRemovePaper = (LinkButton)row.FindControl("btnRemovePaper"); 
                    //string ApproveStatus = (row.FindControl("lblApproveStatus") as HtmlGenericControl).InnerHtml.Trim();

                    if (lblApproveStatus.Text.Trim() == "Approved")
                    {
                        lblApproveStatus.ForeColor = Color.Green;
                        btnRemovePaper.Visible = false;
                    }
                    else if (lblApproveStatus.Text.Trim() == "Rejected")
                    {
                        lblApproveStatus.ForeColor = Color.Red;
                        btnRemovePaper.Visible = false;
                    }
                    else 
                        btnRemovePaper.Visible = true;
                }
            }
        }
        public void gvCoursePaper_RowDeleting(Object sender, GridViewDeleteEventArgs e)
        {

        }
        
    }
}
