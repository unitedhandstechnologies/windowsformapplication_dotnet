<%@ Page Language="C#" AutoEventWireup="true" Codebehind="Default.aspx.cs" Inherits="SRPD.Default"
    EnableViewState="False" %>
    
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head1">
    <title>
       
    </title>

    <script type="text/javascript" language="javascript" src="jscript/jscript_validations.js"></script>


    <script type="text/javascript" language="javascript">
        function validate_Me() {
            var i = -1;
            var myArr = new Array();
            myArr[++i] = new Array(document.getElementById("<% =txtUserName.ClientID%>"), "Empty", "Enter User Name.", "text");
            myArr[++i] = new Array(document.getElementById("<% =txtPassword.ClientID%>"), "Empty", "Enter Password.", "text");
            var ret = validateMe(myArr, 50);

            return ret;
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="background-color: #FFF2D9; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;">
                <tr>
                    <td align="right">
                        <font class="loginLabels"><span style="color: #666666">User</span></font></td>
                    <td align="right" style="color: #666666">
                        <asp:TextBox ID="txtUserName" runat="server" BorderColor="White" Height="15px" CssClass="clLogin"
                            MaxLength="50" Width="138px"></asp:TextBox></td>
                </tr>
                <tr style="color: #666666">
                    <td>
                        <font class="loginLabels"><span style="color: #666666">Password</span></font></td>
                    <td style="color: #666666">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="clLogin" MaxLength="15" TextMode="Password"></asp:TextBox></td>
                </tr>
                <tr style="color: #666666">
                    <td align="right" colspan="2" valign="baseline">
                        <asp:Label ID="lblError" runat="server" Font-Bold="True" Font-Size="6pt" ForeColor="Red"
                            Visible="False">Invalid User Name/Password</asp:Label><asp:Button ID="btnLogin" runat="server"
                                CssClass="butSubmit" Text="Go" OnClick="btnLogin_Click" /></td>
                </tr>
                <tr>
                    <td colspan="2" style="height: 21px">
                        <font class="loginLabels">Forgot Password</font>
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" >
<%--              <object type="application/x-shockwave-flash" style="outline:none;" data="http://hosting.gmodules.com/ig/gadgets/file/112581010116074801021/spider.swf?" width="700" height="600"><param name="movie" value="http://hosting.gmodules.com/ig/gadgets/file/112581010116074801021/spider.swf?"></param><param name="AllowScriptAccess" value="always"></param><param name="wmode" value="opaque"></param><param name="scale" value="noscale"/><param name="salign" value="tl"/></object>
--%>        </div>
    </form>
</body>
</html>
