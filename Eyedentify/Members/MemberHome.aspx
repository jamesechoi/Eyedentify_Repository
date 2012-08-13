<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="MemberHome.aspx.cs" Inherits="Eyedentify.MemberHome" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .layout
        {
            width:300px;
            height:350px;
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
    <asp:DataList ID="GridViewList" runat="server" RepeatColumns="3" Width="900px">
        <ItemTemplate>
            <table class="layout">
                <tr>
                    <td class="style1">
                        <a href ='<%# "Incident.aspx?iID=" +Eval("Incident_ID")%>'> <asp:Image ID="Image1" runat="server" style="padding-top: 15px; border:0" ImageUrl='<%# GetImageString(Eval("Incident_Image_ID"))%>' /></a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HyperLink ID="TitleHyperLink" runat="server" Text='<%# Eval("Subject")%>' NavigateUrl='<%# "Incident.aspx?iID=" +Eval("Incident_ID")%>'></asp:HyperLink>
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
