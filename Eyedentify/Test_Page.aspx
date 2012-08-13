<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_Page.aspx.cs" Inherits="Eyedentify.Test_Page" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
change 1
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

    
        <br />
        <br />
        Incident ID
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
&nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
            Text="Finalise Incident" />
    
        <br />
        <br />
        Incident ID
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
&nbsp;&nbsp;
        <asp:FileUpload ID="FileUpload1" runat="server" />
&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="Upload Image" 
            onclick="Button3_Click" />
    
        <br />
        <br />
        <asp:Button ID="Button4" runat="server" onclick="Button4_Click" 
            Text="Send Email" />
    
    </div>
    </form>
</body>
</html>
