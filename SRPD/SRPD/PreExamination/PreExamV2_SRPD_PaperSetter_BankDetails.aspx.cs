using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PreExamClsLib.Services;
using System.Data;
using System.Drawing;
using Classes;

namespace SRPD.PreExamination
{

    public partial class PreExamV2_SRPD_PaperSetter_BankDetails : System.Web.UI.Page
    {
        #region Variables
        
        SRVSrpd svr = new SRVSrpd();
        string Inst_ID = "-1";
        string PaperSetterID = "51";
        private DataTable dt = new DataTable("Form Entry");

        public string Country
        {
            get
            {
                return ddlCountry.SelectedValue.ToString();
            }
        }

        public string State
        {
            get
            {
                return ddlState.SelectedValue.ToString();
            }
        }

        public string District
        {
            get
            {
                return ddlDistrict.SelectedValue.ToString();
            }
        }

        public string Tehsil
        {
            get
            {
                return ddlTehsil.SelectedValue.ToString();
            }
        }

        public string OtherTehsil
        {
            get
            {
                return txtOTehsil.Text.ToString();
            }
        }

        public string City
        {
            get
            {
                return txtCity.Text.ToString();
            }
        }

        public string RAddress
        {
            get
            {
                return txtRAddress.Text.ToString();
            }
        }

        public string PinCode
        {
            get
            {
                return txtPinCode.Text.ToString();
            }
        }

        public string WhatsappNumber
        {
            get
            {
                return txtWhatsappNumber.Text.ToString();
            }
        }

        public string IFSCCode
        {
            get
            {
                return txtIFSCcode.Text.ToString();
            }
        }

        public string BankAccountNumber
        {
            get
            {
                return txtBankaccnum.Text.ToString();
            }
        }

        public string AccountHolderName
        {
            get
            {
                return txtAccountholder.Text.ToString();
            }
        }

        public string BankName
        {
            get
            {
                return txtBankname.Text.ToString();
            }
        }

        public string BranchName
        {
            get
            {
                return txtBranchname.Text.ToString();
            }
        }

        public string MICR
        {
            get
            {
                return txtMICR.Text.ToString();
            }
        }

        public string Address
        {
            get
            {
                return txtAddress.Text.ToString();
            }
        }
        
        #endregion

        #region PageLoad
        
        protected void Page_Load(object sender, EventArgs e)
        {
            dt.Columns.Add("FirstName");
            dt.Columns.Add("MiddleName");
            dt.Columns.Add("LastName");
            dt.Columns.Add("Country");
            dt.Columns.Add("State");
            dt.Columns.Add("District");
            dt.Columns.Add("Tehsil");
            dt.Columns.Add("OtherTehsil");
            dt.Columns.Add("City");
            dt.Columns.Add("ResidentialAddress");
            dt.Columns.Add("PIN");
            dt.Columns.Add("MobileNumber");
            dt.Columns.Add("E-MailID");
            dt.Columns.Add("WhatsappNumber");
            if (!IsPostBack)
            {
                divPersonalDetails.Style.Add("display", "block");
                divBankDetails.Style.Add("display", "none");
                //BindGridview();
                fillCountry();
            }
            //divPersonalDetails.Style.Add("display", "block");
            //divBankDetails.Style.Add("display", "none");
        }

        #endregion
        
        #region Links for Obtaining Details

        protected void lnkPersonalDetails_Click(object sender, EventArgs e)
        {
            divPersonalDetails.Style.Add("display","block");
            divBankDetails.Style.Add("display","none");

            InitializePersonalDetails();
            lblMsg.Text = "";
            divGridView.Style.Add("display","none");
        }

        protected void lnkBankDetails_Click(object sender, EventArgs e)
        {
            divPersonalDetails.Style.Add("display","none");
            divBankDetails.Style.Add("display","block");
            divGridView.Style.Add("display","block");

            InitializeBankDetails();
            lblMsgIFSCCode.Text = "";

        }
        
        #endregion

        #region Button Functions

        protected void btnSavePersonalDetails_Click(object sender, EventArgs e)
        {
            DataTable dt = svr.UpdatePaperSetterPersonalDetails(Inst_ID,PaperSetterID, Country,State,District,Tehsil,OtherTehsil,City,RAddress,PinCode,WhatsappNumber);
            string InstID = clsGetSettings.InstituteID.Trim();
            if (dt != null && dt.Rows.Count > 0)
            {
                string STATUSMESSAGE = dt.Rows[0]["MSG"].ToString();
                lblMsg.Text = STATUSMESSAGE;
                lblMsg.Visible = true;
                if (STATUSMESSAGE == "Data Saved Successfully.")
                {
                    //lblMsg.Text = "Data Saved Successfully.";
                    lblMsg.ForeColor = Color.Green;
                    //lblMsg.Visible = true;
                }
                else
                {
                    //lblMsg.Text = "Data Not found.";
                    lblMsg.ForeColor = Color.Red;
                }
            }
            BindGridview();

            InitializePersonalDetails();
        }

