<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CadastroInnerJoin.aspx.cs" Inherits="ProjetoTemplate.Paginas.CadastroInnerJoin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/mascara.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(window).load(function () {
            $('#preloader1').delay(0).fadeOut('slow', function () { });
        });
        $(function () {

            $(".btn-preloader").click(function () {
                $('#preloader1').delay(0).fadeIn('fast', function () { });
                $("#genCortina").fadeIn('fast');
            });
        });


    </script>
    <div class="panel panel-body">
        <div class="page-header">
            <h1>Cadastro <small>com InnerJoin</small></h1>
        </div>
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="form-group row">
                    <span class="col-md-4">Classe *:</span>
                    <div class="col-md-8">
                        <asp:DropDownList ID="ddlClasse" runat="server" CssClass="form-control" OnInit="ddlClasse_Init"></asp:DropDownList>
                        <small class="busca">Esse campo é obrigatório!</small>
                    </div>
                </div>
                <div class="form-group row">
                    <span class="col-md-4">Data/Hora *:</span>
                    <div class="col-md-8">
                        <asp:TextBox ID="txtDataHora" runat="server" TextMode="DateTimeLocal" CssClass="form-control" placeholder="dd/mm/aaaa hh:mm"></asp:TextBox>
                        <small class="busca">Esse campo é obrigatório!</small>
                    </div>
                </div>
                <div class="form-group row">
                    <span class="col-md-4">Tipo *:</span>
                    <div class="col-md-8">
                        <asp:RadioButtonList ID="rbTipo" runat="server" OnSelectedIndexChanged="rbTipo_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="0" Selected="True">PESSOA FÍSICA</asp:ListItem>
                            <asp:ListItem Value="1">PESSOA JURÍDICA</asp:ListItem>
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div id="divCpf" runat="server" visible="true">
                    <div class="form-group row">
                        <span class="col-md-4">CPF *:</span>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" placeholder="XXX.XXX.XXX-XX" onkeyup="formataCPF(this,event);" MaxLength="14"></asp:TextBox>
                            <small class="busca">Esse campo é obrigatório!</small>
                        </div>
                    </div>
                </div>
                <div id="divCnpj" runat="server" visible="false">
                    <div class="form-group row">
                        <span class="col-md-4">CNPJ *:</span>
                        <div class="col-md-8">
                            <asp:TextBox ID="txtCNPJ" runat="server" CssClass="form-control" placeholder="XX.XXX.XXX/XXXX-XX" onkeyup="formataCNPJ(this,event);" MaxLength="18"></asp:TextBox>
                            <small class="busca">Esse campo é obrigatório!</small>
                        </div>
                    </div>
                </div>
            </div>
            <div class="form-group row">
                <span class="col-md-4">Telefone *:</span>
                <div class="col-md-8">
                    <asp:TextBox ID="txtTelefone" runat="server" CssClass="form-control" placehold="(XX) XXXX-XXXX" onkeyup="formataTelefone(this,event);" MaxLength="14"></asp:TextBox>
                    <small class="busca">Esse campo é obrigatório!</small>
                </div>
            </div>
            <div class="form-group row text-center">
                <asp:LinkButton ID="btnCadastrar" runat="server" CssClass="btn btn-preloader btn-success" OnClick="btnCadastrar_Click">
                </asp:LinkButton>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
