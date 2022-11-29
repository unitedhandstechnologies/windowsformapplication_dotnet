using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using Classes;

namespace SRPD.PreExamination
{
    public class ClsotherInstitute
    {
        	#region Variable Declaration
	
		private string PK_Uni_ID;
		private string PK_Inst_ID;
		private string FK_SocTrt_ID;
		private string FK_InstTy_ID;
		private string Inst_Name;
		private string Inst_IsResearchCenterFlag;
		private string Inst_Minority_NonMinorityType;
		private string Inst_MinorityStatusFlag;
		private string FK_Reln_Lang_ID;
		private string FK_Fac_ID;
		private string FK_Sub_ID;
		private string FK_InstStat_ID;
		private string FK_ParBdy_ID;
		private string Inst_ForWhomFlag;
		private string FK_Country_ID;
		private string FK_State_ID;
		private string FK_District_ID;
		private string FK_Tehsil_ID;
		private string Inst_OtherTehsil;
		private string Inst_City;
		private string Inst_Address;
		private string Inst_PinCode;
		private string Inst_STD1;
		private string Inst_TelNo1;
		private string Inst_STD2;
		private string Inst_TelNo2;
		private string Inst_STD3;
		private string Inst_TelNo3;
		private string Inst_FaxSTD1;
		private string Inst_FaxNo1;
		private string Inst_FaxSTD2;
		private string Inst_FaxNo2;
		private string Inst_MobileNo1;
		private string Inst_MobileNo2;
		private string Inst_EmailID1;
		private string Inst_EmailID2;
		private string Inst_EmailID3;
		private string Inst_Website;
		private string Inst_ConductionModeFlag;
		private string Inst_Establishment_Date;
		private string Inst_Code;
		private string FK_Affi_ID;
		private string Inst_UniLetterToStateGovt_No;
		private string Inst_UniLetterToStateGovt_Date;
		private string Inst_UniLetterToStateGovt_Image;
		private string Inst_StateGovtGR_No;
		private string Inst_StateGovtGR_Date;
		private string Inst_StateGovtGR_Image;
		private string Inst_UniAffiliationLetter_No;
		private string Inst_UniAffiliationLetter_Date;
		private string Inst_UniAffiliationLetter_Image;
		private string Inst_UGCRec_2F_Letter_No;
		private string Inst_UGCRec_2F_Letter_Date;
		private string Inst_UGCRec_2F_Letter_Image;
		private string Inst_UGCRec_12B_Letter_No;
		private string Inst_UGCRec_12B_Letter_Date;
		private string Inst_UGCRec_12B_Letter_Image;
		private string Inst_AreaCodeFlag;
		private string Inst_NearestRlyStationName;
		private string Inst_DistFrmRlyStation;
		private string Inst_NearestBusStandName;
		private string Inst_DistFrmBusStand;
		private string Inst_NearestAirportName;
		private string Inst_DistFrmAirport;
		private string Inst_Longitude;
		private string Inst_Latitude;
		private string Inst_Altitude;
		private string Inst_Image;
		private string Inst_Vision;
		private string Inst_Mission;
		private string Inst_Goals;
		private string Inst_Principal_Director_HODName;
		private string FK_PriDirHOD_Country_ID;
		private string FK_PriDirHOD_State_ID;
		private string FK_PriDirHOD_District_ID;
		private string FK_PriDirHOD_Tehsil_ID;
		private string Inst_PriDirHOD_OtherTehsil;
		private string Inst_PriDirHOD_City;
		private string Inst_PriDirHOD_Address;
		private string Inst_PriDirHOD_PinCode;
		private string Inst_PriDirHOD_STD1;
		private string Inst_PriDirHOD_TelNo1;
		private string Inst_PriDirHOD_STD2;
		private string Inst_PriDirHOD_TelNo2;
		private string Inst_PriDirHOD_MobileNo1;
		private string Inst_PriDirHOD_EmailID1;
		private string Inst_PriDirHOD_EmailID2;
		private string Inst_OfficeInchargeName;
		private string FK_OffInchr_Desgn_ID;
		private string FK_OffInchr_Country_ID;
		private string FK_OffInchr_State_ID;
		private string FK_OffInchr_District_ID;
		private string FK_OffInchr_Tehsil_ID;
		private string Inst_OffInchr_OtherTehsil;
		private string Inst_OffInchr_City;
		private string Inst_OffInchr_Address;
		private string Inst_OffInchr_PinCode;
		private string Inst_OffInchr_STD1;
		private string Inst_OffInchr_TelNo1;
		private string Inst_OffInchr_STD2;
		private string Inst_OffInchr_TelNo2;
		private string Inst_OffInchr_MobileNo1;
		private string Inst_OffInchr_EmailID1;
		private string Inst_OffInchr_EmailID2;
		private string Active;
			
		private DataTable DT;

		#endregion

