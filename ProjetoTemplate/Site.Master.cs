using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoTemplate
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Método público para verificar se o usuário fez o login no sistema
        /// </summary>
        /// <returns>Retorna verdadeiro ou falso</returns>
        public bool verificaLogin()
        {
            return true;
        }
    }
}