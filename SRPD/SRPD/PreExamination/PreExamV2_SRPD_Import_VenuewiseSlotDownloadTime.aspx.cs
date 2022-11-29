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
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using Classes;
using PreExamClstLib.Services;
using System.Text;
using RKLib.ExportData;
namespace SRPD.PreExamination
{
    public partial class PreExamV2_SRPD_Import_VenuewiseSlotDownloadTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //fill venue list
            }
        }

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

                        string message = oImportFromExcel.CreateTable(folderPath + "\\" + fileUploadExcel.FileName, "SRPD_VenueWiseDownloadTime");
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

                        //oImportFromExcel = null;
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
                lblFileError.Text = ex.Message;
            }
        } 
        #endregion        

        #region btnCancel_Click
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            SRVSeatManagement oImportFromExcel = new SRVSeatManagement();
            string result = string.Empty;
            try
            {
                // setVariables();
                result = oImportFromExcel.Cancelrecords("SRPD_VenueWiseDownloadTime");
                if (result.Equals("Successful"))
                {
                    lblMessage.Text = "Cancelled Successfully.";
                    lblMessage.CssClass = "saveNote";
                    btnConfirm.Enabled = false;
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
        #endregion

        #region btnConfirm_Click
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            clsImportFromExcel ObjImport = new clsImportFromExcel();
            string result = string.Empty;
            try
            {

                clsUser user = (clsUser)Session["user"];
                int res = ObjImport.ImportVenueWiseDownloadTime(user.User_ID);

                if (res != 0)
                {
                    lblMessage.Text = "Data Imported Successfully.";
                    lblMessage.CssClass = "saveNote";
                    btnConfirm.Enabled = false;
                    btnCancel.Enabled = false;
                    //ShowDiscrepancyStats();
                }
                else
                {
                    result = "Unable to Import Data.";
                    lblMessage.Text = result;
                    lblMessage.CssClass = "saveNote";
                }
            }
            catch (Exception ex1)
            {
                lblFileError.Text = ex1.Message;
                lblFileError.CssClass = "errorNote";
            }
            if (ObjImport != null) ObjImport = null;
        }
        #endregion

        #region lnkExistingConfig_Click
        protected void lnkExistingConfig_Click(object sender, EventArgs e)
        {
            clsImportFromExcel srv = new clsImportFromExcel();

            DataTable dt = new DataTable();

            dt = srv.GetVenueWiseDownloadTime(null);

            DataSet ds = new DataSet();
           
            if (dt != null && dt.Rows.Count > 0)
            {
                ds = dt.DataSet;              
                string filename = "VenueWiseDownloadTimeList.xls";
           
                GenerateExcel(filename, ds);

            }
            else
            {
                //lblExportMsg.Text = "Please check if time table is already defined or paper inclusion is not done.";
                //lblExportMsg.CssClass = "errorNote";
            }
        }
        #endregion

        #region oGvDetails_RowDataBound
        protected void oGvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text != "0" && (e.Row.Cells[0].Text == "No. of Venue does not Exists" || e.Row.Cells[0].Text == "No. of Venue Mapping does not Exists" || e.Row.Cells[0].Text == "No. of Duplicate VenueCode" || e.Row.Cells[0].Text == "No of Records where DownloadTime is greater than 120" || e.Row.Cells[0].Text == "No of Records where DownloadTime is less than 30"))
                {
                    e.Row.Cells[0].CssClass = "gridstyle";
                    e.Row.Cells[1].CssClass = "gridstyle";
                    e.Row.Cells[0].Font.Bold = true;
                    e.Row.Cells[1].Font.Bold = true;

                    btnConfirm.Enabled = false;
                    divGrvMsg.Style.Add("display", "block");

                }
                //else
                //    e.Row.Visible = false;
            }

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
            //SRVSeatManagement importFromExcel = new SRVSeatManagement();
            DataTable dt = new DataTable();
            SqlConnection DestCnn = null;//new SqlConnection(clsConnection.getConnectionString());
            string conString = string.Empty;
            conString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=Excel 12.0;";
            

            OleDbConnection connection = new OleDbConnection(conString);
            try
            {
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [sheet1$]", connection);
                OleDbDataAdapter ad = new OleDbDataAdapter(cmd);
                DataSet TableData = new DataSet();
                ad.Fill(TableData);

                if (TableData.Tables[0].Rows.Count > 0)
                {
                    if (TableData.Tables[0].Columns.Count != 2)
                    {
                        exit += "The number of columns in uploaded Excel should be equal to two.";
                    }
                    else if (TableData.Tables[0].Columns.Count == 2)
                    {
                        //checking if columns equal 2
                        string col1 = TableData.Tables[0].Columns[0].ColumnName.Trim();
                        string col2 = TableData.Tables[0].Columns[1].ColumnName.Trim();                        

                        //checking if first row is blank
                        if ((col1.Equals("F1") || col2.Equals("F2")))
                        {
                            exit += "The first row of uploaded Excel is blank.";
                        }

                        if (!col1.Equals("VenueCode", StringComparison.OrdinalIgnoreCase))
                        {
                            exit += "The uploaded Excel file has invalid 1st Column Header.";
                        }
                        if (!col2.Equals("DownloadTime", StringComparison.OrdinalIgnoreCase))
                        {
                            exit += "The uploaded Excel file has invalid 2nd Column Header.";
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

        #region Show Discrepancy Statistics

        void ShowDiscrepancyStats()
        {
            clsImportFromExcel oImportFromExcel = new clsImportFromExcel();
            DataSet oDGetImport = oImportFromExcel.GetVenueWiseDownloadTimeImportStat();
            DataTable odt = new DataTable();
            odt.Columns.Add("Section");
            odt.Columns.Add("NoOfRecords");
            int i = 0;
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
            }

            btnCancel.Enabled = true;
            oImportFromExcel = null;
        }

        #endregion

        #region GenerateExcel

        public void GenerateExcel(string strFileName, DataSet oDS)
        {
            StringBuilder oSB = new StringBuilder();
            int rowCount = 0, rowCounter = 0;
            oSB.AppendLine(
                "<?xml version=\"1.0\"?>"
                + "<ss:Workbook xmlns:ss=\"urn:schemas-microsoft-com:office:spreadsheet\">"
                + "<ss:Styles>"
                + "<ss:Style ss:ID=\"OtherHead\">"
                + "<ss:Font ss:Bold=\"1\" ss:Color=\"#000000\"/>"
                + "</ss:Style>"
                + "<ss:Style ss:ID=\"ColHead\">"
                + "<ss:Font ss:Bold=\"1\" ss:Color=\"#000000\"/>"
                + "<ss:Interior ss:Color=\"#d3cfd7\" ss:Pattern=\"Solid\"/>"
                + "<ss:Borders>"
                + "<ss:Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + "<ss:Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + "<ss:Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + "<ss:Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + "</ss:Borders>"
                + "</ss:Style>"
                + "<ss:Style ss:ID=\"ColData\">"
                + "<ss:Borders>"
                + "<ss:Border ss:Position=\"Left\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + "<ss:Border ss:Position=\"Top\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + "<ss:Border ss:Position=\"Right\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + "<ss:Border ss:Position=\"Bottom\" ss:LineStyle=\"Continuous\" ss:Weight=\"1\"/>"
                + "</ss:Borders>"
                + "</ss:Style>"
                + "</ss:Styles>"
                );

            for (int iTab = 0; iTab <= oDS.Tables.Count - 1; iTab++)
            {
                int[] colwidth = new int[oDS.Tables[iTab].Columns.Count];
                int largest = 0, temp = 0;

                for (int iCol = 0; iCol <= oDS.Tables[iTab].Columns.Count - 1; iCol++)
                {
                    largest = 0;
                    for (int iRow = 0; iRow <= oDS.Tables[iTab].Rows.Count - 1; iRow++)
                    {
                        if ((temp = oDS.Tables[iTab].Rows[iRow][iCol].ToString().Length) > largest)
                        {
                            colwidth[iCol] = temp;
                            largest = temp;
                        }
                        else
                            colwidth[iCol] = largest;
                    }
                }
                rowCount = oDS.Tables[iTab].Rows.Count;
                oSB.AppendLine(
                    "<ss:Worksheet ss:Name=\"" + "Sheet1" + "\">"
                    + "<ss:Table ss:LeftCell=\"1\" ss:TopCell=\"1\">"
                    );

                for (int iCol = 0; iCol <= oDS.Tables[iTab].Columns.Count - 1; iCol++)
                    oSB.AppendLine("<ss:Column ss:Width=\"" + ((colwidth[iCol] > oDS.Tables[iTab].Columns[iCol].ColumnName.Length) ? Math.Floor(colwidth[iCol] * 6.1).ToString() : Math.Floor(oDS.Tables[iTab].Columns[iCol].ColumnName.Length * 6.1).ToString()) + "\"/>");

                //oSB.AppendLine(
                //    "<ss:Row>"
                //    + "<ss:Cell ss:StyleID=\"OtherHead\"><ss:Data ss:Type=\"String\">Exam Event:</ss:Data></ss:Cell>"
                //    + "<ss:Cell><ss:Data ss:Type=\"String\">" + ddlExamevents.SelectedItem.Text.Trim() + "</ss:Data></ss:Cell>"
                //    + "</ss:Row>"

                //    + "<ss:Row></ss:Row>"       // Blank Row
                //    );
                int rowsperSheet = 50000;//ExcelSheet Count per sheet 
                int rowsplit = (rowCount / rowsperSheet) + 1;
                int i = 1;
                while (rowCounter < rowCount && i <= rowsplit)
                {
                    if (i > 1)
                        oSB.AppendLine(
                           "<ss:Worksheet ss:Name=\"" + "Sheet" + i.ToString() + "\">"
                           + "<ss:Table ss:LeftCell=\"1\" ss:TopCell=\"1\">"
                           );
                    oSB.AppendLine("<ss:Row>");
                    for (int iCol = 0; iCol <= oDS.Tables[iTab].Columns.Count - 1; iCol++)
                        oSB.AppendLine("<ss:Cell ss:StyleID=\"ColHead\"><ss:Data ss:Type=\"String\">" + oDS.Tables[iTab].Columns[iCol].ColumnName + "</ss:Data></ss:Cell>");
                    oSB.AppendLine("</ss:Row>");
                    if (i != rowsplit)
                    {
                        for (int iRow = rowCounter; iRow <= rowsperSheet * i; iRow++)
                        {
                            oSB.AppendLine("<ss:Row>");
                            for (int iCol = 0; iCol <= oDS.Tables[iTab].Columns.Count - 1; iCol++)
                                oSB.AppendLine("<ss:Cell ss:StyleID=\"ColData\"><ss:Data ss:Type=\"" + getexceltypeof(oDS.Tables[iTab].Rows[iRow][iCol]) + "\">" + oDS.Tables[iTab].Rows[iRow][iCol].ToString() + "</ss:Data></ss:Cell>");
                            oSB.AppendLine("</ss:Row>");
                        }
                    }
                    else
                    {
                        for (int iRow = rowCounter; iRow < rowCount; iRow++)
                        {
                            oSB.AppendLine("<ss:Row>");
                            for (int iCol = 0; iCol <= oDS.Tables[iTab].Columns.Count - 1; iCol++)
                                oSB.AppendLine("<ss:Cell ss:StyleID=\"ColData\"><ss:Data ss:Type=\"" + getexceltypeof(oDS.Tables[iTab].Rows[iRow][iCol]) + "\">" + oDS.Tables[iTab].Rows[iRow][iCol].ToString() + "</ss:Data></ss:Cell>");
                            oSB.AppendLine("</ss:Row>");
                        }
                    }
                    oSB.AppendLine("</ss:Table></ss:Worksheet>");
                    rowCounter = (rowsperSheet * i) + 1;
                    i++;
                }
                oSB.AppendLine("</ss:Workbook>");
                try
                {
                    Response.Clear();
                    Response.AddHeader("content-disposition", "attachment;filename=" + strFileName);
                    Response.Charset = string.Empty;
                    Response.ContentType = "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";                 
                    Response.Write(oSB.ToString());
                    Response.End();
                }
                catch (Exception ex)
                {
                    //  lblErrorMsg.Text = ex.Message;
                }
            }
        }

        #endregion

        #region getexceltypeof

        private string getexceltypeof(object obj)
        {
            if (obj.GetType().ToString().Contains("System.Int") || obj.GetType().ToString().Contains("System.Double"))
                return "Number";
            else if (obj.GetType().ToString().Contains("System.DateTime"))
                return "DateTime";
            else if (obj.GetType().ToString().Contains("System.Boolean"))
                return "Boolean";
            else
                return "String";
        }

        #endregion
    }
}