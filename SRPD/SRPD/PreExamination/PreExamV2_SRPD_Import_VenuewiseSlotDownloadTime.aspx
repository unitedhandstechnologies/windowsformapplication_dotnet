<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_SRPD_Import_VenuewiseSlotDownloadTime.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SRPD_Import_VenuewiseSlotDownloadTime" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPlaceHolder1">
    <style>
        .gridstyle
        {
            font-weight: bold;
            font-size: 12px;
            color: Red;
        }
    </style>
    <center>
        <table cellpadding="0" cellspacing="0" width="700" height="30px">
            <tr valign="top">
                <td align="left" style="border-bottom: 1px solid #FFD275;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Import Venue wise Download Time From Excel "></asp:Label>
                    <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="700" border="0">
            <tr valign="top">
                <td valign="top" align="center">
                    <div id="divUploadFile" align="center" runat="server">
                        <table>
                            <tbody>
                                <tr>
                                    <td align="right" style="width: 100%">
                                        <b>
                                            <asp:LinkButton ID="lnkExistingConfig" runat="server" 
                                            Text="Download Default Or Saved configuration" 
                                            onclick="lnkExistingConfig_Click"></asp:LinkButton></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 100%">
                                        <b>
                                            <asp:Label ID="lblGenerationSequence" runat="server"></asp:Label></b>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 100%">
                                        <br />
                                        <fieldset style="height: 95px; width: 700px" class="styleFieldset">
                                            <asp:Label ID="lblFileError" runat="server" EnableViewState="False" CssClass="errorNote"
                                                Style="text-align: right" Width="100%"></asp:Label><br />
                                            <br />
                                            <div id="divFileUplToHide" runat="server" style="width: 100%">
                                                <asp:FileUpload ID="fileUploadExcel" runat="server" Font-Size="14px"></asp:FileUpload>
                                                <br />
                                                <br />
                                                <asp:Button ID="btnUploadProceed" CssClass="ButSp" runat="server" Text="Proceed"
                                                    OnClick="btnUploadProceed_Click"></asp:Button>
                                            </div>
                                        </fieldset>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <div id="divInfoHolder" style="text-align: left; padding-top: 0px; padding-bottom: 5px;
                                            padding-left: 5px; width: 95%">
                                            <table width="100%" border="0">
                                                <tr>
                                                    <td style="vertical-align: top">
                                                        <b>Note: </b>
                                                        <div id="divContent" runat="server">
                                                            <ul>
                                                                <li>It should be of file format <strong>".xlsx"</strong></li>
                                                                <li>If download from given link then open file and save as ".xlsx" and then import</li>
                                                                <li>Only <strong>Sheet1</strong> will be considered. </li>
                                                                <li>The first row should not be blank and must contain the column heading.</li>
                                                                <li>The first column of the Excel sheet should be <strong>&quot;VenueCode&quot;</strong>&nbsp;
                                                                </li>
                                                                <li>The second column of the Excel sheet should be <strong>&quot;DownloadTime&quot;</strong>&nbsp;
                                                                    in Minutes. </li>
                                                            </ul>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%">
                                        <asp:Label ID="lblMessage" runat="server" EnableViewState="False" CssClass="errorNote"
                                            Style="text-align: right" Width="100%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100%" align="center">
                                        <table style="width: 100%;" align="center" border="0" runat="server" id="tblDiscrepancyStats"
                                            visible="false">
                                            <tbody>
                                                <tr>
                                                    <td align="center">
                                                        <asp:GridView ID="oGvDetails" runat="server" Width="70%" AutoGenerateColumns="False"
                                                            CssClass="clGrid" OnRowDataBound="oGvDetails_RowDataBound">
                                                            <FooterStyle Font-Bold="True" ForeColor="White"></FooterStyle>
                                                            <RowStyle></RowStyle>
                                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="SubHeading">
                                                            </EmptyDataRowStyle>
                                                            <Columns>
                                                                <asp:BoundField DataField="Section" HeaderText="Section">
                                                                    <HeaderStyle Width="15%" VerticalAlign="Top"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NoOfRecords" HeaderText="No Of Records">
                                                                    <HeaderStyle Width="15%" VerticalAlign="Top"></HeaderStyle>
                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" Wrap="False" BackColor="#2461BF"
                                                                ForeColor="White"></PagerStyle>
                                                            <HeaderStyle Font-Bold="True" BorderStyle="None" CssClass="gridHeader"></HeaderStyle>
                                                            <EditRowStyle BackColor="#2461BF"></EditRowStyle>
                                                        </asp:GridView>
                                                        <br />
                                                        <div id="divGrvMsg" runat="server" style="display: none; text-align: right">
                                                            <asp:Label ID="lblGrvMsg" runat="server" CssClass="errorNote" Text="Please Correct above issues from grid marked in red."></asp:Label>
                                                        </div>
                                                        <br />
                                                       <%-- <asp:Button ID="btnGetDetails" runat="server" Text="Generate Report" CssClass="ButSp"
                                                            OnClick="btnGetDetails_Click" />--%>
                                                        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" Enabled="false" CssClass="ButSp"
                                                            OnClick="btnConfirm_Click" />
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButSp" OnClick="btnCancel_Click" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 16px" colspan="6">
                                                    </td>
                                                </tr>
                                                <%--<tr>
                                                            <td>
                                                                <rsweb:ReportViewer ID="rvGetDiscrepancyStats" runat="server" Font-Size="8pt" Font-Names="Verdana"
                                                                    ProcessingMode="Remote" Visible="false">
                                                                </rsweb:ReportViewer>
                                                                <rsweb:ReportViewer ID="rvGetDiscrepancyStatsCode" runat="server" Font-Size="8pt"
                                                                    Font-Names="Verdana" ProcessingMode="Remote" Visible="false">
                                                                </rsweb:ReportViewer>
                                                            </td>
                                                        </tr>--%>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
