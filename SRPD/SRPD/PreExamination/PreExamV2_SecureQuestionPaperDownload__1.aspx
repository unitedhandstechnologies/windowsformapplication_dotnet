<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SecureQuestionPaperDownload__1.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SecureQuestionPaperDownload__1" %>
<%@ Register Src="WebCtrl/PreExamV2_EventWiseCourseSelection.ascx" TagName="PreExamV2_EventWiseCourseSelection"
    TagPrefix="uc2" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../ajax/common.ashx"></script>
<script type="text/javascript" src="../ajax/PreExaminationV3.PreExamination.Services.SRVScheduleCenter,PreExaminationV3.ashx"></script>
    <script language="javascript" type="text/javascript">

    </script>
    <center>
        <table cellpadding="0" cellspacing="0" width="700" height="30px">
            <tr valign="top">
                <td align="left" style="border-bottom: 1px solid #FFD275;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Secure Question Paper Upload " meta:resourcekey="lblPageHeadResource1"></asp:Label>
                    <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" ForeColor="Black" meta:resourcekey="lblSubHeaderResource1"></asp:Label>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="700" border="0">
            <tr>
                <td valign="top" align="left" width="700">
                    <table style="width: 683px">
                        <tr valign="top">
                            <td align="right" colspan="3">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <!-- Main Content Starts-->
                            <td>
                                <div id="divCourseSelection" runat="server">
                                    <fieldset>
                                        <legend><b>Selection</b></legend>
                                        <uc2:PreExamV2_EventWiseCourseSelection ID="CrSelectionCtrl" runat="server"></uc2:PreExamV2_EventWiseCourseSelection>
                                    </fieldset>
                                </div>                                
                            </td>
                            <!-- Main Content Ends-->
                        </tr>                      
                        <tr>
                            <td>
                                <input runat="server" id="hidInstID" type="hidden" />
                                <input id="hidEventID" type="hidden" runat="server" />
                                <input id="hidFacultyID" type="hidden" runat="server" />
                                <input id="hidCourseID" type="hidden" runat="server" />
                                <input id="hidMolrnID" type="hidden" runat="server" />
                                <input id="hidPtrnID" type="hidden" runat="server" />
                                <input id="hidBrnID" type="hidden" runat="server" />
                                <input id="hidCrPrDetailsID" type="hidden" runat="server" />
                                <input id="hidCrPrChID" type="hidden" runat="server" />
                                <input runat="server" id="hidVenueID" type="hidden" />
                                <input runat="server" id="hidVenueName" type="hidden" />
                                <input id="hidPageDescription" type="hidden" runat="server" />
                                <input id="hidExamEvent" type="hidden" runat="server" />
                                <input id="hidIsEventOpen" type="hidden" runat="server" />
                                    <input id="hidVenueCode" runat="server" type="hidden" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
