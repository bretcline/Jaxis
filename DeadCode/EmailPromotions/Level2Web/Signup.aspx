<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Signup.aspx.cs" Inherits="Level2Web.Signup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
function SetSelected()
{
    document.Signup.txtEmail.select();
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Signup</title>
</head>
<body>
    <form id="Signup" runat="server">
    <div>
    </div>
    
    <asp:Label ID="Label1" runat="server" Text="First Name:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtFirstName" runat="server" TabIndex="1"></asp:TextBox>&nbsp;
    <asp:RequiredFieldValidator ID="firstNameValidator" runat="server" ControlToValidate="txtFirstName" ErrorMessage="*First Name is required"/>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="Last Name:"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtLastName" runat="server" TabIndex="2"></asp:TextBox>&nbsp
    <asp:RequiredFieldValidator ID="lastNameValidator" runat="server" ControlToValidate="txtLastName" ErrorMessage="*Last Name is required"/>
    <br />
    <br />
    <asp:Label ID="Label3" runat="server" Text="Email Address:"></asp:Label>&nbsp;
    <asp:TextBox ID="txtEmail" runat="server" TabIndex="3"></asp:TextBox>&nbsp
    <asp:CustomValidator ID="customValidator" runat="server" ValidateEmptyText="true" ControlToValidate="txtEmail" OnServerValidate="EmailValidate" ErrorMessage="*Email Address is not a valid format" />
    <br />
    <br />
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" />
    </form>
</body>
</html>
