<%@ Page Title="" Language="C#" MasterPageFile="~/Rehber.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Rehber.WebUI.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            input = $("#Arama_Input");
            loading = $("#loading");
            icerik = $("#icerik");

            //hide loading image
            loading.hide();

            ajaxCall(null, null);

            $(input).keyup(function () {
                var deger = $(input).val();
                var birim = $("#drpBirimler").val();

                ajaxCall(deger, birim);
            });

            $("#drpBirimler").change(function () {
                var deger = $("#drpBirimler").val();
                var name = $("#Arama_Input").val();

                ajaxCall(name, deger);
            });
        });


        function ajaxCall(ad, birim) {
            $.ajax({
                url: "PersonelService.asmx/GetPersonels",
                method: "POST",
                data: { ad: ad, birim: birim },
                dataType: "json",
                success: function (data) {
                    loading.hide();
                    var thead = "<thead>";
                    thead += "<tr>";
                    thead += "<th>Fotoğraf</th>";
                    thead += "<th>Unvan/Ad Soyad</th>";
                    thead += "<th>Telefon/Dahili</th>";
                    thead += "<th>Eposta/Web</th>";
                    thead += "<th>Birim</th>";
                    thead += "</tr>";
                    thead += "</thead></tbody>";
                    icerik.html(thead);
                    if (data.length === 0) {
                        icerik.append("<tr>" +
                            "<td colspan='5'>" +
                            "<div class='alert alert-warning' style='background-color: #00BCD4'>" +
                            "<span class='glyphicon glyphicon-info-sign'>" +
                            "</span>" +
                            " Hiç kayıt bulunamadı..." +
                            "</div>" +
                            "</td>" +
                            "</tr>");
                    }

                    if (data) {
                        $.each(data, function (index, value) {
                            var html = "";
                            html = "<tr><td><img src='" + value.Fotograf + "' width='100'/></td>";
                            html += "<td>" + value.Unvan + " " + value.Ad + " " + value.Soyad + "</td>";
                            if (value.Dahili === "")
                                html += "<td>" + value.Telefon + "</td>";
                            else
                                html += "<td>" + value.Telefon + "/Dahili " + value.Dahili + "</td>";
                            html += "<td>" + value.Eposta + "<br/><a href='" + value.Web + "'>" + value.Web + "</a></td>";
                            html += "<td>" + value.BirimAdi + "</td>";
                            html += "</tr>";

                            icerik.append(html);
                        });
                    }
                    icerik.append("</tbody>");
                },
                beforeSend: function () {
                    icerik.empty();
                    loading.show();
                }
            });
        }

    </script>

    <style type="text/css">
        #loading {
            width: 117px;
            height: 88px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div class="row">
        <div class="col-sm-8">
            <div class="form-group">
                <label for="Arama_Input">Ad Soyad Ya da Telefona Göre Arama Yap: </label>
                <input type="text" class="form-control" value="" placeholder="Aranacak kişi" id="Arama_Input" autocomplete="off" />
            </div>
        </div>
        <div class="col-sm-4">
            <label for="drpBirimler">Birime Göre: </label>
            <asp:DropDownList ID="drpBirimler" runat="server" CssClass="form-control border-gray"></asp:DropDownList>
        </div>
    </div>
    <hr />

    <div class="row">
        <div class="col-sm-12">
            <div class="table-responsive">
                <table class="table table-bordered table-hover table-striped" id="icerik"></table>
            </div>

        </div>
    </div>

    <div class="row">
        <div class="col-sm-12">
            <div class="text-center">
                <img src="Images/loading.gif" alt="Loading..." id="loading" />
            </div>
        </div>
    </div>
</asp:Content>
