<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="KayitListele.ascx.cs" Inherits="Rehber.WebUI.KayitListele" %>
<asp:GridView ID="Bulunan_Kayitlar" runat="server" CssClass="table table-bordered table-hover table-condensed" AllowPaging="True" CellPadding="10" GridLines="Horizontal" OnPageIndexChanging="Bulunan_Kayitlar_PageIndexChanging" PageSize="20" AutoGenerateColumns="False" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="0px">
    <AlternatingRowStyle BackColor="#F7F7F7" />
    <Columns>
        <asp:ImageField DataImageUrlField="Resim" HeaderText="Fotoğraf">
        </asp:ImageField>
        <asp:BoundField DataField="Ad" HeaderText="Ad" />
        <asp:BoundField DataField="Soyad" HeaderText="Soyad" />
        <asp:BoundField DataField="Telefon" HeaderText="Telefon" />
    </Columns>
    <EmptyDataTemplate>
        Hiç bir kayıt bulunamadı...
    </EmptyDataTemplate>
    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
    <SortedAscendingCellStyle BackColor="#F4F4FD" />
    <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
    <SortedDescendingCellStyle BackColor="#D8D8F0" />
    <SortedDescendingHeaderStyle BackColor="#3E3277" />
</asp:GridView>