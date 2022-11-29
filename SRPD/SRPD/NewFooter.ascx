<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NewFooter.ascx.cs" Inherits="DPTemplate2.NewFooter" %>

<div id="footer">
    <div align="center" id='footerLink'>
        <ul>
         <asp:Label ID="pnlFooter" runat="server" align="right" >
            <li><a id="lnkPhotoGallary">
                <asp:Literal ID="Literal1" runat="server" EnableViewState="False" meta:resourcekey="Literal1Resource1"
                    Text="Photo Gallery"></asp:Literal></a></li>
            <li><span class='bul'></span><a id="lnkVTour">
                <asp:Literal ID="Literal2" runat="server" EnableViewState="False" meta:resourcekey="Literal2Resource1"
                    Text="Visual Tour"></asp:Literal></a>
                    </li></asp:Label>
            <li><span class='bul'></span><a id="lnkSuggestions">
                <asp:Literal ID="Literal3" runat="server" EnableViewState="False" meta:resourcekey="Literal3Resource1"
                    Text="Suggestion"></asp:Literal></a></li>
            <li><span class='bul'></span><a id="lnkRequestInfo">
                <asp:Literal ID="Literal4" runat="server" EnableViewState="False" meta:resourcekey="Literal4Resource1"
                    Text="Request Info"></asp:Literal></a></li>
            <li><span class='bul'></span><a id="lnkRegisterComplaint">
                <asp:Literal ID="Literal5" runat="server" EnableViewState="False" meta:resourcekey="Literal5Resource1"
                    Text="Complaints"></asp:Literal></a></li>
            <li><span class='bul'></span><a id="lnkDisplayFAQ">
                <asp:Literal ID="Literal6" runat="server" EnableViewState="False" meta:resourcekey="Literal6Resource1"
                    Text="FAQ"></asp:Literal></a></li>
            <li><span class='bul'></span><a id="lnkDisclaimer">
                <asp:Literal ID="Literal7" runat="server" EnableViewState="False" meta:resourcekey="Literal7Resource1"
                    Text="Disclaimer"></asp:Literal></a></li>
        </ul>
    </div>
    <div id="version" align="center">
        <asp:Literal ID="Literal8" runat="server" EnableViewState="False" Text="Copyright 2011. All Rights Reserved. Powered By"
            meta:resourcekey="Literal8Resource2"></asp:Literal>
        <a href="http://www.mkcl.org">
            <asp:Literal ID="Literal9" runat="server" EnableViewState="False" Text="(MKCL)" meta:resourcekey="Literal9Resource2"></asp:Literal></a>
       <div style="width: 100%; height: 16px" align="center">
            <asp:Literal ID="Literal11" runat="server" EnableViewState="False" Text="The website can be best viewed in 1024 * 768 resolution and required version of internet explorer is IE 7.0,Firefox 3.0 and above"
                meta:resourcekey="Literal11Resource1"></asp:Literal>
                <br /><%=Request.ServerVariables["LOCAL_ADDR"].Substring((Request.ServerVariables["LOCAL_ADDR"].LastIndexOf('.')+1), (Request.ServerVariables["LOCAL_ADDR"].Length - (Request.ServerVariables["LOCAL_ADDR"].LastIndexOf('.')+1)))%>
        </div>
        <asp:Label ID="lblVersion" runat="server" Visible="false"></asp:Label></div>
</div>
<script>
    document.getElementById("lnkPhotoGallary").setAttribute("href", "<%=sSitePath%>PhotoGallary.aspx");
    document.getElementById("lnkVTour").setAttribute("href", "<%=sSitePath%>VisualTour.aspx");
    document.getElementById("lnkSuggestions").setAttribute("href", "<%=sSitePath%>Suggestions.aspx");
    document.getElementById("lnkRequestInfo").setAttribute("href", "<%=sSitePath%>RequestInfo.aspx");
    document.getElementById("lnkRegisterComplaint").setAttribute("href", "<%=sSitePath%>RegisterComplaint.aspx");
    document.getElementById("lnkDisplayFAQ").setAttribute("href", "<%=sSitePath%>DisplayFAQ.aspx");
    document.getElementById("lnkDisclaimer").setAttribute("href", "<%=sSitePath%>Disclaimer.aspx");
</script>
