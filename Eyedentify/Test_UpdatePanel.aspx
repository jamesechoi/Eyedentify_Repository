<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebForm1.aspx.cs" Inherits="Eyedentify.WebForm1" %>
change 2
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<script runat="server">

    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(2000);
        Label1.Text += " " + DateTime.Now.ToShortDateString();
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        
<script language="javascript" type="text/javascript">



    var prm = Sys.WebForms.PageRequestManager.getInstance();

    prm.add_initializeRequest(InitializeRequest);
    prm.add_endRequest(EndRequest);
    var postBackElement;
    function InitializeRequest(sender, args) {

        if (prm.get_isInAsyncPostBack())
            args.set_cancel(true);
        postBackElement = args.get_postBackElement();
        if (postBackElement.id == 'Button1')
            $get('UpdateProgress1').style.display = 'block';
    }



    function EndRequest(sender, args) {
        if (postBackElement.id == 'Button1')
            $get('UpdateProgress1').style.display = 'none';
    } 

 

</script>
    </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Label ID="Label1" runat="server" Width="145px"></asp:Label>
            </ContentTemplate>
         <Triggers> 
            <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"/>
        </Triggers>
        </asp:UpdatePanel>
        <asp:UpdateProgress ID="UpdateProgress1"  AssociatedUpdatePanelID="UpdatePanel1" runat="server"><ProgressTemplate>
Update in Progress...... 
</ProgressTemplate>
</asp:UpdateProgress>
 
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </form>
</body>
</html>
