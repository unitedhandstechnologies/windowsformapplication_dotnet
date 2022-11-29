<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_SRPD_QuestionPaperDownloadReport.aspx.cs" Inherits="SRPD.PreExamination.Reports.PreExamV2_SRPD_QuestionPaperDownloadReport"
    EnableEventValidation="false" ValidateRequest="false" Culture="auto" meta:resourcekey="PageResource1"
    UICulture="auto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table cellpadding="0" cellspacing="0" width="700" height="30px">
            <tr valign="top">
                <td align="left" style="border-bottom: 1px solid #FFD275;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Secure Question Paper Download Report "></asp:Label>
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
                                            <fieldset id="ShowDownloadDataDetails" runat="server" width="100%">
                                                <table width="100%">
                                                    <tr id="headerConfiguredData" runat="server">
                                                        <td id="Td1" align="right" colspan="2" style="text-align: left; height: 21px;" class="clSubHeading "
                                                            runat="server">
                                                            <span>List Date Time Slot</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="BorderTB">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100%">
                                                            <asp:GridView ID="gvPaperSlot" runat="server" Width="685px" AutoGenerateColumns="False"
                                                                EnableViewState="true" CssClass="clGrid" HorizontalAlign="Center" EnableModelValidation="True"
                                                                DataKeyNames="ExamDateTime,ExamStartTime,ExamDate,ExamEndTime" OnRowCommand="gvPaperSlot_RowCommand">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr.No." meta:resourcekey="TemplateFieldResource1">
                                                                        <ItemTemplate>
                                                                            <%# (Container.DataItemIndex)+1 %>.
                                                                        </ItemTemplate>
                                                                        <ItemStyle VerticalAlign="Middle" Width="5%" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="ExamDateTime" HeaderText="Exam Date Time Slot" ItemStyle-Font-Bold="true"
                                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%" />
                                                                    <asp:TemplateField HeaderText="Select">
                                                                        <ItemTemplate>
                                                                            <center>
                                                                                <asp:LinkButton runat="server" CommandName="Select" ID="lnkSelect" CommandArgument='<%# Container.DisplayIndex %>'><b>Generate Report</b></asp:LinkButton></center>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="10%"></HeaderStyle>
                                                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="gridHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                                                    Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                                                <RowStyle Wrap="True" CssClass="gridItem"></RowStyle>
                                                            </asp:GridView>
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
                                <input runat="server" id="hidVenueID" type="hidden" />
                                <input runat="server" id="hidVenueName" type="hidden" />
                                <input id="hidPageDescription" type="hidden" runat="server" />
                                <input id="hidVenueCode" type="hidden" runat="server" />
                                <input id="hidExamEvent" type="hidden" runat="server" />
                                <input id="hidExamDateTime" type="hidden" runat="server" />
                                <input id="hidExamStartTime" type="hidden" runat="server" />
                                <input id="hidExamEndTime" type="hidden" runat="server" />
                                <input id="hidExamDate" type="hidden" runat="server" />
                                <input id="hidZipFilePwd" type="hidden" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
