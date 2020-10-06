using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ProjetoTemplate.Classes
{
    /// <summary>
    /// Classe destinada para gerenciar as conexões com o banco de dados
    /// </summary>
    public class ConnectionFactory
    {

        private SqlConnection _Connection;

        public ConnectionFactory()
        {
            try
            {
                string conexao = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
                _Connection = new SqlConnection(conexao);
            }
            catch (Exception ex) {
                throw ex;
            }
        }
        /// <summary>
        /// Método para fechar a conexão com o banco de dados após realização das queries
        /// </summary>
        public void CloseConnection()
        {
            _Connection.Close();
        }

        /// <summary>
        /// Método que devolve um objeto DataTable genérico a partir de uma consulta SQL.
        /// </summary>
        /// <param name="sql"></param>
        /// <returns>DataTable genérico com os campos do SQL passado por parâmetro.</returns>
        public DataTable ExecuteToDataTable(string sql)
        {
            try {
                _Connection.Open();
            }
            catch {
                _Connection.Close();
                _Connection.Open();
            }

            try {
                SqlCommand command = new SqlCommand(sql, _Connection);

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());

                return dt;
            }
            catch {
                
                return null;
            }
            finally {
                _Connection.Close();
            }
        }

        /// <summary>
        /// Método que executa uma função SQL de inserção/alteração
        /// </summary>
        /// <param name="sql">Instrução a ser executada</param>
        /// <returns>Número de linhas afetadas</returns>
        public int executeNonQuery(string sql, string metodo = "", string classe = "")
        {
            try { 
                _Connection.Open(); 
            }
            catch { 
                _Connection.Close(); 
                _Connection.Open(); 
            }

            try {
                SqlCommand command = new SqlCommand( sql, _Connection );
                return (int)command.ExecuteNonQuery();
            }
            catch (Exception ex) {
                Util.registraErro(classe, metodo, ex);
                return -1;
            }
            finally {
                _Connection.Close();
            }
        }


        /// <summary>
        /// Método que executa uma função SQL de inserção/alteração recebendo um SqlCommand
        /// </summary>
        /// <param name="command">Command que será executado</param>
        /// <returns>Númeruo de linhas afetadas</returns>
        public int executeNonCommand(SqlCommand command)
        {
            try { _Connection.Open(); }
            catch { _Connection.Close(); _Connection.Open(); }

            try {
                command.Connection = _Connection;
                return (int)command.ExecuteNonQuery();
            }
            catch (Exception ex) {
                return -1;
            }
            finally {
                _Connection.Close();
            }
        }
    }
}