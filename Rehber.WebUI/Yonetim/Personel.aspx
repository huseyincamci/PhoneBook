<%@ Page Title="" Language="C#" MasterPageFile="~/Yonetim/Admin.Master" AutoEventWireup="true" CodeBehind="Personel.aspx.cs" Inherits="Rehber.WebUI.Yonetim.Personel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Personeller</h3>
    <hr />

    <div class="panel panel-primary">
        <div class="panel-heading">Personel Ekle</div>
        <div class="panel-body">
            <div class="form-group">
                <label for="txtAd">Ad</label>
                <asp:TextBox ID="txtAd" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvAd" runat="server" ErrorMessage="Ad alanı boş geçilemez" ControlToValidate="txtAd" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            
            <div class="form-group">
                <label for="txtSoyad">Soyad</label>
                <asp:TextBox ID="txtSoyad" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvSoyad" runat="server" ErrorMessage="Soyad alanı boş geçilemez" ControlToValidate="txtAd" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            
            <div class="form-group">
                <asp:Button ID="btnPersonelEkle" runat="server" Text="Ekle" CssClass="btn btn-primary" />
            </div>
        </div>
    </div>

    <hr />

    <asp:GridView ID="gvPersoneller" runat="server" CssClass="table table-bordered table-hover table-striped table-responsive" AutoGenerateColumns="False">
        <Columns>
            <asp:ImageField DataImageUrlField="Resim" HeaderText="Fotoğraf">
            </asp:ImageField>
            <asp:BoundField DataField="Ad" HeaderText="Ad" />
            <asp:BoundField DataField="Soyad" HeaderText="Soyad" />
            <asp:BoundField DataField="Telefon" HeaderText="Telefon" />
            <asp:BoundField DataField="Eposta" HeaderText="Eposta" />
        </Columns>
    </asp:GridView>
</asp:Content>
