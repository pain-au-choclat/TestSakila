<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SakilaSearch.aspx.cs" Inherits="TestSakila.SakilaSearch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="stylesheet" href="StyleSheet.css" />
    <title>Gookila</title>
</head>
<body class="centered-wrapper">
    <form id="form1" runat="server">
        <div class="centered-content">
           <asp:Image ID="Image1" runat="server" ImageUrl="~/gookila4.png"></asp:Image>
           <hr />
           <asp:DropDownList ID="ActorList" runat="server" AppendDataBoundItems="true">
               <asp:ListItem Text="Select Actor" Value="0" />
           </asp:DropDownList>               
           <asp:Button ID="SearchButton" BorderStyle="None" runat="server" Text="Search" OnClick="SearchButton_Click" />
           <hr />
           <asp:GridView CssClass="centered-content" ID="GridView1" runat="server" BorderStyle="None" ForeColor="White" ShowHeaderWhenEmpty="true" EmptyDataText="You will need to select an actor first">
           </asp:GridView>
           <hr />
           <asp:Label ID="ExceptionLabel" runat="server" style="color:white" Text="This is where exceptions go! You're all good!"></asp:Label>
            <hr />
        </div>
    </form>
</body>
</html>
