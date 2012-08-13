<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Eyedentify.Admin.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Roles<br />
        <asp:GridView ID="RolesGridView" runat="server" CellPadding="4" ForeColor="#333333"
            GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <br />
        <asp:TextBox ID="RoleTextBox" runat="server" Width="218px"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="AddRoleButton" runat="server" OnClick="AddRoleButton_Click" Text="Add Role" />
        <br />
        <br />
        <br />
        Users<asp:GridView ID="UsersGridView" runat="server" AutoGenerateSelectButton="True"
            CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="UsersGridView_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <br />
        <asp:Panel ID="Panel1" runat="server" Visible="false">
            <asp:Label ID="SelectedUserLabel" runat="server" Text="Label"></asp:Label>
            &nbsp;&nbsp;
            <asp:Button ID="DeselectAllButton" runat="server" OnClick="DeselectAllButton_Click"
                Text="Deselect All" />
            <br />
            <asp:ListBox ID="AssignedRoleListBox" runat="server" Height="129px" SelectionMode="Multiple"
                Width="256px"></asp:ListBox>
            &nbsp;
            <asp:Button ID="AssignRoleButton" runat="server" OnClick="AssignRoleButton_Click"
                Text="Assign Roles" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="CancelButton" runat="server" OnClick="CancelButton_Click" Text="Cancel" />
        </asp:Panel>
        <br />
    </div>
    </form>
</body>
</html>
