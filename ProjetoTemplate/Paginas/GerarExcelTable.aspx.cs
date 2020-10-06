using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjetoTemplate.Paginas
{
    public partial class GerarExcelTable : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] colunas = (string[])Session["GerarExcelTable_Colunas"];
            string[] valores = (string[])Session["GerarExcelTable_Valores"];

            for (int i = 0; i < colunas.Count(); i++)
            {
                BoundField b = new BoundField();
                b.HeaderText = colunas[i];
                b.DataField = valores[i];
                gvExcel.Columns.Add(b);
            }

            // DEFINE O DATASOURCE DO GRIDVIEW COM O COMANDO SQL
            gvExcel.DataSource = Session["GerarExcelTable_Conteudo"];
            // PREENCHE O GRIDVIEW
            gvExcel.DataBind();


            // LABEL INFORMATIVO
            try
            {
                lblinfo.Text = Session["GerarExcelTable_Titulo"].ToString();
               
            }
            catch
            {
            }
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.Charset = "";
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader(
                "Content-Disposition",
                "attachment;filename=dados.xls"
            );
            response.ContentEncoding = System.Text.Encoding.GetEncoding("Windows-1252");

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    gvExcel.RenderControl(htw);
                    response.Write(sw.ToString());
                    response.End();
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        protected void gvExcel_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvExcel_DataBinding(object sender, EventArgs e)
        {

        }

        protected void gvExcel_Init(object sender, EventArgs e)
        {

        }
    }
}