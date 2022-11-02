using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Classes;

namespace SRPD.PreExamination
{
	/// <summary>
	/// Summary description for clsDesignation.
	/// </summary>
	public class clsDesignation
	{
		public clsDesignation()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region All NonTeaching Designations
		/// <summary>
		/// This function gives all Non Teaching Designations. 
		/// </summary>
		/// <returns>datatable</returns>
		public static DataTable allNonTeachingDesignations()
		{
			DBObjectPool Pool=null;
			DBObject oDB=null;
			try
			{
				Pool = DBObjectPool.Instance;
				oDB = Pool.AcquireDBObject();

				DataTable DT =oDB.getdataset("IDV2_allNonTeachingDesignations").Tables[0];
				return(DT);
			}
			finally
			{
				Pool.ReleaseDBObject(oDB);
			}
		}
		#endregion


		



	}
}
