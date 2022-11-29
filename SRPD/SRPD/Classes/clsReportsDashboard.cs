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

namespace Classes
{
    public class clsReportsDashboard
    {

        string UniID = clsGetSettings.UniversityID.Trim();

        internal DataSet GetSRPDDashboardCount(Hashtable oHT)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            DataSet oDT = null;
            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();
                oDT = oDB.getparamdataset("PreExamv2_SRPD_Dashboard_Count", oHT);
            }
            finally
            {
                Pool.ReleaseDBObject(oDB);
            }
            return oDT;
        }

        public DataTable List_SRPD_DashBoard(string DateTime)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();

            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();
                ht.Add("UniId", UniID);
                ht.Add("DateTime", DateTime);

                ds = oDB.getparamdataset("PreExamv2_SRPD_Dashboard_NotUploadedPapers_Report", ht);
            }

            finally
            {
                Pool.ReleaseDBObject(oDB);

            }
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;

        }

        public DataTable List_SRPD_DashBoardNotPublishedVenues(string DateTime)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();

            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();
                ht.Add("UniId", UniID);
                ht.Add("DateTime", DateTime);

                ds = oDB.getparamdataset("PreExamv2_SRPD_Dashboard_NotPublishedVenues_Report", ht);
            }

            finally
            {
                Pool.ReleaseDBObject(oDB);

            }
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;

        }

        public DataTable List_SRPD_DashBoardDownloadPending(string DateTime, string StartTime, string EndTime)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();

            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();
                ht.Add("UniId", UniID);
                ht.Add("DateTime", DateTime);
                ht.Add("StartTime", StartTime);
                ht.Add("EndTime", EndTime);

                ds = oDB.getparamdataset("PreExamv2_SRPD_Dashboard_DownloadPendingPaperCount_Report", ht);
            }

            finally
            {
                Pool.ReleaseDBObject(oDB);

            }
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;

        }

        public DataTable List_SRPD_DashBoardNotPublishedPapers(string DateTime)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();

            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();
                ht.Add("UniId", UniID);
                ht.Add("DateTime", DateTime);

                ds = oDB.getparamdataset("PreExamv2_SRPD_Dashboard_TimeTableNotPublishedPapers_Report", ht);
            }

            finally
            {
                Pool.ReleaseDBObject(oDB);

            }
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;

        }

        public DataTable List_SRPD_DashBoardPaperView(string facId, string crId, string molrnId, string ptrnId, string brnId, string crprDetailsId, string crprchId, string exevId, string ppPpheadCrprchID, string DateTime, string StartTime, string EndTime)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();

            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();

                ht.Add("UniID", UniID);
                ht.Add("FacID", facId);
                ht.Add("CrID", crId);
                ht.Add("MoLrnID", molrnId);
                ht.Add("PtrnID", ptrnId);
                ht.Add("BrnID", brnId);
                ht.Add("CrPrDetailsID", crprDetailsId);
                ht.Add("CrPrChID", crprchId);
                ht.Add("ExEvID", exevId);
                ht.Add("PpPpheadCrprchID", ppPpheadCrprchID);
                ht.Add("DateTime", DateTime);
                ht.Add("StartTime", StartTime);
                ht.Add("EndTime", EndTime);

                ds = oDB.getparamdataset("PreExamv2_SRPD_Dashboard_DownloadPendingPaperView", ht);
            }

            finally
            {
                Pool.ReleaseDBObject(oDB);

            }
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;

        }

        public DataTable List_SRPD_DashBoardFaculty(string DateTime)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();

            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();
                ht.Add("UniId", UniID);
                ht.Add("DateTime", DateTime);

                ds = oDB.getparamdataset("PreExamv2_SRPD_Dashboard_ProgramsWithNoVenue", ht);
            }

            finally
            {
                Pool.ReleaseDBObject(oDB);

            }
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;

        }

        public DataTable List_SRPD_DashBoardInstitute(string DateTime)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();

            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();
                ht.Add("UniId", UniID);
                ht.Add("DateTime", DateTime);

                ds = oDB.getparamdataset("PreExamv2_SRPD_Dashboard_InstitutesNotMappedToAnyCenter_Report", ht);
            }

            finally
            {
                Pool.ReleaseDBObject(oDB);

            }
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;

        }

        public DataTable List_SRPD_VenueWiseDashBoardPendingPaperList(string UniID, string ExEvID, string InstID, string DateTime, string StartTime, string EndTime)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            UniID = clsGetSettings.UniversityID.Trim();

            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();
                ht.Add("UniID", UniID);              
                ht.Add("ExEvID", ExEvID);
                ht.Add("InstID", InstID);
                ht.Add("DateTime", DateTime);
                ht.Add("StartTime", StartTime);
                ht.Add("EndTime", EndTime);


                ds = oDB.getparamdataset("PreExamv2_SRPD_Dashboard_VenueWiseDownloadPendingPaperList", ht);
            }

            finally
            {
                Pool.ReleaseDBObject(oDB);

            }
            if (ds != null && ds.Tables.Count > 0)
                return ds.Tables[0];
            else
                return null;

        }



        #region  List_SRPD_WaterMarkWise_Details
        public DataSet List_SRPD_WaterMarkWise_Details(int WaterMark)
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            DataSet ds = new DataSet();
            Hashtable ht = new Hashtable();
            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();
                ht.Add("WaterMark", WaterMark);
                                          
                ds = oDB.getparamdataset("PreExamv2_SRPD_WaterMarkCodeWise_Details", ht);
            }
            finally
            {
                Pool.ReleaseDBObject(oDB);
            }
            if (ds != null && ds.Tables.Count > 0)
                return ds;
            else
                return null;
        }
        #endregion


    }


}
