using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Classes;
using System.Collections;
using System.Data.SqlClient;


namespace SRPD.PreExamination
{
    public class clsSupervisor
    {


        public clsSupervisor()
        {
        }
        internal string[] AddSupervisor(Hashtable oHs)
        {
            DBObjectPool oPool = null;
            DBObject oDB = null;
            SqlCommand oCmd;
            string[] sRes = new string[1];

            int iRows = 0;
            try
            {
                oPool = DBObjectPool.Instance;
                oDB = oPool.AcquireDBObject();
                oCmd = oDB.GenerateCommand("IDV2_Add_SupervisorDetails", oHs);
                iRows = oCmd.ExecuteNonQuery();
                // if (iRows > 0)
                // {
                sRes[0] = oCmd.Parameters["@Status"].Value.ToString();
                //sRes[1] = oCmd.Parameters["@OTP"].Value.ToString();
               }
          
            finally
            {
                oPool.ReleaseDBObject(oDB);
            }

            return sRes;
        }

        internal string[] UpdateSupervisor(Hashtable oHs)
        {
            //string[] arr = new string[2];
            //clsDBOperations oDBOperations = new clsDBOperations();
            //arr = oDBOperations.Update(oHs, "");
            //return arr;


            DBObjectPool oPool = null;
            DBObject oDB = null;
            SqlCommand oCmd;
            string[] sRes = new string[1];

            int iRows = 0;
            try
            {
                oPool = DBObjectPool.Instance;
                oDB = oPool.AcquireDBObject();
                oCmd = oDB.GenerateCommand("[IDV2_update_SupervisorDetails]", oHs);
                iRows = oCmd.ExecuteNonQuery();
                // if (iRows > 0)
                // {
                sRes[0] = oCmd.Parameters["@Status"].Value.ToString();
               //Res[1] = oCmd.Parameters["@OTP"].Value.ToString();
            }

            finally
            {
                oPool.ReleaseDBObject(oDB);
            }

            return sRes;
        }

        internal DataTable ListSupervisor( Hashtable oHt)
        {
            DataTable oDt = new DataTable();
            clsDBOperations oDBOperations = new clsDBOperations();
            oDt = oDBOperations.List("IDV2_List_SupervisorDetails", oHt);
            return oDt;
        }

        internal string[] DeletetSupervisor(Hashtable oHs)
        {

            string[] arr = new string[2];
            clsDBOperations oDBOperations = new clsDBOperations();
            arr = oDBOperations.Delete(oHs, "IDV2_Delete_SupervisorDetails");
            return arr;

        }

        public DataSet GetSupervisiorRegistrationNotificationDetail()
        {
            DataSet oDs = null;
            DBObjectPool oPool = null;
            DBObject oDB = null;

            try
            {
                oPool = DBObjectPool.Instance;
                oDB = oPool.AcquireDBObject();
                oDs = oDB.getdataset("IDV2_GetSupervisiorRegistrationNotificationDetail");
            }
            finally
            {
                oPool.ReleaseDBObject(oDB);
            }

            return (oDs);
        }

        public string[] UpdateSupervisiorVerifyStatus(Hashtable oHs)
        {

            string[] arr = new string[2];
            clsDBOperations oDBOperations = new clsDBOperations();
            arr = oDBOperations.Update(oHs, "IDV2_Verify_SupervisorDetails");
            return arr;
        }

        internal string[] GetOTP()
        {
           

            DBObjectPool oPool = null;
            DBObject oDB = null;
            SqlCommand oCmd;
            Hashtable oHs=new Hashtable();
            string[] sRes = new string[1];

            int iRows = 0;
            try
            {
                oPool = DBObjectPool.Instance;
                oDB = oPool.AcquireDBObject();
                oCmd = oDB.GenerateCommand("IDV2_GetSupervisor_OTP",oHs);
                iRows = oCmd.ExecuteNonQuery();
                // if (iRows > 0)
                // {
               
                sRes[0] = oCmd.Parameters["@OTP"].Value.ToString();
            }

            finally
            {
                oPool.ReleaseDBObject(oDB);
            }

            return sRes;
        }

        
    }
}