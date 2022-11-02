<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PreExamV2_EventWiseCourseSelection.ascx.cs"
    Inherits="PreExamV3WebCtrl.WebCtrl.PreExamV2_EventWiseCourseSelection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<link href="CSS/PopUp.css" rel="stylesheet" type="text/css" />
<link href="CSS/Matrix.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../ajax/common.ashx"></script>
<script type="text/javascript" src="../Services/SRVScheduleCenter.cs"></script>
<script language="javascript">
    tblListOfPapersClient = '<%=tblListOfPapers.ClientID%>'
    var popup = '<%=ModalPopupExtenderView.ClientID%>'

    function FillGrid() {
        document.getElementById('<%=tblGridNote.ClientID%>').style.display = 'none'
        document.getElementById('<%=tblGridMsg.ClientID%>').style.display = 'none'

        var hidCrPrDet = document.getElementById('<%=hidCrPrDetailsID.ClientID%>').value;
        var hidCrprCh = document.getElementById('<%=hidCrPrChID.ClientID%>').value;
        var hidEvent = document.getElementById('<%=hidEventID.ClientID%>').value;
        if (hidCrprCh != "-1" && hidCrprCh != "") {
            SRVExamForm.FetchExamFormConfiguration(hidCrPrDet, hidCrprCh, hidEvent, FillGrid_CallBack);
        }
        else {
            document.getElementById('<%=tblListOfPapers.ClientID%>').style.display = 'none'
            document.getElementById('<%=tblGridNote.ClientID%>').style.display = 'block'
            document.getElementById('<%=lblGridNote.ClientID%>').innerText = 'Please Select Event upto Course Part Term first before clicking on view exam form configuration link.'
        }


    }

    function FillGrid_CallBack(response) {
        var ds = response.value;

        if (ds != null) {
            if (ds.Rows.length > 0) {
                document.getElementById('<%=tblListOfPapers.ClientID%>').style.display = 'block'
                var tblPaperdetails = document.getElementById(tblListOfPapersClient).getElementsByTagName("tbody")[0]; ;
                if (tblPaperdetails.rows.length != 0) {
                    var cntPaper = tblPaperdetails.rows.length;
                    for (var i = 1; i < cntPaper && i != cntPaper; i++) {
                        if (tblPaperdetails.rows.length != 0) {
                            tblPaperdetails.deleteRow(1);
                        }
                    }
                }

                for (var i = 0; i < ds.Rows.length; i++) {
                    var row = document.createElement("TR");
                    row.setAttribute("align", "left");

                    var cell0 = document.createElement("TD");
                    cell0.style.textAlign = "center";
                    cell0.innerHTML = ds.Rows[i].Pp_Code;
                    row.appendChild(cell0);

                    var cell1 = document.createElement("TD");
                    cell1.style.textAlign = "left";
                    cell1.innerHTML = ds.Rows[i].Pp_Name;
                    row.appendChild(cell1);

                    var cell2 = document.createElement("TD");
                    cell2.style.textAlign = "center";
                    cell2.innerHTML = ds.Rows[i].TchLrnMth_Desc;
                    row.appendChild(cell2);

                    var cell3 = document.createElement("TD");
                    cell3.style.textAlign = "center";
                    cell3.innerHTML = ds.Rows[i].AssMth_Desc;
                    row.appendChild(cell3);

                    var cell4 = document.createElement("TD");
                    cell4.style.textAlign = "center";
                    cell4.innerHTML = ds.Rows[i].AssTy_Abbr;
                    row.appendChild(cell4);

                    var cell5 = document.createElement("TD");
                    cell5.style.textAlign = "center";
                    if (ds.Rows[i].ExistsInConfiguration != 0) {
                        cell5.innerHTML = '<img id="img1" alt="" src="../images/tick_mark.jpg" runat="server"/>'
                    }
                    else {
                        cell5.innerHTML = '<img id="img2" alt="" src="../images/cross_mark.jpg" runat="server"/>'
                    }
                    row.appendChild(cell5);


                    tblPaperdetails.appendChild(row);
                }
            }
        }
        else {
            document.getElementById('<%=tblListOfPapers.ClientID%>').style.display = 'none'
            document.getElementById('<%=tblGridMsg.ClientID%>').style.display = 'block'
            document.getElementById('<%=lblGridError.ClientID%>').innerText = 'Record Not Found'
        }

    }

