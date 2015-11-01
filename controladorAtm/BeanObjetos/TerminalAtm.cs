using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace controladorAtm.BeanObjetos
{
    public class TerminalAtm
    {
        public string ipTerminal{get;set;} /*Direccion ip del terminal*/
        public string ubicacionTerminal { get; set; }/*Ubicacion o direccion terminal*/
        public string codigoTerminal { get; set; } /*Codigo del terminal*/
        public int id_terminal { get; set; } /*Identificador en la base de datos del terminal*/

        public TerminalAtm()
        {
            this.ipTerminal = string.Empty;
            this.ubicacionTerminal = string.Empty;
            this.codigoTerminal = string.Empty;
        }

        public TerminalAtm(string ipTerminal,string ubicacionTerminal,string codigoTerminal) {
            this.ipTerminal = ipTerminal;
            this.ubicacionTerminal = ubicacionTerminal;
            this.codigoTerminal = codigoTerminal;
        }

    }
}
