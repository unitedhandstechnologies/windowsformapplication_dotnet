<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_SecureQuestionPaperUpload_Revised.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SecureQuestionPaperUpload_Revised" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Src="WebCtrl/PreExamV2_EventWiseCourseSelection.ascx" TagName="PreExamV2_EventWiseCourseSelection"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">

        var myElement1;
        function callPpValidate() {
            debugger;
            var message1 = '';
            var PpCode = document.getElementById('<%= Txtpapercode.ClientID%>').value;
            var ExEventIndex = document.getElementById('<%=ddlExamEvent.ClientID%>').selectedIndex;
            if (PpCode == "") {
                message1 += "<li> Please Enter Paper Code.</li><br>";
            }
            if (ExEventIndex == 0) {
                message1 += "<li> Please Select Exam Event.</li><br>";
            }
            if (message1 != '') {
                showValidationSummary(myElement1, message1);
                return false;
            }
        }
    </script>
    <center>
        <table cellpadding="0" cellspacing="0" width="700" height="30px">
            <tr valign="top">
                <td align="left" style="border-bottom: 1px solid #FFD275;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Revised Question Paper Upload" ></asp:Label>
                    <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="700" border="0">
            <tr>
                <td valign="top" align="left" width="700">
                    <table style="width: 683px">
                        <tr align="center">
                            <td>
                                <div runat="server" id="rdbdiv">
                                    <asp:RadioButton ID="rdbProgramwise" GroupName="rdBox" Text="Program Wise" Value="1"
                                        runat="server" AutoPostBack="true" OnCheckedChanged="rdbProgramwise_CheckedChanged" />
                                    <asp:RadioButton ID="rdbpapercodewise" AutoPostBack="true" GroupName="rdBox" Text="Paper Code Wise"
                                        Value="2" runat="server" OnCheckedChanged="rdbpapercodewise_CheckedChanged" />    
                                </div>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td align="right">
                                <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trProgramwise">
                            <!-- Main Content Starts-->
                            <td>
                                <div id="divCourseSelection" runat="server">
                                    <fieldset>
                                        <legend><b>Selection</b></legend>
                                        <uc2:preexamv2_eventwisecourseselection id="CrSelectionCtrl" runat="server"></uc2:preexamv2_eventwisecourseselection>
                                    </fieldset>
                                </div>
                                <div runat="server" id="divAssMthAssType" style="width: 700px; display: none">
                                    <fieldset style="position: static">
                                        <legend><strong>List of Assessment method and Assessment type combination : &nbsp;</strong></legend>
                                        <table id="Table3" cellspacing="0" cellpadding="0" width="700" border="0">
                                            <tbody>
                                                <tr runat="server" id="trCenter">
                                                    <td runat="server" id="Td9" style="height: 36px" align="left">
                                                        <asp:RadioButtonList runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                            Width="654px" ID="rbtListAssementMethodCA" meta:resourcekey="rbtListAssementMethodCAResource1">
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr id="tr9" runat="server" style="font-size: 12pt">
                                                    <td style="font-weight: bold; vertical-align: middle; text-align: center; height: 26px;">
                                                        <asp:Button ID="btnBckToCrSelection" runat="server" CssClass="ButSp" Text="<< Back To Course Selection"
                                                            Width="200px" OnClick="btnBckToCrSelection_Click" meta:resourcekey="btnBckToCrSelectionResource1">
                                                        </asp:Button>&nbsp;<asp:Button ID="btnNext" runat="server" CssClass="ButSp" Text="Next >>"
                                                            Width="200px" OnClick="btnNext_Click" meta:resourcekey="btnNextResource1" Visible="false">
                                                        </asp:Button>                                                      
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </fieldset>
                                </div>
                            </td>
                            <!-- Main Content Ends-->
                        </tr>
                        <tr id="trPapercodewise">
                            <td>
                                <div runat="server" id="divpapercodewiseselection" style="width: 700px; display: none">
                                    <fieldset>
                                        <legend><b>Paper Code Wise Selection</b></legend>
                                        <table width="700" runat="server" id="tblExamEvent" cellpadding="0" cellspacing="10">
                                            <tr id="Tr4" runat="server">
                                                <td id="Td1" style="height: 28px; width: 50%;" align="right" runat="server">
                                                    <strong>Enter Paper Code</strong>
                                                </td>
                                                <td id="Td7" style="height: 28px; width: 2%;" align="right" runat="server">
                                                    :&nbsp;
                                                </td>
                                                <td id="Td8" style="height: 28px" align="left" width="49%" runat="server">
                                                    <asp:TextBox ID="Txtpapercode" runat="server"></asp:TextBox>
                                                    <font class="Mandatory">* </font>
                                                </td>
                                            </tr>
                                            <tr id="Tr1" runat="server">
                                                <td id="Td2" style="height: 28px; width: 50%;" align="right" runat="server">
                                                    <strong>Select examination event</strong>
                                                </td>
                                                <td id="Td3" style="height: 28px; width: 2%;" align="right" runat="server">
                                                    :&nbsp;
                                                </td>
                                                <td id="Td4" style="height: 28px" align="left" width="49%" runat="server">
                                                    <asp:DropDownList ID="ddlExamEvent" runat="server" Height="20px" OnSelectedIndexChanged="ddlExamEvent_SelectedIndexChanged"
                                                        AutoPostBack="true">
                                                        <asp:ListItem Value="0" Selected="True">---Select---</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <font class="Mandatory">* </font>
                                                </td>
                                            </tr>
                                            <tr id="Tr2" runat="server">
                                                <td id="Td5" style="height: 25px" align="center" colspan="3" runat="server">
                                                    &nbsp;
                                                    <asp:Button ID="btnppcodeproceed" runat="server" CausesValidation="false" Text="Proceed"
                                                        Width="110px" Height="25px" OnClientClick="return callPpValidate();" Visible="true"
                                                        OnClick="btnppcodeproceed_Click" />
                                                    &nbsp;
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
                                </div>
                                <div runat="server" id="divAssMthAssTypePaperCodeWise" style="width: 700px; display: none">
                                    <fieldset style="position: static">
                                        <legend><strong>List of Assessment method and Assessment type combination : &nbsp;</strong></legend>
                                        <table id="Table2" cellspacing="0" cellpadding="0" width="700" border="0">
                                            <tbody>
                                                <tr runat="server" id="tr5">
                                                    <td runat="server" id="Td18" style="height: 36px" align="left">
                                                        <asp:RadioButtonList runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                                            Width="654px" ID="rbtListAssementMethodCAPpCodeWise">
                                                        </asp:RadioButtonList>
                                                    </td>
                                                </tr>
                                                <tr id="tr11" runat="server" style="font-size: 12pt">
                                                    <td style="font-weight: bold; vertical-align: middle; text-align: center; height: 26px;">
                                                        <asp:Button ID="Button2" runat="server" CssClass="ButSp" CausesValidation="false"
                                                            Text="<< Back To Paper Code Selection" Width="200px" OnClick="btnBckToPpCodeSelection_Click">
                                                        </asp:Button>&nbsp;<asp:Button ID="btnNextPaperCodeWise" CausesValidation="false"
                                                            runat="server" CssClass="ButSp" Text="Next >>" Width="200px" OnClick="btnNextPaperCodeWise_Click"
                                                            Visible="true"></asp:Button>                                                     
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </fieldset>
                                </div>
                            </td>
                        </tr>                       
                        <tr>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <input id="hidRdbFlag" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hiddate" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidppcode" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidflag" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidAcademicYearID" runat="server" type="hidden" style="width: 43px; height: 11px"
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
                                <input id="hidExamEvent" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidExEvForExport" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidIsEventOpen" runat="server" type="hidden" style="width: 43px; height: 11px"
                                    size="1" />
                                <input id="hidExamEventName" runat="server" size="1" style="width: 43px; height: 11px"
                                    type="hidden" />
                                <input id="hidAssMthID" type="hidden" runat="server" />
                                <input id="hidAssTypeID" type="hidden" runat="server" />
                                <input id="hidAssMthAssTypeName" type="hidden" runat="server" />
                                <input id="hidCourseName" type="hidden" runat="server" />
                                <input id="hidCoursePartTermName" type="hidden" runat="server" />
                                <input id="hidCoursePartName" type="hidden" runat="server" />
                                <input id="hidTchLrnMthID" type="hidden" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