</script>
<div align="right" id="divExamFormConfig" runat="server">
    <asp:LinkButton ID="lnkViewExamFormConfig" runat="server" Visible="False" OnClientClick="FillGrid()"
        Enabled="False" meta:resourcekey="lnkViewExamFormConfigResource1">View Exam Form Configuration</asp:LinkButton>
    <ajaxtoolkit:ModalPopupExtender ID="ModalPopupExtenderView" runat="server" TargetControlID="lnkViewExamFormConfig"
        PopupControlID="divPopUp" BehaviorID="ModalPopupExtenderView"
        Enabled="True" CancelControlID="btnCancel" DynamicServicePath="">
    </ajaxtoolkit:ModalPopupExtender>
</div>
<div id="divPopUp" runat="server" style="width: 720; height: 500; color: Black; display: none;
    font-style: normal;">
    <asp:Panel ID="panelContent" runat="server" Style="overflow-y: auto; display: block;
        border-right: black 2px solid; border-top: black 2px solid; border-left: black 2px solid;
        border-bottom: black 2px solid; overflow-x: auto" Width="720px" 
        Height="500px" BackColor="White" meta:resourcekey="panelContentResource1">
        <br />
        <div id="divContent" align="center" style="display: block;">
            <table id="tblGridNote" runat="server" style="display: none">
                <tr runat="server">
                    <td align="left" runat="server">
                        <asp:Label ID="lblGridNote" runat="server" CssClass="errorNote"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="tblListOfPapers" class="TblPaper" runat="server" cellspacing="1" cellpadding="3"
                border="0" width="100%" style="display: none">
                <tr class="gridHeader" runat="server">
                    <td align="center" runat="server">
                        Paper Code
                    </td>
                    <td align="center" runat="server">
                        Paper Name
                    </td>
                    <td align="center" runat="server">
                        Teaching Learning Method
                    </td>
                    <td align="center" runat="server">
                        Assessment Method
                    </td>
                    <td align="center" runat="server">
                        Assessment Type
                    </td>
                    <td align="center" runat="server">
                        Exists In Configuration
                    </td>
                </tr>
                <tr class="gridItem" runat="server">
                    <td id="Td1" style="height: 15px; font-weight: bold; width: 200px" align="right"
                        runat="server">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </td>
                    <td id="Td2" style="height: 15px; font-weight: bold; width: 200px" align="left" runat="server">
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                    </td>
                    <td id="Td3" style="height: 15px; font-weight: bold; width: 200px" align="right"
                        runat="server">
                        <asp:Label ID="Label3" runat="server"></asp:Label>
                    </td>
                    <td id="Td4" style="height: 15px; font-weight: bold; width: 200px" align="right"
                        runat="server">
                        <asp:Label ID="Label4" runat="server"></asp:Label>
                    </td>
                    <td id="Td5" style="height: 15px; font-weight: bold; width: 200px" align="right"
                        runat="server">
                        <asp:Label ID="Label5" runat="server"></asp:Label>
                    </td>
                    <td id="Td6" style="height: 15px; font-weight: bold; width: 200px" align="center"
                        runat="server">
                        <asp:Label ID="Label6" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table style="display: none" id="tblGridMsg" runat="server">
                <tr align="left" runat="server">
                    <td runat="server">
                        <asp:Label ID="lblGridError" runat="server" CssClass="errorNote"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <asp:Button ID="btnCancel" runat="server" Text="Close" CssClass="butSubmit" 
            meta:resourcekey="btnCancelResource1" />
    </asp:Panel>
