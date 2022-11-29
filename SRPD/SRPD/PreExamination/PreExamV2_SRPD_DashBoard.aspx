<%@ Page Title="" Language="C#" MasterPageFile="~/New_Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_SRPD_DashBoard.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SRPD_DashBoard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        function checkDate() {

            var idate = document.getElementById('<%=txtSelectDate.ClientID%>');
            var btnDate = document.getElementById('<%=btnDateSeletion.ClientID%>');
            resultDiv = document.getElementById("datewarn"),
        dateReg = '/(0[1-9]|[12][0-9]|3[01])[\/](0[1-9]|1[012])[\/]201[4-9]|20[2-9][0-9]/';


            if (!dateReg.test(idate.value)) {
                resultDiv.innerHTML = "Invalid date!";
                resultDiv.style.color = "red";
                btnDate.disabled = false;
                return false;
            }
            else {
                btnDate.disabled = true;
            }
            if (isFutureDate(idate.value)) {
                document.getElementById('<%=txtSelectDate.ClientID%>') = '';
                alert("- Date of issue should be less then today\'s date.");
                return false;
            }

            else {
                resultDiv.innerHTML = "It's a valid date";
                resultDiv.style.color = "green";
            }
        }

            
    </script>
    <div>
        <div class="row mb-2">
            <div class="col-md-12 menu-title">
                <center>
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-3 col-md-offset-4 controls text-center">
                                <div class='input-group date datepicker1'>
                                    <asp:TextBox ID="txtSelectDate" runat="server" MaxLength="10" onkeyup="checkDate()"
                                        CssClass="date-style"></asp:TextBox>
                                    <span class="input-group-addon"><span class="glyphicon glyphicon-calendar"></span>
                                    </span>
                                </div>
                            </div>
                            <div class="col-md-4 text-left">
                                <asp:Button ID="btnDateSeletion" runat="server" Text="Search" CssClass=" btn btn-info"
                                    OnClick="btnDateSelection_Click" />
                                <span style="color: Red">(dd/mm/yyyy)</span>
                                <asp:RequiredFieldValidator ID="rfvSelectDate" runat="server" ControlToValidate="txtSelectDate"
                                    ErrorMessage="Please enter Date." Display="None"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtSelectDate"
                                    ValidationExpression="(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$"
                                    ErrorMessage="Invalid date format." />
                                <asp:CustomValidator ID="cusValTxtDob" runat="server" ControlToValidate="txtSelectDate"
                                    Display="none" ErrorMessage="You have entered invalid date. Entered in (dd/mm/yyyy) format."></asp:CustomValidator>
                            </div>
                        </div>
                    </div>
                </center>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 menu-title">
            </div>
        </div>
        <div class="row menu">
            <div class="col-md-3">
                <div class="small-card">
                    <div class="row d-flex align-items-center">
                        <div class="col-12">
                            <h4>
                                <asp:Label ID="lblPaperCode" runat="server" Text="Label" class="d-flex justify-content-center"> </asp:Label>
                                Total Papers
                            </h4>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="small-card">
                    <div class="row d-flex align-items-center">
                        <div class="col-12">
                            <h4>
                                <asp:Label ID="lblUplodedPaper" runat="server" Text="Label" class="d-flex justify-content-center"></asp:Label>
                                Uploded Paper
                            </h4>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <asp:LinkButton ID="lbtnNotUploadedPaper" runat="server" OnClick="lbtnNotUploadedPaper_Click">
                    <div class="small-card">
                        <div class="row d-flex align-items-center">
                            <div class="col-12">
                                <h4>
                                    <asp:Label ID="lblTotalNotUploadedCount" runat="server" Text="Label" class="d-flex justify-content-center"></asp:Label>
                                    Not-Uploaded Papers
                                </h4>
                            </div>
                        </div>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-3">
                <asp:LinkButton ID="lbtnTimeTableNotPublishedPapers" runat="server" OnClick="lbtnTimeTableNotPublishedPapers_Click">
                    <div class="small-card">
                        <div class="row d-flex align-items-center">
                            <div class="col-12">
                                <h4>
                                    <asp:Label ID="lblNotPublishedCount" runat="server" Text="Label" class="d-flex justify-content-center"></asp:Label>
                                    Time Table Not Published Papers
                                </h4>
                            </div>
                        </div>
                    </div>
                </asp:LinkButton>
            </div>
        </div>
        <div class="row menu">
            <div class="col-md-3">
                <div class="small-card">
                    <div class="row d-flex align-items-center">
                        <div class="col-12">
                            <h4>
                                <asp:Label ID="lblTotalPublishVenue" runat="server" Text="Label" class="d-flex justify-content-center"></asp:Label>
                                Published Venue
                            </h4>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <asp:LinkButton ID="lbtnNotPublishedVenue" runat="server" OnClick="lbtnNotPublishedVenue_Click">
                    <div class="small-card">
                        <div class="row d-flex align-items-center">
                            <div class="col-12">
                                <h4>
                                    <asp:Label ID="lblTotalNotPublishVenue" runat="server" Text="Label" class="d-flex justify-content-center"></asp:Label>
                                    Not published Venue
                                </h4>
                            </div>
                        </div>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-3">
                <asp:LinkButton ID="lnkProgramsWithNoVenue" runat="server" OnClick="lnkProgramsWithNoVenue_Click">
                    <div class="small-card">
                        <div class="row d-flex align-items-center">
                            <div class="col-12">
                                <h4>
                                    <asp:Label ID="lblProgramsWithNoVenue" runat="server" Text="Label" class="d-flex justify-content-center"></asp:Label>
                                    Programs with no Venue
                                </h4>
                            </div>
                        </div>
                    </div>
                </asp:LinkButton>
            </div>
            <div class="col-md-3">
                <asp:LinkButton ID="lnkInstitutesNotMapped" runat="server" OnClick="lnkInstitutesNotMapped_Click">
                    <div class="small-card">
                        <div class="row d-flex align-items-center">
                            <div class="col-12">
                                <h4>
                                    <asp:Label ID="lblInstitutesNotMappedToAnyCenter" runat="server" Text="Label" class="d-flex justify-content-center"></asp:Label>
                                    Institutes Not Mapped To Any Center
                                </h4>
                            </div>
                        </div>
                    </div>
                </asp:LinkButton>
            </div>
        </div>
        <div class="row mb-2">
            <div class="table-wrapper col-md-12" id="slotExam" runat="server">
                <div class="alert alert-info">
                    <h3>
                        <i class="fa fa-bookmark"></i>Examination Slot</h3>
                </div>
            </div>
            <div class="table-wrapper col-md-12">
                <center>
                    <asp:GridView ID="gvExamCount" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        OnRowCommand="gvExamCount_RowCommand" DataKeyNames="ExamStartTime_New,ExamEndTime_New">
                        <Columns>
                            <asp:TemplateField HeaderText="Slot.">
                                <ItemTemplate>
                                    Slot-<%# (Container.DataItemIndex)+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ExamStartTime_New" HeaderText="Start Time" />
                            <asp:BoundField DataField="ExamEndTime_New" HeaderText="End Time" />
                            <asp:BoundField DataField="ExpectedDownload" HeaderText="Expected Download" />
                            <asp:TemplateField HeaderText="Download Pending" meta:resourcekey="TemplateFieldResource2">
                                <ItemTemplate>
                                    <div class="row">
                                        <div style="text-align: left" class="col-sm-4">
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("DownloadPending") %>'></asp:Label>
                                        </div>
                                        <div style="text-align: center; vertical-align: middle;" class="col-sm-4">
                                            <asp:LinkButton ID="lbtnDownloadPending" runat="server" CommandArgument=" <%# Container.DisplayIndex + 1 %>"
                                                CommandName="View"> View </asp:LinkButton>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Total_Papers" HeaderText="Total Papers" />
                        </Columns>
                    </asp:GridView>
                </center>
            </div>
        </div>
        <div class="row mb-2">
            <div class="table-wrapper col-md-12" id="slotpaper" runat="server">
                <div class="alert alert-info">
                    <h3>
                        <i class="fa fa-bookmark"></i>Slotwise Papers Details</h3>
                </div>
            </div>
            <div class="table-wrapper">
                <center>
                    <asp:GridView ID="gvPaperDetails" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                        DataKeyNames="pk_Uni_ID,pk_Fac_ID,pk_Cr_ID,pk_MoLrn_ID,pk_Ptrn_ID,pk_Brn_ID,pk_CrPr_Details_ID,pk_CrPrCh_ID,pk_ExEv_ID,
                ExamStartTime_New,ExamEndTime_New,pk_Pp_PpHead_CrPrCh_ID,Pp_Code,Pp_name" OnRowCommand="gvPaperDetails_RowCommand">
                        <%--  AllowPaging="true" PageSize="25" OnPageIndexChanging="gvPaperDetails_PageIndexChanging"--%>
                        <Columns>
                            <asp:BoundField DataField="SlotNo" HeaderText="Slot No" />
                            <asp:BoundField DataField="Pp_Code" HeaderText="Paper Code" />
                            <asp:BoundField DataField="Pp_Name" HeaderText="Paper Name" />
                            <asp:TemplateField HeaderText="Download Pending">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnPaperDetailsView" runat="server" CommandName="PaperDetailsView"
                                        Text="View" CommandArgument=" <%# Container.DisplayIndex + 1 %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                        </EmptyDataTemplate>
                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                    </asp:GridView>
                </center>
            </div>
        </div>
    </div>
    <input id="hidUniId" type="hidden" runat="server" />
    <input type="hidden" id="hidDateTime" runat="server" value="0" />
    <input type="hidden" id="hidStartTime" runat="server" value="0" />
    <input type="hidden" id="hidEndTime" runat="server" value="0" />
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
</asp:Content>
