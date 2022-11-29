using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Classes;
using System.Collections;
using System.Data;
using ServerSideValidations;
using System.Text.RegularExpressions;
using System.Web.UI.HtmlControls;


namespace SRPD.PreExamination
{
    public partial class PreExamV2_DefineSupervisor : System.Web.UI.Page
    {
        
        #region variable Declaration
        clsCommon oCommon = new clsCommon();
        clsGeneral oGeneral = new clsGeneral();
        DataTable oDT;
        DataTable tempDT;
        Hashtable oHt = new Hashtable();
        clsState oState = new clsState();
        clsDistrict oDistrict = new clsDistrict();
        clsTaluka oTaluka = new clsTaluka();
        clsDesignation oDesignation = new clsDesignation();
        clsSupervisor oSupervisor = new clsSupervisor();
        Validation oValidate;
        DataSet oDs = new DataSet();
        clsCommon common;
        clsUser user;
        string[] retrunKeys = new string[1];
        clsInstitute clsInstitute;

        string fkPriDirHODCountryID = "107";
        ClsotherInstitute institute;
        
        #endregion

        #region Page Load
        protected void Page_Load(object sender, EventArgs e)
        {
            clsUser user = (clsUser)Session["user"];
            if (!IsPostBack)
            {
                //btnDelete.Enabled = false;
                UCSearchInstitute.Visible = false;
                if (user.UserTypeCode == "2")
                {
                    hidInstID.Value = user.UserReferenceID;
                    UCSearchInstitute.Visible = false;
                    MainDiv.Style.Add("display", "block");

                    HtmlInputHidden[] hidden = new HtmlInputHidden[2];
                    hidden[0] = hidInstID;
                    hidden[1] = hidUniID;
                    common = new clsCommon();
                    common.setHiddenVariablesMPC(ref hidden);

                    //Set the home country.


                    DUConfigurations.clsHomeState oHomeState = DUConfigurations.clsDUConfigurations.Instance.HomeState;
                    if (oHomeState != null)
                        hid_homeState.Value = oHomeState.ID.ToString().Trim();
                    FillDropDown();

                    RbRoleLevel.Enabled = false;
                    LastName.Enabled = false;
                    FirstName.Enabled = false;
                    MiddleName.Enabled = false;
                    ddlDesignation.Enabled = false;
                    ddlState.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlTehsil.Enabled = false;
                    OtherTehsil.Enabled = false;
                    Address.Enabled = false;
                    PinCode.Enabled = false;
                    MobileNo.Enabled = false;
                    // EmailID.Enabled = false;
                    City.Enabled = false;
                    txtOTP.Enabled = false;


                    FillGrid();
                    if (RbRoleLevel.SelectedValue == "P")
                    {
                        DisplayInfo();
                    }
              }
                if (user.UserTypeCode != "2")
                {
                    //hidInstID.Value = user.UserReferenceID;
                    UCSearchInstitute.Visible = true;
                    MainDiv.Style.Add("display", "none");

                    HtmlInputHidden[] hidden = new HtmlInputHidden[2];
                    hidden[0] = hidInstID;
                    hidden[1] = hidUniID;
                    common = new clsCommon();
                    common.setHiddenVariablesMPC(ref hidden);

                    //Set the home country.


                    DUConfigurations.clsHomeState oHomeState = DUConfigurations.clsDUConfigurations.Instance.HomeState;
                    if (oHomeState != null)
                        hid_homeState.Value = oHomeState.ID.ToString().Trim();
                    FillDropDown();

                    RbRoleLevel.Enabled = false;
                    LastName.Enabled = false;
                    FirstName.Enabled = false;
                    MiddleName.Enabled = false;
                    ddlDesignation.Enabled = false;
                    ddlState.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlTehsil.Enabled = false;
                    OtherTehsil.Enabled = false;
                    Address.Enabled = false;
                    PinCode.Enabled = false;
                    MobileNo.Enabled = false;
                    // EmailID.Enabled = false;
                    City.Enabled = false;
                    txtOTP.Enabled = false;

                }
               
            }
            UCSearchInstitute.gvData.RowCommand += new GridViewCommandEventHandler(gvData_RowCommand); 
        }

       

