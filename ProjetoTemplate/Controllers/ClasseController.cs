using ProjetoTemplate.Classes;
using ProjetoTemplate.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProjetoTemplate.Controllers
{
    public class ClasseController
    {
        public List<Classe> GetAll(string filtro = "")
        {
            List<Classe> list = new List<Classe>();

            var select = $"SELECT * FROM Classes WHERE (1=1) {filtro}";

            try
            {
                DataTable dt = new ConnectionFactory().ExecuteToDataTable(select);

                foreach(DataRow dr in dt.Rows)
                {
                    var classe = new Classe();

                    classe.Id = Convert.ToInt32(dr["Id"]);
                    classe.Texto = dr["Texto"].ToString();
                    classe.Data = dr["Data"] == DBNull.Value ? new DateTime(1900,01,01) : Convert.ToDateTime(dr["Data"]);
                    classe.Valor = Convert.ToDouble(dr["Valor"]);
                    classe.Booleano = Convert.ToBoolean(dr["Booleano"]);

                    list.Add(classe);

                }
            }
            catch(Exception ex)
            {
                Util.registraErro("Classe", "Consulta", ex);
            }

            return list;

        }

        public Classe GetSingle(int Id)
        {
            return GetAll($" AND Id = {Id}").FirstOrDefault();
            
        }


        public bool Salvar(Classe classe)
        {
            var salvar = "";
            if(classe.Id == 0)
            {
                salvar = $"INSERT INTO Classes (Texto, Data, Valor, Booleano) " +
                    $"VALUES ('{classe.Texto}', '{classe.Data}', {classe.Valor.ToString().Replace(",", ".")}, {(classe.Booleano ? 1 : 0)})";
            }
            else
            {
                salvar = $"UPDATE Classes SET Texto = '{classe.Texto}', Data = '{classe.Data}', Valor = {classe.Valor.ToString().Replace(",", ".")}, Booleano = {(classe.Booleano ? 1 : 0)} " +
                $"WHERE Id = {classe.Id}";

            }

            return new ConnectionFactory().executeNonQuery(salvar, salvar.Split(' ')[0], "Classe") > 0;

        }

        public bool Delete(int Id)
        {
            var delete = $"DELETE FROM Classes WHERE Id = {Id}";

            return new ConnectionFactory().executeNonQuery(delete, delete.Split(' ')[0], "Classe") > 0;
        }
    }
}