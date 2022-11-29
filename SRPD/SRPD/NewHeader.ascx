<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewHeader.ascx.cs" Inherits="DPTemplate2.NewHeader" %>
<%@ Register Src="ColorPallete.ascx" TagName="ColorPallete" TagPrefix="uc1" %>
<%@ Register Src="LanguageOptions.ascx" TagName="LanguageOptions" TagPrefix="uc2" %>
<div>
    <div id="logo">
        <asp:Image ID="Image1" Width="60" Height="60" runat="server" meta:resourcekey="Image1Resource1" />
    </div>
    <div id="header">
        <div id="headerLink">
            <font face="Verdana" size="1">
                <ul>
                    <li>
                        <asp:HyperLink ID="Hyperlink8" runat="server" meta:resourcekey="Hyperlink8Resource1">Home</asp:HyperLink>
                    </li>
                    <asp:Label ID="pnlLogin" runat="server" align="right" >
                        <li>
                            <span class='bul'></span>
                            <asp:HyperLink ID="hlkHome" runat="server" meta:resourcekey="hlkHomeResource1">My Login</asp:HyperLink>
                        </li>
                        <li><span class='bul' ID="bulMySettings" runat="server"></span>&nbsp;<asp:HyperLink ID="hlkMySettings" runat="server" meta:resourcekey="hlkMySettingsResource1">My Settings</asp:HyperLink>
                        </li>   </asp:Label>
                        <li>
                         <span class='bul'></span>
                            <asp:HyperLink ID="hlkLogout" runat="server" meta:resourcekey="hlkLogoutResource1">Logout</asp:HyperLink>
                        </li>
                 <asp:Label ID="pnlLoginOther" runat="server" align="right" >
                    <li>
                     <span class='bul'></span>
                        <asp:HyperLink ID="HyperLink3" runat="server" meta:resourcekey="HyperLink3Resource1">Calendar</asp:HyperLink>
                    </li>
                    <li>
                     <span class='bul'></span>
                        <asp:HyperLink ID="HyperLink4" runat="server" meta:resourcekey="HyperLink4Resource1">Sitemap</asp:HyperLink>
                    </li>
                    <li>
                     <span class='bul'></span>
                        <asp:HyperLink ID="HyperLink5" runat="server" meta:resourcekey="HyperLink5Resource1">Contact Us</asp:HyperLink>
                    </li>
                    </asp:Label>
                </ul>
            </font>
        </div>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td align="left" valign="top">
                    <div>
                        <asp:Label ID="lblName" runat="server" CssClass="logoName" meta:resourcekey="lblNameResource1"></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblAddress" runat="server" CssClass="logoAddress" meta:resourcekey="lblAddressResource1"></asp:Label>
                    </div>
                </td>
                <td align="right" valign="bottom" style="width: 245px; padding-right: 5px;">
                    <%--<div style="padding-top: 10px; margin-top: 10px;">
                        <div style="float: left">
                            <!-- BEGIN : Language options-->
                            <uc2:LanguageOptions ID="LanguageOptions1" runat="server" />
                            <!-- END : Language options -->
                        </div>
                        <div style="float: right">
                            <!-- Begin : color Pallete -->
                            <uc1:ColorPallete ID="ColorPallete1" runat="server" />
                            <!-- End : color Pallete -->
                        </div>
                    </div>--%>
                </td>
            </tr>
        </table>
    </div>
</div>

<script type="text/javascript">   
//
    document.getElementById("<%=Hyperlink8.ClientID%>").setAttribute("href", "<%=Classes.clsGetSettings.PublicSitePath%>");        
    document.getElementById("<%=hlkLogout.ClientID%>").setAttribute("href", "<%=sSitePath%>Logout.aspx");
    document.getElementById("<%=HyperLink3.ClientID%>").setAttribute("href", "<%=sSitePath%>CalendarDisplay.aspx");
    document.getElementById("<%=HyperLink4.ClientID%>").setAttribute("href", "<%=sSitePath%>SiteMap.aspx");
    document.getElementById("<%=HyperLink5.ClientID%>").setAttribute("href", "<%=sSitePath%>ContactUs.aspx");
</script>

