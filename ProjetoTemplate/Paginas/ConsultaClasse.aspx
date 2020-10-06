<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ConsultaClasse.aspx.cs" Inherits="ProjetoTemplate.Paginas.ConsultaClasse" %>
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
    <div class="card card-body">
        <div class="page-header">
            <h1>Consulta <small>de Classe</small></h1>
        </div>
        <div class="form-group row"></div>
        <div class="card card-default">
            <div class="card-header">
                <div class="row">
                    <div class="col-1">
                        <img src="../imagens/logo_atsd.png" style="width:50px;" class="img-fluid" />
                    </div>
                    <div class="col-10 text-center">
                        PESQUISA
                    </div>
                </div>
            </div>
            <div class="card-body">
                <div class="form-group row">
                    <span class="col-md-2">Id:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtId" runat="server" CssClass="form-control text-uppercase" onkeyup="formataInteiro(this,event);"></asp:TextBox>
                    </div>
                    <span class="col-md-2">Texto:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtTexto" runat="server" CssClass="form-control text-uppercase" ></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <span class="col-md-2">Data de:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDataInicio" runat="server" CssClass="form-control" onkeyup="formataData(this,event);" MaxLength="10" placeholder="DD/MM/AAAA"></asp:TextBox>
                    </div>
                    <span class="col-md-2">Até Data:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtDataFim" runat="server" CssClass="form-control" onkeyup="formataData(this,event);" MaxLength="10" placeholder="DD/MM/AAAA"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row">
                    <span class="col-md-2">Valor:</span>
                    <div class="col-md-4">
                        <asp:TextBox ID="txtValor" runat="server" CssClass="form-control" onkeyup="formataValor(this,event);"></asp:TextBox>
                    </div>
                    <span class="col-md-2">Booleano:</span>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlBooleano" runat="server" CssClass="form-control">
                            <asp:ListItem Value="TODOS">TODOS</asp:ListItem>
                            <asp:ListItem Value="1">SIM</asp:ListItem>
                            <asp:ListItem Value="0">NÃO</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group text-center">
                    <asp:LinkButton ID="btnPesquisar" runat="server" CssClass="btn btn-primary btn-preloader" OnClick="btnPesquisar_Click">
                        <span class="glyphicon glyphicon-search"></span> PESQUISAR
                    </asp:LinkButton>
                    <asp:LinkButton ID="btnGerarExcel" Visible="false" runat="server" OnClientClick="return alert('Uma nova janela (popup) abrirá para download do arquivo!');" CssClass="btn btn-success btn-preloader" OnClick="btnGerarExcel_Click">
                        <span class="glyphicon glyphicon-save"></span> GERAR EXCEL
                    </asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="panel panel-default" id="divResultado" runat="server" visible="false">
            <div class="panel-body">
                <div class="form-group text-center">
                    <asp:Label ID="lblTotal" runat="server"></asp:Label>
                </div>
                <div class="form-group text-center">
                    <asp:GridView ID="grdResultado" Width="100%" runat="server" AllowPaging="true" PageSize="20" OnPageIndexChanging="grdResultado_PageIndexChanging" OnRowCommand="grdResultado_RowCommand" HeaderStyle-CssClass="thead-dark" 
                        EmptyDataText="Não foram encontrados dados." CssClass="table table-hover text-uppercase" AllowSorting="true" OnSorting="grdResultado_Sorting" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                            <asp:BoundField DataField="Texto" HeaderText="TEXTO" ControlStyle-CssClass="text-uppercase" SortExpression="Text" />
                            <asp:BoundField DataField="Data" HeaderText="DATA" DataFormatString="{0:dd/MM/yyyy}" SortExpression="Data" />
                            <asp:BoundField DataField="Valor" HeaderText="VALOR" DataFormatString="{0:C2}" SortExpression="Valor" />
                            <asp:BoundField DataField="Booleano" HeaderText="BOOLEANO" SortExpression="Booleano" />
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
                        <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
