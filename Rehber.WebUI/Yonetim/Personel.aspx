﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Yonetim/Admin.Master" AutoEventWireup="true" CodeBehind="Personel.aspx.cs" Inherits="Rehber.WebUI.Yonetim.Personel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.10/jquery.mask.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Personeller</h3>
    <hr />


    <div class="row">
        <div class="col-md-12">
            <%
                if (Session["PERSONELSILINDI"] != null)
                { %>
            <div class="alert alert-success">
                <span class="glyphicon glyphicon-ok-sign"></span>
                <%Response.Write(Session["PERSONELSILINDI"]); %>
            </div>
            <% }
                Session.Clear(); %>

            <%if (Session["HATA"] != null)
                { %>
            <div class="alert alert-danger">
                <span class="glyphicon glyphicon-exclamation-sign"></span>
                <%Response.Write(Session["HATA"]); %>
            </div>
            <% Session.Clear();
                }
                if (Session["BASARILI"] != null)
                { %>
            <div class="alert alert-success">

                <%Response.Write(Session["BASARILI"]); %>
            </div>
            <% Session.Clear();
                }
                if (Session["GUNCEL"] != null)
                { %>

            <div class="alert alert-success">
                <span class="glyphicon glyphicon-ok-sign"></span>
                <%Response.Write(Session["GUNCEL"]); %>
            </div>
            <% }
                Session.Clear();
            %>

            <%
                if (Session["GUNCEL"] != null)
                { %>

            <div class="alert alert-success">
                <span class="glyphicon glyphicon-ok-sign"></span>
                <%Response.Write(Session["GUNCEL"]); %>
            </div>
            <% }
                Session.Clear();
            %>
        </div>
    </div>
    <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
        <div class="panel panel-primary">
            <div class="panel-heading" role="tab" id="headingOne">
                <h4 class="panel-title">
                    <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                        <asp:Label ID="lblPersonelEkleDuzenle" runat="server" Text="Yeni Personel Ekle"></asp:Label>
                        <span class="glyphicon glyphicon-triangle-bottom"></span>
                    </a>
                </h4>
            </div>
            <div id="collapseOne" class="panel-collapse collapse" role="tabpanel" aria-labelledby="headingOne">
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtSicil">Sicil No</label>
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="drpSicilEk" runat="server" CssClass="form-control border-gray">
                                                <asp:ListItem>a</asp:ListItem>
                                                <asp:ListItem>b</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>

                                            <asp:TextBox ID="txtSicil" runat="server" CssClass="form-control"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Sicil No gereklidir." ControlToValidate="txtSicil" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Sadece sayı girilebilir" ControlToValidate="txtSicil" ForeColor="Red" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <asp:HiddenField ID="hfPersonelId" runat="server" />
                            <asp:HiddenField ID="hfFileName" runat="server" />
                            <div class="form-group">
                                <label for="txtAd">Ad</label>
                                <asp:TextBox ID="txtAd" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAd" runat="server" ErrorMessage="Ad alanı boş geçilemez" ControlToValidate="txtAd" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtSoyad">Soyad</label>
                                <asp:TextBox ID="txtSoyad" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvSoyad" runat="server" ErrorMessage="Soyad alanı boş geçilemez" ControlToValidate="txtSoyad" ForeColor="Red"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtTelefon">Telefon/Dahili</label>
                                <table style="width: 100%">
                                    <tr>
                                        <td style="width: 85%">
                                            <asp:TextBox ID="txtTelefon" runat="server" CssClass="form-control" placeholder="0 (999) 999 99 99"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDahili" runat="server" CssClass="form-control" placeholder="Dahili"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:RequiredFieldValidator ID="rqvTelefon" runat="server" ErrorMessage="Telefon alanı boş geçilemez" ControlToValidate="txtTelefon" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtEposta">Eposta</label>
                                <asp:TextBox ID="txtEposta" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqvEposta" runat="server" ErrorMessage="Eposta alanı boş geçilemez" ControlToValidate="txtEposta" ForeColor="Red"></asp:RequiredFieldValidator><br />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEposta" ErrorMessage="Geçerli bir mail adresi giriniz." ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtWeb">Web</label>
                                <asp:TextBox ID="txtWeb" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtWeb" ErrorMessage="Geçerli bir web adresi giriniz. (http://ornek.com)" ForeColor="Red" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="drpUnvan">Unvan</label>
                                <asp:DropDownList ID="drpUnvan" runat="server" CssClass="form-control border-gray"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqrUnvan" runat="server" ErrorMessage="Lütfen unvan seçin" ControlToValidate="drpUnvan" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="drpBirim">Birim</label>
                                <asp:DropDownList ID="drpBirim" runat="server" CssClass="form-control border-gray"></asp:DropDownList>
                                <asp:RequiredFieldValidator ID="rqrBirimGerekli" runat="server" ErrorMessage="Lütfen birim seçin" ControlToValidate="drpBirim" ForeColor="Red" InitialValue="0"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="fuFotograf">Fotoğraf</label>
                                <span class="badge">(150X150 px)</span>
                                <asp:FileUpload ID="fuFotograf" runat="server" CssClass="form-control" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="İzin verilen dosya uzantıları: jpg, png" ValidationExpression="([a-zA-Z0-9\s_\\.\-:])+(.png|.jpg|.gif)$" ControlToValidate="fuFotograf" ForeColor="Red"></asp:RegularExpressionValidator><br />
                                <asp:Label ID="lblMaxBoyut" runat="server" Text="" ForeColor="red"></asp:Label>
                            </div>
                        </div>
                    </div>

                    <div class="btn-group pull-right">
                        <asp:Button ID="btnPersonelIptal" runat="server" Text="İptal" CssClass="btn btn-info pull-right" Enabled="False" OnClick="btnPersonelIptal_Click" />
                        <asp:Button ID="btnPersonelDuzenle" runat="server" Text="Düzenle" ToolTip="Düzenlemek istediğiniz kaydı tablodan seçin" CssClass="btn btn-warning pull-right" OnClick="btnPersonelDuzenle_Click" Enabled="False" />
                        <asp:Button ID="btnPersonelEkle" runat="server" Text="Ekle" CssClass="btn btn-primary pull-right" OnClick="btnPersonelEkle_Click" />
                    </div>
                </div>
            </div>
        </div>
    </div>
    <hr />
    <div class="input-group">
        <asp:TextBox ID="txtKisiAra" runat="server" CssClass="form-control"></asp:TextBox>
        <span class="input-group-btn">
            <asp:Button ID="btnKisiAra" runat="server" Text="Kisi Ara" CssClass="btn btn-warning" CausesValidation="False" OnClick="btnKisiAra_Click" />
        </span>
    </div>
    <hr />
    <div class="table-responsive">
        <asp:GridView ID="gvPersoneller" runat="server" CssClass="table table-bordered table-hover table-striped table-responsive" AutoGenerateColumns="False" OnSelectedIndexChanged="gvPersoneller_SelectedIndexChanged" AutoGenerateSelectButton="True" AutoGenerateDeleteButton="True" OnRowDeleting="gvPersoneller_RowDeleting" OnRowDataBound="gvPersoneller_RowDataBound" DataKeyNames="PersonelId" AllowPaging="True" OnPageIndexChanging="gvPersoneller_PageIndexChanging" PageSize="50">
            <Columns>
                <asp:ImageField DataImageUrlField="Resim" HeaderText="Fotoğraf" ControlStyle-Width="150" ControlStyle-Height="150">
                    <ControlStyle Height="100px" Width="100px"></ControlStyle>
                </asp:ImageField>
                <asp:BoundField DataField="UnvanAdi" HeaderText="Unvan" />
                <asp:BoundField DataField="Ad" HeaderText="Ad" />
                <asp:BoundField DataField="Soyad" HeaderText="Soyad" />
                <asp:BoundField DataField="Telefon" HeaderText="Telefon" />
                <asp:BoundField DataField="Dahili" HeaderText="Dahili" />
                <asp:BoundField DataField="Eposta" HeaderText="Eposta" />
                <asp:BoundField DataField="Web" HeaderText="Web" />
                <asp:BoundField DataField="BirimAdi" HeaderText="Birim" />
            </Columns>
            <EmptyDataTemplate>
                Hiç bir kayıt bulunamadı.
            </EmptyDataTemplate>
            <PagerStyle CssClass="sayfalama" />
        </asp:GridView>
    </div>

    <script type="text/javascript">
        function ConfirmOnDelete(item) {
            if (confirm("Kaydı silmek istiyor musunuz ?") === true)
                return true;
            else
                return false;
        }

        $(function () {
            $('#<%=txtTelefon.ClientID %>').focus(function () {
                $("#<%=txtTelefon.ClientID %>").mask("0 (999) 999 99 99");
            });

            $('#<%=txtDahili.ClientID %>').focus(function () {
                $("#<%=txtDahili.ClientID %>").mask("999");
            });
        });
    </script>
</asp:Content>
