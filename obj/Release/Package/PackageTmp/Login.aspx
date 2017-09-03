<%@ Page Title="" Language="C#" MasterPageFile="~/UILayer/Master1.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Research_Project.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="font-family:Arial">
<table style="border: 1px solid black">
    <tr>
        <td colspan="2">
            <b>Login</b>
        </td>
    </tr>
    <tr>
        <td>
            User Name
        </td>    
        <td>
            :<asp:TextBox ID="txtUserName" runat="server">
            </asp:TextBox>
        </td>    
    </tr>
    <tr>
        <td>
            Password
        </td>    
        <td>
            :<asp:TextBox ID="txtPassword" TextMode="Password" runat="server">
            </asp:TextBox>
        </td>    
    </tr>
    <tr>
        <td>
            <asp:CheckBox ID="chkBoxRememberMe" runat="server" Text="Remember Me" /><br />         
        </td>    
        <td>
            <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" />
        </td>    
    </tr>
    <tr>
        <td><asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label></td><td></td>
    </tr>
</table>
<br />
<a href="Admin/Registration.aspx">Click here to register</a> 
if you do not have a user name and password.
</div>
</asp:Content>