		#region Properties
		public string pk_Uni_ID	
		{ 
			get
			{ 	
				return PK_Uni_ID;	
			}
		}
		
		public string pk_Inst_ID	
		{
			get
			{ 	
				return PK_Inst_ID;	
			}
		}
		
		public string fk_SocTrt_ID	
		{ 
			get 
			{ 	
				return FK_SocTrt_ID;	
			}
		}
		
		public string fk_InstTy_ID	
		{ 
			get 
			{ 	
				return FK_InstTy_ID;	
			}
            set
            {
                FK_InstTy_ID = value;
            }
		}
		
		public string inst_Name	
		{ 
			get 
			{ 	
				return Inst_Name;	
			}
		}
		
		public string inst_IsResearchCenterFlag	
		{ 
			get 
			{ 	
				return Inst_IsResearchCenterFlag;	
			}
		}
		
		public string inst_Minority_NonMinorityType	
		{ 
			get 
			{ 	
				return Inst_Minority_NonMinorityType;	
			}
		}
		
		public string inst_MinorityStatusFlag	
		{ 
			get 
			{ 	
				return Inst_MinorityStatusFlag;	
			}
		}
		
		public string fk_Reln_Lang_ID	
		{ 
			get 
			{ 	
				return FK_Reln_Lang_ID;	
			}
		}
		public string fk_Fac_ID	
		{ 
			get 
			{ 	
				return FK_Fac_ID;	
			}
		}
		public string fk_Sub_ID
		{ 
			get 
			{ 	
				return FK_Sub_ID;	
			}
		}
		public string fk_InstStat_ID	
		{ 
			get 
			{ 	
				return FK_InstStat_ID;	
			}
		}
		
		public string fk_ParBdy_ID	
		{ 
			get 
			{ 	
				return FK_ParBdy_ID; 
			}
		}
		
		public string inst_ForWhomFlag	
		{ 
			get 
			{ 	
				return Inst_ForWhomFlag;	
			}
		}
		
		public string fk_Country_ID	
		{
			get
			{ 	
				return FK_Country_ID;	
			}
		}
		
		public string fk_State_ID	
		{
			get 
			{ 	
				return FK_State_ID;	
			}
		}
		
		public string fk_District_ID	
		{
			get 
			{ 	
				return FK_District_ID;	
			}
		}
		
		public string fk_Tehsil_ID	
		{ 
			get 
			{ 	
				return FK_Tehsil_ID;	
			}
		}
		
		public string inst_OtherTehsil	
		{ 
			get 
			{ 	
				return Inst_OtherTehsil;	
			}
		}
		
		public string inst_City	
		{ 
			get 
			{ 	
				return Inst_City;	
			}
		}
		
		public string inst_Address	
		{ 
			get 
			{ 	
				return Inst_Address;	
			} 
		}
		
		public string inst_PinCode	
		{ 
			get 
			{ 	
				return Inst_PinCode;	
			} 
		}
		
		public string inst_STD1	
		{ 
			get 
			{ 	
				return Inst_STD1;	
			} 
		}
		
		public string inst_TelNo1	
		{ 
			get 
			{ 	
				return Inst_TelNo1;	
			} 
		}
		
		public string inst_STD2	
		{ 
			get 
			{ 	
				return Inst_STD2;	
			} 
		}
		
		public string inst_TelNo2	
		{ 
			get 
			{ 	
				return Inst_TelNo2;	
			} 
		}
		
		public string inst_STD3	
		{ 
			get 
			{ 	
				return Inst_STD3;	
			} 
		}
		
		public string inst_TelNo3	
		{ 
			get 
			{ 	
				return Inst_TelNo3;	
			} 
		}
		
		public string inst_FaxSTD1	
		{ 
			get 
			{ 	
				return Inst_FaxSTD1;	
			} 
		}
		
		public string inst_FaxNo1	
		{ 
			get 
			{ 	
				return Inst_FaxNo1;	
			} 
		}
		
		public string inst_FaxSTD2	
		{ 
			get 
			{ 	
				return Inst_FaxSTD2;	
			} 
		}
		
		public string inst_FaxNo2	
		{ 
			get 
			{ 	
				return Inst_FaxNo2;	
			} 
		}
		
		public string inst_MobileNo1	
		{ 
			get 
			{ 	
				return Inst_MobileNo1;	
			}
		}
		
		public string inst_MobileNo2	
		{ 
			get 
			{ 	
				return Inst_MobileNo2;	
			} 
		}
		
		public string inst_EmailID1	
		{ 
			get 
			{ 	
				return Inst_EmailID1;	
			} 
		}
		
		public string inst_EmailID2	
		{ 
			get 
			{ 	
				return Inst_EmailID2;	
			} 
		}
		
		public string inst_EmailID3	
		{ 
			get 
			{ 	
				return Inst_EmailID3;	
			} 
		}
		
		public string inst_Website	
		{ 
			get 
			{ 	
				return Inst_Website;	
			} 
		}
		
