<%@ Page Title="FAQ" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="FAQ.aspx.cs" Inherits="Eyedentify.FAQ" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    
    <link rel="stylesheet" href="<%= Page.ResolveUrl("~/Styles/STL_faq.css") %>" />
    <script src="<%= Page.ResolveUrl("~/Scripts/SCR_jquery.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/SCR_jquery.scrollTo-1.4.2-min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/SCR_jquery.localscroll-1.2.7-min.js") %>"></script>
   
    <script>
        $(document).ready(function () {
                var sidebar = $('#sidebar_content');
                var top = sidebar.offset().top - parseFloat(sidebar.css('marginTop'));

                $(window).scroll(function (event) {
                    var ypos = $(this).scrollTop();
                    if (ypos >= top) {
                        sidebar.addClass('fixed');
                    }
                    else {
                        sidebar.removeClass('fixed');
                    }
                });

                $.localScroll();

            });
			
        </script>


</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">



    <body>
        <div id="wrapper">
            <h1 id="title">
                Frequently asked questions</h1>
            <hr>
            <div id="primary">
                <h3 id="1">
                    Lectus facilisis vel</h3>
                <ul class="section_menu">
                    <li><a href="#1_1">Lectus massa adipiscing, mattis. Turpis integer massa.</a></li>
                    <li><a href="#1_2">Integer enim montes mauris, arcu est.</a></li>
                    <li><a href="#1_3">Duis, cum lectus, et scelerisque vel.</a></li>
                    <li><a href="#1_4">Eu, quis. Vel aliquet odio rhoncus.</a></li>
                    <li><a href="#1_5">Cras! A lacus nunc. Enim et.</a></li>
                    <li><a href="#1_6">Natoque ac, augue et! Dapibus ut.</a></li>
                    <li><a href="#1_7">Ut, parturient mauris odio est adipiscing.</a></li>
                </ul>
                <dl class="faq">
                    <dt id="1_1">Lectus massa adipiscing, mattis. Turpis integer massa.</dt>
                    <dd>
                        Ultricies in mus, magna rhoncus augue, nec magnis facilisis integer ut pellentesque
                        aliquam sit! Enim odio, porta augue, sed turpis dolor ultrices porttitor arcu massa
                        cum elementum hac in vel, magna magnis, enim scelerisque? Amet aliquam, magna dis
                        porta platea. Cras aliquet. Arcu mid eros aenean parturient cras ac egestas tempor?
                        Lundium parturient dapibus, ridiculus ridiculus dapibus! Quis eros amet.</dd>
                    <dt id="1_2">Integer enim montes mauris, arcu est.</dt>
                    <dd>
                        Et ridiculus vut dis vel integer pid? Adipiscing nec tristique dictumst tristique
                        duis rhoncus sed, scelerisque. Porta, diam augue vel augue porta enim. Et! Tristique
                        montes. Auctor! Pid tristique purus montes. Quis? Sit, enim. Egestas! Tristique
                        amet mattis adipiscing, proin elit adipiscing integer! Enim, odio. Etiam ac, nunc
                        est purus turpis. Nunc! Pid cras scelerisque mid habitasse. Cum magnis.</dd>
                    <dt id="1_3">Duis, cum lectus, et scelerisque vel.</dt>
                    <dd>
                        Purus urna tortor et nec, tristique. Etiam! Placerat elit habitasse scelerisque
                        non lorem pellentesque, ut elementum vel odio natoque, augue mus tincidunt, enim,
                        tristique ac, nisi, ac enim parturient? Mattis aenean, scelerisque diam pulvinar
                        magna porttitor rhoncus, pid, vut pid enim, adipiscing, odio amet eu a montes turpis
                        a, odio egestas, rhoncus nascetur tortor nascetur, nec enim amet turpis.</dd>
                    <dt id="1_4">Eu, quis. Vel aliquet odio rhoncus.</dt>
                    <dd>
                        Velit. Sit rhoncus sit cras vel, platea nisi enim, turpis habitasse, cras aenean
                        ultricies tristique. In egestas dapibus massa tincidunt sagittis dolor. Scelerisque?
                        Elementum, ut enim dis? Egestas odio! Sit purus! Tortor mattis, ac ac. Nec et mid
                        lundium, facilisis turpis! Duis ridiculus in, in nisi sit urna, integer cursus!
                        Ridiculus in sit habitasse purus sociis placerat eu risus.</dd>
                    <dt id="1_5">Cras! A lacus nunc. Enim et.</dt>
                    <dd>
                        Ac. Ut dignissim dictumst velit natoque tristique sit placerat lundium rhoncus.
                        Pellentesque mattis augue! Non in est nec elit in pulvinar lectus vel? Mus ac. Pulvinar
                        lacus aliquam, sit porta porta porttitor odio lacus turpis! Tincidunt et aenean
                        in hac vel phasellus pid mid! Platea, pellentesque lundium? Pid odio ut? Lorem vel
                        turpis sed hac vut urna augue parturient.</dd>
                    <dt id="1_6">Natoque ac, augue et! Dapibus ut.</dt>
                    <dd>
                        Porttitor pid porttitor, cursus nec urna vel tortor aliquet! Diam nunc auctor turpis
                        pulvinar lorem? Sit urna rhoncus rhoncus proin, vut ac auctor? Dapibus elementum
                        odio arcu velit augue! Parturient lacus mus sed odio platea! Ac turpis in ultricies!
                        Arcu. Mus cras porta, non cum tortor ultrices odio rhoncus! Dictumst dapibus sed
                        lacus sed ac facilisis arcu tincidunt scelerisque.</dd>
                    <dt id="1_7">Ut, parturient mauris odio est adipiscing.</dt>
                    <dd>
                        Dignissim in vel. Duis lacus porta augue vel? Montes in a, scelerisque nisi a magnis
                        ac amet, placerat facilisis! Elit placerat placerat habitasse! In? Scelerisque dictumst
                        turpis elementum enim integer mus? Mauris ut nisi massa arcu parturient turpis nec,
                        sit, lacus auctor lectus pellentesque porttitor, rhoncus placerat. Duis montes sed,
                        augue a! Dolor sed auctor, magna vut! Integer cum.</dd>
                </dl>
                <h3 id="2">
                    Vut magna</h3>
                <ul class="section_menu">
                    <li><a href="#2_1">Lectus massa adipiscing, mattis. Turpis integer massa.</a></li>
                    <li><a href="#2_2">Integer enim montes mauris, arcu est.</a></li>
                    <li><a href="#2_3">Duis, cum lectus, et scelerisque vel.</a></li>
                    <li><a href="#2_4">Eu, quis. Vel aliquet odio rhoncus.</a></li>
                    <li><a href="#2_5">Cras! A lacus nunc. Enim et.</a></li>
                    <li><a href="#2_6">Natoque ac, augue et! Dapibus ut.</a></li>
                    <li><a href="#2_7">Ut, parturient mauris odio est adipiscing.</a></li>
                </ul>
                <dl class="faq">
                    <dt id="2_1">Lectus massa adipiscing, mattis. Turpis integer massa.</dt>
                    <dd>
                        Ultricies in mus, magna rhoncus augue, nec magnis facilisis integer ut pellentesque
                        aliquam sit! Enim odio, porta augue, sed turpis dolor ultrices porttitor arcu massa
                        cum elementum hac in vel, magna magnis, enim scelerisque? Amet aliquam, magna dis
                        porta platea. Cras aliquet. Arcu mid eros aenean parturient cras ac egestas tempor?
                        Lundium parturient dapibus, ridiculus ridiculus dapibus! Quis eros amet.</dd>
                    <dt id="2_2">Integer enim montes mauris, arcu est.</dt>
                    <dd>
                        Et ridiculus vut dis vel integer pid? Adipiscing nec tristique dictumst tristique
                        duis rhoncus sed, scelerisque. Porta, diam augue vel augue porta enim. Et! Tristique
                        montes. Auctor! Pid tristique purus montes. Quis? Sit, enim. Egestas! Tristique
                        amet mattis adipiscing, proin elit adipiscing integer! Enim, odio. Etiam ac, nunc
                        est purus turpis. Nunc! Pid cras scelerisque mid habitasse. Cum magnis.</dd>
                    <dt id="2_3">Duis, cum lectus, et scelerisque vel.</dt>
                    <dd>
                        Purus urna tortor et nec, tristique. Etiam! Placerat elit habitasse scelerisque
                        non lorem pellentesque, ut elementum vel odio natoque, augue mus tincidunt, enim,
                        tristique ac, nisi, ac enim parturient? Mattis aenean, scelerisque diam pulvinar
                        magna porttitor rhoncus, pid, vut pid enim, adipiscing, odio amet eu a montes turpis
                        a, odio egestas, rhoncus nascetur tortor nascetur, nec enim amet turpis.</dd>
                    <dt id="2_4">Eu, quis. Vel aliquet odio rhoncus.</dt>
                    <dd>
                        Velit. Sit rhoncus sit cras vel, platea nisi enim, turpis habitasse, cras aenean
                        ultricies tristique. In egestas dapibus massa tincidunt sagittis dolor. Scelerisque?
                        Elementum, ut enim dis? Egestas odio! Sit purus! Tortor mattis, ac ac. Nec et mid
                        lundium, facilisis turpis! Duis ridiculus in, in nisi sit urna, integer cursus!
                        Ridiculus in sit habitasse purus sociis placerat eu risus.</dd>
                    <dt id="2_5">Cras! A lacus nunc. Enim et.</dt>
                    <dd>
                        Ac. Ut dignissim dictumst velit natoque tristique sit placerat lundium rhoncus.
                        Pellentesque mattis augue! Non in est nec elit in pulvinar lectus vel? Mus ac. Pulvinar
                        lacus aliquam, sit porta porta porttitor odio lacus turpis! Tincidunt et aenean
                        in hac vel phasellus pid mid! Platea, pellentesque lundium? Pid odio ut? Lorem vel
                        turpis sed hac vut urna augue parturient.</dd>
                    <dt id="2_6">Natoque ac, augue et! Dapibus ut.</dt>
                    <dd>
                        Porttitor pid porttitor, cursus nec urna vel tortor aliquet! Diam nunc auctor turpis
                        pulvinar lorem? Sit urna rhoncus rhoncus proin, vut ac auctor? Dapibus elementum
                        odio arcu velit augue! Parturient lacus mus sed odio platea! Ac turpis in ultricies!
                        Arcu. Mus cras porta, non cum tortor ultrices odio rhoncus! Dictumst dapibus sed
                        lacus sed ac facilisis arcu tincidunt scelerisque.</dd>
                    <dt id="2_7">Ut, parturient mauris odio est adipiscing.</dt>
                    <dd>
                        Dignissim in vel. Duis lacus porta augue vel? Montes in a, scelerisque nisi a magnis
                        ac amet, placerat facilisis! Elit placerat placerat habitasse! In? Scelerisque dictumst
                        turpis elementum enim integer mus? Mauris ut nisi massa arcu parturient turpis nec,
                        sit, lacus auctor lectus pellentesque porttitor, rhoncus placerat. Duis montes sed,
                        augue a! Dolor sed auctor, magna vut! Integer cum.</dd>
                </dl>
                <h3 id="3">
                    Lacus pulvinar</h3>
                <ul class="section_menu">
                    <li><a href="#3_1">Lectus massa adipiscing, mattis. Turpis integer massa.</a></li>
                    <li><a href="#3_2">Integer enim montes mauris, arcu est.</a></li>
                    <li><a href="#3_3">Duis, cum lectus, et scelerisque vel.</a></li>
                    <li><a href="#3_4">Eu, quis. Vel aliquet odio rhoncus.</a></li>
                    <li><a href="#3_5">Cras! A lacus nunc. Enim et.</a></li>
                    <li><a href="#3_6">Natoque ac, augue et! Dapibus ut.</a></li>
                    <li><a href="#3_7">Ut, parturient mauris odio est adipiscing.</a></li>
                </ul>
                <dl class="faq">
                    <dt id="3_1">Lectus massa adipiscing, mattis. Turpis integer massa.</dt>
                    <dd>
                        Ultricies in mus, magna rhoncus augue, nec magnis facilisis integer ut pellentesque
                        aliquam sit! Enim odio, porta augue, sed turpis dolor ultrices porttitor arcu massa
                        cum elementum hac in vel, magna magnis, enim scelerisque? Amet aliquam, magna dis
                        porta platea. Cras aliquet. Arcu mid eros aenean parturient cras ac egestas tempor?
                        Lundium parturient dapibus, ridiculus ridiculus dapibus! Quis eros amet.</dd>
                    <dt id="3_2">Integer enim montes mauris, arcu est.</dt>
                    <dd>
                        Et ridiculus vut dis vel integer pid? Adipiscing nec tristique dictumst tristique
                        duis rhoncus sed, scelerisque. Porta, diam augue vel augue porta enim. Et! Tristique
                        montes. Auctor! Pid tristique purus montes. Quis? Sit, enim. Egestas! Tristique
                        amet mattis adipiscing, proin elit adipiscing integer! Enim, odio. Etiam ac, nunc
                        est purus turpis. Nunc! Pid cras scelerisque mid habitasse. Cum magnis.</dd>
                    <dt id="3_3">Duis, cum lectus, et scelerisque vel.</dt>
                    <dd>
                        Purus urna tortor et nec, tristique. Etiam! Placerat elit habitasse scelerisque
                        non lorem pellentesque, ut elementum vel odio natoque, augue mus tincidunt, enim,
                        tristique ac, nisi, ac enim parturient? Mattis aenean, scelerisque diam pulvinar
                        magna porttitor rhoncus, pid, vut pid enim, adipiscing, odio amet eu a montes turpis
                        a, odio egestas, rhoncus nascetur tortor nascetur, nec enim amet turpis.</dd>
                    <dt id="3_4">Eu, quis. Vel aliquet odio rhoncus.</dt>
                    <dd>
                        Velit. Sit rhoncus sit cras vel, platea nisi enim, turpis habitasse, cras aenean
                        ultricies tristique. In egestas dapibus massa tincidunt sagittis dolor. Scelerisque?
                        Elementum, ut enim dis? Egestas odio! Sit purus! Tortor mattis, ac ac. Nec et mid
                        lundium, facilisis turpis! Duis ridiculus in, in nisi sit urna, integer cursus!
                        Ridiculus in sit habitasse purus sociis placerat eu risus.</dd>
                    <dt id="3_5">Cras! A lacus nunc. Enim et.</dt>
                    <dd>
                        Ac. Ut dignissim dictumst velit natoque tristique sit placerat lundium rhoncus.
                        Pellentesque mattis augue! Non in est nec elit in pulvinar lectus vel? Mus ac. Pulvinar
                        lacus aliquam, sit porta porta porttitor odio lacus turpis! Tincidunt et aenean
                        in hac vel phasellus pid mid! Platea, pellentesque lundium? Pid odio ut? Lorem vel
                        turpis sed hac vut urna augue parturient.</dd>
                    <dt id="3_6">Natoque ac, augue et! Dapibus ut.</dt>
                    <dd>
                        Porttitor pid porttitor, cursus nec urna vel tortor aliquet! Diam nunc auctor turpis
                        pulvinar lorem? Sit urna rhoncus rhoncus proin, vut ac auctor? Dapibus elementum
                        odio arcu velit augue! Parturient lacus mus sed odio platea! Ac turpis in ultricies!
                        Arcu. Mus cras porta, non cum tortor ultrices odio rhoncus! Dictumst dapibus sed
                        lacus sed ac facilisis arcu tincidunt scelerisque.</dd>
                    <dt id="3_7">Ut, parturient mauris odio est adipiscing.</dt>
                    <dd>
                        Dignissim in vel. Duis lacus porta augue vel? Montes in a, scelerisque nisi a magnis
                        ac amet, placerat facilisis! Elit placerat placerat habitasse! In? Scelerisque dictumst
                        turpis elementum enim integer mus? Mauris ut nisi massa arcu parturient turpis nec,
                        sit, lacus auctor lectus pellentesque porttitor, rhoncus placerat. Duis montes sed,
                        augue a! Dolor sed auctor, magna vut! Integer cum.</dd>
                </dl>
            </div>
            <div id="sidebar">
                <div id="sidebar_content">
                    <h3>
                        Select Category</h3>
                    <ul class="section_menu">
                        <li><a href="#1">Lectus facilisis vel</a></li>
                        <li><a href="#2">Vut magna</a></li>
                        <li><a href="#3">Lacus pulvinar</a></li>
                    </ul>
                </div>
            </div>
        </div>
    </body>

</asp:Content>



