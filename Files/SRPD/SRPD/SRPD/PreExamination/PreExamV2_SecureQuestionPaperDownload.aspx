<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SecureQuestionPaperDownload.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SecureQuestionPaperDownload" %>
<%@ Register Src="WebCtrl/PreExamV2_SearchInstitute.ascx" TagName="PreExamV2_SearchInstitute"   TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table cellpadding="0" cellspacing="0" width="700" height="30">
            <tr valign="top">
                <td align="left" style="border-bottom: 1px solid #FFD275;">
                    <asp:Label ID="lblPageHead" runat="server" 
                        Text="Secure Question Paper Download" meta:resourcekey="lblPageHeadResource1" ></asp:Label>
                    <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" ForeColor="Black" 
                        meta:resourcekey="lblSubHeaderResource1"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table cellspacing="0" cellpadding="0" width="700" border="0">
            <tr>
                <td>
                    <uc2:PreExamV2_SearchInstitute runat="server" ID="PreExamV2_SearchInstitute1"></uc2:PreExamV2_SearchInstitute>
                    <input runat="server" id="hidVenueID" type="hidden" />
                    <input runat="server" id="hidVenueName" type="hidden" />
                    <input id="hidVenueCode" runat="server" type="hidden" />
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
