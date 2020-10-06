using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoTemplate.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Abreviacao { get; set; }
        public List<Cidade> Cidades { get; set; }
    }
}