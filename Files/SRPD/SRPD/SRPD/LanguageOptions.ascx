<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LanguageOptions.ascx.cs" Inherits="DPTemplate2.LanguageOptions" %>
 <asp:PlaceHolder ID="LanguageOptionsHolder" runat="server">
    <div id="palleteHolder" style="z-index:100;width:100%" align="right" >
        <ul class="languageBox" >
            <li class='langSetting'> 
                <span class='langSettingBox'>&nbsp;
                    <asp:Literal ID="Literal1" runat="server" 
                         EnableViewState="false" meta:resourcekey="Literal1Resource1">Change Languages</asp:Literal>
                </span>
                <span class='langSettingIcon'></span>
                <ul class='langList'>                                                 
                   <li>							  
						<asp:PlaceHolder ID="LanguagePlaceHolder" runat="server"></asp:PlaceHolder>								
                   </li>
                </ul>
            </li>
        </ul>
    </div>            
</asp:PlaceHolder>	

<script type="text/javascript">
    $(document).ready
    (
        function(){
            $("ul.languageBox").click
            (
                function() {
                    $(this).find("span.langSettingIcon").toggleClass("langSettingIconHover");
                    $(this).find("ul.langList").toggle("slow");

                }
            )
            $("ul.languageBox").bind("mouseleave",
              function() {
                    $(this).find("span.langSettingIconHover").attr("class", "langSettingIcon");
                    $(this).find("ul.langList").hide("slow");

                }
            )
        }
    )
    function SetVisitorChoice(sVal) {
      createCookie("Language", sVal,365);    
    }
</script>