<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SRPD_PaperSetterApproval_M.aspx.cs"
    Inherits="SRPD.PreExamination.PreExamV2_SRPD_PaperSetterApproval_M" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table cellpadding="0" cellspacing="0" width="700">
            <tr valign="top">
                <td align="center" style="border-bottom: 1px solid #FFD275; height: 10px;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Paper Setter Approval"> </asp:Label>
                </td>
            </tr>
        </table>

        <div>
            <table width="100%">
                <tr>
                    <td align="left" class="style2" width="20%">
                        <strong>
                            <asp:Label ID="lblExamEvent" runat="server" Text="Select Exam Event"></asp:Label>
                        </strong></td>
                    <td align="center" style="width: 1%">
                        <b>
                            <asp:Label ID="lblColon1" runat="server" Text=":"></asp:Label>
                        </b></td>
                    <td align="left" width="79%">
                        <asp:DropDownList ID="ddlExamEvent" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlExamEvent_SelectedIndexChanged">
                        </asp:DropDownList>
                        <strong><span style = "color: #ff0000">*</span></strong>
                        <asp:RequiredFieldValidator ID="rfvExamEvent" runat="server" ControlToValidate = "ddlExamEvent"
                        ErrorMessage="Select Exam Event" InitialValue = "-1" Style="position: relative; left: 0px;"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
        
        <div>
            <table>
                <tr>
                    <td width="100%">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true"
                            RepeatDirection="Horizontal"
                            OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                            <asp:ListItem Value="ProgramSelectionWise" Selected = "True">Program Selection Wise</asp:ListItem>
                            <asp:ListItem Value="PaperCodeWise">Paper Code Wise</asp:ListItem>
                            <asp:ListItem Value="PaperSetterNameWise">Paper Setter Name Wise</asp:ListItem>
                            <asp:ListItem Value="AllSettersWise">All Setter Wise</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table>
                <tr>
                    <td>
                        <div id="ProgramSelectionWise" runat="server">
                            <asp:UpdatePanel ID="updatePnlProgramSelectionWise" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSearch" />
                                </Triggers>
                                <ContentTemplate>
                                    <table cellspacing="3" cellpadding="0" width="100%" align="center" border="0" style="vertical-align: top">
                                        <tr>
                                            <td align="left" class="style2" width="20%">
                                                <strong>
                                                    <asp:Label ID="lblSelectFaculty" runat="server" Text="Select Faculty"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <b>
                                                    <asp:Label ID="lblColon2" runat="server" Text=":"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left" width="79%">
                                                <asp:DropDownList ID="ddlFaculty" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvFaculty" runat="server" ControlToValidate="ddlFaculty"
                                                    ErrorMessage="Select Faculty" InitialValue="-1" Style="position: relative; left: 0px;"
                                                    Display="None" ValidationGroup="vgProgramSelectionWise"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="style2" width="20%">
                                                <strong>
                                                    <asp:Label ID="lblSelectCourse" runat="server" Text="Select Course"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <b>
                                                    <asp:Label ID="lblColon3" runat="server" Text=":"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left" width="79%">
                                                <asp:DropDownList ID="ddlCourse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCourse_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvCourse" runat="server" ControlToValidate="ddlCourse"
                                                    ErrorMessage="Select Course" InitialValue="-1" Style="position: relative; left: 0px;"
                                                    Display="None" ValidationGroup="vgProgramSelectionWise"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="style2" width="20%">
                                                <strong>
                                                    <asp:Label ID="lblBranch" runat="server" Text="Select Branch"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <b>
                                                    <asp:Label ID="lblColon4" runat="server" Text=":"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left" width="79%">
                                                <asp:DropDownList ID="ddlBranch" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvBranch" runat="server" ControlToValidate="ddlBranch"
                                                    ErrorMessage="Select Branch" InitialValue="-1" Style="position: relative; left: 0px;"
                                                    Display="None" ValidationGroup="vgProgramSelectionWise"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="style2" width="20%">
                                                <strong>
                                                    <asp:Label ID="lblSelectPart" runat="server" Text="Select Part"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <b>
                                                    <asp:Label ID="lblColon5" runat="server" Text=":"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left" width="79%">
                                                <asp:DropDownList ID="ddlPart" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPart_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvPart" runat="server" ControlToValidate="ddlPart"
                                                    ErrorMessage="Select Part" InitialValue="-1" Style="position: relative; left: 0px;"
                                                    Display="None" ValidationGroup="vgProgramSelectionWise"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="style2" width="20%">
                                                <strong>
                                                    <asp:Label ID="lblSelectTerm" runat="server" Text="Select Part Term"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <b>
                                                    <asp:Label ID="lblColon6" runat="server" Text=":"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left" width="79%">
                                                <asp:DropDownList ID="ddlTerm" runat="server" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged" AutoPostBack = "true">
                                                </asp:DropDownList>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvTerm" runat="server" ControlToValidate="ddlTerm"
                                                    ErrorMessage="Select Term" InitialValue="-1" Style="position: relative; left: 0px;"
                                                    Display="None" ValidationGroup="vgProgramSelectionWise"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" class="style2" width="20%">
                                                <strong>
                                                    <asp:Label ID="lblPaper" runat="server" Text="Select Paper"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <b>
                                                    <asp:Label ID="lblColon7" runat="server" Text=":"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left" width="79%">
                                                <asp:DropDownList ID="ddlPaper" runat="server">
                                                </asp:DropDownList>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvPaper" runat="server" ControlToValidate="ddlPaper"
                                                    ErrorMessage="Select Paper" InitialValue="-1" Style="position: relative; left: 0px;"
                                                    Display="None" ValidationGroup=""></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <strong>
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                                            ValidationGroup="vgProgramSelectionWise" />
                                    </strong>
                                    <table>
                                        <tr id="trMsg" runat="server">
                                            <td id="Td1" style="font-weight: normal; vertical-align: middle; text-align: left;
                                                height: 21px;" valign="middle" align="left" colspan="3" runat="server">
                                                Note : &nbsp;<span style="color: #ff0000">* <span style="color: black">marked fields
                                                    are mandatory.</span></span>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        HeaderText="Please correct following errors" ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1"
                                        ValidationGroup="vgProgramSelectionWise" />
                                    <input type="hidden" runat="server" id="hidInstID" />
                                    <input type="hidden" runat="server" id="hidEventID" />
                                    <input type="hidden" runat="server" id="hidFacultyID" />
                                    <input type="hidden" runat="server" id="hidCourseID" />
                                    <input type="hidden" runat="server" id="hidMoLrnID" />
                                    <input type="hidden" runat="server" id="hidPtrnID" />
                                    <input type="hidden" runat="server" id="hidBranchID" />
                                    <input type="hidden" runat="server" id="hidCoursePartID" />
                                    <input type="hidden" runat="server" id="hidCoursePartTermID" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="PaperCodeWise" runat="server">
                            <asp:UpdatePanel ID="updatePnlPaperCodeWise" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSearchPaperCodeWise" />
                                </Triggers>
                                <ContentTemplate>
                                    <table cellspacing="3" cellpadding="0" width="100%" align="center" border="0" style="vertical-align: top">
                                        <tr>
                                            <td align="left" class="style2" width="20%">
                                                <strong>
                                                    <asp:Label ID="lblPaperCode" runat="server" Text="Enter Paper Code"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <b>
                                                    <asp:Label ID="lblColon8" runat="server" Text=":"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left" width="79%">
                                                <asp:TextBox ID="txtPaperCode" runat="server"></asp:TextBox>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvPaperCode" runat="server" ControlToValidate="txtPaperCode"
                                                    ErrorMessage="Enter Paper Code" Style="position: relative; left: 0px;" Display="None"
                                                    ValidationGroup="vgPaperCodeWise"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <strong>
                                        <asp:Button ID="btnSearchPaperCodeWise" runat="server" Text="Search" ValidationGroup="vgPaperCodeWise"
                                            OnClick="btnSearchPaperCodeWise_Click" />
                                    </strong>
                                    <table>
                                        <tr id="tr1" runat="server">
                                            <td id="Td2" style="font-weight: normal; vertical-align: middle; text-align: left;
                                                height: 21px;" valign="middle" align="left" colspan="3" runat="server">
                                                Note : &nbsp;<span style="color: #ff0000">* <span style="color: black">marked fields
                                                    are mandatory.</span></span>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                                        HeaderText="Please correct following errors" ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1"
                                        ValidationGroup="vgPaperCodeWise" />
                                    <input type="hidden" runat="server" id="hidPaperCode" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="PaperSetterNameWise" runat="server">
                            <asp:UpdatePanel ID="updatePnlPaperSetterNameWise" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSearchPaperSetterNameWise" />
                                </Triggers>
                                <ContentTemplate>
                                    <table cellspacing="3" cellpadding="0" width="100%" align="center" border="0" style="vertical-align: top">
                                        <tr>
                                            <td align="left" class="style2" width="20%">
                                                <strong>
                                                    <asp:Label ID="lblPaperSetterName" runat="server" Text="Enter Paper Setter Name"></asp:Label>
                                                </strong>
                                            </td>
                                            <td align="center" style="width: 1%">
                                                <b>
                                                    <asp:Label ID="lblColon9" runat="server" Text=":"></asp:Label>
                                                </b>
                                            </td>
                                            <td align="left" width="79%">
                                                <asp:TextBox ID="txtPaperSetterName" runat="server"></asp:TextBox>
                                                <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvPaperSetterName" runat="server" ControlToValidate="txtPaperSetterName"
                                                    ErrorMessage="Enter Paper Setter Name" Style="position: relative; left: 0px;" Display="None"
                                                    ValidationGroup="vgPaperSetterNameWise"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                    <strong>
                                        <asp:Button ID="btnSearchPaperSetterNameWise" runat="server" Text="Search" 
                                        ValidationGroup="vgPaperSetterNameWise" 
                                        onclick="btnSearchPaperSetterNameWise_Click" />
                                    </strong>
                                    <table>
                                        <tr id="tr2" runat="server">
                                            <td id="Td3" style="font-weight: normal; vertical-align: middle; text-align: left;
                                                height: 21px;" valign="middle" align="left" colspan="3" runat="server">
                                                Note : &nbsp;<span style="color: #ff0000">* <span style="color: black">marked fields
                                                    are mandatory.</span></span>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True"
                                        HeaderText="Please correct following errors" ShowSummary="False" meta:resourcekey="ValidationSummary1Resource1"
                                        ValidationGroup="vgPaperSetterNameWise" />
                                    <input type="hidden" runat="server" id="hidPaperSetterName" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div id="GridViewandButton" runat="server">
                            <table>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblmsg" runat="server" Font-Bold="true" />
                                    </td>
                                </tr>
                            </table>
                            <asp:GridView ID="gvProgSelectionWise" runat="server" AutoGenerateColumns="false"
                                Width="100%" >
                                <%--pk_Uni_ID	fk_PpSetterId	fk_Fac_ID	fk_Cr_ID	fk_MoLrn_ID	fk_Ptrn_ID	fk_Brn_ID	fk_CrPr_Details_ID	fk_CrPrCh_ID	fk_ExEv_ID	fk_Pp_ID	
                                            PaperName	pk_CourseName	
                                            fk_Pp_PpHead_CrPrCh_ID	pk_Inst_ID	Is_Approved	APPROVEDSTATUS	Approved_By	pk_Inst_ID	Inst_Name	PaperSetterName	RegistrationDate--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="College Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCollegeCode" runat="server" Text='<%# Eval("pk_Inst_ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="College Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCollegeName" runat="server" Text='<%# Eval("Inst_Name") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paper Setter Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaperSetterName" runat="server" Text='<%# Eval("PaperSetterName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Program Full Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProgramFullName" runat="server" Text='<%# Eval("pk_CourseName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paper Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaperCode" runat="server" Text='<%# Eval("fk_Pp_ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paper Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaperName" runat="server" Text='<%# Eval("PaperName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Registration Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegistrationDate" runat="server" Text='<%# Eval("RegistrationDate") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPpSetterID" runat="server" Text='<%# Eval("fk_PpSetterId") %>' Visible = "false"/>
                                            <asp:HiddenField ID="hidPpSetterID" runat = "server" Value = '<%# Eval("fk_PpSetterId") %>' />
                                            <asp:CheckBox ID="chkSelectRole" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelectRole_CheckedChanged" />
                                            <%--<strong><span style="color: #ff0000">*</span></strong>
                                             <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRole"
                                                ErrorMessage="Select Role" InitialValue="-1" Style="position: relative; left: 0px;"
                                                meta:resourcekey="rfvRoleResource1"></asp:RequiredFieldValidator>--%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div>
                        <asp:GridView ID="gvUpdateRole" runat="server" AutoGenerateColumns="false"
                                Width="100%" OnRowDataBound="gvUpdateRole_OnRowDataBound">
                                <%--pk_Uni_ID	fk_PpSetterId	fk_Fac_ID	fk_Cr_ID	fk_MoLrn_ID	fk_Ptrn_ID	fk_Brn_ID	fk_CrPr_Details_ID	fk_CrPrCh_ID	fk_ExEv_ID	fk_Pp_ID	
                                            PaperName	pk_CourseName	
                                            fk_Pp_PpHead_CrPrCh_ID	pk_Inst_ID	Is_Approved	APPROVEDSTATUS	Approved_By	pk_Inst_ID	Inst_Name	PaperSetterName	RegistrationDate--%>
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr.No.">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="College Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCollegeCode" runat="server" Text='<%# Eval("pk_Inst_ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="College Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCollegeName" runat="server" Text='<%# Eval("Inst_Name") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paper Setter Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaperSetterName" runat="server" Text='<%# Eval("PaperSetterName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Program Full Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProgramFullName" runat="server" Text='<%# Eval("pk_CourseName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paper Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaperCode" runat="server" Text='<%# Eval("fk_Pp_ID") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paper Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaperName" runat="server" Text='<%# Eval("PaperName") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Registration Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegistrationDate" runat="server" Text='<%# Eval("RegistrationDate") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approve and Assign Role">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlRole" runat="server" Visible="true" >
                                            </asp:DropDownList>
                                            
                                             <strong><span style="color: #ff0000">*</span></strong>
                                            <asp:RequiredFieldValidator ID="rfvRole" runat="server" ControlToValidate="ddlRole"
                                                ErrorMessage="Select Role" InitialValue="-1" Style="position: relative; left: 0px;"
                                                meta:resourcekey="rfvRoleResource1"></asp:RequiredFieldValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <asp:Label ID="lblSaveMsg" runat="server" Font-Bold="true"></asp:Label>
                        <asp:Button ID="btnBack" runat="server" Text="Back" onclick="btnBack_Click" Visible = "false" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btnSave" OnClick="btnSave_Click"
                            Visible="false" />
                    </td>
                </tr>
            </table>
        </div>

    </center>


</asp:Content>
