<%@ Page Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_DefineSupervisor.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_DefineSupervisor" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="WebCtrl/linkAff.ascx" TagName="linkAff" TagPrefix="uc4" %>
<%@ Register Src="WebCtrl/linkInst.ascx" TagName="linkInst" TagPrefix="uc2" %>--%>
<%@ Register Src="WebCtrl/PreExamV2_SearchInstitute.ascx" TagName="SearchInstitute" TagPrefix="uc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style type="text/css">
  label {/* for firefox */vertical-align:middle; /*for internet explorer */*bottom:3px;*position:relative; padding:0px 10px 0px 2px;}table tr td{vertical-align: top;}
        .style4
        {
            width: 8%;
        }
        .style5
        {
            width: 10%;
        }
        </style>
    <!-- Heading Starts-->
    <script type="text/javascript">

        function Verify(sno) {
            window.open("ValidateOTP.aspx?SeqNO=" + sno, "tinyWindow", "status=1, width=300, height=200, top=100, left=300");
        }


        $(document).ready(function () {
            showhideOTP();

        });

        function validatemail() {

            var i = -1;
            var myArr = new Array();
            myArr[++i] = new Array(document.getElementById("<%=EmailID.ClientID %>"), "Email", "Enter valid Email ID.", "text");
            var ret = validateMe(myArr, 50);
            return ret;
        }


        function showhideOTP() {

            if (document.getElementById("ctl00_ContentPlaceHolder1_RbRoleLevel_0").checked) {
                document.getElementById("<%=trOTP.ClientID%>").style.display = null;
                document.getElementById("<%=divOTP.ClientID%>").style.display = null;

                document.getElementById("<%=tbseniorjunior.ClientID%>").style.display = 'block';
                document.getElementById("<%=tbPrincipal.ClientID%>").style.display = 'none';


                document.getElementById("<%=trEmailotpSRJR.ClientID%>").style.display = null;
                document.getElementById("<%=divEmailOTPSRJR.ClientID%>").style.display = null;

            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_RbRoleLevel_1").checked) {
                document.getElementById("<%=trOTP.ClientID%>").style.display = 'none';
                document.getElementById("<%=divOTP.ClientID%>").style.display = 'none';
                document.getElementById("<%=tbseniorjunior.ClientID%>").style.display = 'block';
                document.getElementById("<%=tbPrincipal.ClientID%>").style.display = 'none';
                document.getElementById("<%=trEmailotpSRJR.ClientID%>").style.display = 'none';
                document.getElementById("<%=tremailOTP.ClientID%>").style.display = 'none';
                document.getElementById("<%=divEmailOTPSRJR.ClientID%>").style.display = 'none';



            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_RbRoleLevel_2").checked) {
                document.getElementById("<%=tbseniorjunior.ClientID%>").style.display = 'none';
                document.getElementById("<%=tbPrincipal.ClientID%>").style.display = 'block';
                document.getElementById("<%=trOTP.ClientID%>").style.display = null;
                document.getElementById("<%=divOTP.ClientID%>").style.display = null;
                document.getElementById("<%=divOTP.ClientID%>").style.display = null;
                document.getElementById("<%=tremailOTP.ClientID%>").style.display = null;


            }
        }

        function OTPMobile(ctrlToValidate) {
            showhideOTP();
            var i = -1;
            var myArr = new Array();

            myArr[++i] = new Array(document.getElementById("<%=MobileNo.ClientID %>"), "NumericOnly/Empty", "Enter valid Mobile Number.", "text");
            var ret = validateMe(myArr, 50, ctrlToValidate);
            if (ret) {
                ret = ChkMobileNo();
                if (ret == true) {
                    ret = true;
                }
                else {
                    var options = document.getElementById("<%=MobileNo.ClientID %>");
                    showValidationSummary(options, "1. Mobile No. should be of 10 digit.");
                    ret = false;
                }
            }
            return ret
        }

        function fnSaveValidate(ctrlToValidate) {

            var i = -1;
            var myArr = new Array();
            if (!document.getElementById("ctl00_ContentPlaceHolder1_RbRoleLevel_2").checked) {
                var ctrl = new Array(document.getElementById("<%=LastName.ClientID %>"), document.getElementById("<%=FirstName.ClientID %>"), document.getElementById("<%=MiddleName.ClientID %>"));
                myArr[++i] = new Array(ctrl, "AtLeastOneRequired", "Atleast one from Last Name, First Name and Middle Name is required.", "AtleastOne");

                myArr[++i] = new Array(document.getElementById("<%=LastName.ClientID %>"), "AlphaOnly/SingleSpace", "Enter valid Last Name.", "text");
                myArr[++i] = new Array(document.getElementById("<%=FirstName.ClientID %>"), "AlphaOnly/SingleSpace", "Enter valid First Name.", "text");
                myArr[++i] = new Array(document.getElementById("<%=MiddleName.ClientID %>"), "AlphaOnly/SingleSpace", "Enter valid Middle Name.", "text");
                myArr[++i] = new Array(document.getElementById("<%=ddlState.ClientID %>"), "0", "Select State.", "select");
                myArr[++i] = new Array(document.getElementById("<%=ddlDistrict.ClientID %>"), "0", "Select District.", "select");
                myArr[++i] = new Array(document.getElementById("<%=ddlTehsil.ClientID %>"), "0", "Select Tahsil.", "select");

                myArr[++i] = new Array(document.getElementById("<%=PinCode.ClientID %>"), "NumericOnly", "Enter valid Pin Code.", "text");
                myArr[++i] = new Array(document.getElementById("<%=Address.ClientID %>"), "Empty", "Enter valid Address.", "text");
                myArr[++i] = new Array(document.getElementById("<%=Address.ClientID %>"), "SpecialCharacters", "Address  has Invalid Characters.", "text");
                myArr[++i] = new Array(document.getElementById("<%=City.ClientID %>"), "AlphaOnly/Empty", "Enter valid City for Address for Correspondence.", "text");
                myArr[++i] = new Array(document.getElementById("<%=MobileNo.ClientID %>"), "NumericOnly/Empty", "Enter valid Mobile Number.", "text");
            }

            if (document.getElementById("ctl00_ContentPlaceHolder1_RbRoleLevel_0").checked) {
                myArr[++i] = new Array(document.getElementById("<%=txtOTP.ClientID %>"), "Empty", "Enter OTP recived on your mobile.", "text");
                myArr[++i] = new Array(document.getElementById("<%=txtEmailotpSRJR.ClientID %>"), "Empty", "Enter OTP recived on your email .", "text");

            }
            if (document.getElementById("ctl00_ContentPlaceHolder1_RbRoleLevel_2").checked) {

                myArr[++i] = new Array(document.getElementById("<%=MobileNo.ClientID %>"), "NumericOnly/Empty", "Enter valid Mobile Number.", "text");
                myArr[++i] = new Array(document.getElementById("<%=txtOTP.ClientID %>"), "Empty", "Enter OTP recived on youe mobile.", "text");
                myArr[++i] = new Array(document.getElementById("<%=txtEmailOTP.ClientID %>"), "Empty", "Enter OTP recived on your email .", "text");

            }
           
            var ret = validateMe(myArr, 50, ctrlToValidate);
            if (ret) {
                ret = ChkMobileNo();
                if (ret == true) {
                    ret = true;
                }
                else {
                    var options = document.getElementById("<%=MobileNo.ClientID %>");
                    showValidationSummary(options, "1. Mobile No. should be of 10 digit.");
                    ret = false;
                }
                if (document.getElementById("<%=PinCode.ClientID %>").value != "") {
                    ret = ChkPincode();
                    if (ret == true) {
                        ret = true;
                    }
                    else {
                        var options = document.getElementById("<%=PinCode.ClientID %>");
                        showValidationSummary(options, "1. Pin Code No. should be 6 digit.");
                        ret = false;
                    }
                }
            }

            return ret;
        }

        // Check Mobile No should be atleast 10 digits 	
        function ChkMobileNo() {
            var ret = false;
            if (document.getElementById("<%=MobileNo.ClientID %>") != null) {
                if (document.getElementById("<%=MobileNo.ClientID %>").value.length == 10 || document.getElementById("<%=MobileNo.ClientID %>").value.length == "") {
                    ret = true;
                }
            }
            else {
                ret = false;
            }
            return ret;
        }

        function ChkPincode() {
            var ret = true;

            if (document.getElementById("<%=PinCode.ClientID %>").value.length == 6 || document.getElementById("<%=MobileNo.ClientID %>").value.length == "") {
                ret = true;
            }

            else {
                ret = false;
            }
            return ret;
        }

    </script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr align="left">
            <td align="left" style="border-bottom: 1px solid #FFD275; margin-left: 40px;">
                <asp:Label ID="lblPageHead" runat="server" Text="Supervisor Details"></asp:Label>
                <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
            </td>
        </tr>
        <tr>
        <td>
              <uc1:SearchInstitute ID="UCSearchInstitute" runat="server" />
        </td>
        </tr>
        <!--<tr>
            <td align="left" valign="top">
                <table id="tblLink" runat="server" border="0" cellpadding="0" style="display: inline;
                    border-collapse: collapse" width="100%">
                    <tr id="rLnkinst" runat="server">
                        <td style="border-bottom: 1px solid #FFD275;">
                            <%--<uc2:linkinst ID="LinkInst1" runat="server" />--%>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>-->
    </table>
    <br />
    <!-- Heading Ends-->
    <div id="MainDiv" runat="server" width="100%">
    <table cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td valign="top" align="left" width="2%">
                &nbsp;
            </td>
            <td valign="top" align="left" style="width: 80%">
                <!-- Toolbar Starts-->
                <table cellspacing="1" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td valign="top" align="left" width="700">
                            <table class="ToolBar" id="table15" cellspacing="0" cellpadding="0" width="100%"
                                border="0" runat="server">
                                <tr>
                                    <td align="center" width="11%">
                                        <img height="16" src="../images/button_new.gif" width="16" border="0">
                                        <asp:Button ID="btnNew" runat="server" CssClass="But" Text="New" OnClick="btnNew_Click">
                                        </asp:Button>
                                    </td>
                                    <td align="center" width="11%">
                                        <img height="16" src="../images/button_save.gif" width="16" border="0">
                                        <asp:Button ID="btnSave" runat="server" CssClass="But" Text="Save" OnClick="btnSave_Click"
                                            OnClientClick="return fnSaveValidate();"></asp:Button>
                                    </td>
                                   <%-- <td align="center" width="11%">
                                        <img height="16" src="../images/button_delete.gif" width="16" border="0">
                                        <asp:Button ID="btnDelete" runat="server" CssClass="But" Text="Delete" OnClick="btnDelete_Click">
                                        </asp:Button>
                                    </td>--%>
                                   <%-- <td align="center" width="7%">
                                        &nbsp;
                                    </td>
                                    <td align="center" width="11%">
                                        <img height="16" src="../images/button_reset.gif" width="16" border="0"><input class="But"
                                            title="Reset" accesskey="R" tabindex="4" type="reset" value="Reset" name="Reset">
                                    </td>--%>
                                    <td align="right">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <div align="center" style="margin-top: 5px; margin-bottom: 5px">
                    <div id="divContainer" class="clOuterDiv">
                        <div class="clImageHolder">
                        </div>
                        <div id="divInfoHolder" class="clInfoHolder" style="background-color: #EFEFEF; text-align: left;
                            border: dashed 1px #c0c0c0; padding-top: 10px; padding-bottom: 10px; padding-left: 20px;">
                            <asp:Label ID="lblInfo" Text=" Please enter the password provided in the text box"
                                runat="server"></asp:Label></div>
                    </div>
                </div>
                <div align="right" style="margin-bottom: 5px">
                    <asp:Label ID="lblNote" runat="server" CssClass="saveNote" Height="15px"></asp:Label>
                </div>
                <fieldset>
                    <legend>Supervisor Details</legend>
                    <table width="100%">
                        <tr>
                            <td align="right" style="width: 30%; padding-bottom: 10px;">
                                <strong>Role Level</strong>
                            </td>
                            <td align="center" style="width: 1%; padding-bottom: 10px;">
                                <strong>&nbsp;:&nbsp;</strong>
                            </td>
                            <td width="69%" style="padding-bottom: 10px;">
                                <asp:RadioButtonList ID="RbRoleLevel" runat="server" RepeatDirection="Horizontal" 
                                    onclick="showhideOTP();" 
                                    >
                                    <asp:ListItem Text="Senior Supervisor" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="Junior Supervisor" Value="J"></asp:ListItem>
                                    <asp:ListItem Text="Principal/Director/HOD" Value="P" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                    <table width="100%" runat="server" id="tbseniorjunior" style="display: block">
                        <tr>
                            <td colspan="3">
                                <table>
                                    <tr>
                                        <td align="right" class="style5">
                                            <asp:Label runat="server" ID="LblFamilyName" Text="[Last Name]"></asp:Label>
                                        </td>
                                        <td align="center" class="style4">
                                            <asp:Label runat="server" ID="LblGivenName" Text="[First Name]"></asp:Label>
                                        </td>
                                        <td align="left" class="style5">
                                            <asp:Label runat="server" ID="LblFatherName" Text="[Middle Name]"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" style="width: 30%">
                                <b>
                                    <asp:Label runat="server" ID="Label1" Text="Name of the Supervisor"></asp:Label></b>
                            </td>
                            <td align="center" style="width: 1%">
                                <strong>&nbsp;:&nbsp;</strong>
                            </td>
                            <td width="69%">
                                <div>
                                    <asp:TextBox ID="LastName" runat="server" Width="132px" MaxLength="100" CssClass="mask"
                                        ToolTip="Last Name" TabIndex="1"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="LastName"
                                        FilterType="LowercaseLetters, UppercaseLetters,Custom" ValidChars="'" />
                                    <span style="padding-right: 16px"></span>
                                    <asp:TextBox ID="FirstName" runat="server" Width="132px" MaxLength="100" CssClass="mask"
                                        ToolTip="First Name" TabIndex="2"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="FirstName"
                                        FilterType="LowercaseLetters, UppercaseLetters" />
                                    <span style="padding-right: 16px"></span>
                                    <asp:TextBox ID="MiddleName" runat="server" Width="132px" MaxLength="100" CssClass="mask"
                                        ToolTip="Middle Name" TabIndex="3"></asp:TextBox><font class="Mandatory"></font></div>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="MiddleName"
                                    FilterType="LowercaseLetters, UppercaseLetters" />
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="right" style="width: 30%">
                                <b>Designation</b>
                            </td>
                            <td align="center" style="width: 1%">
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td width="69%">
                                <asp:DropDownList ID="ddlDesignation" runat="server" Width="136px" TabIndex="4">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="3">
                                <asp:UpdatePanel ID="upCorrosponds" runat="server">
                                    <ContentTemplate>
                                        <table style="border-collapse: collapse" cellspacing="2" cellpadding="0" width="100%"
                                            border="0">
                                            <tr>
                                                <td align="right" width="30%">
                                                    <b>State</b>
                                                </td>
                                                <td align="center">
                                                    <b>&nbsp;:&nbsp;</b>
                                                </td>
                                                <td align="left" width="69%">
                                                    <b>
                                                        <asp:DropDownList ID="ddlState" runat="server" Width="160px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged" TabIndex="5">
                                                            <asp:ListItem Value="0" Text="--- Select ---"></asp:ListItem>
                                                        </asp:DropDownList>
                                                        <font class="Mandatory">*</font></b>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="right">
                                                    <b>District</b>
                                                </td>
                                                <td align="center">
                                                    <b>&nbsp;:&nbsp;</b>
                                                </td>
                                                <td align="left">
                                                    <b>
                                                        <asp:DropDownList ID="ddlDistrict" runat="server" Width="160px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" TabIndex="5">
                                                            <asp:ListItem Value="0" Text="--- Select ---"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </b><font class="Mandatory">*</font>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td align="right">
                                                    <strong>Tahsil</strong>
                                                </td>
                                                <td align="center">
                                                    <strong>&nbsp;:&nbsp;</strong>
                                                </td>
                                                <td width="69%" align="left">
                                                    <strong></strong>
                                                    <asp:DropDownList ID="ddlTehsil" runat="server" Width="104px" AutoPostBack="True"
                                                        TabIndex="6">
                                                        <asp:ListItem Value="0" Text="--- Select ---"></asp:ListItem>
                                                    </asp:DropDownList>
                                                    <font class="Mandatory"></font>
                                                </td>
                                            </tr>
                                             
                                            <tr id="trOtherTahsil" runat="server" visible="false">
                                                <td align="right">
                                                    <strong>Other Tahsil</strong>
                                                </td>
                                                <td align="center" width="1%">
                                                    <strong>&nbsp;:&nbsp;</strong>
                                                </td>
                                                <td style="width: 488px" align="left">
                                                    <asp:TextBox ID="OtherTehsil" runat="server" CssClass="inputbox" Width="232px" TabIndex="7"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="OtherTehsil"
                                                        FilterType="LowercaseLetters, UppercaseLetters" />
                                                </td>
                                            </tr>
                                             
                                            <tr>
                                                <td align="right" width="30%">
                                                    <strong>Village/Town/City</strong>
                                                </td>
                                                <td align="center" width="1%">
                                                    <b>:</b>
                                                </td>
                                                <td width="69%" align="left">
                                                    <asp:TextBox ID="City" runat="server" CssClass="inputbox" MaxLength="100" Width="144px"
                                                        TabIndex="8"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="City"
                                                        FilterType="LowercaseLetters, UppercaseLetters" />
                                                    <font class="Mandatory">*</font>
                                                </td>
                                            </tr>
                                           
                                            <tr>
                                                <td valign="top" align="right" width="30%">
                                                    <strong>Residential address of registering person</strong>
                                                </td>
                                                <td valign="top" align="center" width="1%">
                                                    <strong>:</strong>
                                                </td>
                                                <td valign="top" style="width: 69%" align="left">
                                                    <asp:TextBox ID="Address" runat="server" CssClass="inputbox" Height="50px" Width="294px"
                                                        TextMode="MultiLine" TabIndex="9"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="Address"
                                                        FilterType="Numbers,LowercaseLetters, UppercaseLetters,Custom" ValidChars=",'." />
                                                    <font class="Mandatory">*</font>
                                                    <asp:Label ID="Label4" runat="server" CssClass="Mandatory" Width="408px" meta:resourcekey="Label4Resource1"> [Do not write 

State/ District/ Tehsil/ City/ PIN again in this Box]</asp:Label>
                                                </td>
                                            </tr>
                                            <br />
                                            <tr>
                                                <td align="right" width="30%">
                                                    <b>
                                                        <asp:Label ID="lblpinCode" runat="server" Font-Bold="True"><b>Pin</b></asp:Label></b>
                                                </td>
                                                <td align="center" width="1%">
                                                    <b>:</b>
                                                </td>
                                                <td align="left" width="69%">
                                                    <asp:TextBox ID="PinCode" runat="server" CssClass="inputbox" MaxLength="6" Width="88px"
                                                        TabIndex="10"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="fPinCode" runat="server" TargetControlID="PinCode"
                                                        FilterType="Numbers" />
                                                         <font class="Mandatory">*</font>
                                                </td>
                                            </tr>
                                             <br />
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ddlState" EventName="SelectedIndexChanged">
                                        </asp:AsyncPostBackTrigger>
                                        <asp:AsyncPostBackTrigger ControlID="ddlDistrict" EventName="SelectedIndexChanged">
                                        </asp:AsyncPostBackTrigger>
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="right" width="30%" valign="middle">
                                <b>email ID </b>
                            </td>
                            <td width="1%" align="center" valign="middle">
                                <b>:</b>
                            </td>
                            <td align="left" width="69%" valign="middle">
                                <table>
                                    <tr>
                                        <td width="30%">
                                            <asp:TextBox ID="EmailID" runat="server" CssClass="inputbox" MaxLength="100" Width="132px"
                                                TabIndex="11"></asp:TextBox>
                                                <font class="Mandatory">*</font>
                                        </td>
                                        <td id="divEmailOTPSRJR" runat="server" width="70%">
                                            &nbsp;<asp:Button ID="btnEmailOTPSRJR" runat="server" Text="Get OTP on E-Mail" align="top"
                                                OnClick="btnEmailOTPSRJR_Click" TabIndex="12" OnClientClick="return validatemail();" />
                                            &nbsp;&nbsp;OTP:One time password
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="EmailID"
                                                FilterType="Numbers,LowercaseLetters, UppercaseLetters,Custom" ValidChars=".@_" />
                                        </td>
                                    </tr>
                                    
                                </table>
                            </td>
                        </tr>
                        
                        <tr id="trEmailotpSRJR" runat="server">
                            <td align="right" width="30%">
                                <b>OTP received through email </b>
                            </td>
                            <td align="center" width="1%">
                                <b>:</b>
                            </td>
                            <td align="left" width="69%">
                                <asp:TextBox ID="txtEmailotpSRJR" runat="server" CssClass="inputbox" MaxLength="10"
                                    Width="125px" TabIndex="12"></asp:TextBox>
                                &nbsp;&nbsp;
                            </td>
                        </tr>
                       
                    </table>
                    <table id="tbPrincipal" runat="server" border="0" cellpadding="0" cellspacing="2"
                        style="border-collapse: collapse;" width="100%">
                        <tr>
                            <td align="right" style="width: 30%">
                                <b>Name of Principal/Director/HOD</b>
                            </td>
                            <td align="center" style="width: 1%">
                                <b>:</b>
                            </td>
                            <td style="width: 69%">
                                <asp:TextBox ID="Inst_Principal_Director_HODName" runat="server" CssClass="inputbox"
                                    MaxLength="250" Width="384px" TabIndex="14"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="right" style="width: 30%">
                                <b>Country</b>
                            </td>
                            <td align="center">
                                <b>:</b>
                            </td>
                            <td align="left">
                                <b>
                                    <asp:DropDownList ID="fk_PriDirHOD_Country" runat="server" onchange="changeprinciCountry(this.value); "
                                        Enabled="false" Width="160px" TabIndex="15">
                                    </asp:DropDownList>
                                </b>
                            </td>
                        </tr>
                       
                        <tr id="trpstate" runat="server">
                            <td align="right" style="width: 30%">
                                <b>State</b>
                            </td>
                            <td align="center">
                                <b>:</b>
                            </td>
                            <td align="left">
                                <b>
                                    <asp:DropDownList ID="fk_PriDirHOD_State_ID" runat="server" onchange="FillDistrictDD(this.value)"
                                        Width="160px" TabIndex="16">
                                    </asp:DropDownList>
                                </b>
                            </td>
                        </tr>
                       
                        <tr id="trpdistrict" runat="server">
                            <td align="right" style="width: 30%; height: 1%">
                                <b>District </b>
                            </td>
                            <td align="center" style="height: 1%">
                                <b>:</b>
                            </td>
                            <td id="TbDistPDH" align="left" style="height: 1%">
                                <b>
                                    <asp:DropDownList ID="fk_PriDirHOD_District" runat="server" onchange="FillTalukaDD(this.value);"
                                        Width="160px" TabIndex="17">
                                    </asp:DropDownList>
                                </b>
                            </td>
                        </tr>
                        
                        <tr id="trptahsil" runat="server">
                            <td align="right" style="width: 30%; height: 28px">
                                <strong>Tahsil&nbsp;</strong>
                            </td>
                            <td align="center" style="height: 28px">
                                <strong>:</strong>
                            </td>
                            <td id="TbTalPDH" style="height: 28px">
                                <strong></strong>
                                <asp:DropDownList ID="fk_PriDirHOD_Tehsil" runat="server" Width="104px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        
                        <tr id="trpOthertahsil" runat="server">
                            <td align="right" style="width: 30%; height: 28px">
                                <b>Other Tahsil</b>
                            </td>
                            <td align="center">
                                <b>:</b>
                            </td>
                            <td>
                                <asp:TextBox ID="Inst_PriDirHOD_OtherTehsil" runat="server" CssClass="inputbox" MaxLength="100"
                                    Width="232px"></asp:TextBox>
                            </td>
                        </tr>
                        <br />
                        <tr>
                            <td align="right" style="width: 30%">
                                <strong>Village/Town/City</strong>
                            </td>
                            <td align="center">
                                <b>:</b>
                            </td>
                            <td>
                                <asp:TextBox ID="Inst_PriDirHOD_City" runat="server" CssClass="inputbox" MaxLength="100"
                                    Width="144px"></asp:TextBox>
                            </td>
                        </tr>
                      
                        <tr>
                            <td align="right" style="width: 30%">
                                <strong>Residential Address</strong>
                            </td>
                            <td align="center">
                                <strong>:</strong>
                            </td>
                            <td>
                                <asp:TextBox ID="Inst_PriDirHOD_Address" runat="server" CssClass="inputbox" Height="50px"
                                    TextMode="MultiLine" Width="294px" TabIndex="18"></asp:TextBox><br />
                                <span class="Mandatory" style="font-size:x-small;">(Do not write State/ District/ Tehsil/ City/ PIN again in this
                                    Box )</span>
                            </td>
                        </tr>
                        
                        <tr>
                            <td align="right" style="width: 30%">
                                <b>PIN</b>
                            </td>
                            <td align="center">
                                <b>:</b>
                            </td>
                            <td>
                                <asp:TextBox ID="Inst_PriDirHOD_PinCode" runat="server" CssClass="inputbox" MaxLength="6"
                                    Width="88px" Enabled="false" TabIndex="19"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="right" style="width: 30%">
                                <b>Resi Phone No. 1</b>
                            </td>
                            <td align="center">
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td valign="middle">
                                <asp:TextBox ID="Inst_PriDirHOD_STD1" runat="server" CssClass="inputbox" MaxLength="6"
                                    Width="30px" Enabled="false" TabIndex="20"></asp:TextBox>&nbsp;
                                <asp:TextBox ID="Inst_PriDirHOD_TelNo1" runat="server" CssClass="inputbox" MaxLength="20"
                                    Width="88px" Enabled="false" TabIndex="21"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="right">
                                <b>Resi Phone No. 2</b>
                            </td>
                            <td align="center">
                                <b>:</b>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="Inst_PriDirHOD_STD2" runat="server" CssClass="inputbox" MaxLength="6"
                                    Width="30px" Enabled="false" TabIndex="22"></asp:TextBox>&nbsp;
                                <asp:TextBox ID="Inst_PriDirHOD_TelNo2" runat="server" CssClass="inputbox" MaxLength="20"
                                    Width="88px" Enabled="false" TabIndex="23"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td align="right" style="width: 30%">
                                <b>Email ID 1</b>
                            </td>
                            <td align="center">
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td>
                                <asp:TextBox ID="Inst_PriDirHOD_EmailID1" runat="server" CssClass="inputbox" MaxLength="100"
                                    Width="200px" TabIndex="24"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="Inst_PriDirHOD_EmailID1"
                                    FilterType="Numbers,LowercaseLetters, UppercaseLetters,Custom" ValidChars=".@_" />
                            </td>
                        </tr>
                        
                        
                        <tr id="tremailOTP" runat="server">
                         <td align="right" style="width: 30%">
                                <b>OTP received through email </b>
                            </td>
                            <td align="center">
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td>
                               <asp:TextBox ID="txtEmailOTP" runat="server" CssClass="inputbox" MaxLength="10" Width="100px"></asp:TextBox>
                                &nbsp;&nbsp;<div style="float: right; width: 49%" id="div1" runat="server">
                                </div>
                                &nbsp;<span style="font-size:x-small">OTP: One Time Password</span>
                                <asp:Button ID="btnEmailOTP" runat="server" Text="Get OTP on E-Mail" align="top"
                                    OnClick="btnEmailOTP_Click" />
                            </td>
                           
                        </tr>
                        
                        <tr>
                            <td align="right" style="width: 30%">
                                <b>Email ID 2</b>
                            </td>
                            <td align="center">
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td>
                                <asp:TextBox ID="Inst_PriDirHOD_EmailID2" runat="server" CssClass="inputbox" MaxLength="100"
                                    Width="200px" TabIndex="26"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="Inst_PriDirHOD_EmailID2"
                                    FilterType="Numbers,LowercaseLetters, UppercaseLetters,Custom" ValidChars=".@_" />
                            </td>
                        </tr>
                        
                    </table>
                    <table width="100%">
                        <tr>
                            <td align="right" width="30%" valign="top">
                                <b>Mobile No. </b>
                            </td>
                            <td align="center" width="1%" valign="top">
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left" width="69%">
                                <div style="float: left; width: 60%">
                                    <asp:TextBox ID="CountryID" runat="server" CssClass="inputbox" Width="30px" ReadOnly="True">+91</asp:TextBox>&nbsp;&nbsp;<asp:TextBox
                                        ID="MobileNo" runat="server" CssClass="inputbox" MaxLength="10" Width="200px"
                                        TabIndex="27"></asp:TextBox> <span class="Mandatory">*</span>
                                   </div>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="MobileNo"
                                    FilterType="Numbers" />
                                <div style="float: left; width: 49%" id="divOTP" runat="server">
                                    <asp:Button ID="btnOTP" runat="server" Text="Get OTP on Mobile" OnClientClick="return OTPMobile();"
                                        OnClick="btnOTP_Click" TabIndex="28" Width="176px" />
                                </div>
                            </td>
                        </tr>

                       
                        <tr id="trOTP" runat="server">
                            <td align="right" width="30%">
                                <b>Enter OTP received on Mobile </b>
                            </td>
                            <td align="center" width="1%">
                                <b>&nbsp;:&nbsp;</b>
                            </td>
                            <td align="left" width="69%">
                                <asp:TextBox ID="txtOTP" runat="server" CssClass="inputbox" MaxLength="10" Width="125px"
                                    TabIndex="13"></asp:TextBox>
                            </td>
                        </tr>
                       
                    </table>
                    <p style="margin-top: 2px; margin-bottom: 5px; margin-left: 5px" align="left">
                        <strong>Note:</strong><font class="Mandatory">*</font> Marked fields are mandatory.
                    </p>
                    <div style="margin-top: 2px; margin-bottom: 10px; margin-left: 40px;" align="left" >
                        <asp:Label ID="LabelNote" runat="server" Height="100px">
                       a.	Please enter valid e-mail ID and click on ‘Get OTP’ <br />
