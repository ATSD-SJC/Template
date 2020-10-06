using System.Web;
using System.Web.UI;

namespace ProjetoTemplate.Classes
{
    class MessageBox
    {
        //public static void Show(string message)
        //{
        //    Page page = HttpContext.Current.CurrentHandler as Page;

        //    if ((!page.ClientScript.IsClientScriptBlockRegistered("alert")))
        //    {
        //     //   page.ClientScript.RegisterClientScriptBlock(page.Page.GetType(), "Alerta", "alert('" + message + "');", true);
        //        //page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", "<script type='text/javascript'>alert('" + message + "');</script>");
        //        page.ScriptManager.RegisterClientScriptBlock(page.GetType(), "alert", "<script type='text/javascript'>alert('" + message + "');</script>");
        //    }
        //}

        public static void Show(string message)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            if ((!page.ClientScript.IsClientScriptBlockRegistered("alert")))
            {
                page.ClientScript.RegisterStartupScript(page.Page.GetType(), "Alerta", "alert('" + message + "');", true);
                //  page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", "<script type='text/javascript'>alert('" + message + "');</script>");
            }
        }

        public static void Exibir(string message)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            if ((!page.ClientScript.IsClientScriptBlockRegistered("alert")))
            {
                page.ClientScript.RegisterClientScriptBlock(page.Page.GetType(), "Alerta", "alert('" + message + "');", true);
              //  page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", "<script type='text/javascript'>alert('" + message + "');</script>");
            }
        }

        public static void AlertCustomizado(string message,string alert,string divAppend)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            page.ClientScript.RegisterClientScriptBlock(page.Page.GetType(), "Alerta", @" $(function() {

            $('#"+divAppend+"').append('<div class='alert alert-"+alert+" alert-dismissible fade in' role='alert'> <button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>×</span></button> <strong>ALERTA!</strong>"+message+". </div>');});", true);

            
        }

        public static void ShowAndRedirect(string message, string path)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;

            if ((!page.ClientScript.IsClientScriptBlockRegistered("alert")))
            {
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", "<script type='text/javascript'>alert('" + message + "'); window.location.href='" + path + "';</script>");
            }
        }



        public static void closePreloader()
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "preloader", "<script text='text/javascript'>closePreloader();</script>" );
        }

        //public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
        //{

        //}
    }
}
