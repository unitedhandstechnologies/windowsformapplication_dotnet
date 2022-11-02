using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using PreExamClstLib.Services;
using System.Data; 
using System.Data.SqlClient;
using System.Configuration;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SRPD_ImportPaperVenueFromExcel : System.Web.UI.Page
    {
       #region Variables

        string uniID = clsGetSettings.UniversityID.ToString();

        #endregion

        #region pageload
        protected void Page_Load(object sender, EventArgs e)
        {
           

            if (!IsPostBack)
            {
                FillExamEvent();
                mltv_Main.SetActiveView(ViewImport);
            }



            
            #region Code for Course selection control

            //CrSelectionCtrl.trExamAppearanceType.Visible = false;
            //CrSelectionCtrl.trCollegeList.Visible = false;
            //CrSelectionCtrl.btnProceed.Click += new EventHandler(btnProceed_Click);
            //CrSelectionCtrl.btnProceed.Text = "Proceed>>";

            
            

            #endregion

        }
        #endregion 

        #region FillExamEvent

        /// <summary>
        /// Filling ddlExamEvent dropdown with exam event.
        /// </summary>
        void FillExamEvent()
        {
            clsImportFromExcel srvExamEvent = new clsImportFromExcel();
            DataTable dt = srvExamEvent.ListExamEvent();
            if (dt != null)
            {
                ddlExamEvent.DataSource = dt;
                ddlExamEvent.DataTextField = "ExamEvent";
                ddlExamEvent.DataValueField = "ExamEventID";
                ddlExamEvent.DataBind();
                ddlExamEvent.Items.Insert(0, new ListItem("-- Select --", "0"));
                ddlExamEvent.SelectedIndex = 0; 
            
            }
            else
            {
                lblMsg.Visible = true;
                lblMsg.Text = "No exam event found!!!!!!";
                lblMsg.CssClass = "errorNote";
            }
        }

        #endregion

        #region Events

        #region btnProceed_Click

        protected void btnEventProceed_Click(object sender, EventArgs e)
        {
            //Setting hidden variable.
            SetHiddenVariables();

            Server.Transfer("PreExamV2_SRPD_ImportPaperVenueFromExcel__1.aspx");

        }

        #endregion

        #region btnProceed_Click

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            SetPublishHiddenVariables();
            DisplayGrid();
        }

        #endregion

        #endregion


        #region DisplayGrid

        private void DisplayGrid()
        {
            clsImportFromExcel oImportFromExcel = new clsImportFromExcel();
            BtnPublish.Visible = true;
            DataSet ds = new DataSet();
            ds = oImportFromExcel.ListPaperVenueForPublishCoursePartTermWise(hidFacultyID.Value, hidCourseID.Value, hidMolrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExamEvent.Value);
            if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
            {
                gvPaperVenue.Visible = true;
                gvPaperVenue.DataSource = ds.Tables[0];
                gvPaperVenue.DataBind();
                //btnSave.Visible = true;
            }
            else
            {
                gvPaperVenue.Visible = false;
                //Err_PersonalDetails.Text = "There is no record found.";
                //Err_PersonalDetails.CssClass = "errorNote";
                //btnSave.Visible = false;
                gvPaperVenue.DataSource = null;
                gvPaperVenue.DataBind();
            }
        }
        #endregion

        #region Other Functions



        #region SetHiddenVariables

        void SetHiddenVariables()
        {
            hidEventID.Value = ddlExamEvent.SelectedItem.Value;
        }

        #endregion

        protected void lnkNewRequest_Click(object sender, EventArgs e)
        {
            mltv_Main.SetActiveView(ViewImport);
        }

        #endregion

        protected void lnkExistingReq_Click(object sender, EventArgs e)
        {
            mltv_Main.SetActiveView(ViewPublish);
        }


        #region SetPublishHiddenVariables

        void SetPublishHiddenVariables()
        {

            string[] strArray = new string[3];

            
            hidFacultyID.Value = CrSelectionCtrl.FacultyID;
            strArray = CrSelectionCtrl.CourseID.Split(',');
            hidCourseID.Value = strArray[0];
            hidMolrnID.Value = strArray[1];
            hidPtrnID.Value = strArray[2];
            hidBrnID.Value = CrSelectionCtrl.BranchID;
            hidCrPrDetailsID.Value = CrSelectionCtrl.CoursePartDetailsID;
            hidCrPrChID.Value = CrSelectionCtrl.CoursePartChildID;
            hidExamEvent.Value = CrSelectionCtrl.ExamEventID; 

        }

        #endregion

        #region btnPublish_Click  
        protected void btnPublish_Click(object sender, EventArgs e)
        {
            clsImportFromExcel oImportFromExcel = new clsImportFromExcel();
            int result = 0;
            result = oImportFromExcel.UpdatePaperVenueToPublish(hidFacultyID.Value, hidCourseID.Value, hidMolrnID.Value, hidPtrnID.Value, hidBrnID.Value, hidCrPrDetailsID.Value, hidCrPrChID.Value, hidExamEvent.Value);
            if (result > 0)
            {
                lblMessgae.Visible = true;
                lblMessgae.Text = "Published Successfully..";
                lblMessgae.CssClass = "saveNote";
                //btnRemoveDataEntry.Enabled = false;
                //ChkRemove.Enabled = false;
                 DisplayGrid();
            }
            else
            {
                lblMessgae.Visible = true;
                lblMessgae.Text = "Not Published please contact admistrator.";
                lblMessgae.CssClass = "errorNote";

            }


        }
         #endregion



        #region gvPaperVenue_RowDataBound

        protected void gvPaperVenue_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton lnk = (LinkButton)e.Row.FindControl("lnkbtnRemove");
                //Access Cell values. 
                string Status = e.Row.Cells[5].Text;

               
                if (Status == "Published")
                {
                    lnk.Enabled = false;
                }
            }
        }

        #endregion

        #region gvPaperVenue_Sorting

        protected void gvPaperVenue_Sorting(object sender, GridViewSortEventArgs e)
        {

            DisplayGrid();
            DataTable dataTable = gvPaperVenue.DataSource as DataTable;
            DataView dataView = null;
            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    dataView = new DataView(dataTable);
                    dataView.Sort = e.SortExpression + " " + GetSortDirection(e.SortExpression);
                    gvPaperVenue.DataSource = dataView;
                    gvPaperVenue.DataBind();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (dataTable != null)
                    dataTable.Dispose();

                if (dataView != null)
                    dataView.Dispose();
            }
        }

        #endregion

        #region GetSortDirection

        private string GetSortDirection(string column)
        {

            // By default, set the sort direction to ascending.
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value can be returned.
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }

            // Save new values in ViewState.
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        #endregion

        protected void gvPaperVenue_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView grv = (GridView)sender;
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                string CrID = gvPaperVenue.DataKeys[id]["pk_Cr_ID"].ToString();
                string FacID = gvPaperVenue.DataKeys[id]["pk_Fac_ID"].ToString();
                string MoLrnID = gvPaperVenue.DataKeys[id]["pk_MoLrn_ID"].ToString();
                string PtrnID = gvPaperVenue.DataKeys[id]["pk_Ptrn_ID"].ToString();
                string BrnID = gvPaperVenue.DataKeys[id]["pk_Brn_ID"].ToString();
                string CrPrDetID = gvPaperVenue.DataKeys[id]["pk_CrPr_Details_ID"].ToString();
                string CrPrChID = gvPaperVenue.DataKeys[id]["pk_CrPrCh_ID"].ToString();
                string examevent = gvPaperVenue.DataKeys[id]["pk_ExamEvent_ID"].ToString();
                string PpPpHeadCrPrChID = gvPaperVenue.DataKeys[id]["pk_Pp_PpHead_CrPrCh_ID"].ToString();
                string fk_Center_ID = gvPaperVenue.DataKeys[id]["fk_Center_ID"].ToString();
                string fk_Inst_ID = gvPaperVenue.DataKeys[id]["fk_Inst_ID"].ToString();


                clsImportFromExcel oImportFromExcel = new clsImportFromExcel();
                string result = string.Empty;
                try
                {
                    int res = oImportFromExcel.RemoveUnpublishedPaperVenue( FacID,CrID,MoLrnID,PtrnID,BrnID,CrPrDetID,CrPrChID,examevent,PpPpHeadCrPrChID,fk_Center_ID,fk_Inst_ID);

                    if (res != 0)
                    {
                        lblMessgae.Visible = true;
                        lblMessgae.Text = "Removed Successfully.";
                        lblMessgae.CssClass = "saveNote";
                        DisplayGrid();
                    }
                    else
                    {
                        lblMessgae.Visible = true;
                        lblMessgae.Text = "Not removed please contact administrator.";
                        lblMessgae.CssClass = "errorNote";
                        DisplayGrid();
                    }

                }
                catch (Exception ex1)
                {
                    lblMessgae.Text = ex1.Message;
                    lblMessgae.CssClass = "errorNote";
                }
                if (oImportFromExcel != null) oImportFromExcel = null;

            }
        }

        #region gvPaperVenue_PageIndexChanging
        protected void gvPaperVenue_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPaperVenue.PageIndex = e.NewPageIndex;
            DisplayGrid();
        }

        #endregion

        
    }
}



 
         