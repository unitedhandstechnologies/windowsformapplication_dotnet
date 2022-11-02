<%@ Page Title="" Language="C#" MasterPageFile="~/Home.Master" AutoEventWireup="true" CodeBehind="PreExamV2_SecureQuestionPaperDownload__2.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SecureQuestionPaperDownload__2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <script language="javascript" type="text/javascript">

     function Hide_UnHide(obj) {
         var node = obj;
         if (node.src.indexOf("plus.gif") != -1)
         { node.src = "<%=Classes.clsGetSettings.SitePath%>images/minus.gif"; node.title = "Collapse"; }
         else { node.src = "<%=Classes.clsGetSettings.SitePath%>images/plus.gif"; node.title = "Expand"; }

         while (node.tagName != 'TABLE') node = node.parentNode;

         node = node.getElementsByTagName('DIV')[0];

         if (node.style.display == "none") node.style.display = "block";
         else node.style.display = "none";
     }

     function fnExpandAll(obj) {
         var tbl = document.getElementById(obj);
         var len = document.getElementById(obj).rows.length;

         for (var i = 1; i < len; i++) {
             if (tbl.rows[i].cells[1] != null) {

                 var divObj = tbl.rows[i].cells[1].children[0].children[0].children[1].children[0].children[0];
                 var imgObj = tbl.rows[i].cells[1].children[0].children[0].children[0].children[0].children[0];

                 var str = imgObj.src;

                 var strArr = str.split("/");

                 if (strArr[strArr.length - 1] == "plus.gif") {
                     imgObj.src = "<%=Classes.clsGetSettings.SitePath%>images/minus.gif";
                     divObj.style.display = "block";
                     IsExpand = true;
                 }
             }
             else
                 break;
         }
         return false;
     }

     function fnCollapseAll(obj) {
         var tbl = document.getElementById(obj);
         var len = document.getElementById(obj).rows.length;

         for (var i = 1; i < len; i++) {

             if (tbl.rows[i].cells[1] != null) {


                 var divObj = tbl.rows[i].cells[1].children[0].children[0].children[1].children[0].children[0];
                 var imgObj = tbl.rows[i].cells[1].children[0].children[0].children[0].children[0].children[0];

                 var str = imgObj.src;
                 var strArr = str.split("/");

                 if (strArr[strArr.length - 1] == "minus.gif") {
                     imgObj.src = "<%=Classes.clsGetSettings.SitePath%>images/plus.gif";
                     divObj.style.display = "none";

                 }
             }
             else
                 break;
         }
         return false;
     }

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

     function setIDs(ddl, hidValue, hidText) {
         debugger;
         var client_ddl = document.getElementById(ddl);
         var client_hidValue = document.getElementById(hidValue);
         client_hidValue.value = client_ddl.value;
         return;
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
                                            <asp:Label ID="lblMsg" runat="server" meta:resourcekey="lblMsgResource1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 706px; height: 110px; text-align: left" align="left" valign="top">
                                            <asp:Label ID="lblMesg" runat="server" meta:resourcekey="lblMesgResource1"></asp:Label>
                                            <fieldset id="ShowDownloadDataDetails" runat="server" width="100%">
                                                <table width="100%">
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
                                                        <td>
                                                            <br />
                                                            <table width="100%" align="center" runat="server" id="tblLnk">
                                                                <tr id="Tr1" runat="server">
                                                                    <td id="Td2" align="left" class="BorderTB" runat="server">
                                                                        <asp:LinkButton ID="lnkBtnExpand" ForeColor="BlueViolet" CssClass="Side" runat="server"
                                                                            Text="Expand All" title="Expand All" meta:resourcekey="lnkBtnExpandResource1"></asp:LinkButton>&nbsp;&nbsp;|&nbsp;
                                                                        <asp:LinkButton ID="lnkBtnCollapse" ForeColor="BlueViolet" CssClass="Side" runat="server"
                                                                            Text="Collapse All" title="Collapse All" meta:resourcekey="lnkBtnCollapseResource1"></asp:LinkButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="100%">
                                                            <asp:GridView ID="gvPapers" runat="server" Width="685px" AutoGenerateColumns="False" EnableViewState = "true"
                                                                CssClass="clGrid" HorizontalAlign="Center" EnableModelValidation="True" OnRowDataBound="gvPapers_RowDataBound"
                                                                DataKeyNames="ExamDateTime,ExamStartTime,ExamDate,ExamEndTime,ZipFilePwd,pk_ExEv_ID"
                                                                meta:resourcekey="gvPapersResource1" OnRowCommand="gvPapers_RowCommand" OnRowCreated="gvPapers_RowCreated">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="Sr.No." meta:resourcekey="TemplateFieldResource1">
                                                                        <ItemTemplate>
                                                                            <%# (Container.DataItemIndex)+1 %>.
                                                                        </ItemTemplate>
                                                                        <ItemStyle VerticalAlign="Top" Width="5%" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Exam Date time wise Papers" meta:resourcekey="TemplateFieldResource4">
                                                                        <ItemTemplate>
                                                                            <table width="100%">
                                                                                <tr align="left" width="100%">
                                                                                    <td>
                                                                                        <img id="iplus" runat="server" alt="Collapse" onclick="Hide_UnHide(this)" onmouseover="javascript:this.style.cursor='hand'" />
                                                                                        &nbsp;<asp:Label ID="lblSlot" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ExamDateTime") %>'
                                                                                            Width="90%" meta:resourcekey="lblSlotResource1"></asp:Label>
                                                                                        <input id="hidExamStartTime" value='<%# DataBinder.Eval(Container, "DataItem.ExamStartTime") %>'
                                                                                            runat="server" type="hidden" />
                                                                                        <input id="hidExamDate" runat="server" type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.ExamDate") %>' />
                                                                                        <input id="hidExamEndTime" runat="server" type="hidden" value='<%# DataBinder.Eval(Container, "DataItem.ExamEndTime") %>' />
                                                                                        <br />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td colspan="4">
                                                                                        <div id="divCoursePart" runat="server" class="gridItem" style="display: block;">
                                                                                            &nbsp;<asp:GridView ID="dgchild" runat="server" AutoGenerateColumns="False" BorderStyle="Double"
                                                                                                BorderWidth="3px" CellPadding="4" CssClass="clGrid" EnableModelValidation="True"
                                                                                                GridLines="Horizontal" HorizontalAlign="Center" OnRowCommand="gvChild_RowCommand"
                                                                                                Width="100%" ShowFooter="True" OnRowDataBound="gvChild_RowDataBound" DataKeyNames="pk_Uni_ID,pk_Fac_ID,pk_Cr_ID,pk_MoLrn_ID,pk_Ptrn_ID,pk_Brn_ID,pk_CrPr_Details_ID,pk_CrPrCh_ID,pk_ExEv_ID,pk_Pp_PpHead_CrPrCh_ID,pk_TchLrnMth_ID,pk_AssMth_ID,pk_AssType_ID,pk_Inst_ID,FileName,Paper,ExamDateTime,ExamStartTime,ExamDate,DownloadStatus"
                                                                                                meta:resourcekey="dgchildResource1">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField HeaderText="SrNo." meta:resourcekey="TemplateFieldResource2">
                                                                                                        <ItemTemplate>
                                                                                                            <center>
                                                                                                                <%# Container.DisplayIndex + 1 %>
                                                                                                            </center>
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:BoundField DataField="Paper" HeaderText="Paper-TLM-AM-AT" ReadOnly="True" meta:resourcekey="BoundFieldResource1" />
                                                                                                    <asp:TemplateField HeaderText="Download Status" meta:resourcekey="TemplateFieldResource3">
                                                                                                        <ItemTemplate>
                                                                                                            <center>
                                                                                                                <asp:Label ID="lblDownloadStatus" runat="server" meta:resourcekey="lblDownloadStatusResource1"></asp:Label>
                                                                                                            </center>
                                                                                                        </ItemTemplate>
                                                                                                        <FooterTemplate>
                                                                                                            <center>
                                                                                                                <table id="tblDownload" runat="server">
                                                                                                                    <tr>
                                                                                                                        <td align="center">
                                                                                                                            <asp:Button ID="btnDownload" CommandArgument='<%# Container.DisplayIndex %>' CommandName="Download"
                                                                                                                                runat="server" Width="150px" class="ButSp" Text="Download" meta:resourcekey="btnDownloadResource1" />
                                                                                                                        </td>
                                                                                                                    </tr>
                                                                                                                </table>
                                                                                                            </center>
                                                                                                        </FooterTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <FooterStyle BackColor="#336699" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                                                                                            </asp:GridView>
                                                                                        </div>
                                                                                        <div id="divSupervisor" runat="server">
                                                                                            <table id="tblSupervisor" runat="server" align="center">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <b>Select Supervisor</b>
                                                                                                    </td>
                                                                                                    <td>
                                                                                                        :
                                                                                                    </td>
                                                                                                    <td id="tdSupervisor" runat="server">
                                                                                                        <asp:DropDownList ID="ddlSupervisor" runat="server" AppendDataBoundItems="True" AutoPostBack="false">
                                                                                                            <asp:ListItem Value="-1" Text="--Select--"></asp:ListItem>
                                                                                                        </asp:DropDownList>         
                                                                                                        
                                                                                                          <input id="hidSupervisorID" type="hidden" runat="server" />
                                                                                                        <input id="hidSupervisorName" type="hidden" runat="server" />                                                                                              
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <br />
                                                                                        </div>
                                                                                        <div id="divOTP" runat="server">
                                                                                            <table id="tblOTP" runat="server" align="center">
                                                                                                <tr>
                                                                                                    <td align="center">
                                                                                                        <asp:Button ID="btnGetOTP" runat="server" Text="Get Download Password" CssClass="ButSp"
                                                                                                            CommandArgument='<%# Container.DisplayIndex %>' CommandName="GetOTP" />

                                                                                                        <input id="hidOTP" runat="server" type="hidden" />
                                                                                                       
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                        <div id="divVerifyOTP" runat="server">
                                                                                            <table id="tblVerifyOTP" runat="server" style="display: none" align="center">
                                                                                                <tr>
                                                                                                    <td colspan="3" align="right">
                                                                                                        <asp:Label ID="lblVerifyMsg" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="right">
                                                                                                        <asp:Label ID="lblEnterPassword" runat="server" Text="Enter Download Password" Font-Bold="true"></asp:Label>
                                                                                                    </td>
                                                                                                    <td align="center">
                                                                                                        <b>: </b>
                                                                                                        <asp:TextBox ID="txtPassword" runat="server"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="right">
                                                                                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="ButSp" CommandArgument='<%# Container.DisplayIndex %>'
                                                                                                            CommandName="VerifyOTP" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                        <div>
                                                                                            <table id="tblDownloadMsg" runat="server" style="display: none">
                                                                                                <tr>
                                                                                                    <td align="center">
                                                                                                        <asp:Label ID="lblDownloadMsg" runat="server"></asp:Label>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="90%" />                                                                      
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <HeaderStyle CssClass="gridHeader" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                                                    Font-Strikeout="False" Font-Underline="False" Wrap="True" />
                                                                <RowStyle Wrap="True" CssClass="gridItem"></RowStyle>
                                                            </asp:GridView>
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
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </center>

</asp:Content>
