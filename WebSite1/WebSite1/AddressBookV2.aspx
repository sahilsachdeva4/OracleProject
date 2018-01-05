<%@ Page Title="Address Book Version 2" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddressBookV2.aspx.cs" Inherits="AddressBookV2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Address Book Version 2</h1>

    <asp:Button ID="insertNewRecord" runat="server" Text="Insert New Employee" OnClick="insertNewRecord_Click" />
    
    <asp:SqlDataSource ID="employeesDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Oracle1ConnectionString %>"  ProviderName="<%$ ConnectionStrings:Oracle1ConnectionString.ProviderName %>" SelectCommand="SELECT EmployeeID, Name, City, MobilePhone FROM DN_Employees ORDER BY Name"></asp:SqlDataSource>
    <asp:GridView ID="grid" runat="server" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="EmployeeID" DataSourceID="employeesDataSource" PageSize="3">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="MobilePhone" HeaderText="MobilePhone" />
            <asp:CommandField ShowSelectButton="True" />
        </Columns>
        <PagerSettings Mode="NextPrevious" NextPageText="Next" PreviousPageText="Prev" />
        <PagerStyle CssClass="GridHeader" />
    </asp:GridView>

    <br />

    <asp:DetailsView ID="employeeDetails" runat="server" AutoGenerateRows="False" DataKeyNames="EmployeeID" DataSourceID="employeeDataSource" OnItemDeleted="employeeDetails_ItemDeleted" OnItemInserted="employeeDetails_ItemInserted" OnItemUpdated="employeeDetails_ItemUpdated" >
        <Fields>
            <asp:TemplateField HeaderText="DepartmentID" SortExpression="DepartmentID">
                <EditItemTemplate>
                    <asp:DropDownList ID="departmentIdEdit" runat="server" DataSourceID="departmentDataSource" DataTextField="Department" DataValueField="DepartmentID" selectedvalue=<%# Bind("DepartmentID") %>>
                    </asp:DropDownList>
                </EditItemTemplate>
                <InsertItemTemplate>
                    <asp:DropDownList ID="departmentIdEdit" runat="server" DataSourceID="departmentDataSource" DataTextField="Department" DataValueField="DepartmentID" selectedvalue=<%# Bind("DepartmentID") %>>
                    </asp:DropDownList>
                </InsertItemTemplate>
                <ItemTemplate>
                    <asp:DropDownList ID="departmentIdEdit" Enabled="false" runat="server" DataSourceID="departmentDataSource" DataTextField="Department" DataValueField="DepartmentID" selectedvalue=<%# Bind("DepartmentID") %>>
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
            <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
            <asp:BoundField DataField="Password" HeaderText="Password" SortExpression="Password" />
            <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
            <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
            <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
            <asp:BoundField DataField="MobilePhone" HeaderText="MobilePhone" SortExpression="MobilePhone" />
            <asp:BoundField DataField="Extension" HeaderText="Extension" SortExpression="Extension" />
            <asp:BoundField DataField="HomePhone" HeaderText="HomePhone" SortExpression="HomePhone" />
            <asp:BoundField DataField="Zip" HeaderText="Zip" SortExpression="Zip" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
        </Fields>
        <HeaderTemplate>
            <%# Eval("Name") %>
        </HeaderTemplate>
        <HeaderStyle CssClass="GridHeader" />
    </asp:DetailsView>
    <asp:SqlDataSource ID="employeeDataSource" runat="server" 
        ConnectionString="<%$ ConnectionStrings:Oracle1ConnectionString %>" 
        ProviderName="<%$ ConnectionStrings:Oracle1ConnectionString.ProviderName %>"
        DeleteCommand="DELETE FROM DN_Employees WHERE EmployeeID = :EmployeeID" 
        InsertCommand="INSERT INTO DN_Employees (DepartmentID, Name, Username, Password, Address, City, State, MobilePhone, Extension, HomePhone, Zip) VALUES (:DepartmentID, :Name, :Username, :Password, :Address, :City, :State, :MobilePhone, :Extension, :HomePhone, :Zip)" 
        SelectCommand="SELECT EmployeeID, DepartmentID, Name, Username, Password, Address, City, State, MobilePhone, Extension, HomePhone, Zip FROM DN_Employees WHERE (EmployeeID = :EmployeeID)" 
        UpdateCommand="UPDATE DN_Employees SET DepartmentID = :DepartmentID, Name = :Name, Username = :Username, Password = :Password, Address = :Address, City = :City, State = :State, MobilePhone = :MobilePhone, Extension = :Extension, HomePhone = :HomePhone, Zip = :Zip WHERE EmployeeID =:EmployeeID">
        <DeleteParameters>
            <asp:Parameter Name="EmployeeID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="DepartmentID" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Username" Type="String" />
            <asp:Parameter Name="Password" Type="String" />
            <asp:Parameter Name="Address" Type="String" />
            <asp:Parameter Name="City" Type="String" />
            <asp:Parameter Name="State" Type="String" />
            <asp:Parameter Name="MobilePhone" Type="String" />
            <asp:Parameter Name="Extension" Type="String" />
            <asp:Parameter Name="HomePhone" Type="String" />
            <asp:Parameter Name="Zip" Type="String" />
        </InsertParameters>
        <SelectParameters>
            <asp:ControlParameter ControlID="grid" Name="EmployeeID" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
        <UpdateParameters>
            <asp:Parameter Name="DepartmentID" Type="Int32" />
            <asp:Parameter Name="Name" Type="String" />
            <asp:Parameter Name="Username" Type="String" />
            <asp:Parameter Name="Password" Type="String" />
            <asp:Parameter Name="Address" Type="String" />
            <asp:Parameter Name="City" Type="String" />
            <asp:Parameter Name="State" Type="String" />
            <asp:Parameter Name="MobilePhone" Type="String" />
            <asp:Parameter Name="Extension" Type="String" />
            <asp:Parameter Name="HomePhone" Type="String" />
            <asp:Parameter Name="Zip" Type="String" />
            <asp:Parameter Name="EmployeeID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="departmentDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:Oracle1ConnectionString %>" SelectCommand="SELECT &quot;DEPARTMENTID&quot;, &quot;DEPARTMENT&quot; FROM &quot;DN_DEPARTMENTS&quot; ORDER BY &quot;DEPARTMENT&quot;" ProviderName="<%$ ConnectionStrings:Oracle1ConnectionString.ProviderName %>"></asp:SqlDataSource>
</asp:Content>

