<%@ Control Language="C#" CodeBehind="NewLeftMenuBar.ascx.cs" AutoEventWireup="true"
    Inherits="DPTemplate2.NewLeftMenuBar" %>
<div id="leftmenuholder">
    <ul id="leftmenu">
        <li class="header">
            <asp:Label runat="server" ID="lblSelectedMenuName"></asp:Label>
        </li>
        <asp:Repeater ID="RepeaterLeftMenu" runat="server">
            <ItemTemplate>
                <li><span style="margin-right:2px;"><span class='bul'></span></span>
                    <a href="<%=Classes.clsGetSettings.SitePath%><%#Eval("Module_Key")%>/<%#Eval("MenuUrl")%>">
                    <%#Eval("Name")%></a> </li>
            </ItemTemplate>
        </asp:Repeater>
        <li class="footer">&nbsp; </li>
    </ul>
</div>
<input id="hid_MenuID" type="hidden" runat="server" />
<input id="hid_ModuleKey" type="hidden" runat="server" />
<input id="hid_MenuTypeIdentifier" type="hidden" runat="server" />
<input id="hid_CommandArgument" type="hidden" runat="server" />

