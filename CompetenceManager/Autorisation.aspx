<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Autorisation.aspx.cs" Inherits="CompetenceManager.Autorisation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
        <br />
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server" BorderStyle="Outset" HorizontalAlign="Center" Width="500px" Wrap="False">
            <br />
            <asp:Label ID="Label1" runat="server" Text="Введите пароль:" Font-Names="Arial" Font-Size="20pt" ForeColor="Black"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="tb_Password" runat="server" Font-Names="Arial" Font-Size="20pt" OnTextChanged="tb_Password_TextChanged" Width="200px" Wrap="False"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="l_Error" runat="server" Font-Bold="True" Font-Names="Arial" ForeColor="#CC0000" Text="Пароль неверен, попробуйте ввести заново." Visible="False"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btn_GoIn" runat="server" Font-Names="Arial" Font-Size="20pt" OnClick="btn_GoIn_Click" Text="Вход" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_Cancel" runat="server" Font-Names="Arial" Font-Size="20pt" OnClick="btn_Cancel_Click" Text="Отмена" />
            <br />
            <br />
        </asp:Panel>
        <br />
    
    </div>
    </form>
</body>
</html>
