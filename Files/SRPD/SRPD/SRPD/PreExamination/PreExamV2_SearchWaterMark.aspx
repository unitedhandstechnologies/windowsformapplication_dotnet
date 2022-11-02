<%@ Page Title="" Language="C#" MasterPageFile="~/New_Home.Master" AutoEventWireup="true"
    CodeBehind="PreExamV2_SearchWaterMark.aspx.cs" Inherits="SRPD.PreExamination.PreExamV2_SearchWaterMark" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" language="javascript">

        function CheckValue() { 
        debugger;
            var letters = /^[A-Za-z]+$/;
            var txtvalue = document.getElementById('<%=txtWaterMark.ClientID%>').value;
           // alert(txtvalue);
            if (txtvalue == "") {
                alert("Please Enter Water Mark..");
                return false;
            }
            else          
             {
                    if (txtvalue.match(letters))
                    {
                        alert("Please Enter Numbers Only..");
                        return false;
                    }
                    else 
                    {
                      return true;
                    }                   
             }
            
        }
            
    </script>
    <div>
        <div class="row mb-2">
            <div class="col-md-12 menu-title">
                <center>
                    <div class="form-horizontal">
                        <div class="form-group">
                           <div class="col-md-4 offset-md-3">
                                <div>
                                    <asp:Label ID="Label1" runat="server" Text="Enter Water Mark" style="text-align:left;vertical-align:middle;" Font-Bold="true"></asp:Label>
                                </div>
                            </div>
                            <div class="col-md-4  controls text-center">
                                <div>
                                    <asp:TextBox ID="txtWaterMark" runat="server" MaxLength="10" BorderWidth="1" class="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-4 text-left">
                                <asp:Button ID="btnDateSeletion" runat="server" Text="Search" CssClass=" btn btn-info"
                                    OnClick="btnDateSelection_Click" OnClientClick="return CheckValue();"/>
                            </div>
                        </div>
                    </div>
                </center>
            </div>
        </div>
        <div  class="col-md-12 id="divmsg" runat="server"  visible="false" style="text-align:right">       
               <asp:Label ID="lblmsg" runat="server" Text="" Visible="false"  Font-Bold="true"></asp:Label>
        </div>
        <div class="row mb-2">
            <div class="table-wrapper col-md-12">
                <center>
                    <asp:GridView ID="grvWaterMarkDetails" runat="server" AutoGenerateColumns="False"
                        EnableModelValidation="True">
                        <Columns>
                            <asp:BoundField DataField="VenueCode" HeaderText="Venue Code"  ItemStyle-Width="20px" />
                            <asp:BoundField DataField="VenueName" HeaderText="Venue Name" />
                            <asp:BoundField DataField="PaperCode" HeaderText="Paper Code" />
                            <asp:BoundField DataField="PaperName" HeaderText="Paper Name" />
                            <asp:BoundField DataField="ProgramName" HeaderText="Program Name(Full Name Of Program)" />
                            <asp:BoundField DataField="Date" HeaderText="Date" />
                            <asp:BoundField DataField="Time" HeaderText="Time" />
                        </Columns>
                    </asp:GridView>
                </center>
            </div>
        </div>
      
    </div>
    <input id="hidUniId" type="hidden" runat="server" />
</asp:Content>
