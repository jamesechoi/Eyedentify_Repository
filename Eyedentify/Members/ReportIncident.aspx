<%@ Page Title="Report Incident" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="ReportIncident.aspx.cs" Inherits="Eyedentify.ReportIncident" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <link rel="stylesheet" media="all" type="text/css" href="<%= Page.ResolveUrl("~/Styles/STL_ReportIncidentGeneral.css") %>" />    
    <link rel="stylesheet" media="all" type="text/css" href="<%= Page.ResolveUrl("~/Styles/STL_jquery-ui.css") %>" />
	<link rel="stylesheet" media="all" type="text/css" href="<%= Page.ResolveUrl("~/Styles/STL_jquery-ui-timepicker-addon.css") %>" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/SCR_jquery-1.7.2.min.js") %>"></script>
	<script type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/SCR_jquery-ui.min.js") %>"></script>
	<script type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/SCR_jquery-ui-timepicker-addon.js") %>"></script>
	<script type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/SCR_jquery-ui-sliderAccess.js") %>"></script>


    <script type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/form.js") %>"></script>
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/jquery.poshytip.js") %>"></script>
    <link rel="stylesheet" media="all" type="text/css" href="<%= Page.ResolveUrl("~/Styles/tip-twitter.css") %>" />


     <script type="text/javascript">
         $(function () {
             $("#<%= IncidentDateTime.ClientID %>").datetimepicker({
                 ampm: true,
                 addSliderAccess: true,
                 sliderAccessArgs: { touchonly: false }
             });

         });
    </script>

	<link href="<%= Page.ResolveUrl("~/Styles/STL_jquery.selectbox.css") %>" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="<%= Page.ResolveUrl("~/Scripts/SCR_jquery.selectbox-0.1.3.js") %>"></script>
    <script type="text/javascript">
        $(function () {
            $('.yourbox').selectbox();
        });
	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" ScriptMode="Debug" CombineScripts="false" />
     <ajaxToolkit:RoundedCornersExtender ID="rce" runat="server" TargetControlID="IncidentID" Radius="20" Corners="All" />
    <table id="pageborder" style="width: 900px; vertical-align: top; height: 462px; margin-left: auto; margin-right: auto; " cellpadding="5" cellspacing="20">
        <tr>
            <td class="style1">
                Location of Incident:
            </td>
            <td>
                <asp:Label ID="LocationLabel" runat="server" Text="Bling, Newmarket, Auckland"></asp:Label>
                &nbsp;<asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="">[change location]</asp:HyperLink>
            </td>
        </tr>
       
        <tr>
            <td class="style1">
                Date and Time of Incident:
            </td>
            <td>
                <asp:TextBox runat="server" name="IncidentDateTime" id="IncidentDateTime" placeholder="click to insert"/>

            </td>
        </tr>

        <tr>
            <td class="style1">
                Relevant Photos:
            </td>
            <td>
                <asp:LinkButton ID="AddImageLinkButton" runat="server" Text="Click here to add photos." />
                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"/>

                <div id="upimages">
                    <asp:GridView runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" DataKeyNames="Incident_Image_ID" GridLines="None" PageSize="5"
                    CssClass="mGrid" ForeColor="#333333" ID="IncidentImageGrid" OnRowDeleting="IncidentImageGrid_RowDeleting"
                    OnRowCommand="IncidentImageGrid_RowCommand" style="margin-left:40px;">
                    <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="Photos">
                            <ItemTemplate>
                                <asp:Image ID="Image1" Height="100" Width="100" runat="server" ImageUrl='<%# "ImagePage.aspx?imgID=" + Eval("Incident_Image_ID")%>' />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        <%--<asp:BoundField DataField="Image_Description">
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:BoundField>--%>
                        <asp:TemplateField HeaderText="Main Photo">
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Eval("Main_Photo")%>' Enabled="false" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="Linkbutton1" Text="Main Photo" CommandName="MainPhoto" CommandArgument='<%# Eval("Incident_Image_ID")%>'
                                    runat="server" />
                                <asp:LinkButton ID="lnk1" runat="server" Text="Delete" OnClientClick="return confirm('Are you sure to Delete?')"
                                    CommandName="Delete"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
                        </asp:TemplateField>
                        <asp:CheckBoxField></asp:CheckBoxField>
                    </Columns>
                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                    <EmptyDataTemplate>
                        <img src="Images/emptyGrid.JPG" alt="" />
                    </EmptyDataTemplate>
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                    <PagerStyle HorizontalAlign="Center" BackColor="#284775" CssClass="pgr" ForeColor="White">
                    </PagerStyle>
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                    <SortedAscendingCellStyle BackColor="#E9E7E2"></SortedAscendingCellStyle>
                    <SortedAscendingHeaderStyle BackColor="#506C8C"></SortedAscendingHeaderStyle>
                    <SortedDescendingCellStyle BackColor="#FFFDF8"></SortedDescendingCellStyle>
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE"></SortedDescendingHeaderStyle>
                    </asp:GridView>

                </div>

                <asp:Panel ID="Panel1" runat="server" Style="display: none; background: white" CssClass="modalPopup">
                    <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #DDDDDD;
                        border: solid 1px Gray; color: black;">
                        <div>
                            <p style="margin-left:20px;">
                                Add Photos:</p>
                        </div>
                    </asp:Panel>
                    <div>
                        <p>
                            <table style="width: 400px; margin-left:20px;">
                                <tr>
                                    <td style="width: 150px">
                                        Step 1: Choose a file
                                    </td>
                                    <td>
                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                        <asp:Label ID="myThrobber" runat="server"> </asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 150px">
                                        Step 3: Main Photo?
                                    </td>
                                    <td>
                                        <asp:CheckBox runat="server" ID="MainPhotoCheckBox"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 150px">
                                        Step 4: Upload Photo
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="Button1" runat="server" ImageAlign="TextTop" ImageUrl="~/Images/uploadImg.JPG"
                                            OnClick="btnSave_Click" Width="140px" />
                                        
                                        <br />
                                        <%--<asp:Label ID="lblMsg" runat="server" ForeColor="Red" />--%>
                                        <br />
                                        <asp:Label ID="Label6" runat="server" ForeColor="Red" />
                                    </td>
                                </tr>
                            </table>
                            
                        </p>
                        <p style="text-align: center;">
                            <asp:Button ID="OkButton" runat="server" Text="OK" style="visibility: hidden;" />
                            <asp:Button ID="FinishButton" runat="server" Text="close window" style="margin-bottom: 20px"/> 
                            <a href="#" class="help" ><img style="margin-bottom:-4px; margin-left:5px;" border="0" src=" http://b3.caspio.com/RMA_ref/help.png "><span>You must upload the photo before you exit the window. Uploading the photo automatically closes the window</span></a>
                            <asp:Button ID="CancelButton" runat="server" Text="Cancel" style="visibility: hidden;"/>

                        </p>
                    </div>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="AddImageLinkButton"
                    PopupControlID="Panel1" OkControlID="OkButton" BackgroundCssClass="modalBackground"
                    CancelControlID="CancelButton" DropShadow="true" PopupDragHandleControlID="Panel3" />
            </td>
            
        </tr>

        <tr>
            <td class="style1">
                Type of Incident:
            </td>
            <td>
                <asp:ListBox ID="IncidentTypeListBox" runat="server" CssClass="Roundedcorner" Height="86px" SelectionMode="Multiple"
                    Width="171px" AutoPostBack="True" OnSelectedIndexChanged="IncidentTypeListBox_SelectedIndexChanged">
                </asp:ListBox>
              
                <a href="#" class="help"><img border="0" src=" http://b3.caspio.com/RMA_ref/help.png "><span>This is a help button - kewl huh?</span></a>

                <br />
                <asp:Panel ID="OtherIncidentTypePanel" Visible="false" runat="server">
                    Describe:
                    <asp:TextBox ID="OtherIncidentyTypeBox" runat="server"></asp:TextBox>
                </asp:Panel>

            </td>
        </tr>

        <tr>
            <td class="style1">
                Description of Incident:
                <asp:Label ID="IncidentID" runat="server" Text="0" Visible="False"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="DescriptionBox"  runat="server" CssClass="Roundedcorner tip" Title="Please enter as much information as you can"
                    Height="87px" MaxLength="500" TextMode="MultiLine"
                    Width="390px" Font-Names="Arial" placeholder="This only works in chrome, http://stackoverflow.com/questions/4681036/html-css-making-a-textbox-with-text-that-is-grayed-out-and-disappears-when-i-cl for alternative"></asp:TextBox>
            
            </td>
        </tr>
                
        <tr>
            <td class="style1">
                Number of People Involved:
            </td>
            <td>
                <asp:DropDownList ID="NoPeopleDropDown" runat="server" Width="100" AutoPostBack="True"
                    OnSelectedIndexChanged="NoPeopleDropDown_SelectedIndexChanged" CssClass="yourbox">
                    <asp:ListItem>unknown</asp:ListItem>
                    <asp:ListItem Value="0">0</asp:ListItem>
                    <asp:ListItem Value="1">1</asp:ListItem>
                    <asp:ListItem Value="2">2</asp:ListItem>
                    <asp:ListItem Value="3">3</asp:ListItem>
                    <asp:ListItem Value="4">4</asp:ListItem>
                    <asp:ListItem Value="5">5+</asp:ListItem>
                </asp:DropDownList>

                <asp:Label ID="Label1" runat="server" ForeColor="black" Visible="false" Width="400px" Font-Names="Arial" Text="The more information you can provide the better, but if any options do not apply or if you are unable to input a value then leave as it is" />
                               

                <div runat="server" id="peopletable" visible="false" style="padding-top:1em">
                <table id="table1" CssClass="Roundedcorner">
                    <tr>
                        <td style="width: 400px">
                            <asp:Repeater ID="PeopleInvolvedRepeater" runat="server">
                                
                                <ItemTemplate>
                                <table>
                                    <tr><p align="center" style=" font-size:medium">Person&nbsp; <%# DataBinder.Eval(Container.DataItem, "person_order_id") %></p></tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="PersonGenderBox" runat="server" CssClass="yourbox">
                                                <asp:ListItem>Gender</asp:ListItem>
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="M">Male</asp:ListItem>
                                                <asp:ListItem Value="F">Female</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="PersonAgeGroupBox" runat="server" CssClass="yourbox">
                                                <asp:ListItem>Age Group</asp:ListItem>
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>5-10 years</asp:ListItem>
                                                <asp:ListItem>11-15 years</asp:ListItem>
                                                <asp:ListItem>16-20 years</asp:ListItem>
                                                <asp:ListItem>21-40 years</asp:ListItem>
                                                <asp:ListItem>41-60 years</asp:ListItem>
                                                <asp:ListItem>61+ years</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="PersonEthnicityBox" runat="server" CssClass="yourbox">
                                                <asp:ListItem>Ethnicity</asp:ListItem>
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>European</asp:ListItem>
                                                <asp:ListItem>Maori</asp:ListItem>
                                                <asp:ListItem>Asian</asp:ListItem>
                                                <asp:ListItem>Indian</asp:ListItem>
                                                <asp:ListItem>Pacific Islander</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>                                    
                                    
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="yourbox">
                                                <asp:ListItem>Height</asp:ListItem>
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem Value="">< 100cm   (< 3'3")</asp:ListItem>
                                                <asp:ListItem Value="">101-110cm (3'3"-3'7")</asp:ListItem>
                                                <asp:ListItem Value="">111-120cm (3'7"-3'11")</asp:ListItem>
                                                <asp:ListItem Value="">121-130cm (3'11"-4'3")</asp:ListItem>
                                                <asp:ListItem Value="">131-140cm (4'3"-4'7")</asp:ListItem>
                                                <asp:ListItem Value="">141-150cm (4'7"-4'11")</asp:ListItem>
                                                <asp:ListItem Value="">151-160cm (4'11"-5'3")</asp:ListItem>
                                                <asp:ListItem Value="">161-170cm (5'3"-5'7")</asp:ListItem>
                                                <asp:ListItem Value="">171-180cm (5'7"-5'11")</asp:ListItem>
                                                <asp:ListItem Value="">181-190cm (5'11"-6'3")</asp:ListItem>
                                                <asp:ListItem Value="">191-200cm (6'3"-6'7")</asp:ListItem>
                                                <asp:ListItem Value="">> 201cm   (> 6'7")</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="yourbox">
                                                <asp:ListItem>Build</asp:ListItem>
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>gangly</asp:ListItem>
                                                <asp:ListItem>well built</asp:ListItem>
                                                <asp:ListItem>slender</asp:ListItem>
                                                <asp:ListItem>plump</asp:ListItem>
                                                <asp:ListItem>obese</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <%--<asp:DropDownList ID="DropDownList3" runat="server" CssClass="yourbox">
                                                <asp:ListItem>Spare</asp:ListItem>
                                                <asp:ListItem></asp:ListItem>
                                                <asp:ListItem>item 1</asp:ListItem>
                                                <asp:ListItem>item 2</asp:ListItem>
                                                <asp:ListItem>item 3</asp:ListItem>
                                            </asp:DropDownList>--%>
                                        </td>
                                        
                                    </tr>                               
                                                     
                                </table>
                                 
                                <asp:TextBox ID="PersonDescriptionBox" CssClass="featuresBox tip" Title="Please enter as much information as you can" runat="server" Width="340px" 
                                            Height="50px" TextMode="MultiLine" Font-Names="Arial" 
                                            placeholder="Describe any distinguishing features that you feel are important, i.e. clothing, tatoos, scars, hats, hair length/color etc"></asp:TextBox>
                                    
                                </ItemTemplate>
                                <SeparatorTemplate>
                                    <hr style=" margin-top:1em; margin-bottom:1em">
                                </SeparatorTemplate>
                            </asp:Repeater>
                        </td>
                    </tr>
                </table>
                </div>
                               
            </td>
        </tr>
        <tr>
            <td class="style1">
                Related Incident:
            </td>
            <td>
                <asp:LinkButton ID="LinkOtherIncidentLinkButton" runat="server" Text="Click here to link to other incident." />
                <asp:Panel ID="LinkOtherIncidentPanel1" runat="server" Style="display: none; background: white"
                    CssClass="modalPopup">
                    <asp:Panel ID="LinkOtherIncidentPanel2" runat="server" Style="cursor: move; background-color: #DDDDDD;
                        border: solid 1px Gray; color: black;">
                        <div>
                            <p>
                                Link to another incident:
                            </p>
                        </div>
                    </asp:Panel>
                    <table style="width: 400px">
                        <tr>
                            <td style="width: 150px">
                                Other Incidents from the area:
                            </td>
                            <td>
                                <asp:ListBox ID="OtherIncidentsListBox" runat="server"></asp:ListBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 150px">
                            </td>
                            <td>
                                <asp:Button ID="LinkRelatedIncidentButton" runat="server" Text="Link" OnClick="LinkRelatedIncidentButton_Click" />
                                <asp:Button ID="LinkOtherIncidentCancelButton" runat="server" Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="LinkOtherIncidentLinkButton"
                    PopupControlID="LinkOtherIncidentPanel1" BackgroundCssClass="modalBackground"
                    CancelControlID="LinkOtherIncidentCancelButton" DropShadow="true" PopupDragHandleControlID="LinkOtherIncidentPanel2" />
            </td>
        </tr>
        <tr>
            <td class="style1">
            </td>
            <td id="buttons">
                <asp:Button ID="CancelandDeleteButton" runat="server" Text="Cancel" OnClick="CancelandDeleteButton_Click" Width="80px" Height="40px" />
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="SubmitButtom" runat="server" Text="Submit" OnClick="SubmitButtom_Click" Width="80px" Height="40px" />
            </td>
        </tr>
    </table>

</asp:Content>