         private void DisplayInfo()
        {
            hidUniID.Value = clsGetSettings.UniversityID;
            institute = new ClsotherInstitute(hidUniID.Value.Trim(), hidInstID.Value.Trim());
            if (institute.inst_Code == null)
            {
                clsInstitute = new clsInstitute(hidUniID.Value.Trim(), hidInstID.Value.Trim());
                if (clsInstitute.inst_Code!=null && clsInstitute.inst_Code.Trim() != "&nbsp;" && clsInstitute.inst_Code.Trim() != "")
                {
                    lblSubHeader.Text = "- for (" + clsInstitute.inst_Code.Trim() + ") " + clsInstitute.inst_Name.Trim();
                }
                else
                {
                    lblSubHeader.Text = "- for " + clsInstitute.inst_Name;
                }
            }
            else
            {
                lblSubHeader.Text = "- for " + institute.inst_Name;
            }

     
            // if (institute != null) institute = null;

            // common = new clsCommon();
            DataTable DT = ClsotherInstitute.AttributeInstituteDetails(hidUniID.Value.Trim(), hidInstID.Value.Trim());
            // common.setValuesToControls(Page, DT);
            if (DT.Rows[0]["inst_Principal_Director_HODName"].ToString() != "")
            {
                //MobileNo011.Text = 
                if (DT.Rows[0]["inst_PriDirHOD_MobileNo1"].ToString() != "" && DT.Rows[0]["inst_PriDirHOD_EmailID1"].ToString() != "" && DT.Rows[0]["inst_PriDirHOD_City"].ToString()!="" && DT.Rows[0]["fk_PriDirHOD_Tehsil_ID"]!="")
                {
                    string mob1 = institute.inst_PriDirHOD_MobileNo1;
                    string[] arr = mob1.Trim().Split('-');
                    //CountryID01.Text = arr[0].ToString();
                    // MobileNo011.Text = arr[1].ToString();
                    hid_PrincipleMobileNo.Value = arr[1].ToString();
                    MobileNo.Text = arr[1].ToString();

                }
                else
                {
                    //string CountryId = DT.Rows[0]["fk_PriDirHOD_Country_ID"].ToString();
                    //if (CountryId == "")
                    //{
                    //    CountryID01.Text = "91";
                    //}
                    //else
                    //{
                    //    DataTable countryCode = clsInstitute.countrywiseCountrycode(Convert.ToInt32(CountryId));
                    //    if (countryCode != null && countryCode.Rows.Count > 0)
                    //        CountryID01.Text = countryCode.Rows[0][0].ToString();
                    //    else
                    //        CountryID01.Text = "";
                    //}
                }

                Inst_Principal_Director_HODName.Text = DT.Rows[0]["inst_Principal_Director_HODName"].ToString();
                Inst_PriDirHOD_EmailID1.Text = DT.Rows[0]["inst_PriDirHOD_EmailID1"].ToString(); ;
                Inst_PriDirHOD_EmailID2.Text = DT.Rows[0]["inst_PriDirHOD_EmailID2"].ToString();
                Inst_PriDirHOD_Address.Text = DT.Rows[0]["inst_PriDirHOD_Address"].ToString();
                Inst_PriDirHOD_City.Text = DT.Rows[0]["inst_PriDirHOD_City"].ToString();
                Inst_PriDirHOD_STD1.Text = DT.Rows[0]["inst_PriDirHOD_STD1"].ToString();
                Inst_PriDirHOD_TelNo1.Text = DT.Rows[0]["inst_PriDirHOD_TelNo1"].ToString();
                Inst_PriDirHOD_STD2.Text = DT.Rows[0]["inst_PriDirHOD_STD2"].ToString();
                Inst_PriDirHOD_TelNo2.Text = DT.Rows[0]["inst_PriDirHOD_TelNo2"].ToString();



                fkPriDirHODCountryID = DT.Rows[0]["fk_PriDirHOD_Country_ID"].ToString();

                fk_PriDirHOD_District_ID.Value = DT.Rows[0]["fk_PriDirHOD_District_ID"].ToString();
                fk_PriDirHOD_Tehsil_ID.Value = DT.Rows[0]["fk_PriDirHOD_Tehsil_ID"].ToString();
                fnFillStateDistrictTaluka(DT.Rows[0]["fk_PriDirHOD_State_ID"].ToString(), DT.Rows[0]["fk_PriDirHOD_District_ID"].ToString(), DT.Rows[0]["fk_PriDirHOD_Tehsil_ID"].ToString());
                if (DT.Rows[0]["fk_PriDirHOD_Country_ID"].ToString() == "107" || DT.Rows[0]["fk_PriDirHOD_Country_ID"].ToString() == "")
                {
                    trpstate.Style.Add("display", "");
                    trpdistrict.Style.Add("display", "");
                    trpdistrict.Style.Add("display", "");
                    trptahsil.Style.Add("display", "");
                    trpOthertahsil.Style.Add("display", "");

                }
                else
                {
                    trpstate.Style.Add("display", "none");
                    trpdistrict.Style.Add("display", "none");
                    trpdistrict.Style.Add("display", "none");
                    trptahsil.Style.Add("display", "none");
                    trpOthertahsil.Style.Add("display", "none");

                }
            }
            else
            {
                lblNote.Visible = true;
                lblNote.Text = "Please first fill the Principal/Director/HOD information In other Detail.";
                lblNote.CssClass = "errorNote";
                btnOTP.Enabled = false;
            }
            
            if (common != null) common = null;

        }

        #endregion

        #region fnFillStateDistrictTaluka

        private void fnFillStateDistrictTaluka(string stateID, string districtID, string tehsilID)
        {
            clsCommon common = new clsCommon();
            clsGeneral general = new clsGeneral();
            DataTable dt = general.ListCountry();
            common.fillDropDown(fk_PriDirHOD_Country, dt, fkPriDirHODCountryID, "Text", "Value", "---- Select ----");


            fk_PriDirHOD_State_ID.Items.Clear();
            clsState state = new clsState();
            dt = state.DisplayAllStates("E");
            common.fillDropDown(fk_PriDirHOD_State_ID, dt, stateID, "State_Name", "State_ID", "---- Select ----");
            if (dt != null) dt = null;

            fk_PriDirHOD_District.Items.Clear();
            clsDistrict district = new clsDistrict();
            dt = district.StateWiseDistricts(fk_PriDirHOD_State_ID.SelectedItem.Value, "E");
            common.fillDropDown(fk_PriDirHOD_District, dt, districtID, "Text", "Value", "---- Select ----");
            if (dt != null) dt = null;

            fk_PriDirHOD_Tehsil.Items.Clear();
            clsTaluka taluka = new clsTaluka();
            dt = taluka.DisplayTalukaWithinDistrict(fk_PriDirHOD_District.SelectedItem.Value, "E");
            common.fillDropDown(fk_PriDirHOD_Tehsil, dt, tehsilID, "Text", "Value", "---- Select ----");
            if (tehsilID != "")
                Inst_PriDirHOD_OtherTehsil.Enabled = false;
            if (dt != null) dt = null;

            if (common != null) common = null;

        }
        #endregion
        #region Clear Control

