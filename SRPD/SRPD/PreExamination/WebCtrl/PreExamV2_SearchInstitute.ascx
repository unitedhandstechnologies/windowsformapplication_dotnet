<%@ Control Language="C#" AutoEventWireup="true" Codebehind="PreExamV2_SearchInstitute.ascx.cs"
    Inherits="PreExamV3WebCtrl.WebCtrl.PreExamV2_SearchInstitute" %>
<%--<script language="javascript" src="<%=Classes.clsGetSettings.SitePath%>jscript_validations.js"></script>
--%>
<link href="../css/style.css" type="text/css" rel="stylesheet" />
<%--<link href="../css/UniPortal.css" type="text/css" rel="stylesheet">

<script language="javascript" src="../JS/header.js"></script>

<script language="javascript" src="../JS/footer.js" type="text/javascript"></script>

--%>

<script type="text/javascript" language="javascript">
		  function hidunhid()
		  {
		  var val;
		  val = document.getElementById("<%=hidCountryId.ClientID%>").value
		  hideUnhide(val)
		  }

		    function hideUnhide(val)
		    {
		   
		    document.getElementById("<%=hidCountryId.ClientID%>").value = val;
		    if(val=="107")
		    {
		   
		    document.getElementById("<%=trDistrict.ClientID%>").style.display="block"; 
		    document.getElementById("<%=trTahsil.ClientID%>").style.display="block"; 
		    document.getElementById("<%=trState.ClientID%>").style.display="block"; 
		    
		    }
		    else
		    {
		    document.getElementById("<%=trDistrict.ClientID%>").style.display="none"; 
		    document.getElementById("<%=trTahsil.ClientID%>").style.display="none"; 
		    document.getElementById("<%=trState.ClientID%>").style.display="none"; 
		    }
		    
		    }
			  
			function FillDistrictDD(val)
			{ 
			
		      var TbDistID= document.getElementById("<%=TbDistID.ClientID%>").id
		     
		      var District_ID = document.getElementById("<%=District_ID.ClientID %>").id ;
		     
			     FillDistrict(TbDistID,val,District_ID,0);
			   
			     document.getElementById("<%=hidStateID.ClientID%>").value = val;
			}
			function FillTalukaDD(val)
			{  
			    var TbTalID = document.getElementById("<%=TbTalID.ClientID %>").id;
			    var Tehsil_ID = document.getElementById("<%=Tehsil_ID.ClientID %>").id;
			     FillTaluka(TbTalID , val ,Tehsil_ID ,0);
			     document.getElementById("<%=hidDistrictID.ClientID%>").value = val;
			      
			}
		    function setTaluka(val)
		    { 
		   
			      document.getElementById("<%=hidTehsilID.ClientID%>").value = val;
			    
	   	    }
			function setOtherTaluka()
			{
			}
			
		
			
</script>

