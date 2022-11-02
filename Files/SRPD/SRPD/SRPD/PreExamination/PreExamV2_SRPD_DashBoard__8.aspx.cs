using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Data;
using RKLib.ExportData;
using System.Web.UI.HtmlControls;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SRPD_DashBoard__8 : System.Web.UI.Page
    {


        clsReportsDashboard oDash = null;
        DataTable dt = new DataTable();
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetHiddenField();
                FillGrid();
            }
        }
        #endregion       

        #region FillGrid

        public void FillGrid()
        {

            oDash = new clsReportsDashboard();

            lblDate.Text = "Slot " + hidDateTime.Value + " Time " + hidStartTime.Value + " To " + hidEndTime.Value;
            try
            {
                dt = oDash.List_SRPD_VenueWiseDashBoardPendingPaperList(hidUniId.Value, hidExEvID.Value, hidInstID.Value, hidDateTime.Value, hidStartTime.Value, hidEndTime.Value);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvDownloadPendingPaperCount.DataSource = dt;
                    gvDownloadPendingPaperCount.DataBind();
                }
                else
                {
                    trNote.Visible = true;
                    lblErrorMsg.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                oDash = null; dt = null;
            }

        }

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
        #endregion

        #region Events

        #region gvDownloadPendingPaperCount_PageIndexChanging

        protected void gvDownloadPendingPaperCount_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            FillGrid();
            gvDownloadPendingPaperCount.PageIndex = e.NewPageIndex;
            gvDownloadPendingPaperCount.DataBind();


        }

        #endregion

        #region gvDownloadPendingPaperCount_RowDataBound

        protected void gvDownloadPendingPaperCount_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gv = (GridView)sender;
        }

        #endregion

        #region btnExport_Click

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string filename = "";
            filename = "Venuewise Paper Download Pending ";// clsGetSettings.UniversityName.ToString();

            try
            {
                oDash = new clsReportsDashboard();

                dt = oDash.List_SRPD_VenueWiseDashBoardPendingPaperList(hidUniId.Value, hidExEvID.Value, hidInstID.Value, hidDateTime.Value, hidStartTime.Value, hidEndTime.Value);
                if (dt != null && dt.Rows.Count > 0)
                {
                    RKLib.ExportData.Export objExport = new RKLib.ExportData.Export();
                    objExport.ExportDetails(dt, Export.ExportFormat.Excel, filename + ".xls");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }
        #endregion

        #region btnBack_Click

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Server.Transfer("PreExamV2_SRPD_DashBoard__3.aspx", true);

        }

        #endregion
        #endregion
    }
}