using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Data.SqlClient;
using Classes;
using System.Data.OleDb;
using System.Text;
using System.IO;

namespace Classes
{
    public class clsImportFromExcel
    {
        #region ImportPaperVenueFromExcel

        public int ImportPaperVenueFromExcel(string examEventID)
        {
            string result;
            DBObjectPool Pool = null;
            DBObject oDB = null;
            Hashtable objHT = new Hashtable();
            string UniID = clsGetSettings.UniversityID.ToString();
            int rowCount = 0;
            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();

                objHT.Add("UniID", UniID);
                objHT.Add("ExamEventID", examEventID);
                objHT.Add("CreatedBy", ((clsUser)System.Web.HttpContext.Current.Session["user"]).User_Name);


                SqlCommand cmd = oDB.GenerateCommand("PreExamv2_SRPD_PaperVenue_ImportFromExcel", objHT);
                rowCount = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                result = ex.Message;
                return 0;
            }
            finally
            {
                Pool.ReleaseDBObject(oDB);
            }
            return rowCount;
        }

        #endregion

        #region ConfirmAndPublishPaperVenue

        public int ConfirmAndPublishPaperVenue(string examEventID)
        {
            string result;
            DBObjectPool Pool = null;
            DBObject oDB = null;
            Hashtable objHT = new Hashtable();
            string UniID = clsGetSettings.UniversityID.ToString();
            int rowCount = 0;
            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();

                objHT.Add("UniID", UniID);
                objHT.Add("ExamEventID", examEventID);
                objHT.Add("CreatedBy", ((clsUser)System.Web.HttpContext.Current.Session["user"]).User_Name);


                SqlCommand cmd = oDB.GenerateCommand("PreExamv2_UpdateSRPD_PaperVenue_ConfirmAndPublish", objHT);
                rowCount = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                result = ex.Message;
                return 0;
            }
            finally
            {
                Pool.ReleaseDBObject(oDB);
            }
            return rowCount;
        }

        #endregion

        #region GetPaperVenueImportFromExcelDiscrepancyStatistics AkhileshG 8/5/2020
        public DataSet GetPaperVenueImportFromExcelDiscrepancyStatistics(string examEventID)
        {
            DBObject oDb = null;
            DBObjectPool pool = null;
            DataSet ds = null;
            Hashtable objHT = new Hashtable();
            string UniID = clsGetSettings.UniversityID.ToString();

            objHT.Add("UniID", UniID);
            objHT.Add("ExamEventID", examEventID);

            try
            {
                pool = DBObjectPool.Instance;
                oDb = pool.AcquireDBObject();
                ds = oDb.getparamdataset("PreExamV2_PaperVenue_ImportFromExcel_GetDiscrepancyStatistics", objHT);
            }
            catch (Exception ex)
            {
                ds = null;
                throw (ex);
            }
            finally
            {
                if (pool != null)
                {
                    pool.ReleaseDBObject(oDb);
                }
            }
            if (ds != null && ds.Tables.Count > 0)
                return ds;
            else
                return null;
        }
        #endregion

        #region GetImportFromExcelDiscrepancyStatistics Report AkhileshG 8/5/2020
        public DataSet GetImportFromExcelDiscrepancyStatisticsReports_PaperVenue(string examEventID)
        {
            DBObject oDb = null;
            DBObjectPool pool = null;
            DataSet ds = null;
            Hashtable objHT = new Hashtable();
            string UniID = clsGetSettings.UniversityID.ToString();

            objHT.Add("UniID", UniID);
            objHT.Add("ExamEventID", examEventID);


            try
            {
                pool = DBObjectPool.Instance;
                oDb = pool.AcquireDBObject();
                ds = oDb.getparamdataset("PreExamV2_PaperVenue_ImportFromExcel_GetDiscrepancyStat_Report", objHT);

            }
            catch (Exception ex)
            {
                ds = null;
                throw (ex);
            }
            finally
            {
                if (pool != null)
                {
                    pool.ReleaseDBObject(oDb);
                }
            }
            if (ds != null && ds.Tables.Count > 0)
                return ds;
            else
                return null;
        }
        #endregion

        #region ListPaperVenueForPublishCoursePartTermWise
        public DataSet ListPaperVenueForPublishCoursePartTermWise(string FacID, string CrID, string MolrnID, string PtrnID, string BrnID, string CrPrDetailsID, string CrPrChID, string ExEvID)
        {

            DBObject oDb = null;
            DBObjectPool pool = null;
            Hashtable ht = new Hashtable();
            string UniID = clsGetSettings.UniversityID.Trim();
            DataSet ds;
            ht.Add("UniID", UniID);
            ht.Add("FacID", FacID);
            ht.Add("CrID", CrID);
            ht.Add("MoLrnID", MolrnID);
            ht.Add("PtrnID", PtrnID);
            ht.Add("BrnID", BrnID);
            ht.Add("CrPrDetailsID", CrPrDetailsID);
            ht.Add("CrPrChID", CrPrChID);
            ht.Add("ExamEventID", ExEvID);


            try
            {
                pool = DBObjectPool.Instance;
                oDb = pool.AcquireDBObject();
                ds = oDb.getparamdataset("PreExamv2_ListSRPD_PaperVenue_ToBePublish", ht);
            }
            catch (Exception ex)
            {
                ds = null;
                throw ex;
            }
            finally
            {
                pool.ReleaseDBObject(oDb);
            }
            return ds;


        }
        #endregion

        #region UpdatePaperVenueToPublish
        public int UpdatePaperVenueToPublish(string FacID, string CrID, string MolrnID, string PtrnID, string BrnID, string CrPrDetailsID, string CrPrChID, string ExEvID)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            int status = 0;
            string UniID = clsGetSettings.UniversityID.Trim();

            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();

                Hashtable hs = new Hashtable();

                hs.Add("UniID", UniID);
                hs.Add("FacID", FacID);
                hs.Add("CrID", CrID);
                hs.Add("MoLrnID", MolrnID);
                hs.Add("PtrnID", PtrnID);
                hs.Add("BrnID", BrnID);
                hs.Add("CrPrDetailsID", CrPrDetailsID);
                hs.Add("CrPrChID", CrPrChID);
                hs.Add("ExamEventID", ExEvID);
                hs.Add("CreatedBy", ((clsUser)System.Web.HttpContext.Current.Session["user"]).User_Name);
                //hs.Add("Status", status); 
                SqlCommand cmd = oDB.GenerateCommand("PreExamv2_UpdateSRPD_PaperVenue_ToPublish", hs);

                status = cmd.ExecuteNonQuery();
                //status = Convert.ToInt16(cmd.Parameters["@Status"].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Pool != null)
                    Pool.ReleaseDBObject(oDB);
            }
            return status;
        }
        #endregion

        #region RemoveUnpublishedPaperVenue
        public int RemoveUnpublishedPaperVenue(string FacID, string CrID, string MolrnID, string PtrnID, string BrnID, string CrPrDetailsID, string CrPrChID, string ExEvID, string Pp_PpHeadID, string CenterID, string InstID)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            int status = 0;
            string UniID = clsGetSettings.UniversityID.Trim();

            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();

                Hashtable hs = new Hashtable();

                hs.Add("UniID", UniID);
                hs.Add("FacID", FacID);
                hs.Add("CrID", CrID);
                hs.Add("MoLrnID", MolrnID);
                hs.Add("PtrnID", PtrnID);
                hs.Add("BrnID", BrnID);
                hs.Add("CrPrDetailsID", CrPrDetailsID);
                hs.Add("CrPrChID", CrPrChID);
                hs.Add("ExamEventID", ExEvID);
                hs.Add("PpPpHeadCrPrChID", Pp_PpHeadID);
                hs.Add("CenterID", CenterID);
                hs.Add("InstID", InstID);
                hs.Add("CreatedBy", ((clsUser)System.Web.HttpContext.Current.Session["user"]).User_Name);
                SqlCommand cmd = oDB.GenerateCommand("PreExamV2_SRPD_RemovePaperVenue_InsertIntoLog", hs);



                status = cmd.ExecuteNonQuery();
                //status = Convert.ToInt16(cmd.Parameters["@Status"].Value);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (Pool != null)
                    Pool.ReleaseDBObject(oDB);
            }
            return status;
        }
        #endregion

        #region Create table to fetch data from excel sheet in database
        public string CreateTable(string FileName, string TableName)
        {
            DataSet TableData = new DataSet();

            DBObjectPool Pool = null;
            DBObject oDB = null;

            Pool = DBObjectPool.Instance;
            oDB = Pool.AcquireDBObject();

            string conn = clsGetSettings.ConnectionString;

            SqlConnection DestCnn = new SqlConnection(conn);
            OleDbConnection connection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=Excel 12.0;");

            try
            {
                OleDbDataAdapter oledba = new OleDbDataAdapter("SELECT * FROM [sheet1$]", connection);
                oledba.Fill(TableData);
                System.Data.DataTable tblSchema = TableData.Tables[0].CreateDataReader().GetSchemaTable();
                if (tblSchema.Rows.Count != 0)
                {
                    StringBuilder QCreate = new StringBuilder();

                    QCreate.Append(" IF  EXISTS (SELECT * FROM sys.objects");
                    QCreate.Append(" WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]')");
                    QCreate.Append(" AND type in (N'U'))");
                    QCreate.Append(" DROP TABLE [dbo].[" + TableName + "] ");
                    QCreate.Append(" CREATE TABLE dbo.[" + TableName + "](");
                    foreach (DataRow dr in tblSchema.Rows)
                    {
                        switch (Convert.ToString(dr["DataType"]))
                        {
                            default:
                                QCreate.Append("[" + dr["ColumnName"].ToString().Trim() + "] varchar(255), ");
                                break;
                        }
                    }

                    string Query = Convert.ToString(QCreate);
                    Query = Query.Remove(Query.LastIndexOf(','));
                    Query = Query + ")";

                    SqlCommand comd = new SqlCommand(Query, DestCnn);
                    if (DestCnn.State == ConnectionState.Closed)
                    {
                        DestCnn.Open();
                    }
                    comd.ExecuteNonQuery();
                    TableData.Clear();
                    oledba.Fill(TableData);
                    SqlBulkCopy sqlcpy = new SqlBulkCopy(DestCnn);
                    sqlcpy.DestinationTableName = "[" + TableName + "]";
                    sqlcpy.WriteToServer(TableData.Tables[0]);
                    string Result = TableName;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
                if (DestCnn != null)
                    DestCnn.Close();
                FileInfo fi = new FileInfo(FileName);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }

            return "0";
        }
        #endregion

        #region GetVenueWiseDownloadTime
        /// <summary>
        /// Get Venue wise download time 
        /// </summary>
        /// <param name="instID"></param> optional
        /// <returns></returns>
        public DataTable GetVenueWiseDownloadTime(string instID)
        {

            Hashtable objHT = new Hashtable();
            DBObjectPool pool = null;
            DBObject dbObject = null;
            DataTable dt = null;

            try
            {

                objHT.Add("InstID", instID);

                pool = DBObjectPool.Instance;
                dbObject = pool.AcquireDBObject();

                dt = dbObject.getparamdataset("PreExamV2_SRPD_GetVenueWiseDownloadtime", objHT).Tables[0];

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (pool != null)
                    pool.ReleaseDBObject(dbObject);
            }

            if (dt.Rows.Count != 0)
                return dt;
            else
                return null;

        }
        #endregion

        #region ImportVenueWiseDownloadTime
        /// <summary>
        /// Import venue wise download time
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public int ImportVenueWiseDownloadTime(string userID)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            SqlCommand cmd;
            int Status = 0;
            Hashtable objHT = new Hashtable();
            objHT.Add("user", userID);

            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();
                cmd = oDB.GenerateCommand("PreExamV2_SRPD_Import_VenueWiseDownloadtime", objHT);
                cmd.CommandType = CommandType.StoredProcedure;

                Status = cmd.ExecuteNonQuery();

                return Status;
            }
            catch (Exception ex)
            {
                Status = 0;
                //throw ex;
            }
            finally
            {
                Pool.ReleaseDBObject(oDB);
            }
            return Status;
        }
        #endregion

        #region GetVenueWiseDownloadTimeImportStat

        public DataSet GetVenueWiseDownloadTimeImportStat()
        {

            Hashtable objHT = new Hashtable();
            DBObjectPool pool = null;
            DBObject dbObject = null;
            DataSet ds = null;
            string uniID = clsGetSettings.UniversityID.Trim();
            try
            {

                objHT.Add("UniID", uniID);

                pool = DBObjectPool.Instance;
                dbObject = pool.AcquireDBObject();

                ds = dbObject.getparamdataset("PreExamV2_SRPD_Import_VenueWiseDownloadTimeStat", objHT);

            }
            catch (Exception ex)
            {
                return null;
                //throw ex;
            }
            finally
            {
                if (pool != null)
                    pool.ReleaseDBObject(dbObject);
            }

            if (ds != null)
                return ds;
            else
                return null;
        }
        #endregion

        #region ListExamEvent

        public DataTable ListExamEvent()
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            string Uni = clsGetSettings.UniversityID.Trim();
            Hashtable objHT = new Hashtable();

            DataTable dt;
            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();
                objHT.Add("UniID", Uni);

                dt = oDB.getparamdataset("PreExamV2_ListExamEvent", objHT).Tables[0];
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            finally
            {
                Pool.ReleaseDBObject(oDB);
            }
            return dt;
        }

        #endregion


        #region Cancelling Records

        public string Cancelrecords(string tablename)
        {
            string result;
            DBObjectPool Pool = null;
            DBObject oDB = null;
            Hashtable objHT = new Hashtable();

            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();

                objHT.Add("TableName", tablename);
                SqlCommand cmd = oDB.GenerateCommand("PreExamV2_SeatMgmt_ImportFromExcel_DeleteTable", objHT);
                int res = cmd.ExecuteNonQuery();
                result = "Successful";
            }
            catch (Exception ex)
            {
                result = ex.Message;
                return null;
            }
            finally
            {
                Pool.ReleaseDBObject(oDB);
            }
            return result;
        }

        #endregion
 

    } 
}
