using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Data.OleDb;
using Classes;
using System.IO;
using PreExamClstLib.Services;
using Microsoft.Reporting.WebForms;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SRPD_ImportPaperVenueFromExcel__1 : System.Web.UI.Page
    {

        #region Page_Load
        string PrmTblVisible = "False";
        string PrmTblDuplicatVenueCode = "False";
        string PrmTblInValidVenueCode = "False";
        // string PrmTblDuplicateSeatNumberVisible = "False";
        string PrmTblCenterCodeNotExistsVisible = "False";
        string PrmTblVenueCodeNotExistsVisible = "False";
        string PrmTblCenterInstituteNotPublishedVisible = "False";
        string PrmTblCenterNotMappedScheduleVisible = "False";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                setVariables();

            }
        }
        #endregion

        #region btnUploadProceed_Click
        protected void btnUploadProceed_Click(object sender, EventArgs e)
        {
            try
            {
                string folderPath = this.Server.MapPath(@"..\PreExamination\ImportFromExcelFile");

                if (fileUploadExcel.HasFile)
                {
                    //checking extension
                    if (!(fileUploadExcel.FileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase) || fileUploadExcel.FileName.EndsWith(".xlsx", StringComparison.OrdinalIgnoreCase)))
                    {
                        lblFileError.Text = "Invalid File Extension.";
                        return;
                    }
                    //code to upload file.
                    CreateFileInServer(folderPath);
                    //checking if data and columns of Excel are valid
                    string proceed = CheckExcelForValidData(folderPath + "\\" + fileUploadExcel.FileName); 
                    if (proceed.Equals(string.Empty))
                    {
                        clsImportFromExcel oImportFromExcel = new clsImportFromExcel();

                        string message = oImportFromExcel.CreateTable(folderPath + "\\" + fileUploadExcel.FileName, "TblImportPaperVenue"+"_"+hidEventID.Value);
                        if (message.Equals("0"))
                        {
                            divFileUplToHide.Disabled = true;
                            ShowDiscrepancyStats();
                        }
                        else
                        {
                            lblFileError.Text = message;
                            lblFileError.CssClass = "errorNote";
                        }

                        oImportFromExcel = null;
                    }
                    else
                    {
                        FileInfo fi = new FileInfo(folderPath + "\\" + fileUploadExcel.FileName);
                        if (fi.Exists)
                        {
                            fi.Delete();
                        }
                        lblFileError.Text = proceed;
                        return;
                    }
                }
                else
                {
                    lblFileError.Text = "Please select valid file";
                    return;
                }
            }
            catch (Exception ex)
            {
               // lblFileError.Text = ex.Message;
            }
        }
        #endregion 

        #region Show Discrepancy Statistics

        void ShowDiscrepancyStats()
        {
            clsImportFromExcel oImportFromExcel = new clsImportFromExcel();
            DataSet oDGetImport = oImportFromExcel.GetPaperVenueImportFromExcelDiscrepancyStatistics(hidEventID.Value);
            DataTable odt = new DataTable();
            odt.Columns.Add("Section");
            odt.Columns.Add("NoOfRecords");
            int i = 0;
            int tables = oDGetImport.Tables.Count;
            if (oDGetImport != null)
            {
                foreach (DataTable oDataTable in oDGetImport.Tables)
                {
                    if (oDataTable.Rows.Count > 0)
                    {
                        object[] rowData = oDataTable.Rows[0].ItemArray;
                        odt.Rows.Add(odt.NewRow());
                        odt.Rows[i].ItemArray = rowData;
                        i++;
                    }
                }
            }
            if (odt.Rows.Count > 0)
            {
                //odt.Rows.RemoveAt(odt.Rows.Count-1);
                oGvDetails.DataSource = odt;
                oGvDetails.DataBind();
                tblDiscrepancyStats.Visible = true;


                lblMessage.Text = "";


              


                if (Convert.ToInt32(odt.Rows[0]["NoOfRecords"].ToString()) > 0)
                {
                    btnConfirm.Enabled = true;
                }
                else
                {
                    btnConfirm.Enabled = false;
                    lblMessage.Text = "No data found for import";
                }


                for (int k = 1; k <= odt.Rows.Count; k++)
                {
                    if (Convert.ToInt32(odt.Rows[k]["NoOfRecords"].ToString()) > 0)
                    {
                        btnConfirm.Enabled = false;
                    }
                }

                //
                //if (hidGenerationAllocationSeq.Value == "1") //Seat Generation Before Venue Allocation
                //{
                //    if (hidImportOption.Value == "1") // Import Only Seat Numbers
                //    {
                //        if (Convert.ToInt32(odt.Rows[6]["NoOfRecords"].ToString()) > 0) //Check if venue is allocated or not
                //        {
                //            btnConfirm.Enabled = false;
                //            lblMessage.Text = "Data cannot be imported as Venue Allocation is already done for selected course part term.";
                //        }
                //    }

                //    if (hidImportOption.Value == "2") // Import Center-Venue Only
                //    {
                //        if (Convert.ToInt32(odt.Rows[8]["NoOfRecords"].ToString()) == 0) //Check if Seat Generation is done 
                //        {
                //            btnConfirm.Enabled = false;
                //            lblMessage.Text = "Data cannot be imported as Seat Generation is not done for selected course part term.";
                //        }
                //        //else if (Convert.ToInt32(odt.Rows[8]["NoOfRecords"].ToString()) > 0 && Convert.ToInt32(odt.Rows[9]["NoOfRecords"].ToString()) > 0)
                //        //{
                //        //    btnConfirm.Enabled = false;
                //        //    lblMessage.Text = "Data cannot be imported as Seat Generation and Venue allocation is done for selected course part term.";
                //        //}
                //    }
                //}
                //else if (hidGenerationAllocationSeq.Value == "0") //Seat Generation After Venue Allocation
                //{
                //    if (hidImportOption.Value == "1") // Import Only Seat 
                //    {
                //        if (Convert.ToInt32(odt.Rows[6]["NoOfRecords"].ToString()) == 0) //Check if Venue allocation is done or not
                //        {
                //            btnConfirm.Enabled = false;
                //            lblMessage.Text = "Data cannot be imported as Venue Allocation is not done for selected course part term.";
                //        }
                //        //else if (Convert.ToInt32(odt.Rows[6]["NoOfRecords"].ToString()) > 0 && Convert.ToInt32(odt.Rows[7]["NoOfRecords"].ToString()) > 0) //Check if seat/Venue allocation is done or not
                //        //{
                //        //    btnConfirm.Enabled = false;
                //        //    lblMessage.Text = "Data cannot be imported as Seat Generation and Venue allocation is done for selected course part term.";
                //        //}
                //    }

                //    if (hidImportOption.Value == "2") // Import Center-Venue Only
                //    {
                //        //if (Convert.ToInt32(odt.Rows[8]["NoOfRecords"].ToString()) > 0 && Convert.ToInt32(odt.Rows[9]["NoOfRecords"].ToString()) > 0)
                //        //{
                //        //    btnConfirm.Enabled = false;
                //        //    lblMessage.Text = "Data cannot be imported as Seat Generation and Venue allocation is done for selected course part term.";
                //        //}
                //        //else 
                //        if (Convert.ToInt32(odt.Rows[8]["NoOfRecords"].ToString()) > 0) //Check if Seat generation is done
                //        {
                //            btnConfirm.Enabled = false;
                //            lblMessage.Text = "Data cannot be imported as Seat Generation is done for selected course part term.";
                //        }
                //    }
                //}

            }

            btnCancel.Enabled = true;
            oImportFromExcel = null;
        }

        #endregion

        #region CreateFileInServer
        private void CreateFileInServer(string folderPath)
        {
            try
            {
                fileUploadExcel.SaveAs(folderPath + @"\" + fileUploadExcel.FileName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Check Excel For Valid Data and Columns

        private string CheckExcelForValidData(string FileName)
        {
            string exit = string.Empty;
            clsImportFromExcel importFromExcel = new clsImportFromExcel();
            DataTable dt = new DataTable();
            SqlConnection DestCnn = null;
            string conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=Excel 12.0;";

            OleDbConnection connection = new OleDbConnection(conString);
            try
            {
                OleDbCommand cmd = new OleDbCommand("SELECT DISTINCT * FROM [sheet1$]", connection);
                OleDbDataAdapter ad = new OleDbDataAdapter(cmd);
                DataSet TableData = new DataSet();
                ad.Fill(TableData);

                if (TableData.Tables[0].Rows.Count > 0)
                {
                    if (TableData.Tables[0].Columns.Count != 5)
                    {
                        exit += "The number of columns in uploaded Excel should be equal to Five.";
                    }
                    else if (TableData.Tables[0].Columns.Count == 5)
                    {
                        //checking if columns equal 5
                        string col1 = TableData.Tables[0].Columns[0].ColumnName.Trim();
                        string col2 = TableData.Tables[0].Columns[1].ColumnName.Trim();
                        string col3 = TableData.Tables[0].Columns[2].ColumnName.Trim();
                        string col4 = TableData.Tables[0].Columns[3].ColumnName.Trim();
                        string col5 = TableData.Tables[0].Columns[4].ColumnName.Trim();

                        //checking if first row is blank
                        if ((col1.Equals("F1") || col2.Equals("F2")))
                            {
                                exit += "The first row of uploaded Excel is blank.";
                            } 
                        if (!col1.Equals("Paper_Code", StringComparison.OrdinalIgnoreCase))
                            {
                                exit += "The uploaded Excel file has invalid 1st Column Header.";
                            }
                        if (!col2.Equals("Center_Code", StringComparison.OrdinalIgnoreCase))
                            {
                                exit += "The uploaded Excel file has invalid 2nd Column Header.";
                            }
                        if (!col3.Equals("Venue_Code", StringComparison.OrdinalIgnoreCase))
                            {
                                exit += "The uploaded Excel file has invalid 3rd Column Header.";
                            }
                        if (!col4.Equals("Exam_Code", StringComparison.OrdinalIgnoreCase))
                            {
                                exit += "The uploaded Excel file has invalid 4th Column Header.";
                            }
                        if (!col5.Equals("Student_Count", StringComparison.OrdinalIgnoreCase))
                            {
                                exit += "The uploaded Excel file has invalid 5th Column Header.";
                            } 
                    }
                }

            }
            catch (Exception ex)
            {
                exit += ex.Message;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
                if (DestCnn != null)
                    DestCnn.Close();
            }
            return exit;
        }

        #endregion

        #region setVariables

        void setVariables()
        {
            ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");
            hidEventID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidEventID")).Value;
        }

        #endregion  

        #region Excel to dataset
        public string CreateTable(string FileName, string TableName)
        {
            //System.Data.DataTable dt = null;
            DataSet TableData = new DataSet();

            DBObjectPool Pool = null;
            DBObject oDB = null;

            Pool = DBObjectPool.Instance;
            oDB = Pool.AcquireDBObject();
            oDB.ThisConnectionFor = DBConnection.ADECWrite;
            string conn = oDB.GetConnectionString();

            SqlConnection DestCnn = new SqlConnection(conn);
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;IMEX=1\";");
            //OleDbConnection connection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR= Yes;\"");
            try
            {
                //OleDbCommand command = new OleDbCommand("Select * FROM [Sheet1$]", connection);
                //connection.Open();

                OleDbDataAdapter oledba = new OleDbDataAdapter("SELECT DISTINCT * FROM [sheet1$]", connection);
                oledba.Fill(TableData);
                System.Data.DataTable tblSchema = TableData.Tables[0].CreateDataReader().GetSchemaTable();
                //string sPath = @"E:\\ADES MKCL\\ALL NEW Developments REQ\\121837_ADE_ImportFromExcel_CoursePartTermWise\\";

                //string sPath = @"D:\\websites\\mum.digitaluniversity.ac\\WindowsServices\\Server\\ErrorLog\\";
                //string sDateTime = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                //System.IO.StreamWriter sw = System.IO.File.AppendText(sPath + "ADESDataInExcelErrorLog.log");
                ////sw.WriteLine(errorLogID + " : " +sDateTime + " ==> " + ex.Message + "---" + ex.Source);
                //sw.WriteLine(sDateTime + " -- " + "-- " + TableData.Tables[0].Rows.Count);
                //sw.Flush();
                //sw.Close();
                //sw = null;


                if (tblSchema.Rows.Count != 0)
                {
                    StringBuilder QCreate = new StringBuilder();

                    //QCreate.Append(" CREATE TABLE dbo.[" + TableName + "](");
                    QCreate.Append(" IF  EXISTS (SELECT * FROM sys.objects");
                    QCreate.Append(" WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]')");
                    QCreate.Append(" AND type in (N'U'))");
                    QCreate.Append(" DROP TABLE [dbo].[" + TableName + "] ");
                    QCreate.Append(" CREATE TABLE dbo.[" + TableName + "](");

                    foreach (DataRow dr in tblSchema.Rows)
                    {
                        switch (Convert.ToString(dr["DataType"]))
                        {
                            //case "System.String":
                            //    QCreate.Append("[" + dr["ColumnName"] + "] varchar(" + dr["ColumnSize"] + "), ");
                            //    break;
                            //case "System.Int32":
                            //    QCreate.Append("[" + dr["ColumnName"] + "] bigint, ");
                            //    break;
                            //case "System.Double":
                            //    QCreate.Append("[" + dr["ColumnName"] + "] decimal, ");
                            //    break;
                            //case "System.DateTime":
                            //    QCreate.Append("[" + dr["ColumnName"] + "] datetime, ");
                            //    break;
                            default:
                                QCreate.Append("[" + dr["ColumnName"].ToString().Trim() + "] varchar(255), ");
                                break;
                        }
                    }

                    string Query = Convert.ToString(QCreate);
                    Query = Query.Remove(Query.LastIndexOf(','));
                    Query = Query + ")";

                    SqlCommand comd = new SqlCommand(Query, DestCnn);
                    //comd.CommandTimeout = 300;
                    if (DestCnn.State == ConnectionState.Closed)
                    {
                        DestCnn.Open();
                    }
                    comd.ExecuteNonQuery();



                    TableData.Clear();
                    oledba.Fill(TableData);
                    SqlBulkCopy sqlcpy = new SqlBulkCopy(DestCnn);
                    sqlcpy.BulkCopyTimeout = 3000;
                    sqlcpy.DestinationTableName = "[" + TableName + "]";
                    sqlcpy.WriteToServer(TableData.Tables[0]);
                    string Result = TableName;

                    sqlcpy.Close();
                    comd.Dispose();

                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
                if (DestCnn != null)
                {
                    DestCnn.Close();
                    DestCnn.Dispose();
                }

                if (TableData != null)
                    TableData.Dispose();

                FileInfo fi = new FileInfo(FileName);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }

            return "0";
        }
        #endregion

        #region btnConfirm_Click
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            btnConfirmAndPublish.Enabled = false;

            clsImportFromExcel oImportFromExcel = new clsImportFromExcel(); 
            string result = string.Empty;
            try
            {
                int res = oImportFromExcel.ImportPaperVenueFromExcel(hidEventID.Value);

                if (res != 0)
                {
                    lblMessage.Text = "Data Imported Successfully.";
                    lblMessage.CssClass = "saveNote";
                }
                else
                {
                    //btnGetDetails.Enabled = true;
                }

            }
            catch (Exception ex1)
            {
                lblFileError.Text = ex1.Message;
                lblFileError.CssClass = "errorNote";
            }
            if (oImportFromExcel != null) oImportFromExcel = null;
        }
        #endregion

        protected void btnGetDetails_Click(object sender, EventArgs e)
        {
            clsImportFromExcel oImportFromExcel = new clsImportFromExcel();
            DataSet ods = oImportFromExcel.GetImportFromExcelDiscrepancyStatisticsReports_PaperVenue(hidEventID.Value);

            if (ods != null)
            {
                rvGetDiscrepancyStats.LocalReport.EnableExternalImages = true;
                rvGetDiscrepancyStats.ProcessingMode = ProcessingMode.Local;
                rvGetDiscrepancyStats.LocalReport.ReportPath = Server.MapPath(@"~\PreExamination\Reports\Rdlc\paperVenue.rdlc");
                //rvGetDiscrepancyStats.LocalReport.ReportPath ="D:\\_Development\\DigitalUniversity\\PreExamV3Reports\\PreExamination\\Reports\\Rdlc\\Paper_Venue.rdlc";
                rvGetDiscrepancyStats.LocalReport.DataSources.Clear();
                if (ods.Tables[0] != null)
                {
                    if (ods.Tables[1].Rows.Count > 0)
                    {
                        PrmTblDuplicatVenueCode = "False";
                        ReportDataSource datasource1 = new ReportDataSource("DataSet1", ods.Tables[0]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource1);
                    }
                    else
                    {
                        PrmTblDuplicatVenueCode = "True";
                        ReportDataSource datasource1 = new ReportDataSource("DataSet1", ods.Tables[0]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource1);
                    }
                }
                if (ods.Tables[1] != null)
                {
                    if (ods.Tables[1].Rows.Count > 0)
                    {
                        PrmTblInValidVenueCode = "False";
                        ReportDataSource datasource2 = new ReportDataSource("DataSet2", ods.Tables[1]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource2);
                    }
                    else
                    {
                        PrmTblInValidVenueCode = "True";
                        ReportDataSource datasource2 = new ReportDataSource("DataSet2", ods.Tables[1]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource2);
                    }

                }
                if (ods.Tables[2] != null)
                {
                    if (ods.Tables[2].Rows.Count > 0)
                    {
                        PrmTblCenterCodeNotExistsVisible = "False";
                        ReportDataSource datasource3 = new ReportDataSource("DataSet3", ods.Tables[2]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource3);
                    }
                    else
                    {
                        PrmTblCenterCodeNotExistsVisible = "True";
                        ReportDataSource datasource3 = new ReportDataSource("DataSet3", ods.Tables[2]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource3);
                    }
                }


                if (ods.Tables[3] != null)
                {
                    if (ods.Tables[3].Rows.Count > 0)
                    {
                        PrmTblVenueCodeNotExistsVisible = "False";
                        ReportDataSource datasource4 = new ReportDataSource("DataSet4", ods.Tables[3]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource4);
                    }
                    else
                    {
                        PrmTblVenueCodeNotExistsVisible = "True";
                        ReportDataSource datasource4 = new ReportDataSource("DataSet4", ods.Tables[3]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource4);
                    }
                }

                if (ods.Tables[4] != null)
                {
                    if (ods.Tables[4].Rows.Count > 0)
                    {
                        PrmTblCenterInstituteNotPublishedVisible = "False";
                        ReportDataSource datasource5 = new ReportDataSource("DataSet5", ods.Tables[4]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource5);
                    }
                    else
                    {
                        PrmTblCenterInstituteNotPublishedVisible = "True";
                        ReportDataSource datasource5 = new ReportDataSource("DataSet5", ods.Tables[4]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource5);
                    }
                }

                if (ods.Tables[5] != null)
                {
                    if (ods.Tables[5].Rows.Count > 0)
                    {
                        PrmTblCenterNotMappedScheduleVisible = "False";
                        ReportDataSource datasource6 = new ReportDataSource("DataSet6", ods.Tables[5]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource6);
                    }
                    else
                    {
                        PrmTblCenterNotMappedScheduleVisible = "True";
                        ReportDataSource datasource6 = new ReportDataSource("DataSet6", ods.Tables[5]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource6);
                    }
                }

                if (ods.Tables[6] != null)
                {
                    if (ods.Tables[5].Rows.Count > 0)
                    {
                        PrmTblCenterNotMappedScheduleVisible = "False";
                        ReportDataSource datasource6 = new ReportDataSource("DataSet7", ods.Tables[6]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource6);
                    }
                    else
                    {
                        PrmTblCenterNotMappedScheduleVisible = "True";
                        ReportDataSource datasource6 = new ReportDataSource("DataSet7", ods.Tables[6]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource6);
                    }
                }


                if (ods.Tables[7] != null)
                {
                    if (ods.Tables[5].Rows.Count > 0)
                    {
                        PrmTblCenterNotMappedScheduleVisible = "False";
                        ReportDataSource datasource6 = new ReportDataSource("DataSet8", ods.Tables[7]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource6);
                    }
                    else
                    {
                        PrmTblCenterNotMappedScheduleVisible = "True";
                        ReportDataSource datasource6 = new ReportDataSource("DataSet8", ods.Tables[7]);
                        rvGetDiscrepancyStats.LocalReport.DataSources.Add(datasource6);
                    }
                }

                //******************Region for parameters ********************************
                ReportParameter[] param = new ReportParameter[5];
                param[0] = new ReportParameter("UniName", clsGetSettings.UniversityName.ToString(), true);
                param[1] = new ReportParameter("UniLogo", clsGetSettings.SitePath + @"\Images\" + clsGetSettings.UniversityLogo, true);
                param[2] = new ReportParameter("UniAddress", clsGetSettings.Address, true);
                param[3] = new ReportParameter("UniURL", clsGetSettings.SitePath.ToLower(), true);

                string oUserLoginDetails = ((clsUser)System.Web.HttpContext.Current.Session["user"]).User_Name;
                string rptFooter = "Report generated by " + oUserLoginDetails + " on " + System.DateTime.Today.ToLongDateString() + ", " + System.DateTime.Now.ToLongTimeString();
                param[4] = new ReportParameter("ReportFooter", rptFooter, true);
                //string CourseName = string.Empty;
                //if (CourseSelectionCtrl.SelectedCoursePartDetailsName == CourseSelectionCtrl.SelectedCoursePartChildName)
                //{
                //    CourseName = "[" + CourseSelectionCtrl.SelectedEventName + "] - " + CourseSelectionCtrl.SelectedCourseName + " - " + CourseSelectionCtrl.SelectedCoursePartDetailsName;
                //    hidPageDescription.Value = lblSubHeader.Text.Trim();
                //}
                //else
                //{
                //    CourseName = " [" + CourseSelectionCtrl.SelectedEventName + "] - " + CourseSelectionCtrl.SelectedCourseName + " - " + CourseSelectionCtrl.SelectedCoursePartDetailsName + " - " + CourseSelectionCtrl.SelectedCoursePartChildName;
                //    hidPageDescription.Value = lblSubHeader.Text.Trim();
                //}
                //param[5] = new ReportParameter("CourseName", CourseName, true);
                rvGetDiscrepancyStats.LocalReport.SetParameters(param);
                rvGetDiscrepancyStats.LocalReport.EnableExternalImages = true;
                rvGetDiscrepancyStats.LocalReport.Refresh();
                RenderReport(rvGetDiscrepancyStats, Response);
            }
            else
            {
                rvGetDiscrepancyStats.Visible = false;
            }
        }


        private void RenderReport(ReportViewer rvGetDiscrepancyStats, HttpResponse response)
        {
            try
            {
                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;
                byte[] bytes = rvGetDiscrepancyStats.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
                response.ContentType = "application/mime";
                response.AddHeader("Content-Disposition", "attachment; filename= ImportFromExcelDiscrepancyStatictics.pdf;");
                response.BinaryWrite(bytes);
                response.End();
            }
            catch (Exception ex)
            {
                string msg = ex.Message;

            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clsImportFromExcel oImportFromExcel = new clsImportFromExcel();
            string result = string.Empty;
            try
            {
                // setVariables();
                result = oImportFromExcel.Cancelrecords("TblImportPaperVenue" + "_" + hidEventID.Value);
                if (result.Equals("Successful"))
                {
                    lblMessage.Text = "Cancelled Successfully.";
                    lblMessage.CssClass = "saveNote";
                    btnConfirm.Enabled = false;
                    btnGetDetails.Enabled = false;
                    btnCancel.Enabled = false;
                }
                else
                {
                    lblFileError.Text = result;
                    lblFileError.CssClass = "saveNote";
                }
            }
            catch (Exception ex2)
            {
                lblMessage.Text = ex2.Message;
                lblMessage.CssClass = "errorNote";
            }
            if (oImportFromExcel != null) oImportFromExcel = null;
        }

        protected void btnConfirmAndPublish_Click(object sender, EventArgs e)
        {
            btnConfirm.Enabled = false;
            clsImportFromExcel oImportFromExcel = new clsImportFromExcel();
            string result = string.Empty;
            try
            {
                int res = oImportFromExcel.ConfirmAndPublishPaperVenue(hidEventID.Value);

                if (res != 0)
                {
                    lblMessage.Text = "Data Confirmed and Published Successfully.";
                    lblMessage.CssClass = "saveNote";
                }
                else
                {
                    lblMessage.Text = "Data not Confirmed and Published please contact admistrator.";
                    lblMessage.CssClass = "saveNote";
                }

            }
            catch (Exception ex1)
            {
                lblFileError.Text = ex1.Message;
                lblFileError.CssClass = "errorNote";
            }
            if (oImportFromExcel != null) oImportFromExcel = null;

        }

    }
}