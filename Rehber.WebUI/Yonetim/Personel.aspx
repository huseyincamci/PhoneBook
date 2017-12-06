<%@ Page Title="" Language="C#" MasterPageFile="~/Yonetim/Admin.Master" AutoEventWireup="true" CodeBehind="Personel.aspx.cs" Inherits="Rehber.WebUI.Yonetim.Personel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Personeller</h3>
    <hr />

    <div class="panel panel-primary">
        <div class="panel-heading">Personel Ekle</div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-12">
                    <%if (Session["HATA"] != null)
                        { %>
                    <div class="alert alert-danger"><%Response.Write(Session["HATA"]); %></div>
                    <% Session.Clear();
                        }
                        else if (Session["BASARILI"] != null)
                        { %>
                    <div class="alert alert-success"><%Response.Write(Session["BASARILI"]); %></div>
                    <% Session.Clear();
                        }
                        else if (Session["GUNCEL"] != null)
                        { %>

                    <div class="alert alert-info"><%Response.Write(Session["GUNCEL"]); %></div>
                    <% }
                        Session.Clear();
                    %>
                </div>
            </div>
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
                        <label for="txtTelefon">Telefon</label>
                        <asp:TextBox ID="txtTelefon" runat="server" CssClass="form-control" placeholder="0 (333) 333 33 33"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rqvTelefon" runat="server" ErrorMessage="Telefon alanı boş geçilemez" ControlToValidate="txtTelefon" ForeColor="Red"></asp:RequiredFieldValidator>
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
    <hr />
    <div class="input-group">
        <asp:TextBox ID="txtKisiAra" runat="server" CssClass="form-control"></asp:TextBox>
        <span class="input-group-btn">
            <asp:Button ID="btnKisiAra" runat="server" Text="Kisi Ara" CssClass="btn btn-warning" CausesValidation="False" OnClick="btnKisiAra_Click" />
        </span>
    </div>
    <hr />
    <asp:GridView ID="gvPersoneller" runat="server" CssClass="table table-bordered table-hover table-striped table-responsive" AutoGenerateColumns="False" OnSelectedIndexChanged="gvPersoneller_SelectedIndexChanged" AutoGenerateSelectButton="True" AutoGenerateDeleteButton="True" OnRowDeleting="gvPersoneller_RowDeleting" OnRowDataBound="gvPersoneller_RowDataBound" DataKeyNames="PersonelId" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="gvPersoneller_PageIndexChanging" PageSize="15">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:ImageField DataImageUrlField="Resim" HeaderText="Fotoğraf" ControlStyle-Width="150" ControlStyle-Height="150">
                <ControlStyle Height="100px" Width="100px"></ControlStyle>
            </asp:ImageField>
            <asp:BoundField DataField="UnvanAdi" HeaderText="Unvan" />
            <asp:BoundField DataField="Ad" HeaderText="Ad" />
            <asp:BoundField DataField="Soyad" HeaderText="Soyad" />
            <asp:BoundField DataField="Telefon" HeaderText="Telefon" />
            <asp:BoundField DataField="Eposta" HeaderText="Eposta" />
            <asp:BoundField DataField="BirimAdi" HeaderText="Birim" />
        </Columns>
        <EditRowStyle BackColor="#7C6F57" />
        <EmptyDataTemplate>
            Hiç bir kayıt bulunamadı.
        </EmptyDataTemplate>
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
    </asp:GridView>

    <script type="text/javascript">
        function ConfirmOnDelete(item) {
            if (confirm("Kaydı silmek istiyor musunuz ?") === true)
                return true;
            else
                return false;
        }
    </script>
</asp:Content>
