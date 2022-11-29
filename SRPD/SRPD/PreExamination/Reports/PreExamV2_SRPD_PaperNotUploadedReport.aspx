<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SRPD_PaperNotUploadedReport.aspx.cs" Inherits="SRPD.PreExamination.Reports.PreExamV2_SRPD_PaperNotUploadedReport" 
EnableEventValidation="false" ValidateRequest="false" Culture="auto" UICulture="auto"
%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<link href="<%=Classes.clsGetSettings.SitePath%>CSS/calendar-blue.css" type="text/css"
        rel="stylesheet" />
    <script type="text/javascript" language="javascript" src="<%=Classes.clsGetSettings.SitePath%>jscript/calendar.js"></script>
    <script type="text/javascript" language="javascript" src="<%=Classes.clsGetSettings.SitePath%>jscript/calendar-en.js"></script>
    <script type="text/javascript" language="javascript" src="<%=Classes.clsGetSettings.SitePath%>jscript/InitCalendarFunc.js"></script>
    <script type="text/javascript" language="javascript" src="<%=Classes.clsGetSettings.SitePath%>jscript/DatePickerJs.js"></script>
    <script language="javascript" type="text/javascript">
        var myElement;
        var myElement1;

        function callValidate() {

            var message = '';
            debugger;
            var strDate = document.getElementById('<%= txtDate.ClientID%>').value;
            var endDate = document.getElementById('<%= txtToDate.ClientID%>').value;

            document.getElementById('<%= hidDate.ClientID%>').value = strDate;
            document.getElementById('<%= hidEndDate.ClientID%>').value = endDate;

            var ExEvIndex = document.getElementById('<%=ddlExamEvent.ClientID%>').selectedIndex;

            if (ExEvIndex == 0) {
                message += "<li> Please Select Exam Event.</li><br>";
            }

            if (strDate == "") {
                message += "<li> Please select Exam From date.</li><br>";
            }

            if (endDate == "") {
                message += "<li> Please select Exam To.</li><br>";
            }

            if (strDate != "" && endDate != "") {
                var status = CompareUserDates(strDate, endDate);

                if (status == false) {
                    message += "<li> From Date should be lower than To Date.</li><br>";
                }
            }


            if (message != '') {
                showValidationSummary(myElement, message);
                return false;
            }
        }
        function callValidate1() {

            var message = '';
            debugger;

            var ExEvIndex = document.getElementById('<%=ddlEvent.ClientID%>').selectedIndex;

            if (ExEvIndex == 0) {
                message += "<li> Please Select Exam Event.</li><br>";
            }

            if (message != '') {
                showValidationSummary(myElement1, message);
                return false;
            }
        }   

    </script>
    <center>
        <table cellpadding="0" cellspacing="0" width="700" height="30px">
            <tr valign="top">
                <td align="left" style="border-bottom: 1px solid #FFD275;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Secure Question Paper Not Uploaded Report "></asp:Label>
                    <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table style="height: 410" cellspacing="0" cellpadding="0" width="700" border="0">
            <tr>
                <td valign="top" align="left" width="2%">
                    &nbsp;
                </td>
                <td valign="top" align="left" width="80%">
                    <table width="700">
                        <tr id="trMsg" runat="server">
                            <td id="Td1" align="right" width="700" style="height: 15px" runat="server">
                                <asp:Label ID="lblMsg" runat="server" Width="700"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <!-- Toolbar Starts-->
                    <div id="divPaperGridView" runat="server">
                        <fieldset>
                            <legend><b>Report for timetable define but paper not uploaded</b></legend>
                            <table width="700" runat="server" id="tblExamEvent">
                                <tr id="Tr1" runat="server">
                                    <td id="Td2" style="height: 28px; width: 17%;" align="right" runat="server">
                                        <strong>Select Exam Event</strong>
                                    </td>
                                    <td id="Td3" style="height: 28px; width: 2%;" align="right" runat="server">
                                        :&nbsp;
                                    </td>
                                    <td id="Td4" style="height: 28px" align="left" width="49%" runat="server">
                                        <asp:DropDownList ID="ddlExamEvent" runat="server">
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
                                        <asp:TextBox ID="txtDate" runat="server" Width="15%"></asp:TextBox>
                                        <span style="vertical-align: bottom"></span><span style="color: #ff0033; vertical-align: bottom">
                                            *</span><span style="vertical-align: middle">[yyyy/mm/dd]</span>
                                        <ajaxtoolkit:calendarextender runat="server" format="yyyy-MM-dd" popupbuttonid="imgCal"
                                            enabled="True" targetcontrolid="txtDate" id="CalendarExtender1" onclientdateselectionchanged="checkDate"
                                            enableviewstate="False">
                                </ajaxtoolkit:calendarextender>
                                        <b>To Date :</b>
                                        <asp:TextBox ID="txtToDate" runat="server" Height="16px" Width="15%"></asp:TextBox>
                                        <span style="color: #ff0033; vertical-align: bottom">*</span><span style="vertical-align: middle">[yyyy/mm/dd]</span>
                                        <ajaxtoolkit:calendarextender runat="server" format="yyyy-MM-dd" popupbuttonid="imgCal"
                                            enabled="True" targetcontrolid="txtToDate" id="CalendarExtender2" onclientdateselectionchanged="checkDate"
                                            enableviewstate="False">
                                </ajaxtoolkit:calendarextender>
                                    </td>
                                </tr>
                                <tr id="Tr2" runat="server">
                                    <td id="Td5" style="height: 18px" align="center" colspan="3" runat="server">
                                        &nbsp;<asp:Button ID="btnSubmit" runat="server" CssClass="ButSp" Text="Generate Report"
                                            Width="100px" OnClick="btnSubmit_Click" OnClientClick="return callValidate();" />&nbsp;
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
                                <tr id="Tr4" runat="server">
                                    <td id="Td7" colspan="3" style="height: 17px" width="700" runat="server">
                                        <input id="hidExamEventName" runat="server" size="1" style="width: 43px; height: 11px"
                                            type="hidden" />
                                        <input id="hidExamEventID" runat="server" size="1" style="width: 43px; height: 11px"
                                            type="hidden" />
                                        <input id="hidEndDate" runat="server" type="hidden" size="1" style="width: 43px;
                                            height: 11px" />
                                        <input id="hidDate" runat="server" type="hidden" size="1" style="width: 43px; height: 11px" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                        <br />
                        <fieldset>
                            <legend><b>Report for timetable not defined</b></legend>
                            <table width="700" runat="server" id="Table1">
                                <tr id="Tr6" runat="server">
                                    <td id="Td10" style="height: 28px; width: 17%;" align="right" runat="server">
                                        <strong>Select Exam Event</strong>
                                    </td>
                                    <td id="Td12" style="height: 28px; width: 2%;" align="right" runat="server">
                                        :&nbsp;
                                    </td>
                                    <td id="Td13" style="height: 28px" align="left" width="49%" runat="server">
                                        <asp:DropDownList ID="ddlEvent" runat="server">
                                            <asp:ListItem Value="0" Selected="True">---Select---</asp:ListItem>
                                        </asp:DropDownList>
                                        <font class="Mandatory">* </font>
                                    </td>
                                </tr>
                                <tr id="Tr9" runat="server">
                                    <td id="Td16" style="height: 18px" align="center" colspan="3" runat="server">
                                        <asp:Button ID="btnGenerate" runat="server" CssClass="ButSp" Text="Generate Report"
                                            Width="100px" OnClientClick="return callValidate1();" OnClick="btnGenerate_Click" />
                                    </td>
                                </tr>
                                <tr id="Tr7" runat="server">
                                    <td id="Td14" style="height: 17px" width="700" colspan="3" runat="server">
                                        <p align="left">
                                            <strong><i>Note:</i></strong> <font class="Mandatory">*</font> marked fields are
                                            mandatory.
                                        </p>
                                    </td>
                                </tr>
                                <tr id="Tr8" runat="server">
                                    <td id="Td15" colspan="3" style="height: 17px" width="700" runat="server">
                                        <input id="hidExamEvent" runat="server" size="1" style="width: 43px; height: 11px"
                                            type="hidden" />
                                        <input id="hidEventID" runat="server" size="1" style="width: 43px; height: 11px"
                                            type="hidden" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
        </table>
    </center>

</asp:Content>
