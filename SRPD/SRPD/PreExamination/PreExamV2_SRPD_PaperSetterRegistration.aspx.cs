using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Collections;
using System.Data;
//using ServerSideValidations;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;
using PreExamClsLib.Services;
using System.Drawing;
//using SRPD.Classes;

namespace SRPD.PreExamination
{
    public partial class PreExamV2_SRPD_PaperSetterRegistration : System.Web.UI.Page
    {
        #region Variables
        DataSet oDs = new DataSet();
        SRVSrpd svr = new SRVSrpd();
        //string Inst_ID = "-1";
        
        #endregion

        #region Properties

        public string FirstName
        {
            get
            {
                return txtFName.Text.ToString();
            }
        }

        public string MiddleName
        {
            get
            {
                return txtMName.Text.ToString();
            }
        }

        public string LastName
        {
            get
            {
                return txtLName.Text.ToString();
            }
        }

        public string MobileNumber
        {
            get
            {
                return txtMobileNumber.Text.ToString();
            }
        }

        public string EmailId
        {
            get
            {
                return txtEmailid.ToString();
            }
        }

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridview();
            }
        }
        #region BindGridView
        private void BindGridview()
        {
            DataTable dtget = svr.GetPaperSetterRegistration();
            gvRegistration.DataSource = dtget;
            gvRegistration.DataBind();

        }
        #endregion
        #region btnSave_Click GaneswarM on 16 Nov 2022 For #205556
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string mName = string.Empty;
            if (txtMName.Text != null)
                mName = txtMName.Text;

            DataTable dt = svr.InsertPaperSetterRegistration( FirstName, MiddleName, txtLName.Text, txtMobileNumber.Text, txtEmailid.Text, ((clsUser)Session["User"]).User_ID);

            if (dt != null && dt.Rows.Count > 0)
            {
                int STATUSMESSAGE = Convert.ToInt32(dt.Rows[0]["STATUSMSG"].ToString());
                string UserName = dt.Rows[0]["USERNAME"].ToString();
                string pass = dt.Rows[0]["PASS"].ToString();
                //lblMsg.Text = STATUSMESSAGE.ToString();
                if (STATUSMESSAGE == 1)
                {

                    lblMsg.ForeColor = Color.Green;
                    lblMsg.Text = "Paper Setter Registration Successful.";
                    using (oDs = new DataSet())
                    {

                        clsPaperSetter oPaperSetter = new clsPaperSetter();
                        oDs = oPaperSetter.GetPaperSetterRegistrationNotificationDetail();

                        // SendSMS(MobileNo.Text, fname, fname, hid_OTP.Value, oDs.Tables[1]);
                        SendMail(txtEmailid.Text, txtFName.Text, UserName,pass , oDs.Tables[0]);
                        lblMsg.Text = lblMsg.Text + " Login Username and Password Have been Sent on your email ID and Sent SMS on Mobile No.";

                        //Get the username and password from Gen_User table
                        //SendSMS(txtMobileNumber.Text, UserName, pass ,  oDs.Tables[1].Rows[0]["NotificationMessage"].ToString(), "1507160373230929769");

                        /*
                        txtFName.Enabled = false;
                        txtMName.Enabled = false;
                        txtLName.Enabled = false;
                        txtMobileNumber.Enabled = false;
                        txtEmailid.Enabled = false;                        
                         */
                    }
                }

                else if (STATUSMESSAGE == 0)
                {
                    lblMsg.ForeColor = Color.Red;
                    lblMsg.Text = "Paper Setter is already Available.";
                }
                //ClearSelection();

                txtFName.Text = "";
                txtMName.Text = "";
                txtLName.Text = "";
                txtMobileNumber.Text = "";
                txtEmailid.Text = "";

            }
            BindGridview();
        }
        #endregion
        #region SendMail
        /*
         Added By GaneswarM on 21 Nov 2022 for #205556
         */
        public void SendMail(string sEmail_ID, string First_Name, string UserName, string Password, DataTable oDatable)
        {

            try
            {
                //oDT = new DataTable();
                // [[SitePath]]
                //oDT = oDs.Tables[0];
                PreExamClstLib.Classes.clsEmailClient oEmailClient = new PreExamClstLib.Classes.clsEmailClient();
                string sNotificationMessage = oDatable.Rows[0]["NotificationMessage"].ToString();
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[First_Name\\]\\]", First_Name);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[UserName\\]\\]", UserName);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[OTP\\]\\]", Password);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[Name_of_University\\]\\]", clsGetSettings.Name);
                //sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[web_site\\]\\]", clsGetSettings.SitePath);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[SitePath\\]\\]", clsGetSettings.SitePath);

                List<string> cc = new List<string>();
                cc.Add("mishraganeswar@gmail.com");
                //cc.Add("ccDeepu@gmail.com");
                oEmailClient.CC = cc;

                // List<string> bcc= new List<string>();
                // bcc.Add("bccpawank.kumawat@gmail.com");
                // bcc.Add("bccDeepu@gmail.com");
                // oEmailClient.BCC = bcc;

                oEmailClient.From = oDatable.Rows[0]["MailFrom"].ToString();
                oEmailClient.Subject = oDatable.Rows[0]["MailSubject"].ToString();
                oEmailClient.Body = sNotificationMessage;
                oEmailClient.IsBodyHtml = true;
                oEmailClient.To = sEmail_ID;


                oEmailClient.Send(oDatable.Rows[0]["SmtpHost"].ToString(), Convert.ToInt32(oDatable.Rows[0]["SmtpPort"].ToString()));

            }
            catch
            { throw; }

        }
        #endregion


        #region Send SMS function
        /*
        Added By GaneswarM on 21 Nov 2022 for #205556
        */
        public string SendSMS(string sMobile_No, string Name, string sOTP, string sMsg, string templateID)
        {
            string res = "-1";
            sMobile_No = "91" + sMobile_No.Trim();

            clsUser user = (clsUser)Session["user"];

            try
            {
                Sancharak.SendSMS obj = new Sancharak.SendSMS();
                //obj.epMessage = "Dear " + Name + sMsg + sOTP + ". via MKCL";
                obj.epUser = user.User_ID.ToString();



                string sNotificationMessage = sMsg;
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[First_Name\\]\\]", Name);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[UserName\\]\\]", Name);                
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[OTP\\]\\]", sOTP);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[Name_of_University\\]\\]", clsPortalSettings.Instance.Name.ToString().Trim());

                obj.epMessage = sNotificationMessage;
                //res = obj.SendPersonalizedSMSForOTPTempId(sMobile_No, "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"), templateID);
                res = obj.SendPersonalizedSMSForTempId(sMobile_No, "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"), templateID);

            }
            catch (Exception Ex)
            { lblMsg.Text = Ex.Message; }

            return res;
        }
        #endregion
    }
}