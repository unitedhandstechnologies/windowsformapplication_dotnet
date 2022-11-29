using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Classes;
using System.Collections;
using System.Data.SqlClient;

namespace Classes
{
    public class clsPaperSetter
    {
        public DataSet GetPaperSetterRegistrationNotificationDetail()
        {
            DataSet oDs = null;
            DBObjectPool oPool = null;
            DBObject oDB = null;

            try
            {
                oPool = DBObjectPool.Instance;
                oDB = oPool.AcquireDBObject();
                oDs = oDB.getdataset("IDV2_GetPaperSetterRegistrationNotificationDetail");
            }
            finally
            {
                oPool.ReleaseDBObject(oDB);
            }

            return (oDs);
        }
    }


}