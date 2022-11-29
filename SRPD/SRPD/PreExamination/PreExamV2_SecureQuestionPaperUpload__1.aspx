<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_SecureQuestionPaperUpload__1.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SecureQuestionPaperUpload__1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="CSS/PopUp.css" type="text/css" rel="stylesheet" />
    <script language="javascript" src="../JS/DraggablePopUps.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        var js_divImageUpload = "<%=divImageUpload.ClientID %>";
        var js_divVenueList = "<%=divVenueList.ClientID %>";


        function fnShowFileDialog(MoLrnID, PtrnID, BrnID, CrPrDetailsID, CrPrChID) {
            //set hidden variable of selected row

            document.getElementById('<%=divImageUpload.ClientID %>').style.display = 'block';
            document.getElementById("IframeImageUpload").style.display = "block";

        }

        function fnShowFileDialog1() {
            //set hidden variable of selected row

            document.getElementById('<%=divVenueList.ClientID %>').style.display = 'block';
            document.getElementById("IframeVenue").style.display = "block";

        }

        function fnUpload() {
            alert("File Uploaded Successfully!!");
            return false;
        }

        function Validate(obj) {
            var tbl = document.getElementById('<%=tblSelectedImage.ClientID%>');

            if (tbl.getElementsByTagName('input')[0].value != "") {
                if (tbl.getElementsByTagName('input')[1].value != "") {
                    return true;
                }
                else {
                    alert('Please enter correct file path');
                    return false;
                }
            }
            else {
                alert('Please enter Question papers setter name');
                return false;
            }

        }

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
        <br />
        <table cellspacing="0" cellpadding="0" width="700" border="0">
            <tr>
                <td valign="top" align="left" width="700">
                    <table>
                        <tr valign="top">
                            <!-- Main Content Starts-->
                            <td>
                                <table style="width: 80%">
                                    <tr>
                                        <td style="width: 706px; height: 12px;" class="clSubHeading">
                                            <span id="mesg" runat="server">List of Papers:</span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblMesg" runat="server" CssClass="errorNote" meta:resourcekey="lblMesgResource1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 706px; height: 110px; text-align: left" align="left" valign="top">
                                            <asp:GridView ID="gvCoursePart" runat="server" AutoGenerateColumns="False" Width="700px"
                                                CssClass="clGrid" OnRowDataBound="gvCoursePart_RowDataBound" OnRowCommand="gvCoursePart_RowCommand"
                                                DataKeyNames="Course_Name,Crpr_Abbr,CrPrCh_Abbr,pk_Fac_ID,pk_Cr_ID,pk_MoLrn_ID,pk_Ptrn_ID,pk_Brn_ID,pk_CrPr_Details_ID,pk_CrPrCh_ID,fk_Record_ID,PpPpHeadCrPrChID,Published,fileUploadedCount,ExamDate"
                                                EnableModelValidation="True" meta:resourcekey="gvCoursePartResource1">
                                                <RowStyle CssClass="gridItem" />
                                                <HeaderStyle CssClass="gridHeader" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SrNo." meta:resourcekey="TemplateFieldResource1">
                                                        <ItemTemplate>
                                                            <center>
                                                                <%# Container.DisplayIndex + 1 %>
                                                            </center>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Course_Name" HeaderText="Course Name" ReadOnly="True"
                                                        meta:resourcekey="BoundFieldResource1" />
                                                    <asp:BoundField DataField="Paper" HeaderText="Paper-TLM-AM-AT" ReadOnly="True" meta:resourcekey="BoundFieldResource1" />
                                                    <asp:BoundField DataField="ExamDateTime" HeaderText="Exam Date & Time" meta:resourcekey="BoundFieldResource2" />
                                                    <asp:BoundField DataField="StudentCount" HeaderText="Student Count" meta:resourcekey="BoundFieldResource3" />
                                                    <asp:TemplateField HeaderText="View Venue Link" meta:resourcekey="TemplateFieldResource2">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkViewVenue" runat="server" CommandName="View" Text="View Venue"
                                                                CommandArgument='<%# Container.DisplayIndex %>' meta:resourcekey="lnkViewVenueResource1"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Question Paper Upload" meta:resourcekey="TemplateFieldResource3">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkUpload" runat="server" CommandName="Upload" Text="Upload"
                                                                CommandArgument='<%# Container.DisplayIndex %>' ForeColor="Blue" meta:resourcekey="lnkUploadResource1"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Question Paper Delete">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Remove" Text="Delete"
                                                                CommandArgument='<%# Container.DisplayIndex %>' ForeColor="Blue"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="fileUploadedCount" HeaderText="No. of files uploaded"
                                                        meta:resourcekey="BoundFieldResource4" />
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 706px; text-align: left">
                                        </td>
                                    </tr>
                                </table>
                                <iframe id="IframeImageUpload" style="display: none; left: 350px; width: 450px; position: absolute;
                                    top: 250px; height: 150px; background-color: white; z-index: 100" frameborder="0"
                                    scrolling="no"></iframe>
                                <div id="divImageUpload" style="border-right: #800000 solid; border-top: #800000 solid;
                                    display: none; z-index: 99999; left: 350px; border-left: #800000 solid; width: 450px;
                                    border-bottom: #800000 solid; position: absolute; top: 250px; height: 150px;
                                    background-color: white" align="center" runat="server">
                                    <table onmousedown="dragStart(event, js_divImageUpload,'IframeImageUpload')" style="cursor: move;
                                        background-color: #800000" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="GridHeadingM1" width="100%" style="height: 18px">
                                                <asp:Label ID="lblHeading" runat="server" Height="18px" Text="<b>Upload Question Paper</b>"
                                                    meta:resourceKey="lblHeadingResource1"></asp:Label>
                                            </td>
                                            <td align="right" style="height: 14px">
                                                <img id="imgCloseImageUpload" alt="Close" style="cursor: hand" onclick="CloseWin(js_divImageUpload , 'IframeImageUpload');"
                                                    src="../Images/closeBtn.GIF" align="right" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="divScroll" style="overflow: auto; height: 410px" runat="server">
                                        <br />
                                        <table class="tblBackColor" id="tblSelectedImage" cellspacing="1" cellpadding="3"
                                            width="95%" border="0" runat="server">
                                            <tr id="Tr1" class="GridSubHeadingM" runat="server">
                                                <td id="Td1" style="height: 16px" valign="top" runat="server" colspan="2">
                                                    <b>Supported file formats for Question Paper upload: '*.pdf'</b>
                                                </td>
                                            </tr>
                                            <tr id="Tr2" runat="server">
                                                <td id="Td3" runat="server">
                                                    <b>Question Paper Setter Name</b>
                                                </td>
                                                <td id="Td4" runat="server" align="left">
                                                    <input id="txtQPSName" type="text" runat="server" />
                                                </td>
                                            </tr>
                                            <tr id="Tr6" runat="server">
                                                <td id="Td6" runat="server">
                                                    <b>Select Set to upload</b>
                                                </td>
                                                <td id="Td2" runat="server" align="left">
                                                    <input id="flUpload" type="file" runat="server" />
                                                </td>
                                            </tr>
                                            <tr id="Tr4" runat="server">
                                                <td id="Td5" runat="server" colspan="2" align="center">
                                                    <br />
                                                    <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="ButSp" OnClick="btnUpload_Click"
                                                        OnClientClick="return Validate(this.id);" meta:resourcekey="btnUploadResource1" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                </div>
                                <iframe id="IframeVenue" style="display: none; left: 350px; width: 500px; position: absolute;
                                    top: 250px; height: 150px; background-color: white; z-index: 100" frameborder="0"
                                    scrolling="no"></iframe>
                                <div id="divVenueList" style="border-right: #800000 solid; border-top: #800000 solid;
                                    display: none; z-index: 99999; left: 350px; border-left: #800000 solid; width: 500px;
                                    border-bottom: #800000 solid; position: absolute; top: 250px; height: 220px;
                                    background-color: white" align="center" runat="server">
                                    <table onmousedown="dragStart(event, js_divVenueList,'IframeVenue')" style="cursor: move;
                                        background-color: #800000" cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td class="GridHeadingM1" width="100%" style="height: 18px">
                                                <asp:Label ID="Label1" runat="server" Height="18px" Text="<b>Venue List</b>" meta:resourcekey="Label1Resource1"></asp:Label>
                                            </td>
                                            <td align="right" style="height: 14px">
                                                <img id="img1" alt="Close" style="cursor: hand" onclick="CloseWin(js_divVenueList , 'IframeVenue');"
                                                    src="../Images/closeBtn.GIF" align="right" />
                                            </td>
                                        </tr>
                                    </table>
                                    <div id="divVenue" style="overflow: auto; height: 200px" runat="server">
                                        <br />
                                        <table class="tblBackColor" id="Table1" cellspacing="1" cellpadding="3" width="95%"
                                            border="0" runat="server">
                                            <tr id="Tr3" runat="server">
                                                <td>
                                                    <asp:GridView ID="grvVenueList" runat="server" AutoGenerateColumns="False" Width="480px"
                                                        CssClass="clGrid" EnableModelValidation="True" meta:resourcekey="grvVenueListResource1">
                                                        <RowStyle CssClass="gridItem" />
                                                        <HeaderStyle CssClass="gridHeader" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="SrNo." meta:resourcekey="TemplateFieldResource4">
                                                                <ItemTemplate>
                                                                    <center>
                                                                        <%# Container.DisplayIndex + 1 %>
                                                                    </center>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="Inst_code" HeaderText="Venue Code" ReadOnly="True" meta:resourcekey="BoundFieldResource5" />
                                                            <asp:BoundField DataField="Inst_Name" HeaderText="Venue Name" meta:resourcekey="BoundFieldResource6" />
                                                        </Columns>
                                                    </asp:GridView>
                                                    <asp:Label ID="lblVenueMsg" runat="server" CssClass="errorNote" meta:resourcekey="lblVenueMsgResource1"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </div>
                                </div>
                            </td>
                            <!-- Main Content Ends-->
                        </tr>
                        <tr>
                            <td style="width: 753px">
                                <input id="hidRdbFlag" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hiddate" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidppcode" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidflag" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidFacID" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidCrID" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidMoLrnID" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidPtrnID" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidBrnID" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidCrPrDetailsID" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidCrPrChID" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidExEvID" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidPageDescription" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidScheduleID" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidScheduleDetails" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidExamEvent" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidIsEventOpen" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidIsExamFormConfigurationExists" runat="server" type="hidden" style="width: 43px;
                                    height: 11px" size="1" />
                                <input id="hidAssMthID" type="hidden" runat="server" />
                                <input id="hidAssTypeID" type="hidden" runat="server" />
                                <input id="hidAssMthAssTypeName" type="hidden" runat="server" />
                                <input id="hidTchLrnMthID" type="hidden" runat="server" />
                                <input id="hidCourseName" type="hidden" runat="server" />
                                <input id="hidCoursePartTermName" type="hidden" runat="server" />
                                <input id="hidCoursePartName" type="hidden" runat="server" />
                                <input id="hidPpPpHeadCrPrChID" type="hidden" runat="server" />
                                <input id="hidRecordID" type="hidden" runat="server" />
                                <input id="hidQPSName" type="hidden" runat="server" />
                                <input id="hidMaxSeqNo" type="hidden" runat="server" />
                                <input id="hidFilepath" type="hidden" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
