<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SecureQuestionPaperDownload__5.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SecureQuestionPaperDownload__5" %>
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
                                                                                EnableModelValidation="True" Width="100%" DataKeyNames="pk_Inst_ID,FileName,Paper,ExamDateTime,ExamStartTime,ExamDate,ExamEndTime,DownloadStatus"
                                                                                OnRowDataBound="gvPapers_RowDataBound">
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
                                                                                </Columns>
                                                                                <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                            </asp:GridView>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                             <br />
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>
</asp:Content>
