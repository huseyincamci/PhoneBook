<%@ Page Title="" Language="C#" MasterPageFile="~/Yonetim/Admin.Master" AutoEventWireup="true" CodeBehind="Unvan.aspx.cs" Inherits="Rehber.WebUI.Yonetim.Unvanlar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Unvanlar</h3>
    <hr />
    <div class="panel panel-default">
        <div class="panel-heading">Unvan Ekle</div>
        <div class="panel-body">
            <div class="form-group">
                <label for="txtUnvan">Unvan</label>
                <asp:TextBox ID="txtUnvan" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvUnvan" runat="server" ErrorMessage="Unvan alanı boş geçilemez" ControlToValidate="txtUnvan" ForeColor="Red"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
                <asp:Button ID="btnUnvanEkle" Text="Ekle" runat="server" CssClass="btn btn-primary" OnClick="btnUnvanEkle_Click" />
            </div>
        </div>
    </div>
    <hr />
    <asp:GridView ID="gvUnvanlar" runat="server" CssClass="table table-bordered table-hover table-striped"></asp:GridView>
</asp:Content>