		public string inst_ConductionModeFlag	
		{ 
			get
			{ 	
				return Inst_ConductionModeFlag;	
			} 
		}
		
		public string inst_Establishment_Date	
		{ 
			get 
			{ 	
				return Inst_Establishment_Date;	
			} 
		}
		
		public string inst_Code	
		{ 
			get 
			{ 	
				return Inst_Code;	
			} 
		}
		
		public string  fk_Affi_ID	
		{ 
			get 
			{ 	
				return FK_Affi_ID;	
			} 
		}
		
		public string inst_UniLetterToStateGovt_No	
		{ 
			get 
			{ 	
				return Inst_UniLetterToStateGovt_No;	
			} 
		}
		public string inst_UniLetterToStateGovt_Date	
		{ 
			get 
			{ 	
				return Inst_UniLetterToStateGovt_Date;	
			} 
		}
		public string inst_UniLetterToStateGovt_Image	
		{ 
			get 
			{ 	
				return Inst_UniLetterToStateGovt_Image;	
			} 
		}
		public string inst_StateGovtGR_No	
		{ 
			get 
			{ 	
				return Inst_StateGovtGR_No;	
			} 
		}
		public string inst_StateGovtGR_Date	
		{ 
			get 
			{ 	
				return Inst_StateGovtGR_Date;	
			} 
		}
		public string inst_StateGovtGR_Image	
		{ 
			get 
			{ 	
				return Inst_StateGovtGR_Image;	
			} 
		}
		public string inst_UniAffiliationLetter_No	
		{ 
			get 
			{
				return Inst_UniAffiliationLetter_No;	
			} 
		}
		public string inst_UniAffiliationLetter_Date	
		{ 
			get 
			{ 	
				return Inst_UniAffiliationLetter_Date;	
			} 
		}
		public string inst_UniAffiliationLetter_Image	
		{ 
			get 
			{ 	
				return Inst_UniAffiliationLetter_Image;	
			}
		}
		public string inst_UGCRec_2F_Letter_No	
		{ 
			get 
			{ 	
				return Inst_UGCRec_2F_Letter_No;	
			} 
		}
		public string inst_UGCRec_2F_Letter_Date	
		{ 
			get 
			{ 	
				return Inst_UGCRec_2F_Letter_Date;	
			} 
		}
		public string inst_UGCRec_2F_Letter_Image	
		{ 
			get 
			{ 	
				return Inst_UGCRec_2F_Letter_Image;	
			} 
		}
		public string inst_UGCRec_12B_Letter_No	
		{ 
			get 
			{ 	
				return Inst_UGCRec_12B_Letter_No;	
			} 
		}
		public string inst_UGCRec_12B_Letter_Date	
		{ 
			get 
			{ 	
				return Inst_UGCRec_12B_Letter_Date;	
			} 
		}
		public string inst_UGCRec_12B_Letter_Image	
		{ 
			get 
			{ 	
				return Inst_UGCRec_12B_Letter_Image;	
			} 
		}
		public string inst_AreaCodeFlag	
		{ 
			get 
			{ 	
				return Inst_AreaCodeFlag;	
			} 
		}
		public string inst_NearestRlyStationName	
		{ 
			get 
			{ 	
				return Inst_NearestRlyStationName;	
			} 
		}
		public string inst_DistFrmRlyStation	
		{ 
			get 
			{ 	
				return Inst_DistFrmRlyStation;	
			} 
		}
		public string inst_NearestBusStandName	
		{ 
			get 
			{ 	
				return Inst_NearestBusStandName;	
			} 
		}
		public string inst_DistFrmBusStand	
		{ 
			get 
			{ 	
				return Inst_DistFrmBusStand;	
			} 
		}
		public string inst_NearestAirportName	
		{ 
			get 
			{ 	
				return Inst_NearestAirportName;	
			} 
		}
		public string inst_DistFrmAirport	
		{ 
			get 
			{ 	
				return Inst_DistFrmAirport;	
			} 
		}
		public string inst_Longitude	
		{ 
			get 
			{ 	
				return Inst_Longitude;	
			} 
		}
		public string inst_Latitude	
		{ 
			get 
			{ 	
				return Inst_Latitude;	
			} 
		}
		public string inst_Altitude	
		{ 
			get 
			{ 	
				return Inst_Altitude;	
			} 
		}
		public string inst_Image	
		{ 
			get 
			{ 	
				return Inst_Image;	
			} 
		}
		public string inst_Vision	
		{ 
			get 
			{ 	
				return Inst_Vision;	
			} 
		}
		public string inst_Mission	
		{ 
			get 
			{ 	
				return Inst_Mission; 
			} 
		}
		
