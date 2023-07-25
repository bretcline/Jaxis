<%@ Page Language="C#" MasterPageFile="~/AdminMaster.master" AutoEventWireup="true" CodeFile="ProjectsAdmin.aspx.cs" Inherits="_Default" Title="Untitled Page" %>

<asp:Content ID="PageData" ContentPlaceHolderID="SubPage2" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td align="center">
                <asp:Label ID="lblHeader" runat="server" Font-Bold="True" Font-Size="Large" 
                    Text="Projects Admin"></asp:Label>
            </td>
        </tr>
    </table>

    <table style="width: 100%;" id="tblHoldProjectAdmin">
        <tr>
            <td rowspan="2" width="30%" valign="top">
                <asp:ListBox ID="lstProjects" runat="server" DataSourceID="LinqDataSource1" 
                    DataTextField="Name" DataValueField="ProjectID" Rows="6" Width="98%" 
                    onselectedindexchanged="lstProjects_SelectedIndexChanged" AutoPostBack="True">
                </asp:ListBox>
                <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                    ContextTypeName="TimetrackerData.TimetrackerLinqDataContext" 
                    TableName="Projects" Where="ProjectVisible == @ProjectVisible">
                    <WhereParameters>
                        <asp:Parameter DefaultValue="true" Name="ProjectVisible" Type="Boolean" />
                    </WhereParameters>
                </asp:LinqDataSource>
                <asp:Button ID="btnAddProject" runat="server" Text="Add Project" 
                    onclick="btnAddProject_Click" />
            &nbsp;<asp:Button ID="btnRemoveProject" runat="server" onclick="btnRemoveProject_Click" 
                    Text="Remove Project" />
            </td>
            <td align="center">
                <asp:Label ID="lblProjectName" runat="server" Text="Project Name"></asp:Label>
            </td>
            <td width="50%">
                <asp:TextBox ID="txtProjectName" runat="server" Width="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Label ID="lblProjectDescription" runat="server" Text="Project Description"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtProjectDescription" runat="server" Width="98%" Height="98%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="30%" valign="top">
                <asp:Button ID="btnManageProjectGroups" runat="server" 
                    onclick="btnManageProjectGroups_Click" Text="Manage Project Groups" />
                </td>
            <td align="center">
                &nbsp;</td>
            <td align="center">
                <asp:Button ID="btnProjectSave" runat="server" onclick="btnProjectSave_Click" 
                    Text="Save Changes" />
                <br />
                <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        </table>
    

</asp:Content>
