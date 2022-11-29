using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PreExamClstLib.Services;
using PreExamClsLib.Services;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SRPD_PaperSetterApproval_M : System.Web.UI.Page
    {
        #region Variables

        ScriptManager scriptManager;
        //string PaperSetterID = "1";
        private DataTable dt = new DataTable("Form Entry");
        string Inst_ID = "19";

        #endregion

        #region public variables

        public ScriptManager ScriptManager
        {
            set { scriptManager = value; }
        }

        public string ExamEventID
        {
            get
            {
                return ddlExamEvent.SelectedValue.ToString();
            }
        }

        public string FacultyID
        {
            get
            {
                return ddlFaculty.SelectedValue.ToString();
            }
        }
        
        public string BranchID
        {
            get
            {
                return ddlBranch.SelectedValue.ToString();
            }
        }

        public string CourseID
        {
            get
            {
                return ddlCourse.SelectedValue.ToString();
            }
        }

        public string PartID
        {
            get
            {
                return ddlPart.SelectedValue.ToString();
            }
        }

        public string TermID
        {
            get
            {
                return ddlTerm.SelectedValue.ToString();
            }
        }

        public string CoursePartDetailsID
        {
            get
            {
                return ddlPart.SelectedValue.ToString();
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            //CreateGrid();
            if (!IsPostBack)
            {
                ProgramSelectionWise.Visible = false;
                GridViewandButton.Visible = false;

                FillExamEvent();
                hidInstID.Value = "19";
            }
        }

        #region Grid

        public void CreateGrid()
        {
            dt.Columns.Add("Sr.No.");
            dt.Columns.Add("College Code");
            dt.Columns.Add("College Name");
            dt.Columns.Add("Paper Setter Name");
            dt.Columns.Add("Program Full Name");
            dt.Columns.Add("Paper Code");
            dt.Columns.Add("Paper Name");
            dt.Columns.Add("Registration Date");
            dt.Columns.Add("Approve and Assign Role");
        }

        public void FillGrid()
        {           
            
            SRVSrpd srv = new SRVSrpd();
            string[] strCourse = ddlCourse.SelectedValue.Trim().Split(',');
            dt = srv.UpdatePaperSetterApproval(Inst_ID, ExamEventID, FacultyID, strCourse[0], strCourse[1], strCourse[2], BranchID, PartID, TermID);
            gvSearchData.DataSource = dt;
            gvSearchData.DataBind();
        }

        #endregion

        #region Events

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(RadioButtonList1.SelectedValue.ToString())
            {
                case "ProgramSelectionWise":
                        ProgramSelectionWise.Visible = true;
                        GridViewandButton.Visible = false;
                        break;
                case "PaperCodeWise":
                        ProgramSelectionWise.Visible = false;
                        GridViewandButton.Visible = true;
                        FillGrid();
                        break;
                case "PaperSetterNameWise":
                        ProgramSelectionWise.Visible = false;
                        GridViewandButton.Visible = true;
                        FillGrid();
                        break;
                case "AllSettersWise":
                        ProgramSelectionWise.Visible = false;
                        GridViewandButton.Visible = true;
                        FillGrid();
                        break;
            }
        }
        
        protected void ddlExamEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("1");
            if (ExamEventID.ToString() != "-1")
            {
                FillEventWiseFaculty(ExamEventID);
                if (scriptManager != null)
                {
                    scriptManager.SetFocus(ddlExamEvent);
                }

                hidEventID.Value = ddlExamEvent.SelectedItem.Value.Trim();
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

        protected void ddlCourse_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("3");
            if (ddlCourse.SelectedValue.ToString() != "-1")
            {
                string[] strCourse = ddlCourse.SelectedValue.Trim().Split(',');
                FillCoursewiseBranch(ExamEventID, FacultyID, strCourse[0], strCourse[1], strCourse[2]);
                if (scriptManager != null)
                {
                    scriptManager.SetFocus(ddlCourse);
                }
                hidCourseID.Value = ddlCourse.SelectedValue;
            }
        }
        
        protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("4");
            if (ddlBranch.SelectedValue.ToString() != "-1")
            {
                string[] strCourse = ddlCourse.SelectedValue.Trim().Split(',');
                FillCoursewisePart(ExamEventID, FacultyID, strCourse[0], strCourse[1], strCourse[2], BranchID);
                if (scriptManager != null)
                {
                    scriptManager.SetFocus(ddlBranch);
                }
                hidBranchID.Value = ddlBranch.SelectedValue;
            }
        }

        protected void ddlPart_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("5");
            if (ddlPart.SelectedValue.ToString() != "-1")
            {
                string[] strCourse = ddlCourse.SelectedValue.Trim().Split(',');
                FillCoursePartWiseTerm(ExamEventID, ddlFaculty.SelectedItem.Value.Trim(), strCourse[0], strCourse[1], strCourse[2], BranchID, CoursePartDetailsID);
                if (scriptManager != null)
                {
                    scriptManager.SetFocus(ddlPart);
                }

                hidPartID.Value = ddlPart.SelectedItem.Value.Trim();
            }
        }

        #endregion

        #region ButtonOperations

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            FillGrid();
            GridViewandButton.Visible = true;
        }

        #endregion

        #region OtherFunctions

        #region InitializeDropDown

        public void InitializeDropDown(string ch)
        {


            switch (ch)
            {
                case "0":// initialize all

                    ddlExamEvent.Items.Clear();
                    ddlExamEvent.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlFaculty.Items.Clear();
                    ddlFaculty.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlCourse.Items.Clear();
                    ddlCourse.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlPart.Items.Clear();
                    ddlPart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));
                    break;

                #region Case '1'
                case "1":

                    ddlFaculty.Items.Clear();
                    ddlFaculty.Items.Add(new ListItem("--- Select ---", "-1"));
                    
                    ddlCourse.Items.Clear();
                    ddlCourse.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlPart.Items.Clear();
                    ddlPart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    break;
                #endregion

                #region Case '2'
                case "2":

                    ddlCourse.Items.Clear();
                    ddlCourse.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlPart.Items.Clear();
                    ddlPart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    break;
                #endregion

                #region Case '3'
                case "3":
                    
                    ddlBranch.Items.Clear();
                    ddlBranch.Items.Add(new ListItem("--- Select ---", "-1"));
                   
                    ddlPart.Items.Clear();
                    ddlPart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));
                    break;
                #endregion

                #region Case '4'
                case "4":
                    
                    ddlPart.Items.Clear();
                    ddlPart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    break;
                #endregion

                #region Case '5'
                case "5":

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    break;
                #endregion
            }
        }

        #endregion

        private void FillExamEvent()
        {
            SRVExamEvent srv = new SRVExamEvent();
            DataTable dt = srv.ListExamEvent();

            ddlExamEvent.Items.Clear();
            ddlExamEvent.Items.Add(new ListItem("--- Select ---", "-1"));

            foreach (DataRow row in dt.Rows)
            {
                ddlExamEvent.Items.Add(new ListItem(row["ExamEvent"].ToString(), row["ExamEventID"].ToString()));
            }
        }
        
        private void FillEventWiseFaculty(string Event)
        {
            DataTable dt = SRVScheduleCenter.ExamEventWiseFacultyList(ExamEventID);
            ddlFaculty.Items.Clear();
            ddlFaculty.Items.Add(new ListItem("--- Select ---", "-1"));

            foreach (DataRow row in dt.Rows)
            {
                ddlFaculty.Items.Add(new ListItem(row["Text"].ToString(), row["Value"].ToString()));
            }
        }

        private void FillfacultywiseCourse(string ExamEvent, string Faculty)
        {
            DataTable dt = SRVSchedule.ExamEventWiseCourse(ExamEventID, FacultyID);
            ddlCourse.Items.Clear();
            ddlCourse.Items.Add(new ListItem("--- Select ---", "-1"));
            foreach (DataRow row in dt.Rows)
            {
                ddlCourse.Items.Add(new ListItem(row["Text"].ToString(), row["Value"].ToString()));
            }
        }

        private void FillCoursewiseBranch(string ExamEvent, string Faculty, string Cr_ID, string MoLrn_ID, string Ptrn_ID)
        {
            DataTable dt = SRVSchedule.ExamEventWiseCourseWiseBranch(ExamEventID, FacultyID, Cr_ID, MoLrn_ID, Ptrn_ID);
            ddlBranch.Items.Clear();
            ddlBranch.Items.Add(new ListItem("--- Select ---", "-1"));
            foreach (DataRow row in dt.Rows)
            {
                ddlBranch.Items.Add(new ListItem(row["Text"].ToString(), row["Value"].ToString()));
            }
        }

        private void FillCoursewisePart(string ExamEvent, string Faculty, string Course, string MoLrn_ID, string Ptrn_ID, string BranchID)
        {
            DataTable dt = SRVSchedule.ExamEventWiseCourseWisePart(ExamEventID, FacultyID, Course, MoLrn_ID, Ptrn_ID, BranchID);
            if (dt.Rows.Count > 0)
            {
                ddlPart.Items.Clear();
                ddlPart.Items.Add(new ListItem("--- Select ---", "-1"));
                foreach (DataRow row in dt.Rows)
                {
                    ddlPart.Items.Add(new ListItem(row["Text"].ToString(), row["Value"].ToString()));
                }
            }
            else
            {
                ddlPart.Items.Clear();
            }
        }

        private void FillCoursePartWiseTerm(string ExamEvent, string Faculty, string Course, string MoLrn_ID, string Ptrn_ID, string BranchID, string CoursePartDetailsID)
        {
            DataTable dt = SRVSchedule.ExamEventWiseCourseWisePartTerm(ExamEvent, Faculty, Course, MoLrn_ID, Ptrn_ID, BranchID, CoursePartDetailsID);
            if (dt.Rows.Count > 0)
            {
                ddlTerm.Items.Clear();
                ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));
                foreach (DataRow row in dt.Rows)
                {
                    ddlTerm.Items.Add(new ListItem(row["Text"].ToString(), row["Value"].ToString()));
                }
            }
            else
            {
                ddlTerm.Items.Clear();
            }
        }

        #endregion

    }
}