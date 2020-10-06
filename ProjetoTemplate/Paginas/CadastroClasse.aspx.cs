using ProjetoTemplate.Classes;
using ProjetoTemplate.Controllers;
using ProjetoTemplate.Models;
using System;

namespace ProjetoTemplate.Generica
{
    public partial class CadastroClasse : System.Web.UI.Page
    {

        public static SiteMaster master = new SiteMaster();

        #region LOADS E INITS

        protected void Page_Load(object sender, EventArgs e)
        {
            if (master.verificaLogin())
            {
                if (!IsPostBack)
                {

                    if (Request.QueryString.Count > 0) //ALTERAÇÃO
                    {
                        btnCadastrar.CssClass = "btn btn-warning btn-preloader";
                        btnCadastrar.Text = "<span class='glyphicon glyphicon-pencil'></span> ALTERAR";
                        CarregarCampos(Convert.ToInt32(Request.QueryString["id"]));
                        //AUDITORIA
                    }
                    else // CADASTRO
                    {
                        btnCadastrar.CssClass = "btn btn-success btn-preloader";
                        btnCadastrar.Text = "<span class='glyphicon glyphicon-floppy-saved'></span> CADASTRAR";
                        CarregarCampos();
                        //AUDITORIA
                    }
                }

            }
        }

        #endregion

        #region BOTÕES CLIQUES

        protected void btnCadastrar_Click(object sender, EventArgs e)
        {
            var resultado = ValidaCampos();
            if (resultado != "")
            {
                MessageBox.Show(resultado);
                return;
            }

            var classe = (Classe)Session["classe"];

            classe.Texto = Util.RemoveAcentos(txtTexto.Text);
            if (txtData.Text != "") { classe.Data = Convert.ToDateTime(txtData.Text); }
            classe.Valor = Convert.ToDouble(txtValor.Text);
            classe.Booleano = ddlBooleano.SelectedValue == "1";

            if (new ClasseController().Salvar(classe))
            {
                MessageBox.ShowAndRedirect("Salvo com sucesso!", "ConsultaClasse.aspx");
                //AUDITORIA
                return;
            }

            MessageBox.Show("Erro ao salvar!");
        }

        #endregion

        #region MÉTODOS AUXILIARES

        public void CarregarCampos(int id = 0)
        {
            Classe classe;
            if (id > 0)
            {
                classe = new ClasseController().GetSingle(id);

                txtTexto.Text = classe.Texto;
                txtData.Text = classe.Data == new DateTime(1900, 01, 01) ? "" : classe.Data.ToShortDateString();
                txtValor.Text = classe.Valor.ToString();
                ddlBooleano.SelectedValue = classe.Booleano ? "1" : "0"; 
            }
            else
            {
                classe = new Classe();
            }

            Session["classe"] = classe;
        }

        public string ValidaCampos()
        {
            if (txtTexto.Text == "")
            {
                txtTexto.Focus();
                return "Digite o texto!";
            }
            if (txtValor.Text == "")
            {
                txtValor.Focus();
                return "Digite o valor!";
            }
            if (Convert.ToDouble(txtValor.Text) == 0)
            {
                txtValor.Focus();
                return "Digite um valor maior que zero!";
            }
            if (ddlBooleano.SelectedValue == "SELECIONE")
            {
                ddlBooleano.Focus();
                return "Selecione um booleano!";
            }
            if (txtData.Text != "" && txtData.Text.Length < 10)
            {
                return "Digite a data corretamente ou deixe-a vazia!";
            }
            return "";
        }

        #endregion

        protected void btnFecharAviso_Click(object sender, EventArgs e)
        {
            divModal.Visible =
                divCortina.Visible = false;
        }
    }
}