<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Default2"  MasterPageFile="~/SiteMaster.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SitePage" runat="server">

    <table width = "100%">
        <tr>
            <td align="right" colspan="2">
    
        <asp:Label ID="lblUsername" runat="server" Text="Username"></asp:Label>
    
            &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtUsername" runat="server" Width="50%"></asp:TextBox>
    
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
    
        <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
    
            &nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtPassword" runat="server" Width="50%" TextMode="Password"></asp:TextBox>
    
            </td>
        </tr>
        <tr>
            <td align="right">
    
                <asp:Label ID="lblInvalidLogin" runat="server" ForeColor="Red"></asp:Label>
            </td>
            <td align="center" width="50%">
                <asp:Button ID="btnLogin" runat="server" onclick="btnLogin_Click" 
                    Text="Login" />
            </td>
        </tr>
    </table>
</asp:Content>