        protected void btnSaveBankDetails_Click(object sender, EventArgs e)
        {
            string UniID = clsGetSettings.UniversityID.Trim();
            DataTable dt = svr.InsertPaperSetterBankDetails(PaperSetterID, IFSCCode, BankAccountNumber, AccountHolderName, BankName, BranchName, MICR, ((clsUser)Session["User"]).User_ID);

            if (dt != null && dt.Rows.Count > 0)
            {
                string STATUSMESSAGE = dt.Rows[0]["MSG"].ToString();
                lblMsg.Text = STATUSMESSAGE;
                lblMsg.Visible = true;
                if (STATUSMESSAGE == "Data Saved Successfully.")
                {
                    //lblMsg.Text = "Data Saved Successfully.";
                    lblMsg.ForeColor = Color.Green;
                   // lblMsg.Visible = true;
                }
                else
                {
                    //lblMsg.Text = "Data Already Found. Enter Different details.";
                    lblMsg.ForeColor = Color.Red;
                }
            }
            BindGridview();

            InitializeBankDetails();
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string IFSC = IFSCCode.ToUpper();
            DataTable dt = svr.GetPaperSetterBankDetailsFromIFSCCode(IFSC);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    txtBankname.Text = dr["pk_Bank_ID"].ToString();
                    txtBranchname.Text = dr["BranchName"].ToString();
                    txtMICR.Text = dr["MICR"].ToString();
                    txtAddress.Text = dr["Address"].ToString();
                }
            }
            else
            {
                lblMsgIFSCCode.Text = "IFSC Code not found.Please enter another IFSC code.Contact Admin for the approval of your IFSC code";
                lblMsgIFSCCode.ForeColor = Color.Red;
            }


        }

        #endregion

        #region IndexChange Functions for Drop Downs
        
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillState();
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillTahsil(District);
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillDistrict();
        }

        #endregion

        #region Other Functions

        private void BindGridview()
        {
            DataTable dtget = svr.GetPaperSetterRegistrationByPSID(Inst_ID, PaperSetterID);
            gvBankPending.DataSource = dtget;
            gvBankPending.DataBind();
            divGridView.Style.Add("display","block");
            //divPersonalDetails.Style.Add("display", "block");
        }

        public void fillCountry()
        {
            DataTable dt = svr.ListCountry();
            ddlCountry.Items.Clear();
            ddlCountry.Items.Add(new ListItem("--- Select ---", "-1"));

            foreach (DataRow row in dt.Rows)
            {
                ddlCountry.Items.Add(new ListItem(row["Text"].ToString(), row["Value"].ToString()));
            }
        }

        public void fillState()
        {
            DataTable dt = svr.ListState();
            ddlState.Items.Clear();
            ddlState.Items.Add(new ListItem("--- Select State ---","-1"));

            foreach (DataRow row in dt.Rows)
            {
                ddlState.Items.Add(new ListItem(row["State_Name"].ToString(), row["State_ID"].ToString()));
            }
        }

        public void fillDistrict()
        {
            clsDistrict district = new clsDistrict();
            DataTable dtDistrict = district.StateWiseDistricts(ddlState.SelectedValue, "E");
            ListItem Li = new ListItem("--Select--", "0");
            DataRow drDistrict = dtDistrict.NewRow();
            drDistrict["text"] = "---Select---";
            drDistrict["value"] = "0";            
            dtDistrict.Rows.InsertAt(drDistrict, 0);


            ddlDistrict.DataTextField = "text";
            ddlDistrict.DataValueField = "value";
            ddlDistrict.DataSource = dtDistrict;
            ddlDistrict.DataBind();
        }

        public void fillTahsil(string districtID)
        {
            clsTaluka taluka = new clsTaluka();
            DataTable dtTahsil = taluka.DisplayTalukaWithinDistrict(districtID, "E");
            DataRow drTahsil = dtTahsil.NewRow();
            drTahsil["text"] = "---Select---";
            drTahsil["value"] = "0";            
            dtTahsil.Rows.InsertAt(drTahsil, 0);


            ddlTehsil.DataTextField = "text";
            ddlTehsil.DataValueField = "value";
            ddlTehsil.DataSource = dtTahsil;
            ddlTehsil.DataBind();
        }

        public void InitializePersonalDetails()
        {
            ddlCountry.Items.Clear();
            ddlCountry.Items.Add(new ListItem("--- Select ---", "-1"));

            ddlDistrict.Items.Clear();
            ddlDistrict.SelectedValue = "-1";

            ddlState.Items.Clear();
            ddlState.SelectedValue = "-1";

            ddlTehsil.Items.Clear();
            ddlTehsil.SelectedValue = "-1";


            txtOTehsil.Text = "";
            txtCity.Text = "";
            txtRAddress.Text = "";
            txtPinCode.Text = "";
            txtWhatsappNumber.Text = "";
        }

        public void InitializeBankDetails()
        {
            txtIFSCcode.Text = "";
            txtBankaccnum.Text = "";
            txtAccountholder.Text = "";
            txtBankname.Text = "";
            txtBranchname.Text = "";
            txtMICR.Text = "";
            txtAddress.Text = "";
        }

        #endregion

    }
}