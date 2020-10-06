using ProjetoTemplate.Classes;
using ProjetoTemplate.Controllers;
using ProjetoTemplate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoTemplate.Paginas
{
    public partial class ConsultaInnerJoin : System.Web.UI.Page
    {
        public static SiteMaster master = new SiteMaster();

        #region LOADS E INITS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (master.verificaLogin())
            {
                if (!IsPostBack)
                {

                    //AUDITORIA
                }
            }
        }

        protected void ddlClasse_Init(object sender, EventArgs e)
        {
            ddlClasse.Items.Clear();
            var classes = new ClasseController().GetAll();
            ddlClasse.Items.Add(new ListItem("SELECIONE", "0"));
            foreach (var classe in classes)
            {
                ddlClasse.Items.Add(new ListItem(classe.Texto.ToUpper(), classe.Id.ToString()));
            }
        }

        #endregion

        #region ELEMENTS CHANGES

        protected void grdResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultado.PageIndex = e.NewPageIndex;

            grdResultado.DataSource = (List<InnerJoin>)Session["consultaInnerJoin"];
            grdResultado.DataBind();
        }

        protected void grdResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "detalhes" || e.CommandName == "alterar" || e.CommandName == "excluir")
            {
                int index = Convert.ToInt32(e.CommandArgument) + (grdResultado.PageIndex * grdResultado.PageSize);
                var innerJoins = (List<InnerJoin>)Session["consultaClasse"];

                switch (e.CommandName)
                {
                    case "detalhes":
                        CarregarDetalhes(innerJoins[index]);
                        break;
                    case "alterar":
                        Response.Redirect($"CadastroInnerJoin.aspx?Id={innerJoins[index].Id}");
                        break;
                    case "excluir":
                        if (new InnerJoinController().Delete(innerJoins[index].Id))
                        {
                            MessageBox.Show("Excluído com sucesso!");
                        }
                        else
                        {
                            MessageBox.Show("Erro ao excluir!");
                        }
                        break;
                }
            }
        }

        protected void grdResultado_Sorting(object sender, GridViewSortEventArgs e)
        {
            var innerJoins = (List<InnerJoin>)Session["consultaInnerJoin"];

            var direcao = GetSortDirection(e.SortExpression);

            if (direcao == "ASC")
            {
                switch (e.SortExpression)
                {
                    case "Classe.Texto":
                        innerJoins = innerJoins.OrderBy(x => x.Classe.Texto).ToList();
                        break;
                    case "Tipo":
                        innerJoins = innerJoins.OrderBy(x => x.Tipo).ToList();
                        break;
                    case "CPF":
                        innerJoins = innerJoins.OrderBy(x => x.Cpf).ThenBy(x => x.Cnpj).ToList();
                        break;
                }

            }
            else
            {
                switch (e.SortExpression)
                {
                    case "Id":
                        innerJoins = innerJoins.OrderByDescending(x => x.Classe.Texto).ToList();
                        break;
                    case "Texto":
                        innerJoins = innerJoins.OrderByDescending(x => x.Tipo).ToList();
                        break;
                    case "Data":
                        innerJoins = innerJoins.OrderByDescending(x => x.Cpf).ThenBy(x => x.Cnpj).ToList();
                        break;

                }
            }
            Session["consultaInnerJoin"] = innerJoins;

            grdResultado.DataSource = innerJoins;
            grdResultado.DataBind();
        }

        protected void grdResultado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            var innerJoins = (List<InnerJoin>)Session["consultaInnerJoin"];
            var index = e.Row.RowIndex;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text.Contains("FÍSICA"))
                {
                    e.Row.Cells[2].Text = innerJoins[index].Cpf;
                }
                else
                {
                    e.Row.Cells[2].Text = innerJoins[index].Cnpj;
                }
            }
        }

        #endregion

        #region BOTÕES CLIQUES

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            var filtro = CarregarFiltro();

            var innerJoin = new InnerJoinController().GetAll(filtro);

            Session["colunaInner"] = "Id";
            divResultado.Visible = true;
            grdResultado.DataSource = innerJoin;
            grdResultado.DataBind();

            lblTotal.Text = $"<b>Total: {innerJoin.Count}.</b>";

            Session["consultaInnerJoin"] = innerJoin;
        }

        protected void btnGerarExcel_Click(object sender, EventArgs e)
        {
            List<InnerJoin> innerJoins = (List<InnerJoin>)Session["consultaInnerJoin"];
            Session["GerarExcelSession_Colunas"] = new string[] { "Id", "Classe", "DataHora", "Tipo", "CPF", "CNPJ", "Telefone" };
            Session["GerarExcelSession_Valores"] = new string[] { "Id", "Classe.Texto", "DataHora", "Tipo", "CPF", "CNPJ", "Telefone" };

            Session["GerarExcelTable_Titulo"] = "Consulta de Inner Join";
            Session["GerarExcelTable_Conteudo"] = innerJoins;
            Response.Redirect("GerarExcelTable.aspx");
        }

        protected void btnGerarCrystal_Click(object sender, EventArgs e)
        {

        }

        protected void btnFechar_Click(object sender, EventArgs e)
        {
            divModal.Visible = false;
            divCortina.Visible = false;
        }

        #endregion

        #region MÉTODOS AUXILIARES

        public string CarregarFiltro()
        {
            var filtro = "";

            if(ddlClasse.SelectedValue != "0")
            {
                filtro += $" AND IdClasse = {ddlClasse.SelectedValue}";
            }
            if(ddlTipo.SelectedValue != "0")
            {
                filtro += $" AND Tipo = '{ddlTipo.SelectedItem.Text}'";
            }
            if(txtCPF.Text != "")
            {
                filtro += $" AND Cpf = '{txtCPF.Text}'";
            }
            if(txtCNPJ.Text != "")
            {
                filtro += $" AND Cnpj = '{txtCNPJ.Text}'";
            }
            if(txtDataInicio.Text != "")
            {
                filtro += $" AND DataHora >= '{txtDataInicio.Text} 00:00:00'";
            }
            if (txtDataFim.Text != "")
            {
                filtro += $" AND DataHora <= '{txtDataFim.Text} 23:59:59'";
            }
            if(txtHoraInicio.Text != "")
            {
                filtro += $" AND CAST(DataHora as time) >= '{txtHoraInicio.Text}'";
            }
            if(txtHoraFim.Text != "")
            {
                filtro += $" AND CAST(DataHora as time) <= '{txtHoraFim.Text}'";
            }
            if(txtTelefone.Text != "")
            {
                filtro += $" AND Telefone = '{txtTelefone.Text}'";
            }
            return filtro;
        }

        public string GetSortDirection(string column)
        {
            string direcao = "ASC";

            string coluna = Session["colunaInner"].ToString();

            if (coluna == column)
            {
                string ultimaDirecao = Session["direcaoInner"].ToString();
                if (ultimaDirecao == "ASC")
                {
                    direcao = "DESC";
                }
            }

            Session["colunaInner"] = column;
            Session["direcaoInner"] = direcao;

            return direcao;

        }

        protected void CarregarDetalhes(InnerJoin innerJoin)
        {

            lblId.Text = innerJoin.Id.ToString();
            lblClasse.Text = innerJoin.Classe.Texto;
            lblDataHora.Text = innerJoin.DataHora.ToString();
            lblTipo.Text = innerJoin.Tipo;
            lblDocumentoSpan.Text = innerJoin.Cpf == "" || innerJoin.Cpf == null ? "CNPJ: " : "CPF: ";
            lblDocumento.Text = innerJoin.Cpf == "" || innerJoin.Cpf == null ? innerJoin.Cnpj : innerJoin.Cpf;
            lblTelefone.Text = innerJoin.Telefone;


            divModal.Visible = true;
            divCortina.Visible = true;
        }

        #endregion

    }
}