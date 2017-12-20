<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EmployeeDirectory.aspx.cs" Inherits="EmployeeDirectory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>
        Employee Directory
    </h1>
    <asp:DataList ID="employeeRepeater" runat="server" OnItemCommand="employeeRepeater_ItemCommand" BackColor="#FFFF99" CssClass="separator" GridLines="Both">
        <EditItemTemplate>
            Name<asp:TextBox ID="nameTextBox" runat="server" Text=<%# Eval("Name")%> ></asp:TextBox>
            <br />
            User Name<asp:TextBox ID="userNameTextBox" runat="server" Text=<%# Eval ("Username") %>></asp:TextBox>
            <br />
            <asp:LinkButton ID="updateButton" runat="server"
                CommandName="updateEdits" CommandArgument=<%# Eval("EmployeeID") %>>Update</asp:LinkButton>
            &nbsp;or&nbsp;
            <asp:LinkButton ID="cancelButton" runat="server"
                CommandName="cancelEdits">Cancel</asp:LinkButton>
        </EditItemTemplate>
        <ItemTemplate>
            <%-- Employee Id: <strong><%# Eval("EmployeeID") %></strong><br /> --%>
            <asp:Literal ID="employeeDetailsLiteral" runat="server" EnableViewState="False">

            </asp:Literal>
            Name: <strong><%# Eval("Name") %></strong><br />
            Username: <strong><%# Eval("Username") %></strong><br />
            <asp:LinkButton ID="detailsButton" runat="server" 
                Text=<%# "View more details about " + Eval("Name") %> 
                CommandName="MoreDetailsPlease" 
                CommandArgument=<%# Eval("EmployeeID") %>>
            </asp:LinkButton><br />
            <asp:LinkButton ID="editButton" runat="server" 
                Text=<%# "Edit Employee " + Eval("Name") %> 
                CommandName="EditItemPlease" 
                CommandArgument=<%# Eval("EmployeeID") %>>
            </asp:LinkButton>
        </ItemTemplate>
    </asp:DataList>
</asp:Content>

