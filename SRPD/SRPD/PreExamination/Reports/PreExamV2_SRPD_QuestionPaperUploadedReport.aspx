<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_SRPD_QuestionPaperUploadedReport.aspx.cs" Inherits="SRPD.PreExamination.Reports.PreExamV2_SRPD_QuestionPaperUploadedReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">
        var myElement;
        function callValidate() {
            var message = '';
            var ExEvIndex = document.getElementById('<%=ddlExamEvent.ClientID%>').selectedIndex;
            var txtDate = document.getElementById('<%=txtDate.ClientID%>').value;
            var txtToDate = document.getElementById('<%=txtToDate.ClientID%>').value;
            if (ExEvIndex == 0) {
                message += "<li> Please Select Exam Event.</li><br>";
            }
            if ((document.getElementById('<%=txtToDate.ClientID%>').value != '') && (document.getElementById('<%=txtDate.ClientID%>').value == '')) {
                message = "<li> Please Select From Date.</li><br>";
            }

            if ((document.getElementById('<%=txtToDate.ClientID%>').value != '') && txtDate > txtToDate) {
                message += "End Date should be greater or equal to the Start Date.<br />";
            }
            if (message != '') {
                showValidationSummary(myElement, message);
                return false;
            }
        }

        function checkDate(sender, args) {
            var theDay = sender._selectedDate.getDay();
            if (theDay == 0) {
                if (confirm("The selected date is sunday/Holiday.\n Do you still want to continue!"))
                { return true; }

                else {
                    document.getElementById('<%=txtDate.ClientID %>').value = '';
                    return false;
                }
            }
        }

    </script>
    <center>
        <table cellpadding="0" cellspacing="0" width="700" height="30px">
            <tr valign="top">
                <td align="left" style="border-bottom: 1px solid #FFD275;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Secure Question Paper Uploaded Report "></asp:Label>
                    <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
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
                                            <asp:Label ID="lblMsg" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trGrv" runat="server">
                                        <td style="width: 706px; height: 110px; text-align: left" align="left" valign="top">
                                            <asp:Label ID="lblMesg" runat="server" meta:resourcekey="lblMesgResource1"></asp:Label>
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
                                                            <asp:DropDownList ID="ddlExamEvent" runat="server" Width="50%">
                                                                <asp:ListItem Value="0" Selected="True">---Select---</asp:ListItem>
                                                            </asp:DropDownList>
                                                            <font class="Mandatory">* </font>
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr5" runat="server">
                                                        <td id="Td8" style="height: 28px; width: 17%;" align="right" runat="server">
                                                            <strong>Select From Date</strong>
                                                        </td>
                                                        <td id="Td9" style="height: 28px; width: 2%;" align="right" runat="server">
                                                            :&nbsp;
                                                        </td>
                                                        <td id="Td11" style="height: 28px" align="left" width="49%" runat="server">
                                                            <asp:TextBox ID="txtDate" runat="server" Width="20%" Text='<%# DateTime.Now.ToShortDateString() %>'></asp:TextBox>
                                                            <span style="vertical-align: bottom"></span><span style="vertical-align: middle">[yyyy/mm/dd]</span>
                                                            <ajaxtoolkit:calendarextender runat="server" format="yyyy-MM-dd" popupbuttonid="imgCal"
                                                                enabled="True" targetcontrolid="txtDate" id="CalendarExtender1" enableviewstate="False">
                                                            </ajaxtoolkit:calendarextender>
                                                            <b>To Date :</b>
                                                            <asp:TextBox ID="txtToDate" runat="server" Height="16px" Width="20%"></asp:TextBox>
                                                            <span style="vertical-align: middle">[yyyy/mm/dd]</span>
                                                            <ajaxtoolkit:calendarextender runat="server" format="yyyy-MM-dd" popupbuttonid="imgCal"
                                                                enabled="True" targetcontrolid="txtToDate" id="CalendarExtender2" enableviewstate="False">
                                                            </ajaxtoolkit:calendarextender>
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
                                                                Width="110px" OnClick="btnExport_Click" OnClientClick="return callValidate();" />
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
                                <input id="hidPageDescription" type="hidden" runat="server" />
                                <input id="hidExamEvent" type="hidden" runat="server" />
                                <input id="hidStartDate" type="hidden" runat="server" />
                                <input id="hidEndDate" type="hidden" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
