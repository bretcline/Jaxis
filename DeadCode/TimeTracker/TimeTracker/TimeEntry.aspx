<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="TimeEntry.aspx.cs" Inherits="_Default" MasterPageFile="~/TimeMaster.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="PageData" runat="server">

    <table id="tblHoldTop" width="100%">
        <tr><td colspan = "3" align="center">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td rowspan = "4" align="center" width="25%">
                <asp:TextBox ID="txtDate" runat="server" AutoPostBack="True" 
                    CausesValidation="True" ontextchanged="txtDate_TextChanged"></asp:TextBox>
                <asp:Calendar ID="clndrDay" runat="server" 
                    onselectionchanged="clndrDay_SelectionChanged1"></asp:Calendar>
            </td>
            <td width="10%" align="right">
                <asp:Label ID="lblProject" runat="server" Text="Project"></asp:Label>
            </td>
            <td width = "65%">
                <asp:DropDownList ID="ddlProjects" runat="server" TabIndex="1">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td width="10%" align="center">
                <asp:Label ID="lblStartTime" runat="server" Text="Start Time"></asp:Label>
            </td>
            <td width = "65%">
                <asp:Label ID="lblStartMonth" runat="server" Text="MM"></asp:Label>
&nbsp;<asp:TextBox ID="txtStartMonth" runat="server" MaxLength="2" TabIndex="1" Width="45px"></asp:TextBox>
&nbsp;<asp:Label ID="lblStartDay" runat="server" Text="DD"></asp:Label>
&nbsp;<asp:TextBox ID="txtStartDay" runat="server" MaxLength="2" TabIndex="1" Width="45px"></asp:TextBox>
&nbsp;<asp:Label ID="lblStartYear" runat="server" Text="YYYY"></asp:Label>
&nbsp;<asp:TextBox ID="txtStartYear" runat="server" MaxLength="4" TabIndex="2" Width="45px"></asp:TextBox>
&nbsp;<asp:Label ID="lblStartHour" runat="server" Text="Hr."></asp:Label>
&nbsp;<asp:TextBox ID="txtStartHour" runat="server" MaxLength="2" TabIndex="3" Width="45px"></asp:TextBox>
&nbsp;<asp:Label ID="lblStartMinute" runat="server" Text="Min."></asp:Label>
&nbsp;<asp:TextBox ID="txtStartMinute" runat="server" MaxLength="2" TabIndex="4" Width="45px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="10%" align="center">
                <asp:Label ID="lblEndTime" runat="server" Text="End Time"></asp:Label>
            </td>
            <td width = "65%">
                <asp:Label ID="lblEndMonth" runat="server" Text="MM"></asp:Label>
&nbsp;<asp:TextBox ID="txtEndMonth" runat="server" MaxLength="2" TabIndex="6" Width="45px"></asp:TextBox>
&nbsp;<asp:Label ID="lblEndDay" runat="server" Text="DD"></asp:Label>
&nbsp;<asp:TextBox ID="txtEndDay" runat="server" MaxLength="2" TabIndex="7" Width="45px"></asp:TextBox>
&nbsp;<asp:Label ID="lblEndYear" runat="server" Text="YYYY"></asp:Label>
&nbsp;<asp:TextBox ID="txtEndYear" runat="server" MaxLength="4" TabIndex="8" Width="45px"></asp:TextBox>
&nbsp;<asp:Label ID="lblEndHour" runat="server" Text="Hr."></asp:Label>
&nbsp;<asp:TextBox ID="txtEndHour" runat="server" MaxLength="2" TabIndex="9" Width="45px"></asp:TextBox>
&nbsp;<asp:Label ID="lblEndMinute" runat="server" Text="Min."></asp:Label>
&nbsp;<asp:TextBox ID="txtEndMinute" runat="server" MaxLength="2" TabIndex="10" Width="45px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width="10%" align="center">
                <asp:Label ID="lblNewNotes" runat="server" Text="New Notes"></asp:Label>
            </td>
            <td width = "65%">
                <asp:TextBox ID="txtNewNotes" runat="server" Height="90px" TabIndex="12" 
                    TextMode="MultiLine" Width="98%"></asp:TextBox>
            </td>
        </tr>
    </table>
    <table id="tblHoldBottom" width="100%">
        <tr>
            <td align="center" width = "33%">
                <asp:Label ID="lblPreviousNotes" runat="server" Text="Notes"></asp:Label>
            </td>
            <td width = "33%">
                &nbsp;</td>
            <td width = "34%">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="3">
                <asp:TextBox ID="txtPreviousNotes" runat="server" Height="100px" TextMode="MultiLine" 
                    Width="98%" ontextchanged="txtNotes_TextChanged" ReadOnly="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td width = "33%">
                &nbsp;</td>
            <td width = "33%" align="right">
                &nbsp;</td>
            <td width = "34%" align="right">
                <asp:Button ID="btnSave" runat="server" Text="Save" Width="65px" 
                    onclick="btnAdd_Click" style="height: 26px" TabIndex="14" /> 
                <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="65px" 
                    onclick="btnDelete_Click" TabIndex="15" />
            </td>
        </tr>
    </table>

</asp:Content> 

