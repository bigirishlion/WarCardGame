<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="WarCardGame._default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Play War!<br />
        <br />
        Player 1<br />
        <asp:TextBox ID="player1TextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        Player 2<br />
        <asp:TextBox ID="player2TextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        How many rounds do you want to play?<br />
        <asp:TextBox ID="totalRoundsTextBox" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="playButton" runat="server" OnClick="playButton_Click" Text="Play" />
        <br />
        <br />
        <asp:Label ID="resultLabel" runat="server"></asp:Label>
    
    </div>
    </form>
</body>
</html>
