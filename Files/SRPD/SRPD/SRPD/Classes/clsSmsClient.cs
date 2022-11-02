using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sancharak;


namespace Classes
{
    public class clsSmsClient
    {
        private string sNotificationMessage = string.Empty;
        private string sUsername = string.Empty;//User Person getting sms
        private string sMobileNo = string.Empty;
        private string sPassword = string.Empty; // Password for Network Credential       
        private string sUsrNameNetwork = string.Empty;// UserName for NetWork Credentials
        private string sSMSFrom = string.Empty;

        public string SMSFrom
        {
            get { return sSMSFrom; }
            set { sSMSFrom = value; }
        }

        public string Password
        {
            get { return sPassword; }
            set { sPassword = value; }
        }
        public string UsrNameNetwork
        {
            get { return sUsrNameNetwork; }
            set { sUsrNameNetwork = value; }
        }
        public string MobileNo
        {
            get { return sMobileNo; }
            set { sMobileNo = value; }
        }

        public string Username
        {
            get { return sUsername; }
            set { sUsername = value; }
        }
        public string Body
        {
            get { return sNotificationMessage; }
            set { sNotificationMessage = value; }
        }

        #region Constructor

        ///<summary>
        /// Constructor
        ///</summary>
        public clsSmsClient()
        {
           
        }



        #endregion

        public string SendSMS(string sMobileNo, string sMessageText, string sUser, string sender, string appCode)
        {
            string sReturn = string.Empty;
            try
            {
                SendSMS objSendSMS = new SendSMS();
                objSendSMS.epMessage = sMessageText;
                objSendSMS.epUser = sUser;
                sReturn = objSendSMS.SendPersonalizedSMS(sMobileNo, appCode);
            }
            catch { throw; }
            return sReturn;
        }

    }
}