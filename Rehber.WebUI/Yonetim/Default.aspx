<%@ Page Title="" Language="C#" MasterPageFile="~/Yonetim/Admin.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Rehber.WebUI.Yonetim.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Anasayfa</h3>
    <hr />
    <div class="row">
        <div class="col-md-4">
            <div class="panel panel-success">
                <div class="panel-heading">
                    Toplam Personel
                </div>
                <div class="panel-body text-center">
                    <span class="glyphicon glyphicon-user" style="font-size: 5em"></span>
                    <asp:Label ID="lblPersonel" runat="server" Text="" Font-Size="5em"></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    Toplam Birim
                </div>
                <div class="panel-body text-center">
                    <span class="glyphicon glyphicon-th" style="font-size: 5em"></span>
                    <asp:Label ID="lblBirim" runat="server" Text="" Font-Size="5em"></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-md-4">
            <div class="panel panel-warning">
                <div class="panel-heading">
                    Toplam Unvan
                </div>
                <div class="panel-body text-center">
                    <span class="glyphicon glyphicon-signal" style="font-size: 5em"></span>
                    <asp:Label ID="lblUnvan" runat="server" Text="" Font-Size="5em"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
