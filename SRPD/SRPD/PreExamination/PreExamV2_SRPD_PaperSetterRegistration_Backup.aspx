<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SRPD_PaperSetterRegistration.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SRPD_PaperSetterRegistration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table "cellpadding="0" cellspacing="0" width="700">
        <tr valign="top">
            <td align="left" style="border-bottom: 1px solid #FFD275; height: 10px;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Paper Setter / Teacher Registration">
                    </asp:Label>
            </td>
        </tr>
    </table>
                    <br />
    <table cellspacing="0" cellpadding="0" width="700" border="0">
        <tr>
            <td>
                <fieldset id="fldAdvanceSearch" class="fieldset" runat="server">
                    <legend>Personal Information </legend>
                    <table style="width: 100%;" bgcolor="White" frame="void">
                        <tr>
                            <td width="35%" align="right">
                                <strong>
                                    <asp:Label ID="lblFName" runat="server" Text="First Name :"></asp:Label>
                                    
                                </strong>
                                    
                            </td>
                            <td width = "35%" align = "left">
                                <asp:TextBox ID="txtFName" runat="server"></asp:TextBox>
                                <strong><span style="color: #ff0000">*</span></strong>
                            </td>
                        </tr>
                        <tr>
                            <td width = "35%" align = "right">
                                <strong>
                                    <asp:Label ID="lblMName" runat="server" Text="Middle Name :"></asp:Label>
                                    
                                </strong>
                            </td>
                            <td width = "35%" align = "left">  
                                <asp:TextBox ID="txtMName" runat="server"></asp:TextBox>
                                <strong><span style="color: #ff0000">*</span></strong>
                            </td>
                        </tr>
                        <tr>
                            <td width = "35%" align = "right">
                                <strong>
                                    <asp:Label ID="lblLName" runat="server" Text="Last Name :"></asp:Label>
                                    
                                </strong>
                            </td>
                            <td width = "35%" align = "left">  
                                <asp:TextBox ID="txtLName" runat="server"></asp:TextBox>
                                <strong><span style="color: #ff0000">*</span></strong>
                            </td>
                        </tr>
                        <tr>
                            <td width = "35%" align ="right">
                                <strong>
                                    <asp:Label ID="lblMobileNumber" runat="server" Text="Mobile Number :"></asp:Label>
                                    
                                </strong>
                            </td>
                            <td width = "35%" align  = "left">
                                <asp:TextBox ID="txtMobileNumber" runat="server"></asp:TextBox>
                                <strong><span style="color: #ff0000">*</span></strong>
                            </td>
                        </tr>
                        <tr>
                            <td width = "35%" align ="right">
                                <strong>
                                    <asp:Label ID="lblEmailid" runat="server" Text="E-Mail ID :"></asp:Label>
                                    
                                </strong>
                            </td>
                            <td width = "35%" align = "left">
                                <asp:TextBox ID="txtEmailid" runat="server"></asp:TextBox>
                                <strong><span style="color: #ff0000">*</span></strong>
                            </td>
                        </tr>
                    </table>
                </fieldset>                
            </td>
        </tr>
    </table>
    <table>
        <tr id="trMsg" runat="server">
            <td id="Td1" style="font-weight: normal; vertical-align: middle; text-align: left; height: 21px;" valign="middle" align="left" colspan="3" runat="server">
                                Note : &nbsp;<span style="color: #ff0000">* <span style="color: black">marked fields
                                    are mandatory.</span></span>&nbsp;
            </td>
         </tr>
    </table>
    <strong>
    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
    </strong>

    <asp:GridView ID="GridView1" runat="server" Enabled="False">
    </asp:GridView>
</asp:Content>
