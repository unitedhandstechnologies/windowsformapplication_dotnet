using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using Classes;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SearchWaterMark : System.Web.UI.Page
    {


        #region Properties
        // Hashtable oHt;
        DataSet ds;
        DataTable oDT;
        clsReportsDashboard oReportsDashboard;
        string todayDate = string.Empty;

        Classes.clsCommon oCommon = new clsCommon();

        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #endregion
        protected void btnDateSelection_Click(object sender, EventArgs e)
        {
             LoadData();
        }

        #region   LoadData
        private void LoadData()
        {
            try
            {
                string uniID = clsGetSettings.UniversityID.Trim();
                hidUniId.Value = uniID;
                int watermark;
                string val = txtWaterMark.Text;
                watermark = Int32.Parse(val);

                oReportsDashboard = new clsReportsDashboard();

                ds = oReportsDashboard.List_SRPD_WaterMarkWise_Details(watermark);

                //if (ds != null)
                //{


                 int rowcnt = ds.Tables[0].Rows.Count; 
 
               
                   oDT = ds.Tables[0];
                   if ( rowcnt > 0)
                    {
                        divmsg.Visible = false;
                        lblmsg.Visible = false;
                        grvWaterMarkDetails.DataSource = oDT;
                        grvWaterMarkDetails.DataBind();
                        grvWaterMarkDetails.Visible = true;

                    }
                    else
                    {
                        divmsg.Visible = true;
                        lblmsg.Visible = true;
                        lblmsg.ForeColor = System.Drawing.Color.DarkOrange;
                        txtWaterMark.Text = "";
                        lblmsg.Text = "No Data Found";
                        grvWaterMarkDetails.Visible = false;
                    }


                }
           // }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }


        }
        #endregion




        
    }
}