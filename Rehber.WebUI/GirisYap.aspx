<%@ Page Title="" Language="C#" MasterPageFile="~/Rehber.Master" AutoEventWireup="true" CodeBehind="GirisYap.aspx.cs" Inherits="Rehber.WebUI.GirisYap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-md-6">
            <div class="panel panel-info">
                <div class="panel-heading">
                    Yönetici Girişi
                </div>
                <div class="panel-body">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <label for="txtKullaniciAdi" class="col-sm-3 control-label">Kullanıcı Adı</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtKullaniciAdi" runat="server" ValidationGroup="KullaniciAdiSifre" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqvKullaniciAdi" runat="server" ErrorMessage="Kullanıcı Adı boş geçilemez" ForeColor="Red" ValidationGroup="KullaniciAdiSifre" ControlToValidate="txtKullaniciAdi"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="txtSifre" class="col-sm-3 control-label">Şifre</label>
                            <div class="col-sm-9">
                                <asp:TextBox ID="txtSifre" runat="server" ValidationGroup="KullaniciAdiSifre" TextMode="Password" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqvSifre" runat="server" ErrorMessage="Şifre boş geçilemez" ForeColor="Red" ValidationGroup="KullaniciAdiSifre" ControlToValidate="txtSifre"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-9">
                                <div class="checkbox">
                                    <label>
                                        <asp:CheckBox ID="cbBeniHatirla" runat="server" />
                                        Beni hatırla
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="col-sm-offset-3 col-sm-9">
                                <asp:Button ID="btnGirisYap" runat="server" Text="Giriş Yap" CssClass="btn btn-warning" Style="left: 0px; top: 0px" ValidationGroup="KullaniciAdiSifre" OnClick="btnGirisYap_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
