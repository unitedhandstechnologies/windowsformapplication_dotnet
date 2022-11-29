<%@ Page Title="" Language="C#" MasterPageFile="~/New_Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_SRPD_DashBoard__8.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SRPD_DashBoard__8" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <table style="height: 30px" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td style="height: 17px; width: 100%" valign="top" align="left">
                    <div id="divPaperCount" class="well well-sm" style="font-weight: bold; height: 30px;
                        width: 100%;">
                        Download Pending Paper Count :
                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
        </table>
        <div class="row mb-2">
            <center>
                <asp:Button ID="btnExport" runat="server" CssClass="btn btn-info" Text="Export To Excel"
                    OnClick="btnExport_Click"></asp:Button>
                &nbsp; &nbsp; &nbsp;
                <asp:Button ID="btnBack" runat="server" CssClass="btn btn-info" Text="Back" OnClick="btnBack_Click">
                </asp:Button>
            </center>
        </div>
        <div class="row mb-2">
            <div class="table-wrapper col-md-12">
                <asp:GridView ID="gvDownloadPendingPaperCount" runat="server" Width="100%" CssClass="clGrid"
                    OnRowDataBound="gvDownloadPendingPaperCount_RowDataBound" AutoGenerateColumns="False"
                    AllowPaging="true" PageSize="25" OnPageIndexChanging="gvDownloadPendingPaperCount_PageIndexChanging">
                    <RowStyle CssClass="gridItem" Wrap="True"></RowStyle>
                    <HeaderStyle CssClass="gridHeader" Font-Bold="True" Wrap="false" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Sr.No." meta:resourcekey="TemplateFieldResource1">
                            <ItemTemplate>
                                <%# (Container.DataItemIndex)+1 %>
                            </ItemTemplate>
                            <ItemStyle Width="3%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Inst_Code" HeaderText="Venue Code"></asp:BoundField>
                        <asp:BoundField DataField="Inst_Name" HeaderText="Venue Name"></asp:BoundField>
                        <asp:BoundField DataField="Course_Name" HeaderText="Program Name"></asp:BoundField>
                        <asp:BoundField DataField="Pp_Code" HeaderText="Paper code"></asp:BoundField>
                        <asp:BoundField DataField="Pp_Name" HeaderText="Paper Name"></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                    </EmptyDataTemplate>
                    <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                </asp:GridView>
            </div>
        </div>
        <table style="height: 30px" cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr id="trNote" runat="server" visible="false">
                <td align="center">
                    <span style="color: Red">
                        <asp:Label ID="lblErrorMsg" runat="server" Text="Records Not Found." Visible="false"
                            CssClass="errorNote"></asp:Label></span>
                </td>
            </tr>
        </table>
        <input id="hidUniId" type="hidden" runat="server" />
        <input type="hidden" id="hidDateTime" runat="server" value="0" />
        <input type="hidden" id="hidStartTime" runat="server" value="0" />
        <input type="hidden" id="hidEndTime" runat="server" value="0" />
        <input type="hidden" id="hidPaperCount" runat="server" value="0" />
        <input type="hidden" id="hidFacID" runat="server" value="0" />
        <input type="hidden" id="hidCrID" runat="server" value="0" />
        <input type="hidden" id="hidMolID" runat="server" value="0" />
        <input type="hidden" id="hidPtrnID" runat="server" value="0" />
        <input type="hidden" id="hidBrnID" runat="server" value="0" />
        <input type="hidden" id="hidCrPrDetailsID" runat="server" value="0" />
        <input type="hidden" id="hidCrPrChID" runat="server" value="0" />
        <input type="hidden" id="hidExEvID" runat="server" value="0" />
        <input type="hidden" id="hidPpPpHeadCrPrChID" runat="server" value="0" />
        <input type="hidden" id="hidInstID" runat="server" value="0" />
    </center>
</asp:Content>