b.	Please enter mobile number and then click on ‘Get OTP’ <br />
c.	Wait for 5-10 minutes to get the OTP on E-mail ID and Mobile Number.  <br />
d.	The OTP received in Email ID and Mobile number are different. <br />
e.	Enter the OTP in respective text and save the information.

                        </asp:Label>
                    </div>
                    <div align="right">
                        <asp:Label ID="OTPMSG" runat="server" CssClass="saveNote" Height="15px"></asp:Label>
                    </div>
                </fieldset>
                <div id="GridHolder" runat="server">
                    <div id="GridHeader" align="left" style="padding-top: 5px; padding-bottom: 5px">
                        <asp:Label ID="lblGridHeading" Text="List of already added Supervisor(s)" Font-Bold="true"
                            runat="server"></asp:Label>
                    </div>
                    <asp:GridView ID="oGVSupervisor" runat="server" Width="80%" AutoGenerateColumns="False"
                        AllowPaging="True" CssClass="clGrid" PageSize="25" OnRowCommand="oGVSupervisor_RowCommand"
                        OnRowDataBound="oGVSupervisor_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No.">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                                <HeaderStyle CssClass="gridHeader" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Pk_Supervisor_ID" DataField="Pk_Supervisor_ID" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Name" HeaderText="Supervisor Name">
                                <HeaderStyle Width="30%" CssClass="gridHeader"></HeaderStyle>
                                <ItemStyle CssClass="gridItem" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Role_Level_Name" HeaderText="RoleLevel">
                                <HeaderStyle Width="10%" CssClass="gridHeader"></HeaderStyle>
                                <ItemStyle CssClass="gridItem" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Mobile_Number" HeaderText="Mobile No">
                                <HeaderStyle Width="10%" CssClass="gridHeader"></HeaderStyle>
                                <ItemStyle CssClass="gridItem" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Email_ID" HeaderText="Email ID">
                                <HeaderStyle Width="15%" CssClass="gridHeader"></HeaderStyle>
                                <ItemStyle CssClass="gridItem" HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Last_Name" DataField="Last_Name" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="First_Name" DataField="First_Name" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Middle_Name" DataField="Middle_Name" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="fk_Desgn_ID" DataField="fk_Desgn_ID" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="fk_Country_ID" DataField="fk_Country_ID" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="fk_State_ID" DataField="fk_State_ID">
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="fk_District_ID" DataField="fk_District_ID" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="fk_Tehsil_ID" DataField="fk_Tehsil_ID" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="OtherTehsil" DataField="OtherTehsil" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="City" DataField="City" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Residential_Address" DataField="Residential_Address" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="PIN" DataField="PIN" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Is_Verified" DataField="Is_Verified" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Role_Level" DataField="Role_Level" >
                                <ItemStyle HorizontalAlign="Left" CssClass="off" />
                                <HeaderStyle CssClass="off" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <HeaderStyle Width="7%" CssClass="gridHeader" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Ediit">Edit</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Delete">
                                <HeaderStyle Width="7%" CssClass="gridHeader" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delet">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle />
                    </asp:GridView>
                </div>
                <input type="hidden" id="hid_MobileNo" runat="server" />
                <input type="hidden" id="hid_PrincipleMobileNo" runat="server" />
                <!-- Entry Area Ends-->
                <input id="fk_District_ID" style="width: 32px; height: 22px" type="hidden" name="Hidden2"
                    runat="server" />
                <input id="hidCountryId" style="width: 32px; height: 22px" type="hidden" name="Hidden2"
                    runat="server" />
                <input id="hidCountryId2" style="width: 32px; height: 22px" type="hidden" name="Hidden2"
                    runat="server" />
                <input id="fk_Tehsil_ID" style="width: 32px; height: 22px" type="hidden" name="Hidden1"
                    runat="server" />
                <input id="hid_homeState" type="hidden" name="hid_homeState" runat="server" />
                <input id="hid_Mode" type="hidden" runat="server" />
                <input id="hid_Supervisor_ID" type="hidden" runat="server" />
                <input type="hidden" runat="server" id="hid_OTP" />
                <input type="hidden" runat="server" id="hid_emailOTP" />
                <input id="hidUniID" type="hidden" size="3" name="Hidden1" runat="server" />
                <input id="hidInstID" type="hidden" size="3" name="Hidden2" runat="server" />
                <input id="fk_PriDirHOD_District_ID" runat="server" name="Hidden1" size="3" style="width: 52px;
                    height: 22px" type="hidden" /><input id="fk_PriDirHOD_Tehsil_ID" runat="server" name="Hidden2"
                        size="2" style="width: 48px; height: 22px" type="hidden" />
            </td>
        </tr>
    </table>
    </div>
</asp:Content>