		public string inst_Goals	
		{ 
			get 
			{ 	
				return Inst_Goals;	
			} 
		}
		public string inst_Principal_Director_HODName	
		{ 
			get 
			{ 	
				return Inst_Principal_Director_HODName;	
			} 
		}
		public string fk_PriDirHOD_Country_ID	
		{ 
			get 
			{ 	
				return FK_PriDirHOD_Country_ID;	
			} 
		}
		public string fk_PriDirHOD_State_ID	
		{ 
			get 
			{ 	
				return FK_PriDirHOD_State_ID;	
			} 
		}
		public string fk_PriDirHOD_District_ID	
		{ 
			get 
			{ 	
				return FK_PriDirHOD_District_ID;	
			} 
		}
		public string fk_PriDirHOD_Tehsil_ID	
		{ 
			get 
			{ 	
				return FK_PriDirHOD_Tehsil_ID;	
			} 
		}
		public string inst_PriDirHOD_OtherTehsil	
		{ 
			get 
			{
				return Inst_PriDirHOD_OtherTehsil;	
			} 
		}
		public string inst_PriDirHOD_City	
		{ 
			get 
			{ 	
				return Inst_PriDirHOD_City;	
			} 
		}
		public string inst_PriDirHOD_Address	
		{ 
			get 
			{ 	
				return Inst_PriDirHOD_Address;	
			} 
		}
		public string inst_PriDirHOD_PinCode	
		{ 
			get 
			{ 	
				return Inst_PriDirHOD_PinCode;	
			} 
		}
		public string inst_PriDirHOD_STD1	
		{ 
			get 
			{ 	
				return Inst_PriDirHOD_STD1;	
			} 
		}
		public string inst_PriDirHOD_TelNo1	
		{ 
			get 
			{ 	
				return Inst_PriDirHOD_TelNo1;	
			} 
		}
		public string inst_PriDirHOD_STD2	
		{ 
			get 
			{ 	
				return Inst_PriDirHOD_STD2;	
			} 
		}
		public string inst_PriDirHOD_TelNo2	
		{ 
			get 
			{ 	
				return Inst_PriDirHOD_TelNo2;	
			} 
		}
		public string inst_PriDirHOD_MobileNo1	
		{ 
			get 
			{ 	
				return Inst_PriDirHOD_MobileNo1;	
			} 
		}
		public string inst_PriDirHOD_EmailID1	
		{ 
			get 
			{ 	
				return Inst_PriDirHOD_EmailID1;	

			} 
		}
		public string inst_PriDirHOD_EmailID2	
		{ 
			get 
			{ 	
				return Inst_PriDirHOD_EmailID2;	
			} 
		}
		public string inst_OfficeInchargeName	
		{ 
			get 
			{ 	
				return Inst_OfficeInchargeName;	
			} 
		}
		public string  fk_OffInchr_Desgn_ID	
		{ 
			get 
			{ 	
				return FK_OffInchr_Desgn_ID;	
			} 
		}
		public string  fk_OffInchr_Country_ID	
		{ 
			get 
			{ 	
				return FK_OffInchr_Country_ID;	
			} 
		}
		public string  fk_OffInchr_State_ID	
		{ 
			get 
			{ 	
				return FK_OffInchr_State_ID;	
			} 
		}
		public string  fk_OffInchr_District_ID	
		{ 
			get 
			{ 	
				return FK_OffInchr_District_ID;	
			} 
		}
		
		public string  fk_OffInchr_Tehsil_ID	
		{ 
			get 
			{ 	
				return FK_OffInchr_Tehsil_ID;	
			} 
		}
		public string inst_OffInchr_OtherTehsil	
		{ 
			get 
			{ 	
				return Inst_OffInchr_OtherTehsil;	
			} 
		}
		public string inst_OffInchr_City	
		{ 
			get 
			{ 	
				return Inst_OffInchr_City;	
			} 
		}
		public string inst_OffInchr_Address	
		{ 
			get
			{ 	
				return Inst_OffInchr_Address;	
			} 
		}
		public string inst_OffInchr_PinCode	
		{ 
			get 
			{ 	
				return Inst_OffInchr_PinCode;	
			} 
		}
		public string inst_OffInchr_STD1	
		{ 
			get
			{ 	
				return Inst_OffInchr_STD1;	
			} 
		}
		public string inst_OffInchr_TelNo1	
		{ 
			get 
			{ 	
				return Inst_OffInchr_TelNo1;	
			} 
		}
		public string inst_OffInchr_STD2	
		{ 
			get 
			{ 	
				return Inst_OffInchr_STD2;	
			} 
		}
		public string inst_OffInchr_TelNo2	
		{ 
			get 
			{ 	
				return Inst_OffInchr_TelNo2;	
			} 
		}
		public string inst_OffInchr_MobileNo1	
		{ 
			get 
			{ 	
				return Inst_OffInchr_MobileNo1;	
			} 
		}
		public string inst_OffInchr_EmailID1	
		{ 
			get 
			{ 	
				return Inst_OffInchr_EmailID1;	
			} 
		}
		public string inst_OffInchr_EmailID2	
		{ 
			get 
			{ 	
				return Inst_OffInchr_EmailID2;	
			} 
		}
       
