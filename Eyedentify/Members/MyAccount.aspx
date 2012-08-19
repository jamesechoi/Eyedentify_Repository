<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MyAccount.aspx.cs" Inherits="Eyedentify.Members.MyAccount" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HyperLink ID="HyperLink1" runat="server" 
        NavigateUrl="ChangePassword.aspx">Change Password</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" 
        NavigateUrl="MemberHome.aspx?type=myAcct&search=myIncidents">View My Incidents</asp:HyperLink>
        <br />
        <asp:HyperLink ID="HyperLink3" runat="server" 
        NavigateUrl="MemberHome.aspx?type=myAcct&search=unfinished">View My Unfinished Incidents</asp:HyperLink>
</asp:Content>
