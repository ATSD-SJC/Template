using ProjetoTemplate.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace ProjetoTemplate.Classes
{
    /// <summary>
    /// Nessa classe irão ficar os métodos utilities do sistema, como métodos de envio de e-mail, validação de cpf,  tratamento de strings, log de erros e etc
    /// </summary>
    public static class Util
    {
        private static string smtp_fixo_envio = "smtp.atsd.com.br";
        private static string from_fixo_envio = "sgcatsd@atsd.com.br";
        private static int porta_fixo_envio = 587;
        private static bool ssl_fixo_envio = false;
        private static bool default_credenciais_fixo_envio = false;
        private static NetworkCredential credenciais_fixo_envio = new NetworkCredential( "sgcatsd@atsd.com.br", "atsd@2016@@" );

        // Producao
        public static string url_principal = $"";



        /// <summary>
        /// Registrar um erro ocorrido em um Catch
        /// </summary>
        /// <param name="pagina">Página da Ocorrência</param>
        /// <param name="funcao">Função Executada pelo Usuário</param>
        /// <param name="ex">Objeto da Exceção</param>
        public static void registraErro(string pagina, string funcao, Exception ex)
        {
            string insert = @"INSERT INTO Log_Erros(pagina, funcao, mensagem, stacktrace, datahora)
                VALUES(@pagina, @funcao, @mensagem, @stacktrace, GETDATE())";
            SqlCommand command = new SqlCommand();

            command.CommandText = insert;
            command.Parameters.AddWithValue( "@pagina", pagina );
            command.Parameters.AddWithValue( "@funcao", funcao );
            try {
                command.Parameters.AddWithValue( "@mensagem", ex.Message.Replace( "'", "´" ) );
                if (ex.StackTrace == null)
                    command.Parameters.AddWithValue( "@stacktrace", "" );
                else
                    command.Parameters.AddWithValue( "@stacktrace", ex.StackTrace.Replace( "'", "´" ) );
            }
            catch {
                command.Parameters.AddWithValue( "@mensagem", "" );
                command.Parameters.AddWithValue( "@stacktrace", "" );
            }

            new ConnectionFactory().executeNonCommand( command );
        }

        /// <summary>
        /// Realiza a Busca de Um CEP em API externa
        /// </summary>
        /// <param name="cep">cep a ser localizado</param>
        /// <returns></returns>
        public static Endereco buscaCEP(string cep)
        {
            var end = new Endereco();
            // Objeto DataSet que receberá a tabela em XML que contém os dados da pesquisa
            DataSet ds = new DataSet();
            // Armazena o arquivo XML retirado da página onde o CEP foi pesquisado
            try {
                string endereco = "http://viacep.com.br/ws/" + cep.Replace( ".", "" ).Replace( "-", "" ) + "/xml/";

                //Chamando API client
                WebClient wc = new WebClient();
                MemoryStream ms = new MemoryStream( wc.DownloadData( endereco ) );
                ds.ReadXml(ms);

                // Caso tenha encontrado o CEP o valor da primeira célula da primeira linha da tabela será 1 
                string item = ds.Tables[0].Rows[0][0].ToString();
                if (!item.Equals( "" )) {
                    // Repassa os valores contidos nas células da primeira linha para suas
                    // respectivas TextBox'es, para serem exibidos para o usuário
                    end.cidade = new Cidade();
                    //new CidadeController().Consulta( " AND UF = '" + ds.Tables[0].Rows[0]["uf"].ToString().Trim() + "' AND nome = '" + RemoveAcentos( ds.Tables[0].Rows[0]["localidade"].ToString().Trim() ) + "' COLLATE Latin1_General_CI_AI" ).FirstOrDefault();
                    end.bairro = ds.Tables[0].Rows[0]["bairro"].ToString().Trim();
                    end.logradouro = ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
                    end.uf = ds.Tables[0].Rows[0]["uf"].ToString().Trim().ToUpper();
                    return end;
                }
            }
            catch (Exception err) {
                registraErro( "Util", "BuscaCep", err );
            }

            return null;
        }


        /// <summary>
        /// Remover acentos de uma string
        /// </summary>
        /// <param name="text">string</param>
        /// <returns>string sem acentos</returns>
        public static string RemoveAcentos(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize( NormalizationForm.FormD ).ToCharArray();
            foreach (char letter in arrayText) {
                if (CharUnicodeInfo.GetUnicodeCategory( letter ) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append( letter );
            }
            return sbReturn.ToString();
        }


        /// <summary>
        /// Verifica a validade de um cpf
        /// </summary>
        /// <param name="valor"></param>
        /// <returns></returns>
        public static bool isCPF(string valor)
        {
            if (valor == "")
                return false;

            string cpf = valor;

            int d1, d2;
            int soma = 0;
            string digitado = "";
            string calculado = "";

            cpf = cpf.Replace( ".", "" ).Replace( "-", "" );
            // Pesos para calcular o primeiro digito
            int[] peso1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            // Pesos para calcular o segundo digito
            int[] peso2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] n = new int[11];

            // Se o tamanho for < 11 entao retorna como inválido
            if (cpf.Length != 11)
                return false;

            // Caso coloque todos os numeros iguais
            switch (cpf) {
                case "11111111111":
                    return false;
                case "00000000000":
                    return false;
                case "2222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;
            }

            try {

                // Quebra cada digito do CPF
                n[0] = Convert.ToInt32( cpf.Substring( 0, 1 ) );
                n[1] = Convert.ToInt32( cpf.Substring( 1, 1 ) );
                n[2] = Convert.ToInt32( cpf.Substring( 2, 1 ) );
                n[3] = Convert.ToInt32( cpf.Substring( 3, 1 ) );
                n[4] = Convert.ToInt32( cpf.Substring( 4, 1 ) );
                n[5] = Convert.ToInt32( cpf.Substring( 5, 1 ) );
                n[6] = Convert.ToInt32( cpf.Substring( 6, 1 ) );
                n[7] = Convert.ToInt32( cpf.Substring( 7, 1 ) );
                n[8] = Convert.ToInt32( cpf.Substring( 8, 1 ) );
                n[9] = Convert.ToInt32( cpf.Substring( 9, 1 ) );
                n[10] = Convert.ToInt32( cpf.Substring( 10, 1 ) );
            }
            catch {
                return false;
            }

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso1.GetUpperBound( 0 ); i++)
                soma += (peso1[i] * Convert.ToInt32( n[i] ));

            // Pega o resto da divisao
            int resto = soma % 11;

            if (resto == 1 || resto == 0)
                d1 = 0;
            else
                d1 = 11 - resto;

            soma = 0;

            // Calcula cada digito com seu respectivo peso
            for (int i = 0; i <= peso2.GetUpperBound( 0 ); i++)
                soma += (peso2[i] * Convert.ToInt32( n[i] ));

            // Pega o resto da divisao
            resto = soma % 11;
            if (resto == 1 || resto == 0)
                d2 = 0;
            else
                d2 = 11 - resto;

            calculado = d1.ToString() + d2.ToString();
            digitado = n[9].ToString() + n[10].ToString();

            // Se os ultimos dois digitos calculados bater com
            // os dois ultimos digitos do cpf entao é válido
            if (calculado == digitado)
                return (true);
            else
                return (false);
        }
    }
}