</div>
<table width="100%">
    <tr>
        <td>
            <asp:UpdatePanel ID="updatePnl" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnProceed" />
                </Triggers>
                <ContentTemplate>
                    <table width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                            <td align="right" width="48%">
                            </td>
                            <td align="center" width="2%">
                            </td>
                            <td align="right">
                                <asp:Label ID="lblError" runat="server" CssClass="errorNote" 
                                    Style="position: relative" meta:resourcekey="lblErrorResource1"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td width="35%" align="right">
                                <strong>
                                    <asp:Label ID="lblSelectFac" runat="server" Text="Select Faculty" 
                                    meta:resourcekey="lblSelectFacResource1"></asp:Label>
                                </strong>
                            </td>
                            <td width="1%" align="center">
                                <strong>: </strong>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlFaculty" runat="server" TabIndex="2" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" 
                                    meta:resourcekey="ddlFacultyResource1">
                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource1">--- Select ---</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; <strong><span style="color: #ff0000">*</span></strong><asp:RequiredFieldValidator
                                    ID="rfvFaculty" runat="server" ControlToValidate="ddlFaculty" ErrorMessage="Select Faculty"
                                    InitialValue="-1" Style="position: relative" Display="None" 
                                    meta:resourcekey="rfvFacultyResource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td width="35%" align="right">
                                <strong>
                                    <asp:Label ID="lblSelectCr" runat="server" Text="Select Course" 
                                    meta:resourcekey="lblSelectCrResource1"></asp:Label></strong>
                            </td>
                            <td width="1%" align="center">
                                <strong>: </strong>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCourseName" runat="server" TabIndex="3" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCourseName_SelectedIndexChanged" 
                                    meta:resourcekey="ddlCourseNameResource1">
                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource2">--- Select ---</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; <strong><span style="color: #ff0000">*</span></strong><asp:RequiredFieldValidator
                                    ID="rfvCourse" runat="server" ControlToValidate="ddlCourseName" ErrorMessage="Select Course"
                                    InitialValue="-1" Style="position: relative" Display="None" 
                                    meta:resourcekey="rfvCourseResource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td width="35%" align="right">
                                <strong>Select Branch </strong>
                            </td>
                            <td width="1%" align="center">
                                <strong>: </strong>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="4" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" 
                                    meta:resourcekey="ddlBranchResource1">
                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource3">--- Select ---</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; <strong><span style="color: #ff0000">*</span></strong><asp:RequiredFieldValidator
                                    ID="rfvBranch" runat="server" ControlToValidate="ddlBranch" ErrorMessage="Select Branch"
                                    InitialValue="-1" Style="position: relative" Display="None" 
                                    meta:resourcekey="rfvBranchResource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td width="35%" align="right">
                                <strong>
                                    <asp:Label ID="lblSelectCrPart" runat="server" Text="Select Course Part" 
                                    meta:resourcekey="lblSelectCrPartResource1"></asp:Label>
                                </strong>
                            </td>
                            <td width="1%" align="center">
                                <strong>: </strong>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCoursePart" runat="server" TabIndex="5" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCoursePart_SelectedIndexChanged" 
                                    meta:resourcekey="ddlCoursePartResource1">
                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource4">--- Select ---</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; <strong><span style="color: #ff0000">*</span></strong><asp:RequiredFieldValidator
                                    ID="rfvCoursePart" runat="server" ControlToValidate="ddlCoursePart" ErrorMessage="Select Course Part"
                                    InitialValue="-1" Style="position: relative" Display="None" 
                                    meta:resourcekey="rfvCoursePartResource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="height: 25px">
                            <td width="35%" align="right">
                                <strong>
                                    <asp:Label ID="lblSelectCrPartTerm" runat="server" 
                                    Text="Select Course Part Term " meta:resourcekey="lblSelectCrPartTermResource1"></asp:Label></strong>
                            </td>
                            <td width="1%" align="center">
                                <strong>: </strong>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCoursePartTerm" runat="server" TabIndex="6" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCoursePartTerm_SelectedIndexChanged" 
                                    meta:resourcekey="ddlCoursePartTermResource1">
                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource5">--- Select ---</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; <strong><span style="color: #ff0000">*</span></strong><asp:RequiredFieldValidator
                                    ID="rfvCoursePartTerm" runat="server" 
                                    ControlToValidate="ddlCoursePartTerm" ErrorMessage="Select Course Part Term"
                                    InitialValue="-1" Style="position: relative" Display="None" 
                                    meta:resourcekey="rfvCoursePartTermResource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr style="height: 25px" id="trExamEvent" runat ="server" >
                            <td style="text-align: right;">
                                <strong>Select Exam Event</strong>
                            </td>
                            <td width="1%" align="center">
                                <strong>: </strong>
                            </td>
                            <td style="text-align: left; width: 60%">
                                <asp:DropDownList ID="ddlEvent" runat="server" 
                                    Style="position: relative; top: 0px; left: 0px;" AutoPostBack="True"
                                    TabIndex="7" OnSelectedIndexChanged="ddlEvent_SelectedIndexChanged" 
                                    meta:resourcekey="ddlEventResource1">
                                    <asp:ListItem Value="-1" meta:resourcekey="ListItemResource6">--- Select ---</asp:ListItem>
                                </asp:DropDownList>
                                &nbsp; <strong><span style="color: #ff0000">*</span></strong>
                                <asp:RequiredFieldValidator ID="rfvExamEvnt" runat="server" ControlToValidate="ddlEvent"
                                    ErrorMessage="Select Exam Event" InitialValue="-1" Style="position: relative;
                                    left: 0px;" Display="None" meta:resourcekey="rfvExamEvntResource1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trExamAppearanceType" runat="server" style="display: none">
                            <td style="font-weight: bold; vertical-align: middle; width: 402px; text-align: right"
                                valign="middle" align="left" runat="server">
                                Select Exam Appearance Type
                            </td>
                            <td style="font-weight: bold; vertical-align: middle; width: 2px; text-align: right"
                                valign="middle" align="left" runat="server">
                                :
                            </td>
                            <td style="width: 479px" align="left" runat="server">
                                <asp:RadioButtonList ID="rbtExamAppearanceType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0">Fresher</asp:ListItem>
                                    <asp:ListItem Value="1">Repeater</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="trCollegeList" runat="server" style="display: none">
                            <td style="font-weight: bold; vertical-align: middle; width: 402px; text-align: right"
                                valign="middle" align="left" runat="server">
                                Select All College/Single College
                            </td>
                            <td style="font-weight: bold; vertical-align: middle; width: 2px; text-align: right"
                                valign="middle" align="left" runat="server">
                                :
                            </td>
                            <td style="width: 479px" align="left" runat="server">
                                <asp:RadioButtonList ID="rbCollegeList" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Selected="True" Value="0" Text="All Colleges"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="Select College"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr id="trButton" runat="server" style="font-size: 12pt">
                            <td align="left" colspan="3" style="font-weight: bold; vertical-align: middle; text-align: center;
                                height: 26px;" valign="middle" runat="server">
                                <br />
                                <asp:Button ID="btnProceed" runat="server" CssClass="ButSp" Text="Proceed"></asp:Button>
                            </td>
                        </tr>
                        <tr id="trMsg" runat="server">
                            <td style="font-weight: normal; vertical-align: middle; text-align: left; height: 21px;"
                                valign="middle" align="left" colspan="3" runat="server">
                                Note : &nbsp;<span style="color: #ff0000">* <span style="color: black">marked fields
                                    are mandatory.</span></span>&nbsp;
                            </td>
                        </tr>
                        <tr id="trlblMsg" runat="server" style="font-size: 12pt">
                            <td style="font-weight: normal; vertical-align: middle; text-align: left; height: 17px;"
                                valign="middle" align="left" colspan="3" runat="server">
                                <asp:Label ID="lblMsg" runat="server" Visible="False" CssClass="errorNote" ></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        HeaderText="Please correct following errors" ShowSummary="False" 
                        meta:resourcekey="ValidationSummary1Resource1" />
                    <asp:Label ID="lblFac" runat="server" Text="Faculty" Style="display: none" 
                        meta:resourcekey="lblFacResource1"></asp:Label>
                    <asp:Label ID="lblCr" runat="server" Text="Course" Style="display: none" 
                        meta:resourcekey="lblCrResource1"></asp:Label>
                    <input type="hidden" runat="server" id="hidCrPrDetailsID" />
                    </input>
                    <input id="hidCrPrChID" runat="server" type="hidden"></input>
                        <input id="hidEventID" runat="server" type="hidden"></input>
                        </input>
                    </input>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>