using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoTemplate.Models
{
    public class Classe
    {
        
        public int Id { get; set; }

        public string Texto { get; set; }

        public DateTime Data { get; set; }

        public double Valor { get; set; }

        public bool Booleano { get; set; }
    }
}