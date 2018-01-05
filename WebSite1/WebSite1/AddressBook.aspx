<%@ Page Title="Dorknozzle Address Book" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddressBook.aspx.cs" Inherits="AddressBook" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Address Book</h1>
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanged="gridSelectionChanged">        
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" />
            <asp:BoundField DataField="City" HeaderText="City" />
            <asp:BoundField DataField="MobilePhone" HeaderText="Mobile Number" />
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <asp:Label ID="detailsLabel" runat="server">
    </asp:Label>
    <br />
    <asp:DetailsView CssClass="GridMain" ID="employeeDetails" runat="server" AutoGenerateRows="False" CellPadding="4" GridLines="None" OnModeChanging="employeeDetails_ModeChanging" OnItemUpdating="employeeDetails_ItemUpdating">
        <Fields>
            <asp:TemplateField HeaderText="Address">
                <EditItemTemplate>
                    <asp:TextBox ID="editAddress" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="insertAddress" runat="server" Text='<%# Bind("Address") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="addressLabel" runat="server" Text='<%# Bind("Address") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="City">
                <EditItemTemplate>
                    <asp:TextBox ID="editCity" runat="server" Text='<%# Bind("City") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="insertCity" runat="server" Text='<%# Bind("City") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="cityLabel" runat="server" Text='<%# Bind("City") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="State">
                <EditItemTemplate>
                    <asp:TextBox ID="editState" runat="server" Text='<%# Bind("State") %>'></asp:TextBox>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:TextBox ID="insertState" runat="server" Text='<%# Bind("State") %>'></asp:TextBox>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="stateLabel" runat="server" Text='<%# Bind("State") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Zip" HeaderText="Zip Code" />
            <asp:BoundField DataField="HomePhone" HeaderText="Home Phone Number" />
            <asp:BoundField DataField="Extension" HeaderText="Entension" />
            <asp:BoundField DataField="MobilePhone" HeaderText="Mobile Phone" />
            <asp:CommandField ShowEditButton="True" />
        </Fields>
        <RowStyle CssClass="GridRow" />
        <HeaderStyle CssClass="GridHeader" />
        <HeaderTemplate>
            <%# Eval("Name") %>
        </HeaderTemplate>
    </asp:DetailsView>
</asp:Content>