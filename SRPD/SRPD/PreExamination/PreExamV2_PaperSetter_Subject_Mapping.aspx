<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Home.Master" CodeBehind="PreExamV2_PaperSetter_Subject_Mapping.aspx.cs"
    Inherits="SRPD.PreExamination.PreExamV2_PaperSetter_Subject_Mapping" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="WebCtrl/PreExamV2_EventWiseCourseSelection.ascx" TagName="PreExamV2_EventWiseCourseSelection"
    TagPrefix="uc2" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table cellpadding="0" cellspacing="0" width="700" height="30px">
            <tr valign="top">
                <td align="left" style="border-bottom: 1px solid #FFD275;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Select Paper Wise Sbject "></asp:Label>
                    <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                </td>
            </tr>
        </table>
        <br />

        <%--<table>
            <tr>
                <td>
                     <uc2:PreExamV2_EventWiseCourseSelection ID="CrSelectionCtrl" runat="server"></uc2:PreExamV2_EventWiseCourseSelection>
                </td>
            </tr>
        </table> --%>
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <div id="div1" runat="server">
                            <asp:LinkButton ID="lnkNewRequest" runat="server" Text="New Request" Font-Bold="True" CausesValidation="false"
                                Font-Underline="True" OnClick="lnkNewRequest_Click">
                            </asp:LinkButton>
                            <b>| </b>
                            <asp:LinkButton ID="lnkExistingReq" runat="server" Text="List Existing Request" Font-Bold="True" CausesValidation="false"
                                Font-Underline="True" OnClick="lnkExistingReq_Click">
                            </asp:LinkButton>
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
                        <div id="divDropDown" runat="server">
                            <asp:UpdatePanel ID="updatePnl" runat="server">
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                </Triggers>
                                <ContentTemplate>
                                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td align="right" width="35%"></td>
                                            <td align="center" width="2%"></td>
                                            <td align="right">
                                                <asp:Label ID="lblError" runat="server" CssClass="errorNote"
                                                    Style="position: relative" meta:resourcekey="lblErrorResource1"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="height: 25px" id="tr1" runat="server">
                                            <td style="text-align: right;">
                                                <strong>Select Exam Event</strong>
                                            </td>
                                            <td width="1%" align="center">
                                                <strong>: </strong>
                                            </td>
                                            <td style="text-align: left; width: 60%">
                                                <asp:DropDownList ID="ddlEvent" runat="server"
                                                    Style="position: relative; top: 0px; left: 0px;" AutoPostBack="True"
                                                    TabIndex="7" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged"
                                                    meta:resourcekey="ddlEventResource1">
                                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource6">--- Select ---</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp; <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvExamEvnt" runat="server" ControlToValidate="ddlEvent"
                                                    ErrorMessage="Select Exam Event" InitialValue="-1" Style="position: relative; left: 0px;"
                                                    Display="None" meta:resourcekey="rfvExamEvntResource1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr style="height: 25px">
                                            <td width="35%" align="right">
                                                <strong>
                                                    <asp:Label ID="lblSelectFac" runat="server" Text="Select Faculty"
                                                        meta:resourcekey="lblSelectFacResource1"></asp:Label>
                                                </strong>
                                            </td>
                                            <td width="1%" align="center">
                                                <strong>: </strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlFaculty" runat="server" TabIndex="2" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"
                                                    meta:resourcekey="ddlFacultyResource1">
                                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource1">--- Select ---</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp; <strong><span style="color: #ff0000">*</span></strong><asp:RequiredFieldValidator
                                                    ID="rfvFaculty" runat="server" ControlToValidate="ddlFaculty" ErrorMessage="Select Faculty"
                                                    InitialValue="-1" Style="position: relative" Display="None"
                                                    meta:resourcekey="rfvFacultyResource1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr style="height: 25px">
                                            <td width="35%" align="right">
                                                <strong>
                                                    <asp:Label ID="lblSelectCr" runat="server" Text="Select Course"
                                                        meta:resourcekey="lblSelectCrResource1"></asp:Label></strong>
                                            </td>
                                            <td width="1%" align="center">
                                                <strong>: </strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlCourseName" runat="server" TabIndex="3" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlCourseName_SelectedIndexChanged"
                                                    meta:resourcekey="ddlCourseNameResource1">
                                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource2">--- Select ---</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp; <strong><span style="color: #ff0000">*</span></strong><asp:RequiredFieldValidator
                                                    ID="rfvCourse" runat="server" ControlToValidate="ddlCourseName" ErrorMessage="Select Course"
                                                    InitialValue="-1" Style="position: relative" Display="None"
                                                    meta:resourcekey="rfvCourseResource1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr style="height: 25px">
                                            <td width="35%" align="right">
                                                <strong>Select Branch </strong>
                                            </td>
                                            <td width="1%" align="center">
                                                <strong>: </strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="4" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged"
                                                    meta:resourcekey="ddlBranchResource1">
                                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource3">--- Select ---</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp; <strong><span style="color: #ff0000">*</span></strong><asp:RequiredFieldValidator
                                                    ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" ErrorMessage="Select Branch"
                                                    InitialValue="-1" Style="position: relative" Display="None"
                                                    meta:resourcekey="rfvBranchResource1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr style="height: 25px">
                                            <td width="35%" align="right">
                                                <strong>
                                                    <asp:Label ID="lblSelectCrPart" runat="server" Text="Select Course Part"
                                                        meta:resourcekey="lblSelectCrPartResource1"></asp:Label>
                                                </strong>
                                            </td>
                                            <td width="1%" align="center">
                                                <strong>: </strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlCoursePart" runat="server" TabIndex="5" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlCoursePart_SelectedIndexChanged"
                                                    meta:resourcekey="ddlCoursePartResource1">
                                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource4">--- Select ---</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp; <strong><span style="color: #ff0000">*</span></strong><asp:RequiredFieldValidator
                                                    ID="rfvCoursePart" runat="server" ControlToValidate="ddlCoursePart" ErrorMessage="Select Course Part"
                                                    InitialValue="-1" Style="position: relative" Display="None"
                                                    meta:resourcekey="rfvCoursePartResource1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr style="height: 25px">
                                            <td width="35%" align="right">
                                                <strong>
                                                    <asp:Label ID="lblSelectCrPartTerm" runat="server"
                                                        Text="Select Course Part Term " meta:resourcekey="lblSelectCrPartTermResource1"></asp:Label></strong>
                                            </td>
                                            <td width="1%" align="center">
                                                <strong>: </strong>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlCoursePartTerm" runat="server" TabIndex="6" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlCoursePartTerm_SelectedIndexChanged"
                                                    meta:resourcekey="ddlCoursePartTermResource1">
                                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource5">--- Select ---</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp; <strong><span style="color: #ff0000">*</span></strong><asp:RequiredFieldValidator
                                                    ID="rfvCoursePartTerm" runat="server"
                                                    ControlToValidate="ddlCoursePartTerm" ErrorMessage="Select Course Part Term"
                                                    InitialValue="-1" Style="position: relative" Display="None"
                                                    meta:resourcekey="rfvCoursePartTermResource1"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        
                                        <tr style="height: 25px" id="trExamEvent" runat="server">
                                            <td style="text-align: right;">
                                                <strong>Select Paper</strong>
                                            </td>
                                            <td width="1%" align="center">
                                                <strong>: </strong>
                                            </td>
                                            <td style="text-align: left; width: 60%">
                                                <asp:DropDownList ID="ddlpaper" runat="server"
                                                    Style="position: relative; top: 0px; left: 0px;" AutoPostBack="True"
                                                    TabIndex="7" OnSelectedIndexChanged="ddlpaper_SelectedIndexChanged"
                                                    meta:resourcekey="ddlEventResource1">
                                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource6">--- Select ---</asp:ListItem>
                                                </asp:DropDownList>
                                                &nbsp; <strong><span style="color: #ff0000">*</span></strong>
                                                <asp:RequiredFieldValidator ID="rfvPaper" runat="server" ControlToValidate="ddlpaper"
                                                    ErrorMessage="Select Paper" InitialValue="-1" Style="position: relative; left: 0px;"
                                                    Display="None"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>

                                        <tr id="trButton" runat="server" style="font-size: 12pt">
                                            <td align="left" colspan="3" style="font-weight: bold; vertical-align: middle; text-align: center; height: 26px;"
                                                valign="middle" runat="server">
                                                <br />
                                                <asp:Button ID="btnSave" runat="server" CssClass="ButSp" Text="Save" OnClick="btnSave_Click"></asp:Button>
                                            </td>
                                        </tr>
                                        <tr id="trMsg" runat="server">
                                            <td style="font-weight: normal; vertical-align: middle; text-align: left; height: 21px;"
                                                valign="middle" align="left" colspan="3" runat="server">Note : &nbsp;<span style="color: #ff0000">* <span style="color: black">marked fields
                                    are mandatory.</span></span>&nbsp;
                                            </td>
                                        </tr>
                                        <tr id="trlblMsg" runat="server" style="font-size: 12pt">
                                            <td style="font-weight: normal; vertical-align: middle; text-align: left; height: 17px;"
                                                valign="middle" align="left" colspan="3" runat="server">
                                                <asp:Label ID="lblMsg" runat="server" Visible="true" CssClass="errorNote"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        HeaderText="Please correct following errors" ShowSummary="False"
                                        meta:resourcekey="ValidationSummary1Resource1" />
                                    <asp:Label ID="lblFac" runat="server" Text="Faculty" Style="display: none"
                                        meta:resourcekey="lblFacResource1"></asp:Label>
                                    <asp:Label ID="lblCr" runat="server" Text="Course" Style="display: none"
                                        meta:resourcekey="lblCrResource1"></asp:Label>

                                    <input type="hidden" runat="server" id="hidFacultyID" />
                                    <input type="hidden" runat="server" id="hidCourseID" />
                                    <input type="hidden" runat="server" id="hidBranchID" />
                                     <input type="hidden" runat="server" id="hidCrPrDetailsID" />
                                    <input type="hidden" runat="server" id="hidCrPrChID" />
                                    <input type="hidden" runat="server" id="hidEventID" />
                                   <input type="hidden" runat="server" id="hidPaperID" />
                                    <input type="hidden" id="hidInstID" runat="server" />
                                    
                  
                                   
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <br />
                        <div id="divGrid" runat="server" style="display: none; width: 700">
                            <asp:GridView ID="gvCoursePaper" CssClass="clGrid" runat="server" AutoGenerateColumns="false" 
                                DataKeyNames="pk_Uni_ID,fk_PpSetterId,fk_Fac_ID,fk_Cr_ID,fk_MoLrn_ID ,fk_Ptrn_ID ,fk_Brn_ID ,fk_CrPr_Details_ID ,fk_CrPrCh_ID,
                                fk_ExEv_ID, fk_Pp_ID" Style="position: relative" Width="700px" PageSize="12"  
                                OnRowCommand="gvCoursePaper_RowCommand" OnDataBound="gvCoursePaper_DataBound" OnRowDeleting="gvCoursePaper_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl No">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                            <asp:HiddenField ID="hidPpSetterId" runat="server" Value=<%# Eval("fk_PpSetterId") %> />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Course Name">
                                        <ItemTemplate>
                                            <%# Eval("pk_CourseName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PaperName">
                                        <ItemTemplate>
                                            <%# Eval("PaperName") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remove Paper"  ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnRemovePaper" runat="server" Text="Remove"  OnClientClick="return confirm('Are you Sure Want To Delete ?');" 
                                                CssClass="butsp" CommandName="Delete" CommandArgument=<%# Eval("fk_PpSetterId") %>  CausesValidation="false"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approve & Rejection Status" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                        <ItemTemplate>
                                          <asp:Label ID="lblApproveStatus" runat="server" Text=<%# Eval("APPROVEDSTATUS") %> Font-Bold="true" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <rowstyle cssclass="gridItem" />
                            <headerstyle cssclass="gridHeader" bordercolor="White" borderstyle="Solid" borderwidth="1px" />
                        </div>

                        
                        
                        
                    </td>
                </tr>
            </table>
        </div>       
    </center>
</asp:Content>
