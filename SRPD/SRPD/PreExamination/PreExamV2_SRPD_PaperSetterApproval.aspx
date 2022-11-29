<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SRPD_PaperSetterApproval.aspx.cs" Inherits="SRPD.PreExamV2_SRPD_PaperSetterApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table cellpadding="0" cellspacing="0" width="700">
        <tr valign="top">
            <td align="center" style="border-bottom: 1px solid #FFD275; height: 10px;">
                <strong>
                    <asp:Label ID="lblPageHead" runat="server" Text="Paper Setter Approval"> </asp:Label>
                </strong>
            </td>
        </tr>
    </table>
    <table cellspacing = "3" cellpadding = "0" width = "100%" align = "center" border = "0" style = "vertical-align:top" visible = "false">
        <tr>
            <td align = "left" class = "style2" width = "20%">
                <strong>
                    <asp:Label ID="lblExamEvent" runat="server" Text="Select Exam Event" 
                    Visible="False"></asp:Label>
                </strong></td>
            <td align = "center" style="width: 1%">
                <b>
                    <asp:Label ID="Label2" runat="server" Text=":" Visible="False"></asp:Label> 
                </b></td>
            <td align = "left" width = "79%">
                <asp:DropDownList ID="ddlExamEvent" runat="server" Visible="False" 
                    onselectedindexchanged="ddlExamEvent_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
        onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack = "true" RepeatDirection = "Horizontal">
        <asp:ListItem Value = "ProgramSelectionWise">Program Selection Wise</asp:ListItem>
        <asp:ListItem Value = "PaperCodeWise">Paper Code Wise</asp:ListItem>
        <asp:ListItem Value = "PaperSetterNameWise">Paper Setter Name Wise</asp:ListItem>
        <asp:ListItem Value = "AllSettersWise">All Setter Wise</asp:ListItem>
    </asp:RadioButtonList>
    <table cellspacing = "3" cellpadding = "0" width = "100%" align = "center" border = "0" style = "vertical-align:top" visible = "false">
        <tr>
            <td align = "left" class = "style2" width = "20%">
                <strong>
                    <asp:Label ID="lblSelectFaculty" runat="server" Text="Select Faculty" 
                    Visible="False"></asp:Label>
                </strong></td>
            <td align = "center" style="width: 1%">
                <b>
                    <asp:Label ID="lblColon1" runat="server" Text=":" Visible="False"></asp:Label> 
                </b></td>
            <td align = "left" width = "79%">
                <asp:DropDownList ID="ddlFaculty" runat="server" Visible="False" 
                    onselectedindexchanged="ddlFaculty_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align = "left" class ="style2" width = "20%">
                <strong>
                    <asp:Label ID="lblSelectProgram" runat="server" Text="Select Program" 
                    Visible="False"></asp:Label>
                </strong></td>
            <td align = "center" style="width: 1%">
                <b>
                    <asp:Label ID="lblColon2" runat="server" Text=":" Visible="False"></asp:Label>
                </b></td>
            <td align = "left" width = "79%">
                <asp:DropDownList ID="ddlProgram" runat="server" Visible="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align = "left" class = "style2" width = "20%">
                <strong>
                    <asp:Label ID="lblSelectPart" runat="server" Text="Select Part" 
                    Visible="False"></asp:Label>
                </strong></td>
            <td align = "center" style="width: 1%">
                <b>
                    <asp:Label ID="lblColon3" runat="server" Text=":" Visible="False"></asp:Label>
                </b></td>
            <td align = "left" width = "79%">
                <asp:DropDownList ID="ddlPart" runat="server" Visible="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align ="left" class = "style2" width="20%">
                <strong>
                    <asp:Label ID="lblSelectTerm" runat="server" Text="Select Term" 
                    Visible="False"></asp:Label>
                </strong></td>
            <td align = "center" style="width: 1%">
                <b>
                    <asp:Label ID="lblColon4" runat="server" Text=":" Visible="False"></asp:Label> 
                </b></td>
            <td align = "left" width = "79%">
                <asp:DropDownList ID="ddlTerm" runat="server" Visible="False">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <strong>
        <asp:Button ID="btnSearch" runat="server" Text="Search" 
        onclick="btnSearch_Click" visible = "false"/>
    </strong>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns = "false" Width = "100%">
    <Columns>  
                <asp:TemplateField HeaderText="Sr.No.">  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txtSrNo" runat="server"></asp:TextBox>  
                    </EditItemTemplate>  
                    <ItemTemplate>  
                        <asp:Label ID="lblSrNo" runat="server" Text="1"></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="College Code">  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txtCollegeCode" runat="server"></asp:TextBox>  
                    </EditItemTemplate>  
                    <ItemTemplate>  
                        <asp:Label ID="lblCollegeCode" runat="server" Text="College01"></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="College Name">  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txtCollegeName" runat="server"></asp:TextBox>  
                    </EditItemTemplate>  
                    <ItemTemplate>  
                          <asp:Label ID="lblCollegeName" runat="server" Text="SKCET"></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>  
                <asp:TemplateField HeaderText="Paper Setter Name">  
                    <EditItemTemplate>  
                        <asp:TextBox ID="txtPaperSetterName" runat="server"></asp:TextBox>  
                    </EditItemTemplate>  
                    <ItemTemplate>  
                        <asp:Label ID="lblPaperSetterName" runat="server" Text="Sindhu"></asp:Label>  
                    </ItemTemplate>  
                </asp:TemplateField>
                <asp:TemplateField HeaderText = "Program Full Name" >
                    <EditItemTemplate>
                        <asp:TextBox ID="txtProgramFullName" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblProgramFullName" runat="server" Text="ProgramFullName"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText = "Paper Code">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPaperCode" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPaperCode" runat="server" Text="PaperCode"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField> 
                <asp:TemplateField HeaderText = "Paper Name">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPaperName" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPaperName" runat="server" Text="PaperName"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText = "Registration Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRegistrationDate" runat="server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRegistrationDate" runat="server" Text="RegistrationDate"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText = "Approve and Assign Role">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtApproveandAssignRole" runat = "server"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:DropDownList ID="ddlRole" runat="server">
                            <asp:ListItem Value = "-1">---Select---</asp:ListItem>
                            <asp:ListItem Value = "Chairman">Chairman</asp:ListItem>
                            <asp:ListItem Value = "Paper Setter">Paper Setter</asp:ListItem>
                            <asp:ListItem Value = "Reject">Reject</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>  
    </asp:GridView>
    <asp:Button ID="btnSave" runat="server" Text="Save" Visible =false />
    <br />
</asp:Content>