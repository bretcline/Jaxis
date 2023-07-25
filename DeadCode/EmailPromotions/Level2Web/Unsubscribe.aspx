<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Unsubscribe.aspx.cs" Inherits="Level2Web.Unsubscribe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Unsubscribe</title>
</head>
<body>
    <form id="Unsubscribe" runat="server">
    <div>
    
    </div>
    <asp:Label ID="Label1" runat="server" Text="Email Address:"></asp:Label>
    &nbsp;&nbsp;
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>&nbsp;
    <asp:CustomValidator ID="customValidator" runat="server" ValidateEmptyText="true" ControlToValidate="txtEmail" OnServerValidate="EmailValidate" ErrorMessage="*Email Address is not a valid format" />
    <br />
    <br />
    <asp:Button ID="btnUnsubscribe" runat="server" Text="Unsubscribe" 
        onclick="btnUnsubscribe_Click" />
    </form>
</body>
</html>
