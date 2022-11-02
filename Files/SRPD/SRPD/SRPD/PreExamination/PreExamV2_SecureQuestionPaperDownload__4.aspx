<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SecureQuestionPaperDownload__4.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SecureQuestionPaperDownload__4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script language="javascript" type="text/javascript">

    function validate(btn, txt, hid) {
        var btn = btn;

        var txtPassword = txt;

        if (txtPassword.value == '') {
            alert('Please Enter Correct Password!');
            return false;
        }
        else {
            if (txtPassword.value == hid) {
                return true;
            }
            else {
                alert('Invalid Password');
                return false;
            }
        }
    }

    function validateDropDown() {
        var ddl = document.getElementById('<%=ddlSupervisor.ClientID%>')

        if (ddl.value == '-1') {
            alert('Please select supervisor from drop down list');

            return false;
        }
        else {

            return true;
        }


    }
    </script>
    <center>
        <table cellpadding="0" cellspacing="0" width="700" height="30px">
            <tr valign="top">
                <td align="left" style="border-bottom: 1px solid #FFD275;">
                    <asp:Label ID="lblPageHead" runat="server" Text="Secure Question Paper Download "></asp:Label>
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
                                    <tr>
                                        <td style="width: 706px; height: 110px; text-align: left" align="left" valign="top">
                                            <asp:Label ID="lblMesg" runat="server" meta:resourcekey="lblMesgResource1"></asp:Label>
                                            <fieldset id="ShowDownloadDataDetails" runat="server" width="100%">
                                                <table width="700">
                                                    <tr id="headerConfiguredData" runat="server">
                                                        <td id="Td1" align="right" colspan="2" style="text-align: left; height: 21px;" class="clSubHeading "
                                                            runat="server">
                                                            <span>List of papers for download</span>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" class="BorderTB">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100%">
                                                            <div>
                                                                <table id="Table1" runat="server">
                                                                    <tr>
                                                                        <td align="left">
                                                                            <asp:Label ID="lblGrvMsg" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div id="divPapers" runat="server" width="100%">
                                                                <table width="100%">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:GridView ID="gvPapers" runat="server" AutoGenerateColumns="False" CssClass="clGrid"
                                                                                EnableModelValidation="True" Width="100%" DataKeyNames="pk_ExEv_ID,pk_Inst_ID,FileName,pk_Pp_ID,Pp_Code,Pp_Name,TLMAMAT,Paper,
                                                                                ExamDateTime,ExamStartTime,ExamDate,ExamEndTime,DownloadStatus,QpCode,PpQuery" 
                                                                                OnRowDataBound="gvPapers_RowDataBound" onrowcommand="gvPapers_RowCommand">
                                                                                <Columns>
                                                                                    <asp:TemplateField HeaderText="SrNo." meta:resourcekey="TemplateFieldResource2">
                                                                                        <ItemTemplate>
                                                                                            <center>
                                                                                                <%# Container.DisplayIndex + 1 %>
                                                                                            </center>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:BoundField DataField="Paper" HeaderText="Paper-TLM-AM-AT" ReadOnly="True" meta:resourcekey="BoundFieldResource1" />
                                                                                    <asp:TemplateField HeaderText="Download Status">
                                                                                        <ItemTemplate>
                                                                                            <center>
                                                                                                <asp:Label ID="lblDownloadStatus" runat="server"></asp:Label>
                                                                                            </center>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField>
                                                                                        <ItemTemplate>
                                                                                            <center>
                                                                                                <center>
                                                                                                    <asp:LinkButton runat="server" CommandName="Query" ID="lnkSelect" CommandArgument='<%# Container.DisplayIndex %>'><b>Submit Query</b>
                                                                                                    </asp:LinkButton>
                                                                                                </center>
                                                                                            </center>
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <br />
                                                            <div id="divSupervisor" runat="server">
                                                                <table id="tblSupervisor" runat="server" align="center" width="700">
                                                                    <tr>
                                                                        <td align="right">
                                                                            <b>Select Supervisor</b>
                                                                        </td>
                                                                        <td align="center" width="2px">
                                                                            :
                                                                        </td>
                                                                        <td id="tdSupervisor" runat="server" align="left">
                                                                            <asp:DropDownList ID="ddlSupervisor" runat="server" AppendDataBoundItems="True" AutoPostBack="false">
                                                                                <asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" align="right">
                                                                            <asp:Label ID="lblSupervisorNote" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                <br />
                                                            </div>
                                                            <div id="divOTP" runat="server">
                                                                <table id="tblOTP" runat="server" align="center" width="700">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <asp:Button ID="btnGetOTP" runat="server" Text="Get Download Password" CssClass="ButSp"
                                                                                OnClick="btnGetOTP_Click" OnClientClick="return validateDropDown();" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div id="divVerifyOTP" runat="server">
                                                                <br />
                                                                <table id="tblVerifyOTP" runat="server" width="700">
                                                                    <tr>
                                                                        <td colspan="3" align="left">
                                                                            <asp:Label ID="lblNote" runat="server"></asp:Label>
                                                                            <br />
                                                                            <br />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="3" align="right">
                                                                            <asp:Label ID="lblVerifyMsg" runat="server"></asp:Label>
                                                                            <br />
                                                                            <br />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td align="right" width="40%">
                                                                            <asp:Label ID="lblEnterPassword" runat="server" Text="Enter Download Password" Font-Bold="true"></asp:Label>
                                                                            <b>: </b>
                                                                        </td>
                                                                        <td align="center" width="20%">
                                                                            <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Button ID="btnVerifyOTP" runat="server" Text="Submit" CssClass="ButSp" OnClick="btnVerifyOTP_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div align="right">
                                                                <table id="tblDownloadMsg" runat="server" align="right">
                                                                    <tr>
                                                                        <td align="right">
                                                                            <asp:Label ID="lblDownloadMsg" runat="server"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div id="divDownload" runat="server" width="700" align="center">
                                                                <table id="tblDownload" runat="server">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <br />
                                                                            <asp:Button ID="btnDownload" runat="server" Width="150px" class="ButSp" Text="Download"
                                                                                OnClick="btnDownload_Click" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" style="width: 100px; height: 15px; text-align: center;" valign="middle">
                                                            &nbsp;
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
                                <input id="hidPpPpHeadCrPrChID" type="hidden" runat="server" />
                                <input id="hidTchLrnMthID" type="hidden" runat="server" />
                                <input id="hidAssMthID" type="hidden" runat="server" />
                                <input id="hidAssTyID" type="hidden" runat="server" />
                                <input id="hidVenueCode" type="hidden" runat="server" />
                                <input id="hidExamEvent" type="hidden" runat="server" />
                                <input id="hidSupervisorID" type="hidden" runat="server" />
                                <input id="hidSupervisorName" type="hidden" runat="server" />
                                <input id="hidOTP" runat="server" type="hidden" />
                                <input id="hidExamDateTime" type="hidden" runat="server" />
                                <input id="hidExamStartTime" type="hidden" runat="server" />
                                <input id="hidExamEndTime" type="hidden" runat="server" />
                                <input id="hidExamDate" type="hidden" runat="server" />
                                <input id="hidZipFilePwd" type="hidden" runat="server" />                                
                                <input id="hidIpAddress" type="hidden" runat="server" />
                                <input id="hidPpID" type="hidden" runat="server" />  
                                <input id="hidPpCode" type="hidden" runat="server" />     
                                <input id="hidPpName" type="hidden" runat="server" />  
                                <input id="hidTLMAMAT" type="hidden" runat="server" />   
                                <input id="hidQpCode" type="hidden" runat="server" />    
                                <input id="hidQuery" type="hidden" runat="server" />                     
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
