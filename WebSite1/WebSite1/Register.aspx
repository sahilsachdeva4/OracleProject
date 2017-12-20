<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Label ID="Label4" runat="server" Text="Name"></asp:Label>
<asp:TextBox ID="txtName" runat="server" ValidationGroup="register"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName" ErrorMessage="RequiredFieldValidator" ValidationGroup="register">Name is required</asp:RequiredFieldValidator>
<br />
<asp:Label ID="Label5" runat="server" Text="Email"></asp:Label>
<asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="register">Enter a valid email address</asp:RegularExpressionValidator>
<br />
<asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
<asp:TextBox ID="txtUsername" runat="server" ValidationGroup="register"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtUsername" ErrorMessage="RequiredFieldValidator" ValidationGroup="register">Username is required</asp:RequiredFieldValidator>
<br />
<asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
<asp:TextBox ID="txtPassword" runat="server" ValidationGroup="register"></asp:TextBox>
<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtPassword" ErrorMessage="RequiredFieldValidator" ValidationGroup="register">Password is required</asp:RequiredFieldValidator>
<br />
<asp:Label ID="Label3" runat="server" Text="Confirm Password"></asp:Label>
<asp:TextBox ID="txtPassword2" runat="server" ValidationGroup="register"></asp:TextBox>
<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtPassword2" ErrorMessage="CompareValidator" ValidationGroup="register">Passwords do not match</asp:CompareValidator>
<br />
<asp:Label ID="Label6" runat="server" Text="Department"></asp:Label>
<asp:DropDownList ID="ddlDepartments" runat="server" DataSourceID="SqlDataSource1" DataTextField="DEPARTMENT" DataValueField="DEPARTMENTID">
</asp:DropDownList>
<asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlDepartments" ErrorMessage="Department is required" ValidationGroup="register"></asp:RequiredFieldValidator>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Oracle1ConnectionString %>" ProviderName="<%$ ConnectionStrings:Oracle1ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;DEPARTMENTID&quot;, &quot;DEPARTMENT&quot; FROM &quot;DN_DEPARTMENTS&quot;"></asp:SqlDataSource>
<br />
<br />
<asp:Button ID="btnRegister" runat="server" OnClick="btnRegister_Click" Text="Register" ValidationGroup="register" />
<br />
<br />
<asp:Label ID="lblStatus" runat="server"></asp:Label>
<br />
<br />
</asp:Content>

