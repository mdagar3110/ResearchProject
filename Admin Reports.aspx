<%@ Page Title="" Language="C#" MasterPageFile="~/UILayer/Master1.Master" AutoEventWireup="true" CodeBehind="Admin Reports.aspx.cs" Inherits="Research_Project.Admin_Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    
     <div >
    <asp:Button ID="btnHome" runat="server" Text="Home" OnClick="btnHome_Click" CssClass="smallButton" />
         <br /><br /><br />
         <asp:Button ID="btnExcelDownload" runat="server" Text="Export Excel" OnClick="btnExcelDownload_Click" CssClass="smallButton" />
         <br /><br />
         <div style="width: 100%; height: 400px; overflow: scroll">
         <asp:GridView ID="gvAdminReports" runat="server" AllowPaging="True" BackColor="#DEBA84" BorderColor="#DEBA84" BorderWidth="1px" CellPadding="3" BorderStyle="None" OnPageIndexChanging="gvAdminReports_PageIndexChanging" CellSpacing="2">
             <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
             <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
             <PagerSettings PageButtonCount="10" FirstPageText="First" LastPageText="Last"/>  
             <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
             <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
             <SelectedRowStyle BackColor="#738A9C" ForeColor="White" Font-Bold="True" />
             <SortedAscendingCellStyle BackColor="#FFF1D4" />
             <SortedAscendingHeaderStyle BackColor="#B95C30" />
             <SortedDescendingCellStyle BackColor="#F1E5CE" />
             <SortedDescendingHeaderStyle BackColor="#93451F" />
         </asp:GridView>
             </div>
         <br />


         </div>
</asp:Content>
