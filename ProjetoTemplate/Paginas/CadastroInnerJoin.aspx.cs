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
    public partial class CadastroInnerJoin : System.Web.UI.Page
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

        protected void rbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            divCpf.Visible = false;
            divCnpj.Visible = false;
            txtCPF.Text = "";
            txtCNPJ.Text = "";
            if (rbTipo.SelectedValue == "0")
            {
                divCpf.Visible = true;
            }
            else
            {
                divCnpj.Visible = true;
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

            var innerJoin = (InnerJoin)Session["innerJoin"];

            innerJoin.Classe = new ClasseController().GetSingle(Convert.ToInt32(ddlClasse.SelectedValue));
            innerJoin.Cnpj = txtCNPJ.Text;
            innerJoin.Cpf = txtCPF.Text;
            innerJoin.DataHora = Convert.ToDateTime(txtDataHora.Text);
            innerJoin.Telefone = txtTelefone.Text;
            innerJoin.Tipo = rbTipo.SelectedValue == "0" ? "PESSOA FÍSICA" : "PESSOA JURÍDICA";

            if (new InnerJoinController().Salvar(innerJoin))
            {
                MessageBox.ShowAndRedirect("Salvo com sucesso!", "ConsultaInnerJoin.aspx");
                //AUDITORIA
                return;
            }

            MessageBox.Show("Erro ao salvar!");

        }

        #endregion

        #region MÉTODOS AUXILIARES

        public void CarregarCampos(int id = 0)
        {
            InnerJoin innerJoin;
            if (id > 0)
            {
                innerJoin = new InnerJoinController().GetSingle(id);
                ddlClasse.SelectedValue = innerJoin.Classe.Id.ToString();
                txtDataHora.Text = innerJoin.DataHora.ToString();
                if (innerJoin.Tipo.Contains("FÍSICA"))
                {
                    rbTipo.SelectedValue = "0";
                    txtCPF.Text = innerJoin.Cpf;
                }
                else
                {
                    rbTipo.SelectedValue = "1";
                    txtCNPJ.Text = innerJoin.Cnpj;
                }
                rbTipo_SelectedIndexChanged(null, null);
                txtTelefone.Text = innerJoin.Telefone;

            }
            else
            {
                innerJoin = new InnerJoin();
            }

            Session["innerJoin"] = innerJoin;
        }

        public string ValidaCampos()
        {
            if (ddlClasse.SelectedValue == "0")
            {
                return "Escolha uma classe!";
            }
            if (txtDataHora.Text == "" || txtDataHora.Text.Length < 16)
            {
                return "Digite a data/hora corretamente!";
            }
            if (!(rbTipo.SelectedValue == "0" || rbTipo.SelectedValue == "1"))
            {
                return "Escolha um tipo!";
            }
            if (rbTipo.SelectedValue == "0" && (txtCPF.Text == "" || txtCPF.Text.Length < 14))
            {
                return "Digite o CPF corretamente!";
            }
            if (rbTipo.SelectedValue == "1" && (txtCNPJ.Text == "" || txtCNPJ.Text.Length < 18))
            {
                return "Digite o CNPJ corretamente!";
            }
            if (txtTelefone.Text == "" || txtTelefone.Text.Length < 14)
            {
                return "Digite o telefone corretamente!";
            }

            return "";
        }

        #endregion

    }
}