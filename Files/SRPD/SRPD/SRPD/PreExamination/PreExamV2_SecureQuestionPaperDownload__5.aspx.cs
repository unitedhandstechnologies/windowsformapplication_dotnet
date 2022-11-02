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

using PreExamClstLib.Services;
using Classes;
using System.IO;
using System.Text;
using System.Xml;
using System.Globalization;
using System.Data.Sql;
using System.Data.SqlClient;
using Sancharak;
using DPTemplate2;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SecureQuestionPaperDownload__5 : System.Web.UI.Page
    {
        #region Variables
        static DataTable dt = null;
        string[] retrunKeys = new string[1];
        int downloadTime = 0;
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            clsUser user = new clsUser();
            user = (clsUser)Session["user"];

            if (!IsPostBack)
            {
                if (user.UserTypeCode == "0")
                {
                    SetHiddenVariables();
                    FillGrid();
                }

            }
            lblDownloadMsg.Text = "";
        }
        #endregion

        #region Events

        #region btnDownload_Click
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            int status = 0;
            clsUser user = (clsUser)Session["user"];

            SRVSecurePaper srv = new SRVSecurePaper();

            status = srv.SaveDownloadDetails(gvPapers.DataKeys[0]["ExamDate"].ToString(), gvPapers.DataKeys[0]["ExamStartTime"].ToString(), gvPapers.DataKeys[0]["ExamEndTime"].ToString(), hidVenueID.Value, user.User_ID.ToString(), hidIpAddress.Value, "0");

            if (status > 0)
            {
                string filePath = clsGetSettings.PhysicalSitePath + @"ExamDownloads\SRPD\Download\";

                string examDateTime = hidExamDateTime.Value;

                examDateTime = examDateTime.Replace("/", "-");
                examDateTime = examDateTime.Replace(":", "");

                filePath = filePath + "University" + @"/" + examDateTime;

                string fileName = gvPapers.DataKeys[0]["FileName"].ToString();

                filePath = filePath + @"/" + fileName;

                if (File.Exists(filePath + ".zip"))
                {
                    Response.AddHeader("Content-Type", "application/octet-stream");
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + fileName + ".zip");   //strCrName.ToString().Trim()  size=" + b.Length.ToString()); //strCrName.ToString().Trim()
                    Response.TransmitFile(filePath + ".zip");
                    Response.End();
                }
                else
                {
                    lblDownloadMsg.Text = "File does not exists";
                    lblDownloadMsg.CssClass = "errorNote";
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('Data cannot be downloaded')", true);
            }

        }
        #endregion

        #region Grid View Related Events

        #region gvPapers_RowDataBound

        protected void gvPapers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView gv = (GridView)sender;
            if ((e.Row.RowType != DataControlRowType.Header) && (e.Row.RowType != DataControlRowType.Footer) && (e.Row.RowType != DataControlRowType.Pager))
            {
                Label lblStatus = (Label)e.Row.FindControl("lblDownloadStatus");

                if (gv.DataKeys[e.Row.RowIndex]["DownloadStatus"].ToString() == "1")
                {
                    lblStatus.Text = "Downloaded";
                }
                else
                {
                    lblStatus.Text = "Not Downloaded";
                }
            }
        }
        #endregion

        #endregion

        #endregion

        #region Other Functions

        #region FillGrid
        public void FillGrid()
        {
            dt = null;
            SRVSecurePaper srv = new SRVSecurePaper();

            dt = srv.ListPaperTlmAmAtForDownload(hidVenueID.Value);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] drArr = null;

                drArr = dt.Select("ExamDateTime ='" + hidExamDateTime.Value.ToString() + "'");

                DataTable dtPapers = new DataTable();

                if (drArr.Length > 0)
                {
                    dtPapers.Columns.Add("pk_Inst_ID");
                    dtPapers.Columns.Add("FileName");
                    dtPapers.Columns.Add("Paper");
                    dtPapers.Columns.Add("ExamDateTime");
                    dtPapers.Columns.Add("ExamStartTime");
                    dtPapers.Columns.Add("ExamDate");
                    dtPapers.Columns.Add("ExamEndTime");
                    dtPapers.Columns.Add("DownloadStatus");

                    foreach (DataRow row in drArr)
                    {
                        dtPapers.ImportRow(row);
                    }
                    dtPapers.AcceptChanges();

                    if (dtPapers.Rows.Count > 0)
                    {
                        gvPapers.DataSource = dtPapers;
                        gvPapers.DataBind();

                        divDownload.Style.Add("display", "block");
                        validateTime();
                    }
                    else
                    {
                        lblMsg.Text = "No Data available for download";
                        lblMsg.CssClass = "errorNote";
                    }
                }
            }
            else
            {
                lblMsg.Text = "No Data available for download";
                lblMsg.CssClass = "errorNote";
            }
        }
        #endregion

        #region SetHiddenVariables
        void SetHiddenVariables()
        {
            try
            {
                ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");

                hidEventID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidEventID")).Value;
                hidExamDate.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidExamDate")).Value;
                hidExamDateTime.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidExamDateTime")).Value;
                hidExamStartTime.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidExamStartTime")).Value;
                hidExamEndTime.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidExamEndTime")).Value;
                hidZipFilePwd.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidZipFilePwd")).Value;

                hidVenueID.Value = "-1";

                lblGrvMsg.Text = "For Exam Time Slot - " + hidExamDateTime.Value;
                lblGrvMsg.Font.Bold = true;

                //lblSubHeader.Text = "for " + hidVenueCode.Value + " - " + hidVenueName.Value;

                //DUConfigurations.clsSRPDDownloadTime time = DUConfigurations.clsDUConfigurations.Instance.SRPDownloadTime;
                int timeSpan = 120;//time.DownloadTime;

                string str = "";

                //if (timeSpan > 12)
                //{
                //    int rem = (timeSpan % 60);

                //    int tmSpan = timeSpan / 60;

                //    if (rem > 0)
                //    {
                //        str = tmSpan.ToString() + ":" + rem.ToString();
                //    }
                //    else
                //    {
                //        str = tmSpan.ToString();
                //    }
                //}
                //else
                //{
                    str = timeSpan.ToString();
               // }


                //Commented by yojana for 23 Aug 2019
                //lblMesg.Text = "*Note: Question Paper download will be available only " + str + " hour before 'Exam Start Time' and 30 minutes after 'Exam Start Time'";

                //Added by yojana for 23 Aug 2019
                lblMesg.Text = "*Note: Question Paper download will be available only " + str + " minutes before 'Exam Start Time' and upto  'Exam End Time'";

                lblMesg.Font.Bold = true;

                #region GetIPAddress

                hidIpAddress.Value = IPNetworking.GetIP4Address();

                #endregion

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        #endregion

        #region Send SMS function
        public string SendSMS(string sMobile_No, string Name, string sOTP, string sMsg, string templateID)
        {
            string res = "-1";
            sMobile_No = "91" + sMobile_No.Trim();
            try
            {
                Sancharak.SendSMS obj = new Sancharak.SendSMS();
                obj.epMessage = "Dear " + Name + sMsg + sOTP + ". via MKCL";
                //res = obj.SendPersonalizedSMS(sMobile_No, "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"));

                //res = obj.SendPersonalizedSMSForOTPTempId(sMobile_No, "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"), templateID);
                res = obj.SendPersonalizedSMSForTempId(sMobile_No, "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"), templateID);

            }
            catch
            { }
            return res;
        }
        #endregion

        #region CreateXML
        public string CreateXML()
        {

            StringBuilder strCourse = new StringBuilder();
            XmlDocument xml = new XmlDocument();
            XmlNode root = xml.CreateNode(XmlNodeType.Element, "Root", "");
            XmlNode childNode = null;
            string xmldata = "";
            xmldata = "<Root>";

            for (int i = 0; i < gvPapers.Rows.Count; i++)
            {
                childNode = xml.CreateNode(XmlNodeType.Element, "Papers", "");
                root.AppendChild(childNode);
                xml.AppendChild(root);
            }
            xmldata += "</Root>";

            return xml.OuterXml;
        }
        #endregion

        #region validateTime
        public void validateTime()
        {
            DateTime ExamDateTime = DateTime.Parse(hidExamDate.Value);
            //check exam data with current date
            if (ExamDateTime.Date == DateTime.Now.Date)
            {
                if (hidExamStartTime.Value.EndsWith(":AM") || hidExamStartTime.Value.EndsWith(":PM"))
                {
                    hidExamStartTime.Value = hidExamStartTime.Value.Remove(hidExamStartTime.Value.Length - 3);
                    hidExamStartTime.Value = hidExamStartTime.Value.Trim();
                }

                if (hidExamStartTime.Value.Length > 5)
                {
                    hidExamStartTime.Value = hidExamStartTime.Value.Remove(hidExamStartTime.Value.Length - 3);
                }

                DateTime dateStartDate = DateTime.ParseExact(hidExamStartTime.Value, "HH:mm", new DateTimeFormatInfo());

                if (hidExamEndTime.Value.EndsWith(":AM") || hidExamEndTime.Value.EndsWith(":PM"))
                {
                    hidExamEndTime.Value = hidExamEndTime.Value.Remove(hidExamEndTime.Value.Length - 3);
                    hidExamEndTime.Value = hidExamEndTime.Value.Trim();
                }

                if (hidExamEndTime.Value.Length > 5)
                {
                    hidExamEndTime.Value = hidExamEndTime.Value.Remove(hidExamEndTime.Value.Length - 3);
                }

                DateTime dateEndDate = DateTime.ParseExact(hidExamEndTime.Value, "HH:mm", new DateTimeFormatInfo());

                // string strTime = DateTime.Now.Hour.ToString()+ ":" + DateTime.Now.Minute.ToString();
                DateTime dateCurrentDate = DateTime.ParseExact(DateTime.Now.ToString("HH:mm"), "HH:mm", new DateTimeFormatInfo());

                // DUConfigurations.clsSRPDDownloadTime time = DUConfigurations.clsDUConfigurations.Instance.DownloadTime; 
               // DUConfigurations.clsSRPDDownloadTime time = DUConfigurations.clsDUConfigurations.Instance.SRPDownloadTime;

                int timeSpan = 120;//time.DownloadTime;
                //if (timeSpan <= 12)
                //{
                //    timeSpan = timeSpan * 60;
                //}



                string startHour = dateStartDate.ToString("HH");
                string Startmin = dateStartDate.ToString("mm");
                string currentHour = dateCurrentDate.ToString("HH");
                string currentMin = dateCurrentDate.ToString("mm");
                string endHour = dateEndDate.ToString("HH");
                string endMin = dateEndDate.ToString("mm");

                string timeToValidate = timeSpan.ToString();

                int StartTime = (Convert.ToInt32(startHour) * 60) + Convert.ToInt32(Startmin);
                int CurrentTime = (Convert.ToInt32(currentHour) * 60) + Convert.ToInt32(currentMin);
                int endTime = (Convert.ToInt32(endHour) * 60) + Convert.ToInt32(endMin);



                //Allow download before exam start time for specified hours and upto endtime
                if ((StartTime - CurrentTime) <= Convert.ToInt32(timeToValidate))
                {
                    btnDownload.Enabled = true;
                }
                else
                {
                    btnDownload.Enabled = false;
                }


                // if (CurrentTime >= StartTime + 30)//Commented by yojana on 23 Aug 2019 for #173166
                if (CurrentTime >= endTime) //Added by yojana for #173166
                {
                    btnDownload.Enabled = false;
                }
            }
            else
            {
                btnDownload.Enabled = false;
            }

        }
        #endregion

        #endregion
    }
}