<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SRPD_PaperSetter_BankDetails.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SRPD_PaperSetter_BankDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <title></title>
    <style>
        .wrapper {
    border:1px solid #000;
    display:inline-block;
}

input,
button {
    background-color:transparent;
    border:0;
}
        

input[type=submit] {
    background-color:transparent;
    border:0;
        }
input[type=text] {
    background-color:transparent;
    border:0;
        }

        .style2
        {
            width: 38%;
        }

    </style>

    <table "cellpadding="0" cellspacing="0" width="700">
        <tr valign="top">
            <td align="left" style="border-bottom: 1px solid #FFD275; height: 10px;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Fill in the following details"> </asp:Label>
            </td>
        </tr>
    </table>  
    
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <div id="div1" runat="server">
                            <asp:LinkButton ID="lnkPersonalDetails" runat="server" Text="Personal Details" 
                                Font-Bold="True" CausesValidation="false"
                                Font-Underline="True" onclick="lnkPersonalDetails_Click"> </asp:LinkButton>
                            <b>| </b>
                            <asp:LinkButton ID="lnkBankDetails" runat="server" Text="Bank Details" 
                                Font-Bold="True" CausesValidation="false"
                                Font-Underline="True" onclick="lnkBankDetails_Click"> </asp:LinkButton>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divPersonalDetails" runat="server">
                            <asp:UpdatePanel ID="updatePnl" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSavePersonalDetails" />
                                </Triggers>
                                <ContentTemplate>            
                                    <legend >Personal Details </legend>                
                                        <table cellspacing="3" cellpadding="0" width="100%" align = "center" border="0"style="vertical-align: top">
                                            <tr>
                                                <td width = "20%" align = "left" Class = "style2">
                                                    <strong>Country</strong>
                                                </td>
                                                <td align = "center" width = 1%">
                                                    <b>:</b></td>
                                                <td align = "left" width = "79%">
                                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack = "True" onselectedindexchanged="ddlCountry_SelectedIndexChanged" >
                                                    </asp:DropDownList>
                                                    <strong><span style = "color: #ff0000">*</span></strong>
                                                    <asp:RequiredFieldValidator ID="rfvCountry" runat="server" ControlToValidate = "ddlCountry" 
                                                    ErrorMessage="Select Country" Style="position: relative; left: 0px;" 
                                                    Display="None" ValidationGroup = "vgPersonalDetails"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width = "20%" align = "left" Class = "style2">
                                                    <strong>State</strong>
                                                </td>
                                                <td align = "center" width = 1%">
                                                    <b>:</b></td>
                                                <td align = "left" width = "79%">
                                                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack = "True" 
                                                        onselectedindexchanged="ddlState_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <strong><span style = "color: #ff0000">*</span></strong>
                                                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate = "ddlState" 
                                                    ErrorMessage="Select State" Style="position: relative; left: 0px;" 
                                                    Display="None" ValidationGroup = "vgPersonalDetails"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width = "20%" align = "left" Class = "style2">
                                                    <strong>District</strong>
                                                </td>
                                                <td align = "center" width = 1%">
                                                    <b>:</b></td>
                                                <td align = "left" width = "79%">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack = "True" 
                                                        onselectedindexchanged="ddlDistrict_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <strong><span style = "color: #ff0000">*</span></strong>
                                                    <asp:RequiredFieldValidator ID="rfvDistrict" runat="server" ControlToValidate = "ddlDistrict" 
                                                    ErrorMessage="Select District" Style="position: relative; left: 0px;" 
                                                    Display="None" ValidationGroup = "vgPersonalDetails"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width = "20%" align = "left" Class = "style2">
                                                    <strong>Tehisl</strong>
                                                </td>
                                                <td align = "center" width = 1%">
                                                    <b>:</b></td>
                                                <td align = "left" width = "79%">
                                                    <asp:DropDownList ID="ddlTehsil" runat="server" AutoPostBack = "true">
                                                    </asp:DropDownList>
                                                    <strong><span style = "color: #ff0000">*</span></strong>
                                                    <asp:RequiredFieldValidator ID="rfvTehsil" runat="server" ControlToValidate = "ddlTehsil" 
                                                    ErrorMessage="Select Tehsil" Style="position: relative; left: 0px;" 
                                                    Display="None" ValidationGroup = "vgPersonalDetails" ></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width = "20%" align = "left" Class = "style2">
                                                    <strong>Other Tehsil</strong>
                                                </td>
                                                <td align = "center" width = 1%">
                                                    <b>:</b></td>
                                                <td align = "left" width = "79%">
                                                    <div class = "wrapper">
                                                        <asp:TextBox ID="txtOTehsil" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width = "20%" align = "left" Class = "style2">
                                                    <strong>City</strong>
                                                </td>
                                                <td align = "center" width = 1%">
                                                    <b>:</b></td>
                                                <td align = "left" width = "79%">
                                                    <div class = "wrapper">
                                                        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width = "20%" align = "left" Class = "style2">
                                                    <strong>Address</strong>
                                                </td>
                                                <td align = "center" width = 1%">
                                                    <b>:</b></td>
                                                <td align = "left" width = "79%">
                                                    <div class = "wrapper">
                                                        <asp:TextBox ID="txtRAddress" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width = "20%" align = "left" Class = "style2">
                                                    <strong>PIN Code</strong>
                                                </td>
                                                <td align = "center" width = 1%">
                                                    <b>:</b></td>
                                                <td align = "left" width = "79%">
                                                    <div class = "wrapper">
                                                        <asp:TextBox ID="txtPinCode" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width = "20%" align = "left" Class = "style2">
                                                    <strong>Whatsapp Number</strong>
                                                </td>
                                                <td align = "center" width = 1%">
                                                    <b>:</b></td>
                                                <td align = "left" width = "79%">
                                                    <div class = "wrapper">
                                                        <asp:TextBox ID="txtWhatsappNumber" runat="server"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>  
                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        HeaderText="Please correct following errors" ShowSummary="False"
                                        meta:resourcekey="ValidationSummary1Resource1" DisplayMode = "BulletList" ValidationGroup = "vgPersonalDetails" />              
                                        <strong>
                                            <asp:Button ID="btnSavePersonalDetails" runat="server"  Text="Save" BackColor="#0066CC" 
                                            ForeColor="White" onclick="btnSavePersonalDetails_Click" ValidationGroup = "vgPersonalDetails"/>
                                        <br />
                                        </strong>
                                    
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                        </div>
                        <br />
                        <div id  = "divGridView" runat = "server">
                            <asp:GridView ID="gvBankPending" runat="server" AutoGenerateColumns="true">

                            </asp:GridView>
                            <asp:Label ID="lblMsg" runat="server" Font-Bold = "true"></asp:Label>
                        </div>
                        
                        <br />
                        <div  id = "divBankDetails" runat = "server">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID = "btnSaveBankDetails" />
                                </Triggers>
                                <ContentTemplate>
                                    <legend >Bank Details </legend>
                                    <table cellspacing="3" cellpadding="0" width="100%" align = "center" border="0"style="vertical-align: top">
                                        <tr>
                                            <td width = "20%" align="left" class="style2">
                                                <strong>IFSC Code</strong></td>
                                            <td align="center" width="1%">
                                                <b>:</b></td>
                                            <td width="79%" align="left">
                                                <div class = "wrapper">
                                                    <asp:TextBox ID="txtIFSCcode" runat="server" CssClass = "wrapper"></asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass = "wrapper" 
                                                        onclick="btnSearch_Click"/>
                                                </div>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvIFSCCode" runat="server"
                                                    ControlToValidate="txtIFSCcode" ErrorMessage="Enter IFSC Code"
                                                    Style="position: relative" Display="None" ValidationGroup = "vgBankDetails"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align = "left" class="style2" width="20%">
                                                <strong>Bank Account Number</strong></td>
                                            <td align="center" width="1%">
                                                <b>:</b></td>
                                            <td width = "79%" align = "left">
                                                <div class = "wrapper">
                                                    <asp:TextBox ID="txtBankaccnum" runat="server" CssClass = "wrapper"></asp:TextBox>
                                                </div>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvBankAccountNumber" runat="server"
                                                    ControlToValidate="txtBankaccnum" ErrorMessage="Enter Bank Account Number"
                                                    Style="position: relative" Display="None" ValidationGroup = "vgBankDetails"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align = "left" class="style2" width="20%">
                                                <strong>Account Holder</strong></td>
                                            <td align="center" width="1%">
                                                <b>:</b></td>
                                            <td width = "79%" align = "left">
                                                <div class = "wrapper">
                                                    <asp:TextBox ID="txtAccountholder" runat="server"></asp:TextBox>
                                                </div>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvAccountHolderName" runat="server"
                                                    ControlToValidate="txtAccountholder" ErrorMessage="Enter Account Holder Name"
                                                    Style="position: relative" Display="None" ValidationGroup = "vgBankDetails"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align = "left" class="style2" width="20%">
                                                <strong>Bank Name</strong></td>
                                            <td align="center" width="1%">
                                                <b>:</b></td>
                                            <td width = "79%" align = "left">
                                                <div class = "wrapper">
                                                    <asp:TextBox ID="txtBankname" runat="server"></asp:TextBox>
                                                </div>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvBankName" runat="server"
                                                    ControlToValidate="txtBankname" ErrorMessage="Enter Bank Name"
                                                    Style="position: relative" Display="None" ValidationGroup = "vgBankDetails"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align = "left" class="style2" width="20%">
                                                <strong>Branch Name</strong></td>
                                            <td align="center" width="1%">
                                                <b>:</b></td>
                                            <td width = "79%" align = "left">
                                                <div class = "wrapper">
                                                    <asp:TextBox ID="txtBranchname" runat="server"></asp:TextBox>
                                                </div>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvBranchName" runat="server"
                                                    ControlToValidate="txtBranchname" ErrorMessage="Enter Branch Name"
                                                    Style="position: relative" Display="None" ValidationGroup = "vgBankDetails"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align = "left" class="style2" width="20%">
                                                <strong>MICR</strong></td>
                                            <td align="center" width="1%">
                                                <b>:</b></td>
                                            <td width = "79%" align ="left">
                                                <div class = "wrapper">
                                                    <asp:TextBox ID="txtMICR" runat="server"></asp:TextBox>
                                                </div>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvMICR" runat="server"
                                                    ControlToValidate="txtMICR" ErrorMessage="Enter MICR"
                                                    Style="position: relative" Display="None" ValidationGroup = "vgBankDetails"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align = "left" class="style2" width="20%">
                                                <strong>Address</strong></td>
                                            <td align="center" width="1%">
                                                <b>:</b></td>
                                            <td width = "79%" align ="left">
                                                <div class = "wrapper">
                                                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                    <table align = "center" width = "100%">
                                    <asp:Label ID="lblMsgIFSCCode" runat="server" Font-Bold = "true"></asp:Label>
                                    </table>
                                    <strong>
                                        
                                        <asp:Button ID="btnSaveBankDetails" runat="server" Text="Save" BackColor="#0066CC" 
                                        ForeColor="White" onclick="btnSaveBankDetails_Click" ValidationGroup = "vgBankDetails" />
                                    </strong>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                        HeaderText="Please correct following errors" ShowSummary="False" ValidationGroup = "vgBankDetails"
                                        meta:resourcekey="ValidationSummary1Resource1" DisplayMode = "BulletList" />              
                                </ContentTemplate>                                
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>