<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="Footer.ascx.cs" Inherits="DPTemplate2.Footer"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>



<%--<div style="MARGIN-TOP:30px;WIDTH:100%">
	<div align="center" style="WIDTH:100%;HEIGHT:16px">
		<!-- <asp:label id="toplinks" runat="server"></asp:label> -->
		<a class='toplinks' href="<%=Classes.clsGetSettings.SitePath%>PhotoGallary.aspx">
			Photo Gallery</a> | <a class='toplinks' href="<%=Classes.clsGetSettings.SitePath%>VisualTour.aspx">
			Visual Tour</a> | <a class='toplinks' href="<%=Classes.clsGetSettings.SitePath%>Suggestions.aspx" >
			Suggestion </a>| <a class='toplinks' href="<%=Classes.clsGetSettings.SitePath%>RequestInfo.aspx">
			Request Info</a> | <a href="<%=Classes.clsGetSettings.SitePath%>RegisterComplaint.aspx" class='toplinks'>
			Complaints</a> | <a href="<%=Classes.clsGetSettings.SitePath%>DisplayFAQ.aspx" class='toplinks'>
			FAQ</a> | <a class='toplinks' href="<%=Classes.clsGetSettings.SitePath%>Disclaimer.aspx">
			Disclaimer</a>
	</div>--%>
<div id="footer" align="center">
    <div id="footerLink" align="center">
        <ul>
            <li><a href='<%=Classes.clsGetSettings.SitePath%>PhotoGallary.aspx'>
                <asp:Literal ID="Literal1" runat="server" EnableViewState="False" meta:resourcekey="Literal1Resource1"
                    Text="Photo Gallery"></asp:Literal></a></li>
            <li><span class='bul'></span><a href='<%=Classes.clsGetSettings.SitePath%>VisualTour.aspx'>
                <asp:Literal ID="Literal2" runat="server" EnableViewState="False" meta:resourcekey="Literal2Resource1"
                    Text="Visual Tour"></asp:Literal></a></li>
            <li><span class='bul'></span><a href='<%=Classes.clsGetSettings.SitePath%>Suggestions.aspx'>
                <asp:Literal ID="Literal3" runat="server" EnableViewState="False" meta:resourcekey="Literal3Resource1"
                    Text="Suggestion"></asp:Literal></a></li>
            <li><span class='bul'></span><a href='<%=Classes.clsGetSettings.SitePath%>RequestInfo.aspx'>
                <asp:Literal ID="Literal4" runat="server" EnableViewState="False" meta:resourcekey="Literal4Resource1"
                    Text="Request Info"></asp:Literal></a></li>
            <li><span class='bul'></span><a href='<%=Classes.clsGetSettings.SitePath%>RegisterComplaint.aspx'>
                <asp:Literal ID="Literal5" runat="server" EnableViewState="False" meta:resourcekey="Literal5Resource1"
                    Text="Complaints"></asp:Literal></a></li>
            <li><span class='bul'></span><a href='<%=Classes.clsGetSettings.SitePath%>DisplayFAQ.aspx'>
                <asp:Literal ID="Literal6" runat="server" EnableViewState="False" meta:resourcekey="Literal6Resource1"
                    Text="FAQ"></asp:Literal></a></li>
            <li><span class='bul'></span><a href='<%=Classes.clsGetSettings.SitePath%>Disclaimer.aspx'>
                <asp:Literal ID="Literal7" runat="server" EnableViewState="False" meta:resourcekey="Literal7Resource1"
                    Text="Disclaimer"></asp:Literal></a></li>
        </ul>
    </div>
    <div  id="version"  align="center">
     <asp:Literal ID="Literal8" runat="server" EnableViewState="False"
        Text="Copyright 2011. All Rights Reserved. Powered By" 
            meta:resourcekey="Literal8Resource2"></asp:Literal> <a href="http://www.mkcl.org">
    <asp:Literal ID="Literal9" runat="server" EnableViewState="False" 
        Text="(MKCL)" meta:resourcekey="Literal9Resource2"></asp:Literal></a>
    <div style="width: 100%; height: 16px" align="center">
        <asp:Literal ID="Literal11" runat="server" EnableViewState="False" Text="The 
	website can be best viewed in 1024 * 768 resolution and required version of 
	internet explorer is IE 8.0 and above" meta:resourcekey="Literal11Resource1"></asp:Literal> <br /><%=Request.ServerVariables["LOCAL_ADDR"].Substring((Request.ServerVariables["LOCAL_ADDR"].LastIndexOf('.')+1), (Request.ServerVariables["LOCAL_ADDR"].Length - (Request.ServerVariables["LOCAL_ADDR"].LastIndexOf('.')+1)))%></div>
	<asp:Label ID="lblVersion" runat="server" Visible ="false"></asp:Label></div>
</div>

