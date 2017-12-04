<%@ Page Title="" Language="C#" MasterPageFile="~/Yonetim/Admin.Master" AutoEventWireup="true" CodeBehind="Gorev.aspx.cs" Inherits="Rehber.WebUI.Yonetim.Gorev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Görevler</h3>
    <hr />
    <div class="panel panel-primary">
        <div class="panel-heading">Görev Ekle</div>
        <div class="panel-body">
            <%
                if (Session["GOREVEKLENDI"] != null)
                { %>
            <div class="alert alert-success">
                <% Response.Write(Session["GOREVEKLENDI"]); Session.Clear(); %>
            </div>
            <% } %>
            <div class="form-group">
                <label for="txtGorev">Gorev</label>
                <asp:TextBox ID="txtGorev" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvGorev" runat="server" ErrorMessage="Görev alanı boş geçilemez" ControlToValidate="txtGorev" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Button ID="btnGorevEkle" Text="Ekle" runat="server" CssClass="btn btn-primary pull-right" OnClick="btnGorevEkle_Click" />
            </div>
        </div>
    </div>
    <hr />
    <asp:GridView ID="grvGorevler" runat="server" CssClass="table table-bordered table-hover table-striped" AutoGenerateColumns="False" OnRowDataBound="grvGorevler_RowDataBound" OnRowDeleting="grvGorevler_RowDeleting" DataKeyNames="GorevId">
        <Columns>
            <asp:BoundField DataField="GorevAdi" HeaderText="Görev" />
            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-danger btn-sm"/>
        </Columns>
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
