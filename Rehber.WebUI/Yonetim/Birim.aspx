﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Yonetim/Admin.Master" AutoEventWireup="true" CodeBehind="Birim.aspx.cs" Inherits="Rehber.WebUI.Yonetim.Birim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h3>Birimler</h3>
    <hr />
    <div class="panel panel-primary">
        <div class="panel-heading">
            Birim Ekle
        </div>
        <div class="panel-body">
            <%
                if (Session["BIRIMEKLENDI"] != null)
                { %>
            <div class="alert alert-success">
                <% Response.Write(Session["BIRIMEKLENDI"]); Session.Clear(); %>
            </div>
            <% } %>
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">
                        <label for="txtBirim">Birim</label>
                        <asp:TextBox ID="txtBirim" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvBirim" runat="server" ErrorMessage="Birim alanı boş geçilemez" ControlToValidate="txtBirim" ForeColor="Red"></asp:RequiredFieldValidator>
                    </div>
                </div>
            </div>

            <div class="form-group">
                <asp:Button ID="btnBirimEkle" runat="server" Text="Ekle" CssClass="btn btn-primary pull-right" OnClick="btnBirimEkle_Click" />
            </div>
        </div>
    </div>
    <hr />
    <asp:GridView ID="gvBirimler" runat="server" CssClass="table table-bordered table-hover table-striped"></asp:GridView>
</asp:Content>
