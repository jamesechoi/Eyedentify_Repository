<%@ Page Title="About Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="About.aspx.cs" Inherits="Eyedentify.About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
    <script type="text/javascript" src="../SCR_jquery.min.js"></script>
    <script type="text/javascript" src="../SCR_chili-1.7.pack.js"></script>
    <script type="text/javascript" src="../SCR_jquery.metadata.v2.js"></script>
    <script type="text/javascript" src="../SCR_jquery.media.js"></script>
    
    <script type="text/javascript">
        $(function () {
            //$.fn.media.mapFormat('avi','quicktime');
            // this one liner handles all the examples on this page
            $('a.media').media();
        });
    </script>

    
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent" >
    <h2>
        About
    </h2>
    <p>
        Put content here..
    </p>

    
    
    <object id="Object1" type="application/x-shockwave-flash" data="<%= Page.ResolveUrl("~/flv_movy/player_flv_maxi.swf") %>" width="480" height="360"> 
    <noscript><a href="http://www.dvdvideosoft.com/products/dvd/Free-YouTube-Download.htm">youtube video downloader</a></noscript> 
    <param name="movie" value="<%= Page.ResolveUrl("~/flv_movy/player_flv_maxi.swf") %>" /> 
    <param name="allowFullScreen" value="true" /> 
    <param name="wmode" value="opaque" /> 
    <param name="allowScriptAccess" value="sameDomain" /> 
    <param name="quality" value="high" /> 
    <param name="menu" value="true" /> 
    <param name="autoplay" value="false" /> 
    <param name="autoload" value="false" /> 
    <param name="FlashVars" value="configxml=<%= Page.ResolveUrl("~/flv_movy/Wildlife.xml") %>" /> 
    </object>
    <p>fvl file</p>

    <a class="media {width:480, height:340}" href="Wildlife/Wildlife.mov">MOV File (video)</a> 
    <%--<a class="media {width:250, height:250}" href="Wildlife/Wildlife.flv">FLV File</a>--%>
    <%--<a class="media {width:250, height:250}" href="http://malsup.github.com/mediaplayer.swf?file=Wildlife/Wildlife.flv">SWF with FLV (mediaplayer.swf?file=curtain.flv)</a>--%>
    <%--<a class="media {width:250, height:200}" href="Wildlife/wildlife.swf">SWF File</a>--%> 
    <%--<a class="media {width:450, height:380, type:'swf'}" href="http://youtube.com/v/TyvN59L4hJU">Youtube Video (SWF)</a> --%>
    <a class="media {width:480, height:425}" href="Wildlife/Wildlife.wmv">WMV File</a> 
    <a class="media {width:480, height:340}" href="Wildlife/Wildlife.avi">AVI File</a> 
    <%--<a class="media {width:250, height:150}" href="http://malsup.github.com/video/pulsar.mpg">MPG File</a>--%> 
    <%--<a class="media {width:250, height:180}" href="http://malsup.github.com/video/tube.3g2">3G2 File (cell phone video)</a> --%>
    <%--<a class="media {width:400, height:250}" href="http://malsup.github.com/video/realvideo.ram">RAM File</a> --%>
    

</asp:Content>
