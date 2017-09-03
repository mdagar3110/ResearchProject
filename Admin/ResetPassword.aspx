<%@ Page Title="" Language="C#" MasterPageFile="~/UILayer/Master1.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Research_Project.Admin.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="font-family:Arial">
    <table style="border: 1px solid black; width:300px">
        <tr>
            <td colspan="2">
                <b>Reset my password</b>
            </td>
        </tr>
        <tr>
            <td>
                User Name
            </td>    
            <td>
                <asp:TextBox ID="txtUserName" Width="150px" runat="server">
                </asp:TextBox>
            </td>    
        </tr>
        <tr>
            <td>
                    
            </td>    
            <td>
                <asp:Button ID="btnResetPassword" runat="server" CssClass="smallButton" 
                Width="150px" Text="Reset Password" onclick="btnResetPassword_Click" />
            </td>    
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server"></asp:Label>
            </td>    
            <td>
                <a href="../Login.aspx">Login</a>
            </td>
        </tr>
    </table>
</div>
</asp:Content>
