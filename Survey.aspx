<%@ Page Title="" Language="C#" MasterPageFile="~/UILayer/Master1.Master" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="Research_Project.Survey" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
       
    <link href="UILayer/StyleSheet1.css" rel="stylesheet" />
    <style type="text/css">
          .img {
        Width:120px;
         Height:120px;
        }
        #main {
        
         background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.2;
        }

       .modalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.9;
        }
        .modalPopup
        {
            background-color:#edf1f3;
            border-width: 3px;
            border-style: solid;
            border-color: Black;
            padding: 3px;
            width: 100px;
            height: 100px;
        }
           #Button3 {
           
           vertical-align:central;
           
           }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <div >
      <div >
    <div align="Middle">
        <br />
          <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:Timer ID="Timer1" runat="server">
            </asp:Timer>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <asp:Label ID="lblTimer" ForeColor="Green" Font-Bold="true" Visible="false"  runat="server" Text=""></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
        </div>

    <asp:Button ID="btnTest" runat="server" Text="Start Test" OnClick="btnTest_Click" CssClass="smallButton" />
    <asp:Panel ID="pnlShowHide" runat="server" Visible="false">
     <table >
        <tr>
            <td> Question <asp:Label ID="lblQuestionNo" runat="server" Text=""></asp:Label> of 24</td>
        </tr>
        <tr>
             <td>
                 <asp:Image ID="imgQuestion" runat="server" CssClass="img"  /></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
           <tr><td>Selections Answers</td></tr>
         </table>
        <table>
          
         <tr>
              
            
             <td>
                 <asp:CheckBox ID="chk1" runat="server" /></td>
            <td>
                <asp:Image ID="img1" runat="server" CssClass="img"  /></td><td></td>
              <td>
                 <asp:CheckBox ID="chk2" runat="server" /></td>
            <td>
                <asp:Image ID="img2" runat="server" CssClass="img"  /></td><td></td>
                <td>
                 <asp:CheckBox ID="chk3" runat="server" /></td>
            <td>
                <asp:Image ID="img3" runat="server" CssClass="img" /></td><td></td>
                <td>
                 <asp:CheckBox ID="chk4" runat="server" /></td>
            <td>
                <asp:Image ID="img4" runat="server" CssClass="img" /></td>
        </tr>
        <tr>
             <td></td>
            <td></td>
            <td></td>
            <td></td>
             <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td>

                <asp:Button ID="btnNext" runat="server" Text="Next >>" OnClick="btnNext_Click" CssClass="smallButton" />
            </td>
        </tr>
    </table>
    </asp:Panel>
  

     <%--Model Popup code started--%>
     <br />

        <%--<asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>--%>
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnDummy" PopupControlID="PnlModal" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
    
        <asp:Button ID="btnDummy" runat="server" Text="Edit" Style="display: none;" />
    <%--The below panel will display as your confirm window--%>

   

    <asp:Panel ID="PnlModal" runat="server" Width="500px" CssClass="modalPopup">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Text="Congrats!! you done with the Survey." Font-Bold="true"></asp:Label>
        <asp:Label ID="lblMessage" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="Button3" runat="server" Text="OK" OnClick="Button3_Click" Width="73px" CssClass="smallButton"  />
    </asp:Panel>
          </div>
    </div>
</asp:Content>
