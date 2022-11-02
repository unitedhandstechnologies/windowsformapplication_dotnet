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
using System.IO;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Data.Sql;
using System.Data.SqlClient;


namespace SRPD.PreExamination
{
    public partial class PreExamV2_SecureQuestionPaperDownload__3 : System.Web.UI.Page
    {
        #region Variables
        DataTable newDt = null;
        static DataTable dt = null;
        DataView dv = null;
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                SetHiddenVariables();
                FillGrid();
            }
        }
        #endregion

        #region Other Functions

        #region FillGrid
        public void FillGrid()
        {
            dt = null;
            SRVSecurePaper srv = new SRVSecurePaper();

            dt = srv.ListPaperTlmAmAtForDownload(hidVenueID.Value);

            if (dt != null && dt.Rows.Count > 0)
            {


                dv = new DataView();
                dv.Table = CreateTable();



                if (dv.Table != null && dv.Table.Rows.Count > 0)
                {
                    try
                    {
                        gvPaperSlot.DataSource = dv;
                        gvPaperSlot.DataBind();
                        gvPaperSlot.Visible = true;
                        dt.Dispose();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            else
            {
                lblMsg.Text = "No paper slot is avialable";
                lblMsg.CssClass = "errorNote";
                trGrv.Style.Add("display", "none");
            }
        }
        #endregion

        #region SetHiddenVariables
        void SetHiddenVariables()
        {
            try
            {
                ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");

                hidVenueID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidVenueID")).Value;
                hidVenueName.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidVenueName")).Value;
                hidVenueCode.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidVenueCode")).Value;

                if (hidVenueID.Value != "-1")
                    lblSubHeader.Text = "for " + hidVenueCode.Value + " - " + hidVenueName.Value;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region CreateTable
        protected DataTable CreateTable()
        {
            newDt = new DataTable("SlotTable");
            DataView dv = dt.DefaultView;
            newDt = dv.ToTable(true, "ExamDateTime", "ExamStartTime", "ExamDate", "ExamEndTime", "ZipFilePwd", "pk_ExEv_ID");

            DataView dataView = new DataView(newDt);
            dataView.Sort = "ExamDateTime ASC";
            newDt = dataView.ToTable();
            return newDt;

        }
        #endregion

        #region gvPaperSlot_RowCommand
        protected void gvPaperSlot_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            SRVSecurePaper srv = new SRVSecurePaper();

            if (e.CommandName == "Select")
            {
                hidEventID.Value = gvPaperSlot.DataKeys[id]["pk_ExEv_ID"].ToString();
                hidExamDate.Value = gvPaperSlot.DataKeys[id]["ExamDate"].ToString();
                hidExamDateTime.Value = gvPaperSlot.DataKeys[id]["ExamDateTime"].ToString();
                hidExamStartTime.Value = gvPaperSlot.DataKeys[id]["ExamStartTime"].ToString();
                hidExamEndTime.Value = gvPaperSlot.DataKeys[id]["ExamEndTime"].ToString();
                hidZipFilePwd.Value = gvPaperSlot.DataKeys[id]["ZipFilePwd"].ToString();

                if (hidVenueID.Value != "-1")
                    Server.Transfer("PreExamV2_SecureQuestionPaperDownload__4.aspx");
                else
                    Server.Transfer("PreExamV2_SecureQuestionPaperDownload__5.aspx");
            }
        }
        #endregion

        #endregion
    }
}