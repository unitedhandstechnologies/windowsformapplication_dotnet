using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using PreExamClstLib.Services;
using PreExamClsLib.Services;
using Classes;
using System.Drawing;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SRPD_PaperSetterApproval_M : System.Web.UI.Page
    {        
        #region Variables

        SRVSrpd srv = new SRVSrpd();
        ScriptManager scriptManager;
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

        public string PaperID
        {
            get
            {
                return ddlPaper.SelectedValue.ToString();
            }
        }

        public string PaperCode
        {
            get
            {
                return txtPaperCode.Text.ToString();
            }
        }

        public string PaperSetterName
        {
            get
            {
                return txtPaperSetterName.Text.ToString();
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProgramSelectionWise.Visible = true;
                PaperCodeWise.Visible = false;
                PaperSetterNameWise.Visible = false;
                GridViewandButton.Visible = false;

                FillExamEvent();
                hidInstID.Value = "19";

                InitializeDropDown("1");
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

        public void FillGridProgSectionWise()
        {
            if(srv == null)
                srv = new SRVSrpd();
                        
            string[] strCourse = ddlCourse.SelectedValue.Trim().Split(',');
            dt = srv.GetPaperSetterProgSectionWise(ExamEventID, FacultyID, strCourse[0], strCourse[1], strCourse[2], BranchID, PartID, TermID,PaperID);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblmsg.Text = "";
                gvProgSelectionWise.DataSource = dt;
                gvProgSelectionWise.DataBind();
                gvProgSelectionWise.Visible = true;
                //btnSave.Visible = true;
                //btnBack.Visible = true;
            }
            else
            {
                lblmsg.Text = "No Data Found For Approval";
                lblmsg.ForeColor = Color.Red;
                gvProgSelectionWise.Visible = false;
                btnSave.Visible = false;
                //btnBack.Visible = false;
            }
            
            lblmsg.Visible = true;
           
        }

        public void FillGridPaperCodeWise()
        {
            if (srv == null)
                srv = new SRVSrpd();
            
            dt = srv.GetPaperSetterPaperCodeWise(ExamEventID,PaperCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblmsg.Text = "";
                gvProgSelectionWise.DataSource = dt;
                gvProgSelectionWise.DataBind();
                gvProgSelectionWise.Visible = true;
                //btnSave.Visible = true;
            }
            else
            {
                lblmsg.Text = "No Data Found For Approval";
                lblmsg.ForeColor = Color.Red;
                gvProgSelectionWise.Visible = false;
                btnSave.Visible = false;
                txtPaperCode.Text = "";
            }            
            lblmsg.Visible = true;
        }

        public void FillGridPaperSetterNameWise()
        {
            if (srv == null)
                srv = new SRVSrpd();
                        
            dt = srv.GetPaperSetterPaperSetterNameWise(ExamEventID, PaperSetterName);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblmsg.Text = "";
                gvProgSelectionWise.DataSource = dt;
                gvProgSelectionWise.DataBind();
                gvProgSelectionWise.Visible = true;
               // btnSave.Visible = true;
            }
            else
            {
                lblmsg.Text = "No Data Found For Approval";
                lblmsg.ForeColor = Color.Red;
                gvProgSelectionWise.Visible = false;
                btnSave.Visible = false;
                txtPaperSetterName.Text = "";
            }
            lblmsg.Visible = true;
        }

        public void FillGridAllSetterWise()
        {
            if (srv == null)
                srv = new SRVSrpd();

            dt = srv.GetPaperSetterAllSetterWise(ExamEventID);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblmsg.Text = "";
                gvProgSelectionWise.DataSource = dt;
                gvProgSelectionWise.DataBind();
                gvProgSelectionWise.Visible = true;
                //btnSave.Visible = true;
            }
            else
            {
                lblmsg.Text = "No Data Found For Approval";
                lblmsg.ForeColor = Color.Red;
                gvProgSelectionWise.Visible = false;
                btnSave.Visible = false;
            }
            lblmsg.Visible = true;
        }

        #endregion

        #region Events

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (RadioButtonList1.SelectedValue.ToString())
            {
                case "ProgramSelectionWise":
                    ProgramSelectionWiseInitialization();
                    ExamEventIndexChange();
                    break;
                case "PaperCodeWise":
                    PaperCodeWiseInitialization();
                    break;
                case "PaperSetterNameWise":
                    PaperSetterNameWiseInitialization();  
                    break;
                case "AllSettersWise":
                    AllSettersWiseInitialization();
                    break;
            }
        }

        protected void ddlExamEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (RadioButtonList1.SelectedValue.ToString())
            {
                case "ProgramSelectionWise":
                    ProgramSelectionWiseInitialization();
                    break;
                case "PaperCodeWise":
                    PaperCodeWiseInitialization();
                    break;
                case "PaperSetterNameWise":
                    PaperSetterNameWiseInitialization();
                    break;
                case "AllSettersWise":
                    AllSettersWiseInitialization();
                    break;

            }
            ExamEventIndexChange();
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

                hidCourseID.Value = strCourse[0].ToString();
                hidMoLrnID.Value = strCourse[1].ToString();
                hidPtrnID.Value = strCourse[2].ToString();
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

                hidCoursePartID.Value = ddlPart.SelectedItem.Value.Trim();
            }
        }

        protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("6");

            if (ddlTerm.SelectedValue.ToString() != "-1")
            {
                string[] strCourse = ddlCourse.SelectedValue.Trim().Split(',');
                FillExamPaper(FacultyID, strCourse[0], strCourse[1], strCourse[2], BranchID, CoursePartDetailsID, TermID, Inst_ID);
                if (scriptManager != null)
                {
                    scriptManager.SetFocus(ddlTerm);
                }

                hidCoursePartTermID.Value = ddlTerm.SelectedItem.Value.Trim();
            }
        }

        protected void gvUpdateRole_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlRole = (e.Row.FindControl("ddlRole") as DropDownList);
                    if (srv == null)
                        srv = new SRVSrpd();
                    DataTable dt1 = new DataTable();
                    dt1 = srv.ListPaperSetterRole();
                    ddlRole.DataSource = dt1;
                    ddlRole.DataTextField = "RoleName";
                    ddlRole.DataValueField = "ID";
                    ddlRole.DataBind();

                    ddlRole.Items.Insert(0, new ListItem("--- Select ---","-1"));
                }
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region ButtonOperations

        protected void btnSearch_Click(object sender, EventArgs e)
        {
           // lblSaveMsg.Visible = false;
            FillGridProgSectionWise();
            GridViewandButton.Visible = true;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            switch (RadioButtonList1.SelectedValue.ToString())
            {
                case "ProgramSelectionWise":
                    SaveProgramSelectionWise();
                    break;
                case "PaperCodeWise":
                    SavePaperCodeWise();
                    FillGridPaperCodeWise();
                    break;
                case "PaperSetterNameWise":
                    SavePaperCodeWise();
                    FillGridPaperSetterNameWise();
                    break;
                case "AllSettersWise":
                    SavePaperCodeWise();
                    FillGridAllSetterWise();
                    break;
            }
        }
        
        protected void btnSearchPaperCodeWise_Click(object sender, EventArgs e)
        {
            lblSaveMsg.Text = "";
            FillGridPaperCodeWise();
            GridViewandButton.Visible = true;
            hidPaperCode.Value = txtPaperCode.Text;
            btnSave.Visible = false;
        }

        protected void btnSearchPaperSetterNameWise_Click(object sender, EventArgs e)
        {
            lblSaveMsg.Text = "";
            hidPaperSetterName.Value = txtPaperSetterName.Text;
            FillGridPaperSetterNameWise();
            GridViewandButton.Visible = true;            
            btnSave.Visible = false;
        }
                
        #endregion

        #region OtherFunctions

        #region InitializeDropDown

        public void InitializeDropDown(string ch)
        {


            switch (ch)
            {
                #region Case '0'
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

                    ddlPaper.Items.Clear();
                    ddlPaper.Items.Add(new ListItem("--- Select ---", "-1"));
                    break;

                #endregion

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

                    ddlPaper.Items.Clear();
                    ddlPaper.Items.Add(new ListItem("--- Select ---", "-1"));
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

                    ddlPaper.Items.Clear();
                    ddlPaper.Items.Add(new ListItem("--- Select ---", "-1"));
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

                    ddlPaper.Items.Clear();
                    ddlPaper.Items.Add(new ListItem("--- Select ---", "-1"));
                    break;
                #endregion

                #region Case '4'
                case "4":

                    ddlPart.Items.Clear();
                    ddlPart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlPaper.Items.Clear();
                    ddlPaper.Items.Add(new ListItem("--- Select ---", "-1"));
                    break;
                #endregion

                #region Case '5'
                case "5":

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlPaper.Items.Clear();
                    ddlPaper.Items.Add(new ListItem("--- Select ---", "-1"));
                    break;
                #endregion

                #region Case '6'
                case "6":

                    ddlPaper.Items.Clear();
                    ddlPaper.Items.Add(new ListItem("--- Select ---", "-1"));
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

        public void FillExamPaper(string facID, string courseID, string MOLrnID, string patternID, string branchID, string coursePart, string coursePartTerm, string Inst_ID)
        {
            DataTable dtExamPaper = SRVScheduleCenter.ListCourseWisePaper(facID, courseID, MOLrnID, patternID, branchID, coursePart, coursePartTerm, Inst_ID);
            if  (dtExamPaper.Rows.Count > 0)
            {
                ddlPaper.Items.Clear();
                ddlPaper.Items.Add(new ListItem("--- Select ---", "-1"));
                foreach (DataRow drExamPaper in dtExamPaper.Rows)
                {
                    ddlPaper.Items.Add(new ListItem(drExamPaper["Text"].ToString(), drExamPaper["Value"].ToString()));
                }
            }
            else
            {
                ddlPaper.Items.Clear();
            }
        }

        public void ProgramSelectionWiseInitialization()
        {
            gvProgSelectionWise.DataSource = null;
            gvProgSelectionWise.DataBind();
            ProgramSelectionWise.Visible = true;
            PaperCodeWise.Visible = false;
            PaperSetterNameWise.Visible = false;
            btnSave.Visible = false;
            GridViewandButton.Visible = false;
            lblmsg.Visible = false;
            lblSaveMsg.Visible = false;
            InitializeDropDown("1");
            gvUpdateRole.Visible = false;
            btnBack.Visible = false;
        }

        public void PaperCodeWiseInitialization()
        {
            gvProgSelectionWise.DataSource = null;
            gvProgSelectionWise.DataBind();
            txtPaperCode.Text = "";
            ProgramSelectionWise.Visible = false;
            PaperCodeWise.Visible = true;
            PaperSetterNameWise.Visible = false;
            btnSave.Visible = false;
            GridViewandButton.Visible = false;
            lblmsg.Visible = false;
            lblSaveMsg.Visible = false;
            gvUpdateRole.Visible = false;
            btnBack.Visible = false;
        }

        public void PaperSetterNameWiseInitialization()
        {
            gvProgSelectionWise.DataSource = null;
            gvProgSelectionWise.DataBind();
            txtPaperSetterName.Text = "";
            ProgramSelectionWise.Visible = false;
            PaperCodeWise.Visible = false;
            PaperSetterNameWise.Visible = true;
            GridViewandButton.Visible = false;
            btnSave.Visible = false;
            lblmsg.Visible = false;
            lblSaveMsg.Visible = false;
            gvUpdateRole.Visible = false;
            btnBack.Visible = false;
        }

        public void AllSettersWiseInitialization()
        {
            gvProgSelectionWise.DataSource = null;
            gvProgSelectionWise.DataBind();
            lblmsg.Text = "";
            ProgramSelectionWise.Visible = false;
            PaperCodeWise.Visible = false;
            PaperSetterNameWise.Visible = false;
            FillGridAllSetterWise();
            GridViewandButton.Visible = true;
            lblSaveMsg.Visible = false;
            gvUpdateRole.Visible = false;
            btnSave.Visible = false;
            btnBack.Visible = false;
        }

        public void SaveProgramSelectionWise()
        {
            foreach (GridViewRow row in gvUpdateRole.Rows)
            {
                DropDownList ddlRole = (DropDownList)row.FindControl("ddlRole");
                if (ddlRole.SelectedValue != "-1")
                {
                    Label lblPaperCode = (Label)row.FindControl("lblPaperCode");

                    if (srv == null)
                        srv = new SRVSrpd();
                    DataTable dt = srv.UpdatePaperSetterApprovalProgramSectionWise(hidEventID.Value, hidFacultyID.Value, hidCourseID.Value, hidMoLrnID.Value,
                                       hidPtrnID.Value, hidBranchID.Value, hidCoursePartID.Value, hidCoursePartTermID.Value, lblPaperCode.Text, ddlRole.SelectedValue, ((clsUser)Session["User"]).User_ID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblSaveMsg.Text = "Approved Successfully.";
                        lblSaveMsg.ForeColor = Color.Green;
                        lblSaveMsg.Visible = true; 
                    }
                }
            }
            //FillGridProgSectionWise();
            //lblmsg.Visible = false;
            gvProgSelectionWise.Visible = false;
            btnSave.Visible = false;
            btnBack.Visible = false;
            InitializeDropDown("1");
            gvUpdateRole.Visible = false;
        }

        public void SavePaperCodeWise()
        {
            foreach (GridViewRow row in gvUpdateRole.Rows)
            {
                DropDownList ddlRole = (DropDownList)row.FindControl("ddlRole");
                if (ddlRole.SelectedValue != "-1")
                {
                    Label lblPaperCode = (Label)row.FindControl("lblPaperCode");

                    if (srv == null)
                        srv = new SRVSrpd();
                    DataTable dt = srv.UpdatePaperSetterApprovalPaperCodeWise(hidEventID.Value.ToString(), lblPaperCode.Text, ddlRole.SelectedValue.ToString(), ((clsUser)Session["User"]).User_ID);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblSaveMsg.Text = "Approved Successfully.";
                        lblSaveMsg.ForeColor = Color.Green;
                        lblSaveMsg.Visible = true;
                        
                    }
                }
            }
            gvProgSelectionWise.Visible = false;
            btnSave.Visible = false;
            btnBack.Visible = false;
            gvUpdateRole.Visible = false;
            //txtPaperCode.Text = "";
            //txtPaperSetterName.Text = "";
        }

        public void ExamEventIndexChange()
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

        #endregion

        
        protected void chkSelectRole_CheckedChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in gvProgSelectionWise.Rows)
            {
                CheckBox checkbx = (CheckBox)row.FindControl("chkSelectRole");
                if(checkbx.Checked == true)
                {
                    Label lblPaperCode = (Label)row.FindControl("lblPaperCode");
                    Label lblPaperSetterID = (Label)row.FindControl("lblPpSetterID");
                    string PaperCode = lblPaperCode.Text;
                    string PaperSetterID = lblPaperSetterID.Text;

                    if (srv == null)
                        srv = new SRVSrpd();

                    DataTable dt = srv.GetPaperSetterProgSectionWise(PaperSetterID, PaperCode);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        lblmsg.Text = "";
                        gvUpdateRole.DataSource = dt;
                        gvUpdateRole.DataBind();
                        gvUpdateRole.Visible = true;
                        gvProgSelectionWise.Visible = false;
                        btnSave.Visible = true;
                        btnBack.Visible = true;
                        lblSaveMsg.Visible = false;
                    }
                    else
                    {
                        lblmsg.Text = "No Data Found For Approval";
                        lblmsg.ForeColor = Color.Red;
                        gvProgSelectionWise.Visible = false;
                        gvUpdateRole.Visible = false;
                        btnSave.Visible = false;
                        btnBack.Visible = false;
                        lblSaveMsg.Visible = false;
                    }
                }
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            switch (RadioButtonList1.SelectedValue.ToString())
            {
                case "ProgramSelectionWise":
                    FillGridProgSectionWise();
                    break;
                case "PaperCodeWise":
                    FillGridPaperCodeWise();
                    hidPaperCode.Value = txtPaperCode.Text;
                    break;
                case "PaperSetterNameWise":
                    FillGridPaperSetterNameWise();
                    hidPaperSetterName.Value = txtPaperSetterName.Text;
                    break;
                case "AllSettersWise":
                    FillGridAllSetterWise();
                    break;
            }
            lblSaveMsg.Text = "";
            GridViewandButton.Visible = true;
            gvUpdateRole.Visible = false;
            btnBack.Visible = false;
            btnSave.Visible = false;
        }
        
    }
}