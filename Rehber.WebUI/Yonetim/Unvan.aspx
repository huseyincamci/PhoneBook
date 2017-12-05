<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostback="true" MasterPageFile="~/Yonetim/Admin.Master" AutoEventWireup="true" CodeBehind="Unvan.aspx.cs" Inherits="Rehber.WebUI.Yonetim.Unvanlar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Unvanlar</h3>
    <hr />
    <div class="panel panel-primary">
        <div class="panel-heading">Unvan Ekle</div>
        <div class="panel-body">
            <%
                if (Session["UNVANEKLENDI"] != null)
                { %>
            <div class="alert alert-success">
                <% Response.Write(Session["UNVANEKLENDI"]); Session.Clear(); %>
            </div>
            <% } %>
            <div class="form-group">
                <label for="txtUnvan">Unvan</label>
                <asp:TextBox ID="txtUnvan" runat="server" CssClass="form-control" ValidationGroup="BirimEkle"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUnvan" runat="server" ErrorMessage="Unvan alanı boş geçilemez" ControlToValidate="txtUnvan" ForeColor="Red" ValidationGroup="BirimEkle"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Button ID="btnUnvanEkle" Text="Ekle" runat="server" CssClass="btn btn-primary pull-right" OnClick="btnUnvanEkle_Click" ValidationGroup="BirimEkle" />
            </div>
        </div>
    </div>
    <hr />
    <asp:GridView ID="gvUnvanlar" runat="server" CssClass="table table-bordered table-hover table-striped" OnRowDeleting="gvUnvanlar_RowDeleting" AutoGenerateColumns="False" DataKeyNames="UnvanId" OnRowCancelingEdit="gvUnvanlar_RowCancelingEdit" OnRowEditing="gvUnvanlar_RowEditing" OnRowUpdating="gvUnvanlar_RowUpdating">
        <Columns>
            <asp:TemplateField HeaderText="Unvan">
                    <EditItemTemplate>
                        <asp:TextBox ID="txtUnvan" CssClass="form-control" runat="server" Text='<%# Eval("UnvanAdi") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUnvan" runat="server" Text='<%# Eval("UnvanAdi") %>'></asp:Label>
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
