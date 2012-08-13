<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Eyedentify._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        Welcome to 
        Eyedentify!
    </h2>
    <p>
        To report an incident, click <a href="Members/ReportIncident.aspx" title="Report an incident">here</a>.
    </p>
    <p>
        Member home page, click <a href="Members/MemberHome.aspx" 
            title="Report an incident">here</a>.
    </p>
</asp:Content>
