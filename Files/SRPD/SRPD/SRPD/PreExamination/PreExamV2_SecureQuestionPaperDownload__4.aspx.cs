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
using PreExamClstLib.Classes;
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
using System.Text.RegularExpressions;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SecureQuestionPaperDownload__4 : System.Web.UI.Page
    {
        #region Variables
        static DataTable dt = null;
        string[] retrunKeys = new string[1];
        int downloadTime = 0;
        #endregion

        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                SetHiddenVariables();
                FillGrid();

            }
            lblDownloadMsg.Text = "";
        }
        #endregion

        #region Events

        #region btnGetOTP_Click
        protected void btnGetOTP_Click(object sender, EventArgs e)
        {

            SRVSecurePaper srv = new SRVSecurePaper();
            DataTable dtSupervisor = new DataTable();
            DataTable dtNotification = new DataTable();
            dtSupervisor = srv.GetSupervisorDetails(hidEventID.Value, hidVenueID.Value);

            if (ddlSupervisor.SelectedItem.Value == "-1" || ddlSupervisor.SelectedItem.Value == "0")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "key1", "alert('Please select Supervisor to send Password')", true);
                return;
            }

            if (dtSupervisor != null && dtSupervisor.Rows.Count > 0)
            {
                hidSupervisorID.Value = ddlSupervisor.SelectedItem.Value;

                if (hidSupervisorID.Value != "-1")
                {
                    DataRow[] dr = dtSupervisor.Select("Pk_Supervisor_ID = " + hidSupervisorID.Value);

                    retrunKeys = srv.GetOTP(); // retrieve One time password
                    hidOTP.Value = retrunKeys[0].ToString(); // set One time password to hidden variable

                    string msg = ",Your Download Password is ";
                    string res = "";



                    if (dr != null && dr.Length > 0)
                    {

                        /**********************************************/
                        //Get mail notification from db
                        dtNotification = srv.GetMailNotification();

                        //sending mail using sendgrid
                        if (dtNotification != null && dt.Rows.Count > 0)
                            SendMail(dr[0]["Email_ID"].ToString(), dr[0]["First_Name"].ToString(), hidOTP.Value, 1, dtNotification);

                        /**********************************************/

                        res = SendSMS(dr[0]["Mobile_Number"].ToString(),
                               dr[0]["First_Name"].ToString(),
                               hidOTP.Value, msg, "1507162884221423950");

                        divDownload.Style.Add("display", "none");
                        divOTP.Style.Add("display", "none");
                        divSupervisor.Style.Add("display", "none");
                        divVerifyOTP.Style.Add("display", "block");

                        lblNote.Text = "NOTE: Dear " + dr[0]["Name"].ToString() + ", you will receive OTP through SMS and E-mail within 5 minutes." +
                            "Please do not refresh or close the window for next 5 minutes.";
                        lblNote.Font.Bold = true;
                        lblNote.CssClass = "saveNote";


                        lblVerifyMsg.Text = "Dowload Password sent to " + dr[0]["Name"].ToString();
                        lblVerifyMsg.Font.Bold = true;

                        ddlSupervisor.Enabled = false;

                        dtNotification.Dispose();

                    }
                    else
                    {
                        lblDownloadMsg.Text = "No Supervisor data found.";
                        lblDownloadMsg.CssClass = "errorNote";
                    }
                }

                btnVerifyOTP.Attributes.Add("onclick", "return validate(" + btnVerifyOTP.ClientID + "," + txtPassword.ClientID + "," + hidOTP.Value + ");");
            }
            else
            {
                lblDownloadMsg.Text = "No Supervisor data found.";
                lblDownloadMsg.CssClass = "errorNote";
            }
            dtSupervisor.Dispose();
        }
        #endregion

        #region btnVerifyOTP_Click
        protected void btnVerifyOTP_Click(object sender, EventArgs e)
        {

            SRVSecurePaper srv = new SRVSecurePaper();
            DataTable dtSupervisor = new DataTable();
            dtSupervisor = srv.GetSupervisorDetails(hidEventID.Value, hidVenueID.Value);
            DataRow[] dr = dtSupervisor.Select("Pk_Supervisor_ID = " + hidSupervisorID.Value);
            //send zip file password to supervisor after verification
            string zipFilePassword = hidZipFilePwd.Value;
            string msg = ",Your password for zip file is ";

            /**********************************************/

            /**********************************************/
            //Get mail notification from db
            DataTable dtNotification = new DataTable();
            dtNotification = srv.GetMailNotification();

            // sending mail using sendgrid
            if (dtNotification != null && dt.Rows.Count > 0)
                SendMail(dr[0]["Email_ID"].ToString(), dr[0]["First_Name"].ToString(), zipFilePassword, 2, dtNotification);

            /**********************************************/

            string res = SendSMS(dr[0]["Mobile_Number"].ToString(),
                      dr[0]["First_Name"].ToString(),
                      zipFilePassword, msg, "1507162884231202483");

            lblDownloadMsg.Text = "File opening password is sent to " + dr[0]["Name"].ToString(); ;
            lblDownloadMsg.Font.Bold = true;

            divDownload.Style.Add("display", "block");
            divOTP.Style.Add("display", "none");
            divSupervisor.Style.Add("display", "none");
            divVerifyOTP.Style.Add("display", "none");
            dtNotification.Dispose();
            dtSupervisor.Dispose();
        }
        #endregion

        #region btnDownload_Click
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            int status = 0;
            clsUser user = (clsUser)Session["user"];

            SRVSecurePaper srv = new SRVSecurePaper();

            status = srv.SaveDownloadDetails(gvPapers.DataKeys[0]["ExamDate"].ToString(), gvPapers.DataKeys[0]["ExamStartTime"].ToString(), gvPapers.DataKeys[0]["ExamEndTime"].ToString(), hidVenueID.Value, user.User_ID.ToString(), hidIpAddress.Value, hidSupervisorID.Value);

            if (status > 0)
            {
                string filePath = clsGetSettings.PhysicalSitePath + @"ExamDownloads\SRPD\Download\";

                string examDateTime = hidExamDateTime.Value;

                examDateTime = examDateTime.Replace("/", "-");
                examDateTime = examDateTime.Replace(":", "");

                filePath = filePath + hidVenueCode.Value + @"/" + examDateTime;

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

        protected void gvPapers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            if (e.CommandName == "Query")
            {
                hidPpID.Value = gvPapers.DataKeys[id]["pk_Pp_ID"].ToString();
                hidPpCode.Value = gvPapers.DataKeys[id]["Pp_Code"].ToString();
                hidPpName.Value = gvPapers.DataKeys[id]["Pp_Name"].ToString();
                hidTLMAMAT.Value = gvPapers.DataKeys[id]["TLMAMAT"].ToString();
                hidQpCode.Value = gvPapers.DataKeys[id]["QpCode"].ToString();
                hidQuery.Value = gvPapers.DataKeys[id]["PpQuery"].ToString();

                Server.Transfer("PreExamV2_SRPD_QuestionPaper_Query.aspx");
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

            lblSupervisorNote.Text = "";

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow[] drArr = null;

                drArr = dt.Select("ExamDateTime ='" + hidExamDateTime.Value.ToString() + "'");

                DataTable dtPapers = new DataTable();

                if (drArr.Length > 0)
                {
                    //dtPapers.Columns.Add("pk_Uni_ID");
                    //dtPapers.Columns.Add("pk_Fac_ID");
                    //dtPapers.Columns.Add("pk_Cr_ID");
                    //dtPapers.Columns.Add("pk_MoLrn_ID");
                    //dtPapers.Columns.Add("pk_Ptrn_ID");
                    //dtPapers.Columns.Add("pk_Brn_ID");
                    //dtPapers.Columns.Add("pk_CrPr_Details_ID");
                    //dtPapers.Columns.Add("pk_CrPrCh_ID");
                    dtPapers.Columns.Add("pk_ExEv_ID");
                    //dtPapers.Columns.Add("pk_Pp_PpHead_CrPrCh_ID");
                    //dtPapers.Columns.Add("pk_TchLrnMth_ID");
                    //dtPapers.Columns.Add("pk_AssMth_ID");
                    //dtPapers.Columns.Add("pk_AssType_ID");
                    dtPapers.Columns.Add("pk_Inst_ID");
                    dtPapers.Columns.Add("FileName");
                    dtPapers.Columns.Add("pk_Pp_ID");
                    dtPapers.Columns.Add("Pp_Code");
                    dtPapers.Columns.Add("Pp_Name");
                    dtPapers.Columns.Add("TLMAMAT");
                    dtPapers.Columns.Add("Paper");
                    dtPapers.Columns.Add("ExamDateTime");
                    dtPapers.Columns.Add("ExamStartTime");
                    dtPapers.Columns.Add("ExamDate");
                    dtPapers.Columns.Add("ExamEndTime");
                    dtPapers.Columns.Add("DownloadStatus");
                    dtPapers.Columns.Add("QpCode");
                    dtPapers.Columns.Add("PpQuery");

                    foreach (DataRow row in drArr)
                    {
                        dtPapers.ImportRow(row);
                    }
                    dtPapers.AcceptChanges();

                    if (dtPapers.Rows.Count > 0)
                    {
                        gvPapers.DataSource = dtPapers;
                        gvPapers.DataBind();

                        divDownload.Style.Add("display", "none");
                        divOTP.Style.Add("display", "block");
                        divSupervisor.Style.Add("display", "block");
                        divVerifyOTP.Style.Add("display", "none");

                        DataTable dtSupervisor = new DataTable();
                        dtSupervisor = srv.GetSupervisorDetails(hidEventID.Value, hidVenueID.Value);

                        ddlSupervisor.Items.Clear();
                        ddlSupervisor.Items.Add(new ListItem("--- Select ---", "-1"));

                        if (dtSupervisor != null && dtSupervisor.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtSupervisor.Rows)
                            {
                                ddlSupervisor.Items.Add(new ListItem(row["Name"].ToString(), row["Pk_Supervisor_ID"].ToString()));
                            }
                            btnGetOTP.Enabled = true;
                            validateTime();
                        }
                        else
                        {
                            lblSupervisorNote.Text = "No Supervisor data found";
                            lblSupervisorNote.CssClass = "errorNote";
                            btnGetOTP.Enabled = false;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "No Data available for download";
                        lblMsg.CssClass = "errorNote";
                        btnGetOTP.Enabled = false;
                    }

                }
            }
            else
            {
                lblMsg.Text = "No Data available for download";
                lblMsg.CssClass = "errorNote";
                btnGetOTP.Enabled = false;
            }
        }
        #endregion

        #region SetHiddenVariables
        void SetHiddenVariables()
        {
            try
            {
                ContentPlaceHolder contentPlaceHolder = (ContentPlaceHolder)Page.PreviousPage.Master.FindControl("ContentPlaceHolder1");

                hidVenueID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidVenueID")).Value;
                hidVenueName.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidVenueName")).Value;
                hidVenueCode.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidVenueCode")).Value;

                hidEventID.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidEventID")).Value;
                hidExamDate.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidExamDate")).Value;
                hidExamDateTime.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidExamDateTime")).Value;
                hidExamStartTime.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidExamStartTime")).Value;
                hidExamEndTime.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidExamEndTime")).Value;
                hidZipFilePwd.Value = ((HtmlInputHidden)contentPlaceHolder.FindControl("hidZipFilePwd")).Value;

                lblGrvMsg.Text = "For Exam Time Slot - " + hidExamDateTime.Value;
                lblGrvMsg.Font.Bold = true;

                lblSubHeader.Text = "for " + hidVenueCode.Value + " - " + hidVenueName.Value;

               // DUConfigurations.clsSRPDDownloadTime time = DUConfigurations.clsDUConfigurations.Instance.SRPDownloadTime;

                clsImportFromExcel srv = new clsImportFromExcel();
                DataTable dt = new DataTable();
                dt = srv.GetVenueWiseDownloadTime(hidVenueID.Value);

                int timeSpan = 120;

                if (dt != null && dt.Rows.Count > 0)
                    timeSpan = Convert.ToInt32(dt.Rows[0]["DownloadTime"].ToString()); 

                downloadTime = timeSpan;
                
               // int timeSpan = time.DownloadTime;

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
                //}



                lblMesg.Text = "*Note: Question Paper download will be available only " + str + " minutes before 'Exam Start Time' and 30 minutes after 'Exam Start Time'";

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

            clsUser user = (clsUser)Session["user"];

            try
            {
                Sancharak.SendSMS obj = new Sancharak.SendSMS();
                obj.epMessage = "Dear " + Name + sMsg + sOTP + ". via MKCL";
                obj.epUser = user.User_ID.ToString();
                //res = obj.SendPersonalizedSMS(sMobile_No, "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"));

                //res = obj.SendPersonalizedSMSForOTPTempId(sMobile_No, "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"), templateID);
                res = obj.SendPersonalizedSMSForTempId(sMobile_No, "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"), templateID);

            }
            catch (Exception)
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
                //childNode.AppendChild(xml.CreateElement("pk_Fac_ID")).InnerText = gvPapers.DataKeys[i]["pk_Fac_ID"].ToString();
                //childNode.AppendChild(xml.CreateElement("pk_Cr_ID")).InnerText = gvPapers.DataKeys[i]["pk_Cr_ID"].ToString();
                //childNode.AppendChild(xml.CreateElement("pk_MoLrn_ID")).InnerText = gvPapers.DataKeys[i]["pk_MoLrn_ID"].ToString();
                //childNode.AppendChild(xml.CreateElement("pk_Ptrn_ID")).InnerText = gvPapers.DataKeys[i]["pk_Ptrn_ID"].ToString();
                //childNode.AppendChild(xml.CreateElement("pk_Brn_ID")).InnerText = gvPapers.DataKeys[i]["pk_Brn_ID"].ToString();
                //childNode.AppendChild(xml.CreateElement("pk_CrPr_Details_ID")).InnerText = gvPapers.DataKeys[i]["pk_CrPr_Details_ID"].ToString();
                //childNode.AppendChild(xml.CreateElement("pk_CrPrCh_ID")).InnerText = gvPapers.DataKeys[i]["pk_CrPrCh_ID"].ToString();
                //childNode.AppendChild(xml.CreateElement("pk_ExEv_ID")).InnerText = gvPapers.DataKeys[i]["pk_ExEv_ID"].ToString();
                //childNode.AppendChild(xml.CreateElement("pk_Pp_PpHead_CrPrCh_ID")).InnerText = gvPapers.DataKeys[i]["pk_Pp_PpHead_CrPrCh_ID"].ToString();
                //childNode.AppendChild(xml.CreateElement("pk_TchLrnMth_ID")).InnerText = gvPapers.DataKeys[i]["pk_TchLrnMth_ID"].ToString();
                //childNode.AppendChild(xml.CreateElement("pk_AssMth_ID")).InnerText = gvPapers.DataKeys[i]["pk_AssMth_ID"].ToString();
                //childNode.AppendChild(xml.CreateElement("pk_AssType_ID")).InnerText = gvPapers.DataKeys[i]["pk_AssType_ID"].ToString();
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
                //DUConfigurations.clsSRPDDownloadTime time = DUConfigurations.clsDUConfigurations.Instance.SRPDownloadTime;

                //int timeSpan = time.DownloadTime;
                //if (timeSpan <= 12)
                //{
                //    timeSpan = timeSpan * 60;
                //}
                int timeSpan = downloadTime;


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
                    btnGetOTP.Enabled = true;
                }
                else
                {
                    btnDownload.Enabled = false;
                    btnGetOTP.Enabled = false;
                }

                //if (endTime < CurrentTime)
                //{
                //    btnDownload.Enabled = false;
                //    btnGetOTP.Enabled = false;
                //}

                if (CurrentTime >= StartTime + 30)
                {
                    btnDownload.Enabled = false;
                    btnGetOTP.Enabled = false;
                }
            }
            else
            {
                btnDownload.Enabled = false;
                btnGetOTP.Enabled = false;
            }

        }
        #endregion


        #region SendMail
        public void SendMail(string sEmail_ID, string First_Name, string OTP, int flag, DataTable dtMailNotification)
        {

            try
            {
                clsEmailClient oEmailClient = new clsEmailClient();
                string sNotificationMessage = dtMailNotification.Rows[0]["NotificationMessage"].ToString();
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[First_Name\\]\\]", First_Name);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[OTP\\]\\]", OTP);

                if (flag == 1)
                {
                    sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[Downloading\\]\\]", "Downloading");
                    sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[Download\\]\\]", "Download");
                }
                if (flag == 2)
                {
                    sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[Downloading\\]\\]", "Opening");
                    sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[Download\\]\\]", "Open");
                }

                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[Name_of_University\\]\\]", clsGetSettings.Name);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[SitePath\\]\\]", clsGetSettings.SitePath);


                oEmailClient.From = dtMailNotification.Rows[0]["MailFrom"].ToString();
                oEmailClient.Subject = dtMailNotification.Rows[0]["MailSubject"].ToString();
                oEmailClient.Body = sNotificationMessage;
                oEmailClient.IsBodyHtml = true;
                oEmailClient.To = sEmail_ID;                
                oEmailClient.Send(dtMailNotification.Rows[0]["SmtpHost"].ToString(), Convert.ToInt32(dtMailNotification.Rows[0]["SmtpPort"].ToString()));
            }
            catch
            { throw; }

        }
        #endregion



        #endregion
    }
}