<%@ Page Title="" Language="C#" MasterPageFile="~/UILayer/Master1.Master" AutoEventWireup="true" CodeBehind="ManageUsers.aspx.cs" Inherits="Research_Project.ManageUsers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server" >
    <asp:Button ID="btnHome" runat="server" OnClick="btnHome_Click" Text="Home" CssClass="smallButton"/>
   <br />
    <div style="display: inline-table;  border: 1px solid black; margin: 10px;padding: 20px;">
    <asp:Label ID="Label1" runat="server" Text="Enter EmailAddress to Reset User To Attempt Survey Again"></asp:Label>

<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>&nbsp;&nbsp;
     <asp:Button ID="Button1" runat="server" Text="ResetUser" OnClick="Button1_Click"  CssClass="smallButton"/>
        <br />
        <asp:Label ID="lblMessageReset" runat="server" ForeColor="Red"></asp:Label>
        <br />
    </div>


    <div style="display: inline-table;  border: 1px solid black; margin: 10px; padding: 20px;">
    
    <asp:Label ID="lblEmail" runat="server" Text="Enter Email Address"></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnSubmit" runat="server" Text="Add User" OnClick="btnSubmit_Click" CssClass="smallButton" /><br />
    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
      
    </div>
    
    <asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="False" DataKeyNames="UserName"
        AutoGenerateEditButton="True"  OnRowCancelingEdit="gvUsers_RowCancelingEdit"  OnRowEditing="gvUsers_RowEditing" OnRowUpdating="gvUsers_RowUpdating" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" CellSpacing="2">
        <Columns>
           

            <asp:BoundField DataField="UserName" HeaderText="User Name" ReadOnly="True" SortExpression="UserName" />
            <asp:BoundField DataField="Email" HeaderText="Email Id" ReadOnly="True" SortExpression="Email" />
            <asp:TemplateField HeaderText="Is Admin">
                <ItemTemplate>
                    <asp:CheckBox ID="chkAdmin" runat="server" Enabled="false" Checked='<%# Eval("IsAdmin") %>' />
                </ItemTemplate>
                <EditItemTemplate>
                     <asp:RadioButtonList ID="rdbAdmin"  runat="server" Checked='<%# Eval("IsAdmin") %>'
            RepeatDirection="Horizontal">
            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
            <asp:ListItem Text="No" Value="0"></asp:ListItem>
        </asp:RadioButtonList>
                </EditItemTemplate>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Is Super Admin">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSuperAdmin" runat="server" Enabled="false" Checked='<%# Eval("ISSuperAdmin") %>'/>
                </ItemTemplate>
                <EditItemTemplate>
                <asp:RadioButtonList ID="rdbSuperAdmin" runat="server" Checked='<%# Eval("ISSuperAdmin") %>' 
            RepeatDirection="Horizontal">
            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
            <asp:ListItem Text="No" Value="0"></asp:ListItem>
        </asp:RadioButtonList>
                </EditItemTemplate>
            </asp:TemplateField>

        </Columns>
        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
    </asp:GridView>
</asp:Content>
