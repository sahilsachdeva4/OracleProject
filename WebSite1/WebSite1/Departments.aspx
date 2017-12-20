<%@ Page Title="Dorknozzle Departments" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Departments.aspx.cs" Inherits="Departments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Departments</h1>
    <asp:Panel ID="deptPane" runat="server"></asp:Panel>
    <asp:Label ID="label" runat="server" Text="Department Name"></asp:Label>
    <asp:TextBox ID="deptName" runat="server"></asp:TextBox>
    <asp:Button ID="AddButton" runat="server" Text="Add" OnClick="AddButton_Click" />
    <asp:Button ID="CancelButton" runat="server" Text="Cancel" OnClick="CancelButton_Click" />
    <asp:GridView ID="departmentsGrid" AllowSorting="true" AllowPaging="true" PageSize="5" runat="server" OnPageIndexChanging="departmentsGrid_PageIndexChanging" OnSorting="departmentsGrid_Sorting"></asp:GridView>
</asp:Content>