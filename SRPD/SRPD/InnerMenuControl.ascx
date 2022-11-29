<%@ Control Language="c#" AutoEventWireup="True" Codebehind="InnerMenuControl.ascx.cs"
    Inherits="DPTemplate2.InnerMenuControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="MenuControl" Src="MenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="NewFooter.ascx" %>
<link href="/CSS/Portal.css" type="text/css" rel="stylesheet">

<script language="javascript" src="/jscript/ypSlideOutMenusC.js"></script>

<table id="Table1" width="100%" border="0">
    <tr>
        <td style="width: 100%">
            <div>
                <uc1:MenuControl ID="UCSubLink" runat="server"></uc1:MenuControl>
            </div>
            <div style ="margin:0px">
                <uc1:MenuControl ID="mnuUniversity" runat="server"></uc1:MenuControl>
            </div>
            <div style ="margin:5px 0px 0px 0px">
                <uc1:MenuControl ID="mnuActivities" runat="server"></uc1:MenuControl>
            </div>
            <div  style ="margin:5px 0px 0px 0px">
                <uc1:MenuControl ID="mnuMedia" runat="server"></uc1:MenuControl>
            </div>
            <div  style ="margin:5px 0px 0px 0px">
                <uc1:MenuControl ID="mnuIPRPublication" runat="server"></uc1:MenuControl>
            </div>
            <div  style ="margin:5px 0px 0px 0px">
                <uc1:MenuControl ID="mnuAcademics" runat="server"></uc1:MenuControl>
            </div>
        </td>
    </tr>
</table>
