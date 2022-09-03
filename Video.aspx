<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Video.aspx.cs" Inherits="AFEXChile.Video" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-md"></div>
        <div class="col-md-9">
            <div class="row">
                <div class="col-md-12">
                    <div class="row mt-5">
                        <div class="col-md-2 mt-5">
                            <asp:LinkButton CssClass="btn btn-lg btn-block btn-light" PostBackUrl="~/Default.aspx" runat="server">
                    <i class="fa fa-rotate-left"></i>&nbsp;Volver
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="col-md-9 mt-5">
                    <div id="video" runat="server" class="video-player-container"></div>
                </div>
                <div class="col-md"></div>
            </div>
        </div>
        <div class="col-md"></div>
    </div>
</asp:Content>
