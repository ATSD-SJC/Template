using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoTemplate.Models
{
    public class InnerJoin
    {
        public int Id { get; set; }
        public Classe Classe { get; set; }
        public DateTime DataHora { get; set; }
        public string Tipo { get; set; }
        public string Cpf { get; set; }
        public string Cnpj { get; set; }
        public string Telefone { get; set; }

    }
}