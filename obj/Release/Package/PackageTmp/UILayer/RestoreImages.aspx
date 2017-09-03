<%@ Page Title="" Language="C#" MasterPageFile="~/UILayer/Master1.Master" AutoEventWireup="true" CodeBehind="RestoreImages.aspx.cs" Inherits="Research_Project.UILayer.RestoreImages" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 
        <style type="text/css">
    .modal
    {
        position: fixed;
        top: 0;
        left: 0;
        background-color: black;
        z-index: 99;
        opacity: 0.2;
        filter: alpha(opacity=20);
        -moz-opacity: 0.2;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 10pt;
        border: 5px solid #67CFF5;
        width: 200px;
        height: 100px;
        display: none;
        position: fixed;
        background-color: White;
        z-index: 999;
    }
</style>

   <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:Label ID="lblResult" runat="server" Text="lblResult" ForeColor="#339933"></asp:Label>

    <br />
<br />
    <asp:FileUpload ID="imgUpload" runat="server" AllowMultiple="true" />

    <br />
<br />

    <asp:Button ID="btnSubmit" runat="server" Text="Upload Images" OnClick="btnSubmit_Click" />


    <div class="loading" align="center">
    Loading. Please wait.<br />
    <br />
    <img src="../loader.gif"  alt="Loading"/>
</div>
</asp:Content>
