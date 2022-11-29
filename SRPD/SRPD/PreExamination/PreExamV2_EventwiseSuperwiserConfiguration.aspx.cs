using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using Classes;
using PreExamClstLib.Classes;
using Sancharak;
using PreExamClstLib.Services;
using System.Text;
using System.Text.RegularExpressions;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_EventwiseSuperwiserConfiguration : System.Web.UI.Page
    {
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                fillExamEvent();
        }
        #endregion

        #region Events

        #region btnProceed_Click
        protected void btnProceed_Click(object sender, EventArgs e)
        {
            clear();
            tabHeader2.Attributes.Add("style", "display:inline");
            divBtnNotConfiguration.Attributes.Add("style", "display:inline");
            divbtnConfiguration.Style.Add("display", "none");
            divEvent.Attributes.Add("style", "display:none");           
            NotSupervisorList();
        }
        #endregion

        #region btnSave_Click Not Configured Supervisor List
        protected void btnSave_Click(object sender, EventArgs e)
        {
            clear();
            SRVExamEvent srvExamEvent = new SRVExamEvent();
            SRVSecurePaper srv = new SRVSecurePaper();
            int result = 0;
            clsUser user = (clsUser)Session["user"];

            string sXML = PrepareInstListXML().ToString();

            result = srvExamEvent.SaveSupervisorConfiguration(hidExamEventID.Value, sXML, user.User_ID.ToString());

            if (result > 0)
            {
                lblSuccess.Text = "Configuration saved successfully";
                divSuccess.Style.Add("display", "block");
                //string sSupervvisorName = "";
                string sUniName = clsGetSettings.UniversityName;

                DataTable oSMSMail = new DataTable();
                DataSet dtNotification = new DataSet();

                oSMSMail = srvExamEvent.Get_SMS_MAIL_Details(sXML, hidExamEventID.Value);

                //Get mail notification from db
                dtNotification = srv.GetMailNotification_Supervisor_Confirmation();

                for (int i = 0; i < oSMSMail.Rows.Count; i++)
                {
                    //string msg = @"Resp.Sir/Madam " + oSMSMail.Rows[i]["Name"] + " Your Registration for Downloading Question Papers is approved by University.Regards Examination Section " + sUniName;
                    //Added by JatinD BR#119854

                    string msg = dtNotification.Tables[1].Rows[0]["NotificationMessage"].ToString();
                    msg = Regex.Replace(msg, "\\<\\<UserName\\>\\>", oSMSMail.Rows[i]["Name"].ToString());
                    msg = Regex.Replace(msg, "\\<\\<ExamEvent\\>\\>", hidExamEventName.Value);
                    msg = Regex.Replace(msg, "\\<\\<UniversityName\\>\\>", sUniName+ ".");

                    string res = SendSMS(oSMSMail.Rows[i]["Mobile_Number"].ToString(), oSMSMail.Rows[i]["Name"].ToString(), msg);

                    //sending mail using sendgrid
                    if (dtNotification.Tables[0] != null)
                        SendMail(oSMSMail.Rows[i]["Email_ID"].ToString(), oSMSMail.Rows[i]["Name"].ToString(), dtNotification.Tables[0]);
                }

                fillGrid();
            }
            else
            {
                lblFailure.Text = "Configuration cannot be saved";
                divFailure.Style.Add("display", "block"); 
            }
        }
        #endregion

        #region Enabled Configured Supervisor List
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            clear();
            SRVExamEvent srvExamEvent = new SRVExamEvent();
            SRVSecurePaper srv = new SRVSecurePaper();
            int result = 0;
            clsUser user = (clsUser)Session["user"];

            string sXML = PrepareConfiguredInstListXML().ToString();

            result = srvExamEvent.UpadteSupervisorConfiguration(hidExamEventID.Value, sXML, user.User_ID.ToString());

            if (result > 0)
            {
                lblSuccess.Text = "Configuration Updated successfully";
                divSuccess.Attributes.Add("style", "display:block");

                string sUniName = clsGetSettings.UniversityName;
                FillConfiguredGrid();
            }
            else
            {
                lblFailure.Text = "Configuration cannot be saved";
                divFailure.Style.Add("display", "block");  
            }
        }
        #endregion

        #region Send SMS function
        public string SendSMS(string sMobile_No, string Name, string sMsg)
        {
            string res = "-1";
            sMobile_No = "91" + sMobile_No.Trim();

            clsUser user = (clsUser)Session["user"];

            try
            {
                Sancharak.SendSMS obj = new Sancharak.SendSMS();
                obj.epMessage = sMsg;
                obj.epUser = user.User_ID.ToString();
                res = obj.SendPersonalizedSMS(sMobile_No, "SRPD" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"));
            }
            catch (Exception)
            { }

            return res;
        }
        #endregion

        #region SendMail
        public void SendMail(string sEmail_ID, string Name, DataTable dtMailNotification)
        {

            try
            {
                clsEmailClient oEmailClient = new clsEmailClient();
                string sNotificationMessage = dtMailNotification.Rows[0]["NotificationMessage"].ToString();
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[First_Name\\]\\]", Name);
                //sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[OTP\\]\\]", OTP);


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

        //#region btnViewConfigured_Click
        //protected void btnViewConfigured_Click(object sender, EventArgs e)
        //{
        //    FillConfiguredGrid();
        //}
        //#endregion

        #region GrvConfigured_PageIndexChanging
        protected void GrvConfigured_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrvConfigured.PageIndex = e.NewPageIndex;
            FillConfiguredGrid();
        }

        #endregion

        #region Not Configured Supervisor List Click
        protected void lnkNotConfiguredSupervisorList_Click(object sender, EventArgs e)
        {
            clear();
            txtSearch.Text = "";
            NotSupervisorList();
            li2.Attributes.Remove("class");
            li2.Attributes.Add("class", "tab");
            li1.Attributes.Remove("class");
            li1.Attributes.Add("class", "tab active");
        }
        #endregion

        #region Configured Supervisor List Click
        protected void lnkConfiguredSupervisorList_Click(object sender, EventArgs e)
        {
            clear();
            txtSearchConfigure.Text = "";
            FillConfiguredGrid();
            li1.Attributes.Remove("class");
            li1.Attributes.Add("class", "tab");
            li2.Attributes.Remove("class");
            li2.Attributes.Add("class", "tab active");
        }
        #endregion

        //#region grvListSupervisor_PageIndexChanging

        //protected void grvListSupervisor_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    grvListSupervisor.PageIndex = e.NewPageIndex;
        //    fillGrid();
        //}
        //#endregion

        #region back
        protected void btnBack_Click(object sender, EventArgs e)
        {
            clear();
            divEvent.Style.Add("display", "block");
            tabHeader2.Attributes.Add("style", "display:none");
            divBtnNotConfiguration.Style.Add("display", "none");
            divbtnConfiguration.Style.Add("display", "none");
        }
        #endregion


        #region Not Configured Supervisor List Search
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            clear();

            hidExamEventID.Value = ddlExamEvent.SelectedValue.ToString();
            hidExamEventName.Value = ddlExamEvent.SelectedItem.Text.ToString();
            fillGridSearch();
            lblSubHeader.Text = "for [" + ddlExamEvent.SelectedItem.Text + "]";
        }
        #endregion

        #region Configured Supervisor List Search
        protected void btnSearchConfigure_Click(object sender, EventArgs e)
        {
            clear();

            hidExamEventID.Value = ddlExamEvent.SelectedValue.ToString();
            hidExamEventName.Value = ddlExamEvent.SelectedItem.Text.ToString();
            FillConfiguredGridSearch();
            lblSubHeader.Text = "for [" + ddlExamEvent.SelectedItem.Text + "]";
        }
        #endregion
        #endregion

        #region Other Functions


        #region clear Success and Failure
        private void clear()
        {
            lblSuccess.Text = "";           
            divSuccess.Style.Add("display", "none");
            lblFailure.Text = "";
            divFailure.Style.Add("display", "none");         
        }
        #endregion
        #region fillExamEvent
        private void fillExamEvent()
        {
            SRVExamEvent srvExamEvent = new SRVExamEvent();
            ddlExamEvent.DataSource = srvExamEvent.ListExamEvent();
            ddlExamEvent.DataTextField = "ExamEvent";
            ddlExamEvent.DataValueField = "ExamEventID";
            ddlExamEvent.DataBind();
        }
        #endregion

        #region PrepareXML Not Configured Supervisor List
        private StringBuilder PrepareInstListXML()
        {
            StringBuilder strb = new StringBuilder("<Root>");

            for (int i = 0; i < grvListSupervisor.Rows.Count; i++)
            {
                if (((CheckBox)grvListSupervisor.Rows[i].FindControl("chkSelect")).Checked)
                {
                    strb.Append(@"<Inst pk_Inst_ID='" + grvListSupervisor.DataKeys[i]["pk_Inst_ID"].ToString() + "' pk_Supervisor_ID='" + grvListSupervisor.DataKeys[i]["pk_Supervisor_ID"].ToString() + "' />");
                }
            }
            return strb.Append("</Root>");
        }
        #endregion

        #region PrepareXML Configured Supervisor List
        private StringBuilder PrepareConfiguredInstListXML()
        {
            StringBuilder strb = new StringBuilder("<Root>");

            for (int i = 0; i < GrvConfigured.Rows.Count; i++)
            {
                if (((CheckBox)GrvConfigured.Rows[i].FindControl("chkSelect")).Checked)
                {
                    strb.Append(@"<Inst pk_Inst_ID='" + GrvConfigured.DataKeys[i]["pk_Inst_ID"].ToString() + "' pk_Supervisor_ID='" + GrvConfigured.DataKeys[i]["pk_Supervisor_ID"].ToString() + "' IsDisabled='0' />");
                }
                else
                {
                    strb.Append(@"<Inst pk_Inst_ID='" + GrvConfigured.DataKeys[i]["pk_Inst_ID"].ToString() + "' pk_Supervisor_ID='" + GrvConfigured.DataKeys[i]["pk_Supervisor_ID"].ToString() + "' IsDisabled='1' />");
                }
            }
            return strb.Append("</Root>");
        }
        #endregion

        #region fillGrid
        public void fillGrid()
        {
            SRVExamEvent srvExamEvent = new SRVExamEvent();
            DataTable dt = srvExamEvent.ListSupervisorDetails(hidExamEventID.Value);
            if (dt != null && dt.Rows.Count > 0)
            {
                grvListSupervisor.DataSource = dt;
                grvListSupervisor.DataBind();
                //lblGrvMsg.Text = "";
                btnSave.Enabled = true;
                btnSave1.Enabled = true;
                divBtnNotConfiguration.Style.Add("display", "block");
                divbtnConfiguration.Style.Add("display", "none");
            }
            else
            {
                grvListSupervisor.DataSource = null;
                grvListSupervisor.DataBind();
                lblFailure.Text = "No Data found for configuration";               
                divFailure.Style.Add("display", "block");
                btnSave.Enabled = false;
                btnSave1.Enabled = false;
                divBtnNotConfiguration.Style.Add("display", "none");
                divbtnConfiguration.Style.Add("display", "none");
            }
        }
        #endregion
        #region not configured search
        public void fillGridSearch()
        {
            SRVExamEvent srvExamEvent = new SRVExamEvent();
            DataTable dt = srvExamEvent.ListSupervisorDetailsSearch(hidExamEventID.Value, txtSearch.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                grvListSupervisor.DataSource = dt;
                grvListSupervisor.DataBind();
                //lblGrvMsg.Text = "";
                btnSave.Enabled = true;
                btnSave1.Enabled = true;
                divBtnNotConfiguration.Style.Add("display", "block");
                divbtnConfiguration.Style.Add("display", "none");

            }
            else
            {
                grvListSupervisor.DataSource = null;
                grvListSupervisor.DataBind();
                lblFailure.Text = "No Data found for this " + txtSearch.Text + " searched item. ";
                divFailure.Style.Add("display", "block");               
                btnSave.Enabled = false;
                btnSave1.Enabled = false;
                divBtnNotConfiguration.Style.Add("display", "none");
                divbtnConfiguration.Style.Add("display", "none");
            }
           
        }
        #endregion
        #region Configured Supervisor List
        private void FillConfiguredGrid()
        {
            SRVExamEvent srvExamEvent = new SRVExamEvent();

            DataTable dt = srvExamEvent.ListConfiguredSupervisorDetails(hidExamEventID.Value);

            if (dt != null && dt.Rows.Count > 0)
            {
                GrvConfigured.DataSource = dt;
                GrvConfigured.DataBind();
                //lblGrvConfigured.Text = "";
                
                btnSave2.Enabled = true;
                btnSave3.Enabled = true;
                divbtnConfiguration.Style.Add("display", "block");
                divBtnNotConfiguration.Style.Add("display", "none");
            }
            else
            {
                GrvConfigured.DataSource = null;
                GrvConfigured.DataBind();
              
                btnSave2.Enabled = false;
                btnSave3.Enabled = false;
                lblFailure.Text = "No Data found for configuration";
                divFailure.Style.Add("display", "block");  
                divBtnNotConfiguration.Style.Add("display", "none");
                divbtnConfiguration.Style.Add("display", "none");
            }          

        }


        #endregion

        #region Configured Supervisor List Search
        private void FillConfiguredGridSearch()
        {
            SRVExamEvent srvExamEvent = new SRVExamEvent();

            DataTable dt = srvExamEvent.ListConfiguredSupervisorDetailsSearch(hidExamEventID.Value, txtSearchConfigure.Text);

            if (dt != null && dt.Rows.Count > 0)
            {
                GrvConfigured.DataSource = dt;
                GrvConfigured.DataBind();
                //lblGrvConfigured.Text = "";
                btnSave.Enabled = true;
                divbtnConfiguration.Style.Add("display", "block");
                divBtnNotConfiguration.Style.Add("display", "none");
            }
            else
            {
                GrvConfigured.DataSource = null;
                GrvConfigured.DataBind();
                lblFailure.Text = "No Data found for this " + txtSearchConfigure.Text + " searched item. ";
                divFailure.Style.Add("display", "block");  
                divBtnNotConfiguration.Style.Add("display", "none");
                divbtnConfiguration.Style.Add("display", "none");
            }            
        }
        #endregion
        #region Not Supervisor List

        protected void NotSupervisorList()
        {
            hidExamEventID.Value = ddlExamEvent.SelectedValue.ToString();
            hidExamEventName.Value = ddlExamEvent.SelectedItem.Text.ToString();
            fillGrid();
            lblSubHeader.Text = "for [" + ddlExamEvent.SelectedItem.Text + "]";
        }
        #endregion

        protected void GrvConfigured_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox CheckBox1 = (e.Row.FindControl("chkSelect") as CheckBox);
                if (GrvConfigured.DataKeys[e.Row.RowIndex]["IsDisabled"].ToString() == "0")
                {
                    CheckBox1.Checked = true;
                }
                else
                {
                    CheckBox1.Checked = false;
                }
            }
        }

        #endregion
    }
}