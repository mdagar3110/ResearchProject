<%@ Page Title="" Language="C#" MasterPageFile="~/UILayer/Master1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Research_Project.UILayer.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <div style="font-family:Arial">
    
        <asp:Button ID="btnUploadImages" runat="server" OnClick="btnUploadImages_Click" Text="Upload New Set Of Images" CssClass="button" />
    
        <br />
        <br />
    
        <asp:Button ID="btnReport" runat="server" Text="Admin Report" Visible="false" OnClick="btnReport_Click"  CssClass="button"/>    
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;    
    <asp:Button ID="btnMgr" runat="server" Text="Manage Admin Users" Visible="false" OnClick="btnMgr_Click" CssClass="button" />    
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Attempt Survey" OnClick="Button1_Click" CssClass="button" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnResetUserRights" runat="server" Text="Reset User to attempt Survey" OnClick="btnResetUserRights_Click"  CssClass="button"/>
    </div>
</asp:Content>
