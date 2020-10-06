using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SegvapComercial
{
    public class Auditoria
    {
        public int Id { get; set; }
      //  public Usuario Usuario { get; set; }
        public String Pagina { get; set; }
        public String Acao { get; set; }
        public DateTime DataHora { get; set; }
        public String Modulo { get; set; }
        public String Descricao { get; set; }
    }
}
