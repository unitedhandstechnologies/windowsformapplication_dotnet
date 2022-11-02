<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SecureQuestionPaper_VenueConfiguration.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SecureQuestionPaper_VenueConfiguration" %>
<%@ Register Src="WebCtrl/PreExamV2_EventWiseCourseSelection.ascx" TagName="PreExamV2_EventWiseCourseSelection"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<center>
        <table cellpadding="0" cellspacing="0" width="700" height="30">
            <tr valign="top">
                <td align="left" style="border-bottom: 1px solid #FFD275;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Secure Question Paper Venue Configuration">
                    </asp:Label>
                    <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" ForeColor="black"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <div runat="server" style="text-align: right" id="divMSG" visible="false">
            <asp:Label ID="lblMSG" CssClass="saveNote" runat="server"></asp:Label>
        </div>
        <table cellspacing="0" cellpadding="0" width="700" border="0">
            <tr valign="top">
                <td valign="top" align="left">
                    <table id="tblFacultySearch" runat="server" width="700">
                        <tr>
                            <td colspan="3">
                                <fieldset id="fldAdvanceSearch" class="fieldset" runat="server">
                                    <legend>Select </legend>
                                    <uc1:preexamv2_eventwisecourseselection id="PreExamV2_EventWiseCourseSelection1"
                                        runat="server" />
                                </fieldset>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div id="divConfig" runat="server" style="display: none; text-align: left">
            <fieldset>
                <legend>SRPD Configuration</legend>
                <table id="tblConfig" runat="server" cellspacing="0" cellpadding="0" width="700"
                    border="0" visible="false">
                    <tr>
                        <td align="right" colspan="3">
                            <asp:Label ID="lblNote" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <b>Do you want to configure SRDP for Venue :</b>
                        </td>
                        <td align="left" style="height: 51px" colspan="2">                            
                            <asp:RadioButtonList ID="rbtnDataStatus" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Table">
                                <asp:ListItem Value="0">With Student Data or </asp:ListItem>
                                <asp:ListItem Value="1">without Student Data?</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr valign="top" align="center">
                        <td colspan="3">
                            <asp:Button ID="btnSave" runat="server" CssClass="ButSp" Text="Save Configuration"
                                onclick="btnSave_Click"></asp:Button>
                        </td>
                    </tr>
                </table>
                <br />
                <table>
                    <tr>
                        <td>
                            <br />
                            <div align="left">
                                <asp:Label ID="lbl" runat="server"><strong>Instruction :</strong> </asp:Label>
                                <ul>
                                    <li>If the student data is available, then SRPD can be configured for with or without
                                        Student data. </li>
                                    <li>If the student data is not available, then SRPD can be configured only for venue
                                        without Student data. </li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </div>
        <input id="hidExamEventID" runat="server" type="hidden" name="hidExamEventID" />
        <input id="hidFacID" runat="server" type="hidden" name="hidFacID" />
        <input id="hidCrID" runat="server" type="hidden" name="hidCrID" />
        <input id="hidMoLrnID" runat="server" type="hidden" name="hidMoLrnID" />
        <input id="hidPtrnID" runat="server" type="hidden" name="hidPtrnID" />
        <input id="hidBrnID" runat="server" type="hidden" name="hidBrnID" />
        <input id="hidCrPrDetailsID" runat="server" type="hidden" name="hidCrPrDetailsID" />
        <input id="hidCrPrChID" runat="server" type="hidden" name="hidCrPrChID" />
        <input id="hidIsEventOpen" runat="server" type="hidden" />
        <input id="hidCourseDetails" runat="server" type="hidden" name="hidCourseDetails" />
        <asp:Label ID="lblCr" runat="server" Text="Course" Style="display: none" meta:resourcekey="lblCrResource1">
        </asp:Label>
    </center>
</asp:Content>
