using ProjetoTemplate.Classes;
using ProjetoTemplate.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjetoTemplate.Controllers
{
    public class InnerJoinController
    {

        public List<InnerJoin> GetAll(string filtro = "")
        {
            List<InnerJoin> list = new List<InnerJoin>();

            var select = $"SELECT * FROM InnerJoins WHERE (1=1) {filtro}";

            try
            {
                DataTable dt = new ConnectionFactory().ExecuteToDataTable(select);

                foreach(DataRow dr in dt.Rows)
                {
                    var innerJoin = new InnerJoin();

                    innerJoin.Id = Convert.ToInt32(dr["Id"]);
                    innerJoin.Classe = new ClasseController().GetSingle(Convert.ToInt32(dr["IdClasse"]));
                    innerJoin.Cnpj = dr["Cnpj"].ToString();
                    innerJoin.Cpf = dr["Cpf"].ToString();
                    innerJoin.DataHora = dr["Data"] == DBNull.Value ? new DateTime(1900, 01, 01) : Convert.ToDateTime(dr["Data"]);
                    innerJoin.Telefone = dr["Telefone"].ToString();
                    innerJoin.Tipo = dr["Tipo"].ToString();

                    list.Add(innerJoin);

                }
            }
            catch(Exception ex)
            {
                Util.registraErro("InnerJoin", "Consulta", ex);
            }

            return list;
        }

        public InnerJoin GetSingle(int id)
        {

            return GetAll($" AND Id = {id}").FirstOrDefault();

        }

        public bool Salvar(InnerJoin innerJoin)
        {
            var salvar = "";
            if(innerJoin.Id == 0)
            {
                salvar = $"INSERT INTO InnerJoins (IdClasse, Data, Tipo, Cpf, Cnpj, Telefone) " +
                    $"VALUES ({innerJoin.Classe.Id}, '{innerJoin.DataHora}', '{innerJoin.Tipo}', '{innerJoin.Cpf}', '{innerJoin.Cnpj}', '{innerJoin.Telefone}')";
            }
            else
            {
                salvar = $"UPDATE InnerJoins SET IdClasse = {innerJoin.Classe.Id}, DataHora = '{innerJoin.DataHora}', Tipo = '{innerJoin.Tipo}', Cpf = '{innerJoin.Cpf}', Cnpj = '{innerJoin.Cnpj}', Telefone = '{innerJoin.Telefone}' " +
                    $"WHERE Id = {innerJoin.Id}";
            }

            return new ConnectionFactory().executeNonQuery(salvar, salvar.Split(' ')[0], "InnerJoin") > 0;
        }

        public bool Delete(int Id)
        {
            var delete = $"DELETE FROM InnerJoins WHERE Id = {Id}";

            return new ConnectionFactory().executeNonQuery(delete, delete.Split(' ')[0], "InnerJoin") > 0;
        }
    }
}