		public string active	
		{ 
			get 
			{ 	
				return Active;	
			} 
		}
	
		#endregion

		#region Constructor
        public ClsotherInstitute(string UniID, string InstID)
		{
			
			PK_Uni_ID = UniID;
			PK_Inst_ID = InstID;
            otherifo_Load();
		}
		#endregion

		#region Function Load
		private void Load()
		{
			DBObjectPool Pool=null;
			DBObject oDB=null;
			try
			{
				Pool = DBObjectPool.Instance;
				oDB = Pool.AcquireDBObject();

				Hashtable oHsTb = new Hashtable();
				oHsTb.Add("pk_Uni_ID",PK_Uni_ID);
				oHsTb.Add("pk_Inst_ID",PK_Inst_ID);
			
				DT =oDB.getparamdataset("IDV2_Attribute_Institute",oHsTb).Tables[0]; 
			
				if(DT.Rows.Count > 0)
				{
					PK_Uni_ID=DT.Rows[0]["pk_Uni_ID"].ToString().Trim();
					PK_Inst_ID=DT.Rows[0]["pk_Inst_ID"].ToString().Trim();
					FK_SocTrt_ID=DT.Rows[0]["fk_SocTrt_ID"].ToString().Trim();
					FK_InstTy_ID=DT.Rows[0]["fk_InstTy_ID"].ToString().Trim();
					Inst_Name=DT.Rows[0]["Inst_Name"].ToString().Trim();
					Inst_IsResearchCenterFlag=DT.Rows[0]["Inst_IsResearchCenterFlag"].ToString().Trim();
					//Inst_Minority_NonMinorityType=DT.Rows[0]["Inst_Minority_NonMinorityType"].ToString().Trim();
					Inst_MinorityStatusFlag=DT.Rows[0]["Inst_MinorityStatusFlag"].ToString().Trim();
					FK_Reln_Lang_ID=DT.Rows[0]["fk_Reln_Lang_ID"].ToString().Trim();
				
					FK_Fac_ID=DT.Rows[0]["FK_Fac_ID"].ToString().Trim();
					FK_Sub_ID=DT.Rows[0]["FK_Sub_ID"].ToString().Trim();

					FK_InstStat_ID=DT.Rows[0]["fk_InstStat_ID"].ToString().Trim();
					FK_ParBdy_ID=DT.Rows[0]["fk_ParBdy_ID"].ToString().Trim();
					Inst_ForWhomFlag=DT.Rows[0]["Inst_ForWhomFlag"].ToString().Trim();
					FK_Country_ID=DT.Rows[0]["fk_Country_ID"].ToString().Trim();
					FK_State_ID=DT.Rows[0]["fk_State_ID"].ToString().Trim();
					FK_District_ID=DT.Rows[0]["fk_District_ID"].ToString().Trim();
					FK_Tehsil_ID=DT.Rows[0]["fk_Tehsil_ID"].ToString().Trim();
					Inst_OtherTehsil=DT.Rows[0]["Inst_OtherTehsil"].ToString().Trim();
					Inst_City=DT.Rows[0]["Inst_City"].ToString().Trim();
					Inst_Address=DT.Rows[0]["Inst_Address"].ToString().Trim();
					Inst_PinCode=DT.Rows[0]["Inst_PinCode"].ToString().Trim();
					Inst_STD1=DT.Rows[0]["Inst_STD1"].ToString().Trim();
					Inst_TelNo1=DT.Rows[0]["Inst_TelNo1"].ToString().Trim();
					Inst_STD2=DT.Rows[0]["Inst_STD2"].ToString().Trim();
					Inst_TelNo2=DT.Rows[0]["Inst_TelNo2"].ToString().Trim();
					Inst_STD3=DT.Rows[0]["Inst_STD3"].ToString().Trim();
					Inst_TelNo3=DT.Rows[0]["Inst_TelNo3"].ToString().Trim();
					Inst_FaxSTD1=DT.Rows[0]["Inst_FaxSTD1"].ToString().Trim();
					Inst_FaxNo1=DT.Rows[0]["Inst_FaxNo1"].ToString().Trim();
					Inst_FaxSTD2=DT.Rows[0]["Inst_FaxSTD2"].ToString().Trim();
					Inst_FaxNo2=DT.Rows[0]["Inst_FaxNo2"].ToString().Trim();
					Inst_MobileNo1=DT.Rows[0]["Inst_MobileNo1"].ToString().Trim();
					Inst_MobileNo2=DT.Rows[0]["Inst_MobileNo2"].ToString().Trim();
					Inst_EmailID1=DT.Rows[0]["Inst_EmailID1"].ToString().Trim();
					Inst_EmailID2=DT.Rows[0]["Inst_EmailID2"].ToString().Trim();
					Inst_EmailID3=DT.Rows[0]["Inst_EmailID3"].ToString().Trim();
					Inst_Website=DT.Rows[0]["Inst_Website"].ToString().Trim();
					Inst_ConductionModeFlag=DT.Rows[0]["Inst_ConductionModeFlag"].ToString().Trim();
					Inst_Establishment_Date=DT.Rows[0]["Inst_Establishment_Date"].ToString().Trim();
					Inst_Code=DT.Rows[0]["Inst_Code"].ToString().Trim();
					
					Inst_AreaCodeFlag=DT.Rows[0]["Inst_AreaCodeFlag"].ToString().Trim();
					Inst_NearestRlyStationName=DT.Rows[0]["Inst_NearestRlyStationName"].ToString().Trim();
					Inst_DistFrmRlyStation=DT.Rows[0]["Inst_DistFrmRlyStation"].ToString().Trim();
					Inst_NearestBusStandName=DT.Rows[0]["Inst_NearestBusStandName"].ToString().Trim();
					Inst_DistFrmBusStand=DT.Rows[0]["Inst_DistFrmBusStand"].ToString().Trim();
					Inst_NearestAirportName=DT.Rows[0]["Inst_NearestAirportName"].ToString().Trim();
					Inst_DistFrmAirport=DT.Rows[0]["Inst_DistFrmAirport"].ToString().Trim();
					Inst_Longitude=DT.Rows[0]["Inst_Longitude"].ToString().Trim();
					Inst_Latitude=DT.Rows[0]["Inst_Latitude"].ToString().Trim();
					Inst_Altitude=DT.Rows[0]["Inst_Altitude"].ToString().Trim();
					Inst_Image=DT.Rows[0]["Inst_Image"].ToString().Trim();
					Inst_Vision=DT.Rows[0]["Inst_Vision"].ToString().Trim();
					Inst_Mission=DT.Rows[0]["Inst_Mission"].ToString().Trim();
					Inst_Goals=DT.Rows[0]["Inst_Goals"].ToString().Trim();
					Inst_Principal_Director_HODName=DT.Rows[0]["Inst_Principal_Director_HODName"].ToString().Trim();
					FK_PriDirHOD_Country_ID=DT.Rows[0]["fk_PriDirHOD_Country_ID"].ToString().Trim();
					FK_PriDirHOD_State_ID=DT.Rows[0]["fk_PriDirHOD_State_ID"].ToString().Trim();
					FK_PriDirHOD_District_ID=DT.Rows[0]["fk_PriDirHOD_District_ID"].ToString().Trim();
					FK_PriDirHOD_Tehsil_ID=DT.Rows[0]["fk_PriDirHOD_Tehsil_ID"].ToString().Trim();
					Inst_PriDirHOD_OtherTehsil=DT.Rows[0]["Inst_PriDirHOD_OtherTehsil"].ToString().Trim();
					Inst_PriDirHOD_City=DT.Rows[0]["Inst_PriDirHOD_City"].ToString().Trim();
					Inst_PriDirHOD_Address=DT.Rows[0]["Inst_PriDirHOD_Address"].ToString().Trim();
					Inst_PriDirHOD_PinCode=DT.Rows[0]["Inst_PriDirHOD_PinCode"].ToString().Trim();
					Inst_PriDirHOD_STD1=DT.Rows[0]["Inst_PriDirHOD_STD1"].ToString().Trim();
					Inst_PriDirHOD_TelNo1=DT.Rows[0]["Inst_PriDirHOD_TelNo1"].ToString().Trim();
					Inst_PriDirHOD_STD2=DT.Rows[0]["Inst_PriDirHOD_STD2"].ToString().Trim();
					Inst_PriDirHOD_TelNo2=DT.Rows[0]["Inst_PriDirHOD_TelNo2"].ToString().Trim();
					Inst_PriDirHOD_MobileNo1=DT.Rows[0]["Inst_PriDirHOD_MobileNo1"].ToString().Trim();
					Inst_PriDirHOD_EmailID1=DT.Rows[0]["Inst_PriDirHOD_EmailID1"].ToString().Trim();
					Inst_PriDirHOD_EmailID2=DT.Rows[0]["Inst_PriDirHOD_EmailID2"].ToString().Trim();
					
					Active=DT.Rows[0]["Active"].ToString().Trim();
				}
			}
			finally
			{
				Pool.ReleaseDBObject(oDB);
			}
		}
		#endregion

