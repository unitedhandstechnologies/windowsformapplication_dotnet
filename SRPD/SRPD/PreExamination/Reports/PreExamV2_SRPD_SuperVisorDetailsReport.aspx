<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_SRPD_SuperVisorDetailsReport.aspx.cs" Inherits="SRPD.PreExamination.Reports.PreExamV2_SRPD_SuperVisorDetailsReport"
    meta:resourcekey="PageResource1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        var myElement;
        function callValidate() {

            var message = '';
            var ExEvIndex = document.getElementById('<%=ddlExamEvent.ClientID%>').selectedIndex;
            if (ExEvIndex == 0) {
                message += "<li> Please Select Exam Event.</li><br>";

            }
            if (message != '') {
                showValidationSummary(myElement, message);
                return false;
            }



        } 

    </script>
    <center>
        <table cellpadding="0" cellspacing="0" width="700" height="30px">
            <tr valign="top">
                <td align="left" style="border-bottom: 1px solid #FFD275;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Secure Question Paper SuperVisor Event wise details report "
                        meta:resourcekey="lblPageHeadResource1"></asp:Label>
                    <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" ForeColor="Black" meta:resourcekey="lblSubHeaderResource1"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table cellspacing="0" cellpadding="0" width="700" border="0">
            <tr>
                <td valign="top" align="left" width="700">
                    <table>
                        <tr valign="top">
                            <!-- Main Content Starts-->
                            <td align="center">
                                <table style="width: 80%">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trGrv" runat="server">
                                        <td style="width: 706px; height: 110px; text-align: left" align="left" valign="top">
                                            <fieldset id="ShowUploadedPaperDetails" runat="server" width="100%">
                                                <table width="700" runat="server" id="tblExamEvent">
                                                    <tr id="Tr1" runat="server">
                                                        <td id="Td2" style="height: 28px; width: 17%;" align="right" runat="server">
                                                            <strong>Select Examination Event</strong>
                                                        </td>
                                                        <td id="Td3" style="height: 28px; width: 2%;" align="right" runat="server">
                                                            :&nbsp;
                                                        </td>
                                                        <td id="Td4" style="height: 28px" align="left" width="49%" runat="server">
                                                            <asp:DropDownList ID="ddlExamEvent" runat="server" Width="50%" meta:resourcekey="ddlExamEventResource1">
                                                                <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource1">---Select---</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <font class="Mandatory">* </font>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="3">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr2" runat="server">
                                                        <td id="Td5" style="height: 18px" align="center" colspan="3" runat="server">
                                                            &nbsp;<asp:Button ID="btnExport" runat="server" CssClass="ButSp" Text="Export to Excel"
                                                                Width="110px" OnClientClick="return callValidate();" OnClick="btnExport_Click"
                                                                meta:resourcekey="btnExportResource1" />
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr3" runat="server">
                                                        <td id="Td6" style="height: 17px" width="700" colspan="3" runat="server">
                                                            <p align="left">
                                                                <strong><i>Note:</i></strong> <font class="Mandatory">*</font> marked fields are
                                                                mandatory.
                                                            </p>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <!-- Main Content Ends-->
                        </tr>
                        <tr>
                            <td style="width: 753px">
                                <input id="hidEventID" type="hidden" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
