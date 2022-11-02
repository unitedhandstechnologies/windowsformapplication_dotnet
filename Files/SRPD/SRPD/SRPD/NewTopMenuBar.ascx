<%@ Control Language="C#" Codebehind="NewTopMenuBar.ascx.cs" AutoEventWireup="true" Inherits="DPTemplate2.NewTopMenuBar" %>

<div id="topMenuParent">
<ul>
<asp:Repeater ID="RepeaterTopMenu" runat="server">        
    <ItemTemplate>
        <li><a href="<%=Classes.clsGetSettings.SitePath%><%#Eval("Module_Key")%>/<%#Eval("MenuUrl")%>">
            <%#Eval("Name")%></a> </li>            
    </ItemTemplate>   
</asp:Repeater>
</ul>
</div>
<input id="hid_MenuID" type="hidden" runat="server" />
<input id="hid_ModuleKey" type="hidden" runat="server" />
<input id="hid_MenuTypeIdentifier" type="hidden" runat="server" />


