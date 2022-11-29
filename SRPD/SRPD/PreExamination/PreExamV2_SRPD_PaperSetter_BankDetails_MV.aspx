<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SRPD_PaperSetter_BankDetails_MV.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SRPD_PaperSetter_BankDetails_MV" %>
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
                    <asp:Label ID="lblPageHead" runat="server" Text="Fill in the following details">
                    </asp:Label>
            </td>
        </tr>
    </table>  
    
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <div id="div1" runat="server">
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="View1" runat="server">
                                <div id="divPersonalDetails" runat="server">
                            <asp:UpdatePanel ID="updatePnl" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSave" />
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
                                                    <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack = "true">
                                                    </asp:DropDownList>
                                                    <strong><span style = "color: #ff0000">*</span></strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width = "20%" align = "left" Class = "style2">
                                                    <strong>State</strong>
                                                </td>
                                                <td align = "center" width = 1%">
                                                    <b>:</b></td>
                                                <td align = "left" width = "79%">
                                                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack = "true">
                                                    </asp:DropDownList>
                                                    <strong><span style = "color: #ff0000">*</span></strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td width = "20%" align = "left" Class = "style2">
                                                    <strong>District</strong>
                                                </td>
                                                <td align = "center" width = 1%">
                                                    <b>:</b></td>
                                                <td align = "left" width = "79%">
                                                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack = "true">
                                                    </asp:DropDownList>
                                                    <strong><span style = "color: #ff0000">*</span></strong>
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
                                                    <strong><span style = "color: #ff0000">*</span></strong>
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
                                                    <strong><span style = "color: #ff0000">*</span></strong>
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
                                                    <strong><span style = "color: #ff0000">*</span></strong>
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
                                                    <strong><span style = "color: #ff0000">*</span></strong>
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
                                                    <strong><span style = "color: #ff0000">*</span></strong>
                                                </td>
                                            </tr>
                                        </table>                
                                        <strong>
                                            <asp:Button ID="btnSave" runat="server" Text="Save" BackColor="#0066CC" 
                                            ForeColor="White" onclick="btnSave_Click"/>
                                        </strong>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        
                                </asp:View>
                                <asp:View ID="View2" runat="server">
                                    <div  id = "divBankDetails" runat = "server">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID = "btnSaveBD" />
                                    <asp:PostBackTrigger ControlID = "btnBack"  />
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
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass = "wrapper"/>
                                                </div>
                                                <strong><span style="color: #ff0000">*</span></strong>
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
                                                <strong><span style="color: #ff0000">*</span></strong>
                                            </td>
                                        </tr>
                                    </table>
                                    <strong>
                                        <asp:Button ID="btnSaveBD" runat="server" Text="Save" BackColor="#0066CC" 
                                        ForeColor="White" />
                                        <asp:Button ID="btnBack" runat="server" Text="Back" 
                                        onclick="btnBack_Click" />
                                    </strong>
                                </ContentTemplate>                                
                            </asp:UpdatePanel>
                        </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
</asp:Content>
