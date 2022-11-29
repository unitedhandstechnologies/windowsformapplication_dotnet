<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="Header.ascx.cs" Inherits="DPTemplate2.Header"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register Src="ColorPallete.ascx" TagName="ColorPallete" TagPrefix="uc1" %>
<%@ Register Src="LanguageOptions.ascx" TagName="LanguageOptions" TagPrefix="uc2" %>

<script type="text/javascript">
    function getLinks(link) {
        window.location.href = link;
    }
</script>

<div id="main">
    <div id="logo">
        <asp:Image ID="Image1"  Width="60" Height="60" AlternateText="University Logo" runat="server" ImageUrl="Images/logo.jpg" meta:resourcekey="Image1Resource1" />
    </div>
    <div id="header">
        <div id="headerLink">
            <font face="Verdana" size="1">
                <ul>
                    <li>
                        <%--<a href="<%=Classes.clsGetSettings.PublicSitePath%>">--%>                        
                        <asp:Literal ID="Literal1" runat="server" EnableViewState="False" meta:resourcekey="Literal1Resource1"
                            Text="Home"></asp:Literal></a></li>
                    <asp:Label ID="pnlLogin" runat="server" align="right">
                        <li>
                             <span class='bul'></span>
                            <asp:HyperLink runat="server" NavigateUrl="/Home.aspx" ID="hlkHome" meta:resourcekey="hlkHomeResource1"
                                Text="My Login"></asp:HyperLink>
                        </li>
                        <li>
                             <span class='bul'></span>
                            <asp:HyperLink runat="server" ID="hlkMySettings" meta:resourcekey="hlkMySettingsResource1"
                                Text="My Settings"></asp:HyperLink>
                        </li>
                        <li>
                             <span class='bul'></span>
                            <asp:HyperLink runat="server" NavigateUrl="/Logout.aspx" ID="hlkLogout" meta:resourcekey="hlkLogoutResource1"
                                Text="Logout"></asp:HyperLink>
                        </li>
                    </asp:Label>
                    <li> <span class='bul'></span>
                    <a href='/CalendarDisplay.aspx'>                        
                        <asp:Literal ID="Literal2" runat="server" EnableViewState="False" meta:resourcekey="Literal2Resource1"
                            Text="Calender"></asp:Literal>
                    </a></li>
                    <li> <span class='bul'></span><a href='/SiteMap.aspx'>
                        <asp:Literal ID="Literal3" runat="server" EnableViewState="False" meta:resourcekey="Literal3Resource1"
                            Text="SiteMap"></asp:Literal></a> </li>
                    <li>
                         <span class='bul'></span>
                    <a href='/ContactUs.aspx'>
                        <asp:Literal ID="Literal4" runat="server" EnableViewState="False" meta:resourcekey="Literal4Resource1"
                            Text="Contact Us"></asp:Literal></a> </li>
                </ul>
            </font>
        </div>
        <table cellpadding="0" cellspacing="0" border="0" width="100%">
            <tr>
                <td align="left" valign="top">
                    <div>
                        <asp:Label ID="lblName" runat="server" CssClass="logoName" EnableViewState="False"
                            meta:resourcekey="lblNameResource1"></asp:Label>
                    </div>
                    <div>
                        <asp:Label ID="lblAddress" runat="server"  CssClass="logoAddress" EnableViewState="False"
                            meta:resourcekey="lblAddressResource1">wewer <br /> sddsd </asp:Label>
                          <br />
                            <asp:Label ID="Label1" runat="server"  CssClass="logoAddress" EnableViewState="False"
                            meta:resourcekey="lblAddressResource1">test</asp:Label>
                    </div>
                </td>
                <td align="right" valign="bottom" style="width: 245px; padding-right: 5px;">
                    <div style="padding-top: 10px; margin-top: 10px;">
                        <div style="float: left">
                            <!-- BEGIN : Language options-->
                            <uc2:LanguageOptions ID="LanguageOptions1" runat="server" />
                            <!-- END : Language options -->
                        </div>
                        <div style="float: right">
                            <%-- Begin : color Pallete --%>
                            <uc1:ColorPallete ID="ColorPallete1" runat="server"></uc1:ColorPallete>
                            <%-- End : color Pallete --%>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id='HeaderMenuHolderUni'>
        <asp:Label ID="lblTopLinks" runat="server" EnableViewState="False" meta:resourcekey="lblTopLinksResource1"></asp:Label>
    </div>
</div>

<script type="text/javascript">

    

    if (document.getElementById("<%=pnlLogin.ClientID%>") != null) {

        if (document.getElementById("<%=hlkMySettings.ClientID%>") != null) {
            document.getElementById("<%=hlkMySettings.ClientID%>").setAttribute("href", "<%=sSitePath%>MySettings.aspx");
        }
        document.getElementById("<%=hlkLogout.ClientID%>").setAttribute("href", "<%=sSitePath%>Logout.aspx");
    }
</script>

