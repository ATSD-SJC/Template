using ProjetoTemplate.Classes;
using ProjetoTemplate.Controllers;
using ProjetoTemplate.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoTemplate.Paginas
{
    public partial class ConsultaClasse : System.Web.UI.Page
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

        #endregion

        #region ELEMENTS CHANGES

        protected void grdResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultado.PageIndex = e.NewPageIndex;

            grdResultado.DataSource = (List<Classe>)Session["consultaClasse"];
            grdResultado.DataBind();
        }

        protected void grdResultado_Sorting(object sender, GridViewSortEventArgs e)
        {
            var classes = (List<Classe>)Session["consultaClasse"];

            var direcao = GetSortDirection(e.SortExpression);

            if (direcao == "ASC")
            {
                switch (e.SortExpression)
                {
                    case "Id":
                        classes = classes.OrderBy(x => x.Id).ToList();
                        break;
                    case "Texto":
                        classes = classes.OrderBy(x => x.Texto).ToList();
                        break;
                    case "Data":
                        classes = classes.OrderBy(x => x.Data).ToList();
                        break;
                    case "Valor":
                        classes = classes.OrderBy(x => x.Valor).ToList();
                        break;
                    case "Booleano":
                        classes = classes.OrderBy(x => x.Booleano).ToList();
                        break;

                }

            }
            else
            {
                switch (e.SortExpression)
                {
                    case "Id":
                        classes = classes.OrderByDescending(x => x.Id).ToList();
                        break;
                    case "Texto":
                        classes = classes.OrderByDescending(x => x.Texto).ToList();
                        break;
                    case "Data":
                        classes = classes.OrderByDescending(x => x.Data).ToList();
                        break;
                    case "Valor":
                        classes = classes.OrderByDescending(x => x.Valor).ToList();
                        break;
                    case "Booleano":
                        classes = classes.OrderByDescending(x => x.Booleano).ToList();
                        break;

                }
            }
            Session["consultaClasse"] = classes;

            grdResultado.DataSource = classes;
            grdResultado.DataBind();


        }

        protected void grdResultado_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "alterar" || e.CommandName == "excluir")
            {
                int index = Convert.ToInt32(e.CommandArgument) + (grdResultado.PageIndex * grdResultado.PageSize);
                var classes = (List<Classe>)Session["consultaClasse"];

                switch (e.CommandName)
                {
                    case "alterar":
                        Response.Redirect($"CadastroClasse.aspx?Id={classes[index].Id}");
                        break;
                    case "excluir":
                        if (new ClasseController().Delete(classes[index].Id))
                        {
                            MessageBox.Show("Excluído com sucesso!");
                            btnPesquisar_Click(null, null);
                        }
                        else
                        {
                            MessageBox.Show("Erro ao excluir!");
                        }
                        break;
                }
            }

        }

        #endregion

        #region BOTÕES CLIQUES

        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            var filtro = CarregarFiltros();

            var classes = new ClasseController().GetAll(filtro);

            #region Teste com dados forçados
            //var classes = new List<Classe> ();
            //for(int i = 1; i < 25; i++)
            //{
            //    var booleano = true;
            //    if(i % 2 != 0)
            //    {
            //        booleano = false;
            //    }
            //    classes.Add(new Classe { 
            //        Id = i,
            //        Texto = $"TEXTO {i}",
            //        Booleano = booleano,
            //        Data = DateTime.Now.AddDays(i),
            //        Valor = i * 100 * new Random().NextDouble()
            //    });
            //}
            #endregion


            Session["coluna"] = "Id";
            divResultado.Visible = true;
            grdResultado.DataSource = classes;
            grdResultado.DataBind();

            btnGerarExcel.Visible = (classes.Count > 0);
            lblTotal.Text = $"<b>Total de Resultados: </b> {classes.Count}.";

            var campos = new string[] { "Id", "Texto", "Data", "Valor", "Booleano" };
            Session["GerarExcelTable_Colunas"] = campos;
            Session["GerarExcelTable_Valores"] = campos;
            Session["GerarExcelTable_Conteudo"] = classes;
            Session["GerarExcelTable_Titulo"] = "Consulta de Classe";
            Session["consultaClasse"] = classes;

        }

        protected void btnGerarExcel_Click(object sender, EventArgs e)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            page.ClientScript.RegisterStartupScript(page.Page.GetType(), "Redirecionar", "<script>window.open('GerarExcelTable.aspx', '_blank', 'toolbar = no, scrollbars = no, resizable = no, top = 500, left = 500, width = 400, height = 400', true)</script>");
        }

        #endregion

        #region MÉTODOS AUXILIARES

        public string CarregarFiltros()
        {
            var filtros = "";

            if (txtId.Text != "")
            {
                filtros += $" AND Id = {txtId.Text}";
                //AUDITORIA
            }
            if (txtTexto.Text != "")
            {
                filtros += $" AND Texto like '%{txtTexto.Text.Replace(" ", "%")}%'";
                //AUDITORIA
            }
            if (txtDataInicio.Text != "")
            {
                filtros += $" AND Data >= '{txtDataInicio.Text}'";
                //AUDITORIA
            }
            if (txtDataFim.Text != "")
            {
                filtros += $" AND Data <= '{txtDataFim.Text}'";
                //AUDITORIA
            }
            if (txtValor.Text != "")
            {
                filtros += $" AND Valor = {txtValor.Text.Replace(".", "").Replace(",", ".")}";
                //AUDITORIA
            }

            //AUDITORIA
            return filtros;
        }

        public string GetSortDirection(string column)
        {
            string direcao = "ASC";

            string coluna = Session["coluna"].ToString();

            if (coluna == column)
            {
                string ultimaDirecao = Session["direcao"].ToString();
                if (ultimaDirecao == "ASC")
                {
                    direcao = "DESC";
                }
            }

            Session["coluna"] = column;
            Session["direcao"] = direcao;

            return direcao;

        }

        #endregion

    }
}