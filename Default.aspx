<%@ Page Async="true" Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="AFEXChile._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <script>

        $(function () {
            var idDeleteVideo = -1;

            $(".btn-delete-video").click(function (e) {
                e.stopPropagation();
                idDeleteVideo = $(this).attr('data-id-video');
                $('#modalConfirmDelete').modal();
            });
            $(".btn-show-video").click(function () {
                $('#modalShowVideo').modal('show');
                idDeleteVideo = $(this).attr('data-id-video');
                $('#video-preview-thumbnail').css('background-image', "url('" + $(this).attr('data-url-thumbnail') + "')");
                $('#video-preview-thumbnail').attr('href','/Video.aspx?Id='+ $(this).attr('data-url-video'));
                $('#video-preview-title').text($(this).attr('data-title'));
                $('#video-preview-description').text($(this).attr('data-description'));
            });
            $("#btnCancelDeleteVideo").click(function () {
                $('#modalConfirmDelete').modal('hide');
            });
            $("#btnConfirmDeleteVideo").click(function () {
                $.ajax({
                    type: "POST",
                    url: "Default.aspx/DeleteVideo",
                    data: "{idVideo: " + idDeleteVideo + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $('#' + idDeleteVideo + 'Image').remove();
                        $('#modalConfirmDelete').modal('hide');
                    },
                    failure: function (response) {
                    },
                    error: function (response) {

                    }
                });
            });
        });
    </script>
    <div class="row mt-5">
        <div class="col-md">
        </div>
        <div class="col-md-8">
            <h2>Añadir nuevo video</h2>
            <div class="input-group input-group-lg mb-3">
                <asp:TextBox ID="txtSearch" placeholder="Añadir" runat="server" CssClass="form-control"></asp:TextBox>
                <div class="input-group-append">
                    <asp:Button ID="btnSearch" runat="server" Text="Añadir" CssClass="btn btn-primary btn-lg btn-search " OnClick="btnSearch_Click" />
                </div>
            </div>
        </div>
        <div class="col-md">
        </div>
    </div>
    <div class="row mt-5">
        <div class="col-md">
        </div>
        <asp:Panel ID="panelVideos" CssClass="col-md-8 container-videos mb-5" runat="server"></asp:Panel>
        <div class="col-md">
        </div>
    </div>

    <div id="modalConfirmDelete" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-body pl-5 pr-5 pb-5 mt-2">
                    <div class="row">
                        <div class="col-md">
                            <button type="button" class="close btn-close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </div>
                    <div class="row mt-2">
                        <div class="col-md-12">
                            <h3>¿Segúro que quieres eliminar este video?</h3>
                        </div>
                    </div>
                    <div class="row mt-5">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">
                            <button type="button" class="btn btn-lg btn-block btn-light" id="btnCancelDeleteVideo">
                                Cancelar
                            </button>
                        </div>
                        <div class="col-md-4">
                            <button type="button" class="btn btn-lg btn-block btn-primary" id="btnConfirmDeleteVideo">
                                Eliminar
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div id="modalShowVideo" class="modal" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-dialog-centered  modal-xl" role="document">
            <div class="modal-content">
                <div class="modal-body pl-5 pr-5 pb-5 mt-3">
                    <div class="row">
                        <div class="col-md">
                            <button type="button" class="close btn-close" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    </div>
                    <div class="row mt-5 mb-4">
                        <div class="col-md">
                            <div id="video-preview-container">
                                <a id="video-preview-thumbnail" class="video-item">
                                    <div id="video-preview-button">
                                    </div>
                                </a>
                                <div id="video-preview-info">
                                    <h5 id="video-preview-title"></h5>
                                    <p id="video-preview-description"></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
