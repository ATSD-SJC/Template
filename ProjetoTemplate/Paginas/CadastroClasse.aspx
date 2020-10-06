<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroClasse.aspx.cs" Inherits="ProjetoTemplate.Generica.CadastroClasse" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/mascara.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(window).on('load', function (event) {
            $('#preloader1').delay(0).fadeOut('slow', function () {
            });
        });
        $(function () {

            $(".btn-preloader").on('click', function (event) {
                $('#preloader1').delay(0).fadeIn('fast', function () {
                });
                $("#genCortina").fadeIn('fast');
            });
        });

    </script>
    <div class="card card-body">
        <div class="page-header">
            <h1>Cadastro <small>de Classe</small></h1>
        </div>
        <div class="form-group row"></div>
        <div class="card card-default">
            <div class="card-header">
                <div class="row">
                    <div class="col-1">
                        <img src="../imagens/logo_atsd.png" style="width:50px;" class="img-fluid" />
                    </div>
                    <div class="col-10 text-center">
                        CADASTRO
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <span class="col-4">Texto *:</span>
                    <div class="col-8">
                        <asp:TextBox ID="txtTexto" runat="server" CssClass="form-control text-uppercase" placeholder="Insira aqui o texto" ></asp:TextBox>
                        <small class="busca">Esse campo é obrigatório!</small>
                    </div>
                </div>
                <div class="form-group row">
                    <span class="col-4">Data:</span>
                    <div class="col-8">
                        <asp:TextBox ID="txtData" runat="server" CssClass="form-control" placeholder="DD/MM/AAAA" onkeyup="formataData(this,event);" MaxLength="10" ></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <span class="col-4">Valor *:</span>
                    <div class="col-8">
                        <asp:TextBox ID="txtValor" runat="server" CssClass="form-control" placeholder="0,00" onkeyup="formataValor(this,event);"></asp:TextBox>
                        <small class="busca">Esse campo é obrigatório!</small>
                    </div>
                </div>
                <div class="form-group row">
                    <span class="col-4">Booleano *:</span>
                    <div class="col-8">
                        <asp:DropDownList ID="ddlBooleano" runat="server" CssClass="form-control">
                            <asp:ListItem Value="SELECIONE">SELECIONE</asp:ListItem>
                            <asp:ListItem Value="1">SIM</asp:ListItem>
                            <asp:ListItem Value="2">NÃO</asp:ListItem>
                        </asp:DropDownList>
                        <small class="busca">Esse campo é obrigatório!</small>
                    </div>
                </div>
                <div class="form-group row">
                    <div class="col text-center">
                        <asp:LinkButton ID="btnCadastrar" runat="server" OnClick="btnCadastrar_Click" CssClass="btn btn-success btn-preloader">
                            <i class="fas fa-save"></i> CADASTRAR
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divCortina" runat="server" visible="false"></div>
        <div id="divModal" class="divModal" runat="server" visible="false">
            <div class="card border-danger" id="divBorder" runat="server">
                <div class="card-header text-center bg-danger" id="divHeader" runat="server">
                    <div class="row">
                        <div class="col-sm-1"></div>
                        <h4 class="col-sm-10"><asp:Label ID="lblTitulo" runat="server"></asp:Label>
                        </h4>
                        <div class="col-sm-1 text-right">
                            <asp:LinkButton ID="btnFecharAviso" runat="server" CssClass="btn text-dark" OnClick="btnFecharAviso_Click">
                                <i class="far fa-times-circle fa-2x"></i>
                            </asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div class="card-body">
                    <div class="form-group row">
                        <div class="col-sm-1"></div>
                        <div class="col-sm-10 text-center">
                            <h2><asp:Label ID="lblAviso" runat="server" Font-Bold="true"></asp:Label></h2>
                        </div>
                    </div>
                </div>
            </div>
        </div>
</asp:Content>
