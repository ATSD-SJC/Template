using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoTemplate.Models
{
    public class Endereco
    {
        public string Cep { get; set; }
        public string logradouro { get; set; }
        public string tipo_logradouro { get; set; }
        public string bairro { get; set; }
        public Cidade cidade { get; set; }
        public string uf { get; set; }
    }
}