        private void ClearControl()
        {

            // btnDelete.Attributes.Add("style", "cursor:default; color:ButtonShadow;");
            PinCode.Text = "";
            FirstName.Text = "";
            MiddleName.Text = "";
            LastName.Text = "";
            City.Text = "";
            OtherTehsil.Text = "";
            Address.Text = "";
            MobileNo.Text = "";
            EmailID.Text = "";
            ddlDistrict.ClearSelection();
            ddlTehsil.ClearSelection();
            ddlDesignation.ClearSelection();
            txtOTP.Text = "";

        }

        #endregion

        #region Button Event

        #region Button New Click

        protected void btnNew_Click(object sender, EventArgs e)
        {
            RbRoleLevel.SelectedValue = "S";
            btnSave.Text = "Save";
            btnSave.Enabled = true;
            ClearControl();
            // btnDelete.Enabled = false;
            lblNote.Text = "";
            hid_Mode.Value = "";
            hid_Supervisor_ID.Value = "";
            //oSupervisor = new clsSupervisor();
            //retrunKeys = oSupervisor.GetOTP();
            //hid_OTP.Value = retrunKeys[0];

            RbRoleLevel.Enabled = true;
            LastName.Enabled = true;
            FirstName.Enabled = true;
            MiddleName.Enabled = true;
            ddlDesignation.Enabled = true;
            ddlState.Enabled = true;
            ddlDistrict.Enabled = true;
            ddlTehsil.Enabled = true;
            OtherTehsil.Enabled = true;
            Address.Enabled = true;
            PinCode.Enabled = true;
            MobileNo.Enabled = true;
           //EmailID.Enabled = true;
            City.Enabled = true;
            txtOTP.Enabled = false;
            OTPMSG.Text = "";
        }

        #endregion

        #region Button Save
        protected void btnSave_Click(object sender, EventArgs e)
        {
               OTPMSG.Text = "";
            //
            //Call for Server Side Validations
            //
           // if ((((RbRoleLevel.SelectedValue == "P" && hid_emailOTP.Value == txtEmailOTP.Text) || (RbRoleLevel.SelectedValue == "S" && hid_emailOTP.Value == txtEmailotpSRJR.Text)) && hid_OTP.Value == txtOTP.Text.ToUpper().Trim() ) || RbRoleLevel.SelectedValue == "J")

              if (( hid_OTP.Value == txtOTP.Text.ToUpper().Trim()) || RbRoleLevel.SelectedValue == "J")
            {
              

                ServerSideValidations();

                //
                //check whether the Server Side Validation has succeeded.
                //////
                if (oValidate.ValidateMe(lblNote))
                {
                    oHt = CreateHashTable();
                    string Msg = string.Empty;

                    if (hid_Supervisor_ID.Value == "")
                    {
                        oSupervisor = new clsSupervisor();

                        retrunKeys = oSupervisor.AddSupervisor(oHt);

                    }

                    else
                    {
                        oSupervisor = new clsSupervisor();
                        retrunKeys = oSupervisor.UpdateSupervisor(oHt);
                    }
                    //hid_OTP.Value = retrunKeys[1];
                    if (retrunKeys[0] == "Y")
                    {
                        btnSave.Enabled = false;
                        
                        lblNote.Text = "Information Saved succesfully.";
                        lblNote.CssClass = "saveNote";
                        lblNote.Visible = true;
                        txtOTP.Text = "";
                        txtEmailotpSRJR.Text = "";
                        FillGrid();

                        if (RbRoleLevel.SelectedValue == "J")
                            trOTP.Attributes.Add("style", "display:none");
                        else

                        { trOTP.Style.Add("display", ""); ; }
                    }

                    if (retrunKeys[0] == "M")
                    {
                        lblNote.Text = "Mobile No. already exists.";
                        lblNote.CssClass = "errorNote";
                        lblNote.Visible = true;
                        RbRoleLevel.Enabled = true;
                        LastName.Enabled = true;
                        FirstName.Enabled = true;
                        MiddleName.Enabled = true;
                        ddlDesignation.Enabled = true;
                        ddlState.Enabled = true;
                        ddlDistrict.Enabled = true;
                        ddlTehsil.Enabled = true;
                        OtherTehsil.Enabled = true;
                        Address.Enabled = true;
                        PinCode.Enabled = true;
                        MobileNo.Enabled = true;
                        //EmailID.Enabled = true;
                        City.Enabled = true;
                        txtOTP.Enabled = false;
                        // FillGrid();
                    }

                    if (retrunKeys[0] == "E")
                    {
                        lblNote.Text = "Email ID already Exists.";
                        lblNote.CssClass = "errorNote";
                        lblNote.Visible = true;
                        RbRoleLevel.Enabled = true;
                        LastName.Enabled = true;
                        FirstName.Enabled = true;
                        MiddleName.Enabled = true;
                        ddlDesignation.Enabled = true;
                        ddlState.Enabled = true;
                        ddlDistrict.Enabled = true;
                        ddlTehsil.Enabled = true;
                        OtherTehsil.Enabled = true;
                        Address.Enabled = true;
                        PinCode.Enabled = true;
                        MobileNo.Enabled = true;
                        //EmailID.Enabled = true;
                        City.Enabled = true;
                        txtOTP.Enabled = false;
                        // FillGrid();
                    }

                    if (retrunKeys[0] == "N")
                    {
                        lblNote.Text = "Problem while processing information. Information cannot be processed.";
                        lblNote.CssClass = "errorNote";

                    }
                }


            }
            else
            {
                lblNote.Text = "Invalid OTP.";
                lblNote.CssClass = "errorNote";
            }
        }
        #endregion

