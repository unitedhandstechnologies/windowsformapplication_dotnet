<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_SRPD_ImportPaperVenueFromExcel.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SRPD_ImportPaperVenueFromExcel" %>

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
                <td valign="top" align="center" width="700">
                    <div id="divLink" runat="server">
                        <asp:LinkButton ID="lnkNewRequest" runat="server" Text="Import From Excel" Font-Bold="True"
                            Font-Underline="True" OnClick="lnkNewRequest_Click" meta:resourcekey="lnkNewRequestResource1"
                            CausesValidation="False"></asp:LinkButton><b> | </b>
                        <asp:LinkButton ID="lnkExistingReq" runat="server" Text="Publish" Font-Bold="True"
                            Font-Underline="True" OnClick="lnkExistingReq_Click" meta:resourcekey="lnkExistingReqResource1"
                            CausesValidation="False"></asp:LinkButton>
                    </div>
                </td>
            </tr>
        </table>
        <asp:MultiView ID="mltv_Main" runat="server">
            <asp:View ID="ViewImport" runat="server">
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
                                                <table runat="server" id="tblEvent" style="width: 100%">
                                                    <tr runat="server" id="trExamEvent">
                                                        <td id="Td1" runat="server" style="font-weight: bold; vertical-align: middle; width: 402px;
                                                            text-align: right" valign="middle" align="left">
                                                            Exam Event
                                                        </td>
                                                        <td id="Td2" runat="server" style="font-weight: bold; vertical-align: middle; width: 2px;
                                                            text-align: right" valign="middle" align="left">
                                                            :
                                                        </td>
                                                        <td id="Td3" runat="server" style="width: 479px">
                                                            <span style="color: #ff0000"><span style="color: #ff0000">
                                                                <asp:DropDownList runat="server" ID="ddlExamEvent">
                                                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                                                </asp:DropDownList>
                                                                *</span></span>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="Tr1">
                                                        <td id="Td7" runat="server" style="font-weight: bold; vertical-align: middle; width: 402px;
                                                            height: 15px; text-align: right" valign="middle" align="left">
                                                        </td>
                                                        <td id="Td8" runat="server" style="font-weight: bold; vertical-align: middle; width: 2px;
                                                            height: 15px; text-align: right" valign="middle" align="left">
                                                        </td>
                                                        <td id="Td9" runat="server" style="width: 479px; height: 15px">
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="Tr2">
                                                        <td id="Td10" runat="server" style="font-weight: bold; vertical-align: middle; text-align: center"
                                                            valign="middle" align="left" colspan="4">
                                                            <asp:Button runat="server" Text="Proceed" CssClass="ButSp" TabIndex="19" Width="150px"
                                                                ID="btnExportToExcel" OnClick="btnEventProceed_Click"></asp:Button>
                                                        </td>
                                                    </tr>
                                                    <tr runat="server" id="Tr3">
                                                        <td id="Td11" runat="server" style="font-weight: normal; vertical-align: middle;
                                                            height: 21px; text-align: left" valign="middle" align="left" colspan="4">
                                                            Note : &nbsp;<span style="color: #ff0000">* <span style="color: black">marked fields
                                                                are mandatory.</span></span>&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                                <%--<uc2:PreExamV2_EventWiseCourseSelection ID="CrSelectionCtrl" runat="server"></uc2:PreExamV2_EventWiseCourseSelection>--%>
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
                                        <input id="hidExamEvent" type="hidden" runat="server" />
                                        <input id="hidCrPrChID" type="hidden" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ViewPublish" runat="server">
                <div id="div1" runat="server">
                    <fieldset>
                        <legend><b>Selection</b></legend>
                        <uc2:PreExamV2_EventWiseCourseSelection ID="CrSelectionCtrl" runat="server"></uc2:PreExamV2_EventWiseCourseSelection>
                    </fieldset>
                </div>
                <div id="divGrid" runat="server" style="display: inline; width: 700">
                    <table cellpadding="0" cellspacing="0" width="700" height="20px">
                        <tr>
                            <td align="right">
                                <b>
                                    <asp:Label ID="lblMessgae" runat="server" Text="" runat="server" Visible="false"></asp:Label>
                                </b>
                            </td>
                        </tr>
                    </table>
                    <asp:GridView ID="gvPaperVenue" CssClass="clGrid" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" AllowPaging="true" OnSorting="gvPaperVenue_Sorting"
                        Style="position: relative" Width="700px" PageSize="20" meta:resourcekey="gvCourseResource1"
                        DataKeyNames="pk_Fac_ID,pk_Cr_ID,pk_MoLrn_ID,pk_Ptrn_ID,pk_Brn_ID,pk_CrPr_Details_ID,pk_CrPrCh_ID,pk_ExamEvent_ID,pk_Pp_PpHead_CrPrCh_ID,fk_Center_ID,fk_Inst_ID"
                        EnableModelValidation="True" OnRowCommand="gvPaperVenue_RowCommand" OnPageIndexChanging="gvPaperVenue_PageIndexChanging"
                        OnRowDataBound="gvPaperVenue_RowDataBound">
                        <RowStyle CssClass="gridItem" />
                        <HeaderStyle CssClass="gridHeader" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr.No.">
                                <ItemTemplate>
                                    <%# Container.DisplayIndex + 1 %>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" />
                                <ItemStyle Font-Bold="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ExamCode" SortExpression="ExamCode" HeaderText="Exam Code" />
                            <asp:BoundField DataField="Paper_Code" HeaderText="Paper Code" SortExpression="Paper_Code">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Center_Code" HeaderText="Center Code" SortExpression="Center_Code">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Venue_Code" HeaderText="Venue Code" SortExpression="Venue_Code">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Published" HeaderText="Status"></asp:BoundField>
                            <asp:TemplateField HeaderText="Remove ">
                                <ItemTemplate>
                                    <b>
                                        <center>
                                            <asp:LinkButton ID="lnkbtnRemove" runat="server" CausesValidation="False" Style="color: Blue"
                                                CommandName="Remove" CommandArgument='<%# Container.DisplayIndex %>' Text="Remove"></asp:LinkButton></center>
                                    </b>
                                </ItemTemplate>
                                <ItemStyle Width="15%" HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <br />
                    <asp:Button runat="server" Text="Publish" CssClass="ButSp" TabIndex="19" Width="150px"
                        Visible="false" ID="BtnPublish" OnClick="btnPublish_Click"></asp:Button>
                    <br />
                </div>
            </asp:View>
        </asp:MultiView>
    </center>
</asp:Content>
