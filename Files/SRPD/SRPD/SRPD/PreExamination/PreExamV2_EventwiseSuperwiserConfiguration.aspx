<%@ Page Title="" Language="C#" MasterPageFile="~/New_Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_EventwiseSuperwiserConfiguration.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_EventwiseSuperwiserConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" type="text/javascript">

        function validate() {
            var gd = document.getElementById('<%=grvListSupervisor.ClientID%>');
            var cnt = 0;
            for (i = 1; i <= gd.rows.length - 1; i++) {
                if (gd.rows[i].cells[5].getElementsByTagName('Input').item().checked == true) {
                    cnt++;
                }
            }

            if (cnt == 0) {
                alert('Please Select atleast one supervisor');
                return false;
            }
            else {
                return true;
            }
        }

        function SelectAllCheckboxes() {
           
            var gd = document.getElementById('<%=grvListSupervisor.ClientID%>');

            if (gd.rows[0].cells[5].getElementsByTagName('Input').item(0).checked == true) {
                for (i = 1; i <= gd.rows.length - 1; i++) {
                    try {
                        gd.rows[i].cells[5].getElementsByTagName('Input').item(0).checked = true;
                    }
                    catch (msg) { }
                }
            }
            else if (gd.rows[0].cells[5].getElementsByTagName('Input').item(0).checked == false) {
                for (i = 1; i <= gd.rows.length - 1; i++) {
                    try {
                        gd.rows[i].cells[5].getElementsByTagName('Input').item(0).checked = false;
                    }
                    catch (msg) { }
                }
            }
        }

        function chkValidate() {
         
            var cnt = 0;

            var gd = document.getElementById('<%=grvListSupervisor.ClientID%>');

            for (i = 1; i <= gd.rows.length - 1; i++) {
                try {
                    if (gd.rows[i].cells[5].getElementsByTagName('Input').item().checked == true) {
                        cnt++;
                    }
                }
                catch (msg) { }
            }

            if (gd.rows.length - 1 == cnt)
                gd.rows[0].cells[5].getElementsByTagName('Input').item().checked = true;
            else
                gd.rows[0].cells[5].getElementsByTagName('Input').item().checked = false;


        }


        function validateConfiguration() {
            var gd = document.getElementById('<%=GrvConfigured.ClientID%>');
            var cnt = 0;
            for (i = 1; i <= gd.rows.length - 1; i++) {
                if (gd.rows[i].cells[5].getElementsByTagName('Input').item().checked == true) {
                    cnt++;
                }
            }

            if (cnt == 0) {
                alert('Please Select atleast one supervisor');
                return false;
            }
            else {
                return true;
            }
        }

        function SelectAllCheckboxesConfiguration() {
            
            var gd = document.getElementById('<%=GrvConfigured.ClientID%>');

            if (gd.rows[0].cells[5].getElementsByTagName('Input').item(0).checked == true) {
                for (i = 1; i <= gd.rows.length - 1; i++) {
                    try {
                        gd.rows[i].cells[5].getElementsByTagName('Input').item(0).checked = true;
                    }
                    catch (msg) { }
                }
            }
            else if (gd.rows[0].cells[5].getElementsByTagName('Input').item(0).checked == false) {
                for (i = 1; i <= gd.rows.length - 1; i++) {
                    try {
                        gd.rows[i].cells[5].getElementsByTagName('Input').item(0).checked = false;
                    }
                    catch (msg) { }
                }
            }
        }

        function chkValidateConfiguration() {
        
            var cnt = 0;

            var gd = document.getElementById('<%=GrvConfigured.ClientID%>');

            for (i = 1; i <= gd.rows.length - 1; i++) {
                try {
                    if (gd.rows[i].cells[5].getElementsByTagName('Input').item().checked == true) {
                        cnt++;
                    }
                }
                catch (msg) { }
            }

            if (gd.rows.length - 1 == cnt)
                gd.rows[0].cells[5].getElementsByTagName('Input').item().checked = true;
            else
                gd.rows[0].cells[5].getElementsByTagName('Input').item().checked = false;


        }     
    </script>
    <div>
        <div class="row">
            <div class="col-md-12 menu-title">
                <div class="text-center">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-12 text-left">
                                <h3>
                                    <i class="fa fa-bookmark"></i>
                                    <asp:Label ID="lblPageHead" runat="server" Text="Eventwise Supervisor configuration"></asp:Label>
                                    <asp:Label ID="lblSubHeader" runat="server" Font-Bold="True" ForeColor="Black"></asp:Label>
                                </h3>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12 menu-title text-left">
                <div class="alert alert-info">
                    <b>NOTE:</b> Eventwise supervisor configuration is for Senior Supervisor only.
                </div>
            </div>
        </div>
        <div class="row" id="divSuccess" runat="server" style="display: none">
            <div class="col-md-12 menu-title text-left">
                <div class="alert alert-success alert-dismissible fade in">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <asp:Label ID="lblSuccess" runat="server"></asp:Label>                  
                </div>
            </div>
        </div>
        <div class="row" id="divFailure" runat="server" style="display: none">
            <div class="col-md-12 menu-title text-left">
                <div class="alert alert-danger alert-dismissible fade in">
                    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <asp:Label ID="lblFailure" runat="server"></asp:Label>                    
                </div>
            </div>
        </div>
        <div id="divEvent" runat="server">
            <div class="row ">
                <div class="col-md-12 menu-title">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-2 col-md-offset-1">
                                <strong>Select Exam Event</strong> :
                            </div>
                            <div class="col-md-4 ">
                                <asp:DropDownList ID="ddlExamEvent" runat="server" AppendDataBoundItems="True" class="form-control">
                                    <asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row mb-2">
                <div class="col-md-12 menu-title">
                    <div class="form-horizontal text-center">
                        <div class="form-group">
                            <div class="col-md-12 text-center">
                                <asp:Button ID="btnProceed" runat="server" Text="Proceed" CssClass=" btn btn-info"
                                    OnClick="btnProceed_Click" />
                                <input id="hidExamEventName" runat="server" type="hidden" />
                                <input id="hidExamEventID" runat="server" type="hidden" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="tabHeader2" runat="server" style="display: none; width: 100%; padding-bottom: 50px;">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:PostBackTrigger ControlID="lnkNotConfiguredSupervisorList" />
                    <asp:PostBackTrigger ControlID="lnkConfiguredSupervisorList" />
                    <asp:PostBackTrigger ControlID="btnSave" />
                    <asp:PostBackTrigger ControlID="btnBack" />
                    <asp:PostBackTrigger ControlID="btnSave1" />
                    <asp:PostBackTrigger ControlID="btnBack1" />
                    <asp:PostBackTrigger ControlID="btnSave2" />
                    <asp:PostBackTrigger ControlID="btnBack2" />
                    <asp:PostBackTrigger ControlID="btnSave3" />
                    <asp:PostBackTrigger ControlID="btnBack3" />
                    <asp:PostBackTrigger ControlID="btnSearch" />
                    <asp:PostBackTrigger ControlID="btnSearchConfigure" />
                </Triggers>
                <ContentTemplate>
                    <div class="holder">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="tab-wrapper">
                                    <ul id="ullist" class="nav nav-tabs">
                                        <li class="tab active" id="li1" runat="server">
                                            <asp:LinkButton ID="lnkNotConfiguredSupervisorList" runat="server" Text="Not Configured Supervisor List"
                                                Font-Bold="true" OnClick="lnkNotConfiguredSupervisorList_Click" class="btn btn-link"></asp:LinkButton>
                                        </li>
                                        <li class="tab" id="li2" runat="server">
                                            <asp:LinkButton ID="lnkConfiguredSupervisorList" runat="server" Text="Configured Supervisor List"
                                                Font-Bold="true" OnClick="lnkConfiguredSupervisorList_Click" class="btn btn-link"></asp:LinkButton>
                                        </li>
                                    </ul>
                                    <div class="tab-content">
                                        <div id="divBtnNotConfiguration" runat="server" style="display: none">
                                            <div class="row mb-2">
                                                <div class="col-md-4 menu-title text-left">
                                                    <asp:TextBox ID="txtSearch" runat="server" MaxLength="10" CssClass="search-style" ></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 menu-title text-left">
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass=" btn btn-info"
                                                        OnClick="btnSearch_Click" />
                                                </div>
                                            </div>
                                            <div class="row mb">
                                                <div class="col-md-12 menu-title">
                                                    <div class="text-center">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <div class="col-md-9  text-right">
                                                                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass=" btn btn-info" OnClick="btnSave_Click"
                                                                        OnClientClick="return validate();" />
                                                                </div>
                                                                <div class="col-md-3 text-left">
                                                                    <asp:Button ID="btnBack2" runat="server" Text="Back" CssClass=" btn btn-info" OnClick="btnBack_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-wrapper col-md-12">
                                                <div class="text-center">
                                                    <asp:GridView ID="grvListSupervisor" runat="server" AutoGenerateColumns="False" Style="position: relative"
                                                        Width="700px" DataKeyNames="pk_Supervisor_ID,pk_Inst_ID,Inst_Code,Inst_Name,SupervisorName,Mobile_Number,Email_ID"
                                                        EnableModelValidation="True">
                                                        <RowStyle CssClass="gridItem" />
                                                        <HeaderStyle CssClass="gridHeader" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Inst_Code" HeaderText="Inst Code"></asp:BoundField>
                                                            <asp:BoundField DataField="Inst_Name" HeaderText="Inst Name"></asp:BoundField>
                                                            <asp:BoundField DataField="SupervisorName" HeaderText="Supervisor Name"></asp:BoundField>
                                                            <asp:BoundField DataField="Mobile_Number" HeaderText="Mobile Number"></asp:BoundField>
                                                            <asp:BoundField DataField="Email_ID" HeaderText="Email ID"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="Select All">
                                                                <HeaderTemplate>
                                                                    Select All
                                                                    <center>
                                                                        <asp:CheckBox runat="server" ID="chkSelectAll" Style="position: relative" onClick="SelectAllCheckboxes();">
                                                                        </asp:CheckBox>
                                                                    </center>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <center>
                                                                        <asp:CheckBox runat="server" ID="chkSelect" Style="position: relative" onClick="chkValidate();" />
                                                                    </center>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <div class="col-md-12 menu-title">
                                                    <div class="text-center">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <div class="col-md-9  text-right">
                                                                    <asp:Button ID="btnSave1" runat="server" Text="Save" CssClass=" btn btn-info" OnClick="btnSave_Click"
                                                                        OnClientClick="return validate();" />
                                                                </div>
                                                                <div class="col-md-3 text-left">
                                                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass=" btn btn-info" OnClick="btnBack_Click" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div id="divbtnConfiguration" runat="server" style="display: none">
                                            <div class="row mb-2">
                                                <div class="col-md-4 menu-title text-left">
                                                    <asp:TextBox ID="txtSearchConfigure" runat="server" MaxLength="10" CssClass="search-style"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 menu-title text-left">
                                                    <asp:Button ID="btnSearchConfigure" runat="server" Text="Search" CssClass=" btn btn-info"
                                                        OnClick="btnSearchConfigure_Click" />
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <div class="col-md-12 menu-title">
                                                    <div class="text-center">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <div class="col-md-9  text-right">
                                                                    <asp:Button ID="btnSave2" runat="server" Text="Save" CssClass=" btn btn-info" OnClick="btnUpdate_Click"
                                                                        OnClientClick="return validateConfiguration();" />&nbsp;
                                                                </div>
                                                                <div class="col-md-3 text-left">
                                                                    <asp:Button ID="btnBack3" runat="server" Text="Back" CssClass=" btn btn-info" OnClick="btnBack_Click" />&nbsp;
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-wrapper col-md-12">
                                                <div class="text-center">
                                                    <asp:GridView ID="GrvConfigured" runat="server" AutoGenerateColumns="False" Style="position: relative"
                                                        Width="700px" DataKeyNames="pk_Supervisor_ID,pk_Inst_ID,Inst_Code,Inst_Name,SupervisorName,Mobile_Number,Email_ID,IsDisabled"
                                                        EnableModelValidation="True" OnPageIndexChanging="GrvConfigured_PageIndexChanging"
                                                        OnRowDataBound="GrvConfigured_RowDataBound">
                                                        <RowStyle CssClass="gridItem" />
                                                        <HeaderStyle CssClass="gridHeader" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Inst_Code" HeaderText="Inst Code"></asp:BoundField>
                                                            <asp:BoundField DataField="Inst_Name" HeaderText="Inst Name"></asp:BoundField>
                                                            <asp:BoundField DataField="SupervisorName" HeaderText="Supervisor Name"></asp:BoundField>
                                                            <asp:BoundField DataField="Mobile_Number" HeaderText="Mobile Number"></asp:BoundField>
                                                            <asp:BoundField DataField="Email_ID" HeaderText="Email ID"></asp:BoundField>
                                                            <asp:TemplateField HeaderText="Select All">
                                                                <HeaderTemplate>
                                                                    Enabled/Disabled
                                                                    <center>
                                                                        <asp:CheckBox runat="server" ID="chkSelectAll" Style="position: relative" onClick="SelectAllCheckboxesConfiguration();">
                                                                        </asp:CheckBox>
                                                                    </center>
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <center>
                                                                        <asp:CheckBox runat="server" ID="chkSelect" Style="position: relative" onClick="chkValidateConfiguration();" />
                                                                    </center>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Right" CssClass="GridPager" />
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="row mb-2">
                                                <div class="col-md-12 menu-title">
                                                    <div class="text-center">
                                                        <div class="form-horizontal">
                                                            <div class="form-group">
                                                                <div class="col-md-9  text-right">
                                                                    <asp:Button ID="btnSave3" runat="server" Text="Save" CssClass=" btn btn-info" OnClick="btnUpdate_Click"
                                                                        OnClientClick="return validateConfiguration();" />&nbsp;
                                                                </div>
                                                                <div class="col-md-3 text-left">
                                                                    <asp:Button ID="btnBack1" runat="server" Text="Back" CssClass=" btn btn-info" OnClick="btnBack_Click" />&nbsp;
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
