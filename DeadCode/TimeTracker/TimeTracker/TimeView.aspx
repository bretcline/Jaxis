<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimeView.aspx.cs" Inherits="TimeView"  MasterPageFile="~/TimeMaster.master" %>

<asp:Content ContentPlaceHolderID=PageData runat=server>

    <table id="tblHoldCalenders" width = "100%">
        <tr>
            <td align="center">
                <asp:Label ID="lblStartError" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="lblSelectStart" runat="server" Text="Select Start Date"></asp:Label>
&nbsp;<asp:TextBox ID="txtStart" runat="server" AutoPostBack="True" 
                    ontextchanged="txtStart_TextChanged"></asp:TextBox>
            </td>
            <td align="center" width="50%">
                <asp:Label ID="lblEndError" runat="server" ForeColor="Red"></asp:Label>
                <br />
                <asp:Label ID="lblSelectEnd" runat="server" Text="Select End Date"></asp:Label>
&nbsp;<asp:TextBox ID="txtEnd" runat="server" AutoPostBack="True" 
                    ontextchanged="txtEnd_TextChanged"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:Calendar ID="clndrStart" runat="server" 
                    onselectionchanged="clndrStart_SelectionChanged"></asp:Calendar>
            </td>
            <td width="50%" align="center">
                <asp:Calendar ID="clndrEnd" runat="server" 
                    onselectionchanged="clndrEnd_SelectionChanged"></asp:Calendar>
            </td>
        </tr>
    </table>

    <table id="tblHoldDataGrid" style="width:100%;">
        <tr>
            <td align="center">
                &nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <asp:GridView ID="grdTime" runat="server" AutoGenerateColumns="False" 
                    DataSourceID="LinqDataSource1" CellPadding="2" CellSpacing="1" AllowSorting="True" 
                    AutoGenerateSelectButton="True" 
                    onselectedindexchanged="grdTime_SelectedIndexChanged" 
                    DataKeyNames="TimeEntryID" AllowPaging="True" PageSize="100" >
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Name" 
                            SortExpression="Name" ReadOnly="True" />
                        <asp:BoundField DataField="Notes" HeaderText="Notes" 
                            SortExpression="Notes" ReadOnly="True" Visible="False" />
                        <asp:BoundField DataField="StartTime" HeaderText="StartTime" 
                            SortExpression="StartTime" ReadOnly="True" />
                        <asp:BoundField DataField="EndTime" HeaderText="EndTime" 
                            SortExpression="EndTime" ReadOnly="True" />
                        <asp:BoundField DataField="Elapsed_Time" HeaderText="Elapsed_Time" 
                            SortExpression="Elapsed_Time" ReadOnly="True" />
                        <asp:BoundField DataField="TimeEntryID" HeaderText="TimeEntryID" 
                            ReadOnly="True" SortExpression="TimeEntryID" Visible="False" />
                    </Columns>
                </asp:GridView>
                <asp:LinqDataSource ID="LinqDataSource1" runat="server" 
                    ContextTypeName="TimetrackerData.TimetrackerLinqDataContext" 
                    OrderBy="StartTime desc, Name" TableName="vwTimeEntries" 
                    
                    Where="UserID == @UserID &amp;&amp; ProjectVisible == @ProjectVisible &amp;&amp; StartTime &gt;= @StartTime &amp;&amp; EndTime &lt;= @EndTime" 
                    Select="new (Name, Notes, StartTime, EndTime, Elapsed_Time, TimeEntryID)">
                    <WhereParameters>
                        <asp:ControlParameter ControlID="txtUserID" Name="UserID" PropertyName="Text" 
                            Type="Int32" />
                        <asp:Parameter DefaultValue="true" Name="ProjectVisible" Type="Boolean" />
                        <asp:ControlParameter ControlID="clndrStart" Name="StartTime" 
                            PropertyName="SelectedDate" Type="DateTime" />
                        <asp:ControlParameter ControlID="clndrEnd" Name="EndTime" 
                            PropertyName="SelectedDate" Type="DateTime" />
                    </WhereParameters>
                </asp:LinqDataSource>
            </td>
        </tr>
    </table>

<asp:TextBox ID="txtUserID" runat="server" Visible="False"></asp:TextBox>

</asp:Content> 