        //#region Button delete
        //protected void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (hid_Supervisor_ID.Value != "")
        //    {
        //        hid_Mode.Value = "D";
        //        oHt = CreateHashTable();
        //        oSupervisor = new clsSupervisor();
        //        retrunKeys = oSupervisor.DeletetSupervisor(oHt);
        //        if (retrunKeys[0] == "Y")
        //        {
        //            lblNote.Text = "Information Deleted Successfully.";
        //            lblNote.CssClass = "saveNote";
        //            lblNote.Visible = true;
        //            ClearControl();
        //            FillGrid();


        //        }
        //        else if (retrunKeys[0] == "N")
        //        {
        //            lblNote.Text = "Problem while processing information. Information cannot be processed.";
        //            lblNote.CssClass = "errorNote";
        //            btnNew.Enabled = false;
        //            btnSave.Enabled = true;
        //            btnDelete.Enabled = true;
        //        }

        //    }

        //}

       // #endregion

        #endregion

        #region Function for Server Side Validations
        ////
        ////Server Side Validations
        ////
        private void ServerSideValidations()
        {
            oValidate = new Validation();
            if (RbRoleLevel.SelectedValue != "P")
            {

                oValidate.inputElement(LastName.Text, Convert.ToString(TypeOfValidation.ContainsCharacterOnly) + "|" + Convert.ToString(TypeOfValidation.AtleastOneRequired) + "|" + Convert.ToString(TypeOfValidation.SingleSpace), "Supervisor's Last Name, First Name and Middle Name", null, FirstName.Text, MiddleName.Text);
                oValidate.inputElement(ddlState.SelectedValue, Convert.ToString(TypeOfValidation.RequiredDropDown), "State", null, "0", null);
                oValidate.inputElement(ddlDistrict.SelectedValue, Convert.ToString(TypeOfValidation.RequiredDropDown), "District", null, "0", null);
                //if (OtherTehsil.Text == "")
                //{
                oValidate.inputElement(ddlTehsil.SelectedValue, Convert.ToString(TypeOfValidation.RequiredDropDown), "Tehsil", null, "0", null);
                //}
                oValidate.inputElement(City.Text.Trim(), Convert.ToString(TypeOfValidation.ContainsCharacterOnly) + "|" + Convert.ToString(TypeOfValidation.NonEmpty), "City", null, null, null);
                // oValidate.inputElement(OtherTehsil.Text.Trim(), Convert.ToString(TypeOfValidation.ContainsCharacterOnly) , " Other Tehsil ",null, null, null);
                oValidate.inputElement(Address.Text.Trim(), Convert.ToString(TypeOfValidation.AddressField) + "|" + Convert.ToString(TypeOfValidation.NonEmpty), "Residential Address", null, null, null);
                oValidate.inputElement(MobileNo.Text.Trim(), Convert.ToString(TypeOfValidation.ContainsNumberOnly) + "|" + Convert.ToString(TypeOfValidation.NonEmpty), "Mobile Number", null, null, null);
                if (RbRoleLevel.SelectedValue == "S")
                {
                    oValidate.inputElement(txtOTP.Text.Trim(), Convert.ToString(TypeOfValidation.NonEmpty), "OTP", null, null, null);
                }
               // oValidate.inputElement(EmailID.Text.Trim(), Convert.ToString(TypeOfValidation.NonEmpty) + "|" + Convert.ToString(TypeOfValidation.ValidEmail), "Email ID", null, null, null);
            }


        }
        #endregion

        #region Function CreateHashTable
        /// <summary>
        /// Creates hash table for save,update and delete p
        /// </summary>
        private Hashtable CreateHashTable()
        {
            oHt["Pk_Uni_ID"] = clsGetSettings.UniversityID.ToString();
            oHt["Pk_Inst_ID"] = hidInstID.Value;
            oHt["Pk_Supervisor_ID"] = hid_Supervisor_ID.Value;
            oHt["Created_By"] = ((clsUser)Session["User"]).User_ID;
            if (hid_Mode.Value != "D") // 
            {
                oHt["Last_Name"] = LastName.Text.Trim();
                oHt["First_Name"] = FirstName.Text.Trim();
                oHt["Middle_Name"] = MiddleName.Text.Trim();
                oHt["fk_Desgn_ID"] = ddlDesignation.SelectedValue.ToString();
                oHt["Role_Level"] = RbRoleLevel.SelectedValue.ToString();

                oHt["fk_State_ID"] = ddlState.SelectedValue.ToString();
                oHt["fk_District_ID"] = ddlDistrict.SelectedValue.ToString();
                oHt["fk_Tehsil_ID"] = ddlTehsil.SelectedValue.ToString();
                oHt["OtherTehsil"] = OtherTehsil.Text.ToString();
                oHt["City"] = City.Text.ToString();
                oHt["Residential_Address"] = Address.Text.ToString();
                oHt["PIN"] = PinCode.Text.ToString();
                oHt["Mobile_Number"] = MobileNo.Text.ToString();
                oHt["Email_ID"] = EmailID.Text.ToString();

            }
            if (RbRoleLevel.SelectedValue == "P")
            {

                oHt["Last_Name"] = "";
                oHt["First_Name"] = Inst_Principal_Director_HODName.Text.Trim();
                oHt["Middle_Name"] = "";
                oHt["fk_Desgn_ID"] = ddlDesignation.SelectedValue.ToString();
                oHt["Role_Level"] = RbRoleLevel.SelectedValue.ToString();

                oHt["fk_State_ID"] = fk_PriDirHOD_State_ID.SelectedValue.ToString();
                oHt["fk_District_ID"] = fk_PriDirHOD_District.SelectedValue.ToString();
                oHt["fk_Tehsil_ID"] = fk_PriDirHOD_Tehsil.SelectedValue.ToString();
                oHt["OtherTehsil"] = Inst_PriDirHOD_OtherTehsil.Text.ToString();
                oHt["City"] = Inst_PriDirHOD_City.Text.ToString();
                oHt["Residential_Address"] = Inst_PriDirHOD_Address.Text.ToString();
                oHt["PIN"] = Inst_PriDirHOD_PinCode.Text.ToString();
                oHt["Mobile_Number"] = MobileNo.Text.ToString();
                oHt["Email_ID"] = Inst_PriDirHOD_EmailID1.Text.ToString();
                
            }

            return oHt;
        }

