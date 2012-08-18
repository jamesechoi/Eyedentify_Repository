<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MemberHome.aspx.cs" Inherits="Eyedentify.MemberHome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .layout
        {
            width: 300px;
            height: 350px;
            text-align: center;
            background: url(../Images/incidentsBackground.png) repeat;
        }
        .style1
        {
            height: 21px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    Incident Type:
    <asp:ListBox ID="IncidentTypeListBox" runat="server" CssClass="Roundedcorner" Height="86px"
        SelectionMode="Multiple" Width="171px" AutoPostBack="True"></asp:ListBox>
    &nbsp;&nbsp; Incident Type:
    <asp:DropDownList ID="DistanceFilterDropdown" runat="server" Width="150px">
        <asp:ListItem Value=""></asp:ListItem>
        <asp:ListItem Value="0.1">Within a 100m radius</asp:ListItem>
        <asp:ListItem Value="0.5">Within a 500m radius</asp:ListItem>
        <asp:ListItem Value="1">Within a 1km radius</asp:ListItem>
    </asp:DropDownList>
    &nbsp;&nbsp;
    <asp:Button ID="FilterButton" runat="server" Text="Filter" OnClick="FilterButton_Click"
        Width="79px" />
    <br />
    <br />
    <asp:DataList ID="GridViewList" runat="server" RepeatColumns="3" Width="900px">
        <ItemTemplate>
            <table class="layout">
                <tr>
                    <td class="style1">
                        <a href='<%# GetHyperLink() +Eval("Incident_ID")%>'>
                            <asp:Image ID="Image1" runat="server" Style="padding-top: 15px; border: 0" ImageUrl='<%# GetImageString(Eval("Incident_Image_ID"))%>' /></a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="TitleHyperLink" runat="server" Text='<%# Eval("Subject")%>' NavigateUrl='<%# GetHyperLink() +Eval("Incident_ID")%>'></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Description")%>'></asp:Label>
                    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:DataList>
    <div _designerregion="0">
    </div>
</asp:Content>
