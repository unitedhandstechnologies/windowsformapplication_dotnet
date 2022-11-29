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

namespace SRPD
{
    public partial class PreExamV2_SRPD_PaperSetterApproval : System.Web.UI.Page
    {
        #region Variables
        ScriptManager scriptmanager;
        string PaperSetterID = "1";
        #endregion
        #region public variables

        public ScriptManager ScriptManager
        {
            set { scriptmanager = value; }
        }

        public string ExamEventID
        {
            get
            {
                return ddlExamEvent.SelectedValue.ToString();
            }
        }
        #endregion

        private DataTable dt = new DataTable("Form Entry");
        protected void Page_Load(object sender, EventArgs e)
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataRow dr = dt.NewRow();
            dr["Sr.No."] = "1";
            dr["College Code"] = "Clg01";
            dr["College Name"] = "SKCET";
            dr["Paper Setter Name"] = "Sindhu";
            dr["Program Full Name"] = "Module Exam";
            dr["Paper Code"] = "ENG01";
            dr["Paper Name"] = "English";
            dr["Registration Date"] = DateTime.Now;
            dr["Approve and Assign Role"] = "Chairman";
            dt.Rows.Add(dr);
            GridView1.DataSource = dt;
            GridView1.DataBind();
            GridView1.Visible = true;
            btnSave.Visible = true;
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonList1.SelectedValue == "ProgramSelectionWise")
            {
                GridView1.Visible = false;
                btnSave.Visible = false;
                lblColon1.Visible = true;
                lblColon2.Visible = true;
                lblColon3.Visible = true;
                lblColon4.Visible = true;
                lblSelectFaculty.Visible = true;
                lblSelectPart.Visible = true;
                lblSelectProgram.Visible = true;
                lblSelectTerm.Visible = true;
                ddlFaculty.Visible = true;
                ddlPart.Visible = true;
                ddlProgram.Visible = true;
                ddlTerm.Visible = true;
                btnSearch.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "PaperCodeWise")
            {
                lblColon1.Visible = false;
                lblColon2.Visible = false;
                lblColon3.Visible = false;
                lblColon4.Visible = false;
                lblSelectFaculty.Visible = false;
                lblSelectPart.Visible = false;
                lblSelectProgram.Visible = false;
                lblSelectTerm.Visible = false;
                ddlFaculty.Visible = false;
                ddlPart.Visible = false;
                ddlProgram.Visible = false;
                ddlTerm.Visible = false;
                btnSearch.Visible = false;


                DataRow dr = dt.NewRow();
                dr["Sr.No."] = "1";
                dr["College Code"] = "Clg01";
                dr["College Name"] = "SKCET";
                dr["Paper Setter Name"] = "Sindhu";
                dr["Program Full Name"] = "Module Exam";
                dr["Paper Code"] = "ENG01";
                dr["Paper Name"] = "English";
                dr["Registration Date"] = DateTime.Now;
                dr["Approve and Assign Role"] = "Chairman";
                dt.Rows.Add(dr);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
                btnSave.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "PaperSetterNameWise")
            {
                lblColon1.Visible = false;
                lblColon2.Visible = false;
                lblColon3.Visible = false;
                lblColon4.Visible = false;
                lblSelectFaculty.Visible = false;
                lblSelectPart.Visible = false;
                lblSelectProgram.Visible = false;
                lblSelectTerm.Visible = false;
                ddlFaculty.Visible = false;
                ddlPart.Visible = false;
                ddlProgram.Visible = false;
                ddlTerm.Visible = false;
                btnSearch.Visible = false;


                DataRow dr = dt.NewRow();
                dr["Sr.No."] = "1";
                dr["College Code"] = "Clg01";
                dr["College Name"] = "SKCET";
                dr["Paper Setter Name"] = "Sindhu";
                dr["Program Full Name"] = "Module Exam";
                dr["Paper Code"] = "ENG01";
                dr["Paper Name"] = "English";
                dr["Registration Date"] = DateTime.Now;
                dr["Approve and Assign Role"] = "Chairman";
                dt.Rows.Add(dr);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
                btnSave.Visible = true;
            }
            else if (RadioButtonList1.SelectedValue == "AllSettersWise")
            {
                lblColon1.Visible = false;
                lblColon2.Visible = false;
                lblColon3.Visible = false;
                lblColon4.Visible = false;
                lblSelectFaculty.Visible = false;
                lblSelectPart.Visible = false;
                lblSelectProgram.Visible = false;
                lblSelectTerm.Visible = false;
                ddlFaculty.Visible = false;
                ddlPart.Visible = false;
                ddlProgram.Visible = false;
                ddlTerm.Visible = false;
                btnSearch.Visible = false;


                DataRow dr = dt.NewRow();
                dr["Sr.No."] = "1";
                dr["College Code"] = "Clg01";
                dr["College Name"] = "SKCET";
                dr["Paper Setter Name"] = "Sindhu";
                dr["Program Full Name"] = "Module Exam";
                dr["Paper Code"] = "ENG01";
                dr["Paper Name"] = "English";
                dr["Registration Date"] = DateTime.Now;
                dr["Approve and Assign Role"] = "Chairman";
                dt.Rows.Add(dr);
                GridView1.DataSource = dt;
                GridView1.DataBind();
                GridView1.Visible = true;
                btnSave.Visible = true;
            }
        }

        protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
           /* InitializeDropDown("1");
            if (ddlFaculty.SelectedValue.ToString() != "-1")
                FillfacultywiseCourse(ExamEventID, FacultyID);
            if (scriptManager != null)
                scriptManager.SetFocus(ddlFaculty);
            hidFacultyID.Value = ddlFaculty.SelectedValue;*/
        }
        public void InitializeDropDown(string ch)
        {


            switch (ch)
            {
                case "0":// initialize all

                    ddlExamEvent.Items.Clear();
                    ddlExamEvent.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlFaculty.Items.Clear();
                    ddlFaculty.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlProgram.Items.Clear();
                    ddlProgram.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlPart.Items.Clear();
                    ddlPart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));

                    break;

                #region Case '1'
                case "1":

                    ddlFaculty.Items.Clear();
                    ddlFaculty.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlProgram.Items.Clear();
                    ddlProgram.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlPart.Items.Clear();
                    ddlPart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));


                    break;
                #endregion

                #region Case '2'
                case "2":

                    ddlProgram.Items.Clear();
                    ddlProgram.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlPart.Items.Clear();
                    ddlPart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));
                    break;
                #endregion

                #region Case '3'
                case "3":

                    ddlPart.Items.Clear();
                    ddlPart.Items.Add(new ListItem("--- Select ---", "-1"));

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));
                    break;
                
                #endregion

                #region Case '4'
                case "4":

                    ddlTerm.Items.Clear();
                    ddlTerm.Items.Add(new ListItem("--- Select ---", "-1"));
                    break;

                #endregion

            }
        }

        protected void ddlExamEvent_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitializeDropDown("1");
            if (ddlExamEvent.SelectedValue.ToString() != "-1")
            {
                //FillEventWisefaculty(ExamEventID);
                if (scriptmanager != null)
                {
                    scriptmanager.SetFocus(ddlExamEvent);
                }

               // hidEventID.Value = ddlExamEvent.SelectedItem.Value.Trim();
            }
        }
    }
}