        #endregion

        #region Fill Drop Down
        private void FillDropDown()
        {
            // This will Fill Country  Combo box           
            //


            // This will Fill Correspondance State Combo box
            //
            using (oDT = new DataTable())
            {
                oDT = oState.DisplayAllStates("E");
                oCommon.fillDropDown(ddlState, oDT, string.Empty, "Text", "Value", "---- Select ----");

                ddlState.SelectedValue = ddlState.Items.FindByValue(hid_homeState.Value).Value;

            }//
            // This will Fill Correspondance District Combo box  
            //
            using (oDT = new DataTable())
            {
                oDT = oDistrict.StateWiseDistricts(Convert.ToString(ddlState.SelectedItem.Value), "E");
                oCommon.fillDropDown(ddlDistrict, oDT, string.Empty, "Text", "Value", "---- Select ----");

            }//

            using (oDT = new DataTable())
            {
                oDT = clsDesignation.allNonTeachingDesignations();
                oCommon.fillDropDown(ddlDesignation, oDT, string.Empty, "Text", "Value", "---- Select ----");

            }//


        }
        #endregion

        #region Fill Grid

        private void FillGrid()
        {
            oSupervisor = new clsSupervisor();
            DataTable oDtListIP = new DataTable();
            Hashtable oht = new Hashtable();
            oht["Pk_Uni_ID"] = clsGetSettings.UniversityID.ToString();
            //if (user.UserTypeCode == "2") // login user is college
            oht["Pk_Inst_ID"] = hidInstID.Value;
            //else
            //    oht["Pk_Inst_ID"] = null;
            oDtListIP = oSupervisor.ListSupervisor(oht);
            if ((oDtListIP != null) && (oDtListIP.Rows.Count > 0))
            {
                oGVSupervisor.DataSource = oDtListIP;
                oGVSupervisor.DataBind();
                oGVSupervisor.Visible = true;
                GridHolder.Visible = true;
            }
            else
            {
                GridHolder.Visible = false;
            }
        }

        #endregion

        #region Dropdown  SelectedIndexChange Event
        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            oDT = new DataTable();
            oDT = oState.DisplayAllStates("E");
            if (oDT.Rows.Count > 0 && oDT != null)
            {
                oCommon.fillDropDown(ddlState, oDT, string.Empty, "Text", "Value", "---Select---");
                ddlState.SelectedValue = ddlState.Items.FindByText(hid_homeState.Value).Value;
            }

        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            // This will Fill Correspondence District Combo box     
            //
            oDT = new DataTable();
            oDT = oDistrict.StateWiseDistricts(Convert.ToString(ddlState.SelectedItem.Value), "E");
            if (oDT.Rows.Count > 0 && oDT != null)
            {
                oCommon.fillDropDown(ddlDistrict, oDT, string.Empty, "Text", "Value", "---Select---");

                ddlTehsil.Items.Clear();
                ListItem Li = new ListItem();
                Li.Text = "---Select---";
                Li.Value = "0";
                ddlTehsil.Items.Add(Li);
            }
            else
            {
                ddlDistrict.Items.Clear();
                ListItem Li = new ListItem();
                Li.Text = "---Select---";
                Li.Value = "0";
                ddlDistrict.Items.Add(Li);

                ddlTehsil.Items.Clear();
                Li = new ListItem();
                Li.Text = "---Select---";
                Li.Value = "0";
                ddlTehsil.Items.Add(Li);
            }

            //  ToolkitScriptManager.GetCurrent(this.Page).SetFocus(ddlStateID);
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            //
            // This will Fill Correspondence Taluka Combo box          
            //
            oDT = new DataTable();
            oDT = oTaluka.DisplayTalukaWithinDistrict(ddlDistrict.SelectedItem.Value, "E");
            if (oDT.Rows.Count > 0 && oDT != null)
            {
                oCommon.fillDropDown(ddlTehsil, oDT, string.Empty, "Text", "Value", "---Select---");
            }
            else
            {
                ddlTehsil.ClearSelection();

            }
            //ToolkitScriptManager.GetCurrent(this.Page).SetFocus(CDistrict);
        }
        #endregion

        #region Grid Event
        protected void oGVSupervisor_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            WebControl wc = e.CommandSource as WebControl;
            GridViewRow row = wc.NamingContainer as GridViewRow;

            lblNote.Visible = false;