<%--<style type="text/css">
.datagridHeader
{
   font-weight: bold;
   vertical-align:middle;
   font-family: Verdana; 
   text-align:center;
	font-size: 8pt;
	font-weight:bold; 
	color:#6f6f6f;
	background-color: #efefef;
	padding:5px;
	border:1px #5975a4 solid;
}
</style>--%>
<asp:UpdatePanel ID="update1" runat="server">
    <Triggers>
        <asp:PostBackTrigger ControlID="gvData" />
        <asp:PostBackTrigger ControlID="btnSearch" />
    </Triggers>
    <ContentTemplate>
        <div align="center" style="vertical-align: top">
            <fieldset class="fieldSet" id="tblSelect" runat="server">
                <legend><strong>Search</strong></legend>
                <table cellspacing="3" cellpadding="0" width="100%" align="center" border="0" style="vertical-align: top">
                    <tr>
                        <td align="right" width="20%">
                            <strong>Type</strong></td>
                        <td align="center" width="1%">
                            <b>:</b></td>
                        <td width="79%" align="left">
                            <asp:RadioButtonList ID="rdbtnInstType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal"
                                ForeColor="Navy" Font-Bold="True" meta:resourcekey="rdbtnInstTypeResource2">
                            </asp:RadioButtonList></td>
                    </tr>
                    <tr>
                        <td align="right" width="20%">
                            <b><span id="lblName0">Name</span></b></td>
                        <td align="center" width="1%">
                            <b>:</b></td>
                        <td width="79%" align="left">
                            <asp:TextBox ID="Inst_Name" runat="server" CssClass="inputbox" Width="395px" MaxLength="300"
                                meta:resourcekey="Inst_NameResource2"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="height: 17px" align="right" width="20%">
                            <b>Country&nbsp; </b>
                        </td>
                        <td style="height: 17px" align="center" width="1%">
                            <b>:</b></td>
                        <td style="height: 17px" width="79%" align="left">
                            <b>
                                <asp:DropDownList ID="ddlCountry" runat="server" Width="184px" onchange="hideUnhide(this.value);"
                                    OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged" meta:resourcekey="ddlCountryResource2">
                                </asp:DropDownList></b></td>
                    </tr>
                    <tr id="trState" runat="server">
                        <td style="height: 17px" align="right" width="20%" runat="server">
                            <b>State&nbsp; </b>
                        </td>
                        <td style="height: 17px" align="center" width="1%" runat="server">
                            <b>:</b></td>
                        <td style="height: 17px" width="79%" align="left" runat="server">
                            <b>
                                <asp:DropDownList ID="State_ID" runat="server" Width="184px" OnSelectedIndexChanged="State_ID_SelectedIndexChanged"
                                    AutoPostBack="True">
                                </asp:DropDownList></b></td>
                    </tr>
                    <tr id="trDistrict" runat="server">
                        <td style="height: 20px" align="right" width="20%" runat="server">
                            <b>District</b></td>
                        <td style="height: 20px" align="center" width="1%" runat="server">
                            <b>:</b></td>
                        <td id="TbDistID" style="height: 20px" width="79%" align="left" runat="server">
                            <asp:DropDownList ID="District_ID" runat="server" Width="184px" OnSelectedIndexChanged="District_ID_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr id="trTahsil" runat="server">
                        <td style="height: 26px" align="right" width="20%" runat="server">
                            <b>Tahsil</b></td>
                        <td style="height: 26px" align="center" width="1%" runat="server">
                            <b>:</b></td>
                        <td id="TbTalID" style="height: 26px" width="79%" align="left" runat="server">
                            <asp:DropDownList ID="Tehsil_ID" runat="server" Width="184px" OnSelectedIndexChanged="Tehsil_ID_SelectedIndexChanged"
                                AutoPostBack="True">
                            </asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td align="right" width="20%">
                            <b>
                                <asp:Label ID="lblCollege" runat="server" Text="College" meta:resourcekey="lblCollegeResource1"></asp:Label>
                                Code</b>
                        </td>
                        <td align="center" width="1%">
                            <b>:</b></td>
                        <td width="79%" align="left">
                            <asp:TextBox ID="Collcode" runat="server" CssClass="inputbox" Width="100px" MaxLength="300"
                                meta:resourcekey="CollcodeResource2"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <asp:Button ID="btnSearch" runat="server" CssClass="butSubmit" Width="70px" Height="18px"
                                Text="Search" BorderStyle="Solid" BorderWidth="1px" OnClick="btnSearch_Click"
                                CausesValidation="False" meta:resourcekey="btnSearchResource2"></asp:Button></td>
                    </tr>
                </table>
            </fieldset>
        </div>
        </div>
        <br />
        <br />
        <fieldset id="fldPapers" runat="server">
            <legend><strong>
                <asp:Label ID="lblCollegeDetails" runat="server" Text="College Details" meta:resourcekey="lblCollegeDetailsResource1"></asp:Label></strong></legend>
            <p style="margin-top: 10px; margin-bottom: 1px; margin-left: 0px" align="center">
                <asp:Label ID="lblGridName" runat="server" CssClass="clSubHeading" Width="700" Height="18px"
                    meta:resourcekey="lblGridNameResource2"></asp:Label></p>
            <p style="margin-top: 0px; margin-bottom: 0px; margin-left: 0px" align="center">
                <asp:GridView ID="gvData" runat="server" CssClass="clGrid" Width="99%" AutoGenerateColumns="False"
                    AllowPaging="True" PageSize="25" OnRowCommand="gvData_RowCommand" OnRowDataBound="gvData_RowDataBound" meta:resourcekey="gvDataResource1" OnPageIndexChanging="gvData_PageIndexChanging">
                    <Columns>
                        <asp:ButtonField CommandName="lnkButSelect" Text="Select" HeaderText="Select" meta:resourcekey="ButtonFieldResource2">
                            <HeaderStyle Width="5%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Font-Bold="True">
                            </ItemStyle>
                        </asp:ButtonField>
                        <asp:BoundField DataField="pk_Inst_ID" HeaderText="pk_Inst_ID"  meta:resourcekey="BoundFieldResource17">
                            <HeaderStyle Width="0%"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Inst_Code" HeaderText="College Code" meta:resourcekey="BoundFieldResource18">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="InstName" HeaderText="Name of College" meta:resourcekey="BoundFieldResource19">
                            <HeaderStyle Width="50%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="Inst_City" HeaderText="City" meta:resourcekey="BoundFieldResource20">
                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="ITaluka_Name" HeaderText="Taluka" meta:resourcekey="BoundFieldResource21">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="District_Name" HeaderText="District" meta:resourcekey="BoundFieldResource22">
                            <ItemStyle HorizontalAlign="Left" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="InstTy_Name" HeaderText="Type" meta:resourcekey="BoundFieldResource23">
                            <HeaderStyle Width="15%"></HeaderStyle>
                        </asp:BoundField>
                        <asp:BoundField DataField="fk_InstTy_ID" HeaderText="Type" Visible="False" meta:resourcekey="BoundFieldResource24">
                            <HeaderStyle Width="15%"></HeaderStyle>
                        </asp:BoundField>
                    </Columns>
                    <RowStyle CssClass="gridItem" />
                    <HeaderStyle CssClass="gridHeader" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" />
                    <PagerStyle HorizontalAlign="Right" VerticalAlign="Middle" />
                </asp:GridView>
                <br />                
            </p>
        </fieldset> &nbsp;       
        <!-- Selection ends -->
        <div align="center">
            <asp:Label ID="lblData" runat="server" ForeColor="Tomato" Font-Bold="True" Width="99%"
                Visible="False" meta:resourcekey="lblDataResource2" Text="No Record Found"></asp:Label></div>
        <input id="hidInstID" style="width: 24px; height: 22px" type="hidden" runat="server" />
        <input id="hidCountryId" style="width: 24px; height: 22px" type="hidden" value="0"
            runat="server" />
        <input id="hidCntry" style="width: 24px; height: 22px" type="hidden" value="0" runat="server" />
        <input id="hidStateID" style="width: 24px; height: 22px" type="hidden" value="0"
            runat="server" />
        <input id="hidDistrictID" style="width: 24px; height: 22px" type="hidden" value="0"
            runat="server" />
        <input id="hidTehsilID" style="width: 24px; height: 22px" type="hidden" value="0"
            runat="server" />
        <input id="hidUniID" style="width: 24px; height: 22px" type="hidden" runat="server" /><input
            type="hidden" runat="server" id="hidregisterationInfo" />
    </ContentTemplate>
</asp:UpdatePanel>
 