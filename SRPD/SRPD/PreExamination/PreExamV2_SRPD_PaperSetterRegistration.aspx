<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_SRPD_PaperSetterRegistration.aspx.cs"
    Inherits="SRPD.PreExamination.PreExamV2_SRPD_PaperSetterRegistration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table cellpadding="0" cellspacing="0" width="700">
        <tr valign="top">
            <td align="left" style="border-bottom: 1px solid #FFD275; height: 10px;">
                <asp:Label ID="lblPageHead" runat="server" Text="Paper Setter / Teacher Registration">
                </asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <table cellspacing="0" cellpadding="0" width="700" border="0">
            <tr>
                <td>
                    <div id="divPersonalInfo" runat="server">
                        <asp:UpdatePanel ID="updatePnl" runat="server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>
                                <table style="width: 100%;" bgcolor="White" frame="void">
                                    <tr>
                                        <td width="35%" align="right">
                                            <strong>
                                                <asp:Label ID="lblFName" runat="server" Text="First Name :"></asp:Label>
                                            </strong>
                                        </td>
                                        <td width="35%" align="left">
                                            <asp:TextBox ID="txtFName" runat="server" AutoPostBack="true"></asp:TextBox>
                                            &nbsp; <strong><span style="color: #ff0000">*</span></strong><asp:RequiredFieldValidator
                                                ID="rfvFName" runat="server" ControlToValidate="txtFName" ErrorMessage="Enter First Name"
                                                Style="position: relative" Display="None"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="35%" align="right">
                                            <strong>
                                                <asp:Label ID="lblMName" runat="server" Text="Middle Name :"></asp:Label>
                                            </strong>
                                        </td>
                                        <td width="35%" align="left">
                                            <asp:TextBox ID="txtMName" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="35%" align="right">
                                            <strong>
                                                <asp:Label ID="lblLName" runat="server" Text="Last Name :"></asp:Label>
                                            </strong>
                                        </td>
                                        <td width="35%" align="left">
                                            <asp:TextBox ID="txtLName" runat="server"></asp:TextBox>
                                            <strong><span style="color: #ff0000">*</span></strong>
                                            <asp:RequiredFieldValidator ID="rfvLName" runat="server" ControlToValidate="txtLName"
                                                ErrorMessage="Enter Last Name" Style="position: relative" Display="None"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="35%" align="right">
                                            <strong>
                                                <asp:Label ID="lblMobileNumber" runat="server" Text="Mobile Number :"></asp:Label>
                                            </strong>
                                        </td>
                                        <td width="35%" align="left">
                                            <asp:TextBox ID="txtMobileNumber" runat="server"></asp:TextBox>
                                            <strong><span style="color: #ff0000">*</span></strong>
                                            <asp:RequiredFieldValidator ID="rfvMobileNumber" runat="server" ControlToValidate="txtMobileNumber"
                                                ErrorMessage="Enter Mobile Number" Style="position: relative; left: 0px;"
                                                Display="None"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="35%" align="right">
                                            <strong>
                                                <asp:Label ID="lblEmailid" runat="server" Text="E-Mail ID :"></asp:Label>
                                            </strong>
                                        </td>
                                        <td width="35%" align="left">
                                            <asp:TextBox ID="txtEmailid" runat="server"></asp:TextBox>
                                            <strong><span style="color: #ff0000">*</span></strong>
                                            <asp:RequiredFieldValidator ID="rfvEmailID" runat="server" ControlToValidate="txtEmailid"
                                                ErrorMessage="Enter Email-ID" Style="position: relative; left: 0px;"
                                                Display="None"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr id="trButton" runat="server" style="font-size: 12pt">
                                        <td align="left" colspan="3" style="font-weight: bold; vertical-align: middle; text-align: center; height: 26px;"
                                            valign="middle" runat="server">
                                            <strong>
                                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="butsp" OnClick="btnSave_Click" />
                                            </strong>
                                        </td>
                                    </tr>
                                    <tr id="trMsg" runat="server">
                                        <td id="Td1" style="font-weight: normal; vertical-align: middle; text-align: left; height: 21px;"
                                            valign="middle" align="left" colspan="3" runat="server">Note : &nbsp;<span style="color: #ff0000">* <span style="color: black">marked fields
                                            are mandatory.</span></span>&nbsp;
                                        </td>
                                    </tr>
                                    <tr id="trlblMsg" runat="server" style="font-size: 12pt">
                                        <td id="Td2" style="font-weight: normal; vertical-align: middle; text-align: left; height: 17px;"
                                            valign="middle" align="left" colspan="3" runat="server">
                                            <asp:Label ID="lblMsg" runat="server" Visible="true" CssClass="errorNote"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    HeaderText="Please correct following errors" ShowSummary="False"
                                    meta:resourcekey="ValidationSummary1Resource1" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvRegistration" runat="server" CssClass="clGrid" AutoGenerateColumns="false" Width="90%">
            <Columns>


                <asp:TemplateField HeaderText="Sl No">
                    <ItemTemplate>
                        <%# Container.DataItemIndex + 1 %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="First Name">
                    <ItemTemplate>
                        <%# Eval("First_Name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Middle Name">
                    <ItemTemplate>
                        <%# Eval("Middle_Name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name">
                    <ItemTemplate>
                        <%# Eval("Last_Name") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Mobile_Number">
                    <ItemTemplate>
                        <%# Eval("Mobile_Number") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email_ID">
                    <ItemTemplate>
                        <%# Eval("Email_ID") %>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <rowstyle cssclass="gridItem" />
        <headerstyle cssclass="gridHeader" bordercolor="White" borderstyle="Solid" borderwidth="1px" />
    </center>
</asp:Content>