            if (row != null)
            {
                if (e.CommandName == "Delet")
                {
                    int index = row.RowIndex;
                    GridViewRow selectedRow = oGVSupervisor.Rows[index];
                    hid_Supervisor_ID.Value = Convert.ToString(selectedRow.Cells[1].Text).Trim();
                    if (hid_Supervisor_ID.Value != "")
                    {
                        oHt["Pk_Uni_ID"] = clsGetSettings.UniversityID.ToString();
                        oHt["Pk_Inst_ID"] = hidInstID.Value;
                        oHt["Pk_Supervisor_ID"] = hid_Supervisor_ID.Value;
                        oHt["Created_By"] = ((clsUser)Session["User"]).User_ID;

                        oSupervisor = new clsSupervisor();
                        retrunKeys = oSupervisor.DeletetSupervisor(oHt);
                        if (retrunKeys[0] == "Y")
                        {
                            lblNote.Text = "Information Deleted Successfully.";
                            lblNote.CssClass = "saveNote";
                            lblNote.Visible = true;
                            ClearControl();
                            FillGrid();


                        }
                        else if (retrunKeys[0] == "N")
                        {
                            lblNote.Text = "Problem while processing information. Information cannot be processed.";
                            lblNote.CssClass = "errorNote";
                            btnNew.Enabled = false;
                            btnSave.Enabled = true;
                           // btnDelete.Enabled = true;
                        }
                    }
                }
                if (e.CommandName == "Ediit")
                {
                    int index = row.RowIndex;
                    GridViewRow selectedRow = oGVSupervisor.Rows[index];
                    hid_Supervisor_ID.Value = Convert.ToString(selectedRow.Cells[1].Text).Trim();
                    RbRoleLevel.SelectedValue = Convert.ToString(selectedRow.Cells[19].Text).Trim();
                    MobileNo.Text = Convert.ToString(selectedRow.Cells[4].Text).Trim();
                    hid_MobileNo.Value = Convert.ToString(selectedRow.Cells[4].Text).Trim();
                    EmailID.Text = Convert.ToString(selectedRow.Cells[5].Text).Trim();
                    LastName.Text = Convert.ToString(selectedRow.Cells[6].Text).Trim();
                    FirstName.Text = Convert.ToString(selectedRow.Cells[7].Text).Trim();
                    MiddleName.Text = Convert.ToString(selectedRow.Cells[8].Text).Trim();
                    ddlDesignation.Text = Convert.ToString(selectedRow.Cells[9].Text).Trim();

                    //ddlStateID.SelectedValue = Convert.ToString(selectedRow.Cells[11].Text).Trim();
                    //ddlDistrict.SelectedValue = Convert.ToString(selectedRow.Cells[12].Text).Trim();
                    //ddlTehsil.SelectedValue = Convert.ToString(selectedRow.Cells[13].Text).Trim();
                    OtherTehsil.Text = Convert.ToString(selectedRow.Cells[14].Text).Trim();
                    City.Text = Convert.ToString(selectedRow.Cells[15].Text).Trim();
                    Address.Text = Convert.ToString(selectedRow.Cells[16].Text).Trim();
                    PinCode.Text = Convert.ToString(selectedRow.Cells[17].Text).Trim();
                    hid_Mode.Value = "U";    // Set Mode Of Operation to "U" for Update Mode             
                    btnSave.Enabled = true;
                    //btnDelete.Enabled = true;
                    btnSave.Text = "Update";

                    //oSupervisor = new clsSupervisor();
                    //retrunKeys = oSupervisor.GetOTP();
                    //hid_OTP.Value = retrunKeys[0];

                    oDT = new DataTable();
                    oDT = oState.DisplayAllStates("E");
                    if (oDT.Rows.Count > 0 && oDT != null)
                    {
                        oCommon.fillDropDown(ddlState, oDT, string.Empty, "Text", "Value", "---Select---");
                        ddlState.SelectedValue = hid_homeState.Value;

                    }

                    ddlState.SelectedValue = Convert.ToString(Convert.ToString(selectedRow.Cells[11].Text));

                    tempDT = new DataTable();
                    tempDT = oDistrict.StateWiseDistricts(Convert.ToString(selectedRow.Cells[11].Text), "E");
                    oCommon.fillDropDown(ddlDistrict, tempDT, Convert.ToString(selectedRow.Cells[11].Text), "Text", "Value", "---- Select ----");
                    ddlDistrict.SelectedValue = Convert.ToString(Convert.ToString(selectedRow.Cells[12].Text));

                    tempDT = new DataTable();
                    tempDT = oTaluka.DisplayTalukaWithinDistrict(Convert.ToString(selectedRow.Cells[12].Text), "E");
                    oCommon.fillDropDown(ddlTehsil, tempDT, Convert.ToString(selectedRow.Cells[12].Text), "Text", "Value", "---- Select ----");
                    ddlTehsil.SelectedValue = Convert.ToString(Convert.ToString(selectedRow.Cells[13].Text));
                    //hid_OTP.Value = "";
                    RbRoleLevel.Enabled = true;
                    LastName.Enabled = true;
                    FirstName.Enabled = true;
                    MiddleName.Enabled = true;
                    ddlDesignation.Enabled = true;
                    ddlState.Enabled = true;
                    ddlDistrict.Enabled = true;
                    ddlTehsil.Enabled = true;
                    OtherTehsil.Enabled = true;
                    Address.Enabled = true;
                    PinCode.Enabled = true;
                    MobileNo.Enabled = true;
                    EmailID.Enabled = true;
                    City.Enabled = true;
                    txtOTP.Enabled = false;

                    if (RbRoleLevel.SelectedValue == "J")
                    {
                        trOTP.Attributes.Add("style", "display:none");
                        //tremailOTP.Attributes.Add("style", "display:none");  
                    }
                    else
                    {
                        trOTP.Style.Add("display", ""); ;
                        //tremailOTP.Attributes.Add("style", "display:block");
                    }

                    if (RbRoleLevel.SelectedValue == "P")
                    {

                        LastName.Text = "";
                        FirstName.Text = "";
                        MiddleName.Text = "";
                        ddlDesignation.SelectedValue = "0";
                        ddlState.SelectedValue = "0";
                        ddlDistrict.SelectedValue = "0";
                        ddlTehsil.SelectedValue = "0";
                        OtherTehsil.Text = "";
                        Address.Text = "";
                        PinCode.Text = "";
                       // MobileNo.Text = "";
                        //EmailID.Text = "";
                        City.Text = "";
                    }

                }
                if (e.CommandName == "Verify")
                {
                    //string strScript = "<script> function openPOpup(sno) { window.open('"'ValidateOTP.aspx?SeqNO='"'+sno', 'tinyWindow', 'status=1, resizable=1, scrollbars=0, width=450,height=280'); </script>";  

                    //strScript = string.Format("<script language='JavaScript'>{0}</script>", strScript);
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "setFocus", strScript, false);
                }
            }

        }

        protected void oGVSupervisor_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[1].Visible = false;
                e.Row.Cells[6].Visible = false;
                e.Row.Cells[7].Visible = false;
                e.Row.Cells[8].Visible = false;
                e.Row.Cells[9].Visible = false;
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = false;
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
                e.Row.Cells[17].Visible = false;
                e.Row.Cells[18].Visible = false;
               e.Row.Cells[19].Visible = false;
                //LinkButton btnVerify = (LinkButton)e.Row.FindControl("btnVerify");
                //if (btnVerify != null)
                //{

                //    if (e.Row.Cells[18].Text == "Y" && (e.Row.Cells[19].Text =="S"))
                //    {
                //        btnVerify.Text = "Verified";
                //        btnVerify.Enabled = false;
                //        //btnVerify.ForeColor = #1DA237;
                //    }
                //    else if (e.Row.Cells[18].Text == "N" && (e.Row.Cells[19].Text == "S"))
                //    {
                //        btnVerify.Text = "Verify";
                //        btnVerify.Enabled = true;
                //    }

                //    if (e.Row.Cells[19].Text == "J")
                //    {
                //        btnVerify.Text = "";

                //    }
                //    btnVerify.CommandArgument = e.Row.RowIndex.ToString();

                //    btnVerify.OnClientClick = "return Verify('" + e.Row.Cells[1].Text + "');";

                if (e.Row.Cells[6].Text == "&nbsp;")
                {
                    e.Row.Cells[6].Text = "";
                }
                if (e.Row.Cells[7].Text == "&nbsp;")
                {
                    e.Row.Cells[7].Text = "";
                }
                if (e.Row.Cells[8].Text == "&nbsp;")
                {
                    e.Row.Cells[8].Text = "";
                }
                if (e.Row.Cells[14].Text == "&nbsp;")
                {
                    e.Row.Cells[14].Text = "";
                }
                if (e.Row.Cells[17].Text == "&nbsp;")
                {
                    e.Row.Cells[17].Text = "";
                }


            }
        }
        #endregion

        #region Send SMS function
        public void SendSMS(string sMobile_No, string sFirst_Name, string sUserName, string sOTP, DataTable oDatable)
        {
            sMobile_No = "91" + sMobile_No;
            try
            {

                //1507160373230929769

                //oDT = new DataTable(); [[Name_of_University]]
                // oDT = oDs.Tables[1];
                clsSmsClient oSmsClient = new clsSmsClient();
                string sNotificationMessage = oDatable.Rows[0]["NotificationMessage"].ToString();
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[First_Name\\]\\]", sFirst_Name);
                //sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[UserName\\]\\]", sUserName);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[OTP\\]\\]", sOTP);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[Name_of_University\\]\\]", clsPortalSettings.Instance.Name.ToString().Trim());
                oSmsClient.Body = sNotificationMessage;
                oSmsClient.Username = sUserName;
                oSmsClient.MobileNo = sMobile_No.Trim();
                oSmsClient.SMSFrom = clsPortalSettings.Instance.Name + "-" + oDatable.Rows[0]["SMSFrom"].ToString();
                // oSmsClient.SendSMS();

                oSmsClient.SendSMS(sMobile_No, sNotificationMessage, sUserName, "", "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"));              

            }
            catch
            { throw; }

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
                //obj.epMessage = "Dear " + Name + sMsg + sOTP + ". via MKCL";
                obj.epUser = user.User_ID.ToString();



                string sNotificationMessage = sMsg;
                //sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[First_Name\\]\\]", Name);                
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[OTP\\]\\]", sOTP);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[Name_of_University\\]\\]", clsPortalSettings.Instance.Name.ToString().Trim());

                obj.epMessage = sNotificationMessage;
                //res = obj.SendPersonalizedSMSForOTPTempId(sMobile_No, "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"), templateID);
                res = obj.SendPersonalizedSMSForTempId(sMobile_No, "OA" + DateTime.Now.ToString("ddMMyyyy") + DateTime.Now.ToString("HHmmss"), templateID);

            }
            catch (Exception)
            { }

            return res;
        }
        #endregion



        public void SendMail(string sEmail_ID, string First_Name, string UserName, string OTP, DataTable oDatable)
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
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[OTP\\]\\]", OTP);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[Name_of_University\\]\\]", clsGetSettings.Name);
                //sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[web_site\\]\\]", clsGetSettings.SitePath);
                sNotificationMessage = Regex.Replace(sNotificationMessage, "\\[\\[SitePath\\]\\]", clsGetSettings.SitePath);

                // List<string> cc= new List<string>();
                //cc.Add("ccpawank.kumawat@gmail.com");
                // cc.Add("ccDeepu@gmail.com");
                // oEmailClient.CC = cc;

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

        #region OTP Button Region
        protected void btnOTP_Click(object sender, EventArgs e)
        {

            if (RbRoleLevel.SelectedValue == "S" || RbRoleLevel.SelectedValue == "P")
            {// Send SMS only for Seneior Role
                    oSupervisor = new clsSupervisor();
                retrunKeys = oSupervisor.GetOTP();
                hid_OTP.Value = retrunKeys[0];
                using (oDs = new DataSet())
                {
                    string fname = "";
                    oSupervisor = new clsSupervisor();
                    oDs = oSupervisor.GetSupervisiorRegistrationNotificationDetail();
                    if (RbRoleLevel.SelectedValue == "P")
                    {
                        fname = Inst_Principal_Director_HODName.Text;
                    }
                    else
                        fname = FirstName.Text;
                    //SendSMS(MobileNo.Text, fname, fname, hid_OTP.Value, oDs.Tables[1]);

                    SendSMS(MobileNo.Text, fname, hid_OTP.Value, oDs.Tables[1].Rows[0]["NotificationMessage"].ToString(), "1507160373230929769");
                    OTPMSG.Text = "Enter OTP received on your mobile.";
                    RbRoleLevel.Enabled = false;
                    LastName.Enabled = false;
                    FirstName.Enabled = false;
                    MiddleName.Enabled = false;
                    ddlDesignation.Enabled = false;
                    ddlState.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlTehsil.Enabled = false;
                    OtherTehsil.Enabled = false;
                    Address.Enabled = false;
                    PinCode.Enabled = false;
                    MobileNo.Enabled = false;
                   // EmailID.Enabled = false;
                    City.Enabled = false;
                    txtOTP.Enabled = true;
                }
                trOTP.Style.Add("display", ""); ;
                
            }

        }
        #endregion

        protected void btnEmailOTP_Click(object sender, EventArgs e)
        {

            if (RbRoleLevel.SelectedValue == "S" || RbRoleLevel.SelectedValue == "P")
            {// Send SMS only for Seneior Role
                oSupervisor = new clsSupervisor();
                retrunKeys = oSupervisor.GetOTP();
                hid_emailOTP.Value = retrunKeys[0];
                string emailId = "";
                using (oDs = new DataSet())
                {
                    string fname = "";
                    oSupervisor = new clsSupervisor();
                    oDs = oSupervisor.GetSupervisiorRegistrationNotificationDetail();
                    if (RbRoleLevel.SelectedValue == "S")
                    {

                        emailId = EmailID.Text;
                    }

                    if (RbRoleLevel.SelectedValue == "P")
                    {
                        fname = Inst_Principal_Director_HODName.Text;
                        emailId = Inst_PriDirHOD_EmailID1.Text;
                    }
                    else
                     fname = FirstName.Text;
                   // SendSMS(MobileNo.Text, fname, fname, hid_OTP.Value, oDs.Tables[1]);
                    SendMail(Inst_PriDirHOD_EmailID1.Text, fname, fname, hid_emailOTP.Value, oDs.Tables[0]);
                    OTPMSG.Text = "Enter OTP received on your email ID.";
                    RbRoleLevel.Enabled = false;
                    LastName.Enabled = false;
                    FirstName.Enabled = false;
                    MiddleName.Enabled = false;
                    ddlDesignation.Enabled = false;
                    ddlState.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlTehsil.Enabled = false;
                    OtherTehsil.Enabled = false;
                    Address.Enabled = false;
                    PinCode.Enabled = false;
                   // MobileNo.Enabled = false;
                    EmailID.Enabled = false;
                    City.Enabled = false;
                    txtOTP.Enabled = true;
                    //txtEmailOTP.Enabled = true;
                }
                //tremailOTP.Attributes.Add("style", "display:block");
             

            }

        }

        protected void btnEmailOTPSRJR_Click(object sender, EventArgs e)
        {

            if (RbRoleLevel.SelectedValue == "S" || RbRoleLevel.SelectedValue == "P")
            {// Send SMS only for Seneior Role
                oSupervisor = new clsSupervisor();
                retrunKeys = oSupervisor.GetOTP();
                hid_emailOTP.Value = retrunKeys[0];
                using (oDs = new DataSet())
                {
                    string fname = "";
                    oSupervisor = new clsSupervisor();
                    oDs = oSupervisor.GetSupervisiorRegistrationNotificationDetail();
                    if (RbRoleLevel.SelectedValue == "P")
                    {
                        fname = Inst_Principal_Director_HODName.Text;
                    }
                    else
                        fname = FirstName.Text;
                    // SendSMS(MobileNo.Text, fname, fname, hid_OTP.Value, oDs.Tables[1]);
                   SendMail(EmailID.Text, fname, fname, hid_emailOTP.Value, oDs.Tables[0]);
                    OTPMSG.Text = "Enter OTP received on your email Id.";
                    RbRoleLevel.Enabled = false;
                    LastName.Enabled = false;
                    FirstName.Enabled = false;
                    MiddleName.Enabled = false;
                    ddlDesignation.Enabled = false;
                    ddlState.Enabled = false;
                    ddlDistrict.Enabled = false;
                    ddlTehsil.Enabled = false;
                    OtherTehsil.Enabled = false;
                    Address.Enabled = false;
                    PinCode.Enabled = false;
                   // MobileNo.Enabled = false;
                    EmailID.Enabled = false;
                    City.Enabled = false;
                    txtOTP.Enabled = true;
                    //txtEmailOTP.Enabled = true;
                }
                //tremailOTP.Attributes.Add("style", "display:block");


            }

        }

        protected void gvData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "lnkButSelect")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = UCSearchInstitute.gvData.Rows[index];

                hidInstID.Value = row.Cells[1].Text;
                //hidInstCode.Value = row.Cells[2].Text;
                //hidInstName.Value = row.Cells[3].Text;
                //lblSubHeader1.Text = "-" + hidInstName.Value;
                UCSearchInstitute.Visible = false;
                MainDiv.Style.Add("display", "block");
                FillGrid();
                if (RbRoleLevel.SelectedValue == "P")
                {
                    DisplayInfo();
                }
            }
        }

       

    }
}