        #region Function Load

      public  void otherifo_Load()
        {
            DBObjectPool Pool = null;
            DBObject oDB = null;
            try
            {
                Pool = DBObjectPool.Instance;
                oDB = Pool.AcquireDBObject();

                Hashtable oHsTb = new Hashtable();
                oHsTb.Add("pk_Uni_ID", PK_Uni_ID);
                oHsTb.Add("pk_Inst_ID", PK_Inst_ID);

                DT = oDB.getparamdataset("IDV2_Attribute_OtherInstitute", oHsTb).Tables[0];

                if (DT.Rows.Count > 0)
                {
                    PK_Uni_ID = DT.Rows[0]["pk_Uni_ID"].ToString().Trim();
                    PK_Inst_ID = DT.Rows[0]["pk_Inst_ID"].ToString().Trim();
                    Inst_Name = DT.Rows[0]["Inst_Name"].ToString().Trim();
                    fk_InstTy_ID = DT.Rows[0]["fk_InstTy_ID"].ToString().Trim();
                    Inst_Principal_Director_HODName = DT.Rows[0]["Inst_Principal_Director_HODName"].ToString().Trim();
                    FK_PriDirHOD_Country_ID = DT.Rows[0]["fk_PriDirHOD_Country_ID"].ToString().Trim();
                    FK_PriDirHOD_State_ID = DT.Rows[0]["fk_PriDirHOD_State_ID"].ToString().Trim();
                    FK_PriDirHOD_District_ID = DT.Rows[0]["fk_PriDirHOD_District_ID"].ToString().Trim();
                    FK_PriDirHOD_Tehsil_ID = DT.Rows[0]["fk_PriDirHOD_Tehsil_ID"].ToString().Trim();
                    Inst_PriDirHOD_OtherTehsil = DT.Rows[0]["Inst_PriDirHOD_OtherTehsil"].ToString().Trim();
                    Inst_PriDirHOD_City = DT.Rows[0]["Inst_PriDirHOD_City"].ToString().Trim();
                    Inst_PriDirHOD_Address = DT.Rows[0]["Inst_PriDirHOD_Address"].ToString().Trim();
                    Inst_PriDirHOD_PinCode = DT.Rows[0]["Inst_PriDirHOD_PinCode"].ToString().Trim();
                    Inst_PriDirHOD_STD1 = DT.Rows[0]["Inst_PriDirHOD_STD1"].ToString().Trim();
                    Inst_PriDirHOD_TelNo1 = DT.Rows[0]["Inst_PriDirHOD_TelNo1"].ToString().Trim();
                    Inst_PriDirHOD_STD2 = DT.Rows[0]["Inst_PriDirHOD_STD2"].ToString().Trim();
                    Inst_PriDirHOD_TelNo2 = DT.Rows[0]["Inst_PriDirHOD_TelNo2"].ToString().Trim();
                    Inst_PriDirHOD_MobileNo1 = DT.Rows[0]["Inst_PriDirHOD_MobileNo1"].ToString().Trim();
                    Inst_PriDirHOD_EmailID1 = DT.Rows[0]["Inst_PriDirHOD_EmailID1"].ToString().Trim();
                    Inst_PriDirHOD_EmailID2 = DT.Rows[0]["Inst_PriDirHOD_EmailID2"].ToString().Trim();
                    
                    Active = DT.Rows[0]["Active"].ToString().Trim();
                }
            }
            finally
            {
                Pool.ReleaseDBObject(oDB);
            }
        }
        #endregion

