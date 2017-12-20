<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HelpDesk.aspx.cs" Inherits="HelpDesk" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Employee Helpdesk Request</h1>
    <p>
        <asp:Label ID="dbErrorMessage" runat="server" Text=""></asp:Label>
    </p>
    <p>Station Number<br />
        <asp:TextBox ID="StationTextBox" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ControlToValidate="StationTextBox" ID="stationRequiredValidator" runat="server" ErrorMessage="<br />Station number is mandatory" Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:CompareValidator ID="stationNumberCheck" runat="server" ErrorMessage="The value must be a number" ControlToValidate="StationTextBox" Display="Dynamic" ForeColor="Red" Operator="DataTypeCheck" Type="Integer"></asp:CompareValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Number must be between 1 and 50" ControlToValidate="StationTextBox" Display="Dynamic" ForeColor="Red" MaximumValue="50" MinimumValue="1" Type="Integer"></asp:RangeValidator>
    </p>
    <p>Problem Category<br />
        <asp:DropDownList ID="CategoryList" runat="server">
            
        </asp:DropDownList>
    </p>
    <p>Problem Subject<br />
        <asp:DropDownList ID="SubjectList" runat="server">

        </asp:DropDownList>
    </p>
    <p>Problem Description:<br />
        <asp:TextBox ID="descriptionTextBox" runat="server" Rows="4" TextMode="MultiLine" Columns="40"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<br />You must enter a description." Display="Dynamic" ForeColor="Red" ControlToValidate="descriptionTextBox"></asp:RequiredFieldValidator>
    </p>
    <p>
        <asp:Button ID="submitButton" runat="server" Text="Submit Request" OnClick="submitButton_Click" />
    </p>
</asp:Content>