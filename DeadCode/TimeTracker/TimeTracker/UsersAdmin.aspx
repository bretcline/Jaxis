<%@ Page Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="UsersAdmin.aspx.cs" Inherits="UsersAdmin" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SubPage2" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td align="center">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="Large" 
                    Text="Users Admin"></asp:Label>
            </td>
        </tr>
    </table>
    
    <script type = "text/jscript">
    
    function clickButton(e, btnSave)
    { 
        var evt = e ? e : window.event;

        var bt = document.getElementById(btnSave);

          if (bt)
          { 
              if (evt.keyCode == 13)
              {
                    bt.click(); 
                    
                    return false;
              } 

          } 

    }
    
    </script>
    
<table style="width: 100%;" id="tblHoldUserAdmin">
        <tr>
            <td rowspan="6" width="30%" valign="top">
                <asp:ListBox ID="lstUsers" runat="server" DataSourceID="LinqDataSource1" 
                    DataTextField="FullName" DataValueField="UserID" Rows="6" Width="98%" 
                    onselectedindexchanged="lstUsers_SelectedIndexChanged" AutoPostBack="True">
                </asp:ListBox>
                <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                    ContextTypeName="TimetrackerData.TimetrackerLinqDataContext" 
                    TableName="Users" Where="UserVisible == @UserVisible">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="true" Name="UserVisible" Type="Boolean" />
                    </WhereParameters>
                </asp:LinqDataSource>
                <asp:Button ID="btnAddUser" runat="server" Text="Add User" 
                    onclick="btnAddUser_Click" />
                &nbsp;<asp:Button ID="btnRemove" runat="server" onclick="btnRemove_Click" 
                    Text="Remove User" />
            </td>
            <td align="center">
                <asp:Label ID="lblUserLogin" runat="server" Text="User Login"></asp:Label>
            </td>
            <td width="50%">
                <asp:TextBox ID="txtUserLogin" runat="server" Width="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblPassword" runat="server" Text="Password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtPassword" runat="server" Width="98%" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm New Password"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtConfirmPassword" runat="server" Width="98%" 
                    TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblFirstName" runat="server" Text="First Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server" Width="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblLastName" runat="server" Text="Last Name"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server" Width="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblCompany" runat="server" Text="Company"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtCompany" runat="server" Width="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="30%" valign="top" align="left">
                <asp:Button ID="btnManageUserGroups" runat="server" 
                    onclick="btnManageUserGroups_Click" Text="Manage User Groups" />
            </td>
            <td align="center">
                &nbsp;</td>
            <td align="center">
                <asp:Button ID="btnSave" runat="server" onclick="btnSave_Click" 
                    Text="Save Changes" />
                <br />
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>

