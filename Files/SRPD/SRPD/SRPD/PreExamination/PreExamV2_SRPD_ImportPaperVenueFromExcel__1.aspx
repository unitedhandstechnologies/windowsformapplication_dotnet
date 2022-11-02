<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_SRPD_ImportPaperVenueFromExcel__1.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SRPD_ImportPaperVenueFromExcel__1" %>

<%@ Register Src="WebCtrl/PreExamV2_EventWiseCourseSelection.ascx" TagName="PreExamV2_EventWiseCourseSelection"
    TagPrefix="uc2" %>
    <%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="divUploadFile" align="center" runat="server" style="width: 100%">
        <table style="width: 97%">
            <tbody>
                <tr>
                    <td>
                        <br />
                        <fieldset style="height: 125px;" class="styleFieldset">
                            <asp:Label ID="lblFileError" runat="server" EnableViewState="False" CssClass="errorNote"
                                Style="text-align: right" Width="100%" meta:resourcekey="lblFileErrorResource1"></asp:Label><br />
                            <br />
                            <div id="divFileUplToHide" runat="server">
                                <table style="width: 97%">
                                    <tr>
                                        <td colspan="3" align="center">
                                            <asp:FileUpload ID="fileUploadExcel" runat="server" Font-Size="14px" Width="467px">
                                            </asp:FileUpload>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">
                                            <asp:Button ID="btnUploadProceed" CssClass="But" runat="server" Text="Proceed" OnClick="btnUploadProceed_Click"
                                                meta:resourcekey="btnUploadProceedResource1" OnClientClick="return validation();">
                                            </asp:Button>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td style="height: 5px">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblMessage" runat="server" EnableViewState="False" CssClass="errorNote"
                            Style="text-align: right" Width="100%" meta:resourcekey="lblMessageResource1"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="divInfoHolder" class="clInfoHolder" style="text-align: left; padding-top: 0px;
                            padding-bottom: 5px; padding-left: 5px; width: 95%">
                            <table width="100%" border="0">
                                <tr>
                                    <td style="vertical-align: top; width: 5px">
                                        <b>Note: </b>
                                    </td>
                                    <td>
                                        <div id="divContent" runat="server">
                                            <ul>
                                                <li>It should be of file format <strong>".xls" or ".xlsx"</strong></li>
                                                <li>Only <strong>Sheet1</strong> will be considered. </li>
                                                <li>The first row should not be blank and must contain the column heading.</li>
                                                <li>The no. of columns in the sheet must be <strong>Five</strong>. </li>
                                              <li>The text in the first row first column of the Excel sheet should be <strong>"Paper_Code"</strong>, second column should be <strong>"Center_Code"</strong>, third column should be <strong>"Venue_Code"</strong>, fourth column should be <strong>"Exam_Code"</strong> and fifth column should be <strong>"Student_Count"</strong></li>
                                            </ul>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
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
                                            CssClass="clGrid" >
                                            <FooterStyle Font-Bold="True" ForeColor="White"></FooterStyle>
                                            <RowStyle></RowStyle>
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="SubHeading">
                                            </EmptyDataRowStyle>
                                            <Columns>
                                                <asp:BoundField DataField="Section" HeaderText="Imported Records">
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
                                        <asp:Button ID="btnGetDetails" runat="server" Text="Generate Report" 
                                            CssClass="ButSp" onclick="btnGetDetails_Click"
                                              />
                                        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" Enabled="false" 
                                            CssClass="ButSp" onclick="btnConfirm_Click"
                                             />
                                         <asp:Button ID="btnConfirmAndPublish" runat="server" 
                                            Text="Confirm and Publish" CssClass="ButSp" onclick="btnConfirmAndPublish_Click"/>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButSp" 
                                            onclick="btnCancel_Click"   />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 16px" colspan="6">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <rsweb:reportviewer id="rvGetDiscrepancyStats" runat="server" font-size="8pt" font-names="Verdana"
                                            processingmode="Remote" visible="false">
                                                        </rsweb:reportviewer>
                                        <rsweb:reportviewer id="rvGetDiscrepancyStatsCode" runat="server" font-size="8pt"
                                            font-names="Verdana" processingmode="Remote" visible="false">
                                                        </rsweb:reportviewer>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                            <ProgressTemplate>
                                <div style="position: absolute; top: 0px; left: 0px; overflow: hidden; padding: 0;
                                    margin: 0; filter: alpha(opacity=30); opacity: 0.3; background-color: white;
                                    z-index: 1000; width: 100%; height: 100%">
                                </div>
                                <div id="DivLoading" class="DivLoading-visible">
                                    <img id="ImgLoading" src='/Images/loading.gif' />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
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
                        <input id="hidIsEventOpen" type="hidden" runat="server" />
                        <input id="hidVenueCode" runat="server" type="hidden" />
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</asp:Content>
