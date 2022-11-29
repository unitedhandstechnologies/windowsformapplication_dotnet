<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SRPD_PaperSetterApproval_M.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SRPD_PaperSetterApproval_M" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<table "cellpadding="0" cellspacing="0" width="700">
        <tr valign="top">
            <td align="Center" style="border-bottom: 1px solid #FFD275; height: 10px;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Paper Setter Approval"> </asp:Label>
            </td>
        </tr>
    </table> 

    <div>
        <table width = "100%">
            <tr>
                <td align = "left" class = "style2" width = "20%">
                <strong>
                    <asp:Label ID="lblExamEvent" runat="server" Text="Select Exam Event" ></asp:Label>
                </strong></td>
                <td align = "center" style="width: 1%">
                    <b>
                        <asp:Label ID="lblColon1" runat="server" Text=":"></asp:Label> 
                    </b></td>
                <td align = "left" width = "79%">
                    <asp:DropDownList ID="ddlExamEvent" runat="server" AutoPostBack = "true" 
                        onselectedindexchanged="ddlExamEvent_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
         </table>
    </div>
    <div>
        <table>
            <tr>
                <td width = "100%">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack = "true" 
                        RepeatDirection = "Horizontal" 
                        onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                        <asp:ListItem Value = "ProgramSelectionWise">Program Selection Wise</asp:ListItem>
                        <asp:ListItem Value = "PaperCodeWise">Paper Code Wise</asp:ListItem>
                        <asp:ListItem Value = "PaperSetterNameWise">Paper Setter Name Wise</asp:ListItem>
                        <asp:ListItem Value = "AllSettersWise">All Setter Wise</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <div id = "ProgramSelectionWise" runat = "server" >
                        <asp:UpdatePanel ID = "updatePnl" runat = "server">
                            <Triggers>
                                <asp:PostBackTrigger ControlID = "btnSearch" />
                            </Triggers>

                            <ContentTemplate>
                                <table cellspacing="3" cellpadding="0" width="100%" align = "center" border="0"style="vertical-align: top">
                                    <tr>
                                        <td align = "left" class = "style2" width = "20%">
                                            <strong>
                                                <asp:Label ID="lblSelectFaculty" runat="server" Text="Select Faculty" ></asp:Label>
                                            </strong></td>
                                        <td align = "center" style="width: 1%">
                                            <b>
                                                <asp:Label ID="lblColon2" runat="server" Text=":" ></asp:Label> 
                                            </b></td>
                                        <td align = "left" width = "79%">
                                            <asp:DropDownList ID="ddlFaculty" runat="server" AutoPostBack = "true"
                                                onselectedindexchanged="ddlFaculty_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align = "left" class ="style2" width = "20%">
                                            <strong>
                                                <asp:Label ID="lblSelectCourse" runat="server" Text="Select Course"></asp:Label>
                                            </strong></td>
                                        <td align = "center" style="width: 1%">
                                            <b>
                                                <asp:Label ID="lblColon3" runat="server" Text=":"></asp:Label>
                                            </b></td>
                                        <td align = "left" width = "79%">
                                            <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack = "true"
                                                onselectedindexchanged="ddlCourse_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align = "left" class ="style2" width = "20%">
                                            <strong>
                                                <asp:Label ID="lblBranch" runat="server" Text="Select Branch"></asp:Label>
                                            </strong></td>
                                        <td align = "center" style="width: 1%">
                                            <b>
                                                <asp:Label ID="lblColon4" runat="server" Text=":"></asp:Label>
                                            </b></td>
                                        <td align = "left" width = "79%">
                                            <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack = "true"
                                                onselectedindexchanged="ddlBranch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align = "left" class = "style2" width = "20%">
                                            <strong>
                                                <asp:Label ID="lblSelectPart" runat="server" Text="Select Part"></asp:Label>
                                            </strong></td>
                                        <td align = "center" style="width: 1%">
                                            <b>
                                                <asp:Label ID="lblColon5" runat="server" Text=":"></asp:Label>
                                            </b></td>
                                        <td align = "left" width = "79%">
                                            <asp:DropDownList ID="ddlPart" runat="server" AutoPostBack = "true"
                                                onselectedindexchanged="ddlPart_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align ="left" class = "style2" width="20%">
                                            <strong>
                                                <asp:Label ID="lblSelectTerm" runat="server" Text="Select Term"></asp:Label>
                                            </strong></td>
                                        <td align = "center" style="width: 1%">
                                            <b>
                                                <asp:Label ID="lblColon6" runat="server" Text=":"></asp:Label> 
                                            </b></td>
                                        <td align = "left" width = "79%">
                                            <asp:DropDownList ID="ddlTerm" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <strong>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" 
                                    onclick="btnSearch_Click"/>
                                </strong>

                                <input type = "hidden" runat = "server" id = "hidInstID" />
                                <input type = "hidden" runat = "server" id = "hidEventID" />
                                <input type = "hidden" runat = "server" id = "hidFacultyID" />
                                <input type = "hidden" runat = "server" id = "hidCourseID" />
                                <input type = "hidden" runat = "server" id = "hidBranchID" />
                                <input type = "hidden" runat = "server" id = "hidPartID" />
                                <input type = "hidden" runat = "server" id = "hidTermID" />
                                </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                                <div id = "GridViewandButton" runat = "server">
                                    <asp:GridView ID="gvSearchData" runat="server" AutoGenerateColumns="false" Width = "100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.No.">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtSrNo" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" runat="server" Text = "1"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="College Code">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCollegeCode" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCollegeCode" runat="server" Text = "CC"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="College Name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtCollegeName" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCollegeName" runat="server" Text = "CN"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Paper Setter Name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPaperSetterName" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaperSetterName" runat="server" Text = "PpSN"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Program Full Name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtProgramFullName" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProgramFullName" runat="server" Text = "PgmFN"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Paper Code">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPaperCode" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaperCode" runat="server" Text = "PpCode"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Paper Name">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtPaperName" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblPaperName" runat="server" Text = "PaperName"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Registration Date">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtRegistrationDate" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRegistrationDate" runat="server" Text = "RD"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approve and Assign Role">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtApproveandAssignRole" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlRole" runat="server">
                                                        <asp:ListItem Value="-1">---Select---</asp:ListItem>
                                                        <asp:ListItem Value="Chairman">Chairman</asp:ListItem>
                                                        <asp:ListItem Value="Paper Setter">Paper Setter</asp:ListItem>
                                                        <asp:ListItem Value="Reject">Reject</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Button ID="btnSave" runat="server" Text="Save"/>
                                </div>
                            
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