		#region Function Reset
		private void Reset()
		{
			Inst_Name="";
			Inst_IsResearchCenterFlag="";
			Inst_Minority_NonMinorityType="";
			Inst_MinorityStatusFlag="";
			Inst_OtherTehsil="";
			Inst_City="";
			Inst_Address="";
			Inst_PinCode="";
			Inst_STD1="";
			Inst_TelNo1="";
			Inst_STD2="";
			Inst_TelNo2="";
			Inst_STD3="";
			Inst_TelNo3="";
			Inst_FaxSTD1="";
			Inst_FaxNo1="";
			Inst_FaxSTD2="";
			Inst_FaxNo2="";
			Inst_MobileNo1="";
			Inst_MobileNo2="";
			Inst_EmailID1="";
			Inst_EmailID2="";
			Inst_EmailID3="";
			Inst_Website="";
			Inst_ConductionModeFlag="";
			Inst_Establishment_Date="";
			Inst_Code="";
			Inst_UniLetterToStateGovt_No="";
			Inst_UniLetterToStateGovt_Date="";
			Inst_UniLetterToStateGovt_Image="";
			Inst_StateGovtGR_No="";
			Inst_StateGovtGR_Date="";
			Inst_StateGovtGR_Image="";
			Inst_UniAffiliationLetter_No="";
			Inst_UniAffiliationLetter_Date="";
			Inst_UniAffiliationLetter_Image="";
			Inst_UGCRec_2F_Letter_No="";
			Inst_UGCRec_2F_Letter_Date="";
			Inst_UGCRec_2F_Letter_Image="";
			Inst_UGCRec_12B_Letter_No="";
			Inst_UGCRec_12B_Letter_Date="";
			Inst_UGCRec_12B_Letter_Image="";
			Inst_AreaCodeFlag="";
			Inst_NearestRlyStationName="";
			Inst_DistFrmRlyStation="";
			Inst_NearestBusStandName="";
			Inst_DistFrmBusStand="";
			Inst_NearestAirportName="";
			Inst_DistFrmAirport="";
			Inst_Longitude="";
			Inst_Latitude="";
			Inst_Altitude="";
			Inst_Image="";
			Inst_Vision="";
			Inst_Mission="";
			Inst_Goals="";
			Inst_Principal_Director_HODName="";
			Inst_PriDirHOD_OtherTehsil="";
			Inst_PriDirHOD_City="";
			Inst_PriDirHOD_Address="";
			Inst_PriDirHOD_PinCode="";
			Inst_PriDirHOD_STD1="";
			Inst_PriDirHOD_TelNo1="";
			Inst_PriDirHOD_STD2="";
			Inst_PriDirHOD_TelNo2="";
			Inst_PriDirHOD_MobileNo1="";
			Inst_PriDirHOD_EmailID1="";
			Inst_PriDirHOD_EmailID2="";
			Inst_OfficeInchargeName="";
			Inst_OffInchr_OtherTehsil="";
			Inst_OffInchr_City="";
			Inst_OffInchr_Address="";
			Inst_OffInchr_PinCode="";
			Inst_OffInchr_STD1="";
			Inst_OffInchr_TelNo1="";
			Inst_OffInchr_STD2="";
			Inst_OffInchr_TelNo2="";
			Inst_OffInchr_MobileNo1="";
			Inst_OffInchr_EmailID1="";
			Inst_OffInchr_EmailID2="";
			Active="";
		}
		#endregion

