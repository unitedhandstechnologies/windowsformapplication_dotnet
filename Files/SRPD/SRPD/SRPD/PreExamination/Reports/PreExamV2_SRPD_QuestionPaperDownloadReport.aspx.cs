using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PreExamClstLib.BusinessObjects;
using PreExamClstLib.Services;
using Classes;
using System.Web.UI.HtmlControls;
using System.Xml;
using System.Xml.Xsl;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.IO;
using RKLib.ExportData;
using System.Globalization;
using System.Reflection;
using TripleDESEncryption;
using System.Configuration;

namespace SRPD.PreExamination.Reports
{
    public partial class PreExamV2_SRPD_QuestionPaperDownloadReport : System.Web.UI.Page
    {
        #region Variables
        DataTable newDt = null;
        static DataTable dt = null;
        DataView dv = null;
        string str = null;
        string retString = null;
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

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

            dt = srv.ListDateTimeSlots();

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

        #region CreateTable
        protected DataTable CreateTable()
        {
            newDt = new DataTable("SlotTable");
            DataView dv = dt.DefaultView;
            newDt = dv.ToTable(true, "ExamDateTime", "ExamStartTime", "ExamDate", "ExamEndTime");

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
                //hidEventID.Value = gvPaperSlot.DataKeys[id]["pk_ExEv_ID"].ToString();
                hidExamDate.Value = gvPaperSlot.DataKeys[id]["ExamDate"].ToString();
                hidExamDateTime.Value = gvPaperSlot.DataKeys[id]["ExamDateTime"].ToString();
                hidExamStartTime.Value = gvPaperSlot.DataKeys[id]["ExamStartTime"].ToString();
                hidExamEndTime.Value = gvPaperSlot.DataKeys[id]["ExamEndTime"].ToString();

                SRVSecurePaper srvReports = new SRVSecurePaper();

                DataTable dt = null;
                try
                {

                    str = hidExamDate.Value;

                    dt = srvReports.QuestionPaperDownloadReport(str, hidExamStartTime.Value, hidExamEndTime.Value);

                    if (dt.Rows.Count > 0 && dt.Rows.Count != 0)
                    {
                        GenerateExcel("QuestionPaperDownloadReport_" + retString + "_" + hidExamStartTime.Value + "-" + hidExamEndTime.Value + ".xls", dt);
                    }
                    else
                    {
                        lblMesg.Text = "No Record Found";
                        lblMesg.CssClass = "errorNote";
                    }
                }
                catch (Exception ex)
                {
                    lblMesg.Text = ex.Message;
                    lblMesg.CssClass = "errorNote";
                }
                finally
                {
                    if (dt != null)
                        dt.Dispose();
                }

            }
        }
        #endregion

        #region Export To Excel

        public void GenerateExcel(string FileName, DataTable dt)
        {
            try
            {
                int count = dt.Columns.Count - 1;
                StringBuilder oSB = new StringBuilder();
                oSB.Append("<?xml version=\"1.0\"?>");
                oSB.Append("<ss:Workbook xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">");
                oSB.Append("<ss:Styles>");
                oSB.Append("<ss:Style ss:ID=\"1\">");
                oSB.Append("<ss:Font ss:Bold=\"1\"/>");
                oSB.Append("</ss:Style>");
                oSB.Append("<ss:Style ss:ID=\"2\">");
                oSB.Append("<ss:Alignment ss:Horizontal=\"Left\" ss:Vertical=\"Top\"/>");
                oSB.Append("<ss:Font ss:Bold=\"1\"/>");
                oSB.Append("</ss:Style>");
                oSB.Append("</ss:Styles>");
                oSB.Append("<ss:Worksheet ss:Name=\"" + "QuestionpaperReport.xls" + "\">");
                oSB.Append("<ss:Table>");

                //Main heading
                #region Main heading

                oSB.Append("<ss:Row ss:StyleID=\"2\">");
                oSB.Append("<ss:Cell ss:MergeAcross = \"" + count + "\" ><ss:Data ss:Type=\"String\">" + "Question paper Report" + "</ss:Data></ss:Cell>");
                oSB.Append("</ss:Row>");
                oSB.Append("<ss:Row ss:StyleID=\"2\">");
                oSB.Append("<ss:Cell ss:MergeAcross = \"" + count + "\" ><ss:Data ss:Type=\"String\">" + "Exam Date: " + retString + "</ss:Data></ss:Cell>");
                oSB.Append("</ss:Row>");
                oSB.Append("<ss:Row ss:StyleID=\"2\">");
                oSB.Append("<ss:Cell ss:MergeAcross = \"" + count + "\" ><ss:Data ss:Type=\"String\">" + "Exam Start Time: " + hidExamStartTime.Value + "</ss:Data></ss:Cell>");
                oSB.Append("</ss:Row>");
                oSB.Append("<ss:Row ss:StyleID=\"2\">");
                oSB.Append("<ss:Cell ss:MergeAcross = \"" + count + "\" ><ss:Data ss:Type=\"String\">" + "Exam End Time : " + hidExamEndTime.Value + "</ss:Data></ss:Cell>");
                oSB.Append("</ss:Row>");


                #endregion

                //Blank row
                #region Blank row

                oSB.Append("<ss:Row ss:StyleID=\"1\">");
                oSB.Append("<ss:Cell ss:MergeAcross = \"" + count + "\" ></ss:Cell>");
                oSB.Append("</ss:Row>");

                #endregion

                #region Column heading
                // Heading
                oSB.Append("<ss:Row ss:StyleID=\"1\">");
                for (int iCol = 0; iCol <= dt.Columns.Count - 1; iCol++)
                {
                    oSB.Append("<ss:Cell><ss:Data ss:Type=\"String\">" + dt.Columns[iCol].ColumnName + "</ss:Data></ss:Cell>");
                }
                oSB.Append("</ss:Row>");

                #endregion

                #region Student data

                for (int iRow = 0; iRow <= dt.Rows.Count - 1; iRow++)
                {
                    oSB.Append("<ss:Row>");
                    for (int iCol = 0; iCol <= dt.Columns.Count - 1; iCol++)
                    {
                        oSB.Append("<ss:Cell><ss:Data ss:Type=\"String\">" + dt.Rows[iRow][iCol].ToString() + "</ss:Data></ss:Cell>");

                    }
                    oSB.Append("</ss:Row>");
                }


                #endregion

                oSB.Append("</ss:Table>");
                oSB.Append("</ss:Worksheet>");
                oSB.Append("</ss:Workbook>");

                Response.Clear();
                Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
                Response.Charset = string.Empty;
                Response.ContentType = "application/vnd.xls";
                Response.Write(oSB.ToString());
                Response.End();


            }
            catch (Exception ex)
            {

            }
        }

        #endregion

        #endregion
    }
}