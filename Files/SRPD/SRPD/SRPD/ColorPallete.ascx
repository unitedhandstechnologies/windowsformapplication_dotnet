<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ColorPallete.ascx.cs"
    Inherits="DPTemplate2.ColorPallete" %>
<div id="palleteHolder" style="width: 100%">
    <ul class="pallete">
        <li><span class='themeSettingBox'>&nbsp;
            <asp:Literal ID="Literal1" runat="server" EnableViewState="False" meta:resourcekey="Literal1Resource2"
                Text="Change Theme"></asp:Literal>
        </span><span class='themeSetting'></span>
            <ul>
                <li title="Orange"><span class="clOrange" onclick="return setCookie('color5.css');">
                    &nbsp;</span></li>
                <li title="Maroon"><span class="clMaroon" onclick="return setCookie('color4.css');">
                    &nbsp;</span></li>
                <li title="Green"><span class="clGreen" onclick="return setCookie('color3.css');">&nbsp;</span></li>
                <li title="Blue"><span class="clBlue" onclick="return setCookie('color2.css');">&nbsp;</span></li>
                <li title="Gray"><span class="clGray" onclick="return setCookie('color1.css');">&nbsp;</span></li>
            </ul>
        </li>
    </ul>
</div>
<script type="text/javascript">
    $(document).ready
    (
        function () {
            $("ul.pallete").click
            (
                function () {
                    $(this).find("span.themeSetting").toggleClass("themeSettingHover");
                    $(this).find("ul").toggle("slow");
                }
            )
            $("ul.pallete").bind("mouseleave",
              function () {
                  $(this).find("span.themeSettingHover").attr("class", "themeSetting");
                  $(this).find("ul").hide("slow");
              }
            )
        }
    )
</script>