        #region All Institutes
        public static DataTable AllInstitutes(string pk_Uni_ID)
		{
			DBObjectPool Pool=null;
			DBObject oDB=null;
			try
			{
				Pool = DBObjectPool.Instance;
				oDB = Pool.AcquireDBObject();

				Hashtable oHs = new Hashtable();
                oHs.Add("pk_Uni_ID", pk_Uni_ID);

				//DataTable DT = getdataset("IDV2_AllInstitute").Tables[0]; //Change Procedure Name
				DataTable DT =oDB.getparamdataset("IDV2_AllInstitute",oHs).Tables[0]; 
				return DT;
			}
			finally
			{
				Pool.ReleaseDBObject(oDB);
			}
		}
		#endregion

		#region Institute Details
		public static DataTable AttributeInstituteDetails(string pk_Uni_ID,string pk_Inst_ID)
		{
			DBObjectPool Pool=null;
			DBObject oDB=null;
			try
			{
				Pool = DBObjectPool.Instance;
				oDB = Pool.AcquireDBObject();

				Hashtable oHs = new Hashtable();
			
				oHs.Add("pk_Uni_ID",pk_Uni_ID);
				oHs.Add("pk_Inst_ID",pk_Inst_ID);

                DataTable DT = oDB.getparamdataset("IDV2_Attribute_OtherInstitute", oHs).Tables[0]; 
				return DT;
			}
			finally
			{
				Pool.ReleaseDBObject(oDB);
			}
		}
		#endregion

		#region Institute Name
		public static string InstituteName(string pkUniID,string pkInstID)
		{
			DBObjectPool Pool=null;
			DBObject oDB=null;
			try
			{
				Pool = DBObjectPool.Instance;
				oDB = Pool.AcquireDBObject();

				Hashtable oHs = new Hashtable();
				string InstName="";
			
				oHs.Add("pk_Uni_ID",pkUniID);
				oHs.Add("pk_Inst_ID",pkInstID);
					
				DataTable DT =oDB.getparamdataset("IDV2_InstituteName",oHs).Tables[0]; 
				if(DT.Rows.Count>0)
				{
					InstName=DT.Rows[0]["Inst_Name"].ToString();
				}
				return InstName;
			}
			finally
			{
				Pool.ReleaseDBObject(oDB);
			}
		}
		#endregion

		#region Institute Search
		public static DataTable InstituteSearch(string Uni_ID,string InstTy_ID,string Inst_Name,string Country_ID,string State_ID,string District_ID,string Tehsil_ID,string Code)
		{
			DBObjectPool Pool=null;
			DBObject oDB=null;
			try
			{
				Pool = DBObjectPool.Instance;
				oDB = Pool.AcquireDBObject();

				Hashtable oHs = new Hashtable();
			
				oHs.Add("Uni_ID",Uni_ID);
				oHs.Add("InstTy_ID",InstTy_ID);
				oHs.Add("Inst_Name",Inst_Name);
				oHs.Add("Country_ID",Country_ID);
				oHs.Add("State_ID",State_ID);
				oHs.Add("District_ID",District_ID);
				oHs.Add("Tehsil_ID",Tehsil_ID);
                oHs.Add("Inst_code", Code);
				DataTable DT =oDB.getparamdataset("IDV2_SearchInstitute",oHs).Tables[0]; 
				return DT;
			}
			finally
			{
				Pool.ReleaseDBObject(oDB);
			}
		}
		#endregion
    }
}
