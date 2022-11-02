<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValidateOTP.aspx.cs" Inherits="SRPD.PreExamination.ValidateOTP" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>OTP</title>
    <script type="text/javascript">
        function closewin() {
            window.close();
        }
    </script>
    <script language="javascript" type="text/javascript" src="<%=Classes.clsGetSettings.SitePath%>jscript/jquery-latest.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <input id="hidInstID" style="width: 51px; height: 22px" type="hidden" size="3" name="Hidden2"
        runat="server" />
    <div align="right">
        <asp:Label ID="lblNote" runat="server"> </asp:Label>
    </div>
    <br />
    <table width="100%">
        <tr>
            <td width="30%" style="text-align: right">
                <asp:Label ID="lblOTP" runat="server">Enter OTP </asp:Label>
            </td>
            <td width="1%">
                :
            </td>
            <td width="55%">
                <asp:TextBox ID="OTP" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div>
    </div>
    <div>
    </div>
    <br />
    <div align="center">
        <asp:Button ID="btnValidate" runat="server" Text="Validate" OnClick="btnValidate_Click" />
        <asp:Button ID="btnclose" runat="server" Text="Close" OnClick="btnclose_Click" />
    </div>
    </form>
</body>
</html>