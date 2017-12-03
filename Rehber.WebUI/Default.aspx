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
                    icerik.html(
                        "<thead>" +
                        "<tr>" +
                        "<th>" +
                        "Fotoğraf" +
                        "</th>" +
                        "<th>" +
                        "Unvan / Ad Soyad" +
                        "</th>" +
                        "<th>" +
                        "Telefon" +
                        "</th>" +
                        "<th>" +
                        "Eposta/Web" +
                        "</th>" +
                        "<th>" +
                        "Birim" +
                        "</th>" +
                        "</tr>" +
                        "</thead>" + "<tbody>");
                    for (var i = 0; i < data.length; i++) {
                        icerik.append("<tr>" +
                            "<td><img src='" + data[i].Fotograf + "' width='100'/></td>" +
                            "<td>" + data[i].Unvan + ' ' + data[i].Ad + ' ' + data[i].Soyad + "</td>" +
                            "<td>" + data[i].Telefon + "</td>" +
                            "<td>" + data[i].Eposta + "<br/><a href='" +
                            data[i].Web
                            + "' target='_blank'>" +
                            data[i].Web
                            + "</a></td>" +
                            "<td>" + data[i].BirimAdi + "</td>" +
                            "</tr>");
                    }
                    icerik.append("</tbody>");
                },
                beforeSend: function () {
                    icerik.children().remove();
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
        <div class="col-lg-8">
            <div class="form-group">
                <label for="Arama_Input">Ad Soyad Ya da Telefona Göre Arama Yap: </label>
                <input type="text" class="form-control" value="" placeholder="Aranacak kişi" id="Arama_Input" autocomplete="off" />
            </div>
        </div>
        <div class="col-lg-4">
            <label for="drpBirimler">Birime Göre: </label>
            <asp:DropDownList ID="drpBirimler" runat="server" CssClass="form-control border-gray"></asp:DropDownList>
        </div>
    </div>
    <hr />

    <div class="row">
        <div class="col-lg-12">
            <table class="table table-bordered table-hover table-striped" id="icerik"></table>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="text-center">
                <img src="Images/loading.gif" alt="Loading..." id="loading" />
            </div>
        </div>
    </div>
</asp:Content>
