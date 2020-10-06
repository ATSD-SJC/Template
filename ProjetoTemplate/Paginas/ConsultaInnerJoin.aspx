<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaInnerJoin.aspx.cs" Inherits="ProjetoTemplate.Paginas.ConsultaInnerJoin" %>
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
            <h1>Consulta <small> de Inner Join</small></h1>
        </div>
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="form-group row">
                    <span class="col-md-2">Classe:</span>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlClasse" runat="server" CssClass="form-control" OnInit="ddlClasse_Init"></asp:DropDownList>
                    </div>
                    <span class="col-md-2">Tipo:</span>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-control">
                            <asp:ListItem Value="0">TODOS</asp:ListItem>
                            <asp:ListItem Value="1">PESSOA FÍSICA</asp:ListItem>
                            <asp:ListItem Value="2">PESSOA JURÍDICA</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group row">
                    <span class="col-md-2">CPF:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" onkeyup="formataCPF(this,event);" MaxLength="14" placeholder="XXX.XXX.XXX-XX"></asp:TextBox>
                    </div>
                    <span class="col-md-2">CNPJ:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtCNPJ" runat="server" CssClass="form-control" onkeyup="formataCNPJ(this,event);" MaxLength="18" placeholder="XX.XXX.XXX/XXXX-XX"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <span class="col-md-2">Data Início:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDataInicio" runat="server" CssClass="form-control" onkeyup="formataData(this,event);" MaxLength="10" placeholder="DD/MM/AAAA"></asp:TextBox>
                    </div>
                    <span class="col-md-2">Data Fim:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDataFim" runat="server" CssClass="form-control" onkeyup="formataData(this,event);" MaxLength="10" placeholder="DD/MM/AAAA"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <span class="col-md-2">Hora Início:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtHoraInicio" runat="server" CssClass="form-control" onkeyup="formataHora(this,event);" MaxLength="5" placeholder="HH:MM"></asp:TextBox>
                    </div>
                    <span class="col-md-2">Hora Fim:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtHoraFim" runat="server" CssClass="form-control" onkeyup="formataHora(this,event);" MaxLength="5" placeholder="HH:MM"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <span class="col-md-2">Telefone:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtTelefone" runat="server" CssClass="form-control" onkeyup="formataTelefone(this,event);" MaxLength="14" placehold="(XX) XXXX-XXXX"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row text-center">
                    <asp:LinkButton ID="btnPesquisar" runat="server" CssClass="btn btn-primary btn-preloader" OnClick="btnPesquisar_Click">
                        <span class="glyphicon glyphicon-search"></span> PESQUISAR
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <div id="divResultado" runat="server" visible="false">
            <div class="panel panel-default">
                <div class="panel-body">
                    <div class="form-group row text-center">
                        <asp:Label ID="lblTotal" runat="server"></asp:Label>
                    </div>
                    <div class="form-group row text-center">
                        <asp:LinkButton ID="btnGerarExcel" runat="server" CssClass="btn btn-success btn-preloader" OnClick="btnGerarExcel_Click">
                            <span class="glyphicon glyphicon-save"></span> GERAR EXCEL
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnGerarCrystal" runat="server" CssClass="btn btn-danger btn-preloader" OnClick="btnGerarCrystal_Click">
                            <span class="glyphicon glyphicon-download-alt"></span> GERAR PDF
                        </asp:LinkButton>
                    </div>
                    <div class="form-group row">
                        <asp:GridView  ID="grdResultado" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdResultado_PageIndexChanging" OnRowCommand="grdResultado_RowCommand" 
                            EmptyDataText="Não foram encontrados dados." CssClass="table table-hover text-uppercase" OnRowDataBound="grdResultado_RowDataBound" AllowSorting="true" OnSorting="grdResultado_Sorting" AutoGenerateColumns="false">
                            <Columns>
                                <asp:BoundField DataField="Classe.Texto" HeaderText="TEXTO" />
                                <asp:BoundField DataField="Tipo" HeaderText="TIPO" />
                                <asp:BoundField DataField="Cpf" HeaderText="CPF/CNPJ" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnDetalhes" runat="server" CommandName="detalhes" CommandArgument="<%#((GridViewRow) Container).RowIndex  %>" CssClass="btn btn-sm btn-info">
                                            <span class="glyphicon glyphicon-list-alt"></span> DETALHES
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnAlterar" runat="server" CssClass="btn-warning btn-sm btn-preloader" CommandArgument="<%#((GridViewRow) Container).RowIndex  %>" CommandName="alterar">
                                            <span class="glyphicon glyphicon-pencil"></span> ALTERAR
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnExcluir" runat="server" CssClass="btn-danger btn-sm" OnClientClick="return confirm('Deseja realmente excluir esses dados?')" CommandArgument="<%#((GridViewRow) Container).RowIndex  %>" CommandName="excluir">
                                            <span class="glyphicon glyphicon-remove-sign"></span> EXCLUIR
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="pagination-ys btn-preloader" HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="divCortina" runat="server" visible="false"></div>
    <div runat="server" id="divModal" visible="false">
        <div class="panel panel-default">
            <div class="panel-body">
                <div class="page-header">
                    <h2>Detalhes <small>Inner Join</small></h2>
                    <div class="form-group row">
                        <span class="col-md-4">Id:</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblId" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <span class="col-md-4">Classe:</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblClasse" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <span class="col-md-4">Data/Hora:</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblDataHora" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <span class="colmd-4">Tipo:</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblTipo" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <span class="col-md-4"><asp:Label ID="lblDocumentoSpan" runat="server"></asp:Label></span>
                        <div class="col-md-8">
                            <asp:Label ID="lblDocumento" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row">
                        <span class="col-md-4">Telefone:</span>
                        <div class="col-md-8">
                            <asp:Label ID="lblTelefone" runat="server"></asp:Label>
                        </div>
                    </div>
                    <div class="form-group row text-center">
                        <asp:LinkButton ID="btnFechar" runat="server" CssClass="btn btn-default" OnClick="btnFechar_Click">
                            <span class="glyphicon glyphicon-remove"></span> FECHAR
                        </asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
