<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Yonetim/Admin.Master" AutoEventWireup="true" CodeBehind="Birim.aspx.cs" Inherits="Rehber.WebUI.Yonetim.Birim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Birimler</h3>
    <hr />
    <div class="row">
        <div class="col-md-12">
            <%
                if (Session["BIRIMEKLENDI"] != null)
                { %>
            <div class="alert alert-success">
                <span class="glyphicon glyphicon-ok-sign"></span>
                <% Response.Write(Session["BIRIMEKLENDI"]); Session.Clear(); %>
            </div>
            <% } %>
            <%
                if (Session["BIRIMSILINDI"] != null)
                { %>
            <div class="alert alert-success">
                <span class="glyphicon glyphicon-ok-sign"></span>
                <% Response.Write(Session["BIRIMSILINDI"]); Session.Clear(); %>
            </div>
            <% } %>
        </div>
    </div>

    <div class="panel panel-primary">
        <div class="panel-heading">
            Birim Ekle
        </div>
        <div class="panel-body">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="txtBirim">Birim</label>
                        <asp:TextBox ID="txtBirim" runat="server" CssClass="form-control" ValidationGroup="BirimEkle"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvBirim" runat="server" ErrorMessage="Birim alanı boş geçilemez" ControlToValidate="txtBirim" ForeColor="Red" ValidationGroup="BirimEkle"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <asp:Button ID="btnBirimEkle" runat="server" Text="Ekle" CssClass="btn btn-primary pull-right" OnClick="btnBirimEkle_Click" ValidationGroup="BirimEkle" />
            </div>
        </div>
    </div>
    <hr />
    <asp:GridView ID="gvBirimler" runat="server" CssClass="table table-bordered table-hover table-striped" OnRowDeleting="gvBirimler_RowDeleting" AutoGenerateColumns="False" DataKeyNames="BirimId" OnRowCancelingEdit="gvBirimler_RowCancelingEdit" OnRowEditing="gvBirimler_RowEditing" OnRowUpdating="gvBirimler_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="Birim">
                <EditItemTemplate>
                    <asp:TextBox ID="txtBirim" CssClass="form-control" runat="server" Text='<%# Eval("BirimAdi") %>'></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="lblBirim" runat="server" Text='<%# Eval("BirimAdi") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <EditItemTemplate>
                    <asp:Button ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-warning" Text="Güncelleştir" />
                    <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" CssClass="btn btn-info" Text="İptal" />
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Button ID="btnEdit" runat="server" CommandName="Edit" CssClass="btn btn-primary" Text="Düzenle" />
                    <asp:Button ID="btnDelete" runat="server" CommandName="Delete" CssClass="btn btn-danger" Text="Sil" OnClientClick="return ConfirmOnDelete();" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EmptyDataTemplate>
            Hiç bir kayıt bulunamadı.
        </EmptyDataTemplate>
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
